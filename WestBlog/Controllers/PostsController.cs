using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WestBlog.Models;
using PagedList;
using PagedList.Mvc;

namespace WestBlog.Controllers
{
    //***
    [RequireHttps]
    public class PostsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Posts
        public ActionResult Index(int? page, string currentFilter, string query, string category)
        {
            var posts = from p in db.Posts
                        select p;

            int pageSize = 6;
            int pageNumber = (page ?? 1);
            
            if (category != null)
            {
                posts = posts.Where(p => p.Category.Equals(category));
            }
            ViewBag.Category = category;

            if (query!= null)
            {
                page = 1;
            }
            else
            {
                query = currentFilter;
            }
            ViewBag.Query = query;

            if (!String.IsNullOrEmpty(query))
            {
                posts = posts.Where(p => p.Body.Contains(query) || 
                                        p.Title.Contains(query) || 
                                     p.Category.Contains(query) || 
                      p.Comments.Any(c=>c.Body.Contains(query)) || 
                      p.Comments.Any(c=>c.Author.DisplayName.Contains(query)));
            }

            return View(posts.Where(p=>p.Published== true).OrderByDescending(p => p.Created).ToPagedList(pageNumber, pageSize));
        }

        [Authorize(Roles ="Admin")]  //You can add this above the controller level (at ***) to apply it to all actions in the class
                                     // Can be layered. Ex: [Authorize] (all logged in users) at class level and
                                     //                     [Authorize(Roles ="Admin, Moderator")] individual controller level
        public ActionResult Admin()
        {
            return View(db.Posts.OrderByDescending(p => p.Created).ToList());
        }

        // GET: Posts/Details/slug
        public ActionResult Details(string slug)
        {
            if (String.IsNullOrWhiteSpace(slug))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            //List<Models.Comment> AllComments = new List<Models.Comment>(db.Comments);
            //List<Models.Comment> PublishedComments = new List<Models.Comment>();
            //for (var i = 0;i< AllComments.Count;i++)
            //{
            //    if (AllComments[i].Published.Equals(true))
            //    {
            //        PublishedComments.Add(AllComments[i]);
            //    }
            //    System.Diagnostics.Debug.WriteLine(PublishedComments);
            //}

            Post post = db.Posts.Include("Comments").FirstOrDefault(p=>p.Slug == slug);
            if (post == null)
            {
                return HttpNotFound();
            }
            return View(post);
        }

        // GET: Posts/Create
        [Authorize(Roles ="Admin")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Posts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Title,Body,MediaURL,Category")] Post post, HttpPostedFileBase image)
        {
            if (ModelState.IsValid)
            {
                var Slug = StringUtilities.URLFriendly(post.Title);

                if (String.IsNullOrWhiteSpace(Slug))
                {
                    ModelState.AddModelError("Title", "Invalid Title");
                    return View(post);
                }
                if (db.Posts.Any(p => p.Slug == Slug))
                {
                    ModelState.AddModelError("Title", "The title must be unique.");
                    return View(post);
                }

                post.Published = true;
                post.Slug = Slug;
                post.Created = DateTimeOffset.Now; //.ToString("D");


                if (ImageUploadValidator.IsWebFriendlyImage(image))
                {
                    var fileName = Path.GetFileName(image.FileName);
                    image.SaveAs(Path.Combine(Server.MapPath("~/Images/"), fileName));
                    post.MediaURL = "~/Images/" + fileName;
                }

                db.Posts.Add(post);
                db.SaveChanges();

                return RedirectToAction("Index");
            }           
            return View(post);
        }

        // GET: Posts/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = db.Posts.Find(id);
            if (post == null)
            {
                return HttpNotFound();
            }

            return View(post);  //View(post) also stores whatever is in 'post' so that if there is an error, the information is saved
        }

        // POST: Posts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Created,Updated,Title,Body,MediaURL,Category,Published,Slug")] Post post, HttpPostedFileBase image, string Submit)
        {
            if (ModelState.IsValid)
            {
                post.Updated = System.DateTimeOffset.Now;
                var slug = post.Slug;

                //TO PREVENT NULLS BEING INSERTED FOR NON-MODIFIED VALUES, either:

                //Set each modified property to true and save changes:
                //db.Posts.Attach(post);
                //db.Entry(post).Property(p => p.Body).IsModified = true;
                //db.Entry(post).Property("Title").IsModified = true;

                //OR replace the entire record, but hide the elements in the form that are not being modified.

                if (ImageUploadValidator.IsWebFriendlyImage(image))
                {
                    var fileName = Path.GetFileName(image.FileName);
                    image.SaveAs(Path.Combine(Server.MapPath("~/Images/"), fileName));
                    post.MediaURL = "~/Images/" + fileName;
                }

                db.Entry(post).State = EntityState.Modified;
                db.SaveChanges();

                if (Submit == "Save and go to Admin page")
                {
                    return RedirectToAction("Admin");
                }
                else if (Submit == "Save and go to post")
                {
                    return RedirectToAction("Details", "Posts", new { slug = slug });
                }
            }
            return View(post); 
        }


        // GET: Posts/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = db.Posts.Find(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            return View(post);
        }

        // POST: Posts/Delete/5
        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
            //The ActionResult has a different name here because we can only have one action with a certain parameter (here, id). By assigning the action name in the HttpPost, we keep the same URL action name but call a different function.
        {
            Post post = db.Posts.Find(id);
            db.Posts.Remove(post);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

    }
}

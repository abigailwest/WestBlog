﻿using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WestBlog.Models;

namespace WestBlog.Controllers
{
    public class CommentsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: /Comments - All Comments
        [Authorize(Roles ="Admin, Moderator")]
        public ActionResult Index()
        {
            return View(db.Comments.OrderBy(c=>c.PostId).ToList());
        }

        // POST: Comments/Create
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include ="Id, PostId, AuthorId, Body, Created")] Comment comment)
        {
            var slug = db.Posts.Find(comment.PostId).Slug;

            if (ModelState.IsValid)
            {
                comment.Created = DateTimeOffset.Now;
                comment.AuthorId = User.Identity.GetUserId();

                db.Comments.Add(comment);
                db.SaveChanges();

                return RedirectToAction("Details", "Posts", new { slug = slug });
            }
            return RedirectToAction("Details", "Posts", new { slug = slug });
        }

        //GET: Comments/Edit/5
        [Authorize(Roles ="Admin, Moderator")]
        public ActionResult Edit (int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult
                    (HttpStatusCode.BadRequest);
            }
            Comment comment = db.Comments.Find(id);
            if (comment == null)
            {
                return HttpNotFound();
            }
            return View(comment);
        }
        //POST: Comments/Edit/5
        [Authorize(Roles = "Admin, Moderator")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id, PostId, AuthorId, Body, Created")]Comment comment)
        {
            var slug = db.Posts.Find(comment.PostId).Slug;
            if (ModelState.IsValid)
            {
                db.Entry(comment).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Details", "Posts", new { slug = slug });
            }
            return View(comment);
        }


    }
}
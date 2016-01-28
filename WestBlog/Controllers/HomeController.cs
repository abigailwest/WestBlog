using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WestBlog.Models;

namespace WestBlog.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        //GET
        public ActionResult Contact()
        {
            return View();
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Contact(ContactMessage contact)
        {
            var es = new EmailService();
            var msg = new IdentityMessage();
            msg.Destination = ConfigurationManager.AppSettings["abigailwwest@gmail.com"];
            msg.Body = "You have been sent a message from " + contact.Name + "(" + contact.Email + ") with the following contents. \n\n" + contact.Message;
            msg.Subject = "Message received through Words from the West";
            es.SendAsync(msg);

            //ViewBag.Message = "Your message was sent successfully. Thank you!";
            return RedirectToAction("Index", "Post");
        }
    }
}
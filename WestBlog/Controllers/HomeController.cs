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
    [RequireHttps]
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
            msg.Destination = ConfigurationManager.AppSettings["ContactEmail"];
            msg.Body = "You have been sent a message from " + contact.Name + " (" + contact.Email + ") with the following contents. <br/><br/>\"" + contact.Message + "\"";
            msg.Subject = "Message received through Words from the West";
            es.SendAsync(msg);

            //ViewBag.Message = "Your message was sent successfully. Thank you!";
            return Redirect("http://awest.azurewebsites.net");
        }
    }
}
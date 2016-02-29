namespace WestBlog.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using System.Web.Security;
    internal sealed class Configuration : DbMigrationsConfiguration<WestBlog.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(WestBlog.Models.ApplicationDbContext context)
        {
            var roleManager = new RoleManager<IdentityRole>(
                new RoleStore<IdentityRole>(context));

            if (!context.Roles.Any(r => r.Name == "Admin"))
                roleManager.Create(new IdentityRole { Name = "Admin" });

            var userManager = new UserManager<ApplicationUser>( new UserStore<ApplicationUser>(context));

            if (!context.Users.Any(u => u.Email == "abigailwwest@gmail.com"))
            {
                userManager.Create(new ApplicationUser
                {
                    UserName = "abigailwwest@gmail.com",
                    Email = "abigailwwest@gmail.com", //Entity Framework assumes username and email are the same in order to have a unique field
                    DisplayName = "Abigail West"
                }, "CFBlog!");
            }

            var userId = userManager.FindByEmail("abigailwwest@gmail.com").Id;
            userManager.AddToRole(userId, "Admin");

            //FOR MODERATOR

            if (!context.Roles.Any(r => r.Name == "Moderator"))
                roleManager.Create(new IdentityRole { Name = "Moderator" });

            if (!context.Users.Any(u => u.Email == "moderator@coderfoundry.com"))
            {
                userManager.Create(new ApplicationUser
                {
                    UserName = "moderator@coderfoundry.com",
                    Email = "moderator@coderfoundry.com", //Entity Framework assumes username and email are the same in order to have a unique field
                    DisplayName = "CF Moderator"
                }, "Password-1");
            }

            userId = userManager.FindByEmail("moderator@coderfoundry.com").Id;
            userManager.AddToRole(userId, "Moderator");

            //GUEST ADMIN
            if (!context.Roles.Any(r => r.Name == "GuestAdmin"))
                roleManager.Create(new IdentityRole { Name = "GuestAdmin" });

            if (!context.Users.Any(u=>u.Email == "guest@wordsfromthewest.com"))
            {
                userManager.Create(new ApplicationUser
                {
                    UserName = "guest@wordsfromthewest.com",
                    Email = "guest@wordsfromthewest.com",
                    DisplayName = "Guest Admin"
                }, "Password-1");
            }

            userId = userManager.FindByEmail("guest@wordsfromthewest.com").Id;
            userManager.AddToRole(userId, "GuestAdmin");


        }
    }
}

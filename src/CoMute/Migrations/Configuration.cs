namespace CoMute.Web.Migrations
{
    using CoMute.Web.Models;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<CoMute.Web.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(CoMute.Web.Models.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            var manager = new UserManager<User>(new UserStore<User>(new ApplicationDbContext()));

            var user = new User()
            {
                UserName = "taiseer.joudeh@mymail.com",
                Email = "taiseer.joudeh@mymail.com",
                EmailConfirmed = true,
                Name = "Taiseer",
                Surname = "Joudeh"
            };

            manager.Create(user, "MySuperP@ssword!");
        }
    }
}


using RoleManagementApp;
namespace RoleManagementApp.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using RoleManagementApp.Models;
    using System;
    using System.Configuration;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<RoleManagementApp.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(RoleManagementApp.Models.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.

            #region Seed Roles
            var roleManager = new RoleManager<IdentityRole>(
                new RoleStore<IdentityRole>(context));

            var startupRoles = ConfigurationManager.AppSettings["startupRoles"].Split(';');


            foreach (var role in startupRoles)
                roleManager.Create(new IdentityRole { Name = role });
            #endregion


            #region Seed the Users
            string adminPassword = ConfigurationManager.AppSettings["adminPassword"];
            string adminEmail = ConfigurationManager.AppSettings["adminEmail"];
            string adminRole = ConfigurationManager.AppSettings["adminRole"];
            string adminUser = ConfigurationManager.AppSettings["adminUserName"];

            var userManager = new   ApplicationUserManager(new UserStore<ApplicationUser>(context));

            var result = userManager.Create(new ApplicationUser
            {
                UserName= adminUser,
                Email = adminEmail

            },adminPassword);

            //Add AdminRole 
            if (result.Succeeded)
                userManager.AddToRole(userManager.FindByName(adminUser).Id, adminRole);


            #endregion

            base.Seed(context);


        }
    }
}

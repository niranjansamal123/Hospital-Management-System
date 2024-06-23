using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Hospital_Management_System.Models;

namespace Hospital_Management_System.Helpers
{
    public class RoleActions
    {
        public void CreateAdmin()
        {
            using (var context = new ApplicationDbContext())
            {
                var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
                var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

                // Create Admin role if it doesn't exist
                if (!roleManager.RoleExists("Admin"))
                {
                    var role = new IdentityRole { Name = "Admin" };
                    roleManager.Create(role);
                }

                // Create Admin user if it doesn't exist
                var user = userManager.FindByName("admin@admin.com");
                if (user == null)
                {
                    user = new ApplicationUser { UserName = "admin@admin.com", Email = "admin@admin.com" };
                    var adminUserResult = userManager.Create(user, "Password123!");

                    // Assign user to Admin role
                    if (adminUserResult.Succeeded)
                    {
                        userManager.AddToRole(user.Id, "Admin");
                    }
                }
            }
        }
    }
}
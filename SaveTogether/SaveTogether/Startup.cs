using Microsoft.Owin;
using Owin;
using SaveTogether.Models;
using SaveTogether.DAL.Context;
using SaveTogether.DAL.Entities;
using Microsoft.Owin.Security.Cookies;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;

[assembly: OwinStartupAttribute(typeof(SaveTogether.Startup))]
namespace SaveTogether
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.CreatePerOwinContext<SaveTogetherContext>(SaveTogetherContext.Create);
            app.CreatePerOwinContext<AuthorizedPersonManager>(AuthorizedPersonManager.Create);
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Account/Login"),
            });

            createRolesAndUsers();

        }

        public void createRolesAndUsers()
        {
            SaveTogetherContext context = new SaveTogetherContext();

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var userManager = new UserManager<Administrator>(new UserStore<Administrator>(context));

            if (!roleManager.RoleExists("Admin"))
            {
 
                var role = new IdentityRole();
                role.Name = "Admin";
                roleManager.Create(role);

                var user = new Administrator();
                user.UserName = "Admin";
                user.Email = "admin@gmail.com";

                string userPWD = "admin123";

                var chkUser = userManager.Create(user, userPWD);

                if (chkUser.Succeeded)
                {
                    var result1 = userManager.AddToRole(user.Id, "Admin");

                }
            }
  
            if (!roleManager.RoleExists("Customer"))
            {
                var role = new IdentityRole();
                role.Name = "Customer";
                roleManager.Create(role);

            }
        }
    }
}

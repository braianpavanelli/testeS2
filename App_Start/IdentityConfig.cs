using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TesteS2IT.Models;

namespace TesteS2IT.App_Start
{
    public class IdentityConfig
    {
        public void Configuration(IAppBuilder app)
        {
            app.CreatePerOwinContext(() => new DBContext());
            app.CreatePerOwinContext<AppUserManager>(AppUserManager.Create);
            app.CreatePerOwinContext<RoleManager<AppRole>>((options, context) =>
                new RoleManager<AppRole>(
                    new RoleStore<AppRole>(context.Get<DBContext>())));

            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Home/Login"),
            });
            this.CreateDefaultRolesAndUsers();
        }

        private void CreateDefaultRolesAndUsers()
        {
            DBContext dbContext = new DBContext();

            var roleManager = new RoleManager<AppRole>(new RoleStore<AppRole>(dbContext));
            var userManager = new AppUserManager(new UserStore<AppUser>(dbContext));



            if (!roleManager.RoleExists("Administrators"))
            {
                var role = new AppRole();
                role.Name = "Administrator";
                roleManager.Create(role);

                var user = new AppUser();
                user.UserName = "administrator";
                user.Email = "administrator@example.com";

                string pass = "system";

                var created = userManager.Create(user, pass);
                if (created.Succeeded)
                {
                    var assigned = userManager.AddToRole(user.Id, role.Name);
                }
            }
        }
    }
}
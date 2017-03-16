using Microsoft.Owin;
using Owin;
using SaveTogether.Models;
using SaveTogether.DAL.Context;
using Microsoft.Owin.Security.Cookies;
using Microsoft.AspNet.Identity;
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

        }
    }
}

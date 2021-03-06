﻿using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;
using ProjectPortfolioRiskManager.Domain.Infrastructure;

namespace ProjectPortfolioRiskManager
{
    public class IdentityConfig
    {
        public void Configuration(IAppBuilder app)
        {
            app.CreatePerOwinContext(EFDbContext.Create);
            app.CreatePerOwinContext<EFUserManager>(EFUserManager.Create);
            app.CreatePerOwinContext<EFRoleManager>(EFRoleManager.Create);

            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/"),
            });
        }
    }
}
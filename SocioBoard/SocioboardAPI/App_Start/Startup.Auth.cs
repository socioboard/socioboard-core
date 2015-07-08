using System;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;
using System.Threading.Tasks;
using Api.Socioboard.Helper.UserManager;

namespace Api.Socioboard.App_Start
{
    public partial class Startup
    {
        // For more information on configuring authentication, please visit http://go.microsoft.com/fwlink/?LinkId=301864
        public void ConfigureAuth(IAppBuilder app)
        {
            string folderStorage = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "_Storage");

            // Configure the db context, user manager and signin manager to use a single instance per request
            // app.CreatePerOwinContext(ApplicationDbContext.Create);
          //  app.CreatePerOwinContext<UserManager>(() => new UserManager(new UserStore()));
           // app.CreatePerOwinContext<RoleManager>(() => new RoleManager(new RoleStore(folderStorage)));
            app.CreatePerOwinContext<SignInService>((options, context) => new SignInService(context.GetUserManager<UserManager>(), context.Authentication));

            // Enable the application to use a cookie to store information for the signed in user
            // and to use a cookie to temporarily store information about a user logging in with a third party login provider
            // Configure the sign in cookie
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Index/Index"),
                Provider = new CookieAuthenticationProvider
                {
                    // Enables the application to validate the security stamp when the user logs in.
                    // This is a security feature which is used when you change a password or add an external login to your account.  
                    OnValidateIdentity = SecurityStampValidator.OnValidateIdentity<UserManager, User, Guid>(
                    validateInterval: TimeSpan.FromMinutes(30),
                    regenerateIdentityCallback: (manager, user) =>
                    {
                        var userIdentity = manager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);
                        return (userIdentity);
                    },
                    getUserIdCallback: (id) => (Guid.Parse(id.GetUserId()))
                    )
                }
            });
        }
    }
}
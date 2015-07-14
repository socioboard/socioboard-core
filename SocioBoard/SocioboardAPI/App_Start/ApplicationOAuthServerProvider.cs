using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Add Usings:
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OAuth;
using System.Security.Claims;
using System.Web;
using System.Security.Principal;
using Api.Socioboard.Model;
using Api.Socioboard.Helper;
using System.Web.Script.Serialization;

namespace Api.Socioboard.App_Start
{
    public class ApplicationOAuthServerProvider : OAuthAuthorizationServerProvider
    {
        private readonly string _publicClientId;

        public ApplicationOAuthServerProvider(string publicClientId)
        {
            if (publicClientId == null)
            {
                throw new ArgumentNullException("publicClientId");
            }

            _publicClientId = publicClientId;
        }

        //public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        //{
        //    context.OwinContext.Response.Headers.Add("access-control-allow-origin", new[] { "*" });

        //    //we check if the passed username and password are correct.
        //    if (context.UserName == "user" && context.Password == "pwd")
        //    {
        //        System.Security.Claims.ClaimsIdentity ci = new System.Security.Claims.ClaimsIdentity("ci");
        //        //this indicates that user is valid one and can be issued a token.
        //        //it has several overloads ,you can take what fits for you.I have used it with ClaimsIdentity
        //        context.Validated(ci);
        //    }
        //    else
        //    {
        //        // a custom error message can be returned to client before rejecting the request.
        //        context.SetError("Incorrect Credentials");
        //        context.Rejected();
        //    }
        //    return ;
        //}


        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            string allowedOrigin = "*";

            context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { allowedOrigin });
            context.OwinContext.Response.Headers.Add("Access-Control-Allow-Headers", new[] { "Content-Type" });

            
                UserRepository userrepo = new UserRepository();
                try {
                    Domain.Socioboard.Domain.User user = userrepo.GetUserInfo(context.UserName, Utility.MD5Hash(context.Password));
                    if (user != null)
                    {

                        Api.Socioboard.Helper.UserManager.User apiUser = new Api.Socioboard.Helper.UserManager.User();
                        apiUser.UserName = user.UserName;
                        apiUser.EmailId = user.EmailId;
                        apiUser.Id = user.Id;

                        ClaimsIdentity identity = new ClaimsIdentity("User");

                        identity.AddClaim(new Claim(ClaimTypes.Name, apiUser.Id.ToString()));
                        
                        //foreach (string claim in user.Claims)
                        //{
                        //    identity.AddClaim(new Claim("Claim", claim));
                        //}

                        var ticket = new AuthenticationTicket(identity, null);
                        context.Validated(ticket);
                    }
                    else
                    {
                        context.SetError("Incorrect Credentials");
                        context.Rejected();
                    }

                }catch(Exception e){}
               
        }

      

        public override Task TokenEndpoint(OAuthTokenEndpointContext context)
        {
            foreach (KeyValuePair<string, string> property in context.Properties.Dictionary)
            {
                context.AdditionalResponseParameters.Add(property.Key, property.Value);
            }

            return Task.FromResult<object>(null);
        }

        public override Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            // Resource owner password credentials does not provide a client ID.
            if (context.ClientId == null)
            {
                context.Validated();
            }

            return Task.FromResult<object>(null);
        }

        public override Task ValidateClientRedirectUri(OAuthValidateClientRedirectUriContext context)
        {
            if (context.ClientId == _publicClientId)
            {
                Uri expectedRootUri = new Uri(context.Request.Uri, "/");

                if (expectedRootUri.AbsoluteUri == context.RedirectUri)
                {
                    context.Validated();
                }
            }

            return Task.FromResult<object>(null);
        }

        public static AuthenticationProperties CreateProperties(string userName)
        {
            IDictionary<string, string> data = new Dictionary<string, string>
{
{ "userName", userName }
};
            return new AuthenticationProperties(data);
        }
    }
}
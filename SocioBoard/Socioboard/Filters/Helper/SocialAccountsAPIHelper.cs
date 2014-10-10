using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Domain.Socioboard.Domain;
using Socioboard.Api;
using System.Web.Script.Serialization;

namespace Socioboard.Helper
{
    public class SocialAccountsAPIHelper
    {
        public static FacebookAccount GetFacebookAccount(string FbUserId)
        {
            Api.FacebookAccount.FacebookAccount objApiFacebookAccount = new Api.FacebookAccount.FacebookAccount();

            FacebookAccount objDomainFacebookAccount = (FacebookAccount)new JavaScriptSerializer().Deserialize(objApiFacebookAccount.getUserDetails(FbUserId), typeof(FacebookAccount));

            return objDomainFacebookAccount;
        }
    }
}
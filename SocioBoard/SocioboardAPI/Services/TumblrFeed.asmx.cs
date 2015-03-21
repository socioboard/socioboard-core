using Api.Socioboard.Helper;
using Api.Socioboard.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Script.Services;
using System.Web.Services;

namespace Api.Socioboard.Services
{
    /// <summary>
    /// Summary description for Group
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [ScriptService]
    public class TumblrFeed : System.Web.Services.WebService
    {
        TumblrFeedRepository objTumblrFeedRepository = new TumblrFeedRepository();
        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string GetAllTumblrFeedOfUsers(string UserId, string ProfileId)
        {
            try
            {
                List<Domain.Socioboard.Domain.TumblrFeed> lstTumblrFeed = objTumblrFeedRepository.GetFeedsOfProfile(ProfileId, Guid.Parse(UserId));
                return new JavaScriptSerializer().Serialize(lstTumblrFeed);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return "Something Went Wrong";
            }
        }

        //
        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string GetAllTumblrFeedOfUsersWithRange(string UserId, string ProfileId, string noOfDataToSkip)
        {
            try
            {
                List<Domain.Socioboard.Domain.TumblrFeed> lstTumblrFeed = objTumblrFeedRepository.GetFeedsOfProfileWithRange(ProfileId, Guid.Parse(UserId), Convert.ToInt32(noOfDataToSkip));
                return new JavaScriptSerializer().Serialize(lstTumblrFeed);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return "Something Went Wrong";
            }
        }
    }
}

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

    public class TwitterFeed : System.Web.Services.WebService
    {
        TwitterFeedRepository objTwitterFeedRepository = new TwitterFeedRepository();
        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string GetAllTwitterFeedsByUserIdAndProfileId(string UserId, string ProfileId)
        {
            try
            {
                List<Domain.Socioboard.Domain.TwitterFeed> lstTwitterFeed = objTwitterFeedRepository.getAllTwitterFeedOfUsers(Guid.Parse(UserId), ProfileId);
                return new JavaScriptSerializer().Serialize(lstTwitterFeed);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return "Something Went Wrong";
            }
        }


        //getAllTwitterFeedOfUsers
        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string getAllTwitterFeedOfUsers(string UserId, string ProfileId)
        {
            try
            {
                List<Domain.Socioboard.Domain.TwitterFeed> lstTwitterFeed = objTwitterFeedRepository.getAllTwitterFeedOfUsers(Guid.Parse(UserId), ProfileId);
                return new JavaScriptSerializer().Serialize(lstTwitterFeed);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return "Something Went Wrong";
            }
        }



        public string TwitterInboxMessagecount(string userid, string profileid, string days)
        {
            int daycount = Convert.ToInt32(days);
            List<Domain.Socioboard.Domain.TwitterFeed> lstfeed = new List<Domain.Socioboard.Domain.TwitterFeed>();

            try
            {

                lstfeed = objTwitterFeedRepository.getAllInboxMessagesByProfileid(Guid.Parse(userid), profileid, daycount);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
            }

            return lstfeed.Count.ToString();

        }

    }
}

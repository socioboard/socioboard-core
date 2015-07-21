using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Script.Services;
using System.Web.Services;
using Domain.Socioboard.Domain;

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
    public class InstagramFeed : System.Web.Services.WebService
    {

        InstagramFeedRepository objInstagramFeedRepository = new InstagramFeedRepository();
        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string GetLinkedInFeeds(string UserId, string LinkedInId)
        {
            try
            {
                List<Domain.Socioboard.Domain.InstagramFeed> lstInstagramFeed = objInstagramFeedRepository.getAllInstagramFeedsOfUser(Guid.Parse(UserId), LinkedInId);
                return new JavaScriptSerializer().Serialize(lstInstagramFeed);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return new JavaScriptSerializer().Serialize("Please Try Again");
            }
        }


        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string GetFeedsOfProfileWithRange(string UserId, string LinkedInId, string noOfDataToSkip, string noOfDataFromTop)
        {
            try
            {
                List<Domain.Socioboard.Domain.InstagramFeed> lstInstagramFeed = objInstagramFeedRepository.getAllInstagramFeedsOfUser(Guid.Parse(UserId), LinkedInId, Convert.ToInt32(noOfDataToSkip), Convert.ToInt32(noOfDataFromTop));
                return new JavaScriptSerializer().Serialize(lstInstagramFeed);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return new JavaScriptSerializer().Serialize("Please Try Again");
            }
        }
    }
}

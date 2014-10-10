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
    public class LinkedInFeed : System.Web.Services.WebService
    {
        LinkedInFeedRepository objLinkedInFeedRepository = new LinkedInFeedRepository();
        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string GetLinkedInFeeds(string UserId, string LinkedInId)
        {
            try
            {
                List<Domain.Socioboard.Domain.LinkedInFeed> lstlinkedinfeeds = objLinkedInFeedRepository.getAllLinkedInFeedsOfUser(Guid.Parse(UserId), LinkedInId);
                return new JavaScriptSerializer().Serialize(lstlinkedinfeeds);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return new JavaScriptSerializer().Serialize("Please Try Again");
            }
        }
    }
}

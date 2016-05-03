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
    /// Summary description for WordpressFeeds
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class WordpressFeeds : System.Web.Services.WebService
    {
        WordpressFeedsRepository objWordpressFeedsRepository = new WordpressFeedsRepository();
        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string GetAllSiteFeedBySiteId(string UserId, string SiteId)
        {
            List<Domain.Socioboard.Domain.WordpressFeeds> lstWordpressFeeds = objWordpressFeedsRepository.GetAllSiteFeedBySiteId(Guid.Parse(UserId),SiteId);
            return new JavaScriptSerializer().Serialize(lstWordpressFeeds);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Script.Serialization;
using System.Web.Script.Services;

namespace Api.Socioboard.Services
{
    /// <summary>
    /// Summary description for WordpressSites
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class WordpressSites : System.Web.Services.WebService
    {
        WordpressSitesRepository objWordpressSitesRepository = new WordpressSitesRepository();
        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }
        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string GetAllSitesByWPUserId(string userid, string wpuserid)
        {
            List<Domain.Socioboard.Domain.WordpressSites> lstWordpressSites = objWordpressSitesRepository.GetAllSitesFromWPUserId(Guid.Parse(userid), wpuserid);
            return new JavaScriptSerializer().Serialize(lstWordpressSites);
 
        }
        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string GetSiteBySiteId(string UserId, string SiteId)
        {
            Domain.Socioboard.Domain.WordpressSites _WordpressSites = objWordpressSitesRepository.GetSiteBySiteId(Guid.Parse(UserId),SiteId);
            return new JavaScriptSerializer().Serialize(_WordpressSites);
        }

    }
}

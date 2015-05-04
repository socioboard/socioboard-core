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
    public class LinkedinMessage : System.Web.Services.WebService
    {
        LinkedInMessageRepository objLinkedInMessageRepository = new LinkedInMessageRepository();

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string GetLinkedInMessages(string UserId, string LinkedInId, int count)
        {
            try
            {
                List<Domain.Socioboard.Domain.LinkedInMessage> lstlinkedinmessages = objLinkedInMessageRepository.getLinkedInMessageDetail(LinkedInId, count.ToString(), Guid.Parse(UserId));
                return new JavaScriptSerializer().Serialize(lstlinkedinmessages);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return new JavaScriptSerializer().Serialize("Please Try Again");
            }
        }
    }
}

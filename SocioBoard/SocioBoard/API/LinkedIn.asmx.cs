using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using SocioBoard.Model;
using SocioBoard.Domain;
using System.Web.Script.Serialization;
using System.Web.Script.Services;

namespace SocialSuitePro.API
{
    /// <summary>
    /// Summary description for LinkedIn
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
     [ScriptService]
    public class LinkedIn : System.Web.Services.WebService
    {

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string UserInformation(string UserId, string LinkedinId)
        {
            try
            {

                Guid Userid = Guid.Parse(UserId);
                LinkedInAccountRepository linkedinaccrepo = new LinkedInAccountRepository();
                LinkedInAccount LinkedAccount = linkedinaccrepo.getUserInformation(Userid, LinkedinId);
                return new JavaScriptSerializer().Serialize(LinkedAccount);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return new JavaScriptSerializer().Serialize("Please Try Again");
            }
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string GetNetworkUpdates(string UserId, string LinkedInId)
        {
            try    
            {

                Guid userid = Guid.Parse(UserId);
                LinkedInAccountRepository linkedinAccountRepo = new LinkedInAccountRepository();
                LinkedInAccount linkedAcc = linkedinAccountRepo.getUserInformation(userid, LinkedInId);
                LinkedInFeedRepository linkedinfeedrepo = new LinkedInFeedRepository();
                List<LinkedInFeed> lstlinkedinfeeds = linkedinfeedrepo.getAllLinkedInFeedsOfProfile(LinkedInId);
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

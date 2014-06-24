using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Script.Services;
using System.Web.Script.Serialization;
using SocioBoard.Model;
using SocioBoard.Domain;

namespace WooSuite.API
{
    /// <summary>
    /// Summary description for Instagram
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
     [ScriptService]
    public class Instagram : System.Web.Services.WebService
    {

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string UserInformation(string UserId, string InstagramId)
        {
            try
            {

                Guid Userid = Guid.Parse(UserId);
                InstagramAccountRepository linkedinaccrepo = new InstagramAccountRepository();
                InstagramAccount LinkedAccount = linkedinaccrepo.getInstagramAccountDetailsById(InstagramId, Userid);
                return new JavaScriptSerializer().Serialize(LinkedAccount);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return new JavaScriptSerializer().Serialize("Please Try Again");
            }
        }
    }
}

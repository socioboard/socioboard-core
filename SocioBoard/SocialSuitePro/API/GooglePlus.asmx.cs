using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Script.Services;
using SocioBoard.Model;
using SocioBoard.Domain;
using System.Web.Script.Serialization;

namespace SocialSuitePro.API
{
    /// <summary>
    /// Summary description for GooglePlus
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
     [ScriptService]
    public class GooglePlus : System.Web.Services.WebService
    {

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string UserInformation(string UserId, string GooglePlusId)
        {
            try
            {

                Guid Userid = Guid.Parse(UserId);
                GooglePlusAccountRepository linkedinaccrepo = new GooglePlusAccountRepository();
                GooglePlusAccount LinkedAccount = linkedinaccrepo.getGooglePlusAccountDetailsById(GooglePlusId, Userid);
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

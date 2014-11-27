using Api.Socioboard.Helper;
using Api.Socioboard.Model;
using System;
using System.Collections;
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
    public class GooglePlusAccount : System.Web.Services.WebService
    {
        GooglePlusAccountRepository ObjGooglePlusAccountsRepo = new GooglePlusAccountRepository();

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string GetAllGooglePlusAccounts()
        {
            try
            {
                ArrayList lstGooglePlusAcc = ObjGooglePlusAccountsRepo.getAllGooglePlusAccounts();
                return new JavaScriptSerializer().Serialize(lstGooglePlusAcc);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return "Something Went Wrong";
            }
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string GetGooglePlusAccountDetailsById(string UserId, string ProfileId)
        {
            try
            {
                Domain.Socioboard.Domain.GooglePlusAccount objGpAccount = ObjGooglePlusAccountsRepo.getGooglePlusAccountDetailsById(ProfileId, Guid.Parse(UserId));
                return new JavaScriptSerializer().Serialize(objGpAccount);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return "Something Went Wrong";
            }
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string UpdateGooglePlusAccountByAdmin(string ObjGooglePlus)
        {
            Domain.Socioboard.Domain.GooglePlusAccount ObjGooglePlusAccount = (Domain.Socioboard.Domain.GooglePlusAccount)(new JavaScriptSerializer().Deserialize(ObjGooglePlus, typeof(Domain.Socioboard.Domain.GooglePlusAccount)));
            try
            {
                ObjGooglePlusAccountsRepo.updateGooglePlusUser(ObjGooglePlusAccount);
                return new JavaScriptSerializer().Serialize("Update Successfully");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return new JavaScriptSerializer().Serialize("Something went Wrong");
            }
        }
    }
}

using Api.Socioboard.Helper;
using Api.Socioboard.Model;
using log4net;
using SocioBoard.Model;
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
    public class Admin : System.Web.Services.WebService
    {
        ILog logger = LogManager.GetLogger(typeof(Admin));
        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string Login(string UserName, string Password)
        {
            try
            {
                AdminRepository AdminRepo = new AdminRepository();
                Domain.Socioboard.Domain.Admin Admin = AdminRepo.GetUserInfo(UserName, Password);
                if (Admin != null)
                {
                    return new JavaScriptSerializer().Serialize(Admin);
                }
                else
                {
                    return new JavaScriptSerializer().Serialize("Not Exist");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return null;
            }
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string ChangeAdminPassword(string NewPassword,string OldPassword, string UserName)
        {
            try
            {
                AdminRepository AdminRepo = new AdminRepository();
                int i = AdminRepo.ChangePassword(NewPassword, OldPassword,UserName);
                if (i != 0)
                {
                    return new JavaScriptSerializer().Serialize("Password Changed Successfully");
                }
                else
                {
                    return new JavaScriptSerializer().Serialize("Not Exist");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return null;
            }
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string UpdateAdminSetting(string ObjADmin)
        {
            try
            {
                AdminRepository ObjAdminRepo = new AdminRepository();
                Domain.Socioboard.Domain.Admin objadmin = (Domain.Socioboard.Domain.Admin)(new JavaScriptSerializer().Deserialize(ObjADmin, typeof(Domain.Socioboard.Domain.Admin)));
                AdminRepository.Update(objadmin);
                return new JavaScriptSerializer().Serialize("Setting Updated Successfully");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return null;
            }
        }

    }
}

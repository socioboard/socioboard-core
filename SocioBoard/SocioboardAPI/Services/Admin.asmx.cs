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
    }
}

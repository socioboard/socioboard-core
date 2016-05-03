using Api.Socioboard.Model;
using Domain.Socioboard.Domain;
using log4net;
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
    /// Summary description for AdminUserDetails
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class AdminUserDetails : System.Web.Services.WebService
    {
        UserRepository objUserRepo = new UserRepository();
        
        ILog logger = LogManager.GetLogger(typeof(Admin));
        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string GetAllUsers(string Objuser)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            serializer.MaxJsonLength = 2147483647;
            try
            {
                Domain.Socioboard.Domain.User ObjUser = (Domain.Socioboard.Domain.User)(new JavaScriptSerializer().Deserialize(Objuser, typeof(Domain.Socioboard.Domain.User)));

                if (ObjUser.UserType == "SuperAdmin")
                {

                    List<Domain.Socioboard.Domain.User> lstUser = objUserRepo.getAllUsersByAdmin();
                    if (lstUser.Count > 0)
                    {
                        return serializer.Serialize(lstUser);
                    }
                    else
                    {
                        return new JavaScriptSerializer().Serialize(new List<Domain.Socioboard.Domain.User>());
                    }
                }
                else
                {
                    return new JavaScriptSerializer().Serialize(new List<Domain.Socioboard.Domain.User>());
                }
            }
            catch (Exception ex)
            {
                logger.Error("GetAllUsers => "+ex.Message);
                Console.WriteLine(ex.StackTrace);
                return new JavaScriptSerializer().Serialize(new List<Domain.Socioboard.Domain.User>());
            }

        }


        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string GetUserById(string Id)
        {
            try
            {
                Domain.Socioboard.Domain.User user = new Domain.Socioboard.Domain.User();
                user = objUserRepo.getUsersById(Guid.Parse(Id));
                return new JavaScriptSerializer().Serialize(user);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return new JavaScriptSerializer().Serialize(new Domain.Socioboard.Domain.User());
            }


        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string UpdateUserAccount(string Id, string UserName, string EmailId, string Package, string Status,string PaymentStatus)
        {
            try
            {
                Domain.Socioboard.Domain.User objUser = new Domain.Socioboard.Domain.User();
                objUser.Id = Guid.Parse(Id);
                objUser.UserName = UserName;
                objUser.EmailId = EmailId;
                objUser.AccountType = Package;
                objUser.ActivationStatus = Status.ToString();
                objUser.PaymentStatus = PaymentStatus;
                UserRepository.UpdateAccountType(objUser);
                return new JavaScriptSerializer().Serialize("Update Successfully");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new JavaScriptSerializer().Serialize("Please Try Again");
            }

        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string DeleteUser(string Id)
        {
            
            try 
	        {
                int delete = objUserRepo.DeleteUser(Guid.Parse(Id));
                return new JavaScriptSerializer().Serialize(delete);
	        }
	        catch (Exception ex)
	        {
                Console.WriteLine(ex.StackTrace);
                return new JavaScriptSerializer().Serialize(0);
	        }
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string getAllDeletedUsers(string Objuser)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            serializer.MaxJsonLength = 2147483647;       
            try
            {

                 Domain.Socioboard.Domain.User ObjUser = (Domain.Socioboard.Domain.User)(new JavaScriptSerializer().Deserialize(Objuser, typeof(Domain.Socioboard.Domain.User)));

                 if (ObjUser.UserType == "SuperAdmin")
                 {
                     List<Domain.Socioboard.Domain.User> lstUser = objUserRepo.getAllDeletedUsersByAdmin();
                     return serializer.Serialize(lstUser);
                 }
                 else
                 {
                     return new JavaScriptSerializer().Serialize(new List<Domain.Socioboard.Domain.User>());
                 }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return new JavaScriptSerializer().Serialize(new List<Domain.Socioboard.Domain.User>());
            }
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string GetUserDataForDataTable(string user, string iDisplayLength, string iDisplayStart, string iSortCol_0, string sSortDir_0, string sSearch)
        {
            string strUser = string.Empty;
            Domain.Socioboard.Domain.User _User = (Domain.Socioboard.Domain.User)new JavaScriptSerializer().Deserialize(user, typeof(Domain.Socioboard.Domain.User));
          
            if (_User.UserType == "SuperAdmin")
            {
                int dispalyLength = Int32.Parse(iDisplayLength);
                int displayStart = Int32.Parse(iDisplayStart);
                int sortCol = Int32.Parse(iSortCol_0);
                string sortDir = sSortDir_0;
                string search = sSearch;


                Domain.Socioboard.Helper.UserDetails _UserDetails = new Domain.Socioboard.Helper.UserDetails();
                _UserDetails = objUserRepo.GetUserDataForDataTable(dispalyLength, displayStart, sortCol, sortDir, search);
                strUser = new JavaScriptSerializer().Serialize(_UserDetails);
            }
            else {
                Domain.Socioboard.Helper.UserDetails _UserDetails = new Domain.Socioboard.Helper.UserDetails();
                strUser = new JavaScriptSerializer().Serialize(_UserDetails);
            }
            
            return strUser;
        }

    }
}

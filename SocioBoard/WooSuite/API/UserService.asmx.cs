using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Script.Services;
using System.Web.Script.Serialization;
using SocioBoard.Domain;
using SocioBoard.Model;
using WooSuite.Helper;
namespace WooSuite.API
{
    /// <summary>
    /// Summary description for UserService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [ScriptService]
    public class UserService : System.Web.Services.WebService
    {

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string Login(string EmailId, string Password)
        {
            //
            try
            {
                UserRepository userrepo = new UserRepository();
                Registration regObject = new Registration();
                User user = userrepo.GetUserInfo(EmailId, regObject.MD5Hash(Password));

                if (user != null)
                {
                    return new JavaScriptSerializer().Serialize(user);
                }
                else
                {
                    return "Invalid user name or password";
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
        public string Register(string EmailId, string Password, string AccountType, string FirstName, string LastName)
        {

            try
            {
                UserRepository userrepo = new UserRepository();
                if (!userrepo.IsUserExist(EmailId))
                {
                    Registration regObject = new Registration();
                    User user = new User();
                    user.AccountType = AccountType;
                    user.EmailId = EmailId;
                    user.CreateDate = DateTime.Now;
                    user.ExpiryDate = DateTime.Now.AddMonths(1);
                    user.Password = regObject.MD5Hash(Password);
                    user.PaymentStatus = "unpaid";
                    user.ProfileUrl = string.Empty;
                    user.TimeZone = string.Empty;
                    user.UserName = FirstName + " " + LastName;
                    user.UserStatus = 1;
                    user.Id = Guid.NewGuid();
                    UserRepository.Add(user);
                    MailSender.SendEMail(user.UserName,Password, EmailId);
                    return new JavaScriptSerializer().Serialize(user);
                }
                else
                {
                    return "Email Already Exists";
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return "Something Went Wrong";
            }



        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string ChangePassword(string EmailId, string Password, string NewPassword)
        {
            try
            {
                User user = new User();
                UserRepository userrepo = new UserRepository();
                int i = userrepo.ChangePassword(NewPassword, Password, EmailId);
                if (i == 1)
                {
                    return "Password Changed Successfully";
                }
                else
                {
                    return "Invalid EmailId";
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return "Please Try Again";
            }
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string ProfilesConnected(string UserId)
        {
            try
            {

                Guid userid = Guid.Parse(UserId);
                SocialProfilesRepository socialRepo = new SocialProfilesRepository();
                List<SocialProfile> lstsocioprofile = socialRepo.getAllSocialProfilesOfUser(userid);
                //foreach(so)
                return new JavaScriptSerializer().Serialize(lstsocioprofile);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return new JavaScriptSerializer().Serialize("Please Try Again");
            }
        }



    }
}

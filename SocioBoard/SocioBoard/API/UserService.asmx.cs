using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Script.Services;
using System.Web.Script.Serialization;
using SocioBoard.Domain;
using SocioBoard.Model;
using SocialSuitePro.Helper;
namespace SocialSuitePro.API
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
                List<profileConnected> lstProfile = new List<profileConnected>();
                foreach (SocialProfile sp in lstsocioprofile)
                {
                    profileConnected pc = new profileConnected();
                    pc.Id = sp.Id;
                    pc.ProfileDate = sp.ProfileDate;
                    pc.ProfileId = sp.ProfileId;
                    pc.ProfileStatus = sp.ProfileStatus;
                    pc.ProfileType = sp.ProfileType;
                    pc.UserId = sp.UserId;
                    if (sp.ProfileType == "facebook")
                    {
                        try
                        {
                            FacebookAccountRepository objFbAccRepo = new FacebookAccountRepository();
                            FacebookAccount objFbAcc = objFbAccRepo.getUserDetails(sp.ProfileId);
                            pc.ProfileName = objFbAcc.FbUserName;
                            pc.ProfileImgUrl = "http://graph.facebook.com/" + sp.ProfileId + "/picture?type=small";
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.StackTrace);
                        }
                    }
                    else if (sp.ProfileType == "twitter")
                    {
                        try
                        {
                            TwitterAccountRepository objTwtAccRepo = new TwitterAccountRepository();
                            TwitterAccount objTwtAcc = objTwtAccRepo.getUserInfo(sp.ProfileId);
                            pc.ProfileName = objTwtAcc.TwitterScreenName;
                            pc.ProfileImgUrl = objTwtAcc.ProfileImageUrl;
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.StackTrace);
                        }
                    }
                    else if (sp.ProfileType == "instagram")
                    {
                        try
                        {
                            InstagramAccountRepository objInsAccRepo = new InstagramAccountRepository();
                            InstagramAccount objInsAcc = objInsAccRepo.getInstagramAccountById(sp.ProfileId);
                            pc.ProfileName = objInsAcc.InsUserName;
                            pc.ProfileImgUrl = objInsAcc.ProfileUrl;
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.StackTrace);
                        }
                    }
                    else if (sp.ProfileType == "linkedin")
                    {
                        try
                        {
                            LinkedInAccountRepository objLiAccRepo = new LinkedInAccountRepository();
                            LinkedInAccount objLiAcc = objLiAccRepo.getLinkedinAccountDetailsById(sp.ProfileId);
                            pc.ProfileName = objLiAcc.LinkedinUserName;
                            pc.ProfileImgUrl = objLiAcc.ProfileImageUrl;
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.StackTrace);
                        }
                    }
                    else if (sp.ProfileType == "googleplus")
                    {
                        try
                        {
                            GooglePlusAccountRepository objGpAccRepo = new GooglePlusAccountRepository();
                            GooglePlusAccount objGpAcc = objGpAccRepo.getUserDetails(sp.ProfileId);
                            pc.ProfileName = objGpAcc.GpUserName;
                            pc.ProfileImgUrl = objGpAcc.GpProfileImage;
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.StackTrace);
                        }
                    }
                    lstProfile.Add(pc);
                }
                return new JavaScriptSerializer().Serialize(lstProfile);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return new JavaScriptSerializer().Serialize("Please Try Again");
            }
        }



    }

    public class profileConnected
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string ProfileId { get; set; }
        public string ProfileType { get; set; }
        public DateTime ProfileDate { get; set; }
        public int ProfileStatus { get; set; }
        public string ProfileName { get; set; }
        public string ProfileImgUrl { get; set; }

    }
}

using Api.Socioboard.Helper;
using Api.Socioboard.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Script.Services;
using System.Web.Services;
using Domain.Socioboard.Domain;
using System.Configuration;
using log4net;
using System.Collections;
using System.Globalization;


namespace Api.Socioboard.Services
{
    /// <summary>
    /// Summary description for UserService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [ScriptService]
    public class User : System.Web.Services.WebService
    {
        ILog logger = LogManager.GetLogger(typeof(User));
        TeamRepository objTeamRepository = new TeamRepository();
        UserRepository userrepo = new UserRepository();
        GroupsRepository objGroupsRepository = new GroupsRepository();
        GroupProfileRepository objGroupProfileRepository = new GroupProfileRepository();
        TeamMemberProfileRepository objTeamMemberProfileRepository = new TeamMemberProfileRepository();
        Domain.Socioboard.Domain.TeamMemberProfile objTeamMemberProfile;

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string Login(string EmailId, string Password)
        {
            logger.Error("Checking Abhay123");
            try
            {
                UserRepository userrepo = new UserRepository();

                Domain.Socioboard.Domain.User objUser = new Domain.Socioboard.Domain.User();
                objUser = userrepo.getUserInfoByEmail(EmailId);
                if (objUser == null)
                {
                    return new JavaScriptSerializer().Serialize("Email Not Exist");
                }

                Domain.Socioboard.Domain.User user = userrepo.GetUserInfo(EmailId, Utility.MD5Hash(Password));
                if (user != null)
                {
                    if (user.UserCode == null || user.UserCode == "")
                    {
                        string code = Utility.GenerateRandomUniqueString();
                        int retint = userrepo.UpdateCode(user.Id, code);
                        user = userrepo.getUsersById(user.Id);
                    }
                    try
                    {
                        userrepo.UpdateLastLoginTime(user.Id);
                    }
                    catch { }
                    return new JavaScriptSerializer().Serialize(user);

                }
                else
                {
                    return new JavaScriptSerializer().Serialize("Not Exist");
                }
            }
            catch (Exception ex)
            {
                logger.Error("Error : " + ex.Message);
                logger.Error("Error : " + ex.StackTrace);
                Console.WriteLine(ex.StackTrace);
                return null;
            }
        }

        //[WebMethod(MessageName = "Pass First & Last Names", Description = "Pass First & Last Names")]
        //[ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        //public string Register(string EmailId, string Password, string AccountType, string FirstName, string LastName)
        //{

        //    try
        //    {
        //        //UserRepository userrepo = new UserRepository();


        //        //UserActivationRepository objUserActivation = new UserActivationRepository();

        //        if (!userrepo.IsUserExist(EmailId))
        //        {


        //            Domain.Socioboard.Domain.User user = new Domain.Socioboard.Domain.User();
        //            user.AccountType = AccountType;
        //            user.EmailId = EmailId;
        //            user.CreateDate = DateTime.Now;
        //            user.ExpiryDate = DateTime.Now.AddMonths(1);
        //            user.Password = Utility.MD5Hash(Password);
        //            user.PaymentStatus = "unpaid";
        //            user.ProfileUrl = string.Empty;
        //            user.TimeZone = string.Empty;
        //            user.UserName = FirstName + " " + LastName;
        //            user.UserStatus = 1;
        //            user.Id = Guid.NewGuid();
        //            UserRepository.Add(user);

        //            return new JavaScriptSerializer().Serialize(user);
        //        }
        //        else
        //        {
        //            //return "Email Already Exists";
        //            return new JavaScriptSerializer().Serialize("Email Already Exists");
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex.StackTrace);
        //        //return "Something Went Wrong";
        //        return new JavaScriptSerializer().Serialize("Something Went Wrong");
        //    }



        //}

        [WebMethod(MessageName = "Pass Username", Description = "Pass Username")]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string Register(string EmailId, string Password, string AccountType, string Username, string ActivationStatus = "0")
        {

            try
            {
                //UserRepository userrepo = new UserRepository();

                //Domain.Socioboard.Domain.UserActivation objUserActivation = new Domain.Socioboard.Domain.UserActivation();
                //UserActivationRepository objUserActivation = new UserActivationRepository();

                logger.Error("Register");

                if (!userrepo.IsUserExist(EmailId))
                {


                    Domain.Socioboard.Domain.User user = new Domain.Socioboard.Domain.User();
                    user.AccountType = AccountType;
                    user.EmailId = EmailId;
                    user.CreateDate = DateTime.Now;
                    user.ExpiryDate = DateTime.Now.AddMonths(1);
                    user.Password = Utility.MD5Hash(Password);
                    user.PaymentStatus = "unpaid";
                    user.ProfileUrl = string.Empty;
                    user.TimeZone = string.Empty;
                    user.UserName = Username;//FirstName + " " + LastName;
                    user.UserStatus = 1;
                    user.Ewallet = "0";
                    user.ActivationStatus = ActivationStatus;//"0"; 
                    user.Id = Guid.NewGuid();
                    user.UserCode = Utility.GenerateRandomUniqueString();
                    user.LastLoginTime = DateTime.Now;
                    UserRepository.Add(user);

                    ////add value in UserActivation
                    //UserActivation.AddUserActivation(user);

                    //add value in userpackage
                    // UserPackageRelation.AddUserPackageRelation(user);


                    try
                    {
                        Domain.Socioboard.Domain.Groups groups = AddGroupByUserId(user.Id);


                        BusinessSettingRepository busnrepo = new BusinessSettingRepository();
                        BusinessSetting.AddBusinessSetting(user.Id, groups.Id, groups.GroupName);
                        Team.AddTeamByGroupIdUserId(user.Id, user.EmailId, groups.Id);

                        UpdateTeam(EmailId, user.Id.ToString(), user.UserName);

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                        logger.Error("Error : " + ex.Message);
                        logger.Error("Error : " + ex.StackTrace);
                    }


                    //MailSender.SendEMail(user.UserName, Password, EmailId);
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
        public string UpdateUser(string UserId, string fname, string lname, string timezone, string picurl, string access_token)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return "Unauthorized access";
            }
            int ret = userrepo.UpdateUserById(Guid.Parse(UserId), fname + " " + lname, timezone, picurl);
            return ret.ToString();
        }

        // [WebMethod]
        //[ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        private void UpdateTeam(string EmailId, string userid, string username)
        {
            Domain.Socioboard.Domain.Groups objGroups = objGroupsRepository.getGroupDetails(Guid.Parse(userid), ConfigurationManager.AppSettings["DefaultGroupName"]);
            List<Domain.Socioboard.Domain.Team> lstTeam = objTeamRepository.GetAllTeamOfUserEmail(EmailId, objGroups.Id);
            foreach (var team in lstTeam)
            {
                Team objTeam = new Team();
                objTeam.UpdateTeam(userid, team.Id.ToString(), username);
                AddTeamMembers(team.GroupId.ToString(), team.Id.ToString());
            }
        }

        // [WebMethod]
        //[ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public void AddTeamMembers(string groupid, string teamid)
        {
            List<Domain.Socioboard.Domain.GroupProfile> lstGroupProfile = objGroupProfileRepository.GetAllGroupProfiles(Guid.Parse(groupid));
            foreach (var GroupProfile in lstGroupProfile)
            {
                objTeamMemberProfile = new Domain.Socioboard.Domain.TeamMemberProfile();
                objTeamMemberProfile.Id = Guid.NewGuid();
                objTeamMemberProfile.TeamId = Guid.Parse(teamid);
                objTeamMemberProfile.ProfileId = GroupProfile.ProfileId;
                objTeamMemberProfile.ProfileType = GroupProfile.ProfileType;
                objTeamMemberProfile.Status = 1;
                objTeamMemberProfile.StatusUpdateDate = DateTime.Now;
                objTeamMemberProfileRepository.addNewTeamMember(objTeamMemberProfile);
            }
        }

        private static Domain.Socioboard.Domain.Groups AddGroupByUserId(Guid userId)
        {
            Domain.Socioboard.Domain.Groups groups = new Domain.Socioboard.Domain.Groups();
            GroupsRepository objGroupRepository = new GroupsRepository();

            groups.Id = Guid.NewGuid();
            groups.GroupName = ConfigurationManager.AppSettings["DefaultGroupName"];
            groups.UserId = userId;
            groups.EntryDate = DateTime.Now;

            objGroupRepository.AddGroup(groups);
            return groups;
        }

        //private static void AddUserPackageRelation(Domain.Socioboard.Domain.User user)
        //{
        //    Domain.Socioboard.Domain.UserPackageRelation objUserPackageRelation = new Domain.Socioboard.Domain.UserPackageRelation();
        //    UserPackageRelationRepository objUserPackageRelationRepository = new UserPackageRelationRepository();
        //    PackageRepository objPackageRepository = new PackageRepository();

        //    Domain.Socioboard.Domain.Package objPackage = objPackageRepository.getPackageDetails(user.AccountType);
        //    objUserPackageRelation.Id = new Guid();
        //    objUserPackageRelation.PackageId = objPackage.Id;
        //    objUserPackageRelation.UserId = user.Id;
        //    objUserPackageRelation.PackageStatus = true;

        //    objUserPackageRelationRepository.AddUserPackageRelation(objUserPackageRelation);
        //}

        //private static void AddUserActivation(Domain.Socioboard.Domain.User user)
        //{
        //    Domain.Socioboard.Domain.UserActivation objUserActivation = new Domain.Socioboard.Domain.UserActivation();
        //    objUserActivation.Id = Guid.NewGuid();
        //    objUserActivation.UserId = user.Id;
        //    objUserActivation.ActivationStatus = "0";
        //    UserActivationRepository.Add(objUserActivation);
        //}

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string ChangePassword(string EmailId, string Password, string NewPassword, string access_token)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return "Unauthorized access";
            }
            try
            {
                User user = new User();
                UserRepository userrepo = new UserRepository();
                int i = userrepo.ChangePassword(Utility.MD5Hash(NewPassword), Utility.MD5Hash(Password), EmailId);
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

        //ChangePasswordWithoutOldPassword
        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string ChangePasswordWithoutOldPassword(string EmailId, string Password, string NewPassword)
        {

            try
            {
                User user = new User();
                UserRepository userrepo = new UserRepository();
                int i = userrepo.ChangePasswordWithoutOldPassword(Utility.MD5Hash(NewPassword), EmailId);
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
        public string ProfilesConnected(string UserId, string access_token)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return "Unauthorized access";
            }
            try
            {

                Guid userid = Guid.Parse(User.Identity.Name);
                SocialProfilesRepository socialRepo = new SocialProfilesRepository();
                List<Domain.Socioboard.Domain.SocialProfile> lstsocioprofile = socialRepo.getAllSocialProfilesOfUser(userid);
                List<profileConnected> lstProfile = new List<profileConnected>();
                foreach (Domain.Socioboard.Domain.SocialProfile sp in lstsocioprofile)
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
                            Domain.Socioboard.Domain.FacebookAccount objFbAcc = objFbAccRepo.getUserDetails(sp.ProfileId);
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
                            Domain.Socioboard.Domain.TwitterAccount objTwtAcc = objTwtAccRepo.getUserInfo(sp.ProfileId);
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
                            Domain.Socioboard.Domain.InstagramAccount objInsAcc = objInsAccRepo.getInstagramAccountById(sp.ProfileId);
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
                            Domain.Socioboard.Domain.LinkedInAccount objLiAcc = objLiAccRepo.getLinkedinAccountDetailsById(sp.ProfileId);
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
                            Domain.Socioboard.Domain.GooglePlusAccount objGpAcc = objGpAccRepo.getUserDetails(sp.ProfileId);
                            pc.ProfileName = objGpAcc.GpUserName;
                            pc.ProfileImgUrl = objGpAcc.GpProfileImage;
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.StackTrace);
                        }
                    }
                    else if (sp.ProfileType == "tumblr")
                    {
                        try
                        {
                            TumblrAccountRepository objTumblrAccountRepository = new TumblrAccountRepository();
                            Domain.Socioboard.Domain.TumblrAccount objTumblrAccount = objTumblrAccountRepository.getTumblrAccountDetailsById(sp.ProfileId);
                            pc.ProfileName = objTumblrAccount.tblrUserName;
                            pc.ProfileImgUrl = objTumblrAccount.tblrProfilePicUrl;
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.StackTrace);
                        }
                    }
                    else if (sp.ProfileType == "youtube")
                    {
                        try
                        {
                            YoutubeAccountRepository objYoutubeAccountRepository = new YoutubeAccountRepository();
                            Domain.Socioboard.Domain.YoutubeAccount objYoutubeAccount = objYoutubeAccountRepository.getYoutubeAccountDetailsById(sp.ProfileId);
                            pc.ProfileName = objYoutubeAccount.Ytusername;
                            pc.ProfileImgUrl = objYoutubeAccount.Ytprofileimage;
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

        //getUsersById
        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string getUsersById(string UserId, string access_token)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return "Unauthorized access";
            }
            Domain.Socioboard.Domain.User objUser = new Domain.Socioboard.Domain.User();
            objUser = userrepo.getUsersById(Guid.Parse(UserId));

            return new JavaScriptSerializer().Serialize(objUser);
        }

        //getUserInfoByEmail
        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string getUserInfoByEmail(string userEmail)
        {
            Domain.Socioboard.Domain.User objUser = new Domain.Socioboard.Domain.User();
            objUser = userrepo.getUserInfoByEmail(userEmail);

            return new JavaScriptSerializer().Serialize(objUser);
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string ResetPassword(Guid id, string password)
        {

            int objUser = userrepo.ResetPassword(id, password);

            return new JavaScriptSerializer().Serialize(objUser.ToString());
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public int UpdateUsertoactivate(string UserId, string ActivationStatus)
        {
            int ret = userrepo.UpdateUserbyUserId(Guid.Parse(UserId), ActivationStatus);
            return ret;
        }

        //Added by Sumit Gupta [11/15/14]
        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string changePaymentStatus(string UserId, string ActivationStatus, string access_token)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return "Unauthorized access";
            }
            int ret = userrepo.changePaymentStatus(Guid.Parse(User.Identity.Name), ActivationStatus);
            return new JavaScriptSerializer().Serialize(ret);//ret;
        }
        //vikash [20/11/2014]
        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string UpdateUserAccountInfoByUserId(string userid, string AccountType, DateTime ExpiryDate, string PaymentStatus, string access_token)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return "Unauthorized access";
            }
            int ret = userrepo.UpdateUserAccountInfoByUserId(userid, AccountType, ExpiryDate, PaymentStatus);
            return ret.ToString();
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string UserCountByMonth()
        {
            try
            {
                ArrayList ret = userrepo.UserCountByMonth();
                return new JavaScriptSerializer().Serialize(ret);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return null;
            }
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string UserCountByAccTypeMonth()
        {
            try
            {
                ArrayList ret = userrepo.UserCountByAccTypeMonth();
                return new JavaScriptSerializer().Serialize(ret);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return null;
            }
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string PaidUserCountByMonth()
        {
            try
            {
                ArrayList ret = userrepo.PaidUserCountByMonth();
                return new JavaScriptSerializer().Serialize(ret);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return null;
            }
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string UnPaidUserCountByMonth()
        {
            try
            {
                ArrayList ret = userrepo.UnPaidUserCountByMonth();
                return new JavaScriptSerializer().Serialize(ret);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return null;
            }
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string UpdateForgetPasswordKey(string userID, string ForgetPasswordKey)
        {
            try
            {
                int ret = userrepo.UpdateChangePasswordKey(Guid.Parse(userID), ForgetPasswordKey);
                return new JavaScriptSerializer().Serialize(ret);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return null;
            }
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string UpdateAdminUser(string UserId, string fname, string lname, string timezone, string profileurl)
        {
            int ret = userrepo.UpdateAdminUserById(Guid.Parse(UserId), fname + " " + lname, timezone, profileurl);
            return ret.ToString();
        }

        // Edited by Antima [05/01/2015]
        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string CheckEmailId(string NewEmailId)
        {
            if (!userrepo.IsUserExist(NewEmailId))
            {
                return "NotExist";
            }
            else
            {
                return "EmailId already Exist";
            }
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string UpdateResetEmailKey(string userID, string ChangeEmailKey)
        {
            try
            {
                int ret = userrepo.UpdateChangeEmailKey(Guid.Parse(userID), ChangeEmailKey);
                return new JavaScriptSerializer().Serialize(ret);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return null;
            }
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string UpdateIsEmailKeyUsed(string userID, string ChangeEmailKey)
        {
            try
            {
                int ret = userrepo.UpdateIsEmailKeyUsed(Guid.Parse(userID), ChangeEmailKey);
                return new JavaScriptSerializer().Serialize(ret);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return null;
            }
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string UpdateIsKeyUsed(string userID)
        {
            try
            {
                int ret = userrepo.UpdateIsKeyUsed(Guid.Parse(userID));
                return new JavaScriptSerializer().Serialize(ret);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return null;
            }
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string UpdatePaymentandEwalletStatusByUserId(string userid, string ewallet, string accounttype, string paymentstatus, string access_token)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return "Unauthorized access";
            }
            try
            {
                int i = userrepo.UpdatePaymentandEwalletStatusByUserId(Guid.Parse(User.Identity.Name), ewallet, accounttype, paymentstatus);
                return new JavaScriptSerializer().Serialize(i);
            }
            catch (Exception)
            {
                return new JavaScriptSerializer().Serialize(0);
            }
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string UpdateEmailId(string Id, string GroupId, string NewEmailId, string access_token)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return "Unauthorized access";
            }
            try
            {
                int ret = 0;
                int ret1 = 0;
                if (!userrepo.IsUserExist(NewEmailId))
                {
                    ret = userrepo.UpdateEmailId(Guid.Parse(Id), NewEmailId);
                    ret1 = objTeamRepository.UpdateEmailIdbyGroupId(Guid.Parse(Id), Guid.Parse(GroupId), NewEmailId);
                    if (ret == 1 && ret1 == 1)
                    {
                        return "Updated Successfully";
                    }
                    else
                    {
                        return "Failed";
                    }
                }
                else
                {
                    return "Email Id alredy Exist";
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }
        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string UpdateEwalletAmount(string UserId, string updatedamount, string access_token)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return "Unauthorized access";
            }
            try
            {
                int ret = userrepo.UpdateEwalletAmount(Guid.Parse(UserId), updatedamount);
                return "success";
            }
            catch (Exception ex)
            {
                return "Somthing Went Wrong";
            }

        }


        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string GetUserInfoByCode(string code)
        {
            try
            {
                Domain.Socioboard.Domain.User ret = userrepo.GetUserInfoByCode(code);
                return new JavaScriptSerializer().Serialize(ret);
            }
            catch (Exception ex)
            {
                return "";
            }
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string GetAllExpiredUser()
        {
            try
            {
                List<Domain.Socioboard.Domain.User> lstUser = userrepo.GetAllExpiredUser();
                return new JavaScriptSerializer().Serialize(lstUser);
            }
            catch (Exception ex)
            {
                return "";
            }
        }
        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string GetAllUsers()
        {
            try
            {
                List<Domain.Socioboard.Domain.User> lstUser = userrepo.getAllUsers();
                return new JavaScriptSerializer().Serialize(lstUser);
            }
            catch (Exception ex)
            {
                return "";
            }
        }

        //vikash [06/04/2015]
        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string getUserInfoForSocialLogin(string logintype)
        {
            Domain.Socioboard.Domain.User objUser = new Domain.Socioboard.Domain.User();
            objUser = userrepo.getUserInfoForSocialLogin(logintype);

            return new JavaScriptSerializer().Serialize(objUser);
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string RegisterbyTwitter(string EmailId, string Password, string AccountType, string Username, string SocioLogin, string PictureUrl, string ActivationStatus = "0")
        {
            try
            {
                logger.Error("RegisterbyTwitter");

                if (!userrepo.IsUserExist(EmailId))
                {
                    Domain.Socioboard.Domain.User user = new Domain.Socioboard.Domain.User();
                    user.AccountType = AccountType;
                    user.EmailId = EmailId;
                    user.CreateDate = DateTime.Now;
                    user.ExpiryDate = DateTime.Now.AddMonths(1);
                    user.Password = Utility.MD5Hash(Password);
                    user.PaymentStatus = "unpaid";
                    user.ProfileUrl = string.Empty;
                    user.TimeZone = string.Empty;
                    user.UserName = Username;//FirstName + " " + LastName;
                    user.UserStatus = 1;
                    user.Ewallet = "0";
                    user.ActivationStatus = ActivationStatus;//"0"; 
                    user.Id = Guid.NewGuid();
                    user.UserCode = Utility.GenerateRandomUniqueString();
                    user.SocialLogin = SocioLogin;
                    user.ProfileUrl = PictureUrl;
                    UserRepository.Add(user);

                    ////add value in UserActivation
                    //UserActivation.AddUserActivation(user);

                    //add value in userpackage
                    // UserPackageRelation.AddUserPackageRelation(user);


                    try
                    {
                        Domain.Socioboard.Domain.Groups groups = AddGroupByUserId(user.Id);


                        BusinessSettingRepository busnrepo = new BusinessSettingRepository();
                        BusinessSetting.AddBusinessSetting(user.Id, groups.Id, groups.GroupName);
                        Team.AddTeamByGroupIdUserId(user.Id, user.EmailId, groups.Id);

                        UpdateTeam(EmailId, user.Id.ToString(), user.UserName);

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                        logger.Error("Error : " + ex.Message);
                        logger.Error("Error : " + ex.StackTrace);
                    }
                    //MailSender.SendEMail(user.UserName, Password, EmailId);
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
        public string CompareDateWithclientlocal(string clientdate, string datetime)
        {
            try
            {
                DateTime client = Convert.ToDateTime(clientdate);

                DateTime server = DateTime.Now;
                DateTime schedule = Convert.ToDateTime(datetime);
                {
                    var kind = schedule.Kind; // will equal DateTimeKind.Unspecified
                    if (DateTime.Compare(client, server) > 0)
                    {
                        double minutes = (client - server).TotalMinutes;
                        schedule = schedule.AddMinutes(minutes);
                    }
                    else if (DateTime.Compare(client, server) == 0)
                    {
                    }
                    else if (DateTime.Compare(client, server) < 0)
                    {
                        double minutes = (client - server).TotalMinutes;
                        schedule = schedule.AddMinutes(minutes);
                    }
                }
                return schedule.ToString();
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                Console.WriteLine(ex.StackTrace);
                return "";
            }
        }
        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string DifferenceBetweenServerandLocalTime(string clientdate)
        {
            try
            {
                double minutes = 0;
                DateTime client = Convert.ToDateTime(clientdate);
                DateTime server = DateTime.Now;
                if (DateTime.Compare(client, server) > 0)
                {
                    minutes = (client - server).TotalMinutes;
                }
                else if (DateTime.Compare(client, server) == 0)
                {
                }
                else if (DateTime.Compare(client, server) < 0)
                {
                    minutes = (client - server).TotalMinutes;
                }
                return minutes.ToString(CultureInfo.InvariantCulture.NumberFormat);
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                Console.WriteLine(ex.StackTrace);
                return "something went wrong";
            }
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string DifferenceBetweenServerandUtc()
        {
            try
            {
                double minutes = 0;

                DateTime server = DateTime.Now;
                DateTime utc = server.ToUniversalTime();
                if (DateTime.Compare(server, utc) > 0)
                {
                    minutes = (server - utc).TotalMinutes;
                }
                else if (DateTime.Compare(server, utc) == 0)
                {
                }
                else if (DateTime.Compare(server, utc) < 0)
                {
                    minutes = (server - utc).TotalMinutes;
                }
                return minutes.ToString(CultureInfo.InvariantCulture.NumberFormat);
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                Console.WriteLine(ex.StackTrace);
                return "something went wrong";
            }
        }
        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string UpdateLastLoginTime(string UserId)
        {
            return Convert.ToString(userrepo.UpdateLastLoginTime(Guid.Parse(UserId)));
        }
        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string InactiveUser()
        {
            return new JavaScriptSerializer().Serialize(userrepo.InactiveUser());
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

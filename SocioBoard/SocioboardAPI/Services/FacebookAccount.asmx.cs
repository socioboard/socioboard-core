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
    public class FacebookAccount : System.Web.Services.WebService
    {
        FacebookAccountRepository objFacebookAccountRepository = new FacebookAccountRepository();
        FacebookFeedRepository objFacebookFeedRepository = new FacebookFeedRepository();
        FacebookMessageRepository objFacebookMessageRepository = new FacebookMessageRepository();
        TeamMemberProfileRepository objTeamMemberProfileRepository = new TeamMemberProfileRepository();
        SocialProfilesRepository objSocialProfilesRepository = new SocialProfilesRepository();
        TeamRepository objTeamRepository = new TeamRepository();
        GroupProfileRepository objGroupProfileRepository = new GroupProfileRepository();
        Domain.Socioboard.Domain.FacebookAccount objFacebook;
        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string AddFacebookAccount(string FbUserId, string FbUserName, string AccessToken, string Friends, string EmailId, string Type, string ProfileUrl, string IsActive, string UserId, string GroupId)
        {
            try
            {
                objFacebook = new Domain.Socioboard.Domain.FacebookAccount();
                objFacebook.Id = Guid.NewGuid();
                objFacebook.FbUserId = FbUserId;
                objFacebook.FbUserName = FbUserName;
                objFacebook.AccessToken = AccessToken;
                objFacebook.Friends = Convert.ToInt16(Friends);
                objFacebook.EmailId = EmailId;
                objFacebook.Type = Type;
                objFacebook.ProfileUrl = ProfileUrl;
                objFacebook.IsActive = Convert.ToInt16(IsActive);
                objFacebook.UserId = Guid.Parse(UserId);
                objFacebookAccountRepository.addFacebookUser(objFacebook);



                return new JavaScriptSerializer().Serialize("Added");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return "Something Went Wrong";
            }
        }

        //[WebMethod]
        //[ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        //public string getFacebookAccountDetailsById(string UserId, string ProfileId)
        //{
        //    try
        //    {
        //        Domain.Socioboard.Domain.FacebookAccount objFacebookAccount = objFacebookAccountRepository.getFacebookAccountDetailsById(ProfileId, Guid.Parse(UserId));
        //        return new JavaScriptSerializer().Serialize(objFacebookAccount);
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex.StackTrace);
        //        return "Something Went Wrong";
        //    }
        //}

        // Edited by Antima[20/12/2014]

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string getFacebookAccountDetailsById(string UserId, string ProfileId)
        {
            try
            {
                Domain.Socioboard.Domain.FacebookAccount objFacebookAccount = new Domain.Socioboard.Domain.FacebookAccount();
                if (objFacebookAccountRepository.checkFacebookUserExists(ProfileId, Guid.Parse(UserId)))
                {
                    objFacebookAccount = objFacebookAccountRepository.getFacebookAccountDetailsById(ProfileId, Guid.Parse(UserId));
                    return new JavaScriptSerializer().Serialize(objFacebookAccount);
                }
                else
                {
                    objFacebookAccount = objFacebookAccountRepository.getUserDetails(ProfileId);
                    return new JavaScriptSerializer().Serialize(objFacebookAccount);
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
        public string GetFacebookAccountDetailsById(string UserId)
        {
            try
            {
                Domain.Socioboard.Domain.FacebookAccount objFacebookAccount = objFacebookAccountRepository.getFacebookAccountDetailsById(UserId);
                return new JavaScriptSerializer().Serialize(objFacebookAccount);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return "Something Went Wrong";
            }
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string IsFacebookuserExist(string UserId, string ProfileId)
        {
            string str = string.Empty;
            try
            {
                bool ret = objFacebookAccountRepository.checkFacebookUserExists(ProfileId, Guid.Parse(UserId));
                if (ret == false)
                {
                    str = "False";
                }
                else
                {
                    str = "True";
                }
                return new JavaScriptSerializer().Serialize(ret);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return "Something Went Wrong";
            }
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string DeleteFacebookAccount(string UserId, string ProfileId,string GroupId)
        {
            try
            {
                objFacebookAccountRepository.deleteFacebookUser(ProfileId, Guid.Parse(UserId));
                objFacebookFeedRepository.deleteAllFeedsOfUser(ProfileId, Guid.Parse(UserId));
                objFacebookMessageRepository.deleteAllMessagesOfUser(ProfileId, Guid.Parse(UserId));
                Domain.Socioboard.Domain.Team objTeam=objTeamRepository.GetTeamByGroupId(Guid.Parse(GroupId));
                objTeamMemberProfileRepository.DeleteTeamMemberProfileByTeamIdProfileId(ProfileId, objTeam.Id);
                objGroupProfileRepository.DeleteGroupProfile(Guid.Parse(UserId), ProfileId, Guid.Parse(GroupId));
                objSocialProfilesRepository.deleteProfile(Guid.Parse(UserId), ProfileId);
                return new JavaScriptSerializer().Serialize("");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return "Something Went Wrong";
            }
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string GetFacebookAccountByUserId(string UserId)
        {
            try
            {
                List<Domain.Socioboard.Domain.FacebookAccount> lstFacebookAccount = objFacebookAccountRepository.GetAllFacebookAccountByUserId(Guid.Parse(UserId));
                return new JavaScriptSerializer().Serialize(lstFacebookAccount);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return "Something Went Wrong";
            }
        }
        //getAllFacebookAccountsOfUser
        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string getAllFacebookAccountsOfUser(string UserId)
        {
            try
            {
                List<Domain.Socioboard.Domain.FacebookAccount> objFacebookAccount = objFacebookAccountRepository.getAllFacebookAccountsOfUser(Guid.Parse(UserId));
                return new JavaScriptSerializer().Serialize(objFacebookAccount);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return "Something Went Wrong";
            }
        }

        //getUserDetails
        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string getUserDetails(string FbUserId)
        {
            try
            {
                Domain.Socioboard.Domain.FacebookAccount objFacebookAccount = objFacebookAccountRepository.getUserDetails(FbUserId);
                return new JavaScriptSerializer().Serialize(objFacebookAccount);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return "Something Went Wrong";
            }
        }
        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string GetAllFacebookAccountsByUserIdAndGroupId(string userid,string groupid)
        {
            try
            {
                List<Domain.Socioboard.Domain.FacebookAccount> lstFacebookAccount = new List<Domain.Socioboard.Domain.FacebookAccount>();
                Domain.Socioboard.Domain.Team objTeam = objTeamRepository.GetTeamByGroupId(Guid.Parse(groupid));
                List<Domain.Socioboard.Domain.TeamMemberProfile> lstTeamMemberProfile=objTeamMemberProfileRepository.GetTeamMemberProfileByTeamIdAndProfileType(objTeam.Id,"facebook");
                foreach (var item in lstTeamMemberProfile)
                {
                    try
                    {
                        if (objFacebookAccountRepository.checkFacebookUserExists(item.ProfileId, Guid.Parse(userid)))
                        {
                            lstFacebookAccount.Add(objFacebookAccountRepository.getFacebookAccountDetailsById(item.ProfileId, Guid.Parse(userid)));
                        }
                        else
                        {
                            lstFacebookAccount.Add(objFacebookAccountRepository.getFacebookAccountDetailsById(item.ProfileId));
                        }
                    }
                    catch (Exception)
                    {
                       
                    }
                }
                return new JavaScriptSerializer().Serialize(lstFacebookAccount);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return "Something Went Wrong";
            }
        }


        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string GetAllFacebookPageByUserIdAndGroupId(string userid, string groupid)
        {
            try
            {
                List<Domain.Socioboard.Domain.FacebookAccount> lstFacebookAccount = new List<Domain.Socioboard.Domain.FacebookAccount>();
                Domain.Socioboard.Domain.Team objTeam = objTeamRepository.GetTeamByGroupId(Guid.Parse(groupid));
                List<Domain.Socioboard.Domain.TeamMemberProfile> lstTeamMemberProfile = objTeamMemberProfileRepository.GetTeamMemberProfileByTeamIdAndProfileType(objTeam.Id, "facebook_page");
                foreach (var item in lstTeamMemberProfile)
                {
                    try
                    {
                        lstFacebookAccount.Add(objFacebookAccountRepository.getFacebookAccountDetailsById(item.ProfileId, Guid.Parse(userid)));
                    }
                    catch (Exception)
                    {

                    }
                }
                return new JavaScriptSerializer().Serialize(lstFacebookAccount);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return "Something Went Wrong";
            }
        }

        //vikash
        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string GetAllFacebookAccountDetails(string profileid,string userid)
        {
            try
            {
                List<Domain.Socioboard.Domain.FacebookAccount> lstfbprofileDetails = objFacebookAccountRepository.getAllAccountDetail(profileid,Guid.Parse(userid));
                return new JavaScriptSerializer().Serialize(lstfbprofileDetails);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return "Something Went Wrong";
            }

        }

        public string getFbToken()
        {

            Domain.Socioboard.Domain.FacebookAccount _FacebookAccount = objFacebookAccountRepository.getToken();
            string token = _FacebookAccount.AccessToken;

            return token;
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string GetAllFacebookAccounts()
        {
            try
            {
                FacebookAccountRepository objFbRepo = new FacebookAccountRepository();
                ArrayList lstFBAcc = objFbRepo.getAllFacebookAccounts();
                return new JavaScriptSerializer().Serialize(lstFBAcc);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return "Something Went Wrong";
            }
        }




    }
}

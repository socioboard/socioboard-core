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
    public class SocialProfile : System.Web.Services.WebService
    {
        SocialProfilesRepository objSocialProfilesRepository = new SocialProfilesRepository();
        Domain.Socioboard.Domain.SocialProfile socialprofile = new Domain.Socioboard.Domain.SocialProfile();

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string AddSocialProfile(string UserId, string ProfileId, string ProfileType, string ProfileStatus)
        {

            try
            {
                socialprofile.UserId = Guid.Parse(UserId);
                socialprofile.ProfileId = ProfileId;
                socialprofile.ProfileType = ProfileType;

                if (!objSocialProfilesRepository.checkUserProfileExist(socialprofile))
                {

                    socialprofile.Id = Guid.NewGuid();
                    socialprofile.ProfileDate = DateTime.Now;
                    socialprofile.ProfileStatus = Convert.ToInt32(ProfileStatus);
                    objSocialProfilesRepository.addNewProfileForUser(socialprofile);

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return "Something Went Wrong";
            }
            return new JavaScriptSerializer().Serialize(socialprofile);

        }


        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string DeleteProfileByProfileId(String UserId, string ProfileId)
        {
            try
            {
                int i = objSocialProfilesRepository.deleteProfile(Guid.Parse(UserId), ProfileId);
                if (i == 1)
                {
                    return "Profile Deleted Successfully";
                }
                else
                {
                    return "Invalid UserId or ProfileId";
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
        public string DeleteProfileByUserId(string UserId)
        {
            try
            {
                int i = objSocialProfilesRepository.DeleteSocialProfileByUserid(Guid.Parse(UserId));
                if (i == 1)
                {
                    return "Profile Deleted Successfully";
                }
                else
                {
                    return "Invalid UserId";
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
        public string GetSocialProfileByUserId(string UserId)
        {
            try
            {
                List<Domain.Socioboard.Domain.SocialProfile> lstSocialProfile = objSocialProfilesRepository.getAllSocialProfilesOfUser(Guid.Parse(UserId));
                return new JavaScriptSerializer().Serialize(lstSocialProfile);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return "Something Went Wrong";
            }
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string SocialProfileByProfilType(string Profiletype)
        {
            try
            {
                List<Domain.Socioboard.Domain.SocialProfile> lstSocialProfile = objSocialProfilesRepository.GetSocialProfileByProfileType(Profiletype);
                return new JavaScriptSerializer().Serialize(lstSocialProfile);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return "Something Went Wrong";
            }
        }



        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string GetAllSocialProfilesOfUserCount(string UserId)
        {
            try
            {

                Guid userid = Guid.Parse(UserId);
                Domain.Socioboard.Domain.SocialProfile objSocialProfile = new Domain.Socioboard.Domain.SocialProfile();
                List<Domain.Socioboard.Domain.SocialProfile> lstSocialProfile = new List<Domain.Socioboard.Domain.SocialProfile>();
                lstSocialProfile = objSocialProfilesRepository.getAllSocialProfilesOfUser(userid);

                //FacebookAccountRepository facebookAccountRepo = new FacebookAccountRepository();
                //FacebookAccount facebook = facebookAccountRepo.getFacebookAccountDetailsById(FacebookId, userid);
                return new JavaScriptSerializer().Serialize(lstSocialProfile.Count);
                //return lstScheduledMessages;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return new JavaScriptSerializer().Serialize("Please try Again");
            }

        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string GetAllSocialProfilesOfUser(string UserId)
        {
            try
            {

                Guid userid = Guid.Parse(UserId);
                Domain.Socioboard.Domain.SocialProfile objSocialProfile = new Domain.Socioboard.Domain.SocialProfile();
                List<Domain.Socioboard.Domain.SocialProfile> lstSocialProfile = new List<Domain.Socioboard.Domain.SocialProfile>();
                lstSocialProfile = objSocialProfilesRepository.getAllSocialProfilesOfUser(userid);

                //FacebookAccountRepository facebookAccountRepo = new FacebookAccountRepository();
                //FacebookAccount facebook = facebookAccountRepo.getFacebookAccountDetailsById(FacebookId, userid);
                return new JavaScriptSerializer().Serialize(lstSocialProfile);
                //return lstScheduledMessages;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return new JavaScriptSerializer().Serialize("Please try Again");
            }

        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string GetAllSocialProfiles()
        {
            try
            {

               
                Domain.Socioboard.Domain.SocialProfile objSocialProfile = new Domain.Socioboard.Domain.SocialProfile();
                List<Domain.Socioboard.Domain.SocialProfile> lstSocialProfile = new List<Domain.Socioboard.Domain.SocialProfile>();
                lstSocialProfile = objSocialProfilesRepository.getAllSocialProfiles();

                //FacebookAccountRepository facebookAccountRepo = new FacebookAccountRepository();
                //FacebookAccount facebook = facebookAccountRepo.getFacebookAccountDetailsById(FacebookId, userid);
                return new JavaScriptSerializer().Serialize(lstSocialProfile);
                //return lstScheduledMessages;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return new JavaScriptSerializer().Serialize("Please try Again");
            }

        }




    }
}

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
    /// Summary description for SocialProfiles
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
  

        
    public class SocialProfiles : System.Web.Services.WebService
    {
        SocialProfilesRepository sociorepo = new SocialProfilesRepository();
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

                if (!sociorepo.checkUserProfileExist(socialprofile))
                {
          
                    socialprofile.Id = Guid.NewGuid();                 
                    socialprofile.ProfileDate = DateTime.Now;
                    socialprofile.ProfileStatus = Convert.ToInt32(ProfileStatus);
                    sociorepo.addNewProfileForUser(socialprofile);
                  
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
        public string DeleteProfileByProfileId(String UserId,string ProfileId)
        {
            try
            {
                int i = sociorepo.deleteProfile(Guid.Parse(UserId), ProfileId);
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
                int i = sociorepo.DeleteSocialProfileByUserid(Guid.Parse(UserId));
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
        public string SocialProfileByUserId(string UserId)
        {
            try
            {
                List<Domain.Socioboard.Domain.SocialProfile> lstSocialProfile = sociorepo.getAllSocialProfilesOfUser(Guid.Parse(UserId));
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
        public string SocialProfileByProfilType(string UserId)
        {
            try
            {
                List<Domain.Socioboard.Domain.SocialProfile> lstSocialProfile = sociorepo.getAllSocialProfilesOfUser(Guid.Parse(UserId));
                return new JavaScriptSerializer().Serialize(lstSocialProfile);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return "Something Went Wrong";
            }
        }




    }
}

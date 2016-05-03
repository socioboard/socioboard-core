using GlobusLinkedinLib.App.Core;
using GlobusLinkedinLib.Authentication;
using log4net;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Domain.Socioboard.Domain;
using Api.Socioboard.Model;
using Api.Socioboard.Services;
using System.Web.Script.Serialization;
using System.Text.RegularExpressions;
using GlobusLinkedinLib.LinkedIn.Core.CompanyMethods;


namespace Api.Socioboard.Controllers
{
     [Api.Socioboard.App_Start.AllowCrossSiteJson]
    public class ApiLinkedInController : ApiController
    {
         ILog logger = LogManager.GetLogger(typeof(ApiLinkedInController));
         private Domain.Socioboard.Domain.LinkedInAccount objLinkedInAccount = new Domain.Socioboard.Domain.LinkedInAccount();
         private Domain.Socioboard.Domain.SocialProfile objSocialProfile = new Domain.Socioboard.Domain.SocialProfile();
         private Domain.Socioboard.Domain.TeamMemberProfile objTeamMemberProfile = new Domain.Socioboard.Domain.TeamMemberProfile();
         private TeamRepository objTeamRepository = new TeamRepository();
         private SocialProfilesRepository objSocialProfilesRepository = new SocialProfilesRepository();
         private LinkedInAccountRepository objLinkedInAccountRepository = new LinkedInAccountRepository();
         private TeamMemberProfileRepository objTeamMemberProfileRepository = new TeamMemberProfileRepository();
         private ScheduledMessageRepository objScheduledMessageRepository = new ScheduledMessageRepository();
         private Domain.Socioboard.Domain.ScheduledMessage objScheduledMessage = new Domain.Socioboard.Domain.ScheduledMessage();
         private LinkedinCompanyPageRepository objLinkedCmpnyPgeRepo = new LinkedinCompanyPageRepository();
         private LinkedinPagePostRepository objLinkedinPagePostRepository = new LinkedinPagePostRepository();
         private GroupProfileRepository grpProfileRepo = new GroupProfileRepository();

        [ActionName("AddLinkedInAccount")]
        [HttpPost]
         public IHttpActionResult AddLinkedInAccount(LinkedInManager LinkedInManager)
        {
            string ret = "";
            string UserId = LinkedInManager.UserId;
            oAuthLinkedIn _oauth = new oAuthLinkedIn();
            LinkedInProfile objProfile = new LinkedInProfile();
            Domain.Socioboard.Domain.GroupProfile grpProfile = new Domain.Socioboard.Domain.GroupProfile();
            try
            {
                _oauth.ConsumerKey = ConfigurationManager.AppSettings["LinkedinApiKey"];
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                logger.Error(ex.Message);
            }

            try
            {
                _oauth.ConsumerSecret = ConfigurationManager.AppSettings["LinkedinSecretKey"];
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                logger.Error(ex.Message);
            }
            string access_token_Url = "https://www.linkedin.com/uas/oauth2/accessToken";
            string access_token_postData = "grant_type=authorization_code&code=" + LinkedInManager.Code + "&redirect_uri=" + System.Web.HttpUtility.UrlEncode(ConfigurationManager.AppSettings["LinkedinCallBackURL"]) + "&client_id=" + ConfigurationManager.AppSettings["LinkedinApiKey"] + "&client_secret=" + ConfigurationManager.AppSettings["LinkedinSecretKey"];
            LinkedInProfile.UserProfile objUserProfile = new LinkedInProfile.UserProfile();
            string token = _oauth.APIWebRequestAccessToken("POST", access_token_Url, access_token_postData);
            var oathtoken = JObject.Parse(token);
            _oauth.Token = oathtoken["access_token"].ToString().TrimStart('"').TrimEnd('"');
            #region Get linkedin Profile data from Api
            try
            {
                _oauth.ConsumerKey = ConfigurationManager.AppSettings["LinkedinApiKey"];
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                logger.Error(ex.Message);
            }

            try
            {
                _oauth.ConsumerSecret = ConfigurationManager.AppSettings["LinkedinSecretKey"];
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                logger.Error(ex.Message);
            }
            try
            {
                objUserProfile = objProfile.GetUserProfile(_oauth);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                logger.Error(ex.Message);
            }
            #endregion
            dynamic data = objUserProfile;
            try
            {
                #region LinkedInAccount
                objLinkedInAccount.UserId = Guid.Parse(UserId);
                objLinkedInAccount.LinkedinUserId = data.id.ToString();
                try
                {
                    objLinkedInAccount.EmailId = data.email.ToString();
                }
                catch (Exception ex)
                {
                    logger.Error(ex.StackTrace);
                }
                objLinkedInAccount.LinkedinUserName = data.first_name.ToString() + data.last_name.ToString();
                objLinkedInAccount.OAuthToken = _oauth.Token;
                objLinkedInAccount.OAuthSecret = _oauth.TokenSecret;
                objLinkedInAccount.OAuthVerifier = _oauth.Verifier;
                try
                {
                    objLinkedInAccount.ProfileImageUrl = data.picture_url.ToString();
                }
                catch (Exception ex)
                {
                    logger.Error(ex.StackTrace);
                }
                try
                {
                    objLinkedInAccount.ProfileUrl = data.profile_url.ToString();
                }
                catch (Exception ex)
                {
                    logger.Error(ex.StackTrace);
                }
                #endregion
                #region SocialProfiles
                try
                {
                    objLinkedInAccount.Connections = data.connections;
                    objLinkedInAccount.IsActive = true;
                    objSocialProfile.UserId = Guid.Parse(UserId);
                    objSocialProfile.ProfileType = "linkedin";
                    objSocialProfile.ProfileId = data.id.ToString();
                    objSocialProfile.ProfileStatus = 1;
                    objSocialProfile.ProfileDate = DateTime.Now;
                    objSocialProfile.Id = Guid.NewGuid();
                }
                catch (Exception ex)
                {
                    logger.Error(ex.Message);
                }
                #endregion SocialProfiles
                #region Add TeamMemberProfile
                try
                {
                    //Domain.Socioboard.Domain.Team objTeam = objTeamRepository.GetTeamByGroupId(Guid.Parse(LinkedInManager.GroupId));
                    //objTeamMemberProfile = new Domain.Socioboard.Domain.TeamMemberProfile();
                    //objTeamMemberProfile.Id = Guid.NewGuid();
                    //objTeamMemberProfile.TeamId = objTeam.Id;
                    //objTeamMemberProfile.Status = 1;
                    //objTeamMemberProfile.ProfileType = "linkedin";
                    //objTeamMemberProfile.StatusUpdateDate = DateTime.Now;
                    //objTeamMemberProfile.ProfileId = objLinkedInAccount.LinkedinUserId;
                    //objTeamMemberProfile.ProfileName = objLinkedInAccount.LinkedinUserName;
                    //objTeamMemberProfile.ProfilePicUrl = objLinkedInAccount.ProfileImageUrl;
                   
                    grpProfile.Id = Guid.NewGuid();
                    grpProfile.GroupId = Guid.Parse(LinkedInManager.GroupId);
                    grpProfile.GroupOwnerId = objLinkedInAccount.UserId;
                    grpProfile.ProfileId = objLinkedInAccount.LinkedinUserId;
                    grpProfile.ProfileType = "linkedin";
                    grpProfile.ProfileName = objLinkedInAccount.LinkedinUserName;
                    grpProfile.EntryDate = DateTime.UtcNow;
                    //grpProfileRepo.AddGroupProfile(grpProfile);

                }
                catch (Exception ex)
                {
                    logger.Error(ex.Message);
                }
                #endregion

            }
            catch (Exception ex)
            {
                logger.Error(ex.StackTrace);
            }
            try
            {
                if (!objLinkedInAccountRepository.checkLinkedinUserExists(objLinkedInAccount.LinkedinUserId, Guid.Parse(UserId)))
                {
                    objLinkedInAccountRepository.addLinkedinUser(objLinkedInAccount);
                    ret = "LinkedIn Account Added Successfully";
                }
                else
                {
                    ret = "LinkedIn Account Already Exist";
                }
                if (!objSocialProfilesRepository.checkUserProfileExist(objSocialProfile))
                {
                    objSocialProfilesRepository.addNewProfileForUser(objSocialProfile);
                    grpProfileRepo.AddGroupProfile(grpProfile);
                }
                

            }
            catch (Exception ex)
            {
                logger.Error(ex.StackTrace);
            }
            return Ok(ret);
        }


        [ActionName("LinkedInProfileUpdate")]
        [HttpPost]
        public IHttpActionResult LinkedInProfileUpdate(LinkedInManager LinkedInManager)
        {
            oAuthLinkedIn _oauth = new oAuthLinkedIn();
            string json = "";
            Domain.Socioboard.Domain.LinkedInAccount _LinkedinAccount = objLinkedInAccountRepository.getLinkedinAccountDetailsById(LinkedInManager.ProfileId);
            try
            {
                _oauth.ConsumerKey = ConfigurationManager.AppSettings["LinkedinApiKey"];
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                logger.Error(ex.Message);
            }

            try
            {
                _oauth.ConsumerSecret = ConfigurationManager.AppSettings["LinkedinSecretKey"];
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                logger.Error(ex.Message);
            }
            _oauth.Token = _LinkedinAccount.OAuthToken;
            string PostUrl = "https://api.linkedin.com/v1/people/~/shares?format=json";
            if (string.IsNullOrEmpty(LinkedInManager.ImageUrl))
            {
                 json = _oauth.LinkedProfilePostWebRequest("POST", PostUrl, LinkedInManager.comment);
            }
            else {

                string imagepath = ConfigurationManager.AppSettings["DomainName"].ToString() + Regex.Split(LinkedInManager.ImageUrl, "wwwroot")[1].Replace("\\", "/");
                json = _oauth.LinkedProfilePostWebRequestWithImage("POST", PostUrl, LinkedInManager.comment,imagepath);
            }
            #region ScheduledMessage
            if (!string.IsNullOrEmpty(json))
            {
                objScheduledMessage.Id = Guid.NewGuid();
                objScheduledMessage.PicUrl = LinkedInManager.ImageUrl;
                objScheduledMessage.ProfileId = LinkedInManager.ProfileId;
                objScheduledMessage.ProfileType = "linkedin";
                objScheduledMessage.ScheduleTime = DateTime.Now;
                objScheduledMessage.ShareMessage = LinkedInManager.comment;
                objScheduledMessage.Status = true;
                objScheduledMessage.UserId = Guid.Parse(LinkedInManager.UserId);
                objScheduledMessage.CreateTime = DateTime.Now;
                objScheduledMessage.ClientTime = DateTime.Now;
                if (!objScheduledMessageRepository.checkMessageExistsAtTime(objScheduledMessage.UserId, objScheduledMessage.ScheduleTime))
                {
                    objScheduledMessageRepository.addNewMessage(objScheduledMessage);
                }
            
            }
            #endregion
            return Ok();
        }


        [ActionName("LinkedInScheduleUpdate")]
        [HttpPost]
        public string LinkedInScheduleUpdate(LinkedInManager LinkedInManager)
        {
            string json = "";
            if (LinkedInManager.ScheduleTime<=DateTime.Now)
            {
                oAuthLinkedIn _oauth = new oAuthLinkedIn();
                
                Domain.Socioboard.Domain.LinkedInAccount _LinkedinAccount = objLinkedInAccountRepository.getLinkedinAccountDetailsById(LinkedInManager.ProfileId);
                try
                {
                    _oauth.ConsumerKey = ConfigurationManager.AppSettings["LinkedinApiKey"];
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    logger.Error(ex.Message);
                }

                try
                {
                    _oauth.ConsumerSecret = ConfigurationManager.AppSettings["LinkedinSecretKey"];
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    logger.Error(ex.Message);
                }
                _oauth.Token = _LinkedinAccount.OAuthToken;
                string PostUrl = "https://api.linkedin.com/v1/people/~/shares?format=json";
                if (string.IsNullOrEmpty(LinkedInManager.ImageUrl))
                {
                    json = _oauth.LinkedProfilePostWebRequest("POST", PostUrl, LinkedInManager.comment);
                }
                else
                {

                    string imagepath = ConfigurationManager.AppSettings["DomainName"].ToString() + Regex.Split(LinkedInManager.ImageUrl, "wwwroot")[1].Replace("\\", "/");
                    json = _oauth.LinkedProfilePostWebRequestWithImage("POST", PostUrl, LinkedInManager.comment, imagepath);
                }
                if (!string.IsNullOrEmpty(json))
                {
                    json = "Message post on LinkedIn for Id :" + LinkedInManager.ProfileId + " and Message: " + LinkedInManager.comment;
                    objScheduledMessageRepository.UpdateScheduledMessage(Guid.Parse(LinkedInManager.ScheduleMessageId));
                }
                else {
                    json= "Something Went Wrong";
                }
            }

            return json;
            
        }


        [ActionName("GetLinkedinCompanyPage")]
        [HttpPost]
        public IHttpActionResult GetLinkedinCompanyPage(LinkedInManager LinkedInManager)
        {
            string UserId = LinkedInManager.UserId;
            oAuthLinkedIn _oauth = new oAuthLinkedIn();
            LinkedInProfile objProfile = new LinkedInProfile();
            List<Helper.AddlinkedinCompanyPage> lstAddLinkedinPage = new List<Helper.AddlinkedinCompanyPage>();
            try
            {
                _oauth.ConsumerKey = ConfigurationManager.AppSettings["LinkedinApiKey"];
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                logger.Error(ex.Message);
            }

            try
            {
                _oauth.ConsumerSecret = ConfigurationManager.AppSettings["LinkedinSecretKey"];
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                logger.Error(ex.Message);
            }
            string access_token_Url = "https://www.linkedin.com/uas/oauth2/accessToken";
            string access_token_postData = "grant_type=authorization_code&code=" + LinkedInManager.Code + "&redirect_uri=" + System.Web.HttpUtility.UrlEncode(ConfigurationManager.AppSettings["LinkedinCallBackURL"]) + "&client_id=" + ConfigurationManager.AppSettings["LinkedinApiKey"] + "&client_secret=" + ConfigurationManager.AppSettings["LinkedinSecretKey"];
            LinkedInProfile.UserProfile objUserProfile = new LinkedInProfile.UserProfile();
            string token = _oauth.APIWebRequestAccessToken("POST", access_token_Url, access_token_postData);
            var oathtoken = JObject.Parse(token);
            _oauth.Token = oathtoken["access_token"].ToString().TrimStart('"').TrimEnd('"');
            string response = _oauth.APIWebRequest("GET", GlobusLinkedinLib.App.Core.Global.GetLinkedInCompanyPageUrl, null);
            try
            {
                var companypage = JObject.Parse(response);
                foreach (var item in companypage["values"])
                {

                    Helper.AddlinkedinCompanyPage objAddLinkedinPage = new Helper.AddlinkedinCompanyPage();
                    objAddLinkedinPage.PageId = item["id"].ToString();
                    objAddLinkedinPage.PageName = item["name"].ToString();
                    objAddLinkedinPage._Oauth = _oauth;
                    lstAddLinkedinPage.Add(objAddLinkedinPage);
                }

                string data = new JavaScriptSerializer().Serialize(lstAddLinkedinPage);
                return Ok(data);
            }
            catch (Exception)
            {
                return Ok("No Company Page Found");
            }
        }


        [ActionName("AddLinkedinCompanyPage")]
        [HttpPost]
        public IHttpActionResult AddLinkedinCompanyPage(LinkedInManager LinkedInManager)
        {
            oAuthLinkedIn _oauth = new oAuthLinkedIn();
            _oauth = (oAuthLinkedIn)(new JavaScriptSerializer().Deserialize(LinkedInManager.Oauth, typeof(oAuthLinkedIn)));

            GlobusLinkedinLib.App.Core.LinkedinCompanyPage objLinedInCmpnyPage = new GlobusLinkedinLib.App.Core.LinkedinCompanyPage();
            GlobusLinkedinLib.App.Core.LinkedinCompanyPage.CompanyProfile objCompanyProfile = new GlobusLinkedinLib.App.Core.LinkedinCompanyPage.CompanyProfile();
            objCompanyProfile = objLinedInCmpnyPage.GetCompanyPageProfile(_oauth, LinkedInManager.ProfileId);
            GetPageProfile(objCompanyProfile, _oauth, LinkedInManager.UserId, LinkedInManager.ProfileId, LinkedInManager.GroupId);
            GetLinkedinCompanyPageFeeds(_oauth, LinkedInManager.UserId,LinkedInManager.ProfileId);
           return Ok();
        }


         [ActionName("PsotCommentOnLinkedinCompanyPageUpdate")]
         [HttpPost]
        public IHttpActionResult PsotCommentOnLinkedinCompanyPageUpdate(LinkedInManager LinkedInManager)
        {
            try
            {
                Domain.Socioboard.Domain.LinkedinCompanyPage objlicompanypage = new Domain.Socioboard.Domain.LinkedinCompanyPage();
                objlicompanypage = objLinkedCmpnyPgeRepo.getCompanyPageInformation(LinkedInManager.ProfileId);
                oAuthLinkedIn Linkedin_oauth = new oAuthLinkedIn();
                try
                {
                    Linkedin_oauth.ConsumerKey = ConfigurationManager.AppSettings["LinkedinApiKey"];
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

                try
                {
                    Linkedin_oauth.ConsumerSecret = ConfigurationManager.AppSettings["LinkedinSecretKey"];
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                Linkedin_oauth.Verifier = objlicompanypage.OAuthVerifier;
                Linkedin_oauth.TokenSecret = objlicompanypage.OAuthSecret;
                Linkedin_oauth.Token = objlicompanypage.OAuthToken;
                Linkedin_oauth.Id = objlicompanypage.LinkedinPageId;
                Linkedin_oauth.FirstName = objlicompanypage.LinkedinPageName;
                Company company = new Company();
                string res = company.SetCommentOnPagePost(Linkedin_oauth, LinkedInManager.ProfileId, LinkedInManager.Updatekey, LinkedInManager.comment);
                if (res != "Failed")
                {
                    Domain.Socioboard.Domain.LinkedinCompanyPagePosts lipost = new LinkedinCompanyPagePosts();
                    lipost = objLinkedinPagePostRepository.getCompanyPagPostInformation(LinkedInManager.Updatekey);
                    lipost.Comments = lipost.Comments + 1;
                    objLinkedinPagePostRepository.updateLinkedinPostCommentofPage(lipost);
                }
                string data= new JavaScriptSerializer().Serialize(res);
                return Ok(data);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return Ok("Something Went Wrong");
            }

        }


         [ActionName("DeleteLinkedinCompanyPage")]
         [HttpPost]
         public IHttpActionResult DeleteLinkedinCompanyPage(LinkedInManager LinkedInManager)
        {
            try
            {
                objLinkedCmpnyPgeRepo.DeleteLinkedInPageByPageid(LinkedInManager.ProfileId, Guid.Parse(LinkedInManager.UserId));
                objLinkedinPagePostRepository.deleteAllPostOfPage(LinkedInManager.ProfileId, Guid.Parse(LinkedInManager.UserId));
                //Domain.Socioboard.Domain.Team objTeam = objTeamRepository.GetTeamByGroupId(Guid.Parse(LinkedInManager.GroupId));
                //objTeamMemberProfileRepository.DeleteTeamMemberProfileByTeamIdProfileId(LinkedInManager.ProfileId, objTeam.Id);
                objSocialProfilesRepository.deleteProfile(Guid.Parse(LinkedInManager.UserId), LinkedInManager.ProfileId, "linkedincompanypage");
                grpProfileRepo.DeleteGroupProfile(Guid.Parse(LinkedInManager.UserId), LinkedInManager.ProfileId, Guid.Parse(LinkedInManager.GroupId), "linkedincompanypage");
                return Ok();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return Ok();
            }
        }

         [ActionName("DeleteLinkedinAccount")]
         [HttpPost]
         public IHttpActionResult DeleteLinkedinAccount(LinkedInManager LinkedInManager)
         {
             try
             {
                 objLinkedInAccountRepository.deleteLinkedinUser(LinkedInManager.ProfileId, Guid.Parse(LinkedInManager.UserId));
                 //Domain.Socioboard.Domain.Team objTeam = objTeamRepository.GetTeamByGroupId(Guid.Parse(LinkedInManager.GroupId));
                 //objTeamMemberProfileRepository.DeleteTeamMemberProfileByTeamIdProfileId(LinkedInManager.ProfileId, objTeam.Id);
                 objSocialProfilesRepository.deleteProfile(Guid.Parse(LinkedInManager.UserId), LinkedInManager.ProfileId, "linkedin");
                 grpProfileRepo.DeleteGroupProfile(Guid.Parse(LinkedInManager.UserId), LinkedInManager.ProfileId, Guid.Parse(LinkedInManager.GroupId), "linkedin");
                 return Ok();
             }
             catch (Exception ex)
             {
                 Console.WriteLine(ex.StackTrace);
                 return Ok();
             }
         }


         [ActionName("CreateUpdateOnLinkedinCompanyPage")]
         [HttpPost]
         public IHttpActionResult CreateUpdateOnLinkedinCompanyPage(LinkedInManager LinkedInManager)
         {
             try
             {
                 Domain.Socioboard.Domain.LinkedinCompanyPage objlicompanypage = new Domain.Socioboard.Domain.LinkedinCompanyPage();
                 objlicompanypage = objLinkedCmpnyPgeRepo.getCompanyPageInformation(LinkedInManager.ProfileId);
                 oAuthLinkedIn Linkedin_oauth = new oAuthLinkedIn();
                 try
                 {
                     Linkedin_oauth.ConsumerKey = ConfigurationManager.AppSettings["LinkedinApiKey"];
                 }
                 catch (Exception ex)
                 {
                     Console.WriteLine(ex.Message);
                 }

                 try
                 {
                     Linkedin_oauth.ConsumerSecret = ConfigurationManager.AppSettings["LinkedinSecretKey"];
                 }
                 catch (Exception ex)
                 {
                     Console.WriteLine(ex.Message);
                 }
                 Linkedin_oauth.Verifier = objlicompanypage.OAuthVerifier;
                 Linkedin_oauth.TokenSecret = objlicompanypage.OAuthSecret;
                 Linkedin_oauth.Token = objlicompanypage.OAuthToken;
                 Linkedin_oauth.Id = objlicompanypage.LinkedinPageId;
                 Linkedin_oauth.FirstName = objlicompanypage.LinkedinPageName;
                 Company company = new Company();
                 if (string.IsNullOrEmpty(LinkedInManager.ImageUrl))
                 {
                     string res = company.SetPostOnPage(Linkedin_oauth, LinkedInManager.ProfileId, LinkedInManager.comment);
                 }
                 else {
                     string imagepath = ConfigurationManager.AppSettings["DomainName"].ToString() + Regex.Split(LinkedInManager.ImageUrl, "wwwroot")[1].Replace("\\", "/");
                     string resdata = company.SetPostOnPageWithImage(Linkedin_oauth, LinkedInManager.ProfileId, imagepath, LinkedInManager.comment);
                 }
                 return Ok();
             }
             catch (Exception ex)
             {
                 Console.WriteLine(ex.StackTrace);
                 return Ok();
             }

         }


         [ActionName("GetAllCompanyPage")]
         [HttpGet]
         public List<Domain.Socioboard.Domain.LinkedinCompanyPage> GetAllCompanyPage()
         {

             List<Domain.Socioboard.Domain.LinkedinCompanyPage> lstCompany = objLinkedCmpnyPgeRepo.GetAllComapnayPage();
             return lstCompany;
         }

         [ActionName("UpdateLinkedInCompanyPage")]
         [HttpPost]
         public string UpdateLinkedInCompanyPage(LinkedInManager LinkedInManager)
         {
            oAuthLinkedIn _oauth = new oAuthLinkedIn();
            Domain.Socioboard.Domain.LinkedinCompanyPage _LinkedinCompanyPage = objLinkedCmpnyPgeRepo.getCompanyPageInformation(LinkedInManager.ProfileId);
            _oauth.ConsumerKey = ConfigurationManager.AppSettings["LinkedinApiKey"];
            _oauth.ConsumerSecret = ConfigurationManager.AppSettings["LinkedinSecretKey"];
            _oauth.Verifier = _LinkedinCompanyPage.OAuthVerifier;
            _oauth.TokenSecret = _LinkedinCompanyPage.OAuthSecret;
            _oauth.Token = _LinkedinCompanyPage.OAuthToken;
            _oauth.Id = _LinkedinCompanyPage.LinkedinPageId;
            _oauth.FirstName = _LinkedinCompanyPage.LinkedinPageName;
            GlobusLinkedinLib.App.Core.LinkedinCompanyPage objLinedInCmpnyPage = new GlobusLinkedinLib.App.Core.LinkedinCompanyPage();
            GlobusLinkedinLib.App.Core.LinkedinCompanyPage.CompanyProfile objCompanyProfile = new GlobusLinkedinLib.App.Core.LinkedinCompanyPage.CompanyProfile();
            objCompanyProfile = objLinedInCmpnyPage.GetCompanyPageProfile(_oauth, LinkedInManager.ProfileId);
            GetPageProfile(objCompanyProfile, _oauth, _LinkedinCompanyPage.UserId.ToString(), _LinkedinCompanyPage.LinkedinPageId, LinkedInManager.GroupId);
            GetLinkedinCompanyPageFeeds(_oauth, _LinkedinCompanyPage.UserId.ToString(), _LinkedinCompanyPage.LinkedinPageId);
            return "LinkedCompanyPageUpdated";
          }


         [ActionName("getScheduledMessage")]
         [HttpGet]
         public List<Domain.Socioboard.Domain.ScheduledMessage> getScheduledMessage()
         {
             try
             {
                 //Guid userid = Guid.Parse(UserId);
                 string profileType = "linkedin";
                 ScheduledMessageRepository objScheduledMessageRepository = new ScheduledMessageRepository();
                 List<Domain.Socioboard.Domain.ScheduledMessage> lstScheduledMessages = objScheduledMessageRepository.GetUnsentSchdeuledMessageByProfileType(profileType);

                 if (lstScheduledMessages == null)
                 {
                     lstScheduledMessages = new List<Domain.Socioboard.Domain.ScheduledMessage>();
                 }

                 return lstScheduledMessages;
             }
             catch (Exception ex)
             {
                 logger.Error(ex.Message);
                 Console.WriteLine(ex.StackTrace);
                 return new List<Domain.Socioboard.Domain.ScheduledMessage> ();
             }
         }


        public void GetPageProfile(dynamic data, oAuthLinkedIn _OAuth, string UserId, string CompanyPageId, string GroupId)
        {
            Domain.Socioboard.Domain.SocialProfile socioprofile = new Domain.Socioboard.Domain.SocialProfile();
            Domain.Socioboard.Domain.GroupProfile grpProfile = new Domain.Socioboard.Domain.GroupProfile();
            SocialProfilesRepository socioprofilerepo = new SocialProfilesRepository();
            Domain.Socioboard.Domain.LinkedinCompanyPage objLinkedincmpnypage = new Domain.Socioboard.Domain.LinkedinCompanyPage();
            LinkedinCompanyPageRepository objLinkedCmpnyPgeRepo = new LinkedinCompanyPageRepository();
            try
            {
                objLinkedincmpnypage.UserId = Guid.Parse(UserId);
                try
                {
                    objLinkedincmpnypage.LinkedinPageId = data.Pageid.ToString();
                }
                catch
                {
                }
                objLinkedincmpnypage.Id = Guid.NewGuid();
                try
                {
                    objLinkedincmpnypage.EmailDomains = data.EmailDomains.ToString();
                }
                catch { }
                objLinkedincmpnypage.LinkedinPageName = data.name.ToString();
                objLinkedincmpnypage.OAuthToken = _OAuth.Token;
                objLinkedincmpnypage.OAuthSecret = _OAuth.TokenSecret;
                objLinkedincmpnypage.OAuthVerifier = _OAuth.Verifier;
                try
                {
                    objLinkedincmpnypage.Description = data.description.ToString();
                }
                catch { }
                try
                {
                    objLinkedincmpnypage.FoundedYear = data.founded_year.ToString();
                }
                catch { }
                try
                {
                    objLinkedincmpnypage.EndYear = data.end_year.ToString();
                }
                catch { }
                try
                {
                    objLinkedincmpnypage.Locations = data.locations.ToString();
                }
                catch { }
                try
                {
                    objLinkedincmpnypage.Specialties = data.Specialties.ToString();
                }
                catch { }
                try
                {
                    objLinkedincmpnypage.WebsiteUrl = data.website_url.ToString();
                }
                catch { }
                try
                {
                    objLinkedincmpnypage.Status = data.status.ToString();
                }
                catch { }
                try
                {
                    objLinkedincmpnypage.EmployeeCountRange = data.employee_count_range.ToString();
                }
                catch { }
                try
                {
                    objLinkedincmpnypage.Industries = data.industries.ToString();
                }
                catch { }
                try
                {
                    string NuberOfFollower = data.num_followers.ToString();
                    objLinkedincmpnypage.NumFollowers = Convert.ToInt16(NuberOfFollower);
                }
                catch { }
                try
                {
                    objLinkedincmpnypage.CompanyType = data.company_type.ToString();
                }
                catch { }
                try
                {
                    objLinkedincmpnypage.LogoUrl = data.logo_url.ToString();
                }
                catch { }
                try
                {
                    objLinkedincmpnypage.SquareLogoUrl = data.square_logo_url.ToString();
                }
                catch { }
                try
                {
                    objLinkedincmpnypage.BlogRssUrl = data.blog_rss_url.ToString();
                }
                catch { }
                try
                {
                    objLinkedincmpnypage.UniversalName = data.universal_name.ToString();
                }
                catch { }
                #region SocialProfiles
                socioprofile.UserId = Guid.Parse(UserId);
                socioprofile.ProfileType = "linkedincompanypage";
                socioprofile.ProfileId = data.Pageid.ToString(); ;
                socioprofile.ProfileStatus = 1;
                socioprofile.ProfileDate = DateTime.Now;
                socioprofile.Id = Guid.NewGuid();
                #endregion

                #region TeamMemberProfile
                //Domain.Socioboard.Domain.Team objTeam = objTeamRepository.GetTeamByGroupId(Guid.Parse(GroupId));
                //objTeamMemberProfile = new Domain.Socioboard.Domain.TeamMemberProfile();
                //objTeamMemberProfile.Id = Guid.NewGuid();
                //objTeamMemberProfile.TeamId = objTeam.Id;
                //objTeamMemberProfile.Status = 1;
                //objTeamMemberProfile.ProfileType = "linkedincompanypage";
                //objTeamMemberProfile.StatusUpdateDate = DateTime.Now;
                //objTeamMemberProfile.ProfileId = socioprofile.ProfileId;
                
                grpProfile.Id = Guid.NewGuid();
                grpProfile.GroupId = Guid.Parse(GroupId);
                grpProfile.GroupOwnerId = objLinkedincmpnypage.UserId;
                grpProfile.ProfileId = objLinkedincmpnypage.LinkedinPageId;
                grpProfile.ProfileType = "linkedincompanypage";
                grpProfile.ProfileName = objLinkedincmpnypage.LinkedinPageName;
                grpProfile.EntryDate = DateTime.UtcNow;
                
                #endregion
            }
            catch
            {
            }
            try
            {
                if (!objSocialProfilesRepository.checkUserProfileExist(socioprofile))
                {
                    objSocialProfilesRepository.addNewProfileForUser(socioprofile);
                    grpProfileRepo.AddGroupProfile(grpProfile);
                }
                if (!string.IsNullOrEmpty(GroupId))
                {
                    if (!objTeamMemberProfileRepository.checkTeamMemberProfile(objTeamMemberProfile.TeamId, objLinkedincmpnypage.LinkedinPageId))
                    {
                        objTeamMemberProfileRepository.addNewTeamMember(objTeamMemberProfile);
                    } 
                }
                if (!objLinkedCmpnyPgeRepo.checkLinkedinPageExists(CompanyPageId, Guid.Parse(UserId)))
                {
                    objLinkedCmpnyPgeRepo.addLinkenCompanyPage(objLinkedincmpnypage);
                }
                else
                {
                    objLinkedincmpnypage.LinkedinPageId = CompanyPageId;
                    objLinkedCmpnyPgeRepo.updateLinkedinPage(objLinkedincmpnypage);
                }
            }
            catch
            {

            }

        }

        public void GetLinkedinCompanyPageFeeds(oAuthLinkedIn _oauth, string UserId, string PageId)
        {
            LinkedinPageUpdate objlinkedinpageupdate = new LinkedinPageUpdate();
            LinkedinPagePostRepository objlipagepostRepo = new LinkedinPagePostRepository();
            List<LinkedinPageUpdate.CompanyPagePosts> objcompanypagepost = new List<LinkedinPageUpdate.CompanyPagePosts>();
            objcompanypagepost = objlinkedinpageupdate.GetPagePosts(_oauth, PageId);
            LinkedinCompanyPagePosts lipagepost = new LinkedinCompanyPagePosts();
            foreach (var item in objcompanypagepost)
            {
                lipagepost.Id = Guid.NewGuid();
                lipagepost.Posts = item.Posts;
                lipagepost.PostDate = Convert.ToDateTime(item.PostDate);
                lipagepost.EntryDate = DateTime.Now;
                lipagepost.UserId = Guid.Parse(UserId);
                lipagepost.Type = item.Type;
                lipagepost.PostId = item.PostId;
                lipagepost.UpdateKey = item.UpdateKey;
                lipagepost.PageId = PageId;
                lipagepost.PostImageUrl = item.PostImageUrl;
                lipagepost.Likes = item.Likes;
                lipagepost.Comments = item.Comments;
                if (!objlipagepostRepo.checkLinkedInPostExists(lipagepost.PostId, lipagepost.UserId))
                {
                    objlipagepostRepo.addLinkedInPagepost(lipagepost);
                }
                else
                {
                    objlipagepostRepo.updateLinkedinPostofPage(lipagepost);
                }

            }
        }

    }


}

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Script.Services;
using System.Web.Services;
using Domain.Socioboard.Domain;
using GlobusLinkedinLib.Authentication;
using GlobusLinkedinLib.App.Core;
using GlobusLinkedinLib.LinkedIn.Core.SocialStreamMethods;
using System.Xml;
using System.IO;
using GlobusLinkedinLib.LinkedIn.Core.CompanyMethods;
using Api.Socioboard.Model;

namespace Api.Socioboard.Services
{
    /// <summary>
    /// Summary description for LinkedinCompanyPage
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class LinkedinCompanyPage : System.Web.Services.WebService
    {

        GroupsRepository objGroupsRepository = new GroupsRepository();
        TeamRepository objTeamRepository = new TeamRepository();
        Domain.Socioboard.Domain.Team objteam;
        TeamMemberProfileRepository objTeamMemberProfileRepository = new TeamMemberProfileRepository();
        Domain.Socioboard.Domain.TeamMemberProfile objTeamMemberProfile;
        SocialProfilesRepository objSocialProfilesRepository = new SocialProfilesRepository();
        Domain.Socioboard.Domain.SocialProfile objSocialProfile;
        LinkedinPagePostRepository objlinkedpagepostrepo = new LinkedinPagePostRepository();
        Domain.Socioboard.Domain.LinkedInFeed objLinkedInFeed;
        ScheduledMessageRepository objScheduledMessageRepository = new ScheduledMessageRepository();
        Domain.Socioboard.Domain.ScheduledMessage objScheduledMessage;
        LinkedinCompanyPageRepository objLinkedCmpnyPgeRepo = new LinkedinCompanyPageRepository();


        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string GetLinkedinCompanyPage(string oauth_token, string oauth_verifier, string reuqestTokenSecret, string consumerKey, string consumerSecret, string UserId, string GroupId)
        {
            List<Helper.AddlinkedinCompanyPage> lstAddLinkedinPage = new List<Helper.AddlinkedinCompanyPage>();
            string ret = string.Empty;
            oAuthLinkedIn _oauth = new oAuthLinkedIn();
            try
            {
                _oauth.ConsumerKey = consumerKey;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            try
            {
                _oauth.ConsumerSecret = consumerSecret;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            try
            {
                _oauth.Token = oauth_token;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            try
            {
                _oauth.TokenSecret = reuqestTokenSecret;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

            }
            try
            {
                _oauth.Verifier = oauth_verifier;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            try
            {
                _oauth.AccessTokenGet(oauth_token);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

            }

            XmlDocument xmlResult = new XmlDocument();
            XmlDocument xmlCompany = new XmlDocument();
            GlobusLinkedinLib.LinkedIn.Core.PeopleMethods.People peopleConnection = new GlobusLinkedinLib.LinkedIn.Core.PeopleMethods.People();
            xmlResult = peopleConnection.Get_UserProfile(_oauth);
            string UserProfileId = xmlResult.GetElementsByTagName("id")[0].InnerText;
            string response = _oauth.APIWebRequest("GET", GlobusLinkedinLib.App.Core.Global.GetCompanyUrl, null);

            string strLidPageDiv = string.Empty;
            xmlCompany.Load(new StringReader(response));

            string cnt = string.Empty;
            XmlElement root = xmlCompany.DocumentElement;
            if (root.HasAttribute("total"))
            {
                cnt = root.GetAttribute("total");
            }
            int total = Convert.ToInt16(cnt);
            if (total != 0)
            {
                for (int i = 0; i < total; i++)
                {
                    Helper.AddlinkedinCompanyPage objAddLinkedinPage = new Helper.AddlinkedinCompanyPage();
                    objAddLinkedinPage.PageId = xmlCompany.GetElementsByTagName("id")[i].InnerText;
                    objAddLinkedinPage.PageName = xmlCompany.GetElementsByTagName("name")[i].InnerText;
                    objAddLinkedinPage._Oauth = _oauth;

                    lstAddLinkedinPage.Add(objAddLinkedinPage);
                }
            }

            return new JavaScriptSerializer().Serialize(lstAddLinkedinPage);
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string AddLinkedinCompanyPage(string CompanyPageId, string oauth, string UserId, string GroupId)
        {
            oAuthLinkedIn _oauth = new oAuthLinkedIn();
            _oauth = (oAuthLinkedIn)(new JavaScriptSerializer().Deserialize(oauth, typeof(oAuthLinkedIn)));

            GlobusLinkedinLib.App.Core.LinkedinCompanyPage objLinedInCmpnyPage = new GlobusLinkedinLib.App.Core.LinkedinCompanyPage();
            GlobusLinkedinLib.App.Core.LinkedinCompanyPage.CompanyProfile objCompanyProfile = new GlobusLinkedinLib.App.Core.LinkedinCompanyPage.CompanyProfile();
            objCompanyProfile = objLinedInCmpnyPage.GetCompanyPageProfile(_oauth, CompanyPageId);
            GetPageProfile(objCompanyProfile, _oauth, UserId, CompanyPageId, GroupId);
            GetLinkedinCompanyPageFeeds(_oauth, UserId, CompanyPageId);
            return "";
        }

        public void GetPageProfile(dynamic data, oAuthLinkedIn _OAuth, string UserId, string CompanyPageId, string GroupId)
        {
            Domain.Socioboard.Domain.SocialProfile socioprofile = new Domain.Socioboard.Domain.SocialProfile();
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
                Domain.Socioboard.Domain.Team objTeam = objTeamRepository.GetTeamByGroupId(Guid.Parse(GroupId));
                objTeamMemberProfile = new Domain.Socioboard.Domain.TeamMemberProfile();
                objTeamMemberProfile.Id = Guid.NewGuid();
                objTeamMemberProfile.TeamId = objTeam.Id;
                objTeamMemberProfile.Status = 1;
                objTeamMemberProfile.ProfileType = "linkedincompanypage";
                objTeamMemberProfile.StatusUpdateDate = DateTime.Now;
                objTeamMemberProfile.ProfileId = socioprofile.ProfileId;
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
                }
                if (!objTeamMemberProfileRepository.checkTeamMemberProfile(objTeamMemberProfile.TeamId, objLinkedincmpnypage.LinkedinPageId))
                {
                    objTeamMemberProfileRepository.addNewTeamMember(objTeamMemberProfile);
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

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string GetLinkedinPagePostComment(string Oauth, string updatekey)
        {
            oAuthLinkedIn _OAuth = new oAuthLinkedIn();
            _OAuth = (oAuthLinkedIn)(new JavaScriptSerializer().Deserialize(Oauth, typeof(oAuthLinkedIn)));
            List<Domain.Socioboard.Domain.LinkdeinPageComment> objLiPageComment = new List<Domain.Socioboard.Domain.LinkdeinPageComment>();

            try
            {
                GlobusLinkedinLib.App.Core.LinkedinPageComment objLiPagePostCmnt = new GlobusLinkedinLib.App.Core.LinkedinPageComment();

                List<LinkedinPageComment.CompanyPageComment> objLiPostcmnt = new List<LinkedinPageComment.CompanyPageComment>();

                objLiPostcmnt = objLiPagePostCmnt.GetPagePostscomment(_OAuth, updatekey);



                foreach (var item in objLiPostcmnt)
                {
                    Domain.Socioboard.Domain.LinkdeinPageComment lipagepostcmnt = new Domain.Socioboard.Domain.LinkdeinPageComment();
                    lipagepostcmnt.Comment = item.Comment;
                    lipagepostcmnt.FirstName = item.FirstName;
                    lipagepostcmnt.LastName = item.LastName;
                    lipagepostcmnt.CommentTime = Convert.ToDateTime(item.CommentTime);
                    if (item.PictureUrl != null)
                    {

                        lipagepostcmnt.PictureUrl = item.PictureUrl;

                    }
                    else
                    {
                        lipagepostcmnt.PictureUrl = "../Contents/img/blank_img.png";
                    }

                    objLiPageComment.Add(lipagepostcmnt);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
            }

            return new JavaScriptSerializer().Serialize(objLiPageComment);
        }


        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string CreateUpdateOnLinkedinCompanyPage(string UserId, string PageId, string PostMessage)
        {
            try
            {
                Domain.Socioboard.Domain.LinkedinCompanyPage objlicompanypage = new Domain.Socioboard.Domain.LinkedinCompanyPage();
                objlicompanypage = objLinkedCmpnyPgeRepo.getCompanyPageInformation(PageId);
                oAuthLinkedIn Linkedin_oauth = new oAuthLinkedIn();
                Linkedin_oauth.Verifier = objlicompanypage.OAuthVerifier;
                Linkedin_oauth.TokenSecret = objlicompanypage.OAuthSecret;
                Linkedin_oauth.Token = objlicompanypage.OAuthToken;
                Linkedin_oauth.Id = objlicompanypage.LinkedinPageId;
                Linkedin_oauth.FirstName = objlicompanypage.LinkedinPageName;
                Company company = new Company();
                string res = company.SetPostOnPage(Linkedin_oauth, PageId, PostMessage);
                return new JavaScriptSerializer().Serialize(res);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return "Something Went Wrong";
            }

        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string PsotCommentOnLinkedinCompanyPageUpdate(string UserId, string PageId, string Updatekey, string Comment)
        {
            try
            {
                Domain.Socioboard.Domain.LinkedinCompanyPage objlicompanypage = new Domain.Socioboard.Domain.LinkedinCompanyPage();
                objlicompanypage = objLinkedCmpnyPgeRepo.getCompanyPageInformation(PageId);
                oAuthLinkedIn Linkedin_oauth = new oAuthLinkedIn();
                Linkedin_oauth.Verifier = objlicompanypage.OAuthVerifier;
                Linkedin_oauth.TokenSecret = objlicompanypage.OAuthSecret;
                Linkedin_oauth.Token = objlicompanypage.OAuthToken;
                Linkedin_oauth.Id = objlicompanypage.LinkedinPageId;
                Linkedin_oauth.FirstName = objlicompanypage.LinkedinPageName;
                Company company = new Company();
                string res = company.SetCommentOnPagePost(Linkedin_oauth, PageId, Updatekey, Comment);
                return new JavaScriptSerializer().Serialize(res);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return "Something Went Wrong";
            }

        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string PutLikeOnLinkedinCompanyPageUpdate(string UserId, string PageId, string Updatekey, string IsLike)
        {
            try
            {
                string msg = string.Empty;

                int isLike = Convert.ToInt16(IsLike);

                if (isLike == 1)
                {
                    msg = "false";
                }
                else
                { msg = "true"; }

                Domain.Socioboard.Domain.LinkedinCompanyPage objlicompanypage = new Domain.Socioboard.Domain.LinkedinCompanyPage();
                objlicompanypage = objLinkedCmpnyPgeRepo.getCompanyPageInformation(PageId);
                oAuthLinkedIn Linkedin_oauth = new oAuthLinkedIn();
                Linkedin_oauth.Verifier = objlicompanypage.OAuthVerifier;
                Linkedin_oauth.TokenSecret = objlicompanypage.OAuthSecret;
                Linkedin_oauth.Token = objlicompanypage.OAuthToken;
                Linkedin_oauth.Id = objlicompanypage.LinkedinPageId;
                Linkedin_oauth.FirstName = objlicompanypage.LinkedinPageName;
                Company company = new Company();
                string res = company.SetLikeUpdateOnPagePost(Linkedin_oauth, Updatekey, msg);
                return new JavaScriptSerializer().Serialize(res);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return "Something Went Wrong";
            }

        }


        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string DeleteLinkedinCompanyPage(string UserId, string LinkedinPageId, string GroupId)
        {
            try
            {
                objLinkedCmpnyPgeRepo.DeleteLinkedInPageByPageid(LinkedinPageId, Guid.Parse(UserId));
                objlinkedpagepostrepo.deleteAllPostOfPage(LinkedinPageId, Guid.Parse(UserId));
                Domain.Socioboard.Domain.Team objTeam = objTeamRepository.GetTeamByGroupId(Guid.Parse(GroupId));
                objTeamMemberProfileRepository.DeleteTeamMemberProfileByTeamIdProfileId(LinkedinPageId, objTeam.Id);
                objSocialProfilesRepository.deleteProfile(Guid.Parse(UserId), LinkedinPageId);
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
        public string GetLinkedinCompanyPageDetailsByUserIdAndPageId(string UserId, string LinkedinPageId)
        {
            Domain.Socioboard.Domain.LinkedinCompanyPage LinkedinCompanyPage=new Domain.Socioboard.Domain.LinkedinCompanyPage ();
            try
            {
                Guid Userid = Guid.Parse(UserId);
                if (objLinkedCmpnyPgeRepo.checkLinkedinPageExists(LinkedinPageId, Userid))
                {
                    LinkedinCompanyPage = objLinkedCmpnyPgeRepo.getCompanyPageInformation(Userid, LinkedinPageId);
                }
                else
                {
                    LinkedinCompanyPage = objLinkedCmpnyPgeRepo.getCompanyPageInformation(LinkedinPageId);
                }
                return new JavaScriptSerializer().Serialize(LinkedinCompanyPage);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return new JavaScriptSerializer().Serialize("Please Try Again");
            }
        }


        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string GetAllLinkedinCompanyPageByUserIdAndGroupId(string userid, string groupid)
        {
            try
            {
                List<Domain.Socioboard.Domain.LinkedinCompanyPage> lstLinkedInCompanyPage = new List<Domain.Socioboard.Domain.LinkedinCompanyPage>();
                Domain.Socioboard.Domain.Team objTeam = objTeamRepository.GetTeamByGroupId(Guid.Parse(groupid));
                List<Domain.Socioboard.Domain.TeamMemberProfile> lstTeamMemberProfile = objTeamMemberProfileRepository.GetTeamMemberProfileByTeamIdAndProfileType(objTeam.Id, "linkedincompanypage");
                foreach (var item in lstTeamMemberProfile)
                {
                    try
                    {
                        lstLinkedInCompanyPage.Add(objLinkedCmpnyPgeRepo.getCompanyPageInformation(Guid.Parse(userid), item.ProfileId));
                    }
                    catch (Exception)
                    {

                    }
                }
                return new JavaScriptSerializer().Serialize(lstLinkedInCompanyPage);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return "Something Went Wrong";
            }
        }



        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string GetAllLinkedinCompanyPagePostsByUserIdAndProfileId(string UserId, string LinkedinPageId)
        {
            try
            {
                List<Domain.Socioboard.Domain.LinkedinCompanyPagePosts> lstlinkedinCompanyPagePost = objlinkedpagepostrepo.getAllLinkedInPagePostsOfUser(Guid.Parse(UserId), LinkedinPageId);
                return new JavaScriptSerializer().Serialize(lstlinkedinCompanyPagePost);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return "Something Went Wrong";
            }
        }
    }
}

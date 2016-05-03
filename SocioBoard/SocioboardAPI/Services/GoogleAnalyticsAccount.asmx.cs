using Api.Socioboard.Helper;
using Api.Socioboard.Model;
using SocioBoard.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
    public class GoogleAnalyticsAccount : System.Web.Services.WebService
    {
        GoogleAnalyticsAccountRepository objGoogleAnalyticsAccountRepository = new GoogleAnalyticsAccountRepository();
        TeamRepository objTeamRepository = new TeamRepository();
        TeamMemberProfileRepository objTeamMemberProfileRepository = new TeamMemberProfileRepository();
        GroupProfileRepository objGroupProfileRepository = new GroupProfileRepository();
        SocialProfilesRepository objSocialProfilesRepository = new SocialProfilesRepository();
        MongoRepository TwitterUrlMentionRepo = new MongoRepository("TwitterUrlMentions");
        MongoRepository ArticlesAndBlogsRepo = new MongoRepository("ArticlesAndBlogs");

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string GetGoogleAnalyticsAccountByUser(string UserId)
        {
            List<Domain.Socioboard.Domain.GoogleAnalyticsAccount> lstGoogleAnalyticsAccountRepository = new List<Domain.Socioboard.Domain.GoogleAnalyticsAccount>();
            try
            {
                lstGoogleAnalyticsAccountRepository = objGoogleAnalyticsAccountRepository.getGoogelAnalyticsAccountsByUser(Guid.Parse(UserId));
            }
            catch (Exception ex)
            {
                lstGoogleAnalyticsAccountRepository = new List<Domain.Socioboard.Domain.GoogleAnalyticsAccount>();
            }
            return new JavaScriptSerializer().Serialize(lstGoogleAnalyticsAccountRepository);
        }
        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string GetGooglePlusAccountDetailsById(string UserId, string GAId)
        {
            Domain.Socioboard.Domain.GoogleAnalyticsAccount _GoogleAnalyticsAccount = new Domain.Socioboard.Domain.GoogleAnalyticsAccount();
            try
            {
                _GoogleAnalyticsAccount = objGoogleAnalyticsAccountRepository.getGoogleAnalyticsAccountDetailsById(GAId, Guid.Parse(UserId));
            }
            catch (Exception ex)
            {
                _GoogleAnalyticsAccount = new Domain.Socioboard.Domain.GoogleAnalyticsAccount();
            }
            return new JavaScriptSerializer().Serialize(_GoogleAnalyticsAccount);
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string GetAllGoogleAnalyticsProfiles()
        {
            List<Domain.Socioboard.Domain.GoogleAnalyticsAccount> lstGoogleAnalyticsAccount = new List<Domain.Socioboard.Domain.GoogleAnalyticsAccount>();
            try
            {
                lstGoogleAnalyticsAccount = objGoogleAnalyticsAccountRepository.getAllGoogleAnalyticsAccounts();
            }
            catch (Exception ex)
            {
                lstGoogleAnalyticsAccount = new List<Domain.Socioboard.Domain.GoogleAnalyticsAccount>();
            }
            return new JavaScriptSerializer().Serialize(lstGoogleAnalyticsAccount);
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string DeleteGoogelAnalyticsAccount(string UserId, string ProfileId, string GroupId,string profiletype)
        {
            try
            {
                objGoogleAnalyticsAccountRepository.deleteGoogelAnalyticsUser(ProfileId, Guid.Parse(UserId));
                Domain.Socioboard.Domain.Team objTeam = objTeamRepository.GetTeamByGroupId(Guid.Parse(GroupId));
                objTeamMemberProfileRepository.DeleteTeamMemberProfileByTeamIdProfileId(ProfileId, objTeam.Id);
                objGroupProfileRepository.DeleteGroupProfile(Guid.Parse(UserId), ProfileId, Guid.Parse(GroupId),profiletype);
                objSocialProfilesRepository.deleteProfile(Guid.Parse(UserId), ProfileId, profiletype);
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
        public string GetGoogleAnalyticsData(string ProfileId)
        {
            Domain.Socioboard.Domain.GoogleAnalyticsReport _GoogleAnalyticsReport = new Domain.Socioboard.Domain.GoogleAnalyticsReport();
            try
            {
                _GoogleAnalyticsReport = objGoogleAnalyticsAccountRepository.GetGoogleAnalyticsReport(ProfileId);
            }
            catch (Exception ex)
            {
                _GoogleAnalyticsReport = new Domain.Socioboard.Domain.GoogleAnalyticsReport();
            }
            return new JavaScriptSerializer().Serialize(_GoogleAnalyticsReport);
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string GetTwitterUrlMention(string hostName, string days)
        {
            List<Domain.Socioboard.MongoDomain.TwitterUrlMentions> lstTwitterUrlMentions = new List<Domain.Socioboard.MongoDomain.TwitterUrlMentions>();
            try
            {
                long date = DateTime.UtcNow.AddDays(-Int32.Parse(days)).Date.ToUnixTimestamp();
                var ret = TwitterUrlMentionRepo.Find<Domain.Socioboard.MongoDomain.TwitterUrlMentions>(t => t.HostName.Equals(hostName) && t.Feeddate > date);
                var task = Task.Run(async () =>
                {
                    return await ret;
                });
                IList<Domain.Socioboard.MongoDomain.TwitterUrlMentions> _lstTwitterUrlMentions = task.Result.Where(t => t.Feeddate > date).ToList();
                lstTwitterUrlMentions = (List<Domain.Socioboard.MongoDomain.TwitterUrlMentions>)_lstTwitterUrlMentions.OrderByDescending(t=>t.Feeddate).ToList();
            }
            catch (Exception ex)
            {
                lstTwitterUrlMentions = new List<Domain.Socioboard.MongoDomain.TwitterUrlMentions>();
            }

            return new JavaScriptSerializer().Serialize(lstTwitterUrlMentions);
        }
        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string GetArticleAndBlogs(string hostName, string days)
        {
            List<Domain.Socioboard.MongoDomain.ArticlesAndBlogs> lstArticlesAndBlogs = new List<Domain.Socioboard.MongoDomain.ArticlesAndBlogs>();
            try
            {
                long date = DateTime.UtcNow.AddDays(-Int32.Parse(days)).Date.ToUnixTimestamp();
                var ret = ArticlesAndBlogsRepo.Find<Domain.Socioboard.MongoDomain.ArticlesAndBlogs>(t => t.HostName.Equals(hostName) && t.Created_Time > date);
                var task = Task.Run(async () =>
                {
                    return await ret;
                });
                IList<Domain.Socioboard.MongoDomain.ArticlesAndBlogs> _lstArticlesAndBlogs = task.Result.Where(t => t.Created_Time > date).ToList();
                lstArticlesAndBlogs = (List<Domain.Socioboard.MongoDomain.ArticlesAndBlogs>)_lstArticlesAndBlogs.OrderByDescending(t=>t.Created_Time).ToList();
            }
            catch (Exception ex)
            {
                lstArticlesAndBlogs = new List<Domain.Socioboard.MongoDomain.ArticlesAndBlogs>();
            }
            return new JavaScriptSerializer().Serialize(lstArticlesAndBlogs);
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Script.Services;
using System.Web.Script.Serialization;
using SocioBoard.Model;
using SocioBoard.Domain;

namespace SocialSuitePro.API
{
    /// <summary>
    /// Summary description for Instagram
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
     [ScriptService]
    public class Instagram : System.Web.Services.WebService
    {

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string UserInformation(string UserId, string InstagramId)
        {
            try
            {

                Guid Userid = Guid.Parse(UserId);
                InstagramAccountRepository linkedinaccrepo = new InstagramAccountRepository();
                InstagramAccount LinkedAccount = linkedinaccrepo.getInstagramAccountDetailsById(InstagramId, Userid);
                return new JavaScriptSerializer().Serialize(LinkedAccount);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return new JavaScriptSerializer().Serialize("Please Try Again");
            }
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string GetInstagramFeeds(string UserId, string InstagramId)
        {
            try
            {

                InstagramFeedRepository objInsFeedRepo = new InstagramFeedRepository();
                List<InstagramFeed> lstInsFeed = objInsFeedRepo.getAllInstagramFeedsOfUser(Guid.Parse(UserId), InstagramId);
                return new JavaScriptSerializer().Serialize(lstInsFeed);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return null;
            }
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string GetInstagramComments(string UserId, string InstagramId, string InsFeedId)
        {
            try
            {
                InstagramCommentRepository objInsCmtRepo = new InstagramCommentRepository();
                List<InstagramComment> lstInsComment = objInsCmtRepo.getAllInstagramCommentsOfUser(Guid.Parse(UserId), InstagramId, InsFeedId);
                return new JavaScriptSerializer().Serialize(lstInsComment);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return null;
            }
        }
    }
}

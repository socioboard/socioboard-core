using Api.Socioboard.Helper;
using Api.Socioboard.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Script.Services;
using System.Web.Services;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Threading.Tasks;

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
    public class InstagramComment : System.Web.Services.WebService
    {
        InstagramCommentRepository _InstagramCommentRepository = new InstagramCommentRepository();


        //[WebMethod]
        //[ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        //public string GetInstagramFeedsComment(string UserId, string FeedId)
        //{
        //    try
        //    {
        //        List<Domain.Socioboard.Domain.InstagramComment> lstInstagramFeed = _InstagramCommentRepository.getAllInstagramCommentsOfUser(Guid.Parse(UserId), FeedId);
        //        return new JavaScriptSerializer().Serialize(lstInstagramFeed);
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex.StackTrace);
        //        return new JavaScriptSerializer().Serialize("Please Try Again");
        //    }
        //}

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string GetInstagramFeedsComment(string UserId, string FeedId)
        {
            List<Domain.Socioboard.MongoDomain.InstagramComment> lstInstagramFeed = new List<Domain.Socioboard.MongoDomain.InstagramComment>();
            try
            {
                MongoRepository instagramCommentRepo = new MongoRepository("InstagramComment");
                var ret = instagramCommentRepo.Find<Domain.Socioboard.MongoDomain.InstagramComment>(t => t.FeedId.Equals(FeedId));
                var task = Task.Run(async () =>
                    {
                        return await ret;
                    });
                IList<Domain.Socioboard.MongoDomain.InstagramComment> _lstInstagramFeed = task.Result.OrderBy(x => x.CommentDate).ToList();
                lstInstagramFeed = _lstInstagramFeed.ToList();
            }
            catch (Exception ex)
            {
                lstInstagramFeed = new List<Domain.Socioboard.MongoDomain.InstagramComment>();
            }
            return new JavaScriptSerializer().Serialize(lstInstagramFeed);
        }





    }
}

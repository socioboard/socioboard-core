using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Script.Services;
using System.Web.Services;
using Domain.Socioboard.Domain;
using Api.Socioboard.Model;
using System.Threading.Tasks;
using MongoDB.Driver;
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
    public class InstagramFeed : System.Web.Services.WebService
    {

        InstagramFeedRepository objInstagramFeedRepository = new InstagramFeedRepository();
        MongoRepository instagramFeedRepo = new MongoRepository("InstagramFeed");

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string GetInstagramFeeds(string UserId, string InstagramId)
        {

            List<Domain.Socioboard.MongoDomain.InstagramFeed> lstInstagramFeed;
            try
            {
                var ret = instagramFeedRepo.Find<Domain.Socioboard.MongoDomain.InstagramFeed>(t => t.InstagramId.Equals(InstagramId));
                var task = Task.Run(async () =>
                {
                    return await ret;
                });
                IList<Domain.Socioboard.MongoDomain.InstagramFeed> _lstInstagramFeed = task.Result;
                lstInstagramFeed = _lstInstagramFeed.ToList();
            }
            catch (Exception ex)
            {
                lstInstagramFeed = new List<Domain.Socioboard.MongoDomain.InstagramFeed>();
            }
            return new JavaScriptSerializer().Serialize(lstInstagramFeed);
            //try
            //{
            //    List<Domain.Socioboard.MongoDomain.InstagramFeed> lstInstagramFeed = objInstagramFeedRepository.getAllInstagramFeedsOfUser(Guid.Parse(UserId), LinkedInId);
            //    return new JavaScriptSerializer().Serialize(lstInstagramFeed);
            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine(ex.StackTrace);
            //    return new JavaScriptSerializer().Serialize("Please Try Again");
            //}
        }


        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string GetFeedsOfProfileWithRange(string UserId, string LinkedInId, string noOfDataToSkip, string noOfDataFromTop)
        {
            List<Domain.Socioboard.MongoDomain.InstagramFeed> lstInstagramFeed;
            try
            {
                try
                {
                    var builder = Builders<Domain.Socioboard.MongoDomain.InstagramFeed>.Sort;
                    var sort = builder.Descending(t => t.FeedDate);
                    var ret = instagramFeedRepo.FindWithRange<Domain.Socioboard.MongoDomain.InstagramFeed>(t => t.InstagramId.Equals(LinkedInId), sort, Int32.Parse(noOfDataToSkip), Int32.Parse(noOfDataFromTop));
                    var task = Task.Run(async () =>
                    {
                        return await ret;
                    });
                    IList<Domain.Socioboard.MongoDomain.InstagramFeed> _lstInstagramFeed = task.Result;
                    lstInstagramFeed = _lstInstagramFeed.ToList();

                }
                catch (Exception ex)
                {
                    lstInstagramFeed = new List<Domain.Socioboard.MongoDomain.InstagramFeed>();
                }
                //List<Domain.Socioboard.Domain.InstagramFeed> lstInstagramFeed = objInstagramFeedRepository.getAllInstagramFeedsOfUser(Guid.Parse(UserId), LinkedInId, Convert.ToInt32(noOfDataToSkip), Convert.ToInt32(noOfDataFromTop));
                return new JavaScriptSerializer().Serialize(lstInstagramFeed);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return new JavaScriptSerializer().Serialize(new List<Domain.Socioboard.MongoDomain.InstagramFeed>());
            }
        }
    }
}

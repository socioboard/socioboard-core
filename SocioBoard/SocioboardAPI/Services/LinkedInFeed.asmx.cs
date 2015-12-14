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
using MongoDB.Bson;
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
    public class LinkedInFeed : System.Web.Services.WebService
    {
        LinkedInFeedRepository objLinkedInFeedRepository = new LinkedInFeedRepository();
        MongoRepository linkedinFeedRepo = new MongoRepository("LinkedInFeed");

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string GetLinkedInFeeds(string UserId, string LinkedInId)
        {
            List<Domain.Socioboard.MongoDomain.LinkedInFeed> lstlinkedinfeeds = new List<Domain.Socioboard.MongoDomain.LinkedInFeed>();
            try
            {
                //if (objLinkedInFeedRepository.checkLinkedInUserExists(LinkedInId, Guid.Parse(UserId)))
                //{
                //    lstlinkedinfeeds = objLinkedInFeedRepository.getAllLinkedInFeedsOfUser(Guid.Parse(UserId), LinkedInId);
                //}
                //else
                //{
                //    lstlinkedinfeeds = objLinkedInFeedRepository.getAllLinkedInUserFeeds(LinkedInId);
                //}
                var ret = linkedinFeedRepo.Find<Domain.Socioboard.MongoDomain.LinkedInFeed>(t=>t.ProfileId.Equals(LinkedInId));
                var task = Task.Run(async () => {
                    return await ret;
                });
                IList<Domain.Socioboard.MongoDomain.LinkedInFeed> _lstLinkedInFeed = ret.Result;
                lstlinkedinfeeds = _lstLinkedInFeed.OrderByDescending(t=>t.FeedsDate).ToList();
                
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                lstlinkedinfeeds = new List<Domain.Socioboard.MongoDomain.LinkedInFeed>();
            }
            return new JavaScriptSerializer().Serialize(lstlinkedinfeeds);
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string GetLinkedInFeedsByUserIdAndProfileIdUsingLimit(string UserId, string LinkedInId, string noOfDataToSkip, string noOfResultsFromTop)
        {
            List<Domain.Socioboard.MongoDomain.LinkedInFeed> lstlinkedinfeeds = new List<Domain.Socioboard.MongoDomain.LinkedInFeed>();
            try
            {
                //if (objLinkedInFeedRepository.checkLinkedInUserExists(LinkedInId, Guid.Parse(UserId)))
                //{
                //    lstlinkedinfeeds = objLinkedInFeedRepository.getAllLinkedInFeedsOfUserOfSBUserWithRangeAndProfileId(Guid.Parse(UserId), LinkedInId, noOfDataToSkip, noOfResultsFromTop);
                //}
                //else
                //{
                //    lstlinkedinfeeds = objLinkedInFeedRepository.getAllLinkedInFeedsOfUserOfSBUserWithRangeByProfileId(LinkedInId, noOfDataToSkip, noOfResultsFromTop);
                //}

                var builder = Builders<Domain.Socioboard.MongoDomain.LinkedInFeed>.Sort;
                var sort = builder.Descending(t => t.FeedsDate);
                var ret = linkedinFeedRepo.FindWithRange<Domain.Socioboard.MongoDomain.LinkedInFeed>(t => t.ProfileId.Equals(LinkedInId), sort, Int32.Parse(noOfDataToSkip), Int32.Parse(noOfResultsFromTop));
                var task = Task.Run(async () => {
                    return await ret;
                });
                IList<Domain.Socioboard.MongoDomain.LinkedInFeed> _lstlinkedinfeeds = task.Result;
                lstlinkedinfeeds = _lstlinkedinfeeds.ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                lstlinkedinfeeds = new List<Domain.Socioboard.MongoDomain.LinkedInFeed>();
            }

            return new JavaScriptSerializer().Serialize(lstlinkedinfeeds);
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string GetLinkedInFeeds1(string UserId, string LinkedInId, int count)
        {
            List<Domain.Socioboard.MongoDomain.LinkedInFeed> lstlinkedinfeeds;
            try
            {
                //List<Domain.Socioboard.Domain.LinkedInFeed> lstlinkedinfeeds =objLinkedInFeedRepository.getAllLinkedInFeedsOfUser(Guid.Parse(UserId), LinkedInId, count);

                var builder = Builders<Domain.Socioboard.MongoDomain.LinkedInFeed>.Sort;
                var sort = builder.Descending(t => t.FeedsDate);
                var ret = linkedinFeedRepo.FindWithRange<Domain.Socioboard.MongoDomain.LinkedInFeed>(t => t.ProfileId.Equals(LinkedInId),sort,count,10);
                var task = Task.Run(async () => {
                    return await ret;
                });
                IList<Domain.Socioboard.MongoDomain.LinkedInFeed> _lstlinkedinfeeds = task.Result;
                lstlinkedinfeeds = _lstlinkedinfeeds.ToList();
                
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                lstlinkedinfeeds = new List<Domain.Socioboard.MongoDomain.LinkedInFeed>();
            }
            return new JavaScriptSerializer().Serialize(lstlinkedinfeeds);
        }

        // Edited by Antima

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string GetAllLinkedInFeedsOfProfileWithId(string ProfileId, string Id)
        {
            Domain.Socioboard.MongoDomain.LinkedInFeed linkedinfeeds;
            try
            {
                ObjectId id = ObjectId.Parse(Id);
                //lstlinkedinfeeds = objLinkedInFeedRepository.getAllLinkedInFeedsOfProfileWithId(ProfileId, Id);
                var ret = linkedinFeedRepo.Find<Domain.Socioboard.MongoDomain.LinkedInFeed>(t=>t.Id.Equals(id));
                var task = Task.Run(async () => {
                    return await ret;
                });
                IList<Domain.Socioboard.MongoDomain.LinkedInFeed> lstlinkedinfeeds = task.Result;
                linkedinfeeds=(Domain.Socioboard.MongoDomain.LinkedInFeed)lstlinkedinfeeds[0];

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                linkedinfeeds = new Domain.Socioboard.MongoDomain.LinkedInFeed();
            }
            return new JavaScriptSerializer().Serialize(linkedinfeeds);
        }
    }
}

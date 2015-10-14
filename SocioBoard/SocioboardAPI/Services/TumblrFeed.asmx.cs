using Api.Socioboard.Helper;
using Api.Socioboard.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Script.Services;
using System.Web.Services;
using System.Globalization;
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
    public class TumblrFeed : System.Web.Services.WebService
    {
        //TumblrFeedRepository objTumblrFeedRepository = new TumblrFeedRepository();
        MongoRepository tumblrFeedRepo = new MongoRepository("TumblrFeed");
        private CultureInfo provider = CultureInfo.InvariantCulture;
        private string format = "yyyy/MM/dd HH:mm:ss";
        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string GetAllTumblrFeedOfUsers(string UserId, string ProfileId)
        {
            List<Domain.Socioboard.MongoDomain.TumblrFeed> lstTumblrFeed;
            var ret = tumblrFeedRepo.Find<Domain.Socioboard.MongoDomain.TumblrFeed>(t => t.ProfileId.Equals(ProfileId));
            try
            {
                var task = Task.Run(async () =>
                   {
                       return await ret;
                   });

                IList<Domain.Socioboard.MongoDomain.TumblrFeed> _lstTumblrFeed = task.Result;
                lstTumblrFeed = _lstTumblrFeed.ToList();
            }
            catch (Exception ex)
            {
                lstTumblrFeed = new List<Domain.Socioboard.MongoDomain.TumblrFeed>();
            }
            return new JavaScriptSerializer().Serialize(lstTumblrFeed);
            //try
            //{
            //    List<Domain.Socioboard.Domain.TumblrFeed> lstTumblrFeed = objTumblrFeedRepository.GetFeedsOfProfile(ProfileId, Guid.Parse(UserId));
            //    return new JavaScriptSerializer().Serialize(lstTumblrFeed);
            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine(ex.StackTrace);
            //    return "Something Went Wrong";
            //}
        }

        //
        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string GetAllTumblrFeedOfUsersWithRange(string UserId, string ProfileId, string noOfDataToSkip)
        {
            List<Domain.Socioboard.MongoDomain.TumblrFeed> lstTumblrFeed;
            try
            {
                var builder = Builders<Domain.Socioboard.MongoDomain.TumblrFeed>.Sort;
                var sort = builder.Descending(t => t.date);
                var ret = tumblrFeedRepo.FindWithRange<Domain.Socioboard.MongoDomain.TumblrFeed>(t => t.ProfileId.Equals(ProfileId), sort, Int32.Parse(noOfDataToSkip), 6);

                var task = Task.Run(async () =>
                   {
                       return await ret;
                   });

                IList<Domain.Socioboard.MongoDomain.TumblrFeed> _lstTumblrFeed = task.Result;
                lstTumblrFeed = _lstTumblrFeed.ToList();
            }
            catch (Exception ex)
            {
                lstTumblrFeed = new List<Domain.Socioboard.MongoDomain.TumblrFeed>();
            }

            return new JavaScriptSerializer().Serialize(lstTumblrFeed);
            //try
            //{
            //    List<Domain.Socioboard.Domain.TumblrFeed> lstTumblrFeed = objTumblrFeedRepository.GetFeedsOfProfileWithRange(ProfileId, Guid.Parse(UserId), Convert.ToInt32(noOfDataToSkip));
            //    return new JavaScriptSerializer().Serialize(lstTumblrFeed);
            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine(ex.StackTrace);
            //    return "Something Went Wrong";
            //}
        }
    }
}

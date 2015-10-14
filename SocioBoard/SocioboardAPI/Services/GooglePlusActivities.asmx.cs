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
    public class GooglePlusActivities : System.Web.Services.WebService
    {
        GooglePlusActivitiesRepository objGooglePlusActivitiesRepository = new GooglePlusActivitiesRepository();
        MongoRepository gplusFeedRepo = new MongoRepository("GoogleplusFeed");
        [WebMethod]
        public string getgoogleplusActivity(string UserId, string ProfileId)
        {
            List<Domain.Socioboard.MongoDomain.GoogleplusFeed> lstGoogleplusFeed;

            try
            {
                var ret = gplusFeedRepo.Find<Domain.Socioboard.MongoDomain.GoogleplusFeed>(t => t.GpUserId.Equals(ProfileId));
                var task = Task.Run(async () =>
                {
                    return await ret;
                });
                IList<Domain.Socioboard.MongoDomain.GoogleplusFeed> _lstGoogleplusFeed = task.Result;
                lstGoogleplusFeed = _lstGoogleplusFeed.OrderByDescending(t => t.PublishedDate).ToList();
            }
            catch (Exception ex)
            {
                lstGoogleplusFeed = new List<Domain.Socioboard.MongoDomain.GoogleplusFeed>();
            }
            //List<Domain.Socioboard.Domain.GooglePlusActivities> lstGooglePlusActivities = objGooglePlusActivitiesRepository.getgoogleplusActivity(Guid.Parse(UserId),ProfileId);
            return new JavaScriptSerializer().Serialize(lstGoogleplusFeed);
        }
    }
}

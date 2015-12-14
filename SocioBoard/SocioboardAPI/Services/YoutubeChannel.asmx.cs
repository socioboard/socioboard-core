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

    public class YoutubeChannel : System.Web.Services.WebService
    {
        YoutubeChannelRepository objYoutubeChannelRepository = new YoutubeChannelRepository();
        MongoRepository youtubefeedrepo = new MongoRepository("YouTubeFeed");
        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string GetAllYoutubeChannelByUserIdAndProfileId(string UserId, string ProfileId)
        {
             List<Domain.Socioboard.Domain.YoutubeChannel> lstYoutubeChannel=new List<Domain.Socioboard.Domain.YoutubeChannel>();
            try
            {
                if (objYoutubeChannelRepository.checkYoutubeChannelExists(ProfileId, Guid.Parse(UserId)))
                {
                    lstYoutubeChannel = objYoutubeChannelRepository.getYoutubeChannelDetailsById(ProfileId, Guid.Parse(UserId));
                }
                else
                {
                    lstYoutubeChannel = objYoutubeChannelRepository.getYoutubeChannelDetailsById(ProfileId);
                }

                return new JavaScriptSerializer().Serialize(lstYoutubeChannel);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return "Something Went Wrong";
            }
        }
        [WebMethod]
        public string GetAllYoutubeVideos(string profileId)
        {
            List<Domain.Socioboard.MongoDomain.YouTubeFeed> lstYouTubeFeed;
            try
            {

                var ret = youtubefeedrepo.Find<Domain.Socioboard.MongoDomain.YouTubeFeed>(t => t.YoutubeId.Equals(profileId));
                var task = Task.Run(async () =>
                {
                    return await ret;
                });
                IList<Domain.Socioboard.MongoDomain.YouTubeFeed> _lstYouTubeFeed = task.Result;
                lstYouTubeFeed = _lstYouTubeFeed.OrderByDescending(t => t.PublishTime).ToList();
            }
            catch (Exception ex)
            {
                lstYouTubeFeed = new List<Domain.Socioboard.MongoDomain.YouTubeFeed>();
            }

            return new JavaScriptSerializer().Serialize(lstYouTubeFeed);

        }

        
    }
}

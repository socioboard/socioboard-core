using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Script.Serialization;
using Domain.Socioboard.Domain;
using log4net;



namespace SocioboardDataServices
{
    class YouTubeData : ISocialSiteData
    {

        ILog logger = LogManager.GetLogger(typeof(YouTubeData));
        public string GetData(object UserId,string youtubeId )
        {
            string ret = string.Empty;
            Guid userId = (Guid)UserId;
            YoutubeAccount ObjYoutubeAccount = new YoutubeAccount();
            Api.YoutubeAccount.YoutubeAccount ApiObjYoutubeAccount = new Api.YoutubeAccount.YoutubeAccount();
            List<Domain.Socioboard.Domain.YoutubeAccount> LstYoutubeAccount = (List<Domain.Socioboard.Domain.YoutubeAccount>)(new JavaScriptSerializer().Deserialize(ApiObjYoutubeAccount.GetAllYoutubeAccountDetailsById(userId.ToString()), typeof(List<Domain.Socioboard.Domain.YoutubeAccount>)));
            List<YoutubeAccount> lstYoutubeAccount = new List<YoutubeAccount>();
            foreach (YoutubeAccount Lstitem in LstYoutubeAccount)
            {
                Api.Youtube.Youtube ApiObjYoutube = new Api.Youtube.Youtube();
               ret= ApiObjYoutube.getYoutubeData(Lstitem.UserId.ToString(), Lstitem.Ytuserid);
            }
            return ret;
        }
       

        public void GetSearchData(object parameters)
        {
            throw new NotImplementedException();
        }
    }
}

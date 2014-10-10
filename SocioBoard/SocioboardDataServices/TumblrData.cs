using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Domain.Socioboard.Domain;
using System.Collections;
using System.Text.RegularExpressions;
using System.Web.Script.Serialization;

namespace SocioboardDataServices
{
    public class TumblrData : ISocialSiteData
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="UserId">Tumblr User id</param>
        public string GetData(object UserId,string TumblrId)
        {
            string ret = string.Empty;
            Guid userId = (Guid)UserId;

            TumblrFeed objTumblrFeed = new TumblrFeed();
            Api.TumblrFeed.TumblrFeed ApiObjTumblrFeed = new Api.TumblrFeed.TumblrFeed();
            Api.TumblrAccount.TumblrAccount ApiObjTumblrAccount = new Api.TumblrAccount.TumblrAccount();
            //List<TumblrAccount> lstTumblrAccount = new List<TumblrAccount>();

            List<Domain.Socioboard.Domain.TumblrAccount> lstTumblrAccount = (List<Domain.Socioboard.Domain.TumblrAccount>)(new JavaScriptSerializer().Deserialize(ApiObjTumblrAccount.GetAllTumblrAccountsOfUser(userId.ToString()), typeof(List<Domain.Socioboard.Domain.TumblrAccount>)));


            foreach (TumblrAccount lstItem in lstTumblrAccount)
            {
                Api.Tumblr.Tumblr ApiObjTumblr = new Api.Tumblr.Tumblr();
                ret = ApiObjTumblr.getTumblrData(lstItem.UserId.ToString(), lstItem.tblrUserName);
                             
            }
            return ret;
        }

        public void GetSearchData(object parameters)
        {
            throw new NotImplementedException();
        }
    }
}

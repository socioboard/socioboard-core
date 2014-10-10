using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Domain.Socioboard.Domain;
using System.Collections;
using System.Configuration;
using System.Web.Script.Serialization;

namespace SocioboardDataServices
{
    public class InstagramData : ISocialSiteData
    {
        public string GetData(object objUserId, string instagramid)
        {
            string ret = string.Empty;
            Guid UserId = (Guid)objUserId;
            Api.InstagramAccount.InstagramAccount ApiObjInstagramAccount = new Api.InstagramAccount.InstagramAccount();
            List<Domain.Socioboard.Domain.InstagramAccount> lstInstagramAccount = (List<Domain.Socioboard.Domain.InstagramAccount>)(new JavaScriptSerializer().Deserialize(ApiObjInstagramAccount.GetAllInstagramAccounts(UserId.ToString()), typeof(List<Domain.Socioboard.Domain.InstagramAccount>)));
            foreach (InstagramAccount item in lstInstagramAccount)
            {
                Api.Instagram.Instagram apiObjInstagram = new Api.Instagram.Instagram();
                ret = apiObjInstagram.getInstagramData(item.UserId.ToString(), item.InstagramId);
            }


            return ret;
        }




        public void GetSearchData(object parameters)
        {
            throw new NotImplementedException();
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SocioBoard.Helper;
using SocioBoard.Model;
using System.Collections;
using SocioBoard.Domain;

namespace SocialSiteDataService
{
    public class GoogleAnalyticsData
    {
        public void getAnalytics(object obj)
        {
            GanalyticsHelper objGaHelper = new GanalyticsHelper();
            Guid user = Guid.Parse(obj.ToString());

            GoogleAnalyticsAccountRepository gAccRepo = new GoogleAnalyticsAccountRepository();
            ArrayList aslt = gAccRepo.getGoogelAnalyticsAccountsOfUser(user);


            foreach (var item in aslt)
            {
                Array temp = (Array)item;
                      objGaHelper.getCountryAnalyticsApi(temp.GetValue(0).ToString(), user);
                objGaHelper.getYearWiseAnalyticsApi(temp.GetValue(0).ToString(), user);

            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using GlobusGooglePlusLib.App.Core;
using GlobusGooglePlusLib.Authentication;

namespace GlobusGooglePlusLib.GAnalytics.Core.AnalyticsMethod
{
    public class Analytics
    {
        public string getAnalyticsData(string strProfileId,string metricDimension,string strdtFrom,string strdtTo,string strToken)
        {
            string strData = string.Empty;
            oAuthToken objToken = new oAuthToken();
            try
            {
                string strDataUrl = Globals.strGetGaAnalytics + strProfileId + "&" + metricDimension + "&start-date=" + strdtFrom + "&end-date=" + strdtTo + "&access_token=" + strToken;
                strData=objToken.WebRequest(GlobusGooglePlusLib.Authentication.oAuthToken.Method.GET, strDataUrl, "");
            }
            catch (Exception Err)
            {
                Console.Write(Err.StackTrace);
            }
            return strData;
        }
    }
}

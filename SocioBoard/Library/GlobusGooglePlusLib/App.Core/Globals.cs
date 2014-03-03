using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GlobusGooglePlusLib.App.Core
{
    public static class Globals
    {
        public static string strAuthentication = "https://accounts.google.com/o/oauth2/auth";
        public static string strRefreshToken = "https://accounts.google.com/o/oauth2/token";
        public static string strUserInfo = "https://www.googleapis.com/oauth2/v1/userinfo?alt=json";

        #region People
        public static string strGetPeopleProfile = "https://www.googleapis.com/plus/v1/people/";
        public static string strGetSearchPeople = "https://www.googleapis.com/plus/v1/people?query=";
        public static string strGetPeopleListByActivity = "https://www.googleapis.com/plus/v1/activities/";
        public static string strGetPeopleList = "https://www.googleapis.com/plus/v1/people/"; 
        #endregion

        #region Activities
        public static string strGetActivitiesList = "https://www.googleapis.com/plus/v1/people/";
        public static string strGetActivityById = "https://www.googleapis.com/plus/v1/activities/";
        public static string strGetSearchActivity = "https://www.googleapis.com/plus/v1/activities/"; 
        #endregion

        #region Comments
        public static string strGetCommentListByActivityId = "https://www.googleapis.com/plus/v1/activities/";
        public static string strGetCommentByCommentId = "https://www.googleapis.com/plus/v1/comments/"; 
        #endregion

        #region Moments
        public static string strMoments = "https://www.googleapis.com/plus/v1/people/";
        public static string strRemoveMoments = "https://www.googleapis.com/plus/v1/moments/"; 
        #endregion


        #region Google Analytics
        public static string strgetGaAccounts = "https://www.googleapis.com/analytics/v2.4/management/accounts/";
        public static string strGetGaAnalytics = "https://www.googleapis.com/analytics/v2.4/data?ids=ga:";
        #endregion

    }
}

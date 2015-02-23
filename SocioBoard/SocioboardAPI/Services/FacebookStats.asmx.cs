using Api.Socioboard.Helper;
using Api.Socioboard.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Script.Services;
using System.Web.Services;
using SocioBoard.Model;
using Facebook;
using log4net;

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
    public class FacebookStats : System.Web.Services.WebService
    {
        ILog logger = LogManager.GetLogger(typeof(FacebookStats));

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public bool AddFacebookFriendsGender(string ProfileId, string FacebookUserId)
        {
            Api.Socioboard.Services.FacebookAccount _FacebookAccount = new FacebookAccount();
            Domain.Socioboard.Domain.FacebookAccount _facebookAccount = new Domain.Socioboard.Domain.FacebookAccount();

            _facebookAccount = (Domain.Socioboard.Domain.FacebookAccount)(new System.Web.Script.Serialization.JavaScriptSerializer().Deserialize(_FacebookAccount.getFacebookAccountDetailsById(ProfileId, FacebookUserId), typeof(Domain.Socioboard.Domain.FacebookAccount)));

            if (string.IsNullOrEmpty(_facebookAccount.AccessToken))
            {
                _facebookAccount = new Domain.Socioboard.Domain.FacebookAccount();
                Api.Socioboard.Services.FacebookAccountRepository _FacebookAccountRepository = new FacebookAccountRepository();

                System.Collections.ArrayList lstFacebookAccounts = _FacebookAccountRepository.getAllFacebookAccounts();

                Random _random = new Random();
                var rnum = _random.Next(0, lstFacebookAccounts.Count - 1);
                _facebookAccount = (Domain.Socioboard.Domain.FacebookAccount)lstFacebookAccounts[rnum];
            }
            int malecount = 0;
            int femalecount = 0;

            Domain.Socioboard.Domain.FacebookStats objfbStats = new Domain.Socioboard.Domain.FacebookStats();
            FacebookStatsRepository objFBStatsRepo = new FacebookStatsRepository();

            FacebookClient fb = new FacebookClient(); 
            fb.AccessToken = _facebookAccount.AccessToken;
            try
            {
                dynamic data = fb.Get("v2.0/me/friends?fields=gender");

                //dynamic data, dynamic profile, Guid userId


                foreach (var item in data["data"])
                {

                    try
                    {
                        if (item["gender"] == "male")
                            malecount++;
                        else if (item["gender"] == "female")
                            femalecount++;
                    }
                    catch (Exception ex)
                    {
                        logger.Error(ex.Message);
                        logger.Error(ex.StackTrace);
                    }

                }
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                logger.Error(ex.StackTrace);
                return false;
            }
            objfbStats.EntryDate = DateTime.Now;
            objfbStats.FbUserId = _facebookAccount.FbUserId;//profile["id"].ToString();
            objfbStats.FemaleCount = femalecount;
            objfbStats.Id = Guid.NewGuid();
            objfbStats.MaleCount = malecount;
            objfbStats.UserId = _facebookAccount.UserId;
            objfbStats.FanCount = getfanCount(objfbStats, _facebookAccount.AccessToken);

            if (objFBStatsRepo.checkFacebookStatsExists(objfbStats.FbUserId.ToString(), objfbStats.UserId, objfbStats.FanCount, objfbStats.MaleCount, objfbStats.FemaleCount))
            {
                objFBStatsRepo.addFacebookStats(objfbStats);
            }


            return true;
        }


        private int getfanCount(Domain.Socioboard.Domain.FacebookStats objfbsts, string accessToken)
        {
            int friendscnt = 0;
            long friendscount = 0;
            try
            {

                FacebookClient fb = new FacebookClient();

                //string accessToken = HttpContext.Current.Session["accesstoken"].ToString();

                fb.AccessToken = accessToken;

                var client = new FacebookClient();

                dynamic me = fb.Get("me");

                dynamic friedscount = fb.Get("fql", new { q = "SELECT friend_count FROM user WHERE uid=me()" });

                foreach (var friend in friedscount.data)
                {
                    friendscount = friend.friend_count;
                }

                friendscnt = Convert.ToInt32(friendscount);
            }
            catch (Exception ex)
            {
                //logger.Error(ex.StackTrace);
                //Console.WriteLine(ex.StackTrace);
            }
            return friendscnt;
        }


    }
}

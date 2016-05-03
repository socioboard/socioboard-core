using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using Api.Socioboard.Helper;
using System.Web.Script.Serialization;
using System.Web.Script.Services;
using NHibernate.Linq;
using NHibernate.Criterion;
using log4net;
using System.Configuration;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Text;
using System.Web.Script.Serialization;
using System.Xml;
using GlobusInstagramLib.Authentication;
using GlobusLinkedinLib.Authentication;
using GlobusTwitterLib.Authentication;
using log4net;
using Newtonsoft.Json.Linq;
using Facebook;
using Api.Socioboard.Helper;

namespace Api.Socioboard.Services
{
    /// <summary>
    /// Summary description for InstagramReports
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class InstagramReports : System.Web.Services.WebService
    {
        ILog logger = LogManager.GetLogger(typeof(InstagramReports));

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string GetInstagramAccounts()
        {
            List<Domain.Socioboard.Domain.InstagramAccount> groups = new List<Domain.Socioboard.Domain.InstagramAccount>();
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                try
                {
                    groups = session.CreateQuery("from InstagramAccount").List<Domain.Socioboard.Domain.InstagramAccount>().ToList();
                }
                catch (Exception e)
                {
                    logger.Error(e.Message);
                    logger.Error(e.StackTrace);
                }
            }
            return new JavaScriptSerializer().Serialize(groups);
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string GetInstaAccountById(string profileid)
        {
            Domain.Socioboard.Domain.InstagramAccount _InstaAccount = new Domain.Socioboard.Domain.InstagramAccount();
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                try
                {
                    NHibernate.IQuery querry = session.CreateQuery("from InstagramAccount i where i.InstagramId = : id").SetParameter("id", profileid);
                    _InstaAccount = (Domain.Socioboard.Domain.InstagramAccount)querry.UniqueResult();
                }
                catch (Exception e)
                {
                    logger.Error(e.Message);
                    logger.Error(e.StackTrace);
                }
            }
            return new JavaScriptSerializer().Serialize(_InstaAccount);
        }
        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string GetVideoPosts(string profileid)
        {
            Domain.Socioboard.Domain.ReturnData r = new Domain.Socioboard.Domain.ReturnData();
            DateTime present_date = DateTime.Now;
            string perday_15 = string.Empty;
            string perday_30 = string.Empty;
            string perday_60 = string.Empty;
            string perday_90 = string.Empty;
            int item_15 = 0;
            int item_30 = 0;
            int item_60 = 0;
            int item_90 = 0;
            string ret_string = string.Empty;
            List<Domain.Socioboard.Domain.InstagramSelfFeed> InstagramSelfFeed_15 = new List<Domain.Socioboard.Domain.InstagramSelfFeed>();
            List<Domain.Socioboard.Domain.InstagramSelfFeed> InstagramSelfFeed_30 = new List<Domain.Socioboard.Domain.InstagramSelfFeed>();
            List<Domain.Socioboard.Domain.InstagramSelfFeed> InstagramSelfFeed_60 = new List<Domain.Socioboard.Domain.InstagramSelfFeed>();
            List<Domain.Socioboard.Domain.InstagramSelfFeed> InstagramSelfFeed_90 = new List<Domain.Socioboard.Domain.InstagramSelfFeed>();
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                try
                {
                    InstagramSelfFeed_90 = session.Query<Domain.Socioboard.Domain.InstagramSelfFeed>().Where(m => m.Created_Time < present_date.AddDays(1).Date && m.Created_Time > present_date.AddDays(-90).Date && m.Type == "video" && m.ProfileId == profileid).GroupBy(x => x.FeedId).Select(g => g.First()).ToList();

                    InstagramSelfFeed_60 = InstagramSelfFeed_90.Where(m => m.Created_Time < present_date.AddDays(1).Date && m.Created_Time > present_date.AddDays(-60).Date).ToList();

                    InstagramSelfFeed_30 = InstagramSelfFeed_90.Where(m => m.Created_Time < present_date.AddDays(1).Date && m.Created_Time > present_date.AddDays(-30).Date).ToList();

                    InstagramSelfFeed_15 = InstagramSelfFeed_90.Where(m => m.Created_Time < present_date.AddDays(1).Date && m.Created_Time > present_date.AddDays(-15).Date).ToList();

                    item_90 = InstagramSelfFeed_90.Count;

                    item_60 = InstagramSelfFeed_60.Count;

                    item_30 = InstagramSelfFeed_30.Count;

                    item_15 = InstagramSelfFeed_15.Count;
                }

                catch (Exception e)
                {
                    logger.Error(e.Message);
                    logger.Error(e.StackTrace);
                }
                present_date = DateTime.Now;
                while (present_date.Date != DateTime.Now.Date.AddDays(-90))
                {
                    try
                    {
                        List<Domain.Socioboard.Domain.InstagramSelfFeed> _InstagramSelfFeed = new List<Domain.Socioboard.Domain.InstagramSelfFeed>();

                        //using (NHibernate.ISession session4 = SessionFactory.GetNewSession())
                        //{
                        int item = 0;
                        try
                        {
                            //item = session4.QueryOver<Domain.Socioboard.Domain.InstagramSelfFeed>().Where(m => m.Created_Time >= present_date.Date.AddSeconds(1) && m.Created_Time <= present_date.AddDays(1).Date.AddSeconds(-1) && m.Type == "video" && m.ProfileId == profileid)
                            //                             .Select(Projections.CountDistinct<Domain.Socioboard.Domain.InstagramSelfFeed>(x => x.FeedId))
                            //                             .FutureValue<int>().Value;

                            _InstagramSelfFeed = InstagramSelfFeed_90.Where(m => m.Created_Time >= present_date.Date.AddSeconds(1) && m.Created_Time <= present_date.AddDays(1).Date.AddSeconds(-1)).ToList();

                            item = _InstagramSelfFeed.Count;

                        }
                        catch (Exception e)
                        {
                            item = 0;
                        }
                        int add_data = item;
                        perday_90 = perday_90 + add_data.ToString() + ",";

                        present_date = present_date.AddDays(-1);
                        logger.Error("perdayvideoposts >>" + perday_90);
                        //}
                    }
                    catch (Exception e)
                    {
                        logger.Error("perdayvideopostserror >>" + e.Message);
                    }
                }
                try
                {
                    string[] arr = perday_90.Split(',');
                    for (int i = 0; i < 15; i++)
                    {
                        perday_15 = perday_15 + arr[i] + ",";
                    }
                    for (int i = 0; i < 30; i++)
                    {
                        perday_30 = perday_30 + arr[i] + ",";
                    }
                    for (int i = 0; i < 60; i++)
                    {
                        perday_60 = perday_60 + arr[i] + ",";
                    }
                }
                catch (Exception)
                {
                }
                r._15 = item_15;
                r._30 = item_30;
                r._60 = item_60;
                r._90 = item_90;
                r.perday_15 = perday_15;
                r.perday_30 = perday_30;
                r.perday_60 = perday_60;
                r.perday_90 = perday_90;
            }
            ret_string = new JavaScriptSerializer().Serialize(r);
            return ret_string;
        }
        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string GetImagePosts(string profileid)
        {
            Domain.Socioboard.Domain.ReturnData r = new Domain.Socioboard.Domain.ReturnData();
            DateTime present_date = DateTime.Now;
            string perday_15 = string.Empty;
            string perday_30 = string.Empty;
            string perday_60 = string.Empty;
            string perday_90 = string.Empty;
            int item_15 = 0;
            int item_30 = 0;
            int item_60 = 0;
            int item_90 = 0;
            string ret_string = string.Empty;

            List<Domain.Socioboard.Domain.InstagramSelfFeed> InstagramSelfFeed_15 = new List<Domain.Socioboard.Domain.InstagramSelfFeed>();
            List<Domain.Socioboard.Domain.InstagramSelfFeed> InstagramSelfFeed_30 = new List<Domain.Socioboard.Domain.InstagramSelfFeed>();
            List<Domain.Socioboard.Domain.InstagramSelfFeed> InstagramSelfFeed_60 = new List<Domain.Socioboard.Domain.InstagramSelfFeed>();
            List<Domain.Socioboard.Domain.InstagramSelfFeed> InstagramSelfFeed_90 = new List<Domain.Socioboard.Domain.InstagramSelfFeed>();

            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                try
                {
                    InstagramSelfFeed_90 = session.Query<Domain.Socioboard.Domain.InstagramSelfFeed>().Where(m => m.Created_Time < present_date.AddDays(1).Date && m.Created_Time > present_date.AddDays(-90).Date && m.Type == "image" && m.ProfileId == profileid).GroupBy(x => x.FeedId).Select(g => g.First()).ToList();

                    InstagramSelfFeed_60 = InstagramSelfFeed_90.Where(m => m.Created_Time < present_date.AddDays(1).Date && m.Created_Time > present_date.AddDays(-60).Date).ToList();

                    InstagramSelfFeed_30 = InstagramSelfFeed_90.Where(m => m.Created_Time < present_date.AddDays(1).Date && m.Created_Time > present_date.AddDays(-30).Date).ToList();

                    InstagramSelfFeed_15 = InstagramSelfFeed_90.Where(m => m.Created_Time < present_date.AddDays(1).Date && m.Created_Time > present_date.AddDays(-15).Date).ToList();

                    item_90=InstagramSelfFeed_90.Count;

                    item_60=InstagramSelfFeed_60.Count;

                    item_30=InstagramSelfFeed_30.Count;

                    item_15=InstagramSelfFeed_15.Count;
                }

                catch (Exception e)
                {
                    logger.Error(e.Message);
                    logger.Error(e.StackTrace);
                }


                present_date = DateTime.Now;

                while (present_date.Date != DateTime.Now.Date.AddDays(-90))
                {

                    try
                    {
                        List<Domain.Socioboard.Domain.InstagramSelfFeed> _InstagramSelfFeed = new List<Domain.Socioboard.Domain.InstagramSelfFeed>();
                        //using (NHibernate.ISession session4 = SessionFactory.GetNewSession())
                        //{
                            int item = 0;
                            try
                            {

                                _InstagramSelfFeed = InstagramSelfFeed_90.Where(m => m.Created_Time >= present_date.Date.AddSeconds(1) && m.Created_Time <= present_date.AddDays(1).Date.AddSeconds(-1)).ToList();
                                //item = session4.QueryOver<Domain.Socioboard.Domain.InstagramSelfFeed>().Where(m => m.Created_Time >= present_date.Date.AddSeconds(1) && m.Created_Time <= present_date.AddDays(1).Date.AddSeconds(-1) && m.Type == "image" && m.ProfileId == profileid)
                                //                             .Select(Projections.CountDistinct<Domain.Socioboard.Domain.InstagramSelfFeed>(x => x.FeedId))
                                //                             .FutureValue<int>().Value;
                                item = _InstagramSelfFeed.Count;


                            }
                            catch (Exception e)
                            {

                                item = 0;
                            }
                            int add_data = item;
                            perday_90 = perday_90 + add_data.ToString() + ",";

                            present_date = present_date.AddDays(-1);
                            logger.Error("perdayimageposts >>" + perday_90);
                        //}
                    }
                    catch (Exception e)
                    {
                        Console.Write(e.StackTrace);
                    }
                }
                try
                {
                    string[] arr = perday_90.Split(',');
                    for (int i = 0; i < 15; i++)
                    {
                        perday_15 = perday_15 + arr[i] + ",";
                    }
                    for (int i = 0; i < 30; i++)
                    {
                        perday_30 = perday_30 + arr[i] + ",";
                    }
                    for (int i = 0; i < 60; i++)
                    {
                        perday_60 = perday_60 + arr[i] + ",";
                    }
                }
                catch (Exception)
                {
                }
                r._15 = item_15;
                r._30 = item_30;
                r._60 = item_60;
                r._90 = item_90;
                r.perday_15 = perday_15;
                r.perday_30 = perday_30;
                r.perday_60 = perday_60;
                r.perday_90 = perday_90;
            }
            ret_string = new JavaScriptSerializer().Serialize(r);
            return ret_string;
        }
        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string InstagramUserDetail(string profileid)
        {
            List<Domain.Socioboard.Domain.InstagramUserDetails> userdetails = new List<Domain.Socioboard.Domain.InstagramUserDetails>();
            Domain.Socioboard.Domain.InstagramUserDetails userdetail = new Domain.Socioboard.Domain.InstagramUserDetails();
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                try
                {
                    userdetails = session.CreateQuery("from InstagramUserDetails i where i.Profile_Id = : Profileid order by Created_Time desc").SetParameter("Profileid", profileid).List<Domain.Socioboard.Domain.InstagramUserDetails>().ToList();
                    userdetail = userdetails.First();
                }
                catch (Exception e)
                {
                    try
                    {
                        List<Domain.Socioboard.Domain.InstagramAccount> lstAccount = new List<Domain.Socioboard.Domain.InstagramAccount>();
                        Domain.Socioboard.Domain.InstagramAccount _Account = new Domain.Socioboard.Domain.InstagramAccount();

                        lstAccount = session.CreateQuery("from InstagramAccount i where i.InstagramId = : Profileid").SetParameter("Profileid", profileid).List<Domain.Socioboard.Domain.InstagramAccount>().ToList();
                        _Account = lstAccount.First();

                        userdetail.Profile_Id = _Account.InstagramId;
                        userdetail.Insta_Name = _Account.InsUserName;
                        userdetail.Full_Name = _Account.InsUserName;
                        userdetail.Follower = _Account.Followers.ToString();
                        userdetail.Following = _Account.FollowedBy.ToString();
                        userdetail.Created_Time = DateTime.Now;
                        userdetail.Media_Count = _Account.TotalImages.ToString();
                    }
                    catch { }

                    logger.Error(e.Message);
                    logger.Error(e.StackTrace);
                }
            }
            return new JavaScriptSerializer().Serialize(userdetail);

        }


        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]

        public string InstagramFollowerGained(string profileid)
        {
            Domain.Socioboard.Domain.ReturnData r = new Domain.Socioboard.Domain.ReturnData();
            DateTime present_date = DateTime.Now;
            string perday_15 = string.Empty;
            string perday_30 = string.Empty;
            string perday_60 = string.Empty;
            string perday_90 = string.Empty;

            string ret_string = string.Empty;
            List<Domain.Socioboard.Domain.InstagramUserDetails> follower_15 = new List<Domain.Socioboard.Domain.InstagramUserDetails>();
            List<Domain.Socioboard.Domain.InstagramUserDetails> follower_30 = new List<Domain.Socioboard.Domain.InstagramUserDetails>();
            List<Domain.Socioboard.Domain.InstagramUserDetails> follower_60 = new List<Domain.Socioboard.Domain.InstagramUserDetails>();
            List<Domain.Socioboard.Domain.InstagramUserDetails> follower_90 = new List<Domain.Socioboard.Domain.InstagramUserDetails>();
            Domain.Socioboard.Domain.InstagramUserDetails _15_first = new Domain.Socioboard.Domain.InstagramUserDetails();
            Domain.Socioboard.Domain.InstagramUserDetails _15_last = new Domain.Socioboard.Domain.InstagramUserDetails();
            Domain.Socioboard.Domain.InstagramUserDetails _30_first = new Domain.Socioboard.Domain.InstagramUserDetails();
            Domain.Socioboard.Domain.InstagramUserDetails _30_last = new Domain.Socioboard.Domain.InstagramUserDetails();
            Domain.Socioboard.Domain.InstagramUserDetails _60_first = new Domain.Socioboard.Domain.InstagramUserDetails();
            Domain.Socioboard.Domain.InstagramUserDetails _60_last = new Domain.Socioboard.Domain.InstagramUserDetails();
            Domain.Socioboard.Domain.InstagramUserDetails _90_first = new Domain.Socioboard.Domain.InstagramUserDetails();
            Domain.Socioboard.Domain.InstagramUserDetails _90_last = new Domain.Socioboard.Domain.InstagramUserDetails();

            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                try
                {
                    string follow_str = "from InstagramUserDetails i where i.Profile_Id = : ProfileId";

                    try
                    {
                        follower_90 = session.CreateQuery(follow_str).SetParameter("ProfileId", profileid)
                                                                        .List<Domain.Socioboard.Domain.InstagramUserDetails>().ToList().Where(x => x.Created_Time < DateTime.Now.AddDays(1).Date && x.Created_Time > DateTime.Now.AddDays(-90).Date).OrderByDescending(y => y.Created_Time).ToList();

                        follower_60 = follower_90.Where(x => x.Created_Time < DateTime.Now.AddDays(1).Date && x.Created_Time > DateTime.Now.AddDays(-60).Date).OrderByDescending(y => y.Created_Time).ToList();

                        follower_30 = follower_90.Where(x => x.Created_Time < DateTime.Now.AddDays(1).Date && x.Created_Time > DateTime.Now.AddDays(-30).Date).OrderByDescending(y => y.Created_Time).ToList();

                        follower_15 = follower_90.Where(x => x.Created_Time < DateTime.Now.AddDays(1).Date && x.Created_Time > DateTime.Now.AddDays(-15).Date).OrderByDescending(y => y.Created_Time).ToList();

                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.StackTrace);
                        logger.Error(e.Message);
                        logger.Error(e.StackTrace);
                    }
                    present_date = DateTime.Now;
                    while (present_date.Date != DateTime.Now.Date.AddDays(-90))
                    {

                        try
                        {

                            //using (NHibernate.ISession session2 = SessionFactory.GetNewSession())
                            //{

                            //List<Domain.Socioboard.Domain.InstagramUserDetails> lstresult = session2.Query<Domain.Socioboard.Domain.InstagramUserDetails>().Where(t => t.Profile_Id == profileid && t.Created_Time >= present_date.Date.AddSeconds(1) && t.Created_Time <= present_date.Date.AddDays(1).AddSeconds(-1)).OrderByDescending(t => t.Created_Time).ToList();

                            List<Domain.Socioboard.Domain.InstagramUserDetails> lstresult = follower_90.Where(t => t.Created_Time >= present_date.Date.AddSeconds(1) && t.Created_Time <= present_date.AddDays(1).Date.AddSeconds(-1)).OrderByDescending(t => t.Created_Time).ToList();

                            if (lstresult.Count > 0)
                            {
                                Domain.Socioboard.Domain.InstagramUserDetails _result = lstresult.First();
                                Domain.Socioboard.Domain.InstagramUserDetails _result1 = lstresult.Last();

                                long add_data = long.Parse(_result.Follower);

                                long add_data1 = long.Parse(_result1.Follower);

                                long newfollowerdata = add_data - add_data1;

                                perday_90 = perday_90 + newfollowerdata.ToString() + ",";
                            }
                            else
                            {
                                perday_90 = perday_90 + "0,";
                            }
                            present_date = present_date.AddDays(-1);
                            logger.Error("perdayfollower >>" + perday_90);
                            //}
                        }
                        catch (Exception e)
                        {
                            logger.Error("perdayfollowererror >>" + e.Message);
                        }
                    }

                    try
                    {
                        string[] arr = perday_90.Split(',');
                        for (int i = 0; i < 15; i++)
                        {
                            perday_15 = perday_15 + arr[i] + ",";
                        }
                        for (int i = 0; i < 30; i++)
                        {
                            perday_30 = perday_30 + arr[i] + ",";
                        }
                        for (int i = 0; i < 60; i++)
                        {
                            perday_60 = perday_60 + arr[i] + ",";
                        }
                    }
                    catch (Exception)
                    {
                    }

                    if (follower_15.Count > 0)
                    {
                        _15_first = follower_15.First();
                        _15_last = follower_15.Last();
                        r._15 = long.Parse(_15_first.Follower) - long.Parse(_15_last.Follower);
                    }
                    else
                    {
                        r._15 = 0;
                    }
                    if (follower_30.Count > 0)
                    {
                        _30_first = follower_30.First();
                        _30_last = follower_30.Last();
                        r._30 = long.Parse(_30_first.Follower) - long.Parse(_30_last.Follower);
                    }
                    else
                    {
                        r._30 = 0;
                    }
                    if (follower_60.Count > 0)
                    {
                        _60_first = follower_60.First();
                        _60_last = follower_60.Last();
                        r._60 = long.Parse(_60_first.Follower) - long.Parse(_60_last.Follower);
                    }
                    else
                    {
                        r._60 = 0;
                    }
                    if (follower_90.Count > 0)
                    {
                        _90_first = follower_90.First();
                        _90_last = follower_90.Last();
                        r._90 = long.Parse(_90_first.Follower) - long.Parse(_90_last.Follower);
                    }
                    else
                    {
                        r._90 = 0;
                    }
                    r.perday_15 = perday_15;
                    r.perday_30 = perday_30;
                    r.perday_60 = perday_60;
                    r.perday_90 = perday_90;
                }
                catch (Exception e)
                {
                    logger.Error(e.Message);
                    logger.Error(e.StackTrace);
                }
            }
            string ret = new JavaScriptSerializer().Serialize(r);
            return ret;
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string InstagramFollowingGained(string profileid)
        {
            Domain.Socioboard.Domain.ReturnData r = new Domain.Socioboard.Domain.ReturnData();
            DateTime present_date = DateTime.Now;
            string perday_15 = string.Empty;
            string perday_30 = string.Empty;
            string perday_60 = string.Empty;
            string perday_90 = string.Empty;
            string ret_string = string.Empty;
            List<Domain.Socioboard.Domain.InstagramUserDetails> following_15 = new List<Domain.Socioboard.Domain.InstagramUserDetails>();
            List<Domain.Socioboard.Domain.InstagramUserDetails> following_30 = new List<Domain.Socioboard.Domain.InstagramUserDetails>();
            List<Domain.Socioboard.Domain.InstagramUserDetails> following_60 = new List<Domain.Socioboard.Domain.InstagramUserDetails>();
            List<Domain.Socioboard.Domain.InstagramUserDetails> following_90 = new List<Domain.Socioboard.Domain.InstagramUserDetails>();
            Domain.Socioboard.Domain.InstagramUserDetails _15_first = new Domain.Socioboard.Domain.InstagramUserDetails();
            Domain.Socioboard.Domain.InstagramUserDetails _15_last = new Domain.Socioboard.Domain.InstagramUserDetails();
            Domain.Socioboard.Domain.InstagramUserDetails _30_first = new Domain.Socioboard.Domain.InstagramUserDetails();
            Domain.Socioboard.Domain.InstagramUserDetails _30_last = new Domain.Socioboard.Domain.InstagramUserDetails();
            Domain.Socioboard.Domain.InstagramUserDetails _60_first = new Domain.Socioboard.Domain.InstagramUserDetails();
            Domain.Socioboard.Domain.InstagramUserDetails _60_last = new Domain.Socioboard.Domain.InstagramUserDetails();
            Domain.Socioboard.Domain.InstagramUserDetails _90_first = new Domain.Socioboard.Domain.InstagramUserDetails();
            Domain.Socioboard.Domain.InstagramUserDetails _90_last = new Domain.Socioboard.Domain.InstagramUserDetails();

            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                try
                {
                    string follow_str = "from InstagramUserDetails i where i.Profile_Id = : ProfileId";
                    try
                    {

                        following_90 = session.CreateQuery(follow_str).SetParameter("ProfileId", profileid)
                                                                        .List<Domain.Socioboard.Domain.InstagramUserDetails>().ToList().Where(x => x.Created_Time <= DateTime.Now && x.Created_Time >= (DateTime.Now.AddDays(-90))).OrderByDescending(y => y.Created_Time).ToList();

                        following_60 = following_90.Where(x => x.Created_Time < DateTime.Now.AddDays(1).Date && x.Created_Time > DateTime.Now.AddDays(-60).Date).OrderByDescending(y => y.Created_Time).ToList();

                        following_30 = following_90.Where(x => x.Created_Time < DateTime.Now.AddDays(1).Date && x.Created_Time > DateTime.Now.AddDays(-30).Date).OrderByDescending(y => y.Created_Time).ToList();

                        following_15 = following_90.Where(x => x.Created_Time < DateTime.Now.AddDays(1).Date && x.Created_Time > DateTime.Now.AddDays(-15).Date).OrderByDescending(y => y.Created_Time).ToList();

                    }
                    catch (Exception e)
                    {
                        logger.Error(e.Message);
                        logger.Error(e.StackTrace);
                    }
                    present_date = DateTime.Now;
                    while (present_date.Date != DateTime.Now.Date.AddDays(-90))
                    {
                        try
                        {
                            //using (NHibernate.ISession session2 = SessionFactory.GetNewSession())
                            //{

                            //List<Domain.Socioboard.Domain.InstagramUserDetails> lstresult = session2.Query<Domain.Socioboard.Domain.InstagramUserDetails>().Where(t => t.Profile_Id == profileid && t.Created_Time >= present_date.Date.AddSeconds(1) && t.Created_Time <= present_date.Date.AddDays(1).AddSeconds(-1)).OrderByDescending(t => t.Created_Time).ToList();

                            List<Domain.Socioboard.Domain.InstagramUserDetails> lstresult = following_90.Where(t => t.Created_Time >= present_date.Date.AddSeconds(1) && t.Created_Time <= present_date.Date.AddDays(1).AddSeconds(-1)).OrderByDescending(t => t.Created_Time).ToList();
                            if (lstresult.Count > 0)
                            {
                                Domain.Socioboard.Domain.InstagramUserDetails _result = lstresult.First();
                                Domain.Socioboard.Domain.InstagramUserDetails _result1 = lstresult.Last();

                                long add_data = long.Parse(_result.Following);

                                long add_data1 = long.Parse(_result1.Following);

                                long newfollowerdata = add_data - add_data1;

                                perday_90 = perday_90 + newfollowerdata.ToString() + ",";
                            }
                            else
                            {
                                perday_90 = perday_90 + "0,";
                            }
                            present_date = present_date.AddDays(-1);
                            //    logger.Error("perdayfollowing >>" + perday_90);
                            //}
                        }
                        catch (Exception e)
                        {
                            logger.Error(e.Message);
                        }
                    }
                    try
                    {
                        string[] arr = perday_90.Split(',');
                        for (int i = 0; i < 15; i++)
                        {
                            perday_15 = perday_15 + arr[i] + ",";
                        }
                        for (int i = 0; i < 30; i++)
                        {
                            perday_30 = perday_30 + arr[i] + ",";
                        }
                        for (int i = 0; i < 60; i++)
                        {
                            perday_60 = perday_60 + arr[i] + ",";
                        }
                    }
                    catch (Exception)
                    {

                    }
                    if (following_15.Count > 0)
                    {
                        _15_first = following_15.First();
                        _15_last = following_15.Last();
                        r._15 = long.Parse(_15_first.Following) - long.Parse(_15_last.Following);
                    }
                    else
                    {
                        r._15 = 0;
                    }

                    if (following_30.Count > 0)
                    {
                        _30_first = following_30.First();
                        _30_last = following_30.Last();
                        r._30 = long.Parse(_30_first.Following) - long.Parse(_30_last.Following);
                    }
                    else
                    {
                        r._30 = 0;
                    }
                    if (following_60.Count > 0)
                    {
                        _60_first = following_60.First();
                        _60_last = following_60.Last();
                        r._60 = long.Parse(_60_first.Following) - long.Parse(_60_last.Following);
                    }
                    else
                    {
                        r._60 = 0;
                    }
                    if (following_90.Count > 0)
                    {
                        _90_first = following_90.First();
                        _90_last = following_90.Last();
                        r._90 = long.Parse(_90_first.Following) - long.Parse(_90_last.Following);
                    }
                    else
                    {
                        r._90 = 0;
                    }
                    r.perday_15 = perday_15;
                    r.perday_30 = perday_30;
                    r.perday_60 = perday_60;
                    r.perday_90 = perday_90;
                }
                catch (Exception e)
                {
                    logger.Error(e.Message);
                    logger.Error(e.StackTrace);
                }
            }
            string ret = new JavaScriptSerializer().Serialize(r);
            return ret;
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string InstagramPostCommentGained(string profileid)
        {
            Domain.Socioboard.Domain.ReturnData r = new Domain.Socioboard.Domain.ReturnData();
            DateTime present_date = DateTime.Now;
            string perday_15 = string.Empty;
            string perday_30 = string.Empty;
            string perday_60 = string.Empty;
            string perday_90 = string.Empty;
            string ret_string = string.Empty;
            List<Domain.Socioboard.Domain.InstagramPostComments> postcomments_15 = new List<Domain.Socioboard.Domain.InstagramPostComments>();
            List<Domain.Socioboard.Domain.InstagramPostComments> postcomments_30 = new List<Domain.Socioboard.Domain.InstagramPostComments>();
            List<Domain.Socioboard.Domain.InstagramPostComments> postcomments_60 = new List<Domain.Socioboard.Domain.InstagramPostComments>();
            List<Domain.Socioboard.Domain.InstagramPostComments> postcomments_90 = new List<Domain.Socioboard.Domain.InstagramPostComments>();

            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                try
                {
                    string follow_str = "from InstagramPostComments i where i.Profile_Id = : ProfileId";
                    try
                    {
                        postcomments_90 = session.CreateQuery(follow_str).SetParameter("ProfileId", profileid)
                                                                        .List<Domain.Socioboard.Domain.InstagramPostComments>().ToList().Where(x => x.Created_Time < DateTime.Now.AddDays(1).Date && x.Created_Time > (DateTime.Now.AddDays(-90).Date)).OrderByDescending(y => y.Created_Time).ToList();

                        postcomments_60 = postcomments_90.Where(x => x.Created_Time < DateTime.Now.AddDays(1).Date && x.Created_Time > (DateTime.Now.AddDays(-60).Date)).OrderByDescending(y => y.Created_Time).ToList();

                        postcomments_30 = postcomments_90.Where(x => x.Created_Time < DateTime.Now.AddDays(1).Date && x.Created_Time > (DateTime.Now.AddDays(-30).Date)).OrderByDescending(y => y.Created_Time).ToList();

                        postcomments_15 = postcomments_90.Where(x => x.Created_Time < DateTime.Now.AddDays(1).Date && x.Created_Time > (DateTime.Now.AddDays(-15).Date)).OrderByDescending(y => y.Created_Time).ToList();

                    }
                    catch (Exception e)
                    {
                        logger.Error(e.Message);
                        logger.Error(e.StackTrace);
                    }

                    present_date = DateTime.Now;

                    while (present_date.Date != DateTime.Now.Date.AddDays(-90))
                    {
                        try
                        {
                            //using (NHibernate.ISession session2 = SessionFactory.GetNewSession())
                            //{
                            //List<Domain.Socioboard.Domain.InstagramPostComments> lstresult = session2.Query<Domain.Socioboard.Domain.InstagramPostComments>().Where(t => t.Profile_Id == profileid && t.Created_Time >= present_date.Date.AddSeconds(1) && t.Created_Time <= present_date.Date.AddDays(1).AddSeconds(-1)).OrderByDescending(t => t.Created_Time).ToList();
                            List<Domain.Socioboard.Domain.InstagramPostComments> lstresult = postcomments_90.Where(t => t.Created_Time >= present_date.Date.AddSeconds(-1) && t.Created_Time <= present_date.Date.AddDays(1).AddSeconds(-1)).OrderByDescending(t => t.Created_Time).ToList();
                            if (lstresult.Count > 0)
                            {
                                int item = lstresult.Count;

                                perday_90 = perday_90 + item.ToString() + ",";
                            }
                            else
                            {
                                perday_90 = perday_90 + "0,";
                            }
                            present_date = present_date.AddDays(-1);
                            //logger.Error("perdaycomment >>" + perday_90);
                            //}
                        }
                        catch (Exception e)
                        {
                            logger.Error("perdaycommenterror >>" + e.Message);
                        }
                    }

                    try
                    {
                        string[] arr = perday_90.Split(',');
                        for (int i = 0; i < 15; i++)
                        {
                            perday_15 = perday_15 + arr[i] + ",";
                        }
                        for (int i = 0; i < 30; i++)
                        {
                            perday_30 = perday_30 + arr[i] + ",";
                        }
                        for (int i = 0; i < 60; i++)
                        {
                            perday_60 = perday_60 + arr[i] + ",";
                        }
                    }
                    catch { }
                    r._15 = postcomments_15.Count;
                    r._30 = postcomments_30.Count;
                    r._60 = postcomments_60.Count;
                    r._90 = postcomments_90.Count;

                    r.perday_15 = perday_15;
                    r.perday_30 = perday_30;
                    r.perday_60 = perday_60;
                    r.perday_90 = perday_90;

                }
                catch (Exception e)
                {
                    logger.Error(e.Message);
                    logger.Error(e.StackTrace);
                }
            }
            string ret = new JavaScriptSerializer().Serialize(r);
            return ret;
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string InstagramPostLikesGained(string profileid)
        {
            Domain.Socioboard.Domain.ReturnData r = new Domain.Socioboard.Domain.ReturnData();
            DateTime present_date = DateTime.Now;
            string perday_15 = string.Empty;
            string perday_30 = string.Empty;
            string perday_60 = string.Empty;
            string perday_90 = string.Empty;
            string ret_string = string.Empty;
            List<Domain.Socioboard.Domain.InstagramPostLikes> postlikes_15 = new List<Domain.Socioboard.Domain.InstagramPostLikes>();
            List<Domain.Socioboard.Domain.InstagramPostLikes> postlikes_30 = new List<Domain.Socioboard.Domain.InstagramPostLikes>();
            List<Domain.Socioboard.Domain.InstagramPostLikes> postlikes_60 = new List<Domain.Socioboard.Domain.InstagramPostLikes>();
            List<Domain.Socioboard.Domain.InstagramPostLikes> postlikes_90 = new List<Domain.Socioboard.Domain.InstagramPostLikes>();

            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                try
                {
                    string follow_str = "from InstagramPostLikes i where i.Profile_Id = : ProfileId";
                    try
                    {
                        postlikes_90 = session.CreateQuery(follow_str).SetParameter("ProfileId", profileid)
                                                                        .List<Domain.Socioboard.Domain.InstagramPostLikes>().ToList().Where(x => x.Created_Date < DateTime.Now.AddDays(1).Date && x.Created_Date > (DateTime.Now.AddDays(-90).Date)).OrderByDescending(y => y.Created_Date).ToList();

                        postlikes_60 = postlikes_90.Where(x => x.Created_Date <= DateTime.Now && x.Created_Date >= (DateTime.Now.AddDays(-60))).OrderByDescending(y => y.Created_Date).ToList();

                        postlikes_30 = postlikes_90.Where(x => x.Created_Date <= DateTime.Now && x.Created_Date >= (DateTime.Now.AddDays(-30))).OrderByDescending(y => y.Created_Date).ToList();

                        postlikes_15 = postlikes_90.Where(x => x.Created_Date <= DateTime.Now && x.Created_Date >= (DateTime.Now.AddDays(-15))).OrderByDescending(y => y.Created_Date).ToList();
                    }
                    catch (Exception e)
                    {
                        logger.Error(e.Message);
                        logger.Error(e.StackTrace);
                    }
                    present_date = DateTime.Now;
                    while (present_date.Date != DateTime.Now.Date.AddDays(-90))
                    {
                        try
                        {
                            using (NHibernate.ISession session2 = SessionFactory.GetNewSession())
                            {
                                // List<Domain.Socioboard.Domain.InstagramPostLikes> lstresult = session2.Query<Domain.Socioboard.Domain.InstagramPostLikes>().Where(t => t.Profile_Id == profileid && t.Created_Date >= present_date.Date.AddSeconds(1) && t.Created_Date <= present_date.Date.AddDays(1).AddSeconds(-1)).OrderByDescending(t => t.Created_Date).ToList();
                                List<Domain.Socioboard.Domain.InstagramPostLikes> lstresult = postlikes_90.Where(t => t.Created_Date >= present_date.Date.AddSeconds(1) && t.Created_Date <= present_date.Date.AddDays(1).AddSeconds(-1)).OrderByDescending(t => t.Created_Date).ToList();
                                if (lstresult.Count > 0)
                                {
                                    int item = lstresult.Count;
                                    perday_90 = perday_90 + item.ToString() + ",";
                                }
                                else
                                {
                                    perday_90 = perday_90 + "0,";
                                }
                                present_date = present_date.AddDays(-1);
                                //logger.Error("perdaylikes >>" + perday_90);
                            }
                        }
                        catch (Exception e)
                        {
                            logger.Error("perdaylikeserror >>" + e.Message);
                        }
                    }
                    try
                    {
                        string[] arr = perday_90.Split(',');
                        for (int i = 0; i < 15; i++)
                        {
                            perday_15 = perday_15 + arr[i] + ",";
                        }
                        for (int i = 0; i < 30; i++)
                        {
                            perday_30 = perday_30 + arr[i] + ",";
                        }
                        for (int i = 0; i < 60; i++)
                        {
                            perday_60 = perday_60 + arr[i] + ",";
                        }
                    }
                    catch { }

                    r._15 = postlikes_15.Count;
                    r._30 = postlikes_30.Count;
                    r._60 = postlikes_60.Count;
                    r._90 = postlikes_90.Count;

                    r.perday_15 = perday_15;
                    r.perday_30 = perday_30;
                    r.perday_60 = perday_60;
                    r.perday_90 = perday_90;

                }
                catch (Exception e)
                {
                    logger.Error(e.Message);
                    logger.Error(e.StackTrace);
                }
            }
            string ret = new JavaScriptSerializer().Serialize(r);
            return ret;
        }

        [WebMethod]
        public void insertdata(string i)
        {
            try
            {
                Domain.Socioboard.Domain.InstagramReport insert = (Domain.Socioboard.Domain.InstagramReport)new JavaScriptSerializer().Deserialize(i, typeof(Domain.Socioboard.Domain.InstagramReport));
                bool exist = false;
                using (NHibernate.ISession session = SessionFactory.GetNewSession())
                {
                    exist = session.Query<Domain.Socioboard.Domain.InstagramReport>()
                                  .Any(x => x.Profile_Id == insert.Profile_Id);
                }
                using (NHibernate.ISession session1 = SessionFactory.GetNewSession())
                {
                    //After Session creation, start and open Transaction. 
                    using (NHibernate.ITransaction transaction = session1.BeginTransaction())
                    {
                        if (exist)
                        {
                            int i1 = session1.CreateQuery("update InstagramReport set Media_Count = : Media_Count , Follower = : Follower , Following = : Following , follow_15 = : follow_15 , perday_follow_15 = : perday_follow_15 ,  follow_30 = : follow_30 , perday_follow_30 = : perday_follow_30 , follow_60 = : follow_60 , perday_follow_60 = : perday_follow_60 , follow_90 = : follow_90 , perday_follow_90 = : perday_follow_90 , following_15 =: following_15 , perday_following_15 = : perday_following_15 , following_30 =: following_30 , perday_following_30 = : perday_following_30 , following_60 =: following_60 , perday_following_60 = : perday_following_60 , following_90 =: following_90 , perday_following_90 = : perday_following_90 , postcomment_15 = : postcomment_15 , perday_postcomment_15 = : perday_postcomment_15 , postcomment_30 = : postcomment_30 , perday_postcomment_30 = : perday_postcomment_30 , postcomment_60 = : postcomment_60 , perday_postcomment_60 = : perday_postcomment_60 , postcomment_90 = : postcomment_90 , perday_postcomment_90 = : perday_postcomment_90 , postlike_15 = : postlike_15 , perday_postlike_15 = : perday_postlike_15 , postlike_30 = : postlike_30 , perday_postlike_30 = : perday_postlike_30 , postlike_60 = : postlike_60 , perday_postlike_60 = : perday_postlike_60 , postlike_90 = : postlike_90 , perday_postlike_90 = : perday_postlike_90 , videopost_15 = : videopost_15 , perday_videopost_15 = : perday_videopost_15 , videopost_30 = : videopost_30 , perday_videopost_30 = : perday_videopost_30 , videopost_60 = : videopost_60 , perday_videopost_60 = : perday_videopost_60 , videopost_90 = : videopost_90 , perday_videopost_90 = : perday_videopost_90 , imagepost_15 = : imagepost_15 , perday_imagepost_15 = : perday_imagepost_15 , imagepost_30 = : imagepost_30 , perday_imagepost_30 = : perday_imagepost_30 , imagepost_60 = : imagepost_60 , perday_imagepost_60 = : perday_imagepost_60 , imagepost_90 = : imagepost_90 , perday_imagepost_90 = : perday_imagepost_90 where Profile_Id = : Profile_Id")
                                    .SetParameter("Media_Count", insert.Media_Count)
                                    .SetParameter("Follower", insert.Follower)
                                    .SetParameter("Following", insert.Following)
                                    .SetParameter("follow_15", insert.follow_15)
                                    .SetParameter("perday_follow_15", insert.perday_follow_15)

                                     .SetParameter("follow_30", insert.follow_30)
                                     .SetParameter("perday_follow_30", insert.perday_follow_30)

                                     .SetParameter("follow_60", insert.follow_60)
                                     .SetParameter("perday_follow_60", insert.perday_follow_60)
                                     .SetParameter("follow_90", insert.follow_90)
                                     .SetParameter("perday_follow_90", insert.perday_follow_90)

                                     .SetParameter("following_15", insert.following_15)
                                     .SetParameter("perday_following_15", insert.perday_following_15)

                                     .SetParameter("following_30", insert.following_30)
                                     .SetParameter("perday_following_30", insert.perday_following_30)

                                     .SetParameter("following_60", insert.following_60)
                                     .SetParameter("perday_following_60", insert.perday_following_60)

                                     .SetParameter("following_90", insert.following_90)
                                     .SetParameter("perday_following_90", insert.perday_following_90)

                                     .SetParameter("postcomment_15", insert.postcomment_15)
                                     .SetParameter("perday_postcomment_15", insert.perday_postcomment_15)

                                     .SetParameter("postcomment_30", insert.postcomment_30)
                                     .SetParameter("perday_postcomment_30", insert.perday_postcomment_30)

                                     .SetParameter("postcomment_60", insert.postcomment_60)
                                     .SetParameter("perday_postcomment_60", insert.perday_postcomment_60)

                                     .SetParameter("postcomment_90", insert.postcomment_90)
                                     .SetParameter("perday_postcomment_90", insert.perday_postcomment_90)

                                     .SetParameter("postlike_15", insert.postlike_15)
                                     .SetParameter("perday_postlike_15", insert.perday_postlike_15)

                                     .SetParameter("postlike_30", insert.postlike_30)
                                     .SetParameter("perday_postlike_30", insert.perday_postlike_30)

                                     .SetParameter("postlike_60", insert.postlike_60)
                                     .SetParameter("perday_postlike_60", insert.perday_postlike_60)

                                     .SetParameter("postlike_90", insert.postlike_90)
                                     .SetParameter("perday_postlike_90", insert.perday_postlike_90)
                                     .SetParameter("videopost_15", insert.videopost_15)
                                     .SetParameter("perday_videopost_15", insert.perday_videopost_15)

                                     .SetParameter("videopost_30", insert.videopost_30)
                                     .SetParameter("perday_videopost_30", insert.perday_videopost_30)

                                     .SetParameter("videopost_60", insert.videopost_60)
                                     .SetParameter("perday_videopost_60", insert.perday_videopost_60)

                                     .SetParameter("videopost_90", insert.videopost_90)
                                     .SetParameter("perday_videopost_90", insert.perday_videopost_90)

                                     .SetParameter("imagepost_15", insert.imagepost_15)
                                     .SetParameter("perday_imagepost_15", insert.perday_imagepost_15)

                                         .SetParameter("imagepost_30", insert.imagepost_30)
                                     .SetParameter("perday_imagepost_30", insert.perday_imagepost_30)

                                         .SetParameter("imagepost_60", insert.imagepost_60)
                                     .SetParameter("perday_imagepost_60", insert.perday_imagepost_60)

                                         .SetParameter("imagepost_90", insert.imagepost_90)
                                     .SetParameter("perday_imagepost_90", insert.perday_imagepost_90)

                                     .SetParameter("Profile_Id", insert.Profile_Id)
                                     .ExecuteUpdate();
                            transaction.Commit(); 
                        }
                        else
                        {
                            session1.Save(insert);
                            transaction.Commit();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
            }
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string retrievedata(string profileid)
        {
            string ret_string = string.Empty;
            Domain.Socioboard.Domain.InstagramReport ret = new Domain.Socioboard.Domain.InstagramReport();
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                try
                {
                    NHibernate.IQuery retrieve = session.CreateQuery("from InstagramReport where Profile_Id =: Profile_Id").SetParameter("Profile_Id", profileid);
                    ret = (Domain.Socioboard.Domain.InstagramReport)retrieve.UniqueResult();
                     
                }
                catch {
                    ret = new Domain.Socioboard.Domain.InstagramReport();
                }
            }
            return new JavaScriptSerializer().Serialize(ret);;
        }
        [WebMethod]
        public string GetInstagramData(string ProfileIds)
        {
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                string[] arrstr = ProfileIds.Split(',');
                List<Domain.Socioboard.Domain.InstagramReport> lstInstagramReports = session.Query<Domain.Socioboard.Domain.InstagramReport>().Where(U => arrstr.Contains(U.Profile_Id)).ToList();
                return new JavaScriptSerializer().Serialize(lstInstagramReports);
            }
        }

    }
}
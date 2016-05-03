using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using Api.Socioboard.Helper;
//using Domain.Socioboard.Reports;
using System.Web.Script.Serialization;
using System.Web.Script.Services;
using NHibernate.Linq;
using NHibernate.Criterion;
using log4net;

namespace Api.Socioboard.Services
{
    /// <summary>
    /// Summary description for TwitterReport
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class TwitterReport : System.Web.Services.WebService
    {
        ILog logger = LogManager.GetLogger(typeof(TwitterReport));
      
        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]

        public string getprofileids()
        {

            // List<Domain.Socioboard.Domain.TwitterAccount> profileids = new List<Domain.Socioboard.Domain.TwitterAccount>();
            List<string> ids = new List<string>();
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                try
                {
                    ids = session.Query<Domain.Socioboard.Domain.TwitterAccount>().Select(x => x.TwitterUserId).Distinct().ToList();
                    //profileids = (List<Domain.Socioboard.Domain.TwitterAccount>)ids.List<Domain.Socioboard.Domain.TwitterAccount>();


                }
                catch (Exception e)
                {
                    logger.Error(e.Message);
                    logger.Error(e.StackTrace);

                }
            }


            return new JavaScriptSerializer().Serialize(ids);
        }
        [WebMethod]
        public string gettotalfollowers(string profileid)
        {

            string totalfollowers = string.Empty;
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {

                try
                {
                    totalfollowers = session.Query<Domain.Socioboard.Domain.TwitterAccount>().Where(a => a.TwitterUserId == profileid).Select(x => x.FollowersCount).ToList().First().ToString();


                }
                catch (Exception e)
                {

                    logger.Error(e.Message);
                    logger.Error(e.StackTrace);
                }
            }

            return totalfollowers;
        }
        [WebMethod]
        public string gettotalfollowing(string profileid)
        {

            string totalfollowing = string.Empty;
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {

                try
                {
                    totalfollowing = session.Query<Domain.Socioboard.Domain.TwitterAccount>().Where(a => a.TwitterUserId == profileid).Select(x => x.FollowingCount).ToList().First().ToString();


                }
                catch (Exception e)
                {

                    logger.Error(e.Message);
                    logger.Error(e.StackTrace);
                }
            }

            return totalfollowing;
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]

        public string newfollower(string profileid)
        {



            Domain.Socioboard.Domain.ReturnData r = new Domain.Socioboard.Domain.ReturnData();

            DateTime present_date = DateTime.Now;
            string perday_15 = string.Empty;
            string perday_30 = string.Empty;
            string perday_60 = string.Empty;
            string perday_90 = string.Empty;
            string AllProfileId = string.Empty;
            string FbProfileId = string.Empty;
            string TwtProfileId = string.Empty;
            string[] ArrProfileId = { };

            List<Domain.Socioboard.Domain.InboxMessages> InboxMessages_15 = new List<Domain.Socioboard.Domain.InboxMessages>();
            List<Domain.Socioboard.Domain.InboxMessages> InboxMessages_30 = new List<Domain.Socioboard.Domain.InboxMessages>();
            List<Domain.Socioboard.Domain.InboxMessages> InboxMessages_60 = new List<Domain.Socioboard.Domain.InboxMessages>();
            List<Domain.Socioboard.Domain.InboxMessages> InboxMessages_90 = new List<Domain.Socioboard.Domain.InboxMessages>();

            //string strtwitterfollowers = "from InboxMessages t where t.ProfileId =: ProfileId and t.MessageType =: MessageType";

            //string strtwitterfollowers = "from TwitterAccountFollowers t where t.ProfileId =: ProfileId";


            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                try
                {
                    InboxMessages_90 = session.Query<Domain.Socioboard.Domain.InboxMessages>().Where(U => U.Status == 0 && U.CreatedTime < DateTime.Now.AddDays(1).Date && U.CreatedTime >= (DateTime.Now.AddDays(-90).Date.AddSeconds(1)) && U.ProfileId == profileid && U.MessageType == "twt_followers").GroupBy(x => x.FromId).Select(g => g.First()).ToList<Domain.Socioboard.Domain.InboxMessages>();

                    InboxMessages_60 = InboxMessages_90.Where(x => x.CreatedTime < DateTime.Now.AddDays(1).Date && x.CreatedTime >= (DateTime.Now.AddDays(-60).Date.AddSeconds(1))).ToList();

                    InboxMessages_30 = InboxMessages_90.Where(x => x.CreatedTime < DateTime.Now.AddDays(1).Date && x.CreatedTime >= (DateTime.Now.AddDays(-30).Date.AddSeconds(1))).ToList();

                    InboxMessages_15 = InboxMessages_90.Where(x => x.CreatedTime < DateTime.Now.AddDays(1).Date && x.CreatedTime >= (DateTime.Now.AddDays(-15).Date.AddSeconds(1))).ToList();


                }

                catch (Exception e)
                {

                    logger.Error(e.Message);
                    logger.Error(e.StackTrace);

                }
            }

            present_date = DateTime.Now;

            while (present_date.Date != DateTime.Now.Date.AddDays(-90))
            {
                try
                {
                    using (NHibernate.ISession session4 = SessionFactory.GetNewSession())
                    {
                        List<Domain.Socioboard.Domain.InboxMessages> lstresult = InboxMessages_90.Where(t => t.CreatedTime >= present_date.Date.AddSeconds(1) && t.CreatedTime <= present_date.Date.AddDays(1).AddSeconds(-1)).ToList();

                        if (lstresult.Count > 0)
                        {
                            perday_90 = perday_90 + lstresult.Count.ToString() + ",";
                        }
                        else
                        {
                            perday_90 = perday_90 + "0,";
                        }
                        logger.Error("perdaytwtn>>" + perday_90);
                        present_date = present_date.AddDays(-1);
                    }
                }
                catch (Exception e)
                {
                    logger.Error("perdaytwtnerror >>" + e);
                    logger.Error(e.Message);
                    logger.Error(e.StackTrace);
                    Console.Write(e.StackTrace);
                }
            }




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




            if (InboxMessages_15.Count > 0)
            {

                r._15 = InboxMessages_15.Count;
            }
            else
            {
                r._15 = 0;
            }

            if (InboxMessages_30.Count > 0)
            {

                r._30 = InboxMessages_30.Count;
            }
            else
            {
                r._30 = 0;
            }

            if (InboxMessages_60.Count > 0)
            {

                r._60 = InboxMessages_60.Count;
            }
            else
            {
                r._60 = 0;
            }

            if (InboxMessages_90.Count > 0)
            {

                r._90 = InboxMessages_90.Count;
            }
            else
            {
                r._90 = 0;
            }


            r.perday_15 = perday_15;
            r.perday_30 = perday_30;
            r.perday_60 = perday_60;
            r.perday_90 = perday_90;

            string ret = new JavaScriptSerializer().Serialize(r);
            return ret;

        }


        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]

        public string youfollowed(string profileid)
        {

            Domain.Socioboard.Domain.ReturnData r = new Domain.Socioboard.Domain.ReturnData();
            Domain.Socioboard.Domain.TwitterAccountFollowers _15_first = new Domain.Socioboard.Domain.TwitterAccountFollowers();
            Domain.Socioboard.Domain.TwitterAccountFollowers _30_first = new Domain.Socioboard.Domain.TwitterAccountFollowers();
            Domain.Socioboard.Domain.TwitterAccountFollowers _60_first = new Domain.Socioboard.Domain.TwitterAccountFollowers();
            Domain.Socioboard.Domain.TwitterAccountFollowers _90_first = new Domain.Socioboard.Domain.TwitterAccountFollowers();

            Domain.Socioboard.Domain.TwitterAccountFollowers _15_last = new Domain.Socioboard.Domain.TwitterAccountFollowers();
            Domain.Socioboard.Domain.TwitterAccountFollowers _30_last = new Domain.Socioboard.Domain.TwitterAccountFollowers();
            Domain.Socioboard.Domain.TwitterAccountFollowers _60_last = new Domain.Socioboard.Domain.TwitterAccountFollowers();
            Domain.Socioboard.Domain.TwitterAccountFollowers _90_last = new Domain.Socioboard.Domain.TwitterAccountFollowers();


            DateTime present_date = DateTime.Now;
            string perday_15 = string.Empty;
            string perday_30 = string.Empty;
            string perday_60 = string.Empty;
            string perday_90 = string.Empty;
            string AllProfileId = string.Empty;
            string FbProfileId = string.Empty;
            string TwtProfileId = string.Empty;
            string[] ArrProfileId = { };
            List<Domain.Socioboard.Domain.TwitterAccountFollowers> twitterfollowers_15 = new List<Domain.Socioboard.Domain.TwitterAccountFollowers>();
            List<Domain.Socioboard.Domain.TwitterAccountFollowers> twitterfollowers_30 = new List<Domain.Socioboard.Domain.TwitterAccountFollowers>();
            List<Domain.Socioboard.Domain.TwitterAccountFollowers> twitterfollowers_60 = new List<Domain.Socioboard.Domain.TwitterAccountFollowers>();
            List<Domain.Socioboard.Domain.TwitterAccountFollowers> twitterfollowers_90 = new List<Domain.Socioboard.Domain.TwitterAccountFollowers>();
            string strtwitterfollowers = "from TwitterAccountFollowers t where t.ProfileId =: ProfileId";


            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                try
                {
                    twitterfollowers_90 = session.CreateQuery(strtwitterfollowers)
                                                          .SetParameter("ProfileId", profileid)
                                                          .List<Domain.Socioboard.Domain.TwitterAccountFollowers>().ToList().Where(x => x.EntryDate <= DateTime.Now.AddDays(1).Date && x.EntryDate >= (DateTime.Now.AddDays(-90).Date)).OrderByDescending(y => y.EntryDate).ToList();

                    twitterfollowers_60 = twitterfollowers_90.Where(x => x.EntryDate <= DateTime.Now.AddDays(1).Date && x.EntryDate >= (DateTime.Now.AddDays(-60).Date)).OrderByDescending(y => y.EntryDate).ToList();

                    twitterfollowers_30 = twitterfollowers_90.Where(x => x.EntryDate <= DateTime.Now.AddDays(1).Date && x.EntryDate >= (DateTime.Now.AddDays(-30).Date)).OrderByDescending(y => y.EntryDate).ToList();

                    twitterfollowers_15 = twitterfollowers_90.Where(x => x.EntryDate <= DateTime.Now.AddDays(1).Date && x.EntryDate >= (DateTime.Now.AddDays(-15).Date)).OrderByDescending(y => y.EntryDate).ToList();

                }

                catch (Exception e)
                {

                    logger.Error(e.Message);
                    logger.Error(e.StackTrace);

                }
            }

            present_date = DateTime.Now;

            while (present_date.Date != DateTime.Now.Date.AddDays(-90))
            {

                try
                {


                    using (NHibernate.ISession session4 = SessionFactory.GetNewSession())
                    {
                        //List<Domain.Socioboard.Domain.TwitterAccountFollowers> lstresult = session4.Query<Domain.Socioboard.Domain.TwitterAccountFollowers>().Where(t => t.ProfileId == profileid && t.EntryDate >= present_date.Date.AddSeconds(1) && t.EntryDate <= present_date.Date.AddDays(1).AddSeconds(-1)).OrderByDescending(t => t.EntryDate).ToList();

                        List<Domain.Socioboard.Domain.TwitterAccountFollowers> lstresult = twitterfollowers_90.Where(t => t.EntryDate >= present_date.Date.AddSeconds(1) && t.EntryDate <= present_date.Date.AddDays(1).AddSeconds(-1)).OrderByDescending(t => t.EntryDate).ToList();

                        if (lstresult.Count > 0)
                        {
                            Domain.Socioboard.Domain.TwitterAccountFollowers _result = lstresult.First();

                            Domain.Socioboard.Domain.TwitterAccountFollowers _result1 = lstresult.Last();


                            long add_data = _result.FollowingsCount;
                            long add_data1 = _result1.FollowingsCount;

                            long newfollowingdata = add_data - add_data1;
                            perday_90 = perday_90 + newfollowingdata.ToString() + ",";
                        }
                        else
                        {

                            perday_90 = perday_90 + "0,";
                        }
                        logger.Error("perdaytwtf>>" + perday_90);
                        present_date = present_date.AddDays(-1);


                    }
                }
                catch (Exception e)
                {
                    logger.Error("perdaytwtferror >>" + e);
                    Console.Write(e.StackTrace);
                }
            }




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




            if (twitterfollowers_15.Count > 0)
            {
                _15_first = twitterfollowers_15.First();
                _15_last = twitterfollowers_15.Last();
                r._15 = _15_first.FollowingsCount - _15_last.FollowingsCount;
            }
            else
            {
                r._15 = 0;
            }

            if (twitterfollowers_30.Count > 0)
            {
                _30_first = twitterfollowers_30.First();
                _30_last = twitterfollowers_30.Last();
                r._30 = _30_first.FollowingsCount - _30_last.FollowingsCount;
            }
            else
            {
                r._30 = 0;
            }

            if (twitterfollowers_60.Count > 0)
            {
                _60_first = twitterfollowers_60.First();
                _60_last = twitterfollowers_60.Last();
                r._60 = _60_first.FollowingsCount - _60_last.FollowingsCount;
            }
            else
            {
                r._60 = 0;
            }

            if (twitterfollowers_90.Count > 0)
            {
                _90_first = twitterfollowers_90.First();
                _90_last = twitterfollowers_90.Last();
                r._90 = _90_first.FollowingsCount - _90_last.FollowingsCount;
            }
            else
            {
                r._90 = 0;
            }


            r.perday_15 = perday_15;
            r.perday_30 = perday_30;
            r.perday_60 = perday_60;
            r.perday_90 = perday_90;

            string ret = new JavaScriptSerializer().Serialize(r);
            return ret;

        }


        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]

        public string directmessage(string profileid)
        {

            Domain.Socioboard.Domain.ReturnData r = new Domain.Socioboard.Domain.ReturnData();
           
            DateTime present_date = DateTime.Now;
            string perday_15 = string.Empty;
            string perday_30 = string.Empty;
            string perday_60 = string.Empty;
            string perday_90 = string.Empty;
            string AllProfileId = string.Empty;
            string FbProfileId = string.Empty;
            string TwtProfileId = string.Empty;
            string[] ArrProfileId = { };
            List<Domain.Socioboard.Domain.TwitterDirectMessages> twitterdirectmessages_15 = new List<Domain.Socioboard.Domain.TwitterDirectMessages>();
            List<Domain.Socioboard.Domain.TwitterDirectMessages> twitterdirectmessages_30 = new List<Domain.Socioboard.Domain.TwitterDirectMessages>();
            List<Domain.Socioboard.Domain.TwitterDirectMessages> twitterdirectmessages_60 = new List<Domain.Socioboard.Domain.TwitterDirectMessages>();
            List<Domain.Socioboard.Domain.TwitterDirectMessages> twitterdirectmessages_90 = new List<Domain.Socioboard.Domain.TwitterDirectMessages>();
            string strtwitterfollowers = "from TwitterDirectMessages t where t.RecipientId =: ProfileId";


            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                try
                {
                    twitterdirectmessages_90 = session.CreateQuery(strtwitterfollowers)
                                                           .SetParameter("ProfileId", profileid)
                                                           .List<Domain.Socioboard.Domain.TwitterDirectMessages>().ToList().Where(x => x.CreatedDate <= DateTime.Now.AddDays(1).Date.AddSeconds(-1) && x.CreatedDate >= (DateTime.Now.AddDays(-90).Date.AddSeconds(1))).GroupBy(x => x.MessageId).Select(g => g.First()).ToList();

                    twitterdirectmessages_60 = twitterdirectmessages_90.Where(x => x.CreatedDate <= DateTime.Now.AddDays(1).Date.AddSeconds(-1) && x.CreatedDate >= (DateTime.Now.AddDays(-60).Date.AddSeconds(1))).ToList();

                    twitterdirectmessages_30 = twitterdirectmessages_90.Where(x => x.CreatedDate <= DateTime.Now.AddDays(1).Date.AddSeconds(-1) && x.CreatedDate >= (DateTime.Now.AddDays(-30).Date.AddSeconds(1))).ToList();

                    twitterdirectmessages_15 = twitterdirectmessages_90.Where(x => x.CreatedDate <= DateTime.Now.AddDays(1).Date.AddSeconds(-1) && x.CreatedDate >= (DateTime.Now.AddDays(-15).Date.AddSeconds(1))).ToList();



                }

                catch (Exception e)
                {

                    logger.Error(e.Message);
                    logger.Error(e.StackTrace);

                }
            }

            present_date = DateTime.Now;

            while (present_date.Date != DateTime.Now.Date.AddDays(-90))
            {
                try
                {
                    using (NHibernate.ISession session1 = SessionFactory.GetNewSession())
                    {
                        List<Domain.Socioboard.Domain.TwitterDirectMessages> _twitterdirectmessages = new List<Domain.Socioboard.Domain.TwitterDirectMessages>();

                        int item1;
                        try
                        {


                            _twitterdirectmessages = twitterdirectmessages_90.Where(m => m.CreatedDate >= present_date.Date.AddSeconds(1) && m.CreatedDate <= present_date.AddDays(1).Date.AddSeconds(-1)).ToList();

                            //item1 = session1.QueryOver<Domain.Socioboard.Domain.TwitterDirectMessages>().Where(m => m.CreatedDate >= present_date.Date.AddSeconds(1) && m.CreatedDate <= present_date.AddDays(1).Date.AddSeconds(-1)).Where( S => S.RecipientId == profileid)
                            //                             .Select(Projections.CountDistinct<Domain.Socioboard.Domain.TwitterDirectMessages>(x => x.MessageId))
                            //                             .FutureValue<int>().Value;

                        }
                        catch (Exception e)
                        {
                            item1 = 0;
                        }

                        int add_data = _twitterdirectmessages.Count;

                        perday_90 = perday_90 + add_data.ToString() + ",";

                        present_date = present_date.AddDays(-1);
                        logger.Error("perdayinbox >>" + perday_90);
                    }
                }
                catch (Exception e)
                {
                    logger.Error("perdaydmerror >>" + e);
                    logger.Error(e.Message);
                    logger.Error(e.StackTrace);
                    Console.Write(e.StackTrace);
                }
            }




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


            r._15 = twitterdirectmessages_15.Count;
            r._30 = twitterdirectmessages_30.Count;
            r._60 = twitterdirectmessages_60.Count;
            r._90 = twitterdirectmessages_90.Count;



            r.perday_15 = perday_15;
            r.perday_30 = perday_30;
            r.perday_60 = perday_60;
            r.perday_90 = perday_90;

            string ret = new JavaScriptSerializer().Serialize(r);
            return ret;

        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]

        public string directmessagesent(string profileid)
        {

            Domain.Socioboard.Domain.ReturnData r = new Domain.Socioboard.Domain.ReturnData();
            
            DateTime present_date = DateTime.Now;
            string perday_15 = string.Empty;
            string perday_30 = string.Empty;
            string perday_60 = string.Empty;
            string perday_90 = string.Empty;
            string AllProfileId = string.Empty;
            string FbProfileId = string.Empty;
            string TwtProfileId = string.Empty;
            string[] ArrProfileId = { };
            List<Domain.Socioboard.Domain.TwitterDirectMessages> twitterdirectmessages_15 = new List<Domain.Socioboard.Domain.TwitterDirectMessages>();
            List<Domain.Socioboard.Domain.TwitterDirectMessages> twitterdirectmessages_30 = new List<Domain.Socioboard.Domain.TwitterDirectMessages>();
            List<Domain.Socioboard.Domain.TwitterDirectMessages> twitterdirectmessages_60 = new List<Domain.Socioboard.Domain.TwitterDirectMessages>();
            List<Domain.Socioboard.Domain.TwitterDirectMessages> twitterdirectmessages_90 = new List<Domain.Socioboard.Domain.TwitterDirectMessages>();
            string strtwitterfollowers = "from TwitterDirectMessages t where t.SenderId =: ProfileId";


            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                try
                {
                    twitterdirectmessages_90 = session.CreateQuery(strtwitterfollowers)
                                                           .SetParameter("ProfileId", profileid)
                                                           .List<Domain.Socioboard.Domain.TwitterDirectMessages>().ToList().Where(x => x.CreatedDate <= DateTime.Now.AddDays(1).Date.AddSeconds(-1) && x.CreatedDate >= (DateTime.Now.AddDays(-90).Date.AddSeconds(1))).GroupBy(x=>x.MessageId).Select(g=>g.First()).ToList();

                    twitterdirectmessages_60 = twitterdirectmessages_90.Where(x => x.CreatedDate <= DateTime.Now.AddDays(1).Date.AddSeconds(-1) && x.CreatedDate >= (DateTime.Now.AddDays(-60).Date.AddSeconds(1))).ToList();

                    twitterdirectmessages_30 = twitterdirectmessages_90.Where(x => x.CreatedDate <= DateTime.Now.AddDays(1).Date.AddSeconds(-1) && x.CreatedDate >= (DateTime.Now.AddDays(-30).Date.AddSeconds(1))).ToList();

                    twitterdirectmessages_15 = twitterdirectmessages_90.Where(x => x.CreatedDate <= DateTime.Now.AddDays(1).Date.AddSeconds(-1) && x.CreatedDate >= (DateTime.Now.AddDays(-15).Date.AddSeconds(1))).ToList();


                }

                catch (Exception e)
                {

                    logger.Error(e.Message);
                    logger.Error(e.StackTrace);

                }
            }

            present_date = DateTime.Now;

            while (present_date.Date != DateTime.Now.Date.AddDays(-90))
            {

                try
                {
                    List<Domain.Socioboard.Domain.TwitterDirectMessages> _twitterdirectmessages = new List<Domain.Socioboard.Domain.TwitterDirectMessages>();

                    using (NHibernate.ISession session4 = SessionFactory.GetNewSession())
                    {
                        int item1 = 0;

                        try
                        {

                            _twitterdirectmessages = twitterdirectmessages_90.Where(m => m.CreatedDate >= present_date.Date.AddSeconds(1) && m.CreatedDate <= present_date.AddDays(1).Date.AddSeconds(-1)).ToList();

                            //item1 = session4.QueryOver<Domain.Socioboard.Domain.TwitterDirectMessages>().Where(m => m.CreatedDate >= present_date.Date.AddSeconds(1) && m.CreatedDate <= present_date.AddDays(1).Date.AddSeconds(-1)).Where(s => s.SenderId == profileid)
                            //                              .Select(Projections.CountDistinct<Domain.Socioboard.Domain.TwitterDirectMessages>(x => x.MessageId))
                            //                              .FutureValue<int>().Value;

                        }
                        catch (Exception e)
                        {
                            item1 = 0;
                        }

                        int add_data = _twitterdirectmessages.Count;

                        perday_90 = perday_90 + add_data.ToString() + ",";

                        present_date = present_date.AddDays(-1);
                        logger.Error("perdaydirectmessagesent>>" + perday_90);

                    }
                }
                catch (Exception e)
                {
                    logger.Error("perdaydmsenterror >>" + e);
                    Console.Write(e.StackTrace);
                }
            }




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


            r._15 = twitterdirectmessages_15.Count;
            r._30 = twitterdirectmessages_30.Count;
            r._60 = twitterdirectmessages_60.Count;
            r._90 = twitterdirectmessages_90.Count;



            r.perday_15 = perday_15;
            r.perday_30 = perday_30;
            r.perday_60 = perday_60;
            r.perday_90 = perday_90;

            string ret = new JavaScriptSerializer().Serialize(r);
            return ret;

        }



        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]

        public string twittermention(string profileid)
        {

            Domain.Socioboard.Domain.ReturnData r = new Domain.Socioboard.Domain.ReturnData();

            DateTime present_date = DateTime.Now;
            string perday_15 = string.Empty;
            string perday_30 = string.Empty;
            string perday_60 = string.Empty;
            string perday_90 = string.Empty;
            string AllProfileId = string.Empty;
            string FbProfileId = string.Empty;
            string TwtProfileId = string.Empty;
            string[] ArrProfileId = { };
            List<Domain.Socioboard.Domain.InboxMessages> inboxmessages_15 = new List<Domain.Socioboard.Domain.InboxMessages>();
            List<Domain.Socioboard.Domain.InboxMessages> inboxmessages_30 = new List<Domain.Socioboard.Domain.InboxMessages>();
            List<Domain.Socioboard.Domain.InboxMessages> inboxmessages_60 = new List<Domain.Socioboard.Domain.InboxMessages>();
            List<Domain.Socioboard.Domain.InboxMessages> inboxmessages_90 = new List<Domain.Socioboard.Domain.InboxMessages>();
            string strtwitterfollowers = "from InboxMessages t where t.ProfileId =: ProfileId and t.MessageType = : msgtype";


            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                try
                {
                    inboxmessages_90 = session.CreateQuery(strtwitterfollowers)
                                                           .SetParameter("ProfileId", profileid)
                                                            .SetParameter("msgtype", "twt_mention")
                                                           .List<Domain.Socioboard.Domain.InboxMessages>().ToList().Where(x => x.CreatedTime <= DateTime.Now.AddDays(1).Date.AddSeconds(-1) && x.CreatedTime >= (DateTime.Now.AddDays(-90).Date.AddSeconds(1))).GroupBy(x=>x.MessageId).Select(g=>g.First()).ToList();
                    inboxmessages_60 = inboxmessages_90.Where(x => x.CreatedTime <= DateTime.Now.AddDays(1).Date.AddSeconds(-1) && x.CreatedTime >= (DateTime.Now.AddDays(-60).Date.AddSeconds(1))).ToList();

                    inboxmessages_30 = inboxmessages_90.Where(x => x.CreatedTime <= DateTime.Now.AddDays(1).Date.AddSeconds(-1) && x.CreatedTime >= (DateTime.Now.AddDays(-30).Date.AddSeconds(1))).ToList();

                    inboxmessages_15 = inboxmessages_90.Where(x => x.CreatedTime <= DateTime.Now.AddDays(1).Date.AddSeconds(-1) && x.CreatedTime >= (DateTime.Now.AddDays(-15).Date.AddSeconds(1))).ToList();


                }

                catch (Exception e)
                {

                    logger.Error(e.Message);
                    logger.Error(e.StackTrace);

                }
            }

            present_date = DateTime.Now;

            while (present_date.Date != DateTime.Now.Date.AddDays(-90))
            {

                try
                {
                    List<Domain.Socioboard.Domain.InboxMessages> _inboxmessages = new List<Domain.Socioboard.Domain.InboxMessages>();

                    using (NHibernate.ISession session4 = SessionFactory.GetNewSession())
                    {
                        int item = 0;

                        try
                        {
                            _inboxmessages = inboxmessages_90.Where(m => m.CreatedTime >= present_date.Date.AddSeconds(1) && m.CreatedTime <= present_date.AddDays(1).Date.AddSeconds(-1)).ToList();

                            //item = session4.QueryOver<Domain.Socioboard.Domain.InboxMessages>().Where(m => m.CreatedTime >= present_date.Date.AddSeconds(1) && m.CreatedTime <= present_date.AddDays(1).Date.AddSeconds(-1) && m.MessageType == "twt_mention").Where(s => s.RecipientId == profileid)
                            //                         .Select(Projections.CountDistinct<Domain.Socioboard.Domain.InboxMessages>(x => x.MessageId))
                            //                         .FutureValue<int>().Value;
                        }
                        catch (Exception e)
                        {
                            item = 0;

                        }

                        int add_data = _inboxmessages.Count;

                        perday_90 = perday_90 + add_data.ToString() + ",";

                        present_date = present_date.AddDays(-1);
                        logger.Error("perdaymention >>" + perday_90);

                    }
                }
                catch (Exception e)
                {
                    logger.Error("perdaymentionerror >>" + e);
                    logger.Error(e.StackTrace);
                    Console.Write(e.StackTrace);
                }
            }



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


            r._15 = inboxmessages_15.Count;
            r._30 = inboxmessages_30.Count;
            r._60 = inboxmessages_60.Count;
            r._90 = inboxmessages_90.Count;



            r.perday_15 = perday_15;
            r.perday_30 = perday_30;
            r.perday_60 = perday_60;
            r.perday_90 = perday_90;

            string ret = new JavaScriptSerializer().Serialize(r);
            return ret;

        }



        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]

        public string twitterretweets(string profileid)
        {

            Domain.Socioboard.Domain.ReturnData r = new Domain.Socioboard.Domain.ReturnData();

            DateTime present_date = DateTime.Now;
            string perday_15 = string.Empty;
            string perday_30 = string.Empty;
            string perday_60 = string.Empty;
            string perday_90 = string.Empty;
            string AllProfileId = string.Empty;
            string FbProfileId = string.Empty;
            string TwtProfileId = string.Empty;
            string[] ArrProfileId = { };
            List<Domain.Socioboard.Domain.InboxMessages> inboxmessages_15 = new List<Domain.Socioboard.Domain.InboxMessages>();
            List<Domain.Socioboard.Domain.InboxMessages> inboxmessages_30 = new List<Domain.Socioboard.Domain.InboxMessages>();
            List<Domain.Socioboard.Domain.InboxMessages> inboxmessages_60 = new List<Domain.Socioboard.Domain.InboxMessages>();
            List<Domain.Socioboard.Domain.InboxMessages> inboxmessages_90 = new List<Domain.Socioboard.Domain.InboxMessages>();
            string strtwitterfollowers = "from InboxMessages t where t.ProfileId =: ProfileId and t.MessageType = : msgtype";


            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                try
                {
                    inboxmessages_90 = session.CreateQuery(strtwitterfollowers)
                                                           .SetParameter("ProfileId", profileid)
                                                            .SetParameter("msgtype", "twt_retweet")
                                                           .List<Domain.Socioboard.Domain.InboxMessages>().ToList().Where(x => x.CreatedTime <= DateTime.Now.AddDays(1).Date.AddSeconds(-1) && x.CreatedTime >= (DateTime.Now.AddDays(-90).Date.AddSeconds(1))).GroupBy(x => new { x.MessageId, x.FromId }).Select(g => g.First()).ToList();

                    inboxmessages_60 = inboxmessages_90.Where(x => x.CreatedTime <= DateTime.Now.AddDays(1).Date.AddSeconds(-1) && x.CreatedTime >= (DateTime.Now.AddDays(-60).Date.AddSeconds(1))).ToList();

                    inboxmessages_30 = inboxmessages_90.Where(x => x.CreatedTime <= DateTime.Now.AddDays(1).Date.AddSeconds(-1) && x.CreatedTime >= (DateTime.Now.AddDays(-30).Date.AddSeconds(1))).ToList();

                    inboxmessages_15 = inboxmessages_90.Where(x => x.CreatedTime <= DateTime.Now.AddDays(1).Date.AddSeconds(-1) && x.CreatedTime >= (DateTime.Now.AddDays(-15).Date.AddSeconds(1))).ToList();


                }

                catch (Exception e)
                {

                    logger.Error(e.Message);
                    logger.Error(e.StackTrace);

                }
            }

            present_date = DateTime.Now;

            while (present_date.Date != DateTime.Now.Date.AddDays(-90))
            {

                try
                {
                    List<Domain.Socioboard.Domain.InboxMessages> _inboxmessages = new List<Domain.Socioboard.Domain.InboxMessages>();

                    using (NHibernate.ISession session4 = SessionFactory.GetNewSession())
                    {
                        int item = 0;

                        try
                        {
                            _inboxmessages = inboxmessages_90.Where(m => m.CreatedTime >= present_date.Date.AddSeconds(1) && m.CreatedTime <= present_date.AddDays(1).Date.AddSeconds(-1)).ToList();

                            //item = session4.QueryOver<Domain.Socioboard.Domain.InboxMessages>().Where(m => m.CreatedTime >= present_date.Date.AddSeconds(1) && m.CreatedTime <= present_date.AddDays(1).Date.AddSeconds(-1) && m.MessageType == "twt_retweet").Where(s => s.RecipientId == profileid)
                            //                         .Select(Projections.CountDistinct<Domain.Socioboard.Domain.InboxMessages>(x => x.MessageId))
                            //                         .FutureValue<int>().Value;
                        }
                        catch (Exception e)
                        {
                            item = 0;

                        }

                        int add_data = _inboxmessages.Count;

                        perday_90 = perday_90 + add_data.ToString() + ",";

                        present_date = present_date.AddDays(-1);
                        logger.Error("perdaymention >>" + perday_90);

                    }
                }
                catch (Exception e)
                {
                    logger.Error("perdaymentionerror >>" + e);
                    logger.Error(e.StackTrace);
                    Console.Write(e.StackTrace);
                }
            }




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

            r._15 = inboxmessages_15.Count;
            r._30 = inboxmessages_30.Count;
            r._60 = inboxmessages_60.Count;
            r._90 = inboxmessages_90.Count;



            r.perday_15 = perday_15;
            r.perday_30 = perday_30;
            r.perday_60 = perday_60;
            r.perday_90 = perday_90;

            string ret = new JavaScriptSerializer().Serialize(r);
            return ret;

        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]

        public string clicks(string profileid)
        {

            Domain.Socioboard.Domain.ReturnData r = new Domain.Socioboard.Domain.ReturnData();

            DateTime present_date = DateTime.Now;
            string perday_15 = string.Empty;
            string perday_30 = string.Empty;
            string perday_60 = string.Empty;
            string perday_90 = string.Empty;
            string AllProfileId = string.Empty;
            string FbProfileId = string.Empty;
            string TwtProfileId = string.Empty;
            string[] ArrProfileId = { };
            List<Domain.Socioboard.Domain.ScheduledMessage> clicks_15 = new List<Domain.Socioboard.Domain.ScheduledMessage>();
            List<Domain.Socioboard.Domain.ScheduledMessage> clicks_30 = new List<Domain.Socioboard.Domain.ScheduledMessage>();
            List<Domain.Socioboard.Domain.ScheduledMessage> clicks_60 = new List<Domain.Socioboard.Domain.ScheduledMessage>();
            List<Domain.Socioboard.Domain.ScheduledMessage> clicks_90 = new List<Domain.Socioboard.Domain.ScheduledMessage>();
            List<Domain.Socioboard.Domain.ScheduledMessage> perday_clicks_90 = new List<Domain.Socioboard.Domain.ScheduledMessage>();

            string link = "http://bit.ly/";

            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                try
                {


                    clicks_90 = session.Query<Domain.Socioboard.Domain.ScheduledMessage>().Where(x => x.ShareMessage.Contains(link) && x.ProfileId == profileid && x.Status == true && x.ScheduleTime <= DateTime.Now.AddDays(1).Date.AddSeconds(-1) && x.ScheduleTime > (DateTime.Now.AddDays(-90).Date)).ToList<Domain.Socioboard.Domain.ScheduledMessage>();

                    clicks_60 = clicks_90.Where(x => x.ScheduleTime <= DateTime.Now.AddDays(1).Date.AddSeconds(-1) && x.ScheduleTime > (DateTime.Now.AddDays(-60).Date)).ToList<Domain.Socioboard.Domain.ScheduledMessage>();

                    clicks_30 = clicks_90.Where(x => x.ScheduleTime <= DateTime.Now.AddDays(1).Date.AddSeconds(-1) && x.ScheduleTime > (DateTime.Now.AddDays(-30).Date)).ToList<Domain.Socioboard.Domain.ScheduledMessage>();

                    clicks_15 = clicks_90.Where(x => x.ScheduleTime <= DateTime.Now.AddDays(1).Date.AddSeconds(-1) && x.ScheduleTime > (DateTime.Now.AddDays(-15).Date)).ToList<Domain.Socioboard.Domain.ScheduledMessage>();

                }

                catch (Exception e)
                {

                    logger.Error(e.Message);
                    logger.Error(e.StackTrace);

                }
            }

            present_date = DateTime.Now;

            while (present_date.Date != DateTime.Now.Date.AddDays(-90))
            {

                try
                {

                    using (NHibernate.ISession session4 = SessionFactory.GetNewSession())
                    {
                        int item = 0;

                        try
                        {
                            //perday_clicks_90 = session4.Query<Domain.Socioboard.Domain.ScheduledMessage>().Where(x => x.ShareMessage.Contains(link) && x.ProfileId == profileid && x.Status == true && x.ScheduleTime <= present_date.AddDays(1).Date.AddSeconds(-1) && x.ScheduleTime >= present_date.Date.AddSeconds(1)).ToList<Domain.Socioboard.Domain.ScheduledMessage>();
                            perday_clicks_90 = clicks_90.Where(x => x.ScheduleTime <= present_date.AddDays(1).Date.AddSeconds(-1) && x.ScheduleTime >= present_date.Date.AddSeconds(1)).ToList<Domain.Socioboard.Domain.ScheduledMessage>();
                            item = perday_clicks_90.Count;
                        }
                        catch (Exception e)
                        {
                            item = 0;

                        }

                        int add_data = item;

                        perday_90 = perday_90 + add_data.ToString() + ",";

                        present_date = present_date.AddDays(-1);
                        logger.Error("perdayclick >>" + perday_90);

                    }
                }
                catch (Exception e)
                {
                    logger.Error("perdayclickerror >>" + e);
                    logger.Error(e.StackTrace);
                    Console.Write(e.StackTrace);
                }
            }




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

            r._15 = clicks_15.Count;
            r._30 = clicks_30.Count;
            r._60 = clicks_60.Count;
            r._90 = clicks_90.Count;



            r.perday_15 = perday_15;
            r.perday_30 = perday_30;
            r.perday_60 = perday_60;
            r.perday_90 = perday_90;

            string ret = new JavaScriptSerializer().Serialize(r);
            return ret;

        }



        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]

        public string messagesent(string profileid)
        {

            Domain.Socioboard.Domain.ReturnData r = new Domain.Socioboard.Domain.ReturnData();



            DateTime present_date = DateTime.Now;
            string perday_15 = string.Empty;
            string perday_30 = string.Empty;
            string perday_60 = string.Empty;
            string perday_90 = string.Empty;
            string AllProfileId = string.Empty;
            string FbProfileId = string.Empty;
            string TwtProfileId = string.Empty;
            string[] ArrProfileId = { };
            List<Domain.Socioboard.Domain.TwitterDirectMessages> twitterdirectmessages_15 = new List<Domain.Socioboard.Domain.TwitterDirectMessages>();
            List<Domain.Socioboard.Domain.TwitterDirectMessages> twitterdirectmessages_30 = new List<Domain.Socioboard.Domain.TwitterDirectMessages>();
            List<Domain.Socioboard.Domain.TwitterDirectMessages> twitterdirectmessages_60 = new List<Domain.Socioboard.Domain.TwitterDirectMessages>();
            List<Domain.Socioboard.Domain.TwitterDirectMessages> twitterdirectmessages_90 = new List<Domain.Socioboard.Domain.TwitterDirectMessages>();
            List<Domain.Socioboard.Domain.ScheduledMessage> schedule_15 = new List<Domain.Socioboard.Domain.ScheduledMessage>();
            List<Domain.Socioboard.Domain.ScheduledMessage> schedule_30 = new List<Domain.Socioboard.Domain.ScheduledMessage>();
            List<Domain.Socioboard.Domain.ScheduledMessage> schedule_60 = new List<Domain.Socioboard.Domain.ScheduledMessage>();
            List<Domain.Socioboard.Domain.ScheduledMessage> schedule_90 = new List<Domain.Socioboard.Domain.ScheduledMessage>();

            string strtwitterfollowers = "from TwitterDirectMessages t where t.SenderId =: ProfileId";


            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                try
                {

                    twitterdirectmessages_90 = session.CreateQuery(strtwitterfollowers)
                                                           .SetParameter("ProfileId", profileid)
                                                           .List<Domain.Socioboard.Domain.TwitterDirectMessages>().ToList().Where(x => x.CreatedDate <= DateTime.Now.AddDays(1).Date.AddSeconds(-1) && x.CreatedDate > (DateTime.Now.AddDays(-90).Date)).GroupBy(x => x.MessageId).Select(g => g.First()).ToList();

                    twitterdirectmessages_60 = twitterdirectmessages_90.Where(x => x.CreatedDate <= DateTime.Now.AddDays(1).Date.AddSeconds(-1) && x.CreatedDate > (DateTime.Now.AddDays(-60).Date)).ToList();

                    twitterdirectmessages_30 = twitterdirectmessages_90.Where(x => x.CreatedDate <= DateTime.Now.AddDays(1).Date.AddSeconds(-1) && x.CreatedDate > (DateTime.Now.AddDays(-30).Date)).ToList();

                    twitterdirectmessages_15 = twitterdirectmessages_90.Where(x => x.CreatedDate <= DateTime.Now.AddDays(1).Date.AddSeconds(-1) && x.CreatedDate > (DateTime.Now.AddDays(-15).Date)).ToList();

                }

                catch (Exception e)
                {

                    logger.Error(e.Message);
                    logger.Error(e.StackTrace);

                }
            }
            string strschedule = "from ScheduledMessage t where t.ProfileId =: profileid and t.Status = : msgtype";

            using (NHibernate.ISession session2 = SessionFactory.GetNewSession())
            {
                try
                {

                    schedule_90 = session2.CreateQuery(strschedule)
                                           .SetParameter("profileid", profileid)
                                           .SetParameter("msgtype", true)
                                           .List<Domain.Socioboard.Domain.ScheduledMessage>().ToList().Where(x => x.ScheduleTime <= DateTime.Now.AddDays(1).Date.AddSeconds(-1) && x.ScheduleTime > (DateTime.Now.AddDays(-90).Date)).ToList();

                    schedule_60 = schedule_90.Where(x => x.ScheduleTime <= DateTime.Now.AddDays(1).Date.AddSeconds(-1) && x.ScheduleTime > (DateTime.Now.AddDays(-60).Date)).ToList();

                    schedule_30 = schedule_90.Where(x => x.ScheduleTime <= DateTime.Now.AddDays(1).Date.AddSeconds(-1) && x.ScheduleTime > (DateTime.Now.AddDays(-30).Date)).ToList();

                    schedule_15 = schedule_90.Where(x => x.ScheduleTime <= DateTime.Now.AddDays(1).Date.AddSeconds(-1) && x.ScheduleTime > (DateTime.Now.AddDays(-15).Date)).ToList();


                }
                catch (Exception e)
                {

                    logger.Error(e.Message);
                    logger.Error(e.StackTrace);
                }

            }

            present_date = DateTime.Now;

            while (present_date.Date != DateTime.Now.Date.AddDays(-90))
            {

                try
                {
                    using (NHibernate.ISession session5 = SessionFactory.GetNewSession())
                    {
                        int item = 0;
                        int item1 = 0;

                        try
                        {
                            //List<Domain.Socioboard.Domain.ScheduledMessage> perday_schedule_90 = session5.CreateQuery(strschedule)
                            //                                                            .SetParameter("profileid", profileid)
                            //                                                            .SetParameter("msgtype", true)
                            //                                                            .List<Domain.Socioboard.Domain.ScheduledMessage>().ToList().Where(x => x.ScheduleTime <= present_date.AddDays(1).Date.AddSeconds(-1) && x.ScheduleTime > present_date.Date).ToList();

                            List<Domain.Socioboard.Domain.ScheduledMessage> perday_schedule_90 = schedule_90.Where(x => x.ScheduleTime <= present_date.AddDays(1).Date.AddSeconds(-1) && x.ScheduleTime > present_date.Date).ToList();

                            item = perday_schedule_90.Count;
                        }
                        catch (Exception e)
                        {

                            item = 0;

                        }

                        try
                        {
                            //List<Domain.Socioboard.Domain.TwitterDirectMessages> perday_twitterdirectmessages_90 = session5.CreateQuery(strtwitterfollowers)
                            //                                    .SetParameter("ProfileId", profileid)
                            //                                     .List<Domain.Socioboard.Domain.TwitterDirectMessages>().ToList().Where(x => x.CreatedDate <= present_date.AddDays(1).Date.AddSeconds(-1) && x.CreatedDate > present_date.Date).ToList();

                            List<Domain.Socioboard.Domain.TwitterDirectMessages> perday_twitterdirectmessages_90 = twitterdirectmessages_90.Where(x => x.CreatedDate <= present_date.AddDays(1).Date.AddSeconds(-1) && x.CreatedDate > present_date.Date).ToList();
                            item1 = perday_twitterdirectmessages_90.Count;
                        }
                        catch (Exception e)
                        {
                            item1 = 0;

                        }

                        int add_data = item + item1;

                        perday_90 = perday_90 + add_data.ToString() + ",";

                        present_date = present_date.AddDays(-1);
                        logger.Error("perdaysent >>" + perday_90);
                    }
                }
                catch (Exception e)
                {
                    logger.Error("perdaydmsenterror >>" + e);
                    Console.Write(e.StackTrace);
                }
            }

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


            r._15 = twitterdirectmessages_15.Count + schedule_15.Count;
            r._30 = twitterdirectmessages_30.Count + schedule_30.Count;
            r._60 = twitterdirectmessages_60.Count + schedule_60.Count;
            r._90 = twitterdirectmessages_90.Count + schedule_90.Count;



            r.perday_15 = perday_15;
            r.perday_30 = perday_30;
            r.perday_60 = perday_60;
            r.perday_90 = perday_90;

            string ret = new JavaScriptSerializer().Serialize(r);
            return ret;

        }
        
        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]

        public string messagerecieved(string profileid)
        {

            Domain.Socioboard.Domain.ReturnData r = new Domain.Socioboard.Domain.ReturnData();



            DateTime present_date = DateTime.Now;
            string perday_15 = string.Empty;
            string perday_30 = string.Empty;
            string perday_60 = string.Empty;
            string perday_90 = string.Empty;
            string AllProfileId = string.Empty;
            string FbProfileId = string.Empty;
            string TwtProfileId = string.Empty;
            string[] ArrProfileId = { };
            List<Domain.Socioboard.Domain.TwitterDirectMessages> twitterdirectmessages_15 = new List<Domain.Socioboard.Domain.TwitterDirectMessages>();
            List<Domain.Socioboard.Domain.TwitterDirectMessages> twitterdirectmessages_30 = new List<Domain.Socioboard.Domain.TwitterDirectMessages>();
            List<Domain.Socioboard.Domain.TwitterDirectMessages> twitterdirectmessages_60 = new List<Domain.Socioboard.Domain.TwitterDirectMessages>();
            List<Domain.Socioboard.Domain.TwitterDirectMessages> twitterdirectmessages_90 = new List<Domain.Socioboard.Domain.TwitterDirectMessages>();
            List<Domain.Socioboard.Domain.InboxMessages> inboxmessages_15 = new List<Domain.Socioboard.Domain.InboxMessages>();
            List<Domain.Socioboard.Domain.InboxMessages> inboxmessages_30 = new List<Domain.Socioboard.Domain.InboxMessages>();
            List<Domain.Socioboard.Domain.InboxMessages> inboxmessages_60 = new List<Domain.Socioboard.Domain.InboxMessages>();
            List<Domain.Socioboard.Domain.InboxMessages> inboxmessages_90 = new List<Domain.Socioboard.Domain.InboxMessages>();

            string strtwitterfollowers = "from TwitterDirectMessages t where t.SenderId =: ProfileId";


            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                try
                {
                   
                    twitterdirectmessages_90 = session.CreateQuery(strtwitterfollowers)
                                                           .SetParameter("ProfileId", profileid)
                                                           .List<Domain.Socioboard.Domain.TwitterDirectMessages>().ToList().Where(x => x.CreatedDate <= DateTime.Now.AddDays(1).Date.AddSeconds(-1) && x.CreatedDate > (DateTime.Now.AddDays(-90).Date)).GroupBy(x=>x.MessageId).Select(g=>g.First()).ToList();
                    
                    twitterdirectmessages_60=twitterdirectmessages_90.Where(x => x.CreatedDate <= DateTime.Now.AddDays(1).Date.AddSeconds(-1) && x.CreatedDate > (DateTime.Now.AddDays(-60).Date)).ToList();

                    twitterdirectmessages_30=twitterdirectmessages_90.Where(x => x.CreatedDate <= DateTime.Now.AddDays(1).Date.AddSeconds(-1) && x.CreatedDate > (DateTime.Now.AddDays(-30).Date)).ToList();

                    twitterdirectmessages_15=twitterdirectmessages_90.Where(x => x.CreatedDate <= DateTime.Now.AddDays(1).Date.AddSeconds(-1) && x.CreatedDate > (DateTime.Now.AddDays(-15).Date)).ToList();
                }

                catch (Exception e)
                {

                    logger.Error(e.Message);
                    logger.Error(e.StackTrace);

                }
            }
            string strinboxmessages = "from InboxMessages t where t.RecipientId =: profileid and t.MessageType = : msgtype";

            using (NHibernate.ISession session2 = SessionFactory.GetNewSession())
            {
                try
                {
                    
                    inboxmessages_90 = session2.CreateQuery(strinboxmessages)
                                           .SetParameter("profileid", profileid)
                                           .SetParameter("msgtype", "twt_mention")
                                             .List<Domain.Socioboard.Domain.InboxMessages>().ToList().Where(x => x.CreatedTime <= DateTime.Now.AddDays(1).Date.AddSeconds(-1) && x.CreatedTime > (DateTime.Now.AddDays(-90).Date)).GroupBy(x=>x.MessageId).Select(g=>g.First()).ToList();

                    inboxmessages_60=inboxmessages_90.Where(x => x.CreatedTime <= DateTime.Now.AddDays(1).Date.AddSeconds(-1) && x.CreatedTime > (DateTime.Now.AddDays(-60).Date)).ToList();

                    inboxmessages_30=inboxmessages_90.Where(x => x.CreatedTime <= DateTime.Now.AddDays(1).Date.AddSeconds(-1) && x.CreatedTime > (DateTime.Now.AddDays(-30).Date)).ToList();

                    inboxmessages_15=inboxmessages_90.Where(x => x.CreatedTime <= DateTime.Now.AddDays(1).Date.AddSeconds(-1) && x.CreatedTime > (DateTime.Now.AddDays(-15).Date)).ToList();


                }
                catch (Exception e)
                {

                    logger.Error(e.Message);
                    logger.Error(e.StackTrace);
                }

            }

            present_date = DateTime.Now;

            while (present_date.Date != DateTime.Now.Date.AddDays(-90))
            {
                List<Domain.Socioboard.Domain.TwitterDirectMessages> _twitterdirectmessages = new List<Domain.Socioboard.Domain.TwitterDirectMessages>();
                List<Domain.Socioboard.Domain.InboxMessages> _inboxmessages = new List<Domain.Socioboard.Domain.InboxMessages>();
                try
                {
                    using (NHibernate.ISession session1 = SessionFactory.GetNewSession())
                    {
                        int item=0;
                        int item1 = 0;
                        try
                        {

                            _inboxmessages=inboxmessages_90.Where(m => m.CreatedTime >= present_date.Date && m.CreatedTime <= present_date.AddDays(1).Date.AddSeconds(-1)).ToList();
                                //item = session1.QueryOver<Domain.Socioboard.Domain.InboxMessages>().Where(m => m.CreatedTime >= present_date.Date && m.CreatedTime <= present_date.AddDays(1).Date.AddSeconds(-1) && m.MessageType == "twt_mention").Where(S => S.RecipientId == profileid)
                                //                            .Select(Projections.CountDistinct<Domain.Socioboard.Domain.InboxMessages>(x => x.MessageId))
                                //                            .FutureValue<int>().Value;
                            item=_inboxmessages.Count;
                        }
                        catch (Exception e)
                        {
                            item = 0;
                                            
                        }


                        try
                        {
                            _twitterdirectmessages = twitterdirectmessages_90.Where(m => m.CreatedDate >= present_date.Date && m.CreatedDate <= present_date.AddDays(1).Date.AddSeconds(-1)).ToList();

                            //item1 = session1.QueryOver<Domain.Socioboard.Domain.TwitterDirectMessages>().Where(m => m.CreatedDate >= present_date.Date && m.CreatedDate <= present_date.AddDays(1).Date.AddSeconds(-1)).Where(S => S.RecipientId == profileid)
                            //                                .Select(Projections.CountDistinct<Domain.Socioboard.Domain.TwitterDirectMessages>(x => x.MessageId))
                            //                                .FutureValue<int>().Value;
                            item1 = _twitterdirectmessages.Count;
                        }
                        catch (Exception e)
                        {
                            item1 = 0;                         
                        }

                        int add_data = item + item1;

                        perday_90 = perday_90 + add_data.ToString() + ",";

                        present_date = present_date.AddDays(-1);
                        logger.Error("perdayinbox >>" + perday_90);
                    }
                }
                catch (Exception e)
                {
                    logger.Error("perdaydmsenterror >>" + e);
                    Console.Write(e.StackTrace);
                }
            }




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


            r._15 = twitterdirectmessages_15.Count + inboxmessages_15.Count;
            r._30 = twitterdirectmessages_30.Count + inboxmessages_30.Count;
            r._60 = twitterdirectmessages_60.Count + inboxmessages_60.Count;
            r._90 = twitterdirectmessages_90.Count + inboxmessages_90.Count;



            r.perday_15 = perday_15;
            r.perday_30 = perday_30;
            r.perday_60 = perday_60;
            r.perday_90 = perday_90;

            string ret = new JavaScriptSerializer().Serialize(r);
            return ret;

        }

        [WebMethod]
        public void top_five_fans(string profileid)
        {

            List<Domain.Socioboard.Domain.TopFiveFans_15> topfans_15 = new List<Domain.Socioboard.Domain.TopFiveFans_15>();
            List<Domain.Socioboard.Domain.TopFiveFans_30> topfans_30 = new List<Domain.Socioboard.Domain.TopFiveFans_30>();
            List<Domain.Socioboard.Domain.TopFiveFans_60> topfans_60 = new List<Domain.Socioboard.Domain.TopFiveFans_60>();
            List<Domain.Socioboard.Domain.TopFiveFans_90> topfans_90 = new List<Domain.Socioboard.Domain.TopFiveFans_90>();
           
            List<Domain.Socioboard.Domain.InboxMessages> mention_retweets_15 = new List<Domain.Socioboard.Domain.InboxMessages>();
            List<Domain.Socioboard.Domain.InboxMessages> mention_retweets_30 = new List<Domain.Socioboard.Domain.InboxMessages>();
            List<Domain.Socioboard.Domain.InboxMessages> mention_retweets_60 = new List<Domain.Socioboard.Domain.InboxMessages>();
            List<Domain.Socioboard.Domain.InboxMessages> mention_retweets_90 = new List<Domain.Socioboard.Domain.InboxMessages>();
            
            Domain.Socioboard.Domain.TopFiveFans_15 first_15 = new Domain.Socioboard.Domain.TopFiveFans_15();
            Domain.Socioboard.Domain.TopFiveFans_15 second_15 = new Domain.Socioboard.Domain.TopFiveFans_15();
            Domain.Socioboard.Domain.TopFiveFans_15 third_15 = new Domain.Socioboard.Domain.TopFiveFans_15();
            Domain.Socioboard.Domain.TopFiveFans_15 fourth_15 = new Domain.Socioboard.Domain.TopFiveFans_15();
            Domain.Socioboard.Domain.TopFiveFans_15 fifth_15 = new Domain.Socioboard.Domain.TopFiveFans_15();

            Domain.Socioboard.Domain.TopFiveFans_30 first_30 = new Domain.Socioboard.Domain.TopFiveFans_30();
            Domain.Socioboard.Domain.TopFiveFans_30 second_30 = new Domain.Socioboard.Domain.TopFiveFans_30();
            Domain.Socioboard.Domain.TopFiveFans_30 third_30 = new Domain.Socioboard.Domain.TopFiveFans_30();
            Domain.Socioboard.Domain.TopFiveFans_30 fourth_30 = new Domain.Socioboard.Domain.TopFiveFans_30();
            Domain.Socioboard.Domain.TopFiveFans_30 fifth_30 = new Domain.Socioboard.Domain.TopFiveFans_30();
            
            Domain.Socioboard.Domain.TopFiveFans_60 first_60 = new Domain.Socioboard.Domain.TopFiveFans_60();
            Domain.Socioboard.Domain.TopFiveFans_60 second_60 = new Domain.Socioboard.Domain.TopFiveFans_60();
            Domain.Socioboard.Domain.TopFiveFans_60 third_60 = new Domain.Socioboard.Domain.TopFiveFans_60();
            Domain.Socioboard.Domain.TopFiveFans_60 fourth_60 = new Domain.Socioboard.Domain.TopFiveFans_60();
            Domain.Socioboard.Domain.TopFiveFans_60 fifth_60 = new Domain.Socioboard.Domain.TopFiveFans_60();

            Domain.Socioboard.Domain.TopFiveFans_90 first_90 = new Domain.Socioboard.Domain.TopFiveFans_90();
            Domain.Socioboard.Domain.TopFiveFans_90 second_90 = new Domain.Socioboard.Domain.TopFiveFans_90();
            Domain.Socioboard.Domain.TopFiveFans_90 third_90 = new Domain.Socioboard.Domain.TopFiveFans_90();
            Domain.Socioboard.Domain.TopFiveFans_90 fourth_90 = new Domain.Socioboard.Domain.TopFiveFans_90();
            Domain.Socioboard.Domain.TopFiveFans_90 fifth_90 = new Domain.Socioboard.Domain.TopFiveFans_90();
        
            try
            {
                using (NHibernate.ISession session = SessionFactory.GetNewSession())
                {
                    deletefans_90(profileid);
                    mention_retweets_90 = session.Query<Domain.Socioboard.Domain.InboxMessages>().Where(x => x.CreatedTime <= DateTime.Now.AddDays(1).Date.AddSeconds(-1) && x.CreatedTime > (DateTime.Now.AddDays(-90).Date) && x.ProfileId == profileid && (x.MessageType == "twt_mention" || x.MessageType == "twt_retweets")).ToList();
                    
                    dynamic list_90 = mention_retweets_90.GroupBy(n => n.FromId).Select(group => new Domain.Socioboard.Domain.TopFiveFans_90(group.Count(), group.First().FromId, profileid,group.First().FromName)).ToList();
                    topfans_90 = (List<Domain.Socioboard.Domain.TopFiveFans_90>)list_90;
                    first_90 = topfans_90.OrderByDescending(r => r.Count).FirstOrDefault();
                    if (first_90 != null)
                    {

                        int mentions = mention_retweets_90.Where(x => x.FromId == first_90.FromId && x.MessageType == "twt_mention").ToList().Count;
                        int retweets = mention_retweets_90.Where(x => x.FromId == first_90.FromId && x.MessageType == "twt_retweet").ToList().Count;
                        string image_url = mention_retweets_90.Where(a => a.FromId == first_90.FromId).Select(x => x.FromImageUrl).ToList().First().ToString();

                        insertfans_90(first_90, 1, mentions, retweets, image_url);
                       
                    }

                    
                    second_90 = topfans_90.OrderByDescending(r => r.Count).Skip(1).FirstOrDefault();
                    if (second_90 != null)
                    {
                        int mentions = mention_retweets_90.Where(x => x.FromId == second_90.FromId && x.MessageType == "twt_mention").ToList().Count;
                        int retweets = mention_retweets_90.Where(x => x.FromId == second_90.FromId && x.MessageType == "twt_retweet").ToList().Count;
                        string image_url = mention_retweets_90.Where(a => a.FromId == second_90.FromId).Select(x => x.FromImageUrl).ToList().First().ToString();

                        insertfans_90(second_90, 2, mentions, retweets, image_url);
                    }

                    third_90 = topfans_90.OrderByDescending(r => r.Count).Skip(2).FirstOrDefault();
                    if(third_90 != null)
                    {
                        int mentions = mention_retweets_90.Where(x => x.FromId == third_90.FromId && x.MessageType == "twt_mention").ToList().Count;
                        int retweets = mention_retweets_90.Where(x => x.FromId == third_90.FromId && x.MessageType == "twt_retweet").ToList().Count;
                        string image_url = mention_retweets_90.Where(a => a.FromId == third_90.FromId).Select(x => x.FromImageUrl).ToList().First().ToString();

                        insertfans_90(third_90, 3, mentions, retweets, image_url);
                    }

                    fourth_90 = topfans_90.OrderByDescending(r => r.Count).Skip(3).FirstOrDefault();
                    if(fourth_90 != null)
                    {
                        int mentions = mention_retweets_90.Where(x => x.FromId == fourth_90.FromId && x.MessageType == "twt_mention").ToList().Count;
                        int retweets = mention_retweets_90.Where(x => x.FromId == fourth_90.FromId && x.MessageType == "twt_retweet").ToList().Count;
                        string image_url = mention_retweets_90.Where(a => a.FromId == fourth_90.FromId).Select(x => x.FromImageUrl).ToList().First().ToString();

                        insertfans_90(fourth_90, 4, mentions, retweets, image_url);
                    }
                    fifth_90= topfans_90.OrderByDescending(r => r.Count).Skip(4).FirstOrDefault();
                    if(fifth_90 !=null)
                    {
                        int mentions = mention_retweets_90.Where(x => x.FromId == fifth_90.FromId && x.MessageType == "twt_mention").ToList().Count;
                        int retweets = mention_retweets_90.Where(x => x.FromId == fifth_90.FromId && x.MessageType == "twt_retweet").ToList().Count;
                        string image_url = mention_retweets_90.Where(a => a.FromId == fifth_90.FromId).Select(x => x.FromImageUrl).ToList().First().ToString();

                        insertfans_90(fifth_90, 5, mentions, retweets, image_url);
                    }


                    deletefans_60(profileid);
                    mention_retweets_60 = session.Query<Domain.Socioboard.Domain.InboxMessages>().Where(x => x.CreatedTime <= DateTime.Now.AddDays(1).Date.AddSeconds(-1) && x.CreatedTime > (DateTime.Now.AddDays(-90).Date) && x.ProfileId == profileid && (x.MessageType == "twt_mention" || x.MessageType == "twt_retweets")).ToList();
                    dynamic list_60 = mention_retweets_60.GroupBy(n => n.FromId).Select(group => new Domain.Socioboard.Domain.TopFiveFans_60(group.Count(), group.First().FromId, profileid, group.First().FromName)).ToList();
                    topfans_60 = (List<Domain.Socioboard.Domain.TopFiveFans_60>)list_60;


                    first_60 = topfans_60.OrderByDescending(r => r.Count).FirstOrDefault();
                    if (first_60 != null)
                    {
                        int mentions = mention_retweets_60.Where(x => x.FromId == first_60.FromId && x.MessageType == "twt_mention").ToList().Count;
                        int retweets = mention_retweets_60.Where(x => x.FromId == first_60.FromId && x.MessageType == "twt_retweet").ToList().Count;
                        string image_url = mention_retweets_60.Where(a => a.FromId == first_60.FromId).Select(x => x.FromImageUrl).ToList().First().ToString();

                        insertfans_60(first_60, 1, mentions, retweets, image_url);
                    }


                    second_60 = topfans_60.OrderByDescending(r => r.Count).Skip(1).FirstOrDefault();
                    if (second_60 != null)
                    {
                        int mentions = mention_retweets_60.Where(x => x.FromId == second_60.FromId && x.MessageType == "twt_mention").ToList().Count;
                        int retweets = mention_retweets_60.Where(x => x.FromId == second_60.FromId && x.MessageType == "twt_retweet").ToList().Count;
                        string image_url = mention_retweets_60.Where(a => a.FromId == second_60.FromId).Select(x => x.FromImageUrl).ToList().First().ToString();

                        insertfans_60(second_60, 2, mentions, retweets, image_url);
                    }

                    third_60 = topfans_60.OrderByDescending(r => r.Count).Skip(2).FirstOrDefault();
                    if (third_60 != null)
                    {
                        int mentions = mention_retweets_60.Where(x => x.FromId == third_60.FromId && x.MessageType == "twt_mention").ToList().Count;
                        int retweets = mention_retweets_60.Where(x => x.FromId == third_60.FromId && x.MessageType == "twt_retweet").ToList().Count;
                        string image_url = mention_retweets_60.Where(a => a.FromId == third_60.FromId).Select(x => x.FromImageUrl).ToList().First().ToString();

                        insertfans_60(third_60, 3, mentions, retweets, image_url);
                    }

                    fourth_60 = topfans_60.OrderByDescending(r => r.Count).Skip(3).FirstOrDefault();
                    if (fourth_60 != null)
                    {
                        int mentions = mention_retweets_60.Where(x => x.FromId == fourth_60.FromId && x.MessageType == "twt_mention").ToList().Count;
                        int retweets = mention_retweets_60.Where(x => x.FromId == fourth_60.FromId && x.MessageType == "twt_retweet").ToList().Count;
                        string image_url = mention_retweets_60.Where(a => a.FromId == fourth_60.FromId).Select(x => x.FromImageUrl).ToList().First().ToString();

                        insertfans_60(fourth_60, 4, mentions, retweets, image_url);
                    }

                    fifth_60 = topfans_60.OrderByDescending(r => r.Count).Skip(4).FirstOrDefault();
                    if (fifth_60 != null)
                    {
                        int mentions = mention_retweets_60.Where(x => x.FromId == fifth_60.FromId && x.MessageType == "twt_mention").ToList().Count;
                        int retweets = mention_retweets_60.Where(x => x.FromId == fifth_60.FromId && x.MessageType == "twt_retweet").ToList().Count;
                        string image_url = mention_retweets_60.Where(a => a.FromId == fifth_60.FromId).Select(x => x.FromImageUrl).ToList().First().ToString();

                        insertfans_60(fifth_60, 5, mentions, retweets, image_url);
                    }


                    deletefans_30(profileid);
                    mention_retweets_30 = session.Query<Domain.Socioboard.Domain.InboxMessages>().Where(x => x.CreatedTime <= DateTime.Now.AddDays(1).Date.AddSeconds(-1) && x.CreatedTime > (DateTime.Now.AddDays(-90).Date) && x.ProfileId == profileid && (x.MessageType == "twt_mention" || x.MessageType == "twt_retweets")).ToList();
                    dynamic list_30 = mention_retweets_30.GroupBy(n => n.FromId).Select(group => new Domain.Socioboard.Domain.TopFiveFans_30(group.Count(), group.First().FromId, profileid, group.First().FromName)).ToList();
                    topfans_30 = (List<Domain.Socioboard.Domain.TopFiveFans_30>)list_30;


                    first_30 = topfans_30.OrderByDescending(r => r.Count).FirstOrDefault();
                    if (first_30 != null)
                    {
                        int mentions = mention_retweets_30.Where(x => x.FromId == first_30.FromId && x.MessageType == "twt_mention").ToList().Count;
                        int retweets = mention_retweets_30.Where(x => x.FromId == first_30.FromId && x.MessageType == "twt_retweet").ToList().Count;
                        string image_url = mention_retweets_30.Where(a => a.FromId == first_30.FromId).Select(x => x.FromImageUrl).ToList().First().ToString();

                        insertfans_30(first_30, 1, mentions, retweets, image_url);
                    }


                    second_30 = topfans_30.OrderByDescending(r => r.Count).Skip(1).FirstOrDefault();
                    if (second_30 != null)
                    {
                        int mentions = mention_retweets_30.Where(x => x.FromId == second_30.FromId && x.MessageType == "twt_mention").ToList().Count;
                        int retweets = mention_retweets_30.Where(x => x.FromId == second_30.FromId && x.MessageType == "twt_retweet").ToList().Count;
                        string image_url = mention_retweets_30.Where(a => a.FromId == second_30.FromId).Select(x => x.FromImageUrl).ToList().First().ToString();

                        insertfans_30(second_30, 2, mentions, retweets, image_url);
                    }

                    third_30 = topfans_30.OrderByDescending(r => r.Count).Skip(2).FirstOrDefault();
                    if (third_30 != null)
                    {
                        int mentions = mention_retweets_30.Where(x => x.FromId == third_30.FromId && x.MessageType == "twt_mention").ToList().Count;
                        int retweets = mention_retweets_30.Where(x => x.FromId == third_30.FromId && x.MessageType == "twt_retweet").ToList().Count;
                        string image_url = mention_retweets_30.Where(a => a.FromId == third_30.FromId).Select(x => x.FromImageUrl).ToList().First().ToString();

                        insertfans_30(third_30, 3, mentions, retweets, image_url);
                    }

                    fourth_30 = topfans_30.OrderByDescending(r => r.Count).Skip(3).FirstOrDefault();
                    if (fourth_30 != null)
                    {
                        int mentions = mention_retweets_30.Where(x => x.FromId == fourth_30.FromId && x.MessageType == "twt_mention").ToList().Count;
                        int retweets = mention_retweets_30.Where(x => x.FromId == fourth_30.FromId && x.MessageType == "twt_retweet").ToList().Count;
                        string image_url = mention_retweets_30.Where(a => a.FromId == fourth_30.FromId).Select(x => x.FromImageUrl).ToList().First().ToString();

                        insertfans_30(fourth_30, 4, mentions, retweets, image_url);
                    }

                    fifth_30 = topfans_30.OrderByDescending(r => r.Count).Skip(4).FirstOrDefault();
                    if (fifth_30 != null)
                    {
                        int mentions = mention_retweets_30.Where(x => x.FromId == fifth_30.FromId && x.MessageType == "twt_mention").ToList().Count;
                        int retweets = mention_retweets_30.Where(x => x.FromId == fifth_30.FromId && x.MessageType == "twt_retweet").ToList().Count;
                        string image_url = mention_retweets_30.Where(a => a.FromId == fifth_30.FromId).Select(x => x.FromImageUrl).ToList().First().ToString();

                        insertfans_30(fifth_30, 5, mentions, retweets, image_url);
                    }


                    deletefans_15(profileid);
                    mention_retweets_15 = session.Query<Domain.Socioboard.Domain.InboxMessages>().Where(x => x.CreatedTime <= DateTime.Now.AddDays(1).Date.AddSeconds(-1) && x.CreatedTime > (DateTime.Now.AddDays(-90).Date) && x.ProfileId == profileid && (x.MessageType == "twt_mention" || x.MessageType == "twt_retweets")).ToList();
                    dynamic list_15 = mention_retweets_15.GroupBy(n => n.FromId).Select(group => new Domain.Socioboard.Domain.TopFiveFans_15(group.Count(), group.First().FromId, profileid, group.First().FromName)).ToList();
                    topfans_15 = (List<Domain.Socioboard.Domain.TopFiveFans_15>)list_15;


                    first_15 = topfans_15.OrderByDescending(r => r.Count).FirstOrDefault();
                    if (first_15 != null)
                    {
                        int mentions = mention_retweets_15.Where(x => x.FromId == first_15.FromId && x.MessageType == "twt_mention").ToList().Count;
                        int retweets = mention_retweets_15.Where(x => x.FromId == first_15.FromId && x.MessageType == "twt_retweet").ToList().Count;
                        string image_url = mention_retweets_15.Where(a => a.FromId == first_15.FromId).Select(x => x.FromImageUrl).ToList().First().ToString();

                        insertfans_15(first_15, 1, mentions, retweets, image_url);
                    }


                    second_15 = topfans_15.OrderByDescending(r => r.Count).Skip(1).FirstOrDefault();
                    if (second_15 != null)
                    {
                        int mentions = mention_retweets_15.Where(x => x.FromId == second_15.FromId && x.MessageType == "twt_mention").ToList().Count;
                        int retweets = mention_retweets_15.Where(x => x.FromId == second_15.FromId && x.MessageType == "twt_retweet").ToList().Count;
                        string image_url = mention_retweets_15.Where(a => a.FromId == second_15.FromId).Select(x => x.FromImageUrl).ToList().First().ToString();

                        insertfans_15(second_15, 2, mentions, retweets, image_url);
                    }

                    third_15 = topfans_15.OrderByDescending(r => r.Count).Skip(2).FirstOrDefault();
                    if (third_15 != null)
                    {
                        int mentions = mention_retweets_15.Where(x => x.FromId == third_15.FromId && x.MessageType == "twt_mention").ToList().Count;
                        int retweets = mention_retweets_15.Where(x => x.FromId == third_15.FromId && x.MessageType == "twt_retweet").ToList().Count;
                        string image_url = mention_retweets_15.Where(a => a.FromId == third_15.FromId).Select(x => x.FromImageUrl).ToList().First().ToString();

                        insertfans_15(third_15, 3, mentions, retweets, image_url);
                    }

                    fourth_15 = topfans_15.OrderByDescending(r => r.Count).Skip(3).FirstOrDefault();
                    if (fourth_15 != null)
                    {
                        int mentions = mention_retweets_15.Where(x => x.FromId == fourth_15.FromId && x.MessageType == "twt_mention").ToList().Count;
                        int retweets = mention_retweets_15.Where(x => x.FromId == fourth_15.FromId && x.MessageType == "twt_retweet").ToList().Count;
                        string image_url = mention_retweets_15.Where(a => a.FromId == fourth_15.FromId).Select(x => x.FromImageUrl).ToList().First().ToString();

                        insertfans_15(fourth_15, 4, mentions, retweets, image_url);
                    }
                    fifth_15 = topfans_15.OrderByDescending(r => r.Count).Skip(4).FirstOrDefault();
                    if (fifth_15 != null)
                    {
                        int mentions = mention_retweets_15.Where(x => x.FromId == fifth_15.FromId && x.MessageType == "twt_mention").ToList().Count;
                        int retweets = mention_retweets_15.Where(x => x.FromId == fifth_15.FromId && x.MessageType == "twt_retweet").ToList().Count;
                        string image_url = mention_retweets_15.Where(a => a.FromId == fifth_15.FromId).Select(x => x.FromImageUrl).ToList().First().ToString();

                        insertfans_15(fifth_15, 5, mentions, retweets, image_url);
                    }




                }

            }
            catch { 
            }
        }

        public void deletefans_90(string profileid)
        {
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {

                    try
                    {
                        NHibernate.IQuery query = session.CreateQuery("delete from TopFiveFans_90 where ProfileId = : profileid")
                                                            .SetParameter("profileid", profileid)
                                                            ;
                        int isUpdated = query.ExecuteUpdate();
                        transaction.Commit();

                    }
                    catch (Exception)
                    {
                                             
                    }
                }               
            }
           

        }



        public void deletefans_60(string profileid)
        {
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {

                    try
                    {
                        NHibernate.IQuery query = session.CreateQuery("delete from TopFiveFans_60 where ProfileId = : profileid")
                                                            .SetParameter("profileid", profileid)
                                                            ;
                        int isUpdated = query.ExecuteUpdate();
                        transaction.Commit();

                    }
                    catch (Exception)
                    {

                        throw;
                    }
                }
            }


        }


        public void deletefans_30(string profileid)
        {
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {

                    try
                    {
                        NHibernate.IQuery query = session.CreateQuery("delete from TopFiveFans_30 where ProfileId = : profileid")
                                                            .SetParameter("profileid", profileid)
                                                            ;
                        int isUpdated = query.ExecuteUpdate();
                        transaction.Commit();

                    }
                    catch (Exception)
                    {

                        throw;
                    }
                }
            }


        }


        public void deletefans_15(string profileid)
        {
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {

                    try
                    {
                        NHibernate.IQuery query = session.CreateQuery("delete from TopFiveFans_15 where ProfileId = : profileid")
                                                            .SetParameter("profileid", profileid)
                                                            ;
                        int isUpdated = query.ExecuteUpdate();
                        transaction.Commit();

                    }
                    catch (Exception)
                    {

                        throw;
                    }
                }
            }


        }


        public void insertfans_90(Domain.Socioboard.Domain.TopFiveFans_90 data , int rank , int mention , int retweet , string img_url)
        {
            data.Rank = rank;
            data.Mentioncount = mention;
            data.Retweetcount = retweet;
            data.FromImageUrl = img_url;
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {

                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {

                    try
                    {

                      
                            session.Save(data);
                            transaction.Commit();
                       

                    }
                    catch (Exception)
                    {

                    }
                }
            }


        }





        public void insertfans_60(Domain.Socioboard.Domain.TopFiveFans_60 data, int rank, int mention, int retweet, string img_url)
        {
            data.Rank = rank;
            data.Mentioncount = mention;
            data.Retweetcount = retweet;
            data.FromImageUrl = img_url;
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {

                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {

                    try
                    {   
                       
                            session.Save(data);
                            transaction.Commit();
                       
                    }
                    catch (Exception)
                    {

                    }
                }
            }


        }


        public void insertfans_30(Domain.Socioboard.Domain.TopFiveFans_30 data, int rank, int mention, int retweet, string img_url)
        {
            data.Rank = rank;
            data.Mentioncount = mention;
            data.Retweetcount = retweet;
            data.FromImageUrl = img_url;
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {

                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {

                    try
                    {

                        
                      
                            session.Save(data);
                            transaction.Commit();
                       
                    }
                    catch (Exception)
                    {

                    }
                }
            }


        }

        public void insertfans_15(Domain.Socioboard.Domain.TopFiveFans_15 data, int rank, int mention, int retweet, string img_url)
        {
            data.Rank = rank;
            data.Mentioncount = mention;
            data.Retweetcount = retweet;
            data.FromImageUrl = img_url;

            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {

                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {

                    try
                    {

                      
                    
                            session.Save(data);
                            transaction.Commit();

                    }
                    catch (Exception)
                    {

                    }
                }
            }


        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string retrievefan_15(string id)
        {
            string ret_string = string.Empty;
            Domain.Socioboard.Domain.TopFiveFans_15 ret = new Domain.Socioboard.Domain.TopFiveFans_15();
            List<Domain.Socioboard.Domain.TopFiveFans_15> ret_list = new List<Domain.Socioboard.Domain.TopFiveFans_15>();

            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {

                try
                {

                    ret_list = session.CreateQuery("from TopFiveFans_15 where ProfileId =: id").SetParameter("id", id).List<Domain.Socioboard.Domain.TopFiveFans_15>().ToList().OrderByDescending(x => x.Rank).ToList();
                    if (ret_list == null)
                    {
                        ret_list = new List<Domain.Socioboard.Domain.TopFiveFans_15>();
                    }
                    ret_string = new JavaScriptSerializer().Serialize(ret_list);

                }
                catch (Exception e)
                {
                    logger.Error("noretrievedata>>");
                    Console.WriteLine(e.StackTrace);
                    logger.Error(e.Message);
                    logger.Error(e.StackTrace);

                    ret_list = new List<Domain.Socioboard.Domain.TopFiveFans_15>();
                    ret_string = new JavaScriptSerializer().Serialize(ret_list);
                }

            }
            return ret_string;
        }


        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string retrievefan_30(string id)
        {
            string ret_string = string.Empty;
            Domain.Socioboard.Domain.TopFiveFans_30 ret = new Domain.Socioboard.Domain.TopFiveFans_30();
            List<Domain.Socioboard.Domain.TopFiveFans_30> ret_list = new List<Domain.Socioboard.Domain.TopFiveFans_30>();

            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {

                try
                {

                    ret_list = session.CreateQuery("from TopFiveFans_30 where ProfileId =: id").SetParameter("id", id).List<Domain.Socioboard.Domain.TopFiveFans_30>().ToList().OrderByDescending(x => x.Rank).ToList();
                    if (ret_list == null)
                    {
                        ret_list = new List<Domain.Socioboard.Domain.TopFiveFans_30>();
                    }
                    ret_string = new JavaScriptSerializer().Serialize(ret_list);

                }
                catch (Exception e)
                {
                    logger.Error("noretrievedata>>");
                    Console.WriteLine(e.StackTrace);
                    logger.Error(e.Message);
                    logger.Error(e.StackTrace);

                    ret_list = new List<Domain.Socioboard.Domain.TopFiveFans_30>();
                    ret_string = new JavaScriptSerializer().Serialize(ret_list);
                }

            }
            return ret_string;
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string retrievefan_60(string id)
        {
            string ret_string = string.Empty;
            Domain.Socioboard.Domain.TopFiveFans_60 ret = new Domain.Socioboard.Domain.TopFiveFans_60();
            List<Domain.Socioboard.Domain.TopFiveFans_60> ret_list = new List<Domain.Socioboard.Domain.TopFiveFans_60>();

            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {

                try
                {

                    ret_list = session.CreateQuery("from TopFiveFans_60 where ProfileId =: id").SetParameter("id", id).List<Domain.Socioboard.Domain.TopFiveFans_60>().ToList().OrderByDescending(x => x.Rank).ToList();
                    if (ret_list == null)
                    {
                        ret_list = new List<Domain.Socioboard.Domain.TopFiveFans_60>();
                    }
                    ret_string = new JavaScriptSerializer().Serialize(ret_list);

                }
                catch (Exception e)
                {
                    logger.Error("noretrievedata>>");
                    Console.WriteLine(e.StackTrace);
                    logger.Error(e.Message);
                    logger.Error(e.StackTrace);

                    ret_list = new List<Domain.Socioboard.Domain.TopFiveFans_60>();
                    ret_string = new JavaScriptSerializer().Serialize(ret_list);
                }

            }
            return ret_string;
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string retrievefan_90(string id)
        {
            string ret_string = string.Empty;
            Domain.Socioboard.Domain.TopFiveFans_90 ret = new Domain.Socioboard.Domain.TopFiveFans_90();
            List<Domain.Socioboard.Domain.TopFiveFans_90> ret_list = new List<Domain.Socioboard.Domain.TopFiveFans_90>();

            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {

                try
                {

                    ret_list = session.CreateQuery("from TopFiveFans_90 where ProfileId =: id").SetParameter("id", id).List<Domain.Socioboard.Domain.TopFiveFans_90>().ToList().OrderBy(x => x.Rank).ToList();
                    if (ret_list == null)
                    {
                        ret_list = new List<Domain.Socioboard.Domain.TopFiveFans_90>();
                    }
                    ret_string = new JavaScriptSerializer().Serialize(ret_list);

                }
                catch (Exception e)
                {
                    logger.Error("noretrievedata>>");
                    Console.WriteLine(e.StackTrace);
                    logger.Error(e.Message);
                    logger.Error(e.StackTrace);

                    ret_list = new List<Domain.Socioboard.Domain.TopFiveFans_90>();
                    ret_string = new JavaScriptSerializer().Serialize(ret_list);
                }

            }
            return ret_string;
        }





        [WebMethod]
        public void insertdata(string i)
        {

            Domain.Socioboard.Domain.TwitterReport insert = (Domain.Socioboard.Domain.TwitterReport)new JavaScriptSerializer().Deserialize(i, typeof(Domain.Socioboard.Domain.TwitterReport));

            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                try
                {
                    bool exist = false;
                    //After Session creation, start Transaction. 
                    using (NHibernate.ITransaction transaction = session.BeginTransaction())
                    {
                        //Proceed action, to save data.
                        try
                        {
                            exist = session.Query<Domain.Socioboard.Domain.TwitterReport>()
                                           .Any(x => x.twitterprofileid == insert.twitterprofileid);
                        }
                        catch (Exception ex)
                        {
                            exist = false;
                        }
                        if (exist)
                        {
                            int _i = session.CreateQuery("Update TwitterReport set twitterprofileid = : twitterprofileid , totalfollower = : totalfollower , totalconnection = : totalconnection , newfollower_15 = : newfollower_15 , perday_newfollower_15 = : perday_newfollower_15 , newfollower_30 = : newfollower_30 , perday_newfollower_30 = : perday_newfollower_30 , newfollower_60 = : newfollower_60 , perday_newfollower_60 = : perday_newfollower_60 , newfollower_90 = : newfollower_90 , perday_newfollower_90 = : perday_newfollower_90 , following_15 = : following_15 , perday_following_15 = : perday_following_15 , following_30 = : following_30 , perday_following_30 = : perday_following_30 , following_60 = : following_60 , perday_following_60 = : perday_following_60 , following_90 = : following_90 , perday_following_90 = : perday_following_90 , directmessage_15 = : directmessage_15 , perday_directmessage_15 = : perday_directmessage_15 , directmessage_30 = : directmessage_30 , perday_directmessage_30 = : perday_directmessage_30 , directmessage_60 = : directmessage_60 , perday_directmessage_60 = : perday_directmessage_60 , directmessage_90 = : directmessage_90 , perday_directmessage_90 = : perday_directmessage_90 , mention_15 = : mention_15 , perday_mention_15 = : perday_mention_15 , mention_30 = : mention_30 , perday_mention_30 = : perday_mention_30 , mention_60 = : mention_60 , perday_mention_60 = : perday_mention_60 , mention_90 = : mention_90 , perday_mention_90 = : perday_mention_90 , messagesent_15 = : messagesent_15 , perday_messagesent_15 = : perday_messagesent_15 , messagesent_30 = : messagesent_30 , perday_messagesent_30 = : perday_messagesent_30 , messagesent_60 = : messagesent_60 , perday_messagesent_60 = : perday_messagesent_60 , messagesent_90 = : messagesent_90 , perday_messagesent_90 = : perday_messagesent_90 , messagerecieved_15 = : messagerecieved_15 , perday_messagerecieved_15 = : perday_messagerecieved_15 , messagerecieved_30 = : messagerecieved_30 , perday_messagerecieved_30 = : perday_messagerecieved_30 , messagerecieved_60 = : messagerecieved_60 , perday_messagerecieved_60 = : perday_messagerecieved_60 , messagerecieved_90 = : messagerecieved_90 , perday_messagerecieved_90 = : perday_messagerecieved_90 , click_15 = : click_15 , perday_click_15 = : perday_click_15 , click_30 = : click_30 , perday_click_30 = : perday_click_30 , click_60 = : click_60 , perday_click_60 = : perday_click_60 , click_90 = : click_90 , perday_click_90 = : perday_click_90 , retweets_15 = : retweets_15 , perday_retweets_15 = : perday_retweets_15 , retweets_30 = : retweets_30 , perday_retweets_30 = : perday_retweets_30 , retweets_60 = : retweets_60 , perday_retweets_60 = : perday_retweets_60 , retweets_90 = : retweets_90 , perday_retweets_90 = : perday_retweets_90 , directmessagesent_15 = :  directmessagesent_15 , perday_directmessagesent_15 = : perday_directmessagesent_15 , directmessagesent_30 = :  directmessagesent_30 , perday_directmessagesent_30 = : perday_directmessagesent_30 , directmessagesent_60 = :  directmessagesent_60 , perday_directmessagesent_60 = : perday_directmessagesent_60 , directmessagesent_90 = :  directmessagesent_90 , perday_directmessagesent_90 = : perday_directmessagesent_90 , sexratio = : sexratio where twitterprofileid = : twitterprofileid")
                                .SetParameter("twitterprofileid", insert.twitterprofileid)
                                .SetParameter("totalfollower", insert.totalfollower)

                                .SetParameter("totalconnection", insert.totalconnection)

                                .SetParameter("newfollower_15", insert.newfollower_15)
                                .SetParameter("perday_newfollower_15", insert.perday_newfollower_15)

                                 .SetParameter("newfollower_30", insert.newfollower_30)
                                .SetParameter("perday_newfollower_30", insert.perday_newfollower_30)

                                 .SetParameter("newfollower_60", insert.newfollower_60)
                                .SetParameter("perday_newfollower_60", insert.perday_newfollower_60)

                                 .SetParameter("newfollower_90", insert.newfollower_90)
                                .SetParameter("perday_newfollower_90", insert.perday_newfollower_90)

                                 .SetParameter("following_15", insert.following_15)
                                .SetParameter("perday_following_15", insert.perday_following_15)

                                 .SetParameter("following_30", insert.following_30)
                                .SetParameter("perday_following_30", insert.perday_following_30)

                                 .SetParameter("following_60", insert.following_60)
                                .SetParameter("perday_following_60", insert.perday_following_60)

                                 .SetParameter("following_90", insert.following_90)
                                .SetParameter("perday_following_90", insert.perday_following_90)

                                  .SetParameter("directmessage_15", insert.directmessage_15)
                                .SetParameter("perday_directmessage_15", insert.perday_directmessage_15)

                                       .SetParameter("directmessage_30", insert.directmessage_30)
                                .SetParameter("perday_directmessage_30", insert.perday_directmessage_30)

                                       .SetParameter("directmessage_60", insert.directmessage_60)
                                .SetParameter("perday_directmessage_60", insert.perday_directmessage_60)

                                       .SetParameter("directmessage_90", insert.directmessage_90)
                                .SetParameter("perday_directmessage_90", insert.perday_directmessage_90)

                                         .SetParameter("mention_15", insert.mention_15)
                                .SetParameter("perday_mention_15", insert.perday_mention_15)

                                           .SetParameter("mention_30", insert.mention_30)
                                .SetParameter("perday_mention_30", insert.perday_mention_30)

                                           .SetParameter("mention_60", insert.mention_60)
                                .SetParameter("perday_mention_60", insert.perday_mention_60)

                                           .SetParameter("mention_90", insert.mention_90)
                                .SetParameter("perday_mention_90", insert.perday_mention_90)

                                          .SetParameter("messagesent_15", insert.messagesent_15)
                                .SetParameter("perday_messagesent_15", insert.perday_messagesent_15)

                                           .SetParameter("messagesent_30", insert.messagesent_30)
                                .SetParameter("perday_messagesent_30", insert.perday_messagesent_30)

                                           .SetParameter("messagesent_60", insert.messagesent_60)
                                .SetParameter("perday_messagesent_60", insert.perday_messagesent_60)

                                           .SetParameter("messagesent_90", insert.messagesent_90)
                                .SetParameter("perday_messagesent_90", insert.perday_messagesent_90)


                                           .SetParameter("messagerecieved_15", insert.messagerecieved_15)
                                .SetParameter("perday_messagerecieved_15", insert.perday_messagerecieved_15)

                                    .SetParameter("messagerecieved_30", insert.messagerecieved_30)
                                .SetParameter("perday_messagerecieved_30", insert.perday_messagerecieved_30)

                            .SetParameter("messagerecieved_60", insert.messagerecieved_60)
                                .SetParameter("perday_messagerecieved_60", insert.perday_messagerecieved_60)

                                    .SetParameter("messagerecieved_90", insert.messagerecieved_90)
                                .SetParameter("perday_messagerecieved_90", insert.perday_messagerecieved_90)

                                  .SetParameter("click_15", insert.click_15)
                                .SetParameter("perday_click_15", insert.perday_click_15)

                                   .SetParameter("click_30", insert.click_30)
                                .SetParameter("perday_click_30", insert.perday_click_30)

                                   .SetParameter("click_60", insert.click_60)
                                .SetParameter("perday_click_60", insert.perday_click_60)

                                   .SetParameter("click_90", insert.click_90)
                                .SetParameter("perday_click_90", insert.perday_click_90)

                                 .SetParameter("retweets_15", insert.retweets_15)
                                .SetParameter("perday_retweets_15", insert.perday_retweets_15)

                                  .SetParameter("retweets_30", insert.retweets_30)
                                .SetParameter("perday_retweets_30", insert.perday_retweets_30)

                                  .SetParameter("retweets_60", insert.retweets_60)
                                .SetParameter("perday_retweets_60", insert.perday_retweets_60)

                                  .SetParameter("retweets_90", insert.retweets_90)
                                .SetParameter("perday_retweets_90", insert.perday_retweets_90)

                                   .SetParameter("directmessagesent_15", insert.directmessagesent_15)
                                .SetParameter("perday_directmessagesent_15", insert.perday_directmessagesent_15)

                                           .SetParameter("directmessagesent_30", insert.directmessagesent_30)
                                .SetParameter("perday_directmessagesent_30", insert.perday_directmessagesent_30)

                                           .SetParameter("directmessagesent_60", insert.directmessagesent_60)
                                .SetParameter("perday_directmessagesent_60", insert.perday_directmessagesent_60)

                                           .SetParameter("directmessagesent_90", insert.directmessagesent_90)
                                .SetParameter("perday_directmessagesent_90", insert.perday_directmessagesent_90)
                                .SetParameter("sexratio", insert.sexratio)
                                .SetParameter("twitterprofileid", insert.twitterprofileid)

                            .ExecuteUpdate();
                            transaction.Commit();
                            logger.Error("inserteddata>>");


                        }
                        else
                        {
                            session.Save(insert);
                            transaction.Commit();
                        }
                    }// End Using Trasaction
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.StackTrace);
                    logger.Error("notinserteddata>>");
                    logger.Error(e.Message);
                    logger.Error(e.StackTrace);


                }
            }// End using session

        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string retrievedata(string id)
        {
            string ret_string = string.Empty;
            Domain.Socioboard.Domain.TwitterReport ret = new Domain.Socioboard.Domain.TwitterReport();
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {

                try
                {

                    NHibernate.IQuery retrieve = session.CreateQuery("from TwitterReport where twitterprofileid =: grpid").SetParameter("grpid", id);
                    ret = (Domain.Socioboard.Domain.TwitterReport)retrieve.UniqueResult();
                    if (ret == null)
                    {
                        ret = new Domain.Socioboard.Domain.TwitterReport();
                    }
                    ret_string = new JavaScriptSerializer().Serialize(ret);

                }
                catch (Exception e)
                {
                    logger.Error("noretrievedata>>");
                    Console.WriteLine(e.StackTrace);
                    logger.Error(e.Message);
                    logger.Error(e.StackTrace);
                    ret = new Domain.Socioboard.Domain.TwitterReport();
                    ret_string = new JavaScriptSerializer().Serialize(ret);
                }

            }
            return ret_string;
        }
        
        [WebMethod]
        public string gettwittersexdivision(string profileid)
        {
            Domain.Socioboard.Domain.ReturnData r = new Domain.Socioboard.Domain.ReturnData();
            DateTime present_date = DateTime.Now;
            string perday_15 = string.Empty;
            string perday_30 = string.Empty;
            string perday_60 = string.Empty;
            string perday_90 = string.Empty;
            string firstname = string.Empty;
            string AllProfileId = string.Empty;
            string FbProfileId = string.Empty;
            string TwtProfileId = string.Empty;
            string[] ArrProfileId = { };
            List<Domain.Socioboard.Domain.Team> teams;
            List<Domain.Socioboard.Domain.TeamMemberProfile> teammemberprofiles;
            List<Domain.Socioboard.Domain.TwitterFollowerNames> twtfollowernames;
            long total = 0;
            long male_count = 0;
            long female_count = 0;

            string ret_string = string.Empty;
            try
            {

                string strtwtfollowernames = "from TwitterFollowerNames t where t.TwitterProfileId = : profileid)";

                using (NHibernate.ISession session3 = SessionFactory.GetNewSession())
                {

                    twtfollowernames = session3.CreateQuery(strtwtfollowernames).SetParameter("profileid", profileid).List<Domain.Socioboard.Domain.TwitterFollowerNames>().ToList();

                }

                total += twtfollowernames.Count;

                if (total > 0)
                {
                    foreach (Domain.Socioboard.Domain.TwitterFollowerNames twtfollowername in twtfollowernames)
                    {
                        if (twtfollowername.Name.Contains(" "))
                        {
                            firstname = twtfollowername.Name.Split(' ')[0];
                        }
                        else
                        {
                            firstname = twtfollowername.Name;
                        }
                        string namecheck = "from TwitterNameTable t where t.Name =: name ";
                        using (NHibernate.ISession session4 = SessionFactory.GetNewSession())
                        {

                            NHibernate.IQuery querry = session4.CreateQuery(namecheck).SetParameter("name", firstname);


                            Domain.Socioboard.Domain.TwitterNameTable _TwitterNameTable = (Domain.Socioboard.Domain.TwitterNameTable)querry.UniqueResult();
                            if (_TwitterNameTable != null)
                            {
                                if (_TwitterNameTable.Gender == 1)
                                {
                                    male_count++;
                                }

                            }
                            else
                            {

                                int length = (firstname.Length) / 2;
                                string sub_name = firstname.Substring(0, length);



                                using (NHibernate.ISession session5 = SessionFactory.GetNewSession())
                                {

                                    List<Domain.Socioboard.Domain.TwitterNameTable> root_names = session5.Query<Domain.Socioboard.Domain.TwitterNameTable>().Where(x => x.Name.Contains(sub_name)).ToList<Domain.Socioboard.Domain.TwitterNameTable>();
                                    int ret = cosine_similarity(root_names, firstname);
                                    if (ret == 1)
                                    {
                                        male_count++;
                                    }
                                }

                            }





                        }
                    }


                }
                else
                {
                    ret_string = "0,0";
                }
            }




            catch (Exception e)
            {


                logger.Error(e.Message);
                logger.Error(e.StackTrace);
            }
            if (ret_string.Equals("0,0"))
            {

            }
            else
            {
                female_count = total - male_count;
                ret_string = male_count.ToString() + "," + female_count.ToString();
            }
            return ret_string;
        }
        
        public int cosine_similarity(List<Domain.Socioboard.Domain.TwitterNameTable> names, string nametomatch)
        {

            nametomatch = nametomatch.ToLower();
            Dictionary<char, int> alphabets = new Dictionary<char, int>();
            alphabets.Add('a', 0);
            alphabets.Add('b', 1);
            alphabets.Add('c', 2);
            alphabets.Add('d', 3);
            alphabets.Add('e', 4);
            alphabets.Add('f', 5);
            alphabets.Add('g', 6);
            alphabets.Add('h', 7);
            alphabets.Add('i', 8);
            alphabets.Add('j', 9);
            alphabets.Add('k', 10);
            alphabets.Add('l', 11);
            alphabets.Add('m', 12);
            alphabets.Add('n', 13);
            alphabets.Add('o', 14);
            alphabets.Add('p', 15);
            alphabets.Add('q', 16);
            alphabets.Add('r', 17);
            alphabets.Add('s', 18);
            alphabets.Add('t', 19);
            alphabets.Add('u', 20);
            alphabets.Add('v', 21);
            alphabets.Add('w', 22);
            alphabets.Add('x', 23);
            alphabets.Add('y', 24);
            alphabets.Add('z', 25);
            int ret_gender = 1;
            double similarity = 0.0;
            double max_similarity = 0.0;
            int prod = 0;
            int sq_a = 0;
            int sq_b = 0;
            double srt_a;
            double srt_b;
            int pos;
            int[] a_array = new int[26];
            int[] b_array = new int[26];
            List<double> ar = new List<double>();

            foreach (Domain.Socioboard.Domain.TwitterNameTable name in names)
            {
                try
                {
                    name.Name = name.Name.ToLower();
                    name.Name = name.Name.Split(' ')[0];
                    for (int i = 0; i < 26; i++)
                    {
                        a_array[i] = 0;
                        b_array[i] = 0;
                    }

                    foreach (char c in nametomatch)
                    {
                        pos = alphabets[c];
                        a_array[pos]++;
                    }

                    foreach (char c in name.Name)
                    {
                        pos = alphabets[c];
                        b_array[pos]++;
                    }

                    for (int i = 0; i < 26; i++)
                    {

                        prod += a_array[i] * b_array[i];

                    }


                    for (int i = 0; i < 26; i++)
                    {
                        sq_a += a_array[i] * a_array[i];
                        sq_b += b_array[i] * b_array[i];
                    }

                    srt_a = Math.Sqrt(sq_a);
                    srt_b = Math.Sqrt(sq_b);

                    similarity = (prod * 100) / (srt_a * srt_b);

                    ar.Add(similarity);
                }
                catch (Exception ex)
                {
                }

            }

            int lstcount = ar.Count;

            for (int i = 0; i < lstcount; i++)
            {

                if (ar[i] > max_similarity)
                {
                    max_similarity = ar[i];
                }


            }

            for (int i = 0; i < lstcount; i++)
            {

                if (ar[i] == max_similarity)
                {

                    ret_gender = names[i].Gender;
                }

            }

            return ret_gender;
        }
               
     

    }
    
}




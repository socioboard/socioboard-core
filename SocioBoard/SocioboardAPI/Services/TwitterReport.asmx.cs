using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using Api.Socioboard.Helper;
using Domain.Socioboard.Reports;
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
                    twitterfollowers_15 = session.CreateQuery(strtwitterfollowers)
                                                          .SetParameter("ProfileId", profileid)
                                                          .List<Domain.Socioboard.Domain.TwitterAccountFollowers>().ToList().Where(x => x.EntryDate <= DateTime.Now && x.EntryDate >= (DateTime.Now.AddDays(-15))).ToList();

                    twitterfollowers_30 = session.CreateQuery(strtwitterfollowers)
                                                          .SetParameter("ProfileId", profileid)
                                                          .List<Domain.Socioboard.Domain.TwitterAccountFollowers>().ToList().Where(x => x.EntryDate <= DateTime.Now && x.EntryDate >= (DateTime.Now.AddDays(-30))).ToList();
                    twitterfollowers_60 = session.CreateQuery(strtwitterfollowers)
                                                           .SetParameter("ProfileId", profileid)
                                                           .List<Domain.Socioboard.Domain.TwitterAccountFollowers>().ToList().Where(x => x.EntryDate <= DateTime.Now && x.EntryDate >= (DateTime.Now.AddDays(-60))).ToList();
                    twitterfollowers_90 = session.CreateQuery(strtwitterfollowers)
                                                           .SetParameter("ProfileId", profileid)
                                                           .List<Domain.Socioboard.Domain.TwitterAccountFollowers>().ToList().Where(x => x.EntryDate <= DateTime.Now && x.EntryDate >= (DateTime.Now.AddDays(-90))).ToList();

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
                        List<Domain.Socioboard.Domain.TwitterAccountFollowers> lstresult = session4.Query<Domain.Socioboard.Domain.TwitterAccountFollowers>().Where(t => t.ProfileId == profileid && t.EntryDate >= present_date.Date.AddSeconds(1) && t.EntryDate <= present_date.Date.AddDays(1).AddSeconds(-1)).OrderByDescending(t => t.EntryDate).ToList();

                        if (lstresult.Count > 0)
                        {
                            Domain.Socioboard.Domain.TwitterAccountFollowers _result = lstresult.First();


                            string add_data = _result.FollowersCount.ToString();


                            perday_90 = perday_90 + add_data + ",";
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




            try
            {
                perday_15 = perday_90.Substring(0, 29);
                perday_30 = perday_90.Substring(0, 59);
                perday_60 = perday_90.Substring(0, 119);
            }
            catch (Exception e)
            {
                logger.Error(e.Message);
            }


            if (twitterfollowers_15.Count > 0)
            {
                _15_first = twitterfollowers_15.First();
                _15_last = twitterfollowers_15.Last();
                r._15 = _15_first.FollowersCount - _15_last.FollowersCount;
            }
            else
            {
                r._15 = 0;
            }

            if (twitterfollowers_30.Count > 0)
            {
                _30_first = twitterfollowers_30.First();
                _30_last = twitterfollowers_30.Last();
                r._30 = _30_first.FollowersCount - _30_last.FollowersCount;
            }
            else
            {
                r._30 = 0;
            }

            if (twitterfollowers_60.Count > 0)
            {
                _60_first = twitterfollowers_60.First();
                _60_last = twitterfollowers_60.Last();
                r._60 = _60_first.FollowersCount - _60_last.FollowersCount;
            }
            else
            {
                r._60 = 0;
            }

            if (twitterfollowers_90.Count > 0)
            {
                _90_first = twitterfollowers_90.First();
                _90_last = twitterfollowers_90.Last();
                r._90 = _90_first.FollowersCount - _90_last.FollowersCount;
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
                    twitterfollowers_15 = session.CreateQuery(strtwitterfollowers)
                                                          .SetParameter("ProfileId", profileid)
                                                          .List<Domain.Socioboard.Domain.TwitterAccountFollowers>().ToList().Where(x => x.EntryDate <= DateTime.Now && x.EntryDate >= (DateTime.Now.AddDays(-15))).ToList();

                    twitterfollowers_30 = session.CreateQuery(strtwitterfollowers)
                                                          .SetParameter("ProfileId", profileid)
                                                          .List<Domain.Socioboard.Domain.TwitterAccountFollowers>().ToList().Where(x => x.EntryDate <= DateTime.Now && x.EntryDate >= (DateTime.Now.AddDays(-30))).ToList();
                    twitterfollowers_60 = session.CreateQuery(strtwitterfollowers)
                                                           .SetParameter("ProfileId", profileid)
                                                           .List<Domain.Socioboard.Domain.TwitterAccountFollowers>().ToList().Where(x => x.EntryDate <= DateTime.Now && x.EntryDate >= (DateTime.Now.AddDays(-60))).ToList();
                    twitterfollowers_90 = session.CreateQuery(strtwitterfollowers)
                                                           .SetParameter("ProfileId", profileid)
                                                           .List<Domain.Socioboard.Domain.TwitterAccountFollowers>().ToList().Where(x => x.EntryDate <= DateTime.Now && x.EntryDate >= (DateTime.Now.AddDays(-90))).ToList();

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
                        List<Domain.Socioboard.Domain.TwitterAccountFollowers> lstresult = session4.Query<Domain.Socioboard.Domain.TwitterAccountFollowers>().Where(t => t.ProfileId == profileid && t.EntryDate >= present_date.Date.AddSeconds(1) && t.EntryDate <= present_date.Date.AddDays(1).AddSeconds(-1)).OrderByDescending(t => t.EntryDate).ToList();

                        if (lstresult.Count > 0)
                        {
                            Domain.Socioboard.Domain.TwitterAccountFollowers _result = lstresult.First();


                            string add_data = _result.FollowingsCount.ToString();


                            perday_90 = perday_90 + add_data + ",";
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




            try
            {
                perday_15 = perday_90.Substring(0, 29);
                perday_30 = perday_90.Substring(0, 59);
                perday_60 = perday_90.Substring(0, 119);
            }
            catch (Exception e)
            {
                logger.Error(e.Message);
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
            Domain.Socioboard.Domain.TwitterDirectMessages _15_first = new Domain.Socioboard.Domain.TwitterDirectMessages();
            Domain.Socioboard.Domain.TwitterDirectMessages _30_first = new Domain.Socioboard.Domain.TwitterDirectMessages();
            Domain.Socioboard.Domain.TwitterDirectMessages _60_first = new Domain.Socioboard.Domain.TwitterDirectMessages();
            Domain.Socioboard.Domain.TwitterDirectMessages _90_first = new Domain.Socioboard.Domain.TwitterDirectMessages();

            Domain.Socioboard.Domain.TwitterDirectMessages _15_last = new Domain.Socioboard.Domain.TwitterDirectMessages();
            Domain.Socioboard.Domain.TwitterDirectMessages _30_last = new Domain.Socioboard.Domain.TwitterDirectMessages();
            Domain.Socioboard.Domain.TwitterDirectMessages _60_last = new Domain.Socioboard.Domain.TwitterDirectMessages();
            Domain.Socioboard.Domain.TwitterDirectMessages _90_last = new Domain.Socioboard.Domain.TwitterDirectMessages();


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
                    twitterdirectmessages_15 = session.CreateQuery(strtwitterfollowers)
                                                          .SetParameter("ProfileId", profileid)
                                                          .List<Domain.Socioboard.Domain.TwitterDirectMessages>().ToList().Where(x => x.EntryDate <= DateTime.Now && x.EntryDate >= (DateTime.Now.AddDays(-15))).ToList();

                    twitterdirectmessages_30 = session.CreateQuery(strtwitterfollowers)
                                                          .SetParameter("ProfileId", profileid)
                                                          .List<Domain.Socioboard.Domain.TwitterDirectMessages>().ToList().Where(x => x.EntryDate <= DateTime.Now && x.EntryDate >= (DateTime.Now.AddDays(-30))).ToList();
                    twitterdirectmessages_60 = session.CreateQuery(strtwitterfollowers)
                                                           .SetParameter("ProfileId", profileid)
                                                           .List<Domain.Socioboard.Domain.TwitterDirectMessages>().ToList().Where(x => x.EntryDate <= DateTime.Now && x.EntryDate >= (DateTime.Now.AddDays(-60))).ToList();
                    twitterdirectmessages_90 = session.CreateQuery(strtwitterfollowers)
                                                           .SetParameter("ProfileId", profileid)
                                                           .List<Domain.Socioboard.Domain.TwitterDirectMessages>().ToList().Where(x => x.EntryDate <= DateTime.Now && x.EntryDate >= (DateTime.Now.AddDays(-90))).ToList();

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


                        int item1;
                        try
                        {
                            item1 = session1.QueryOver<Domain.Socioboard.Domain.TwitterDirectMessages>().Where(m => m.CreatedDate >= present_date.Date && m.CreatedDate <= present_date.AddDays(1).Date).Where( S => S.RecipientId == profileid)
                                                         .Select(Projections.CountDistinct<Domain.Socioboard.Domain.TwitterDirectMessages>(x => x.MessageId))
                                                         .FutureValue<int>().Value;

                        }
                        catch (Exception e)
                        {
                            item1 = 0;
                        }

                        int add_data =   item1;

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




            try
            {
                perday_15 = perday_90.Substring(0, 29);
                perday_30 = perday_90.Substring(0, 59);
                perday_60 = perday_90.Substring(0, 119);
            }
            catch (Exception e)
            {
                logger.Error(e.Message);
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
            Domain.Socioboard.Domain.TwitterDirectMessages _15_first = new Domain.Socioboard.Domain.TwitterDirectMessages();
            Domain.Socioboard.Domain.TwitterDirectMessages _30_first = new Domain.Socioboard.Domain.TwitterDirectMessages();
            Domain.Socioboard.Domain.TwitterDirectMessages _60_first = new Domain.Socioboard.Domain.TwitterDirectMessages();
            Domain.Socioboard.Domain.TwitterDirectMessages _90_first = new Domain.Socioboard.Domain.TwitterDirectMessages();

            Domain.Socioboard.Domain.TwitterDirectMessages _15_last = new Domain.Socioboard.Domain.TwitterDirectMessages();
            Domain.Socioboard.Domain.TwitterDirectMessages _30_last = new Domain.Socioboard.Domain.TwitterDirectMessages();
            Domain.Socioboard.Domain.TwitterDirectMessages _60_last = new Domain.Socioboard.Domain.TwitterDirectMessages();
            Domain.Socioboard.Domain.TwitterDirectMessages _90_last = new Domain.Socioboard.Domain.TwitterDirectMessages();


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
                    twitterdirectmessages_15 = session.CreateQuery(strtwitterfollowers)
                                                          .SetParameter("ProfileId", profileid)
                                                          .List<Domain.Socioboard.Domain.TwitterDirectMessages>().ToList().Where(x => x.EntryDate <= DateTime.Now && x.EntryDate >= (DateTime.Now.AddDays(-15))).ToList();

                    twitterdirectmessages_30 = session.CreateQuery(strtwitterfollowers)
                                                          .SetParameter("ProfileId", profileid)
                                                          .List<Domain.Socioboard.Domain.TwitterDirectMessages>().ToList().Where(x => x.EntryDate <= DateTime.Now && x.EntryDate >= (DateTime.Now.AddDays(-30))).ToList();
                    twitterdirectmessages_60 = session.CreateQuery(strtwitterfollowers)
                                                           .SetParameter("ProfileId", profileid)
                                                           .List<Domain.Socioboard.Domain.TwitterDirectMessages>().ToList().Where(x => x.EntryDate <= DateTime.Now && x.EntryDate >= (DateTime.Now.AddDays(-60))).ToList();
                    twitterdirectmessages_90 = session.CreateQuery(strtwitterfollowers)
                                                           .SetParameter("ProfileId", profileid)
                                                           .List<Domain.Socioboard.Domain.TwitterDirectMessages>().ToList().Where(x => x.EntryDate <= DateTime.Now && x.EntryDate >= (DateTime.Now.AddDays(-90))).ToList();

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
                        int item1 = 0;

                        try
                        {

                            item1 = session4.QueryOver<Domain.Socioboard.Domain.TwitterDirectMessages>().Where(m => m.CreatedDate >= present_date.Date && m.CreatedDate <= present_date.AddDays(1).Date).Where(s => s.SenderId == profileid)
                                                          .Select(Projections.CountDistinct<Domain.Socioboard.Domain.TwitterDirectMessages>(x => x.MessageId))
                                                          .FutureValue<int>().Value;

                        }
                        catch (Exception e)
                        {
                            item1 = 0;
                        }

                        int add_data = item1;

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




            try
            {
                perday_15 = perday_90.Substring(0, 29);
                perday_30 = perday_90.Substring(0, 59);
                perday_60 = perday_90.Substring(0, 119);
            }
            catch (Exception e)
            {
                logger.Error(e.Message);
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
                    inboxmessages_15 = session.CreateQuery(strtwitterfollowers)
                                                          .SetParameter("ProfileId", profileid)
                                                           .SetParameter("msgtype", "twt_mention")
                                                          .List<Domain.Socioboard.Domain.InboxMessages>().ToList().Where(x => x.EntryTime <= DateTime.Now && x.EntryTime >= (DateTime.Now.AddDays(-15))).ToList();

                    inboxmessages_30 = session.CreateQuery(strtwitterfollowers)
                                                          .SetParameter("ProfileId", profileid)
                                                           .SetParameter("msgtype", "twt_mention")
                                                          .List<Domain.Socioboard.Domain.InboxMessages>().ToList().Where(x => x.EntryTime <= DateTime.Now && x.EntryTime >= (DateTime.Now.AddDays(-30))).ToList();
                    inboxmessages_60 = session.CreateQuery(strtwitterfollowers)
                                                           .SetParameter("ProfileId", profileid)
                                                            .SetParameter("msgtype", "twt_mention")
                                                           .List<Domain.Socioboard.Domain.InboxMessages>().ToList().Where(x => x.EntryTime <= DateTime.Now && x.EntryTime >= (DateTime.Now.AddDays(-60))).ToList();
                    inboxmessages_90 = session.CreateQuery(strtwitterfollowers)
                                                           .SetParameter("ProfileId", profileid)
                                                            .SetParameter("msgtype", "twt_mention")
                                                           .List<Domain.Socioboard.Domain.InboxMessages>().ToList().Where(x => x.EntryTime <= DateTime.Now && x.EntryTime >= (DateTime.Now.AddDays(-90))).ToList();

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
                            item = session4.QueryOver<Domain.Socioboard.Domain.InboxMessages>().Where(m => m.CreatedTime >= present_date.Date && m.CreatedTime <= present_date.AddDays(1).Date && m.MessageType == "twt_mention").Where(s => s.RecipientId == profileid)
                                                     .Select(Projections.CountDistinct<Domain.Socioboard.Domain.InboxMessages>(x => x.MessageId))
                                                     .FutureValue<int>().Value;
                        }
                        catch (Exception e)
                        {
                            item = 0;

                        }

                        int add_data = item;

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




            try
            {
                perday_15 = perday_90.Substring(0, 29);
                perday_30 = perday_90.Substring(0, 59);
                perday_60 = perday_90.Substring(0, 119);
            }
            catch (Exception e)
            {
                logger.Error(e.Message);
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
                    inboxmessages_15 = session.CreateQuery(strtwitterfollowers)
                                                          .SetParameter("ProfileId", profileid)
                                                           .SetParameter("msgtype", "twt_retweet")
                                                          .List<Domain.Socioboard.Domain.InboxMessages>().ToList().Where(x => x.EntryTime <= DateTime.Now && x.EntryTime >= (DateTime.Now.AddDays(-15))).ToList();

                    inboxmessages_30 = session.CreateQuery(strtwitterfollowers)
                                                          .SetParameter("ProfileId", profileid)
                                                           .SetParameter("msgtype", "twt_retweet")
                                                          .List<Domain.Socioboard.Domain.InboxMessages>().ToList().Where(x => x.EntryTime <= DateTime.Now && x.EntryTime >= (DateTime.Now.AddDays(-30))).ToList();
                    inboxmessages_60 = session.CreateQuery(strtwitterfollowers)
                                                           .SetParameter("ProfileId", profileid)
                                                            .SetParameter("msgtype", "twt_retweet")
                                                           .List<Domain.Socioboard.Domain.InboxMessages>().ToList().Where(x => x.EntryTime <= DateTime.Now && x.EntryTime >= (DateTime.Now.AddDays(-60))).ToList();
                    inboxmessages_90 = session.CreateQuery(strtwitterfollowers)
                                                           .SetParameter("ProfileId", profileid)
                                                            .SetParameter("msgtype", "twt_retweet")
                                                           .List<Domain.Socioboard.Domain.InboxMessages>().ToList().Where(x => x.EntryTime <= DateTime.Now && x.EntryTime >= (DateTime.Now.AddDays(-90))).ToList();

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
                            item = session4.QueryOver<Domain.Socioboard.Domain.InboxMessages>().Where(m => m.CreatedTime >= present_date.Date && m.CreatedTime <= present_date.AddDays(1).Date && m.MessageType == "twt_retweet").Where(s => s.RecipientId == profileid)
                                                     .Select(Projections.CountDistinct<Domain.Socioboard.Domain.InboxMessages>(x => x.MessageId))
                                                     .FutureValue<int>().Value;
                        }
                        catch (Exception e)
                        {
                            item = 0;

                        }

                        int add_data = item;

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




            try
            {
                perday_15 = perday_90.Substring(0, 29);
                perday_30 = perday_90.Substring(0, 59);
                perday_60 = perday_90.Substring(0, 119);
            }
            catch (Exception e)
            {
                logger.Error(e.Message);
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


                    clicks_15 = session.Query<Domain.Socioboard.Domain.ScheduledMessage>().Where(x => x.ShareMessage.Contains(link) && x.ProfileId == profileid && x.Status == true && x.ScheduleTime <= DateTime.Now && x.ScheduleTime >= (DateTime.Now.AddDays(-15))).ToList<Domain.Socioboard.Domain.ScheduledMessage>();


                    clicks_30 = session.Query<Domain.Socioboard.Domain.ScheduledMessage>().Where(x => x.ShareMessage.Contains(link) && x.ProfileId == profileid && x.Status == true && x.ScheduleTime <= DateTime.Now && x.ScheduleTime >= (DateTime.Now.AddDays(-30))).ToList<Domain.Socioboard.Domain.ScheduledMessage>();

                    clicks_60 = session.Query<Domain.Socioboard.Domain.ScheduledMessage>().Where(x => x.ShareMessage.Contains(link) && x.ProfileId == profileid && x.Status == true && x.ScheduleTime <= DateTime.Now && x.ScheduleTime >= (DateTime.Now.AddDays(-60))).ToList<Domain.Socioboard.Domain.ScheduledMessage>();

                    clicks_90 = session.Query<Domain.Socioboard.Domain.ScheduledMessage>().Where(x => x.ShareMessage.Contains(link) && x.ProfileId == profileid && x.Status == true && x.ScheduleTime <= DateTime.Now && x.ScheduleTime >= (DateTime.Now.AddDays(-90))).ToList<Domain.Socioboard.Domain.ScheduledMessage>();
                            
                            
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
                            perday_clicks_90 = session4.Query<Domain.Socioboard.Domain.ScheduledMessage>().Where(x => x.ShareMessage.Contains(link) && x.ProfileId == profileid && x.Status == true && x.ScheduleTime <= present_date.AddDays(1).Date && x.ScheduleTime >= present_date.Date).ToList<Domain.Socioboard.Domain.ScheduledMessage>();

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




            try
            {
                perday_15 = perday_90.Substring(0, 29);
                perday_30 = perday_90.Substring(0, 59);
                perday_60 = perday_90.Substring(0, 119);
            }
            catch (Exception e)
            {
                logger.Error(e.Message);
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
                    twitterdirectmessages_15 = session.CreateQuery(strtwitterfollowers)
                                                          .SetParameter("ProfileId", profileid)
                                                          .List<Domain.Socioboard.Domain.TwitterDirectMessages>().ToList().Where(x => x.EntryDate <= DateTime.Now && x.EntryDate >= (DateTime.Now.AddDays(-15))).ToList();

                    twitterdirectmessages_30 = session.CreateQuery(strtwitterfollowers)
                                                          .SetParameter("ProfileId", profileid)
                                                          .List<Domain.Socioboard.Domain.TwitterDirectMessages>().ToList().Where(x => x.EntryDate <= DateTime.Now && x.EntryDate >= (DateTime.Now.AddDays(-30))).ToList();
                    twitterdirectmessages_60 = session.CreateQuery(strtwitterfollowers)
                                                           .SetParameter("ProfileId", profileid)
                                                           .List<Domain.Socioboard.Domain.TwitterDirectMessages>().ToList().Where(x => x.EntryDate <= DateTime.Now && x.EntryDate >= (DateTime.Now.AddDays(-60))).ToList();
                    twitterdirectmessages_90 = session.CreateQuery(strtwitterfollowers)
                                                           .SetParameter("ProfileId", profileid)
                                                           .List<Domain.Socioboard.Domain.TwitterDirectMessages>().ToList().Where(x => x.EntryDate <= DateTime.Now && x.EntryDate >= (DateTime.Now.AddDays(-90))).ToList();

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
                    schedule_15 = session2.CreateQuery(strschedule)
                                                  .SetParameter("profileid", profileid)
                                                  .SetParameter("msgtype", true)
                                                  .List<Domain.Socioboard.Domain.ScheduledMessage>().ToList().Where(x => x.ScheduleTime <= DateTime.Now && x.ScheduleTime >= (DateTime.Now.AddDays(-15))).ToList();

                    schedule_30 = session2.CreateQuery(strschedule)
                                            .SetParameter("profileid", profileid)
                                            .SetParameter("msgtype", true)
                                            .List<Domain.Socioboard.Domain.ScheduledMessage>().ToList().Where(x => x.ScheduleTime <= DateTime.Now && x.ScheduleTime >= (DateTime.Now.AddDays(-30))).ToList();

                    schedule_60 = session2.CreateQuery(strschedule)
                                           .SetParameter("profileid", profileid)
                                           .SetParameter("msgtype", true)
                                           .List<Domain.Socioboard.Domain.ScheduledMessage>().ToList().Where(x => x.ScheduleTime <= DateTime.Now && x.ScheduleTime >= (DateTime.Now.AddDays(-60))).ToList();

                    schedule_90 = session2.CreateQuery(strschedule)
                                           .SetParameter("profileid", profileid)
                                           .SetParameter("msgtype", true)
                                           .List<Domain.Socioboard.Domain.ScheduledMessage>().ToList().Where(x => x.ScheduleTime <= DateTime.Now && x.ScheduleTime >= (DateTime.Now.AddDays(-90))).ToList();

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
                            List<Domain.Socioboard.Domain.ScheduledMessage> perday_schedule_90 = session5.CreateQuery(strschedule)
                                                                                        .SetParameter("profileid", profileid)
                                                                                        .SetParameter("msgtype", true)
                                                                                        .List<Domain.Socioboard.Domain.ScheduledMessage>().ToList().Where(x => x.ScheduleTime <= present_date.AddDays(1).Date && x.ScheduleTime >= present_date.Date).ToList();



                            item = perday_schedule_90.Count;
                        }
                        catch (Exception e)
                        {

                            item = 0;

                        }

                        try
                        {
                            List<Domain.Socioboard.Domain.TwitterDirectMessages> perday_twitterdirectmessages_90 = session5.CreateQuery(strtwitterfollowers)
                                                                .SetParameter("ProfileId", profileid)
                                                                 .List<Domain.Socioboard.Domain.TwitterDirectMessages>().ToList().Where(x => x.CreatedDate <= present_date.AddDays(1).Date && x.CreatedDate >= present_date.Date).ToList();
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




            try
            {
                perday_15 = perday_90.Substring(0, 29);
                perday_30 = perday_90.Substring(0, 59);
                perday_60 = perday_90.Substring(0, 119);
            }
            catch (Exception e)
            {
                logger.Error(e.Message);
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
                    twitterdirectmessages_15 = session.CreateQuery(strtwitterfollowers)
                                                          .SetParameter("ProfileId", profileid)
                                                          .List<Domain.Socioboard.Domain.TwitterDirectMessages>().ToList().Where(x => x.EntryDate <= DateTime.Now && x.EntryDate >= (DateTime.Now.AddDays(-15))).ToList();

                    twitterdirectmessages_30 = session.CreateQuery(strtwitterfollowers)
                                                          .SetParameter("ProfileId", profileid)
                                                          .List<Domain.Socioboard.Domain.TwitterDirectMessages>().ToList().Where(x => x.EntryDate <= DateTime.Now && x.EntryDate >= (DateTime.Now.AddDays(-30))).ToList();
                    twitterdirectmessages_60 = session.CreateQuery(strtwitterfollowers)
                                                           .SetParameter("ProfileId", profileid)
                                                           .List<Domain.Socioboard.Domain.TwitterDirectMessages>().ToList().Where(x => x.EntryDate <= DateTime.Now && x.EntryDate >= (DateTime.Now.AddDays(-60))).ToList();
                    twitterdirectmessages_90 = session.CreateQuery(strtwitterfollowers)
                                                           .SetParameter("ProfileId", profileid)
                                                           .List<Domain.Socioboard.Domain.TwitterDirectMessages>().ToList().Where(x => x.EntryDate <= DateTime.Now && x.EntryDate >= (DateTime.Now.AddDays(-90))).ToList();

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
                    inboxmessages_15 = session2.CreateQuery(strinboxmessages)
                                                  .SetParameter("profileid", profileid)
                                                  .SetParameter("msgtype", "twt_mention")
                                                 .List<Domain.Socioboard.Domain.InboxMessages>().ToList().Where(x => x.CreatedTime <= DateTime.Now && x.CreatedTime >= (DateTime.Now.AddDays(-15))).ToList();

                    inboxmessages_30 = session2.CreateQuery(strinboxmessages)
                                            .SetParameter("profileid", profileid)
                                            .SetParameter("msgtype", "twt_mention")
                                             .List<Domain.Socioboard.Domain.InboxMessages>().ToList().Where(x => x.CreatedTime <= DateTime.Now && x.CreatedTime >= (DateTime.Now.AddDays(-15))).ToList();

                    inboxmessages_60 = session2.CreateQuery(strinboxmessages)
                                           .SetParameter("profileid", profileid)
                                           .SetParameter("msgtype", "twt_mention")
                                            .List<Domain.Socioboard.Domain.InboxMessages>().ToList().Where(x => x.CreatedTime <= DateTime.Now && x.CreatedTime >= (DateTime.Now.AddDays(-15))).ToList();

                    inboxmessages_90 = session2.CreateQuery(strinboxmessages)
                                           .SetParameter("profileid", profileid)
                                           .SetParameter("msgtype", "twt_mention")
                                             .List<Domain.Socioboard.Domain.InboxMessages>().ToList().Where(x => x.CreatedTime <= DateTime.Now && x.CreatedTime >= (DateTime.Now.AddDays(-15))).ToList();

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
                                        int item=0;
                                        int item1 = 0;
                                        try
                                        {
                                             item = session1.QueryOver<Domain.Socioboard.Domain.InboxMessages>().Where(m => m.CreatedTime >= present_date.Date && m.CreatedTime <= present_date.AddDays(1).Date && m.MessageType == "twt_mention").Where(S => S.RecipientId == profileid)
                                                                          .Select(Projections.CountDistinct<Domain.Socioboard.Domain.InboxMessages>(x => x.MessageId))
                                                                          .FutureValue<int>().Value;

                                        }
                                        catch (Exception e)
                                        {
                                            item = 0;
                                            
                                        }


                                        try
                                        {
                                            item1 = session1.QueryOver<Domain.Socioboard.Domain.TwitterDirectMessages>().Where(m => m.CreatedDate >= present_date.Date && m.CreatedDate <= present_date.AddDays(1).Date).Where(S => S.RecipientId == profileid)
                                                                         .Select(Projections.CountDistinct<Domain.Socioboard.Domain.TwitterDirectMessages>(x => x.MessageId))
                                                                         .FutureValue<int>().Value;

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




            try
            {
                perday_15 = perday_90.Substring(0, 29);
                perday_30 = perday_90.Substring(0, 59);
                perday_60 = perday_90.Substring(0, 119);
            }
            catch (Exception e)
            {
                logger.Error(e.Message);
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
        public void insertdata(string i)
        {

            Domain.Socioboard.Domain.TwitterReport insert = (Domain.Socioboard.Domain.TwitterReport)new JavaScriptSerializer().Deserialize(i, typeof(Domain.Socioboard.Domain.TwitterReport));

            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                try
                {
                    //After Session creation, start Transaction. 
                    using (NHibernate.ITransaction transaction = session.BeginTransaction())
                    {
                        //Proceed action, to save data.
                        bool exist = session.Query<Domain.Socioboard.Domain.TwitterReport>()
                           .Any(x => x.twitterprofileid == insert.twitterprofileid);
                        if (exist)
                        {
                            int _i = session.CreateQuery("Update TwitterReport set twitterprofileid = : twitterprofileid , totalfollower = : totalfollower , totalconnection = : totalconnection , newfollower_15 = : newfollower_15 , perday_newfollower_15 = : perday_newfollower_15 , newfollower_30 = : newfollower_30 , perday_newfollower_30 = : perday_newfollower_30 , newfollower_60 = : newfollower_60 , perday_newfollower_60 = : perday_newfollower_60 , newfollower_90 = : newfollower_90 , perday_newfollower_90 = : perday_newfollower_90 , following_15 = : following_15 , perday_following_15 = : perday_following_15 , following_30 = : following_30 , perday_following_30 = : perday_following_30 , following_60 = : following_60 , perday_following_60 = : perday_following_60 , following_90 = : following_90 , perday_following_90 = : perday_following_90 , directmessage_15 = : directmessage_15 , perday_directmessage_15 = : perday_directmessage_15 , directmessage_30 = : directmessage_30 , perday_directmessage_30 = : perday_directmessage_30 , directmessage_60 = : directmessage_60 , perday_directmessage_60 = : perday_directmessage_60 , directmessage_90 = : directmessage_90 , perday_directmessage_90 = : perday_directmessage_90 , mention_15 = : mention_15 , perday_mention_15 = : perday_mention_15 , mention_30 = : mention_30 , perday_mention_30 = : perday_mention_30 , mention_60 = : mention_60 , perday_mention_60 = : perday_mention_60 , mention_90 = : mention_90 , perday_mention_90 = : perday_mention_90 , messagesent_15 = : messagesent_15 , perday_messagesent_15 = : perday_messagesent_15 , messagesent_30 = : messagesent_30 , perday_messagesent_30 = : perday_messagesent_30 , messagesent_60 = : messagesent_60 , perday_messagesent_60 = : perday_messagesent_60 , messagesent_90 = : messagesent_90 , perday_messagesent_90 = : perday_messagesent_90 , messagerecieved_15 = : messagerecieved_15 , perday_messagerecieved_15 = : perday_messagerecieved_15 , messagerecieved_30 = : messagerecieved_30 , perday_messagerecieved_30 = : perday_messagerecieved_30 , messagerecieved_60 = : messagerecieved_60 , perday_messagerecieved_60 = : perday_messagerecieved_60 , messagerecieved_90 = : messagerecieved_90 , perday_messagerecieved_90 = : perday_messagerecieved_90 , click_15 = : click_15 , perday_click_15 = : perday_click_15 , click_30 = : click_30 , perday_click_30 = : perday_click_30 , click_60 = : click_60 , perday_click_60 = : perday_click_60 , click_90 = : click_90 , perday_click_90 = : perday_click_90 , retweets_15 = : retweets_15 , perday_retweets_15 = : perday_retweets_15 , retweets_30 = : retweets_30 , perday_retweets_30 = : perday_retweets_30 , retweets_60 = : retweets_60 , perday_retweets_60 = : perday_retweets_60 , retweets_90 = : retweets_90 , perday_retweets_90 = : perday_retweets_90 , directmessagesent_15 = :  directmessagesent_15 , perday_directmessagesent_15 = : perday_directmessagesent_15 , directmessagesent_30 = :  directmessagesent_30 , perday_directmessagesent_30 = : perday_directmessagesent_30 , directmessagesent_60 = :  directmessagesent_60 , perday_directmessagesent_60 = : perday_directmessagesent_60 , directmessagesent_90 = :  directmessagesent_90 , perday_directmessagesent_90 = : perday_directmessagesent_90")
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
                    ret_string = new JavaScriptSerializer().Serialize(ret);

                }
                catch (Exception e)
                {
                    logger.Error("noretrievedata>>");
                    Console.WriteLine(e.StackTrace);
                    logger.Error(e.Message);
                    logger.Error(e.StackTrace);

                }

            }
            return ret_string;
        }
        


    }
}
               
            
          

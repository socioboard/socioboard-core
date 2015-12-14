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
    /// Summary description for GroupReports
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class GroupReports : System.Web.Services.WebService
    {
        ILog logger = LogManager.GetLogger(typeof(GroupReports));

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string getgroups()
        {
            List<Domain.Socioboard.Domain.Groups> groups = new List<Domain.Socioboard.Domain.Groups>();
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                try
                {
                    groups = session.CreateQuery("from Groups").List<Domain.Socioboard.Domain.Groups>().ToList();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.StackTrace);
                    logger.Error(e.Message);
                    logger.Error(e.StackTrace);
                }
            }
            return new JavaScriptSerializer().Serialize(groups);
        }
        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string getinboxcount(string groupid, string userid)
        {
            Domain.Socioboard.Domain.ReturnData r = new Domain.Socioboard.Domain.ReturnData();
            long _15 = 0;
            long _30 = 0;
            long _60 = 0;
            long _90 = 0;
            List<Domain.Socioboard.Domain.InboxMessages> inboxmessages_15;
            List<Domain.Socioboard.Domain.InboxMessages> inboxmessages_30;
            List<Domain.Socioboard.Domain.InboxMessages> inboxmessages_60;
            List<Domain.Socioboard.Domain.InboxMessages> inboxmessages_90;
            List<Domain.Socioboard.Domain.TwitterDirectMessages> twitterdirectmessages_15;
            List<Domain.Socioboard.Domain.TwitterDirectMessages> twitterdirectmessages_30;
            List<Domain.Socioboard.Domain.TwitterDirectMessages> twitterdirectmessages_60;
            List<Domain.Socioboard.Domain.TwitterDirectMessages> twitterdirectmessages_90;
            List<Domain.Socioboard.Domain.Team> teams;
            List<Domain.Socioboard.Domain.TeamMemberProfile> teammemberprofiles;
            DateTime present_date = DateTime.Now;
            string perday_15 = string.Empty;
            string perday_30 = string.Empty;
            string perday_60 = string.Empty;
            string perday_90 = string.Empty;

            string AllProfileId = string.Empty;
            string FbProfileId = string.Empty;
            string TwtProfileId = string.Empty;
            string[] ArrProfileId = { };

            try
            {
                using (NHibernate.ISession session4 = SessionFactory.GetNewSession())
                {
                    teams = session4.CreateQuery("from Team t where t.GroupId = : groupid and t.UserId = : userid ").SetParameter("groupid", Guid.Parse(groupid)).SetParameter("userid", Guid.Parse(userid)).List<Domain.Socioboard.Domain.Team>().ToList();
                }
                foreach (Domain.Socioboard.Domain.Team team in teams)
                {
                    using (NHibernate.ISession session5 = SessionFactory.GetNewSession())
                    {
                        teammemberprofiles = session5.CreateQuery("from TeamMemberProfile t where t.TeamId = : teamid and t.ProfileType =: ProfileType").SetParameter("ProfileType", "twitter").SetParameter("teamid", team.Id).List<Domain.Socioboard.Domain.TeamMemberProfile>().ToList();
                    }
                    foreach (Domain.Socioboard.Domain.TeamMemberProfile teammemberprofile in teammemberprofiles)
                    {
                        try
                        {
                            AllProfileId += teammemberprofile.ProfileId + ',';
                        }
                        catch (Exception Err)
                        {
                            Console.Write(Err.StackTrace);
                        }
                    }
                    try
                    {
                        AllProfileId = AllProfileId.Substring(0, AllProfileId.Length - 1);
                        ArrProfileId = AllProfileId.Split(',');
                    }
                    catch (Exception Err)
                    {
                        Console.Write(Err.StackTrace);
                    }
                    if (!string.IsNullOrEmpty(AllProfileId))
                    {
                        string strtwitterdirectmessages = "from TwitterDirectMessages t where t.UserId =: UserId and t.RecipientId In(" + AllProfileId + ")";

                        using (NHibernate.ISession session2 = SessionFactory.GetNewSession())
                        {

                            twitterdirectmessages_90 = session2.CreateQuery(strtwitterdirectmessages)
                                                                                  .SetParameter("UserId", Guid.Parse(userid))
                                                                                .List<Domain.Socioboard.Domain.TwitterDirectMessages>().ToList().Where(x => x.CreatedDate < DateTime.Now.AddDays(1).Date && x.CreatedDate >= (DateTime.Now.AddDays(-90).Date)).ToList();

                            twitterdirectmessages_60 = twitterdirectmessages_90.Where(x => x.CreatedDate < DateTime.Now.AddDays(1).Date && x.CreatedDate >= (DateTime.Now.AddDays(-60).Date)).ToList();
                            twitterdirectmessages_30 = twitterdirectmessages_90.Where(x => x.CreatedDate < DateTime.Now.AddDays(1).Date && x.CreatedDate >= (DateTime.Now.AddDays(-30).Date)).ToList();
                            twitterdirectmessages_15 = twitterdirectmessages_90.Where(x => x.CreatedDate < DateTime.Now.AddDays(1).Date && x.CreatedDate >= (DateTime.Now.AddDays(-15).Date)).ToList();


                        }
                        string strinboxmessages = "from InboxMessages t where t.UserId =: UserId and t.MessageType = : msgtype and t.RecipientId In(" + AllProfileId + ")";

                        using (NHibernate.ISession session3 = SessionFactory.GetNewSession())
                        {

                            inboxmessages_90 = session3.CreateQuery(strinboxmessages)
                                                                              .SetParameter("UserId", Guid.Parse(userid))
                                                                                .SetParameter("msgtype", "twt_mention")
                                                                                .List<Domain.Socioboard.Domain.InboxMessages>().ToList().Where(x => x.CreatedTime < DateTime.Now.AddDays(1).Date && x.CreatedTime >= (DateTime.Now.AddDays(-90).Date)).ToList();

                            inboxmessages_60 = inboxmessages_90.Where(x => x.CreatedTime < DateTime.Now.AddDays(1).Date && x.CreatedTime >= (DateTime.Now.AddDays(-60).Date)).ToList();

                            inboxmessages_30 = inboxmessages_90.Where(x => x.CreatedTime < DateTime.Now.AddDays(1).Date && x.CreatedTime >= (DateTime.Now.AddDays(-30).Date)).ToList();

                            inboxmessages_15 = inboxmessages_90.Where(x => x.CreatedTime < DateTime.Now.AddDays(1).Date && x.CreatedTime >= (DateTime.Now.AddDays(-15).Date)).ToList();


                        }
                        present_date = DateTime.Now;
                        while (present_date.Date != DateTime.Now.Date.AddDays(-90))
                        {
                            List<Domain.Socioboard.Domain.InboxMessages> _inboxmessages = new List<Domain.Socioboard.Domain.InboxMessages>();
                            List<Domain.Socioboard.Domain.TwitterDirectMessages> _twitterdirectmessages = new List<Domain.Socioboard.Domain.TwitterDirectMessages>();
                            try
                            {
                                using (NHibernate.ISession session1 = SessionFactory.GetNewSession())
                                {
                                    int item = 0;
                                    int item1 = 0;
                                    try
                                    {
                                        //item = session1.QueryOver<Domain.Socioboard.Domain.InboxMessages>().Where(m => m.CreatedTime >= present_date.Date.AddSeconds(1) && m.CreatedTime <= present_date.AddDays(1).Date.AddSeconds(-1) && m.MessageType == "twt_mention").WhereRestrictionOn(S => S.RecipientId).IsIn(ArrProfileId)
                                        //                             .Select(Projections.CountDistinct<Domain.Socioboard.Domain.InboxMessages>(x => x.MessageId))
                                        //                             .FutureValue<int>().Value;

                                        _inboxmessages = inboxmessages_90.Where(m => m.CreatedTime >= present_date.Date.AddSeconds(1) && m.CreatedTime <= present_date.AddDays(1).Date.AddSeconds(-1)).ToList();
                                        item = _inboxmessages.Count;
                                    }
                                    catch (Exception e)
                                    {
                                        item = 0;
                                    }
                                    try
                                    {
                                        //item1 = session1.QueryOver<Domain.Socioboard.Domain.TwitterDirectMessages>().Where(m => m.CreatedDate >= present_date.Date.AddSeconds(1) && m.CreatedDate <= present_date.AddDays(1).Date.AddSeconds(-1)).WhereRestrictionOn(S => S.RecipientId).IsIn(ArrProfileId)
                                        //                             .Select(Projections.CountDistinct<Domain.Socioboard.Domain.TwitterDirectMessages>(x => x.MessageId))
                                        //                             .FutureValue<int>().Value;

                                        _twitterdirectmessages = twitterdirectmessages_90.Where(m => m.CreatedDate >= present_date.Date.AddSeconds(1) && m.CreatedDate <= present_date.AddDays(1).Date.AddSeconds(-1)).ToList();

                                        item1 = _twitterdirectmessages.Count;
                                    }
                                    catch (Exception e)
                                    {
                                        item1 = 0;
                                    }

                                    int add_data = item + item1;

                                    perday_90 = perday_90 + add_data.ToString() + ",";

                                    present_date = present_date.AddDays(-1);
                                    //logger.Error("perdayinbox >>" + perday_90);
                                }
                            }
                            catch (Exception e)
                            {
                                logger.Error("perdayinboxerror >>" + e.Message);
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
                        r._15 = twitterdirectmessages_15.Count + inboxmessages_15.Count;
                        r._30 = twitterdirectmessages_30.Count + inboxmessages_30.Count;
                        r._60 = twitterdirectmessages_60.Count + inboxmessages_60.Count;
                        r._90 = twitterdirectmessages_90.Count + inboxmessages_90.Count;

                        r.perday_15 = perday_15;
                        r.perday_30 = perday_30;
                        r.perday_60 = perday_60;
                        r.perday_90 = perday_90;
                    }
                }
            }
            catch (Exception e)
            {
                logger.Error(e.Message);
                logger.Error(e.StackTrace);
            }

            string ret = new JavaScriptSerializer().Serialize(r);
            return ret;
        }
        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string getsentmessage(string groupid, string userid)
        {

            Domain.Socioboard.Domain.ReturnData r = new Domain.Socioboard.Domain.ReturnData();
            long _15 = 0;
            long _30 = 0;
            long _60 = 0;
            long _90 = 0;

            DateTime present_date = DateTime.Now;
            string perday_15 = string.Empty;
            string perday_30 = string.Empty;
            string perday_60 = string.Empty;
            string perday_90 = string.Empty;

            string AllProfileId = string.Empty;
            string AllProfileIds = string.Empty;
            string FbProfileId = string.Empty;
            string TwtProfileId = string.Empty;
            string[] ArrProfileId = { };
            List<Domain.Socioboard.Domain.Team> teams;
            List<Domain.Socioboard.Domain.TeamMemberProfile> teammemberprofiles;
            List<Domain.Socioboard.Domain.TwitterDirectMessages> twitterdirectmessages_15;
            List<Domain.Socioboard.Domain.TwitterDirectMessages> twitterdirectmessages_30;
            List<Domain.Socioboard.Domain.TwitterDirectMessages> twitterdirectmessages_60;
            List<Domain.Socioboard.Domain.TwitterDirectMessages> twitterdirectmessages_90;
            List<Domain.Socioboard.Domain.ScheduledMessage> schedule_15;
            List<Domain.Socioboard.Domain.ScheduledMessage> schedule_30;
            List<Domain.Socioboard.Domain.ScheduledMessage> schedule_60;
            List<Domain.Socioboard.Domain.ScheduledMessage> schedule_90;


            try
            {
                using (NHibernate.ISession session1 = SessionFactory.GetNewSession())
                {

                    teams = session1.CreateQuery("from Team t where t.GroupId = : groupid and t.UserId = : userid ").SetParameter("groupid", Guid.Parse(groupid)).SetParameter("userid", Guid.Parse(userid)).List<Domain.Socioboard.Domain.Team>().ToList();
                }

                foreach (Domain.Socioboard.Domain.Team team in teams)
                {
                    using (NHibernate.ISession session2 = SessionFactory.GetNewSession())
                    {

                        teammemberprofiles = session2.CreateQuery("from TeamMemberProfile t where t.TeamId = : teamid ").SetParameter("teamid", team.Id).List<Domain.Socioboard.Domain.TeamMemberProfile>().ToList();
                    }
                    foreach (Domain.Socioboard.Domain.TeamMemberProfile teammemberprofile in teammemberprofiles)
                    {
                        try
                        {
                            AllProfileId += teammemberprofile.ProfileId + ',';
                            AllProfileIds += "'" + teammemberprofile.ProfileId + "',";
                        }
                        catch (Exception Err)
                        {
                            Console.Write(Err.StackTrace);
                        }
                    }

                    try
                    {
                        AllProfileId = AllProfileId.Substring(0, AllProfileId.Length - 1);
                        AllProfileIds = AllProfileIds.Substring(0, AllProfileIds.Length - 1);
                        ArrProfileId = AllProfileId.Split(',');
                    }
                    catch (Exception Err)
                    {
                        Console.Write(Err.StackTrace);
                    }


                    if (!string.IsNullOrEmpty(AllProfileId))
                    {
                        string strtwitterdirectmessages = "from TwitterDirectMessages t where t.UserId =: UserId and t.SenderId In(" + AllProfileIds + ")";

                        using (NHibernate.ISession session3 = SessionFactory.GetNewSession())
                        {

                            twitterdirectmessages_90 = session3.CreateQuery(strtwitterdirectmessages)
                                                                                 .SetParameter("UserId", Guid.Parse(userid))
                                                                                 .List<Domain.Socioboard.Domain.TwitterDirectMessages>().ToList().Where(x => x.CreatedDate < DateTime.Now.AddDays(1).Date && x.CreatedDate > (DateTime.Now.AddDays(-90).Date)).ToList();

                            twitterdirectmessages_60 = twitterdirectmessages_90.Where(x => x.CreatedDate < DateTime.Now.AddDays(1).Date && x.CreatedDate > (DateTime.Now.AddDays(-60).Date)).ToList();

                            twitterdirectmessages_30 = twitterdirectmessages_90.Where(x => x.CreatedDate < DateTime.Now.AddDays(1).Date && x.CreatedDate > (DateTime.Now.AddDays(-30).Date)).ToList();

                            twitterdirectmessages_15 = twitterdirectmessages_90.Where(x => x.CreatedDate < DateTime.Now.AddDays(1).Date && x.CreatedDate > (DateTime.Now.AddDays(-15).Date)).ToList();

                        }
                        string strschedule = "from ScheduledMessage t where t.UserId =: UserId and t.Status = : msgtype and t.ProfileId In(" + AllProfileIds + ")";

                        using (NHibernate.ISession session4 = SessionFactory.GetNewSession())
                        {

                            schedule_90 = session4.CreateQuery(strschedule)
                                                                .SetParameter("UserId", Guid.Parse(userid))
                                                                .SetParameter("msgtype", true)
                                                                .List<Domain.Socioboard.Domain.ScheduledMessage>().ToList().Where(x => x.ScheduleTime < DateTime.Now.AddDays(1).Date && x.ScheduleTime > (DateTime.Now.AddDays(-90).Date)).ToList();

                            schedule_60 = schedule_90.Where(x => x.ScheduleTime < DateTime.Now.AddDays(1).Date && x.ScheduleTime > (DateTime.Now.AddDays(-60).Date)).ToList();

                            schedule_30 = schedule_90.Where(x => x.ScheduleTime < DateTime.Now.AddDays(1).Date && x.ScheduleTime > (DateTime.Now.AddDays(-30).Date)).ToList();

                            schedule_15 = schedule_90.Where(x => x.ScheduleTime < DateTime.Now.AddDays(1).Date && x.ScheduleTime > (DateTime.Now.AddDays(-15).Date)).ToList();

                        }

                        present_date = DateTime.Now;

                        while (present_date.Date != DateTime.Now.Date.AddDays(-90))
                        {
                            try
                            {
                                //  int item = session.QueryOver<Domain.Socioboard.Domain.ScheduledMessage>().Where(m => m.ScheduleTime >= present_date.Date && m.ScheduleTime <= present_date.AddDays(1).Date && ArrProfileId.Contains(m.ProfileId) && m.Status == 1).Select(Projections.RowCount()).FutureValue<int>().Value;

                                using (NHibernate.ISession session5 = SessionFactory.GetNewSession())
                                {
                                    int item = 0;
                                    int item1 = 0;

                                    try
                                    {
                                        //List<Domain.Socioboard.Domain.ScheduledMessage> perday_schedule_90 = session5.CreateQuery(strschedule)
                                        //                                                            .SetParameter("UserId", Guid.Parse(userid))
                                        //                                                            .SetParameter("msgtype", true)
                                        //                                                            .List<Domain.Socioboard.Domain.ScheduledMessage>().ToList().Where(x => x.ScheduleTime <= present_date.AddDays(1).Date.AddSeconds(-1) && x.ScheduleTime >= present_date.Date.AddSeconds(1)).ToList();

                                        List<Domain.Socioboard.Domain.ScheduledMessage> perday_schedule_90 = schedule_90.Where(x => x.ScheduleTime <= present_date.AddDays(1).Date.AddSeconds(-1) && x.ScheduleTime >= present_date.Date.AddSeconds(1)).ToList();

                                        item = perday_schedule_90.Count;
                                    }
                                    catch (Exception e)
                                    {

                                        item = 0;

                                    }

                                    try
                                    {
                                        //List<Domain.Socioboard.Domain.TwitterDirectMessages> perday_twitterdirectmessages_90 = session5.CreateQuery(strtwitterdirectmessages)
                                        //                                      .SetParameter("UserId", Guid.Parse(userid))
                                        //                                     .List<Domain.Socioboard.Domain.TwitterDirectMessages>().ToList().Where(x => x.CreatedDate <= present_date.AddDays(1).Date.AddSeconds(-1) && x.CreatedDate >= present_date.Date.AddSeconds(1)).ToList();

                                        List<Domain.Socioboard.Domain.TwitterDirectMessages> perday_twitterdirectmessages_90 = twitterdirectmessages_90.Where(x => x.CreatedDate <= present_date.AddDays(1).Date.AddSeconds(-1) && x.CreatedDate >= present_date.Date.AddSeconds(1)).ToList();

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
                                logger.Error("perdaysenterror >>" + e);
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



                        r._15 = twitterdirectmessages_15.Count + schedule_15.Count;
                        r._30 = twitterdirectmessages_30.Count + schedule_30.Count;
                        r._60 = twitterdirectmessages_60.Count + schedule_60.Count;
                        r._90 = twitterdirectmessages_90.Count + schedule_90.Count;


                        r.perday_15 = perday_15;
                        r.perday_30 = perday_30;
                        r.perday_60 = perday_60;
                        r.perday_90 = perday_90;
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
                logger.Error(e.Message);
                logger.Error(e.StackTrace);
            }

            string ret = new JavaScriptSerializer().Serialize(r);
            return ret;
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string gettwitterfollowers(string groupid, string userid)
        {

            Domain.Socioboard.Domain.ReturnData r = new Domain.Socioboard.Domain.ReturnData();

            int i = 0;
            long[] arr_perday = new long[90];
            DateTime present_date = DateTime.Now;
            string perday_15 = string.Empty;
            string perday_30 = string.Empty;
            string perday_60 = string.Empty;
            string perday_90 = string.Empty;
            string AllProfileId = string.Empty;
            string FbProfileId = string.Empty;
            string TwtProfileId = string.Empty;
            string[] perdayalldatas = { };

            string[] ArrProfileId = { };
            List<Domain.Socioboard.Domain.Team> teams;
            List<Domain.Socioboard.Domain.TeamMemberProfile> teammemberprofiles;

            List<Domain.Socioboard.Domain.InboxMessages> InboxMessages_15 = new List<Domain.Socioboard.Domain.InboxMessages>();
            List<Domain.Socioboard.Domain.InboxMessages> InboxMessages_30 = new List<Domain.Socioboard.Domain.InboxMessages>();
            List<Domain.Socioboard.Domain.InboxMessages> InboxMessages_60 = new List<Domain.Socioboard.Domain.InboxMessages>();
            List<Domain.Socioboard.Domain.InboxMessages> InboxMessages_90 = new List<Domain.Socioboard.Domain.InboxMessages>();

            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                try
                {
                    using (NHibernate.ISession session1 = SessionFactory.GetNewSession())
                    {
                        teams = session1.CreateQuery("from Team t where t.GroupId = : groupid and t.UserId = : userid ").SetParameter("groupid", Guid.Parse(groupid)).SetParameter("userid", Guid.Parse(userid)).List<Domain.Socioboard.Domain.Team>().ToList();
                    }
                    foreach (Domain.Socioboard.Domain.Team team in teams)
                    {

                        using (NHibernate.ISession session2 = SessionFactory.GetNewSession())
                        {
                            teammemberprofiles = session2.CreateQuery("from TeamMemberProfile t where t.TeamId = : teamid and  t.ProfileType =: ProfileType").SetParameter("teamid", team.Id).SetParameter("ProfileType", "twitter").List<Domain.Socioboard.Domain.TeamMemberProfile>().ToList();
                        }

                        foreach (Domain.Socioboard.Domain.TeamMemberProfile teammemberprofile in teammemberprofiles)
                        {
                            try
                            {
                                TwtProfileId += teammemberprofile.ProfileId + ',';
                            }
                            catch (Exception Err)
                            {
                                Console.Write(Err.StackTrace);
                            }
                        }
                        try
                        {
                            AllProfileId = TwtProfileId.Substring(0, TwtProfileId.Length - 1);
                            ArrProfileId = AllProfileId.Split(',');
                        }
                        catch (Exception Err)
                        {
                            Console.Write(Err.StackTrace);
                        }

                        if (!string.IsNullOrEmpty(AllProfileId))
                        {

                            InboxMessages_90 = session.Query<Domain.Socioboard.Domain.InboxMessages>().Where(U => U.Status == 0 && U.CreatedTime < DateTime.Now.AddDays(1).Date && U.CreatedTime >= (DateTime.Now.AddDays(-90).Date.AddSeconds(1)) && ArrProfileId.Contains(U.ProfileId) && U.MessageType == "twt_followers").GroupBy(x => new { x.ProfileId,x.FromId}).Select(g=>g.First()).ToList<Domain.Socioboard.Domain.InboxMessages>();

                            InboxMessages_60 = InboxMessages_90.Where(U => U.CreatedTime < DateTime.Now.AddDays(1).Date && U.CreatedTime >= (DateTime.Now.AddDays(-60).Date.AddSeconds(1))).ToList();

                            InboxMessages_30 = InboxMessages_90.Where(U => U.CreatedTime < DateTime.Now.AddDays(1).Date && U.CreatedTime >= (DateTime.Now.AddDays(-30).Date.AddSeconds(1))).ToList();

                            InboxMessages_15 = InboxMessages_90.Where(U => U.CreatedTime < DateTime.Now.AddDays(1).Date && U.CreatedTime >= (DateTime.Now.AddDays(-15).Date.AddSeconds(1))).ToList();

                            present_date = DateTime.Now;
                            i = 0;
                            while (present_date.Date != DateTime.Now.Date.AddDays(-90))
                            {
                                try
                                {
                                    using (NHibernate.ISession session4 = SessionFactory.GetNewSession())
                                    {
                                        //List<Domain.Socioboard.Domain.InboxMessages> lstresult = session4.Query<Domain.Socioboard.Domain.TwitterAccountFollowers>().Where(t => t.ProfileId == id && t.EntryDate >= present_date.Date.AddSeconds(1) && t.EntryDate <= present_date.AddDays(1).Date.AddSeconds(-1)).OrderByDescending(t => t.EntryDate).ToList();

                                        List<Domain.Socioboard.Domain.InboxMessages> lstresult = InboxMessages_90.Where(t => t.CreatedTime >= present_date.Date.AddSeconds(1) && t.CreatedTime <= present_date.AddDays(1).Date.AddSeconds(-1)).ToList();
                                        if (lstresult.Count > 0)
                                        {
                                            int newfollowerdata = lstresult.Count();

                                            perday_90 = perday_90 + newfollowerdata.ToString() + ",";

                                        }
                                        else
                                        {
                                            perday_90 += "0,";
                                        }
                                    }
                                }
                                catch (Exception e)
                                {
                                    logger.Error("perdaytwtnerror >>" + e.Message);
                                }

                                //logger.Error("perdaytwtn>>" + perday_90);
                                present_date = present_date.AddDays(-1);
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
                        }
                        try
                        {
                            string[] arr = perday_90.Split(',');
                            for (i = 0; i < 15; i++)
                            {
                                perday_15 = perday_15 + arr[i] + ",";
                            }
                            for (i = 0; i < 30; i++)
                            {
                                perday_30 = perday_30 + arr[i] + ",";
                            }
                            for (i = 0; i < 60; i++)
                            {
                                perday_60 = perday_60 + arr[i] + ",";
                            }
                        }
                        catch (Exception)
                        {

                        }

                        r.perday_15 = perday_15;
                        r.perday_30 = perday_30;
                        r.perday_60 = perday_60;
                        r.perday_90 = perday_90;

                    }

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
        public string getfbfans(string groupid, string userid)
        {

            Domain.Socioboard.Domain.ReturnData r = new Domain.Socioboard.Domain.ReturnData();
            
            int i = 0;
            long[] arr_perday = new long[90];
            DateTime present_date = DateTime.Now;
            string perday_15 = string.Empty;
            string perday_30 = string.Empty;
            string perday_60 = string.Empty;
            string perday_90 = string.Empty;
            string AllProfileId = string.Empty;
            string AllProfileIds = string.Empty;

            string FbProfileId = string.Empty;
            string FbProfileIds = string.Empty;
            string TwtProfileId = string.Empty;
            string[] ArrProfileId = { };
            List<Domain.Socioboard.Domain.Team> teams;
            List<Domain.Socioboard.Domain.TeamMemberProfile> teammemberprofiles;
            List<Domain.Socioboard.Domain.FacebookFanPage> fbfan_15;
            List<Domain.Socioboard.Domain.FacebookFanPage> fbfan_30;
            List<Domain.Socioboard.Domain.FacebookFanPage> fbfan_60;
            List<Domain.Socioboard.Domain.FacebookFanPage> fbfan_90;
            Domain.Socioboard.Domain.FacebookFanPage _15_first = new Domain.Socioboard.Domain.FacebookFanPage();
            Domain.Socioboard.Domain.FacebookFanPage _30_first = new Domain.Socioboard.Domain.FacebookFanPage();
            Domain.Socioboard.Domain.FacebookFanPage _60_first = new Domain.Socioboard.Domain.FacebookFanPage();
            Domain.Socioboard.Domain.FacebookFanPage _90_first = new Domain.Socioboard.Domain.FacebookFanPage();

            Domain.Socioboard.Domain.FacebookFanPage _15_last = new Domain.Socioboard.Domain.FacebookFanPage();
            Domain.Socioboard.Domain.FacebookFanPage _30_last = new Domain.Socioboard.Domain.FacebookFanPage();
            Domain.Socioboard.Domain.FacebookFanPage _60_last = new Domain.Socioboard.Domain.FacebookFanPage();
            Domain.Socioboard.Domain.FacebookFanPage _90_last = new Domain.Socioboard.Domain.FacebookFanPage();
            long perdayalldata = 0;


            try
            {
                using (NHibernate.ISession session1 = SessionFactory.GetNewSession())
                {
                    teams = session1.CreateQuery("from Team t where t.GroupId = : groupid and t.UserId = : userid ").SetParameter("groupid", Guid.Parse(groupid)).SetParameter("userid", Guid.Parse(userid)).List<Domain.Socioboard.Domain.Team>().ToList();
                }
                foreach (Domain.Socioboard.Domain.Team team in teams)
                {
                    using (NHibernate.ISession session2 = SessionFactory.GetNewSession())
                    {

                        teammemberprofiles = session2.CreateQuery("from TeamMemberProfile t where t.TeamId = : teamid and t.ProfileType = : ProfileType").SetParameter("teamid", team.Id).SetParameter("ProfileType", "facebook_page").List<Domain.Socioboard.Domain.TeamMemberProfile>().ToList();
                    }
                    foreach (Domain.Socioboard.Domain.TeamMemberProfile teammemberprofile in teammemberprofiles)
                    {

                        try
                        {
                            FbProfileId += teammemberprofile.ProfileId + ',';
                            FbProfileIds += "'" + teammemberprofile.ProfileId + "',";

                        }
                        catch (Exception Err)
                        {
                            Console.Write(Err.StackTrace);
                        }
                    }

                    try
                    {
                        AllProfileId = FbProfileId.Substring(0, FbProfileId.Length - 1);
                        AllProfileIds = FbProfileIds.Substring(0, FbProfileIds.Length - 1);

                        ArrProfileId = AllProfileId.Split(',');
                    }
                    catch (Exception Err)
                    {
                        Console.Write(Err.StackTrace);
                    }

                    if (!string.IsNullOrEmpty(AllProfileId))
                    {
                        string strfbfan = "from FacebookFanPage t where t.UserId =: UserId and t.ProfilePageId In(" + AllProfileIds + ")";

                        using (NHibernate.ISession session3 = SessionFactory.GetNewSession())
                        {

                            try
                            {

                                fbfan_90 = session3.CreateQuery(strfbfan)
                                                                      .SetParameter("UserId", Guid.Parse(userid))
                                                                       .List<Domain.Socioboard.Domain.FacebookFanPage>().ToList().Where(x => x.EntryDate <= DateTime.Now && x.EntryDate >= (DateTime.Now.AddDays(-90))).OrderByDescending(y => y.EntryDate).ToList();

                            }
                            catch (Exception)
                            {

                                fbfan_90 = new List<Domain.Socioboard.Domain.FacebookFanPage>();
                            }

                            try
                            {
                                fbfan_15 = fbfan_90.Where(x => x.EntryDate <= DateTime.Now && x.EntryDate >= (DateTime.Now.AddDays(-15))).OrderByDescending(y => y.EntryDate).ToList();

                            }
                            catch (Exception)
                            {
                                fbfan_15 = new List<Domain.Socioboard.Domain.FacebookFanPage>();
                            }


                            try
                            {
                                fbfan_30 = fbfan_90.Where(x => x.EntryDate <= DateTime.Now && x.EntryDate >= (DateTime.Now.AddDays(-30))).OrderByDescending(y => y.EntryDate).ToList();
                            }
                            catch (Exception)
                            {
                                fbfan_30 = new List<Domain.Socioboard.Domain.FacebookFanPage>();
                            }

                            try
                            {
                                fbfan_60 = fbfan_90.Where(x => x.EntryDate <= DateTime.Now && x.EntryDate >= (DateTime.Now.AddDays(-60))).OrderByDescending(y => y.EntryDate).ToList();
                            }
                            catch (Exception)
                            {
                                fbfan_60 = new List<Domain.Socioboard.Domain.FacebookFanPage>();
                            }
                        }

                        present_date = DateTime.Now;

                        while (present_date.Date != DateTime.Now.Date.AddDays(-90))
                        {
                            perdayalldata = 0;
                            foreach (string profileid in ArrProfileId)
                            {
                                try
                                {
                                    using (NHibernate.ISession session4 = SessionFactory.GetNewSession())
                                    {
                                        List<Domain.Socioboard.Domain.FacebookFanPage> lstresult = fbfan_90.Where(t => t.EntryDate >= present_date.Date.AddSeconds(1) && t.EntryDate <= present_date.Date.AddDays(1).AddSeconds(-1)).OrderByDescending(t => t.EntryDate).ToList();

                                        if (lstresult.Count > 0)
                                        {
                                            Domain.Socioboard.Domain.FacebookFanPage _result = lstresult.First();
                                            Domain.Socioboard.Domain.FacebookFanPage _result1 = lstresult.Last();

                                            long add_data = long.Parse(_result.FanpageCount);

                                            long add_data1 = long.Parse(_result1.FanpageCount);

                                            long newfollowerdata = add_data - add_data1;

                                            perdayalldata += newfollowerdata;
                                        }
                                        else
                                        {
                                            perdayalldata += 0;
                                        }

                                    }
                                }

                                catch (Exception e)
                                {
                                    logger.Error("perdayfberror >>" + e);
                                    Console.Write(e.StackTrace);
                                }
                            }
                            arr_perday[i] = perdayalldata;
                            i++;

                            logger.Error("perdayfb>>" + perday_90);
                            present_date = present_date.AddDays(-1);
                        }
                        for (i = 0; i < 90; i++)
                        {
                            perday_90 = perday_90 + arr_perday[i].ToString() + ",";
                        }
                        try
                        {
                            string[] arr = perday_90.Split(',');
                            for (i = 0; i < 15; i++)
                            {
                                perday_15 = perday_15 + arr[i] + ",";
                            }
                            for (i = 0; i < 30; i++)
                            {
                                perday_30 = perday_30 + arr[i] + ",";
                            }
                            for (i = 0; i < 60; i++)
                            {
                                perday_60 = perday_60 + arr[i] + ",";
                            }
                        }
                        catch (Exception)
                        {


                        }
                        if (fbfan_15.Count > 0)
                        {
                            _15_first = fbfan_15.First();
                            _15_last = fbfan_15.Last();
                            r._15 = long.Parse(_15_first.FanpageCount) - long.Parse(_15_last.FanpageCount);
                        }
                        else
                        {
                            r._15 = 0;
                        }

                        if (fbfan_30.Count > 0)
                        {
                            _30_first = fbfan_30.First();
                            _30_last = fbfan_30.Last();
                            r._30 = long.Parse(_30_first.FanpageCount) - long.Parse(_30_last.FanpageCount);
                        }
                        else
                        {
                            r._30 = 0;
                        }

                        if (fbfan_60.Count > 0)
                        {
                            _60_first = fbfan_60.First();
                            _60_last = fbfan_60.Last();
                            r._60 = long.Parse(_60_first.FanpageCount) - long.Parse(_60_last.FanpageCount);
                        }
                        else
                        {
                            r._60 = 0;
                        }

                        if (fbfan_90.Count > 0)
                        {
                            _90_first = fbfan_90.First();
                            _90_last = fbfan_90.Last();
                            r._90 = long.Parse(_90_first.FanpageCount) - long.Parse(_90_last.FanpageCount);
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
                }
            }
            catch (Exception e)
            {
                logger.Error(e.Message);
                logger.Error(e.StackTrace);
            }
            string ret = new JavaScriptSerializer().Serialize(r);
            return ret;
        }

        [WebMethod]
        public void insertdata(string i)
        {

            Domain.Socioboard.Domain.GroupReports insert = (Domain.Socioboard.Domain.GroupReports)new JavaScriptSerializer().Deserialize(i, typeof(Domain.Socioboard.Domain.GroupReports));

            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                try
                {
                    //After Session creation, start Transaction. 
                    using (NHibernate.ITransaction transaction = session.BeginTransaction())
                    {
                        //Proceed action, to save data.
                        bool exist = session.Query<Domain.Socioboard.Domain.GroupReports>()
                           .Any(x => x.GroupId == insert.GroupId);
                        if (exist)
                        {
                            int _i = session.CreateQuery("Update GroupReports set inbox_15 = : inbox_15 , perday_inbox_15 = : perday_inbox_15 , inbox_30 = : inbox_30 , perday_inbox_30 = : perday_inbox_30 , inbox_60 = : inbox_60 , perday_inbox_60 = : perday_inbox_60 , inbox_90 = : inbox_90 , perday_inbox_90 = : perday_inbox_90 , sent_15 = : sent_15 , perday_sent_15 = : perday_sent_15 , sent_30 = : sent_30 , perday_sent_30 = : perday_sent_30 , sent_60 = : sent_60 , perday_sent_60 = : perday_sent_60 , sent_90 = : sent_90 , perday_sent_90 = : perday_sent_90 , twitterfollower_15 = : twitterfollower_15 , perday_twitterfollower_15 = : perday_twitterfollower_15 , twitterfollower_30 = : twitterfollower_30 , perday_twitterfollower_30 = : perday_twitterfollower_30 , twitterfollower_60 = : twitterfollower_60 , perday_twitterfollower_60 = : perday_twitterfollower_60 , twitterfollower_90 = : twitterfollower_90 , perday_twitterfollower_90 = : perday_twitterfollower_90 , fbfan_15 = : fbfan_15 , perday_fbfan_15 = : perday_fbfan_15 , fbfan_30 = : fbfan_30 , perday_fbfan_30 = : perday_fbfan_30 , fbfan_60 = : fbfan_60 , perday_fbfan_60 = : perday_fbfan_60 , fbfan_90 = : fbfan_90 , perday_fbfan_90 = : perday_fbfan_90 , interaction_15 = : interaction_15 , perday_interaction_15 = : perday_interaction_15 , interaction_30 = : interaction_30 , perday_interaction_30 = : perday_interaction_30 , interaction_60 = : interaction_60 , perday_interaction_60 = : perday_interaction_60 , interaction_90 = : interaction_90 , perday_interaction_90 = : perday_interaction_90 , twtmentions_15 = : twtmentions_15 , perday_twtmentions_15 = : perday_twtmentions_15 , twtmentions_30 = : twtmentions_30 , perday_twtmentions_30 = : perday_twtmentions_30 , twtmentions_60 = : twtmentions_60 , perday_twtmentions_60 = : perday_twtmentions_60 , twtmentions_90 = : twtmentions_90 , perday_twtmentions_90 = : perday_twtmentions_90 , twtretweets_15 = : twtretweets_15 , perday_twtretweets_15 = : perday_twtretweets_15 , twtretweets_30 = : twtretweets_30 , perday_twtretweets_30 = : perday_twtretweets_30 , twtretweets_60 = : twtretweets_60 , perday_twtretweets_60 = : perday_twtretweets_60 , twtretweets_90 = : twtretweets_90 , perday_twtretweets_90 = : perday_twtretweets_90 , sexratio = : sexratio , uniqueusers_15 = : uniqueusers_15 , perday_uniqueusers_15 = : perday_uniqueusers_15 , uniqueusers_30 = : uniqueusers_30 , perday_uniqueusers_30 = : perday_uniqueusers_30 , uniqueusers_60 = : uniqueusers_60 , perday_uniqueusers_60 = : perday_uniqueusers_60 , uniqueusers_90 = : uniqueusers_90 , perday_uniqueusers_90 = : perday_uniqueusers_90 , twitter_account_count = : twitter_account_count where GroupId = : GroupId")
                                .SetParameter("inbox_15", insert.inbox_15)
                                .SetParameter("perday_inbox_15", insert.perday_inbox_15)

                                .SetParameter("inbox_30", insert.inbox_30)
                                .SetParameter("perday_inbox_30", insert.perday_inbox_30)

                                .SetParameter("inbox_60", insert.inbox_60)
                                .SetParameter("perday_inbox_60", insert.perday_inbox_60)

                                .SetParameter("inbox_90", insert.inbox_90)
                                .SetParameter("perday_inbox_90", insert.perday_inbox_90)

                                .SetParameter("sent_15", insert.sent_15)
                                 .SetParameter("perday_sent_15", insert.perday_sent_15)

                                .SetParameter("sent_30", insert.sent_30)
                                .SetParameter("perday_sent_30", insert.perday_sent_30)

                                .SetParameter("sent_60", insert.sent_60)
                                .SetParameter("perday_sent_60", insert.perday_sent_60)

                                .SetParameter("sent_90", insert.sent_90)
                                .SetParameter("perday_sent_90", insert.perday_sent_90)

                                .SetParameter("twitterfollower_15", insert.twitterfollower_15)
                                .SetParameter("perday_twitterfollower_15", insert.perday_twitterfollower_15)

                                .SetParameter("twitterfollower_30", insert.twitterfollower_30)
                                .SetParameter("perday_twitterfollower_30", insert.perday_twitterfollower_30)

                                .SetParameter("twitterfollower_60", insert.twitterfollower_60)
                                   .SetParameter("perday_twitterfollower_60", insert.perday_twitterfollower_60)

                                .SetParameter("twitterfollower_90", insert.twitterfollower_90)
                                   .SetParameter("perday_twitterfollower_90", insert.perday_twitterfollower_90)

                                .SetParameter("fbfan_15", insert.fbfan_15)
                                .SetParameter("perday_fbfan_15", insert.perday_fbfan_15)

                                .SetParameter("fbfan_30", insert.fbfan_30)
                                .SetParameter("perday_fbfan_30", insert.perday_fbfan_30)

                                .SetParameter("fbfan_60", insert.fbfan_60)
                                .SetParameter("perday_fbfan_60", insert.perday_fbfan_60)

                                .SetParameter("fbfan_90", insert.fbfan_90)
                                .SetParameter("perday_fbfan_90", insert.perday_fbfan_90)

                                 .SetParameter("interaction_15", insert.interaction_15)
                                 .SetParameter("perday_interaction_15", insert.perday_interaction_15)

                                 .SetParameter("interaction_30", insert.interaction_30)
                                 .SetParameter("perday_interaction_30", insert.perday_interaction_30)

                                 .SetParameter("interaction_60", insert.interaction_60)
                                 .SetParameter("perday_interaction_60", insert.perday_interaction_60)

                                 .SetParameter("interaction_90", insert.interaction_90)
                                 .SetParameter("perday_interaction_90", insert.perday_interaction_90)

                                 .SetParameter("twtmentions_15", insert.twtmentions_15)
                                 .SetParameter("perday_twtmentions_15", insert.perday_twtmentions_15)

                                 .SetParameter("twtmentions_30", insert.twtmentions_30)
                                 .SetParameter("perday_twtmentions_30", insert.perday_twtmentions_30)

                                 .SetParameter("twtmentions_60", insert.twtmentions_60)
                                 .SetParameter("perday_twtmentions_60", insert.perday_twtmentions_60)

                                 .SetParameter("twtmentions_90", insert.twtmentions_90)
                                 .SetParameter("perday_twtmentions_90", insert.perday_twtmentions_90)

                                 .SetParameter("twtretweets_15", insert.twtretweets_15)
                                    .SetParameter("perday_twtretweets_15", insert.perday_twtretweets_15)

                                 .SetParameter("twtretweets_30", insert.twtretweets_30)
                                    .SetParameter("perday_twtretweets_30", insert.perday_twtretweets_30)

                                 .SetParameter("twtretweets_60", insert.twtretweets_60)
                                 .SetParameter("perday_twtretweets_60", insert.perday_twtretweets_60)

                                 .SetParameter("twtretweets_90", insert.twtretweets_90)
                                .SetParameter("perday_twtretweets_90", insert.perday_twtretweets_90)
                              .SetParameter("sexratio", insert.sexratio)

                              .SetParameter("uniqueusers_15", insert.uniqueusers_15)
                              .SetParameter("perday_uniqueusers_15", insert.perday_uniqueusers_15)

                                  .SetParameter("uniqueusers_30", insert.uniqueusers_30)
                              .SetParameter("perday_uniqueusers_30", insert.perday_uniqueusers_30)

                                  .SetParameter("uniqueusers_60", insert.uniqueusers_60)
                              .SetParameter("perday_uniqueusers_60", insert.perday_uniqueusers_60)

                          .SetParameter("uniqueusers_90", insert.uniqueusers_90)
                              .SetParameter("perday_uniqueusers_90", insert.perday_uniqueusers_90)
                              .SetParameter("twitter_account_count", insert.twitter_account_count)
                          .SetParameter("GroupId", insert.GroupId)
                              .ExecuteUpdate();
                            transaction.Commit();
                            logger.Error("inserteddata>> Update");


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
        public string getinteractions(string groupid, string userid)
        {

            Domain.Socioboard.Domain.ReturnData r = new Domain.Socioboard.Domain.ReturnData();
            long _15 = 0;
            long _30 = 0;
            long _60 = 0;
            long _90 = 0;
            DateTime present_date = DateTime.Now;
            string perday_15 = string.Empty;
            string perday_30 = string.Empty;
            string perday_60 = string.Empty;
            string perday_90 = string.Empty;
            string AllProfileId = string.Empty;
            string AllProfileIds = string.Empty;

            string FbProfileId = string.Empty;
            string TwtProfileId = string.Empty;
            string[] ArrProfileId = { };
            List<Domain.Socioboard.Domain.Team> teams;
            List<Domain.Socioboard.Domain.TeamMemberProfile> teammemberprofiles;
            List<Domain.Socioboard.Domain.InboxMessages> inboxmessages_15;
            List<Domain.Socioboard.Domain.InboxMessages> inboxmessages_30;
            List<Domain.Socioboard.Domain.InboxMessages> inboxmessages_60;
            List<Domain.Socioboard.Domain.InboxMessages> inboxmessages_90;
            List<Domain.Socioboard.Domain.TwitterDirectMessages> twitterdirectmessages_15;
            List<Domain.Socioboard.Domain.TwitterDirectMessages> twitterdirectmessages_30;
            List<Domain.Socioboard.Domain.TwitterDirectMessages> twitterdirectmessages_60;
            List<Domain.Socioboard.Domain.TwitterDirectMessages> twitterdirectmessages_90;

            string ret_string = string.Empty;

            try
            {

                using (NHibernate.ISession session1 = SessionFactory.GetNewSession())
                {
                    teams = session1.CreateQuery("from Team t where t.GroupId = : groupid and t.UserId = : userid ").SetParameter("groupid", Guid.Parse(groupid)).SetParameter("userid", Guid.Parse(userid)).List<Domain.Socioboard.Domain.Team>().ToList();
                }
                foreach (Domain.Socioboard.Domain.Team team in teams)
                {
                    using (NHibernate.ISession session2 = SessionFactory.GetNewSession())
                    {
                        teammemberprofiles = session2.CreateQuery("from TeamMemberProfile t where t.TeamId = : teamid ").SetParameter("teamid", team.Id).List<Domain.Socioboard.Domain.TeamMemberProfile>().ToList();
                    }
                    foreach (Domain.Socioboard.Domain.TeamMemberProfile teammemberprofile in teammemberprofiles)
                    {
                        try
                        {
                            AllProfileId += teammemberprofile.ProfileId + ',';
                            AllProfileIds += "'" + teammemberprofile.ProfileId + "',";
                        }
                        catch (Exception Err)
                        {
                            Console.Write(Err.StackTrace);
                        }
                    }
                    try
                    {
                        AllProfileId = AllProfileId.Substring(0, AllProfileId.Length - 1);
                        AllProfileIds = AllProfileIds.Substring(0, AllProfileIds.Length - 1);
                        ArrProfileId = AllProfileId.Split(',');
                    }
                    catch (Exception Err)
                    {
                        Console.Write(Err.StackTrace);
                    }
                    if (!string.IsNullOrEmpty(AllProfileId))
                    {
                        string strinboxmessages = "from InboxMessages t where t.UserId =: UserId and t.MessageType = : msgtype and t.RecipientId In(" + AllProfileIds + ")";
                        using (NHibernate.ISession session3 = SessionFactory.GetNewSession())
                        {
                           
                            inboxmessages_90 = session3.CreateQuery(strinboxmessages)
                                                                          .SetParameter("UserId", Guid.Parse(userid))
                                                                          .SetParameter("msgtype", "twt_retweet")
                                                                                       .List<Domain.Socioboard.Domain.InboxMessages>().ToList().Where(x => x.CreatedTime <= DateTime.Now.AddDays(1).Date.AddSeconds(-1) && x.CreatedTime > (DateTime.Now.AddDays(-90).Date)).ToList();

                            inboxmessages_60 = inboxmessages_90.Where(x => x.CreatedTime <= DateTime.Now.AddDays(1).Date.AddSeconds(-1) && x.CreatedTime > (DateTime.Now.AddDays(-60).Date)).ToList();

                            inboxmessages_30 = inboxmessages_90.Where(x => x.CreatedTime <= DateTime.Now.AddDays(1).Date.AddSeconds(-1) && x.CreatedTime > (DateTime.Now.AddDays(-30).Date)).ToList();

                            inboxmessages_15 = inboxmessages_90.Where(x => x.CreatedTime <= DateTime.Now.AddDays(1).Date.AddSeconds(-1) && x.CreatedTime > (DateTime.Now.AddDays(-15).Date)).ToList();

                        }
                        string strtwitterdirectmessages = "from TwitterDirectMessages t where t.UserId =: UserId and t.SenderId In(" + AllProfileIds + ")";
                        using (NHibernate.ISession session4 = SessionFactory.GetNewSession())
                        {
                                                    
                            twitterdirectmessages_90 = session4.CreateQuery(strtwitterdirectmessages).SetParameter("UserId", Guid.Parse(userid))
                                                                                       .List<Domain.Socioboard.Domain.TwitterDirectMessages>().ToList().Where(x => x.CreatedDate <= DateTime.Now.AddDays(1).Date.AddSeconds(-1) && x.CreatedDate > (DateTime.Now.AddDays(-90).Date)).ToList();

                            twitterdirectmessages_60=twitterdirectmessages_90.Where(x => x.CreatedDate <= DateTime.Now.AddDays(1).Date.AddSeconds(-1) && x.CreatedDate > (DateTime.Now.AddDays(-60).Date)).ToList();

                            twitterdirectmessages_30=twitterdirectmessages_90.Where(x => x.CreatedDate <= DateTime.Now.AddDays(1).Date.AddSeconds(-1) && x.CreatedDate > (DateTime.Now.AddDays(-30).Date)).ToList();

                            twitterdirectmessages_15=twitterdirectmessages_90.Where(x => x.CreatedDate <= DateTime.Now.AddDays(1).Date.AddSeconds(-1) && x.CreatedDate > (DateTime.Now.AddDays(-15).Date)).ToList();

                        }

                        present_date = DateTime.Now;

                        while (present_date.Date != DateTime.Now.Date.AddDays(-90))
                        {
                            try
                            {
                                List<Domain.Socioboard.Domain.InboxMessages> _inboxmessages;
                                List<Domain.Socioboard.Domain.TwitterDirectMessages> _twitterdirectmessages;
                                using (NHibernate.ISession session5 = SessionFactory.GetNewSession())
                                {
                                    int item = 0;
                                    int item1 = 0;
                                    try
                                    {
                                        //item = session5.QueryOver<Domain.Socioboard.Domain.InboxMessages>().Where(m => m.CreatedTime >= present_date.Date.AddSeconds(1) && m.CreatedTime <= present_date.AddDays(1).Date.AddSeconds(-1) && m.MessageType == "twt_retweet" && m.UserId == Guid.Parse(userid)).WhereRestrictionOn(S => S.RecipientId).IsIn(ArrProfileId)
                                        //                              .FutureValue<int>().Value;

                                        _inboxmessages = inboxmessages_90.Where(m => m.CreatedTime >= present_date.Date.AddSeconds(1) && m.CreatedTime <= present_date.AddDays(1).Date.AddSeconds(-1)).ToList();

                                        item = _inboxmessages.Count;
                                    }
                                    catch (Exception e)
                                    {
                                        item = 0;
                                    }

                                    try
                                    {
                                        //item1 = session5.QueryOver<Domain.Socioboard.Domain.TwitterDirectMessages>().Where(m => m.CreatedDate >= present_date.Date.AddSeconds(1) && m.CreatedDate <= present_date.AddDays(1).Date.AddSeconds(-1) && m.UserId == Guid.Parse(userid)).WhereRestrictionOn(S => S.SenderId).IsIn(ArrProfileId)
                                        //                              .Select(Projections.CountDistinct<Domain.Socioboard.Domain.TwitterDirectMessages>(x => x.MessageId))
                                        //                              .FutureValue<int>().Value;

                                        _twitterdirectmessages = twitterdirectmessages_90.Where(m => m.CreatedDate >= present_date.Date.AddSeconds(1) && m.CreatedDate <= present_date.AddDays(1).Date.AddSeconds(-1)).ToList();

                                        item1 = _twitterdirectmessages.Count;
                                    }
                                    catch (Exception e)
                                    {
                                        item1 = 0;
                                    }

                                    int add_data = item + item1;

                                    perday_90 = perday_90 + add_data.ToString() + ",";

                                    present_date = present_date.AddDays(-1);
                                    //logger.Error("perdayinteraction>>" + perday_90);
                                }
                            }
                            catch (Exception e)
                            {
                                logger.Error("perdayintrerror >>" + e.Message);
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
                        
                        r._15 = twitterdirectmessages_15.Count + inboxmessages_15.Count;
                        r._30 = twitterdirectmessages_30.Count + inboxmessages_30.Count;
                        r._60 = twitterdirectmessages_60.Count + inboxmessages_60.Count;
                        r._90 = twitterdirectmessages_90.Count + inboxmessages_90.Count;

                        r.perday_15 = perday_15;
                        r.perday_30 = perday_30;
                        r.perday_60 = perday_60;
                        r.perday_90 = perday_90;
                        
                    }

                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
            }

            ret_string = new JavaScriptSerializer().Serialize(r);
            return ret_string;
        }


        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string retrievedata(string groupid)
        {
            string ret_string = string.Empty;
            Domain.Socioboard.Domain.GroupReports ret = new Domain.Socioboard.Domain.GroupReports();
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {

                try
                {

                    NHibernate.IQuery retrieve = session.CreateQuery("from GroupReports where GroupId =: grpid").SetParameter("grpid", Guid.Parse(groupid));
                    ret = (Domain.Socioboard.Domain.GroupReports)retrieve.UniqueResult();
                    if (ret == null)
                    {
                        ret = new Domain.Socioboard.Domain.GroupReports();
                    }
                    ret_string = new JavaScriptSerializer().Serialize(ret);

                }
                catch (Exception e)
                {
                    logger.Error("noretrievedata>>");
                    Console.WriteLine(e.StackTrace);
                    logger.Error(e.Message);
                    logger.Error(e.StackTrace);
                    ret = new Domain.Socioboard.Domain.GroupReports();
                    ret_string = new JavaScriptSerializer().Serialize(ret);
                }

            }
            return ret_string;
        }



        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string gettwtmentions(string groupid, string userid)
        {
            Domain.Socioboard.Domain.ReturnData r = new Domain.Socioboard.Domain.ReturnData();
            long _15 = 0;
            long _30 = 0;
            long _60 = 0;
            long _90 = 0;
            DateTime present_date = DateTime.Now;
            string perday_15 = string.Empty;
            string perday_30 = string.Empty;
            string perday_60 = string.Empty;
            string perday_90 = string.Empty;

            string AllProfileId = string.Empty;
            string FbProfileId = string.Empty;
            string TwtProfileId = string.Empty;
            string[] ArrProfileId = { };
            List<Domain.Socioboard.Domain.Team> teams;
            List<Domain.Socioboard.Domain.TeamMemberProfile> teammemberprofiles;
            List<Domain.Socioboard.Domain.InboxMessages> inboxmessages_15;
            List<Domain.Socioboard.Domain.InboxMessages> inboxmessages_30;
            List<Domain.Socioboard.Domain.InboxMessages> inboxmessages_60;
            List<Domain.Socioboard.Domain.InboxMessages> inboxmessages_90;

            string ret_string = string.Empty;
            try
            {
                using (NHibernate.ISession session1 = SessionFactory.GetNewSession())
                {
                    teams = session1.CreateQuery("from Team t where t.GroupId = : groupid and t.UserId = : userid ").SetParameter("groupid", Guid.Parse(groupid)).SetParameter("userid", Guid.Parse(userid)).List<Domain.Socioboard.Domain.Team>().ToList();

                }
                foreach (Domain.Socioboard.Domain.Team team in teams)
                {
                    using (NHibernate.ISession session2 = SessionFactory.GetNewSession())
                    {
                        teammemberprofiles = session2.CreateQuery("from TeamMemberProfile t where t.TeamId = : teamid and t.ProfileType =: ProfileType").SetParameter("ProfileType", "twitter").SetParameter("teamid", team.Id).List<Domain.Socioboard.Domain.TeamMemberProfile>().ToList();
                    }

                    foreach (Domain.Socioboard.Domain.TeamMemberProfile teammemberprofile in teammemberprofiles)
                    {
                        try
                        {
                            AllProfileId += teammemberprofile.ProfileId + ',';
                        }
                        catch (Exception Err)
                        {
                            Console.Write(Err.StackTrace);
                        }
                    }

                    try
                    {
                        AllProfileId = AllProfileId.Substring(0, AllProfileId.Length - 1);
                        ArrProfileId = AllProfileId.Split(',');
                    }
                    catch (Exception Err)
                    {
                        Console.Write(Err.StackTrace);
                    }
                    if (!string.IsNullOrEmpty(AllProfileId))
                    {
                        string strinboxmessages = "from InboxMessages t where t.UserId =: UserId and t.MessageType = : msgtype and t.RecipientId In(" + AllProfileId + ")";

                        using (NHibernate.ISession session3 = SessionFactory.GetNewSession())
                        {
                            
                            inboxmessages_90 = session3.CreateQuery(strinboxmessages)
                                                                                 .SetParameter("UserId", Guid.Parse(userid))
                                                                                 .SetParameter("msgtype", "twt_mention")
                                                                                 .List<Domain.Socioboard.Domain.InboxMessages>().ToList().Where(x => x.CreatedTime <= DateTime.Now.Date.AddSeconds(-1) && x.CreatedTime > (DateTime.Now.AddDays(-90).Date)).ToList();

                            inboxmessages_60 = inboxmessages_90.Where(x => x.CreatedTime <= DateTime.Now.Date.AddSeconds(-1) && x.CreatedTime > (DateTime.Now.AddDays(-60).Date)).ToList();

                            inboxmessages_30 = inboxmessages_90.Where(x => x.CreatedTime <= DateTime.Now.Date.AddSeconds(-1) && x.CreatedTime > (DateTime.Now.AddDays(-30).Date)).ToList();

                            inboxmessages_15 = inboxmessages_90.Where(x => x.CreatedTime <= DateTime.Now.Date.AddSeconds(-1) && x.CreatedTime > (DateTime.Now.AddDays(-15).Date)).ToList();


                        }


                        present_date = DateTime.Now;

                        while (present_date.Date != DateTime.Now.Date.AddDays(-90))
                        {

                            try
                            {
                                List<Domain.Socioboard.Domain.InboxMessages> _inboxmessages;
                                using (NHibernate.ISession session4 = SessionFactory.GetNewSession())
                                {
                                    int item = 0;

                                    try
                                    {
                                        //item = session4.QueryOver<Domain.Socioboard.Domain.InboxMessages>().Where(m => m.CreatedTime >= present_date.Date.AddSeconds(1) && m.CreatedTime <= present_date.AddDays(1).Date.AddSeconds(-1) && m.MessageType == "twt_mention").WhereRestrictionOn(S => S.RecipientId).IsIn(ArrProfileId)
                                        //                         .Select(Projections.CountDistinct<Domain.Socioboard.Domain.InboxMessages>(x => x.MessageId))
                                        //                         .FutureValue<int>().Value;

                                        _inboxmessages = inboxmessages_90.Where(m => m.CreatedTime >= present_date.Date.AddSeconds(1) && m.CreatedTime <= present_date.AddDays(1).Date.AddSeconds(-1)).ToList();
                                        item = _inboxmessages.Count;
                                    }
                                    catch (Exception e)
                                    {
                                        item = 0;
                                    }

                                    int add_data = item;

                                    perday_90 = perday_90 + add_data.ToString() + ",";

                                    present_date = present_date.AddDays(-1);
                                    //logger.Error("perdayinbox >>" + perday_90);
                                }
                            }
                            catch (Exception e)
                            {
                                logger.Error("perdayinboxerror >>" + e);
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

                        r._15 = inboxmessages_15.Count;
                        r._30 = inboxmessages_30.Count;
                        r._60 = inboxmessages_60.Count;
                        r._90 = inboxmessages_90.Count;
                        r.perday_15 = perday_15;
                        r.perday_30 = perday_30;
                        r.perday_60 = perday_60;
                        r.perday_90 = perday_90;

                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
            }
            ret_string = new JavaScriptSerializer().Serialize(r);
            return ret_string;
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string gettwtretweets(string groupid, string userid)
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
            List<Domain.Socioboard.Domain.Team> teams;
            List<Domain.Socioboard.Domain.TeamMemberProfile> teammemberprofiles;
            List<Domain.Socioboard.Domain.InboxMessages> inboxmessages_15;
            List<Domain.Socioboard.Domain.InboxMessages> inboxmessages_30;
            List<Domain.Socioboard.Domain.InboxMessages> inboxmessages_60;
            List<Domain.Socioboard.Domain.InboxMessages> inboxmessages_90;

            string ret_string = string.Empty;
            try
            {
                using (NHibernate.ISession session1 = SessionFactory.GetNewSession())
                {
                    teams = session1.CreateQuery("from Team t where t.GroupId = : groupid and t.UserId = : userid ").SetParameter("groupid", Guid.Parse(groupid)).SetParameter("userid", Guid.Parse(userid)).List<Domain.Socioboard.Domain.Team>().ToList();
                }
                foreach (Domain.Socioboard.Domain.Team team in teams)
                {
                    using (NHibernate.ISession session2 = SessionFactory.GetNewSession())
                    {
                        teammemberprofiles = session2.CreateQuery("from TeamMemberProfile t where t.TeamId = : teamid and t.ProfileType =: ProfileType").SetParameter("ProfileType", "twitter").SetParameter("teamid", team.Id).List<Domain.Socioboard.Domain.TeamMemberProfile>().ToList();
                    }
                    foreach (Domain.Socioboard.Domain.TeamMemberProfile teammemberprofile in teammemberprofiles)
                    {
                        try
                        {
                            AllProfileId += teammemberprofile.ProfileId + ',';
                        }
                        catch (Exception Err)
                        {
                            Console.Write(Err.StackTrace);
                        }
                    }
                    try
                    {
                        AllProfileId = AllProfileId.Substring(0, AllProfileId.Length - 1);
                        ArrProfileId = AllProfileId.Split(',');
                    }
                    catch (Exception Err)
                    {
                        Console.Write(Err.StackTrace);
                    }
                    if (!string.IsNullOrEmpty(AllProfileId))
                    {
                        string strinboxmessages = "from InboxMessages t where t.UserId =: UserId and t.MessageType = : msgtype and t.RecipientId In(" + AllProfileId + ")";

                        using (NHibernate.ISession session3 = SessionFactory.GetNewSession())
                        {
                            
                            inboxmessages_90 = session3.CreateQuery(strinboxmessages)
                                                                                 .SetParameter("UserId", Guid.Parse(userid))
                                                                                 .SetParameter("msgtype", "twt_retweet")
                                                                                 .List<Domain.Socioboard.Domain.InboxMessages>().ToList().Where(x => x.CreatedTime <= DateTime.Now.AddDays(1).Date.AddSeconds(-1) && x.CreatedTime > (DateTime.Now.AddDays(-90).Date)).ToList();

                            inboxmessages_60 = inboxmessages_90.Where(x => x.CreatedTime <= DateTime.Now.AddDays(1).Date.AddSeconds(-1) && x.CreatedTime > (DateTime.Now.AddDays(-60).Date)).ToList();

                            inboxmessages_30 = inboxmessages_90.Where(x => x.CreatedTime <= DateTime.Now.AddDays(1).Date.AddSeconds(-1) && x.CreatedTime > (DateTime.Now.AddDays(-30).Date)).ToList();

                            inboxmessages_15 = inboxmessages_90.Where(x => x.CreatedTime <= DateTime.Now.AddDays(1).Date.AddSeconds(-1) && x.CreatedTime > (DateTime.Now.AddDays(-15).Date)).ToList();

                        }

                        present_date = DateTime.Now;

                        while (present_date.Date != DateTime.Now.Date.AddDays(-90))
                        {
                            try
                            {
                                List<Domain.Socioboard.Domain.InboxMessages> _inboxmessages;
                                using (NHibernate.ISession session4 = SessionFactory.GetNewSession())
                                {
                                    int item = 0;
                                    try
                                    {
                                        //item = session4.QueryOver<Domain.Socioboard.Domain.InboxMessages>().Where(m => m.CreatedTime > present_date.Date && m.CreatedTime <= present_date.AddDays(1).Date.AddSeconds(-1) && m.MessageType == "twt_retweet" && m.UserId == Guid.Parse(userid)).WhereRestrictionOn(S => S.RecipientId).IsIn(ArrProfileId)
                                        //                             .FutureValue<int>().Value;
                                        _inboxmessages = inboxmessages_90.Where(m => m.CreatedTime > present_date.Date && m.CreatedTime <= present_date.AddDays(1).Date.AddSeconds(-1)).ToList();

                                        item = _inboxmessages.Count;
                                    }
                                    catch (Exception e)
                                    {
                                        item = 0;
                                    }
                                    int add_data = item;
                                    perday_90 = perday_90 + add_data.ToString() + ",";

                                    present_date = present_date.AddDays(-1);
                                    logger.Error("perdayinbox >>" + perday_90);
                                }
                            }
                            catch (Exception e)
                            {
                                logger.Error("perdayinboxerror >>" + e);

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

                        r._15 = inboxmessages_15.Count;
                        r._30 = inboxmessages_30.Count;
                        r._60 = inboxmessages_60.Count;
                        r._90 = inboxmessages_90.Count;
                        r.perday_15 = perday_15;
                        r.perday_30 = perday_30;
                        r.perday_60 = perday_60;
                        r.perday_90 = perday_90;

                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
            }
            ret_string = new JavaScriptSerializer().Serialize(r);
            return ret_string;


        }



        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string uniquetwitteruser(string groupid, string userid)
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
            List<Domain.Socioboard.Domain.Team> teams;
            List<Domain.Socioboard.Domain.TeamMemberProfile> teammemberprofiles;

            List<Domain.Socioboard.Domain.InboxMessages> inboxmessages_15 = new List<Domain.Socioboard.Domain.InboxMessages>();
            List<Domain.Socioboard.Domain.InboxMessages> inboxmessages_30 = new List<Domain.Socioboard.Domain.InboxMessages>();
            List<Domain.Socioboard.Domain.InboxMessages> inboxmessages_60 = new List<Domain.Socioboard.Domain.InboxMessages>();
            List<Domain.Socioboard.Domain.InboxMessages> inboxmessages_90 = new List<Domain.Socioboard.Domain.InboxMessages>();

            int item_15 = 0;
            int item_30 = 0;
            int item_60 = 0;
            int item_90 = 0;

            string ret_string = string.Empty;
            try
            {
                using (NHibernate.ISession session1 = SessionFactory.GetNewSession())
                {
                    teams = session1.CreateQuery("from Team t where t.GroupId = : groupid and t.UserId = : userid ").SetParameter("groupid", Guid.Parse(groupid)).SetParameter("userid", Guid.Parse(userid)).List<Domain.Socioboard.Domain.Team>().ToList();
                }
                foreach (Domain.Socioboard.Domain.Team team in teams)
                {
                    using (NHibernate.ISession session2 = SessionFactory.GetNewSession())
                    {
                        teammemberprofiles = session2.CreateQuery("from TeamMemberProfile t where t.TeamId = : teamid and t.ProfileType =: ProfileType").SetParameter("ProfileType", "twitter").SetParameter("teamid", team.Id).List<Domain.Socioboard.Domain.TeamMemberProfile>().ToList();
                    }

                    foreach (Domain.Socioboard.Domain.TeamMemberProfile teammemberprofile in teammemberprofiles)
                    {
                        try
                        {
                            AllProfileId += teammemberprofile.ProfileId + ',';
                        }
                        catch (Exception Err)
                        {
                            Console.Write(Err.StackTrace);
                        }
                    }
                    try
                    {
                        AllProfileId = AllProfileId.Substring(0, AllProfileId.Length - 1);
                        ArrProfileId = AllProfileId.Split(',');
                    }
                    catch (Exception Err)
                    {
                        Console.Write(Err.StackTrace);
                    }
                    if (!string.IsNullOrEmpty(AllProfileId))
                    {

                        string strinboxmessages = "from InboxMessages t where t.UserId =: UserId and t.MessageType = : msgtype and Staus= and t.RecipientId In(" + AllProfileId + ")";

                        using (NHibernate.ISession session3 = SessionFactory.GetNewSession())
                        {
                            try
                            {
                                inboxmessages_90 = session3.Query<Domain.Socioboard.Domain.InboxMessages>().Where(U => U.Status == 0 && U.CreatedTime < DateTime.Now.AddDays(1).Date && U.CreatedTime >= (DateTime.Now.AddDays(-90).Date.AddSeconds(1)) && ArrProfileId.Contains(U.ProfileId) && U.MessageType == "twt_followers").GroupBy(x => x.FromId).Select(g => g.First()).ToList<Domain.Socioboard.Domain.InboxMessages>();
                                item_90 = inboxmessages_90.Count;
                            }
                            catch (Exception)
                            {

                            }
                            try
                            {
                                inboxmessages_60 = inboxmessages_90.Where(m => m.CreatedTime < present_date.Date && m.CreatedTime >= present_date.AddDays(-60).Date).ToList();
                                item_60 = inboxmessages_60.Count;
                            }
                            catch (Exception)
                            {

                            }
                            try
                            {
                                inboxmessages_30 = inboxmessages_90.Where(m => m.CreatedTime < present_date.Date && m.CreatedTime >= present_date.AddDays(-30).Date).ToList();
                                item_30 = inboxmessages_60.Count;

                            }
                            catch (Exception)
                            {


                            }
                            try
                            {
                                inboxmessages_15 = inboxmessages_90.Where(m => m.CreatedTime < present_date.Date && m.CreatedTime >= present_date.AddDays(-15).Date).ToList();
                                item_15 = inboxmessages_60.Count;
                            }
                            catch (Exception)
                            {
                                
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
                                        //item = session4.QueryOver<Domain.Socioboard.Domain.InboxMessages>().Where(m => m.CreatedTime >= present_date.Date.AddSeconds(1) && m.CreatedTime <= present_date.AddDays(1).Date.AddSeconds(-1) && m.MessageType == "twt_followers").WhereRestrictionOn(S => S.ProfileId).IsIn(ArrProfileId)
                                        //                             .Select(Projections.CountDistinct<Domain.Socioboard.Domain.InboxMessages>(x => x.FromId))
                                        //                             .FutureValue<int>().Value;
                                        _inboxmessages = inboxmessages_90.Where(m => m.CreatedTime >= present_date.Date.AddSeconds(1) && m.CreatedTime <= present_date.AddDays(1).Date.AddSeconds(-1)).ToList();
                                        item = _inboxmessages.Count;

                                    }
                                    catch (Exception e)
                                    {
                                        item = 0;
                                    }
                                    int add_data = item;
                                    perday_90 = perday_90 + add_data.ToString() + ",";

                                    present_date = present_date.AddDays(-1);
                                    logger.Error("perdayuniqueusers >>" + perday_90);
                                }
                            }
                            catch (Exception e)
                            {
                                logger.Error("perdayuniqueuserserror >>" + e.Message);
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
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
            }
            ret_string = new JavaScriptSerializer().Serialize(r);
            return ret_string;
        }

        [WebMethod]
        public string total_twitter_accounts(string groupid, string userid)
        {
            List<Domain.Socioboard.Domain.Team> teams = new List<Domain.Socioboard.Domain.Team>();
            List<Domain.Socioboard.Domain.TeamMemberProfile> teammemberprofiles = new List<Domain.Socioboard.Domain.TeamMemberProfile>();
            string ret_string = string.Empty;

            try
            {
                using (NHibernate.ISession session1 = SessionFactory.GetNewSession())
                {
                    teams = session1.CreateQuery("from Team t where t.GroupId = : groupid and t.UserId = : userid ").SetParameter("groupid", Guid.Parse(groupid)).SetParameter("userid", Guid.Parse(userid)).List<Domain.Socioboard.Domain.Team>().ToList();
                }
                foreach (Domain.Socioboard.Domain.Team team in teams)
                {
                    using (NHibernate.ISession session2 = SessionFactory.GetNewSession())
                    {
                        teammemberprofiles = session2.CreateQuery("from TeamMemberProfile t where t.TeamId = : teamid and t.ProfileType =: ProfileType").SetParameter("ProfileType", "twitter").SetParameter("teamid", team.Id).List<Domain.Socioboard.Domain.TeamMemberProfile>().ToList();
                    }

                }

                ret_string = teammemberprofiles.Count.ToString();

            }
            catch (Exception e)
            {
                logger.Error(e.StackTrace);
                logger.Error(e.Message);
            }

            return ret_string;
            
        }
        [WebMethod]
        public string gettwittersexdivision(string groupid, string userid)
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
                using (NHibernate.ISession session1 = SessionFactory.GetNewSession())
                {
                    teams = session1.CreateQuery("from Team t where t.GroupId = : groupid and t.UserId = : userid ").SetParameter("groupid", Guid.Parse(groupid)).SetParameter("userid", Guid.Parse(userid)).List<Domain.Socioboard.Domain.Team>().ToList();
                }

                foreach (Domain.Socioboard.Domain.Team team in teams)
                {

                    using (NHibernate.ISession session2 = SessionFactory.GetNewSession())
                    {
                        teammemberprofiles = session2.CreateQuery("from TeamMemberProfile t where t.TeamId = : teamid and t.ProfileType =: ProfileType").SetParameter("ProfileType", "twitter").SetParameter("teamid", team.Id).List<Domain.Socioboard.Domain.TeamMemberProfile>().ToList();
                    }

                    foreach (Domain.Socioboard.Domain.TeamMemberProfile teammemberprofile in teammemberprofiles)
                    {
                        try
                        {
                            AllProfileId += teammemberprofile.ProfileId + ',';
                        }
                        catch (Exception Err)
                        {
                            Console.Write(Err.StackTrace);
                        }
                    }

                    try
                    {
                        AllProfileId = AllProfileId.Substring(0, AllProfileId.Length - 1);
                        ArrProfileId = AllProfileId.Split(',');
                    }
                    catch (Exception Err)
                    {
                        Console.Write(Err.StackTrace);
                    }

                    if (!string.IsNullOrEmpty(AllProfileId))
                    {

                        string strtwtfollowernames = "from TwitterFollowerNames t where t.TwitterProfileId In(" + AllProfileId + ")";

                        using (NHibernate.ISession session3 = SessionFactory.GetNewSession())
                        {
                            twtfollowernames = session3.CreateQuery(strtwtfollowernames).List<Domain.Socioboard.Domain.TwitterFollowerNames>().ToList();

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

        [WebMethod]
        public string GetSexRatio(string profileids)
        {
            int male_count = 0;
            int female_count = 0;
            int total = 0;
            List<Domain.Socioboard.Domain.TwitterFollowerNames> twtfollowernames;
            string firstname = string.Empty;
            string ret_string = string.Empty;
            if (!string.IsNullOrEmpty(profileids))
            {

                string strtwtfollowernames = "from TwitterFollowerNames t where t.TwitterProfileId In(" + profileids + ")";

                using (NHibernate.ISession session3 = SessionFactory.GetNewSession())
                {

                    twtfollowernames = session3.CreateQuery(strtwtfollowernames).List<Domain.Socioboard.Domain.TwitterFollowerNames>().ToList();

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

    }
}

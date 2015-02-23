using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using log4net;
using Api.Socioboard.Helper;
using Domain.Socioboard.Domain;
using Domain.Socioboard.Helper;
using NHibernate.Criterion;


namespace Api.Socioboard.Services
{
    public class ScheduledMessageRepository : IScheduledMessageRepository
    {
        ILog logger = LogManager.GetLogger(typeof(ScheduledMessageRepository));
        /// <addNewMessage>
        /// Add New Message
        /// </summary>
        /// <param name="schmesg">Set Values in a ScheduledMessage Class Property and Pass the Object of ScheduledMessage Class.(Domein.ScheduledMessage)</param>
        public void addNewMessage(Domain.Socioboard.Domain.ScheduledMessage schmesg)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    //Proceed action, to save new data.
                    session.Save(schmesg);
                    transaction.Commit();
                }//End Transaction
            }//End Session
        }


        /// <deleteMessage>
        /// Delete Message
        /// </summary>
        /// <param name="schmesg">Set Id in a ScheduledMessage Class Property and Pass the Object of ScheduledMessage Class.(Domein.ScheduledMessage)</param>
        public void deleteMessage(Domain.Socioboard.Domain.ScheduledMessage schmesg)
        {
            //Creates a database connection and opens up a session.
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    //Proceed action, to delete Scheduled message by id.
                    session.CreateQuery("delete from ScheduledMessage where Id = :id")
                        .SetParameter("id", schmesg.Id)
                        .ExecuteUpdate();
                    transaction.Commit();

                }//End Transaction
            }//End Session
        }


        /// <deleteMessage>
        /// Delete Message by id.
        /// </summary>
        /// <param name="Id">Id of scheduled message.(Guid)</param>
        public void deleteMessage(Guid Id)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        // Proceed action to delete scheduled message by Id.
                        int i = session.CreateQuery("delete from ScheduledMessage where Id = :id")
                                  .SetParameter("id", Id)
                                  .ExecuteUpdate();
                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {

                    }
                }//End Transaction
            }//End Session
        }





        public void deleteMessage(Guid Id, string profileid)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        // Proceed action to delete scheduled message by Id.
                        int i = session.CreateQuery("delete from ScheduledMessage where UserId = :id and ProfileId = :Prof")
                                 .SetParameter("id", Id)
                                 .SetParameter("Prof", profileid)
                                 .ExecuteUpdate();
                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);

                    }
                }//End Transaction
            }//End Session
        }


        public List<Domain.Socioboard.Domain.ScheduledMessage> getAllSentMessageDetailsbyDate(Guid userid, string profileid, int days)
        {
            DateTime AssignDate = DateTime.Now;
            DateTime AssinDate = AssignDate.AddDays(-days);//.ToString("yyyy-MM-dd HH:mm:ss");



            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //Begin session trasaction and opens up.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        string str = "from ScheduledMessage where UserId=:userid and ScheduleTime>=:AssinDate and  Status=1 and ProfileId IN(";
                        string[] arrsrt = profileid.Split(',');
                        foreach (string sstr in arrsrt)
                        {
                            str += "'" + (sstr) + "'" + ",";
                        }
                        str = str.Substring(0, str.Length - 1);
                        str += ")";
                        List<Domain.Socioboard.Domain.ScheduledMessage> alst = session.CreateQuery(str).SetParameter("userid", userid).SetParameter("AssinDate", AssinDate)
                       .List<Domain.Socioboard.Domain.ScheduledMessage>()
                       .ToList<Domain.Socioboard.Domain.ScheduledMessage>();
                        return alst;

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                        return null;
                    }

                }//End Trasaction
            }//End session
        }

        //vikash

        public List<Domain.Socioboard.Domain.ScheduledMessage> getAllSentMessageDetailsforADay(Guid userid, string profileid, int days)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //Begin session trasaction and opens up.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        string str = "from ScheduledMessage where UserId=:userid and  Status=1 and ProfileId IN(";
                        string[] arrsrt = profileid.Split(',');
                        foreach (string sstr in arrsrt)
                        {
                            str += "'" + (sstr) + "'" + ",";
                        }
                        str = str.Substring(0, str.Length - 1);
                        str += ") order by ScheduleTime desc";
                        List<Domain.Socioboard.Domain.ScheduledMessage> alst = session.CreateQuery(str).SetParameter("userid", userid)
                       .List<Domain.Socioboard.Domain.ScheduledMessage>()
                       .ToList<Domain.Socioboard.Domain.ScheduledMessage>().Where(a => a.ScheduleTime.Date == DateTime.Now.AddDays(-days).Date).ToList<Domain.Socioboard.Domain.ScheduledMessage>();
                        return alst;

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                        return null;
                    }

                }//End Trasaction
            }//End session
        }

        public List<Domain.Socioboard.Domain.ScheduledMessage> getAllSentMessageDetailsByDays(Guid userid, string profileid, int days)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //Begin session trasaction and opens up.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        string str = "from ScheduledMessage where UserId=:userid and  Status=1 and ProfileId IN(";
                        string[] arrsrt = profileid.Split(',');
                        foreach (string sstr in arrsrt)
                        {
                            str += "'" + (sstr) + "'" + ",";
                        }
                        str = str.Substring(0, str.Length - 1);
                        str += ") order by ScheduleTime desc";
                        List<Domain.Socioboard.Domain.ScheduledMessage> alst = session.CreateQuery(str).SetParameter("userid", userid)
                       .List<Domain.Socioboard.Domain.ScheduledMessage>()
                       .ToList<Domain.Socioboard.Domain.ScheduledMessage>().Where(a => a.ScheduleTime.Date >= DateTime.Now.AddDays(-days).Date).ToList<Domain.Socioboard.Domain.ScheduledMessage>();
                        return alst;

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                        return null;
                    }

                }//End Trasaction
            }//End session
        }

        public List<Domain.Socioboard.Domain.ScheduledMessage> getAllSentMessageDetailsByMonth(Guid userid, string profileid, int month)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //Begin session trasaction and opens up.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        string str = "from ScheduledMessage where UserId=:userid and  Status=1 and ProfileId IN(";
                        string[] arrsrt = profileid.Split(',');
                        foreach (string sstr in arrsrt)
                        {
                            str += "'" + (sstr) + "'" + ",";
                        }
                        str = str.Substring(0, str.Length - 1);
                        str += ") order by ScheduleTime desc";
                        List<Domain.Socioboard.Domain.ScheduledMessage> alst = session.CreateQuery(str).SetParameter("userid", userid)
                       .List<Domain.Socioboard.Domain.ScheduledMessage>()
                       .ToList<Domain.Socioboard.Domain.ScheduledMessage>().Where(a => a.ScheduleTime.Month == DateTime.Now.AddMonths(-month).Month).ToList<Domain.Socioboard.Domain.ScheduledMessage>();
                        return alst;

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                        return null;
                    }

                }//End Trasaction
            }//End session
        }
        public List<Domain.Socioboard.Domain.ScheduledMessage> getAllSentMessageDetailsForCustomrange(Guid userid, string profileid, DateTime sdate, DateTime ldate)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //Begin session trasaction and opens up.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        string str = "from ScheduledMessage where UserId=:userid and  Status=1 and ProfileId IN(";
                        string[] arrsrt = profileid.Split(',');
                        foreach (string sstr in arrsrt)
                        {
                            str += "'" + (sstr) + "'" + ",";
                        }
                        str = str.Substring(0, str.Length - 1);
                        str += ") order by ScheduleTime desc";
                        List<Domain.Socioboard.Domain.ScheduledMessage> alst = session.CreateQuery(str).SetParameter("userid", userid)
                       .List<Domain.Socioboard.Domain.ScheduledMessage>()
                       .ToList<Domain.Socioboard.Domain.ScheduledMessage>().Where(a => a.ScheduleTime.Date >= sdate && a.ScheduleTime.Date <= ldate).ToList<Domain.Socioboard.Domain.ScheduledMessage>();
                        return alst;

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                        return null;
                    }

                }//End Trasaction
            }//End session
        }
    
        /// <updateMessage>
        /// Update Message status by id.
        /// </summary>
        /// <param name="id">Id of scheduler. (Guid)</param>
        public void updateMessage(Guid id)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        //Proceed action, to Update status of scheduled message.
                        NHibernate.IQuery query = session.CreateQuery("update ScheduledMessage set Status = 1 where Id = :id");
                        query.SetParameter("id", id);
                        query.ExecuteUpdate();
                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                    }
                }//End Transaction
            }//End Session
        }


        /// <UpdateProfileScheduleMessage>
        /// Update Profile Schedule Message by id.
        /// </UpdateProfileScheduleMessage>
        /// <param name="Id">Id of Scheduled Message.(Guid)</param>
        /// <param name="profileid">Profile id (String)</param>
        /// <param name="message">New Message (String)</param>
        /// <param name="network">Network.(Facebook, Twitter and linkedin)</param>
        /// <param name="scheduledtime">Time od posting message.(DateTime)</param>
        /// <param name="picurl">Url of image.(String)</param>
        public void UpdateProfileScheduleMessage(Guid Id, string profileid, string message, string network, DateTime scheduledtime, string picurl)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        //Proceed action , to update Scheduled Message.
                        NHibernate.IQuery query = session.CreateQuery("update ScheduledMessage set ProfileId =:prof,ShareMessage =:share,ProfileType  =:prot, ScheduleTime =:scheduledtime,PicUrl =:picurl  where Id = :id");
                        query.SetParameter("id", Id);
                        query.SetParameter("prof", profileid);
                        query.SetParameter("share", message);
                        query.SetParameter("prot", network);
                        query.SetParameter("scheduledtime", scheduledtime);
                        query.SetParameter("picurl", picurl);
                        query.ExecuteUpdate();
                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                    }
                }//End Transaction
            }//End Session
        }



        /// <UpdateProfileScheduleMessage>
        /// Update Profile Schedule Message
        /// </summary>
        /// <param name="Id">Id of Scheduled Message.(Guid)</param>
        /// <param name="message">New Message of scheduler.(String)</param>
        public void UpdateProfileScheduleMessage(Guid Id, string message)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        //Proceed action to update message by id.
                        NHibernate.IQuery query = session.CreateQuery("update ScheduledMessage set ShareMessage =:share where Id = :id");
                        query.SetParameter("id", Id);
                        query.SetParameter("share", message);
                        query.ExecuteUpdate();
                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                    }
                }//End Transaction
            }//End Session
        }


        /// <UpdateProfileScheduleMessageStatus>
        /// Update Profile Schedule Message Status by id.
        /// </summary>
        /// <param name="Id">Id of scheduled message.(Guid)</param>
        /// <param name="Status">Change status.(0 or 1)</param>
        public void UpdateProfileScheduleMessageStatus(Guid Id, string Status)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        //Proceed action, to Update scheduled message status.
                        NHibernate.IQuery query = session.CreateQuery("update ScheduledMessage set Status =:Status where Id = :id");
                        query.SetParameter("id", Id);
                        query.SetParameter("Status", Status);
                        query.ExecuteUpdate();
                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                    }
                }//End Transaction
            }//End Session
        }


        /// <UpdateProfileScheduleMessageStat>
        /// Update Profile Schedule Message Stat
        /// </summary>
        /// <param name="Id">Id of scheduled message.(Guid)</param>
        /// <param name="Status">Status of scheduled message.(Ture or False)</param>
        public void UpdateProfileScheduleMessageStat(Guid Id, bool Status)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        //Proceed action, to Update status by id.
                        NHibernate.IQuery query = null;
                        if (Status == true)
                        {
                            query = session.CreateQuery("update ScheduledMessage set Status =0 where Id = :id");
                        }
                        else
                        {
                            query = session.CreateQuery("update ScheduledMessage set Status =1 where Id = :id");
                        }
                        //NHibernate.IQuery query = session.CreateQuery("update ScheduledMessage set Status =:Status where Id = :id");
                        query.SetParameter("id", Id);
                        //query.SetParameter("Status", Status);
                        query.ExecuteUpdate();
                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                    }
                }//End Transaction
            }//End Session
        }


        //public List<ScheduledMessage> getAllMessage()
        //{
        //    using (NHibernate.ISession session = SessionFactory.GetNewSession())
        //    {
        //        using (NHibernate.ITransaction transaction = session.BeginTransaction())
        //        {

        //            try
        //            {
        //                NHibernate.IQuery query = session.CreateQuery("from ScheduledMessage where Status = 0");
        //                List<ScheduledMessage> lst = new List<ScheduledMessage>();
        //                foreach (ScheduledMessage item in query.Enumerable())
        //                {
        //                    lst.Add(item);
        //                }
        //                return lst;
        //            } 
        //            catch (Exception ex)
        //            {
        //                Console.WriteLine(ex.StackTrace);
        //                return null;
        //            }
        //        }
        //    }
        //}


        /// <checkMessageExistsAtTime>
        /// Check Message Exists At Time
        /// </summary>
        /// <param name="UserId">User id.(Guid)</param>
        /// <param name="schetime">Schedule time.(DateTime)</param>
        /// <returns>True or False.(bool)</returns>
        public bool checkMessageExistsAtTime(Guid UserId, DateTime schetime)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        //Proce action, to get data.
                        NHibernate.IQuery query = session.CreateQuery("from ScheduledMessage where UserId = :userid and ScheduleTime = :schtime");
                        query.SetParameter("userid", UserId);
                        query.SetParameter("schtime", schetime);
                        var result = query.UniqueResult();

                        if (result == null)
                            return false;
                        else
                            return true;

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                        return true;
                    }

                }//End Transaction
            }//End Session
        }


        /// <checkMessageExistsAtTime>
        /// 
        /// </summary>
        /// <param name="UserId"></param>
        /// <param name="sharemessage"></param>
        /// <param name="schetime"></param>
        /// <param name="profileid"></param>
        /// <returns></returns>
        public bool checkMessageExistsAtTime(Guid UserId, string sharemessage, DateTime schetime, string profileid)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        //Proceed action, to check message is scheduled.
                        NHibernate.IQuery query = session.CreateQuery("from ScheduledMessage where UserId = :userid and ScheduleTime = :schtime and ShareMessage = :shmsg and ProfileId = :profileid");
                        query.SetParameter("userid", UserId);
                        query.SetParameter("profileid", profileid);
                        query.SetParameter("shmsg", sharemessage);
                        query.SetParameter("schtime", schetime);
                        var result = query.UniqueResult();

                        if (result == null)
                            return false;
                        else
                            return true;

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                        return true;
                    }

                }//End Transaction
            }//End Session
        }


        /// <getAllMessagesOfUser>
        /// Get All Messages Of User
        /// </summary>
        /// <param name="userid">User id.(Guid)</param>
        /// <param name="profileid">Profile id.(string)</param>
        /// <returns>Return object of ScheduledMessage Class with  value of each member in the form of list.(List<ScheduledMessage>)</returns>
        public List<Domain.Socioboard.Domain.ScheduledMessage> getAllMessagesOfUser(Guid userid, string profileid)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        //proceed action, to get all messages by user id and profileid.
                        List<Domain.Socioboard.Domain.ScheduledMessage> lstschmsg = session.CreateQuery("from ScheduledMessage where UserId = :userid and ScheduleTime > :schtime and ProfileId =:profileid and Status = 0")
                       .SetParameter("userid", userid)
                       .SetParameter("schtime", DateTime.Now)
                       .SetParameter("profileid", profileid)
                       .List<Domain.Socioboard.Domain.ScheduledMessage>()
                       .ToList<Domain.Socioboard.Domain.ScheduledMessage>();


                        //List<ScheduledMessage> lstschmsg = new List<ScheduledMessage>();
                        //foreach (ScheduledMessage item in query.Enumerable())
                        //{
                        //    lstschmsg.Add(item);
                        //}
                        return lstschmsg;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                        return null;
                    }
                }//End Transaction
            }//End Session
        }





        public List<Domain.Socioboard.Domain.ScheduledMessage> getAllMessagesOfUser(string profileid)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        //proceed action, to get all messages by user id and profileid.
                        List<Domain.Socioboard.Domain.ScheduledMessage> lstschmsg = session.CreateQuery("from ScheduledMessage where  ScheduleTime > :schtime and ProfileId =:profileid and Status = 0 order by ScheduleTime desc")
                       .SetParameter("schtime", DateTime.Now)
                       .SetParameter("profileid", profileid)
                       .List<Domain.Socioboard.Domain.ScheduledMessage>()
                       .ToList<Domain.Socioboard.Domain.ScheduledMessage>();


                        //List<ScheduledMessage> lstschmsg = new List<ScheduledMessage>();
                        //foreach (ScheduledMessage item in query.Enumerable())
                        //{
                        //    lstschmsg.Add(item);
                        //}
                        return lstschmsg;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                        return null;
                    }
                }//End Transaction
            }//End Session
        }




        public Domain.Socioboard.Domain.ScheduledMessage GetScheduledMessageDetails(Guid id)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        //proceed action, to get all messages by user id and profileid.
                        List<Domain.Socioboard.Domain.ScheduledMessage> lstschmsg = session.CreateQuery("from ScheduledMessage where Id=:id ")
                       .SetParameter("id", id)
                       .List<Domain.Socioboard.Domain.ScheduledMessage>()
                       .ToList<Domain.Socioboard.Domain.ScheduledMessage>();
                        return lstschmsg[0];
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                        return null;
                    }
                }//End Transaction
            }//End Session
        }


        public List<Domain.Socioboard.Domain.ScheduledMessage> GetUnsentSchdeuledMessageByProfileType(string profiletype)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        //proceed action, to get all messages by user id and profileid.
                        List<Domain.Socioboard.Domain.ScheduledMessage> lstschmsg = session.CreateQuery("from ScheduledMessage where  ScheduleTime <= :schtime and ProfileType =:profiletype and Status = 0 order by ScheduleTime desc")
                       .SetParameter("profiletype", profiletype)
                        .SetParameter("schtime", DateTime.Now)
                       .List<Domain.Socioboard.Domain.ScheduledMessage>()
                       .ToList<Domain.Socioboard.Domain.ScheduledMessage>();


                        //List<ScheduledMessage> lstschmsg = new List<ScheduledMessage>();
                        //foreach (ScheduledMessage item in query.Enumerable())
                        //{
                        //    lstschmsg.Add(item);
                        //}
                        return lstschmsg;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                        return null;
                    }
                }//End Transaction
            }//End Session
        }

        public void UpdateScheduledMessage(Guid msgId)
        {
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        //Proceed action , to update Scheduled Message.
                        NHibernate.IQuery query = session.CreateQuery("update ScheduledMessage set Status=1 where Id = :id");
                        query.SetParameter("id", msgId);
                        query.ExecuteUpdate();
                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                    }
                }
            }
        }


        /// <getAllMessages>
        /// Get All Messages by date.
        /// </summary>
        /// <param name="date">DateTime.(DateTime)</param>
        /// <returns>Return object of ScheduledMessage Class with  value of each member in the form of IEnumerable list.(IEnumerable<ScheduledMessage>)</returns>
        public IEnumerable<Domain.Socioboard.Domain.ScheduledMessage> getAllMessages(DateTime date)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {

                try
                {
                    //Proceed action, to get all records of scheduled message.
                    return session.CreateCriteria(typeof(Domain.Socioboard.Domain.ScheduledMessage)).List<Domain.Socioboard.Domain.ScheduledMessage>().Where(x => x.Status == false && x.ScheduleTime <= date);

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.StackTrace);
                    return null;
                }

            }//End Session

        }


        /// <getAllMessages>
        /// Get All Messages by date.
        /// </summary>
        /// <returns>Return object of ScheduledMessage Class with  value of each member in the form of IEnumerable list.(IEnumerable<ScheduledMessage>)</returns>
        public IEnumerable<Domain.Socioboard.Domain.ScheduledMessage> getAllMessage()
        {
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {

                try
                {
                    //Proceed action, to get all records of scheduled message.
                    return session.CreateCriteria(typeof(Domain.Socioboard.Domain.ScheduledMessage)).List<Domain.Socioboard.Domain.ScheduledMessage>().Where(x => x.Status == false && x.ScheduleTime <= DateTime.Now);

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.StackTrace);
                    return null;
                }

            }//End Session
        }


        /// <getAllMessages>
        /// Get All Messages by date.
        /// </summary>
        /// <param name="schtime">DateTime.(DateTime)</param>
        /// <returns>Return object of ScheduledMessage Class with  value of each member in the form of  list.(List<ScheduledMessage>)</returns>
        public List<Domain.Socioboard.Domain.ScheduledMessage> getAllmessages(DateTime schtime)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        List<Domain.Socioboard.Domain.ScheduledMessage> lstschtime = session.CreateQuery("from ScheduledMessage where ScheduleTime <= :shctime and Status = false")
                         .SetParameter("shctime", schtime)
                         .List<Domain.Socioboard.Domain.ScheduledMessage>()
                         .ToList<Domain.Socioboard.Domain.ScheduledMessage>();

                        //List<ScheduledMessage> lstschtime = query.Enumerable<ScheduledMessage>().ToList<ScheduledMessage>();
                        //foreach (ScheduledMessage item in query.Enumerable())
                        //{
                        //    lstschtime.Add(item);
                        //}
                        return lstschtime;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                        return null;
                    }
                }//End Transaction
            }//End Session
        }


        //getAllMessagesOfUser used in webservice

        /// <getAllMessagesOfUser>
        /// Get All Messages Of User
        /// </summary>
        /// <param name="UserId">User id (Guid)</param>
        /// <returns>Return object of ScheduledMessage Class with  value of each member in the form of  list.(List<ScheduledMessage>)</returns>
        public List<Domain.Socioboard.Domain.ScheduledMessage> getAllMessagesOfUser(Guid UserId)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        //Proceed action, to get all records of scheduled message.
                        List<Domain.Socioboard.Domain.ScheduledMessage> lstschtime = session.CreateQuery("from ScheduledMessage where UserId =:userid order by ScheduleTime desc")
                        .SetParameter("userid", UserId)
                        .List<Domain.Socioboard.Domain.ScheduledMessage>()
                        .ToList<Domain.Socioboard.Domain.ScheduledMessage>();

                        //List<ScheduledMessage> lstschtime = query.Enumerable<ScheduledMessage>().ToList<ScheduledMessage>();

                        return lstschtime;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                        return null;
                    }
                }//End Transaction
            }//End Session
        }





        public List<Domain.Socioboard.Domain.ScheduledMessage> getAllMessagesDetail(string profileid, Guid userid)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //Begin session trasaction and opens up.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        string str = "from ScheduledMessage where UserId=:userid and ProfileId IN(";
                        string[] arrsrt = profileid.Split(',');
                        foreach (string sstr in arrsrt)
                        {
                            str += "'" + (sstr) + "'" + ",";
                        }
                        str = str.Substring(0, str.Length - 1);
                        str += ") order by ScheduleTime desc ";
                        List<Domain.Socioboard.Domain.ScheduledMessage> alst = session.CreateQuery(str).SetParameter("userid", userid)
                       .List<Domain.Socioboard.Domain.ScheduledMessage>()
                       .ToList<Domain.Socioboard.Domain.ScheduledMessage>();
                        return alst;

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                        return null;
                    }

                }//End Trasaction
            }//End session
        }






        //public List<Domain.Socioboard.Domain.ScheduledMessage> getAllSentMessageDetails(string profileid)
        //{
        //    //Creates a database connection and opens up a session
        //    using (NHibernate.ISession session = SessionFactory.GetNewSession())
        //    {
        //        //Begin session trasaction and opens up.
        //        using (NHibernate.ITransaction transaction = session.BeginTransaction())
        //        {
        //            try
        //            {
        //                string str = "from ScheduledMessage where Status=1 and ProfileId IN(";
        //                string[] arrsrt = profileid.Split(',');
        //                foreach (string sstr in arrsrt)
        //                {
        //                    str += "'" + (sstr) + "'" + ",";
        //                }
        //                str = str.Substring(0, str.Length - 1);
        //                str += ")";
        //                List<Domain.Socioboard.Domain.ScheduledMessage> alst = session.CreateQuery(str)
        //               .List<Domain.Socioboard.Domain.ScheduledMessage>()
        //               .ToList<Domain.Socioboard.Domain.ScheduledMessage>();
        //                return alst;

        //            }
        //            catch (Exception ex)
        //            {
        //                Console.WriteLine(ex.StackTrace);
        //                return null;
        //            }

        //        }//End Trasaction
        //    }//End session
        //}

        public List<Domain.Socioboard.Domain.ScheduledMessage> getAllSentMessageDetails(string profileid, Guid userid)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //Begin session trasaction and opens up.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        string str = "from ScheduledMessage where UserId=:userid and Status=1 and ProfileId IN(";
                        string[] arrsrt = profileid.Split(',');
                        foreach (string sstr in arrsrt)
                        {
                            str += "'" + (sstr) + "'" + ",";
                        }
                        str = str.Substring(0, str.Length - 1);
                        str += ")";
                        List<Domain.Socioboard.Domain.ScheduledMessage> alst = session.CreateQuery(str).SetParameter("userid", userid)
                       .List<Domain.Socioboard.Domain.ScheduledMessage>()
                       .ToList<Domain.Socioboard.Domain.ScheduledMessage>();
                        return alst;

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                        return null;
                    }

                }//End Trasaction
            }//End session
        }







        /// <getAllIUnSentMessagesOfUser>
        /// Get AllI UnSent Messages Of User
        /// </summary>
        /// <param name="UserId">User id.(Guid)</param>
        /// <returns>Return object of ScheduledMessage Class with  value of each member in the form of  list.(List<ScheduledMessage>)</returns>
        public List<Domain.Socioboard.Domain.ScheduledMessage> getAllIUnSentMessagesOfUser(Guid UserId)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        //Proceed action,to get all unread messages.
                        List<Domain.Socioboard.Domain.ScheduledMessage> lstschtime = session.CreateQuery("from ScheduledMessage where Status = 0 and UserId =:userid")
                          .SetParameter("userid", UserId)
                          .List<Domain.Socioboard.Domain.ScheduledMessage>()
                          .ToList<Domain.Socioboard.Domain.ScheduledMessage>();

                        //  List<ScheduledMessage> lstschtime = query.Enumerable<ScheduledMessage>().ToList<ScheduledMessage>();

                        return lstschtime;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                        return null;
                    }
                }//End Transaction
            }//End Session
        }






        public List<Domain.Socioboard.Domain.ScheduledMessage> getAllSentMessagesOfUser(Guid UserId)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        //Proceed action,to get all unread messages.
                        List<Domain.Socioboard.Domain.ScheduledMessage> lstschtime = session.CreateQuery("from ScheduledMessage where Status = 1 and UserId =:userid order by ClientTime desc")
                          .SetParameter("userid", UserId)
                          .List<Domain.Socioboard.Domain.ScheduledMessage>()
                          .ToList<Domain.Socioboard.Domain.ScheduledMessage>();

                        //  List<ScheduledMessage> lstschtime = query.Enumerable<ScheduledMessage>().ToList<ScheduledMessage>();

                        return lstschtime;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                        return null;
                    }
                }//End Transaction
            }//End Session
        }



        public List<Domain.Socioboard.Domain.ScheduledMessage> GetAllUnSentMessagesAccordingToGroup(Guid UserId, string profileid, string network)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        //Proceed action,to get all unread messages.
                        List<Domain.Socioboard.Domain.ScheduledMessage> lstschtime = session.CreateQuery("from ScheduledMessage where Status = 0 and UserId =:userid and ProfileType=:profiletype and ProfileId=:profileid order by ClientTime desc")
                          .SetParameter("userid", UserId)
                          .SetParameter("profiletype", network)
                          .SetParameter("profileid", profileid)
                          .List<Domain.Socioboard.Domain.ScheduledMessage>()
                          .ToList<Domain.Socioboard.Domain.ScheduledMessage>();

                        //  List<ScheduledMessage> lstschtime = query.Enumerable<ScheduledMessage>().ToList<ScheduledMessage>();

                        return lstschtime;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                        return null;
                    }
                }//End Transaction
            }//End Session
        }


        public List<Domain.Socioboard.Domain.ScheduledMessage> getAllSentMessagesOfUser(string profileid)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        //Proceed action,to get all unread messages.
                        List<Domain.Socioboard.Domain.ScheduledMessage> lstschtime = session.CreateQuery("from ScheduledMessage where Status = 1 and ProfileId =:profileid order by ClientTime desc")
                          .SetParameter("profileid", profileid)
                          .List<Domain.Socioboard.Domain.ScheduledMessage>()
                          .ToList<Domain.Socioboard.Domain.ScheduledMessage>();

                        //  List<ScheduledMessage> lstschtime = query.Enumerable<ScheduledMessage>().ToList<ScheduledMessage>();

                        return lstschtime;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                        return null;
                    }
                }//End Transaction
            }//End Session
        }



        /// <getWooQueueMessage>
        /// Get WooQueue Message
        /// </summary>
        /// <param name="UserId">User id.(Guid)</param>
        /// <returns>Return object of ScheduledMessage Class with  value of each member in the form of  list.(List<ScheduledMessage>)</returns>
        public List<Domain.Socioboard.Domain.ScheduledMessage> getWooQueueMessage(Guid UserId)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        //Proceed action, to Data by user id.
                        // Where message status is zero and scheduled message Data is grater than given date time.                        
                        List<Domain.Socioboard.Domain.ScheduledMessage> lstschtime = session.CreateQuery("from ScheduledMessage where Status = 0 and UserId =:userid and ScheduleTime >= :schtime")
                        .SetParameter("userid", UserId)
                        .SetParameter("schtime", DateTime.Now)
                        .List<Domain.Socioboard.Domain.ScheduledMessage>()
                        .ToList<Domain.Socioboard.Domain.ScheduledMessage>();
                        //  List<ScheduledMessage> lstschtime = query.Enumerable<ScheduledMessage>().ToList<ScheduledMessage>();

                        return lstschtime;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                        return null;
                    }
                }//End Transaction
            }//End Session
        }


        /// <getScheduleMessageByMessageId>
        /// Get Schedule Message By Message Id.
        /// </summary>
        /// <param name="Id">Id of scheduled message.(Guid)</param>
        /// <returns>Return object of ScheduledMessage Class with  all Scheduled message details.(List<ScheduledMessage>)</returns>
        public Domain.Socioboard.Domain.ScheduledMessage getScheduleMessageByMessageId(Guid Id)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        //Proceed action, to get message by id.
                        NHibernate.IQuery query = session.CreateQuery("from ScheduledMessage Id = :id");
                        query.SetParameter("id", Id);

                        Domain.Socioboard.Domain.ScheduledMessage lstschtime = query.UniqueResult<Domain.Socioboard.Domain.ScheduledMessage>();
                        return lstschtime;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                        return null;
                    }
                }//End Transaction
            }//End Session
        }


        /// <DeleteScheduledMessageByUserid>
        /// Delete ScheduledMessageByUserid
        /// </summary>
        /// <param name="userid">User id.(Guid)</param>
        /// <returns>Return 1 for true and 0 for false.(int)</returns>
        public int DeleteScheduledMessageByUserid(Guid userid)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        //Proceed action, to delete scheduled message by user id.
                        NHibernate.IQuery query = session.CreateQuery("delete from ScheduledMessage where UserId = :userid")
                                        .SetParameter("userid", userid);
                        int isUpdated = query.ExecuteUpdate();
                        transaction.Commit();
                        return isUpdated;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                        return 0;
                    }
                }//End Transaction
            }//End Session
        }



        /// <GetAllScheduledDetails>
        /// Get All Scheduled Details
        /// </summary>
        /// <returns>Return object of ScheduledMessage Class with  value of each member in the form of  list.(List<ScheduledMessage>)</returns>
        public List<Domain.Socioboard.Helper.ScheduledTracker> GetAllScheduledDetails()
        {
            List<ScheduledTracker> lstScheduledTracker = new List<ScheduledTracker>();

            try
            {
                //Creates a database connection and opens up a session
                using (NHibernate.ISession session = SessionFactory.GetNewSession())
                {
                    //After Session creation, start Transaction.
                    using (NHibernate.ITransaction transaction = session.BeginTransaction())
                    {
                        try
                        {
                            //Proceed action, to get all schedule data.
                            var res = session.CreateQuery("select count(Id), UserId from ScheduledMessage group by UserId order by ScheduleTime desc").SetMaxResults(100);

                            //Get the the all returned values from res
                            foreach (Object[] item in res.Enumerable())
                            {
                                try
                                {
                                    //add values in ScheduledTracker class object
                                    // set the values in class property
                                    // and add in List for returning list of objects. 
                                    // Type type = item.GetType();
                                    ScheduledTracker objscheduledTracker = new ScheduledTracker();
                                    objscheduledTracker.Count = Convert.ToInt32(item[item.Length - 2]);
                                    objscheduledTracker.Id = Convert.ToString(item[item.Length - 1]);

                                    //Add class object in List.
                                    lstScheduledTracker.Add(objscheduledTracker);

                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine("Error : " + ex.StackTrace);
                                }
                            }

                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.StackTrace);
                            //return null;
                        }
                    }//End Transaction
                }//End Session
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                //return null;
            }

            return lstScheduledTracker;
        }


        public bool deleteScheduleMessage(Guid userid, string messageid)
        {
            bool status = false;
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        //Proceed action, to delete existing account by user id and twitter id.
                        NHibernate.IQuery query = session.CreateQuery("delete from ScheduledMessage where UserId = :userid and Id = :messageid")
                                        .SetParameter("userid", userid)
                                        .SetParameter("messageid", messageid);
                        int isUpdated = query.ExecuteUpdate();
                        transaction.Commit();
                        status = true;
                    }
                    catch (Exception ex)
                    {
                        logger.Error(ex.Message);
                    }
                    return status;
                }//End Transaction
            }//End Session
        }
        public void UpdateScheduleMessage(Guid userid, string messageid, string mess, DateTime dt)
        {

            //Creates a database connection and opens up a session
            try
            {
                using (NHibernate.ISession session = SessionFactory.GetNewSession())
                {
                    //After Session creation, start Transaction.
                    using (NHibernate.ITransaction transaction = session.BeginTransaction())
                    {
                        //Proceed action, to update existing Draft message by draft id.
                        session.CreateQuery("Update ScheduledMessage set ShareMessage =:mess,ScheduleTime =:dt where UserId = :userid and Id = :messageid")
                            .SetParameter("mess", mess)
                            .SetParameter("userid", userid)
                            .SetParameter("dt", dt)
                            .SetParameter("messageid", messageid)
                            .ExecuteUpdate();
                        transaction.Commit();


                    }
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);

            }


        }



        public int GetSentMessageCountByProfileIdAndUserId(Guid UserId, string profileids)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //Begin session trasaction and opens up.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        string[] arrsrt = profileids.Split(',');
                        var results = session.QueryOver<Domain.Socioboard.Domain.ScheduledMessage>().Where(U => U.UserId == UserId).AndRestrictionOn(m => m.ProfileId).IsIn(arrsrt).Select(Projections.RowCount()).FutureValue<int>().Value;
                        return Int16.Parse(results.ToString());
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                        return 0;
                    }
                }//End Transaction
            }// End se
        }


    }
}
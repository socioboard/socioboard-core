using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SocioBoard.Domain;
using SocioBoard.Helper;

namespace SocioBoard.Model
{
    public class ScheduledMessageRepository : IScheduledMessageRepository
    {
        public void addNewMessage(ScheduledMessage schmesg)
        {
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    session.Save(schmesg);
                    transaction.Commit();
                }
            }
        }
        public void deleteMessage(ScheduledMessage schmesg)
        {
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    session.CreateQuery("delete from ScheduledMessage where Id = :id")
                        .SetParameter("id", schmesg.Id)
                        .ExecuteUpdate();
                    transaction.Commit();

                }
            }
        }
        public void deleteMessage(Guid Id)
        {
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        int i = session.CreateQuery("delete from ScheduledMessage where Id = :id")
                                  .SetParameter("id", Id)
                                  .ExecuteUpdate();
                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {

                    }
                }
            }
        }
        public void updateMessage(Guid id)
        {
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        NHibernate.IQuery query = session.CreateQuery("update ScheduledMessage set Status = 1 where Id = :id");
                        query.SetParameter("id", id);
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





        public void UpdateProfileScheduleMessage(Guid Id, string profileid, string message, string network, DateTime scheduledtime, string picurl)
        {
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
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
                }
            }

        }

        public void UpdateProfileScheduleMessage(Guid Id, string message)
        {
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
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
                }
            }

        }
        public void UpdateProfileScheduleMessageStatus(Guid Id, string Status)
        {
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
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
                }
            }

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
        public bool checkMessageExistsAtTime(Guid UserId, DateTime schetime)
        {
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
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

                }
            }
        }
        public bool checkMessageExistsAtTime(Guid UserId, string sharemessage, DateTime schetime, string profileid)
        {
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
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

                }
            }
        }
        public List<ScheduledMessage> getAllMessagesOfUser(Guid userid, string profileid)
        {
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        List<ScheduledMessage> lstschmsg = session.CreateQuery("from ScheduledMessage where UserId = :userid and ScheduleTime > :schtime and ProfileId =:profileid and Status = 0")
                       .SetParameter("userid", userid)
                       .SetParameter("schtime", DateTime.Now)
                       .SetParameter("profileid", profileid)
                       .List<ScheduledMessage>()
                       .ToList<ScheduledMessage>();


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
                }
            }

        }
        public IEnumerable<ScheduledMessage> getAllMessages(DateTime date)
        {
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {

                try
                {
                    return session.CreateCriteria(typeof(ScheduledMessage)).List<ScheduledMessage>().Where(x => x.Status == false && x.ScheduleTime <= date);

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.StackTrace);
                    return null;
                }

            }

        }
        public IEnumerable<ScheduledMessage> getAllMessage()
        {
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {

                try
                {
                    return session.CreateCriteria(typeof(ScheduledMessage)).List<ScheduledMessage>().Where(x => x.Status == false && x.ScheduleTime <= DateTime.Now);

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.StackTrace);
                    return null;
                }

            }

        }
        public List<ScheduledMessage> getAllmessages(DateTime schtime)
        {
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        List<ScheduledMessage> lstschtime = session.CreateQuery("from ScheduledMessage where ScheduleTime <= :shctime and Status = false")
                         .SetParameter("shctime", schtime)
                         .List<ScheduledMessage>()
                         .ToList<ScheduledMessage>();

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
                }
            }
        }


        //getAllMessagesOfUser used in webservice
        public List<ScheduledMessage> getAllMessagesOfUser(Guid UserId)
        {
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        List<ScheduledMessage> lstschtime = session.CreateQuery("from ScheduledMessage where UserId =:userid")
                        .SetParameter("userid", UserId)
                        .List<ScheduledMessage>()
                        .ToList<ScheduledMessage>();

                        //List<ScheduledMessage> lstschtime = query.Enumerable<ScheduledMessage>().ToList<ScheduledMessage>();

                        return lstschtime;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                        return null;
                    }
                }
            }
        }

        public List<ScheduledMessage> getAllIUnSentMessagesOfUser(Guid UserId)
        {
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        List<ScheduledMessage> lstschtime = session.CreateQuery("from ScheduledMessage where Status = 0 and UserId =:userid")
                          .SetParameter("userid", UserId)
                          .List<ScheduledMessage>()
                          .ToList<ScheduledMessage>();

                        //  List<ScheduledMessage> lstschtime = query.Enumerable<ScheduledMessage>().ToList<ScheduledMessage>();

                        return lstschtime;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                        return null;
                    }
                }
            }
        }

        public List<ScheduledMessage> getWooQueueMessage(Guid UserId)
        {
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        List<ScheduledMessage> lstschtime = session.CreateQuery("from ScheduledMessage where Status = 0 and UserId =:userid and ScheduleTime >= :schtime")
                        .SetParameter("userid", UserId)
                        .SetParameter("schtime", DateTime.Now)
                        .List<ScheduledMessage>()
                        .ToList<ScheduledMessage>();
                        //  List<ScheduledMessage> lstschtime = query.Enumerable<ScheduledMessage>().ToList<ScheduledMessage>();

                        return lstschtime;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                        return null;
                    }
                }
            }
        }

        public ScheduledMessage getScheduleMessageByMessageId(Guid Id)
        {
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        NHibernate.IQuery query = session.CreateQuery("from ScheduledMessage Id = :id");
                        query.SetParameter("id", Id);

                        ScheduledMessage lstschtime = query.UniqueResult<ScheduledMessage>();
                        return lstschtime;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                        return null;
                    }
                }
            }
        }

    }
}
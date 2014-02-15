using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SocioBoard.Domain;
using SocioBoard.Helper;
using NHibernate.Transform;
using System.Collections;
using System.Data;
using NHibernate.Linq;

namespace SocioBoard.Model
{
    public class FacebookMessageRepository : IFacebookMessageRepository
    {

        public void addFacebookMessage(FacebookMessage fbmsg)
        {
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    session.Save(fbmsg);
                    transaction.Commit();
                }
            }
        }

        public int deleteFacebookMessage(FacebookMessage fbaccount)
        {
            throw new NotImplementedException();
        }

        public int updateFacebookMessage(FacebookMessage fbaccount)
        {
            throw new NotImplementedException();
        }

        public List<FacebookMessage> getAllFacebookMessagesOfUser(Guid UserId, string profileId)
        {
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        List<FacebookMessage> alst = session.CreateQuery("from FacebookMessage where UserId = :userid and ProfileId = :profileId")
                         .SetParameter("userid", UserId)
                         .SetParameter("profileId", profileId)
                         .List<FacebookMessage>()
                         .ToList<FacebookMessage>();

                        #region oldcode
                        //List<FacebookMessage> alst = new List<FacebookMessage>();
                        //foreach (FacebookMessage item in query.Enumerable<FacebookMessage>().OrderByDescending(x => x.MessageDate))
                        //{
                        //    alst.Add(item);
                        //} 
                        #endregion

                        return alst;

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                        return null;
                    }

                }
            }
        }

        public void getAllFacebookMessagesOfUsers(Guid UserId, string profileId)
        {
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        NHibernate.IQuery query = session.CreateQuery("from FacebookMessage where UserId = :userid and ProfileId = :profileId");
                        query.SetParameter("userid", UserId);
                        query.SetParameter("profileId", profileId);
                        foreach (var item in query.Enumerable())
                        {

                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);

                    }

                }
            }
        }


        public bool checkFacebookMessageExists(string Id, Guid Userid)
        {
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        NHibernate.IQuery query = session.CreateQuery("from FacebookMessage where UserId = :userid and MessageId = :msgid");
                        query.SetParameter("userid", Userid);
                        query.SetParameter("msgid", Id);
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

        public void deleteAllMessagesOfUser(string fbuserid, Guid userid)
        {
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        NHibernate.IQuery query = session.CreateQuery("delete from FacebookMessage where UserId = :userid and ProfileId = :profileId");
                        query.SetParameter("userid", userid);
                        query.SetParameter("profileId", fbuserid);
                        int isUpdated = query.ExecuteUpdate();
                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);

                    }

                }
            }
        }

        public List<FacebookMessage> getFacebookUserWallPost(Guid userid, string profileid)
        {
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        List<FacebookMessage> alst = session.CreateQuery("from FacebookMessage where UserId = :userid and ProfileId = :profileId and Type='fb_home'")
                         .SetParameter("userid", userid)
                         .SetParameter("profileId", profileid)
                         .List<FacebookMessage>()
                         .ToList<FacebookMessage>();


                        #region oldcode
                        //List<FacebookMessage> alst = new List<FacebookMessage>();
                        //foreach (FacebookMessage item in query.Enumerable<FacebookMessage>().OrderByDescending(x => x.MessageDate))
                        //{
                        //    alst.Add(item);
                        //} 
                        #endregion

                        return alst;

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                        return null;
                    }

                }
            }
        }

        public List<FacebookMessage> getFacebookUserWallPost(Guid userid, string profileid, int count)
        {
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        List<FacebookMessage> alst = session.CreateQuery("from FacebookMessage where UserId = :userid and ProfileId = :profileId and Type='fb_home'")
                        .SetParameter("userid", userid)
                        .SetParameter("profileId", profileid)
                        .SetFirstResult(count)
                        .SetMaxResults(10)
                        .List<FacebookMessage>()
                        .ToList<FacebookMessage>();

                        #region oldcode
                        //List<FacebookMessage> alst = new List<FacebookMessage>();
                        //foreach (FacebookMessage item in query.Enumerable<FacebookMessage>().OrderByDescending(x => x.MessageDate))
                        //{
                        //    alst.Add(item);
                        //} 
                        #endregion

                        return alst;

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                        return null;
                    }

                }
            }
        }


        /***********************************************************************************************/

        public List<FacebookMessage> getAllMessageOfProfile(string profileid)
        {
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        List<FacebookMessage> alst = session.CreateQuery("from FacebookMessage where ProfileId = :profileId")
                        .SetParameter("profileId", profileid)
                        .List<FacebookMessage>()
                        .ToList<FacebookMessage>();

                        #region oldcode
                        //List<FacebookMessage> alst = new List<FacebookMessage>();
                        //foreach (FacebookMessage item in query.Enumerable<FacebookMessage>().OrderByDescending(x => x.MessageDate))
                        //{
                        //    alst.Add(item);
                        //} 
                        #endregion

                        return alst;

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                        return null;
                    }

                }
            }

        }

        public List<FacebookMessage> getAllWallpostsOfProfile(string profileid)
        {
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        List<FacebookMessage> alst = session.CreateQuery("from FacebookMessage where ProfileId = :profileId and Type='fb_home'")
                        .SetParameter("profileId", profileid)
                        .List<FacebookMessage>()
                        .ToList<FacebookMessage>();

                        #region oldcode
                        //List<FacebookMessage> alst = new List<FacebookMessage>();
                        //foreach (FacebookMessage item in query.Enumerable<FacebookMessage>().OrderByDescending(x => x.MessageDate))
                        //{
                        //    alst.Add(item);
                        //} 
                        #endregion
                        return alst;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                        return null;
                    }

                }
            }
        }

        public List<FacebookMessage> getAllWallpostsOfProfile(string profileid, int count)
        {
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        List<FacebookMessage> lst = session.CreateQuery("from FacebookMessage where ProfileId = :profileId and Type='fb_home' order by MessageDate Desc")
                        .SetParameter("profileId", profileid)
                        .SetFirstResult(count)
                        .SetMaxResults(10)
                        .List<FacebookMessage>()
                        .ToList<FacebookMessage>();

                        #region oldcode
                        //List<FacebookMessage> lst = new List<FacebookMessage>();
                        //foreach (FacebookMessage item in query.Enumerable<FacebookMessage>().OrderByDescending(x => x.MessageDate))
                        //{
                        //    lst.Add(item);
                        //} 
                        #endregion


                        return lst;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                        return null;
                    }

                }
            }
        }


        public bool checkFacebookMessageExists(string Id)
        {
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        NHibernate.IQuery query = session.CreateQuery("from FacebookMessage where MessageId = :msgid");
                        query.SetParameter("msgid", Id);
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


        public List<FacebookMessage> getAllInboxMessages(string profileid)
        {

            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        List<FacebookMessage> lstfbmsg = session.CreateQuery("from FacebookMessage where Type = 'inbox_message' and ProfileId = :profid")
                        .SetParameter("profid", profileid)
                        .List<FacebookMessage>()
                        .ToList<FacebookMessage>();
                        #region oldcode
                        //List<FacebookMessage> lstfbmsg = new List<FacebookMessage>();

                        //foreach (FacebookMessage item in query.Enumerable())
                        //{
                        //    lstfbmsg.Add(item);
                        //} 
                        #endregion
                        return lstfbmsg;

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                        return null;
                    }

                }
            }
        }

        public List<FacebookMessage> getAllSentMessages(string profileid)
        {
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        List<FacebookMessage> lstfbmsg = session.CreateQuery("from FacebookMessage where Type = 'fb_tag' and ProfileId = :profid")
                        .SetParameter("profid", profileid)
                        .List<FacebookMessage>()
                        .ToList<FacebookMessage>();

                        #region oldcode
                        //List<FacebookMessage> lstfbmsg = new List<FacebookMessage>();
                        //foreach (FacebookMessage item in query.Enumerable<FacebookMessage>().OrderByDescending(x => x.MessageDate))
                        //{
                        //    lstfbmsg.Add(item);
                        //} 
                        #endregion
                        return lstfbmsg;

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                        return null;
                    }

                }
            }
        }




        public int DeleteFacebookMessageByUserid(Guid userid)
        {
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        NHibernate.IQuery query = session.CreateQuery("delete from FacebookMessage where UserId = :userid")
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
                }
            }
        }



    }
}
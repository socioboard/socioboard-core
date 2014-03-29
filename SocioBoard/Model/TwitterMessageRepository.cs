using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SocioBoard.Domain;
using SocioBoard.Helper;
using System.Collections;

namespace SocioBoard.Model
{
    public class TwitterMessageRepository:ITwitterMessageRepository
    {

        /// <addTwitterMessage>
        /// Add New Twitter Message
        /// </summary>
        /// <param name="twtmsg">Set Values in a TwitterMessage Class Property and Pass the Object of Class.(Domein.TwitterMessage)</param>
        public void addTwitterMessage(TwitterMessage twtmsg)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, open up a Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    //Proceed action, to save data.
                    session.Save(twtmsg);
                    transaction.Commit();
                }//End Transaction
            }//End Session
        }



        /// <deleteTwitterMessage>
        /// Delete Twitter Message
        /// </summary>
        /// <param name="twtmsg">Set Values of profile id and user id in a TwitterMessage Class Property and Pass the Object of Class.(Domein.TwitterMessage)</param>
        /// <returns>Return 1 for success and 0 for failure.(int) </returns>
        public int deleteTwitterMessage(TwitterMessage twtmsg)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, open up a Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        //Proceed action, to delete twitter message by twitter user id and user id 
                        NHibernate.IQuery query = session.CreateQuery("delete from TwitterMessage where ProfileId = :twtuserid and UserId = :userid")
                                        .SetParameter("twtuserid", twtmsg.ProfileId)
                                        .SetParameter("userid", twtmsg.UserId);
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


        /// <deleteTwitterMessage>
        /// Delete Twitter Message
        /// </summary>
        /// <param name="profileid">profile id.(string)</param>
        /// <param name="userid">User id.(Guid)</param>
        /// <returns>Return 1 for success and 0 for failure.(int) </returns>
        public int deleteTwitterMessage(string profileid,Guid userid)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, open up a Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        //Proceed action, to delete messages by profile id and iuser id.
                        NHibernate.IQuery query = session.CreateQuery("delete from TwitterMessage where ProfileId = :twtuserid and UserId = :userid")
                                        .SetParameter("twtuserid", profileid)
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

        public int updateTwitterMessage(TwitterMessage fbaccount)
        {
            throw new NotImplementedException();
        }


        /// <getAllTwitterMessagesOfUser>
        /// Get All Twitter Messages Of User
        /// </summary>
        /// <param name="UserId">User id.(Guid)</param>
        /// <param name="profileid">Profile id.(String)</param>
        /// <returns>Return object of TwitterMessage Class with  value of each member in the form of list.(List<TwitterMessage>)</returns>
        public List<TwitterMessage> getAllTwitterMessagesOfUser(Guid UserId,string profileid)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, open up a Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        //Proceed action, to get messages of profile by profile id and user id.
                        NHibernate.IQuery query = session.CreateQuery("from TwitterMessage where UserId = :userid and ProfileId = :profid");
                        query.SetParameter("userid", UserId);
                        query.SetParameter("profid", profileid);
                        List<TwitterMessage> lstmsg = new List<TwitterMessage>();
                        foreach (TwitterMessage item in query.Enumerable<TwitterMessage>().OrderByDescending(x=>x.MessageDate))
                        {
                            lstmsg.Add(item);
                        }
                        return lstmsg;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                        return null;
                    }

                }//End Transaction
            }//End Session
        }


        /// <getAllReadMessagesOfUser>
        /// Get All Read Messages Of User
        /// </summary>
        /// <param name="UserId">User id.(Guid)</param>
        /// <param name="profileid">Profile id.(String)</param>
        /// <returns>Return object of TwitterMessage Class with  value of each member in the form of list.(List<TwitterMessage>)</returns>
        public List<TwitterMessage> getAllReadMessagesOfUser(Guid UserId, string profileid)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, open up a Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        //Proceed action, to get twitter messages where read status is 1.
                        List<TwitterMessage> lstmsg = session.CreateQuery("from TwitterMessage where ReadStatus = 1 and UserId = :userid and ProfileId = :profid ORDER BY EntryDate DESC")
                       .SetParameter("userid", UserId)
                       .SetParameter("profid", profileid)
                       .List<TwitterMessage>()
                       .ToList<TwitterMessage>();

                        //List<TwitterMessage> lstmsg = new List<TwitterMessage>();
                        //foreach (TwitterMessage item in query.Enumerable<TwitterMessage>().OrderByDescending(x => x.MessageDate))
                        //{
                        //    lstmsg.Add(item);
                        //}
                        return lstmsg;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                        return null;
                    }

                }//End Transaction
            }//End Session
        }



        /// <getAllReadMessagesOfUser>
        /// Get All Read Twitter Messages Of User By Profile id.
        /// </summary>
        /// <param name="profileid">Profile id.(string)</param>
        /// <returns>Return object of TwitterMessage Class with  value of each member in the form of list.(List<TwitterMessage>)</returns>
        public List<TwitterMessage> getAllReadMessagesOfUser(string profileid)
        {
            //Creates a database connection and opens up a session.
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, open up a Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        //Proceed action, to get all twitter messages where read status is 1.
                        List<TwitterMessage> lstmsg = session.CreateQuery("from TwitterMessage where ReadStatus = 1 and  ProfileId = :profid")
                       .SetParameter("profid", profileid)
                       .List<TwitterMessage>()
                       .ToList<TwitterMessage>();

                        //List<TwitterMessage> lstmsg = new List<TwitterMessage>();
                        //foreach (TwitterMessage item in query.Enumerable<TwitterMessage>().OrderByDescending(x => x.MessageDate))
                        //{
                        //    lstmsg.Add(item);
                        //}
                        return lstmsg;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                        return null;
                    }

                }//End Transaction
            }//End Session
        }


        /// <checkTwitterMessageExists>
        /// Check Twitter Message Exists
        /// </summary>
        /// <param name="Id">Id </param>
        /// <param name="Userid">User id.(Guid)</param>
        /// <param name="messageid">Message id.(string)</param>
        /// <returns>True or False.(bool)</returns>
        public bool checkTwitterMessageExists(string Id, Guid Userid,string messageid)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, open up a Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        //Proceed action, to get twitter messages of twitter account by user id and profile id.
                        NHibernate.IQuery query = session.CreateQuery("from TwitterMessage where UserId = :userid and ProfileId = :Twtuserid and MessageId = :messid");
                        query.SetParameter("userid", Userid);
                        query.SetParameter("Twtuserid", Id);
                        query.SetParameter("messid", messageid);
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


        /// <getAllTwitterMessages>
        /// Get All Twitter Messages
        /// </summary>
        /// <param name="userid">User id.(Guid)</param>
        /// <returns>Return object of TwitterMessage Class with  value of each member in the form of list.(List<TwitterMessage>)</returns>
        public List<TwitterMessage> getAllTwitterMessages(Guid userid)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, open up a Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        //Proceed action, to get all twitter account messages of user by user id.
                        List<TwitterMessage> lstmsg = session.CreateQuery("from TwitterMessage where UserId = :userid")
                        .SetParameter("userid", userid)
                        .List<TwitterMessage>()
                        .ToList<TwitterMessage>();


                        //List<TwitterMessage> lstmsg = new List<TwitterMessage>();
                        //foreach (TwitterMessage item in query.Enumerable<TwitterMessage>().OrderByDescending(x=>x.MessageDate))
                        //{
                        //    lstmsg.Add(item);
                        //}
                        return lstmsg;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                        return null;
                    }

                }//End Transaction
            }//End Session
        }


        /// <getAllTwitterMessagesOfProfile>
        /// Get All Twitter Messages Of Profile
        /// </summary>
        /// <param name="profileid">Twitter profile id.(string)</param>
        /// <returns>Return object of TwitterMessage Class with  value of each member in the form of list.(List<TwitterMessage>)</returns>
        public List<TwitterMessage> getAllTwitterMessagesOfProfile(string profileid)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, open up a Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        //Proceed action, to get all twitter messages of profile by profile id
                        // And order by Entry date.
                        List<TwitterMessage> lstmsg = session.CreateQuery("from TwitterMessage where  ProfileId = :profid ORDER BY EntryDate DESC")
                        .SetParameter("profid", profileid)
                        .List<TwitterMessage>()
                        .ToList<TwitterMessage>();

                        //List<TwitterMessage> lstmsg = new List<TwitterMessage>();
                        //foreach (TwitterMessage item in query.Enumerable<TwitterMessage>().OrderByDescending(x => x.MessageDate))
                        //{
                        //    lstmsg.Add(item);
                        //}
                        return lstmsg;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                        return null;
                    }

                }//End Transaction
            }//End Session
        }


        /// <gettwtMessageStats>
        /// Get Twitter Message Stats
        /// </summary>
        /// <param name="UserId">User id.(Guid)</param>
        /// <param name="days">Number of day's.(int)</param>
        /// <returns>Return values in the form of array list.(ArrayList)</returns>
        public ArrayList gettwtMessageStats(Guid UserId,int days)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, open up a Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        //Select Distinct Count(MessageId) from TwitterMessage where EntryDate>=DATE_ADD(NOW(),INTERVAL -" + days + " DAY) and UserId =:userid Group by DATE_FORMAT(EntryDate,'%y-%m-%d') 
                        NHibernate.IQuery query = session.CreateSQLQuery("SELECT count(Id) FROM TwitterMessage WHERE EntryDate > DATE_SUB(NOW(), INTERVAL "+ days +" DAY) and UserId =:userid GROUP BY WEEK(EntryDate)")
                            .SetParameter("userid", UserId);
                        ArrayList alstFBmsgs = new ArrayList();

                        foreach (var item in query.List())
                        {
                            alstFBmsgs.Add(item);
                        }
                        return alstFBmsgs;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                        return null;
                    }
                }//End Transaction
            }//End Session
        }



        /// <gettwtMessageStatsHome>
        /// Get twitter message stats of home
        /// </summary>
        /// <param name="UserId">User id (Guid)</param>
        /// <param name="days">Number of day's (int)</param>
        /// <returns>Return values in the form of array list.(ArrayList)</returns>
        public ArrayList gettwtMessageStatsHome(Guid UserId, int days)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, open up a Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        //Proceed action, to get total number of twitter messages of user.
                        NHibernate.IQuery query = session.CreateSQLQuery("Select Distinct Count(MessageId) from TwitterMessage where EntryDate>=DATE_ADD(NOW(),INTERVAL -" + days + " DAY) and UserId =:userid Group by DATE_FORMAT(EntryDate,'%y-%m-%d')")
                            .SetParameter("userid", UserId);
                        ArrayList alstFBmsgs = new ArrayList();

                        foreach (var item in query.List())
                        {
                            alstFBmsgs.Add(item);
                        }
                        return alstFBmsgs;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                        return null;
                    }
                }//End Transaction
            }//End Session
        }



        /// <gettwtMessageStatsByProfileId>
        /// Get Twitter Message Stats By Profile Id.
        /// </summary>
        /// <param name="UserId">User id (Guid)</param>
        /// <param name="profileId">Profile id (string)</param>
        /// <param name="days">Number of day's (int)</param>
        /// <returns>Return values in the form of array list.(ArrayList)</returns>
        public ArrayList gettwtMessageStatsByProfileId(Guid UserId,string profileId,int days)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, open up a Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        //Proceed action, to get total number of twitter messages of prifile.
                        NHibernate.IQuery query = session.CreateSQLQuery("Select Distinct Count(MessageId) from TwitterMessage where EntryDate>=DATE_ADD(NOW(),INTERVAL -" + days + " DAY) and UserId =:userid and ProfileId='" + profileId + "' Group by DATE_FORMAT(EntryDate,'%y-%m-%d') ")
                            .SetParameter("userid", UserId);
                        ArrayList alstFBmsgs = new ArrayList();

                        foreach (var item in query.List())
                        {
                            alstFBmsgs.Add(item);
                        }
                        return alstFBmsgs;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                        return null;
                    }
                }//End Transaction
            }//End Session
        }


        /// <gettwtFeedsStats>
        /// Get Twitter Feeds Stats
        /// </summary>
        /// <param name="UserId">User id (Guid)</param>
        /// <param name="days">number of day's (int)</param>
        /// <returns>Return values in the form of array list.(ArrayList)</returns>
        public ArrayList gettwtFeedsStats(Guid UserId,int days)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, open up a Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        //Select Distinct Count(MessageId) from TwitterFeed where EntryDate>=DATE_ADD(NOW(),INTERVAL -7 DAY) and UserId =:userid Group by DATE_FORMAT(EntryDate,'%y-%m-%d') 
                        NHibernate.IQuery query = session.CreateSQLQuery("SELECT count(Id) FROM TwitterFeed WHERE FeedDate > DATE_SUB(NOW(), INTERVAL "+ days +" DAY) and UserId =:userid GROUP BY WEEK(FeedDate)")
                            .SetParameter("userid", UserId);
                        ArrayList alstFBmsgs = new ArrayList();

                        foreach (var item in query.List())
                        {
                            alstFBmsgs.Add(item);
                        }
                        return alstFBmsgs;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                        return null;
                    }
                }//End Transaction
            }//End Session
        }



        /// <gettwtFeedsStatsHome>
        /// Get Twitter Feeds Stats Home
        /// </summary>
        /// <param name="UserId">User id (Guid)</param>
        /// <param name="days">Number of day's (int)</param>
        /// <returns>Return values in the form of array list.(ArrayList)</returns>
        public ArrayList gettwtFeedsStatsHome(Guid UserId,int days)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, open up a Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        //Proceed action, to get total number of messages of user.
                        // Where we are finding messages by number of days.
                        NHibernate.IQuery query = session.CreateSQLQuery("Select Distinct Count(MessageId) from TwitterFeed where EntryDate>=DATE_ADD(NOW(),INTERVAL -"+ days +" DAY) and UserId =:userid Group by DATE_FORMAT(EntryDate,'%y-%m-%d') ")
                            .SetParameter("userid", UserId);
                        ArrayList alstFBmsgs = new ArrayList();

                        foreach (var item in query.List())
                        {
                            alstFBmsgs.Add(item);
                        }
                        return alstFBmsgs;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                        return null;
                    }
                }//End Transaction
            }//End Session
        }


        /// <gettwtFeedsStatsByProfileId>
        /// Get Twitter Feeds Stats By Profile id
        /// </summary>
        /// <param name="UserId"></param>
        /// <param name="profileId"></param>
        /// <param name="days"></param>
        /// <returns></returns>
        public ArrayList gettwtFeedsStatsByProfileId(Guid UserId,string profileId,int days)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, open up a Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        NHibernate.IQuery query = session.CreateSQLQuery("Select Distinct Count(MessageId) from TwitterFeed where EntryDate>=DATE_ADD(NOW(),INTERVAL -"+ days +" DAY) and UserId =:userid and ProfileId=:profileId Group by DATE_FORMAT(EntryDate,'%y-%m-%d') ")
                            .SetParameter("userid", UserId)
                        .SetParameter("profileId", profileId);
                        ArrayList alstFBmsgs = new ArrayList();

                        foreach (var item in query.List())
                        {
                            alstFBmsgs.Add(item);
                        }
                        return alstFBmsgs;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                        return null;
                    }
                }//End Transaction
            }//End Session
        }

        public bool checkTwitterMessageExists(string messageid)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, open up a Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        NHibernate.IQuery query = session.CreateQuery("from TwitterMessage where MessageId = :messid");
                        query.SetParameter("messid", messageid);
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

        public ArrayList getRetweetStatsByProfileId(Guid UserId, string profileId,int days)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, open up a Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        //NHibernate.IQuery query = session.CreateSQLQuery("Select Distinct Count(MessageId) from TwitterMessage where Type=:retweet and EntryDate>=DATE_ADD(NOW(),INTERVAL -" + days + " DAY) and UserId =:userid and ProfileId=:profileId Group by DATE_FORMAT(EntryDate,'%y-%m-%d') ")
                            NHibernate.IQuery query = session.CreateSQLQuery("Select Distinct Count(MessageId) from TwitterMessage where Type=:retweet and UserId =:userid and ProfileId=:profileId")
                            .SetParameter("userid", UserId)
                        .SetParameter("profileId", profileId)
                        //.SetParameter("retweet", "twt_retweets");
                        .SetParameter("retweet", "twt_usertweets");
                        ArrayList alstFBmsgs = new ArrayList();

                        foreach (var item in query.List())
                        {
                            alstFBmsgs.Add(item);
                        }
                        return alstFBmsgs;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                        return null;
                    }
                }//End Transaction
            }//End Session
        }

        public int getRepliesCount(Guid UserId, string profileId)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, open up a Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    int repliesCount = 0;
                    try
                    {                        
                        NHibernate.IQuery query = session.CreateSQLQuery("Select Count(MessageId) from TwitterMessage where InReplyToStatusUserId!=null and UserId =:userid and ProfileId=:profileId")
                            .SetParameter("userid", UserId)
                        .SetParameter("profileId", profileId);
                        ArrayList alstFBmsgs = new ArrayList();

                        foreach (var item in query.List())
                        {
                            repliesCount=int.Parse(item.ToString());
                        }
                        return repliesCount;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                        return repliesCount;
                    }
                }//End Transaction
            }//End Session
        }

        public int getRetweetCount(Guid UserId, string profileId)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, open up a Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    int repliesCount = 0;
                    try
                    {
                        NHibernate.IQuery query = session.CreateSQLQuery("Select Count(MessageId) from TwitterMessage where Type='twt_retweets' and UserId =:userid and ProfileId=:profileId")
                            .SetParameter("userid", UserId)
                        .SetParameter("profileId", profileId);
                        ArrayList alstFBmsgs = new ArrayList();

                        foreach (var item in query.List())
                        {
                            repliesCount = int.Parse(item.ToString());
                        }
                        return repliesCount;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                        return repliesCount;
                    }
                }//End Transaction
            }//End Session
        }

        public int getUserRetweetCount(Guid UserId)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, open up a Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    int repliesCount = 0;
                    try
                    {
                        NHibernate.IQuery query = session.CreateSQLQuery("Select Count(MessageId) from TwitterMessage where Type='twt_retweets' and UserId =:userid")
                            .SetParameter("userid", UserId);
                        ArrayList alstFBmsgs = new ArrayList();

                        foreach (var item in query.List())
                        {
                            repliesCount = int.Parse(item.ToString());
                        }
                        return repliesCount;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                        return repliesCount;
                    }
                }//End Transaction
            }//End Session
        }

        public int updateScreenName(string profileid, string screenname)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, open up a Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        int i = session.CreateQuery("Update TwitterMessage set ScreenName =:twtScreenName where ProfileId = :id")
                                   .SetParameter("twtScreenName", screenname)
                                   .SetParameter("id", profileid)
                                   .ExecuteUpdate();
                        transaction.Commit();
                        return i;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                        return 0;
                    }
                }//End Transaction
            }//End Session
        }

        public List<TwitterMessage> getUnreadMessages(Guid UserId, string Profileid)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, open up a Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        NHibernate.IQuery query = session.CreateQuery("from TwitterMessage where ReadStatus=0 and UserId= :userid and ProfileId =:profid ORDER BY EntryDate DESC")
                                                    .SetParameter("userid", UserId)
                                                    .SetParameter("profid", Profileid);
                        List<TwitterMessage> lsttwtMessage = new List<TwitterMessage>();
                        foreach (TwitterMessage item in query.Enumerable<TwitterMessage>())
                        {
                            lsttwtMessage.Add(item);
                        }
                        return lsttwtMessage;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                        return null;
                    }
                }//End Transaction
            }//End Session
        }

        public List<TwitterMessage> getUnreadMessages(string Profileid)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, open up a Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        NHibernate.IQuery query = session.CreateQuery("from TwitterMessage where ReadStatus=0 and ProfileId =:profid")
                                                   
                                                    .SetParameter("profid", Profileid);
                        List<TwitterMessage> lsttwtMessage = new List<TwitterMessage>();
                        foreach (TwitterMessage item in query.Enumerable<TwitterMessage>())
                        {
                            lsttwtMessage.Add(item);
                        }
                        return lsttwtMessage;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                        return null;
                    }
                }//End Transaction
            }//End Session
        }

        public int getCountUnreadMessages(Guid UserId)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, open up a Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        NHibernate.IQuery query = session.CreateQuery("from TwitterMessage where ReadStatus=0 and UserId= :userid")
                                                    .SetParameter("userid", UserId);
                                                    
                        int i = 0;
                        foreach (TwitterMessage item in query.Enumerable<TwitterMessage>())
                        {
                            i++;
                        }
                       return i;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                        return 0;
                    }
                }//End Transaction
            }//End Session
        }

        public int updateMessageStatus(Guid UserId)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, open up a Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        int i = session.CreateQuery("Update TwitterMessage set ReadStatus =1 where UserId = :id")
                                   .SetParameter("id", UserId)
                                   .ExecuteUpdate();
                        transaction.Commit();
                        return i;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                        return 0;
                    }
                }//End Transaction
            }//End Session
        }


        public ArrayList getpostrepliesgraph(Guid userid, int start, int end)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, open up a Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        //Select Distinct Count(MessageId) from FacebookMessage where MessageDate>=DATE_ADD(NOW(),INTERVAL -7 DAY) and UserId =:userid Group by DATE_FORMAT(MessageDate,'%y-%m-%d')
                        if (end == 0)
                        {
                            NHibernate.IQuery query = session.CreateSQLQuery("select ts.DMRecievedCount from TwitterStats ts,SocialProfile sp where ts.FbUserId= sp.ProfileId and sp.UserId=" + userid + " and sp.ProfileType='twitter' limit"+start+","+end+"");
                            query.SetParameter("userid", userid);
                            ArrayList alstFBmsgs = new ArrayList();

                            foreach (var item in query.List())
                            {
                                alstFBmsgs.Add(item);
                            }
                            return alstFBmsgs;
                        }
                        else
                        {
                            NHibernate.IQuery query = session.CreateSQLQuery("select ts.DMRecievedCount from TwitterStats ts,SocialProfile sp where ts.FbUserId= sp.ProfileId and sp.UserId=" + userid + " and sp.ProfileType='twitter'");
                            query.SetParameter("userid", userid);
                            ArrayList alstFBmsgs = new ArrayList();

                            foreach (var item in query.List())
                            {
                                alstFBmsgs.Add(item);
                            }
                            return alstFBmsgs;
                        }


                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                        return null;
                    }
                }//End Transaction
            }//End Session
        }


        public int DeleteTwitterMessageByUserid(Guid userid)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, open up a Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        NHibernate.IQuery query = session.CreateQuery("delete from TwitterMessage where UserId = :userid")
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

    }
}
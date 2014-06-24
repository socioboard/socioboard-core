using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SocioBoard.Domain;
using SocioBoard.Helper;
using System.Collections;

namespace SocioBoard.Model
{
    public class TwitterDirectMessageRepository : ITwitterDirectMessagesRepository
    {
        /// <addNewDirectMessage>
        /// Add New Direct Message
        /// </summary>
        /// <param name="twtDirectMessages">Set Values in a TwitterDirectMessages Class Property and Pass the Object of TwitterDirectMessages Class.(Domein.TwitterDirectMessages)</param>
        public void addNewDirectMessage(TwitterDirectMessages twtDirectMessages)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    //Proceed action, to save data.
                    session.Save(twtDirectMessages);
                    transaction.Commit();
                }//End Transaction
            }//End Session
        }


        /// <deleteDirectMessage>
        /// Delete Direct Message by user id and profile id.
        /// </summary>
        /// <param name="userid">User id.(Guid)</param>
        /// <param name="profileid">Profile id.(String)</param>
        /// <returns>Return 1 for success and 0 for failure.(int) </returns>
        public int deleteDirectMessage(Guid userid,string profileid)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        //Proceed action, to delete Twitter direct message.
                        NHibernate.IQuery query = session.CreateQuery("delete from TwitterDirectMessages where SenderId = :twtuserid and UserId = :userid")
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

        public void updateDirectMessage(TwitterDirectMessages twtDirectMessages)
        {
            throw new NotImplementedException();
        }


        /// <getAllDirectMessagesByScreenName>
        /// Get All Direct Messages By ScreenName
        /// </summary>
        /// <param name="screenName">Twitter account screen name.(String)</param>
        /// <returns>Return object of TwitterDirectMessages Class with  value of each member in the form of list.(List<TwitterDirectMessages>)</returns>
        public List<TwitterDirectMessages> getAllDirectMessagesByScreenName(string screenName)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    //Proceed action, to get Messages by screen name.
                    List<TwitterDirectMessages> alstFBAccounts = session.CreateQuery("from TwitterDirectMessages where SenderScreenName = :teamid")
                    .SetParameter("teamid", screenName)
                    .List<TwitterDirectMessages>()
                    .ToList<TwitterDirectMessages>();

                    //List<TwitterDirectMessages> alstFBAccounts = new List<TwitterDirectMessages>();

                    //foreach (TwitterDirectMessages item in query.Enumerable())
                    //{
                    //    alstFBAccounts.Add(item);
                    //}
                    return alstFBAccounts;

                }//End Transaction
            }//End Session
        }


        /// <checkExistsDirectMessages>
        /// Check Exist Direct Messages
        /// </summary>
        /// <param name="MessageId">Message id.(String)</param>
        /// <returns>True and False.(bool)</returns>
        public bool checkExistsDirectMessages(string MessageId)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        //Proceed action, to get message by message id.
                        NHibernate.IQuery query = session.CreateQuery("from TwitterDirectMessages where MessageId = :userid ");
                        query.SetParameter("userid", MessageId);

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


        /// <getAllDirectMessagesById>
        /// Get All Direct Messages By Id.
        /// </summary>
        /// <param name="profileid">Twitter account pofile id.(string)</param>
        /// <returns>Return object of TwitterDirectMessages Class with  value of each member in the form of list.(List<TwitterDirectMessages>)</returns>
        public List<TwitterDirectMessages> getAllDirectMessagesById(string profileid)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    //Proceed action, to get Twitter direct message by profile id.
                    List<TwitterDirectMessages> alstFBAccounts = session.CreateQuery("from TwitterDirectMessages where SenderId = :teamid")
                    .SetParameter("teamid", profileid)
                    .List<TwitterDirectMessages>()
                    .ToList<TwitterDirectMessages>();

                    //List<TwitterDirectMessages> alstFBAccounts = new List<TwitterDirectMessages>();

                    //foreach (TwitterDirectMessages item in query.Enumerable())
                    //{
                    //    alstFBAccounts.Add(item);
                    //}

                    return alstFBAccounts;
                }//End Transaction
            }//End Session
        }


        /// <gettwtDMRecieveStatsByProfileId>
        /// Get twitter direct messages recieve stats by profile id.
        /// </summary>
        /// <param name="UserId">user id.(Guid)</param>
        /// <param name="profileId">Profile id.(String)</param>
        /// <returns>Return values in the form of array list.(ArrayList)</returns>
        public ArrayList gettwtDMRecieveStatsByProfileId(Guid UserId, string profileId)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        //Proceed action, to get total count of message for last 7 day's by sender id and profileId.
                        NHibernate.IQuery query = session.CreateSQLQuery("Select Distinct Count(MessageId) from TwitterDirectMessages where EntryDate>=DATE_ADD(NOW(),INTERVAL -7 DAY) and UserId =:userid and RecipientId='" + profileId + "' Group by DATE_FORMAT(EntryDate,'%y-%m-%d') ")
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


        /// <gettwtDMSendStatsByProfileId>
        /// Get twitter direct messages send stats by profile id.
        /// </summary>
        /// <param name="UserId">User id.(Guid)</param>
        /// <param name="profileId">Profile id.(String)</param>
        /// <returns>Return values in the form of array list.(ArrayList)</returns>
        public ArrayList gettwtDMSendStatsByProfileId(Guid UserId, string profileId)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        //Proceed action, to get total count of message for last 7 day's by sender id.
                        NHibernate.IQuery query = session.CreateSQLQuery("Select Distinct Count(MessageId) from TwitterDirectMessages where EntryDate>=DATE_ADD(NOW(),INTERVAL -7 DAY) and UserId =:userid and SenderId='" + profileId + "' Group by DATE_FORMAT(EntryDate,'%y-%m-%d') ")
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


        /// <DeleteTwitterDirectMessagesByUserid>
        /// Delete Twitter Direct Messages By User id.
        /// </summary>
        /// <param name="userid">User id.(guid)</param>
        /// <returns>Return 1 for success and 0 for failure.(int) </returns>
        public int DeleteTwitterDirectMessagesByUserid(Guid userid)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        //Delete twitter direct message of user by user id.
                        NHibernate.IQuery query = session.CreateQuery("delete from TwitterDirectMessages where UserId = :userid")
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
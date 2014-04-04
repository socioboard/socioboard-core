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

        /// <addFacebookMessage>
        /// Add new Facebook Message
        /// </summary>
        /// <param name="fbmsg">Set Values in a FacebookMessage Class Property and Pass the same Object of FacebookMessage Class.(Domain.FacebookMessage)</param>
        public void addFacebookMessage(FacebookMessage fbmsg)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    //Proceed action, to save data.
                    session.Save(fbmsg);
                    transaction.Commit();
                }//End Transaction
            }//End session
        }


        public int deleteFacebookMessage(FacebookMessage fbaccount)
        {
            throw new NotImplementedException();
        }

        public int updateFacebookMessage(FacebookMessage fbaccount)
        {
            throw new NotImplementedException();
        }


        /// <getAllFacebookMessagesOfUser>
        /// Get all Facebook Message of User by UserId(Guid) and profileId(String).
        /// </summary>
        /// <param name="UserId">UserId of FacebookMessage(Guid)</param>
        /// <param name="profileId">profileId of FacebookMessage(string)</param>
        /// <returns>Return all Facebook Massage of User in the form List type.</returns>
        public List<FacebookMessage> getAllFacebookMessagesOfUser(Guid UserId, string profileId)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {

                        //Proceed action to get all Facebook Message of User.
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

                }//End Transaction
            }//End session
        }



        /// <getAllFacebookMessagesOfUsers>
        /// Get all Facebook Message of all User by UserId(Guid) and profileId(String).
        /// </summary>
        /// <param name="UserId">UserId FacebookMessage(Guid)</param>
        /// <param name="profileId">profileId FacebookMessage(string)</param>
        public void getAllFacebookMessagesOfUsers(Guid UserId, string profileId)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {

                        //Proceed action to get all Facebook Message of all User.
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

                }//End Transaction
            }//End session
        }



        /// <checkFacebookMessageExists>
        ///  Check FacebookMessage is Exist or not in Database by Userid(Guid) Id(String).
        /// </summary>
        /// <param name="Id">Id FacebookMessage(MessageId)</param>
        /// <param name="UserId">UserId FacebookMessage(Guid)</param>
        /// <returns>Return true or false</returns>
        public bool checkFacebookMessageExists(string Id, Guid Userid)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {

                        //Proceed action to Check FacebookMessage is Exist or not in Database.
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

                }//End Transaction
            }//End session
        }


        /// <deleteAllMessagesOfUser>
        /// Delete all message of user from database by userId(Guid) and fbUserid(String).
        /// </summary>
        /// <param name="fbuserid">fbUserId FacebookMessage(String)</param>
        /// <param name="UserId">UserId FacebookMessage(Guid)</param>
        public void deleteAllMessagesOfUser(string fbuserid, Guid userid)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {

                        //Proceed action, to delete all message of user from database.
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

                }//End Transaction
            }//End session
        }


        /// <getFacebookUserWallPost>
        ///  Get all wall post of Facebook User by UserId(Guid) and ProfileId(String).
        /// </summary>
        /// <param name="userid">userid FacebookMessage(Guid)</param>
        /// <param name="profileid">profileid FacebookMessage(string)</param>
        /// <returns>Return a object of FacebookMessage Class with  value of each member in form List type.</returns>
        public List<FacebookMessage> getFacebookUserWallPost(Guid userid, string profileid)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {

                        //Proceed action to, get all wall post of Facebook User.
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

                }//End Transaction
            }//End session
        }


        /// <getFacebookUserWallPost>
        ///  Get all wall post of Facebook User by UserId(Guid) and ProfileId(String).
        /// </summary>
        /// <param name="userid">userid FacebookMessage(Guid)</param>
        /// <param name="profileid">profileid FacebookMessage(string)</param>
        /// <param name="count">Count is used for counting upto 10 results.</param>
        /// <returns>Return a object of FacebookMessage Class with  value of each member in form List type.</returns>
        public List<FacebookMessage> getFacebookUserWallPost(Guid userid, string profileid, int count)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {

                        //Proceed action to, get Maximum 10 wall post of Facebook User.
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

                }//End Transaction
            }//End session
        }


        /***********************************************************************************************/


        /// <getAllMessageOfProfile>
        /// Get all Message from facebookmessage by ProfileId(string).
        /// </summary>
        /// <param name="profileid">profileId FacebookMessage(String)</param>
        /// <returns>Return all Message on behalf of ProfileId in form List type.</returns>
        public List<FacebookMessage> getAllMessageOfProfile(string profileid)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {

                        //Proceed action, to Get all Message from FacebookMessage.
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

                }//End Transaction
            }//End session

        }


        /// <getAllWallpostsOfProfile>
        /// Get all Wallpost of User by ProfileId(string)
        /// </summary>
        /// <param name="profileid">ProfileId FacebookMessage(String)</param>
        /// <returns>Return all Wall post on behalf of ProfileId in form List type.</returns>
        public List<FacebookMessage> getAllWallpostsOfProfile(string profileid)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {

                        //Proceed action,to Get all Wallpost of User from FacebookMessage.
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

                }//End Transaction
            }//End session
        }



        /// <getAllWallpostsOfProfile>
        ///  Get all Wallpost of User by ProfileId(string)
        /// </summary>
        /// <param name="profileid">profileId FacebookMessage(String)</param>
        /// <param name="count">Count is used for counting upto 10 results.</param>
        /// <returns>Return all Message on behalf of ProfileId in form List type.</returns>
        public List<FacebookMessage> getAllWallpostsOfProfile(string profileid, int count)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        //Proceed action to get all Wallpost of User from FacebookMessage.
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

                }//End Transaction
            }//End session
        }



        /// <checkFacebookMessageExists>
        /// Check Message is Exist in database or not.
        /// </summary>
        /// <param name="Id">Id FacebookMessage(String)</param>
        /// <returns>Return true or false</returns>
        public bool checkFacebookMessageExists(string Id)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        //Proceed action to Check Message is Exist or not.
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

                }//End Transaction
            }//End session
        }



        /// <getAllInboxMessages>
        /// Get All Inbox Messages from FacebookMessage
        /// </summary>
        /// <param name="profileid">profileId FacebookMessage(String)</param>
        /// <returns>Return all Inbox Message on behalf of ProfileId in form List type.</returns>
        public List<FacebookMessage> getAllInboxMessages(string profileid)
        {

            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {

                        //Proceed action to get All Inbox Messages from FacebookMessage.
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

                }//End Transaction
            }//End session
        }



        /// <getAllSentMessages>
        /// Get all Sent message from Facebook Message.
        /// </summary>
        /// <param name="profileid">profileId FacebookMessage(String)</param>
        /// <returns>Return all Sent message on behalf of ProfileId in form List type.</returns>
        public List<FacebookMessage> getAllSentMessages(string profileid)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {

                        //Proceed action to get all Sent message from Facebook Message.
                        //List<FacebookMessage> lstfbmsg = session.CreateQuery("from FacebookMessage where Type = 'fb_tag' and ProfileId = :profid")
                        List<FacebookMessage> lstfbmsg = session.CreateQuery("from FacebookMessage where ProfileId = :profid ORDER BY MessageDate DESC")
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

                }//End Transaction
            }//End session
        }



        /// <DeleteFacebookMessageByUserid>
        /// Delete a Facebook Message from database by UserId(Guid).
        /// </summary>
        /// <param name="userid">UserId(Guid)</param>
        /// <returns>return Integer 1 for true 0 for false.</returns>
        public int DeleteFacebookMessageByUserid(Guid userid)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {

                        //Proceed action to Delete a Facebook Message from database by UserId(Guid).
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
                }//End Transaction
            }//End session
        }



    }
}
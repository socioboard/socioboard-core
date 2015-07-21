using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Domain.Socioboard.Domain;
using NHibernate.Linq;
using Api.Socioboard.Helper;

namespace Api.Socioboard.Services
{
    public class InstagramFeedRepository : IInstagramFeed
    {

        /// <addInstagramFeed>
        /// Add new instagram feed
        /// </summary>
        /// <param name="insfeed">Set Values in a InstagramFeed Class Property and Pass the same Object of InstagramFeed Class.(Domain.InstagramFeed)</param>
        public void addInstagramFeed(Domain.Socioboard.Domain.InstagramFeed insfeed)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    //Save and commit new data.
                    session.Save(insfeed);
                    transaction.Commit();
                }//End Transaction
            }//End Session
        }

        public int deleteInstagramFeed(Domain.Socioboard.Domain.InstagramFeed fbfeed)
        {
            throw new NotImplementedException();
        }

        public int updateInstagramFeed(Domain.Socioboard.Domain.InstagramFeed fbfeed)
        {
            throw new NotImplementedException();
        }


        /// <getAllInstagramFeedsOfUser>
        /// Get all instagram feeds of user
        /// </summary>
        /// <param name="UserId">User id(Guid)</param>
        /// <param name="profileid">profileid of Instagram(String)</param>
        /// <returns>Return a object of InstagramFeed Class with  value of each member in form of List type.</returns>
        public List<Domain.Socioboard.Domain.InstagramFeed> getAllInstagramFeedsOfUser(Guid UserId, string profileid)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        //Proceed acction, to get instagram feed.
                        List<Domain.Socioboard.Domain.InstagramFeed> alst = session.CreateQuery("from InstagramFeed where UserId = :userid and InstagramId = :profileId")
                      .SetParameter("userid", UserId)
                       .SetParameter("profileId", profileid)
                       .List<Domain.Socioboard.Domain.InstagramFeed>()
                       .ToList<Domain.Socioboard.Domain.InstagramFeed>();

                        #region oldcode
                        //List<InstagramFeed> alst = new List<InstagramFeed>();
                        //foreach (InstagramFeed item in query.Enumerable())
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
            }//End Session
        }

        /// <checkInstagramFeedExists>
        /// Check if instagram feed is exist or not in database by feed id(String) and Userid(Guid).
        /// </summary>
        /// <param name="feedid">feedid InstagramComment(String)</param>
        /// <param name="Userid">Userid InstagramComment(Guid)</param>
        /// <returns>Return true or false (bool) </returns>
        public bool checkInstagramFeedExists(string feedid, Guid Userid)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        //Proceed action, to Check feed.
                        NHibernate.IQuery query = session.CreateQuery("from InstagramFeed where UserId = :userid and FeedId = :msgid");
                        query.SetParameter("userid", Userid);
                        query.SetParameter("msgid", feedid);
                        var resutl = query.UniqueResult();

                        if (resutl == null)
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


        /// <deleteAllFeedsOfUser>
        /// Delete all feeds of user from database by facebook user id(String) and User id(Guid).
        /// </summary>
        /// <param name="fbuserid">facebook userid (String)</param>
        /// <param name="userid">userid(Guid)</param>
        public void deleteAllFeedsOfUser(string fbuserid, Guid userid)
        {
           //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        //Proceed action, to Delete instagram feed.
                        NHibernate.IQuery query = session.CreateQuery("delete from InstagramFeed where UserId = :userid and InstagramId = :profileId");
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
            }//End Session
        }


        /// <getAllInstagramFeedsOfUser>
        /// Get all instagram feeds of user.
        /// </summary>
        /// <param name="UserId">userid(Guid)</param>
        /// <param name="profileid">Profile id of instagram(String)</param>
        /// <param name="count">Get the toral numbert of data(int)</param>
        /// <returns>Return a object of InstagramFeed Class with  value of each member in form of List type.(List<InstagramFeed>)</returns>
        public List<Domain.Socioboard.Domain.InstagramFeed> getAllInstagramFeedsOfUser(Guid UserId, string profileid, int noOfDataToSkip, int noOfDataFromTop)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        //Proceed action, to get feeds
                        List<Domain.Socioboard.Domain.InstagramFeed> alst = session.CreateQuery("from InstagramFeed where UserId = :userid and InstagramId = :profileId Order By FeedDate DESC")
                        .SetParameter("userid", UserId)
                        .SetParameter("profileId", profileid)
                        .SetFirstResult(noOfDataToSkip)
                        .SetMaxResults(noOfDataFromTop)
                        .List<Domain.Socioboard.Domain.InstagramFeed>()
                        .ToList<Domain.Socioboard.Domain.InstagramFeed>();

                        #region oldcode
                        //List<InstagramFeed> alst = new List<InstagramFeed>();
                        //foreach (InstagramFeed item in query.Enumerable())
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
            }//End Session
        }

        public List<Domain.Socioboard.Domain.InstagramFeed> GetFeedsOfProfileWithRange(string profileid, Guid id, int noOfDataToSkip)
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
                        //List<Domain.Socioboard.Domain.TumblrFeed> lstmsg = session.CreateQuery("from TumblrFeed where  ProfileId = :profid and UserId=:userid ")
                        //.SetParameter("profid", profileid)
                        //.SetParameter("userid", id)
                        //.List<Domain.Socioboard.Domain.TumblrFeed>()
                        //.ToList<Domain.Socioboard.Domain.TumblrFeed>();

                        List<Domain.Socioboard.Domain.InstagramFeed> lstmsg = session.Query<Domain.Socioboard.Domain.InstagramFeed>().Where(u => u.UserId == id && u.InstagramId.Equals(profileid)).OrderByDescending(x => x.EntryDate).Skip(Convert.ToInt32(noOfDataToSkip)).Take(15).ToList<Domain.Socioboard.Domain.InstagramFeed>();

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
         /// <DeleteInstagramCommentByUserid>
        /// Delete Instagram feed by userid.
        /// </summary>
        /// <param name="userid">User id(Guid)</param>
        /// <returns>Return integer 1 for true and 0 for false.</returns>
        public int DeleteInstagramFeedByUserid(Guid userid)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        //Proceed action, to delete user feed.
                        NHibernate.IQuery query = session.CreateQuery("delete from InstagramFeed where UserId = :userid")
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
                }//End transaction
            }//End session
        }

        public int UpdateLikesOfProfile(string FeedId, int IsLike, int LikeCount)
        {
            int update = 0;

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
                            //Proceed action, to update Data
                            // And Set the reuired paremeters to find the specific values.
                            update = session.CreateQuery("Update InstagramFeed set IsLike = :Islike,LikeCount = : likecount where  FeedId = :feedid")
                                .SetParameter("feedid", FeedId)
                                .SetParameter("likecount", LikeCount)
                                  .SetParameter("Islike", IsLike)
                                .ExecuteUpdate();

                            transaction.Commit();
                            update = 1;
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.StackTrace);
                            update = 0;
                        }
                    }//End Transaction
                }//End session
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error : " + ex.StackTrace);
            }

            return update;
        }




    }
}
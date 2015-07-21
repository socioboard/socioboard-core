using Api.Socioboard.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Domain.Socioboard.Domain;

namespace Api.Socioboard.Services
{
    public class InstagramCommentRepository : IInstagramComment
    {
        /// <addInstagramComment>
        /// Add Instagram comment to database
        /// </summary>
        /// <param name="inscomment">Set Values in a InstagramComment Class Property and Pass the same Object of InstagramComment Class.(Domain.InstagramComment)</param>
        public void addInstagramComment(Domain.Socioboard.Domain.InstagramComment inscomment)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    //Proceed action to Save data.
                    session.Save(inscomment);
                    transaction.Commit();
                }//End Transaction
            }//End session
        }


        public int deleteInstagramComment(Domain.Socioboard.Domain.InstagramComment inscomment)
        {
            throw new NotImplementedException();
        }

        public int updateInstagramComment(Domain.Socioboard.Domain.InstagramComment inscomment)
        {
            throw new NotImplementedException();
        }


        /// <getAllInstagramCommentsOfUser>
        /// Get Instagram Comment of user by UserId(Guid) ProfileId(string) and feedid(string).
        /// </summary>
        /// <param name="UserId">Userid InstagramComment(Guid)</param>
        /// <param name="profileid">profileid InstagramComment(String)</param>
        /// <param name="feedid">feedid InstagramComment(String)</param>
        /// <returns>Return a object of InstagramComment Class with  value of each member in form List type.</returns>
        public List<Domain.Socioboard.Domain.InstagramComment> getAllInstagramCommentsOfUser(Guid UserId, string feedid)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        //Proceed action to get Instagram Comment of user from Database.
                        // And Set the reuired paremeters to find the specific values.
                        List<Domain.Socioboard.Domain.InstagramComment> alst = session.CreateQuery("from InstagramComment where UserId = :userid and FeedId=:feedid Order By CommentDate DESC")
                        .SetParameter("userid", UserId)
                        .SetParameter("feedid", feedid)
                        .List<Domain.Socioboard.Domain.InstagramComment>()
                        .ToList<Domain.Socioboard.Domain.InstagramComment>();
                        #region Oldcode
                        //List<InstagramComment> alst = new List<InstagramComment>();
                        //foreach (InstagramComment item in query.Enumerable())
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


        /// <checkInstagramCommentExists>
        /// Check if instagram Comments is exist or not in database by feedid(String) and Userid(Guid).
        /// </summary>
        /// <param name="feedid">feedid InstagramComment(String)</param>
        /// <param name="Userid">Userid InstagramComment(Guid)</param>
        /// <returns>Return true or false </returns>
        public bool checkInstagramCommentExists(string feedid, Guid Userid)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {

                        //Proceed action to check if instagram Comments is exist or not in database.
                        NHibernate.IQuery query = session.CreateQuery("from InstagramComment where UserId = :userid and CommentId = :msgid");
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

                }//End Transaction
            }//End session
        }


        /// <deleteAllCommentsOfUser>
        /// Delete all comments of user from database by InstagramId(String) and UserId(Guid).
        /// </summary>
        /// <param name="fbuserid">fbuserid InstagramComment(String)</param>
        /// <param name="userid">userid InstagramComment(Guid)</param>
        public void deleteAllCommentsOfUser(string fbuserid, Guid userid)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {

                        //Proceed action to Delete all comments of user from database.
                        NHibernate.IQuery query = session.CreateQuery("delete from InstagramComment where UserId = :userid and InstagramId = :profileId");
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


        /// <DeleteInstagramCommentByUserid>
        /// Delete Instagram comment from database by userid.
        /// </summary>
        /// <param name="userid">Userid InstagramComment(Guid)</param>
        /// <returns>Return integer 1 for true and 0 for false.</returns>
        public int DeleteInstagramCommentByUserid(Guid userid)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {

                        //delete Instagram comment from database.
                        NHibernate.IQuery query = session.CreateQuery("delete from InstagramComment where UserId = :userid")
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
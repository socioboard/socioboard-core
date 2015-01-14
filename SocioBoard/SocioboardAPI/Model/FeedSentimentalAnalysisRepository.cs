using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Api.Socioboard.Helper;

namespace Api.Socioboard.Model
{
    public class FeedSentimentalAnalysisRepository
    {
        /// <AddDrafts>
        /// Add new draft
        /// </summary>
        /// <param name="d">Set Values in a Draft Class Property and Pass the Object of Draft Class (SocioBoard.Domain.Draft).</param>
        public void Add(Domain.Socioboard.Domain.FeedSentimentalAnalysis _FeedSentimentalAnalysis)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //Begin session trasaction and opens up.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    //Proceed action , to save data.
                    session.Save(_FeedSentimentalAnalysis);
                    transaction.Commit();
                }//End transaction
            }//End session
        }

        /// <checkTwitterFeedExists>
        /// Check Feed is Exists or not
        /// </summary>
        /// <param name="Id">Id of feed.(string)</param>
        /// <param name="Userid">User id.(Guid)</param>
        /// <param name="messageId">feed id of feed.(string) </param>
        /// <returns>True or False.(bool)</returns>
        public bool checkFeedExists(string ProfileId, Guid UserId, string FeedId)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        //Proceed action, to find twitter feeds by user id, profile id and message id.
                        NHibernate.IQuery query = session.CreateQuery("from FeedSentimentAlanalysis where UserId = :UserId and ProfileId = :ProfileId and FeedId = :FeedId");
                        query.SetParameter("UserId", UserId);
                        query.SetParameter("ProfileId", ProfileId);
                        query.SetParameter("FeedId", FeedId);
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

        /// <summary>
        /// Get All Negative Feeds of User  
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns></returns>
        public List<Domain.Socioboard.Domain.FeedSentimentalAnalysis> getAllNegativeFeedsOfProfile(string ProfileId)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        Guid AssigneUserId = Guid.Parse("00000000-0000-0000-0000-000000000000");
                        List<Domain.Socioboard.Domain.FeedSentimentalAnalysis> lstmsg = session.CreateQuery("from FeedSentimentalAnalysis where ProfileId = :ProfileId and Negative < 0.8 and AssigneUserId = :AssigneUserId")
                        .SetParameter("ProfileId", ProfileId)
                        .SetParameter("AssigneUserId", AssigneUserId)
                        .List<Domain.Socioboard.Domain.FeedSentimentalAnalysis>()
                        .ToList<Domain.Socioboard.Domain.FeedSentimentalAnalysis>();

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

        /// <summary>
        /// Get All profiles
        /// </summary>
        /// <returns></returns>
        public List<Domain.Socioboard.Domain.FeedSentimentalAnalysis> getAllProfiles()
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        List<Domain.Socioboard.Domain.FeedSentimentalAnalysis> lstmsg = session.CreateQuery("from FeedSentimentalAnalysis group by ProfileId")

                        .List<Domain.Socioboard.Domain.FeedSentimentalAnalysis>()
                        .ToList<Domain.Socioboard.Domain.FeedSentimentalAnalysis>();

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

        /// <summary>
        /// Update Assigned UserId of Negative Feed
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="AssignedUserId"></param>
        public int updateAssignedStatus(Guid Id, Guid AssignedUserId)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {

                    try
                    {
                        //Update status
                        int i = session.CreateQuery("Update FeedSentimentalAnalysis set AssigneUserId = :AssignedUserId  where Id = :Id")
                          .SetParameter("Id", Id)
                          .SetParameter("AssignedUserId", AssignedUserId)
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
     
        /// <summary>
        /// Get Tickets of User
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns></returns>
        public List<Domain.Socioboard.Domain.FeedSentimentalAnalysis> getNegativeFeedsOfUser(Guid UserId)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        List<Domain.Socioboard.Domain.FeedSentimentalAnalysis> lstmsg = session.CreateQuery("from FeedSentimentalAnalysis where AssigneUserId =: UserId")
                        .SetParameter("UserId", UserId)
                        .List<Domain.Socioboard.Domain.FeedSentimentalAnalysis>()
                        .ToList<Domain.Socioboard.Domain.FeedSentimentalAnalysis>();

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

        public List<Domain.Socioboard.Domain.FeedSentimentalAnalysis> getAllNegativeFeedsOfUser(Guid UserId)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        List<Domain.Socioboard.Domain.FeedSentimentalAnalysis> lstmsg = session.CreateQuery("from FeedSentimentalAnalysis where UserId =: UserId and Negative < 0.8")
                        .SetParameter("UserId", UserId)
                        .List<Domain.Socioboard.Domain.FeedSentimentalAnalysis>()
                        .ToList<Domain.Socioboard.Domain.FeedSentimentalAnalysis>();

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

    }
}
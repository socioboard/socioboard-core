using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Domain.Socioboard.Domain;
using NHibernate.Transform;
using System.Collections;
using System.Data;
using NHibernate.Linq;
using Api.Socioboard.Helper;

namespace Api.Socioboard.Services
{
    public class FbPagePostRepository
    {
        /// <addFbPagePost>
        /// Add new FbPagePost
        /// </summary>
        /// <param name="fbmsg">Set Values in a FbPagePost Class Property and Pass the same Object of FbPagePost Class.(Domain.FbPagePost)</param>
        public void addFbPagePost(Domain.Socioboard.Domain.FbPagePost _FbPagePost)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    //Proceed action, to save data.
                    session.Save(_FbPagePost);
                    transaction.Commit();
                }//End Transaction
            }//End session
        }



        public List<Domain.Socioboard.Domain.FbPagePost> getAllPost(string profileId, Guid userid)
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
                        List<Domain.Socioboard.Domain.FbPagePost> alst = session.CreateQuery("from FbPagePost where UserId = :userid and PageId = :profileId order by PostDate desc")
                         .SetParameter("userid", userid)
                         .SetParameter("profileId", profileId)
                         .List<Domain.Socioboard.Domain.FbPagePost>()
                         .ToList<Domain.Socioboard.Domain.FbPagePost>();

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


        public Domain.Socioboard.Domain.FbPagePost GetPostDetails(Guid id)
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
                        List<Domain.Socioboard.Domain.FbPagePost> alst = session.CreateQuery("from FbPagePost where Id = :id")
                         .SetParameter("id", id)
                         .List<Domain.Socioboard.Domain.FbPagePost>()
                         .ToList<Domain.Socioboard.Domain.FbPagePost>();

                        return alst[0];

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                        return null;
                    }

                }//End Transaction
            }//End session
        }

        public bool IsPostExist(Domain.Socioboard.Domain.FbPagePost _FbPagePost)
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
                        List<Domain.Socioboard.Domain.FbPagePost> alst = session.CreateQuery("from FbPagePost where UserId = :userid and PageId = :pageid and PostId = :postid and FromId = :fromid")
                         .SetParameter("userid", _FbPagePost.UserId)
                         .SetParameter("pageid", _FbPagePost.PageId)
                         .SetParameter("postid", _FbPagePost.PostId)
                         .SetParameter("fromid", _FbPagePost.FromId)
                         .List<Domain.Socioboard.Domain.FbPagePost>()
                         .ToList<Domain.Socioboard.Domain.FbPagePost>();

                        if (alst.Count > 0)
                            return true;
                        else
                            return false;

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                        return true;
                    }

                }//End Transaction
            }//End session
        }

        public int UpdateFbPagePostStatus(Domain.Socioboard.Domain.FbPagePost _FbPagePost)
        {
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        int i = session.CreateQuery("Update FbPagePost set Likes =:Likes,Comments =:Comments,Shares=:Shares where PostId =:PostId")
                                                  .SetParameter("Likes", _FbPagePost.Likes)
                                                  .SetParameter("Comments", _FbPagePost.Comments)
                                                  .SetParameter("Shares", _FbPagePost.Shares)
                                                  .SetParameter("PostId", _FbPagePost.PostId)
                                                  .ExecuteUpdate();
                        transaction.Commit();
                        return i;
                    }
                    catch (Exception ex)
                    {
                        return 0;
                    }
                }
            }
        }

    }
}
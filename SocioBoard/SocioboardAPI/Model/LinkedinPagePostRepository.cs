using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Domain.Socioboard.Domain;
using System.Collections;
using Api.Socioboard.Helper;

namespace Api.Socioboard.Model
{
    public class LinkedinPagePostRepository
    {
        public void addLinkedInPagepost(Domain.Socioboard.Domain.LinkedinCompanyPagePosts lipost)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction. 
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    session.Save(lipost);
                    transaction.Commit();
                }//End Transaction
            }//End Session
        }

        public List<Domain.Socioboard.Domain.LinkedinCompanyPagePosts> getAllLinkedInPagePostsOfUser(Guid UserId, string pageid)
        {
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction. 
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        //Proceed action, to get linkedin feeds
                        List<Domain.Socioboard.Domain.LinkedinCompanyPagePosts> alst = session.CreateQuery("from LinkedinCompanyPagePosts where UserId = :userid and PageId = :pageid ORDER BY PostDate DESC")
                        .SetParameter("userid", UserId)
                        .SetParameter("pageid", pageid)
                        .List<Domain.Socioboard.Domain.LinkedinCompanyPagePosts>()
                        .ToList<Domain.Socioboard.Domain.LinkedinCompanyPagePosts>();
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

        public List<Domain.Socioboard.Domain.LinkedinCompanyPagePosts> getAllLinkedInPostOfPage(string PageId)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction. 
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        //Proceed action, to get all feed of account
                        List<Domain.Socioboard.Domain.LinkedinCompanyPagePosts> alst = session.CreateQuery("from LinkedinCompanyPagePosts where PageId = :pageid ORDER BY EntryDate DESC")
                        .SetParameter("pageid", PageId)
                        .List<Domain.Socioboard.Domain.LinkedinCompanyPagePosts>()
                        .ToList<Domain.Socioboard.Domain.LinkedinCompanyPagePosts>();



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

        public bool checkLinkedInPostExists(string postid, Guid userId)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction. 
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        //Proceed action to check linkedin feed. 
                        NHibernate.IQuery query = session.CreateQuery("from LinkedinCompanyPagePosts where  PostId = :postid and UserId =: userid");
                        query.SetParameter("postid", postid);
                        query.SetParameter("userid", userId);
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
            }//End Session
        }

        public int deleteAllPostOfPage(string PageId, Guid userid)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction. 
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        //Proceed action, to Delete linkedin feed
                        NHibernate.IQuery query = session.CreateQuery("delete from LinkedinCompanyPagePosts where UserId = :userid and PageId = :pageid");
                        query.SetParameter("userid", userid);
                        query.SetParameter("pageid", PageId);
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

        public void updateLinkedinPostofPage(Domain.Socioboard.Domain.LinkedinCompanyPagePosts lipost)
        {
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        session.CreateQuery("Update LinkedinCompanyPagePosts set Likes =:Likes,Comments =:Comments,IsLiked=:IsLiked where PostId = :PostId and UserId = :UserId")
                            .SetParameter("Likes", lipost.Likes)
                            .SetParameter("Comments", lipost.Comments)
                            .SetParameter("IsLiked", lipost.IsLiked)
                            .SetParameter("PostId", lipost.PostId)
                            .SetParameter("UserId", lipost.UserId)
                            .ExecuteUpdate();
                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                        // return 0;
                    }
                }
            }



        }
    }
}
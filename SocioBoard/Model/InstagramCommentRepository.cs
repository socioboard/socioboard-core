using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SocioBoard.Helper;
using SocioBoard.Domain;

namespace SocioBoard.Model
{
    public class InstagramCommentRepository : IInstagramComment
    {
        public void addInstagramComment(InstagramComment inscomment)
        {
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    session.Save(inscomment);
                    transaction.Commit();
                }
            }
        }

        public int deleteInstagramComment(InstagramComment inscomment)
        {
            throw new NotImplementedException();
        }

        public int updateInstagramComment(InstagramComment inscomment)
        {
            throw new NotImplementedException();
        }

        public List<InstagramComment> getAllInstagramCommentsOfUser(Guid UserId, string profileid,string feedid)
        {
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        List<InstagramComment> alst = session.CreateQuery("from InstagramComment where UserId = :userid and InstagramId = :profileId and FeedId=:feedid")
                        .SetParameter("userid", UserId)
                        .SetParameter("profileId", profileid)
                        .SetParameter("feedid", feedid)
                        .SetFirstResult(1)
                        .SetMaxResults(3)
                        .List<InstagramComment>()
                        .ToList<InstagramComment>();
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

                }
            }
        }

        public bool checkInstagramCommentExists(string feedid, Guid Userid)
        {
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
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

                }
            }
        }

        public void deleteAllCommentsOfUser(string fbuserid, Guid userid)
        {
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
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

                }
            }
        }
    }
}
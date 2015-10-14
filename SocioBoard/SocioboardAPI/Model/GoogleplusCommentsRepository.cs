using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NHibernate.Linq;
using Api.Socioboard.Helper;
namespace Api.Socioboard.Services
{
    public class GoogleplusCommentsRepository
    {
        public void Add(Domain.Socioboard.Domain.GoogleplusComments _GoogleplusComments)
        {
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    session.Save(_GoogleplusComments);
                    transaction.Commit();
                }
            }
        }

        public bool IsExist(string CommentId)
        {
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                bool ret = session.Query<Domain.Socioboard.Domain.GoogleplusComments>().Any(x => x.CommentId == CommentId);
                return ret;
            }
        }

        public void AddLikes(Domain.Socioboard.Domain.GoogleplusLike _GoogleplusLike)
        {
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {

                    session.Save(_GoogleplusLike);
                    transaction.Commit();
                }
            }
        }

        public bool IsLikeExist(string FromId, string FeedId)
        {
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                bool ret = session.Query<Domain.Socioboard.Domain.GoogleplusLike>().Any(x=>x.FeedId==FeedId && x.FromId==FromId);
                return ret;
            }
        }
    }
}
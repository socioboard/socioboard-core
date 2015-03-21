using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Api.Socioboard.Helper;

namespace Api.Socioboard.Services
{
    public class AffiliatesRepository
    {
        public void Add(Domain.Socioboard.Domain.Affiliates affiliate)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    session.Save(affiliate);
                    transaction.Commit();
                }
            }
        }

        public List<Domain.Socioboard.Domain.Affiliates> GetAffiliateDataByUserId(Guid UserId, Guid FriendsUserId)
        {
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        List<Domain.Socioboard.Domain.Affiliates> lst = session.CreateQuery("from Affiliates where UserId=: userid and FriendUserId=:friendsuserid ORDER BY AffiliateDate DESC")
                                       .SetParameter("userid", UserId)
                                       .SetParameter("friendsuserid", FriendsUserId).List<Domain.Socioboard.Domain.Affiliates>().ToList<Domain.Socioboard.Domain.Affiliates>();
                        return lst;
                    }
                    catch (Exception ex)
                    {
                        return null;
                    }
                }
            }
        }


        public List<Domain.Socioboard.Domain.Affiliates> GetAffiliateDataByUserId(Guid UserId)
        {
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        List<Domain.Socioboard.Domain.Affiliates> lst = session.CreateQuery("from Affiliates where UserId=: userid ORDER BY AffiliateDate DESC")
                                       .SetParameter("userid", UserId)
                                       .List<Domain.Socioboard.Domain.Affiliates>().ToList<Domain.Socioboard.Domain.Affiliates>();
                        return lst;
                    }
                    catch (Exception ex)
                    {
                        return null;
                    }
                }
            }
        }

    }
}
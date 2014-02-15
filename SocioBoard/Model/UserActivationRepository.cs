using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SocioBoard.Domain;
using SocioBoard.Helper;

namespace SocioBoard.Model
{
    public class UserActivationRepository
    {


        public static void Add(UserActivation user)
        {
            try
            {
                using (NHibernate.ISession session = SessionFactory.GetNewSession())
                {
                    using (NHibernate.ITransaction transaction = session.BeginTransaction())
                    {
                        session.Save(user);
                        transaction.Commit();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public static void Update(UserActivation user)
        {
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        int i = session.CreateQuery("Update UserActivation set ActivationStatus =:ActivationStatus where UserId = :userid and ID=:id")
                                  .SetParameter("id", user.Id)
                                  .SetParameter("userid", user.UserId)
                                  .SetParameter("ActivationStatus", user.ActivationStatus)
                                  .ExecuteUpdate();
                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);

                    }


                }
            }
        }
        public UserActivation GetUserActivationStatus(string UserId)
        {
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        NHibernate.IQuery query = session.CreateQuery("from UserActivation u where u.UserId = : userid");
                        query.SetParameter("userid", Guid.Parse(UserId));
                        UserActivation result = (UserActivation)query.UniqueResult();
                        return result;

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                        return null;
                    }



                }
            }
        }

        public UserActivation GetUserActivationStatusbyid(string Id)
        {
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        NHibernate.IQuery query = session.CreateQuery("select * from UserActivation where ID = :id");
                        query.SetParameter("id", Guid.Parse(Id));
                        UserActivation result = (UserActivation)query.UniqueResult();
                        return result;

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                        return null;
                    }



                }
            }
        }


        public int DeleteUserActivationByUserid(Guid userid)
        {
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        NHibernate.IQuery query = session.CreateQuery("delete from UserActivation where UserId = :userid")
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
                }
            }
        }

    }
}
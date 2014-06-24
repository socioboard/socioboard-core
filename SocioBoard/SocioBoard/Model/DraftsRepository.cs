using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SocioBoard.Domain;
using SocioBoard.Helper;

namespace SocioBoard.Model
{
    public class DraftsRepository : IDraftsRepository
    {
        public void AddDrafts(Drafts d)
        {

            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    session.Save(d);
                    transaction.Commit();
                }
            }
        }

        public int DeleteDrafts(Drafts d)
        {
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        NHibernate.IQuery query = session.CreateQuery("delete from Drafts where UserId = :userid and Message =:mess")
                                        .SetParameter("userid", d.UserId)
                                        .SetParameter("mess", d.Message);
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


        public int DeleteDrafts(Guid Id)
        {
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        NHibernate.IQuery query = session.CreateQuery("delete from Drafts where Id = :id")
                                        .SetParameter("id", Id);

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




        public void UpdateDrafts(Drafts d)
        {
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        session.CreateQuery("Update Drafts set Message =:mess,ModifiedDate =:mod where UserId =:userid")
                            .SetParameter("mess", d.Message)
                            .SetParameter("mod", d.ModifiedDate)
                            .SetParameter("userid", d.UserId)
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

        public void UpdateDrafts(Guid Id, string message)
        {
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    session.CreateQuery("Update Drafts set Message =:mess,ModifiedDate =:mod where Id =:id")
                        .SetParameter("mess", message)
                        .SetParameter("id", Id)
                        .SetParameter("mod", DateTime.Now)
                        .ExecuteUpdate();
                    transaction.Commit();


                }
            }

        }

        public List<Drafts> getAllDrafts(Guid UserId)
        {
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        List<Drafts> lst = session.CreateQuery("From Drafts where UserId = :userid")
                                           .SetParameter("userid", UserId)
                                           .List<Drafts>()
                                           .ToList<Drafts>();
                        #region oldcode
                        //List<Drafts> lst = new List<Drafts>();
                        //foreach (Drafts item in query.Enumerable<Drafts>())
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
                }
            }

        }

        public bool IsDraftsMessageExist(Guid UserId, string message)
        {
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        NHibernate.IQuery query = session.CreateQuery("From Drafts where UserId = :userid and Message =:mess");
                        query.SetParameter("userid", UserId);
                        query.SetParameter("mess", message);
                        List<Drafts> lst = new List<Drafts>();
                        foreach (Drafts item in query.Enumerable<Drafts>())
                        {
                            lst.Add(item);
                        }
                        if (lst.Count == 0)
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
    }


}
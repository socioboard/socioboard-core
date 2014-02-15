using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SocioBoard.Domain;
using SocioBoard.Helper;
using System.Collections;


namespace SocioBoard.Model
{
    public class GroupRepository:IGroupRepository
    {
        public void AddGroup(Groups group)
        {
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    session.Save(group);
                    transaction.Commit();
                }
            }
        }

        public int DeleteGroup(Groups group)
        {
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        NHibernate.IQuery query = session.CreateQuery("delete from Groups where UserId = :userid and GroupName = :name")
                                        .SetParameter("name", group.GroupName)
                                        .SetParameter("userid", group.UserId);
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


        public int DeleteGroup(Guid groupid)
        {
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        NHibernate.IQuery query = session.CreateQuery("delete from Groups where Id = :userid")
                                        .SetParameter("userid", groupid);
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

        public void UpdateGroup(Groups group)
        {
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        session.CreateQuery("Update Groups set GroupName =:groupname where UserId = :userid")
                            .SetParameter("groupname", group.GroupName)
                            .SetParameter("userid",group.UserId )
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

        public List<Groups> getAllGroups(Guid Userid)
        {
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    List<Groups> alstFBAccounts = session.CreateQuery("from Groups where UserId = :userid")
                    .SetParameter("userid", Userid)
                    .List<Groups>()
                    .ToList<Groups>();

                    #region oldcode
                    //List<Groups> alstFBAccounts = new List<Groups>();

                    //  foreach (Groups item in query.Enumerable())
                    //  {
                    //      alstFBAccounts.Add(item);
                    //  }
                    //   
                    #endregion
                    return alstFBAccounts;
                }
            }
        }

        public bool checkGroupExists(Guid userid,string groupname)
        {
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        NHibernate.IQuery query = session.CreateQuery("from Groups where UserId = :userid and GroupName =:groupname");
                        //  query.SetParameter("userid", group.UserId);  UserId =:userid and
                        query.SetParameter("userid",userid);
                        query.SetParameter("groupname", groupname);
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

                   
                }
            }
        }

        public Groups getGroupDetails(Guid userid, string groupname)
        {
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        NHibernate.IQuery query = session.CreateQuery("from Groups where UserId = :userid and GroupName=:groupname");
                      
                        query.SetParameter("userid", userid);
                        query.SetParameter("groupname", groupname);
                        Groups grou = query.UniqueResult<Groups>();
                        return grou;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                        return null;
                    }


                }
            }
        }


        public Groups getGroupDetailsbyId(Guid userid, Guid groupid)
        {
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        NHibernate.IQuery query = session.CreateQuery("from Groups where UserId = :userid and Id=:groupname");

                        query.SetParameter("userid", userid);
                        query.SetParameter("groupname", groupid);
                        Groups grou = query.UniqueResult<Groups>();
                        return grou;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                        return null;
                    }


                }
            }
        }
        //public ArrayList GetProfilesOfGroups(Guid groupid,Guid userid)
        //{
        //    using (NHibernate.ISession session = SessionFactory.GetNewSession())
        //    {
        //        using (NHibernate.ITransaction transaction = session.BeginTransaction())
        //        {
        //            try
        //            {
        //                NHibernate.IQuery query = session.CreateQuery("from Groups where UserId = :userid and Id=:groupid");

        //                query.SetParameter("userid", userid);
        //                query.SetParameter("groupid", groupid);
        //                ArrayList alstgroup = new ArrayList();
        //                foreach (var item in query.Enumerable())
        //                {
        //                    alstgroup.Add(item);
        //                }

        //                return alstgroup;
        //            }
        //            catch (Exception ex)
        //            {
        //                Console.WriteLine(ex.StackTrace);
        //                return null;
        //            }


        //        }
        //    }
        //}


        public int DeleteGroupsByUserid(Guid userid)
        {
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        NHibernate.IQuery query = session.CreateQuery("delete from Groups where UserId = :userid")
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
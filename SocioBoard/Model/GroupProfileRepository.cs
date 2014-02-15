using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SocioBoard.Domain;
using SocioBoard.Helper;

namespace SocioBoard.Model
{
    public class GroupProfileRepository:IGroupProfilesRepository
    {
        public void AddGroupProfile(GroupProfile group)
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

        public int DeleteGroupProfile(Guid userid,string profileid,Guid groupId)
        {
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        NHibernate.IQuery query = session.CreateQuery("delete from GroupProfile where GroupOwnerId = :userid and ProfileId = :id and GroupId = :groupid")
                                        .SetParameter("id", profileid)
                                        .SetParameter("groupid",groupId)
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


        public int DeleteAllGroupProfile(Guid GroupId)
        {
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        NHibernate.IQuery query = session.CreateQuery("delete from GroupProfile where GroupId = :id")
                                        .SetParameter("id", GroupId);
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

        public void UpdateGroupProfile(GroupProfile group)
        {
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        session.CreateQuery("Update GroupProfiles set EntryDate =:entrydate where Id = :userid")
                            .SetParameter("userid", group.Id)
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

        public List<GroupProfile> getAllGroupProfiles(Guid Userid,Guid groupid)
        {
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    List<GroupProfile> alstFBAccounts = session.CreateQuery("from GroupProfile where GroupOwnerId = :userid and GroupId =:groupid")
                    .SetParameter("userid", Userid)
                    .SetParameter("groupid", groupid)
                    .List<GroupProfile>()
                    .ToList<GroupProfile>();

                    #region oldcode
                    //List<GroupProfile> alstFBAccounts = new List<GroupProfile>();

                    //foreach (GroupProfile item in query.Enumerable())
                    //{
                    //    alstFBAccounts.Add(item);
                    //} 
                    #endregion
                    return alstFBAccounts;

                }
            }
        }

        public bool checkGroupProfileExists(Guid userid, Guid groupid,string profileid)
        {
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        NHibernate.IQuery query = session.CreateQuery("from GroupProfile where GroupOwnerId = :userid and GroupId = :id and ProfileId =:profileid");
                        query.SetParameter("id",groupid);
                        query.SetParameter("userid", userid);
                        query.SetParameter("profileid", profileid);
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



        public int DeleteGroupProfileByUserid(Guid userid)
        {
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        NHibernate.IQuery query = session.CreateQuery("delete from GroupProfile where UserId = :userid")
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
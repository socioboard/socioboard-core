using Api.Socioboard.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Api.Socioboard.Model
{
    public class GroupMembersRepository
    {
        /// <AddGroup>
        /// Add a new group
        /// </summary>
        /// <param name="group">Set Values in a Groups Class Property and Pass the same Object of Groups Class.(Domain.Groups)</param>
        public void AddGroupMemeber(Domain.Socioboard.Domain.Groupmembers groupMember)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    //Proceed action to save data.
                    session.Save(groupMember);
                    transaction.Commit();
                }//End Transaction
            }//End Session
        }

        public List<Domain.Socioboard.Domain.Groupmembers> GetGroupMemebers(string GroupId) 
        {
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    List<Domain.Socioboard.Domain.Groupmembers> lstGroupMembers = session.CreateQuery("from groupmembers where GroupId = :GroupId")
                    .SetParameter("GroupId", GroupId)
                    .List<Domain.Socioboard.Domain.Groupmembers>()
                    .ToList<Domain.Socioboard.Domain.Groupmembers>();

                    #region oldcode
                    //List<Groups> alstFBAccounts = new List<Groups>();

                    //  foreach (Groups item in query.Enumerable())
                    //  {
                    //      alstFBAccounts.Add(item);
                    //  }
                    //   
                    #endregion
                    return lstGroupMembers;
                }//End Transaction
            }//End Session
        }

        public List<Domain.Socioboard.Domain.Groupmembers> GetGroupMemebersByStatus(string GroupId,int Status)
        {
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    List<Domain.Socioboard.Domain.Groupmembers> lstGroupMembers = session.CreateQuery("from Groupmembers where GroupId = :groupId and Status =: Status")
                    .SetParameter("groupId", GroupId)
                    .SetParameter("Status", Status)
                    .List<Domain.Socioboard.Domain.Groupmembers>()
                    .ToList<Domain.Socioboard.Domain.Groupmembers>();

                    #region oldcode
                    //List<Groups> alstFBAccounts = new List<Groups>();

                    //  foreach (Groups item in query.Enumerable())
                    //  {
                    //      alstFBAccounts.Add(item);
                    //  }
                    //   
                    #endregion
                    return lstGroupMembers;
                }//End Transaction
            }//End Session
        }


        public int DeleteGroupMember(string GroupId, string UserId) 
        {
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        //Proceed action, to delete a group detail by group guid.
                        NHibernate.IQuery query = session.CreateQuery("delete from Groupmembers where Userid = :userid and Groupid =: GroupId")
                                        .SetParameter("userid", UserId)
                                        .SetParameter("GroupId", GroupId);
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

        public List<Domain.Socioboard.Domain.Groupmembers> GetUserGroupmembers(string UserId) 
        {
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    List<Domain.Socioboard.Domain.Groupmembers> lstGroupMembers = session.CreateQuery("from Groupmembers where Userid = :Userid and Status =: Status")
                    .SetParameter("Userid", UserId)
                    .SetParameter("Status", 1)
                    .List<Domain.Socioboard.Domain.Groupmembers>()
                    .ToList<Domain.Socioboard.Domain.Groupmembers>();

                    #region oldcode
                    //List<Groups> alstFBAccounts = new List<Groups>();

                    //  foreach (Groups item in query.Enumerable())
                    //  {
                    //      alstFBAccounts.Add(item);
                    //  }
                    //   
                    #endregion
                    return lstGroupMembers;
                }//End Transaction
            }//End Session
        }

        public Domain.Socioboard.Domain.Groupmembers GetGroupMember(Guid Id)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {

                    // proceed action, to get all Facebook Account of User by UserId(Guid) and FbUserId(string).
                    NHibernate.IQuery query = session.CreateQuery("from Groupmembers where Id = :Id");
                    query.SetParameter("Id", Id);
                    Domain.Socioboard.Domain.Groupmembers result = (Domain.Socioboard.Domain.Groupmembers)query.UniqueResult();
                    return result;
                }//End Transaction
            }//End session
        }

        public bool updateBoard(Domain.Socioboard.Domain.Groupmembers grpMember)
        {
            bool isUpdate = false;
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        // Proceed action to Update Data.
                        // And Set the reuired paremeters to find the specific values.
                        session.CreateQuery("Update Groupmembers set Emailid=:Emailid,Groupid=:Groupid,IsAdmin=:IsAdmin,Membercode=:Membercode,Status=:Status,Userid=:Userid where Id = :Id")
                            .SetParameter("Emailid", grpMember.Emailid)
                            .SetParameter("Groupid", grpMember.Groupid)
                            .SetParameter("IsAdmin", grpMember.IsAdmin)
                            .SetParameter("Membercode", grpMember.Membercode)
                            .SetParameter("Status", grpMember.Status)
                            .SetParameter("Userid", grpMember.Userid)
                            .SetParameter("Id", grpMember.Id)
                            .ExecuteUpdate();
                        transaction.Commit();
                        isUpdate = true;
                        return isUpdate;

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                        return isUpdate;
                    }
                }//End Transaction
            }//End session
        }


        public bool checkMemberExistsingroup(string groupid, string UserId)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        //Proceed action, to find profile is exist or not.
                        NHibernate.IQuery query = session.CreateQuery("from Groupmembers where Groupid = :Groupid and Userid =:Userid");
                        query.SetParameter("Groupid", groupid);
                        query.SetParameter("Userid", UserId);
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

        public int DeleteGroupMember(string GroupId)
        {
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        //Proceed action, to delete a group detail by group guid.
                        NHibernate.IQuery query = session.CreateQuery("delete from Groupmembers where Groupid =: GroupId")
                                        .SetParameter("GroupId", GroupId);
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

        public int DeleteGroupMemberByUserId(string EmailId)
        {
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        //Proceed action, to delete a group detail by group guid.
                        NHibernate.IQuery query = session.CreateQuery("delete from Groupmembers where Emalid =: EmalId")
                                        .SetParameter("EmalId", EmailId);
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

    }
}
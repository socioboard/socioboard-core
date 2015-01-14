using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Api.Socioboard.Helper;
using Domain.Socioboard.Domain;

namespace Api.Socioboard.Services
{
    public class GroupProfileRepository:IGroupProfilesRepository
    {
        /// <AddGroupProfile>
        /// Add a new group profile
        /// </summary>
        /// <param name="group">Set Values in a GroupProfile Class Property and Pass the same Object of GroupProfile Class.(Domain.GroupProfile)</param>
        public void AddGroupProfile(Domain.Socioboard.Domain.GroupProfile group)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    //Proceed action to save data.
                    session.Save(group);
                    transaction.Commit();
                }//End Transaction
            }//End Session
        }

        /// <DeleteGroupProfile>
        /// Delete a group profile of user by profile id , group id and profile id.
        /// </summary>
        /// <param name="userid">Id of user(Guid)</param>
        /// <param name="profileid">Profile id(String)</param>
        /// <param name="groupId">Id of group(Guid)</param>
        /// <returns>Return 1 when Data  is deleted Otherwise retun 0. (int)</returns>
        public int DeleteGroupProfile(Guid userid,string profileid,Guid groupId)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        //Proceed action, to delete data 
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
                }//End Transaction
            }//End Session
        }

        /// <DeleteAllGroupProfile>
        /// Delete all group profile of user by group id.
        /// </summary>
        /// <param name="GroupId">Group id.(Guid)</param>
        /// <returns>Return 1 when Data is successfully deleted Otherwise retun 0 . (int)</returns>
        public int DeleteAllGroupProfile(Guid GroupId)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        //Proceed action, to delete group profile by Group id.
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
                }//End Transaction
            }//End Session
        }

        /// <UpdateGroupProfile>
        /// Update the details of group profile
        /// </summary>
        /// <param name="group">Set Values in a GroupProfile Class Property and Pass the same Object of GroupProfile Class.(Domain.GroupProfile)</param>
        public void UpdateGroupProfile(Domain.Socioboard.Domain.GroupProfile group)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        //Proceed action, to update specific data by user id.
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
                }//End Transaction
            }//End Session
        }

        /// <getAllGroupProfiles>
        /// Get all group profiles details by user id and group id  
        /// </summary>
        /// <param name="Userid">Id of user (Guid)</param>
        /// <param name="groupid">Id of group (Guid)</param>
        /// <returns>List of group profiles.(List<GroupProfile>)</returns>
        public List<Domain.Socioboard.Domain.GroupProfile> getAllGroupProfiles(Guid Userid, Guid groupid)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    //Proceed action, to find details of group by user id and group id.
                    List<Domain.Socioboard.Domain.GroupProfile> alstFBAccounts = session.CreateQuery("from GroupProfile where GroupOwnerId = :userid and GroupId =:groupid")
                    .SetParameter("userid", Userid)
                    .SetParameter("groupid", groupid)
                    .List<Domain.Socioboard.Domain.GroupProfile>()
                    .ToList<Domain.Socioboard.Domain.GroupProfile>();

                    #region oldcode
                    //List<GroupProfile> alstFBAccounts = new List<GroupProfile>();

                    //foreach (GroupProfile item in query.Enumerable())
                    //{
                    //    alstFBAccounts.Add(item);
                    //} 
                    #endregion
                    return alstFBAccounts;

                }//End Transaction
            }//End Session
        }

        /// <checkGroupProfileExists>
        /// Check the existing group profile
        /// </summary>
        /// <param name="userid">Id of user(Guid)</param>
        /// <param name="groupid">Id of Group(Guid)</param>
        /// <param name="profileid">Id of profile(String)</param>
        /// <returns>Bool(True or False).</returns>
        public bool checkGroupProfileExists(Guid userid, Guid groupid,string profileid)
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
                }//End Transaction
            }//End Session
        }

        /// <DeleteGroupProfileByUserid>
        /// Delete group profile by userid
        /// </summary>
        /// <param name="userid">Id of user(Guid)</param>
        /// <returns>Return 1 when Data is successfully deleted Otherwise retun 0 . (int)</returns>
        public int DeleteGroupProfileByUserid(Guid userid)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        //Proceed action, to Delete group profile 
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
                }//End Transaction
            }//End Session
        }

        /// <getAllGroupProfiles>
        /// Get all group profiles details by user id and group id  
        /// </summary>
        /// <param name="Userid">Id of user (Guid)</param>
        /// <param name="groupid">Id of group (Guid)</param>
        /// <returns>List of group profiles.(List<GroupProfile>)</returns>
        public List<Domain.Socioboard.Domain.GroupProfile> GetAllGroupProfiles(Guid groupid)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    //Proceed action, to find details of group by user id and group id.
                    List<Domain.Socioboard.Domain.GroupProfile> alstFBAccounts = session.CreateQuery("from GroupProfile where GroupId =:groupid")
                    .SetParameter("groupid", groupid)
                    .List<Domain.Socioboard.Domain.GroupProfile>()
                    .ToList<Domain.Socioboard.Domain.GroupProfile>();

                    #region oldcode
                    //List<GroupProfile> alstFBAccounts = new List<GroupProfile>();

                    //foreach (GroupProfile item in query.Enumerable())
                    //{
                    //    alstFBAccounts.Add(item);
                    //} 
                    #endregion
                    return alstFBAccounts;

                }//End Transaction
            }//End Session
        }

        // Edited by Antima[20/12/2014]

        public bool checkProfileExistsingroup(Guid groupid, string profileid)
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
                        NHibernate.IQuery query = session.CreateQuery("from GroupProfile where GroupId = :id and ProfileId =:profileid");
                        query.SetParameter("id", groupid);
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
                }//End Transaction
            }//End Session
        }

        /// <checkGroupProfileExists>
        /// Check the existing group profile
        /// </summary>
        /// <param name="userid">Id of user(Guid)</param>
        /// <param name="groupid">Id of Group(Guid)</param>
        /// <param name="profileid">Id of profile(String)</param>
        /// <returns>Bool(True or False).</returns>
        public bool checkUserGroupExists(Guid userid, Guid groupid)
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
                        NHibernate.IQuery query = session.CreateQuery("from GroupProfile where GroupOwnerId = :userid and GroupId = :id");
                        query.SetParameter("id", groupid);
                        query.SetParameter("userid", userid);
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
    }
}
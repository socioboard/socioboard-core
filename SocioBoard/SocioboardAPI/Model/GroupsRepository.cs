using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Api.Socioboard.Helper;
using Domain.Socioboard.Domain;


namespace Api.Socioboard.Services
{
    public class GroupsRepository:IGroupRepository
    {

        /// <AddGroup>
        /// Add a new group
        /// </summary>
        /// <param name="group">Set Values in a Groups Class Property and Pass the same Object of Groups Class.(Domain.Groups)</param>
        public void AddGroup(Domain.Socioboard.Domain.Groups group)
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


        /// <DeleteGroup>
        /// Delete a group of user by profile id , group id and profile id.
        /// </summary>
        /// <param name="group">Set the group name and user id in a Groups Class Property and Pass the same Object of Groups Class.(Domain.Groups)</param>
        /// <returns>Return 1 when Data  is deleted Otherwise retun 0. (int)</returns>
        public int DeleteGroup(Domain.Socioboard.Domain.Groups group)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        //Proceed action, to delete the group data.
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
                }//End Transaction
            }//End Session
        }


        /// <DeleteGroup>
        /// Delete group by group id.
        /// </summary>
        /// <param name="groupid">Id of group(Guid)</param>
        /// <returns>Return 1 when Data  is deleted Otherwise retun 0. (int)</returns>
        public int DeleteGroup(Guid groupid)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        //Proceed action, to delete a group detail by group guid.
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
                }//End Transaction
            }//End Session
        }


        /// <UpdateGroup>
        /// Update group name
        /// </summary>
        /// <param name="group">Set the group name in a Groups Class Property and Pass the same Object of Groups Class.(Domain.Groups)</param>
        public void UpdateGroup(Domain.Socioboard.Domain.Groups group)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        //Proceed action, to update group name.
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
                }//End Transaction
            }//End Session
        }


        /// <getAllGroups>
        /// Get all groups of user
        /// </summary>
        /// <param name="Userid">id of user(Guid)</param>
        /// <returns>List of groups(List<Groups>)</returns>
        public List<Domain.Socioboard.Domain.Groups> getAllGroups(Guid Userid)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    List<Domain.Socioboard.Domain.Groups> alstFBAccounts = session.CreateQuery("from Groups where UserId = :userid")
                    .SetParameter("userid", Userid)
                    .List<Domain.Socioboard.Domain.Groups>()
                    .ToList<Domain.Socioboard.Domain.Groups>();

                    #region oldcode
                    //List<Groups> alstFBAccounts = new List<Groups>();

                    //  foreach (Groups item in query.Enumerable())
                    //  {
                    //      alstFBAccounts.Add(item);
                    //  }
                    //   
                    #endregion
                    return alstFBAccounts;
                }//End Transaction
            }//End Session
        }

        public List<Domain.Socioboard.Domain.Groups> getAllGroupsCompleteDetails(Guid Userid)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    List<Domain.Socioboard.Domain.Groups> alstFBAccounts = session.CreateQuery("from Groups where UserId = :userid and GroupName !='Socioboard'")
                    .SetParameter("userid", Userid)
                    .List<Domain.Socioboard.Domain.Groups>()
                    .ToList<Domain.Socioboard.Domain.Groups>();

                    #region oldcode
                    //List<Groups> alstFBAccounts = new List<Groups>();

                    //  foreach (Groups item in query.Enumerable())
                    //  {
                    //      alstFBAccounts.Add(item);
                    //  }
                    //   
                    #endregion
                    return alstFBAccounts;
                }//End Transaction
            }//End Session
        }



        public Domain.Socioboard.Domain.Groups getGroupDetail(Guid userid)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {

                        List<Domain.Socioboard.Domain.Groups> lstDetails = session.CreateQuery("from Groups where UserId=:userid and GroupName='Socioboard'")

                        .SetParameter("userid", userid)
                       .List<Domain.Socioboard.Domain.Groups>()
                       .ToList<Domain.Socioboard.Domain.Groups>();
                        return lstDetails[0];
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                        return null;
                    }
                }//End Transaction
            }//End Session
        }



        public Domain.Socioboard.Domain.Groups getGroupName(Guid id)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {

                        List<Domain.Socioboard.Domain.Groups> lstDetails = session.CreateQuery("from Groups where Id=:id ")

                        .SetParameter("id", id)
                       .List<Domain.Socioboard.Domain.Groups>()
                       .ToList<Domain.Socioboard.Domain.Groups>();
                        return lstDetails[0];
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                        return null;
                    }
                }//End Transaction
            }//End Session
        }



        /// <checkGroupExists>
        /// Check the group are exist or not.
        /// </summary>
        /// <param name="userid">Id of user(Guid)</param>
        /// <param name="groupname">Name of group(String)</param>
        /// <returns>Bool(True or False)</returns>
        public bool checkGroupExists(Guid userid,string groupname)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        //Proceed action, to find group by and and group name
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
                }//End Transaction
            }//End Session
        }


        /// <getGroupDetails>
        /// Get the details of Group
        /// </summary>
        /// <param name="userid">User id (Guid)</param>
        /// <param name="groupname">Name of Group(string)</param>
        /// <returns>Return the object of group.(Domein.Group)</returns>
        public Domain.Socioboard.Domain.Groups getGroupDetails(Guid userid, string groupname)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        //Proceed action to Get group records.
                        NHibernate.IQuery query = session.CreateQuery("from Groups where UserId = :userid and GroupName=:groupname");
                      
                        query.SetParameter("userid", userid);
                        query.SetParameter("groupname", groupname);
                        Domain.Socioboard.Domain.Groups grou = query.UniqueResult<Domain.Socioboard.Domain.Groups>();
                        return grou;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                        return null;
                    }
                }//End Transaction
            }//End Session
        }


        /// <getGroupDetailsbyId>
        /// Get the details of Group by user id and group id.
        /// </summary>
        /// <param name="userid">User id (Guid)</param>
        /// <param name="groupname">Id of Group(Guid)</param>
        /// <returns>Return the object of group.(Domein.Group)</returns>
        public Domain.Socioboard.Domain.Groups getGroupDetailsbyId(Guid userid, Guid groupid)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        NHibernate.IQuery query = session.CreateQuery("from Groups where UserId = :userid and Id=:groupname");

                        query.SetParameter("userid", userid);
                        query.SetParameter("groupname", groupid);
                        Domain.Socioboard.Domain.Groups grou = query.UniqueResult<Domain.Socioboard.Domain.Groups>();
                        return grou;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                        return null;
                    }

                }//End Transaction
            }//End Session
        }

    


        /// <DeleteGroup>
        /// Delete group by group id.
        /// </summary>
        /// <param name="userid">Id of User(Guid)</param>
        /// <returns>Return 1 when Data  is deleted Otherwise retun 0. (int)</returns>
        public int DeleteGroupsByUserid(Guid userid)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        //Proceed action to delete group.
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
                }//End Transaction
            }//End Session
        }


        public ArrayList getAllGroupsDetails(Guid GroupIde)
        {

            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    NHibernate.IQuery query = session.CreateSQLQuery("Select * from groups where Id=:GroupIde")
                   .SetParameter("GroupIde", GroupIde);
                    ArrayList alstStats = new ArrayList();

                    foreach (var item in query.List())
                    {
                        alstStats.Add(item);
                    }
                    return alstStats;

                }
            }

        }



        public List<Domain.Socioboard.Domain.Groups> getAllGroupsDetail(Guid GroupIde)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        //proceed action, to get all messages by user id and profileid.
                        List<Domain.Socioboard.Domain.Groups> lstDetails = session.CreateQuery("from Groups where Id=:GroupIde")
                       .SetParameter("GroupIde", GroupIde)
                       .List<Domain.Socioboard.Domain.Groups>()
                       .ToList<Domain.Socioboard.Domain.Groups>();
                        return lstDetails;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                        return null;
                    }
                }//End Transaction
            }//End Session
        }









    }
}
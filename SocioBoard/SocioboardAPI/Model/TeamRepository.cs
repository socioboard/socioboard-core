using Domain.Socioboard.Domain;
using Api.Socioboard.Helper;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
namespace Api.Socioboard.Services
{
    public class TeamRepository : ITeamRepository
    {
        /// <addNewTeam>
        /// Add New Team
        /// </summary>
        /// <param name="team">Set Values in a Team Class Property and Pass the Object of Team Class.(Domein.Team)</param>
        public void addNewTeam(Domain.Socioboard.Domain.Team team)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    //Proceed action, to save new team details.
                    session.Save(team);
                    transaction.Commit();
                }//End Transaction
            }//End Session
        }

        /// <deleteTeam>
        /// Delete Team
        /// </summary>
        /// <param name="team">Set Values of team id and email id in a Team Class Property and Pass the Object of Team Class.(Domein.Team)</param>
        /// <returns>Return 1 for success and 0 for failure.(int) </returns>
        public int deleteTeam(Domain.Socioboard.Domain.Team team)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        //Proceed action, to delete team by user id and email id.
                        NHibernate.IQuery query = session.CreateQuery("delete from Team where UserId = :userid and EmailId = :emailid")
                                        .SetParameter("userid", team.UserId)
                                        .SetParameter("emailid", team.EmailId);
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

        public int deleteGroupRelatedTeam(Guid groupid)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        //Proceed action, to delete team by user id and email id.
                        NHibernate.IQuery query = session.CreateQuery("delete from Team where GroupId = :groupid")

                        .SetParameter("groupid", groupid);
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

        public int deleteinviteteamMember(Guid id)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        //Proceed action, to delete team by user id and email id.
                        NHibernate.IQuery query = session.CreateQuery("delete from Team where Id = :id")

                        .SetParameter("id", id);
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

        ///// <updateTeam>
        ///// Update Team
        ///// </summary>
        ///// <param name="team">Set Values in a Team Class Property and Pass the Object of Team Class.(Domein.Team)</param>
        //public void updateTeam(Domain.Socioboard.Domain.Team team)
        //{
        //    //Creates a database connection and opens up a session
        //    using (NHibernate.ISession session = SessionFactory.GetNewSession())
        //    {
        //        //After Session creation, start Transaction.
        //        using (NHibernate.ITransaction transaction = session.BeginTransaction())
        //        {
        //            try
        //            {
        //                //Proceed action, to update team details.
        //                session.CreateQuery("Update Team set FirstName =:firstname,LastName =:lastname,StatusUpdateDate =:statusupdatedate,InviteStatus=:invitestatus,AccessLevel = :accesslevel where UserId = :userid and EmailId = :emailid")
        //                    .SetParameter("firstname", team.FirstName)
        //                    .SetParameter("lastname", team.LastName)
        //                      .SetParameter("statusupdatedate", team.StatusUpdateDate)
        //                      .SetParameter("invitestatus", team.InviteStatus)
        //                        .SetParameter("accesslevel", team.AccessLevel)
        //                          .SetParameter("userid", team.UserId)
        //                            .SetParameter("emailid", team.EmailId)

        //                    .ExecuteUpdate();
        //                transaction.Commit();


        //            }
        //            catch (Exception ex)
        //            {
        //                Console.WriteLine(ex.StackTrace);
        //                // return 0;
        //            }
        //        }//End Transaction
        //    }//End Session
        //}

        /// <getAllTeamsOfUser>
        /// Get All Teams Of User
        /// </summary>
        /// <param name="UserId">User id.(Guid)</param>
        /// <returns>Return object of Team Class with  value of each member in the form of list.(List<Team>)</returns>
        public List<Domain.Socioboard.Domain.Team> getAllTeamsOfUser(Guid UserId, Guid groupid, string EmailId)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        //Proceed action, to get team details of user where invitstatus is two.
                        //List<Team> alstFBAccounts = session.CreateQuery("from Team where UserId = :userid and GroupId=:groupid and InviteStatus =1")  .SetParameter("userid", UserId)
                        List<Domain.Socioboard.Domain.Team> alstFBAccounts = session.CreateQuery("from Team where GroupId=:groupid and InviteStatus =1")//session.CreateQuery("from Team where GroupId=:groupid and InviteStatus =1 and EmailId!=:EmailId")
                        .SetParameter("groupid", groupid)
                            //.SetParameter("EmailId", EmailId)
                        .List<Domain.Socioboard.Domain.Team>()
                        .ToList<Domain.Socioboard.Domain.Team>();


                        //List<Team> alstFBAccounts = new List<Team>();

                        //foreach (Team item in query.Enumerable())
                        //{
                        //    alstFBAccounts.Add(item);
                        //}
                        return alstFBAccounts;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                        return null;
                    }

                }//End Transaction
            }//End Session
        }

        /// <getTeam>
        /// Get Team
        /// </summary>
        /// <param name="EmailId">user Emailid.(String)</param>
        /// <param name="UserId">User id.(Guid)</param>
        /// <returns>Return the object of Team class with value.(Domain.Team)</returns>
        public Domain.Socioboard.Domain.Team getTeam(string EmailId, Guid UserId)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        //Proceed action, to get team details of user id and email id.
                        NHibernate.IQuery query = session.CreateQuery("from Team where UserId = :userid and EmailId =:emailid");
                        query.SetParameter("userid", UserId);
                        query.SetParameter("emailid", EmailId);
                        Domain.Socioboard.Domain.Team alstFBAccounts = query.UniqueResult<Domain.Socioboard.Domain.Team>();

                        return alstFBAccounts;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                        return null;
                    }

                }//End Transaction
            }//End Session
        }

        /// <getMemberById>
        /// Get Member By Id
        /// </summary>
        /// <param name="memberId">member id.(Guid)</param>
        /// <param name="UserId">User id.(Guid)</param>
        /// <returns>Return the object of Team class with value.(Domain.Team)</returns>
        public Domain.Socioboard.Domain.Team getMemberById(Guid memberId, Guid UserId)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        //Proceed action, to get team by user id and member id.
                        NHibernate.IQuery query = session.CreateQuery("from Team where UserId = :userid and Id =:memberId");
                        query.SetParameter("userid", UserId);
                        query.SetParameter("memberId", memberId);
                        Domain.Socioboard.Domain.Team alstFBAccounts = query.UniqueResult<Domain.Socioboard.Domain.Team>();

                        return alstFBAccounts;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                        return null;
                    }
                }//End Transaction
            }//End Session
        }

        /// <getMemberByEmailId>
        /// Get Team Member By Email Id.and user id.
        /// </summary>
        /// <param name="UserId">User id.(Guid)</param>
        /// <param name="EmailId">Member Email id.(string)</param>
        /// <returns>Return the object of Team class with value.(Domain.Team)</returns>
        public Domain.Socioboard.Domain.Team getMemberByEmailId(Guid UserId, string EmailId)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        //Proceed action, to get details of team by user id and email id.
                        NHibernate.IQuery query = session.CreateQuery("from Team where UserId = :userid and EmailId =:memberId");
                        query.SetParameter("userid", UserId);
                        query.SetParameter("memberId", EmailId);
                        Domain.Socioboard.Domain.Team alstFBAccounts = query.UniqueResult<Domain.Socioboard.Domain.Team>();

                        return alstFBAccounts;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                        return null;
                    }
                }//End Transaction
            }//End Session
        }

        /// <checkTeamExists>
        /// Check Team Exists
        /// </summary>
        /// <param name="EmailId">email id.(String)</param>
        /// <param name="UserId">User id.(Guid)</param>
        /// <returns>True and False.(bool)</returns>
        public bool checkTeamExists(string EmailId, Guid UserId, Guid gpId)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        //Proceed action, to get team detail by user id and email id.
                        NHibernate.IQuery query = session.CreateQuery("from Team where UserId = :UserId and EmailId =:EmailId and GroupId=:gpId");
                        query.SetParameter("UserId", UserId);
                        query.SetParameter("EmailId", EmailId);
                        query.SetParameter("gpId", gpId);
                        var alstFBAccounts = query.UniqueResult<Domain.Socioboard.Domain.Team>();

                        if (alstFBAccounts == null)
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

        /// <getTeamByEmailId>
        /// Get Team By Email Id.
        /// </summary>
        /// <param name="EmailId">memver Email id.(String)</param>
        /// <returns>Return the object of Team class with value.(Domain.Team)</returns>
        public Domain.Socioboard.Domain.Team getTeamByEmailId(string EmailId)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        //Proceed action, to get team details.
                        NHibernate.IQuery query = session.CreateQuery("from Team where EmailId =:memberId");
                        query.SetParameter("memberId", EmailId);
                        Domain.Socioboard.Domain.Team alstFBAccounts = query.UniqueResult<Domain.Socioboard.Domain.Team>();
                        return alstFBAccounts;

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                        return null;
                    }
                }//End Transaction
            }//End Session
        }

        /// <getTeamById>
        /// Get Team By Id
        /// </summary>
        /// <param name="Id">Team id.(Guid)</param>
        /// <returns>Return the object of Team class with value.(Domain.Team)</returns>
        public Domain.Socioboard.Domain.Team getTeamById(Guid Id)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {

                    try
                    {
                        NHibernate.IQuery query = session.CreateQuery("from Team where Id =:memberId");
                        query.SetParameter("memberId", Id);
                        Domain.Socioboard.Domain.Team alstFBAccounts = query.UniqueResult<Domain.Socioboard.Domain.Team>();
                        return alstFBAccounts;

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                        return null;
                    }
                }//End Transaction
            }//End Session
        }

        /// <updateTeamStatus>
        /// Update Team Status
        /// </summary>
        /// <param name="ID">Id of team.(Guid)</param>
        public void updateTeamStatus(Guid ID)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        //Update status
                        session.CreateQuery("Update Team set InviteStatus = 2  where Id = :userid")
                        .SetParameter("userid", ID)
                        .ExecuteUpdate();
                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);

                    }
                }//End Transaction
            }//End Session
        }

        /// <DeleteTeamByUserid>
        /// Delete Team By User id
        /// </summary>
        /// <param name="userid">User id.(Guid)</param>
        /// <returns>Return 1 for success and 0 for failure.(int) </returns>
        public int DeleteTeamByUserid(Guid userid)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        //Proceed action, to delete team od user.
                        NHibernate.IQuery query = session.CreateQuery("delete from Team where UserId = :userid")
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

        public Domain.Socioboard.Domain.Team getAllGroupsDetails(string emailId, Guid groupId, Guid userid)
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
                        List<Domain.Socioboard.Domain.Team> lstDetails = session.CreateQuery("from Team where (EmailId=:emailId or UserId=:userid) and GroupId=:groupId")
                       .SetParameter("emailId", emailId)
                        .SetParameter("groupId", groupId)
                         .SetParameter("userid", userid)
                       .List<Domain.Socioboard.Domain.Team>()
                       .ToList<Domain.Socioboard.Domain.Team>();
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

        public ArrayList getAllAccountUser(string emailId, Guid userid)
        {

            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    NHibernate.IQuery query = session.CreateSQLQuery("Select distinct(GroupId) from Team where EmailId=:emailId or UserId=:userid")
                    .SetParameter("userid", userid)
                        .SetParameter("emailId", emailId);
                    ArrayList alstStats = new ArrayList();

                    foreach (var item in query.List())
                    {
                        alstStats.Add(item);
                    }
                    return alstStats;

                }
            }

        }

        public Domain.Socioboard.Domain.Team getAllDetails(Guid groupid, string useremail)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {

                        List<Domain.Socioboard.Domain.Team> lstDetails = session.CreateQuery("from Team where GroupId=:groupid and EmailId=:useremail")
                       .SetParameter("useremail", useremail)
                        .SetParameter("groupid", groupid)
                       .List<Domain.Socioboard.Domain.Team>()
                       .ToList<Domain.Socioboard.Domain.Team>();
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

        public List<Domain.Socioboard.Domain.Team> getAllDetailsUserEmail(Guid groupId)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {

                        List<Domain.Socioboard.Domain.Team> alstFBAccounts = session.CreateQuery("from Team where GroupId=:groupId")
                        .SetParameter("groupId", groupId)
                        .List<Domain.Socioboard.Domain.Team>()
                        .ToList<Domain.Socioboard.Domain.Team>();

                        return alstFBAccounts;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                        return null;
                    }

                }//End Transaction
            }//End Session
        }

        public Domain.Socioboard.Domain.Team GetTeamByGroupId(Guid groupId)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {

                        List<Domain.Socioboard.Domain.Team> alstFBAccounts = session.CreateQuery("from Team where GroupId=:groupId order by StatusUpdateDate")
                        .SetParameter("groupId", groupId)
                        .List<Domain.Socioboard.Domain.Team>()
                        .ToList<Domain.Socioboard.Domain.Team>();

                        return alstFBAccounts[0];
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                        return null;
                    }

                }//End Transaction
            }//End Session
        }

        public Domain.Socioboard.Domain.Team getAllDetailsByTeamID(Guid Id, Guid groupId)
        {
            Domain.Socioboard.Domain.Team objTeam = new Domain.Socioboard.Domain.Team();
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {

                        List<Domain.Socioboard.Domain.Team> alstAccounts = session.CreateQuery("from Team where Id=:id and GroupId=:groupId")
                        .SetParameter("id", Id)
                        .SetParameter("groupId", groupId)
                        .List<Domain.Socioboard.Domain.Team>()
                        .ToList<Domain.Socioboard.Domain.Team>();
                        objTeam = alstAccounts[0];
                        return objTeam;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                        return null;
                    }

                }//End Transaction
            }//End Session
        }

        public List<Domain.Socioboard.Domain.Team> GetAllTeamExcludeUser(Guid groupId, Guid userid)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {

                        List<Domain.Socioboard.Domain.Team> alstFBAccounts = session.CreateQuery("from Team where GroupId=:groupId and UserId!=:uid")
                        .SetParameter("groupId", groupId)
                        .SetParameter("uid", userid)
                        .List<Domain.Socioboard.Domain.Team>()
                        .ToList<Domain.Socioboard.Domain.Team>();

                        return alstFBAccounts;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                        return null;
                    }

                }//End Transaction
            }//End Session
        }

        public Domain.Socioboard.Domain.Team GetAllTeam(Guid groupId, Guid userid)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {

                        List<Domain.Socioboard.Domain.Team> alstFBAccounts = session.CreateQuery("from Team where GroupId=:groupId and UserId=:uid")
                        .SetParameter("groupId", groupId)
                        .SetParameter("uid", userid)
                        .List<Domain.Socioboard.Domain.Team>()
                        .ToList<Domain.Socioboard.Domain.Team>();

                        return alstFBAccounts[0];
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                        return null;
                    }

                }//End Transaction
            }//End Session
        }

        public List<Domain.Socioboard.Domain.Team> GetAllTeamExcludeUserAccordingtoStatus(Guid groupId, Guid userid, int status)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        List<Domain.Socioboard.Domain.Team> alstFBAccounts = session.CreateQuery("from Team where GroupId=:groupId and UserId!=:uid and InviteStatus=:status")
                        .SetParameter("groupId", groupId)
                        .SetParameter("uid", userid)
                          .SetParameter("status", status)
                        .List<Domain.Socioboard.Domain.Team>()
                        .ToList<Domain.Socioboard.Domain.Team>();
                        return alstFBAccounts;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                        return null;
                    }

                }//End Transaction
            }//End Session
        }

        /// <updateTeam>
        /// Update Team
        /// </summary>
        /// <param name="team">Set Values in a Team Class Property and Pass the Object of Team Class.(Domein.Team)</param>
        public void updateTeam(Domain.Socioboard.Domain.Team team)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        //Proceed action, to update team details.
                        session.CreateQuery("Update Team set FirstName =:firstname,LastName =:lastname,StatusUpdateDate =:statusupdatedate,InviteStatus=:invitestatus,UserId = :userid where Id = :teamid")
                            .SetParameter("firstname", team.FirstName)
                            .SetParameter("lastname", team.LastName)
                              .SetParameter("statusupdatedate", team.StatusUpdateDate)
                              .SetParameter("invitestatus", team.InviteStatus)
                                  .SetParameter("userid", team.UserId)
                                      .SetParameter("teamid", team.Id)
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

        public void updateTeambyteamid(Guid teamid)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        //Proceed action, to update team details.
                        session.CreateQuery("Update Team set InviteStatus=1 where Id = :teamid")
                                      .SetParameter("teamid", teamid)
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

        public List<Domain.Socioboard.Domain.Team> GetAllTeamOfUserEmail(string emailid, Guid groupid)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        List<Domain.Socioboard.Domain.Team> alstFBAccounts = session.CreateQuery("from Team where EmailId=:email and GroupId!=:gid and InviteStatus=:status")
                        .SetParameter("email", emailid)
                        .SetParameter("gid", groupid)
                        .SetParameter("status", 0)
                        .List<Domain.Socioboard.Domain.Team>()
                        .ToList<Domain.Socioboard.Domain.Team>();

                        return alstFBAccounts;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                        return null;
                    }

                }//End Transaction
            }//End Session
        }

        /// <getTeamByEmailId>
        /// Get Team By Email Id.
        /// </summary>
        /// <param name="EmailId">memver Email id.(String)</param>
        /// <returns>Return the object of Team class with value.(Domain.Team)</returns>
        public List<Domain.Socioboard.Domain.Team> GetTeamByUserid(Guid userid)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        //Proceed action, to get team details.
                        List<Domain.Socioboard.Domain.Team> alstFBAccounts = session.CreateQuery("from Team where UserId =:uid and InviteStatus =1")
                       .SetParameter("uid", userid)
                        .List<Domain.Socioboard.Domain.Team>()
                        .ToList<Domain.Socioboard.Domain.Team>();
                        return alstFBAccounts;

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                        return null;
                    }
                }//End Transaction
            }//End Session
        }

        // Edited by Antima[20/12/2014]

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public List<Domain.Socioboard.Domain.Team> GetTeamByTeamId(Guid TeamId)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        //Proceed action, to get team details.
                        List<Domain.Socioboard.Domain.Team> alstteam = session.CreateQuery("from Team where Id =:TeamId")
                       .SetParameter("TeamId", TeamId)
                        .List<Domain.Socioboard.Domain.Team>()
                        .ToList<Domain.Socioboard.Domain.Team>();
                        return alstteam;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                        return null;
                    }
                }//End Transaction
            }//End Session
        }

        // Added by Antima[5/1/2015]
        public int UpdateEmailIdbyGroupId(Guid UserId, Guid GroupId, string NewEmailId)
        {
            int i = 0;
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        i = session.CreateQuery("Update Team set EmailId=:NewEmailId  where UserId = :UserId and GroupId = :GroupId")
                                  .SetParameter("UserId", UserId)
                                  .SetParameter("GroupId", GroupId)
                                  .SetParameter("NewEmailId", NewEmailId)
                                  .ExecuteUpdate();
                        transaction.Commit();
                    }
                    catch { }
                }
            }
            return i;
        }

    }
}
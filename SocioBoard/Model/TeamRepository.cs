using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SocioBoard.Domain;
using SocioBoard.Helper;

namespace SocioBoard.Model
{
    public class TeamRepository : ITeamRepository
    {

        /// <addNewTeam>
        /// Add New Team
        /// </summary>
        /// <param name="team">Set Values in a Team Class Property and Pass the Object of Team Class.(Domein.Team)</param>
        public void addNewTeam(Team team)
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
        public int deleteTeam(Team team)
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


        /// <updateTeam>
        /// Update Team
        /// </summary>
        /// <param name="team">Set Values in a Team Class Property and Pass the Object of Team Class.(Domein.Team)</param>
        public void updateTeam(Team team)
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
                        session.CreateQuery("Update Team set FirstName =:firstname,LastName =:lastname,StatusUpdateDate =:statusupdatedate,InviteStatus=:invitestatus,AccessLevel = :accesslevel where UserId = :userid and EmailId = :emailid")
                            .SetParameter("firstname", team.FirstName)
                            .SetParameter("lastname", team.LastName)
                              .SetParameter("statusupdatedate", team.StatusUpdateDate)
                              .SetParameter("invitestatus", team.InviteStatus)
                                .SetParameter("accesslevel", team.AccessLevel)
                                  .SetParameter("userid", team.UserId)
                                    .SetParameter("emailid", team.EmailId)

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


        /// <getAllTeamsOfUser>
        /// Get All Teams Of User
        /// </summary>
        /// <param name="UserId">User id.(Guid)</param>
        /// <returns>Return object of Team Class with  value of each member in the form of list.(List<Team>)</returns>
        public List<Team> getAllTeamsOfUser(Guid UserId)
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
                        List<Team> alstFBAccounts = session.CreateQuery("from Team where UserId = :userid and InviteStatus = 2")
                        .SetParameter("userid", UserId)
                        .List<Team>()
                        .ToList<Team>();


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
        public Team getTeam(string EmailId, Guid UserId)
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
                        Team alstFBAccounts = query.UniqueResult<Team>();

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
        public Team getMemberById(Guid memberId, Guid UserId)
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
                        Team alstFBAccounts = query.UniqueResult<Team>();

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
        public Team getMemberByEmailId(Guid UserId, string EmailId)
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
                        Team alstFBAccounts = query.UniqueResult<Team>();

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
        public bool checkTeamExists(string EmailId, Guid UserId)
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
                        NHibernate.IQuery query = session.CreateQuery("from Team where UserId = :userid and EmailId =:emailid");
                        query.SetParameter("userid", UserId);
                        query.SetParameter("emailid", EmailId);
                        var alstFBAccounts = query.UniqueResult<Team>();

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
        public Team getTeamByEmailId(string EmailId)
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
                        Team alstFBAccounts = query.UniqueResult<Team>();
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
        public Team getTeamById(Guid Id)
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
                        Team alstFBAccounts = query.UniqueResult<Team>();
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

    }
}
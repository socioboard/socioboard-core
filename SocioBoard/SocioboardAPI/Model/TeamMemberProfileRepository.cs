using Domain.Socioboard.Domain;
using Api.Socioboard.Helper;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
namespace Api.Socioboard.Services
{
    public class TeamMemberProfileRepository : ITeamMemberProfileRepository
    {
        /// <addNewTeamMember>
        /// Add New TeamMember
        /// </summary>
        /// <param name="team">Set Values in a TeamMemberProfile Class Property and Pass the Object of TeamMemberProfile Class.(Domein.TeamMemberProfile)</param>
        public void addNewTeamMember(Domain.Socioboard.Domain.TeamMemberProfile team)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    //Proceed action , to save new team member profile.
                    session.Save(team);
                    transaction.Commit();
                }//End Transaction
            }//End Session
        }

        /// <deleteTeamMember>
        /// Delete profile of TeamMember
        /// </summary>
        /// <param name="team">Set the team id and prfile id of team in a TeamMemberProfile Class Property and Pass the Object of TeamMemberProfile Class.(Domein.TeamMemberProfile)</param>
        /// <returns>Return 1 for successfully delete and 0 for failed.(int)</returns>
        public int deleteTeamMember(Domain.Socioboard.Domain.TeamMemberProfile team)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        //Proceed action, to deleting team profile by id and team id.
                        NHibernate.IQuery query = session.CreateQuery("delete from TeamMemberProfile where TeamId = :teamid and ProfileId = :profileid")
                                        .SetParameter("teamin", team.TeamId)
                                        .SetParameter("profileid", team.ProfileId);
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

        public int deleteTeamMember(Guid teamid)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        //Proceed action, to deleting team profile by id and team id.
                        NHibernate.IQuery query = session.CreateQuery("delete from TeamMemberProfile where TeamId = :teamid")
                                        .SetParameter("teamid", teamid);

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

        /// <updateTeamMember>
        /// Update TeamMember details
        /// </summary>
        /// <param name="team">Set Values in a TeamMemberProfile Class Property and Pass the Object of TeamMemberProfile Class.(Domein.TeamMemberProfile)</param>
        public void updateTeamMember(Domain.Socioboard.Domain.TeamMemberProfile team)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        //Proceed action, to update details of team member profile.
                        session.CreateQuery("Update TeamMemberProfile set Status =:status,StatusUpdateDate =:statusdate where TeamId = :teamid and  ProfileId = :profileId")
                            .SetParameter("status", team.Status)
                            .SetParameter("statusdate", team.StatusUpdateDate)
                            .SetParameter("teamid", team.TeamId)
                            .SetParameter("profileId", team.ProfileId)
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

        /// <getAllTeamMemberProfilesOfTeam>
        /// Get All TeamMember Profiles Of Team
        /// </summary>
        /// <param name="TeamId">Team id.(Guid)</param>
        /// <returns>Return object of TeamMemberProfile Class with  value of each member in the form of list.(List<TeamMemberProfile>)</returns>
        public List<Domain.Socioboard.Domain.TeamMemberProfile> getAllTeamMemberProfilesOfTeam(Guid TeamId)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    //Proceed action, to get details of team by team id.
                    List<Domain.Socioboard.Domain.TeamMemberProfile> alTeamMember = session.CreateQuery("from TeamMemberProfile where TeamId = :teamid")
                    .SetParameter("teamid", TeamId)
                    .List<Domain.Socioboard.Domain.TeamMemberProfile>()
                    .ToList<Domain.Socioboard.Domain.TeamMemberProfile>();

                    //List<TeamMemberProfile> alstFBAccounts = new List<TeamMemberProfile>();

                    //foreach (TeamMemberProfile item in query.Enumerable())
                    //{
                    //    alstFBAccounts.Add(item);
                    //}
                    return alTeamMember;

                }//End Transaction
            }//End Session
        }

        public List<Domain.Socioboard.Domain.TeamMemberProfile> getTeamMemberProfileData(Guid TeamId)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    //Proceed action, to get details of team by team id.
                    List<Domain.Socioboard.Domain.TeamMemberProfile> alstFBAccounts = session.CreateQuery("from TeamMemberProfile where TeamId = :teamid and ProfileType='facebook'")
                    .SetParameter("teamid", TeamId)
                    .List<Domain.Socioboard.Domain.TeamMemberProfile>()
                    .ToList<Domain.Socioboard.Domain.TeamMemberProfile>();

                    //List<TeamMemberProfile> alstFBAccounts = new List<TeamMemberProfile>();

                    //foreach (TeamMemberProfile item in query.Enumerable())
                    //{
                    //    alstFBAccounts.Add(item);
                    //}
                    return alstFBAccounts;

                }//End Transaction
            }//End Session
        }

        public List<Domain.Socioboard.Domain.TeamMemberProfile> getTwtTeamMemberProfileData(Guid TeamId)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    //Proceed action, to get details of team by team id.
                    List<Domain.Socioboard.Domain.TeamMemberProfile> alstFBAccounts = session.CreateQuery("from TeamMemberProfile where TeamId = :teamid and ProfileType='twitter'")
                    .SetParameter("teamid", TeamId)
                    .List<Domain.Socioboard.Domain.TeamMemberProfile>()
                    .ToList<Domain.Socioboard.Domain.TeamMemberProfile>();

                    //List<TeamMemberProfile> alstFBAccounts = new List<TeamMemberProfile>();

                    //foreach (TeamMemberProfile item in query.Enumerable())
                    //{
                    //    alstFBAccounts.Add(item);
                    //}
                    return alstFBAccounts;

                }//End Transaction
            }//End Session
        }
        
        public List<Domain.Socioboard.Domain.TeamMemberProfile> getTumblrTeamMemberProfileData(Guid TeamId)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    //Proceed action, to get details of team by team id.
                    List<Domain.Socioboard.Domain.TeamMemberProfile> alstFBAccounts = session.CreateQuery("from TeamMemberProfile where TeamId = :teamid and ProfileType='tumblr'")
                    .SetParameter("teamid", TeamId)
                    .List<Domain.Socioboard.Domain.TeamMemberProfile>()
                    .ToList<Domain.Socioboard.Domain.TeamMemberProfile>();

                    //List<TeamMemberProfile> alstFBAccounts = new List<TeamMemberProfile>();

                    //foreach (TeamMemberProfile item in query.Enumerable())
                    //{
                    //    alstFBAccounts.Add(item);
                    //}
                    return alstFBAccounts;

                }//End Transaction
            }//End Session
        }

        public List<Domain.Socioboard.Domain.TeamMemberProfile> getLinkedInTeamMemberProfileData(Guid TeamId)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    //Proceed action, to get details of team by team id.
                    List<Domain.Socioboard.Domain.TeamMemberProfile> alstFBAccounts = session.CreateQuery("from TeamMemberProfile where TeamId = :teamid and ProfileType='linkedIn'")
                    .SetParameter("teamid", TeamId)
                    .List<Domain.Socioboard.Domain.TeamMemberProfile>()
                    .ToList<Domain.Socioboard.Domain.TeamMemberProfile>();

                    //List<TeamMemberProfile> alstFBAccounts = new List<TeamMemberProfile>();

                    //foreach (TeamMemberProfile item in query.Enumerable())
                    //{
                    //    alstFBAccounts.Add(item);
                    //}
                    return alstFBAccounts;

                }//End Transaction
            }//End Session
        }

        public List<Domain.Socioboard.Domain.TeamMemberProfile> GetTeamMemberProfileByTeamIdAndProfileType(Guid TeamId, string profiletype)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    //Proceed action, to get details of team by team id.
                    List<Domain.Socioboard.Domain.TeamMemberProfile> alstFBAccounts = session.CreateQuery("from TeamMemberProfile where TeamId = :teamid and ProfileType=:profiletype")
                    .SetParameter("teamid", TeamId)
                    .SetParameter("profiletype", profiletype)
                    .List<Domain.Socioboard.Domain.TeamMemberProfile>()
                    .ToList<Domain.Socioboard.Domain.TeamMemberProfile>();

                    //List<TeamMemberProfile> alstFBAccounts = new List<TeamMemberProfile>();

                    //foreach (TeamMemberProfile item in query.Enumerable())
                    //{
                    //    alstFBAccounts.Add(item);
                    //}
                    return alstFBAccounts;

                }//End Transaction
            }//End Session
        }

        /// <getTeamMemberProfile>
        /// Get TeamMember Profile
        /// </summary>
        /// <param name="Teamid">Team id.(Guid)</param>
        /// <param name="ProfileId">Profile id.(string)</param>
        /// <returns>>Return the object of TeamMemberProfile class with value.(Domain.TeamMemberProfile)</returns>
        public Domain.Socioboard.Domain.TeamMemberProfile getTeamMemberProfile(Guid Teamid, string ProfileId)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        //Proceed action, to get details of team profile.
                        NHibernate.IQuery query = session.CreateQuery("from TeamMemberProfile where TeamId = :teamid and ProfileId = :profileid");
                        query.SetParameter("teamid", Teamid);
                        query.SetParameter("profileid", ProfileId);
                        Domain.Socioboard.Domain.TeamMemberProfile alstFBAccounts = query.UniqueResult<Domain.Socioboard.Domain.TeamMemberProfile>();

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

        /// <checkTeamMemberProfile>
        /// Check Team Member Profile
        /// </summary>
        /// <param name="Teamid">Team id. (Guid)</param>
        /// <param name="ProfileId">Profile id.(String)</param>
        /// <returns>if profile is exist its return true , otherwise it is return false.(bool)</returns>
        public bool checkTeamMemberProfile(Guid Teamid, string ProfileId)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        //Get the details of team profile by team id and profile id.
                        List<Domain.Socioboard.Domain.TeamMemberProfile> alstFBAccounts = session.CreateQuery("from TeamMemberProfile where TeamId = :teamid and ProfileId = :profileid")
                         .SetParameter("teamid", Teamid)
                         .SetParameter("profileid", ProfileId)
                        .List<Domain.Socioboard.Domain.TeamMemberProfile>()
                     .ToList<Domain.Socioboard.Domain.TeamMemberProfile>();
                        if (alstFBAccounts == null || alstFBAccounts.Count==0)
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
        public bool checkTeamMemberProfilebyType(Guid Teamid, string ProfileId ,string profiletype)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        //Get the details of team profile by team id and profile id.
                        List<Domain.Socioboard.Domain.TeamMemberProfile> alstFBAccounts = session.CreateQuery("from TeamMemberProfile where TeamId = :teamid and ProfileId = :profileid and ProfileType=:ProfileType")
                         .SetParameter("teamid", Teamid)
                         .SetParameter("profileid", ProfileId)
                         .SetParameter("ProfileType", profiletype)
                        .List<Domain.Socioboard.Domain.TeamMemberProfile>()
                     .ToList<Domain.Socioboard.Domain.TeamMemberProfile>();
                        if (alstFBAccounts == null || alstFBAccounts.Count == 0)
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

        /// <DeleteTeamMemberProfileByUserid>
        /// Delete Team Member Profile By Userid
        /// </summary>
        /// <param name="userid">user id.(Guid)</param>
        /// <returns>Return 1 for success and 0 for failure.(int) </returns>
        public int DeleteTeamMemberProfileByUserid(Guid userid)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        //Proceed action, to delete records of user.
                        NHibernate.IQuery query = session.CreateQuery("delete from TeamMemberProfile where UserId = :userid")
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

        /// <returns>Return 1 for success and 0 for failure.(int) </returns>
        public int DeleteTeamMemberProfileByTeamId(Guid teamid)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        //Proceed action, to delete records of user.
                        NHibernate.IQuery query = session.CreateQuery("delete from TeamMemberProfile where TeamId = :teamid")
                                        .SetParameter("teamid", teamid);
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

        public int DeleteTeamMemberProfileByUserid(string ProfileId)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        //Proceed action, to delete records of user.
                        NHibernate.IQuery query = session.CreateQuery("delete from TeamMemberProfile where ProfileId = :ProfileId")
                                        .SetParameter("ProfileId", ProfileId);
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

        public int DeleteTeamMemberProfileByTeamIdProfileId(string ProfileId, Guid teamid)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        //Proceed action, to delete records of user.
                        NHibernate.IQuery query = session.CreateQuery("delete from TeamMemberProfile where ProfileId = :ProfileId and TeamId=:teamid")
                          .SetParameter("ProfileId", ProfileId)
                          .SetParameter("teamid", teamid);
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

        // Edited by Antima[20/12/2014]

        public List<Domain.Socioboard.Domain.TeamMemberProfile> getAllTeamsByProfileId(string ProfileId)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    //Proceed action, to get details of team by team id.
                    List<Domain.Socioboard.Domain.TeamMemberProfile> lstAllteam = session.CreateQuery("from TeamMemberProfile where ProfileId =:ProfileId")
                    .SetParameter("ProfileId", ProfileId)
                    .List<Domain.Socioboard.Domain.TeamMemberProfile>()
                    .ToList<Domain.Socioboard.Domain.TeamMemberProfile>();

                    return lstAllteam;

                }//End Transaction
            }//End Session
        }



        //edited by avinash[14/02/2015]
        public void updateTeamMemberbyprofileid(Domain.Socioboard.Domain.TeamMemberProfile team)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        //Proceed action, to update details of team member profile.
                        session.CreateQuery("Update TeamMemberProfile set ProfileName =:ProfileName,ProfilePicUrl =:ProfilePicUrl where ProfileId = :ProfileId")
                            .SetParameter("ProfileName", team.ProfileName)
                            .SetParameter("ProfilePicUrl", team.ProfilePicUrl)
                            .SetParameter("ProfileId", team.ProfileId)
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

    }
}
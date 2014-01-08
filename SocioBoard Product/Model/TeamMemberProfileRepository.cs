using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SocioBoard.Domain;
using SocioBoard.Helper;

namespace SocioBoard.Model
{
    public class TeamMemberProfileRepository:ITeamMemberProfileRepository
    {
        public void addNewTeamMember(TeamMemberProfile team)
        {
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    session.Save(team);
                    transaction.Commit();
                }
            }
        }

        public int deleteTeamMember(TeamMemberProfile team)
        {
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
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
                }
            }
        }

        public void updateTeamMember(TeamMemberProfile team)
        {
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        session.CreateQuery("Update TeamMemberProfile set Status =:status,StatusUpdateDate =:statusdate where TeamId = :teamid and  ProfileId = :profileId")
                            .SetParameter("status", team.Status)
                            .SetParameter("statusdate", team.StatusUpdateDate)
                            .SetParameter("teamid", team.TeamId)
                            .SetParameter("profileId",team.ProfileId)
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

        public List<TeamMemberProfile> getAllTeamMemberProfilesOfTeam(Guid TeamId)
        {
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    List<TeamMemberProfile> alstFBAccounts = session.CreateQuery("from TeamMemberProfile where TeamId = :teamid")
                    .SetParameter("teamid", TeamId)
                    .List<TeamMemberProfile>()
                    .ToList<TeamMemberProfile>();

                    //List<TeamMemberProfile> alstFBAccounts = new List<TeamMemberProfile>();

                    //foreach (TeamMemberProfile item in query.Enumerable())
                    //{
                    //    alstFBAccounts.Add(item);
                    //}
                    return alstFBAccounts;

                }
            }
        }

        public TeamMemberProfile getTeamMemberProfile(Guid Teamid, string ProfileId)
        {
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        NHibernate.IQuery query = session.CreateQuery("from TeamMemberProfile where TeamId = :teamid and ProfileId = :profileid");
                        query.SetParameter("teamid", Teamid);
                        query.SetParameter("profileid", ProfileId);
                        TeamMemberProfile alstFBAccounts = query.UniqueResult<TeamMemberProfile>();

                        return alstFBAccounts;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                        return null;
                    }

                }
            }
        }

        public bool checkTeamMemberProfile(Guid Teamid, string ProfileId)
        {
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        NHibernate.IQuery query = session.CreateQuery("from TeamMemberProfile where TeamId = :teamid and ProfileId = :profileid");
                        query.SetParameter("teamid", Teamid);
                        query.SetParameter("profileid", ProfileId);
                        var alstFBAccounts = query.UniqueResult<TeamMemberProfile>();
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

                }
            }
        }
    }
}
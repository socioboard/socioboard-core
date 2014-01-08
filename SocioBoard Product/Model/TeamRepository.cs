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

        public void addNewTeam(Team team)
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

        public int deleteTeam(Team team)
        {
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
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
                }
            }
        }

        public void updateTeam(Team team)
        {
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
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
                }
            }
        }

        public List<Team> getAllTeamsOfUser(Guid UserId)
        {
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
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

                }
            }
        }

        public Team getTeam(string EmailId, Guid UserId)
        {
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
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

                }
            }
        }

        public Team getMemberById(Guid memberId, Guid UserId)
        {
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
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

                }
            }
        }

        public Team getMemberByEmailId(Guid UserId, string EmailId)
        {
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
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

                }
            }
        }

        public bool checkTeamExists(string EmailId, Guid UserId)
        {
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
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

                }
            }
        }

        public Team getTeamByEmailId(string EmailId)
        {

            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
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

                }
            }
        }

        public Team getTeamById(Guid Id)
        {
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
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

                }
            }
        }

        public void updateTeamStatus(Guid ID)
        {
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        session.CreateQuery("Update Team set InviteStatus = 2  where Id = :userid")
                        .SetParameter("userid", ID)
                        .ExecuteUpdate();
                        transaction.Commit();


                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                       
                    }
                }
            }
        }
    }
}
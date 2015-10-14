using Api.Socioboard.Helper;
using Domain.Socioboard.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NHibernate.Linq;
namespace Api.Socioboard.Services
{
    public class TwitterAccountFollowersRepository
    {

        public void addTwitterAccountFollower(Domain.Socioboard.Domain.TwitterAccountFollowers task)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        //Proceed action, to save new data.
                        session.Save(task);
                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                    }
                }//End Transaction
            }//End Session
        }


        public List<Domain.Socioboard.Domain.TwitterAccountFollowers> getAllFollower(Guid userid, string profileid, int day)
        {

            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //Begin session trasaction and opens up.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    DateTime AssignDate = DateTime.Now;
                    DateTime AssinDate = AssignDate.AddDays(-day);//.ToString("yyyy-MM-dd HH:mm:ss");

                    try
                    {
                        string str = " from ( from TwitterAccountFollowers where UserId=:userid and EntryDate>=:AssinDate  and ProfileId IN(";
                        string[] arrsrt = profileid.Split(',');
                        foreach (string sstr in arrsrt)
                        {
                            str += "'" + (sstr) + "'" + ",";
                        }
                        str = str.Substring(0, str.Length - 1);
                        str += ") order by EntryDate desc) as baai group by ProfileId ";
                        List<Domain.Socioboard.Domain.TwitterAccountFollowers> alst = session.CreateQuery(str).SetParameter("userid", userid).SetParameter("AssinDate", AssinDate)
                       .List<Domain.Socioboard.Domain.TwitterAccountFollowers>()
                       .ToList<Domain.Socioboard.Domain.TwitterAccountFollowers>();   

                        return alst;

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                        return null;
                    }

                }//End Trasaction
            }//End session

        }

        public List<Domain.Socioboard.Domain.TwitterAccountFollowers> getAllFollowerbeforedays(Guid userid, string profileid, int day)
        {

            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //Begin session trasaction and opens up.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    DateTime AssignDate = DateTime.Now;
                    DateTime AssinDate = AssignDate.AddDays(-day);//.ToString("yyyy-MM-dd HH:mm:ss");

                    try
                    {
                        string str = "from TwitterAccountFollowers where UserId=:userid and EntryDate < :AssinDate  and ProfileId IN(";
                        string[] arrsrt = profileid.Split(',');
                        foreach (string sstr in arrsrt)
                        {
                            str += "'" + (sstr) + "'" + ",";
                        }
                        str = str.Substring(0, str.Length - 1);
                        str += ") order by EntryDate desc limit 1";
                        List<Domain.Socioboard.Domain.TwitterAccountFollowers> alst = session.CreateQuery(str).SetParameter("userid", userid).SetParameter("AssinDate", AssinDate)
                       .List<Domain.Socioboard.Domain.TwitterAccountFollowers>()
                       .ToList<Domain.Socioboard.Domain.TwitterAccountFollowers>();
                        return alst;

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                        return null;
                    }

                }//End Trasaction
            }//End session

        }

        public int getAllFollower1(Guid UserId, string profileid, int days)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //Begin session trasaction and opens up.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        NHibernate.IQuery query = session.CreateSQLQuery("Select FollowersCount from TwitterAccountFollowers where UserId=:UserId and  ProfileId=:profileid and EntryDate>=DATE_ADD(NOW(),INTERVAL -" + days + " DAY) order by EntryDate desc limit 1");
                        query.SetParameter("profileid", profileid);
                        query.SetParameter("UserId", UserId);
                        int i = 0;
                        foreach (var item in query.List())
                        {
                          i =  Convert.ToInt32(item);
                        }
                        return i;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                        return 0;
                    }

                }//End Transaction  
            }//End Session

        }

        public int getAllFollowerbeforedays1(Guid UserId, string profileid, int days)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //Begin session trasaction and opens up.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        NHibernate.IQuery query = session.CreateSQLQuery("Select FollowersCount from TwitterAccountFollowers where UserId=:UserId and  ProfileId=:profileid and EntryDate < DATE_ADD(NOW(),INTERVAL -" + days + " DAY) order by EntryDate desc limit 1");
                        query.SetParameter("profileid", profileid);
                        query.SetParameter("UserId", UserId);
                        int i = 0;
                        foreach (var item in query.List())
                        {
                            i = Convert.ToInt32(item);
                        }
                        return i;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                        return 0;
                    }

                }//End Transaction  
            }//End Session

        }


        public bool IsTwitterAccountExistsFirst(Guid UserId, string Profileid)
        {
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                bool exist = session.Query<Domain.Socioboard.Domain.TwitterAccountFollowers>().Any(x => x.UserId == UserId && x.ProfileId == Profileid && x.EntryDate>=DateTime.Now.Date.AddSeconds(1) && x.EntryDate<=DateTime.Now.Date.AddHours(12));
                return exist;
            }
        }

        public bool IsTwitterAccountExistsSecond(Guid UserId, string Profileid)
        {
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                bool exist = session.Query<Domain.Socioboard.Domain.TwitterAccountFollowers>().Any(x => x.UserId == UserId && x.ProfileId == Profileid && x.EntryDate >= DateTime.Now.Date.AddHours(12).AddSeconds(1) && x.EntryDate <= DateTime.Now.AddDays(1).Date.AddSeconds(-1));
                return exist;
            }
        }

        public void UpdateTwitterAccountFollowerFirst(Domain.Socioboard.Domain.TwitterAccountFollowers _TwitterAccountFollowers)
        { 
         //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //Begin session trasaction and opens up.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    string str = "Update TwitterAccountFollowers set FollowingsCount=:FollowingsCount, FollowersCount=:FollowersCount where UserId=:UserId and ProfileId=:ProfileId and EntryDate>=:EntryDate1 and EntryDate<=:EntryDate2";
                    int i = session.CreateQuery(str)
                        .SetParameter("FollowingsCount", _TwitterAccountFollowers.FollowingsCount)
                        .SetParameter("FollowersCount", _TwitterAccountFollowers.FollowersCount)
                        .SetParameter("UserId", _TwitterAccountFollowers.UserId)
                        .SetParameter("ProfileId",_TwitterAccountFollowers.ProfileId)
                        .SetParameter("EntryDate1", DateTime.Now.Date.AddSeconds(1))
                        .SetParameter("EntryDate2", DateTime.Now.Date.AddHours(12))
                        .ExecuteUpdate();
                    transaction.Commit();
                }
            }
        }

        public void UpdateTwitterAccountFollowerSecond(Domain.Socioboard.Domain.TwitterAccountFollowers _TwitterAccountFollowers)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //Begin session trasaction and opens up.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    string str = "Update TwitterAccountFollowers set FollowingsCount=:FollowingsCount, FollowersCount=:FollowersCount where UserId=:UserId and ProfileId=:ProfileId and EntryDate>=:EntryDate1 and EntryDate<=:EntryDate2";
                    int i = session.CreateQuery(str)
                        .SetParameter("FollowingsCount", _TwitterAccountFollowers.FollowingsCount)
                        .SetParameter("FollowersCount", _TwitterAccountFollowers.FollowersCount)
                        .SetParameter("UserId", _TwitterAccountFollowers.UserId)
                        .SetParameter("ProfileId", _TwitterAccountFollowers.ProfileId)
                         .SetParameter("EntryDate1", DateTime.Now.Date.AddHours(12).AddSeconds(1))
                        .SetParameter("EntryDate2", DateTime.Now.AddDays(1).Date.AddSeconds(-1))
                        .ExecuteUpdate();
                    transaction.Commit();
                }
            }
        }

    }
}
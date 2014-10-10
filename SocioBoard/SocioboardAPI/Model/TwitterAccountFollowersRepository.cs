using Api.Socioboard.Helper;
using Domain.Socioboard.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Api.Socioboard.Services
{
    public class TwitterAccountFollowersRepository
    {
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

    

    }
}
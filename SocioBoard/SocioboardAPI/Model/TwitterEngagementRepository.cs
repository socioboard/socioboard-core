using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Domain.Socioboard.Domain;
using Api.Socioboard.Helper;
using System.Collections;
using log4net;


namespace Api.Socioboard.Services
{

    public class TwitterEngagementRepository
    {
        ILog logger = LogManager.GetLogger(typeof(TwitterEngagementRepository));
        /// <Add>
        /// Add a new admin in DataBase. 
        /// </summary>
        /// <param name="user">Set Values in a Admin Class Property and Pass the Object of Admin Class (Domain.Socioboard.Domain.Admin).</param>
        public  void Add(Domain.Socioboard.Domain.TwitterEngagement user)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start and open Transaction. 
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    //Proceed action to save data.
                    try
                    {
                        session.Save(user);
                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                        logger.Error(ex.StackTrace);
                    }

                }//End Using trasaction
            }//End Using session
        }


        public ArrayList getTwitterStatsById(string twtuserid, Guid userid, int days)
        {
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        //Select * from TwitterStats where UserId = :userid and TwitterId = :Twtuserid and EntryDate>=DATE_ADD(NOW(),INTERVAL -" + days + " DAY) Group By Date(EntryDate)
                        NHibernate.IQuery query = session.CreateSQLQuery("SELECT * FROM TwitterEngagement WHERE UserId=:userid and EntryDate>=DATE_ADD(NOW(),INTERVAL -" + days + " DAY) and  ProfileId = :Twtuserid Group By Date(EntryDate)");
                        query.SetParameter("userid", userid);
                        query.SetParameter("Twtuserid", twtuserid);
                        ArrayList alstTwtStats = new ArrayList();

                        foreach (var item in query.List())
                        {
                            alstTwtStats.Add(item);
                        }
                        return alstTwtStats;

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                        return null;
                    }

                }
            }
        }


     }
}
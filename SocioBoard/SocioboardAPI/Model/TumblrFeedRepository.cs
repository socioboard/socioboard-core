using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using log4net;
using Domain.Socioboard.Domain;
using Api.Socioboard.Helper;
using NHibernate.Linq;


namespace Api.Socioboard.Services
{
    public class TumblrFeedRepository
    {
        static ILog logger = LogManager.GetLogger(typeof(TumblrFeedRepository));
        public static void Add(Domain.Socioboard.Domain.TumblrFeed tumblrFeed)
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
                        session.Save(tumblrFeed);
                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                     logger.Error("Add mien prob hai :"+ ex.Message);
                    }

                }//End Using trasaction
            }//End Using session
        }



        public bool checkTumblrMessageExists(Domain.Socioboard.Domain.TumblrFeed tumblrFeed)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //Begin session trasaction and opens up.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        //Proceed action, to get all data of facebook feed by user feed id and User id(Guid).
                        NHibernate.IQuery query = session.CreateQuery("from TumblrFeed where UserId = :userid and blogId = :msgid");
                        query.SetParameter("userid", tumblrFeed.UserId);
                        query.SetParameter("msgid", tumblrFeed.blogId);
                        var resutl = query.UniqueResult();

                        if (resutl == null)
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
            }//End session
        }

        public List<Domain.Socioboard.Domain.TumblrFeed> getFeedOfProfile(string profileid)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, open up a Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        //Proceed action, to get all twitter messages of profile by profile id
                        // And order by Entry date.
                        List<Domain.Socioboard.Domain.TumblrFeed> lstmsg = session.CreateQuery("from TumblrFeed where  ProfileId = :profid group by blogId")
                        .SetParameter("profid", profileid)
                        .List<Domain.Socioboard.Domain.TumblrFeed>()
                        .ToList<Domain.Socioboard.Domain.TumblrFeed>();

                        //List<TwitterMessage> lstmsg = new List<TwitterMessage>();
                        //foreach (TwitterMessage item in query.Enumerable<TwitterMessage>().OrderByDescending(x => x.MessageDate))
                        //{
                        //    lstmsg.Add(item);
                        //}
                        return lstmsg;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                        return null;
                    }

                }//End Transaction
            }//End Session
        }



        public List<Domain.Socioboard.Domain.TumblrFeed> GetFeedsOfProfile(string profileid, Guid id)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, open up a Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        //Proceed action, to get all twitter messages of profile by profile id
                        // And order by Entry date.
                        List<Domain.Socioboard.Domain.TumblrFeed> lstmsg = session.CreateQuery("from TumblrFeed where  ProfileId = :profid and UserId=:userid ")
                        .SetParameter("profid", profileid)
                        .SetParameter("userid", id)
                        .List<Domain.Socioboard.Domain.TumblrFeed>()
                        .ToList<Domain.Socioboard.Domain.TumblrFeed>();

                        //List<TwitterMessage> lstmsg = new List<TwitterMessage>();
                        //foreach (TwitterMessage item in query.Enumerable<TwitterMessage>().OrderByDescending(x => x.MessageDate))
                        //{
                        //    lstmsg.Add(item);
                        //}
                        return lstmsg;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                        return null;
                    }

                }//End Transaction
            }//End Session
        }


        public Domain.Socioboard.Domain.TumblrFeed getFeedOfProfilebyIdProfileId(string profileid, Guid id)
        {

            Domain.Socioboard.Domain.TumblrFeed result = new Domain.Socioboard.Domain.TumblrFeed();
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {

                    // proceed action, to get all Facebook Account of User by UserId(Guid) and FbUserId(string).
                    List<Domain.Socioboard.Domain.TumblrFeed> objlstfb = session.CreateQuery("from TumblrFeed where  ProfileId = :profid and Id=:id ")
                           .SetParameter("profid", profileid)
                        .SetParameter("id", id)
                       .List<Domain.Socioboard.Domain.TumblrFeed>().ToList<Domain.Socioboard.Domain.TumblrFeed>();
                    if (objlstfb.Count > 0)
                    {
                        result = objlstfb[0];
                    }
                    return result;
                }//End Transaction
            }//End session
        }
        public void UpdateTumblrFeed(Domain.Socioboard.Domain.TumblrFeed tumblrFeed)
        {
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {


                    try
                    {
                        
                    }

                    catch (Exception)
                    {

                        throw;
                    }
                }
            }

        }


        public int DeleteTumblrDataByUserid(Guid userid,string Profileid)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        //Proceed action, to delete twitter account info by user id.
                        NHibernate.IQuery query = session.CreateQuery("delete from TumblrFeed where UserId = :userid and ProfileId=:Profileid")
                                        .SetParameter("userid", userid)
                                        .SetParameter("Profileid", Profileid);
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


        public int DeleteTumblrDataByProfileid(string Profileid,string blogname)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        //Proceed action, to delete twitter account info by user id.
                        NHibernate.IQuery query = session.CreateQuery("delete from TumblrFeed where blogname=:blogname and ProfileId=:Profileid")
                                         .SetParameter("blogname", blogname)
                                        .SetParameter("Profileid", Profileid);
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









        public int UpdateDashboardOfProfileLikes(string profileid, Guid id, int like)
        {
            int update = 0;

            try
            {
                //Creates a database connection and opens up a session
                using (NHibernate.ISession session = SessionFactory.GetNewSession())
                {
                    //After Session creation, start Transaction.
                    using (NHibernate.ITransaction transaction = session.BeginTransaction())
                    {
                        try
                        {
                            //Proceed action, to update Data
                            // And Set the reuired paremeters to find the specific values.
                            update = session.CreateQuery("Update TumblrFeed set liked = :like where  ProfileId = :profileid and Id=:id")
                                .SetParameter("profileid", profileid)
                                .SetParameter("id", id)
                                  .SetParameter("like", like)
                                .ExecuteUpdate();

                            transaction.Commit();
                            update = 1;
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.StackTrace);
                            update = 0;
                        }
                    }//End Transaction
                }//End session
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error : " + ex.StackTrace);
            }

            return update;
        }


        public int UpdateDashboardOfProfileNotes(string profileid, Guid id, int like,int notes)
        {
            int update = 0;
           
            if (like == 0)
            {
                notes = notes - 1;
            }

            else
            {
               notes= notes + 1;
            }

            try
            {
                //Creates a database connection and opens up a session
                using (NHibernate.ISession session = SessionFactory.GetNewSession())
                {
                    //After Session creation, start Transaction.
                    using (NHibernate.ITransaction transaction = session.BeginTransaction())
                    {
                        try
                        {
                            //Proceed action, to update Data
                            // And Set the reuired paremeters to find the specific values.
                            update = session.CreateQuery("Update TumblrFeed set notes ='" + notes + "' where  ProfileId = :profileid and Id=:id")
                                .SetParameter("profileid", profileid)
                                .SetParameter("id", id)                                  
                                .ExecuteUpdate();

                            transaction.Commit();
                            update = 1;
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.StackTrace);
                            update = 0;
                        }
                    }//End Transaction
                }//End session
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error : " + ex.StackTrace);
            }

            return update;
        }

        public List<Domain.Socioboard.Domain.TumblrFeed> GetFeedsOfProfileWithRange(string profileid, Guid id, int noOfDataToSkip)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, open up a Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        //Proceed action, to get all twitter messages of profile by profile id
                        // And order by Entry date.
                        //List<Domain.Socioboard.Domain.TumblrFeed> lstmsg = session.CreateQuery("from TumblrFeed where  ProfileId = :profid and UserId=:userid ")
                        //.SetParameter("profid", profileid)
                        //.SetParameter("userid", id)
                        //.List<Domain.Socioboard.Domain.TumblrFeed>()
                        //.ToList<Domain.Socioboard.Domain.TumblrFeed>();

                        List<Domain.Socioboard.Domain.TumblrFeed> lstmsg = session.Query<Domain.Socioboard.Domain.TumblrFeed>().Where(u => u.UserId == id && u.ProfileId.Equals(profileid)).OrderByDescending(x => x.date).Skip(Convert.ToInt32(noOfDataToSkip)).Take(15).ToList<Domain.Socioboard.Domain.TumblrFeed>();
                   
                        return lstmsg;
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
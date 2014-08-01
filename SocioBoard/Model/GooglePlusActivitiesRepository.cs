using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SocioBoard.Domain;
using SocioBoard.Helper;

namespace SocioBoard.Model
{
    public class GooglePlusActivitiesRepository : IGooglePlusActivitiesRepository
    {

        /// <addgoogleplusActivity>
        /// Add a new googleplus Activity
        /// </summary>
        /// <param name="gpmsg">Set Values in a GooglePlusActivities Class Property and Pass the same Object of GooglePlusActivities Class.(Domain.GooglePlusActivities)</param>
        public void addgoogleplusActivity(GooglePlusActivities gpmsg)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        //Proceed action to save data.
                        session.Save(gpmsg);
                        transaction.Commit();
                    }
                    catch (Exception Err)
                    {
                        Console.Write(Err.Message.ToString());
                    }
                }//End Transaction
            }//End session
        }


        public int deletegoogleplusActivity(GooglePlusActivities gpmsg)
        {
            throw new NotImplementedException();
        }

        public int updategoogleplusActivity(GooglePlusActivities gpmsg)
        {
            throw new NotImplementedException();
        }


        /// <getAllgoogleplusActivityOfUser>
        /// Get the details of all google plus activity for a user.
        /// </summary>
        /// <param name="UserId">User id (Guid).</param>
        /// <param name="profileId">Profile id.(string)</param>
        /// <returns>List of Google plus activities. (List<Domain.GooglePlusActivities>)</returns>
        public List<GooglePlusActivities> getAllgoogleplusActivityOfUser(Guid UserId, string profileId)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        //Proceed action, to get all google plus activities.
                        List<GooglePlusActivities> alst = session.CreateQuery("from GooglePlusActivities where UserId = :userid and GpUserId = :profileId")
                        .SetParameter("userid", UserId)
                        .SetParameter("profileId", profileId)
                        .List<GooglePlusActivities>()
                        .ToList<GooglePlusActivities>();



                        #region oldcode
                        //List<GooglePlusActivities> alst = new List<GooglePlusActivities>();
                        //foreach (GooglePlusActivities item in query.Enumerable<GooglePlusActivities>().OrderByDescending(x => x.PublishedDate))
                        //{
                        //    alst.Add(item);
                        //} 
                        #endregion

                        return alst;

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                        return null;
                    }

                }//End Transaction
            }//End session
        }


        public List<GooglePlusActivities> getAllgplusOfUser(string profileId)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        //Proceed action, to get all google plus activities.



                        string str = "from GooglePlusActivities where GpUserId IN(";
                        string[] arrsrt = profileId.Split(',');
                        foreach (string sstr in arrsrt)
                        {
                            str += Convert.ToInt64(sstr) + ",";
                        }
                        str = str.Substring(0, str.Length - 1);
                        str += ") ORDER BY EntryDate DESC";

                       // List<GooglePlusActivities> alst = session.CreateQuery("from GooglePlusActivities where UserId = :userid and GpUserId = :profileId")
                        List<GooglePlusActivities> alst = session.CreateQuery(str)
                      //  .SetParameter("userid", UserId)
                       // .SetParameter("profileId", profileId)
                        .List<GooglePlusActivities>()
                        .ToList<GooglePlusActivities>();



                        #region oldcode
                        //List<GooglePlusActivities> alst = new List<GooglePlusActivities>();
                        //foreach (GooglePlusActivities item in query.Enumerable<GooglePlusActivities>().OrderByDescending(x => x.PublishedDate))
                        //{
                        //    alst.Add(item);
                        //} 
                        #endregion

                        return alst;

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                        return null;
                    }

                }//End Transaction
            }//End session
        }


        /// <getAllgoogleplusActivityOfUser>
        /// Get the all user activities on google plus by profile id.
        /// </summary>
        /// <param name="profileId">Google plus profile id (String)</param>
        /// <returns>List of Google plus activities class objects. (List<Domain.GooglePlusActivities>)</returns>
        public List<GooglePlusActivities> getAllgoogleplusActivityOfUser(string profileId)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        //Proceed action, to get all active
                        List<GooglePlusActivities> alst = session.CreateQuery("from GooglePlusActivities where  GpUserId = :profileId")

                        .SetParameter("profileId", profileId)
                        .List<GooglePlusActivities>()
                        .ToList<GooglePlusActivities>();



                        #region oldcode
                        //List<GooglePlusActivities> alst = new List<GooglePlusActivities>();
                        //foreach (GooglePlusActivities item in query.Enumerable<GooglePlusActivities>().OrderByDescending(x => x.PublishedDate))
                        //{
                        //    alst.Add(item);
                        //} 
                        #endregion

                        return alst;

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                        return null;
                    }

                }//End Transaction
            }//End Session
        }
        
        
        //public void getAllgoogleplusActivitysOfUsers(Guid UserId, string profileId)
        //{
        //    using (NHibernate.ISession session = SessionFactory.GetNewSession())
        //    {
        //        using (NHibernate.ITransaction transaction = session.BeginTransaction())
        //        {
        //            try
        //            {
        //                NHibernate.IQuery query = session.CreateQuery("from GooglePlusActivities where UserId = :userid and GpUserId = :profileId");
        //                query.SetParameter("userid", UserId);
        //                query.SetParameter("profileId", profileId);
        //                foreach (var item in query.Enumerable())
        //                {

        //                }
        //            }
        //            catch (Exception ex)
        //            {
        //                Console.WriteLine(ex.StackTrace);

        //            }

        //        }
        //    }
        //}



        /// <checkgoogleplusActivityExists>
        /// Check google plus Activity by user id and Activity Id.
        /// </summary>
        /// <param name="Id">Google activity id.(String)</param>
        /// <param name="Userid">User id. (Guid)</param>
        /// <returns>Bool (True or Flase)</returns>
        public bool checkgoogleplusActivityExists(string Id, Guid Userid)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        //Proceed action, to find all activities by user id and activity id.
                        NHibernate.IQuery query = session.CreateQuery("from GooglePlusActivities where UserId = :userid and ActivityId = :msgid");
                        query.SetParameter("userid", Userid);
                        query.SetParameter("msgid", Id);
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



        /// <deleteAllActivitysOfUser>
        /// delete All Activitys Of User
        /// </summary>
        /// <param name="gpuserid">Google plus user id.(String)</param>
        /// <param name="userid">Id of user. (Guid)</param>
        public void deleteAllActivitysOfUser(string gpuserid, Guid userid)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        //Proceed action, to delete data
                        NHibernate.IQuery query = session.CreateQuery("delete from GooglePlusActivities where UserId = :userid and GpUserId = :profileId");
                        query.SetParameter("userid", userid);
                        query.SetParameter("profileId", gpuserid);
                        int isUpdated = query.ExecuteUpdate();
                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);

                    }

                }//End Transaction
            }//End Session
        }


        /// <getgoogleplusActivity>
        /// Get all Google plus activities of user by profile id and user id.
        /// </summary>
        /// <param name="userid">User id. (Guid)</param>
        /// <param name="profileid">Google plus profile id.(String)</param>
        /// <returns>List of google plus activities class (List<GooglePlusActivities>)</returns>
        public List<GooglePlusActivities> getgoogleplusActivity(Guid userid, string profileid)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        //Proceed action to get all google plus activies by user id and Profile id.
                        List<GooglePlusActivities> alst = session.CreateQuery("from GooglePlusActivities where UserId = :userid and GpUserId = :profileId")
                        .SetParameter("userid", userid)
                        .SetParameter("profileId", profileid)
                        .List<GooglePlusActivities>()
                        .ToList<GooglePlusActivities>();

                        #region oldcode
                        //List<GooglePlusActivities> alst = new List<GooglePlusActivities>();
                        //foreach (GooglePlusActivities item in query.Enumerable<GooglePlusActivities>().OrderByDescending(x => x.PublishedDate))
                        //{
                        //    alst.Add(item);
                        //} 
                        #endregion

                        return alst;

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                        return null;
                    }

                }//End Transaction
            }//End Session
        }



        /// <DeleteGooglePlusActivitiesByUserid>
        /// Delete Google Plus Activities By User id.
        /// </summary>
        /// <param name="userid">User id (Guid)</param>
        /// <returns>Return 1 when Data is successfully deleted Otherwise retun 0.(int)</returns>
        public int DeleteGooglePlusActivitiesByUserid(Guid userid)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        //Proceed action, to deleting Data.
                        NHibernate.IQuery query = session.CreateQuery("delete from GooglePlusActivities where UserId = :userid")
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
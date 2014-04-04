using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SocioBoard.Domain;
using SocioBoard.Helper;
using System.Collections;

namespace SocioBoard.Model
{
    public class SocialProfilesRepository : ISocialProfilesRepository
    {

        /// <getAllSocialProfilesOfUser>
        /// Get All Social Profiles Of User
        /// </summary>
        /// <param name="userid">user id (Guid)</param>
        /// <returns>Return object of SocialProfile Class with  value of each member in the form of  list.(List<SocialProfile>)</returns>
        public List<SocialProfile> getAllSocialProfilesOfUser(Guid userid)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    //Proceed action, to get all Data by user id.
                    List<SocialProfile> alst = session.CreateQuery("from SocialProfile where UserId = :userid")
                    .SetParameter("userid", userid)
                    .List<SocialProfile>()
                    .ToList<SocialProfile>();


                    //List<SocialProfile> alst = new List<SocialProfile>();
                    // foreach (SocialProfile item in query.Enumerable())
                    // {
                    //     alst.Add(item);
                    // }
                    return alst;

                }//End Transaction
            }//End Session
        }


        /// <getAllSocialProfilesTypeOfUser>
        /// Get All Social Profiles Type Of User
        /// </summary>
        /// <param name="userid">User id. (Guid)</param>
        /// <param name="profiletype">Profile type. (String)</param>
        /// <returns>Return object of SocialProfile Class with  value of each member in the form of  list.(List<SocialProfile>)</returns>
        public List<SocialProfile> getAllSocialProfilesTypeOfUser(Guid userid, string profiletype)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    //Proceed action, to get all data from table by user id and profile type.
                    List<SocialProfile> alst = session.CreateQuery("from SocialProfile where UserId = :userid and ProfileType=:profiletype")
                    .SetParameter("userid", userid)
                    .SetParameter("profiletype", profiletype)
                    .List<SocialProfile>()
                    .ToList<SocialProfile>();

                    //List<SocialProfile> alst = new List<SocialProfile>();
                    //foreach (SocialProfile item in query.Enumerable())
                    //{
                    //    alst.Add(item);
                    //}

                    return alst;

                }//End Transaction
            }//End Session
        }


        /// <getAllSocialProfiles>
        /// Get All Social Profiles
        /// </summary>
        /// <returns>Return object of SocialProfile Class with  value of each member in the form of  list.(List<SocialProfile>)</returns>
        public List<SocialProfile> getAllSocialProfiles()
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    //Proceed action, to get all data.
                    List<SocialProfile> alst = session.CreateQuery("from SocialProfile").List<SocialProfile>().ToList<SocialProfile>();

                    //List<SocialProfile> alst = new List<SocialProfile>();
                    //foreach (SocialProfile item in query.Enumerable())
                    //{
                    //    alst.Add(item);
                    //}

                    return alst;

                }//End Transaction
            }//End Session
        }



        /// <getLimitProfilesOfUser>
        /// Get Limit Profiles Of User
        /// </summary>
        /// <param name="userid">User id.(Guid)</param>
        /// <param name="limit">Totale number required data. (int)</param>
        /// <returns>Return Array list with value.(ArrayList)</returns>
        public ArrayList getLimitProfilesOfUser(Guid userid, int limit)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    //Set defaulr max result
                    int maxResult = 3;
                    //Check the limit is not zero
                    if (limit == 0)
                        maxResult = 2;

                    //Proceed action, to get records by user id.
                    NHibernate.IQuery query = session.CreateQuery("from SocialProfile where UserId = :userid");
                    query.SetFirstResult(limit);
                    query.SetMaxResults(maxResult);
                    query.SetParameter("userid", userid);

                    ArrayList alst = new ArrayList();
                    foreach (var item in query.Enumerable())
                    {
                        alst.Add(item);
                    }

                    return alst;

                }//End Transaction
            }//End Session
        }


        /// <getLimitProfilesOfUser>
        /// Get Limit Profiles Of User
        /// </summary>
        /// <param name="userid">User id.(Guid)</param>
        /// <param name="limit">Limit of Required Data. (int)</param>
        /// <param name="maxResult">Get Maximum results.(int)</param>
        /// <returns>Return Array list with value.(ArrayList)</returns>
        public ArrayList getLimitProfilesOfUser(Guid userid, int limit, int maxResult)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    //Proceed action to get user records.
                    NHibernate.IQuery query = session.CreateQuery("from SocialProfile where UserId = :userid");
                    query.SetFirstResult(limit);
                    query.SetMaxResults(maxResult);
                    query.SetParameter("userid", userid);

                    ArrayList alst = new ArrayList();
                    foreach (var item in query.Enumerable())
                    {
                        alst.Add(item);
                    }
                    return alst;

                }//End Transaction
            }//End Session
        }


        /// <addNewProfileForUser>
        /// Add New Profile For User
        /// </summary>
        /// <param name="socio">Set Values in a SocialProfile Class Property and Pass the Object of SocialProfile Class.(Domein.SocialProfile)</param>
        public void addNewProfileForUser(SocialProfile socio)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    //Proceed action, to Save data.
                    session.Save(socio);
                    transaction.Commit();
                }//End Transaction
            }//End Session
        }



        /// <checkUserProfileExist>
        /// Check User Profile Exist
        /// </summary>
        /// <param name="socio">Set Values in a SocialProfile Class Property and Pass the Object of SocialProfile Class.(Domein.SocialProfile)</param>
        /// <returns>True or False.(bool)</returns>
        public bool checkUserProfileExist(SocialProfile socio)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        //Proceed action, to check Data.
                        NHibernate.IQuery query = session.CreateQuery("from SocialProfile where UserId = :userid and ProfileId = :profileid and  ProfileType = :profiletype");
                        query.SetParameter("userid", socio.UserId);
                        query.SetParameter("profileid", socio.ProfileId);
                        query.SetParameter("profiletype", socio.ProfileType);

                        var result = query.UniqueResult();
                        if (result == null)
                            return false;
                        else
                            return true;

                    }
                    catch (Exception ex)
                    {
                        Console.Write(ex.StackTrace);
                        return true;
                    }


                }//End Transaction
            }//End Session
        }



        /// <deleteProfile>
        /// Delte existing profile by user id and profile id.
        /// </summary>
        /// <param name="userid">user id.(Guid)</param>
        /// <param name="profileid">Profile id.(String)</param>
        /// <returns>Return 1 for true and 0 for false.(int)</returns>
        public int deleteProfile(Guid userid, string profileid)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        //Proceed action, to delete profile.
                        NHibernate.IQuery query = session.CreateQuery("delete from SocialProfile where UserId = :userid and ProfileId = :profileid")
                                        .SetParameter("userid", userid)
                                        .SetParameter("profileid", profileid);
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


        /// <updateSocialProfile>
        /// Update Social Profile
        /// </summary>
        /// <param name="socio">Set Values in a SocialProfile Class Property and Pass the Object of SocialProfile Class.(Domein.SocialProfile)</param>
        /// <returns>Return 1 for true and 0 for false.(int)</returns>
        public int updateSocialProfile(SocialProfile socio)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        //Proceed action, to update existing data from new profile value.
                        NHibernate.IQuery query = session.CreateQuery("Update SocialProfile set ProfileDate=:profiledate where UserId = :userid and ProfileId = :profileid")

                                        .SetParameter("profiledate", DateTime.Now)
                                         .SetParameter("userid", socio.UserId)
                                         .SetParameter("profileid", socio.ProfileId);
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

        //public SocialProfile getSocialProfilesOfUserById(Guid userid,string profileId)
        //{
        //    using (NHibernate.ISession session = SessionFactory.GetNewSession())
        //    {
        //        using (NHibernate.ITransaction transaction = session.BeginTransaction())
        //        {
        //            NHibernate.IQuery query = session.CreateQuery("from SocialProfile where UserId = :userid And ProfileId=:profileId");
        //            query.SetParameter("userid", userid);
        //            query.SetParameter("profileId", profileId);

        //            SocialProfile alst = (SocialProfile)query.UniqueResult();
        //            return alst;

        //        }
        //    }
        //}

        /// <checkProfileExistsMoreThanOne>
        /// Check Profile Exists More Than One
        /// </summary>
        /// <param name="profileid">Profile id.(String)</param>
        /// <returns>Return object of SocialProfile Class with  value of each member in the form of  list.(List<SocialProfile>)</returns>
        public List<SocialProfile> checkProfileExistsMoreThanOne(string profileid)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        List<SocialProfile> lstsocioprofile = session.CreateQuery("from SocialProfile where ProfileId = :profileid")
                        .SetParameter("profileid", profileid)
                        .List<SocialProfile>()
                        .ToList<SocialProfile>();

                        //List<SocialProfile> lstsocioprofile = new List<SocialProfile>();

                        //foreach (SocialProfile item in lstsocioprofile)
                        //{
                        //    lstsocioprofile.Add(item);
                        //}
                        return lstsocioprofile;

                    }
                    catch (Exception ex)
                    {
                        Console.Write(ex.StackTrace);
                        return null;
                    }
                }//End Transaction
            }//End Session
        }


        /// <DeleteSocialProfileByUserid>
        /// Delete Social Profile By User id
        /// </summary>
        /// <param name="userid">User id.</param>
        /// <returns>Return 1 for true and 0 for false.(int)</returns>
        public int DeleteSocialProfileByUserid(Guid userid)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        //Proceed action, to delete social profile by user id. 
                        NHibernate.IQuery query = session.CreateQuery("delete from SocialProfile where UserId = :userid")
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
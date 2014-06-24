using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SocioBoard.Domain;
using SocioBoard.Helper;
using System.Collections;

namespace SocioBoard.Model
{
    public class SocialProfilesRepository:ISocialProfilesRepository
    {
        public List<SocialProfile> getAllSocialProfilesOfUser(Guid userid)
        {
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
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
                         
                }
            }
        }

        public List<SocialProfile> getAllSocialProfilesTypeOfUser(Guid userid, string profiletype)
        {
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
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

                }
            }
        }

        public List<SocialProfile> getAllSocialProfiles()
        {
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    List<SocialProfile> alst = session.CreateQuery("from SocialProfile").List<SocialProfile>().ToList<SocialProfile>();

                    //List<SocialProfile> alst = new List<SocialProfile>();
                    //foreach (SocialProfile item in query.Enumerable())
                    //{
                    //    alst.Add(item);
                    //}

                    return alst;

                }
            }
        }

        public ArrayList getLimitProfilesOfUser(Guid userid, int limit)
        {
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    int maxResult = 3;
                    if (limit == 0)
                        maxResult = 2;
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

                }
            }
        
        }

        public ArrayList getLimitProfilesOfUser(Guid userid, int limit,int maxResult)
        {
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
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

                }
            }

        }

        public void addNewProfileForUser(SocialProfile socio)
        {
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    session.Save(socio);
                    transaction.Commit();
                }
            }
        }

        public bool checkUserProfileExist(SocialProfile socio)
        {
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
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

                    
                }
            }
        }

        public int deleteProfile(Guid userid,string profileid)
        {
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
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
                }
            }
        }

        public int updateSocialProfile(SocialProfile socio)
        {
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
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
                }
            }
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


        public List<SocialProfile> checkProfileExistsMoreThanOne(string profileid)
        {
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
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


                }
            }
        }
    }
}
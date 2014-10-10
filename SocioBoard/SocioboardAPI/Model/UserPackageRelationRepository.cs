using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Domain.Socioboard.Domain;
using Api.Socioboard.Helper;
using log4net;

namespace Api.Socioboard.Model
{
    public class UserPackageRelationRepository : IUserPackageRelation
    {
        ILog logger = LogManager.GetLogger(typeof(UserPackageRelationRepository));

        public List<UserPackageRelation> getAllUserPackageRelation()
        {
            List<UserPackageRelation> alstFBAccounts=null;
            try
            {
                using (NHibernate.ISession session = SessionFactory.GetNewSession())
                {
                    using (NHibernate.ITransaction transaction = session.BeginTransaction())
                    {
                        try
                        {
                            alstFBAccounts = session.CreateQuery("from UserPackageRelation").List<UserPackageRelation>().ToList<UserPackageRelation>();
                            return alstFBAccounts;
                        }
                        catch (Exception ex)
                        {
                            logger.Error("Error : " + ex.StackTrace);
                            Console.WriteLine("Error : " + ex.StackTrace);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Error("Error : " + ex.StackTrace);
                Console.WriteLine("Error : " + ex.StackTrace);
            }

            return alstFBAccounts;
        }


        public List<UserPackageRelation> getUserPackageRelationByUserId(User objuser)
        {
            List<UserPackageRelation> alstFBAccounts = new List<UserPackageRelation> ();
            try
            {
                using (NHibernate.ISession session = SessionFactory.GetNewSession())
                {
                    using (NHibernate.ITransaction transaction = session.BeginTransaction())
                    {
                        try
                        {
                            alstFBAccounts = session.CreateQuery("from  UserPackageRelation  where UserId = : UserId and PackageStatus= :stat")
                            .SetParameter("UserId", objuser.Id)
                            .SetParameter("stat", true)
                            .List<UserPackageRelation>().ToList<UserPackageRelation>();
                            
                        }
                        catch (Exception ex)
                        {
                            logger.Error("Error : " + ex.StackTrace);
                            Console.WriteLine("Error : " + ex.StackTrace);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Error("Error : " + ex.StackTrace);
                Console.WriteLine("Error : " + ex.StackTrace);
            }

            return alstFBAccounts;
        }

        public List<UserPackageRelation> getAllUserPackageRelationByUserId(User objuser)
        {
            List<UserPackageRelation> alstFBAccounts = null;
            try
            {
                using (NHibernate.ISession session = SessionFactory.GetNewSession())
                {
                    using (NHibernate.ITransaction transaction = session.BeginTransaction())
                    {
                        try
                        {
                            alstFBAccounts = session.CreateQuery("from  UserPackageRelation  where UserId = : UserId")
                            .SetParameter("UserId", objuser.Id)
                            .List<UserPackageRelation>().ToList<UserPackageRelation>();
                        }
                        catch (Exception ex)
                        {
                            logger.Error("Error : " + ex.StackTrace);
                            Console.WriteLine("Error : " + ex.StackTrace);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Error("Error : " + ex.StackTrace);
                Console.WriteLine("Error : " + ex.StackTrace);
            }

            return alstFBAccounts;
        }

        public void AddUserPackageRelation(UserPackageRelation userPackageRelation)
        {
            try
            {
                using (NHibernate.ISession session = SessionFactory.GetNewSession())
                {
                    using (NHibernate.ITransaction transaction = session.BeginTransaction())
                    {
                        session.Save(userPackageRelation);
                        transaction.Commit();
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Error("Error : " + ex.StackTrace);
                Console.WriteLine("Error : " + ex.StackTrace);
            }
        }

        public int DeleteUserPackageRelation(UserPackageRelation userPackageRelation)
        {
            int isUpdated = 0;
            try
            {
                using (NHibernate.ISession session = SessionFactory.GetNewSession())
                {
                    using (NHibernate.ITransaction transaction = session.BeginTransaction())
                    {
                        try
                        {
                            NHibernate.IQuery query = session.CreateQuery("delete from userPackageRelation where Id = :adsid")
                                            .SetParameter("adsid", userPackageRelation.Id);
                            isUpdated = query.ExecuteUpdate();
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
            catch (Exception ex)
            {
                logger.Error("Error : " + ex.StackTrace);
                Console.WriteLine("Error : " + ex.StackTrace);
            }

            return isUpdated;
        }

        public int UpdateUserPackageRelation(User userPackageRelation)
        {
            int i = 0;
            try
            {
                using (NHibernate.ISession session = SessionFactory.GetNewSession())
                {
                    using (NHibernate.ITransaction transaction = session.BeginTransaction())
                    {
                        try
                        {
                             i=session.CreateQuery("Update UserPackageRelation set PackageStatus =:packageStatus where UserId = :userId")
                                .SetParameter("packageStatus", false)
                                .SetParameter("userId", userPackageRelation.Id)
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
            catch (Exception ex)
            {
                logger.Error("Error : " + ex.StackTrace);
                Console.WriteLine("Error : " + ex.StackTrace);
            }
            return i;
        }

        public int UpdatePackageRelationByUserIdAndPackageId(User userPackageRelation)
        {
            int i = 0;
            try
            {
                using (NHibernate.ISession session = SessionFactory.GetNewSession())
                {
                    using (NHibernate.ITransaction transaction = session.BeginTransaction())
                    {
                        try
                        {
                            i = session.CreateQuery("Update UserPackageRelation set PackageStatus =:packageStatus where UserId = :userId")
                               .SetParameter("packageStatus", false)
                               .SetParameter("userId", userPackageRelation.Id)
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
            catch (Exception ex)
            {
                logger.Error("Error : " + ex.StackTrace);
                Console.WriteLine("Error : " + ex.StackTrace);
            }
            return i;
        }

        public int DeleteuserPackageRelationByUserid(Guid userid)
        {
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        NHibernate.IQuery query = session.CreateQuery("delete from userPackageRelation where UserId = :userid")
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
                }
            }
        }

        
    }
}
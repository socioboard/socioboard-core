using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SocioBoard.Helper;
using SocioBoard.Domain;
using log4net;

namespace SocioBoard.Model
{
    public class UserRefRelationRepository
    {
        ILog logger = LogManager.GetLogger(typeof(Registration));

        public int AddUserRefRelation(UserRefRelation userRefRelation)
        {
            int res = 0;
            try
            {
                using (NHibernate.ISession session = SessionFactory.GetNewSession())
                {
                    using (NHibernate.ITransaction transaction = session.BeginTransaction())
                    {
                        session.Save(userRefRelation);
                        transaction.Commit();

                        res = 1;
                    }
                }
                logger.Error("Coming out of AddUserRefRelation");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error : " + ex.StackTrace);
                logger.Error("UserRefRelationRepository>>AddUserRefRelation" + ex.Message);
            }

            return res;
        }

        public int UpdateUserRefRelation(UserRefRelation userRefRelation)
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
                            i = session.CreateQuery("Update UserRefRelation set ReferenceUserId =:referenceUserId, RefereeUserId =: refereeUserId , ReferenceUserEmail=:referenceUserEmail,RefereeUserEmail =:refereeUserEmail,EntryDate =:entryDate  where Id = :id")
                                      .SetParameter("referenceUserId", userRefRelation.ReferenceUserId)
                                      .SetParameter("refereeUserId", userRefRelation.RefereeUserId)
                                      .SetParameter("referenceUserEmail", userRefRelation.ReferenceUserEmail)
                                      .SetParameter("refereeUserEmail", userRefRelation.RefereeUserEmail)
                                      .SetParameter("entryDate", userRefRelation.EntryDate)

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
            catch (Exception ex)
            {
                Console.WriteLine("Error : " + ex.StackTrace);
            }

            return i;
        }

        public int UpdateStatusById(UserRefRelation userRefRelation)
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
                            i = session.CreateQuery("Update UserRefRelation set Status =:status  where Id = :id")
                                      .SetParameter("status", userRefRelation.Status)
                                      .SetParameter("id", userRefRelation.Id)

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
            catch (Exception ex)
            {
                Console.WriteLine("Error : " + ex.StackTrace);
            }

            return i;
        }


        public bool UpdateStatusByReferenceAndRefreeId(Guid reference, Guid refree)
        {
            List<UserRefRelation> lstUserRefRelation;
            bool res = false;
            try
            {
                using (NHibernate.ISession session = SessionFactory.GetNewSession())
                {
                    using (NHibernate.ITransaction transaction = session.BeginTransaction())
                    {
                        try
                        {
                            lstUserRefRelation = session.CreateQuery("select Status from UserRefRelation where ReferenceUserId=:reference and RefereeUserId:refree")
                          .SetParameter("reference", refree)
                          .SetParameter("refree", refree)
                          .List<UserRefRelation>().ToList<UserRefRelation>();

                            if (lstUserRefRelation.Count > 0)
                            {
                                if (lstUserRefRelation[0].Status == null)
                                {
                                    res = true;
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.StackTrace);

                        }

                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error : " + ex.StackTrace);
            }

            return res;
        }



        public List<UserRefRelation> GetAllUserRefRelationInfo(UserRefRelation userRefRelation)
        {
            List<UserRefRelation> lstUserRefRelation = new List<UserRefRelation>();
            try
            {
                using (NHibernate.ISession session = SessionFactory.GetNewSession())
                {
                    using (NHibernate.ITransaction transaction = session.BeginTransaction())
                    {
                        try
                        {
                            lstUserRefRelation = session.CreateQuery("from UserRefRelation u where u.Id=:id")
                            .SetParameter("id", userRefRelation.Id)
                            .List<UserRefRelation>().ToList<UserRefRelation>();
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.StackTrace);

                        }

                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);

            }

            return lstUserRefRelation;
        }

        public List<UserRefRelation> GetUserRefRelationInfo()
        {
            List<UserRefRelation> lstUserRefRelation = new List<UserRefRelation>();
            try
            {
                using (NHibernate.ISession session = SessionFactory.GetNewSession())
                {
                    using (NHibernate.ITransaction transaction = session.BeginTransaction())
                    {
                        try
                        {
                            lstUserRefRelation = session.CreateQuery("from UserRefRelation u where u.Id !=null")
                            .List<UserRefRelation>().ToList<UserRefRelation>();
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.StackTrace);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);

            }

            return lstUserRefRelation;
        }


        public List<UserRefRelation> GetUserRefRelationInfoByReferenceId(UserRefRelation userRefRelation)
        {
            List<UserRefRelation> lstUserRefRelation = new List<UserRefRelation>();
            try
            {
                using (NHibernate.ISession session = SessionFactory.GetNewSession())
                {
                    using (NHibernate.ITransaction transaction = session.BeginTransaction())
                    {
                        try
                        {
                            lstUserRefRelation = session.CreateQuery("from UserRefRelation u where u.ReferenceUserId =:referenceUserId")
                                .SetParameter("referenceUserId", userRefRelation.ReferenceUserId)
                            .List<UserRefRelation>().ToList<UserRefRelation>();
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.StackTrace);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);

            }

            return lstUserRefRelation;
        }

        public List<UserRefRelation> GetUserRefRelationInfoByRefreeId(UserRefRelation userRefRelation)
        {
            List<UserRefRelation> lstUserRefRelation = new List<UserRefRelation>();
            try
            {
                using (NHibernate.ISession session = SessionFactory.GetNewSession())
                {
                    using (NHibernate.ITransaction transaction = session.BeginTransaction())
                    {
                        try
                        {
                            lstUserRefRelation = session.CreateQuery("from UserRefRelation u where u.RefereeUserId =:referenceUserId")
                                .SetParameter("referenceUserId", userRefRelation.ReferenceUserId)
                            .List<UserRefRelation>().ToList<UserRefRelation>();
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.StackTrace);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);

            }

            return lstUserRefRelation;
        }
    }
}
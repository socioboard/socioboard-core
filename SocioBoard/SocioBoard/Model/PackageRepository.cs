using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SocioBoard.Helper;
using SocioBoard.Domain;

namespace SocioBoard.Model
{
    public class PackageRepository :IPackageRepository
    {
        public void AddPackage(Package package)
        {
            try
            {
                using (NHibernate.ISession session = SessionFactory.GetNewSession())
                {
                    using (NHibernate.ITransaction transaction = session.BeginTransaction())
                    {
                        session.Save(package);
                        transaction.Commit();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                // return 0;
            }
        }

     
        public int DeletePackage(Guid packageid)
        {
            try
            {
                using (NHibernate.ISession session = SessionFactory.GetNewSession())
                {
                    using (NHibernate.ITransaction transaction = session.BeginTransaction())
                    {
                        try
                        {
                            NHibernate.IQuery query = session.CreateQuery("delete from Package where Id = :packageid")
                                            .SetParameter("packageid", packageid);
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
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return 0;
            }
        }

        public void UpdatePackage(Package package)
        {
            try
            {
                using (NHibernate.ISession session = SessionFactory.GetNewSession())
                {
                    using (NHibernate.ITransaction transaction = session.BeginTransaction())
                    {
                        try
                        {
                            session.CreateQuery("Update Package set PackageName =:packagename , Pricing=:pricing, Status=:status where Id = :packageid")
                                .SetParameter("packagename", package.PackageName)
                                .SetParameter("pricing", package.Pricing)
                                .SetParameter("status", package.Status)
                                .SetParameter("packageid", package.Id)
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
                Console.WriteLine(ex.StackTrace);
                // return 0;
            }
        }

        public List<Package> getAllPackage()
        {
            List<Package> alstFBAccounts = new List<Package>();
            try
            {
                using (NHibernate.ISession session = SessionFactory.GetNewSession())
                {
                    using (NHibernate.ITransaction transaction = session.BeginTransaction())
                    {
                        alstFBAccounts = session.CreateQuery("from Package order by Pricing").List<Package>().ToList<Package>();

                        //List<Package> alstFBAccounts = new List<Package>();

                        //foreach (Package item in query.Enumerable())
                        //{
                        //    alstFBAccounts.Add(item);
                        //}
                        

                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                // return 0;
            }

            return alstFBAccounts;
        }

        public bool checkPackageExists(string packagename)
        {
            try
            {
                using (NHibernate.ISession session = SessionFactory.GetNewSession())
                {
                    using (NHibernate.ITransaction transaction = session.BeginTransaction())
                    {
                        try
                        {
                            NHibernate.IQuery query = session.CreateQuery("from Package where PackageName =:packagename");
                            query.SetParameter("packagename", packagename);
                            var result = query.UniqueResult();
                            if (result == null)
                                return false;
                            else
                                return true;

                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.StackTrace);
                            return false;
                        }


                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return false;
            }
        }

        public bool checkPackageExists(Guid packageid)
        {
            try
            {
                using (NHibernate.ISession session = SessionFactory.GetNewSession())
                {
                    using (NHibernate.ITransaction transaction = session.BeginTransaction())
                    {
                        try
                        {
                            NHibernate.IQuery query = session.CreateQuery("from Package where Id =:packageid");
                            query.SetParameter("packageid", packageid);
                            var result = query.UniqueResult();
                            if (result == null)
                                return false;
                            else
                                return true;

                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.StackTrace);
                            return false;
                        }


                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return false;
            }
        }

        public Package getPackageDetails(string packagename)
        {
            try
            {
                using (NHibernate.ISession session = SessionFactory.GetNewSession())
                {
                    using (NHibernate.ITransaction transaction = session.BeginTransaction())
                    {
                        try
                        {
                            NHibernate.IQuery query = session.CreateQuery("from Package where PackageName=:packagename");
                            query.SetParameter("packagename", packagename);
                            Package grou = query.UniqueResult<Package>();
                            return grou;
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.StackTrace);
                            return null;
                        }


                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return null;
            }
        }

        public Package getPackageDetailsbyId(Guid packageid)
        {
            try
            {
                using (NHibernate.ISession session = SessionFactory.GetNewSession())
                {
                    using (NHibernate.ITransaction transaction = session.BeginTransaction())
                    {
                        try
                        {
                            NHibernate.IQuery query = session.CreateQuery("from Package where Id=:packageid");

                            query.SetParameter("packageid", packageid);
                            Package grou = query.UniqueResult<Package>();
                            return grou;
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.StackTrace);
                            return null;
                        }


                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return null;
            }
        }
    }
}
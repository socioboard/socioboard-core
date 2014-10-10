using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Api.Socioboard.Helper;
using Domain.Socioboard.Domain;

namespace Api.Socioboard.Model
{
    public class PackageRepository :IPackageRepository
    {
        /// <AddPackage>
        /// Add Package
        /// </summary>
        /// <param name="package">Set Values in a Package Class Property and Pass the Object of Package Class.(Domein.Package)</param>
        public void AddPackage(Package package)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    //Proceed action, to save data.
                    session.Save(package);
                    transaction.Commit();
                }//End Transaction
            }//End Session
        }


        /// <DeletePackage>
        /// Delete Package
        /// </summary>
        /// <param name="packageid">Package Id.(Guid)</param>
        /// <returns>Return 1 for Successfully updated and 0 for Failed Updation.(int)</returns>
        public int DeletePackage(Guid packageid)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        //Proceed action, to delete package data by id.
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
                }//End Transaction
            }//End Session
        }


        /// <UpdatePackage>
        /// Update Package
        /// </summary>
        /// <param name="package">Set Values in a Package Class Property and Pass the Object of Package Class.(Domein.Package)</param>
        public void UpdatePackage(Package package)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        //Proceed action, to update data.
                        session.CreateQuery("Update Package set PackageName =:packagename , Pricing=:pricing, Status=:status where Id = :packageid")
                            .SetParameter("packagename", package.PackageName)
                            .SetParameter("pricing", package.Pricing)
                            .SetParameter("status",package.Status)
                            .SetParameter("packageid",package.Id)
                            .ExecuteUpdate();
                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                        // return 0;
                    }
                }//End Transaction
            }//End Session
        }


        /// <getAllPackage>
        /// Get All Package
        /// </summary>
        /// <returns>Return object of Package Class with  value of each member in the form of list.(List<Package>)</returns>
        public List<Package> getAllPackage()
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    //Proceed action, to get all package data.
                    List<Package> alstFBAccounts = session.CreateQuery("from Package order by Pricing").List<Package>().ToList<Package>();

                    //List<Package> alstFBAccounts = new List<Package>();

                    //foreach (Package item in query.Enumerable())
                    //{
                    //    alstFBAccounts.Add(item);
                    //}
                    return alstFBAccounts;

                }//End Transaction
            }//End Session
        }


        /// <checkPackageExists>
        /// Check Package is Exists
        /// </summary>
        /// <param name="packagename">Name of package.(String)</param>
        /// <returns>True or Flase.(bool)</returns>
        public bool checkPackageExists(string packagename)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        //Proceed action, to get package by name.
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
                        return true;
                    }
                }//End Transaction
            }//End Session
        }


        /// <checkPackageExists>
        /// Check Package by id.
        /// </summary>
        /// <param name="packageid">Package id.(Guid)</param>
        /// <returns>True or False.(bool)</returns>
        public bool checkPackageExists(Guid packageid)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        //Proceed action, to get package by id.
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
                        return true;
                    }

                }//End Transaction
            }//End Session
        }


        /// <getPackageDetails>
        /// Get Package Details by package name.
        /// </summary>
        /// <param name="packagename">package name.(String)</param>
        /// <returns>Return the object of package class with value.(Domain.package)</returns>
        public Package getPackageDetails(string packagename)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        //proceed action, to get package details by name.
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
                }//End Transaction
            }//End Session
        }


        /// <getPackageDetailsbyId>
        /// Get Package Details by Id
        /// </summary>
        /// <param name="packageid">Package</param>
        /// <returns>Return the object of package class with value.(Domain.package)</returns>
        public Package getPackageDetailsbyId(Guid packageid)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        //Proceed action, to get details of package by id.
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
                }//End Transaction
            }//End Session
        }
    }
}
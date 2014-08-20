using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SocioBoard.Helper;
using log4net;
using SocioBoard.Domain;
using SocioBoard.Admin.Scheduler;

namespace SocioBoard.Model
{
    public class LoginLogsRepository
    {
        /// <logger>
        /// To get log messages
        /// </logger>
        ILog logger = LogManager.GetLogger(typeof(LoginLogsRepository));


        /// <Add>
        /// Add logging details of user
        /// </Add>
        /// <param name="loginLogs">Set Values in a LoginLogs Class Property and Pass the Object of LoginLogs Class (SocioBoard.Domain.LoginLogs).</param>
        public void Add(SocioBoard.Domain.LoginLogs loginLogs)
        {
            try
            {
                //Creates a database connection and opens up a session
                using (NHibernate.ISession session = SessionFactory.GetNewSession())
                {
                    //After Session creation, start Transaction. 
                    using (NHibernate.ITransaction transaction = session.BeginTransaction())
                    {
                        //Proceed action, to save details.
                        session.Save(loginLogs);
                        transaction.Commit();
                    }//End Transaction
                }//End Session
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
                logger.Error(ex.StackTrace);
            }
        }


        /// <GetLoginDetailsByUserId>
        /// Get Login Details By User Id
        /// </GetLoginDetailsByUserId>
        /// <param name="UserId">Id of socioboard User.(Guid)</param>
        /// <returns>Return objects of LoginLogs Class with value of each member in the form of list.(List<LoginLogs>)</returns>
        public List<LoginLogs> GetLoginDetailsByUserId(Guid UserId)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        List<LoginLogs> lst = session.CreateQuery("From LoginLogs where UserId = :userid order by LoginTime desc")
                                           .SetParameter("userid", UserId)
                                           .List<LoginLogs>()
                                           .ToList<LoginLogs>();
                        return lst;

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                        return null;
                    }
                }//End Transaction
            }//End Session
        }


        /// <GetAllLoginLogsDetails>
        /// Get All Login Logs Details
        /// </GetAllLoginLogsDetails>
        /// <returns>Return objects of LoginLogsTracker Class with value of each member in the form of list.(List<LoginLogsTracker>)</returns>
        public List<LoginLogsTracker> GetAllLoginLogsDetails()
        {
            //Create a list to store all object in list.
            List<LoginLogsTracker> lstLoginLogsTracker = new List<LoginLogsTracker>();

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
                            //Proceed action, to get all log record for an account
                            var res = session.CreateQuery("select count(Id),UserId,UserName from LoginLogs group by UserId order by LoginTime desc");

                            //Get the all return value from res
                            foreach (Object[] item in res.Enumerable())
                            {
                                try
                                {
                                    // Make new individual LoginLogsTracker object to set records
                                    LoginLogsTracker objLoginLogsTracker = new LoginLogsTracker();
                                    objLoginLogsTracker._count = Convert.ToInt32(item[item.Length - 3]);
                                    objLoginLogsTracker._UserId = Guid.Parse(item[item.Length - 2].ToString());
                                    objLoginLogsTracker._UserName = (item[item.Length - 1].ToString());
                                    //Add these records in the list.
                                    lstLoginLogsTracker.Add(objLoginLogsTracker);

                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine("Error : " + ex.StackTrace);
                                }
                            }

                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.StackTrace);
                            //return null;
                        }
                    }//End Transaction
                }//End Session
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                //return null;
            }

            return lstLoginLogsTracker;
        }


    }
}
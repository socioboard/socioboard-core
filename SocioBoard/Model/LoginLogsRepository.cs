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
        ILog logger = LogManager.GetLogger(typeof(LoginLogsRepository));
        public void Add(SocioBoard.Domain.LoginLogs loginLogs)
        {
            try
            {
                using (NHibernate.ISession session = SessionFactory.GetNewSession())
                {
                    using (NHibernate.ITransaction transaction = session.BeginTransaction())
                    {
                        session.Save(loginLogs);
                        transaction.Commit();
                    }
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
                logger.Error(ex.StackTrace);
            }
        }

        public List<LoginLogs> GetLoginDetailsByUserId(Guid UserId)
        {
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
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
                }
            }

        }


        public List<LoginLogsTracker> GetAllLoginLogsDetails()
        {
            List<LoginLogsTracker> lstLoginLogsTracker = new List<LoginLogsTracker>();

            try
            {
                using (NHibernate.ISession session = SessionFactory.GetNewSession())
                {
                    using (NHibernate.ITransaction transaction = session.BeginTransaction())
                    {
                        try
                        {
                            var res = session.CreateQuery("select count(Id),UserId,UserName from LoginLogs group by UserId order by LoginTime desc");

                            foreach (Object[] item in res.Enumerable())
                            {
                                try
                                {
                                    LoginLogsTracker objLoginLogsTracker = new LoginLogsTracker();
                                    objLoginLogsTracker._count = Convert.ToInt32(item[item.Length - 3]);
                                    objLoginLogsTracker._UserId = Guid.Parse(item[item.Length - 2].ToString());
                                    objLoginLogsTracker._UserName = (item[item.Length - 1].ToString());

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
                    }
                }
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
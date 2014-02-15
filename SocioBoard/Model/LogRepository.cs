using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SocioBoard.Domain;
using SocioBoard.Helper;
namespace SocioBoard.Model
{
    public class LogRepository : ILogException
    {
        private static readonly Object LockAddLog = new Object();
        public void AddLog(Log log)
        {

            lock (LockAddLog)
            {
                try
                {
                    using (NHibernate.ISession session = SessionFactory.GetNewSession())
                    {
                        using (NHibernate.ITransaction transaction = session.BeginTransaction())
                        {
                            session.Save(log);
                            transaction.Commit();
                        }
                    }
                }
                catch (Exception ex)
                {
                }

            }
        }

        public void DeleteLog(Log log)
        {
            throw new NotImplementedException();
        }

        public void UpdateLog(Log log)
        {
            throw new NotImplementedException();
        }

        public List<Log> getAllLogs(Log log)
        {
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                       // List<Log> lstlog = new List<Log>();
                        List<Log> lstlog = session.CreateQuery("from Log").List<Log>().ToList<Log>();
                        //foreach (Log item in query.Enumerable<Log>())
                        //{
                        //    lstlog.Add(item);
                        //}
                        return lstlog;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                        return null;
                    }
                }
            }
        }


        public int DeleteLogByUserid(Guid userid)
        {
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        NHibernate.IQuery query = session.CreateQuery("delete from Log where UserId = :userid")
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
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
        /// <LockAddLog>
        /// Create an object to manage thread 
        /// </LockAddLog>
        private static readonly Object LockAddLog = new Object();


        /// <AddLog>
        /// Add Log
        /// </AddLog>
        /// <param name="log">Set Values in a Log Class Property and Pass the Object of Log Class (SocioBoard.Domain.Log).</param>
        public void AddLog(Log log)
        {
            //Lock the functionality 
            //When multiple users to access add data at same time.
            lock (LockAddLog)
            {
                try
                {
                    //Creates a database connection and opens up a session
                    using (NHibernate.ISession session = SessionFactory.GetNewSession())
                    {
                        //After Session creation, start Transaction. 
                        using (NHibernate.ITransaction transaction = session.BeginTransaction())
                        {
                            //Proceed action, to save new details
                            session.Save(log);
                            transaction.Commit();
                        }//End Transaction
                    }//End Session
                }
                catch (Exception ex)
                {
                }

            }//End Lock
        }

        public void DeleteLog(Log log)
        {
            throw new NotImplementedException();
        }

        public void UpdateLog(Log log)
        {
            throw new NotImplementedException();
        }


        /// <getAllLogs>
        /// Get the all log records
        /// </getAllLogs>
        /// <param name="log"></param>
        /// <returns></returns>
        public List<Log> getAllLogs(Log log)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction. 
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        //Proceed action, to get all records.
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
                }//End Transaction
            }//End Session
        }


        /// <DeleteLogByUserid>
        /// Delete Log By User id
        /// </DeleteLogByUserid>
        /// <param name="userid"></param>
        /// <returns></returns>
        public int DeleteLogByUserid(Guid userid)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction. 
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        //Proceed action, to delete records by user id.
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
                }//End Transaction
            }//End Session
        }


    }
}
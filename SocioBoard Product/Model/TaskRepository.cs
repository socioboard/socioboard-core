using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;
using SocioBoard.Helper;
using SocioBoard.Domain;
using NHibernate.SqlCommand;
using NHibernate.Transform;
using NHibernate;
using NHibernate.Criterion;
using NHibernate.Type;

namespace SocioBoard.Model
{
    public class TaskRepository : ITaskRepository
    {
        public void addTask(Tasks task)
        {
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        session.Save(task);
                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                    }
                }
            }
        }

        public int deleteTask(string taskid, Guid userid)
        {
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        NHibernate.IQuery query = session.CreateQuery("delete from Task where Id = :taskid and UserId = :userid")
                                        .SetParameter("taskid", taskid)
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

        public void updateTask(Tasks task)
        {
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        session.CreateQuery("Update Tasks set TaskMessage =:TaskMessage,UserId =:UserId,AssignTaskTo =:AssignTaskTo,TaskStatus=:TaskStatus,AssignDate=:AssignDate where Id = :taskid and UserId = :userid")
                            .SetParameter("TaskMessage", task.TaskMessage)
                            .SetParameter("UserId", task.UserId)
                            .SetParameter("AssignTaskTo", task.AssignTaskTo)
                            .SetParameter("TaskStatus", task.TaskStatus)
                            .SetParameter("AssignDate",task.AssignDate)
                            .SetParameter("taskid", task.Id)
                            .SetParameter("userid", task.UserId)
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

        public ArrayList getAllTasksOfUser(Guid UserId)
        {
            ArrayList alstTask = new ArrayList();
            try
            {
                using (NHibernate.ISession session = SessionFactory.GetNewSession())
                {
                    using (NHibernate.ITransaction transaction = session.BeginTransaction())
                    {
                        NHibernate.IQuery query = session.CreateQuery("from Tasks where UserId = :userid");
                        query.SetParameter("userid", UserId);
                       

                        foreach (var item in query.Enumerable())
                        {
                            alstTask.Add(item);
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error : " + ex.StackTrace);
            }
            return alstTask;
        }

        public ArrayList getAllIncompleteTasksOfUser(Guid UserId)
        {

            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    NHibernate.IQuery query = session.CreateQuery("from Tasks where UserId = :userid and TaskStatus = 0");
                    query.SetParameter("userid", UserId);
                    ArrayList alstTask = new ArrayList();
                    foreach (var item in query.Enumerable())
                    {
                        alstTask.Add(item);
                    }
                    return alstTask;

                }
            }

        }


        public Tasks getTaskById(string Taskid, Guid userId)
        {
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    NHibernate.IQuery query = session.CreateQuery("from FacebookAccount where FbUserId = :Fbuserid and UserId=:userId");
                    query.SetParameter("Fbuserid", Taskid);
                    query.SetParameter("userId", userId);
                    Tasks result = (Tasks)query.UniqueResult();
                    return result;
                }
            }
        }

        public bool checkTaskExists(string TaskId, Guid Userid)
        {
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        NHibernate.IQuery query = session.CreateQuery("from Task where UserId = :userid and Id = :taskid");
                        query.SetParameter("userid", Userid);
                        query.SetParameter("taskid", TaskId);
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

                }
            }
        }

        public void updateTaskStatus(Guid TaskId,Guid UserId,bool status)
        {
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        session.CreateQuery("Update Tasks set TaskStatus=:TaskStatus, CompletionDate=:completedate where Id = :taskid and UserId = :userid")
                            .SetParameter("userid", UserId)
                            .SetParameter("taskid", TaskId)
                            .SetParameter("TaskStatus", status)
                            .SetParameter("completedate", DateTime.Now)
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

        public ArrayList getAllTasksOfUserByStatus(Guid UserId,bool status)
        {

            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    NHibernate.IQuery query = session.CreateQuery("from Tasks where UserId = :userid and TaskStatus=:TaskStatus");
                    query.SetParameter("userid", UserId);
                    query.SetParameter("TaskStatus", status);
                    ArrayList alstTask = new ArrayList();

                    foreach (var item in query.Enumerable())
                    {
                        alstTask.Add(item);
                    }
                    return alstTask;

                }
            }

        }

        public ArrayList getAllMyTasksOfUser(Guid UserId,Guid AssignTo)
        {

            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    NHibernate.IQuery query = session.CreateQuery("from Tasks where UserId = :userid and AssignTaskTo=:AssignTo");
                    query.SetParameter("userid", UserId);
                    query.SetParameter("AssignTo", AssignTo);
                    ArrayList alstTask = new ArrayList();

                    foreach (var item in query.Enumerable())
                    {
                        alstTask.Add(item);
                    }
                    return alstTask;

                }
            }

        }

        public ArrayList getTasksByUserwithDetail(Guid USerId,int days)
        {
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    var queryString = @"SELECT * FROM Tasks ts LEFT JOIN User u on ts.AssignTaskTo=u.Id where ts.UserId=:userid and AssignDate>=DATE_ADD(NOW(),INTERVAL -" + days + " DAY)";
                     var query = session.CreateSQLQuery(queryString)
                      .SetParameter("userid", USerId);

                    ArrayList alstTask = new ArrayList();

                    foreach (var item in query.List())
                    {
                        alstTask.Add(item);
                    }
                    return alstTask;

                }
            }
        }

        public List<int> GetTaskByUserIdAndYear(Guid Userid)
        {
            List<int> lstShareCount = new List<int>();
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        //int year = DateTime.Now.Year;

                        List<string> lstYear = GetLastYears();

                        for (int i = 0; i < 12; i++)
                        {
                            string year = lstYear[i];

                            List<Tasks> lstFacebookStats = session.CreateQuery("from Tasks where AssignDate Like :year and UserId= :userId ")
                                .SetParameter("userId", Userid)
                                .SetParameter("year", "%" + year + "%", TypeFactory.GetAnsiStringType(15)).List<Tasks>().ToList<Tasks>(); 

                            lstShareCount.Add(lstFacebookStats.Count);
                        }

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);

                    }

                    return lstShareCount;

                }
            }
        }

        public List<string> GetLastYears()
        {
            List<string> arryear = new List<string>();

            try
            {
                string currentYear = DateTime.Now.Year.ToString();

                bool flag = true;
                int fromyear = Convert.ToInt32(currentYear) - 11;
                while (flag)
                {
                    arryear.Add(fromyear.ToString());
                    fromyear++;
                    if (fromyear.ToString() == currentYear)
                    {
                        flag = false;
                        arryear.Add(currentYear);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error : " + ex.StackTrace);
            }

            return arryear;
        }
    }
}
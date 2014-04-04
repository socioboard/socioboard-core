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

        /// <addTask>
        /// Add New Task
        /// </summary>
        /// <param name="task">Set Values in a Tasks Class Property and Pass the Object of Tasks Class.(Domein.Tasks)</param>
        public void addTask(Tasks task)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        //Proceed action, to save new data.
                        session.Save(task);
                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                    }
                }//End Transaction
            }//End Session
        }



        /// <deleteTask>
        /// Delete Task
        /// </summary>
        /// <param name="taskid">Task id.(String)</param>
        /// <param name="userid">User id.(Guid)</param>
        /// <returns>Return 1 for true and 0 for false.(int)</returns>
        public int deleteTask(string taskid, Guid userid)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        //Proceed action, to delete task by task id and user id.
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
                }//End Transaction
            }//End Session
        }


        /// <updateTask>
        /// Update Task
        /// </summary>
        /// <param name="task">Set Values in a Tasks Class Property and Pass the Object of Tasks Class.(Domein.Tasks)</param>
        public void updateTask(Tasks task)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        //Proceed action, to update task 
                        session.CreateQuery("Update Tasks set TaskMessage =:TaskMessage,UserId =:UserId,AssignTaskTo =:AssignTaskTo,TaskStatus=:TaskStatus,AssignDate=:AssignDate where Id = :taskid and UserId = :userid")
                            .SetParameter("TaskMessage", task.TaskMessage)
                            .SetParameter("UserId", task.UserId)
                            .SetParameter("AssignTaskTo", task.AssignTaskTo)
                            .SetParameter("TaskStatus", task.TaskStatus)
                            .SetParameter("AssignDate", task.AssignDate)
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
                }//End Transaction
            }//End Session
        }



        /// <getAllTasksOfUser>
        /// Get All Tasks Of User
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns>Return values in the form of array list.(ArrayList)</returns>
        public ArrayList getAllTasksOfUser(Guid UserId)
        {
            ArrayList alstTask = new ArrayList();
            try
            {
                //Creates a database connection and opens up a session
                using (NHibernate.ISession session = SessionFactory.GetNewSession())
                {
                    //After Session creation, start Transaction.
                    using (NHibernate.ITransaction transaction = session.BeginTransaction())
                    {
                        //Proceed action, to get all task of user.
                        NHibernate.IQuery query = session.CreateQuery("from Tasks where UserId = :userid");
                        query.SetParameter("userid", UserId);


                        foreach (var item in query.Enumerable())
                        {
                            alstTask.Add(item);
                        }

                    }//End Transaction
                }//End Session
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error : " + ex.StackTrace);
            }
            return alstTask;
        }


        /// <getAllIncompleteTasksOfUser>
        /// Get All Incomplete Tasks Of User
        /// </summary>
        /// <param name="UserId">User id (Guid)</param>
        /// <returns>Return values in the form of array list.(ArrayList)</returns>
        public ArrayList getAllIncompleteTasksOfUser(Guid UserId)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    //Proceed action, to get values by user id and where status is zero.
                    NHibernate.IQuery query = session.CreateQuery("from Tasks where UserId = :userid and TaskStatus = 0");
                    query.SetParameter("userid", UserId);
                    ArrayList alstTask = new ArrayList();
                    foreach (var item in query.Enumerable())
                    {
                        alstTask.Add(item);
                    }
                    return alstTask;

                }//End Transaction
            }//End Session
        }



        /// <getTaskById>
        /// Get Task By Id
        /// </summary>
        /// <param name="Taskid">Task id.(string)</param>
        /// <param name="userId">User id.(Guid)</param>
        /// <returns>Return object of Tasks Class with  all Tasks info.(List<Tasks>)</returns>
        public Tasks getTaskById(string Taskid, Guid userId)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    //Proceed action, to get Account details by Fb user id and user id.
                    NHibernate.IQuery query = session.CreateQuery("from FacebookAccount where FbUserId = :Fbuserid and UserId=:userId");
                    query.SetParameter("Fbuserid", Taskid);
                    query.SetParameter("userId", userId);
                    Tasks result = (Tasks)query.UniqueResult();
                    return result;
                }//End Transaction
            }//End Session
        }


        /// <checkTaskExists>
        /// checkTaskExists
        /// </summary>
        /// <param name="TaskId">Task id.(String)</param>
        /// <param name="Userid">User id.(Guid)</param>
        /// <returns>True and False.(bool)</returns>
        public bool checkTaskExists(string TaskId, Guid Userid)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        //Proceed action, to get task details by userid and task id.
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

                }//End Transaction
            }//End Session
        }



        /// <updateTaskStatus>
        /// update Task Status
        /// </summary>
        /// <param name="TaskId">Task id.(guid)</param>
        /// <param name="UserId">User id.(Guid)</param>
        /// <param name="status">Status.(Bool)</param>
        public void updateTaskStatus(Guid TaskId, Guid UserId, bool status)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        //Proceed action, to get Task status and completion date by user id and task id.
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
                }//End Transaction
            }//End Session
        }



        /// <getAllTasksOfUserByStatus>
        /// Get all tasks of user by status.
        /// </summary>
        /// <param name="UserId">User id.(Guid)</param>
        /// <param name="status">Status.(bool)</param>
        /// <returns>Return values in the form of array list.(ArrayList)</returns>
        public ArrayList getAllTasksOfUserByStatus(Guid UserId, bool status)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    //Proceed action, to get Tasks by user id and task status.
                    NHibernate.IQuery query = session.CreateQuery("from Tasks where UserId = :userid and TaskStatus=:TaskStatus");
                    query.SetParameter("userid", UserId);
                    query.SetParameter("TaskStatus", status);
                    ArrayList alstTask = new ArrayList();

                    foreach (var item in query.Enumerable())
                    {
                        alstTask.Add(item);
                    }
                    return alstTask;

                }//End Transaction
            }//End Session
        }



        /// <getAllMyTasksOfUser>
        /// Get All My Tasks Of User
        /// </summary>
        /// <param name="UserId">User id.(Guid)</param>
        /// <param name="AssignTo">Assign to.(Guid)</param>
        /// <returns>Return values in the form of array list.(ArrayList)</returns>
        public ArrayList getAllMyTasksOfUser(Guid UserId, Guid AssignTo)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    //Proceed action, to get values by user id and assign task to.
                    NHibernate.IQuery query = session.CreateQuery("from Tasks where UserId = :userid and AssignTaskTo=:AssignTo");
                    query.SetParameter("userid", UserId);
                    query.SetParameter("AssignTo", AssignTo);
                    ArrayList alstTask = new ArrayList();

                    foreach (var item in query.Enumerable())
                    {
                        alstTask.Add(item);
                    }
                    return alstTask;

                }//End Transaction
            }//End Session
        }


        /// <getTasksByUserwithDetail>
        /// Get Tasks By User with Detail
        /// </summary>
        /// <param name="USerId">User id.(Guid)</param>
        /// <param name="days">Number of Days.(int)</param>
        /// <returns>Return values in the form of array list.(ArrayList)</returns>
        public ArrayList getTasksByUserwithDetail(Guid USerId, int days)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    //Proceed action, to get details of task.
                    var queryString = @"SELECT * FROM Tasks ts LEFT JOIN User u on ts.AssignTaskTo=u.Id where ts.UserId=:userid and AssignDate>=DATE_ADD(NOW(),INTERVAL -" + days + " DAY)";
                    var query = session.CreateSQLQuery(queryString)
                     .SetParameter("userid", USerId);

                    ArrayList alstTask = new ArrayList();

                    foreach (var item in query.List())
                    {
                        alstTask.Add(item);
                    }
                    return alstTask;

                }//End Transaction
            }//End Session
        }



        /// <GetTaskByUserIdAndYear>
        /// Get Task By User Id And Year
        /// </summary>
        /// <param name="Userid">User id.(Guid)</param>
        /// <returns>Return list of int which is contain years.(list<int>)</returns>
        public List<int> GetTaskByUserIdAndYear(Guid Userid)
        {
            List<int> lstShareCount = new List<int>();
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        //int year = DateTime.Now.Year;
                        //Get the List of perivious Years from now.
                        List<string> lstYear = GetLastYears();

                        for (int i = 0; i < 12; i++)
                        {
                            string year = lstYear[i];

                            //Proceed action, to get Task by year and user id.
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
                }//End Transaction
            }//End Session
        }


        /// <GetLastYears>
        /// Get Last Years
        /// </summary>
        /// <returns>List of years(List<string>)</returns>
        public List<string> GetLastYears()
        {
            List<string> arryear = new List<string>();

            try
            {
                //Get current year
                string currentYear = DateTime.Now.Year.ToString();

                bool flag = true;

                //get the previous 11 year 
                int fromyear = Convert.ToInt32(currentYear) - 11;
                while (flag)
                {
                    //add year in array list.
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

            //retur list
            return arryear;
        }


        /// <DeleteTasksByUserid>
        /// Delete Tasks By Userid
        /// </summary>
        /// <param name="userid">User id.(Guid)</param>
        /// <returns>Return 1 for true and 0 for false.(int)</returns>
        public int DeleteTasksByUserid(Guid userid)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        //Proceed action to delete task of user by user id.
                        NHibernate.IQuery query = session.CreateQuery("delete from Tasks where UserId = :userid")
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
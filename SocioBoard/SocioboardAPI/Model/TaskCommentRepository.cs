using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;
using NHibernate.Type;
using Domain.Socioboard.Domain;
using Api.Socioboard.Helper;

namespace Api.Socioboard.Services
{
    public class TaskCommentRepository : ITaskCommentRepository
    {

        /// <addTaskComment>
        /// Add Task Comment
        /// </summary>
        /// <param name="taskcomment">Set Values in a taskcomment Class Property and Pass the Object of taskcomment Class.(Domein.taskcomment)</param>
        public void addTaskComment(Domain.Socioboard.Domain.TaskComment taskcomment)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    //Proceed action, to Save data.
                    session.Save(taskcomment);
                    transaction.Commit();
                }//End Transaction
            }//End Session
        }
        

        /// <getAllTasksCommentOfUser>
        /// Get All Tasks Comment Of User
        /// </summary>
        /// <param name="UserId">User id.(Guid)</param>
        /// <param name="TaskId">Task id.(Guid)</param>
        /// <returns>Return Array list with value.(ArrayList)</returns>
        public ArrayList getAllTasksCommentOfUser( Guid TaskId)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    //Proceed action, to get Task comments by user id and task id. 
                    NHibernate.IQuery query = session.CreateQuery("from TaskComment where  TaskId=:taskid");
                 //   query.SetParameter("userid", UserId);
                    query.SetParameter("taskid", TaskId);
                    ArrayList alstTaskcomment = new ArrayList();

                    foreach (var item in query.Enumerable())
                    {
                        alstTaskcomment.Add(item);
                    }
                    return alstTaskcomment;

                }//End Transaction
            }//End Session
        }

        public List<Domain.Socioboard.Domain.TaskComment> getAllTasksCommentOfUser(Guid TaskId, Guid UserId)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    List<Domain.Socioboard.Domain.TaskComment> alst = session.CreateQuery("from TaskComment where  TaskId=:taskid and UserId=:userId")
                      .SetParameter("taskid", TaskId)
                      .SetParameter("userId", UserId)
                      .List<Domain.Socioboard.Domain.TaskComment>()
                      .ToList<Domain.Socioboard.Domain.TaskComment>();

                    //ArrayList alstTaskcomment = new ArrayList();
                    //foreach (var item in query.Enumerable())
                    //{
                    //    alstTaskcomment.Add(item);
                    //}




                    return alst;

                }//End Transaction
            }//End Session
        }
        /// <getAllTasksComent>
        /// Get All Tasks Coment
        /// </summary>
        /// <param name="UserId">User id.(Guid)</param>
        /// <param name="days">Number of days.(int)</param>
        /// <returns>Return Array list with value.(ArrayList)</returns>
        public ArrayList getAllTasksComent(Guid UserId, int days)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    //Proceed action, to get all task of user.
                    NHibernate.IQuery query = session.CreateSQLQuery("select * from TaskComment where UserId = :userid and CommentDate>=DATE_ADD(NOW(),INTERVAL -" + days + " DAY)");
                    query.SetParameter("userid", UserId);
                    ArrayList alstTaskcomment = new ArrayList();

                    foreach (var item in query.List())
                    {
                        alstTaskcomment.Add(item);
                    }
                    return alstTaskcomment;

                }//End Transaction
            }//End Session
        }
        

        /// <GetTaskCommentByUserIdAndYear>
        /// Get Task Comment By User Id And Year
        /// </summary>
        /// <param name="Userid">User id (Guid)</param>
        /// <returns>List of comment count according to year.(List<int>)</returns>
        public List<int> GetTaskCommentByUserIdAndYear(Guid Userid)
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

                        List<string> lstYear = GetLastYears();

                        for (int i = 0; i < 12; i++)
                        {
                            //Get the year from list.
                            string year = lstYear[i];
                            //Proceed action, to get the data according to year and user id. 
                            List<Domain.Socioboard.Domain.TaskComment> lstFacebookStats = session.CreateQuery("from TaskComment where CommentDate Like :year and UserId= :userId ")
                                .SetParameter("userId", Userid)
                                .SetParameter("year", "%" + year + "%", TypeFactory.GetAnsiStringType(15)).List<Domain.Socioboard.Domain.TaskComment>().ToList<Domain.Socioboard.Domain.TaskComment>();
                            //add in list
                            lstShareCount.Add(lstFacebookStats.Count);
                        }//End For loop
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
        /// Get the list of last 12 years.
        /// </summary>
        /// <returns>List of string which is containe Years.</returns>
        public List<string> GetLastYears()
        {
            List<string> arryear = new List<string>();

            try
            {
                //Get the current year from date time
                string currentYear = DateTime.Now.Year.ToString();
                //Set the flag 
                bool flag = true;
                //Get the previous 12 year 
                int fromyear = Convert.ToInt32(currentYear) - 11;
                while (flag)
                {
                    //add the year in list. 
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


        /// <DeleteTaskCommentByUserid>
        /// Delete all Task Comment By User id
        /// </summary>
        /// <param name="userid">User id (Guid)</param>
        /// <returns>Return 1 for true and 0 for false.(int)</returns>
        public int DeleteTaskCommentByUserid(Guid userid)
        {
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        //Proceed action, to Delete specific data by user id.
                        NHibernate.IQuery query = session.CreateQuery("delete from TaskComment where UserId = :userid")
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

        /// <summary>
        /// Delete TaskComment By TaskId.
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        public int DeleteTaskCommentByTaskId(Guid TaskId)
        {
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        //Proceed action, to Delete specific data by user id.
                        NHibernate.IQuery query = session.CreateQuery("delete from TaskComment where TaskId = :taskid")
                                        .SetParameter("taskid", TaskId);
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
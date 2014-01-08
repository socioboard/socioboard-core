using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SocioBoard.Domain;
using SocioBoard.Helper;
using System.Collections;
using NHibernate.Type;

namespace SocioBoard.Model
{
    public class TaskCommentRepository : ITaskCommentRepository
    {
        public void addTaskComment(TaskComment taskcomment)
        {
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    session.Save(taskcomment);
                    transaction.Commit();
                }
            }
        }

        public ArrayList getAllTasksCommentOfUser(Guid UserId, Guid TaskId)
        {

            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    NHibernate.IQuery query = session.CreateQuery("from TaskComment where UserId = :userid and TaskId=:taskid");
                    query.SetParameter("userid", UserId);
                    query.SetParameter("taskid", TaskId);
                    ArrayList alstTaskcomment = new ArrayList();

                    foreach (var item in query.Enumerable())
                    {
                        alstTaskcomment.Add(item);
                    }
                    return alstTaskcomment;

                }
            }

        }

        public ArrayList getAllTasksComent(Guid UserId,int days)
        {
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    NHibernate.IQuery query = session.CreateSQLQuery("select * from TaskComment where UserId = :userid and CommentDate>=DATE_ADD(NOW(),INTERVAL -" + days + " DAY)");
                    query.SetParameter("userid", UserId);
                    ArrayList alstTaskcomment = new ArrayList();

                    foreach (var item in query.List())
                    {
                        alstTaskcomment.Add(item);
                    }
                    return alstTaskcomment;

                }
            }
        }

        public List<int> GetTaskCommentByUserIdAndYear(Guid Userid)
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

                            List<TaskComment> lstFacebookStats = session.CreateQuery("from TaskComment where CommentDate Like :year and UserId= :userId ")
                                .SetParameter("userId", Userid)
                                .SetParameter("year", "%" + year + "%", TypeFactory.GetAnsiStringType(15)).List<TaskComment>().ToList<TaskComment>();

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
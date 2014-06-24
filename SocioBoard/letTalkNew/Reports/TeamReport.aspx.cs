using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SocioBoard.Model;
using SocioBoard.Domain;
using System.Collections;
using System.Reflection;

namespace letTalkNew.Reports
{
    public partial class TeamReport : System.Web.UI.Page
    {
        public string postGraphArr = string.Empty;
        public string replyGraphArr = string.Empty;
        public int commentCnt = 0;
        public int taskCount = 0;
        public int tcount = 0;
        public int taskPer = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["LoggedUser"] == null)
            {
                Response.Redirect("/Default.aspx");
                return;
            }
            if (!IsPostBack)
            {

                User user = (User)Session["LoggedUser"];

                if (user == null)
                    Response.Redirect("/Default.aspx");

                spanDate.InnerHtml = "from " + DateTime.Now.AddDays(-15).ToShortDateString() + " to " + DateTime.Now.ToShortDateString();
                spanTopDate.InnerHtml = "from " + DateTime.Now.AddDays(-15).ToShortDateString() + " to " + DateTime.Now.ToShortDateString();
                try
                {
                    getTaskDetail(15);
                }
                catch (Exception Err)
                {
                    Console.Write(Err.StackTrace);
                }

                try
                {
                    getAvgCount(15);
                }
                catch (Exception err)
                {
                    Console.Write(err.StackTrace);
                }

                postGraphArr = GetPostGraph();
                replyGraphArr = GetReplyGraph();
            }
        }


        public void getTaskDetail(int days)
        {
            try
            {
                User user = (User)Session["LoggedUser"];
                TaskRepository objTaskRepo = new TaskRepository();
                List<object> destination = new List<object>();
                ArrayList lstTask = objTaskRepo.getTasksByUserwithDetail(user.Id, days);
                string strTask = "<div class=\"task-labels\"><div class=\"task-labe-1\">TASK OWNER</div><div class=\"task-labe-2\">ASSIGNED</div>" +
                                 "<div class=\"task-labe-3\">TASK MESSAGE</div><div class=\"task-labe-4\">ASSIGN DATE</div><div class=\"task-labe-5\">COMPLETION DATE</div>" +
                                 "<div class=\"task-labe-6\">STATUS</div><div class=\"clear\"></div></div>";
                foreach (var item in lstTask)
                {
                    Array temp = (Array)item;
                    string taskStatus = string.Empty;
                    string completeDate = string.Empty;
                    if (temp.GetValue(4).Equals(false))
                    {
                        taskStatus = "Incomplete";
                    }
                    else
                    {
                        taskStatus = "Completed";
                        completeDate = temp.GetValue(6).ToString();
                    }
                    strTask = strTask + "<div class=\"task-header\"><div class=\"task-header-owner\"><div class=\"avathar-pub\"><img src=\"" + temp.GetValue(10) + "\" alt=\"\" /></div>" +
                                    "<div class=\"task-header-owner-name\">" + user.UserName + "</div><div class=\"clear\"></div></div><div class=\"assigned-lable\">" + temp.GetValue(8) + "</div><div class=\"assigned-lable\">" + temp.GetValue(1) + "</div><div class=\"task-text-3\">" + temp.GetValue(5) + "</div>" +
                                    "<div class=\"task-text-4\">" + completeDate + "</div><div class=\"task-text-5\">" + taskStatus + "</div><div class=\"clear\"></div></div>";

                    temp.GetValue(0);
                }
                taskCount = lstTask.Count;
                taskdiv.InnerHtml = strTask;
                divName.InnerHtml = user.UserName;

            }
            catch (Exception err)
            {
                Console.Write(err.StackTrace);
            }
        }
        public void getAvgCount(int days)
        {
            try
            {
                User user = (User)Session["LoggedUser"];
                TaskCommentRepository objTaskCmtRepo = new TaskCommentRepository();
                ArrayList lstcmtTask = objTaskCmtRepo.getAllTasksComent(user.Id, days);
                commentCnt = lstcmtTask.Count;
                tcount = commentCnt / taskCount;
                repliescount.InnerHtml = commentCnt + " REPLIES /" + taskCount + " TOTAL POSTS";
                taskPer = (commentCnt / taskCount) * 100;
            }
            catch (Exception err)
            {
                Console.Write(err.StackTrace);
            }
        }

        protected void btnfifteen_Click(object sender, EventArgs e)
        {

            spanDate.InnerHtml = "from " + DateTime.Now.AddDays(-15).ToShortDateString() + " to " + DateTime.Now.ToShortDateString();
            spanTopDate.InnerHtml = "from " + DateTime.Now.AddDays(-15).ToShortDateString() + " to " + DateTime.Now.ToShortDateString();
            try
            {
                getTaskDetail(15);
            }
            catch (Exception Err)
            {
                Console.Write(Err.StackTrace);
            }

            try
            {
                getAvgCount(15);
            }
            catch (Exception err)
            {
                Console.Write(err.StackTrace);
            }
        }

        protected void btnthirty_Click(object sender, EventArgs e)
        {

            spanDate.InnerHtml = "from " + DateTime.Now.AddDays(-30).ToShortDateString() + " to " + DateTime.Now.ToShortDateString();
            spanTopDate.InnerHtml = "from " + DateTime.Now.AddDays(-30).ToShortDateString() + " to " + DateTime.Now.ToShortDateString();
            try
            {
                getTaskDetail(30);
            }
            catch (Exception Err)
            {
                Console.Write(Err.StackTrace);
            }

            try
            {
                getAvgCount(30);
            }
            catch (Exception err)
            {
                Console.Write(err.StackTrace);
            }
        }

        protected void btnsixty_Click(object sender, EventArgs e)
        {

            spanDate.InnerHtml = "from " + DateTime.Now.AddDays(-60).ToShortDateString() + " to " + DateTime.Now.ToShortDateString();
            spanTopDate.InnerHtml = "from " + DateTime.Now.AddDays(-60).ToShortDateString() + " to " + DateTime.Now.ToShortDateString();
            try
            {
                getTaskDetail(60);
            }
            catch (Exception Err)
            {
                Console.Write(Err.StackTrace);
            }

            try
            {
                getAvgCount(60);
            }
            catch (Exception err)
            {
                Console.Write(err.StackTrace);
            }
        }

        protected void btnninty_Click(object sender, EventArgs e)
        {

            spanDate.InnerHtml = "from " + DateTime.Now.AddDays(-90).ToShortDateString() + " to " + DateTime.Now.ToShortDateString();
            spanTopDate.InnerHtml = "from " + DateTime.Now.AddDays(-90).ToShortDateString() + " to " + DateTime.Now.ToShortDateString();
            try
            {
                getTaskDetail(90);
            }
            catch (Exception Err)
            {
                Console.Write(Err.StackTrace);
            }

            try
            {
                getAvgCount(90);
            }
            catch (Exception err)
            {
                Console.Write(err.StackTrace);

            }
        }

        public string GetPostGraph()
        {
            string postGraphData = string.Empty;
            try
            {
                //--------Graph Data Format-----------------

                //[
                //   { x: new Date(2001, 0), y: 0 },
                //{ x: new Date(2002, 0), y: 0.001 },
                //{ x: new Date(2003, 0), y: 0.01 },
                //{ x: new Date(2004, 0), y: 0.05 },
                //{ x: new Date(2005, 0), y: 0.1 },
                //{ x: new Date(2006, 0), y: 0.98 },
                //{ x: new Date(2007, 0), y: 0.22 },
                //{ x: new Date(2008, 0), y: 0.38 },
                //{ x: new Date(2009, 0), y: 0.56 },
                //{ x: new Date(2010, 0), y: 0.98 },
                //{ x: new Date(2011, 0), y: 0.91 },
                //{ x: new Date(2012, 0), y: 0.94 }
                //]

                User user = (User)Session["LoggedUser"];

                TaskRepository objTaskRepository = new TaskRepository();

                List<string> lstLastYear=GetLastYears();

                List<int> lstShareCount = objTaskRepository.GetTaskByUserIdAndYear(user.Id);

                string shareGarphArray = string.Empty;

                for (int i = 0; i < 12; i++)
                {
                    try
                    {
                        if (i == 0)
                        {
                            shareGarphArray += "{ x: new Date(" + lstLastYear[i] + ", 0), y: 0." + lstShareCount[i] + " },";
                            continue;
                        }
                        if (i == 1)
                        {
                            shareGarphArray += "{ x: new Date(" + lstLastYear[i] + ", 0), y: 0." + lstShareCount[i] + " },";
                            continue;
                        }
                        if (i == 2)
                        {
                            shareGarphArray += "{ x: new Date(" + lstLastYear[i] + ", 0), y: 0." + lstShareCount[i] + " },";
                            continue;
                        }
                        if (i == 3)
                        {
                            shareGarphArray += "{ x: new Date(" + lstLastYear[i] + ", 0), y: 0." + lstShareCount[i] + " },";
                            continue;
                        }
                        if (i == 4)
                        {
                            shareGarphArray += "{ x: new Date(" + lstLastYear[i] + ", 0), y: 0." + lstShareCount[i] + " },";
                            continue;
                        }
                        if (i == 5)
                        {
                            shareGarphArray += "{ x: new Date(" + lstLastYear[i] + ", 0), y: 0." + lstShareCount[i] + " },";
                            continue;
                        }
                        if (i == 6)
                        {
                            shareGarphArray += "{ x: new Date(" + lstLastYear[i] + ", 0), y: 0." + lstShareCount[i] + " },";
                            continue;
                        }
                        if (i == 7)
                        {
                            shareGarphArray += "{ x: new Date(" + lstLastYear[i] + ", 0), y: 0." + lstShareCount[i] + " },";
                            continue;
                        }
                        if (i == 8)
                        {
                            shareGarphArray += "{ x: new Date(" + lstLastYear[i] + ", 0), y: 0." + lstShareCount[i] + " },";
                            continue;
                        }
                        if (i == 9)
                        {
                            shareGarphArray += "{ x: new Date(" + lstLastYear[i] + ", 0), y: 0." + lstShareCount[i] + " },";
                            continue;
                        }
                        if (i == 10)
                        {
                            shareGarphArray += "{ x: new Date(" + lstLastYear[i] + ", 0), y: 0." + lstShareCount[i] + " },";
                            continue;
                        }
                        if (i == 11)
                        {
                            shareGarphArray += "{ x: new Date(" + lstLastYear[i] + ", 0), y: 0." + lstShareCount[i] + " },";
                            continue;
                        }

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error : " + ex.StackTrace);
                    }
                }

                shareGarphArray = "[" + shareGarphArray.Substring(0, shareGarphArray.Length - 1) + "]";

                postGraphData = shareGarphArray;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error : " + ex.StackTrace);
            }
            return postGraphData;
        }

        public string GetReplyGraph()
        {
            string replyGraphData = string.Empty;
            try
            {
                //--------Graph Data Format-----------------

                //    [
                //    { x: new Date(2001, 00), y: 0.18 },
                //{ x: new Date(2002, 00), y: 0.2 },
                //{ x: new Date(2003, 0), y: 0.25 },
                //{ x: new Date(2004, 0), y: 0.35 },
                //{ x: new Date(2005, 0), y: 0.42 },
                //{ x: new Date(2006, 0), y: 0.2 },
                //{ x: new Date(2007, 0), y: 0.58 },
                //{ x: new Date(2008, 0), y: 0.67 },
                //{ x: new Date(2009, 0), y: 0.78 },
                //{ x: new Date(2010, 0), y: 0.88 },
                //{ x: new Date(2011, 0), y: 0.98 },
                //{ x: new Date(2012, 0), y: 0.04 },
                //{ x: new Date(2012, 0), y: 0.30 }


                //]

                User user = (User)Session["LoggedUser"];

                TaskCommentRepository objTaskCommentRepository = new TaskCommentRepository();

                List<string> lstLastYear = GetLastYears();

                List<int> lstShareCount = objTaskCommentRepository.GetTaskCommentByUserIdAndYear(user.Id);

                string shareGarphArray = string.Empty;

                for (int i = 0; i < 12; i++)
                {
                    try
                    {
                        if (i == 0)
                        {
                            shareGarphArray += "{ x: new Date(" + lstLastYear[i] + ", 0), y: 0." + lstShareCount[i] + " },";
                            continue;
                        }
                        if (i == 1)
                        {
                            shareGarphArray += "{ x: new Date(" + lstLastYear[i] + ", 0), y: 0." + lstShareCount[i] + " },";
                            continue;
                        }
                        if (i == 2)
                        {
                            shareGarphArray += "{ x: new Date(" + lstLastYear[i] + ", 0), y: 0." + lstShareCount[i] + " },";
                            continue;
                        }
                        if (i == 3)
                        {
                            shareGarphArray += "{ x: new Date(" + lstLastYear[i] + ", 0), y: 0." + lstShareCount[i] + " },";
                            continue;
                        }
                        if (i == 4)
                        {
                            shareGarphArray += "{ x: new Date(" + lstLastYear[i] + ", 0), y: 0." + lstShareCount[i] + " },";
                            continue;
                        }
                        if (i == 5)
                        {
                            shareGarphArray += "{ x: new Date(" + lstLastYear[i] + ", 0), y: 0." + lstShareCount[i] + " },";
                            continue;
                        }
                        if (i == 6)
                        {
                            shareGarphArray += "{ x: new Date(" + lstLastYear[i] + ", 0), y: 0." + lstShareCount[i] + " },";
                            continue;
                        }
                        if (i == 7)
                        {
                            shareGarphArray += "{ x: new Date(" + lstLastYear[i] + ", 0), y: 0." + lstShareCount[i] + " },";
                            continue;
                        }
                        if (i == 8)
                        {
                            shareGarphArray += "{ x: new Date(" + lstLastYear[i] + ", 0), y: 0." + lstShareCount[i] + " },";
                            continue;
                        }
                        if (i == 9)
                        {
                            shareGarphArray += "{ x: new Date(" + lstLastYear[i] + ", 0), y: 0." + lstShareCount[i] + " },";
                            continue;
                        }
                        if (i == 10)
                        {
                            shareGarphArray += "{ x: new Date(" + lstLastYear[i] + ", 0), y: 0." + lstShareCount[i] + " },";
                            continue;
                        }
                        if (i == 11)
                        {
                            shareGarphArray += "{ x: new Date(" + lstLastYear[i] + ", 0), y: 0." + lstShareCount[i] + " },";
                            shareGarphArray += "{ x: new Date(" + lstLastYear[i] + ", 0), y: 0." + lstShareCount[i] + " },";
                            continue;
                        }

                        
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error : " + ex.StackTrace);
                    }
                }

                shareGarphArray = "[" + shareGarphArray.Substring(0, shareGarphArray.Length - 1) + "]";

                replyGraphData = shareGarphArray;
            }

            catch (Exception ex)
            {
                Console.WriteLine("Error : " + ex.StackTrace);
            }
            return replyGraphData;
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
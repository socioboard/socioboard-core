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

namespace SocialSuitePro.Reports
{
    public partial class TeamReport : System.Web.UI.Page
    {
        public int commentCnt = 0;
        public int taskCount = 0;
        public int tcount = 0;
        public int taskPer = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                User user = (User)Session["LoggedUser"];

                if (user == null)
                    Response.Redirect("/Default.aspx");

               spanDate.InnerHtml="from " + DateTime.Now.AddDays(-15).ToShortDateString() + " to " + DateTime.Now.ToShortDateString();
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
        }

        public void getTaskDetail(int days)
        {
            try
            {
                User user = (User)Session["LoggedUser"];
                TaskRepository objTaskRepo = new TaskRepository();
                List<object> destination = new List<object>();
                ArrayList lstTask = objTaskRepo.getTasksByUserwithDetail(user.Id, days);
                string strTask = "<div class=\"task-labels\"><div class=\"task-labe-1\">TASK OWNER</div><div class=\"task-labe-2\">ASSIGNED</div>"+
                                 "<div class=\"task-labe-3\">TASK MESSAGE</div><div class=\"task-labe-4\">ASSIGN DATE</div><div class=\"task-labe-5\">COMPLETION DATE</div>"+
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
    }
}
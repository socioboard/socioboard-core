using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SocioBoard.Model;
using SocioBoard.Helper;
using System.Data;
using SocioBoard.Domain;
using System.Collections;
using log4net;

namespace SocioBoard.Message
{
    public partial class Task : System.Web.UI.Page
    {
        ILog logger = LogManager.GetLogger(typeof(Task));

        TaskRepository taskrepo = new TaskRepository();
        public static Guid custid = Guid.Empty;
        static string imgpath = string.Empty;
        ArrayList taskdata = new ArrayList();
        string TaskStatus = string.Empty;

        static string custname = string.Empty;
        UserRepository objUserRepository = new UserRepository();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                SocioBoard.Domain.User user = (User)Session["LoggedUser"];

              
                #region for You can use only 30 days as Unpaid User

                if (user.PaymentStatus.ToLower() == "unpaid")
                {
                    if (!SBUtils.IsUserWorkingDaysValid(user.ExpiryDate))
                    {
                        // ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('You can use only 30 days as Unpaid User !');", true);

                        Session["GreaterThan30Days"] = "GreaterThan30Days";

                        Response.Redirect("/Settings/Billing.aspx");
                    }
                }

                Session["GreaterThan30Days"] = null;
                #endregion




               btnchangestatus.Attributes.Add("onclick", "return checkStatusInfo();");  


                User loginInfoEmail = (User)Session["LoggedUser"];
                if (Session["LoggedUser"] != null)
                {
                    custid = loginInfoEmail.Id;
                    blackcount.InnerHtml = Convert.ToString((int)Session["CountMessages"]);
                    custname = loginInfoEmail.UserName;
                }
                else
                {
                    Response.Redirect("/Default.aspx", false);
                }

                if (!IsPostBack)
                {
                   this.rdbtnmytask_CheckedChanged(sender, e);
                }

            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
            }
        }


        protected void bindTeamTask()
        {
            string path = "";
            string strbind = string.Empty;
            taskdiv.InnerHtml = "";
            int i = 0;
            int taskid = 0;
            string preaddedcomment = "";
            SocioBoard.Domain.Team team = (SocioBoard.Domain.Team)Session["GroupName"];
            TaskCommentRepository objTaskCmtRepo = new TaskCommentRepository();
            taskdata = taskrepo.getAllTasksOfUser(custid,team.GroupId);
            foreach (Tasks item in taskdata)
            {
                if (item.TaskStatus == true)
                {
                    TaskStatus = "Completed";

                }
                else
                {
                    TaskStatus = "Pending";
                }

                imgpath = path;
                i++;
                strbind += "<section class=\"section\" id=\"Section" + item.Id + "\"><div class=\"js-task-cont read\">"+
                            "<span id=\"taskcomment\" class=\"ficon task_active\">" +
                                      "<img  onclick=\"getmemberdata('" + item.Id + "');\" src=\"../Contents/img/task_pin.png\" width=\"14\" height=\"17\" alt=\"\" /></span>" +
                            "<section class=\"task-activity third\"><p>Name</p><div>" + item.AssignDate + "</div><input type=\"hidden\" id=\"hdntaskid_" + i + "\" value=" + item.Id + " />" +
                                  "<p>Assigned by Name</p></section>" +
                            "<section class=\"task-owner\">" +
                                   "<img width=\"32\" height=\"32\" border=\"0\" class=\"avatar\" src=" + path + " />" +
                                   "</section><section class=\"task-message font-13 third\"><a class=\"tip_left\">" + item.TaskMessage + "</a>" +
                                  "</section><section class=\"task-status\"><div class=\"ui_light floating task_status_change\"><a class=\"ui-sproutmenu\" href=\"#nogo\">" +
                                     "<b>" + TaskStatus + "</b><span class=\"ui-sproutmenu-status\">" +
                                      "<img id=\"img_" + item.Id + "_" + item.TaskStatus + "\" class=\"edit_button\" src=\"../Contents/img/icon_edit.png\" onclick=\"PerformClick(this.id)\" title=\"Edit Status\" />" +
                                      "</span></a></div></section></div>" +
                                      "</section>";

                ArrayList pretask = objTaskCmtRepo.getAllTasksCommentOfUser(item.Id);
                if (pretask != null)
                {
                    preaddedcomment += "<div id=" + item.Id + " style=\"display:none\" >";
                    foreach (TaskComment items in pretask)
                    {
                        preaddedcomment += "<div id=\"task_comment_" + item.Id + "_" + items.Id + "\" class=\"assign_comments\" >" +
                                        "<section><article class=\"task_assign\">" +
                                        "<img src=" + imgpath + " width=\"30\" height=\"30\" alt=\"\"  />  " +
                                            "<article><input id=\"hdncommentsid\" type=\"hidden\" value=" + items.Id + " /><p class=\"msg_article\">" + items.Comment + "</p>" +
                                                "<aside class=\"days_ago\">By " + custname + " at " + items.CommentDate + "</aside>" +
                                            "</article></article></section></div>";
                    }

                    preaddedcomment += "</div>";
                }


            }

            if (string.IsNullOrEmpty(strbind))
            {
                strbind = "Sorry no data !";
            }
            taskdiv.InnerHtml = strbind;
            prevComments.InnerHtml = preaddedcomment;

        }

        protected void rdbtnteamtask_CheckedChanged(object sender, EventArgs e)
        {
          
            User user = (User)Session["LoggedUser"];
            SocioBoard.Domain.Team team = (SocioBoard.Domain.Team)Session["GroupName"];

            if (chkincomplete.Checked == true)
            {
                chkincomplete.Checked = false;
            }
            if (CheckBox1.Checked == true)
            {
                CheckBox1.Checked = false;
            }
            if (rdbtnmytask.Checked == true)
            {
                rdbtnteamtask.Checked = false;
            }

            string path = "";
            string strbind = string.Empty;
            taskdiv.InnerHtml = "";
            int i = 0;
            string preaddedcomment = "";

            if (string.IsNullOrEmpty(user.ProfileUrl))
            {
                path = "../Contents/img/blank_img.png";
            }
            else
            {
                path = user.ProfileUrl;
            }

            TaskCommentRepository objTaskCmtRepo = new TaskCommentRepository();
            TeamRepository objTeam = new TeamRepository();
            taskdata = taskrepo.getAllTasksOfUser(custid,team.GroupId);
            foreach (Tasks item in taskdata)
            {
                if (item.TaskStatus == true)
                {
                    TaskStatus = "Completed";

                }
                else
                {
                    TaskStatus = "Pending";
                }

                string strAssignedTo = string.Empty;
                User objdetails = objUserRepository.getUsersById(item.AssignTaskTo);                            
                strAssignedTo = objdetails.UserName;
             
                imgpath = path;
                i++;

                if (user.Id == item.UserId)
                {
                    strbind += "<section class=\"section\" id=\"Section" + item.Id + "\"><div class=\"js-task-cont read\">" +
                                "<span id=\"taskcomment\" class=\"ficon task_active\">" +
                                      "<img  onclick=\"getmemberdata('" + item.Id + "');\" src=\"../Contents/img/task_pin.png\" width=\"14\" height=\"17\" alt=\"\" /></span>" +
                               "<section class=\"task-activity third\"><p>" + strAssignedTo + "</p><div>" + item.AssignDate + "</div><input type=\"hidden\" id=\"hdntaskid_" + i + "\" value=" + item.Id + " />" +
                                      "<p>Assigned by " + custname + "</p></section>" +
                                      "<section class=\"task-owner\">" +
                                       "<img width=\"32\" height=\"32\" border=\"0\" class=\"avatar\" src=" + path + " />" +
                                       "</section><section class=\"task-message font-13 third\"><a class=\"tip_left\">" + item.TaskMessage + "</a>" +
                                      "</section><section class=\"task-status\"><div class=\"ui_light floating task_status_change\"><a class=\"ui-sproutmenu\" href=\"#nogo\">" +
                                          "<b>" + TaskStatus + "</b><span class=\"ui-sproutmenu-status\">" +
                        // user.Id.ToString()==item.UserId.ToString()?"<img id=\"img_" + item.Id + "_" + item.TaskStatus + "\" class=\"edit_button\" src=\"../Contents/img/icon_edit.png\" onclick=\"PerformClick(this.id)\" title=\"Edit Status\" />":""+
                                          "<img id=\"img_" + item.Id + "_" + item.TaskStatus + "\" class=\"edit_button\" src=\"../Contents/img/icon_edit.png\" onclick=\"PerformClick(this.id)\" title=\"Edit Status\" />" +
                                          "</span></a></div></section></div>" +
                                          "</section>";
                }

                else
                {
                    strbind += "<section class=\"section\" id=\"Section" + item.Id + "\"><div class=\"js-task-cont read\">" +
                             "<span id=\"taskcomment\" class=\"ficon task_active\">" +
                                   "<img  onclick=\"getmemberdata('" + item.Id + "');\" src=\"../Contents/img/task_pin.png\" width=\"14\" height=\"17\" alt=\"\" /></span>" +
                            "<section class=\"task-activity third\"><p>" + strAssignedTo + "</p><div>" + item.AssignDate + "</div><input type=\"hidden\" id=\"hdntaskid_" + i + "\" value=" + item.Id + " />" +
                                   "<p>Assigned by " + custname + "</p></section>" +
                                   "<section class=\"task-owner\">" +
                                    "<img width=\"32\" height=\"32\" border=\"0\" class=\"avatar\" src=" + path + " />" +
                                    "</section><section class=\"task-message font-13 third\"><a class=\"tip_left\">" + item.TaskMessage + "</a>" +
                                   "</section><section class=\"task-status\"><div class=\"ui_light floating task_status_change\"><a class=\"ui-sproutmenu\" href=\"#nogo\">" +
                                       "<b>" + TaskStatus + "</b><span class=\"ui-sproutmenu-status\">" +                                                   
                                       "</span></a></div></section></div>" +
                                       "</section>";
                }


                ArrayList pretask = objTaskCmtRepo.getAllTasksCommentOfUser(item.Id);
                if (pretask != null)
                {
                    preaddedcomment += "<div id=" + item.Id + " style=\"display:none\" >";
                    foreach (TaskComment items in pretask)
                    {
                        User details = objUserRepository.getUsersById(items.UserId);
                        string strAssigned = details.UserName;

                        preaddedcomment += "<div id=\"task_comment_" + item.Id + "_" + items.Id + "\" class=\"assign_comments\" >" +
                                        "<section><article class=\"task_assign\">" +
                                        "<img src=" + imgpath + " width=\"30\" height=\"30\" alt=\"\"  />  " +
                                            "<article><input id=\"hdncommentsid\" type=\"hidden\" value=" + items.Id + " /><p class=\"msg_article\">" + items.Comment + "</p>" +
                                                "<aside class=\"days_ago\"> By " + strAssigned + " at " + items.CommentDate + "</aside>" +
                                            "</article></article></section></div>";
                    }

                    preaddedcomment += "</div>";
                }


            }

            if (string.IsNullOrEmpty(strbind))
            {
                strbind = "Sorry no data !";
            }

            taskdiv.InnerHtml = strbind;
            prevComments.InnerHtml = preaddedcomment;
        }

        protected void rdbtnmytask_CheckedChanged(object sender, EventArgs e)
        {

            User user = (User)Session["LoggedUser"];
            SocioBoard.Domain.Team team = (SocioBoard.Domain.Team)Session["GroupName"];
            string imagepathofuser = string.Empty;
          //  string path = "";
            string strbind = string.Empty;
            taskdiv.InnerHtml = "";
            int i = 0;
          //  int taskid = 0;
            string preaddedcomment = "";
            if (chkincomplete.Checked == true)
            {
                chkincomplete.Checked = false;
            }
            if (CheckBox1.Checked == true)
            {
                CheckBox1.Checked = false;
            }
            if (rdbtnteamtask.Checked == true)
            {
                rdbtnteamtask.Checked = false;
            }
           
            if (string.IsNullOrEmpty(user.ProfileUrl))
            {
                imagepathofuser = "../Contents/img/blank_img.png";
            }
            else
            {
                imagepathofuser = user.ProfileUrl;
            }


            
            taskdata = taskrepo.getAllMyTasksOfUser(custid,team.GroupId);
            TaskCommentRepository objTaskCmtRepo = new TaskCommentRepository();
            TeamRepository objTeam = new TeamRepository();
            foreach (Tasks item in taskdata)
            {
                if (item.TaskStatus == true)
                {
                    TaskStatus = "Completed";

                }
                else
                {
                    TaskStatus = "Pending";
                }
                  
                  
                try
                {
                    User objdetails = objUserRepository.getUsersById(item.UserId);
                     string strAssignedby = objdetails.UserName;                
                    i++;
                    if (user.Id == objdetails.Id)
                    {
                        strbind += "<section class=\"section\" id=\"Section" + item.Id + "\"><div class=\"js-task-cont read\">" +
                                "<span id=\"taskcomment\" class=\"ficon task_active\">" +
                                          "<img  onclick=\"getmemberdata('" + item.Id + "');\" src=\"../Contents/img/task_pin.png\" width=\"14\" height=\"17\" alt=\"\" /></span>" +
                                "<section class=\"task-activity third\"><p>" + custname + "</p><div>" + item.AssignDate + "</div><input type=\"hidden\" id=\"hdntaskid_" + i + "\" value=" + item.Id + " />" +
                                      "<p>Assigned by " + strAssignedby + "</p></section>" +
                                "<section class=\"task-owner\">" +
                                       "<img width=\"32\" height=\"32\" border=\"0\" class=\"avatar\" src=\"" + imagepathofuser + "\" />" +
                                       "</section><section class=\"task-message font-13 third\"><a class=\"tip_left\">" + item.TaskMessage + "</a>" +
                                      "</section><section class=\"task-status\"><div class=\"ui_light floating task_status_change\"><a class=\"ui-sproutmenu\" href=\"#nogo\">" +
                                          "<b>" + TaskStatus + "</b><span class=\"ui-sproutmenu-status\">" +
                                          "<img id=\"img_" + item.Id + "_" + item.TaskStatus + "\" class=\"edit_button\" src=\"../Contents/img/icon_edit.png\" onclick=\"PerformClick(this.id)\" title=\"Edit Status\" />" +
                                          "</span></a></div></section></div>" +
                                          "</section>";
                    }
                    else
                    {
                        strbind += "<section class=\"section\" id=\"Section" + item.Id + "\"><div class=\"js-task-cont read\">" +
                             "<span id=\"taskcomment\" class=\"ficon task_active\">" +
                                       "<img  onclick=\"getmemberdata('" + item.Id + "');\" src=\"../Contents/img/task_pin.png\" width=\"14\" height=\"17\" alt=\"\" /></span>" +
                             "<section class=\"task-activity third\"><p>" + custname + "</p><div>" + item.AssignDate + "</div><input type=\"hidden\" id=\"hdntaskid_" + i + "\" value=" + item.Id + " />" +
                                   "<p>Assigned by " + strAssignedby + "</p></section>" +
                             "<section class=\"task-owner\">" +
                                    "<img width=\"32\" height=\"32\" border=\"0\" class=\"avatar\" src=\"" + imagepathofuser + "\" />" +
                                    "</section><section class=\"task-message font-13 third\"><a class=\"tip_left\">" + item.TaskMessage + "</a>" +
                                   "</section><section class=\"task-status\"><div class=\"ui_light floating task_status_change\"><a class=\"ui-sproutmenu\" href=\"#nogo\">" +
                                       "<b>" + TaskStatus + "</b><span class=\"ui-sproutmenu-status\">" +
                                
                                       "</span></a></div></section></div>" +
                                       "</section>";
                    }
                    ArrayList pretask = objTaskCmtRepo.getAllTasksCommentOfUser(item.Id);
                    if (pretask != null)
                    {
                        preaddedcomment += "<div id=" + item.Id + " style=\"display:none\" >";
                        foreach (TaskComment items in pretask)
                        {
                            User details = objUserRepository.getUsersById(items.UserId);
                            string strAssigned = details.UserName;



                            preaddedcomment += "<div id=\"task_comment_" + item.Id + "_" + items.Id + "\" class=\"assign_comments\" >" +
                                            "<section><article class=\"task_assign\">" +
                                            "<img src=" + imgpath + " width=\"30\" height=\"30\" alt=\"\"  />  " +
                                                "<article><input id=\"hdncommentsid\" type=\"hidden\" value=" + items.Id + " /><p class=\"msg_article\">" + items.Comment + "</p>" +
                                                    "<aside class=\"days_ago\"> By " + strAssigned + " at " + items.CommentDate + "</aside>" +
                                                "</article></article></section></div>";
                        }

                        preaddedcomment += "</div>";
                    }

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    logger.Error(ex.Message);
                }

            }

            if (string.IsNullOrEmpty(strbind))
            {
                strbind = "Sorry no data !";
            }
            taskdiv.InnerHtml = strbind;
            prevComments.InnerHtml = preaddedcomment;

        }
   

        protected void chkincomplete_CheckedChanged(object sender, EventArgs e)
        {
            User user = (User)Session["LoggedUser"];
            SocioBoard.Domain.Team team = (SocioBoard.Domain.Team)Session["GroupName"];
            string path = "";
            string strbind = string.Empty;
            taskdiv.InnerHtml = "";
            int i = 0;
            int taskid = 0;
            string preaddedcomment = "";
            if (chkincomplete.Checked)
            {
                if (chkincomplete.Checked == true)
                {
                    chkincomplete.Checked = true;
                    CheckBox1.Checked = false;
                }

                if (rdbtnmytask.Checked == true)
                {
                    rdbtnmytask.Checked = false;
                }
                if (rdbtnteamtask.Checked == true)
                {
                    rdbtnteamtask.Checked = false;
                }


                if (string.IsNullOrEmpty(user.ProfileUrl))
                {
                    path = "../Contents/img/blank_img.png";
                }
                else
                {
                    path = user.ProfileUrl;
                }

                taskdata = taskrepo.getAllIncompleteTasksOfUser(custid,team.GroupId);
                foreach (Tasks item in taskrepo.getAllIncompleteTasksOfUsers(custid))
                {
                    taskdata.Add(item);
                }
                   

                TaskCommentRepository objTaskCmtRepo = new TaskCommentRepository();
                TeamRepository objTeam = new TeamRepository();
                foreach (Tasks item in taskdata)
                {
                    if (item.TaskStatus == true)
                    {
                        TaskStatus = "Completed";

                    }
                    else
                    {
                        TaskStatus = "Pending";
                    }



                    User objdetails = objUserRepository.getUsersById(item.AssignTaskTo);
                    string strAssignedTo = objdetails.UserName;
                    User objdetail = objUserRepository.getUsersById(item.UserId);
                    string strAssignedBy = objdetail.UserName;
                    imgpath = path;
                    i++;


                    if (user.Id == objdetail.Id)
                    {
                        strbind += "<section class=\"section\" id=\"Section" + item.Id + "\"><div class=\"js-task-cont read\">" +
                                    "<span id=\"taskcomment\" class=\"ficon task_active\">" +
                                              "<img  onclick=\"getmemberdata('" + item.Id + "');\" src=\"../Contents/img/task_pin.png\" width=\"14\" height=\"17\" alt=\"\" /></span>" +
                                   "<section class=\"task-activity third\"><p>" + strAssignedTo + "</p><div>" + item.AssignDate + "</div><input type=\"hidden\" id=\"hdntaskid_" + i + "\" value=" + item.Id + " />" +
                                          "<p>Assigned by " + strAssignedBy + "</p></section>" +
                                   "<section class=\"task-owner\">" +
                                           "<img width=\"32\" height=\"32\" border=\"0\" class=\"avatar\" src=" + path + " />" +
                                           "</section><section class=\"task-message font-13 third\"><a class=\"tip_left\">" + item.TaskMessage + "</a>" +
                                          "</section><section class=\"task-status\"><div class=\"ui_light floating task_status_change\"><a class=\"ui-sproutmenu\" href=\"#nogo\">" +
                                            "<b>" + TaskStatus + "</b><span class=\"ui-sproutmenu-status\">" +
                                              "<img id=\"img_" + item.Id + "_" + item.TaskStatus + "\" class=\"edit_button\" src=\"../Contents/img/icon_edit.png\" onclick=\"PerformClick(this.id)\" title=\"Edit Status\" />" +
                                              "</span></a></div></section></div>" +
                                              "</section>";
                    }
                    else
                    {
                        strbind += "<section class=\"section\" id=\"Section" + item.Id + "\"><div class=\"js-task-cont read\">" +
                                      "<span id=\"taskcomment\" class=\"ficon task_active\">" +
                                                "<img  onclick=\"getmemberdata('" + item.Id + "');\" src=\"../Contents/img/task_pin.png\" width=\"14\" height=\"17\" alt=\"\" /></span>" +
                                     "<section class=\"task-activity third\"><p>" + strAssignedTo + "</p><div>" + item.AssignDate + "</div><input type=\"hidden\" id=\"hdntaskid_" + i + "\" value=" + item.Id + " />" +
                                            "<p>Assigned by " + strAssignedBy + "</p></section>" +
                                     "<section class=\"task-owner\">" +
                                             "<img width=\"32\" height=\"32\" border=\"0\" class=\"avatar\" src=" + path + " />" +
                                             "</section><section class=\"task-message font-13 third\"><a class=\"tip_left\">" + item.TaskMessage + "</a>" +
                                            "</section><section class=\"task-status\"><div class=\"ui_light floating task_status_change\"><a class=\"ui-sproutmenu\" href=\"#nogo\">" +
                                              "<b>" + TaskStatus + "</b><span class=\"ui-sproutmenu-status\">" +                            
                                                "</span></a></div></section></div>" +
                                                "</section>";
                    }

                    ArrayList pretask = objTaskCmtRepo.getAllTasksCommentOfUser(item.Id);
                    if (pretask != null)
                    {
                        preaddedcomment += "<div id=" + item.Id + " style=\"display:none\" >";
                        foreach (TaskComment items in pretask)
                        {

                            User details = objUserRepository.getUsersById(items.UserId);
                            string strAssigned = details.UserName;

                            preaddedcomment += "<div id=\"task_comment_" + item.Id + "_" + items.Id + "\" class=\"assign_comments\" >" +
                                            "<section><article class=\"task_assign\">" +
                                            "<img src=" + imgpath + " width=\"30\" height=\"30\" alt=\"\"  />  " +
                                                "<article><input id=\"hdncommentsid\" type=\"hidden\" value=" + items.Id + " /><p class=\"msg_article\">" + items.Comment + "</p>" +
                                                    "<aside class=\"days_ago\"> By " + strAssigned + " at  " + items.CommentDate + "</aside>" +
                                                "</article></article></section></div>";
                        }

                        preaddedcomment += "</div>";
                    }


                }

                if (string.IsNullOrEmpty(strbind))
                {
                    strbind = "Sorry no data !";
                }

                taskdiv.InnerHtml = strbind;
                prevComments.InnerHtml = preaddedcomment;

            }
            else
            {
                try
                {
                    bindTeamTask();
                }
                catch (Exception ex)
                {

                    logger.Error(ex.Message);
                }
            }
        }

        protected void CheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            User user = (User)Session["LoggedUser"];
            SocioBoard.Domain.Team team = (SocioBoard.Domain.Team)Session["GroupName"];
            if (CheckBox1.Checked)
            {
                if (chkincomplete.Checked == true)
                {
                    chkincomplete.Checked = false;
                    CheckBox1.Checked = true;
                }

                if (rdbtnmytask.Checked == true)
                    rdbtnmytask.Checked = false;

                if (rdbtnteamtask.Checked == true)
                    rdbtnteamtask.Checked = false;

              
                string path = "";
                string strbind = string.Empty;
                taskdiv.InnerHtml = "";
                int i = 0;
                string preaddedcomment = "";

                if (string.IsNullOrEmpty(user.ProfileUrl))
                {
                    path = "../Contents/img/blank_img.png";
                }
                else
                {
                    path = user.ProfileUrl;
                }

                //taskdata = taskrepo.getAllCompleteTasksOfUser(custid, custid);
                taskdata = taskrepo.getAllCompleteTasksOfUser(custid, team.GroupId,custid);
                //foreach (Tasks item in taskrepo.getAllCompleteTasksOfUsers(custid))
                //{
                //    taskdata.Add(item);
                //}
                foreach (Tasks item in taskrepo.getAllCompleteTasksOfUsers(custid,team.GroupId))
                {
                    taskdata.Add(item);
                }
                TaskCommentRepository objTaskCmtRepo = new TaskCommentRepository();
                TeamRepository objTeam = new TeamRepository();
                foreach (Tasks item in taskdata)
                {
                    if (item.TaskStatus == true)
                    {
                        TaskStatus = "Completed";

                    }
                    else
                    {
                        TaskStatus = "Pending";
                    }



                    User objdetails = objUserRepository.getUsersById(item.AssignTaskTo);
                    string strAssignedTo = objdetails.UserName;
                    User objdetail = objUserRepository.getUsersById(item.UserId);
                    string strAssignedBy = objdetail.UserName;
                    imgpath = path;
                    i++;

                    if (user.Id == objdetail.Id)
                    {
                        strbind += "<section class=\"section\" id=\"Section" + item.Id + "\"><div class=\"js-task-cont read\">" +
                                    "<span id=\"taskcomment\" class=\"ficon task_active\">" +
                                              "<img  onclick=\"getmemberdata('" + item.Id + "');\" src=\"../Contents/img/task_pin.png\" width=\"14\" height=\"17\" alt=\"\" /></span>" +
                                   "<section class=\"task-activity third\"><p>" + strAssignedTo + "</p><div>" + item.AssignDate + "</div><input type=\"hidden\" id=\"hdntaskid_" + i + "\" value=" + item.Id + " />" +
                                          "<p>Assigned by " + strAssignedBy + "</p></section>" +
                                    "<section class=\"task-owner\">" +
                                           "<img width=\"32\" height=\"32\" border=\"0\" class=\"avatar\" src=" + path + " />" +
                                           "</section><section class=\"task-message font-13 third\"><a class=\"tip_left\">" + item.TaskMessage + "</a>" +
                                          "</section><section class=\"task-status\"><div class=\"ui_light floating task_status_change\"><a class=\"ui-sproutmenu\" href=\"#nogo\">" +
                                            "<b>" + TaskStatus + "</b><span class=\"ui-sproutmenu-status\">" +
                                              "<img id=\"img_" + item.Id + "_" + item.TaskStatus + "\" class=\"edit_button\" src=\"../Contents/img/icon_edit.png\" onclick=\"PerformClick(this.id)\" title=\"Edit Status\" />" +
                                              "</span></a></div></section></div>" +
                                              "</section>";
                    }

                    else
                    {
                        strbind += "<section class=\"section\" id=\"Section" + item.Id + "\"><div class=\"js-task-cont read\">" +
                                    "<span id=\"taskcomment\" class=\"ficon task_active\">" +
                                              "<img  onclick=\"getmemberdata('" + item.Id + "');\" src=\"../Contents/img/task_pin.png\" width=\"14\" height=\"17\" alt=\"\" /></span>" +
                                   "<section class=\"task-activity third\"><p>" + strAssignedTo + "</p><div>" + item.AssignDate + "</div><input type=\"hidden\" id=\"hdntaskid_" + i + "\" value=" + item.Id + " />" +
                                          "<p>Assigned by " + strAssignedBy + "</p></section>" +
                                    "<section class=\"task-owner\">" +
                                           "<img width=\"32\" height=\"32\" border=\"0\" class=\"avatar\" src=" + path + " />" +
                                           "</section><section class=\"task-message font-13 third\"><a class=\"tip_left\">" + item.TaskMessage + "</a>" +
                                          "</section><section class=\"task-status\"><div class=\"ui_light floating task_status_change\"><a class=\"ui-sproutmenu\" href=\"#nogo\">" +
                                            "<b>" + TaskStatus + "</b><span class=\"ui-sproutmenu-status\">" +                            
                                              "</span></a></div></section></div>" +
                                              "</section>";
                    }


                    ArrayList pretask = objTaskCmtRepo.getAllTasksCommentOfUser(item.Id);
                    if (pretask != null)
                    {
                        preaddedcomment += "<div id=" + item.Id + " style=\"display:none\" >";
                        foreach (TaskComment items in pretask)
                        {
                            User details = objUserRepository.getUsersById(items.UserId);
                            string strAssigned = details.UserName;

                            
                            preaddedcomment += "<div id=\"task_comment_" + item.Id + "_" + items.Id + "\" class=\"assign_comments\" >" +
                                            "<section><article class=\"task_assign\">" +
                                            "<img src=" + imgpath + " width=\"30\" height=\"30\" alt=\"\"  />  " +
                                                "<article><input id=\"hdncommentsid\" type=\"hidden\" value=" + items.Id + " /><p class=\"msg_article\">" + items.Comment + "</p>" +
                                                    "<aside class=\"days_ago\"> By " + strAssigned + " at " + items.CommentDate + "</aside>" +
                                                "</article></article></section></div>";
                        }

                        preaddedcomment += "</div>";
                    }


                }

                if (string.IsNullOrEmpty(strbind))
                {
                    strbind = "Sorry no data !";
                }
                taskdiv.InnerHtml = strbind;
                prevComments.InnerHtml = preaddedcomment;
            }
            else
            {
                try
                {
                    bindTeamTask();
                }
                catch (Exception ex)
                {

                    logger.Error(ex.Message);
                }
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtComment.Text))
            {
                try
                {
                    Guid taskid = Guid.Parse(hdnTask_id.Value);
                    SocioBoard.Domain.Team team = (SocioBoard.Domain.Team)Session["GroupName"];
                    string curdate = DateTime.Now.ToString("yyyy-MM-dd H:mm:ss ").ToString();
                    TaskCommentRepository objTaskCmtRepo = new TaskCommentRepository();
                    TaskComment objTaskCmt = new TaskComment();
                    objTaskCmt.Comment = txtComment.Text;
                    objTaskCmt.CommentDate = DateTime.Now;
                    objTaskCmt.Id = Guid.NewGuid();
                    objTaskCmt.TaskId = taskid;
                    objTaskCmt.UserId = custid;
                    objTaskCmtRepo.addTaskComment(objTaskCmt);
                    hdnTask_id.Value = "";
                    TaskRepository taskRepo = new TaskRepository();
                    ArrayList alst = taskRepo.getAllIncompleteTasksOfUser(custid,team.GroupId);
                    Session["IncomingTasks"] = alst.Count;
                    txtComment.Text = "";
                }
                catch (Exception ex)
                {
                    logger.Error(ex.Message);
                }
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("Task.aspx", false);
        }
        
        protected void btn_Click(object sender, EventArgs e)
        {
            try
            {
                int res = 0;
                Guid taskid = Guid.Parse(hdnTask_id.Value);
                SocioBoard.Domain.Team team = (SocioBoard.Domain.Team)Session["GroupName"];
                bool status = bool.Parse(hdnstatus.Value.ToString());
                if (status == true)
                    status = false;
                else
                    status = true;
                TaskRepository objTaskRepo = new TaskRepository();
                objTaskRepo.updateTaskStatus(taskid, custid, status);
                TaskRepository taskRepo = new TaskRepository();
                ArrayList alst = taskRepo.getAllIncompleteTasksOfUser(custid,team.GroupId);
                Session["IncomingTasks"] = alst.Count;
                bindTeamTask();
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);

            }
        }


    }
}
using Api.Socioboard.Helper;
using Api.Socioboard.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Script.Services;
using System.Web.Services;
using Domain.Socioboard.Domain;
using System.Collections;

namespace Api.Socioboard.Services
{
    /// <summary>
    /// Summary description for Group
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [ScriptService]
    public class Tasks : System.Web.Services.WebService
    {

        [WebMethod]
        public void CreateNewTask(string description, string userid, Domain.Socioboard.Domain.Tasks task, string assigntoId, string comment)
        {
            string descritption = description;
            Guid idtoassign = Guid.Empty;
            idtoassign = Guid.Parse(assigntoId);

            Domain.Socioboard.Domain.Tasks objTask = task;
            TaskRepository objTaskRepo = new TaskRepository();
            objTask.AssignDate = DateTime.Now.ToString("yyyy-MM-dd H:mm:ss").ToString();
            objTask.AssignTaskTo = idtoassign;
            objTask.TaskStatus = false;
            objTask.TaskMessage = descritption;
            objTask.UserId = Guid.Parse(userid);
            Guid taskid = Guid.NewGuid();
            objTask.Id = taskid;
            objTaskRepo.addTask(objTask);

            /////////////////       
            string Comment = comment;
            if (!string.IsNullOrEmpty(comment))
            {
                string curdate = DateTime.Now.ToString("yyyy-MM-dd H:mm:ss").ToString();
                Domain.Socioboard.Domain.TaskComment objcmt = new Domain.Socioboard.Domain.TaskComment();
                TaskCommentRepository objcmtRepo = new TaskCommentRepository();
                objcmt.Comment = comment;
                objcmt.CommentDate = DateTime.Now;
                objcmt.EntryDate = DateTime.Now;
                objcmt.Id = Guid.NewGuid();
                objcmt.TaskId = objTask.Id;
                objcmt.UserId = Guid.Parse(userid);
                objcmtRepo.addTaskComment(objcmt);
            }
        }
        [WebMethod]

        public void CreateNewTaskForAnroid(string description, string userid, string assigntoId, string comment)
        {
            string descritption = description;
            Guid idtoassign = Guid.Empty;
            idtoassign = Guid.Parse(assigntoId);

            Domain.Socioboard.Domain.Tasks objTask = new Domain.Socioboard.Domain.Tasks();
            TaskRepository objTaskRepo = new TaskRepository();
            objTask.AssignDate = DateTime.Now.ToString("yyyy-MM-dd H:mm:ss").ToString();
            objTask.AssignTaskTo = idtoassign;
            objTask.TaskStatus = false;
            objTask.TaskMessage = descritption;
            objTask.UserId = Guid.Parse(userid);
            Guid taskid = Guid.NewGuid();
            objTask.Id = taskid;
            objTaskRepo.addTask(objTask);

            /////////////////       
            string Comment = comment;
            if (!string.IsNullOrEmpty(comment))
            {
                string curdate = DateTime.Now.ToString("yyyy-MM-dd H:mm:ss").ToString();
                Domain.Socioboard.Domain.TaskComment objcmt = new Domain.Socioboard.Domain.TaskComment();
                TaskCommentRepository objcmtRepo = new TaskCommentRepository();
                objcmt.Comment = comment;
                objcmt.CommentDate = DateTime.Now;
                objcmt.EntryDate = DateTime.Now;
                objcmt.Id = Guid.NewGuid();
                objcmt.TaskId = objTask.Id;
                objcmt.UserId = Guid.Parse(userid);
                objcmtRepo.addTaskComment(objcmt);
            }
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string GetAllTasks(string custid)
        {
            ArrayList taskdata = new ArrayList();
            TaskCommentRepository objTaskCmtRepo = new TaskCommentRepository();
            TaskRepository taskrepo = new TaskRepository();
            taskdata = taskrepo.getAllTasksOfUser(Guid.Parse(custid));
            return new JavaScriptSerializer().Serialize(taskdata);
        }


        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string UpdateTasksByUserIdTaskId(string taskMessage, string assignTaskTo, string taskStatus, DateTime assignDate, string taskId, string userId)
        {
            string status = string.Empty;
            try
            {

                TaskRepository taskrepo = new TaskRepository();
                Domain.Socioboard.Domain.Tasks objTask = new Domain.Socioboard.Domain.Tasks();

                var Assign_Date = String.Format("{0:yyyy-M-d HH:mm:ss}", assignDate);
                //var Completion_Date = String.Format("{0:yyyy-M-d HH:mm:ss}", DateTime.Now);
                objTask.Id = Guid.Parse(taskId);
                objTask.UserId = Guid.Parse(userId);
                objTask.AssignDate = Convert.ToString(Assign_Date);
                //objTask.CompletionDate = DateTime.Parse(String.Format("{0:yyyy-M-d HH:mm:ss}", DateTime.Now));
                objTask.AssignTaskTo = Guid.Parse(assignTaskTo);
                objTask.TaskMessage = taskMessage;
                objTask.TaskStatus = Convert.ToBoolean(taskStatus);

                taskrepo.updateTask(objTask);

                status = "Success";
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error : " + ex.StackTrace);
            }
            return new JavaScriptSerializer().Serialize(status);
        }

        //getAllTasksOfUserList
        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string getAllTasksOfUserList(string userid, string groupid)
        {
            List<Domain.Socioboard.Domain.Tasks> taskdata = new List<Domain.Socioboard.Domain.Tasks>();
            TaskRepository taskrepo = new TaskRepository();
            taskdata = taskrepo.getAllTasksOfUserList(Guid.Parse(userid), Guid.Parse(groupid));
            return new JavaScriptSerializer().Serialize(taskdata);
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string GetAllTaskByUserIdAndGroupId(string userid, string username, string profileurl, string days, string groupid)
        {
            UserRepository userrepo = new UserRepository();
            Guid UserId = Guid.Parse(userid);
            List<Domain.Socioboard.Domain.Tasks> taskdata = new List<Domain.Socioboard.Domain.Tasks>();
            List<Domain.Socioboard.Domain.TaskByUser> taskbyuser = new List<Domain.Socioboard.Domain.TaskByUser>();
            TaskByUser _TaskByUser = new TaskByUser();
            TaskRepository taskrepo = new TaskRepository();
            taskdata = taskrepo.getAllTasksOfUserBYDays(Guid.Parse(userid), Guid.Parse(groupid), Convert.ToInt32(days));
            foreach (var item in taskdata)
            {
                _TaskByUser.TaskMessage = item.TaskMessage;
                _TaskByUser.TaskStatus = item.TaskStatus;
                _TaskByUser.AssignDate = item.AssignDate;
                _TaskByUser.CompletionDate = item.CompletionDate;
                try
                {
                    if (UserId == item.AssignTaskTo)
                    {
                        _TaskByUser.AssignToUserName = username;
                    }
                    else
                    {
                        Domain.Socioboard.Domain.User User = userrepo.getUsersById(item.AssignTaskTo);
                        _TaskByUser.AssignToUserName = User.UserName;

                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.StackTrace);
                }
                try
                {
                    if (UserId == item.UserId)
                    {
                        _TaskByUser.AssignByUserName = username;
                    }
                    else
                    {
                        Domain.Socioboard.Domain.User User = userrepo.getUsersById(item.UserId);
                        _TaskByUser.AssignByUserName = User.UserName;

                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.StackTrace);
                }

                _TaskByUser.days = Convert.ToInt32(days);
                _TaskByUser.UserName = username;
                _TaskByUser.ProfileUrl = profileurl;
                taskbyuser.Add(_TaskByUser);
            }
            return new JavaScriptSerializer().Serialize(taskbyuser);
        }


        //getAllTasksCommentOfUser
        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string getAllTasksCommentOfUser(string taskid)
        {
            ArrayList taskdata = new ArrayList();
            TaskRepository taskrepo = new TaskRepository();
            taskdata = taskrepo.getAllTasksCommentOfUser(Guid.Parse(taskid));
            return new JavaScriptSerializer().Serialize(taskdata);
        }

        //getAllTasksCommentOfUser
        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string getAllTasksCommentOfUserList(string taskid)
        {
            List<Domain.Socioboard.Domain.TaskComment> lstTaskComment = new List<Domain.Socioboard.Domain.TaskComment>();
            TaskRepository taskrepo = new TaskRepository();
            lstTaskComment = taskrepo.getAllTasksCommentOfUserList(Guid.Parse(taskid));
            return new JavaScriptSerializer().Serialize(lstTaskComment);
        }

        //addTask
        [WebMethod]
        public void addTask(string description, string userid, Domain.Socioboard.Domain.Tasks task, string assigntoId, string comment, string AssignDate)
        {
            string descritption = description;
            Guid idtoassign = Guid.Empty;
            idtoassign = Guid.Parse(assigntoId);

            Domain.Socioboard.Domain.Tasks objTask = task;
            TaskRepository objTaskRepo = new TaskRepository();
            objTask.AssignDate = DateTime.Now.ToString("yyyy-MM-dd H:mm:ss").ToString();
            objTask.AssignTaskTo = idtoassign;
            objTask.TaskStatus = false;
            objTask.TaskMessage = descritption;
            objTask.UserId = Guid.Parse(userid);
            Guid taskid = Guid.NewGuid();
            objTask.Id = taskid;
            objTaskRepo.addTask(objTask);

            /////////////////       
            string Comment = comment;
            if (!string.IsNullOrEmpty(comment))
            {
                string curdate = DateTime.Now.ToString("yyyy-MM-dd H:mm:ss").ToString();
                Domain.Socioboard.Domain.TaskComment objcmt = new Domain.Socioboard.Domain.TaskComment();
                TaskCommentRepository objcmtRepo = new TaskCommentRepository();
                objcmt.Comment = comment;
                objcmt.CommentDate = DateTime.Now;
                objcmt.EntryDate = DateTime.Now;
                objcmt.Id = Guid.NewGuid();
                objcmt.TaskId = objTask.Id;
                objcmt.UserId = Guid.Parse(userid);
                objcmtRepo.addTaskComment(objcmt);
            }
        }

        //addTask
        [WebMethod]
        public void AddNewTaskWithGroup(string description, string userid, Domain.Socioboard.Domain.Tasks task, string assigntoId, string comment, string AssignDate, string groupid)
        {
            string descritption = description;
            Guid idtoassign = Guid.Empty;
            idtoassign = Guid.Parse(assigntoId);

            Domain.Socioboard.Domain.Tasks objTask = task;
            TaskRepository objTaskRepo = new TaskRepository();
            objTask.AssignDate = DateTime.Now.ToString("yyyy-MM-dd H:mm:ss").ToString();
            objTask.AssignTaskTo = idtoassign;
            objTask.TaskStatus = false;
            objTask.TaskMessage = descritption;
            objTask.UserId = Guid.Parse(userid);
            Guid taskid = Guid.NewGuid();
            objTask.Id = taskid;
            objTask.GroupId = Guid.Parse(groupid);
            objTaskRepo.addTask(objTask);

            /////////////////       
            string Comment = comment;
            if (!string.IsNullOrEmpty(comment))
            {
                string curdate = DateTime.Now.ToString("yyyy-MM-dd H:mm:ss").ToString();
                Domain.Socioboard.Domain.TaskComment objcmt = new Domain.Socioboard.Domain.TaskComment();
                TaskCommentRepository objcmtRepo = new TaskCommentRepository();
                objcmt.Comment = comment;
                objcmt.CommentDate = DateTime.Now;
                objcmt.EntryDate = DateTime.Now;
                objcmt.Id = Guid.NewGuid();
                objcmt.TaskId = objTask.Id;
                objcmt.UserId = Guid.Parse(userid);
                objcmtRepo.addTaskComment(objcmt);
            }
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string ChangeTaskStatus(string UserId, string TaskId, string Status)
        {
            string ret = string.Empty;
            try
            {
                Guid taskid = Guid.Parse(TaskId);
                bool status = bool.Parse(Status);
                if (status == true)
                    status = false;
                else
                    status = true;
                TaskRepository objTaskRepo = new TaskRepository();
                objTaskRepo.updateTaskStatus(taskid, Guid.Parse(UserId), status);
                ret = "Success";
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.StackTrace);
                ret = "Fail";
            }
            return new JavaScriptSerializer().Serialize(ret);
        }


        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string UpdateTaskReadStatus(string TaskId, string UserId, string GroupId)
        {
            string ret = string.Empty;
            try
            {
                TaskRepository taskrepo = new TaskRepository();
                Guid taskid = Guid.Parse(TaskId);
                Guid userid = Guid.Parse(UserId);
                Guid groupid = Guid.Parse(GroupId);

                taskrepo.UpdateTaskReadStatus(taskid, userid, groupid);

                ret = "Success";
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                ret = "Fail";
            }
            return new JavaScriptSerializer().Serialize(ret);
        }




    }
}

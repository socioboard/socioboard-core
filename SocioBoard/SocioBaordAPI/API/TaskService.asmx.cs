using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Script.Services;
using SocioBoard.Model;
using SocioBoard.Domain;
using System.Collections;
using System.Web.Script.Serialization;

namespace SocioBaordAPI.API
{
    /// <summary>
    /// Summary description for TaskService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [ScriptService]
    public class TaskService : System.Web.Services.WebService
    {

        [WebMethod]
        public void CreateNewTask(string description, string userid, Tasks task, string assigntoId, string comment)
        {
            string descritption = description;
            Guid idtoassign = Guid.Empty;
            idtoassign = Guid.Parse(assigntoId);

            Tasks objTask = new Tasks();
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
                TaskComment objcmt = new TaskComment();
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

            Tasks objTask = new Tasks();
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
                TaskComment objcmt = new TaskComment();
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

        //[WebMethod]
        //[ScriptMethod(UseHttpGet= false, ResponseFormat = ResponseFormat.Json)]
        //public string UpdateTasksByUserIdTaskId(string taskMessage, string assignTaskTo, string taskStatus, DateTime assignDate, string taskId, string userId)
        //{
        //    string status = string.Empty;
        //    try
        //    {
                
        //        TaskRepository taskrepo = new TaskRepository();
        //        Tasks objTask = new Tasks();

        //        objTask.Id = Guid.Parse(taskId);
        //        objTask.UserId = Guid.Parse(userId);
        //        objTask.AssignDate = Convert.ToString(assignDate);
        //        objTask.AssignTaskTo = Guid.Parse(assignTaskTo);
        //        objTask.TaskMessage = taskMessage;
        //        objTask.TaskStatus = Convert.ToBoolean(taskStatus);
               
        //        taskrepo.updateTask(objTask);

        //        status = "Success";
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine("Error : " + ex.StackTrace);
        //    }
        //    return new JavaScriptSerializer().Serialize(status);
        //}


        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string UpdateTasksByUserIdTaskId(string taskMessage, string assignTaskTo, string taskStatus, DateTime assignDate, string taskId, string userId)
        {
            string status = string.Empty;
            try
            {

                TaskRepository taskrepo = new TaskRepository();
                Tasks objTask = new Tasks();

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


        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string GetAllTaskCommentByUserIdTaskId(string taskId, string userId)
        {
            ArrayList arrList = new ArrayList();
            string JStr = string.Empty;
            try
            {

                TaskCommentRepository taskrepo = new TaskCommentRepository();
                TaskComment objTask = new TaskComment();

                objTask.Id = Guid.Parse(taskId);
                objTask.UserId = Guid.Parse(userId);


                arrList = taskrepo.getAllTasksCommentOfUser(objTask.UserId,objTask.Id);

                if (arrList.Count > 0)
                {
                    JStr = new JavaScriptSerializer().Serialize(arrList);
                }
                else
                {
                    JStr = "Data Not Found";
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error : " + ex.StackTrace);
            }
            return JStr;
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string AddTaskComment(string comment, string userId,string taskId,DateTime commentDate,DateTime entryDate)
        {
            string status = "";
            try
            {

                TaskCommentRepository taskrepo = new TaskCommentRepository();
                TaskComment objTask = new TaskComment();

                objTask.Id = new Guid();
                objTask.UserId = Guid.Parse(userId);
                objTask.Comment = comment;
                objTask.TaskId = Guid.Parse(taskId);
                objTask.CommentDate = commentDate;
                objTask.EntryDate = DateTime.Now;

                taskrepo.addTaskComment(objTask);

                status = "Success";
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error : " + ex.StackTrace);
            }
            return new JavaScriptSerializer().Serialize(status);
        }


    }
}

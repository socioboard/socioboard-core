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
namespace SocialCrowd.API
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
        public void CreateNewTask(string description, string userid, Tasks task,string assigntoId,string comment)
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


    }
}

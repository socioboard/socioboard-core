using Api.Socioboard.Helper;
using Api.Socioboard.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Script.Services;
using System.Web.Services;
using Domain.Socioboard.Domain;

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
    public class TaskComment : System.Web.Services.WebService
    {

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string GetAllTaskCommentByUserIdTaskId(string taskId, string userId)
        {
           // ArrayList arrList = new ArrayList();
           
            string JStr = string.Empty;
            try
            {

                TaskCommentRepository objTaskCommentRepository = new TaskCommentRepository();
                Domain.Socioboard.Domain.TaskComment objTask = new Domain.Socioboard.Domain.TaskComment();
                List<Domain.Socioboard.Domain.TaskComment> arrList = new List<Domain.Socioboard.Domain.TaskComment>();
                objTask.Id = Guid.Parse(taskId);
                objTask.UserId = Guid.Parse(userId);


                arrList = objTaskCommentRepository.getAllTasksCommentOfUser(objTask.Id,objTask.UserId);

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
        public string AddTaskComment(string comment, string userId, string taskId, string commentDate, DateTime entryDate)
        {
            string status = "";
            try
            {

                TaskCommentRepository taskrepo = new TaskCommentRepository();
                Domain.Socioboard.Domain.TaskComment objTask = new Domain.Socioboard.Domain.TaskComment();

                objTask.Id = new Guid();
                objTask.UserId = Guid.Parse(userId);
                objTask.Comment = comment;
                objTask.TaskId = Guid.Parse(taskId);
                //objTask.CommentDate = Convert.ToDateTime(commentDate);
                objTask.CommentDate = DateTime.Now;
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

using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;
using Newtonsoft.Json.Linq;
using Socioboard.Api.TeamMemberProfile;
using Socioboard.Helper;
using Socioboard.App_Start;

namespace Socioboard.Controllers
{

    public class MessagesController : BaseController
    {
        //
        // GET: /Message/

        public ActionResult Index()
        {
            if (Session["Paid_User"].ToString() == "Unpaid")
            {
                return RedirectToAction("Billing", "PersonalSetting");
            }
            else
            {
                return View("Index");
            }
            //DataSet ds = bindMessages();
            //return View("_MessagesMidPartial", ds.Tables[0]);

            //string viewToStr = RenderRazorViewToString("_MessagesLeftSideBarPartial", null);

            //return Content(viewToStr);

            //return View("Index"); //View("_MessagesMidPartialNew", ds); //View("Index");
        }

        public ActionResult accordianprofiles()
        {
            Api.Team.Team objApiTeam = new Api.Team.Team();
            string groupid = Session["group"].ToString();
            Domain.Socioboard.Domain.Team team = (Domain.Socioboard.Domain.Team)new JavaScriptSerializer().Deserialize(objApiTeam.GetTeamByGroupId(Session["group"].ToString()), typeof(Domain.Socioboard.Domain.Team));

    
            Api.TeamMemberProfile.TeamMemberProfile objApiTeamMemberProfile = new Api.TeamMemberProfile.TeamMemberProfile();
            List<Domain.Socioboard.Domain.TeamMemberProfile> alstprofiles = (List<Domain.Socioboard.Domain.TeamMemberProfile>)new JavaScriptSerializer().Deserialize(objApiTeamMemberProfile.GetTeamMemberProfilesByTeamId(team.Id.ToString()), typeof(List<Domain.Socioboard.Domain.TeamMemberProfile>));
        
            return PartialView("_MessagesRightPartial", alstprofiles);//Content(view_MessagesaccordianprofilesPartial);
        }

        public string RenderRazorViewToString(string viewName, object model)
        {
            ViewData.Model = model;
            using (var sw = new StringWriter())
            {
                var viewResult = ViewEngines.Engines.FindPartialView(ControllerContext,
                                                                         viewName);
                var viewContext = new ViewContext(ControllerContext, viewResult.View,
                                             ViewData, TempData, sw);
                viewResult.View.Render(viewContext, sw);
                viewResult.ViewEngine.ReleaseView(ControllerContext, viewResult.View);
                return sw.GetStringBuilder().ToString();
            }
        }

        //Commented by SumitGupta [09-02-2015]
        //public DataSet bindMessages()
        //{
        //    Domain.Socioboard.Domain.User _user = (Domain.Socioboard.Domain.User)Session["User"];
        //    DataSet ds = null;
        //    DataSet dataset = new DataSet();
            
        //    clsFeedsAndMessages clsfeedsandmess = new clsFeedsAndMessages();

        //    //string[] profid = new string[] { };

        //    string[] profid = (string[])Session["ProfileSelected"];
        //    Domain.Socioboard.Domain.Team team = SBUtils.GetTeamFromGroupId();

        //    try
        //    {
        //        string message = string.Empty;


        //        try
        //        {
        //            //if (profid != null)
        //            //{
        //                //profid = Request.QueryString["profileid[]"].Split(',');
        //                //if (Request.QueryString["type"] != null)
        //                //{
        //                    Session["countMesageDataTable_" + profid] = null;
        //                //}
        //            //}


        //        }
        //        catch (Exception ex)
        //        {
        //            Console.WriteLine(ex.StackTrace);
        //        }

        //        string facebook = string.Empty;

        //        foreach (var item in profid)
        //        {
        //            if (string.IsNullOrEmpty(item))
        //            {
        //                facebook = "emptyprofile";
        //            }
        //            else
        //            {
        //                facebook = "profile";
        //            }
        //        }

        //        if (string.IsNullOrEmpty(facebook))
        //        {
        //            facebook = "emptyprofile";
        //        }

        //        //if (facebook == "emptyprofile")
        //        {
        //            try
        //            {
        //                //DataSet ds = null;
        //                Session["countMesageDataTable_" + profid] = null;
        //                if (facebook == "emptyprofile")
        //                {
        //                    ds = clsfeedsandmess.bindMessagesIntoDataTable(team.Id); 
        //                }
        //                else
        //                {
        //                    ds = clsfeedsandmess.bindFeedMessageIntoDataTable(profid);
        //                }
        //                //FacebookFeedRepository fbFeedRepo = new FacebookFeedRepository();
        //                Session["MessageDataTable"] = ds;

        //                ds = (DataSet)Session["MessageDataTable"];

        //                if (Session["countMessageDataTable"] == null)
        //                {
        //                    Session["countMessageDataTable"] = 0;
        //                }
        //                int noOfDataToSkip = (int)Session["countMessageDataTable"];
        //                //DataTable records = ds.Tables[0].Rows.Cast<System.Data.DataRow>().Skip(noOfDataToSkip).Take(noOfDataToSkip + 15).CopyToDataTable();
        //                DataTable records = ds.Tables[0].Rows.Cast<System.Data.DataRow>().Skip(noOfDataToSkip).Take(15).CopyToDataTable();
        //                Session["countMessageDataTable"] = noOfDataToSkip + 15;
        //                //message = this.BindData(records);

        //                dataset.Tables.Add(records);
        //                return dataset;
                        
        //            }
        //            catch (Exception ex)
        //            {
        //                Console.WriteLine(ex.StackTrace);
        //            }
        //        }

        //        //else
        //        //{
        //        //    try
        //        //    {
        //        //        DataSet ds = null;
        //        //        Session["countMessageDataTable"] = null;

        //        //        ds = clsfeedsandmess.bindFeedMessageIntoDataTable(profid);
        //        //        Session["MessageDataTable"] = ds;

        //        //        ds = (DataSet)Session["MessageDataTable"];



        //        //        if (Session["countMesageDataTable_" + profid] == null)
        //        //        {
        //        //            Session["countMesageDataTable_" + profid] = 0;
        //        //        }

        //        //        int noOfDataToSkip = (int)Session["countMesageDataTable_" + profid];


        //        //        DataTable records = ds.Tables[0].Rows.Cast<System.Data.DataRow>().Skip(noOfDataToSkip).Take(noOfDataToSkip + 15).CopyToDataTable();
        //        //        Session["countMesageDataTable_" + profid] = noOfDataToSkip + 15;

        //        //    }
        //        //    catch (Exception ex)
        //        //    {
        //        //        Console.WriteLine(ex.StackTrace);
        //        //    }
        //        //}

        //        //if (string.IsNullOrEmpty(message))
        //        //{

        //        //}

        //        //Response.Write(message);

        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex.StackTrace);
        //    }

        //    return dataset;
        //}

        //Updated by SumitGupta [09-02-2015]
        public DataSet bindMessages()
        {
            Domain.Socioboard.Domain.User _user = (Domain.Socioboard.Domain.User)Session["User"];
            DataSet ds = null;
            DataSet dataset = new DataSet();

            clsFeedsAndMessages clsfeedsandmess = new clsFeedsAndMessages();

            //string[] profid = new string[] { };

            string[] profid = (string[])Session["ProfileSelected"];
            Domain.Socioboard.Domain.Team team = SBUtils.GetTeamFromGroupId();

            try
            {
                //For getting data range
                Session["MessageDataTable"] = ds;

                ds = (DataSet)Session["MessageDataTable"];

                if (Session["countMessageDataTable"] == null)
                {
                    Session["countMessageDataTable"] = 0;
                }

                int noOfDataToSkip = (int)Session["countMessageDataTable"];
                //DataTable records = ds.Tables[0].Rows.Cast<System.Data.DataRow>().Skip(noOfDataToSkip).Take(15).CopyToDataTable();
                Session["countMessageDataTable"] = noOfDataToSkip + 15;

                string message = string.Empty;


                try
                {
                    //if (profid != null)
                    //{
                    //profid = Request.QueryString["profileid[]"].Split(',');
                    //if (Request.QueryString["type"] != null)
                    //{
                    Session["countMesageDataTable_" + profid] = null;
                    //}
                    //}


                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.StackTrace);
                }

                string facebook = string.Empty;

                foreach (var item in profid)
                {
                    if (string.IsNullOrEmpty(item))
                    {
                        facebook = "emptyprofile";
                    }
                    else
                    {
                        facebook = "profile";
                    }
                }

                if (string.IsNullOrEmpty(facebook))
                {
                    facebook = "emptyprofile";
                }

                //if (facebook == "emptyprofile")
                {
                    try
                    {
                        //DataSet ds = null;
                        Session["countMesageDataTable_" + profid] = null;
                        if (facebook == "emptyprofile")
                        {
                            //Updated by SumitGupta [09-02-2015]
                            //ds = clsfeedsandmess.bindMessagesIntoDataTable(team.Id);
                            ds = clsfeedsandmess.bindMessagesIntoDataTable(team.Id, noOfDataToSkip);
                        }
                        else
                        {
                            //Updated by SumitGupta [09-02-2015]
                            //ds = clsfeedsandmess.bindFeedMessageIntoDataTable(profid);
                            ds = clsfeedsandmess.bindFeedMessageIntoDataTable(profid, noOfDataToSkip);
                        }
                      
                        Session["MessageDataTable"] = ds;

                        ds = (DataSet)Session["MessageDataTable"];

                        if (Session["countMessageDataTable"] == null)
                        {
                            Session["countMessageDataTable"] = 0;
                        }

                        //Updated by Sumit Gupta [09-02-2015]
                        //dataset.Tables.Add(records);
                        //return dataset;
                        return ds;

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                    }
                }

                //else
                //{
                //    try
                //    {
                //        DataSet ds = null;
                //        Session["countMessageDataTable"] = null;

                //        ds = clsfeedsandmess.bindFeedMessageIntoDataTable(profid);
                //        Session["MessageDataTable"] = ds;

                //        ds = (DataSet)Session["MessageDataTable"];



                //        if (Session["countMesageDataTable_" + profid] == null)
                //        {
                //            Session["countMesageDataTable_" + profid] = 0;
                //        }

                //        int noOfDataToSkip = (int)Session["countMesageDataTable_" + profid];


                //        DataTable records = ds.Tables[0].Rows.Cast<System.Data.DataRow>().Skip(noOfDataToSkip).Take(noOfDataToSkip + 15).CopyToDataTable();
                //        Session["countMesageDataTable_" + profid] = noOfDataToSkip + 15;

                //    }
                //    catch (Exception ex)
                //    {
                //        Console.WriteLine(ex.StackTrace);
                //    }
                //}

                //if (string.IsNullOrEmpty(message))
                //{

                //}

                //Response.Write(message);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
            }

            return dataset;
        }

        public ActionResult MessagesMidPartialNew()
        {
            Session["countMessageDataTable"] = 0;
            Session["ProfileSelected"] = new string[] { }; ;
            DataSet dataset = bindMessages();
            if (dataset.Tables.Count > 0 && dataset != null)
            {
                return PartialView("_MessagesMidPartialNew", dataset);
            }
            else
            {
                return Content("nodata");
            }

        }

      

        public ActionResult BindInboxOnScroll()
        {
            DataSet dataset = bindMessages();
            if (dataset.Tables.Count > 0 && dataset != null)
            {
                return PartialView("_SmartInboxPartial", dataset);
            }
            else
            {
                return Content("nodata");
            }

        }

        //LoadMessagesByProfile
        public ActionResult LoadMessagesByProfile()
        {
            Session["countMessageDataTable"] = 0;
            try
            {
                string[] profid = Request.QueryString["profileid[]"].Split(',');
                Session["ProfileSelected"] = profid;
            }
            catch (Exception ex)
            {
                Session["ProfileSelected"] = new string[] { };
            }
            DataSet dataset = bindMessages();
            if (dataset.Tables.Count > 0 && dataset != null)
            {
                return PartialView("_SmartInboxPartial", dataset);
            }
            else
            {
                return Content("nodata");
            }

        }

        public ActionResult Task()
        {
            if (Session["Paid_User"].ToString() == "Unpaid")
            {
                return RedirectToAction("Billing", "PersonalSetting");
            }
            else
            {
                return View();
            }
           // return View();
        }
        public ActionResult loadtask()
        {
            Domain.Socioboard.Domain.User objUser = (Domain.Socioboard.Domain.User)Session["User"];

            Domain.Socioboard.Domain.Team team = SBUtils.GetTeamFromGroupId();

            Api.Tasks.Tasks objApiTasks = new Api.Tasks.Tasks();
            List<Domain.Socioboard.Domain.Tasks> taskdata = (List<Domain.Socioboard.Domain.Tasks>)new JavaScriptSerializer().Deserialize(objApiTasks.getAllTasksOfUserList(objUser.Id.ToString(), team.GroupId.ToString()), typeof(List<Domain.Socioboard.Domain.Tasks>));
            ViewBag.Task = "MyTask";
            return PartialView("_TaskPartial", taskdata);

        }

        public PartialViewResult LoadIncompleteTask()
        {
            Domain.Socioboard.Domain.User objUser = (Domain.Socioboard.Domain.User)Session["User"];

            Domain.Socioboard.Domain.Team team = SBUtils.GetTeamFromGroupId();

            Api.Tasks.Tasks objApiTasks = new Api.Tasks.Tasks();
            List<Domain.Socioboard.Domain.Tasks> taskdata = (List<Domain.Socioboard.Domain.Tasks>)new JavaScriptSerializer().Deserialize(objApiTasks.GetAllIncompleteTaskofUser(objUser.Id.ToString(), team.GroupId.ToString()), typeof(List<Domain.Socioboard.Domain.Tasks>));
            ViewBag.Task = "PendingTask";
            return PartialView("_TaskPartial", taskdata);
        }

        public PartialViewResult LoadCompleteTask()
        {
            Domain.Socioboard.Domain.User objUser = (Domain.Socioboard.Domain.User)Session["User"];

            Domain.Socioboard.Domain.Team team = SBUtils.GetTeamFromGroupId();

            Api.Tasks.Tasks objApiTasks = new Api.Tasks.Tasks();
            List<Domain.Socioboard.Domain.Tasks> taskdata = (List<Domain.Socioboard.Domain.Tasks>)new JavaScriptSerializer().Deserialize(objApiTasks.GetAllCompleteTaskofUser(objUser.Id.ToString(), team.GroupId.ToString()), typeof(List<Domain.Socioboard.Domain.Tasks>));
            ViewBag.Task = "CompleteTask";
            return PartialView("_TaskPartial", taskdata);
        }
        public PartialViewResult LoadTeamTask()
        {
            Domain.Socioboard.Domain.User objUser = (Domain.Socioboard.Domain.User)Session["User"];

            Domain.Socioboard.Domain.Team team = SBUtils.GetTeamFromGroupId();

            Api.Tasks.Tasks objApiTasks = new Api.Tasks.Tasks();
            List<Domain.Socioboard.Domain.Tasks> taskdata = (List<Domain.Socioboard.Domain.Tasks>)new JavaScriptSerializer().Deserialize(objApiTasks.GetAllTeamTask(objUser.Id.ToString(), team.GroupId.ToString()), typeof(List<Domain.Socioboard.Domain.Tasks>));
            ViewBag.Task = "TeamTask";
            return PartialView("_TaskPartial", taskdata);
        }

        public ActionResult savetask()
        {
             string groupid = Session["group"].ToString();

            Domain.Socioboard.Domain.User objUser = (Domain.Socioboard.Domain.User)Session["User"];

            string descritption = Request.QueryString["description"];
            string MessageDate = Request.QueryString["msgdate"];

            string AssignDate = Request.QueryString["now"];

            string comment = Request.QueryString["comment"];

            Guid idtoassign = Guid.Empty;
            try
            {
                if (!string.IsNullOrEmpty(Request.QueryString["memberid"]))
                {
                    idtoassign = Guid.Parse(Request.QueryString["memberid"]);
                }
            }
            catch (Exception ex)
            {

            }

            Api.Tasks.Tasks1 objTasks = new Api.Tasks.Tasks1();

            Api.Tasks.Tasks objApiTasks = new Api.Tasks.Tasks();
            objApiTasks.AddNewTaskWithGroup(descritption,MessageDate,objUser.Id.ToString(), objTasks, idtoassign.ToString(), comment, AssignDate, groupid);

            string Groupid = Session["group"].ToString();

            Api.BusinessSetting.BusinessSetting objApiBusinessSetting = new Api.BusinessSetting.BusinessSetting();
            Domain.Socioboard.Domain.BusinessSetting objbsns = (Domain.Socioboard.Domain.BusinessSetting)new JavaScriptSerializer().Deserialize(objApiBusinessSetting.GetDetailsofBusinessOwner(Groupid), typeof(Domain.Socioboard.Domain.BusinessSetting));
            if (objbsns.TaskNotification==true)
            {
                Api.User.User ObjApiUser=new Api.User.User();
                Domain.Socioboard.Domain.User UsertoSendMail=(Domain.Socioboard.Domain.User)(new JavaScriptSerializer().Deserialize(ObjApiUser.getUsersById(idtoassign.ToString()),typeof(Domain.Socioboard.Domain.User)));
                Api.MailSender.MailSender ApiobjMailSender = new Api.MailSender.MailSender(); 
                string mailsender = "";
                try 
	            {
                    var mailBody = Helper.SBUtils.RenderViewToString(this.ControllerContext, "_TaskNotificationMailPartial", UsertoSendMail);
                    string Subject = "TASK ASSIGNMENT on Socioboard";

                    mailsender = ApiobjMailSender.SendTaskNotificationMail(UsertoSendMail.EmailId, mailBody, Subject);
	            }
	            catch (Exception)
	            {
		
		            throw;
	            } 
            }

            return Content("");
        }

        public void updatedstatus()
        {
            //try
            //{
            //    int i = 0;
            //    int j = 0;
            //    List<TeamMemberProfile> alstprofiles = objTeamMemberProfileRepository.getAllTeamMemberProfilesOfTeam(team.Id);
            //    foreach (TeamMemberProfile item in alstprofiles)
            //    {
            //        if (item.ProfileType == "twitter")
            //        {
            //            TwitterMessageRepository twtmsgRepo = new TwitterMessageRepository();
            //            i = twtmsgRepo.updateMessageStatus(item.ProfileId);
            //        }
            //        else if (item.ProfileType == "facebook")
            //        {
            //            FacebookFeedRepository fbfeedRepo = new FacebookFeedRepository();
            //            j = fbfeedRepo.updateMessageStatus(item.ProfileId);

            //        }
            //    }


            //    if (i > 0 || j > 0)
            //    {
            //        Session["CountMessages"] = 0;
            //        Session["MessageDataTable"] = null;


            //        DataSet ds = null;
            //        if (Session["MessageDataTable"] == null)
            //        {
            //            //clsFeedsAndMessages clsfeedsandmess = new clsFeedsAndMessages();
            //            ds = clsfeedsandmess.bindMessagesIntoDataTable(team.Id);
            //            FacebookFeedRepository fbFeedRepo = new FacebookFeedRepository();
            //            Session["MessageDataTable"] = ds;
            //        }
            //        else
            //        {
            //            ds = (DataSet)Session["MessageDataTable"];
            //        }
            //    }


            //}
            //catch (Exception ex)
            //{
            //    //logger.Error(ex.Message);
            //    Console.WriteLine(ex.Message);

            //}
        }

        public ActionResult sentmsg()
        {
            if (Session["Paid_User"].ToString() == "Unpaid")
            {
                return RedirectToAction("Billing", "PersonalSetting");
            }
            else
            {
                return View();
            }
            //return View();
        }
        //public ActionResult loadsentmsg()
        //{
        //    return PartialView("Index - Copy");
        //}

        public ActionResult loadsentmsg()
        {
            string AllProfileId = string.Empty;
            Domain.Socioboard.Domain.User objUser = (Domain.Socioboard.Domain.User)Session["User"];
            Api.ScheduledMessage.ScheduledMessage ApiobjScheduledMessage = new Api.ScheduledMessage.ScheduledMessage();
            Dictionary<Domain.Socioboard.Domain.TeamMemberProfile, object> allprofileofuser = SBUtils.GetUserProfilesccordingToGroup();
            foreach (var item in allprofileofuser)
            {
                try
                {
                    AllProfileId += item.Key.ProfileId + ',';
                }
                catch (Exception Err)
                {
                    Console.Write(Err.StackTrace);
                }
            }
            try
            {
                AllProfileId = AllProfileId.Substring(0, (AllProfileId.Length - 1));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
            }
            List<Domain.Socioboard.Domain.ScheduledMessage> lstSchedulemsg = (List<Domain.Socioboard.Domain.ScheduledMessage>)(new JavaScriptSerializer().Deserialize(ApiobjScheduledMessage.getAllSentMessageDetails(AllProfileId, objUser.Id.ToString()), typeof(List<Domain.Socioboard.Domain.ScheduledMessage>)));
            return PartialView("_sentmsgPartial", lstSchedulemsg);
        }

        public ActionResult addTaskComment()
        {
            string comment = Request.QueryString["comment"];
              string taskid = Request.QueryString["taskid"];
              string CommentDateTime = Request.QueryString["CommentDateTime"];

            Domain.Socioboard.Domain.User objDomainUser=(Domain.Socioboard.Domain.User)Session["User"];

            Api.TaskComment.TaskComment objApiTaskComment = new Api.TaskComment.TaskComment();
            string res = objApiTaskComment.AddTaskComment(comment, objDomainUser.Id.ToString(), taskid, CommentDateTime, DateTime.Now);

            return Content(res);
        }



        public ActionResult TwitterReply()
        {
            try
            {
                Api.Twitter.Twitter ApiobjTwitter = new Api.Twitter.Twitter();
                Domain.Socioboard.Domain.User objUser = (Domain.Socioboard.Domain.User)Session["User"];
                string comment = Request.QueryString["comment"];
                string ProfileId = Request.QueryString["ProfileId"];
                string messageid = Request.QueryString["messageid"];
                string replypost = ApiobjTwitter.TwitterReplyUpdate(comment, objUser.Id.ToString(), ProfileId, messageid);
                //JArray replystatus =(JArray)(new JavaScriptSerializer().Deserialize(replypost, typeof(JArray)));
                return Content("success");
            }
            catch (Exception ex)
            {
                Console.Write(ex.StackTrace);
                return Content("");
            }
        }

        public ActionResult FacebokReply()
        {
            try
            {
                Api.Facebook.Facebook ApiobjFacebook = new Api.Facebook.Facebook();
                Domain.Socioboard.Domain.User objUser = (Domain.Socioboard.Domain.User)Session["User"];
                string comment = Request.QueryString["comment"];
                string ProfileId = Request.QueryString["ProfileId"];
                string messageid = Request.QueryString["messageid"];
                string replaypost = ApiobjFacebook.FacebookComment(comment, ProfileId, messageid, objUser.Id.ToString());
                return Content("success");
            }
            catch (Exception ex)
            {
                Console.Write(ex.StackTrace);
                return Content("");
            }
        }
        public ActionResult SaveArchiveMessage()
        {
            try
            {
                Api.ArchiveMessage.ArchiveMessage ApiobjArchiveMessage = new Api.ArchiveMessage.ArchiveMessage();
                Domain.Socioboard.Domain.User objUser = (Domain.Socioboard.Domain.User)Session["User"];
                string ProfileId = Request.QueryString["ProfileId"];
                string MessageId = Request.QueryString["MessageId"];
                string Network = Request.QueryString["network"];
                string UserName = Request.QueryString["username"];
                string MessageDate = Request.QueryString["MessageDate"];
                string ProfileUrl = Request.QueryString["profileurl"];
                string Message = Request.QueryString["message"];
                try
                {
                    if (!ApiobjArchiveMessage.CheckArchiveMessageExists(objUser.Id.ToString(), MessageId))
                    {
                        ApiobjArchiveMessage.AddArchiveMessage(objUser.Id.ToString(), ProfileId, Network, UserName, MessageId, Message, MessageDate, ProfileUrl);
                        return Content("Archived successfully");
                    }
                    else
                    {
                        return Content("Message already Archived");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.StackTrace);
                    return Content("Somthing went wrong!!");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return Content("");
            }
        }

        public ActionResult DeleteArchiveMessage()
        {
            try
            {
                Api.ArchiveMessage.ArchiveMessage ApiobjArchiveMessage = new Api.ArchiveMessage.ArchiveMessage();
                Domain.Socioboard.Domain.User objUser = (Domain.Socioboard.Domain.User)Session["User"];
                string ProfileId = Request.QueryString["ProfileId"];
                string MessageId = Request.QueryString["MessageId"];
                string Network = Request.QueryString["network"];
                string UserName = Request.QueryString["username"];
                string MessageDate = Request.QueryString["MessageDate"];
                string ProfileUrl = Request.QueryString["profileurl"];
                string Message = Request.QueryString["message"];
                try
                {
                    ApiobjArchiveMessage.DeleteArchiveMessage(objUser.Id.ToString(), ProfileId, Network, UserName, MessageId, Message, MessageDate, ProfileUrl);
                    return Content("Archived successfully");

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.StackTrace);
                    return Content("Somthing went wrong!!");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return Content("");
            }
        }

        public ActionResult Archive()
        {
            if (Session["Paid_User"].ToString() == "Unpaid")
            {
                return RedirectToAction("Billing", "PersonalSetting");
            }
            else
            {
                return View();
            }
           // return View();
        }

        public ActionResult loadarchive()
        {
           
            string AllProfileId = string.Empty;
            Domain.Socioboard.Domain.User objUser = (Domain.Socioboard.Domain.User)Session["User"];
            Api.ArchiveMessage.ArchiveMessage ApiobjArchiveMessage = new Api.ArchiveMessage.ArchiveMessage();
            Dictionary<Domain.Socioboard.Domain.TeamMemberProfile, object> allprofileofuser = SBUtils.GetUserProfilesccordingToGroup();
            foreach (var item in allprofileofuser)
            {
                try
                {
                    AllProfileId += item.Key.ProfileId + ',';
                }
                catch (Exception Err)
                {
                    Console.Write(Err.StackTrace);
                }
            }
            try
            {
                AllProfileId = AllProfileId.Substring(0, (AllProfileId.Length - 1));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
            }
            List<Domain.Socioboard.Domain.ArchiveMessage> lstAllArchive = (List<Domain.Socioboard.Domain.ArchiveMessage>)(new JavaScriptSerializer().Deserialize(ApiobjArchiveMessage.GetAllArchiveMessagesDetails(objUser.Id.ToString(), AllProfileId), typeof(List<Domain.Socioboard.Domain.ArchiveMessage>)));
            return PartialView("_ArchivePartial", lstAllArchive);
        }

        public ActionResult ChangeTaskStatus(string Taskid, string Status)
        {
            Domain.Socioboard.Domain.User objUser = (Domain.Socioboard.Domain.User)Session["User"];
            Api.Tasks.Tasks objTaskStatusChange = new Api.Tasks.Tasks();
            objTaskStatusChange.ChangeTaskStatus(objUser.Id.ToString(), Taskid, Status);
            return Content("Success");
        }

        public ActionResult DeleteTask(string Taskid)
        {
            Domain.Socioboard.Domain.User objUser = (Domain.Socioboard.Domain.User)Session["User"];
            Api.Tasks.Tasks objTaskStatusChange = new Api.Tasks.Tasks();
            objTaskStatusChange.DeleteTask(Taskid);
            return Content("Success");
        }

        public ActionResult getProfileDetails(string ProfileId, string Network)
        {

            Dictionary<string, object> _dicProfileDetails = new Dictionary<string, object>();
            if (Network == "twitter")
            {
                Domain.Socioboard.Domain.User objUser = (Domain.Socioboard.Domain.User)Session["User"];
                Api.Twitter.Twitter ApiobjTwitter = new Api.Twitter.Twitter();
                string ProfileDescription = ApiobjTwitter.TwitterProfileDetails(objUser.Id.ToString(), ProfileId);
               // Domain.Socioboard.Helper.TwitterProfileDetails ProfileDetails = (Domain.Socioboard.Helper.TwitterProfileDetails)(new JavaScriptSerializer().Deserialize(ProfileDescription, typeof(Domain.Socioboard.Helper.TwitterProfileDetails)));
                Domain.Socioboard.Domain.TwitterAccount ProfileDetails = (Domain.Socioboard.Domain.TwitterAccount)(new JavaScriptSerializer().Deserialize(ProfileDescription, typeof(Domain.Socioboard.Domain.TwitterAccount)));
                _dicProfileDetails.Add("Twitter", ProfileDetails);
            }
            if (Network == "facebook")
            {
                Domain.Socioboard.Domain.User objUser = (Domain.Socioboard.Domain.User)Session["User"];
                Api.Facebook.Facebook ApiobjFacebook = new Api.Facebook.Facebook();
                string ProfileDescription = ApiobjFacebook.FacebookProfileDetails(objUser.Id.ToString(), ProfileId);
                Domain.Socioboard.Domain.FacebookAccount ProfileDetails = (Domain.Socioboard.Domain.FacebookAccount)(new JavaScriptSerializer().Deserialize(ProfileDescription, typeof(Domain.Socioboard.Domain.FacebookAccount)));
                _dicProfileDetails.Add("Facebook", ProfileDetails);
            }
            if (Network == "linkedin")
            {
                Domain.Socioboard.Domain.User objUser = (Domain.Socioboard.Domain.User)Session["User"];
                Api.Linkedin.Linkedin ApiobjLinkedin = new Api.Linkedin.Linkedin();
                string ProfileDescription = ApiobjLinkedin.LinkedinProfileDetails(objUser.Id.ToString(), ProfileId);
                Domain.Socioboard.Domain.LinkedInAccount ProfileDetails = (Domain.Socioboard.Domain.LinkedInAccount)(new JavaScriptSerializer().Deserialize(ProfileDescription, typeof(Domain.Socioboard.Domain.LinkedInAccount)));
                _dicProfileDetails.Add("Linkedin", ProfileDetails);
            }

            return PartialView("_SocialProfileDetail", _dicProfileDetails);
        }

        public ActionResult LoadSentmsgByDay(string day)
        {
            string AllProfileId = string.Empty;
            Domain.Socioboard.Domain.User objUser = (Domain.Socioboard.Domain.User)Session["User"];
            Api.ScheduledMessage.ScheduledMessage ApiobjScheduledMessage = new Api.ScheduledMessage.ScheduledMessage();
            Dictionary<Domain.Socioboard.Domain.TeamMemberProfile, object> allprofileofuser = SBUtils.GetUserProfilesccordingToGroup();
            foreach (var item in allprofileofuser)
            {
                try
                {
                    AllProfileId += item.Key.ProfileId + ',';
                }
                catch (Exception Err)
                {
                    Console.Write(Err.StackTrace);
                }
            }
            try
            {
                AllProfileId = AllProfileId.Substring(0, (AllProfileId.Length - 1));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
            }
            List<Domain.Socioboard.Domain.ScheduledMessage> lstSchedulemsg = (List<Domain.Socioboard.Domain.ScheduledMessage>)(new JavaScriptSerializer().Deserialize(ApiobjScheduledMessage.GetAllScheduledMessageforADay(objUser.Id.ToString(), AllProfileId, day), typeof(List<Domain.Socioboard.Domain.ScheduledMessage>)));
            return PartialView("_sentmsgPartial", lstSchedulemsg);
        }

        public ActionResult LoadSentmsgByDays(string days)
        {
            string AllProfileId = string.Empty;
            Domain.Socioboard.Domain.User objUser = (Domain.Socioboard.Domain.User)Session["User"];
            Api.ScheduledMessage.ScheduledMessage ApiobjScheduledMessage = new Api.ScheduledMessage.ScheduledMessage();
            Dictionary<Domain.Socioboard.Domain.TeamMemberProfile, object> allprofileofuser = SBUtils.GetUserProfilesccordingToGroup();
            foreach (var item in allprofileofuser)
            {
                try
                {
                    AllProfileId += item.Key.ProfileId + ',';
                }
                catch (Exception Err)
                {
                    Console.Write(Err.StackTrace);
                }
            }
            try
            {
                AllProfileId = AllProfileId.Substring(0, (AllProfileId.Length - 1));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
            }
            List<Domain.Socioboard.Domain.ScheduledMessage> lstSchedulemsg = (List<Domain.Socioboard.Domain.ScheduledMessage>)(new JavaScriptSerializer().Deserialize(ApiobjScheduledMessage.GetAllScheduledMessageByDays(objUser.Id.ToString(), AllProfileId, days), typeof(List<Domain.Socioboard.Domain.ScheduledMessage>)));
            return PartialView("_sentmsgPartial", lstSchedulemsg);
        }
        public ActionResult LoadSentmsgByMonth(string month)
        {
            string AllProfileId = string.Empty;
            Domain.Socioboard.Domain.User objUser = (Domain.Socioboard.Domain.User)Session["User"];
            Api.ScheduledMessage.ScheduledMessage ApiobjScheduledMessage = new Api.ScheduledMessage.ScheduledMessage();
            Dictionary<Domain.Socioboard.Domain.TeamMemberProfile, object> allprofileofuser = SBUtils.GetUserProfilesccordingToGroup();
            foreach (var item in allprofileofuser)
            {
                try
                {
                    AllProfileId += item.Key.ProfileId + ',';
                }
                catch (Exception Err)
                {
                    Console.Write(Err.StackTrace);
                }
            }
            try
            {
                AllProfileId = AllProfileId.Substring(0, (AllProfileId.Length - 1));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
            }
            List<Domain.Socioboard.Domain.ScheduledMessage> lstSchedulemsg = (List<Domain.Socioboard.Domain.ScheduledMessage>)(new JavaScriptSerializer().Deserialize(ApiobjScheduledMessage.GetAllScheduledMessageByMonth(objUser.Id.ToString(), AllProfileId, month), typeof(List<Domain.Socioboard.Domain.ScheduledMessage>)));
            return PartialView("_sentmsgPartial", lstSchedulemsg);
        }
        public ActionResult LoadSentmsgForCustomrange(string sdate, string ldate)
        {
            string AllProfileId = string.Empty;
            Domain.Socioboard.Domain.User objUser = (Domain.Socioboard.Domain.User)Session["User"];
            Api.ScheduledMessage.ScheduledMessage ApiobjScheduledMessage = new Api.ScheduledMessage.ScheduledMessage();
            Dictionary<Domain.Socioboard.Domain.TeamMemberProfile, object> allprofileofuser = SBUtils.GetUserProfilesccordingToGroup();
            foreach (var item in allprofileofuser)
            {
                try
                {
                    AllProfileId += item.Key.ProfileId + ',';
                }
                catch (Exception Err)
                {
                    Console.Write(Err.StackTrace);
                }
            }
            try
            {
                AllProfileId = AllProfileId.Substring(0, (AllProfileId.Length - 1));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
            }
            List<Domain.Socioboard.Domain.ScheduledMessage> lstSchedulemsg = (List<Domain.Socioboard.Domain.ScheduledMessage>)(new JavaScriptSerializer().Deserialize(ApiobjScheduledMessage.GetAllSentMessageDetailsForCustomrange(objUser.Id.ToString(), AllProfileId, sdate, ldate), typeof(List<Domain.Socioboard.Domain.ScheduledMessage>)));
            return PartialView("_sentmsgPartial", lstSchedulemsg);
        }

        public ActionResult ShowMsgMailPopUp(string MsgId, string Network)
        {
            Domain.Socioboard.Domain.User objUser = (Domain.Socioboard.Domain.User)Session["User"];
            Dictionary<string, object> twtinfo = new Dictionary<string, object>();
            Api.TwitterMessage.TwitterMessage ApiobjTwitterMessage = new Api.TwitterMessage.TwitterMessage();
            Api.FacebookMessage.FacebookMessage ApiobjFacebookMessage = new Api.FacebookMessage.FacebookMessage();
            if (Network == "twitter")
            {
                Domain.Socioboard.Domain.TwitterMessage twtmessage = (Domain.Socioboard.Domain.TwitterMessage)(new JavaScriptSerializer().Deserialize(ApiobjTwitterMessage.GetTwitterMessageByMessageId(objUser.Id.ToString(), MsgId), typeof(Domain.Socioboard.Domain.TwitterMessage)));
                twtinfo.Add("twt_msg", twtmessage);
            }
            else if (Network == "facebook")
            {
                Domain.Socioboard.Domain.FacebookMessage fbmessage = (Domain.Socioboard.Domain.FacebookMessage)(new JavaScriptSerializer().Deserialize(ApiobjFacebookMessage.GetFacebookMessageByMessageId(objUser.Id.ToString(), MsgId), typeof(Domain.Socioboard.Domain.FacebookMessage)));
                twtinfo.Add("fb_msg", fbmessage);
            }
            return PartialView("_MailMsgSendingPartial", twtinfo);
            //return PartialView("_TwitterMailSendingPartial", twtfeed);
        }

        //Vikash [04/12/2014]
        public void ExportSentMessages(List<Domain.Socioboard.Domain.ScheduledMessage> _ScheduledMessage, Domain.Socioboard.Domain.User _user)
        {
            var details = new System.Data.DataTable("sentmessage");
            details.Columns.Add("Date", typeof(string));
            details.Columns.Add("Network", typeof(string));
            details.Columns.Add("ProfileId", typeof(string));
            details.Columns.Add("Sent By", typeof(string));
            details.Columns.Add("Message", typeof(string));
            foreach (Domain.Socioboard.Domain.ScheduledMessage item_sent in _ScheduledMessage)
            {
                details.Rows.Add(item_sent.ScheduleTime, item_sent.ProfileType, item_sent.ProfileId, _user.UserName, item_sent.ShareMessage);
            }
            var grid = new GridView();
            grid.DataSource = details;
            grid.DataBind();
            Response.ClearContent();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment; filename=SentMessages_" + (DateTime.Now.Ticks).ToString() + ".xls");
            Response.ContentType = "application/ms-excel";
            Response.Charset = "";
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            grid.RenderControl(htw);
            Response.Output.Write(sw.ToString());
            Response.Flush();
            Response.End();
        }

        public ActionResult ExportSentmsgByDay(string day)
        {
            string AllProfileId = string.Empty;
            Domain.Socioboard.Domain.User objUser = (Domain.Socioboard.Domain.User)Session["User"];
            Api.ScheduledMessage.ScheduledMessage ApiobjScheduledMessage = new Api.ScheduledMessage.ScheduledMessage();
            Dictionary<Domain.Socioboard.Domain.TeamMemberProfile, object> allprofileofuser = SBUtils.GetUserProfilesccordingToGroup();
            foreach (var item in allprofileofuser)
            {
                try
                {
                    AllProfileId += item.Key.ProfileId + ',';
                }
                catch (Exception Err)
                {
                    Console.Write(Err.StackTrace);
                }
            }
            try
            {
                AllProfileId = AllProfileId.Substring(0, (AllProfileId.Length - 1));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
            }
            List<Domain.Socioboard.Domain.ScheduledMessage> lstSchedulemsg = (List<Domain.Socioboard.Domain.ScheduledMessage>)(new JavaScriptSerializer().Deserialize(ApiobjScheduledMessage.GetAllScheduledMessageforADay(objUser.Id.ToString(), AllProfileId, day), typeof(List<Domain.Socioboard.Domain.ScheduledMessage>)));
            ExportSentMessages(lstSchedulemsg, objUser);
            return RedirectToAction("sentmsg");
        }
        public ActionResult ExportSentmsgByDays(string days)
        {
            string AllProfileId = string.Empty;
            Domain.Socioboard.Domain.User objUser = (Domain.Socioboard.Domain.User)Session["User"];
            Api.ScheduledMessage.ScheduledMessage ApiobjScheduledMessage = new Api.ScheduledMessage.ScheduledMessage();
            Dictionary<Domain.Socioboard.Domain.TeamMemberProfile, object> allprofileofuser = SBUtils.GetUserProfilesccordingToGroup();
            foreach (var item in allprofileofuser)
            {
                try
                {
                    AllProfileId += item.Key.ProfileId + ',';
                }
                catch (Exception Err)
                {
                    Console.Write(Err.StackTrace);
                }
            }
            try
            {
                AllProfileId = AllProfileId.Substring(0, (AllProfileId.Length - 1));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
            }
            List<Domain.Socioboard.Domain.ScheduledMessage> lstSchedulemsg = (List<Domain.Socioboard.Domain.ScheduledMessage>)(new JavaScriptSerializer().Deserialize(ApiobjScheduledMessage.GetAllScheduledMessageByDays(objUser.Id.ToString(), AllProfileId, days), typeof(List<Domain.Socioboard.Domain.ScheduledMessage>)));
            ExportSentMessages(lstSchedulemsg, objUser);
            return RedirectToAction("sentmsg");
        }
        public ActionResult ExportSentmsgByMonth(string month)
        {
            string AllProfileId = string.Empty;
            Domain.Socioboard.Domain.User objUser = (Domain.Socioboard.Domain.User)Session["User"];
            Api.ScheduledMessage.ScheduledMessage ApiobjScheduledMessage = new Api.ScheduledMessage.ScheduledMessage();
            Dictionary<Domain.Socioboard.Domain.TeamMemberProfile, object> allprofileofuser = SBUtils.GetUserProfilesccordingToGroup();
            foreach (var item in allprofileofuser)
            {
                try
                {
                    AllProfileId += item.Key.ProfileId + ',';
                }
                catch (Exception Err)
                {
                    Console.Write(Err.StackTrace);
                }
            }
            try
            {
                AllProfileId = AllProfileId.Substring(0, (AllProfileId.Length - 1));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
            }
            List<Domain.Socioboard.Domain.ScheduledMessage> lstSchedulemsg = (List<Domain.Socioboard.Domain.ScheduledMessage>)(new JavaScriptSerializer().Deserialize(ApiobjScheduledMessage.GetAllScheduledMessageByMonth(objUser.Id.ToString(), AllProfileId, month), typeof(List<Domain.Socioboard.Domain.ScheduledMessage>)));
            ExportSentMessages(lstSchedulemsg, objUser);
            return RedirectToAction("sentmsg");
        }
        public ActionResult ExportSentmsgForCustomrange(string sdate, string ldate)
        {
            string AllProfileId = string.Empty;
            Domain.Socioboard.Domain.User objUser = (Domain.Socioboard.Domain.User)Session["User"];
            Api.ScheduledMessage.ScheduledMessage ApiobjScheduledMessage = new Api.ScheduledMessage.ScheduledMessage();
            Dictionary<Domain.Socioboard.Domain.TeamMemberProfile, object> allprofileofuser = SBUtils.GetUserProfilesccordingToGroup();
            foreach (var item in allprofileofuser)
            {
                try
                {
                    AllProfileId += item.Key.ProfileId + ',';
                }
                catch (Exception Err)
                {
                    Console.Write(Err.StackTrace);
                }
            }
            try
            {
                AllProfileId = AllProfileId.Substring(0, (AllProfileId.Length - 1));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
            }
            List<Domain.Socioboard.Domain.ScheduledMessage> lstSchedulemsg = (List<Domain.Socioboard.Domain.ScheduledMessage>)(new JavaScriptSerializer().Deserialize(ApiobjScheduledMessage.GetAllSentMessageDetailsForCustomrange(objUser.Id.ToString(), AllProfileId, sdate, ldate), typeof(List<Domain.Socioboard.Domain.ScheduledMessage>)));
            ExportSentMessages(lstSchedulemsg, objUser);
            return RedirectToAction("sentmsg");
        }

        

    }
}

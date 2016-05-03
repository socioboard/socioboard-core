using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Domain.Socioboard.Domain;
using System.Web.Script.Serialization;
using Socioboard.Helper;
using System.IO;
using Socioboard.App_Start;
using log4net;
using DataStreams.Xlsx;
using Excel = Microsoft.Office.Interop.Excel;
using System.Threading.Tasks;

namespace Socioboard.Controllers
{
  //  [Authorize]
    [CustomAuthorize]
    public class PublishingController : BaseController
    {
        //
        // GET: /Publishing/

        public ActionResult Index()
        {
            if (Session["User"] != null)
            {
                @ViewBag.Message = Request.QueryString["Message"];
                if (Session["Paid_User"].ToString() == "Unpaid")
                {
                    return RedirectToAction("Billing", "PersonalSetting");
                }
                else
                {
                    return View();
                }
            }
            else
            {
                return RedirectToAction("Index", "Index");
            }
            //return View();
        }
        public ActionResult Socioqueue()
        {
            if (Session["User"] != null)
            {
                if (Session["Paid_User"].ToString() == "Unpaid")
                {
                    return RedirectToAction("Billing", "PersonalSetting");
                }
                else
                {
                    return View();
                }
            }
            else
            {
                return RedirectToAction("Index", "Index");
            }
            //return View();
        }
        public ActionResult Draft()
        {
            if (Session["User"] != null)
            {
                if (Session["Paid_User"].ToString() == "Unpaid")
                {
                    return RedirectToAction("Billing", "PersonalSetting");
                }
                else
                {
                    return View();
                }
            }
            else
            {
                return RedirectToAction("Index", "Index");
            }
            //return View();
        }

        public ActionResult loadsocialqueue()
        {
            User objUser = (User)Session["User"];
            Api.ScheduledMessage.ScheduledMessage ApiobjScheduledMessage = new Api.ScheduledMessage.ScheduledMessage();
            //ApiobjScheduledMessage.GetSociaoQueueMessageByUserIdAndGroupId(objUser.Id.ToString(), Session["group"].ToString());
            List<ScheduledMessage> lstScheduledMessage = (List<ScheduledMessage>)(new JavaScriptSerializer().Deserialize(ApiobjScheduledMessage.GetSociaoQueueMessageByUserIdAndGroupId(objUser.Id.ToString(), Session["group"].ToString()), typeof(List<ScheduledMessage>)));
            return PartialView("_SocialQueuePartial", lstScheduledMessage);
        }

        public ActionResult loaddrafts()
        {
            User objUser = (User)Session["User"];
            Api.Drafts.Drafts ApiobjDrafts = new Api.Drafts.Drafts();
            List<Drafts> lstScheduledMessage = (List<Drafts>)(new JavaScriptSerializer().Deserialize(ApiobjDrafts.GetDraftMessageByUserIdAndGroupId(objUser.Id.ToString(), Session["group"].ToString()), typeof(List<Drafts>)));
            return PartialView("_DraftPartial", lstScheduledMessage);
        }
        public ActionResult ModifyDraftMessage(string draftid, string draftmsg)
        {
            User objUser = (User)Session["User"];
            Api.Drafts.Drafts ApiobjDrafts = new Api.Drafts.Drafts();
            string retmsg = ApiobjDrafts.UpdateDraftsMessage(draftid, objUser.Id.ToString(), Session["group"].ToString(), draftmsg);
            return Content(retmsg);
        }
        public ActionResult DeleteDraftMessage(string draftid)
        {
            User objUser = (User)Session["User"];
            Api.Drafts.Drafts ApiobjDrafts = new Api.Drafts.Drafts();
            string retmsg = ApiobjDrafts.DeleteDrafts(draftid);
            return Content(retmsg);
        }

        public ActionResult DeleteSocioQueueMessage(string msgid)
        {
            User objUser = (User)Session["User"];
            Api.ScheduledMessage.ScheduledMessage ApiobjScheduledMessage = new Api.ScheduledMessage.ScheduledMessage();
            string retmsg = ApiobjScheduledMessage.DeleteSchecduledMessage(msgid);
            return Content(retmsg);
        }


        public ActionResult EditSocioQueueMessage(string msgid, string msg)
        {
            User objUser = (User)Session["User"];
            Api.ScheduledMessage.ScheduledMessage ApiobjScheduledMessage = new Api.ScheduledMessage.ScheduledMessage();
            string retmsg = ApiobjScheduledMessage.EditSchecduledMessage(msgid, msg);
            return Content(retmsg);
        }

        public async Task<ActionResult> ComposeScheduledMessage()
        {
            User objUser = (User)Session["User"];
            Dictionary<GroupProfile, object> dict_TeamMember = new Dictionary<GroupProfile, object>();
            if (Session["group"] != null)
            {
                dict_TeamMember = await SBHelper.GetGroupProfiles();
            }
            return PartialView("_ComposeMessageSchedulerPartial", dict_TeamMember);
        }

        [HttpPost]
        public ActionResult ScheduledMessage(string scheduledmessage, string scheduleddate, string scheduledtime, string profiles, string clienttime)
        {
            var fi = Request.Files["file"];
            string file = string.Empty;
            if (Request.Files.Count > 0)
            {
                if (fi != null)
                {
                    var path = Server.MapPath("~/Themes/" + System.Configuration.ConfigurationManager.AppSettings["domain"] + "/Contents/img/upload");

                    // var path = System.Configuration.ConfigurationManager.AppSettings["MailSenderDomain"]+"Contents/img/upload";
                    file = path + "\\" + fi.FileName;
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }
                    fi.SaveAs(file);
                    path = path + "\\" + fi.FileName;
                }
            }

            User objUser = (User)Session["User"];
            Api.ScheduledMessage.ScheduledMessage ApiobjScheduledMessage = new Api.ScheduledMessage.ScheduledMessage();
            string retmsg = ApiobjScheduledMessage.AddAllScheduledMessage(profiles, scheduledmessage, clienttime, scheduleddate, scheduledtime, objUser.Id.ToString(), file,"");
            return Content("_ComposeMessagePartial");
        }


        public ActionResult SaveDraft(string scheduledmessage)
        {
            User objUser = (User)Session["User"];
            Api.Drafts.Drafts ApiobjDrafts = new Api.Drafts.Drafts();
            string retmsg = ApiobjDrafts.AddDraft(objUser.Id.ToString(), Session["group"].ToString(), DateTime.Now, scheduledmessage);
            return Content(retmsg);
        }

        //-------------For Bulk Schedule Import
        //-------------Edited by Kushagra[21/04/2015]
        public ActionResult InputCSVForBulkImport(string profiles, string clienttime)
        {
            string scheduletime = string.Empty;
            string scheduledate = string.Empty;
            string schedulemsg = string.Empty;
            string year = string.Empty;
            string month = string.Empty;
            string day = string.Empty;
            string hour = string.Empty;
            string minute = string.Empty;
            string picURL = string.Empty;
            string[] itemsData = { };
            string[] iteminfo = { };
            User objUser = (User)Session["User"];
            var file = Request.Files["filesinput"];


            Stream stream = file.InputStream;

            //List<string> fileDataList = readcsvfile(stream);
            List<string[]> csvinfo = parseExcel(stream);

            foreach (var item in csvinfo)
            {
                try
                {
                    string[] arr = item;
                    //itemsData = Regex.Split(item, ",");

                    schedulemsg = arr[0];
                    //iteminfo = Regex.Split(itemsData[1], ",");

                    //year = "201" + iteminfo[0];
                    year = arr[1];
                    month = arr[2];
                    if (Int32.Parse(month) < 10)
                    {

                        if (!month.Contains("0"))
                        {

                            month = "0" + month;
                        }
                    }
                    else
                    {
                        if (Int32.Parse(month) > 12)
                        {
                            continue;
                        }
                    }
                    day = arr[3];
                    if (Int32.Parse(day) < 10)
                    {
                        if (!day.Contains("0"))
                        {
                            day = "0" + day;
                        }
                    }
                    else
                    {
                        if (Int32.Parse(day) > 31)
                        {

                            continue;
                        }
                    }
                    hour = arr[4];
                    if (Int32.Parse(hour) < 10)
                    {
                        if (!hour.Contains("0"))
                        {
                            hour = "0" + hour;
                        }
                        if (hour == "0" || hour == "")
                        {
                            hour = "00";
                        }
                    }
                    else
                    {
                        if (Int32.Parse(hour) > 24)
                        {
                            continue;
                        }
                    }
                    minute = arr[5];
                    if (Int32.Parse(minute) < 10)
                    {
                        if (!minute.Contains("0"))
                        {
                            minute = "0" + minute;
                        }
                    }
                    else
                    {
                        if (Int32.Parse(minute) > 60)
                        {
                            continue;
                        }
                    }
                    try
                    {
                        picURL = arr[6];
                    }
                    catch (Exception ex)
                    {
                        picURL = "";
                    }
                    scheduledate = year + "/" + month + "/" + day;
                    scheduletime = hour + ":" + minute + ":00";

                    Api.ScheduledMessage.ScheduledMessage ApiobjScheduledMessage = new Api.ScheduledMessage.ScheduledMessage();
                    string retmsg = ApiobjScheduledMessage.AddAllScheduledMessage(profiles, schedulemsg, clienttime, scheduledate, scheduletime, objUser.Id.ToString(), picURL,"");

                }
                catch (Exception ex)
                {

                }
            }

            return Content("success");
        }


        //-------------For Bulk Schedule Import
        //-------------Edited by Kushagra[21/04/2015]
        public List<string[]> parseExcel(Stream stream)
        {
            List<string[]> parsedData = new List<string[]>();
            try
            {
                string path = string.Empty;
                List<string> tempdata = new List<string>();
                StreamReader sr = new StreamReader(stream, System.Text.Encoding.GetEncoding(1250));
                // string[] row =new string[5];
                string singlerow = string.Empty;
                //Excel.FillFormat xlfile = new Excel.FillFormat();
                Excel.Application xlApp;
                Excel.Workbook xlWorkBook;
                Excel.Worksheet xlWorkSheet;
                Excel.Range range;

                string str;
                int rCnt = 0;
                int cCnt = 0;

                var tempFilepath = string.Empty;
                var fi = Request.Files["filesinput"];
                string file = string.Empty;

                if (Request.Files.Count > 0)
                {
                    if (fi != null)
                    {
                        tempFilepath = Server.MapPath("~/Themes/" + System.Configuration.ConfigurationManager.AppSettings["domain"] + "/Contents/img/upload");

                        // var path = System.Configuration.ConfigurationManager.AppSettings["MailSenderDomain"]+"Contents/img/upload";
                        file = tempFilepath + "\\" + fi.FileName;
                        if (!Directory.Exists(tempFilepath))
                        {
                            Directory.CreateDirectory(tempFilepath);
                        }
                        fi.SaveAs(file);
                        tempFilepath = tempFilepath + "\\" + fi.FileName;
                    }
                }


                xlApp = new Excel.Application();
                // xlApp = new Excel.Application();
                xlWorkBook = xlApp.Workbooks.Open(tempFilepath, 0, true, 5, "", "", true, Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "\t", false, false, 0, true, 1, 0);
                //xlWorkBook = xlApp.Workbooks.Open(path, 0, true, 5, "", "", true, Microsoft.Office.Interop.Excel, "\t", false, false, 0, true, 1, 0);
                xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);

                range = xlWorkSheet.UsedRange;

                for (rCnt = 1; rCnt <= range.Rows.Count; rCnt++)
                {
                    //string[] row = new string[9];
                    string[] row = new string[range.Columns.Count];
                    for (cCnt = 1; cCnt <= range.Columns.Count; cCnt++)
                    {
                        try
                        {
                            str = (string)(range.Cells[rCnt, cCnt] as Excel.Range).Value2.ToString();
                            row[cCnt - 1] = str;
                        }
                        catch { }
                    }
                    parsedData.Add(row);
                }

                xlWorkBook.Close(true, null, null);
                xlApp.Quit();

                releaseObject(xlWorkSheet);
                releaseObject(xlWorkBook);
                releaseObject(xlApp);
                return parsedData;
            }
            catch
            {
                return parsedData;
            }

        }



        //-------------For Bulk Schedule Import
        //-------------Edited by Kushagra[21/04/2015]
        private void releaseObject(object obj)
        {
            try
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(obj);
                obj = null;
            }
            catch (Exception ex)
            {
                obj = null;
                //MessageBox.Show("Unable to release the Object " + ex.ToString());
            }
            finally
            {
                GC.Collect();
            }
        }




        public async Task<ActionResult> Calendar()
        {
            User objUser = (User)Session["User"];
            Dictionary<GroupProfile, object> dict_TeamMember = new Dictionary<GroupProfile, object>();
            if (Session["group"] != null)
            {
                dict_TeamMember = await SBHelper.GetGroupProfiles();
            }
            return View(dict_TeamMember);
        }

        [HttpGet]
        public JsonResult GetFeeds(DateTime start, DateTime end)
        {
            User objUser = (User)Session["User"];
            Api.ScheduledMessage.ScheduledMessage ApiobjScheduledMessage = new Api.ScheduledMessage.ScheduledMessage();
            List<ScheduledMessage> lstScheduledMessage = (List<ScheduledMessage>)(new JavaScriptSerializer().Deserialize(ApiobjScheduledMessage.GetSociaoQueueMessageByUserIdAndGroupIdBetweenDates(objUser.Id.ToString(), Session["group"].ToString(), start.ToString(), end.ToString()), typeof(List<ScheduledMessage>)));
            string path = System.Configuration.ConfigurationManager.AppSettings["domain"];
            var eventList = from e in lstScheduledMessage
                            select new
                            {
                                id = e.Id,
                                title = e.ShareMessage,
                              //  start = new DateTime(e.ScheduleTime.Year, e.ScheduleTime.Month, e.ScheduleTime.Day, e.ScheduleTime.Hour, e.ScheduleTime.Minute, e.ScheduleTime.Second).ToString("yyyy-MM-dd HH':'mm':'ss"),
                              start = ScheduleTime(e.ClientTime.ToLocalTime(),e.CreateTime,e.ScheduleTime) , 
                              //url
                                allDay = false,
                                description = e.ShareMessage,
                                profileId = e.ProfileId,
                                Image = e.PicUrl,
                                ProfileImg = GetProfileImage(e.ProfileId,e.ProfileType)
                                //Image = "/Themes/" + path + "/" +e.PicUrl.Split(new string[] { path }, StringSplitOptions.None)[2],
                            };
            var rows = eventList.ToArray();

            return Json(rows, JsonRequestBehavior.AllowGet);
        }

        string GetProfileImage(string ProfileId, string ProfileType) 
        {
            Domain.Socioboard.Domain.User objuser = (Domain.Socioboard.Domain.User)Session["User"];
            string profileImg = "/Themes/@path/Contents/img/anonymousUser.jpg";
            string profileName = "";
            if (ProfileType.Equals("facebook")) 
            {
                Socioboard.Api.FacebookAccount.FacebookAccount ApiobjFacebookAccount = new Socioboard.Api.FacebookAccount.FacebookAccount();
                FacebookAccount objFacebookAccount = (FacebookAccount)(new System.Web.Script.Serialization.JavaScriptSerializer().Deserialize(ApiobjFacebookAccount.getFacebookAccountDetailsById(objuser.Id.ToString(), ProfileId), typeof(FacebookAccount)));
                profileName = objFacebookAccount.FbUserName;
                profileImg = "http://graph.facebook.com/" + objFacebookAccount.FbUserId + "/picture?type=small";
               
            }
            else if (ProfileType.Equals("twitter"))
            {
                Socioboard.Api.TwitterAccount.TwitterAccount ApiobjTwitterAccount = new Socioboard.Api.TwitterAccount.TwitterAccount();
                TwitterAccount objTwitterAccount = (TwitterAccount)(new System.Web.Script.Serialization.JavaScriptSerializer().Deserialize(ApiobjTwitterAccount.GetTwitterAccountDetailsById(objuser.Id.ToString(), ProfileId), typeof(TwitterAccount)));
                profileName = objTwitterAccount.TwitterScreenName;
                profileImg = objTwitterAccount.ProfileImageUrl;
            }
            else if (ProfileType.Equals("linkedin"))
            {
                Socioboard.Api.LinkedinAccount.LinkedinAccount ApiobjLinkedinAccount = new Socioboard.Api.LinkedinAccount.LinkedinAccount();
                LinkedInAccount objLinkedInAccount = (LinkedInAccount)(new System.Web.Script.Serialization.JavaScriptSerializer().Deserialize(ApiobjLinkedinAccount.GetLinkedinAccountDetailsById(objuser.Id.ToString(), ProfileId), typeof(LinkedInAccount)));
                profileName = objLinkedInAccount.LinkedinUserName;
                profileImg = objLinkedInAccount.ProfileImageUrl;
               
            }
            else if (ProfileType.Equals("tumblr"))
            {
                Socioboard.Api.TumblrAccount.TumblrAccount ApiobjTumblrAccount = new Socioboard.Api.TumblrAccount.TumblrAccount();
                TumblrAccount objTumblrAccount = (TumblrAccount)(new System.Web.Script.Serialization.JavaScriptSerializer().Deserialize(ApiobjTumblrAccount.GetTumblrAccountDetailsById(objuser.Id.ToString(), ProfileId), typeof(TumblrAccount)));
                profileName = objTumblrAccount.tblrUserName;
                profileImg = "http://api.tumblr.com/v2/blog/" + objTumblrAccount.tblrUserName + ".tumblr.com/avatar";
            }
            return profileImg+","+profileName;
        }

        string ScheduleTime(DateTime clientdate, DateTime server, DateTime scheduletime)
        {
            DateTime client = Convert.ToDateTime(clientdate);
            string strTimeNow = String.Format("{0:s}", client).Replace('T', ' ');
            DateTime schedule = Convert.ToDateTime(scheduletime);
            if (DateTime.Compare(client, server) > 0)
            {

                //double minutes = (server - client).TotalMinutes;
                double minutes = (client - server).TotalMinutes;
                schedule = schedule.AddMinutes(minutes);
                return String.Format(schedule.ToString("yyyy-MM-dd HH':'mm':'ss"));

            }
            else if (DateTime.Compare(client, server) == 0)
            {
                return String.Format(schedule.ToString("yyyy-MM-dd HH':'mm':'ss"));
            }
            else if (DateTime.Compare(client, server) < 0)
            {
                //double minutes = (server - client).TotalMinutes;
                double minutes = (client - server).TotalMinutes;
                schedule = schedule.AddMinutes(minutes);
                return String.Format(schedule.ToString("yyyy-MM-dd HH':'mm':'ss"));
            }
            return null;
        }
    }
}

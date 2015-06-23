using Domain.Socioboard.Domain;
using Socioboard.App_Start;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Socioboard.Controllers
{
    [Authorize]
    [CustomAuthorize]
    public class GroupController : BaseController
    {
        //
        // GET: /Group/

        public ActionResult Index()
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
      
        public ActionResult LoadGroups()
        {
           
            return PartialView("_LoadGroups", Helper.SBUtils.GetGroupsMenuAccordingToGroup());
        }
        
        [HttpPost]
        public ActionResult GroupPosts(string grpId, string accToken, string ProfileId)
        {
            return PartialView("_GroupInfo", Helper.SBUtils.GetFbGroupDataAccordingGroupId(grpId, accToken, ProfileId));
        }
        
        [HttpPost]
        public ActionResult LinkedinGroupPosts(string groupid, string linkeid)
        {
           

         return PartialView("_GroupInfo", Helper.SBUtils.GetLinkedinGroupDataAccordingGroupId(groupid, linkeid));
          
        } 
       
        [HttpPost]
        public ActionResult CommentOnLinkedinPost(string groupid,string GpPostid,string message,string LinkedinUserId)
         {
          string response = Helper.SBUtils.CommentOnLinkedinPost(groupid, GpPostid, message, LinkedinUserId);
           return Content(response);

        }

        [HttpPost]
        public ActionResult LikeOnLinkedinPost(string GpPostid, string LinkedinUserId, string isLike)
        {
            string response = Helper.SBUtils.LikeOnLinkedinPost(GpPostid,LinkedinUserId,isLike);
            return Content(response);
        } 
       
        [HttpPost]
        public ActionResult FollowLinkedinPost(string GpPostid, string LinkedinUserId, string isFollowing)
        {
            string response = Helper.SBUtils.FollowLinkedinPost(GpPostid, LinkedinUserId, isFollowing);
            return Content(response);
        } 
       
        [HttpPost]
        public ActionResult postFBGroupFeeds(string gid, string accToken, string msg)
        {
            string response = Helper.SBUtils.PostOnFBGroupFeeds(gid, accToken, msg);
            return Content(response);
        } 
             
        [HttpPost]
        public ActionResult postLinkedInGroupFeeds(string gid, string linkedInUserId, string msg, string title)
        {
            string response = Helper.SBUtils.PostLinkedInGroupFeeds(gid, linkedInUserId, msg, title);
            return Content(response);
        }

        [HttpPost]
        public ActionResult PostOnselectedGroup(FormCollection frmcollection)
        {
            string msg = string.Empty;
            string title = string.Empty;
            string intrval = string.Empty;
            string fbuserid = string.Empty;
            string linuserid = string.Empty;
            string clienttime = string.Empty;
            var SelectedGroupId="";
           
                 SelectedGroupId = frmcollection["gid"].ToString();
                 title = frmcollection["title"].ToString();
                 msg = frmcollection["msg"].ToString();
                 intrval = frmcollection["intervaltime"].ToString();
                
                 clienttime = frmcollection["clienttime"].ToString();
                string time = frmcollection["timeforsch"];
                string date = frmcollection["dateforsch"];
                var files = Request.Files.Count;
                dynamic fi = Request.Files["files"];

                string filepath = string.Empty;
                string imagefile = string.Empty;
                if (Request.Files.Count > 0)
                {
                    if (fi != null)
                    {
                        var path = Server.MapPath("~/Themes/" + System.Configuration.ConfigurationManager.AppSettings["domain"] + "/Contents/img/upload");


                        filepath = path + "\\" + fi.FileName;
                        if (!Directory.Exists(path))
                        {
                            Directory.CreateDirectory(path);
                        }
                        fi.SaveAs(filepath);
                    }
                     imagefile = fi.FileName;
                }


                string response = Helper.SBUtils.PostOnSelectedGroups(SelectedGroupId, title, msg, intrval, clienttime, time, date, filepath);


              return Content(response);
           
        }

        //Modified by sumit gupta [27-02-15]
        [HttpPost]
        public ActionResult PostOnselectedGroupModified(FormCollection frmcollection)
        {
            string msg = string.Empty;
            string title = string.Empty;
            string intrval = string.Empty;
            string fbuserid = string.Empty;
            string linuserid = string.Empty;
            string clienttime = string.Empty;
            var SelectedGroupId = "";

            SelectedGroupId = frmcollection["gid"].ToString();
            title = frmcollection["title"].ToString();
            msg = frmcollection["msg"].ToString();
            intrval = frmcollection["intervaltime"].ToString();

            clienttime = frmcollection["clienttime"].ToString();
            string time = frmcollection["timeforsch"];
            string date = frmcollection["dateforsch"];
            var files = Request.Files.Count;
            dynamic fi = Request.Files["files"];

            string filepath = string.Empty;
            string imagefile = string.Empty;
            if (Request.Files.Count > 0)
            {
                if (fi != null)
                {
                    var path = Server.MapPath("~/Themes/" + System.Configuration.ConfigurationManager.AppSettings["domain"] + "/Contents/img/upload");


                    filepath = path + "\\" + fi.FileName;
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }
                    fi.SaveAs(filepath);
                }
                imagefile = fi.FileName;
            }


            string response = Helper.SBUtils.PostOnSelectedGroups(SelectedGroupId, title, msg, intrval, clienttime, time, date, filepath);


            return Content(response);

        }

        [HttpPost]
        public ActionResult CommentOnFbGroupPost(string GpPostid, string comment, string Accesstoken)
        {
            try
            {
                Api.Facebook.Facebook _Facebook = new Api.Facebook.Facebook();
                string result = _Facebook.CommentOnFbGroupPost(GpPostid, comment, Accesstoken);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
            }
            return Content("success");
        }
        public ActionResult LikeOnFbGroupPost(string GpPostid, string AcceessToken, string Isliked)
        {
            Api.Facebook.Facebook _Facebook = new Api.Facebook.Facebook();
            string ret = _Facebook.LikeFBGroupPost(GpPostid, AcceessToken, Isliked);
            return Content(ret);
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Domain.Socioboard.Domain;
using Socioboard.Helper;
using System.Web.Script.Serialization;
using Socioboard.App_Start;

namespace Socioboard.Controllers
{
    [Authorize]
    [CustomAuthorize]
    public class UserGroupController : BaseController
    {

        //
        // GET: /UserGroup/

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
            // return View();
        }
        public ActionResult LoadUserGroup()
        {
            return PartialView("_UserGroupPartial");
        }
        public ActionResult LoadGroup()
        {
            User objUser = (User)Session["User"];
            Socioboard.Api.Groups.Groups objApiGroups = new Socioboard.Api.Groups.Groups();
            List<Domain.Socioboard.Domain.Groups> lstgroup = (List<Domain.Socioboard.Domain.Groups>)(new System.Web.Script.Serialization.JavaScriptSerializer().Deserialize(objApiGroups.GetGroupDetailsByUserId(objUser.Id.ToString()), typeof(List<Domain.Socioboard.Domain.Groups>)));
            return PartialView("_GroupPartial", lstgroup);
        }
        public ActionResult LoadGroupInfo(string selectedgroupid)
        {
            return PartialView("_GroupInfoPartial", selectedgroupid);
        }

        public ActionResult LoadGroupUserProfiles()
        {
            Dictionary<SocialProfile, object> dict_Socialprofiled = new Dictionary<SocialProfile, object>();
            dict_Socialprofiled = SBUtils.GetAllUserProfiles();
            return PartialView("_UserProfilesPartial", dict_Socialprofiled);
        }
        public ActionResult AddGroup(string groupname)
        {
            User objUser = (User)Session["User"];
            Socioboard.Api.Groups.Groups objApiGroups = new Socioboard.Api.Groups.Groups();
            //Domain.Socioboard.Domain.Groups objgroup = (Domain.Socioboard.Domain.Groups)(new System.Web.Script.Serialization.JavaScriptSerializer().Deserialize(objApiGroups.AddGroup(groupname, objUser.Id.ToString()), typeof(Domain.Socioboard.Domain.Groups)));
            string ret = objApiGroups.AddGroup(groupname, objUser.Id.ToString());
            return Content(ret);
        }
        public ActionResult connectedprofilestothisgroup(string selectedgroupid)
        {
            Session["selectedgroupid"] = selectedgroupid;
            Dictionary<GroupProfile, object> dict_ProfileConnected = new Dictionary<GroupProfile, object>();

            dict_ProfileConnected = SBUtils.GetProfilesConnectedwithgroup();
            return PartialView("_ProfilesConnectedToGroupPartial", dict_ProfileConnected);
        }
        public ActionResult AddprofileToCurrentGroup(string profileid, string network)
        {
            User objUser = (User)Session["User"];
            string selectedgroupid = Session["selectedgroupid"].ToString();
            Api.GroupProfile.GroupProfile ApiobjGroupProfile = new Api.GroupProfile.GroupProfile();
            ApiobjGroupProfile.AddProfileToGroup(profileid, network, selectedgroupid, objUser.Id.ToString());
            return Content("");
        }
        public ActionResult DeleteprofileFromCurrentGroup(string profileid)
        {
            User objUser = (User)Session["User"];
            string selectedgroupid = Session["selectedgroupid"].ToString();
            Api.GroupProfile.GroupProfile ApiobjGroupProfile = new Api.GroupProfile.GroupProfile();
            ApiobjGroupProfile.DeleteProfileFromGroup(profileid, selectedgroupid, objUser.Id.ToString());
            return Content("");
        }
        public ActionResult DeleteGroup(string Groupid)
        {
            User objUser = (User)Session["User"];
            Api.Groups.Groups ApiobjGroups = new Api.Groups.Groups();
            ApiobjGroups.DeleteGroupById(Groupid, objUser.Id.ToString());
            return Content("");
        }

        public ActionResult PendingUser(string Groupid)
        {
            User objUser = (User)Session["User"];
            string selectedgroupid = Session["selectedgroupid"].ToString();
            Api.Team.Team ApiobjTeam = new Api.Team.Team();
            List<Domain.Socioboard.Domain.Team> lstteam = (List<Domain.Socioboard.Domain.Team>)(new JavaScriptSerializer().Deserialize(ApiobjTeam.GetTeamByStatus(selectedgroupid, objUser.Id.ToString(), "0"), typeof(List<Domain.Socioboard.Domain.Team>)));
            return PartialView("_PendingUserPartial", lstteam);
        }
        public ActionResult AcceptedUser(string Groupid)
        {
            User objUser = (User)Session["User"];
            string selectedgroupid = Session["selectedgroupid"].ToString();
            Api.Team.Team ApiobjTeam = new Api.Team.Team();
            List<Domain.Socioboard.Domain.Team> lstteam = (List<Domain.Socioboard.Domain.Team>)(new JavaScriptSerializer().Deserialize(ApiobjTeam.GetTeamByStatus(selectedgroupid, objUser.Id.ToString(), "1"), typeof(List<Domain.Socioboard.Domain.Team>)));
            return PartialView("_AcceptedUserPartial", lstteam);
        }
        //public ActionResult AddTeamMember(string email)
        //{
        //    string response = string.Empty;

        //    User objUser = (User)Session["User"];
        //    string selectedgroupid = Session["selectedgroupid"].ToString();
        //    Api.Team.Team ApiobjTeam = new Api.Team.Team();
        //    Api.User.User ApiobjUser = new Api.User.User();
        //    User objuserinfo = (User)(new JavaScriptSerializer().Deserialize(ApiobjUser.getUserInfoByEmail(email), typeof(User)));
        //    if (objuserinfo != null)
        //    {
        //        string[] name = objuserinfo.UserName.Split(' ');
        //        string fname = name[0];
        //        string lname = string.Empty;
        //        for (int i = 1; i < name.Length; i++)
        //        {
        //            lname += name[i];
        //        }
        //       response = ApiobjTeam.AddTeam(objuserinfo.Id.ToString(), "0", fname, lname, email, "", selectedgroupid, objUser.EmailId, objUser.UserName);
        //    }
        //    else
        //    {
        //        response = ApiobjTeam.AddTeam(Guid.NewGuid().ToString(), "0", "", "", email, "", selectedgroupid, objUser.EmailId, objUser.UserName);
        //    }
        //    //return Content("_AcceptedUserPartial");
        //    return Content(response);
        //}

        // Edited by Antima[6/11/2014]

        public ActionResult AddTeamMember(string email)
        {
            //string[] arr = new string[]{};
            string SentMails = string.Empty;
            try
            {
                List<string> arr = new List<string>();
                string[] arr1 = new string[] { };
                string NotSentMails = string.Empty;
                User objUser = (User)Session["User"];
                string selectedgroupid = string.Empty;
                if (Session["selectedgroupid"] == null || Session["selectedgroupid"] == "")
                {
                    selectedgroupid = Request.QueryString["groupid"];
                }
                else
                {
                    selectedgroupid = Session["selectedgroupid"].ToString();
                }
                Api.Team.Team ApiobjTeam = new Api.Team.Team();
                Api.User.User ApiobjUser = new Api.User.User();
                if (email.Contains(','))
                {
                    arr = email.Split(',').ToList();
                }
                else
                {
                    //arr[0] = email;
                    arr.Add(email);
                }

                foreach (var item in arr)
                {
                    if (item.Contains(':'))
                    {
                        arr1 = item.Split(':');
                    }

                    string res = "";
                    User objuserinfo = (User)(new JavaScriptSerializer().Deserialize(ApiobjUser.getUserInfoByEmail(arr1[0]), typeof(User)));

                    if (objuserinfo != null)
                    {
                        string[] name = objuserinfo.UserName.Split(' ');
                        string fname = name[0];
                        string lname = string.Empty;
                        for (int i = 1; i < name.Length; i++)
                        {
                            lname += name[i];
                        }

                        res = ApiobjTeam.AddTeam(objuserinfo.Id.ToString(), "0", fname, lname, arr1[0], "", selectedgroupid, objUser.EmailId, objUser.UserName);
                    }
                    else
                    {
                        res = ApiobjTeam.AddTeam(objUser.Id.ToString(), "0", arr1[1], arr1[2], arr1[0], "", selectedgroupid, objUser.EmailId, objUser.UserName);
                    }
                    //SentMails += res + ',';

                    if (!string.IsNullOrEmpty(res) && SentMails != "Something Went Wrong")
                    {
                        Domain.Socioboard.Domain.Team objDomainTeam = (Domain.Socioboard.Domain.Team)new JavaScriptSerializer().Deserialize(res, typeof(Domain.Socioboard.Domain.Team));
                        if (objDomainTeam != null)
                        {
                            SentMails += objDomainTeam.EmailId + ',';
                        }
                    }
                    else
                    {
                        NotSentMails += arr1[0] + ',';
                    }
                }
                SentMails = "{\"SentMails\":" + "\"" + SentMails + "\",\"NotSentMails\":" + "\"" + NotSentMails + "\"}";
            }
            catch (Exception ex)
            {
            }

            return Content(SentMails);
        }

        public ActionResult CheckprofileToCurrentGroup(Guid UserId, string GroupId)
        {
            Api.GroupProfile.GroupProfile ApiobjGroupProfile = new Api.GroupProfile.GroupProfile();
            List<Domain.Socioboard.Domain.GroupProfile> lstGroupProfile = (List<Domain.Socioboard.Domain.GroupProfile>)(new JavaScriptSerializer().Deserialize(ApiobjGroupProfile.GetAllProfilesConnectedWithGroup(UserId.ToString(), GroupId), typeof(List<Domain.Socioboard.Domain.GroupProfile>)));
            if (lstGroupProfile.Count != 0)
            {
                return Content("Success");
            }
            else
            {
                return Content("Failure");
            }

        }

    }
}

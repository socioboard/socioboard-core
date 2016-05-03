using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Domain.Socioboard.Domain;
using Socioboard.Helper;
using System.Web.Script.Serialization;
using Socioboard.App_Start;
using System.Net.Http;
using System.Threading.Tasks;

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
        public async Task<ActionResult> AddGroup(string groupname)
        {
            string accesstoken = string.Empty;
            User objUser = (User)Session["User"];
            string ret = string.Empty;
            //Socioboard.Api.Groups.Groups objApiGroups = new Socioboard.Api.Groups.Groups();
            ////Domain.Socioboard.Domain.Groups objgroup = (Domain.Socioboard.Domain.Groups)(new System.Web.Script.Serialization.JavaScriptSerializer().Deserialize(objApiGroups.AddGroup(groupname, objUser.Id.ToString()), typeof(Domain.Socioboard.Domain.Groups)));
            //string ret = objApiGroups.AddGroup(groupname, objUser.Id.ToString());
            List<KeyValuePair<string, string>> Parameters = new List<KeyValuePair<string, string>>();
            Parameters.Add(new KeyValuePair<string, string>("GroupName", groupname));
            Parameters.Add(new KeyValuePair<string, string>("UserId", objUser.Id.ToString()));
            Parameters.Add(new KeyValuePair<string, string>("Id", Guid.NewGuid().ToString()));

            if (Session["access_token"] != null)
            {
                accesstoken = Session["access_token"].ToString();
            }

            HttpResponseMessage response = await WebApiReq.PostReq("api/ApiGroups/AddGroup", Parameters, "Bearer", accesstoken);
            if (response.IsSuccessStatusCode)
            {
                ret = await response.Content.ReadAsAsync<string>();
            }

            return Content(ret);
        }
        public async Task<ActionResult> connectedprofilestothisgroup(string selectedgroupid)
        {
            Session["selectedgroupid"] = selectedgroupid;
            Dictionary<GroupProfile, object> dict_ProfileConnected = new Dictionary<GroupProfile, object>();

            dict_ProfileConnected = await SBHelper.GetGroupProfilesByGroupId(selectedgroupid);
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
        public async Task<ActionResult> DeleteprofileFromCurrentGroup(string profileid)
        {
            User objUser = (User)Session["User"];
            string selectedgroupid = Session["selectedgroupid"].ToString();
            string output = string.Empty;

            HttpResponseMessage response = await WebApiReq.DelReq("api/ApiGroupProfiles/DeleteProfileFromGroup?profileid=" + profileid + "&groupid=" + selectedgroupid + "&userid=" + objUser.Id, "Bearer", Session["access_token"].ToString());
            if (response.IsSuccessStatusCode)
            {
                output = await response.Content.ReadAsAsync<string>();
            }


            //Api.GroupProfile.GroupProfile ApiobjGroupProfile = new Api.GroupProfile.GroupProfile();
            //ApiobjGroupProfile.DeleteProfileFromGroup(profileid, selectedgroupid, objUser.Id.ToString());
            return Content("");
        }
        public ActionResult DeleteGroup(string Groupid)
        {
            User objUser = (User)Session["User"];
            Api.Groups.Groups ApiobjGroups = new Api.Groups.Groups();
            ApiobjGroups.DeleteGroupById(Groupid, objUser.Id.ToString());
            return Content("");
        }

        public async Task<ActionResult> PendingUser(string Groupid)
        {
            User objUser = (User)Session["User"];
            string selectedgroupid = Session["selectedgroupid"].ToString();
            IEnumerable<Domain.Socioboard.Domain.Groupmembers> lstGroupMembers = new List<Domain.Socioboard.Domain.Groupmembers>();
            HttpResponseMessage response = await WebApiReq.GetReq("api/ApiGroupMembers/GetPendingGroupMembers?GroupId=" + selectedgroupid, "Bearer", Session["access_token"].ToString());
            if (response.IsSuccessStatusCode)
            {
                lstGroupMembers = await response.Content.ReadAsAsync<IEnumerable<Domain.Socioboard.Domain.Groupmembers>>();
            }


            //Api.Team.Team ApiobjTeam = new Api.Team.Team();
            //List<Domain.Socioboard.Domain.Team> lstteam = (List<Domain.Socioboard.Domain.Team>)(new JavaScriptSerializer().Deserialize(ApiobjTeam.GetTeamByStatus(selectedgroupid, objUser.Id.ToString(), "0"), typeof(List<Domain.Socioboard.Domain.Team>)));
            return PartialView("_PendingUserPartial", lstGroupMembers);
        }
        public async Task<ActionResult> AcceptedUser(string Groupid)
        {

            User objUser = (User)Session["User"];
            string selectedgroupid = Session["selectedgroupid"].ToString();
            IEnumerable<Domain.Socioboard.Domain.Groupmembers> lstGroupMembers = new List<Domain.Socioboard.Domain.Groupmembers>();
            HttpResponseMessage response = await WebApiReq.GetReq("api/ApiGroupMembers/GetAcceptedGroupMembers?GroupId=" + selectedgroupid, "Bearer", Session["access_token"].ToString());
            if (response.IsSuccessStatusCode)
            {
                lstGroupMembers = await response.Content.ReadAsAsync<IEnumerable<Domain.Socioboard.Domain.Groupmembers>>();
            }


            return PartialView("_AcceptedUserPartial", lstGroupMembers);
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

        public async Task<ActionResult> AddTeamMember(string email)
        {
            string selectedgroupid = string.Empty;
            string SentMails = string.Empty;
            User objUser = (User)Session["user"];
            if (Session["selectedgroupid"] == null || Session["selectedgroupid"] == "")
            {
                selectedgroupid = Request.QueryString["groupid"];
            }
            else
            {
                selectedgroupid = Session["selectedgroupid"].ToString();
            }

            HttpResponseMessage response = await WebApiReq.GetReq("api/ApiGroupMembers/AddGroupMembers?Emails=" + email + "&GroupId=" + selectedgroupid + "&UserId=" + objUser.Id, "Bearer", Session["access_token"].ToString());
            if (response.IsSuccessStatusCode)
            {
                SentMails = await response.Content.ReadAsAsync<string>();
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

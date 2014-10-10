using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Domain.Socioboard.Domain;
using Socioboard.Helper;
using System.Web.Script.Serialization;

namespace Socioboard.Controllers
{
    public class UserGroupController : Controller
    {
        //
        // GET: /UserGroup/

        public ActionResult Index()
        {
            return View();
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
            Domain.Socioboard.Domain.Groups objgroup = (Domain.Socioboard.Domain.Groups)(new System.Web.Script.Serialization.JavaScriptSerializer().Deserialize(objApiGroups.AddGroup(groupname, objUser.Id.ToString()), typeof(Domain.Socioboard.Domain.Groups)));
            return Content(objgroup.Id.ToString());
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
        public ActionResult AddTeamMember(string email)
        {
            User objUser = (User)Session["User"];
            string selectedgroupid = Session["selectedgroupid"].ToString();
            Api.Team.Team ApiobjTeam = new Api.Team.Team();
            Api.User.User ApiobjUser = new Api.User.User();
            User objuserinfo = (User)(new JavaScriptSerializer().Deserialize(ApiobjUser.getUserInfoByEmail(email), typeof(User)));
            if (objuserinfo != null)
            {
                string[] name = objuserinfo.UserName.Split(' ');
                string fname = name[0];
                string lname = string.Empty;
                for (int i = 1; i < name.Length; i++)
                {
                    lname += name[i];
                }
                ApiobjTeam.AddTeam(objuserinfo.Id.ToString(), "0", fname, lname, email, "", selectedgroupid, objUser.EmailId, objUser.UserName);
            }
            else
            {
                ApiobjTeam.AddTeam(Guid.NewGuid().ToString(), "0", "", "", email, "", selectedgroupid, objUser.EmailId, objUser.UserName);
            }
            return Content("_AcceptedUserPartial");
        }

    }
}

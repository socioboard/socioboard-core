using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Newtonsoft.Json.Linq;
using GlobusTwitterLib.Authentication;

using GlobusLinkedinLib.Authentication;
using GlobusInstagramLib.Instagram.Core;
using GlobusInstagramLib.Instagram.Core;
using GlobusTwitterLib.Twitter.Core.UserMethods;
using System.Configuration;
using SocioBoard.Model;
using SocioBoard.Domain;

namespace SocialSuitePro.Settings
{
    public partial class AjaxInsertGroup : System.Web.UI.Page
    {

        protected void page_load(object sender, EventArgs e)
        {
            ProcessRequest();
        }


        public void ProcessRequest()
        {
           TeamRepository objTeamRepository = new TeamRepository();
           TeamMemberProfileRepository objTeamMemberProfileRepository=new TeamMemberProfileRepository();
           FacebookAccountRepository fbaccountrepo = new FacebookAccountRepository();
           TwitterAccountRepository twtaccountrepo = new TwitterAccountRepository();
           LinkedInAccountRepository linkedaccrepo = new LinkedInAccountRepository();
           InstagramAccountRepository instagramrepo = new InstagramAccountRepository();
           GroupProfileRepository groupprofilerepo = new GroupProfileRepository();
           BusinessSettingRepository objbsnsrepo = new BusinessSettingRepository();
           TumblrAccountRepository tumblrrepo = new TumblrAccountRepository();





            User user = (User)Session["LoggedUser"];
            if (Request.QueryString["op"] != null)
            {

                if (Request.QueryString["op"] == "SaveGroupName")
                {
                    string groupName = Request.QueryString["groupname"];
                    GroupRepository grouprepo = new GroupRepository();
                    Groups group = new Groups();
                    group.Id = Guid.NewGuid();
                    group.GroupName = groupName;
                    group.UserId = user.Id;
                    group.EntryDate = DateTime.Now;
                  
                    if (!grouprepo.checkGroupExists(user.Id, groupName))
                    {
                        
                        grouprepo.AddGroup(group);
                        Groups grou = grouprepo.getGroupDetails(user.Id, groupName);
                        Session["GroupName"] = grou;
                    }
                    else
                    {
                        Groups grou = grouprepo.getGroupDetails(user.Id, groupName);
                        Session["GroupName"] = grou;
                    }
                }
                else if (Request.QueryString["op"] == "bindGroupProfiles")
                {
                    string bindprofiles = string.Empty;
                    Guid groupid = Guid.Parse(Request.QueryString["groupId"]);
                    Session["GroupId"] = groupid;
                    GroupProfileRepository groupprofilesrepo = new GroupProfileRepository();
                    List<GroupProfile> lstgroupprofile = groupprofilesrepo.getAllGroupProfiles(user.Id, groupid);
                    foreach (GroupProfile item in lstgroupprofile)
                    {
                        if (item.ProfileType == "facebook")
                        {
                           
                            FacebookAccount account = fbaccountrepo.getFacebookAccountDetailsById(item.ProfileId, user.Id);
                            if (account != null)
                            {
                                bindprofiles += "<div id=\"facebook_" + item.ProfileId + "\" class=\"ws_conct\"> <span class=\"img\"><img width=\"48\" height=\"48\" src=\"http://graph.facebook.com/" + item.ProfileId + "/picture?type=small\" alt=\"\"><i><img width=\"16\" height=\"16\" src=\"../Contents/img/fb_icon.png\" alt=\"\"></i></span><div class=\"fourfifth\">" +
                                    "<div class=\"location-container\">" + account.FbUserName + "</div><span onclick=\"AddProfileInInviteTeamMember('" + account.FbUserId + "','" + groupid + "','" + item.ProfileType + "')\" class=\"add remove\">+</span><span onclick=\"RemoveProfileFromGroup('" + item.ProfileId + "')\" class=\"add remove\">✖</span></div></div>";
                            }
                        }
                        else if (item.ProfileType == "twitter")
                        {
                            TwitterAccount twtaccount = twtaccountrepo.getUserInformation(user.Id, item.ProfileId);
                            string profileimgurl = string.Empty;
                            if (twtaccount != null)
                            {
                            if (twtaccount.ProfileImageUrl == string.Empty)
                            {
                                profileimgurl = "../../Contents/img/blank_img.png";
                            }
                            else
                            {
                                profileimgurl = twtaccount.ProfileImageUrl;
                            }
                          
                                bindprofiles +=
                                       "<div id=\"twitter_" + item.ProfileId + "\" class=\"ws_conct active\"> <span class=\"img\"><img width=\"48\" height=\"48\" src=\"" + profileimgurl + "\" alt=\"\"><i><img width=\"16\" height=\"16\" src=\"../Contents/img/twticon.png\" alt=\"\"></i></span><div class=\"fourfifth\">" +
                                       "<div class=\"location-container\">" + twtaccount.TwitterScreenName + "</div><span onclick=\"AddProfileInInviteTeamMember('" + twtaccount.TwitterUserId + "','"+groupid+"','" + item.ProfileType + "')\" class=\"add remove\">+</span><span onclick=\"RemoveProfileFromGroup('" + item.ProfileId + "')\"  class=\"add remove\">✖</span></div></div>";
                            }
                        }
                        else if (item.ProfileType == "linkedin")
                        {                            
                            LinkedInAccount linkedaccount = linkedaccrepo.getUserInformation(user.Id, item.ProfileId);
                            string profileimgurl = string.Empty;
                            if (linkedaccount != null)
                            {
                                if (linkedaccount.ProfileUrl == string.Empty)
                                {
                                    profileimgurl = "../../Contents/img/blank_img.png";
                                }
                                else
                                {
                                    profileimgurl = linkedaccount.ProfileImageUrl;
                                }
                                bindprofiles += "<div id=\"linkedin_" + item.ProfileId + "\" class=\"ws_conct active\"><span class=\"img\"><img width=\"48\" height=\"48\" alt=\"\" src=\"" + profileimgurl + "\" ><i>" +
                                                 "<img width=\"16\" height=\"16\" alt=\"\" src=\"../Contents/img/link_icon.png\"></i></span>" +
                                                 "<div class=\"fourfifth\"><div class=\"location-container\">" + linkedaccount.LinkedinUserName + "</div>" +
                                                 "<span onclick=\"AddProfileInInviteTeamMember('" + linkedaccount.LinkedinUserId + "','" + groupid + "','" + item.ProfileType + "')\" class=\"add remove\">+</span><span onclick=\"RemoveProfileFromGroup('" + item.ProfileId + "')\" class=\"add remove\">✖</span></div></div>";
                            }
                        }

                        else if (item.ProfileType == "tumblr")
                        {
                            TumblrAccount tumblraccount = tumblrrepo.getTumblrAccountDetailsById(item.ProfileId,user.Id);
                            string profileimgurl = string.Empty;
                            if (tumblraccount != null)
                            {
                                if (tumblraccount.tblrProfilePicUrl == string.Empty)
                                {
                                    profileimgurl = "../../Contents/img/blank_img.png";
                                }
                                else
                                {
                                    profileimgurl = "http://api.tumblr.com/v2/blog/" + tumblraccount.tblrUserName + ".tumblr.com/avatar"; 
                                }
                                bindprofiles += "<div id=\"tumblr_" + item.ProfileId + "\" class=\"ws_conct active\"><span class=\"img\"><img width=\"48\" height=\"48\" alt=\"\" src=\"http://api.tumblr.com/v2/blog/" + tumblraccount.tblrUserName + ".tumblr.com/avatar\" ><i>" +
                                                 "<img width=\"16\" height=\"16\" alt=\"\" src=\"../Contents/img/tumblr.png\"></i></span>" +
                                                 "<div class=\"fourfifth\"><div class=\"location-container\">" +tumblraccount.tblrUserName + "</div>" +
                                                 "<span onclick=\"AddProfileInInviteTeamMember('" + tumblraccount.tblrUserName + "','" + groupid + "','" + item.ProfileType + "')\" class=\"add remove\">+</span><span onclick=\"RemoveProfileFromGroup('" + item.ProfileId + "')\" class=\"add remove\">✖</span></div></div>";
                            }
                        }



                        else if (item.ProfileType == "instagram")
                        {
                            string profileimgurl = string.Empty;
                            
                            InstagramAccount instaaccount = instagramrepo.getInstagramAccountDetailsById(item.ProfileId, user.Id);
                            if (instaaccount != null)
                            {
                                if (instaaccount.ProfileUrl == string.Empty)
                                {
                                    profileimgurl = "../../Contents/img/blank_img.png";
                                }
                                else
                                {
                                    profileimgurl = instaaccount.ProfileUrl;
                                }

                                bindprofiles += "<div id=\"instagram_" + item.ProfileId + "\" class=\"ws_conct active\"><span class=\"img\"><img width=\"48\" height=\"48\" src=\"" + profileimgurl + "\" alt=\"\"><i>" +
                                                  "<img width=\"16\" height=\"16\" alt=\"\" src=\"../Contents/img/instagram_24X24.png\"></i></span><div class=\"fourfifth\"><div class=\"location-container\">" + instaaccount.InsUserName + "</div>" +
                                    "<span onclick=\"AddProfileInInviteTeamMember('" + instaaccount.InstagramId + "','" + groupid + "','" + item.ProfileType + "')\" class=\"add remove\">+</span><span onclick=\"RemoveProfileFromGroup('" + item.ProfileId + "')\" class=\"add remove\">✖</span></div></div>";
                            }
                        }
                   }
                    Response.Write(bindprofiles);

                }
                else if (Request.QueryString["op"] == "deleteGroupName")
                {
                    Guid groupid = Guid.Parse(Request.QueryString["groupId"]);
                 
                    GroupRepository grouprepo = new GroupRepository();
                    grouprepo.DeleteGroup(groupid);                  
                    int count = groupprofilerepo.DeleteAllGroupProfile(groupid);
                    int cnt = objbsnsrepo.DeleteBusinessSettingByUserid(groupid);

                    List<Team> objTeamId = objTeamRepository.getAllDetailsUserEmail(groupid);
                    foreach (Team item in objTeamId)
                    {
                        int deteleTeamMember = objTeamMemberProfileRepository.deleteTeamMember(item.Id);
                        
                    }
                    int deleteTeam = objTeamRepository.deleteGroupRelatedTeam(groupid);

                }
                else if (Request.QueryString["op"] == "addProfilestoGroup")
                {
                    string network = Request.QueryString["network"];
                    string id = Request.QueryString["profileid"];
                    Guid groupid = (Guid)Session["GroupId"];
                    GroupProfile groupprofile = new GroupProfile();
                    groupprofile.EntryDate = DateTime.Now;
                    groupprofile.GroupId = groupid;
                    groupprofile.Id = Guid.NewGuid();
                    groupprofile.ProfileId = id;
                    groupprofile.ProfileType = network;
                    groupprofile.GroupOwnerId = user.Id;

                    GroupProfileRepository grouprepo = new GroupProfileRepository();

                    if (!grouprepo.checkGroupProfileExists(user.Id, groupid, id))
                    {
                        grouprepo.AddGroupProfile(groupprofile);
                    }

                    Response.Write(groupid);

                }
                else if (Request.QueryString["op"] == "deleteGroupProfiles")
                {
                    Guid groupid = (Guid)Session["GroupId"];
                    try
                    {
                        string profileid = Request.QueryString["profileid"];
                        GroupProfileRepository grouprepo = new GroupProfileRepository();
                        grouprepo.DeleteGroupProfile(user.Id, profileid, groupid);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                    }
                    Response.Write(groupid);
                }

                if (Request.QueryString["op"] == "GetInviteMember")
                {
                    string bindprofiles = string.Empty;
                    string profileimgurl = string.Empty;

                    try
                    {
                        string gp = Request.QueryString["groupId"];
                        Guid GroupId = Guid.Parse(gp);
                       // TeamRepository objTeamRepository = new TeamRepository();
                        List<Team> objTeam = objTeamRepository.getAllDetailsUserEmail(GroupId);
                      
                        if (objTeam.Count != 0)
                        {
                            foreach (Team item in objTeam)
                            {
                                UserRepository objUserRepository = new UserRepository();
                                User ObjUserDetails = objUserRepository.getUserInfoByEmail(item.EmailId);
                                if (ObjUserDetails != null)
                                {
                                    if (string.IsNullOrEmpty(ObjUserDetails.ProfileUrl))
                                    {
                                        profileimgurl = "../../Contents/img/blank_img.png";
                                    }
                                    else
                                    {
                                        profileimgurl = ObjUserDetails.ProfileUrl;
                                    }

                                    bindprofiles += "<div style=\"float:left; margin-right:18%\"id=\"" + item.Id + "\">" +
                                                 "<div style=\"float:left\"><span class=\"img\"><img width=\"48\" height=\"48\" src=\"" + profileimgurl + "\" alt=\"\"></span>" +
                                                "</div><div style=\"float:left\" class=\"fourfifth\"><div style=\"font-size:small \">" + ObjUserDetails.UserName + "</div> </div><div style=\"float:left;margin-left:3px\" onclick=\"ShowInviteMemberProfileDetails('" + GroupId + "','" + ObjUserDetails.EmailId + "','" + user.Id + "')\"><input class=\"abc\" type=\"radio\" name=\"sex\" value=" + item.Id + "></div>" +
                                                "<span onclick=\"RemoveInviteMemberFromGroup('" + item.Id + "')\" style=\"margin-left:25px;font-size:small;cursor:pointer;position: absolute; margin-top: 28px;margin-left:10px\">x</span></div>";

                                    //bindprofiles += "<div id=\"" + item.Id + "\" class=\"ws_conct active\"> <span class=\"img\"><img width=\"48\" height=\"48\" src=\"" + profileimgurl + "\" alt=\"\"><i><img width=\"16\" height=\"16\" src=\"../Contents/img/twticon.png\" alt=\"\"></i></span><div class=\"fourfifth\">" +
                                    //  "<div class=\"location-container\">" + ObjUserDetails.UserName + "</div><span class=\"add remove\" onclick=\"ShowInviteMemberProfileDetails('" + GroupId + "','" + ObjUserDetails.EmailId + "','" + user.Id + "')\"><input class=\"abc\" type=\"radio\" name=\"sex\" value=" + item.Id + "></span><span onclick=\"RemoveInviteMemberFromGroup('" + item.Id + "')\"  class=\"add remove\">✖</span></div></div>";
                                    
                                    
                                    
                                                                                                                                                                                                  
                                }
                            }
                        }

                        Response.Write(bindprofiles);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);

                    }
                }

                if (Request.QueryString["op"] == "RemoveInviteMemberFromGroup")
                {
                    if (!string.IsNullOrEmpty(Request.QueryString["Id"]))
                    {
                        try
                        {
                            string ide = Request.QueryString["Id"];
                            Guid id = Guid.Parse(ide);
                            int deleteTeam = objTeamRepository.deleteinviteteamMember(id);
                            int deleteProfiles = objTeamMemberProfileRepository.DeleteTeamMemberProfileByTeamId(id);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.StackTrace);

                        }
                    }
                }

                //modified by hozefa 4-7-2014   
                if (Request.QueryString["op"] == "ShowInviteMemberProfileDetails")
                {
                    string bindprofiles = string.Empty;
                    string gpId = Request.QueryString["groupId"];
                    Guid gpid = Guid.Parse(gpId);
                    string emailId = Request.QueryString["emailid"];
                    string userId = Request.QueryString["userid"];

                    Team teamdata = objTeamRepository.getAllDetails(gpid, emailId);

                    List<TeamMemberProfile> objTeamMemProfile = objTeamMemberProfileRepository.getAllTeamMemberProfilesOfTeam(teamdata.Id);
                    try
                    {
                        foreach (TeamMemberProfile item in objTeamMemProfile)
                        {
                            if (item.ProfileType == "facebook")
                            {

                                FacebookAccount account = fbaccountrepo.getFacebookAccountDetailsById(item.ProfileId);
                                if (account != null)
                                {
                                    bindprofiles += "<div id=\"item\" style=\"float:left;width:170px;margin-top:6px\"  id=\"facebook_" + item.ProfileId + "\"><div style=\"float:left\"<span class=\"img\">" +
                                             "<img width=\"48\" height=\"48\" src=\"http://graph.facebook.com/" + item.ProfileId + "/picture?type=small\" alt=\"\"></img><i><img style=\"margin-left:-18px\" width=\"16\" height=\"16\" src=\"../Contents/img/fb_icon.png\" alt=\"\"></img></i>" +
                                             "</span></div><div style=\"float:left\"><div style=\"font-size:small\">" + account.FbUserName + "</div></div>" +
                                              "<span  onclick=\"RemoveInviteMemberProfileFromTeamMember('" + teamdata.Id + "','" + item.ProfileId + "','" + gpId + "','" + emailId + "','" + userId + "')\" style=\"margin-left:25px;font-size:small;cursor:pointer;position: absolute; margin-top: 28px;margin-left:10px\">x</span></div>";
                                }
                            }
                            else if (item.ProfileType == "twitter")
                            {
                                TwitterAccount twtaccount = twtaccountrepo.getUserInformation(item.ProfileId);
                                string profileimgurl = string.Empty;
                                if (twtaccount != null)
                                {
                                    if (twtaccount.ProfileImageUrl == string.Empty)
                                    {
                                        profileimgurl = "../../Contents/img/blank_img.png";
                                    }
                                    else
                                    {
                                        profileimgurl = twtaccount.ProfileImageUrl;
                                    }

                                    bindprofiles += "<div id=\"item\" style=\"float:left; width:170px;margin-top:6px\"   id=\"twitter_" + item.ProfileId + "\"><div style=\"float:left\"<span class=\"img\">" +
                                             "<img width=\"48\" height=\"48\" src=\"" + profileimgurl + "\" alt=\"\"></img><i><img style=\"margin-left:-18px\" width=\"16\" height=\"16\" src=\"../Contents/img/twticon.png\" alt=\"\"></img></i>" +
                                             "</span></div><div style=\"float:left\"><div style=\"font-size:small\">" + twtaccount.TwitterScreenName + "</div></div>" +
                                              "<span onclick=\"RemoveInviteMemberProfileFromTeamMember('" + teamdata.Id + "','" + item.ProfileId + "','" + gpId + "','" + emailId + "','" + userId + "')\" style=\"margin-left:25px;font-size:small;cursor:pointer;position: absolute; margin-top: 28px;margin-left:10px\">x</span></div>";
                                }
                            }

                            else if (item.ProfileType == "linkedin")
                            {

                                LinkedInAccount linkedaccount = linkedaccrepo.getUserInformation(item.ProfileId);
                                string profileimgurl = string.Empty;
                                if (linkedaccount != null)
                                {
                                    if (linkedaccount.ProfileUrl == string.Empty)
                                    {
                                        profileimgurl = "../../Contents/img/blank_img.png";
                                    }
                                    else
                                    {
                                        profileimgurl = linkedaccount.ProfileImageUrl;
                                    }

                                    bindprofiles += "<div id=\"item\" style=\"float:left;width:170px;margin-top:6px\"   id=\"linkedin_" + item.ProfileId + "\"><div style=\"float:left\"<span class=\"img\">" +
                                              "<img width=\"48\" height=\"48\" src=\"" + profileimgurl + "\" alt=\"\"></img><i><img style=\"margin-left:-18px\" width=\"16\" height=\"16\" src=\"../Contents/img/link_icon.png\" alt=\"\"></img></i>" +
                                              "</span></div><div style=\"float:left\"><div style=\"font-size:small\">" + linkedaccount.LinkedinUserName + "</div></div>" +
                                               "<span onclick=\"RemoveInviteMemberProfileFromTeamMember('" + teamdata.Id + "','" + item.ProfileId + "','" + gpId + "','" + emailId + "','" + userId + "')\" style=\"margin-left:25px;font-size:small;cursor:pointer;position: absolute; margin-top: 28px;margin-left:10px\">x</span></div>";
                                }
                            }

                            else if (item.ProfileType == "instagram")
                            {
                                string profileimgurl = string.Empty;

                                InstagramAccount instaaccount = instagramrepo.getInstagramAccountDetailsById(item.ProfileId);
                                if (instaaccount != null)
                                {
                                    if (instaaccount.ProfileUrl == string.Empty)
                                    {
                                        profileimgurl = "../../Contents/img/blank_img.png";
                                    }
                                    else
                                    {
                                        profileimgurl = instaaccount.ProfileUrl;
                                    }

                                    bindprofiles += "<div id=\"item\" style=\"float:left;width:170px; margin-top:6px\"   id=\"instagram_" + item.ProfileId + "\"><div style=\"float:left\"<span class=\"img\">" +
                                             "<img width=\"48\" height=\"48\" src=\"" + profileimgurl + "\" alt=\"\"></img><i><img style=\"margin-left:-18px\" width=\"16\" height=\"16\" src=\"../Contents/img/instagram_24X24.png\" alt=\"\"></img></i>" +
                                             "</span></div><div style=\"float:left\"><div style=\"font-size:small\">" + instaaccount.InsUserName + "</div></div>" +
                                              "<span onclick=\"RemoveInviteMemberProfileFromTeamMember('" + teamdata.Id + "','" + item.ProfileId + "','" + gpId + "','" + emailId + "','" + userId + "')\" style=\"margin-left:25px;font-size:small;cursor:pointer;position: absolute; margin-top: 28px;margin-left:10px\">x</span></div>";
                                }
                            }



                            else if (item.ProfileType == "tumblr")
                            {
                                string profileimgurl = string.Empty;

                                TumblrAccount tumblraccount = tumblrrepo.getTumblrAccountDetailsById(item.ProfileId);
                                if (tumblraccount != null)
                                {
                                    if (tumblraccount.tblrProfilePicUrl == string.Empty)
                                    {
                                        profileimgurl = "../../Contents/img/blank_img.png";
                                    }
                                    else
                                    {
                                        profileimgurl = "http://api.tumblr.com/v2/blog/" + tumblraccount.tblrUserName + ".tumblr.com/avatar";
                                    }

                                    bindprofiles += "<div id=\"item\" style=\"float:left;width:170px; margin-top:6px\"   id=\"tumblr_" + item.ProfileId + "\"><div style=\"float:left\"<span class=\"img\">" +
                                             "<img width=\"48\" height=\"48\" src=\"" + profileimgurl + "\" alt=\"\"></img><i><img style=\"margin-left:-18px\" width=\"16\" height=\"16\" src=\"../Contents/img/tumblr.png\" alt=\"\"></img></i>" +
                                             "</span></div><div style=\"float:left\"><div style=\"font-size:small\">" +tumblraccount.tblrUserName + "</div></div>" +
                                              "<span onclick=\"RemoveInviteMemberProfileFromTeamMember('" + teamdata.Id + "','" + item.ProfileId + "','" + gpId + "','" + emailId + "','" + userId + "')\" style=\"margin-left:25px;font-size:small;cursor:pointer;position: absolute; margin-top: 28px;margin-left:10px\">x</span></div>";
                                }
                            }





                        }
                    }
                    catch (Exception ex)
                    {

                        Console.WriteLine(ex.StackTrace);
                    }

                    Response.Write(bindprofiles);

                }


                if (Request.QueryString["op"] == "RemoveInviteMemberProfileFromTeamMember")
                {
                    string profileId = Request.QueryString["ProfileId"];
                    Guid teamid = Guid.Parse(Request.QueryString["TeamId"]);
                    try
                    {
                        int deleteTeamMembeProfile = objTeamMemberProfileRepository.DeleteTeamMemberProfileByTeamIdProfileId(profileId,teamid);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                    }                               
                }

                if (Request.QueryString["op"] == "AddProfileInInviteTeamMember")
                {
                    try
                    {
                        string EmailId = string.Empty;
                        string Result = string.Empty;
                        TeamMemberProfile objteam = new TeamMemberProfile();
                        objteam.ProfileId = Request.QueryString["Profileid"];
                        objteam.ProfileType = Request.QueryString["Profiletype"];
                        string GrpId = Request.QueryString["Groupid"];
                        Guid grpid = Guid.Parse(GrpId);

                        TeamRepository objTeamrepo = new TeamRepository();
                        Team team = new Team();
                        Guid id = Guid.NewGuid();
                        objteam.Id = id;
                        string teamid = Request.QueryString["Teamid"];
                        objteam.TeamId = Guid.Parse(teamid);
                        objteam.StatusUpdateDate = DateTime.Now;
                        objteam.Status = 0;
                        team = objTeamrepo.getAllDetailsByTeamID(objteam.TeamId, grpid);
                        EmailId = team.EmailId;
                        try
                        {
                            if (!objTeamMemberProfileRepository.checkTeamMemberProfile(objteam.TeamId, objteam.ProfileId))
                            {
                                objTeamMemberProfileRepository.addNewTeamMember(objteam);
                                Result = "Success";
                            }
                            else
                            {
                                //ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('This Profile Already Added.');", true);
                                Result = "Fail";
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.StackTrace);
                        }
                        Response.Write(Result + "_" + EmailId);
                    }
                    catch (Exception ex)
                    {

                        Console.WriteLine(ex.StackTrace);
                    }                  
                }                    
            }
        }
    }
}
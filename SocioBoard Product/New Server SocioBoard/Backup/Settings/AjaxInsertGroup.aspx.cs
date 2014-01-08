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
                            FacebookAccountRepository fbaccountrepo = new FacebookAccountRepository();
                            FacebookAccount account = fbaccountrepo.getFacebookAccountDetailsById(item.ProfileId, user.Id);
                            if (account != null)
                            {
                                bindprofiles += "<div id=\"facebook_" + item.ProfileId + "\" class=\"ws_conct\"> <span class=\"img\"><img width=\"48\" height=\"48\" src=\"http://graph.facebook.com/" + item.ProfileId + "/picture?type=small\" alt=\"\"><i><img width=\"16\" height=\"16\" src=\"../Contents/img/fb_icon.png\" alt=\"\"></i></span><div class=\"fourfifth\">" +
                                    "<div class=\"location-container\">" + account.FbUserName + "</div><span onclick=\"RemoveProfileFromGroup('" + item.ProfileId + "')\" class=\"add remove\">✖</span></div></div>";
                            }
                        }
                        else if (item.ProfileType == "twitter")
                        {
                            TwitterAccountRepository twtaccountrepo = new TwitterAccountRepository();
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
                                       "<div class=\"location-container\">" + twtaccount.TwitterScreenName + "</div><span onclick=\"RemoveProfileFromGroup('" + item.ProfileId + "')\"  class=\"add remove\">✖</span></div></div>";
                            }
                        }
                        else if (item.ProfileType == "linkedin")
                        {
                            LinkedInAccountRepository linkedaccrepo = new LinkedInAccountRepository();
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
                                    profileimgurl = linkedaccount.ProfileUrl;
                                }
                                bindprofiles += "<div id=\"linkedin_" + item.ProfileId + "\" class=\"ws_conct active\"><span class=\"img\"><img width=\"48\" height=\"48\" alt=\"\" src=\"" + profileimgurl + "\" ><i>" +
                                                 "<img width=\"16\" height=\"16\" alt=\"\" src=\"../Contents/img/link_icon.png\"></i></span>" +
                                                 "<div class=\"fourfifth\"><div class=\"location-container\">" + linkedaccount.LinkedinUserName + "</div>" +
                                                 "<span onclick=\"RemoveProfileFromGroup('" + item.ProfileId + "')\" class=\"add remove\">✖</span></div></div>";
                            }
                        }
                        else if (item.ProfileType == "instagram")
                        {
                            string profileimgurl = string.Empty;
                            InstagramAccountRepository instagramrepo = new InstagramAccountRepository();
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
                                    "<span onclick=\"RemoveProfileFromGroup('" + item.ProfileId + "')\" class=\"add remove\">✖</span></div></div>";
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
                    GroupProfileRepository groupprofilerepo = new GroupProfileRepository();
                    int count = groupprofilerepo.DeleteAllGroupProfile(groupid);

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
                }
                else if (Request.QueryString["op"] == "deleteGroupProfiles")
                {
                    Guid groupid = (Guid)Session["GroupId"];
                    string profileid = Request.QueryString["profileid"];
                    GroupProfileRepository grouprepo = new GroupProfileRepository();
                    grouprepo.DeleteGroupProfile(user.Id, profileid,groupid);

                }
            }

        }
    }
}
using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SocioBoard.Domain;
using System.Configuration;
using Facebook;
using SocioBoard.Model;
using SocioBoard.Helper;
using GlobusLinkedinLib.Authentication;
using GlobusLinkedinLib.App.Core;

namespace SocialScoup.Group
{
    public partial class Group : System.Web.UI.Page
    {
        FacebookAccountRepository fbAccRepo = new FacebookAccountRepository();
        LinkedInAccountRepository linkedrepo = new LinkedInAccountRepository();
        public string groupname = string.Empty;
        
        string leftsidedata = string.Empty;
    
        protected void Page_Load(object sender, EventArgs e)
        {
            
            try
            {
                User user = (User)Session["LoggedUser"];
                SocioBoard.Domain.Team team = (SocioBoard.Domain.Team)Session["GroupName"];
                try
                {
                    #region for You can use only 30 days as Unpaid User

                    if (user.PaymentStatus.ToLower() == "unpaid")
                    {
                        if (!SBUtils.IsUserWorkingDaysValid(user.ExpiryDate))
                        {
                           Session["GreaterThan30Days"] = "GreaterThan30Days";
                           Response.Redirect("../Settings/Billing.aspx");
                        }
                    }
                    #endregion
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error : " + ex.StackTrace);
                }

                //if (!IsPostBack)
                //{                   
                     string profileid=string.Empty;
                     string LinkedINprofileid = string.Empty;
                      if (user == null)
                        Response.Redirect("/Default.aspx");

                    SocioBoard.Domain.FacebookAccount objFacebookAccount;
                    TeamMemberProfileRepository objTeamMemberProfileRepository = new TeamMemberProfileRepository();
                    List<TeamMemberProfile> allprofiles = objTeamMemberProfileRepository.getTeamMemberProfileData(team.Id);
                    List<TeamMemberProfile> allLinkedInprofiles = objTeamMemberProfileRepository.getLinkedInTeamMemberProfileData(team.Id);
                    oAuthLinkedIn objoAuthLinkedIn = new oAuthLinkedIn();
                    try
                    {
                        if (allLinkedInprofiles.Count != 0)
                        {
                            foreach (TeamMemberProfile item in allLinkedInprofiles)
                            {
                                LinkedINprofileid += item.ProfileId + ',';
                            }
                            LinkedINprofileid = LinkedINprofileid.Substring(0, LinkedINprofileid.Length - 1);
                            List<LinkedInAccount> arrLinkedInAccount = linkedrepo.getAllAccountDetail(LinkedINprofileid);

                            foreach (var item in arrLinkedInAccount)
                            {
                                objoAuthLinkedIn.Token = item.OAuthToken;
                                objoAuthLinkedIn.Verifier = item.OAuthVerifier;
                                objoAuthLinkedIn.TokenSecret = item.OAuthSecret;

                                //if (item.LinkedinUserName.Length > 15)
                                //{
                                //    item.LinkedinUserName = item.LinkedinUserName.Substring(0, 14);
                                //    item.LinkedinUserName = item.LinkedinUserName + "..";
                                //}
                                //else
                                //{
                                //    item.LinkedinUserName = item.LinkedinUserName;
                                //}

                                leftsidedata += "<div class=\"accordion-group\"><div class=\"accordion-heading\">"
                                    + "<a href=\"#" + item.Id + "\" data-parent=\"#accordion2\" data-toggle=\"collapse\" class=\"accordion-toggle\">"
                                       + "<img width=\"19\" class=\"fesim\" src=\"" + item.ProfileImageUrl + "\" /><span class=\"groupname\">" + item.LinkedinUserName + " </span><i class=\"icon-sort-down pull-right hidden\">"
                                        + "</i></a></div><div id=\"" + item.Id + "\" class=\"accordion-body collapse\" ><div class=\"accordion-inner\"><ul>";
                                List<GlobusLinkedinLib.App.Core.LinkedInGroup.Group_Updates> lstlinkedinGroup = GetGroupsName(objoAuthLinkedIn);
                                if (lstlinkedinGroup.Count == 0)
                                {
                                    leftsidedata += "<li><a  linkedInUserId=\"" + item.LinkedinUserId + "\">No Group Found</a> </li>";
                                }
                                else
                                {
                                    foreach (var item1 in lstlinkedinGroup)
                                    {
                                        leftsidedata += "<li class=\"pull-left\"> <input style=\"float: left;\" type=\"checkbox\" id=\"" + item.LinkedinUserId + "_lin_" + item1.id + "\" value=\"lin_" + item.LinkedinUserId + "\"><a gid=\"" + item1.id + "\" onclick=\"linkedingroupdetails('" + item1.id + "','" + item.LinkedinUserId + "');\" linkedInUserId=\"" + item.LinkedinUserId + "\" style=\"margin-left: 20px;\" href=\"#\">" + item1.GroupName + "</a> </li>";
                                    }
                                }
                                leftsidedata += "</ul></div></div></div>";
                            }

                        }


                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                    }

                    if (allprofiles.Count!=0)
                    {
                        foreach (TeamMemberProfile item in allprofiles)
                        {
                            profileid += item.ProfileId + ',';
                        }
                        profileid = profileid.Substring(0, profileid.Length - 1);
                        List<FacebookAccount> arrFacebookAccount = fbAccRepo.getAllAccountDetail(profileid);

                        foreach (var item in arrFacebookAccount)
                        {
                            objFacebookAccount = new FacebookAccount();
                            objFacebookAccount = (SocioBoard.Domain.FacebookAccount)item;

                            //if (objFacebookAccount.FbUserName.Length > 15)
                            //{
                            //    objFacebookAccount.FbUserName = objFacebookAccount.FbUserName.Substring(0, 14);
                            //    objFacebookAccount.FbUserName = objFacebookAccount.FbUserName + "..";
                            //}
                            //else
                            //{
                            //    objFacebookAccount.FbUserName = objFacebookAccount.FbUserName;
                            //}

                            if (objFacebookAccount.Type != "page")
                            {
                                leftsidedata += "<div class=\"accordion-group\"><div class=\"accordion-heading\">"
                                    + "<a href=\"#" + objFacebookAccount.Id + "\" data-parent=\"#accordion2\" data-toggle=\"collapse\" class=\"accordion-toggle\">"
                                       + "<img width=\"19\" class=\"fesim\" src=\"http://graph.facebook.com/" + objFacebookAccount.FbUserId + "/picture?type=small\" alt=\"\" /><span class=\"groupname\">" + objFacebookAccount.FbUserName + " </span><i class=\"icon-sort-down pull-right hidden\">"
                                        + "</i></a></div><div id=\"" + objFacebookAccount.Id + "\" class=\"accordion-body collapse\" ><div class=\"accordion-inner\"><ul>";

                                List<FacebookGroup> lstFacebookGroup = GetGroupName(objFacebookAccount.AccessToken);
                                if (lstFacebookGroup.Count == 0)
                                {

                                    leftsidedata += "<li><a  fbUserId=\"" + objFacebookAccount.FbUserId + "\">No Group Found</a> </li>";
                                }
                                else
                                {
                                    foreach (var item1 in lstFacebookGroup)
                                    {
                                        //leftsidedata += "<li class=\"grpli\"><a gid=\"" + item1.GroupId + "\" onclick=\"facebookgroupdetails('" + item1.GroupId + "','" + objFacebookAccount.AccessToken + "');\" fbUserId=\"" + objFacebookAccount.FbUserId + "\" href=\"#\">" + item1.Name + "</a> </li>";

                                        leftsidedata += "<li class=\"pull-left\"><input style=\"float: left;\" type=\"checkbox\" id=\"" + objFacebookAccount.FbUserId + "_fb_" + item1.GroupId + "\" value=\"fb_" + objFacebookAccount.AccessToken + "\"><a gid=\"" + item1.GroupId + "\" onclick=\"facebookgroupdetails('" + item1.GroupId + "','" + objFacebookAccount.AccessToken + "');\" fbUserId=\"" + objFacebookAccount.FbUserId + "\" style=\"margin-left: 20px;\" href=\"#\">" + item1.Name + "</a> </li>";
                                    
                                    }
                                }
                                leftsidedata += "</ul></div></div></div>";
                            }
                        }
                    }
                    accordion2.InnerHtml = leftsidedata;

                   
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
            }
        }

        public List<FacebookGroup> GetGroupName(string accesstoken)
        {
            List<FacebookGroup> lstGroupName = new List<FacebookGroup>();
            try
            {
                FacebookClient fb = new FacebookClient();
                fb.AccessToken = accesstoken;
                dynamic groups = fb.Get("me/groups");

                foreach (var item in groups["data"])
                {
                    try
                   {
                        FacebookGroup objFacebookGroup = new FacebookGroup();
                        objFacebookGroup.Name = item["name"].ToString();
                        objFacebookGroup.GroupId = item["id"].ToString();
                        lstGroupName.Add(objFacebookGroup);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
            }

            return lstGroupName;
        }

        public object GetGroupInfo(string accesstoken, string grpId)
        {
            List<FacebookGroup> lstGroupName = new List<FacebookGroup>();

            object objgroupInfo = new object();

            try
            {
                FacebookClient fb = new FacebookClient();
                fb.AccessToken = accesstoken;
                objgroupInfo = fb.Get(grpId);
            }
           
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
            }
            return objgroupInfo;
        }



        public List<GlobusLinkedinLib.App.Core.LinkedInGroup.Group_Updates> GetGroupsName(oAuthLinkedIn objoAuthLinkedIn)
        {
            List<GlobusLinkedinLib.App.Core.LinkedInGroup.Group_Updates> objGroup = new List<GlobusLinkedinLib.App.Core.LinkedInGroup.Group_Updates>();
            LinkedInGroup objLinkedInGroup = new LinkedInGroup();
            objGroup = objLinkedInGroup.GetGroupUpdates(objoAuthLinkedIn, 20);
            return objGroup;
        }


    }
}
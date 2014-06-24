using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Collections;

using System.Configuration;
using SocioBoard.Domain;
using SocioBoard.Model;
using WooSuite.Helper;


namespace WooSuite.Settings
{
    public partial class InviteMember : System.Web.UI.Page
    {
        public static string AccessLevel = string.Empty;
        protected void page_load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                User user = (User)Session["LoggedUser"];

                if (!string.IsNullOrEmpty(Request.QueryString["q"]))
                {
                    Guid groupguid = Guid.Parse(Request.QueryString["q"]);
                    Session["GroupId"] = groupguid;
                }
                if (user == null)
                    Response.Redirect("Home.aspx");
                memberName.Text = user.UserName;
                BindSocialProfiles();
            }
        }

        protected void rbAdmin_CheckedChanged(object sender, EventArgs e)
        {
            rbAdmin.Checked = true;
            rbUser.Checked = false;
            if (rbAdmin.Checked == true && rbUser.Checked == false)
            {
                AccessLevel = "admin";
            }
            else
            {
                AccessLevel = "user";
            }
        }



        protected void rbUser_CheckedChanged(object sender, EventArgs e)
        {
            rbAdmin.Checked = false;
            rbUser.Checked = true;

            if (rbAdmin.Checked == false && rbUser.Checked == true)
            {
                AccessLevel = "user";
            }
            else
            {
                AccessLevel = "admin";
            }
        }

        public void BindSocialProfiles()
        {
            try
            {
                User user = (User)Session["LoggedUser"];

                if (Session["GroupId"] != null)
                {
                    Guid groupid = (Guid)Session["GroupId"];
                    GroupProfileRepository groupprofilesrepo = new GroupProfileRepository();
                    GroupRepository grouprepo = new GroupRepository();
                    Groups groups = grouprepo.getGroupDetailsbyId(user.Id, groupid);

                    List<GroupProfile> lstgroupprofile = groupprofilesrepo.getAllGroupProfiles(user.Id, groupid);

                    string bindfacebookprofiles = string.Empty;
                    string bindtwitterprofiles = string.Empty;
                    string bindlinkedinprofiles = string.Empty;
                    string bindinstagramprofiles = string.Empty;
                    int i = 0;

                    foreach (GroupProfile item in lstgroupprofile)
                    {
                        if (item.ProfileType == "facebook")
                        {
                            try
                            {
                                FacebookAccountRepository fbaccountrepo = new FacebookAccountRepository();
                                FacebookAccount account = fbaccountrepo.getFacebookAccountDetailsById(item.ProfileId, user.Id);

                                bindfacebookprofiles += "<div class=\"ws_tm_network_one\"><div class=\"ws_tm_user_name\">" + account.FbUserName + "</div>" +
                                                      "<div class=\"ws_tm_chkbx\"><input type=\"checkbox\" value=\"facebook_" + item.ProfileId + "\" onclick=\"isProfileID('" + item.ProfileId + "')\" id=\"facebookcheck_" + i + "\" name=\"chkbox_" + i + "\"></div></div>";

                            }
                            catch (Exception ex)
                            {
                            }
                        }
                        else if (item.ProfileType == "twitter")
                        {
                            try
                            {
                                TwitterAccountRepository twtaccountrepo = new TwitterAccountRepository();
                                TwitterAccount twtaccount = twtaccountrepo.getUserInformation(user.Id, item.ProfileId);

                                bindtwitterprofiles += "<div class=\"ws_tm_network_one\"><div class=\"ws_tm_user_name\">" + twtaccount.TwitterScreenName + "</div>" +
                                                    "<div class=\"ws_tm_chkbx\"><input type=\"checkbox\" value=\"twitter_" + item.ProfileId + "\" onclick=\"isProfileID('" + item.ProfileId + "')\" id=\"twittercheck_" + i + "\" name=\"chkbox_" + i + "\"></div></div>";

                            }
                            catch (Exception ex)
                            {

                            }
                        }
                        else if (item.ProfileType == "linkedin")
                        {
                            try
                            {

                                LinkedInAccountRepository linkedaccrepo = new LinkedInAccountRepository();
                                LinkedInAccount linkedaccount = linkedaccrepo.getUserInformation(user.Id, item.ProfileId);
                                bindlinkedinprofiles += "<div class=\"ws_tm_network_one\"><div class=\"ws_tm_user_name\">" + linkedaccount.LinkedinUserName + "</div>" +
                                                      "<div class=\"ws_tm_chkbx\"><input type=\"checkbox\" value=\"linkedin_" + item.ProfileId + "\" onclick=\"isProfileID('" + item.ProfileId + "')\" id=\"linkedincheck_" + i + "\" name=\"chkbox_" + i + "\"></div></div>";


                            }
                            catch (Exception ex)
                            {

                            }
                        }
                        else if (item.ProfileType == "instagram")
                        {
                            try
                            {
                                InstagramAccountRepository instagramrepo = new InstagramAccountRepository();
                                InstagramAccount instaaccount = instagramrepo.getInstagramAccountDetailsById(item.ProfileId, user.Id);

                                bindinstagramprofiles += "<div class=\"ws_tm_network_one\"><div class=\"ws_tm_user_name\">" + instaaccount.InsUserName + "</div>" +
                                                       "<div class=\"ws_tm_chkbx\"><input type=\"checkbox\" value=\"instagram_" + item.ProfileId + "\" onclick=\"isProfileID('" + item.ProfileId + "')\" id=\"instagramcheck_" + i + "\" name=\"chkbox_" + i + "\"></div></div>";

                            }
                            catch (Exception ex)
                            {

                                throw;
                            }
                        }
                        i++;
                    }
                    if (!string.IsNullOrEmpty(bindfacebookprofiles))
                    {
                        FacebookAc.InnerHtml = bindfacebookprofiles;
                    }
                    else
                    {
                        FacebookAc.InnerHtml = "No Facebook Profiles for " + groups.GroupName + " Group";
                    }

                    if (!string.IsNullOrEmpty(bindtwitterprofiles))
                    {
                        TwitterAc.InnerHtml = bindtwitterprofiles;
                    }
                    else
                    {
                        TwitterAc.InnerHtml = "No Twitter Profiles for " + groups.GroupName + " Group";
                    }
                    if (!string.IsNullOrEmpty(bindinstagramprofiles))
                    {
                        InstagramAc.InnerHtml = bindinstagramprofiles;
                    }
                    else
                    {
                        InstagramAc.InnerHtml = "No Instagram Profiles for " + groups.GroupName + " Group";
                    }
                    if (!string.IsNullOrEmpty(bindlinkedinprofiles))
                    {
                        LinkedInAc.InnerHtml = bindlinkedinprofiles;
                    }
                    else
                    {
                        LinkedInAc.InnerHtml = "No LinkedIn Profiles for " + groups.GroupName + " Group";
                    }
                    totalaccountscheck.InnerHtml = i.ToString();

                }
            }
            catch (Exception ex)
            {
                
            }
        }

        protected void btnSendInvite_Click(object sender, EventArgs e)
        {

            try
            {
                int totalchkboxes = Convert.ToInt32(totalaccountscheck.InnerHtml.ToString());
                string[] totalchkboxesArray = new string[totalchkboxes];


                int jj = 0;
                for (int i = 0; i < totalchkboxes; i++)
                {
                    if (Page.Request.Form["chkbox_" + i + ""] != null)
                    {
                        totalchkboxesArray[jj] = Page.Request.Form["chkbox_" + i + ""].ToString();
                        jj++;
                    }
                }

                if (!string.IsNullOrEmpty(txtEmail.Text) && !string.IsNullOrEmpty(txtFirstName.Text) && !string.IsNullOrEmpty(txtLastName.Text))
                {
                    if (txtEmail.Text != "Enter Email Address" && txtFirstName.Text != "First Name" && txtLastName.Text != "Last Name")
                    {
                        if (rbAdmin.Checked || rbUser.Checked)
                        {
                            if (rbAdmin.Checked)
                            {
                                AccessLevel = "admin";
                            }
                            else if (rbUser.Checked)
                            {
                                AccessLevel = "user";
                            }

                            if (AccessLevel != string.Empty)
                            {
                                TeamRepository teamrepo = new TeamRepository();
                                Team team = null;
                                User user = (User)Session["LoggedUser"];
                                if (!teamrepo.checkTeamExists(txtEmail.Text, user.Id))
                                {
                                    team = new Team();
                                    team.Id = Guid.NewGuid();
                                    team.FirstName = txtFirstName.Text;
                                    team.LastName = txtLastName.Text;
                                    team.StatusUpdateDate = DateTime.Now;
                                    team.EmailId = txtEmail.Text;
                                    team.UserId = user.Id;
                                    team.InviteStatus = 1;
                                    team.InviteDate = DateTime.Now;
                                    team.AccessLevel = AccessLevel;
                                    teamrepo.addNewTeam(team);
                                }
                                else
                                {
                                    team = teamrepo.getMemberByEmailId(user.Id, txtEmail.Text);
                                }
                                MailSender.SendInvitationEmail(team.FirstName + " " + team.LastName, user.UserName, team.EmailId,team.Id);
                                if (totalchkboxesArray.Count() != 0)
                                {
                                    TeamMemberProfileRepository teammemberprofilerepo = new TeamMemberProfileRepository();
                                    foreach (var item in totalchkboxesArray)
                                    {
                                        try
                                        {
                                            if (!string.IsNullOrEmpty(item))
                                            {
                                                string[] itemarray = item.Split('_');
                                                TeamMemberProfile teammember = new TeamMemberProfile();
                                                teammember.Id = Guid.NewGuid();
                                                teammember.ProfileId = itemarray[1];
                                                teammember.ProfileType = itemarray[0];
                                                teammember.Status = 1;
                                                teammember.StatusUpdateDate = DateTime.Now;
                                                teammember.TeamId = team.Id;
                                                if (!teammemberprofilerepo.checkTeamMemberProfile(teammember.TeamId, teammember.ProfileId))
                                                {
                                                    teammemberprofilerepo.addNewTeamMember(teammember);
                                                }
                                                else
                                                {
                                                    teammemberprofilerepo.updateTeamMember(teammember);
                                                }
                                            }

                                        }
                                        catch (Exception ex)
                                        {
                                            Console.WriteLine(ex.StackTrace);
                                        }
                                    }

                                    Response.Redirect("InviteMember.aspx");
                                }

                            }
                            else
                            {

                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
            }


        }



    }
}
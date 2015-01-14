using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Script.Services;
using System.Web.Services;
using Api.Socioboard.Model;

namespace Api.Socioboard.Services
{
    /// <summary>
    /// Summary description for Team
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class Team : System.Web.Services.WebService
    {
        TeamRepository teamrepo = new TeamRepository();
        Domain.Socioboard.Domain.Team team = new Domain.Socioboard.Domain.Team();
        GroupsRepository objGroupsRepository = new GroupsRepository();

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string AddTeam(string UserId, string InviteStatus, string FirstName, string LastName, string EmailId, string AccessLevel, string GroupId, string useremail, string username)
        {
            try
            {
                if (!teamrepo.checkTeamExists(EmailId, Guid.Parse(UserId), Guid.Parse(GroupId)))
                {
                    team.Id = Guid.NewGuid();
                    team.UserId = Guid.Parse(UserId);
                    team.InviteStatus = Convert.ToInt32(InviteStatus);
                    team.InviteDate = DateTime.Now;
                    team.StatusUpdateDate = DateTime.Now;
                    team.GroupId = Guid.Parse(GroupId);
                    team.StatusUpdateDate = DateTime.Now;
                    team.FirstName = FirstName;
                    team.LastName = LastName;
                    team.EmailId = EmailId;
                    team.AccessLevel = AccessLevel;
                    teamrepo.addNewTeam(team);
                    string check = team.Id.ToString();

                    string mailpath = HttpContext.Current.Server.MapPath("~/Layouts/Mails/GroupInvitation.html");
                    string html = File.ReadAllText(mailpath);
                    html = html.Replace("[join link]", ConfigurationManager.AppSettings["MailSenderDomain"] + "Home/Index?teamid=" + team.Id.ToString());
                    string usernameSend = ConfigurationManager.AppSettings["Mandrillusername"];
                    string host = ConfigurationManager.AppSettings["Mandrillhost"];
                    string port = ConfigurationManager.AppSettings["Mandrillport"];
                    string pass = ConfigurationManager.AppSettings["Mandrillpassword"];
                    GlobusMailLib.MailHelper objMailHelper = new GlobusMailLib.MailHelper();
                    objMailHelper.SendMailByMandrill(host, Convert.ToInt32(port), useremail, username, "", EmailId, "", "", "Group Invitation", html, usernameSend, pass);

                    return new JavaScriptSerializer().Serialize(team);
                }
                else
                {
                    return "";
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return "Something Went Wrong";
            }
        }

        //public string AddTeam(string UserId, string InviteStatus, string FirstName, string LastName, string EmailId, string AccessLevel, string GroupId, string useremail, string username)
        //{
        //    try
        //    {
        //        if (!teamrepo.checkTeamExists(EmailId, Guid.Parse(UserId), Guid.Parse(GroupId)))
        //        {
        //            team.Id = Guid.NewGuid();
        //            team.UserId = Guid.Parse(UserId);
        //            team.InviteStatus = Convert.ToInt32(InviteStatus);
        //            team.InviteDate = DateTime.Now;
        //            team.StatusUpdateDate = DateTime.Now;
        //            team.GroupId = Guid.Parse(GroupId);
        //            team.StatusUpdateDate = DateTime.Now;
        //            team.FirstName = FirstName;
        //            team.LastName = LastName;
        //            team.EmailId = EmailId;
        //            team.AccessLevel = AccessLevel;
        //            teamrepo.addNewTeam(team);
        //            string check = team.Id.ToString();

        //            string mailpath = HttpContext.Current.Server.MapPath("~/Layouts/Mails/GroupInvitation.html");
        //            string html = File.ReadAllText(mailpath);
        //            html = html.Replace("[join link]", ConfigurationManager.AppSettings["MailSenderDomain"] + "Home/Index?teamid=" + team.Id.ToString());
        //            string usernameSend = ConfigurationManager.AppSettings["Mandrillusername"];
        //            string host = ConfigurationManager.AppSettings["Mandrillhost"];
        //            string port = ConfigurationManager.AppSettings["Mandrillport"];
        //            string pass = ConfigurationManager.AppSettings["Mandrillpassword"];
        //            GlobusMailLib.MailHelper objMailHelper = new GlobusMailLib.MailHelper();
        //            objMailHelper.SendMailByMandrill(host, Convert.ToInt32(port), useremail, username, "", EmailId, "", "", "Group Invitation", html, usernameSend, pass);
        //        }
        //        else
        //        {

        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex.StackTrace);
        //        return "Something Went Wrong";
        //    }
        //    return new JavaScriptSerializer().Serialize(team);

        //}

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string GetTeamById(string Id)
        {
            try
            {
                Domain.Socioboard.Domain.Team objTeam = teamrepo.getTeamById(Guid.Parse(Id));
                return new JavaScriptSerializer().Serialize(objTeam);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return "Something Went Wrong";
            }
        }


        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string GetTeamByGroupId(string GroupId)
        {
            try
            {
                Domain.Socioboard.Domain.Team objTeam = teamrepo.GetTeamByGroupId(Guid.Parse(GroupId));
                return new JavaScriptSerializer().Serialize(objTeam);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return "Something Went Wrong";
            }
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public static void AddTeamByGroupIdUserId(Guid userId, string userEmailId, Guid groupId)
        {
            Domain.Socioboard.Domain.Team teams = new Domain.Socioboard.Domain.Team();
            TeamRepository objTeamRepository = new TeamRepository();

            teams.Id = Guid.NewGuid();
            teams.GroupId = groupId;
            teams.UserId = userId;
            teams.EmailId = userEmailId;
            teams.InviteStatus = 1;
            objTeamRepository.addNewTeam(teams);
        }


        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string GetTeamByStatus(string GroupId, string userid, string status)
        {
            try
            {
                List<Domain.Socioboard.Domain.Team> lstTeam = teamrepo.GetAllTeamExcludeUserAccordingtoStatus(Guid.Parse(GroupId), Guid.Parse(userid), Convert.ToInt16(status));
                return new JavaScriptSerializer().Serialize(lstTeam);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return "Something Went Wrong";
            }
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string UpdateTeam(string userid, string teamid, string UserName)
        {
            try
            {
                string[] fnamelname = UserName.Split(' ');
                string fname = fnamelname[0];
                string lname = string.Empty;
                for (int i = 1; i < fnamelname.Length; i++)
                {
                    lname += fnamelname[i];
                }
                team = new Domain.Socioboard.Domain.Team();
                team.Id = Guid.Parse(teamid);
                team.UserId = Guid.Parse(userid);
                team.FirstName = fname;
                team.LastName = lname;
                team.StatusUpdateDate = DateTime.Now;
                team.InviteStatus = 1;
                teamrepo.updateTeam(team);
                User objUser=new Services.User ();
               
                    
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return "Something Went Wrong";
            }
            return new JavaScriptSerializer().Serialize(team);

        }


        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string UpdateTeambyteamid(string teamid)
        {
            try
            {
                teamrepo.updateTeambyteamid(Guid.Parse(teamid));
                User objUser = new Services.User();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return "Something Went Wrong";
            }
            return new JavaScriptSerializer().Serialize(team);

        }



        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string GetTeamByUserId(string userid)
        {
            try
            {
                List<Domain.Socioboard.Domain.Team> lstTeam = teamrepo.GetTeamByUserid(Guid.Parse(userid));
                List<Domain.Socioboard.Domain.Groups> lstGroups = new List<Domain.Socioboard.Domain.Groups>();
                foreach (var item in lstTeam)
                {
                    lstGroups.Add(objGroupsRepository.getGroupName(item.GroupId));
                }

                return new JavaScriptSerializer().Serialize(lstGroups);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return "Something Went Wrong";
            }
        }


        //getAllTeamsOfUser
        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string getAllTeamsOfUser(string UserId, string groupId, string emailId)
        {
            List<Domain.Socioboard.Domain.Team> lstTeam = new List<Domain.Socioboard.Domain.Team>();
            lstTeam = teamrepo.getAllTeamsOfUser(Guid.Parse(UserId), Guid.Parse(groupId), emailId);

            return new JavaScriptSerializer().Serialize(lstTeam);
        }

        TeamMemberProfileRepository objTeamMemberProfileRepository = new TeamMemberProfileRepository();
        SentimentalAnalysis objSentimentalAnalysis = new SentimentalAnalysis();
        FeedSentimentalAnalysisRepository _FeedSentimentalAnalysisRepository = new FeedSentimentalAnalysisRepository();

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string getAllGroupMembersofTeam()
        {
            try
            {
                List<Domain.Socioboard.Domain.FeedSentimentalAnalysis> lstAllProfiles = new List<Domain.Socioboard.Domain.FeedSentimentalAnalysis>();
                List<Domain.Socioboard.Domain.TeamMemberProfile> lstTeamId = new List<Domain.Socioboard.Domain.TeamMemberProfile>();
                List<Domain.Socioboard.Domain.Team> lstTeam = new List<Domain.Socioboard.Domain.Team>();
                List<Domain.Socioboard.Domain.Team> lstgroupmember = new List<Domain.Socioboard.Domain.Team>();
                List<Domain.Socioboard.Domain.Team> lstAllgroupmembers = new List<Domain.Socioboard.Domain.Team>();
                try
                {
                    lstAllProfiles = _FeedSentimentalAnalysisRepository.getAllProfiles();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.StackTrace);
                }
                foreach (var item in lstAllProfiles)
                {
                    try
                    {
                        lstTeamId = objTeamMemberProfileRepository.getAllTeamsByProfileId(item.ProfileId);
                        foreach (var lstTeamId_item in lstTeamId)
                        {
                            lstTeam = teamrepo.GetTeamByTeamId(lstTeamId_item.TeamId);
                            foreach (var lstTeam_item in lstTeam)
                            {
                                lstgroupmember = teamrepo.getAllDetailsUserEmail(lstTeam_item.GroupId);
                                if (lstgroupmember.Count > 1)
                                {
                                    lstAllgroupmembers.AddRange(lstgroupmember);
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                    }
                }
                return new JavaScriptSerializer().Serialize(lstAllgroupmembers);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return "Something Went Wrong";
            }
        }

    }
}

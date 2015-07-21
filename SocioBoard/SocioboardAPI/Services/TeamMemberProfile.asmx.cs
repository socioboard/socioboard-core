using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Script.Services;
using System.Web.Services;
using Domain.Socioboard.Domain;

namespace Api.Socioboard.Services
{
    /// <summary>
    /// Summary description for TeamMemberProfile
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]

    public class TeamMemberProfile : System.Web.Services.WebService
    {
        TeamMemberProfileRepository teammemberrepo = new TeamMemberProfileRepository();
        Domain.Socioboard.Domain.TeamMemberProfile teammember = new Domain.Socioboard.Domain.TeamMemberProfile();

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string AddTeamMemberProfile(string TeamId, string ProfileId, string ProfileType, string Status)
        {

            try
            {
                if (!teammemberrepo.checkTeamMemberProfile(Guid.Parse(TeamId), ProfileId))
                {
                    teammember.Id = Guid.NewGuid();
                    teammember.TeamId = Guid.Parse(TeamId);
                    teammember.ProfileId = ProfileId;
                    teammember.ProfileType = ProfileType;
                    teammember.Status = Convert.ToInt32(Status);
                    teammember.StatusUpdateDate = DateTime.Now;
                    teammemberrepo.addNewTeamMember(teammember);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return "Something Went Wrong";
            }
            return new JavaScriptSerializer().Serialize(teammember);

        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string GetTeamMemberProfilesByTeamId(string TeamId)
        {
            try
            {
                List<Domain.Socioboard.Domain.TeamMemberProfile> lstTeamMember = teammemberrepo.getAllTeamMemberProfilesOfTeam(Guid.Parse(TeamId));
                return new JavaScriptSerializer().Serialize(lstTeamMember);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return "Something Went Wrong";
            }
        }

        //getAllTeamMemberProfilesOfTeam
        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string getAllTeamMemberProfilesOfTeam(string TeamId)
        {
            try
            {
                List<Domain.Socioboard.Domain.TeamMemberProfile> lstTeamMember = teammemberrepo.getAllTeamMemberProfilesOfTeam(Guid.Parse(TeamId));
                return new JavaScriptSerializer().Serialize(lstTeamMember);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return "Something Went Wrong";
            }
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string GetTeamMembeDetailsForGroupReport(string TeamId, string userid, string days)
        {
            string FacebookprofileId = string.Empty;
            string TwitterprofileId = string.Empty;
            string FacebookFanPageId = string.Empty;
            string profid = string.Empty;
            string FacebookInboxMessagecount = string.Empty;
            string TwitterInboxMessagecount = string.Empty;
            Domain.Socioboard.Domain.GroupStatDetails _GroupStatDetails = new Domain.Socioboard.Domain.GroupStatDetails();

            Guid UserId = Guid.Parse(userid);
            try
            {
                List<FacebookAccount> _facebookAccount = new List<FacebookAccount>();
                List<Domain.Socioboard.Domain.TeamMemberProfile> lstTeamMember = teammemberrepo.getAllTeamMemberProfilesOfTeam(Guid.Parse(TeamId));

               // _GroupStatDetails.ProfilesByUser = lstTeamMember.Count();
                foreach (Domain.Socioboard.Domain.TeamMemberProfile TeamMemberProfile in lstTeamMember)
                {

                    #region MyRegion
                    try
                    {
                        if (TeamMemberProfile.ProfileType == "facebook" || TeamMemberProfile.ProfileType == "twitter")
                        {
                            profid += TeamMemberProfile.ProfileId + ',';
                        }

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                    }
                    try
                    {
                        if (TeamMemberProfile.ProfileType == "facebook")
                        {
                            FacebookprofileId += TeamMemberProfile.ProfileId + ',';
                        }

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                    }
                    try
                    {
                        if (TeamMemberProfile.ProfileType == "twitter")
                        {
                            TwitterprofileId += TeamMemberProfile.ProfileId + ',';
                        }

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                    }
                    try
                    {
                        if (TeamMemberProfile.ProfileType == "facebook_page")
                        {
                            FacebookFanPageId += TeamMemberProfile.ProfileId + ',';
                        }

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                    }
                    #endregion

                }

                #region MyRegion
                try
                {
                    profid = profid.Substring(0, profid.Length - 1);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.StackTrace);
                }
                try
                {
                    FacebookFanPageId = FacebookFanPageId.Substring(0, FacebookFanPageId.Length - 1);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.StackTrace);
                    FacebookFanPageId = null;
                }
                try
                {
                    TwitterprofileId = TwitterprofileId.Substring(0, TwitterprofileId.Length - 1);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.StackTrace);
                    TwitterprofileId = null;
                }
                try
                {
                    FacebookprofileId = FacebookprofileId.Substring(0, FacebookprofileId.Length - 1);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.StackTrace);
                    FacebookprofileId = null;
                }
                #endregion

                #region Inboxmessagecount
                if (!string.IsNullOrEmpty(FacebookprofileId))
                {
                    try
                    {
                        FacebookMessage _FacebookMessage = new FacebookMessage();
                        FacebookInboxMessagecount = _FacebookMessage.GetAllInboxMessage(userid, FacebookprofileId, days);
                    }
                    catch (Exception ex)
                    {
                        FacebookInboxMessagecount = (0).ToString();
                        Console.WriteLine(ex.StackTrace);
                    }
                }
                else
                {
                    FacebookInboxMessagecount = (0).ToString();
                }

                if (!string.IsNullOrEmpty(TwitterprofileId))
                {
                    try
                    {
                        TwitterFeed _TwitterFeed = new TwitterFeed();
                        TwitterInboxMessagecount = _TwitterFeed.TwitterInboxMessagecount(userid, TwitterprofileId, days);
                    }
                    catch (Exception ex)
                    {
                        TwitterInboxMessagecount = (0).ToString();
                        Console.WriteLine(ex.StackTrace);
                    }
                }

                else
                {
                    TwitterInboxMessagecount = (0).ToString();
                }

                _GroupStatDetails.IncommingMessage = (Convert.ToInt32(FacebookInboxMessagecount) + Convert.ToInt32(TwitterInboxMessagecount));
                #endregion

                #region sentmessage
                if (!string.IsNullOrEmpty(profid))
                {
                    try
                    {
                        ScheduledMessage _ScheduledMessage = new ScheduledMessage();
                        _GroupStatDetails.SentMessage = _ScheduledMessage.GetAllScheduledMessage(userid, profid, days);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                        _GroupStatDetails.SentMessage = 0;
                    }
                }
                #endregion

                #region twitterfolllower
                try
                {
                    TwitterAccountFollowers _TwitterAccountFollowers = new TwitterAccountFollowers();
                    _GroupStatDetails.TwitterFollower = _TwitterAccountFollowers.FollowerCount(userid, TwitterprofileId, days);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.StackTrace);
                    _GroupStatDetails.TwitterFollower = 0;
                }

                #endregion

                #region fancount
                try
                {
                    FacebookFanPage _FacebookFanPage = new FacebookFanPage();
                    _GroupStatDetails.FacebookFan = _FacebookFanPage.FacebookFans(userid, FacebookFanPageId, days);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.StackTrace);
                    _GroupStatDetails.FacebookFan = 0;
                }

                #endregion


                #region MentionRetweetDetails
                try
                {

                    TwitterMessage _TwitterMessage = new TwitterMessage();
                    _GroupStatDetails.MentionGraph = string.Empty;
                    string graphdetails = _TwitterMessage.GetAllRetweetMentionBydays(userid, TwitterprofileId, days);
                    //string graphdetails = inboxmsgrepo.GetAllRetweetMentionBydays(userid, TwitterprofileId, days);

                    string[] data = graphdetails.Split('@');
                    foreach (var item in data)
                    {
                        if (item.Contains("usrtwet^"))
                        {
                            _GroupStatDetails.UserTweetGraph = item.Replace("usrtwet^", "");
                        }
                        else if (item.Contains("mention^"))
                        {
                            _GroupStatDetails.MentionGraph = item.Replace("mention^", "");
                        }
                        else if (item.Contains("retwet^"))
                        {
                            _GroupStatDetails.RetweetGraph = item.Replace("retwet^", "");
                        }
                        else if (item.Contains("metion"))
                        {
                            _GroupStatDetails.Mention = Convert.ToInt32(item.Replace("metion", ""));
                        }
                        else if (item.Contains("retwet"))
                        {
                            _GroupStatDetails.Retweet = Convert.ToInt32(item.Replace("retwet", ""));
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.StackTrace);
                }
                #endregion

                try
                {
                    ScheduledMessage _ScheduledMessage = new ScheduledMessage();

                    string details = _ScheduledMessage.GetAllScheduleMsgDetailsForReport(userid, TwitterprofileId, days);
                    string[] data = details.Split('@');
                    foreach (var item in data)
                    {
                        if (item.Contains("plaintext_"))
                        {
                            _GroupStatDetails.PlainText = Convert.ToInt32(item.Replace("plaintext_", ""));

                        }

                        else
                        {
                            _GroupStatDetails.PhotoLink = Convert.ToInt32(item);
                        }
                    }

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.StackTrace);
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return "Something Went Wrong";
            }

            return new JavaScriptSerializer().Serialize(_GroupStatDetails);


        }
    }
}

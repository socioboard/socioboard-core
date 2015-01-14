using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Script.Services;
using System.Web.Services;
using Api.Socioboard.Model;
using Domain.Socioboard.Domain;
using log4net;

namespace Api.Socioboard.Services
{
    /// <summary>
    /// Summary description for SentimentalAnalysis
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class SentimentalAnalysis : System.Web.Services.WebService
    {
        ILog logger = LogManager.GetLogger(typeof(SentimentalAnalysis));
        Domain.Socioboard.Domain.FeedSentimentalAnalysis _FeedSentimentalAnalysis = new FeedSentimentalAnalysis();
        FeedSentimentalAnalysisRepository _FeedSentimentalAnalysisRepository = new FeedSentimentalAnalysisRepository();
        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string GetPostSentimentsFromUclassify(Guid Userid, string profileid, string feedId, string Message, string Network)
        {
            string responce = string.Empty;
            try
            {
                Guid _Userid = Guid.Parse(Userid.ToString());
                responce = GetSentimentsOfText(_Userid, profileid, feedId, Message, Network);
            }
            catch (Exception ex)
            {
                responce = "";
            }
            return responce;
        }

        public string GetSentimentsOfText(Guid Userid, string profileid, string feedId, string Message, string Network)
        {
            string _Strreturn = string.Empty;
            try
            {
                string _SentimentalRestUrl = "http://uclassify.com/browse/uClassify/Sentiment/ClassifyText?readkey=" + ConfigurationManager.AppSettings["ReadKey"] + "&text=" + Message + "&output=json&version=1.01";

                GlobusLinkedinLib.Authentication.oAuthLinkedIn _oAuthLinkedIn = new GlobusLinkedinLib.Authentication.oAuthLinkedIn();
                string response = _oAuthLinkedIn.WebRequest(GlobusLinkedinLib.Authentication.oAuthLinkedIn.Method.GET, Uri.EscapeUriString(_SentimentalRestUrl), "");

                var JData = Newtonsoft.Json.Linq.JObject.Parse(response);

                string negative = JData["cls1"]["negative"].ToString();
                string positive = JData["cls1"]["positive"].ToString();


                Domain.Socioboard.Domain.FeedSentimentalAnalysis _FeedSentimentalAnalysis = new FeedSentimentalAnalysis();
                _FeedSentimentalAnalysis.Id = Guid.NewGuid();
                _FeedSentimentalAnalysis.UserId = Userid;
                _FeedSentimentalAnalysis.ProfileId = profileid;
                _FeedSentimentalAnalysis.FeedId = feedId;
                _FeedSentimentalAnalysis.Positive = positive;
                _FeedSentimentalAnalysis.Negative = negative;
                _FeedSentimentalAnalysis.EntryDate = DateTime.Now;
                _FeedSentimentalAnalysis.Network = Network;

                Model.FeedSentimentalAnalysisRepository _FeedSentimentalAnalysisRepository = new Model.FeedSentimentalAnalysisRepository();
                _FeedSentimentalAnalysisRepository.Add(_FeedSentimentalAnalysis);

                _Strreturn = "Success";
            }
            catch (Exception)
            {
                _Strreturn = "failure";
            }


            return _Strreturn;
        }

        /// <summary>
        /// Get All Negative Feed of All Profile.
        /// </summary>
        /// <param name="ProfileId"></param>
        /// <returns></returns>
        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string getAllNegativeFeedsOfProfile()
        {
            try
            {
                List<Domain.Socioboard.Domain.FeedSentimentalAnalysis> lstAllProfiles = new List<Domain.Socioboard.Domain.FeedSentimentalAnalysis>();
                List<Domain.Socioboard.Domain.FeedSentimentalAnalysis> lstNegativeFeedsOfProfile = new List<Domain.Socioboard.Domain.FeedSentimentalAnalysis>();
                List<Domain.Socioboard.Domain.FeedSentimentalAnalysis> lstNegativeFeedsOfAllProfile = new List<Domain.Socioboard.Domain.FeedSentimentalAnalysis>();

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
                        lstNegativeFeedsOfProfile = _FeedSentimentalAnalysisRepository.getAllNegativeFeedsOfProfile(item.ProfileId);
                        lstNegativeFeedsOfAllProfile.AddRange(lstNegativeFeedsOfProfile);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                    }
                }
                return new JavaScriptSerializer().Serialize(lstNegativeFeedsOfAllProfile);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return "Something Went Wrong";
            }
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string updateAssignedStatus(string Id, string AssignedUserId)
        {
            int ret = 0;
            try
            {
                ret = _FeedSentimentalAnalysisRepository.updateAssignedStatus(Guid.Parse(Id), Guid.Parse(AssignedUserId));
                if (ret == 1)
                {
                    return "Updated";
                }
                else
                {
                    return "Failure";
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return "Something Went Wrong";
            }
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string getNegativeFeedsOfUser(string UserId)
        {
            List<Domain.Socioboard.Domain.FeedSentimentalAnalysis> lstNegativeFeedsOfUser = new List<Domain.Socioboard.Domain.FeedSentimentalAnalysis>();
            try
            {
                lstNegativeFeedsOfUser = _FeedSentimentalAnalysisRepository.getNegativeFeedsOfUser(Guid.Parse(UserId));
                return new JavaScriptSerializer().Serialize(lstNegativeFeedsOfUser);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return "Something Went Wrong";
            }
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string GetTicketsofGroup(string GroupId, string UserId)
        {
            string AssignedUser = string.Empty;
            try
            {
                GroupProfileRepository objGroupProfileRepository = new GroupProfileRepository();
                FacebookFeedRepository objFacebookFeedRepository = new FacebookFeedRepository();
                TwitterFeedRepository objTwitterFeedRepository = new TwitterFeedRepository();
                List<Domain.Socioboard.Domain.FeedSentimentalAnalysis> lstNegativeFeedsOfUser = new List<Domain.Socioboard.Domain.FeedSentimentalAnalysis>();
                List<Domain.Socioboard.Domain.GroupProfile> objGroupProfile = new List<Domain.Socioboard.Domain.GroupProfile>();
                List<FBTwitterFeeds> objListFBTwitterFeeds = new List<FBTwitterFeeds>();

                objGroupProfile = objGroupProfileRepository.getAllGroupProfiles(Guid.Parse(UserId), Guid.Parse(GroupId));

                if (objGroupProfile.Count > 0)
                {
                    lstNegativeFeedsOfUser = _FeedSentimentalAnalysisRepository.getAllNegativeFeedsOfUser(Guid.Parse(UserId));
                }
                else
                {
                    lstNegativeFeedsOfUser = _FeedSentimentalAnalysisRepository.getNegativeFeedsOfUser(Guid.Parse(UserId));
                }
                if (lstNegativeFeedsOfUser != null)
                {
                    foreach (var item in lstNegativeFeedsOfUser)
                    {
                        FBTwitterFeeds objFBTwitterFeeds = new FBTwitterFeeds();
                        UserRepository objUserRepository = new UserRepository();
                        Domain.Socioboard.Domain.User user = objUserRepository.getUsersById(item.AssigneUserId);
                        if (objGroupProfileRepository.checkProfileExistsingroup(Guid.Parse(GroupId), item.ProfileId))
                        {
                            string Network = item.Network;
                            if (Network == "facebook")
                            {
                                Domain.Socioboard.Domain.FacebookFeed facebookfeed = objFacebookFeedRepository.getFacebookFeedByProfileId(item.ProfileId, item.FeedId);
                                if (facebookfeed != null)
                                {
                                    objFBTwitterFeeds.FacebookFeed = facebookfeed;
                                }
                            }
                            if (Network == "twitter")
                            {
                                Domain.Socioboard.Domain.TwitterFeed twtfeed = objTwitterFeedRepository.getTwitterFeedByProfileId(item.ProfileId, item.FeedId);
                                if (twtfeed != null)
                                {
                                    objFBTwitterFeeds.TwitterFeed = twtfeed;
                                }
                            }
                            if (objFBTwitterFeeds.TwitterFeed != null)
                            {
                                try
                                {
                                    objFBTwitterFeeds.TicketNo = item.TicketNo;
                                    if (user != null)
                                    {
                                        objFBTwitterFeeds.AssignedUser = user.UserName;
                                    }
                                    else
                                    {
                                        objFBTwitterFeeds.AssignedUser = "";
                                    }
                                    objListFBTwitterFeeds.Add(objFBTwitterFeeds);
                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine(ex.StackTrace);
                                }
                            }
                            if (objFBTwitterFeeds.FacebookFeed != null)
                            {
                                try
                                {
                                    objFBTwitterFeeds.TicketNo = item.TicketNo;
                                    if (user != null)
                                    {
                                        objFBTwitterFeeds.AssignedUser = user.UserName;
                                    }
                                    else
                                    {
                                        objFBTwitterFeeds.AssignedUser = "";
                                    }
                                    objListFBTwitterFeeds.Add(objFBTwitterFeeds);
                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine(ex.StackTrace);
                                }
                            }
                        }
                    } 
                }
               
                return new JavaScriptSerializer().Serialize(objListFBTwitterFeeds);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return "SomethingWentWrong";
            }

        }

    }
}

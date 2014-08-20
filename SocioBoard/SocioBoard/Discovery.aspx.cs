using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SocioBoard.Domain;
using System.IO;
using Newtonsoft.Json.Linq;
using System.Net;
using SocioBoard.Model;
using System.Text;
using System.Configuration;
using GlobusTwitterLib.Authentication;
using System.Collections;
using GlobusTwitterLib.Twitter.Core.SearchMethods;
using log4net;
using SocioBoard.Helper;

namespace SocialSuitePro
{
    public partial class Discovery : System.Web.UI.Page
    {

        ILog logger = LogManager.GetLogger(typeof(Discovery));
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                #region for You can use only 30 days as Unpaid User

                SocioBoard.Domain.User user = (User)Session["LoggedUser"];
                if (user.PaymentStatus.ToLower() == "unpaid")
                {
                    if (!SBUtils.IsUserWorkingDaysValid(user.ExpiryDate))
                    {
                        // ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('You can use only 30 days as Unpaid User !');", true);

                        Session["GreaterThan30Days"] = "GreaterThan30Days";

                        Response.Redirect("/Settings/Billing.aspx");
                    }
                }
                #endregion
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error : " + ex.StackTrace);
            }

          
        }

        public void btnSearch_Click(object sender, EventArgs e)
        {
           
            try
            {
                //if (!string.IsNullOrEmpty(txtSearchText.Text))
                //{

                var SearchBy = string.Empty;

                //if (chkbykeyword)
                //{
                //    SearchBy = chkbykeyword.Value;
                //}
                //else
                //{
                //    SearchBy = chkbykeyword.Value;
                //}
                //var abc =chkbysearch.Items.Cast<ListItem>().Where(i => i.Selected).Select(i => i.Value);
                //var SearchBy1 = chkbysearch.Items.Cast<ListItem>().Where(li => li.Selected).Select(li => li.Value).ToList();
                
                string searchRes = getresults(txtSearchText.Text);
                    if (!string.IsNullOrEmpty(searchRes))
                    {
                        searchresults.InnerHtml = "<ul id=\"message-list\">" + searchRes + "</ul>";
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Keyword not found !');", true);
                    }
                //}

                //else
                //{
                //    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Enter any desired name for search !');", true);
                //}
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error : " + ex.StackTrace);
            }
        }

        public string getresults(string keyword)
        {
            User user = (User)Session["LoggedUser"];
            int i = 0;
            string searchRes = string.Empty;
            if (!string.IsNullOrEmpty(keyword))
            {
                DiscoverySearch dissearch = new DiscoverySearch();
                DiscoverySearchRepository dissearchrepo = new DiscoverySearchRepository();

                //Get data from Database if present, against the specified keyword
                List<DiscoverySearch> discoveryList = dissearchrepo.getResultsFromKeyword(keyword);

                if (discoveryList.Count == 0) //if no data, get the data for specified keyword from social media apis
                {

                    #region Twitter

                    try
                    {
                        oAuthTwitter oauth = new oAuthTwitter();
                        oauth.ConsumerKey = ConfigurationManager.AppSettings["consumerKey"].ToString();
                        oauth.ConsumerKeySecret = ConfigurationManager.AppSettings["consumerSecret"].ToString();
                        oauth.CallBackUrl = ConfigurationManager.AppSettings["callbackurl"].ToString();
                        TwitterAccountRepository twtAccRepo = new TwitterAccountRepository();
                        ArrayList alst = twtAccRepo.getAllTwitterAccounts();
                        foreach (TwitterAccount item in alst)
                        {
                            oauth.AccessToken = item.OAuthToken;
                            oauth.AccessTokenSecret = item.OAuthSecret;
                            oauth.TwitterUserId = item.TwitterUserId;
                            oauth.TwitterScreenName = item.TwitterScreenName;
                            if (TwitterHelper.CheckTwitterToken(oauth, keyword))
                            {
                                break;
                            }
                            else
                            {

                            }
                        }

                        Search search = new Search();
                        JArray twitterSearchResult = search.Get_Search_Tweets(oauth, keyword);

                        foreach (var item in twitterSearchResult)
                        {
                            var results = item["statuses"];

                            foreach (var chile in results)
                            {
                                try
                                {
                                    dissearch.CreatedTime = SocioBoard.Helper.Extensions.ParseTwitterTime(chile["created_at"].ToString().TrimStart('"').TrimEnd('"')); ;
                                    dissearch.EntryDate = DateTime.Now;
                                    dissearch.FromId = chile["user"]["id_str"].ToString().TrimStart('"').TrimEnd('"');
                                    dissearch.FromName = chile["user"]["screen_name"].ToString().TrimStart('"').TrimEnd('"');
                                    dissearch.ProfileImageUrl = chile["user"]["profile_image_url"].ToString().TrimStart('"').TrimEnd('"');
                                    dissearch.SearchKeyword = txtSearchText.Text;
                                    dissearch.Network = "twitter";
                                    dissearch.Message = chile["text"].ToString().TrimStart('"').TrimEnd('"');
                                    dissearch.MessageId = chile["id_str"].ToString().TrimStart('"').TrimEnd('"');
                                    dissearch.Id = Guid.NewGuid();
                                    dissearch.UserId = user.Id;

                                    string postID = chile["id"].ToString();
                                    string postURL = "https://twitter.com/" + dissearch.FromName + "/status/" + postID;

                                    if (!dissearchrepo.isKeywordPresent(dissearch.SearchKeyword, dissearch.MessageId))
                                    {
                                        dissearchrepo.addNewSearchResult(dissearch);
                                    }

                                    searchRes += this.BindData(dissearch, i, postURL);


                                    i++;
                                }
                                catch (Exception ex)
                                {
                                    logger.Error(ex.StackTrace);
                                    Console.WriteLine(ex.StackTrace);
                                }
                            }
                        }

                    }
                    catch (Exception ex)
                    {
                        logger.Error(ex.StackTrace);
                        Console.WriteLine(ex.StackTrace);
                    }
                    #endregion
                    #region Facebook
                    try
                    {
                        #region FacebookSearch
                        int j = 0;
                        string accesstoken = string.Empty;
                        FacebookAccountRepository fbAccRepo = new FacebookAccountRepository();
                        ArrayList asltFbAccount = fbAccRepo.getAllFacebookAccounts();
                        foreach (FacebookAccount item in asltFbAccount)
                        {
                            accesstoken = item.AccessToken;
                            if (FacebookHelper.CheckFacebookToken(accesstoken, txtSearchText.Text))
                            {

                                break;
                            }
                        }

                        string facebookSearchUrl = "https://graph.facebook.com/search?q=" + txtSearchText.Text + " &type=post&access_token=" + accesstoken;
                        var facerequest = (HttpWebRequest)WebRequest.Create(facebookSearchUrl);
                        facerequest.Method = "GET";
                        string outputface = string.Empty;
                        using (var response = facerequest.GetResponse())
                        {
                            using (var stream = new StreamReader(response.GetResponseStream(), Encoding.GetEncoding(1252)))
                            {
                                outputface = stream.ReadToEnd();
                            }
                        }
                        if (!outputface.StartsWith("["))
                            outputface = "[" + outputface + "]";


                        JArray facebookSearchResult = JArray.Parse(outputface);

                        foreach (var item in facebookSearchResult)
                        {
                            var data = item["data"];

                            foreach (var chile in data)
                            {
                                try
                                {
                                    dissearch.CreatedTime = DateTime.Parse(chile["created_time"].ToString());
                                    dissearch.EntryDate = DateTime.Now;
                                    dissearch.FromId = chile["from"]["id"].ToString();
                                    dissearch.FromName = chile["from"]["name"].ToString();
                                    dissearch.ProfileImageUrl = "http://graph.facebook.com/" + chile["from"]["id"] + "/picture?type=small";
                                    dissearch.SearchKeyword = txtSearchText.Text;
                                    dissearch.Network = "facebook";
                                    dissearch.Message = chile["message"].ToString();
                                    dissearch.MessageId = chile["id"].ToString();
                                    dissearch.Id = Guid.NewGuid();
                                    dissearch.UserId = user.Id;

                                    string postURL = "https://www.facebook.com/" + dissearch.MessageId;

                                    if (!dissearchrepo.isKeywordPresent(dissearch.SearchKeyword, dissearch.MessageId))
                                    {
                                        dissearchrepo.addNewSearchResult(dissearch);
                                    }
                                    searchRes += this.BindData(dissearch, i, postURL);
                                }
                                catch (Exception ex)
                                {
                                    logger.Error(ex.StackTrace);
                                    Console.WriteLine(ex.StackTrace);
                                }


                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        logger.Error(ex.StackTrace);
                        Console.WriteLine(ex.StackTrace);
                    }
                        #endregion

                    #endregion
                }
                else // if data is there, bind data from databse
                {
                    foreach (DiscoverySearch item in discoveryList)
                    {
                        string postURL = string.Empty;
                        if (item.Network=="facebook")
                        {
                            postURL = "https://www.facebook.com/" + item.MessageId; ;
                        }
                        else if (item.Network=="twitter")
                        {
                            postURL = "https://twitter.com/" + item.FromName + "/status/" + item.MessageId;
                        }

                        searchRes += this.BindData(item, i, postURL);
                        i++;
                    }
                }

            }
            return searchRes;
        }

        public string getlistresults(string keyword)
        {
            User user = (User)Session["LoggedUser"];
            int i = 0;
            string searchRes = string.Empty;
            if (!string.IsNullOrEmpty(keyword))
            {
                DiscoverySearch dissearch = new DiscoverySearch();
                DiscoverySearchRepository dissearchrepo = new DiscoverySearchRepository();

                List<DiscoverySearch> discoveryList = dissearchrepo.getResultsFromKeyword(keyword);

                if (discoveryList.Count == 0)
                {

                }
                else
                {
                    foreach (DiscoverySearch item in discoveryList)
                    {
                        string postURL = string.Empty;
                        if (item.Network == "facebook")
                        {
                            postURL = "https://www.facebook.com/" + item.MessageId; ;
                        }
                        else if (item.Network == "twitter")
                        {
                            postURL = "https://twitter.com/" + item.FromName + "/status/" + item.MessageId;
                        }

                        searchRes += this.BindData(item, i, postURL);
                        i++;
                    }
                }

            }
            return searchRes;
        }

        public string BindData(DiscoverySearch dissearch, int i, string postURL="")
        {

            string message = string.Empty;
            if (dissearch.Network == "facebook")
            {
                try
                {

                    try
                    {
                        if (!SBUtils.IsImageUrl(dissearch.ProfileImageUrl))
                        {
                            if (dissearch.Network == "facebook")
                            {
                                dissearch.ProfileImageUrl = "../Contents/img/blank_img.png";
                            }
                            else if (dissearch.Network == "twitter")
                            {
                                dissearch.ProfileImageUrl = "../Contents/img/blank_img.png";
                            }
                        }
                        
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }


                    //message += "<li><div class=\"userpictiny\" id=\"messagetaskable_"+i+"\"><div style=\"width:60px;height:auto;float:left\">"+
                    //            "<img width=\"48\" height=\"48\" title=\""+dissearch.FromName+"\" alt=\""+dissearch.FromName+"\" src=\""+ dissearch.ProfileImageUrl+"\" onclick=\"detailsprofile(this.alt);\" id=\"formprofileurl_"+i+"\"><a title=\"\" class=\"userurlpic\" href=\"#\"><img width=\"16\" height=\"16\" alt=\"\" src=\"../Contents/img/fb_icon.png\">"+
                    //            "</a></div></div><div class=\"message-list-content\" id=\"messagedescription_"+i+"\">"+
                    //            "<div style=\"width:500px;height:auto;float:left\"><p>"+dissearch.Message +"</p>"+
                    //            "<div class=\"message-list-info\"><span><a onclick=\"detailsprofile(this.id);\" id=\"rowname_"+i+"\" href=\"#\">"+dissearch.FromName+"</a> "+dissearch.CreatedTime+"</span>"+
                    //            "</div></div></div></li>";
                    //i++;

                    string[] msgId = dissearch.MessageId.Split('_');

                    message += "<li><div class=\"userpictiny\" id=\"messagetaskable_" + i + "\"><div style=\"width:60px;height:auto;float:left\">" +
                               "<img width=\"48\" height=\"48\" title=\"" + dissearch.FromName + "\" alt=\"" + dissearch.FromName + "\" src=\"" + dissearch.ProfileImageUrl + "\" onclick=\"detailsdiscoveryfacebook('" + msgId[0] + "');\" id=\"formprofileurl_" + i + "\"><a title=\"\" class=\"userurlpic\" href=\"#\"><img width=\"16\" height=\"16\" alt=\"\" src=\"../Contents/img/fb_icon.png\">" +
                               "</a></div></div><div class=\"message-list-content leftset\" id=\"messagedescription_" + i + "\">" +
                               "<div style=\"width:500px;height:auto;float:left\"><p>" + dissearch.Message + "</p>" +
                               "<div class=\"message-list-info\"><span><a id=\"rowname_" + i + "\" href=\"" + postURL + "\" target=\"_blank\" >" + dissearch.FromName + "</a> " + dissearch.CreatedTime + "</span>" +
                               "</div></div></div></li>";
                    i++;
                }
                catch (Exception ex)
                {

                    Console.WriteLine(ex.Message);
                }
            }
            else if (dissearch.Network == "twitter")
            {
                try
                {
                    //message += "<li><div class=\"userpictiny\" id=\"messagetaskable_" + i + "\"><div style=\"width:60px;height:auto;float:left\">" +
                    //            "<img width=\"48\" height=\"48\" title=\"" + dissearch.FromName + "\" alt=\"" + dissearch.FromName + "\" src=\"" + dissearch.ProfileImageUrl + "\" onclick=\"detailsprofile(this.alt);\" id=\"formprofileurl_" + i + "\"><a title=\"\" class=\"userurlpic\" href=\"#\"><img width=\"16\" height=\"16\" alt=\"\" src=\"../Contents/img/twticon.png\">" +
                    //            "</a></div></div><div class=\"message-list-content\" id=\"messagedescription_" + i + "\">" +
                    //            "<div style=\"width:500px;height:auto;float:left\"><p>" + dissearch.Message + "</p>" +
                    //            "<div class=\"message-list-info\"><span><a onclick=\"detailsprofile(this.id);\" id=\"rowname_" + i + "\" href=\"#\">" + dissearch.FromName + "</a> " + dissearch.CreatedTime + "</span>" +
                    //            "</div></div></div></li>";


                    message += "<li><div class=\"userpictiny\" id=\"messagetaskable_" + i + "\"><div style=\"width:60px;height:auto;float:left\">" +
                               "<img width=\"48\" height=\"48\" title=\"" + dissearch.FromName + "\" alt=\"" + dissearch.FromName + "\" src=\"" + dissearch.ProfileImageUrl + "\" onclick=\"detailsdiscoverytwitter('" + dissearch.FromId + "') ;\" id=\"formprofileurl_" + i + "\"><a title=\"\" class=\"userurlpic\" href=\"#\"><img width=\"16\" height=\"16\" alt=\"\" src=\"../Contents/img/twticon.png\">" +
                               "</a></div></div><div class=\"message-list-content leftset\" id=\"messagedescription_" + i + "\">" +
                               "<div style=\"width:500px;height:auto;float:left\"><p>" + dissearch.Message + "</p>" +
                               "<div class=\"message-list-info\"><span><a \" id=\"rowname_" + i + "\" href=\"" + postURL + "\"  target=\"_blank\" >" + dissearch.FromName + "</a> " + dissearch.CreatedTime + "</span>" +
                               "</div></div></div></li>";
                }
                catch (Exception ex)
                {

                    Console.WriteLine(ex.Message);
                }
            }
            return message;
        }

        
        

        
    }
}
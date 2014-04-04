using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SocioBoard.Model;
using SocioBoard.Domain;
using System.Collections;
using System.Data;
using SocioBoard.Helper;
using Newtonsoft.Json.Linq;
using GlobusGooglePlusLib.App.Core;
using Facebook;
using GlobusTwitterLib.Authentication;
using GlobusTwitterLib.App.Core;
using GlobusTwitterLib.Twitter.Core.TweetMethods;
using log4net;
using GlobusGooglePlusLib.Authentication;
namespace SocioBoard.Message
{
    public partial class AjaxMessage : System.Web.UI.Page
    {
        ILog logger = LogManager.GetLogger(typeof(AjaxMessage));
        protected void Page_Load(object sender, EventArgs e)
        {

            User use = (User)Session["LoggedUser"];
            if (use == null)
                Response.Redirect("/Default.aspx");
            try
            {
                ProcessRequest();
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                Console.WriteLine(ex.Message);
            }


        }

        void ProcessRequest()
        {
            User user = (User)Session["LoggedUser"];
            if (Request.QueryString["op"] != null)
            {
                if (Request.QueryString["op"] == "bindMessages")
                {
                    DataSet ds = null;
                    if (Session["MessageDataTable"] == null)
                    {
                        clsFeedsAndMessages clsfeedsandmess = new clsFeedsAndMessages();
                        ds = clsfeedsandmess.bindMessagesIntoDataTable(user);
                        FacebookFeedRepository fbFeedRepo = new FacebookFeedRepository();
                        Session["MessageDataTable"] = ds;
                    }
                    else
                    {
                        ds = (DataSet)Session["MessageDataTable"];
                    }
                    string message = this.BindData(ds.Tables[0]);
                    Response.Write(message);

                }
                else if (Request.QueryString["op"] == "inbox_messages")
                {
                    DataSet ds = null;
                    if (Session["InboxMessages"] == null)
                    {
                        clsFeedsAndMessages clsfeedsandmessages = new clsFeedsAndMessages();
                        ds = clsfeedsandmessages.bindSentMessagesToDataTable(user, "");
                        Session["InboxMessages"] = ds;
                    }
                    else
                    {
                        ds = (DataSet)Session["InboxMessages"];
                    }
                    string message = this.BindData(ds.Tables[0]);
                    Response.Write(message);

                }
                else if (Request.QueryString["op"] == "bindProfiles")
                {

                    string profiles = string.Empty;
                    int i = 0;
                    profiles += "<ul class=\"options_list\">";

                    /*Binding facebook profiles in Accordian*/
                    FacebookAccountRepository facerepo = new FacebookAccountRepository();
                    ArrayList alstfacebookprofiles = facerepo.getOnlyFacebookAccountsOfUser(user.Id);
                    foreach (FacebookAccount item in alstfacebookprofiles)
                    {
                        try
                        {
                            profiles += "<ul><li><a id=\"checkimg_" + i + "\" href=\"#\" onclick=\"checkprofile('checkimg_" + i + "','" + item.FbUserId + "','message','facebook');\"><img src=\"../Contents/img/admin/fbicon.png\"  width=\"15\" height=\"15\" alt=\"\" >" + item.FbUserName + "</a></li>";
                            i++;
                        }
                        catch (Exception ex)
                        {
                            logger.Error(ex.Message);
                            Console.WriteLine(ex.Message);
                        }

                    }


                    /*Binding TwitterProfiles in Accordian*/
                    TwitterAccountRepository twtaccountrepo = new TwitterAccountRepository();
                    ArrayList alsttwt = twtaccountrepo.getAllTwitterAccountsOfUser(user.Id);
                    foreach (TwitterAccount item in alsttwt)
                    {
                        try
                        {
                            profiles += "<ul><li><a href=\"#\" id=\"checkimg_" + i + "\" onclick=\"checkprofile('checkimg_" + i + "','" + item.TwitterUserId + "','message','twitter');\"><img src=\"../Contents/img/admin/twittericon.png\"  width=\"15\" height=\"15\" alt=\"\" >" + item.TwitterScreenName + "</a></li>";
                            i++;
                        }
                        catch (Exception ex)
                        {
                            logger.Error(ex.Message);
                            Console.WriteLine(ex.Message);
                        }
                    }

                    GooglePlusAccountRepository gpAccRepo = new GooglePlusAccountRepository();
                    ArrayList alstgp = gpAccRepo.getAllGooglePlusAccountsOfUser(user.Id);
                    foreach (GooglePlusAccount item in alstgp)
                    {
                        try
                        {
                            profiles += "<ul><li><a href=\"#\" id=\"checkimg_" + i + "\" onclick=\"checkprofile('checkimg_" + i + "','" + item.GpUserId + "','message','googleplus');\"><img src=\"../Contents/img/google_plus.png\"  width=\"15\" height=\"15\" alt=\"\" >" + item.GpUserName + "</a></li>";
                            i++;
                        }
                        catch (Exception esx)
                        {
                            logger.Error(esx.Message);
                            Console.WriteLine(esx.Message);
                        }
                    }
                    profiles += "</ul><input type=\"hidden\" id=\"profilecounter\" value=\"" + i + "\">";
                    Response.Write(profiles);
                }
                else if (Request.QueryString["op"] == "changeTaskStatus")
                {
                    Guid taskid = Guid.Parse(Request.QueryString["taskid"]);
                    bool status = bool.Parse(Request.QueryString["status"]);

                    if (status == true)
                        status = false;
                    else
                        status = true;
                    TaskRepository objTaskRepo = new TaskRepository();
                    objTaskRepo.updateTaskStatus(taskid, user.Id, status);

                }
                else if (Request.QueryString["op"] == "savetask")
                {
                    string descritption = Request.QueryString["description"];
                    Guid idtoassign = Guid.Empty;
                    try
                    {
                        if (Request.QueryString["memberid"] != string.Empty)
                        {
                            idtoassign = Guid.Parse(Request.QueryString["memberid"].ToString());
                        }
                    }
                    catch (Exception ex)
                    {
                        logger.Error(ex.Message);
                        // idtoassign = 0;
                    }
                    Tasks objTask = new Tasks();
                    TaskRepository objTaskRepo = new TaskRepository();
                    objTask.AssignDate = DateTime.Now.ToString("yyyy-MM-dd H:mm:ss").ToString();
                    objTask.AssignTaskTo = idtoassign;
                    objTask.TaskStatus = false;
                    objTask.TaskMessage = descritption;
                    objTask.UserId = user.Id;
                    Guid taskid = Guid.NewGuid();
                    objTask.Id = taskid;
                    objTaskRepo.addTask(objTask);

                    /////////////////       
                    string comment = Request.QueryString["comment"];
                    if (!string.IsNullOrEmpty(comment))
                    {
                        string curdate = DateTime.Now.ToString("yyyy-MM-dd H:mm:ss").ToString();
                        TaskComment objcmt = new TaskComment();
                        TaskCommentRepository objcmtRepo = new TaskCommentRepository();
                        objcmt.Comment = comment;
                        objcmt.CommentDate = DateTime.Now;
                        objcmt.EntryDate = DateTime.Now;
                        objcmt.Id = Guid.NewGuid();
                        objcmt.TaskId = objTask.Id;
                        objcmt.UserId = user.Id;
                        objcmtRepo.addTaskComment(objcmt);
                    }

                }
                else if (Request.QueryString["op"] == "bindteam")
                {
                    TeamRepository objTeam = new TeamRepository();
                    string message = string.Empty;
                    message += "<ul>";
                    IEnumerable<dynamic> result = objTeam.getAllTeamsOfUser(user.Id);

                    if (result != null)
                    {
                        foreach (Team item in result)
                        {

                            message += "<li><a>";
                            message += "<img src=\"../Contents/img/blank_img.png\" alt=\"\" />";
                            message += "<span class=\"name\">" +
                                                     item.FirstName + " " + item.LastName +
                                                 "</span>" +
                                              " <span>" +
                                              "<input id=\"customerid_" + item.Id + "\" type=\"radio\" name=\"team_members\" value=\"customerid_" + item.Id + "\">" +
                                              "</span>" +
                                             "</a></li>";
                        }

                        message += "<li><a>";
                        if (string.IsNullOrEmpty(user.ProfileUrl))
                        {

                            message += "<img src=\"../Contents/img/blank_img.png\" alt=\"\" />";
                        }
                        else
                        {
                            message += "<img src=\"" + user.ProfileUrl + "\" alt=\"\" />";
                        }

                        message += "<span class=\"name\">" +
                                 user.UserName +
                              "</span>" +
                           " <span>" +
                           "<input id=\"customerid_" + user.Id + "\" type=\"radio\" name=\"team_members\" value=\"customerid_" + user.Id + "\">" +
                           "</span></a></li>";

                    }
                    else
                    {
                        message += "<li><a>";

                        if (string.IsNullOrEmpty(user.ProfileUrl))
                        {

                            message += "<img src=\"../Contents/img/blank_img.png\" alt=\"\" />";
                        }
                        else
                        {
                            message += "<img src=\"" + user.ProfileUrl + "\" alt=\"\" />";
                        }



                        message += "<span class=\"name\">" +
                                            user.UserName +
                                         "</span>" +
                                      " <span>" +
                                      "<input id=\"customerid_" + user.Id + "\" type=\"radio\" name=\"team_members\" value=\"customerid_" + user.Id + "\">" +
                                      "</span>" +
                                     "</a></li>";

                    }
                    message += "</ul>";
                    Response.Write(message);
                }
                else if (Request.QueryString["op"] == "bindarchive")
                {
                    ArchiveMessageRepository objArchiveRepo = new ArchiveMessageRepository();

                    string message = string.Empty;
                    message += "<ul  id=\"message-list\">";
                    List<ArchiveMessage> result = objArchiveRepo.getAllArchiveMessage(user.Id);
                    int sorteddatacount = 0;
                    if (result != null)
                    {
                        foreach (ArchiveMessage item in result)
                        {
                            message += "<li>";
                            sorteddatacount++;
                            if (item.SocialGroup == "twitter")
                            {
                                message += "<div id=\"messagetaskable_" + sorteddatacount + "\" class=\"userpictiny\"><div style=\"width:60px;height:60px;float:left\"><img id=\"formprofileurl_" + sorteddatacount + "\" onclick=\"detailsprofile(this.alt);\" src=\"" + item.ImgUrl + "\" height=\"48\" width=\"48\" alt=\"\" title=\"\" />" +
                                             "<a href=\"#\" class=\"userurlpic\" title=\"\"><img src=\"../Contents/img/twticon.png\" width=\"16\" height=\"16\" alt=\"\" onclick=\"detailsprofile(this.alt);\"></a></div>" +
                                             "</div><div id=\"messagedescription_" + sorteddatacount + "\" class=\"message-list-content\"><div  id=\"msgdescription_" + sorteddatacount + "\" style=\"width:500px;height:auto;float:left\"><p>" + item.Message + "</p>" +
                                                 "<div class=\"message-list-info\"><span><a href=\"#\" id=\"rowname_" + sorteddatacount + "\" onclick=\"detailsprofile(" + item.ProfileId + ");\">" + item.ProfileId + "</a> " + item.CreatedDateTime + "</span>" +
                                                 "<div class=\"scl\">" +
                                                 "<a id=\"createtasktwt_" + sorteddatacount + "\" href=\"#\" onclick=\"createtask(this.id);\"><img title=\"Task\" src=\"../Contents/img/pin.png\" alt=\"\" width=\"14\" height=\"17\" border=\"none\"></a><a href=\"#\"><img title=\"Comment\" src=\"../Contents/img/admin/goto.png\" width=\"12\" height=\"12\" alt=\"\"/></a></div></div></div></div></li>";
                            }
                            else if (item.SocialGroup == "facebook")
                            {
                                message += "<div id=\"messagetaskable_" + sorteddatacount + "\" class=\"userpictiny\"><div style=\"width:60px;height:60px;float:left\"><img id=\"formprofileurl_" + sorteddatacount + "\" onclick=\"detailsprofile(this.alt);\" src=\"" + item.ImgUrl + "\" height=\"48\" width=\"48\" alt=\"\" title=\"\" />" +
                                            "<a href=\"#\" class=\"userurlpic\" title=\"\"><img src=\"../Contents/img/fb_icon.png\" width=\"16\" height=\"16\" alt=\"\" onclick=\"detailsprofile(this.alt);\"></a></div>" +
                                            "</div><div id=\"messagedescription_" + sorteddatacount + "\" class=\"message-list-content\"><div  id=\"msgdescription_" + sorteddatacount + "\" style=\"width:500px;height:auto;float:left\"><p>" + item.Message + "</p>" +
                                                "<div class=\"message-list-info\"><span><a href=\"#\" id=\"rowname_" + sorteddatacount + "\" onclick=\"getFacebookProfiles(" + item.ProfileId + ");\">" + item.ProfileId + "</a> " + item.CreatedDateTime + "</span>" +
                                                "<div class=\"scl\">" +
                                                "<a id=\"createtasktwt_" + sorteddatacount + "\" href=\"#\" onclick=\"createtask(this.id);\"><img title=\"Task\" src=\"../Contents/img/pin.png\" alt=\"\" width=\"14\" height=\"17\" border=\"none\"></a><a href=\"#\"><img title=\"Comment\" src=\"../Contents/img/admin/goto.png\" width=\"12\" height=\"12\" alt=\"\"/></a></div></div></div></div></li>";

                            }
                            else if (item.SocialGroup == "googleplus")
                            {
                                message += "<div id=\"messagetaskable_" + sorteddatacount + "\" class=\"userpictiny\"><div style=\"width:60px;height:60px;float:left\"><img id=\"formprofileurl_" + sorteddatacount + "\" onclick=\"detailsprofile(this.alt);\" src=\"" + item.ImgUrl + "\" height=\"48\" width=\"48\" alt=\"\" title=\"\" />" +
                                            "<a href=\"#\" class=\"userurlpic\" title=\"\"><img src=\"../Contents/img/google_plus.png\" width=\"16\" height=\"16\" alt=\"\" onclick=\"detailsprofile(this.alt);></a></div>" +
                                            "</div><div id=\"messagedescription_" + sorteddatacount + "\" class=\"message-list-content\"><div  id=\"msgdescription_" + sorteddatacount + "\" style=\"width:500px;height:auto;float:left\"><p>" + item.Message + "</p>" +
                                                "<div class=\"message-list-info\"><span><a href=\"#\" id=\"rowname_" + sorteddatacount + "\" onclick=\"detailsprofile(" + item.ProfileId + ");\">" + item.ProfileId + "</a> " + item.CreatedDateTime + "</span>" +
                                                "<div class=\"scl\">" +
                                                "<a id=\"createtasktwt_" + sorteddatacount + "\" href=\"#\" onclick=\"createtask(this.id);\"><img title=\"Task\" src=\"../Contents/img/pin.png\" alt=\"\" width=\"14\" height=\"17\" border=\"none\"></a></div></div></div></div></li>";

                            }
                            message += "</li>";
                        }

                    }
                    message += "</ul>";
                    Response.Write(message);
                }
                else if (Request.QueryString["op"] == "savearchivemsg")
                {
                    User use = (User)Session["LoggedUser"];
                    ArchiveMessage am = new ArchiveMessage();
                    ArchiveMessageRepository objArchiveRepo = new ArchiveMessageRepository();
                    am.UserId = user.Id;
                    am.ImgUrl = Request.QueryString["imgurl"];
                    //am.user_name = Request.QueryString["UserName"];
                    //am.msg = Request.QueryString["Msg"];
                    ////am.sociel_group = Request.QueryString["Network"];
                    //am.createdtime = Request.QueryString["CreatedTime"];

                    System.IO.StreamReader sr = new System.IO.StreamReader(Request.InputStream);
                    string line = "";
                    line = sr.ReadToEnd();
                    // JObject jo = JObject.Parse("[" + line + "]");
                    // am.UserName = Request.QueryString["UserName"];//Server.UrlDecode((string)jo["UserName"]);
                    //am.Message = Request.QueryString["Msg"];//Server.UrlDecode((string)jo["Msg"]);
                    JObject jo = JObject.Parse(line);
                    am.Message = Server.UrlDecode((string)jo["Msg"]); ;//Server.UrlDecode((string)jo["Msg"]);
                    am.SocialGroup = Request.QueryString["Network"];// Server.UrlDecode((string)jo["Network"]);
                    am.CreatedDateTime = Request.QueryString["CreatedTime"];
                    am.MessageId = Request.QueryString["MessageId"];
                    am.ProfileId = Request.QueryString["ProfileId"];
                    am.UserId = use.Id;

                    // Server.UrlDecode((string)jo["CreatedTime"]);

                    if (am.UserName != string.Empty)
                    {
                        if (!objArchiveRepo.checkArchiveMessageExists(user.Id, am.MessageId))
                        {
                            objArchiveRepo.AddArchiveMessage(am);
                            Response.Write("Message Archive Successfully");
                        }
                        else
                        {
                            Response.Write("Message Already in Archive");
                        }
                    }
                }
                else if (Request.QueryString["op"] == "createfacebookcomments")
                {
                    FacebookAccountRepository facerepo = new FacebookAccountRepository();
                    string postid = Request.QueryString["replyid"];
                    string message = Request.QueryString["replytext"];
                    string userid = Request.QueryString["userid"];
                    FacebookAccount result = facerepo.getFacebookAccountDetailsById(userid, user.Id);

                    FacebookClient fc = new FacebookClient(result.AccessToken);
                    Dictionary<string, object> parameters = new Dictionary<string, object>();
                    parameters.Add("message", message);
                    JsonObject dyn = (JsonObject)fc.Post("/" + postid+ "/comments", parameters);

                }
                else if (Request.QueryString["op"] == "getFacebookComments")
                {
                    FacebookAccountRepository facerepo = new FacebookAccountRepository();
                    string postid = Request.QueryString["postid"];
                    string userid = Request.QueryString["userid"];
                    FacebookAccount result = facerepo.getFacebookAccountDetailsById(userid, user.Id);

                    FacebookClient fc = new FacebookClient(result.AccessToken);
                    JsonObject dyn = (JsonObject)fc.Get("/" + postid + "/comments");
                    string mess = string.Empty;
                    dynamic jc = dyn["data"];
                    int iii = 0;
                    foreach (dynamic item in jc)
                    {
                        mess += "<div class=\"messages\"><section><aside><section class=\"js-avatar_tip\" data-sstip_class=\"twt_avatar_tip\">" +
                                "<a class=\"avatar_link view_profile\">" +
                                "<img width=\"54\" height=\"54\" border=\"0\" id=\"" + item["id"] + "\" class=\"avatar\" src=\"http://graph.facebook.com/" + item["from"]["id"] + "/picture?type=small\"><article class=\"message-type-icon\"></article>" +
                                 "</a></section><ul></ul></aside><article><div class=\"\"><a class=\"language\" href=\"\"></a></div>" +
                                 "<div class=\"message_actions\"><a class=\"gear_small\" href=\"#\"><span title=\"Options\" class=\"ficon\">?</span></a></div><div id=\"messagedescription_" + iii + "\" class=\"message-text font-14\">" + item["message"] + "</div><section class=\"bubble-meta\"><article class=\"threefourth text-overflow\"><section class=\"floatleft\"><a class=\"js-avatar_tip view_profile profile_link\" data-sstip_class=\"twt_avatar_tip\"><span id=\"rowname_" + iii + "\">" + item["from"]["name"] + "</span></a>&nbsp;<a data-msg-time=\"1363926699000\" class=\"time\" target=\"_blank\" title=\"View message on Twitter\">" + item["created_time"] + "</a><span class=\"location\">&nbsp;</span></section></article><ul class=\"message-buttons quarter clearfix\"></ul></section></article></section></div>";
                    }
                    Response.Write(mess);

                }
                else if (Request.QueryString["op"] == "twittercomments")
                {

                    Tweet objTwitterMethod = new Tweet();
                    TwitterAccountRepository objTwtAccRepo = new TwitterAccountRepository();
                    try
                    {
                        string messid = Request.QueryString["messid"];
                        string replytext = Request.QueryString["replytext"];
                        string replyid = Request.QueryString["replyid"];
                        string userid = Request.QueryString["userid"];
                        string username = Request.QueryString["username"];
                        string rowid = Request.QueryString["rowid"];
                        TwitterAccount objTwtAcc = objTwtAccRepo.getUserInformation(user.Id, userid);

                        TwitterHelper twthelper = new TwitterHelper();

                        oAuthTwitter OAuthTwt = new oAuthTwitter();
                        OAuthTwt.AccessToken = objTwtAcc.OAuthToken;
                        OAuthTwt.AccessTokenSecret = objTwtAcc.OAuthSecret;
                        OAuthTwt.TwitterScreenName = objTwtAcc.TwitterScreenName;
                        twthelper.SetCofigDetailsForTwitter(OAuthTwt);
                        Tweet twt = new Tweet();
                        JArray post = twt.Post_Statuses_Update(OAuthTwt, replytext);
                    }
                    catch (Exception ex)
                    {
                        logger.Error(ex.Message);
                    }
                }
                else if (Request.QueryString["op"] == "gpProfile")
                {
                    GooglePlusAccountRepository objgpAccRepo = new GooglePlusAccountRepository();
                    GooglePlusAccount objGpAcc = objgpAccRepo.getGooglePlusAccountDetailsById(Request.QueryString["gpid"].ToString(), user.Id);
                    PeopleController obj = new PeopleController();
                    oAuthToken objgpToken = new oAuthToken();
                    JArray objProfile = null;
                    try
                    {
                        string strAccess = objgpToken.GetAccessToken(objGpAcc.RefreshToken);
                        if (!strAccess.StartsWith("["))
                            strAccess = "[" + strAccess + "]";
                        JArray objArray = JArray.Parse(strAccess);
                        foreach (var itemgp in objArray)
                        {
                            objGpAcc.AccessToken = itemgp["access_token"].ToString();
                        }
                        objProfile = obj.GetPeopleProfile(Request.QueryString["gpid"].ToString(), objGpAcc.AccessToken);
                    }
                    catch (Exception Err)
                    {
                        logger.Error(Err.Message);
                        Console.Write(Err.Message.ToString());
                    }
                    string jas = string.Empty;
                      foreach (var item in objProfile)
                    {
                        jas += "<div class=\"modal-small draggable\">";
	                    jas += "<div class=\"modal-content\">";
		                jas += "<button type=\"button\" class=\"modal-btn button b-close\">";
			            jas += "<span class=\"icon close-medium\"><span class=\"visuallyhidden\">X</span></span></button>";
		                jas += "<div class=\"modal-header\"><h3 class=\"modal-title\">Profile summary</h3></div>";
		                jas += "<div class=\"modal-body profile-modal\">";
			            jas += "<div class=\"module profile-card component profile-header\">";
				        jas += "<div style=\"background-image: url('https://pbs.twimg.com/profile_banners/215936249/1371721359');\" class=\"profile-header-inner flex-module clearfix\">";
					    jas += "<div class=\"profile-header-inner-overlay\"></div>";
						jas += "<a href=\"#\" class=\"profile-picture media-thumbnail js-nav\">";
                        string[] imgurl = item["image"]["url"].ToString().Split('?');
                        jas += "<img src=\"" + imgurl[0] + " alt=\"" + item["name"]["givenName"] + "\" class=\"avatar size73\"></a>";
						jas += "<div class=\"profile-card-inner\">";
						jas += "<h1 class=\"fullname editable-group\">";
                        jas += "<a class=\"js-nav\" href=\"#\">" + item["name"]["givenName"] + "</a>";
						jas += "<a href=\"#\" class=\"verified-link js-tooltip\">";
						jas += "<span class=\"icon verified verified-large-border\">";
						jas += "<span class=\"visuallyhidden\"></span></span></a></h1>";
                        jas += "<h2 class=\"username\">";
						jas += "<a class=\"pretty-link js-nav\" href=\"#\">";
                        jas += "<span class=\"screen-name\"><s></s>" + item["displayName"] + "</span></a></h2>";
						jas += "<div class=\"bio-container editable-group\"><p class=\"bio profile-field\"></p></div>";
						jas += "<p class=\"location-and-url\">";
						jas += "<span class=\"location-container editable-group\">";
						jas += "<span class=\"location profile-field\"></span></span>";
						jas += "<span class=\"divider hidden\"></span> ";
						jas += "<span class=\"url editable-group\">  ";
						jas += "<span class=\"profile-field\">";
                        jas += "<a target=\"_blank\" rel=\"me nofollow\" href=\"" + item["url"] + "\" title=\"#\">" + item["url"] + " </a></span></span></p>";
						jas += "<div style=\"cursor: pointer; width: 16px; height: 16px; display: inline-block;\">&nbsp;</div><p></p></div></div>";
					    jas += "<div class=\"clearfix\"><div class=\"default-footer\">";
						jas += "<div class=\"btn-group\"><div class=\"follow_button\"></div></div></div></div>";
				        jas += "<div class=\"profile-social-proof\">";
					    jas += "<div class=\"follow-bar\"></div></div></div>";
			            jas += "<ol class=\"recent-tweets\"><li class=\"\"><div><i class=\"dogear\"></i></div></li></ol>";
			            jas += "<div class=\"go_to_profile\">";
				        jas += "<small><a class=\"view_profile\" target=\"_blank\" href=\""+ item["url"] +"\">Go to full profile →</a></small></div></div>";
		                jas += "<div class=\"loading\"><span class=\"spinner-bigger\"></span></div></div></div>";
                    }
                    Response.Write(jas);
                }
                else if (Request.QueryString["op"] == "updatedstatus")
                {
                    try
                    {
                        TwitterMessageRepository twtmsgRepo = new TwitterMessageRepository();
                        int i = twtmsgRepo.updateMessageStatus(user.Id);
                        FacebookFeedRepository fbfeedRepo = new FacebookFeedRepository();
                        int j = fbfeedRepo.updateMessageStatus(user.Id);

                        if (i > 0 || j > 0)
                        {
                            Session["CountMessages"] = 0;
                            Session["MessageDataTable"] = null;


                            DataSet ds = null;
                            if (Session["MessageDataTable"] == null)
                            {
                                clsFeedsAndMessages clsfeedsandmess = new clsFeedsAndMessages();
                                ds = clsfeedsandmess.bindMessagesIntoDataTable(user);
                                FacebookFeedRepository fbFeedRepo = new FacebookFeedRepository();
                                Session["MessageDataTable"] = ds;
                            }
                            else
                            {
                                ds = (DataSet)Session["MessageDataTable"];
                            }
                        }


                    }
                    catch (Exception ex)
                    {
                        logger.Error(ex.Message);
                        Console.WriteLine(ex.Message);

                    }
                }
            }
        }

        public string BindData(DataTable dt)
        {
            string message = string.Empty;
            try
            {
                if (Session["CountMessages"] != null)
                {
                    string count = Convert.ToString((int)Session["CountMessages"]);
                    if (count == "0")
                    {
                        //message = "No Message found";

                    }

                }
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                Console.WriteLine(ex.Message);
            }

            //string message = string.Empty;
            DataView dv = dt.DefaultView;
            dv.Sort = "ReadStatus ,MessageDate desc";
            DataTable sortedDT = dv.ToTable();
            int sorteddatacount = 0;

            if (sortedDT.Rows.Count > 0)
            {
                // DataRow[] array_dr = dt.Select("MessageDate like '%'", "MessageDate desc");
                User use = (User)Session["LoggedUser"];
                message += "<ul id=\"message-list\">";
                UrlExtractor urlextarct = new UrlExtractor();
                foreach (DataRow row in sortedDT.Rows)
                {
                    if (row["Network"].ToString() == "twitter")
                    {
                        try
                        {
                            if (row["ReadStatus"].ToString() == "0")
                            {
                                message += "<li class=\"unread\">";
                            }
                            else
                            {
                                message += "<li>";
                            }
                            message += "<div id=\"messagetaskable_" + sorteddatacount + "\" class=\"userpictiny\"><div style=\"width:60px;height:60px;float:left\"><img id=\"formprofileurl_" + sorteddatacount + "\" onclick=\"detailsprofile(this.alt);\" src=\"" + row["FromProfileUrl"] + "\" height=\"48\" width=\"48\" alt=\"" + row["FromId"] + "\" title=\"" + row["FromName"] + "\" />" +
                                                   "<a href=\"#\" class=\"userurlpic\" title=\"\"><img src=\"../Contents/img/twticon.png\" width=\"16\" height=\"16\" alt=\"\"></a></div><span id=\"network_" + sorteddatacount + "\" style=\"display:none;\">twitter</span>" +
                                                   "</div><div id=\"messagedescription_" + sorteddatacount + "\" class=\"message-list-content\"><div id=\"msgdescription_" + sorteddatacount + "\" style=\"width:500px;height:auto;float:left\"><p>";

                            string[] str = urlextarct.splitUrlFromString(row["Message"].ToString());

                            foreach (string substritem in str)
                            {
                                if (!string.IsNullOrEmpty(substritem))
                                {
                                    if (substritem.Contains("http"))
                                    {
                                        message += "<a target=\"_blank\" href=\"" + substritem + "\">" + substritem + "</a>";
                                    }
                                    else
                                    {
                                        message += substritem;
                                    }
                                }
                            }


                            message += "</p>" +
                                         "<div class=\"message-list-info\"><span><a href=\"#\" id=\"rowname_" + sorteddatacount + "\" onclick=\"detailsprofile('" + row["FromId"] + "');\">" + row["FromName"] + "</a><div id=\"createdtime_" + sorteddatacount + "\">" + row["MessageDate"].ToString() + "</div></span>" +
                                         "<div class=\"scl\">" +
                                         "<a id=\"createtasktwt_" + sorteddatacount + "\" href=\"#\" onclick=\"createtask(this.id);\"><img title=\"Task\" src=\"../Contents/img/pin.png\" alt=\"\" width=\"14\" height=\"17\" border=\"none\"></a><a href=\"#\"><img title=\"Comment\" src=\"../Contents/img/admin/goto.png\" width=\"12\" height=\"12\"  onclick=replyfunction(" + sorteddatacount + ",'twitter','" + row["MessageId"].ToString() + "','" + row["FromId"] + "') alt=\"\"/></a><a id=\"savearchive_" + sorteddatacount + "\" href=\"#\" onclick=\"savearchivemsg(" + sorteddatacount + ",'twitter','" + row["MessageId"].ToString() + "','" + row["ProfileId"].ToString() + "');\"><img title=\"Archive\" src=\"../Contents/img/archive.png\" alt=\"\" width=\"14\" height=\"17\" border=\"none\"></a></div></div></div></div></li>";
                        }
                        catch (Exception ex)
                        {
                            logger.Error(ex.Message);
                            Console.WriteLine(ex.Message);
                        }
                    }
                    else if (row["Network"].ToString() == "facebook")
                    {

                        try
                        {

                            if (row["ReadStatus"].ToString() == "0")
                            {
                                message += "<li class=\"unread\">";
                            }
                            else
                            {
                                message += "<li>";
                            }

                            message += "<div id=\"messagetaskable_" + sorteddatacount + "\" class=\"userpictiny\"><div style=\"width:60px;height:60px;float:left\"><img id=\"formprofileurl_" + sorteddatacount + "\" onclick=\"getFacebookProfiles(this.alt);\" src=\"" + row["FromProfileUrl"] + "\" height=\"48\" width=\"48\" alt=\"" + row["FromId"] + "\" title=\"" + row["FromName"] + "\" />" +
                                                       "<a href=\"#\" class=\"userurlpic\" title=\"\"><img src=\"../Contents/img/fb_icon.png\" width=\"16\" height=\"16\" alt=\"\"></a></div>" +
                                                       "</div><div id=\"messagedescription_" + sorteddatacount + "\" class=\"message-list-content\"><div  id=\"msgdescription_" + sorteddatacount + "\" style=\"width:500px;height:auto;float:left\"><p>";


                            string[] str = urlextarct.splitUrlFromString(row["Message"].ToString());
                            foreach (string substritem in str)
                            {
                                if (!string.IsNullOrEmpty(substritem))
                                {
                                    if (substritem.Contains("http"))
                                    {
                                        message += "<a target=\"_blank\" href=\"" + substritem + "\">" + substritem + "</a>";
                                    }
                                    else
                                    {
                                        message += substritem;
                                    }
                                }
                            }
                            message += "</p>" +
                                             "<div class=\"message-list-info\"><span><a href=\"#\" id=\"rowname_" + sorteddatacount + "\" onclick=\"getFacebookProfiles(" + row["FromId"] + ");\">" + row["FromName"] + "</a><div id=\"createdtime_" + sorteddatacount + "\">" + row["MessageDate"].ToString() + "</div></span>" +
                                             "<div class=\"scl\">" +
                                             "<a id=\"createtasktwt_" + sorteddatacount + "\" href=\"#\" onclick=\"createtask(this.id);\"><img title=\"Task\" src=\"../Contents/img/pin.png\" alt=\"\" width=\"14\" height=\"17\" border=\"none\"></a><a href=\"#\"><img title=\"Comment\" src=\"../Contents/img/admin/goto.png\"  width=\"12\" height=\"12\" onclick=replyfunction(" + sorteddatacount + ",'facebook','" + row["MessageId"].ToString() + "','" + row["FromId"] + "')  alt=\"\"/></a><a id=\"savearchive_" + sorteddatacount + "\" href=\"#\" onclick=\"savearchivemsg(" + sorteddatacount + ",'facebook','" + row["MessageId"].ToString() + "','" + row["ProfileId"].ToString() + "');\"><img title=\"Archive\" src=\"../Contents/img/archive.png\" alt=\"\" width=\"14\" height=\"17\" border=\"none\"></a></div></div></div></div></li>";
                        }
                        catch (Exception ex)
                        {
                            logger.Error(ex.Message);
                            Console.WriteLine(ex.Message);
                        }
                    }
                    else if (row["Network"].ToString() == "googleplus")
                    {
                        try
                        {

                            if (row["ReadStatus"].ToString() == "0")
                            {
                                message += "<li class=\"unread\">";
                            }
                            else
                            {
                                message += "<li>";
                            }
                            message += "<div id=\"messagetaskable_" + sorteddatacount + "\" class=\"userpictiny\"><div style=\"width:60px;height:60px;float:left\"><img id=\"formprofileurl_" + sorteddatacount + "\" onclick=\"detailsprofile(this.alt);\" src=\"" + row["FromProfileUrl"] + "\" height=\"48\" width=\"48\" alt=\"" + row["FromName"] + "\" title=\"" + row["FromName"] + "\" />" +
                                            "<a href=\"#\" class=\"userurlpic\" title=\"\"><img src=\"../Contents/img/google_plus.png\" width=\"16\" height=\"16\" alt=\"\"></a></div>" +
                                            "</div><div id=\"messagedescription_" + sorteddatacount + "\" class=\"message-list-content\"><div  id=\"msgdescription_" + sorteddatacount + "\" style=\"width:500px;height:auto;float:left\"><p>";

                            string[] str = urlextarct.splitUrlFromString(row["Message"].ToString());
                            foreach (string substritem in str)
                            {
                                if (!string.IsNullOrEmpty(substritem))
                                {
                                    if (substritem.Contains("http"))
                                    {
                                        message += "<a target=\"_blank\" href=\"" + substritem + "\">" + substritem + "</a>";
                                    }
                                    else
                                    {
                                        message += substritem;
                                    }
                                }
                            }

                            message += "</p>" +
                                             "<div class=\"message-list-info\"><span><a href=\"#\" id=\"rowname_" + sorteddatacount + "\" onclick=\"getGooglePlusProfiles('" + row["FromId"] + "');\">" + row["FromName"] + "</a><div id=\"createdtime_" + sorteddatacount + "\">" + row["MessageDate"].ToString() + "</div></span>" +
                                             "<div class=\"scl\">" +
                                             "<a id=\"createtasktwt_" + sorteddatacount + "\" href=\"#\" onclick=\"createtask(this.id);\"><img title=\"Task\" src=\"../Contents/img/pin.png\" alt=\"\" width=\"14\" height=\"17\" border=\"none\"></a><a href=\"#\"><img title=\"Comment\" src=\"../Contents/img/admin/goto.png\" width=\"12\" height=\"12\" alt=\"\"/></a><a id=\"savearchive_" + sorteddatacount + "\" href=\"#\" onclick=\"savearchivemsg(" + sorteddatacount + ",'googleplus','" + row["MessageId"].ToString() + "','" + row["ProfileId"].ToString() + "');\"><img title=\"Archive\" src=\"../Contents/img/archive.png\" alt=\"\" width=\"14\" height=\"17\" border=\"none\"></a></div></div></div></div></li>";
                        }
                        catch (Exception ex)
                        {
                            logger.Error(ex.Message);
                            Console.WriteLine(ex.Message);
                        }
                    }
                    sorteddatacount++;

                }
            }
            
            return message;


        }

    }
}
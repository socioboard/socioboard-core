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
using Newtonsoft.Json.Linq;
using GlobusLinkedinLib.Authentication;
using GlobusLinkedinLib.App.Core;
using GlobusLinkedinLib.LinkedIn.Core.SocialStreamMethods;
using System.IO;
using log4net;

namespace SocialScoup.Group
{
    public partial class AjaxGroup : System.Web.UI.Page
    {
        ILog logger = LogManager.GetLogger(typeof(AjaxGroup));
        string gid = string.Empty;
        string ack = string.Empty;
        string returndata = string.Empty;

        LinkedInAccountRepository linAccRepo = new LinkedInAccountRepository();
        LinkedInGroup objLinkedInGroup = new LinkedInGroup();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                System.IO.StreamReader sr;
                Newtonsoft.Json.Linq.JObject jo;

                try
                {
                    if (Request.QueryString["op"].ToString() == "postFBGroupFeeds")
                    {
                        sr = new System.IO.StreamReader(Request.InputStream);
                        string data = "";
                        data = sr.ReadToEnd();
                        jo = Newtonsoft.Json.Linq.JObject.Parse(data);
                        gid = Server.UrlDecode((string)jo["gid"]);
                        ack = Server.UrlDecode((string)jo["ack"]);
                        string msg = Server.UrlDecode((string)jo["msg"]);
                        
                        string res = PostFBGroupFeeds(ack, gid, msg);

                        Response.Write(res);
                        return;
                    }

                    else if (Request.QueryString["op"].ToString() == "postonselectedgroup")
                    {
                     try
                        {

                            logger.Error("cod is here");
                            SocioBoard.Domain.User user = (SocioBoard.Domain.User)Session["LoggedUser"];
                            ScheduledMessageRepository objScheduledMessageRepository = new ScheduledMessageRepository();
                            GroupScheduleMessageRepository objGroupScheduleMEssageRepository = new GroupScheduleMessageRepository();
                            ScheduledMessage schmessage = new ScheduledMessage();
                            GroupScheduleMessage grpschmessage = new GroupScheduleMessage();
                            string msg = string.Empty;
                            string title = string.Empty;
                            string intrval = string.Empty;
                            string fbuserid = string.Empty;
                            string linuserid = string.Empty;
                            string clienttime = string.Empty;
                            
                            var SelectedGroupId = Request.Form["gid"].ToString().Split(',');
                            title = Request.Form["title"].ToString();
                            msg = Request.Form["msg"].ToString();
                            intrval = Request.Form["intervaltime"].ToString();
                            fbuserid = Request.Form["fbuserid"].ToString();
                            linuserid = Request.Form["linuserid"].ToString();
                            clienttime = Request.Form["clienttime"].ToString();
                            string time = Request.Form["timeforsch"];
                            string date = Request.Form["dateforsch"];
                            var files = Request.Files.Count;
                            var fi = Request.Files["files"];
                            string file = string.Empty;
                            int intervaltime=0;
                            intervaltime = Convert.ToInt32(intrval);

                            Session["scheduletime"] = null;


                          
                            string filepath=string.Empty;
                 
                            if (Request.Files.Count > 0)
                            {
                                if (fi != null)
                                {
                                    var path = Server.MapPath("~/Contents/img/upload");

                                  
                                    filepath = path + "/" + fi.FileName;
                                    if (!Directory.Exists(path))
                                    {
                                        Directory.CreateDirectory(path);
                                    }
                                    fi.SaveAs(filepath);
                                }
                            }

                            foreach (var item in SelectedGroupId)
                            {
                                string[] networkingwithid = item.Split('_');
                               
                                if (networkingwithid[1] == "lin")
                                {
                                    try
                                    {
                                        string[] arrliusrid = linuserid.Split('_');
                                        string linkuserid = arrliusrid[1];
                                        string groupid = networkingwithid[2];
                                        string profileid = networkingwithid[0];
                                       
                                        if (intervaltime != 0)
                                        {
                                            if(Session["scheduletime"]==null)
                                            {
                                                  string servertime = this.CompareDateWithclient(clienttime, date + " " + time);
                                                  schmessage.ScheduleTime = Convert.ToDateTime(servertime);
                                                  DateTime d1 = schmessage.ScheduleTime;
                                                  DateTime d2 = d1.AddMinutes(intervaltime);
                                                  Session["scheduletime"] = d2;
                                            }
                                            else
                                            {
                                                DateTime d1 = (DateTime)Session["scheduletime"];
                                                schmessage.ScheduleTime = d1;
                                                DateTime d2 = d1.AddMinutes(intervaltime);
                                                Session["scheduletime"] = d2;
                                            }

                                        }
                                     
                                        SocialStream sociostream = new SocialStream();
                                        string message = title + "$%^_^%$" + msg;
                                        schmessage.CreateTime = DateTime.Now;
                                        schmessage.ProfileType = "linkedingroup";
                                        schmessage.ProfileId = profileid;
                                        schmessage.Id = Guid.NewGuid();
                                        if (Request.Files.Count > 0)
                                        {
                                           // schmessage.PicUrl = ConfigurationManager.AppSettings["MailSenderDomain"] + "Contents/img/upload/" + fi.FileName;
                                            var path = System.Configuration.ConfigurationManager.AppSettings["MailSenderDomain"] + "Contents/img/upload";
                                            file = path + "/" + fi.FileName;
                                            schmessage.PicUrl = file;
                                          
                                        }
                                        else
                                        {
                                            schmessage.PicUrl = "Null";
                                        }

                                        schmessage.ClientTime = Convert.ToDateTime(clienttime);
                                        schmessage.ShareMessage = message; ;
                                        schmessage.UserId = user.Id;
                                        schmessage.Status = false;


                                        logger.Error("cod is befor insert in schedule message");
                                        objScheduledMessageRepository.addNewMessage(schmessage);

                                        grpschmessage.Id = Guid.NewGuid();
                                        grpschmessage.ScheduleMessageId = schmessage.Id;
                                        grpschmessage.GroupId = groupid;
                                        objGroupScheduleMEssageRepository.addNewGroupMessage(grpschmessage);

                                    }
                                    catch (Exception ex)
                                    {
                                        logger.Error("cod is in exception");
                                        logger.Error(ex.StackTrace);
                                        Console.WriteLine(ex.StackTrace);
                                    }
                                }
                                else if (networkingwithid[1] == "fb")
                                {
                                    try
                                    {
                                        string facebookgrouppost = string.Empty;
                                        string[] arrfbusrid = fbuserid.Split('_');
                                        string acccesstkn = arrfbusrid[1];
                                        string groupid = networkingwithid[2];
                                        string profileid = networkingwithid[0];

                                        if (intervaltime != 0)
                                        {
                                            if (Session["scheduletime"] == null)
                                            {
                                                string servertime = this.CompareDateWithclient(clienttime, date + " " + time);
                                                schmessage.ScheduleTime = Convert.ToDateTime(servertime);
                                                DateTime d1 = schmessage.ScheduleTime;
                                                DateTime d2 = d1.AddMinutes(intervaltime);
                                                Session["scheduletime"] = d2;
                                            }
                                            else
                                            {
                                                DateTime d1 = (DateTime)Session["scheduletime"];
                                                schmessage.ScheduleTime = d1;
                                                DateTime d2 = d1.AddMinutes(intervaltime);
                                                Session["scheduletime"] = d2;
                                            }
                                        }                                                                              
                                        schmessage.CreateTime = DateTime.Now;
                                        schmessage.ProfileType = "facebookgroup";
                                        schmessage.ProfileId = profileid;
                                        schmessage.Id = Guid.NewGuid();
                                        if (Request.Files.Count > 0)
                                        {
                                           // schmessage.PicUrl = ConfigurationManager.AppSettings["MailSenderDomain"] + "Contents/img/upload/" + fi.FileName;
                                            var path = System.Configuration.ConfigurationManager.AppSettings["MailSenderDomain"] + "Contents/img/upload";
                                            file = path + "/" + fi.FileName;
                                            schmessage.PicUrl = file;
                                        }
                                        else
                                        {
                                            schmessage.PicUrl = "Null";
                                        }

                                        schmessage.ClientTime = Convert.ToDateTime(clienttime);                                
                                        schmessage.ShareMessage = msg;
                                        schmessage.UserId = user.Id;
                                        schmessage.Status = false;
                                        objScheduledMessageRepository.addNewMessage(schmessage);
                                        grpschmessage.Id = Guid.NewGuid();
                                        grpschmessage.ScheduleMessageId = schmessage.Id;
                                        grpschmessage.GroupId = groupid;
                                        objGroupScheduleMEssageRepository.addNewGroupMessage(grpschmessage);                                      
                                    }
                                    catch (Exception ex)
                                    {
                                        Console.WriteLine(ex.StackTrace);
                                    }
                                }

                            }//End For Each 
                          
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.StackTrace);
                        }
                        Response.Write("success");                    
                    }

                    else if (Request.QueryString["op"].ToString() == "getlinkedInGroupDetails")
                    {
                        string GroupData = string.Empty;
                        string picurl = string.Empty;
                        string summary = string.Empty;

                        string groupid = Request.QueryString["groupid"].ToString();
                        string LinkedinUserId = Request.QueryString["linkuserid"].ToString();


                        LinkedInAccount arrLinkedinAccoount = linAccRepo.getLinkedinAccountDetailsById(LinkedinUserId);
                        oAuthLinkedIn objoAuthLinkedIn = new oAuthLinkedIn();
                        objoAuthLinkedIn.Token = arrLinkedinAccoount.OAuthToken;
                        objoAuthLinkedIn.Verifier = arrLinkedinAccoount.OAuthVerifier;
                        objoAuthLinkedIn.TokenSecret = arrLinkedinAccoount.OAuthSecret;
                        List<GlobusLinkedinLib.App.Core.LinkedInGroup.Group_Updates> lstlinkedinGroup = GetGroupPostDetail(objoAuthLinkedIn, 50, groupid);

                        try
                        {
                            foreach (var item in lstlinkedinGroup)
                            {
                                picurl = "";
                                if (string.IsNullOrEmpty(item.pictureurl))
                                {
                                    picurl = "../../Contents/img/blank_img.png";
                                }
                                else
                                {
                                    picurl = item.pictureurl;
                                }

                                if (string.IsNullOrEmpty(item.summary))
                                {
                                    summary = ".";
                                }
                                else
                                {
                                    summary = item.summary;
                                }

                                GroupData += "<div id=\"abhay\" class=\"storyContent\"><a class=\"actorPhoto\"><img src=\"" + picurl + "\"  alt=\"\" style=\"width:56px;height:56px\"></a>" +
                              "<div class=\"storyInnerContent\"><div class=\"actordescription\"><a class=\"passiveName\">" + item.firstname + " " + item.lastname + "  -    " + item.headline + "</a></div>" +
                              "<div class=\"messagebody\"><div style=\"color: black;font-size: large;margin-bottom: 15\">" + Server.HtmlEncode(item.title) + "</div>" + summary + "</div>" +
                               "</div>" +
                                    "<p style=\"margin-left:60px\">comments(" + item.comments_total + ") likes- " + item.likes_total + "</p><p><span class=\"comment\" onclick=\"FollowPosts('" + groupid + "','" + item.GpPostid + "','" + LinkedinUserId + "','" + item.isFollowing + "')\">" + getfollow(item.isFollowing) + "</span></p>" +
                                    "<p><span class=\"comment\" onclick=\"LikePosts('" + groupid + "','" + item.GpPostid + "','" + LinkedinUserId + "','" + item.isLiked + "')\">" + getlike(item.isLiked) + "</span></p>" +
                                    "<p><span id=\"commentlin_" + item.GpPostid + "\" class=\"comment\" onclick=\"CommentOnPosts('" + item.GpPostid + "')\">Comment</span></p>" +


                                       "<p class=\"commeent_box\"><input id=\"textlin_" + item.GpPostid + "\" type=\"text\" class=\"put_comments\"></p>" +
                                     "<p><span onclick=\"commentLin('" + groupid + "','" + item.GpPostid + "','" + LinkedinUserId + "')\" id=\"oklin_" + item.GpPostid + "\" class=\"ok\">ok</span><span id=\"cancellin_" + item.GpPostid + "\" onclick=\"cancelLin('" + item.GpPostid + "');\" class=\"cancel\"> cancel</span></p>" +




                                    "</div>";



                            }

                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.StackTrace);
                        }

                        Response.Write(GroupData);
                        return;

                    }



                    else if (Request.QueryString["op"].ToString() == "linkedCommentOnPost")
                    {
                        try
                        {
                            string message = Request.QueryString["message"].ToString();
                            string groupid = Request.QueryString["groupid"].ToString();
                            string LinkedinUserId = (Request.QueryString["LinkedinUserId"]);
                            string GpPostid = (Request.QueryString["GpPostid"]);


                            LinkedInAccount arrLinkedinAccoount = linAccRepo.getLinkedinAccountDetailsById(LinkedinUserId);
                            oAuthLinkedIn objoAuthLinkedIn = new oAuthLinkedIn();
                            objoAuthLinkedIn.Token = arrLinkedinAccoount.OAuthToken;
                            objoAuthLinkedIn.Verifier = arrLinkedinAccoount.OAuthVerifier;
                            objoAuthLinkedIn.TokenSecret = arrLinkedinAccoount.OAuthSecret;
                            SocialStream sociostream = new SocialStream();
                            string res = sociostream.SetCommentOnPost(objoAuthLinkedIn, GpPostid, message);

                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.StackTrace);
                        }

                    }






                    else if (Request.QueryString["op"].ToString() == "FollowPost")
                    {
                        try
                        {
                            string msg = string.Empty;
                            string postid = Request.QueryString["groupid"].ToString();
                            string LinkedinUserId = (Request.QueryString["LinkedinUserId"]);
                            int isFollowing = Convert.ToInt16(Request.QueryString["isFollowing"]);

                            if (isFollowing == 1)
                            {
                                msg = "false";
                            }
                            else
                            { msg = "true"; }


                            LinkedInAccount arrLinkedinAccoount = linAccRepo.getLinkedinAccountDetailsById(LinkedinUserId);
                            oAuthLinkedIn objoAuthLinkedIn = new oAuthLinkedIn();
                            objoAuthLinkedIn.Token = arrLinkedinAccoount.OAuthToken;
                            objoAuthLinkedIn.Verifier = arrLinkedinAccoount.OAuthVerifier;
                            objoAuthLinkedIn.TokenSecret = arrLinkedinAccoount.OAuthSecret;


                            SocialStream sociostream = new SocialStream();
                            string res = sociostream.SetFollowCountUpdate(objoAuthLinkedIn, postid, msg);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.StackTrace);
                        }



                    }

                    else if (Request.QueryString["op"].ToString() == "postLinkedInGroupFeeds")
                    {
                        string result = "success";
                        try
                        {
                            string groupid = Request.QueryString["groupid"].ToString();
                            string title = Request.QueryString["title"].ToString();
                            string LinkedinUserId = Request.QueryString["LinkedinUserId"].ToString();
                            string msg = Request.QueryString["msg"].ToString();
                            LinkedInAccount arrLinkedinAccoount = linAccRepo.getLinkedinAccountDetailsById(LinkedinUserId);
                            oAuthLinkedIn objoAuthLinkedIn = new oAuthLinkedIn();
                            objoAuthLinkedIn.Token = arrLinkedinAccoount.OAuthToken;
                            objoAuthLinkedIn.Verifier = arrLinkedinAccoount.OAuthVerifier;
                            objoAuthLinkedIn.TokenSecret = arrLinkedinAccoount.OAuthSecret;

                            SocialStream sociostream = new SocialStream();
                            string res = sociostream.SetPostUpdate(objoAuthLinkedIn, groupid, msg, title);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.StackTrace);
                        }

                        Response.Write(result);

                    }

                    else if (Request.QueryString["op"].ToString() == "LikePost")
                    {
                        try
                        {
                            string msg = string.Empty;
                            string postid = Request.QueryString["groupid"].ToString();
                            string LinkedinUserId = (Request.QueryString["LinkedinUserId"]);
                            int isLike = Convert.ToInt16(Request.QueryString["isLike"]);

                            if (isLike == 1)
                            {
                                msg = "false";
                            }
                            else
                            { msg = "true"; }


                            LinkedInAccount arrLinkedinAccoount = linAccRepo.getLinkedinAccountDetailsById(LinkedinUserId);
                            oAuthLinkedIn objoAuthLinkedIn = new oAuthLinkedIn();
                            objoAuthLinkedIn.Token = arrLinkedinAccoount.OAuthToken;
                            objoAuthLinkedIn.Verifier = arrLinkedinAccoount.OAuthVerifier;
                            objoAuthLinkedIn.TokenSecret = arrLinkedinAccoount.OAuthSecret;


                            SocialStream sociostream = new SocialStream();
                            string res = sociostream.SetLikeUpdate(objoAuthLinkedIn, postid, msg);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.StackTrace);
                        }

                    }

                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error : " + ex.StackTrace);
                }

                sr = new System.IO.StreamReader(Request.InputStream);
                string line = "";
                line = sr.ReadToEnd();
                jo = Newtonsoft.Json.Linq.JObject.Parse(line);
                gid = Server.UrlDecode((string)jo["gid"]);
                ack = Server.UrlDecode((string)jo["ack"]);
                returndata = fgroupfeeds(ack, gid);
                Response.Write(returndata);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error : " + ex.StackTrace);
            }

        }

        public string GetAccessToken(string fbUserId)
        {
            string accessToken = string.Empty;
            try
            {

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error : " + ex.StackTrace);
            }
            return accessToken;
        }

        public string PostFBGroupFeeds(string ack, string gid, string msg)
        {
            string postFBGroupFeeds = string.Empty;
            try
            {
                FacebookClient fb = new FacebookClient();
                fb.AccessToken = ack;

                Dictionary<string, object> dicFeed = new Dictionary<string, object>();
                dicFeed.Add("message", msg);

                object postId=fb.Post(gid + "/feed", dicFeed);
                postFBGroupFeeds = postId.ToString();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error : " + ex.StackTrace);
            }
            return postFBGroupFeeds;
        }

        public string fgroupfeeds(string ack, string gid)
        {
            string ret = string.Empty;
            try
            {
                FacebookClient fb = new FacebookClient();
                fb.AccessToken = ack;
                dynamic groupsfeeds = fb.Get(gid + "/feed");
                ret = groupsfeeds.ToString();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error : " + ex.StackTrace);
            }
            return ret;
        }


        public List<GlobusLinkedinLib.App.Core.LinkedInGroup.Group_Updates> GetGroupPostDetail(oAuthLinkedIn objoAuthLinkedIn, int count, string groupid)
        {
            List<GlobusLinkedinLib.App.Core.LinkedInGroup.Group_Updates> objGroup = new List<GlobusLinkedinLib.App.Core.LinkedInGroup.Group_Updates>();

            objGroup = objLinkedInGroup.GetGroupPostData(objoAuthLinkedIn, 50, groupid);

            return objGroup;
        }


        public string getfollow(int following)
        {
            string str = string.Empty;

            try
            {
                if (following == 1)
                {
                    str = "unfollow";
                }
                else
                {
                    str = "follow";
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
            }
            return str;
        }

        public string getlike(int like)
        {
            string str = string.Empty;

            try
            {
                if (like == 1)
                {
                    str = "unlike";
                }
                else
                {
                    str = "like";
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
            }
            return str;
        }


        //public string CompareDateWithclient(string clientdate, string scheduletime)
        //{
        //    DateTime client = Convert.ToDateTime(clientdate);
        //    string strTimeNow = String.Format("{0:s}", client).Replace('T', ' ');

        //    DateTime server = DateTime.Now;
        //    DateTime schedule = Convert.ToDateTime(scheduletime);
        //    if (DateTime.Compare(client, server) > 0)
        //    {

        //        //double minutes = (server - client).TotalMinutes;
        //        double minutes = (client - server).TotalMinutes;
        //        schedule = schedule.AddMinutes(minutes);

        //    }
        //    else if (DateTime.Compare(client, server) == 0)
        //    {


        //    }
        //    else if (DateTime.Compare(client, server) < 0)
        //    {
        //        double minutes = (server - client).TotalMinutes;
        //        schedule = schedule.AddMinutes(-minutes);
        //    }
        //    return schedule.ToString();
        //}

        public string CompareDateWithclient(string clientdate, string scheduletime)
        {
            DateTime client = Convert.ToDateTime(clientdate);
            string strTimeNow = String.Format("{0:s}", client).Replace('T', ' ');

            DateTime server = DateTime.Now;
            DateTime schedule = Convert.ToDateTime(scheduletime);
            if (DateTime.Compare(client, server) > 0)
            {

                double minutes = (server - client).TotalMinutes;
                schedule = schedule.AddMinutes(minutes);

            }
            else if (DateTime.Compare(client, server) == 0)
            {


            }
            else if (DateTime.Compare(client, server) < 0)
            {
                double minutes = (server - client).TotalMinutes;
                schedule = schedule.AddMinutes(-minutes);
            }
            return schedule.ToString() ;
        }


    }
}
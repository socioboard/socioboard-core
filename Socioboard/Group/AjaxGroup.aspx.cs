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

namespace SocialScoup.Group
{
    public partial class AjaxGroup : System.Web.UI.Page
    {
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
                              "<div class=\"messagebody\">" + item.title + "< /br>" + summary + "</div>" +
                               "</div>" +//below code is used for comment in group
                                    //+ '<a href="#" class="retweets"></a>'
                                    //+ '<p><span class="comment" onclick="commentText(\'' + val.id + '\');" id="commentfb_' + val.id + '">Comment</span></p>'
                                    "<p style=\"margin-left:60px\">comments(" + item.comments_total + ") likes- " + item.likes_total + "</p><p><span class=\"comment\" onclick=\"FollowPosts('" + groupid + "','" + item.GpPostid + "','" + LinkedinUserId + "','" + item.isFollowing + "')\">" + getfollow(item.isFollowing) + "</span></p>" +
                                    "<p><span class=\"comment\" onclick=\"LikePosts('" + groupid + "','" + item.GpPostid + "','" + LinkedinUserId + "','" + item.isLiked + "')\">" + getlike(item.isLiked) + "</span></p></div>";
                                //+ '<p><span class="ok" id="okfb_' + val.id + '" onclick="commentFBGroup(\'' + val.id + '\',\'' + fuid + '\')">ok</span><span class="cancel" onclick="cancelFB(\'' + val.id + '\');" id="cancelfb_' + val.id + '"> cancel</span></p>'   
                            }

                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.StackTrace);
                        }

                        Response.Write(GroupData);
                        return;

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
       



    }
}
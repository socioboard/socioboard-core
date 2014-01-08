using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SocioBoard.Domain;
using SocioBoard.Helper;
using System.Data;
using SocioBoard.Model;
using System.Collections;
using Facebook;
using GlobusInstagramLib.App.Core;
using GlobusInstagramLib.Authentication;

namespace SocioBoard.Feeds
{
    public partial class AjaxFeed : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                ProcessRequest();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
            }
        }

        void ProcessRequest()
        {
            Domain.User user = (Domain.User)Session["LoggedUser"];
            if (Request.QueryString["op"] != null)
            {
                if (Request.QueryString["op"] == "bindFeeds")
                {
                  
                    clsFeedsAndMessages clsfeedsandmess = new clsFeedsAndMessages();
                    string network = Request.QueryString["network"];
                        DataSet ds = clsfeedsandmess.bindFeedsIntoDataTable(user, network);
                        if (network == "facebook")
                        {
                            Session["FacebookFeedDataTable"] = ds.Tables[0];
                        }
                        else if (network == "twitter")
                        {
                            Session["TwitterFeedDataTable"] = ds.Tables[0];
                        }
                        else if (network == "linkedin")
                        {
                            Session["LinkedInFeedDataTable"] = ds.Tables[0];
                        }
                        else if (network == "instagram")
                        {
                            Session["InstagramFeedDataTable"] = ds.Tables[0];
                        }
                        string message = this.BindData(ds.Tables[0]);
                        Response.Write(message);
                   
                }
                else if (Request.QueryString["op"] == "bindProfiles")
                {
                    int i = 0;
                    string profiles = string.Empty;
                    profiles += "<ul class=\"options_list\">";
                    string network = Request.QueryString["network"];

                    if (!string.IsNullOrEmpty(network))
                    {

                        /*facebook profiles for feeds tab*/
                        if (network == "facebook")
                        {
                            FacebookAccountRepository facerepo = new FacebookAccountRepository();
                            ArrayList alstfacebookprofiles = facerepo.getAllFacebookAccountsOfUser(user.Id);
                            foreach (FacebookAccount item in alstfacebookprofiles)
                            {
                                profiles += "<li><a id=\"greencheck_" + i + "\" ><span class=\"network_icon\">" +
                                            "<img src=\"../Contents/Images/fb_icon.png\" width=\"17\" height=\"16\" alt=\"\" /></span>" +
                                         "<span id=\"profileusername_" + i + "\" class=\"user_name\">" + item.FbUserName + "</span><input type=\"hidden\" id=\"fbhidden_" + i + "\" value=\"" + item.FbUserId + "\" /> <span id=\"checkid_" + i + "\" class=\"checkbx_green\">" +
                                             "<img id=\"checkimg_" + i + "\" src=\"../Contents/Images/msg/network_click.png\" width=\"17\" height=\"17\" alt=\"\" onclick=\"checkprofile(this.id,'feed','facebook');\" /></span>" +
                                     "</a></li> ";
                                i++;
                            }
                        }
                        else if (network == "twitter")
                        {
                            TwitterAccountRepository twtaccountrepo = new TwitterAccountRepository();
                            ArrayList asltwitterprofiles = twtaccountrepo.getAllTwitterAccountsOfUser(user.Id);
                            foreach (TwitterAccount item in asltwitterprofiles)
                            {
                                profiles += "<li><a id=\"greencheck_" + i + "\" ><span class=\"network_icon\">" +
                                     "<img src=\"../Contents/Images/msg/network_twt.png\" width=\"17\" height=\"16\" alt=\"\" /></span>" +
                                  "<span id=\"profileusername_" + i + "\" class=\"user_name\">" + item.TwitterScreenName + "</span> <span id=\"checkid_" + i + "\" class=\"checkbx_green\"><input type=\"hidden\" id=\"twthidden_" + i + "\" value=\"" + item.TwitterUserId + "\">" +
                                      "<img id=\"checkimg_" + i + "\" src=\"../Contents/Images/msg/network_click.png\" width=\"17\" height=\"17\" alt=\"\"  onclick=\"checkprofile(this.id,'feed','twitter');\"/></span>" +
                              "</a></li> ";
                                i++;
                            }
                        }
                        else if (network == "linkedin")
                        {
                            LinkedInAccountRepository liRepo = new LinkedInAccountRepository();
                            ArrayList asllinkedinProfiles = liRepo.getAllLinkedinAccountsOfUser(user.Id);
                            foreach (LinkedInAccount item in asllinkedinProfiles)
                            {
                                profiles += "<li><a id=\"greencheck_" + i + "\" ><span class=\"network_icon\">" +
                                     "<img src=\"../Contents/Images/msg/network_linked.png\" width=\"17\" height=\"16\" alt=\"\" /></span>" +
                                  "<span id=\"profileusername_" + i + "\" class=\"user_name\">" + item.LinkedinUserName + "</span> <span id=\"checkid_" + i + "\" class=\"checkbx_green\"><input type=\"hidden\" id=\"twthidden_" + i + "\" value=\"" + item.LinkedinUserId + "\">" +
                                      "<img id=\"checkimg_" + i + "\" src=\"../Contents/Images/msg/network_click.png\" width=\"17\" height=\"17\" alt=\"\"  onclick=\"checkprofile(this.id,'feed','linkedin');\"/></span>" +
                              "</a></li> ";
                                i++;
                            }
                            profiles += "</ul><input type=\"hidden\" id=\"profilecounter\" value=\"" + i + "\">";
                        }
                        else if (network == "instagram")
                        {
                            InstagramAccountRepository InsRepo = new InstagramAccountRepository();
                            ArrayList aslinstagramProfiles = InsRepo.getAllInstagramAccountsOfUser(user.Id);
                            foreach (InstagramAccount item in aslinstagramProfiles)
                            {
                                profiles += "<li><a id=\"greencheck_" + i + "\" ><span class=\"network_icon\">" +
                                     "<img src=\"../Contents/Images/instagram_24X24.png\" width=\"17\" height=\"16\" alt=\"\" /></span>" +
                                  "<span id=\"profileusername_" + i + "\" class=\"user_name\">" + item.InsUserName + "</span> <span id=\"checkid_" + i + "\" class=\"checkbx_green\"><input type=\"hidden\" id=\"twthidden_" + i + "\" value=\"" + item.InstagramId + "\">" +
                                      "<img id=\"checkimg_" + i + "\" src=\"../Contents/Images/msg/network_click.png\" width=\"17\" height=\"17\" alt=\"\"  onclick=\"checkprofile(this.id,'feed','instagram');\"/></span>" +
                              "</a></li> ";
                                i++;
                            }
                            profiles += "</ul><input type=\"hidden\" id=\"profilecounter\" value=\"" + i + "\">";
                        }
                            Response.Write(profiles);
                    }

                }
                else if (Request.QueryString["op"] == "IntagramProfiles")
                {
                    InstagramAccountRepository InsRepo = new InstagramAccountRepository();
                    ArrayList aslinstagramProfiles = InsRepo.getAllInstagramAccountsOfUser(user.Id);
                    string profiles = string.Empty;
                    string mediaId=Request.QueryString["mediaId"].ToString();
                    foreach (InstagramAccount item in aslinstagramProfiles)
                    {
                        profiles += " <img onclick='postLikeRequest("+ mediaId +","+ item.InstagramId +"," + item.AccessToken + ")' id='" + item.InstagramId + "' src='" + item.ProfileUrl + "'/>";
                    }
                    Response.Write(profiles);
                }
                else if (Request.QueryString["op"] == "postLike")
                {
                    LikesController objlikectr = new LikesController();
                    bool postlike= objlikectr.PostUserLike(Request.QueryString["mediaId"], Request.QueryString["InstagramId"], Request.QueryString["access"]);
                    Response.Write(postlike);
                }
                else if (Request.QueryString["op"] == "updatewallposts")
                {

                    //FacebookAccountRepository fbrepo = new FacebookAccountRepository();
                    //ArrayList alstfbaccounts = fbrepo.getAllFacebookAccountsOfUser(user.Id);
                    //foreach (FacebookAccount item in alstfbaccounts)
                    //{
                    //    FacebookClient fb = new FacebookClient(item.AccessToken);
                    //    FacebookHelper fbhelper = new FacebookHelper();
                    //    var feeds = fb.Get("/me/feed");
                    //    var home = fb.Get("me/home");
                    //    var profile = fb.Get("me");




                    //    long friendscount = 0;
                    //    try
                    //    {
                    //        dynamic friedscount = fb.Get("fql", new { q = "SELECT friend_count FROM user WHERE uid=me()" });
                    //        foreach (var friend in friedscount.data)
                    //        {
                    //            friendscount = friend.friend_count;
                    //        }
                    //    }
                    //    catch (Exception ex)
                    //    {
                    //        Console.WriteLine(ex.StackTrace);
                    //    }
                    //    fbhelper.getFacebookUserHome(home, profile);


                    //    fbhelper.getFacebookUserFeeds(feeds, profile);
                    //    fbhelper.getFacebookUserProfile(profile, item.AccessToken, friendscount, user.Id);
                    //}

                    //clsFeedsAndMessages clsfeedsandmess = new clsFeedsAndMessages();
                    //DataSet ds = clsfeedsandmess.bindFeedsIntoDataTable(user);
                    //string message = this.BindData(ds.Tables[0]);
                    //Response.Write(message);


                }
            }
        }

        public string BindData(DataTable dt)
        {
            string message = string.Empty;
            DataView dv = dt.DefaultView;
            dv.Sort = "MessageDate desc";
            DataTable sortedDT = dv.ToTable();
            int sorteddatacount = 0;
            LikesController objLikes = new LikesController();
            InstagramCommentRepository objCommentRepo = new InstagramCommentRepository();
            Domain.User user = (Domain.User)Session["LoggedUser"];
            foreach (DataRow row in sortedDT.Rows)
            {
              

                    if (row["Network"].ToString() == "twitter")
                    {
                        message += "<div id=\"messagetaskable_" + sorteddatacount + "\" class=\"messages taskable\"><section><aside><section class=\"js-avatar_tip\" data-sstip_class=\"twt_avatar_tip\">" +
                                    "<a class=\"avatar_link view_profile\" href=\"javascript:void(0)\"><img id=\"formprofileurl_" + sorteddatacount + "\" onclick=\"detailsprofile(this.alt);\" width=\"54\" height=\"54\" border=\"0\" class=\"avatar\" src=\"" + row["FromProfileUrl"] + "\" alt=\"" + row["FromName"] + "\">" +
                                     "<article class=\"message-type-icon\"><span class=\"twitter_bm\"><img src=\"../Contents/Images/twticon.png\" width=\"16\" height=\"16\" /></span></article></a></section><ul></ul></aside><article><div class=\"\">" +
                                     "<a class=\"language\" href=\"\"></a></div><div class=\"message_actions\"><a class=\"gear_small\" href=\"#\"><span title=\"Options\" class=\"ficon\">?</span>" +
                                     "</a></div>";

                        message += "<div id=\"messagedescription_" + sorteddatacount + "\" class=\"message-text font-14\">" + row["Message"] + "</div>" +
                                        "<section class=\"bubble-meta\">" +
                                           "<article class=\"threefourth text-overflow\">" +
                                               "<section class=\"floatleft\">" +
                                                   "<a data-tip=\"View Yaroslav Lukashev's Profile\"  class=\"js-avatar_tip view_profile profile_link\" data-sstip_class=\"twt_avatar_tip\"><span id=\"network_" + sorteddatacount + "\" style=\"display:none;\">twitter</span><span style=\"display:none;\" id=\"messageid_" + sorteddatacount + "\">" + row["MessageId"] + "</span><span style=\"display:none;\" id=\"fromid_" + sorteddatacount + "\">" + row["FromId"] + "</span><span style=\"display:none;\" id=\"rowid_" + sorteddatacount + "\">" + row["ProfileId"] + "</span><span id=\"rowname_" + sorteddatacount + "\" onclick=\"detailsprofile(this.id);\">" + row["FromName"] + "</span>" +
                                                   "</a>&nbsp;<a data-msg-time=\"1363926699000\" class=\"time\" target=\"_blank\" title=\"View message on Twitter\" href=\"#\">" + row["MessageDate"].ToString() + "</a>, <span class=\"location\" >&nbsp;</span>" +
                                        "</section></article><ul class=\"message-buttons quarter clearfix\"><li><a href=\"#\"><img src=\"../Contents/Images/replay.png\" alt=\"\" width=\"17\" height=\"24\" border=\"none\"  onclick=replyfunction(" + sorteddatacount + ") ></a></li>" +
                                        "<li><a id=\"createtasktwt_" + sorteddatacount + "\" href=\"#\" onclick=\"createtask(this.id);\"><img src=\"../Contents/Images/pin.png\" alt=\"\" width=\"14\" height=\"17\" border=\"none\"></a></li>" +

                                           "</ul></section></article></section> </div>";
                    }
                    else if (row["Network"].ToString() == "linkedin")
                    {

                       message += "<div id=\"messagetaskable_" + sorteddatacount + "\" class=\"messages taskable\"><section><aside><section class=\"js-avatar_tip\" data-sstip_class=\"twt_avatar_tip\">" +
                                           "<a class=\"avatar_link view_profile\" href=\"javascript:void(0)\"><img id=\"formprofileurl_" + sorteddatacount + "\" width=\"54\" height=\"54\" border=\"0\" class=\"avatar\" src=\"" + row["FromProfileUrl"] + "\" alt=\"\" onclick=\"getFacebookProfiles(this.alt);\" >" +
                                            "<article class=\"message-type-icon\"><span class=\"facebook_bm\"><img src=\"../Contents/Images/linked_25X24.png\"  width=\"16\" height=\"16\"/></span></article></a></section><ul></ul></aside><article><div class=\"\">" +
                                            "<a class=\"language\" href=\"\"></a></div><div class=\"message_actions\"><a class=\"gear_small\" href=\"#\"><span title=\"Options\" class=\"ficon\">?</span>" +
                                            "</a></div><div id=\"messagedescription_" + sorteddatacount + "\" class=\"message-text font-14\">" + row["Message"] + "</div>" +
                                             "<section class=\"bubble-meta\">" +
                                                "<article class=\"threefourth text-overflow\">" +
                                                    "<section class=\"floatleft\">" +
                                                        "<a data-tip=\"View Yaroslav Lukashev's Profile\"  class=\"js-avatar_tip view_profile profile_link\"  data-sstip_class=\"twt_avatar_tip\"   ><span id=\"network_" + sorteddatacount + "\" style=\"display:none;\">facebook</span><span style=\"display:none;\" id=\"rowid_" + sorteddatacount + "\">" + row["ProfileId"] + "</span><span style=\"display:none;\" id=\"rowname_" + sorteddatacount + "\">" + row["FromName"] + "</span><span style=\"display:none;\" id=\"messageid_" + sorteddatacount + "\">" + row["MessageId"] + "</span><span style=\"display:none;\" id=\"fromid_" + sorteddatacount + "\">" + row["FromId"] + "</span><span id=\"" + row["FromId"] + "\"onclick=\"getFacebookProfiles(this.id);\" >" + row["FromName"] + "</span>" +
                                                        "</a>&nbsp;<a id=\"createdtime_" + sorteddatacount + "\" data-msg-time=\"1363926699000\" class=\"time\" target=\"_blank\" title=\"View message on Twitter\" href=\"#\">" + row["MessageDate"].ToString() + "</a><span class=\"location\">&nbsp;</span>" +
                                             "</section></article><ul class=\"message-buttons quarter clearfix\"><li><a href=\"#\"><img src=\"../Contents/Images/replay.png\" alt=\"\" width=\"17\" height=\"24\" border=\"none\" onclick=replyfunction(" + sorteddatacount + ") ></a></li>" +
                                             "<li><a  id=\"createtasktwt_" + sorteddatacount + "\" href=\"#\" onclick=\"createtask(this.id);\"><img src=\"../Contents/Images/pin.png\" alt=\"\" width=\"14\" height=\"17\" border=\"none\"></a></li>" +
                                             "<li><a id=\"savearchive_" + sorteddatacount + "\" href=\"#\" onclick=\"savearchivemsg(" + sorteddatacount + ");\"><img src=\"../Contents/Images/archive.png\" alt=\"\" width=\"14\" height=\"17\" border=\"none\"></a></li>" +
                                               "</ul></section></article></section> </div>";

                    }
                    else if (row["Network"].ToString() == "facebook")
                    {
                        message += "<div id=\"messagetaskable_" + sorteddatacount + "\" class=\"messages taskable\"><section><aside><section class=\"js-avatar_tip\" data-sstip_class=\"twt_avatar_tip\">" +
                                           "<a class=\"avatar_link view_profile\" href=\"javascript:void(0)\"><img id=\"formprofileurl_" + sorteddatacount + "\" width=\"54\" height=\"54\" border=\"0\" class=\"avatar\" src=\"" + row["FromProfileUrl"] + "\" alt=\"" + row["FromId"] + "\" onclick=\"getFacebookProfiles(this.alt);\" >" +
                                            "<article class=\"message-type-icon\"><span class=\"facebook_bm\"><img src=\"../Contents/Images/fb_icon.png\"  width=\"16\" height=\"16\"/></span></article></a></section><ul></ul></aside><article><div class=\"\">" +
                                            "<a class=\"language\" href=\"\"></a></div><div class=\"message_actions\"><a class=\"gear_small\" href=\"#\"><span title=\"Options\" class=\"ficon\">?</span>" +
                                            "</a></div><div id=\"messagedescription_" + sorteddatacount + "\" class=\"message-text font-14\">" + row["Message"] + "</div>" +
                                             "<section class=\"bubble-meta\">" +
                                                "<article class=\"threefourth text-overflow\">" +
                                                    "<section class=\"floatleft\">" +
                                                        "<a data-tip=\"View Yaroslav Lukashev's Profile\"  class=\"js-avatar_tip view_profile profile_link\"  data-sstip_class=\"twt_avatar_tip\"   ><span id=\"network_" + sorteddatacount + "\" style=\"display:none;\">facebook</span><span style=\"display:none;\" id=\"rowid_" + sorteddatacount + "\">" + row["ProfileId"] + "</span><span style=\"display:none;\" id=\"rowname_" + sorteddatacount + "\">" + row["FromName"] + "</span><span style=\"display:none;\" id=\"messageid_" + sorteddatacount + "\">" + row["MessageId"] + "</span><span style=\"display:none;\" id=\"fromid_" + sorteddatacount + "\">" + row["FromId"] + "</span><span id=\"" + row["FromId"] + "\"onclick=\"getFacebookProfiles(this.id);\" >" + row["FromName"] + "</span>" +
                                                        "</a>&nbsp;<a id=\"createdtime_" + sorteddatacount + "\" data-msg-time=\"1363926699000\" class=\"time\" target=\"_blank\" title=\"View message on Twitter\" href=\"#\">" + row["MessageDate"].ToString() + "</a><span class=\"location\">&nbsp;</span>" +
                                             "</section></article><ul class=\"message-buttons quarter clearfix\"><li><a href=\"#\"><img src=\"../Contents/Images/replay.png\" alt=\"\" width=\"17\" height=\"24\" border=\"none\" onclick=replyfunction(" + sorteddatacount + ") ></a></li>" +
                                             "<li><a  id=\"createtasktwt_" + sorteddatacount + "\" href=\"#\" onclick=\"createtask(this.id);\"><img src=\"../Contents/Images/pin.png\" alt=\"\" width=\"14\" height=\"17\" border=\"none\"></a></li>" +
                                             "<li><a id=\"savearchive_" + sorteddatacount + "\" href=\"#\" onclick=\"savearchivemsg(" + sorteddatacount + ");\"><img src=\"../Contents/Images/archive.png\" alt=\"\" width=\"14\" height=\"17\" border=\"none\"></a></li>" +
                                               "</ul></section></article></section> </div>";
                    }
                    else if (row["Network"].ToString() == "instagram")
                    {
                        message += "<div id =\"div_" + sorteddatacount + "\" class=\"instagram_img_textbg\">" +

                         "<div class=\"inst_minage_bg\"><img id=img_" + sorteddatacount + " src=\"" + row["Message"] + "\" alt=\"" + row["MessageId"] + "\" border=\"none\" width=\"240\" height=\"240\" /></div>" +
                                 " <div class=\"inst_comment_bg\">" +

                                   "<div class=\"inputbg\"><input id= textcomment_" + sorteddatacount + " type=\"text\" value=\"\" /></div>" +

                                   "<div class=\"instg_liec_bg\">";
                        bool liked = false;
                        try
                        {
                            //liked = objLikes.LikeToggle(row["FeedId"], row["InstagramId"], item.AccessToken);
                        }
                        catch
                        {
                        }
                        if (!liked)
                        {
                            message += "<div class=\"like_btnbg\"><a id = \""+ row["MessageId"] +"\" class=\"instagram unliked_liked\" href=\"javascript:void(0);\"  onclick =\"showinsprof(this.id)\"></a></div>" +
                                        " <div class=\"comment_btnbg\"><a id=\"comment_" + sorteddatacount + "\" href=\"javascript:void(0);\"  onclick=\"showinsprof(this.id);\"></a></div>" +

                                       "</div></div>";

                        }
                        else
                        {
                            message += "<div class=\"like_btnbg\"><a id = \"" + row["MessageId"] + "\" class=\"instagram liked\" href=\"javascript:void(0);\"  onclick =\"showinsprof(this.id)\"></a></div>" +
                                        " <div class=\"comment_btnbg\"><a id=\"comment_" + sorteddatacount + "\" href=\"javascript:void(0);\"  onclick=\"showinsprof(this.id);\"></a></div>" +

                                       "</div></div>";
                        }
                        List<InstagramComment> lstcomment = objCommentRepo.getAllInstagramCommentsOfUser(user.Id, row["ProfileId"].ToString(), row["MessageId"].ToString());
                        if (lstcomment != null)
                        {
                            try
                            {
                                foreach (InstagramComment insFeed in lstcomment)
                                {
                                    message += "<div class=\"instagram_comment_div\">" +
                                     "<div class=\"user_photo\"><img src=\""+ insFeed.FromProfilePic +"\" width=\"30\" height=\"30\" alt=\"\" /></div>" +
                                     "<div class=\"comment_details\">" +
                                         "<div class=\"user_name\">"+ insFeed.FromName +"</div>" +
                                         " <div class=\"user_name_description\">" + insFeed.Comment + "</div>" +
                                     "</div>" +
                                  "</div>";
                                }
                            }
                            catch (Exception err)
                            { }
                        }
                      //  html += "<div class=\"instagram_comment_div\">" +
                      //   "<div class=\"user_photo\"><img src=\"" + usercmts.data[n - 1].from.profile_picture + "\" width=\"30\" height=\"30\" alt=\"\" /></div>" +
                      //   "<div class=\"comment_details\">" +
                      //       "<div class=\"user_name\">" + usercmts.data[n - 1].from.username + "</div>" +
                      //       " <div class=\"user_name_description\">" + usercmts.data[n - 1].text + "</div>" +
                      //   "</div>" +
                      //"</div>" +


                      //               "<div class=\"instagram_comment_div\">" +
                      //   "<div class=\"user_photo\"><img src=\"" + usercmts.data[n - 2].from.profile_picture + "\" width=\"30\" height=\"30\" alt=\"\" /></div>" +
                      //   "<div class=\"comment_details\">" +
                      //       "<div class=\"user_name\">" + usercmts.data[n - 2].from.username + "</div>" +
                      //       " <div class=\"user_name_description\">" + usercmts.data[n - 2].text + "</div>" +
                      //   "</div>" +
                      //"</div>" +


                      //  "<div class=\"instagram_comment_div\">" +
                      //   "<div class=\"user_photo\"><img src=\"" + usercmts.data[n - 3].from.profile_picture + "\" width=\"30\" height=\"30\" alt=\"\" /></div>" +
                      //   "<div class=\"comment_details\">" +
                      //       "<div class=\"user_name\">" + usercmts.data[n - 3].from.username + "</div>" +
                      //       " <div class=\"user_name_description\">" + usercmts.data[n - 3].text + "</div>" +
                      //   "</div>" +
                      //"</div>" +
                      message+= "</div>";
                    }
                    sorteddatacount++;
                }
            
            return message;


        }
    }
}
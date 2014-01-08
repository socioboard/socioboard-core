using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SocioBoard.Domain;
using System.Collections;
using GlobusInstagramLib.App.Core;
using GlobusInstagramLib.Authentication;
using System.Configuration;
using SocioBoard.Model;
using log4net;

namespace SocialSuitePro.Feeds
{
    public partial class Instagram : System.Web.UI.Page
    {
        ILog logger = LogManager.GetLogger(typeof(Instagram));

        InstagramAccount objInsAcc = new SocioBoard.Domain.InstagramAccount();
        InstagramAccountRepository objInsAccRepo = new InstagramAccountRepository();
        InstagramFeedRepository objInsFeedRepo = new InstagramFeedRepository();
        InstagramFeed objInsFeed = new InstagramFeed();
        InstagramCommentRepository objInsCmtRepo = new InstagramCommentRepository();
        List<SocioBoard.Domain.InstagramComment> lstInsCmt = new List<SocioBoard.Domain.InstagramComment>();
        protected void Page_Load(object sender, EventArgs e)
        {
            string strInsImage = "	<div class=\"content\"><div id=\"instag\" class=\"row-fluid\">";
            try
            {
                 SocioBoard.Domain.User user = (SocioBoard.Domain.User)Session["LoggedUser"];
                 ArrayList arrInsAccount = objInsAccRepo.getAllInstagramAccountsOfUser(user.Id);
               
                 foreach (InstagramAccount item in arrInsAccount)
                 {
                     List<InstagramFeed> lstInsFeed = objInsFeedRepo.getAllInstagramFeedsOfUser(user.Id, item.InstagramId);
                     foreach (InstagramFeed feed in lstInsFeed)
                     {
                         strInsImage += "<div class=\"span3\"><div class=\"row-fluid\"><div class=\"span12 box whitebg feedwrap\"><div class=\"topicon\"><div class=\"pull-left\">" +

                                      "</div><div class=\"pull-right\"><a title=\"\" href=\"#\" onClick=\"likesupdate('" + feed.FeedId + "','" + item.AccessToken + "','" + user.Id + "','like')\"><img width=\"14\" alt=\"\" src=\"../Contents/img/admin/heart-empty.png\"  style=\"margin-top: 9px;\"></a><a title=\"\" href=\"#\"><img width=\"14\" alt=\"\" src=\"../Contents/img/admin/speech-bubble-left.png\"  style=\"margin-top: 9px;\"></a>" +
                                      "</div></div><div class=\"pic\"><img alt=\"\" src=\"" + feed.FeedImageUrl + "\"></div><div class=\"desc\"><p></p><span class=\"pull-left span3\">" +
                                      "<img width=\"12\" alt=\"\" src=\"../Contents/img/admin/heart-empty.png\"> " + feed.LikeCount + "</span><span class=\"pull-left span3\"><img width=\"12\" alt=\"\" src=\"../Contents/img/admin/speech-bubble-left.png\"> 37</span><div class=\"clearfix\"></div><div class=\"userprof\">";

                         lstInsCmt = objInsCmtRepo.getAllInstagramCommentsOfUser(user.Id, item.InstagramId, feed.FeedId);
                         foreach (InstagramComment insCmt in lstInsCmt)
                         {

                             strInsImage += "<div class=\"userprof\"><div class=\"pull-left\"><a href=\"#\">" +
                                             "<img width=\"36\" alt=\"\" src=\"" + insCmt.FromProfilePic + "\"></a></div><div class=\"pull-left descr\"><p>" + insCmt.Comment + "</p>" +
                                              "<span class=\"usert\">"+insCmt.CommentDate+"</span></div></div>";



                             //strInsImage += "<div class=\"pull-left\"><a href=\"#\"><img width=\"36\" src=\"../Contents/img/admin/1_2.png\" alt=\"\"></a></div><div class=\"pull-left descr\"><a class=\"usern\" href=\"#\">" + insCmt.FromName + "</a><span class=\"usert\">about 2 hours ago</span></div></div><div class=\"userconve\"><div class=\"pull-left\"><a href=\"#\"><img width=\"36\" src=\"img/admin/2_2.png\" alt=\"\"></a>" +
                             // "</div><div class=\"pull-left descr\"><p>" + insCmt.Comment + "</p><span class=\"usert\">" + insCmt.CommentDate + "</span></div>";
                             
                         }

                         strInsImage += "</div></div></div></div></div>";
                     }
                 }
                 strInsImage += "</div></div>";
                 contentcontainer.InnerHtml= strInsImage;
            }
            catch (Exception Err)
            {
                logger.Error(Err.Message);
            
            }
        }
    }
}
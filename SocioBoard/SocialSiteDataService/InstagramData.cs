using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SocioBoard.Model;
using GlobusInstagramLib.App.Core;
using GlobusInstagramLib.Authentication;
using SocioBoard.Domain;
using System.Collections;
using System.Configuration;

namespace SocialSiteDataService
{
    public class InstagramData
    {
        public void GetInstagramData(Guid UserId)
        {
            getIntagramImages(UserId);
        }
        public void getIntagramImages(object instaId)
        {
            Guid instaid = (Guid)instaId;

            InstagramAccountRepository objIns = new InstagramAccountRepository();
            InstagramResponse<GlobusInstagramLib.App.Core.User[]> userinf = new InstagramResponse<GlobusInstagramLib.App.Core.User[]>();
            InstagramResponse<GlobusInstagramLib.App.Core.User[]> userinf1 = new InstagramResponse<GlobusInstagramLib.App.Core.User[]>();
            InstagramResponse<InstagramMedia[]> userinf2 = new InstagramResponse<InstagramMedia[]>();
            InstagramResponse<Comment[]> usercmts = new InstagramResponse<Comment[]>();
            MediaController objMedia = new MediaController();
            CommentController objComment = new CommentController();
            LikesController objLikes = new LikesController();
            InstagramFeedRepository objInsFeedRepo = new InstagramFeedRepository();
            InstagramFeed objFeed = new InstagramFeed();
            InstagramComment objinsComment = new InstagramComment();
            InstagramCommentRepository objInsRepo = new InstagramCommentRepository();
            GlobusInstagramLib.Authentication.ConfigurationIns configi = new GlobusInstagramLib.Authentication.ConfigurationIns("https://api.instagram.com/oauth/authorize/", ConfigurationManager.AppSettings["InstagramClientKey"].ToString(), ConfigurationManager.AppSettings["InstagramClientSec"].ToString(), ConfigurationManager.AppSettings["InstagramCallBackURL"].ToString(), "http://api.instagram.com/oauth/access_token", "https://api.instagram.com/v1/", "");
            oAuthInstagram _api = oAuthInstagram.GetInstance(configi);
            ArrayList aslt = objIns.getAllInstagramAccountsOfUser(instaid);
            string html = string.Empty;
            int i = 0;
            // string[] allhtmls = new string[aslt.Count];
            string[] allhtmls = new string[0];
            int countofimages = 0;
            foreach (InstagramAccount item in aslt)
            {

                try
                {
                    GlobusInstagramLib.Instagram.Core.UsersMethods.Users userInstagram = new GlobusInstagramLib.Instagram.Core.UsersMethods.Users();
                    try
                    {
                        userinf2 = userInstagram.UserRecentMedia(item.InstagramId, string.Empty, string.Empty, "20", string.Empty, string.Empty, item.AccessToken);

                    }
                    catch { }


                }
                catch { }



                if (userinf2 != null)
                {
                    for (int j = 0; j < userinf2.data.Count(); j++)
                    {
                        try
                        {
                            usercmts = objComment.GetComment(userinf2.data[j].id, item.AccessToken);
                            bool liked = false;
                            try
                            {
                                liked = objLikes.LikeToggle(userinf2.data[j].id, item.InstagramId, item.AccessToken);
                            }
                            catch
                            {
                            }
                            int n = usercmts.data.Count();
                            for (int cmt = 0; cmt < usercmts.data.Count(); cmt++)
                            {
                                objinsComment.Comment = usercmts.data[cmt].text;
                                objinsComment.CommentDate = usercmts.data[cmt].created_time.ToString();
                                objinsComment.CommentId = usercmts.data[cmt].id;
                                objinsComment.EntryDate = DateTime.Now.ToString();
                                objinsComment.FeedId = userinf2.data[j].id;
                                objinsComment.Id = Guid.NewGuid();
                                objinsComment.InstagramId = item.InstagramId;
                                objinsComment.UserId = item.UserId;
                                objinsComment.FromName = usercmts.data[cmt].from.full_name;
                                objinsComment.FromProfilePic = usercmts.data[cmt].from.profile_picture;
                                if (!objInsRepo.checkInstagramCommentExists(usercmts.data[cmt].id, item.UserId))
                                    objInsRepo.addInstagramComment(objinsComment);
                            }
                            objFeed.EntryDate = DateTime.Now;
                            objFeed.FeedDate = userinf2.data[j].created_time.ToString();
                            objFeed.FeedId = userinf2.data[j].id;
                            objFeed.FeedImageUrl = userinf2.data[j].images.low_resolution.url.ToString();
                            objFeed.InstagramId = item.InstagramId;
                            objFeed.LikeCount = userinf2.data[j].likes.count;
                            objFeed.UserId = item.UserId;
                            if (!objInsFeedRepo.checkInstagramFeedExists(userinf2.data[j].id, item.UserId))
                                objInsFeedRepo.addInstagramFeed(objFeed);


                        }
                        catch
                        {
                        }
                        i++;

                    }
                }
                i++;
           //     allhtmls[countofimages] = html;
                html = string.Empty;
                countofimages++;
                break;
            }
            string totalhtml = string.Empty;
            try
            {
                for (int k = 0; k < countofimages; k++)
                {
                    totalhtml = totalhtml + allhtmls[k];
                }
            }
            catch
            {
            }
          
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using GlobusInstagramLib.Authentication;
using System.Configuration;
using SocioBoard.Domain;
using GlobusInstagramLib.App.Core;
using SocioBoard.Model;
using System.Collections;
using log4net;

namespace SocialSuitePro
{
    public partial class InstagramManager : System.Web.UI.Page
    {
        oAuthInstagram objInsta = new oAuthInstagram();
        InstagramAccountRepository objInsRepo = new InstagramAccountRepository();
        oAuthInstagram _api;
        ILog logger = LogManager.GetLogger(typeof(InstagramManager));

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    try
                    {
                        Session["instagramtotalprofiles"] = null;
                    }
                    catch (Exception ex)
                    {
                        logger.Error(ex.StackTrace);
                        Console.WriteLine(ex.StackTrace);
                    }
                    getAccessToken();

                    Response.Redirect("Home.aspx");
                }
                catch (Exception ex)
                {
                    logger.Error(ex.StackTrace);
                    Console.WriteLine(ex.StackTrace);
                }
            }
        }
        public void getAccessToken()
        {
            GlobusInstagramLib.Authentication.ConfigurationIns configi = new GlobusInstagramLib.Authentication.ConfigurationIns("https://api.instagram.com/oauth/authorize/", ConfigurationManager.AppSettings["InstagramClientKey"].ToString(), ConfigurationManager.AppSettings["InstagramClientSec"].ToString(), ConfigurationManager.AppSettings["InstagramCallBackURL"].ToString(), "http://api.instagram.com/oauth/access_token", "https://api.instagram.com/v1/", "");
            SocialProfilesRepository socioprofilerepo = new SocialProfilesRepository();
            SocialProfile socioprofile = new SocialProfile();

            _api = oAuthInstagram.GetInstance(configi);
            AccessToken access = new AccessToken();
            string code = Request.QueryString["code"].ToString();
            SocioBoard.Domain.User user = (SocioBoard.Domain.User)Session["LoggedUser"];
            access = _api.AuthGetAccessToken(code);

            UserController objusercontroller = new UserController();
            InstagramResponse<GlobusInstagramLib.App.Core.User> objuser = objusercontroller.GetUserDetails(access.user.id, access.access_token);

            InstagramAccount objInsAccount = new InstagramAccount();
            objInsAccount.AccessToken = access.access_token;
            //objInsAccount.FollowedBy=access.user.
            objInsAccount.InstagramId = access.user.id;
            objInsAccount.ProfileUrl = access.user.profile_picture;
            objInsAccount.InsUserName = access.user.username;
            objInsAccount.TotalImages = objuser.data.counts.media;
            objInsAccount.FollowedBy = objuser.data.counts.followed_by;
            objInsAccount.Followers = objuser.data.counts.follows;
            objInsAccount.UserId = user.Id;

            socioprofile.UserId = user.Id;
            socioprofile.ProfileType = "instagram";
            socioprofile.ProfileId = access.user.id;
            socioprofile.ProfileStatus = 1;
            socioprofile.ProfileDate = DateTime.Now;
            socioprofile.Id = Guid.NewGuid();

            if (objInsRepo.checkInstagramUserExists(access.user.id, user.Id))
            {
                objInsRepo.updateInstagramUser(objInsAccount);
                if (!socioprofilerepo.checkUserProfileExist(socioprofile))
                {
                    socioprofilerepo.addNewProfileForUser(socioprofile);
                }
            }
            else
            {
                objInsRepo.addInstagramUser(objInsAccount);
                if (!socioprofilerepo.checkUserProfileExist(socioprofile))
                {
                    socioprofilerepo.addNewProfileForUser(socioprofile);
                }
            }
       string messages =     getIntagramImages(objInsAccount);

          
            Response.Write(messages);
        }

        public string getIntagramImages( InstagramAccount objInsAccount)
        {
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
          //  ArrayList aslt = objIns.getAllInstagramAccountsOfUser(instaid);
            string html = string.Empty;
            int i = 0;
            // string[] allhtmls = new string[aslt.Count];
            string[] allhtmls = new string[0];
            int countofimages = 0;
            GlobusInstagramLib.Instagram.Core.UsersMethods.Users userInstagram = new GlobusInstagramLib.Instagram.Core.UsersMethods.Users();
                    try
                    {
                        userinf2 = userInstagram.UserRecentMedia(objInsAccount.InstagramId,string.Empty,string.Empty,"20",string.Empty,string.Empty,objInsAccount.AccessToken);

                    }
                    catch(Exception ex) {
                        logger.Error(ex.StackTrace);
                    }


                    if (userinf2 != null)
                    {
                        for (int j = 0; j < userinf2.data.Count(); j++)
                        {
                            try
                            {
                                usercmts = objComment.GetComment(userinf2.data[j].id,objInsAccount.AccessToken);
                                bool liked = false;
                                try
                                {
                                    liked = objLikes.LikeToggle(userinf2.data[j].id, objInsAccount.InstagramId, objInsAccount.AccessToken);
                                }
                                catch(Exception ex)
                                {
                                    logger.Error(ex.StackTrace);
                                }
                                int n = usercmts.data.Count();
                                for (int cmt = usercmts.data.Count() - 1; cmt > usercmts.data.Count() - 3; cmt--)
                                {
                                    try
                                    {
                                        objinsComment.Comment = usercmts.data[cmt].text;
                                        objinsComment.CommentDate = usercmts.data[cmt].created_time.ToString();
                                        objinsComment.CommentId = usercmts.data[cmt].id;
                                        objinsComment.EntryDate = DateTime.Now.ToString();
                                        objinsComment.FeedId = userinf2.data[j].id;
                                        objinsComment.Id = Guid.NewGuid();
                                        objinsComment.InstagramId = objInsAccount.InstagramId;
                                        objinsComment.UserId = objInsAccount.UserId;
                                        objinsComment.FromName = usercmts.data[cmt].from.full_name;
                                        objinsComment.FromProfilePic = usercmts.data[cmt].from.profile_picture;
                                        if (!objInsRepo.checkInstagramCommentExists(usercmts.data[cmt].id, objInsAccount.UserId))
                                            objInsRepo.addInstagramComment(objinsComment);
                                    }
                                    catch (Exception ex)
                                    {
                                        logger.Error(ex.StackTrace);
                                        Console.WriteLine(ex.StackTrace);
                                    }
                                }
                                objFeed.EntryDate = DateTime.Now;
                                objFeed.FeedDate = userinf2.data[j].created_time.ToString();
                                objFeed.FeedId = userinf2.data[j].id;
                                objFeed.FeedImageUrl = userinf2.data[j].images.low_resolution.url.ToString();
                                objFeed.InstagramId = objInsAccount.InstagramId;
                                objFeed.LikeCount = userinf2.data[j].likes.count;
                                objFeed.UserId = objInsAccount.UserId;
                                if (!objInsFeedRepo.checkInstagramFeedExists(userinf2.data[j].id, objInsAccount.UserId))
                                    objInsFeedRepo.addInstagramFeed(objFeed);


                            }
                            catch(Exception ex)
                            {
                                logger.Error(ex.StackTrace);
                            }
                            i++;

                        }
                    }
                    i++;
                
            
            string totalhtml = string.Empty;
            try
            {
                for (int k = 0; k < countofimages; k++)
                {
                    totalhtml = totalhtml + allhtmls[k];
                }
            }
            catch(Exception ex)
            {
                logger.Error(ex.StackTrace);

            }
            Session["AllHtmls"] = allhtmls;
            return totalhtml;

          
        }
    }
}
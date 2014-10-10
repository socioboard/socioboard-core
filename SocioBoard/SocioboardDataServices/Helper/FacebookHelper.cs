using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Domain.Socioboard.Domain;
//using SocioBoard.Model;
//using Facebook;
using System.Net;
using System.IO;
using System.Text;

namespace SocioboardDataServices.Helper
{
    public class FacebookHelper
    {
        public void getFacebookUserProfile(dynamic data, string accesstoken, long friends, Guid user)
        {
            SocialProfile socioprofile = new SocialProfile();
            //SocialProfilesRepository socioprofilerepo = new SocialProfilesRepository();
            Api.SocialProfile.SocialProfile ApiObjSocialProfile = new Api.SocialProfile.SocialProfile();
            FacebookAccount fbAccount = new FacebookAccount();
            //FacebookAccountRepository fbrepo = new FacebookAccountRepository();
            Api.FacebookFeed.FacebookFeed ApiObjFacebookFeed = new Api.FacebookFeed.FacebookFeed();
            try
            {
                try
                {
                    fbAccount.AccessToken = accesstoken;
                }
                catch
                {
                }
                try
                {
                    fbAccount.EmailId = data["email"].ToString();
                }
                catch
                {
                }
                try
                {
                    fbAccount.FbUserId = data["id"].ToString();
                }
                catch
                {
                }

                try
                {
                    fbAccount.ProfileUrl = data["link"].ToString();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.StackTrace);
                }


                try
                {
                    fbAccount.FbUserName = data["name"].ToString();
                }
                catch
                {
                }
                try
                {
                    fbAccount.Friends = Convert.ToInt32(friends);
                }
                catch
                {
                }
                try
                {
                    fbAccount.Id = Guid.NewGuid();
                }
                catch
                {
                }
                fbAccount.IsActive = 1;
                try
                {
                    if (HttpContext.Current.Session["fbSocial"] != null)
                    {
                        if (HttpContext.Current.Session["fbSocial"] == "p")
                        {
                            //FacebookClient fbClient = new FacebookClient(accesstoken);
                            //int fancountPage = 0;
                            //dynamic fancount = fbClient.Get("fql", new { q = " SELECT fan_count FROM page WHERE page_id =" + fbAccount.FbUserId });
                            //foreach (var friend in fancount.data)
                            //{
                            //    fancountPage = friend.fan_count;

                            //}
                            //fbAccount.Friends = Convert.ToInt32(fancountPage);
                            fbAccount.Type = "page";
                        }
                        else
                        {
                            fbAccount.Type = "account";
                        }
                        fbAccount.UserId = user;
                    }

                    if (HttpContext.Current.Session["UserAndGroupsForFacebook"] != null)
                    {
                        try
                        {
                            fbAccount.UserId = user;
                            fbAccount.Type = "account";
                        }
                        catch (Exception ex)
                        {

                        }
                    }
                }
                catch
                {
                }

                #region unused
                //if (HttpContext.Current.Session["login"] != null)
                //{
                //    if (HttpContext.Current.Session["login"].ToString().Equals("facebook"))
                //    {
                //        User usr = new User();
                //        UserRepository userrepo = new UserRepository();
                //        Registration regObject = new Registration();
                //        usr.AccountType = "free";
                //        usr.CreateDate = DateTime.Now;
                //        usr.ExpiryDate = DateTime.Now.AddMonths(1);
                //        usr.Id = Guid.NewGuid();
                //        usr.UserName = data["name"].ToString();
                //        usr.Password = regObject.MD5Hash(data["name"].ToString());
                //        usr.EmailId = data["email"].ToString();
                //        usr.UserStatus = 1;
                //        if (!userrepo.IsUserExist(data["email"].ToString()))
                //        {
                //            UserRepository.Add(usr);
                //        }
                //    }
                //} 
                #endregion
                try
                {
                    socioprofile.UserId = user;
                }
                catch
                {
                }
                try
                {
                    socioprofile.ProfileType = "facebook";
                }
                catch
                {
                }
                try
                {
                    socioprofile.ProfileId = data["id"].ToString();
                }
                catch
                {
                }
                try
                {
                    socioprofile.ProfileStatus = 1;
                }
                catch
                {
                }
                try
                {
                    socioprofile.ProfileDate = DateTime.Now;
                }
                catch
                {
                }
                try
                {
                    socioprofile.Id = Guid.NewGuid();
                }
                catch
                {
                }
                if (HttpContext.Current.Session["fbSocial"] != null)
                {
                    if (HttpContext.Current.Session["fbSocial"] == "p")
                    {

                        HttpContext.Current.Session["fbpagedetail"] = fbAccount;
                    }
                    else
                    {
                        if (!fbrepo.checkFacebookUserExists(fbAccount.FbUserId, user))
                        {
                            fbrepo.addFacebookUser(fbAccount);
                            if (!socioprofilerepo.checkUserProfileExist(socioprofile))
                            {
                               
                                socioprofilerepo.addNewProfileForUser(socioprofile);

                                GroupRepository objGroupRepository = new GroupRepository();
                                SocioBoard.Domain.Team team = (SocioBoard.Domain.Team)HttpContext.Current.Session["GroupName"];
                                Groups lstDetails = objGroupRepository.getGroupName(team.GroupId);
                                if (lstDetails.GroupName == "Socioboard")
                                {                                   
                                    TeamMemberProfileRepository objTeamMemberProfileRepository = new TeamMemberProfileRepository();                                  
                                    TeamMemberProfile teammemberprofile = new TeamMemberProfile();
                                    teammemberprofile.Id = Guid.NewGuid();
                                    teammemberprofile.TeamId = team.Id;
                                    teammemberprofile.ProfileId = fbAccount.FbUserId;
                                    teammemberprofile.ProfileType = "facebook";
                                    teammemberprofile.StatusUpdateDate = DateTime.Now;

                                    objTeamMemberProfileRepository.addNewTeamMember(teammemberprofile);

                                }




                            }
                            else
                            {
                                socioprofilerepo.updateSocialProfile(socioprofile);
                            }

                           

                        }
                        else
                        {
                            HttpContext.Current.Session["alreadyexist"] = fbAccount;
                            fbrepo.updateFacebookUser(fbAccount);
                            if (!socioprofilerepo.checkUserProfileExist(socioprofile))
                            {
                                socioprofilerepo.addNewProfileForUser(socioprofile);
                            }
                            else
                            {
                                socioprofilerepo.updateSocialProfile(socioprofile);
                            }
                        }

                    }
                }

                if (HttpContext.Current.Session["UserAndGroupsForFacebook"] != null)
                {
                    if (HttpContext.Current.Session["UserAndGroupsForFacebook"].ToString() == "facebook")
                    {
                        try
                        {
                            if (!fbrepo.checkFacebookUserExists(fbAccount.FbUserId, user))
                            {
                                fbrepo.addFacebookUser(fbAccount);
                                if (!socioprofilerepo.checkUserProfileExist(socioprofile))
                                {
                                    socioprofilerepo.addNewProfileForUser(socioprofile);
                                }
                                else
                                {
                                    socioprofilerepo.updateSocialProfile(socioprofile);
                                }
                            }
                            else
                            {
                                fbrepo.updateFacebookUser(fbAccount);
                                if (!socioprofilerepo.checkUserProfileExist(socioprofile))
                                {
                                    socioprofilerepo.addNewProfileForUser(socioprofile);
                                }
                                else
                                {
                                    socioprofilerepo.updateSocialProfile(socioprofile);
                                }
                            
                            }
                            if (HttpContext.Current.Session["GroupName"] != null)
                            {
                                GroupProfile groupprofile = new GroupProfile();
                                GroupProfileRepository groupprofilerepo = new GroupProfileRepository();
                                Groups group = (Groups)HttpContext.Current.Session["GroupName"];
                                groupprofile.Id = Guid.NewGuid();
                                groupprofile.GroupOwnerId = user;
                                groupprofile.ProfileId = socioprofile.ProfileId;
                                groupprofile.ProfileType = "facebook";
                                groupprofile.GroupId = group.Id;
                                groupprofile.EntryDate = DateTime.Now;
                                if (!groupprofilerepo.checkGroupProfileExists(user, group.Id, groupprofile.ProfileId))
                                {
                                    groupprofilerepo.AddGroupProfile(groupprofile);
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.StackTrace);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
            }
        }

        // Not using right now, it is just to count friends from returned list
        public int getFacebookFriendsCount(dynamic data)
        {
            int totalfriends = 0;
            try
            {

                foreach (var item in data["data"])
                {
                    totalfriends++;
                }
                return totalfriends;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return totalfriends;
            }
        }

        // Saving FacebookFeeds of user into database i.e. FacebookFeed Table
        public void getFacebookUserFeeds(dynamic data, dynamic profile)
        {
            FacebookFeed fbfeed = new FacebookFeed();
            User user = (User)HttpContext.Current.Session["LoggedUser"];
            FacebookFeedRepository fbfeedrepo = new FacebookFeedRepository();

            if (data != null)
            {
                foreach (var result in data["data"])
                {

                    fbfeed.Type = "fb_feed";

                    try
                    {
                        fbfeed.UserId = user.Id;
                    }
                    catch (Exception ex)
                    { Console.WriteLine(ex.StackTrace); }

                    try
                    {
                        fbfeed.ProfileId = profile["id"].ToString();
                    }
                    catch (Exception ex)
                    { Console.WriteLine(ex.StackTrace); }

                    try
                    {
                        fbfeed.Id = Guid.NewGuid();
                    }
                    catch (Exception ex)
                    { Console.WriteLine(ex.StackTrace); }



                    fbfeed.FromProfileUrl = "http://graph.facebook.com/" + result["from"]["id"] + "/picture?type=small";
                    fbfeed.FromName = result["from"]["name"].ToString();
                    fbfeed.FromId = result["from"]["id"].ToString();
                    fbfeed.FeedId = result["id"].ToString();
                    fbfeed.FeedDate = DateTime.Parse(result["created_time"].ToString());
                    fbfeed.FbComment = "http://graph.facebook.com/" + result["id"] + "/comments";
                    fbfeed.FbLike = "http://graph.facebook.com/" + result["id"] + "/likes";
                    string message = string.Empty;
                    int lstfbcount = 0;

                    if (lstfbcount < 25)
                    {
                        try
                        {
                            if (result["message"] != null)
                            {
                                message = result["message"];
                                lstfbcount++;

                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.StackTrace);
                            try
                            {
                                if (result["description"] != null)
                                {
                                    message = result["description"];
                                    lstfbcount++;

                                }
                            }
                            catch (Exception exx)
                            {
                                try
                                {
                                    Console.WriteLine(exx.StackTrace);
                                    if (result["story"] != null)
                                    {
                                        message = result["story"];
                                        lstfbcount++;
                                    }
                                }
                                catch (Exception exxx)
                                {
                                    Console.WriteLine(exxx.StackTrace);
                                    message = string.Empty;
                                }
                            }

                        }
                    }
                    fbfeed.FeedDescription = message;
                    fbfeed.EntryDate = DateTime.Now;

                    if (!fbfeedrepo.checkFacebookFeedExists(fbfeed.FeedId))
                    {
                        fbfeedrepo.addFacebookFeed(fbfeed);
                    }
                }
            }
        }


        //Saving FacebookWall of user into database i.e. FacebookMessage table
        public void getFacebookUserHome(dynamic data, dynamic profile)
        {
            FacebookMessage fbmsg = new FacebookMessage();
            FacebookMessageRepository fbmsgrepo = new FacebookMessageRepository();

            string profileid = string.Empty;
            User user = (User)HttpContext.Current.Session["LoggedUser"];

            if (data != null)
            {
                int lstfbcount = 0;
                foreach (dynamic result in data["data"])
                {
                    string message = string.Empty;
                    string imgprof = "http://graph.facebook.com/" + result["from"]["id"] + "/picture?type=small";
                    fbmsg.EntryDate = DateTime.Now;
                    fbmsg.MessageId = result["id"].ToString();
                    fbmsg.FromId = result["from"]["id"].ToString();
                    fbmsg.FromName = result["from"]["name"].ToString();
                    fbmsg.FromProfileUrl = imgprof;
                    fbmsg.Id = Guid.NewGuid();
                    fbmsg.MessageDate = DateTime.Parse(result["created_time"].ToString());
                    fbmsg.UserId = user.Id;

                    try
                    {
                        fbmsg.Picture = result["picture"].ToString();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                        fbmsg.Picture = null;
                    }

                    try
                    {
                        if (result["message_tags"][0] != null)
                        {




                            if (result["to"] != null)
                            {
                                foreach (var item in result["to"]["data"])
                                {
                                    if (result["from"] != null)
                                    {

                                        if (item["id"] != profile["id"])
                                        {
                                            if (result["from"]["id"] == profile["id"])
                                            {
                                                fbmsg.Type = "fb_tag";
                                            }
                                            else
                                            {
                                                fbmsg.Type = "fb_home";
                                            }
                                        }
                                        else
                                        {
                                            fbmsg.Type = "fb_home";
                                        }
                                    }
                                }

                            }
                            else
                            {
                                fbmsg.Type = "fb_home";

                            }
                        }
                        else
                        {
                            fbmsg.Type = "fb_home";
                        }
                    }
                    catch (Exception ex)
                    {
                        fbmsg.Type = "fb_home";
                        Console.WriteLine(ex.StackTrace);
                    }

                    fbmsg.ProfileId = profile["id"].ToString();
                    fbmsg.FbComment = "http://graph.facebook.com/" + result["id"] + "/comments";
                    fbmsg.FbLike = "http://graph.facebook.com/" + result["id"] + "/likes";


                    if (lstfbcount < 25)
                    {
                        try
                        {
                            if (result["message"] != null)
                            {
                                message = result["message"];
                                lstfbcount++;

                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.StackTrace);
                            try
                            {
                                if (result["description"] != null)
                                {
                                    message = result["description"];
                                    lstfbcount++;

                                }
                            }
                            catch (Exception exx)
                            {
                                try
                                {
                                    Console.WriteLine(exx.StackTrace);
                                    if (result["story"] != null)
                                    {
                                        message = result["story"];
                                        lstfbcount++;
                                    }
                                }
                                catch (Exception exxx)
                                {
                                    Console.WriteLine(exxx.StackTrace);
                                    message = string.Empty;
                                }
                            }

                        }

                    }
                    fbmsg.Message = message;

                    if (!fbmsgrepo.checkFacebookMessageExists(fbmsg.MessageId))
                    {
                        fbmsgrepo.addFacebookMessage(fbmsg);
                    }


                }
            }
        }


        public List<FacebookMessage> getFacebookMessagesOfUser(Guid userid, string profileid)
        {
            FacebookMessageRepository fbmsgrepo = new FacebookMessageRepository();
            List<FacebookMessage> lstfbmsg = fbmsgrepo.getAllFacebookMessagesOfUser(userid, profileid);
            return lstfbmsg;
        }

        public List<FacebookFeed> getFacebookFeedsOfUser(Guid userid, string profileid)
        {
            FacebookFeedRepository facefeedrepo = new FacebookFeedRepository();
            List<FacebookFeed> lstfacefeed = facefeedrepo.getAllFacebookFeedsOfUser(userid, profileid);
            return lstfacefeed;
        }





        public int getfanCount(ref FacebookStats objfbsts)
        {
            int friendscnt = 0;
            long friendscount = 0;
            try
            {

                FacebookClient fb = new FacebookClient();

                string accessToken = HttpContext.Current.Session["accesstoken"].ToString();

                fb.AccessToken = accessToken;


                var client = new FacebookClient();

                dynamic me = fb.Get("me");


                dynamic friedscount = fb.Get("fql", new { q = "SELECT friend_count FROM user WHERE uid=me()" });

                foreach (var friend in friedscount.data)
                {
                    friendscount = friend.friend_count;
                }

                friendscnt = Convert.ToInt32(friendscount);


            }
            catch (Exception ex)
            {
                //logger.Error(ex.StackTrace);
                //Console.WriteLine(ex.StackTrace);

            }
            return friendscnt;
        }


        public void getfbFriendsGenderStats(dynamic data, dynamic profile, Guid userId)
        {
            FacebookStats objfbStats = new FacebookStats();
            FacebookStatsRepository objFBStatsRepo = new FacebookStatsRepository();
            int malecount = 0;
            int femalecount = 0;
            foreach (var item in data["data"])
            {
                if (item["gender"] == "male")
                    malecount++;
                else if (item["gender"] == "female")
                    femalecount++;
            }
            objfbStats.EntryDate = DateTime.Now;
            objfbStats.FbUserId = profile["id"].ToString();
            objfbStats.FemaleCount = femalecount;
            objfbStats.Id = Guid.NewGuid();
            objfbStats.MaleCount = malecount;
            objfbStats.UserId = userId;
            objfbStats.FanCount = getfanCount(ref objfbStats);
            objFBStatsRepo.addFacebookStats(objfbStats);

        }


        public void getfbFriendsGenderStatsForFanPage(dynamic profile, Guid userId, ref FacebookAccount objfbacnt)
        {
            FacebookStats objfbStats = new FacebookStats();
            FacebookStatsRepository objFBStatsRepo = new FacebookStatsRepository();
            //int malecount = 0;
            //int femalecount = 0;
            //foreach (var item in data["data"])
            //{
            //    if (item["gender"] == "male")
            //        malecount++;
            //    else if (item["gender"] == "female")
            //        femalecount++;
            //}
            objfbStats.EntryDate = DateTime.Now;
            objfbStats.FbUserId = profile["id"].ToString();
            //objfbStats.FemaleCount = femalecount;
            objfbStats.Id = Guid.NewGuid();
            //objfbStats.MaleCount = malecount;
            objfbStats.UserId = userId;
            objfbStats.FanCount = objfbacnt.Friends;
            //objfbStats.ShareCount = getShareCount();
            //objfbStats.CommentCount = getCommentCount();
            //objfbStats.LikeCount = getLikeCount();
            objFBStatsRepo.addFacebookStats(objfbStats);
            FacebookInsightStatsHelper objfbinshlpr = new FacebookInsightStatsHelper();
            string pId = profile["id"].ToString();
            //string pId = "329139457226886";
            objfbinshlpr.getPageImpresion(pId, userId, 7);

        }









        public void getInboxMessages(dynamic data, dynamic profile, Guid userId)
        {
            FacebookMessage fbmsg = new FacebookMessage();
            FacebookMessageRepository fbmsgrepo = new FacebookMessageRepository();

            string profileid = string.Empty;
            User user = (User)HttpContext.Current.Session["LoggedUser"];

            if (data != null)
            {
                int lstfbcount = 0;
                foreach (dynamic result in data["data"])
                {


                    try
                    {
                        foreach (dynamic message in result["comments"]["data"])
                        {
                            //Do want you want with the messages
                            fbmsg.MessageId = message["id"];
                            fbmsg.FromName = message["from"]["name"];
                            fbmsg.FromId = message["from"]["id"];
                            fbmsg.Message = message["message"];
                            fbmsg.MessageDate = DateTime.Parse(message["created_time"].ToString()); ;
                            fbmsg.FromProfileUrl = "http://graph.facebook.com/" + message["from"]["id"] + "/picture?type=small";
                            fbmsg.EntryDate = DateTime.Now;
                            fbmsg.Id = Guid.NewGuid();
                            fbmsg.ProfileId = profile["id"].ToString();
                            fbmsg.Type = "inbox_message";
                            fbmsg.UserId = userId;
                            if (!fbmsgrepo.checkFacebookMessageExists(fbmsg.MessageId))
                            {
                                fbmsgrepo.addFacebookMessage(fbmsg);
                            }

                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                    }

                }
            }
        }

        
        //public List<FacebookGroup> GetFacebookUserGroup(dynamic group, dynamic profile,string token)
        //{
        //    List<FacebookGroup> temp = new List<FacebookGroup>();
        //    List<FacebookGroup> lstfacebookgroup1 = new List<FacebookGroup>();
        //    FacebookGroup objfacegroup = new FacebookGroup();
        //    User user = (User)HttpContext.Current.Session["LoggedUser"];
        //    FacebookFeedRepository fbfeedrepo = new FacebookFeedRepository();

        //    if (group != null)
        //    {
        //        foreach (var result in group["data"])
        //        {
        //            objfacegroup.GroupiId = result["id"];
        //            temp.Add(objfacegroup);
        //        }
        //        foreach (var result in temp)
        //        {
        //            result.GroupiId
        //        }

        //    }
        //}

        //Saving FacebookGroups of user into database i.e. FacebookGroups table
        public void GetFacebookGroups(dynamic group, dynamic profile)
        {
            FacebookGroup fbgroup = new FacebookGroup();
            User user = (User)HttpContext.Current.Session["LoggedUser"];
            FacebookGroupRepository fbgrouprepo = new FacebookGroupRepository();

            if (group != null)
            {
                
                    try
                    {
                        fbgroup.Id = Guid.NewGuid();
                    }
                    catch (Exception ex)
                    { Console.WriteLine(ex.StackTrace); }

                    try
                    {
                        fbgroup.UserId = user.Id;
                    }
                    catch (Exception ex)
                    { Console.WriteLine(ex.StackTrace); }

                    try
                    {
                        fbgroup.ProfileId = profile["id"].ToString();
                    }
                    catch (Exception ex)
                    { Console.WriteLine(ex.StackTrace); }

                    fbgroup.GroupId = group["id"];
                    fbgroup.Icon = group["icon"];
                    fbgroup.Cover = "";
                    fbgroup.Owner = group["owner"]["name"];
                    fbgroup.Name = group["name"];
                    fbgroup.Description = group["description"];
                    fbgroup.Link = group["link"];
                    fbgroup.Privacy = group["privacy"];
                    fbgroup.UpdatedTime =Convert.ToDateTime( group["updated_time"]);
                    fbgroup.EntryDate = DateTime.Now;



                    if (!fbgrouprepo.checkFacebookGroupExists(fbgroup.GroupId.ToString(),fbgroup.ProfileId.ToString()))
                    {
                        fbgrouprepo.AddFacebookGroup(fbgroup);
                    }
                
            }
        }

        public static bool CheckFacebookToken(string fbtoken, string txtvalue)
        {
            bool checkFacebookToken = false;
            try
            {
                string facebookSearchUrl = "https://graph.facebook.com/search?q=" + txtvalue + " &type=post&access_token=" + fbtoken;
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
                checkFacebookToken = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return checkFacebookToken;
        }
    }
}
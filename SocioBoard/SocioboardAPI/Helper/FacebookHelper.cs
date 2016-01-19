using Api.Socioboard.Model;
using Facebook;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Web;

namespace Api.Socioboard.Helper
{
    public static class FacebookHelper
    {

        public static string getFacebookRecentPost(string fbAccesstoken, string pageId)
        {
            string output = string.Empty;
            string facebookSearchUrl = "https://graph.facebook.com/v1.0/" + pageId + "/posts?limit=30&access_token=" + fbAccesstoken;
            var facebooklistpagerequest = (HttpWebRequest)WebRequest.Create(facebookSearchUrl);
            facebooklistpagerequest.Method = "GET";
            facebooklistpagerequest.Credentials = CredentialCache.DefaultCredentials;
            facebooklistpagerequest.AllowWriteStreamBuffering = true;
            facebooklistpagerequest.ServicePoint.Expect100Continue = false;
            facebooklistpagerequest.PreAuthenticate = false;
            try
            {
                using (var response = facebooklistpagerequest.GetResponse())
                {
                    using (var stream = new StreamReader(response.GetResponseStream(), Encoding.GetEncoding(1252)))
                    {
                        output = stream.ReadToEnd();
                    }
                }
            }
            catch (Exception e) { }
            return output;
        }


        public static string ShareFeed(string fbAccesstoken, string FeedId, string pageId, string message, string fbUserId, string title, int time)
        {

            try
            {
                ShareathonRepository sharepo = new ShareathonRepository();
                Domain.Socioboard.Domain.SharethonPost objshrpost = new Domain.Socioboard.Domain.SharethonPost();
                string link = "https://www.facebook.com/" + pageId + "/posts/" + FeedId;

                FacebookClient fb = new FacebookClient();
                fb.AccessToken = fbAccesstoken;
                var args = new Dictionary<string, object>();

                args["message"] = message;
                args["description"] = title;
                args["link"] = link;
                if (!sharepo.IsPostExist(fbUserId, pageId, FeedId))
                {
                    dynamic output = fb.Post("v2.0/" + fbUserId + "/feed", args);
                    objshrpost.Id = Guid.NewGuid();
                    objshrpost.Facebookaccountid = fbUserId;
                    objshrpost.Facebookpageid = pageId;
                    string feed_id = output["id"].ToString();
                    objshrpost.PostId = FeedId;
                    objshrpost.PostedTime = DateTime.UtcNow;
                    sharepo.AddShareathonPost(objshrpost);

                }

            }
            catch (Exception ex)
            {

            }



            return "success";
        }


        public static string ShareFeedonGroup(string fbAccesstoken, string FeedId, string pageId, string message, string fbgroupId, string title, int time, string faceaccountId)
        {
            string[] feedid = FeedId.TrimEnd(',').Split(',');

            string[] grpid = fbgroupId.Split(',');
            for (int i = 0; i < feedid.Length; i++)
            {
                try
                {
                    ShareathonGroupRepository shareagrp = new ShareathonGroupRepository();
                    Domain.Socioboard.Domain.SharethonGroupPost objshrgrp = new Domain.Socioboard.Domain.SharethonGroupPost();
                    string link = "https://www.facebook.com/" + feedid[i];

                    FacebookClient fb = new FacebookClient();
                    fb.AccessToken = fbAccesstoken;
                    var args = new Dictionary<string, object>();


                    args["message"] = message;
                    args["description"] = title;
                    args["link"] = link;
                    foreach (var item in grpid)
                    {
                        new Thread(delegate()
                        {
                            PostGroupMultiple(item, time, faceaccountId, feedid, i, shareagrp, objshrgrp, fb, args);
                        });
                        Thread.Sleep(1000 * 15);
                    }




                }
                catch (Exception ex)
                {

                }
            }


            return "success";
        }

        private static void PostGroupMultiple(string fbgroupId, int time, string faceaccountId, string[] feedid, int i, ShareathonGroupRepository shareagrp, Domain.Socioboard.Domain.SharethonGroupPost objshrgrp, FacebookClient fb, Dictionary<string, object> args)
        {
            if (!shareagrp.IsPostExist(fbgroupId, feedid[i], faceaccountId))
            {
                try
                {
                    dynamic output = fb.Post("v2.0/" + fbgroupId + "/feed", args);
                }
                catch (Exception ex)
                {
                    Console.Write(ex.StackTrace);
                }
                objshrgrp.Id = Guid.NewGuid();
                objshrgrp.Facebookaccountid = faceaccountId;
                objshrgrp.Facebookgroupid = fbgroupId;
                objshrgrp.PostId = feedid[i];
                shareagrp.AddShareathonPost(objshrgrp);
                Thread.Sleep(1000 * 60 * time);
            }
        }


        public static string GetFbPageDetails(string url, string accestoken)
        {
            try
            {
                FacebookClient fb = new FacebookClient();
                fb.AccessToken = accestoken;
                dynamic pageinfo = null;
                string[] pageurl = url.Split(',');
                string ProfilePageId = "";
                try
                {
                    foreach (var item in pageurl)
                    {
                        pageinfo = fb.Get(item);
                        ProfilePageId = pageinfo["id"] + "," + ProfilePageId;
                    }

                }
                catch (Exception ex1)
                {


                }

                return ProfilePageId;
            }
            catch (Exception ex)
            {
                return "";
            }
        }



        public static void postfeedGroup(string fbaccesstoken, string fbgrpids, string feedid, string facebookaccountid, int time)
        {
            List<string> lstPost = new List<string>();
            int lstCout = 0;
            bool isPosted = false;
            ShareathonGroupRepository shareagrp = new ShareathonGroupRepository();
            Domain.Socioboard.Domain.SharethonGroupPost objshrgrp = new Domain.Socioboard.Domain.SharethonGroupPost();
            FacebookClient fb = new FacebookClient();
            string[] postid = feedid.TrimEnd(',').Split(',');
            fb.AccessToken = fbaccesstoken;
            Random r = new Random();
            int length = postid.Length;
            while (length != lstCout)
            {

                int i = r.Next(0, length - 1);

                if (!lstPost.Contains(postid[i]))
                {
                    lstPost.Add(postid[i]);
                    lstCout++;


                    string[] group = fbgrpids.Split(',');
                    foreach (var item in group)
                    {
                        isPosted = shareagrp.IsPostExist(item, postid[i], facebookaccountid);
                        if (!isPosted)
                        {
                            string link = "https://www.facebook.com/" + postid[i];
                            var args = new Dictionary<string, object>();

                            args["link"] = link;
                            try
                            {
                                dynamic output = fb.Post("v2.0/" + item + "/feed", args);
                            }
                            catch (Exception ex)
                            {
                                Console.Write(ex.StackTrace);
                            }
                            objshrgrp.Id = Guid.NewGuid();
                            objshrgrp.Facebookaccountid = facebookaccountid;
                            objshrgrp.Facebookgroupid = item;
                            objshrgrp.PostId = postid[i];
                            objshrgrp.PostedTime = DateTime.UtcNow;
                            shareagrp.AddShareathonPost(objshrgrp);

                            Thread.Sleep(1000 * 15);
                        }
                    }

                    Thread.Sleep(1000 * 60 * time);
                }
            }

        }


       

        
    }
}
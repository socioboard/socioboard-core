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

namespace SocialScoup.Group
{
    public partial class AjaxGroup : System.Web.UI.Page
    {
        string gid = string.Empty;
        string ack = string.Empty;
        string returndata = string.Empty;
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
    }
}
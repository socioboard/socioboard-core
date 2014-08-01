using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GlobusTumblrLib.Authentication;
using System.Configuration;

namespace GlobusTumblerLib.Tumblr.Core.BlogMethods
{
    public class PublishedPosts
    {

        public void PostData(string accesstoken, string accesstokensecret, string Hostname,string body,string title,string type)
        {
           try
            {
            oAuthTumbler.TumblrConsumerKey = ConfigurationManager.AppSettings["TumblrClientKey"];
            oAuthTumbler.TumblrConsumerSecret = ConfigurationManager.AppSettings["TumblrClientSec"];
            var prms = new Dictionary<string, object>();
            var postUrl = string.Empty ;
            if (type == "text")
            {
               
                prms.Add("type", "text");
                prms.Add("title", title);
                prms.Add("body", body);

                // var postUrl = "https://api.tumblr.com/v2/blog/" + Hostname + ".tumblr.com/posts/text?api_key=" + oAuthTumbler.TumblrConsumerKey;
                postUrl = "https://api.tumblr.com/v2/blog/" + Hostname + ".tumblr.com/post";
            }
            if (type == "quote")
            {
                

                prms.Add("type", "quote");
                prms.Add("quote", title);
                prms.Add("source", body);            
                postUrl = "https://api.tumblr.com/v2/blog/" + Hostname + ".tumblr.com/post";
            }
            else if (type == "photo")
            {
                // Load file meta data with FileInfo
                System.IO.FileInfo fileInfo = new System.IO.FileInfo(title);

                // The byte[] to save the data in
                byte[] data = new byte[fileInfo.Length];

                // Load a filestream and put its content into the byte[]
                using (System.IO.FileStream fs = fileInfo.OpenRead())
                {
                    fs.Read(data, 0, data.Length);
                }

                // Delete the temporary file
                fileInfo.Delete();

                prms.Add("type", "photo");
                prms.Add("caption", body);
                prms.Add("source", title);
                prms.Add("data", data);

                postUrl = "https://api.tumblr.com/v2/blog/" + Hostname + ".tumblr.com/post";
            }
          
                KeyValuePair<string, string> LoginDetails = new KeyValuePair<string, string>(accesstoken, accesstokensecret);

               
                string result = oAuthTumbler.OAuthData(postUrl, "POST", LoginDetails.Key, LoginDetails.Value, prms);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
            }

        }


        public void PostAudioData(string accesstoken, string accesstokensecret, string Hostname, string Externalurl, string type)
        {
            try
            {
                oAuthTumbler.TumblrConsumerKey = ConfigurationManager.AppSettings["TumblrClientKey"];
                oAuthTumbler.TumblrConsumerSecret = ConfigurationManager.AppSettings["TumblrClientSec"];
                var prms = new Dictionary<string, object>();
                var postUrl = string.Empty;
               
                if (type == "audio")
                {
                    // Load file meta data with FileInfo
                    System.IO.FileInfo fileInfo = new System.IO.FileInfo(Externalurl);

                    // The byte[] to save the data in
                    byte[] data = new byte[fileInfo.Length];

                    // Load a filestream and put its content into the byte[]
                    using (System.IO.FileStream fs = fileInfo.OpenRead())
                    {
                        fs.Read(data, 0, data.Length);
                    }

                    // Delete the temporary file
                    fileInfo.Delete();

                    prms.Add("type", "audio");
                  //  prms.Add("caption", body);
                    prms.Add("source", Externalurl);
                    prms.Add("data", data);

                    postUrl = "https://api.tumblr.com/v2/blog/" + Hostname + ".tumblr.com/post";
                }

                KeyValuePair<string, string> LoginDetails = new KeyValuePair<string, string>(accesstoken, accesstokensecret);


                string result = oAuthTumbler.OAuthData(postUrl, "POST", LoginDetails.Key, LoginDetails.Value, prms);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
            }

        }


        public void PostdescriptionData(string accesstoken, string accesstokensecret, string Hostname, string linkurl, string title, string description, string type)
        {
           try
            {
            oAuthTumbler.TumblrConsumerKey = ConfigurationManager.AppSettings["TumblrClientKey"];
            oAuthTumbler.TumblrConsumerSecret = ConfigurationManager.AppSettings["TumblrClientSec"];
            var prms = new Dictionary<string, object>();
            var postUrl = string.Empty ;
            if (type == "link")
            {              
                prms.Add("type", "link");
                prms.Add("title", title);
                prms.Add("url", linkurl);
                prms.Add("description", description);              
                postUrl = "https://api.tumblr.com/v2/blog/" + Hostname + ".tumblr.com/post";
            }
               
            else if (type == "chat")
            {
                prms.Add("type", "chat");
                prms.Add("title", title);
                prms.Add("conversation", linkurl);
               // prms.Add("dialogue", description);

                  postUrl = "https://api.tumblr.com/v2/blog/" + Hostname + ".tumblr.com/post";
            }

            else if (type == "video")
            {
                prms.Add("type", "video");
              
                if (string.IsNullOrEmpty(linkurl))
                {
                    prms.Add("embed", "title");
                    prms.Add("caption", "description");

                    //System.IO.FileInfo fileInfo = new System.IO.FileInfo(title);


                    //byte[] data = new byte[fileInfo.Length];

                    //// Load a filestream and put its content into the byte[]
                    //using (System.IO.FileStream fs = fileInfo.OpenRead())
                    //{
                    //    fs.Read(data, 0, data.Length);
                    //}

                    //// Delete the temporary file
                    //fileInfo.Delete();

                    //prms.Add("data", data);

                }


                else {

                    prms.Add("embed", "linkurl");
                    System.IO.FileInfo fileInfo = new System.IO.FileInfo(linkurl);


                    byte[] data = new byte[fileInfo.Length];

                    // Load a filestream and put its content into the byte[]
                    using (System.IO.FileStream fs = fileInfo.OpenRead())
                    {
                        fs.Read(data, 0, data.Length);
                    }

                    // Delete the temporary file
                    fileInfo.Delete();

                    prms.Add("data", data);
                }



                postUrl = "https://api.tumblr.com/v2/blog/" + Hostname + ".tumblr.com/post";
            }

           

                KeyValuePair<string, string> LoginDetails = new KeyValuePair<string, string>(accesstoken, accesstokensecret);

               
                string result = oAuthTumbler.OAuthData(postUrl, "POST", LoginDetails.Key, LoginDetails.Value, prms);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
            }

        }

    }
}

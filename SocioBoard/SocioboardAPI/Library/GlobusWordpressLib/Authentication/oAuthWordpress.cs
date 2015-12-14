using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace GlobusWordpressLib.Authentication
{
    public class oAuthWordpress : oAuthBase
    {
        public string client_id = "";
        public string redirect_uri = "";
        public string client_secret = "";
        public string code = "";
        public string access_token = "";
        public string APIWebRequest(string url, string postData)
        {
            string pageContent = "";
            HttpWebRequest httpRequest = (HttpWebRequest)WebRequest.Create(url);
            httpRequest.Method = "POST";
            byte[] data = Encoding.ASCII.GetBytes(postData);
            httpRequest.ContentType = "multipart/form-data";
            if (!string.IsNullOrEmpty(access_token))
            {
                httpRequest.Headers.Add("Authorization", "Bearer " + access_token);
            }
            httpRequest.ContentLength = data.Length;
            Stream requestStream = httpRequest.GetRequestStream();
            requestStream.Write(data, 0, data.Length);
            requestStream.Close();
            try
            {
                HttpWebResponse httResponse = (HttpWebResponse)httpRequest.GetResponse();
                Stream responseStream = httResponse.GetResponseStream();
                StreamReader responseStreamReader = new StreamReader(responseStream, Encoding.Default);
                pageContent = responseStreamReader.ReadToEnd();
                responseStreamReader.Close();
                responseStream.Close();
                httResponse.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return pageContent;
        }
        public string PostDataToGetAccessToken()
        {
            string postData = "client_id=" + HttpUtility.UrlEncode(client_id) + "&redirect_uri=" + HttpUtility.UrlEncode(redirect_uri) + "&client_secret=" + HttpUtility.UrlEncode(client_secret) + "&code=" + HttpUtility.UrlEncode(code) + "&grant_type=authorization_code";
            return postData;
        }
        public string APIWebRequestToGetUserInfo(string url)
        {
            HttpWebRequest httpRequest = (HttpWebRequest)WebRequest.Create(url);
            httpRequest.Method = "GET";
            httpRequest.ContentType = "application/x-www-form-urlencoded";
            httpRequest.Headers.Add("Authorization", "Bearer " + access_token);
            HttpWebResponse httResponse = (HttpWebResponse)httpRequest.GetResponse();
            Stream responseStream = httResponse.GetResponseStream();
            StreamReader responseStreamReader = new StreamReader(responseStream, Encoding.Default);
            string pageContent = responseStreamReader.ReadToEnd();
            responseStreamReader.Close();
            responseStream.Close();
            httResponse.Close();
            return pageContent;
        }
        public string PostDataToPostBlog(string title, string content, string tags, string image) 
        {
            string PostData = "title=" + HttpUtility.UrlEncode(title) + "&content=" + HttpUtility.UrlEncode(content) + "&tags=" + HttpUtility.UrlEncode(tags) + "&categories=API";
            //if (!string.IsNullOrEmpty(image))
            //{
            //    PostData += "&media[]=" +  image;
            //}
            return PostData;
        }
    }
}

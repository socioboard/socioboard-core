using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.IO;
using System.Net;
using GlobusGooglePlusLib.App.Core;
using Newtonsoft.Json.Linq;

namespace GlobusGooglePlusLib.Authentication
{
    public class oAuthToken
    {
        public enum Method { GET, POST, DELETE };
        private string _consumerKey = string.Empty;
        private string _consumerSecret = string.Empty;

        public string ConsumerKey
        {
            get
            {
                if (_consumerKey.Length == 0)
                {
                    _consumerKey = ConfigurationManager.AppSettings["consumerKey"];
                }
                return _consumerKey;
            }
            set { _consumerKey = value; }
        }

        public string ConsumerSecret
        {
            get
            {
                if (_consumerSecret.Length == 0)
                {
                    _consumerSecret = ConfigurationManager.AppSettings["consumerSecret"];
                }
                return _consumerSecret;
            }
            set { _consumerSecret = value; }
        }

        /// <summary>
        ///  To get authentication Link
        /// </summary>
        /// <param name="scope"></param>
        /// <returns></returns>
        public string GetAutherizationLink(string scope)
        {
            string strAuthUrl = Globals.strAuthentication;
            strAuthUrl +="?scope="+ scope +"&state=%2Fprofile&redirect_uri=" + ConfigurationManager.AppSettings["GplusRedirectUrl"] + "&response_type=code&client_id=" + ConfigurationManager.AppSettings["GplusClientId"] + "&approval_prompt=force&access_type=offline";
            return strAuthUrl;
        }

        /// <summary>
        /// After the web server receives the authorization code, it may exchange the authorization code for an access token and a refresh token.
        /// </summary>
        /// <param name="code">authorization code</param>
        /// <returns></returns>
        public string GetRefreshToken(string code)
        {
            string postData = "code=" + code + "&client_id=" + ConfigurationManager.AppSettings["GplusClientId"] + "&client_secret=" + ConfigurationManager.AppSettings["GplusClientSecretKey"] + "&redirect_uri=" + ConfigurationManager.AppSettings["GplusRedirectUrl"] + "&grant_type=authorization_code";
            string result = WebRequest(GlobusGooglePlusLib.Authentication.oAuthToken.Method.POST, Globals.strRefreshToken, postData);
            return result;
        }

        /// <summary>
        ///  obtain a new access token by sending a refresh token to the Google OAuth 2.0 Authorization server.
        /// </summary>
        /// <param name="refreshToken">refreshToken</param>
        /// <returns></returns>
        public string GetAccessToken(string refreshToken)
        {
            string postData = "refresh_token=" + refreshToken + "&client_id=" + ConfigurationManager.AppSettings["GplusClientId"] + "&client_secret=" + ConfigurationManager.AppSettings["GplusClientSecretKey"] + "&grant_type=refresh_token";
            string[] header = { "token_type", "expires_in" };
            string[] val = { "Bearer", "3600" };
            Uri path = new Uri(Globals.strRefreshToken);
          //  string response = postWebRequest(path, postData, header, val);
            string response = WebRequest(GlobusGooglePlusLib.Authentication.oAuthToken.Method.POST, Globals.strRefreshToken, postData);
            return response;
        }

        public JArray GetUserInfo(string UserId,string access)
        {
            oAuthToken objToken = new oAuthToken();
            string RequestUrl = Globals.strUserInfo +"&access_token=" + access;
            Uri path = new Uri(RequestUrl);
            string[] header = { "token_type", "expires_in" };
            string[] val = { "Bearer", "3600" };
            string response = WebRequestHeader(path, header, val);
            if (!response.StartsWith("["))
                response = "[" + response + "]";
            return JArray.Parse(response);
        }
        /// <summary>
        /// Web Request Wrapper
        /// </summary>
        /// <param name="method">Http Method</param>
        /// <param name="url">Full url to the web resource</param>
        /// <param name="postData">Data to post in querystring format</param>
        /// <returns>The web server response.</returns>
        public string WebRequest(Method method, string url, string postData)
        {
            HttpWebRequest webRequest = null;
            StreamWriter requestWriter = null;
            string responseData = "";

            webRequest = System.Net.WebRequest.Create(url) as HttpWebRequest;
            webRequest.Method = method.ToString();
            webRequest.ServicePoint.Expect100Continue = false;
            //webRequest.UserAgent  = "Identify your application please.";
            //webRequest.Timeout = 20000;
            if (method == Method.POST || method == Method.DELETE)
            {
                
                webRequest.ContentType = "application/x-www-form-urlencoded";
                //  webRequest.ContentType = "multipart/form-data;";
                //POST the data.
                requestWriter = new StreamWriter(webRequest.GetRequestStream());
                try
                {
                    requestWriter.Write(postData);
                }
                catch
                {
                    throw;
                }
                finally
                {
                    requestWriter.Close();
                    requestWriter = null;
                }
            }

            responseData = WebResponseGet(webRequest);
            webRequest = null;
            return responseData;

        }

        /// <summary>
        /// Process the web response.
        /// </summary>
        /// <param name="webRequest">The request object.</param>
        /// <returns>The response data.</returns>
        public string WebResponseGet(HttpWebRequest webRequest)
        {
            StreamReader responseReader = null;
            string responseData = "";

            try
            {
                responseReader = new StreamReader(webRequest.GetResponse().GetResponseStream());
                responseData = responseReader.ReadToEnd();
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                webRequest.GetResponse().GetResponseStream().Close();
                responseReader.Close();
                responseReader = null;
            }

            return responseData;
        }

        public string WebRequestHeader(Uri url, string[] HeaderName, string[] Value)
        {
            HttpWebRequest gRequest;
              HttpWebResponse gResponse;
            gRequest = (HttpWebRequest)System.Net.WebRequest.Create(url.ToString());
            gRequest.UserAgent = "Mozilla/5.0 (Windows; U; Windows NT 5.1; en-US; rv:1.9.0.4) Gecko/2008102920 Firefox/3.0.4";
            gRequest.CookieContainer = new CookieContainer();
            gRequest.Method = "GET";
          //  gRequest.ContentLength = 1024;
            //gRequest.Proxy = new WebProxy("173.234.140.18");

            for (int i = 0; i < HeaderName.Length; i++)
            {
                gRequest.Headers.Add(HeaderName[i], Value[i]);
            }
          
        
            gResponse = (HttpWebResponse)gRequest.GetResponse();
            Stream getstream = gResponse.GetResponseStream();
            StreamReader readStream = new StreamReader(getstream);
            
         
                //get all the cookies from the current request and add them to the response object cookies
                gResponse.Cookies = gRequest.CookieContainer.GetCookies(gRequest.RequestUri);
                //check if response object has any cookies or not
             

                StreamReader reader = new StreamReader(gResponse.GetResponseStream());
                string responseString = reader.ReadToEnd();
                reader.Close();
                //Console.Write("Response String:" + responseString);
                return responseString;
         

        }

        public string postWebRequest(Uri formActionUrl, string postData, string[] HeaderName, string[] Value)
        {
            HttpWebRequest gRequest;
            HttpWebResponse gResponse;
            gRequest = (HttpWebRequest)System.Net.WebRequest.Create(formActionUrl);
            gRequest.UserAgent = "Mozilla/5.0 (Windows; U; Windows NT 5.1; en-US; rv:1.9.0.4) Gecko/2008102920 Firefox/3.0.4";
            gRequest.CookieContainer = new CookieContainer();
            gRequest.Method = "POST";
            gRequest.Accept = " text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8, */*";
            gRequest.KeepAlive = true;
            gRequest.ContentType = @"text/html; charset=iso-8859-1";

            for (int i = 0; i < HeaderName.Length; i++)
            {
                gRequest.Headers.Add(HeaderName[i], Value[i]);
            }
          

            //logic to postdata to the form
            string postdata = string.Format(postData);
            byte[] postBuffer = System.Text.Encoding.GetEncoding(1252).GetBytes(postData);
            gRequest.ContentLength = postBuffer.Length;
            Stream postDataStream = gRequest.GetRequestStream();
            postDataStream.Write(postBuffer, 0, postBuffer.Length);
            postDataStream.Close();
            //post data logic ends

            //Get Response for this request url
            gResponse = (HttpWebResponse)gRequest.GetResponse();



            //check if the status code is http 200 or http ok
            if (gResponse.StatusCode == HttpStatusCode.OK)
            {
                StreamReader reader = new StreamReader(gResponse.GetResponseStream());
                string responseString = reader.ReadToEnd();
                reader.Close();
                //Console.Write("Response String:" + responseString);
                return responseString;
            }
            else
            {
                return "Error in posting data";
            }
        }
 
    }
}

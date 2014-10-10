using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.IO;
using System.Net;
using GlobussDropboxLib.App.Core;
using Newtonsoft.Json.Linq;
using System.Web;
using System.Collections.Specialized;

namespace GlobussDropboxLib.Authentication
{
    public class oAuthToken : oAuthBase2
    {
        public enum Method { GET, POST, DELETE };
        private string _consumerKey = string.Empty;
        private string _consumerSecret = string.Empty;

        private string _token = "";
        private string _tokenSecret = "";

        public string Token { get { return _token; } set { _token = value; } }
        public string TokenSecret { get { return _tokenSecret; } set { _tokenSecret = value; } }

        public string Id { get; set; }
        public string FirstName { get; set; }

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
        /// Gets the request token and secret.
        /// </summary>
        /// <returns></returns>
        public string GetRequestToken()
        {
            var uri = new Uri(Global._APP_REQUESTTOEKN_URL);

            // Generate a signature
            oAuthToken oAuth = new oAuthToken();
            string nonce = oAuth.GenerateNonce();
            string timeStamp = oAuth.GenerateTimeStamp();
            string parameters;
            string normalizedUrl;
            string signature = oAuth.GenerateSignature(uri, _consumerKey, _consumerSecret,
                String.Empty, String.Empty, "GET", timeStamp, nonce, oAuthBase2.SignatureTypes.HMACSHA1,
                out normalizedUrl, out parameters);

            // Encode the signature
            signature = HttpUtility.UrlEncode(signature);

            // Now buld the url by appending the consumer key, secret timestamp and all.
            StringBuilder requestUri = new StringBuilder(uri.ToString());
            requestUri.AppendFormat("?oauth_consumer_key={0}&", _consumerKey);
            requestUri.AppendFormat("oauth_nonce={0}&", nonce);
            requestUri.AppendFormat("oauth_timestamp={0}&", timeStamp);
            requestUri.AppendFormat("oauth_signature_method={0}&", "HMAC-SHA1");
            requestUri.AppendFormat("oauth_version={0}&", "1.0");
            requestUri.AppendFormat("oauth_signature={0}", signature);

            // createa request to call the dropbox api.
            string request = WebRequest(Method.GET, (new Uri(requestUri.ToString())).AbsoluteUri, string.Empty);
            //request.Method = WebRequestMethods.Http.Get;

            //// Get the response.
            //WebResponse response = request.GetResponse();

            // Read the response
            string queryString = request;

            var parts = queryString.Split('&');
            this.Token = parts[1].Substring(parts[1].IndexOf('=') + 1);
            this.TokenSecret = parts[0].Substring(parts[0].IndexOf('=') + 1);
            return request;

        }


        /// <summary>
        /// Gets the access token by the request token and the token secret.
        /// </summary>
        /// <param name="oauthToken"></param>
        /// <returns></returns>
        public string GetAccessToken()
        {

            var uri = new Uri(Global._App_ACCESSTOKEN_URI);

            oAuthBase2 oAuth = new oAuthBase2();

            // Generate a signature
            var nonce = oAuth.GenerateNonce();
            var timeStamp = oAuth.GenerateTimeStamp();
            string parameters;
            string normalizedUrl;
            var signature = oAuth.GenerateSignature(uri, _consumerKey, _consumerSecret,
                this.Token, this._tokenSecret, "GET", timeStamp, nonce,
                oAuthBase2.SignatureTypes.HMACSHA1, out normalizedUrl, out parameters);

            // Encode the signature
            signature = HttpUtility.UrlEncode(signature);

            // Now buld the url by appending the consumer key, secret timestamp and all.
            var requestUri = new StringBuilder(uri.ToString());
            requestUri.AppendFormat("?oauth_consumer_key={0}&", this._consumerKey);
            requestUri.AppendFormat("oauth_token={0}&", this.Token);
            requestUri.AppendFormat("oauth_nonce={0}&", nonce);
            requestUri.AppendFormat("oauth_timestamp={0}&", timeStamp);
            requestUri.AppendFormat("oauth_signature_method={0}&", "HMAC-SHA1");
            requestUri.AppendFormat("oauth_version={0}&", "1.0");
            requestUri.AppendFormat("oauth_signature={0}", signature);

            // createa request to call the dropbox api.
            string request = WebRequest(Method.GET, requestUri.ToString(), string.Empty);
            //request.Method = WebRequestMethods.Http.Get;

            //// Get the response.
            //WebResponse response = request.GetResponse();

            //// Read the response
            //StreamReader reader = new StreamReader(response.GetResponseStream());
            string accessToken = request;//reader.ReadToEnd();

            var parts = accessToken.Split('&');
            string token = parts[1].Substring(parts[1].IndexOf('=') + 1);
            string secret = parts[0].Substring(parts[0].IndexOf('=') + 1);

            return request;//new OAuthToken(token, secret);
        }


        /// <summary>
        /// Exchange the request token for an access token.
        /// </summary>
        /// <param name="authToken">The oauth_token is supplied by Twitter's authorization page following the callback.</param>
        public void AccessTokenGet(string authToken)
        {
            this.Token = authToken;

            string response = oAuthWebRequest(Method.POST, Global._App_ACCESSTOKEN_URI, string.Empty);

            if (response.Length > 0)
            {
                //Store the Token and Token Secret
                NameValueCollection qs = HttpUtility.ParseQueryString(response);
                if (qs["oauth_token"] != null)
                {
                    this.Token = qs["oauth_token"];
                }
                if (qs["oauth_token_secret"] != null)
                {
                    this.TokenSecret = qs["oauth_token_secret"];
                }
            }
        }



        /// <summary>
        /// Submit a web request using oAuth.
        /// </summary>
        /// <param name="method">GET or POST</param>
        /// <param name="url">The full url, including the querystring.</param>
        /// <param name="postData">Data to post (querystring format)</param>
        /// <returns>The web server response.</returns>
        public string oAuthWebRequest(Method method, string url, string postData)
        {
            string outUrl = "";
            string querystring = "";
            string ret = "";


            //Setup postData for signing.
            //Add the postData to the querystring.
            if (method == Method.POST)
            {
                if (postData.Length > 0)
                {
                    //Decode the parameters and re-encode using the oAuth UrlEncode method.
                    NameValueCollection qs = HttpUtility.ParseQueryString(postData);
                    postData = "";
                    foreach (string key in qs.AllKeys)
                    {
                        if (postData.Length > 0)
                        {
                            postData += "&";
                        }
                        qs[key] = HttpUtility.UrlDecode(qs[key]);
                        qs[key] = this.UrlEncode(qs[key]);
                        postData += key + "=" + qs[key];

                    }
                    if (url.IndexOf("?") > 0)
                    {
                        url += "&";
                    }
                    else
                    {
                        url += "?";
                    }
                    url += postData;
                }
            }

            Uri uri = new Uri(url);

            string nonce = this.GenerateNonce();
            string timeStamp = this.GenerateTimeStamp();

            //Generate Signature
            string sig = this.GenerateSignature(uri,
                this._consumerKey,
                this._consumerSecret,
                this.Token,
                this.TokenSecret,
                method.ToString(),
                timeStamp,
                nonce,
                out outUrl,
                out querystring);


            querystring += "&oauth_signature=" + HttpUtility.UrlEncode(sig);

            //Convert the querystring to postData
            if (method == Method.POST)
            {
                postData = querystring;
                querystring = "";
            }

            if (querystring.Length > 0)
            {
                outUrl += "?";
            }

            if (method == Method.POST || method == Method.GET)
                ret = WebRequest(method, outUrl + querystring, postData);
            //else if (method == Method.PUT)
            //ret = WebRequestWithPut(outUrl + querystring, postData);
            return ret;
        }


        /// <summary>
        /// WebRequestWithPut
        /// </summary>
        /// <param name="method">WebRequestWithPut</param>
        /// <param name="url"></param>
        /// <param name="postData"></param>
        /// <returns></returns>
        public string APIWebRequest(string method, string url, string postData)
        {
            Uri uri = new Uri(url);
            string nonce = this.GenerateNonce();
            string timeStamp = this.GenerateTimeStamp();

            string outUrl, querystring;

            //Generate Signature
            string sig = this.GenerateSignature(uri,
                this._consumerKey,
                this._consumerSecret,
                this.Token,
                this.TokenSecret,
                method,
                timeStamp,
                nonce,
                out outUrl,
                out querystring);

            //querystring += "&oauth_signature=" + HttpUtility.UrlEncode(sig);
            //NameValueCollection qs = HttpUtility.ParseQueryString(querystring);

            //string finalGetUrl = outUrl + "?" + querystring;

            HttpWebRequest webRequest = null;

            //webRequest = System.Net.WebRequest.Create(finalGetUrl) as HttpWebRequest;
            webRequest = System.Net.WebRequest.Create(url) as HttpWebRequest;
            //webRequest.ContentType = "text/xml";
            webRequest.Method = method;
            webRequest.Credentials = CredentialCache.DefaultCredentials;
            webRequest.AllowWriteStreamBuffering = true;

            webRequest.PreAuthenticate = true;
            webRequest.ServicePoint.Expect100Continue = false;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3;

            webRequest.Headers.Add("Authorization", "OAuth oauth_version=\"1.0\", oauth_signature_method=\"PLAINTEXT\", oauth_consumer_key=\"" + ConfigurationManager.AppSettings["DBX_Appkey"] + "\", oauth_token=\"" + this.Token + "\", oauth_signature=\"" + sig + "\"");
            //webRequest.Headers.Add("Authorization", "OAuth oauth_nonce=\"" + nonce + "\", oauth_signature_method=\"HMAC-SHA1\", oauth_timestamp=\"" + timeStamp + "\", oauth_consumer_key=\"" + this.ConsumerKey + "\", oauth_token=\"" + this.Token + "\", oauth_signature=\"" + HttpUtility.UrlEncode(sig) + "\", oauth_version=\"1.0\"");
            if (postData != null)
            {
                byte[] fileToSend = Encoding.UTF8.GetBytes(postData);
                webRequest.ContentLength = fileToSend.Length;

                Stream reqStream = webRequest.GetRequestStream();

                reqStream.Write(fileToSend, 0, fileToSend.Length);
                reqStream.Close();
            }

            string returned = WebResponseGet(webRequest);

            return returned;
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
                //webRequest.Headers["Authorization"] = "Basic czZCaGRSa3F0MzpnWDFmQmF0M2JW";
              
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
                //throw;
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
            gRequest = (HttpWebRequest)System.Net.WebRequest.Create(url);
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

using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Web;
using System.IO;
using System.Collections.Specialized;
using System.Text.RegularExpressions;
//using BaseLib;

namespace Api.Socioboard.Helper
{
    public class GlobusHttpHelper
    {
        CookieCollection gCookies;
        public HttpWebRequest gRequest;
        public HttpWebResponse gResponse;

        //public string UserAgent = "Mozilla/5.0 (Windows NT 6.1; rv:20.0) Gecko/20100101 Firefox/20.0";
        public string UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/43.0.2357.124 Safari/537.36";//"Mozilla/5.0 (Windows NT 6.1; rv:20.0) Gecko/20100101 Firefox/20.0";   //"Mozilla/4.0 (compatible; MSIE 8.0; Windows NT 6.1; WOW64; Trident/4.0; SLCC2; .NET CLR 2.0.50727; .NET CLR 3.5.30729; .NET CLR 3.0.30729; Media Center PC 6.0; .NET4.0C; .NET4.0E; InfoPath.3)";
        int Timeout = 90000;

        string proxyAddress = string.Empty;
        int port = 80;
        string proxyUsername = string.Empty;
        string proxyPassword = string.Empty;

        public GlobusHttpHelper()
        {
            ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
        }

        public Uri GetResponseData()
        {
            return gResponse.ResponseUri;
        }

        public void getUserAgentForMobileVersion()
        {
            if (Globals.IsMobileVersion)
            {
                UserAgent = "Mozilla/5.0 (iPhone; U; CPU iPhone OS 4_0 like Mac OS X; en-us) AppleWebKit/532.9 (KHTML, like Gecko) Version/4.0.5 Mobile/8A293 Safari/6531.22.7";
            }
            else
            {
                UserAgent = "Mozilla/5.0 (Windows NT 6.1; rv:20.0) Gecko/20100101 Firefox/20.0";   //"Mozilla/4.0 (compatible; MSIE 8.0; Windows NT 6.1; WOW64; Trident/4.0; SLCC2; .NET CLR 2.0.50727; .NET CLR 3.5.30729; .NET CLR 3.0.30729; Media Center PC 6.0; .NET4.0C; .NET4.0E; InfoPath.3)";
            }
        }
        public string getHtmlfromUrl(Uri url, string Referes, string Token)
        {
            try
            {
                getUserAgentForMobileVersion();
                setExpect100Continue();
                gRequest = (HttpWebRequest)WebRequest.Create(url);

                //gRequest.UserAgent = "Mozilla/5.0 (Windows NT 6.1; rv:12.0) Gecko/20100101 Firefox/12.0";// "Mozilla/5.0 (Windows; U; Windows NT 6.1; en-US; rv:1.9.2.24) Gecko/20111103 Firefox/3.6.24";
                gRequest.UserAgent = UserAgent;
                gRequest.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8";
                gRequest.Headers["Accept-Charset"] = "ISO-8859-1,utf-8;q=0.7,*;q=0.7";
                //gRequest.Headers["Cache-Control"] = "max-age=0";
                gRequest.Headers["Accept-Language"] = "en-us,en;q=0.5";
                //gRequest.Connection = "keep-alive";
                gRequest.Timeout = Timeout;

                gRequest.KeepAlive = true;

                gRequest.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;

                gRequest.CookieContainer = new CookieContainer(); //gCookiesContainer;

                gRequest.Method = "GET";
                //gRequest.AllowAutoRedirect = false;
                ChangeProxy(proxyAddress, port, proxyUsername, proxyPassword);

                if (!string.IsNullOrEmpty(Referes))
                {
                    gRequest.Referer = Referes;
                }
                if (!string.IsNullOrEmpty(Token))
                {
                    gRequest.Headers.Add("X-CSRFToken", Token);
                    gRequest.Headers.Add("X-Requested-With", "XMLHttpRequest");
                }



                #region CookieManagment

                if (this.gCookies != null && this.gCookies.Count > 0)
                {
                    setExpect100Continue();
                    gRequest.CookieContainer.Add(gCookies);

                    try
                    {
                        //gRequest.CookieContainer.Add(url, new Cookie("__qca", "P0 - 2078004405 - 1321685323158", "/"));
                        //gRequest.CookieContainer.Add(url, new Cookie("__utma", "101828306.1814567160.1321685324.1322116799.1322206824.9", "/"));
                        //gRequest.CookieContainer.Add(url, new Cookie("__utmz", "101828306.1321685324.1.1.utmcsr=(direct)|utmccn=(direct)|utmcmd=(none)", "/"));
                        //gRequest.CookieContainer.Add(url, new Cookie("__utmb", "101828306.2.10.1321858563", "/"));
                        //gRequest.CookieContainer.Add(url, new Cookie("__utmc", "101828306", "/"));
                    }
                    catch (Exception ex)
                    {

                    }
                }
                //Get Response for this request url

                setExpect100Continue();

                gResponse = (HttpWebResponse)gRequest.GetResponse();

                //check if the status code is http 200 or http ok
                if (gResponse.StatusCode == HttpStatusCode.OK)
                {
                    //get all the cookies from the current request and add them to the response object cookies
                    setExpect100Continue();
                    gResponse.Cookies = gRequest.CookieContainer.GetCookies(gRequest.RequestUri);


                    //check if response object has any cookies or not
                    if (gResponse.Cookies.Count > 0)
                    {
                        //check if this is the first request/response, if this is the response of first request gCookies
                        //will be null
                        if (this.gCookies == null)
                        {
                            gCookies = gResponse.Cookies;
                        }
                        else
                        {
                            foreach (Cookie oRespCookie in gResponse.Cookies)
                            {
                                bool bMatch = false;
                                foreach (Cookie oReqCookie in this.gCookies)
                                {
                                    if (oReqCookie.Name == oRespCookie.Name)
                                    {
                                        oReqCookie.Value = oRespCookie.Value;
                                        bMatch = true;
                                        break; // 
                                    }
                                }
                                if (!bMatch)
                                    this.gCookies.Add(oRespCookie);
                            }
                        }
                    }
                #endregion

                    using (StreamReader reader = new StreamReader(gResponse.GetResponseStream()))
                    {
                        string responseString = reader.ReadToEnd();
                        return responseString;
                    }
                }
                else
                {
                    return "Error";
                }
            }
            catch (Exception ex)
            {
                //return null;
                if (ex.Message.Contains("The remote server returned an error: (429)") || ex.Message.Contains("Too Many Requests") || ex.Message.Contains("Client Error (429)"))
                {
                    return "Too Many Requestes";
                }
                return null;
            }

        }


        public string getHtmlfromUrl(Uri url)
        {
            string responseString = string.Empty;
            string responseURI = string.Empty;
            try
            {
                //setExpect100Continue();
                gRequest = (HttpWebRequest)WebRequest.Create(url);
                gRequest.UserAgent = "Mozilla/5.0 (Windows NT 6.1; rv:18.0) Gecko/20100101 Firefox/18.0";

                gRequest.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8";
                gRequest.Headers["Accept-Charset"] = "ISO-8859-1,utf-8;q=0.7,*;q=0.7";
                gRequest.Headers["Accept-Language"] = "en-us,en;q=0.5";

                gRequest.KeepAlive = true;

                gRequest.AllowAutoRedirect = true;

                gRequest.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;

                gRequest.CookieContainer = new CookieContainer(); //gCookiesContainer;

                gRequest.Method = "GET";

                gRequest.Headers.Add("Javascript-enabled", "true");

                #region CookieManagment

                if (this.gCookies != null && this.gCookies.Count > 0)
                {
                    setExpect100Continue();
                    gRequest.CookieContainer.Add(gCookies);

                }

                if (this.gCookies == null)
                {
                    this.gCookies = new CookieCollection();
                }



                //Get Response for this request url

                setExpect100Continue();
                gResponse = (HttpWebResponse)gRequest.GetResponse();

                //check if the status code is http 200 or http ok
                if (gResponse.StatusCode == HttpStatusCode.OK)
                {
                    //get all the cookies from the current request and add them to the response object cookies
                    setExpect100Continue();
                    gResponse.Cookies = gRequest.CookieContainer.GetCookies(gRequest.RequestUri);


                    //check if response object has any cookies or not
                    if (gResponse.Cookies.Count > 0)
                    {
                        //check if this is the first request/response, if this is the response of first request gCookies
                        //will be null
                        if (this.gCookies == null)
                        {
                            gCookies = gResponse.Cookies;
                        }
                        else
                        {
                            foreach (Cookie oRespCookie in gResponse.Cookies)
                            {
                                bool bMatch = false;
                                foreach (Cookie oReqCookie in this.gCookies)
                                {
                                    if (oReqCookie.Name == oRespCookie.Name)
                                    {
                                        oReqCookie.Value = oRespCookie.Value;
                                        bMatch = true;
                                        break; // 
                                    }
                                }
                                if (!bMatch)
                                    this.gCookies.Add(oRespCookie);
                            }
                        }
                    }
                #endregion

                    responseURI = gResponse.ResponseUri.AbsoluteUri;


                    StreamReader reader = new StreamReader(gResponse.GetResponseStream(), Encoding.ASCII);
                    responseString = reader.ReadToEnd();
                    reader.Close();
                    return responseString;
                }
                else
                {
                    return "Error";
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
            }
            return responseString;
        }

        public static string GetParamValue(string pgSrc, string paramName)
        {
            try
            {
                if (pgSrc.Contains("name='" + paramName + "'"))
                {
                    string param = "name='" + paramName + "'";
                    int startparamName = pgSrc.IndexOf(param) + param.Length;
                    startparamName = pgSrc.IndexOf("value=", startparamName) + "value=".Length + 1;
                    int endparamName = pgSrc.IndexOf("'", startparamName);
                    string valueparamName = pgSrc.Substring(startparamName, endparamName - startparamName);
                    return valueparamName;
                }
                else if (pgSrc.Contains("name=\"" + paramName + "\""))
                {
                    string param = "name=\"" + paramName + "\"";
                    int startparamName = pgSrc.IndexOf(param) + param.Length;
                    startparamName = pgSrc.IndexOf("value=", startparamName) + "value=".Length + 1;
                    int endcommentPostID = pgSrc.IndexOf("\"", startparamName);
                    string valueparamName = pgSrc.Substring(startparamName, endcommentPostID - startparamName);
                    return valueparamName;
                }
                else if (pgSrc.Contains("name=\\\"" + paramName + "\\\""))
                {
                    string param = "name=\\\"" + paramName + "\\\"";
                    int startparamName = pgSrc.IndexOf(param) + param.Length;
                    startparamName = pgSrc.IndexOf("value=\\", startparamName) + "value=\\".Length + 1;
                    int endcommentPostID = pgSrc.IndexOf("\\\"", startparamName);
                    string valueparamName = pgSrc.Substring(startparamName, endcommentPostID - startparamName);
                    return valueparamName;
                }
                return null;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public string getHtmlfromUrlMobile(Uri url, string Referes, string Token, string MobileAgent)
        {
            try
            {
                setExpect100Continue();
                gRequest = (HttpWebRequest)WebRequest.Create(url);
                gRequest.UserAgent = UserAgent;
                if (!string.IsNullOrEmpty(MobileAgent))
                {
                    gRequest.UserAgent = MobileAgent;// "Mozilla/5.0 (Windows; U; Windows NT 6.1; en-US; rv:1.9.2.24) Gecko/20111103 Firefox/3.6.24";
                }
                gRequest.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8";
                gRequest.Headers["Accept-Charset"] = "ISO-8859-1,utf-8;q=0.7,*;q=0.7";
                //gRequest.Headers["Cache-Control"] = "max-age=0";
                gRequest.Headers["Accept-Language"] = "en-us,en;q=0.5";
                //gRequest.Connection = "keep-alive";
                gRequest.Timeout = Timeout;

                gRequest.KeepAlive = true;

                gRequest.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;

                gRequest.CookieContainer = new CookieContainer(); //gCookiesContainer;

                gRequest.Method = "GET";
                //gRequest.AllowAutoRedirect = false;
                ChangeProxy(proxyAddress, port, proxyUsername, proxyPassword);

                if (!string.IsNullOrEmpty(Referes))
                {
                    gRequest.Referer = Referes;
                }
                if (!string.IsNullOrEmpty(Token))
                {
                    gRequest.Headers.Add("X-CSRFToken", Token);
                    gRequest.Headers.Add("X-Requested-With", "XMLHttpRequest");
                }



                #region CookieManagment

                if (this.gCookies != null && this.gCookies.Count > 0)
                {
                    setExpect100Continue();
                    gRequest.CookieContainer.Add(gCookies);

                    try
                    {
                        //gRequest.CookieContainer.Add(url, new Cookie("__qca", "P0 - 2078004405 - 1321685323158", "/"));
                        //gRequest.CookieContainer.Add(url, new Cookie("__utma", "101828306.1814567160.1321685324.1322116799.1322206824.9", "/"));
                        //gRequest.CookieContainer.Add(url, new Cookie("__utmz", "101828306.1321685324.1.1.utmcsr=(direct)|utmccn=(direct)|utmcmd=(none)", "/"));
                        //gRequest.CookieContainer.Add(url, new Cookie("__utmb", "101828306.2.10.1321858563", "/"));
                        //gRequest.CookieContainer.Add(url, new Cookie("__utmc", "101828306", "/"));
                    }
                    catch (Exception ex)
                    {

                    }
                }
                //Get Response for this request url

                setExpect100Continue();

                gResponse = (HttpWebResponse)gRequest.GetResponse();

                //check if the status code is http 200 or http ok
                if (gResponse.StatusCode == HttpStatusCode.OK)
                {
                    //get all the cookies from the current request and add them to the response object cookies
                    setExpect100Continue();
                    gResponse.Cookies = gRequest.CookieContainer.GetCookies(gRequest.RequestUri);


                    //check if response object has any cookies or not
                    if (gResponse.Cookies.Count > 0)
                    {
                        //check if this is the first request/response, if this is the response of first request gCookies
                        //will be null
                        if (this.gCookies == null)
                        {
                            gCookies = gResponse.Cookies;
                        }
                        else
                        {
                            foreach (Cookie oRespCookie in gResponse.Cookies)
                            {
                                bool bMatch = false;
                                foreach (Cookie oReqCookie in this.gCookies)
                                {
                                    if (oReqCookie.Name == oRespCookie.Name)
                                    {
                                        oReqCookie.Value = oRespCookie.Value;
                                        bMatch = true;
                                        break; // 
                                    }
                                }
                                if (!bMatch)
                                    this.gCookies.Add(oRespCookie);
                            }
                        }
                    }
                #endregion

                    using (StreamReader reader = new StreamReader(gResponse.GetResponseStream()))
                    {
                        string responseString = reader.ReadToEnd();
                        return responseString;
                    }
                }
                else
                {
                    return "Error";
                }
            }
            catch (Exception ex)
            {
                return null;
            }

        }

        public string getHtmlfromUrl(Uri url, string Referes, string Token, string UserAgent)
        {
            try
            {
                setExpect100Continue();
                gRequest = (HttpWebRequest)WebRequest.Create(url);

                gRequest.UserAgent = "Mozilla/5.0 (Windows NT 6.1; rv:12.0) Gecko/20100101 Firefox/12.0";// "Mozilla/5.0 (Windows; U; Windows NT 6.1; en-US; rv:1.9.2.24) Gecko/20111103 Firefox/3.6.24";
                if (!string.IsNullOrEmpty(UserAgent))
                {
                    gRequest.UserAgent = UserAgent;
                }

                gRequest.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8";
                gRequest.Headers["Accept-Charset"] = "ISO-8859-1,utf-8;q=0.7,*;q=0.7";
                //gRequest.Headers["Cache-Control"] = "max-age=0";
                gRequest.Headers["Accept-Language"] = "en-us,en;q=0.5";
                //gRequest.Connection = "keep-alive";
                gRequest.Timeout = Timeout;

                gRequest.KeepAlive = true;

                gRequest.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;

                gRequest.CookieContainer = new CookieContainer(); //gCookiesContainer;

                gRequest.Method = "GET";
                //gRequest.AllowAutoRedirect = false;
                ChangeProxy(proxyAddress, port, proxyUsername, proxyPassword);

                if (!string.IsNullOrEmpty(Referes))
                {
                    gRequest.Referer = Referes;
                }
                if (!string.IsNullOrEmpty(Token))
                {
                    gRequest.Headers.Add("X-CSRFToken", Token);
                    gRequest.Headers.Add("X-Requested-With", "XMLHttpRequest");
                }



                #region CookieManagment

                if (this.gCookies != null && this.gCookies.Count > 0)
                {
                    setExpect100Continue();
                    gRequest.CookieContainer.Add(gCookies);

                    try
                    {
                        //gRequest.CookieContainer.Add(url, new Cookie("__qca", "P0 - 2078004405 - 1321685323158", "/"));
                        //gRequest.CookieContainer.Add(url, new Cookie("__utma", "101828306.1814567160.1321685324.1322116799.1322206824.9", "/"));
                        //gRequest.CookieContainer.Add(url, new Cookie("__utmz", "101828306.1321685324.1.1.utmcsr=(direct)|utmccn=(direct)|utmcmd=(none)", "/"));
                        //gRequest.CookieContainer.Add(url, new Cookie("__utmb", "101828306.2.10.1321858563", "/"));
                        //gRequest.CookieContainer.Add(url, new Cookie("__utmc", "101828306", "/"));
                    }
                    catch (Exception ex)
                    {

                    }
                }
                //Get Response for this request url

                setExpect100Continue();

                gResponse = (HttpWebResponse)gRequest.GetResponse();

                //check if the status code is http 200 or http ok
                if (gResponse.StatusCode == HttpStatusCode.OK)
                {
                    //get all the cookies from the current request and add them to the response object cookies
                    setExpect100Continue();
                    gResponse.Cookies = gRequest.CookieContainer.GetCookies(gRequest.RequestUri);


                    //check if response object has any cookies or not
                    if (gResponse.Cookies.Count > 0)
                    {
                        //check if this is the first request/response, if this is the response of first request gCookies
                        //will be null
                        if (this.gCookies == null)
                        {
                            gCookies = gResponse.Cookies;
                        }
                        else
                        {
                            foreach (Cookie oRespCookie in gResponse.Cookies)
                            {
                                bool bMatch = false;
                                foreach (Cookie oReqCookie in this.gCookies)
                                {
                                    if (oReqCookie.Name == oRespCookie.Name)
                                    {
                                        oReqCookie.Value = oRespCookie.Value;
                                        bMatch = true;
                                        break; // 
                                    }
                                }
                                if (!bMatch)
                                    this.gCookies.Add(oRespCookie);
                            }
                        }
                    }
                #endregion

                    using (StreamReader reader = new StreamReader(gResponse.GetResponseStream()))
                    {
                        string responseString = reader.ReadToEnd();
                        return responseString;
                    }
                }
                else
                {
                    return "Error";
                }
            }
            catch (Exception ex)
            {
                return null;
            }

        }

        public string getHtmlfromUrlProxyChecker(Uri url, string proxyAddress, int port, string proxyUsername, string proxyPassword)
        {
            string responseString = string.Empty;
            string responseURI = string.Empty;
            try
            {
                setExpect100Continue();
                gRequest = (HttpWebRequest)WebRequest.Create(url);
                //gRequest.UserAgent = "Mozilla/5.0 (Windows; U; Windows NT 6.1; en-US; rv:1.9.2.24) Gecko/20111103 Firefox/3.6.24";

                gRequest.UserAgent = "Mozilla/5.0 (Windows; U; Windows NT 6.1; en-US; rv:1.9.2.24) Gecko/20111103 Firefox/3.6.24";
                gRequest.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8";
                gRequest.Headers["Accept-Charset"] = "ISO-8859-1,utf-8;q=0.7,*;q=0.7";
                //gRequest.Headers["Cache-Control"] = "max-age=0";
                gRequest.Headers["Accept-Language"] = "en-us,en;q=0.5";
                //gRequest.Connection = "keep-alive";

                gRequest.KeepAlive = true;

                gRequest.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;

                ///Set Proxy
                this.proxyAddress = proxyAddress;
                this.port = port;
                this.proxyUsername = proxyUsername;
                this.proxyPassword = proxyPassword;

                ChangeProxy(proxyAddress, port, proxyUsername, proxyPassword);

                gRequest.CookieContainer = new CookieContainer(); //gCookiesContainer;

                gRequest.Method = "GET";
                //gRequest.Accept = "image/jpeg, application/x-ms-application, image/gif, application/xaml+xml, image/pjpeg, application/x-ms-xbap, application/vnd.ms-excel, application/vnd.ms-powerpoint, application/msword, */*";
                gRequest.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;

                #region CookieManagment

                if (this.gCookies != null && this.gCookies.Count > 0)
                {
                    setExpect100Continue();
                    gRequest.CookieContainer.Add(gCookies);
                }
                //Get Response for this request url

                setExpect100Continue();
                gResponse = (HttpWebResponse)gRequest.GetResponse();

                //check if the status code is http 200 or http ok
                if (gResponse.StatusCode == HttpStatusCode.OK)
                {
                    //get all the cookies from the current request and add them to the response object cookies
                    setExpect100Continue();
                    gResponse.Cookies = gRequest.CookieContainer.GetCookies(gRequest.RequestUri);


                    //check if response object has any cookies or not
                    if (gResponse.Cookies.Count > 0)
                    {
                        //check if this is the first request/response, if this is the response of first request gCookies
                        //will be null
                        if (this.gCookies == null)
                        {
                            gCookies = gResponse.Cookies;
                        }
                        else
                        {
                            foreach (Cookie oRespCookie in gResponse.Cookies)
                            {
                                bool bMatch = false;
                                foreach (Cookie oReqCookie in this.gCookies)
                                {
                                    if (oReqCookie.Name == oRespCookie.Name)
                                    {
                                        oReqCookie.Value = oRespCookie.Value;
                                        bMatch = true;
                                        break; // 
                                    }
                                }
                                if (!bMatch)
                                    this.gCookies.Add(oRespCookie);
                            }
                        }
                    }
                #endregion

                    responseURI = gResponse.ResponseUri.AbsoluteUri;

                    StreamReader reader = new StreamReader(gResponse.GetResponseStream());
                    responseString = reader.ReadToEnd();
                    reader.Close();
                    return responseString;
                }
                else
                {
                    return "Error";
                }
            }
            catch
            {
            }
            return responseString;
        }

        public string getHtmlfromUrlProxy(Uri url, string Referes, string proxyAddress, string strport, string proxyUsername, string proxyPassword)
        {
            setExpect100Continue();
            gRequest = (HttpWebRequest)WebRequest.Create(url);
            //gRequest.UserAgent = "Mozilla/5.0 (Windows; U; Windows NT 6.1; en-US; rv:1.9.2.24) Gecko/20111103 Firefox/3.6.24";

            gRequest.UserAgent = "Mozilla/5.0 (Windows NT 6.1; rv:12.0) Gecko/20100101 Firefox/12.0";//"Mozilla/5.0 (Windows; U; Windows NT 6.1; en-US; rv:1.9.2.24) Gecko/20111103 Firefox/3.6.24";
            gRequest.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8";
            gRequest.Headers["Accept-Charset"] = "ISO-8859-1,utf-8;q=0.7,*;q=0.7";
            //gRequest.Headers["Cache-Control"] = "max-age=0";
            gRequest.Headers["Accept-Language"] = "en-us,en;q=0.5";
            //gRequest.Connection = "keep-alive";
            gRequest.Timeout = Timeout;

            gRequest.KeepAlive = true;

            gRequest.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;
            if (!string.IsNullOrEmpty(Referes))
            {
                gRequest.Referer = Referes;
            }
            ///Set Proxy
            this.proxyAddress = proxyAddress;
            if (Globals.IdCheck.IsMatch(strport) && !string.IsNullOrEmpty(strport))
            {
                this.port = int.Parse(strport);
            }
            this.proxyUsername = proxyUsername;
            this.proxyPassword = proxyPassword;

            ChangeProxy(proxyAddress, port, proxyUsername, proxyPassword);

            gRequest.CookieContainer = new CookieContainer(); //gCookiesContainer;

            gRequest.Method = "GET";
            //gRequest.Accept = "image/jpeg, application/x-ms-application, image/gif, application/xaml+xml, image/pjpeg, application/x-ms-xbap, application/vnd.ms-excel, application/vnd.ms-powerpoint, application/msword, */*";
            gRequest.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;

            #region CookieManagment

            if (this.gCookies != null && this.gCookies.Count > 0)
            {
                setExpect100Continue();
                gRequest.CookieContainer.Add(gCookies);

                //try
                //{
                //    gRequest.CookieContainer.Add(url, new Cookie("__qca", "P0-2078004405-1321685323158", "/"));
                //    gRequest.CookieContainer.Add(url, new Cookie("__utma", "101828306.1814567160.1321685324.1321697619.1321858563.3", "/"));
                //    gRequest.CookieContainer.Add(url, new Cookie("__utmz", "101828306.1321685324.1.1.utmcsr=(direct)|utmccn=(direct)|utmcmd=(none)", "/"));
                //    gRequest.CookieContainer.Add(url, new Cookie("__utmb", "101828306.2.10.1321858563", "/"));
                //    gRequest.CookieContainer.Add(url, new Cookie("__utmc", "101828306", "/"));
                //}
                //catch (Exception ex)
                //{

                //}
            }
            //Get Response for this request url

            setExpect100Continue();
            try
            {
                gResponse = (HttpWebResponse)gRequest.GetResponse();
            }
            catch { };

            //check if the status code is http 200 or http ok
            if (gResponse.StatusCode == HttpStatusCode.OK)
            {
                //get all the cookies from the current request and add them to the response object cookies
                setExpect100Continue();
                gResponse.Cookies = gRequest.CookieContainer.GetCookies(gRequest.RequestUri);


                //check if response object has any cookies or not
                if (gResponse.Cookies.Count > 0)
                {
                    //check if this is the first request/response, if this is the response of first request gCookies
                    //will be null
                    if (this.gCookies == null)
                    {
                        gCookies = gResponse.Cookies;
                    }
                    else
                    {
                        foreach (Cookie oRespCookie in gResponse.Cookies)
                        {
                            bool bMatch = false;
                            foreach (Cookie oReqCookie in this.gCookies)
                            {
                                if (oReqCookie.Name == oRespCookie.Name)
                                {
                                    oReqCookie.Value = oRespCookie.Value;
                                    bMatch = true;
                                    break; // 
                                }
                            }
                            if (!bMatch)
                                this.gCookies.Add(oRespCookie);
                        }
                    }
                }
            #endregion

                using (StreamReader reader = new StreamReader(gResponse.GetResponseStream()))
                {
                    string responseString = reader.ReadToEnd();
                    return responseString;
                }
            }
            else
            {
                return "Error";
            }

        }

        public string getHtmlfromUrlProxy(Uri url, string proxyAddress, string strport, string proxyUsername, string proxyPassword, string Referes, string Token)
        {
            setExpect100Continue();
            gRequest = (HttpWebRequest)WebRequest.Create(url);
            //gRequest.UserAgent = "Mozilla/5.0 (Windows; U; Windows NT 6.1; en-US; rv:1.9.2.24) Gecko/20111103 Firefox/3.6.24";

            gRequest.UserAgent = UserAgent;// "Mozilla/5.0 (Windows; U; Windows NT 6.1; en-US; rv:1.9.2.24) Gecko/20111103 Firefox/3.6.24";
            gRequest.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8";
            gRequest.Headers["Accept-Charset"] = "ISO-8859-1,utf-8;q=0.7,*;q=0.7";
            //gRequest.Headers["Cache-Control"] = "max-age=0";
            gRequest.Headers["Accept-Language"] = "en-us,en;q=0.5";
            //gRequest.Connection = "keep-alive";]
            gRequest.Headers.Add("Javascript-enabled", "true");
            gRequest.Timeout = Timeout;

            if (!string.IsNullOrEmpty(Referes))
            {
                gRequest.Referer = Referes;
            }
            if (!string.IsNullOrEmpty(Token))
            {
                gRequest.Headers.Add("X-CSRFToken", Token);
                gRequest.Headers.Add("X-Requested-With", "XMLHttpRequest");
            }

            gRequest.KeepAlive = true;

            gRequest.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;

            ///Set Proxy
            this.proxyAddress = proxyAddress;
            if (Globals.IdCheck.IsMatch(strport) && !string.IsNullOrEmpty(strport))
            {
                this.port = int.Parse(strport);
            }
            this.proxyUsername = proxyUsername;
            this.proxyPassword = proxyPassword;

            ChangeProxy(proxyAddress, port, proxyUsername, proxyPassword);

            gRequest.CookieContainer = new CookieContainer(); //gCookiesContainer;

            gRequest.Method = "GET";
            //gRequest.Accept = "image/jpeg, application/x-ms-application, image/gif, application/xaml+xml, image/pjpeg, application/x-ms-xbap, application/vnd.ms-excel, application/vnd.ms-powerpoint, application/msword, */*";
            gRequest.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;

            #region CookieManagment

            if (this.gCookies != null && this.gCookies.Count > 0)
            {
                setExpect100Continue();
                gRequest.CookieContainer.Add(gCookies);

                //try
                //{
                //    gRequest.CookieContainer.Add(url, new Cookie("__qca", "P0-2078004405-1321685323158", "/"));
                //    gRequest.CookieContainer.Add(url, new Cookie("__utma", "101828306.1814567160.1321685324.1321697619.1321858563.3", "/"));
                //    gRequest.CookieContainer.Add(url, new Cookie("__utmz", "101828306.1321685324.1.1.utmcsr=(direct)|utmccn=(direct)|utmcmd=(none)", "/"));
                //    gRequest.CookieContainer.Add(url, new Cookie("__utmb", "101828306.2.10.1321858563", "/"));
                //    gRequest.CookieContainer.Add(url, new Cookie("__utmc", "101828306", "/"));
                //}
                //catch (Exception ex)
                //{

                //}
            }
            //Get Response for this request url

            setExpect100Continue();
            gResponse = (HttpWebResponse)gRequest.GetResponse();

            //check if the status code is http 200 or http ok
            if (gResponse.StatusCode == HttpStatusCode.OK)
            {
                //get all the cookies from the current request and add them to the response object cookies
                setExpect100Continue();
                gResponse.Cookies = gRequest.CookieContainer.GetCookies(gRequest.RequestUri);


                //check if response object has any cookies or not
                if (gResponse.Cookies.Count > 0)
                {
                    //check if this is the first request/response, if this is the response of first request gCookies
                    //will be null
                    if (this.gCookies == null)
                    {
                        gCookies = gResponse.Cookies;
                    }
                    else
                    {
                        foreach (Cookie oRespCookie in gResponse.Cookies)
                        {
                            bool bMatch = false;
                            foreach (Cookie oReqCookie in this.gCookies)
                            {
                                if (oReqCookie.Name == oRespCookie.Name)
                                {
                                    oReqCookie.Value = oRespCookie.Value;
                                    bMatch = true;
                                    break; // 
                                }
                            }
                            if (!bMatch)
                                this.gCookies.Add(oRespCookie);
                        }
                    }
                }
            #endregion

                using (StreamReader reader = new StreamReader(gResponse.GetResponseStream()))
                {
                    string responseString = reader.ReadToEnd();
                    return responseString;
                }
            }
            else
            {
                return "Error";
            }

        }

        public string getHtmlfromUrlProxy(Uri url, string proxyAddress, string strport, string proxyUsername, string proxyPassword, string Referes, string Token, string UserAgent)
        {
            setExpect100Continue();
            gRequest = (HttpWebRequest)WebRequest.Create(url);
            //gRequest.UserAgent = "Mozilla/5.0 (Windows; U; Windows NT 6.1; en-US; rv:1.9.2.24) Gecko/20111103 Firefox/3.6.24";

            gRequest.UserAgent = "Mozilla/5.0 (Windows; U; Windows NT 6.1; en-US; rv:1.9.2.24) Gecko/20111103 Firefox/3.6.24";//"Mozilla/4.0 (compatible; MSIE 8.0; Windows NT 6.1; WOW64; Trident/4.0; SLCC2; .NET CLR 2.0.50727; .NET CLR 3.5.30729; .NET CLR 3.0.30729; Media Center PC 6.0; .NET4.0C; .NET4.0E; InfoPath.3)";;
            if (!string.IsNullOrEmpty(UserAgent))
            {
                gRequest.UserAgent = UserAgent;
            }
            gRequest.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8";
            gRequest.Headers["Accept-Charset"] = "ISO-8859-1,utf-8;q=0.7,*;q=0.7";
            //gRequest.Headers["Cache-Control"] = "max-age=0";
            gRequest.Headers["Accept-Language"] = "en-us,en;q=0.5";
            //gRequest.Connection = "keep-alive";
            gRequest.Timeout = Timeout;

            if (!string.IsNullOrEmpty(Referes))
            {
                gRequest.Referer = Referes;
            }
            if (!string.IsNullOrEmpty(Token))
            {
                gRequest.Headers.Add("X-CSRFToken", Token);
                gRequest.Headers.Add("X-Requested-With", "XMLHttpRequest");
            }

            gRequest.KeepAlive = true;

            gRequest.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;

            ///Set Proxy
            this.proxyAddress = proxyAddress;
            if (Globals.IdCheck.IsMatch(strport) && !string.IsNullOrEmpty(strport))
            {
                this.port = int.Parse(strport);
            }
            this.proxyUsername = proxyUsername;
            this.proxyPassword = proxyPassword;

            ChangeProxy(proxyAddress, port, proxyUsername, proxyPassword);

            gRequest.CookieContainer = new CookieContainer(); //gCookiesContainer;

            gRequest.Method = "GET";
            //gRequest.Accept = "image/jpeg, application/x-ms-application, image/gif, application/xaml+xml, image/pjpeg, application/x-ms-xbap, application/vnd.ms-excel, application/vnd.ms-powerpoint, application/msword, */*";
            gRequest.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;

            #region CookieManagment

            if (this.gCookies != null && this.gCookies.Count > 0)
            {
                setExpect100Continue();
                gRequest.CookieContainer.Add(gCookies);

                //try
                //{
                //    gRequest.CookieContainer.Add(url, new Cookie("__qca", "P0-2078004405-1321685323158", "/"));
                //    gRequest.CookieContainer.Add(url, new Cookie("__utma", "101828306.1814567160.1321685324.1321697619.1321858563.3", "/"));
                //    gRequest.CookieContainer.Add(url, new Cookie("__utmz", "101828306.1321685324.1.1.utmcsr=(direct)|utmccn=(direct)|utmcmd=(none)", "/"));
                //    gRequest.CookieContainer.Add(url, new Cookie("__utmb", "101828306.2.10.1321858563", "/"));
                //    gRequest.CookieContainer.Add(url, new Cookie("__utmc", "101828306", "/"));
                //}
                //catch (Exception ex)
                //{

                //}
            }
            //Get Response for this request url

            setExpect100Continue();
            try
            {
                gResponse = (HttpWebResponse)gRequest.GetResponse();
            }
            catch { };

            //check if the status code is http 200 or http ok
            if (gResponse.StatusCode == HttpStatusCode.OK)
            {
                //get all the cookies from the current request and add them to the response object cookies
                setExpect100Continue();
                gResponse.Cookies = gRequest.CookieContainer.GetCookies(gRequest.RequestUri);


                //check if response object has any cookies or not
                if (gResponse.Cookies.Count > 0)
                {
                    //check if this is the first request/response, if this is the response of first request gCookies
                    //will be null
                    if (this.gCookies == null)
                    {
                        gCookies = gResponse.Cookies;
                    }
                    else
                    {
                        foreach (Cookie oRespCookie in gResponse.Cookies)
                        {
                            bool bMatch = false;
                            foreach (Cookie oReqCookie in this.gCookies)
                            {
                                if (oReqCookie.Name == oRespCookie.Name)
                                {
                                    oReqCookie.Value = oRespCookie.Value;
                                    bMatch = true;
                                    break; // 
                                }
                            }
                            if (!bMatch)
                                this.gCookies.Add(oRespCookie);
                        }
                    }
                }
            #endregion

                using (StreamReader reader = new StreamReader(gResponse.GetResponseStream()))
                {
                    string responseString = reader.ReadToEnd();
                    return responseString;
                }
            }
            else
            {
                return "Error";
            }

        }

        public string getHtmlfromAsx(Uri url)
        {
            setExpect100Continue();
            gRequest = (HttpWebRequest)WebRequest.Create(url);
            gRequest.UserAgent = UserAgent;
            gRequest.CookieContainer = new CookieContainer();//new CookieContainer();
            gRequest.ContentType = "video/x-ms-asf";

            if (this.gCookies != null && this.gCookies.Count > 0)
            {
                gRequest.CookieContainer.Add(gCookies);
                setExpect100Continue();
            }
            //Get Response for this request url
            gResponse = (HttpWebResponse)gRequest.GetResponse();
            setExpect100Continue();
            //check if the status code is http 200 or http ok
            if (gResponse.StatusCode == HttpStatusCode.OK)
            {
                //get all the cookies from the current request and add them to the response object cookies
                gResponse.Cookies = gRequest.CookieContainer.GetCookies(gRequest.RequestUri);
                setExpect100Continue();
                //check if response object has any cookies or not
                if (gResponse.Cookies.Count > 0)
                {
                    //check if this is the first request/response, if this is the response of first request gCookies
                    //will be null
                    if (this.gCookies == null)
                    {
                        gCookies = gResponse.Cookies;
                    }
                    else
                    {
                        foreach (Cookie oRespCookie in gResponse.Cookies)
                        {
                            bool bMatch = false;
                            foreach (Cookie oReqCookie in this.gCookies)
                            {
                                if (oReqCookie.Name == oRespCookie.Name)
                                {
                                    oReqCookie.Value = oRespCookie.Value;
                                    bMatch = true;
                                    break; // 
                                }
                            }
                            if (!bMatch)
                                this.gCookies.Add(oRespCookie);
                        }
                    }
                }

                using (StreamReader reader = new StreamReader(gResponse.GetResponseStream()))
                {
                    string responseString = reader.ReadToEnd();
                    return responseString;
                }
            }
            else
            {
                return "Error";
            }
        }

       

        public string HttpUploadFile(string url, string file, string paramName, string contentType, NameValueCollection nvc, bool IsLocalFile, ref string status)
        {
            //ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };

            ////log.Debug(string.Format("Uploading {0} to {1}", file, url));
            //string boundary = "---------------------------" + DateTime.Now.Ticks.ToString();
            string boundary = "---------------------------" + DateTime.Now.Ticks.ToString();
            //string boundary = "---------------------------" + DateTime.Now.Ticks.ToString();
            byte[] boundarybytes = System.Text.Encoding.ASCII.GetBytes("\r\n--" + boundary + "\r\n");

            gRequest = (HttpWebRequest)WebRequest.Create("https://twitter.com/settings/profile");

            gRequest.UserAgent = UserAgent;
            gRequest.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8";
            //gRequest.Headers["Accept-Charset"] = "ISO-8859-1,utf-8;q=0.7,*;q=0.7";
            gRequest.Headers["Accept-Language"] = "en-us,en;q=0.5";

            gRequest.KeepAlive = true;

            //gRequest.AllowAutoRedirect = false;

            gRequest.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;

            string tempBoundary = boundary + "\r\n";
            byte[] tempBoundarybytes = System.Text.Encoding.ASCII.GetBytes("--" + boundary + "\r\n");

            //gRequest.ContentType = "multipart/form-data; boundary=" + boundary;
            gRequest.ContentType = "multipart/form-data; boundary=" + tempBoundary;
            gRequest.Method = "POST";
            gRequest.Credentials = System.Net.CredentialCache.DefaultCredentials;

            gRequest.Referer = "https://twitter.com/settings/profile";

            //ChangeProxy(proxyAddress, port, proxyUsername, proxyPassword);

            ChangeProxy(proxyAddress, port, proxyUsername, proxyPassword);

            gRequest.CookieContainer = new CookieContainer(); //gCookiesContainer;

            #region CookieManagment

            if (this.gCookies != null && this.gCookies.Count > 0)
            {
                gRequest.CookieContainer.Add(gCookies);

                //gRequest.CookieContainer.Add(new Cookie("__utma", "43838368.370306257.1336542481.1336542481.1336542481.1", "/", "twitter.com"));
                //gRequest.CookieContainer.Add(new Cookie("__utmb", "43838368.25.10.1336542481", "/", "twitter.com"));
                //gRequest.CookieContainer.Add(new Cookie("__utmc", "43838368", "/", "twitter.com"));
                //gRequest.CookieContainer.Add(new Cookie("__utmz", "43838368.1336542481.1.1.utmcsr=(direct)|utmccn=(direct)|utmcmd=(none)", "/", "twitter.com"));
            }
            #endregion

            using (Stream rs = gRequest.GetRequestStream())
            {
                string formdataTemplate = "Content-Disposition: form-data; name=\"{0}\"\r\n\r\n{1}";
                int temp = 0;
                foreach (string key in nvc.Keys)
                {
                    if (temp == 0)
                    {
                        rs.Write(tempBoundarybytes, 0, tempBoundarybytes.Length);
                        temp++;
                    }
                    else
                    {
                        rs.Write(boundarybytes, 0, boundarybytes.Length);
                    }
                    string formitem = string.Format(formdataTemplate, key, nvc[key]);
                    byte[] formitembytes = System.Text.Encoding.UTF8.GetBytes(formitem);
                    rs.Write(formitembytes, 0, formitembytes.Length);
                }
                rs.Write(boundarybytes, 0, boundarybytes.Length);


                if (IsLocalFile)
                {
                    string headerTemplate = "Content-Disposition: form-data; name=\"{0}\"; filename=\"" + file + "\"\r\nContent-Type: {2}\r\n\r\n";
                    string header = string.Format(headerTemplate, "profile_image[uploaded_data]", file, contentType);
                    byte[] headerbytes = System.Text.Encoding.UTF8.GetBytes(header);
                    rs.Write(headerbytes, 0, headerbytes.Length);

                    if (!string.IsNullOrEmpty(file))
                    {
                        FileStream fileStream = new FileStream(file, FileMode.Open, FileAccess.Read);
                        byte[] buffer = new byte[4096];
                        int bytesRead = 0;
                        while ((bytesRead = fileStream.Read(buffer, 0, buffer.Length)) != 0)
                        {
                            rs.Write(buffer, 0, bytesRead);
                        }
                        fileStream.Close();
                    }
                }

                byte[] trailer = System.Text.Encoding.ASCII.GetBytes("\r\n--" + boundary + "--\r\n");
                rs.Write(trailer, 0, trailer.Length);
            }

            #region CookieManagment

            if (this.gCookies != null && this.gCookies.Count > 0)
            {
                gRequest.CookieContainer.Add(gCookies);
            }

            #endregion

            WebResponse wresp = null;
            try
            {
                //wresp = gRequest.GetResponse();
                gResponse = (HttpWebResponse)gRequest.GetResponse();
                using (StreamReader reader = new StreamReader(gResponse.GetResponseStream()))
                {
                    string responseString = reader.ReadToEnd();
                    status = "okay";
                    return responseString;
                }

            }
            catch (Exception ex)
            {
                //log.Error("Error uploading file", ex);
                if (wresp != null)
                {
                    wresp.Close();
                    wresp = null;
                }
                return null;
            }
            finally
            {
                gRequest = null;
            }
            //}

        }

        public string HttpUploadFileBackground(string url, string file, string paramName, string contentType, NameValueCollection nvc, bool IsLocalFile, ref string status)
        {
            //ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };

            ////log.Debug(string.Format("Uploading {0} to {1}", file, url));
            //string boundary = "---------------------------" + DateTime.Now.Ticks.ToString();
            string boundary = "---------------------------" + DateTime.Now.Ticks.ToString();
            //string boundary = "---------------------------" + DateTime.Now.Ticks.ToString();
            byte[] boundarybytes = System.Text.Encoding.ASCII.GetBytes("\r\n--" + boundary + "\r\n");

            gRequest = (HttpWebRequest)WebRequest.Create("https://twitter.com/settings/design/update");

            gRequest.UserAgent = UserAgent;
            gRequest.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8";
            //gRequest.Headers["Accept-Charset"] = "ISO-8859-1,utf-8;q=0.7,*;q=0.7";
            gRequest.Headers["Accept-Language"] = "en-us,en;q=0.5";

            gRequest.KeepAlive = true;

            //gRequest.AllowAutoRedirect = false;

            gRequest.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;

            string tempBoundary = boundary + "\r\n";
            byte[] tempBoundarybytes = System.Text.Encoding.ASCII.GetBytes("--" + boundary + "\r\n");

            //gRequest.ContentType = "multipart/form-data; boundary=" + boundary;
            gRequest.ContentType = "multipart/form-data; boundary=" + tempBoundary;
            gRequest.Method = "POST";
            gRequest.Credentials = System.Net.CredentialCache.DefaultCredentials;

            gRequest.Referer = "https://twitter.com/settings/design/update";

            //ChangeProxy(proxyAddress, port, proxyUsername, proxyPassword);

            ChangeProxy(proxyAddress, port, proxyUsername, proxyPassword);

            gRequest.CookieContainer = new CookieContainer(); //gCookiesContainer;

            #region CookieManagment

            if (this.gCookies != null && this.gCookies.Count > 0)
            {
                gRequest.CookieContainer.Add(gCookies);

                //gRequest.CookieContainer.Add(new Cookie("__utma", "43838368.370306257.1336542481.1336542481.1336542481.1", "/", "twitter.com"));
                //gRequest.CookieContainer.Add(new Cookie("__utmb", "43838368.25.10.1336542481", "/", "twitter.com"));
                //gRequest.CookieContainer.Add(new Cookie("__utmc", "43838368", "/", "twitter.com"));
                //gRequest.CookieContainer.Add(new Cookie("__utmz", "43838368.1336542481.1.1.utmcsr=(direct)|utmccn=(direct)|utmcmd=(none)", "/", "twitter.com"));
            }
            #endregion

            using (Stream rs = gRequest.GetRequestStream())
            {
                string formdataTemplate = "Content-Disposition: form-data; name=\"{0}\"\r\n\r\n{1}";
                int temp = 0;
                foreach (string key in nvc.Keys)
                {
                    if (temp == 0)
                    {
                        rs.Write(tempBoundarybytes, 0, tempBoundarybytes.Length);
                        temp++;
                    }
                    else
                    {
                        rs.Write(boundarybytes, 0, boundarybytes.Length);
                    }
                    string formitem = string.Format(formdataTemplate, key, nvc[key]);
                    byte[] formitembytes = System.Text.Encoding.UTF8.GetBytes(formitem);
                    rs.Write(formitembytes, 0, formitembytes.Length);
                }
                rs.Write(boundarybytes, 0, boundarybytes.Length);



                if (IsLocalFile)
                {
                    string headerTemplate = "Content-Disposition: form-data; name=\"{0}\"; filename=\"" + file + "\"\r\nContent-Type: {2}\r\n\r\n";
                    string header = string.Format(headerTemplate, "media_data[]", file, contentType);
                    byte[] headerbytes = System.Text.Encoding.UTF8.GetBytes(header);
                    rs.Write(headerbytes, 0, headerbytes.Length);

                    if (!string.IsNullOrEmpty(file))
                    {
                        FileStream fileStream = new FileStream(file, FileMode.Open, FileAccess.Read);
                        byte[] buffer = new byte[4096];
                        int bytesRead = 0;
                        while ((bytesRead = fileStream.Read(buffer, 0, buffer.Length)) != 0)
                        {
                            rs.Write(buffer, 0, bytesRead);
                        }
                        fileStream.Close();
                    }
                }

                byte[] trailer = System.Text.Encoding.ASCII.GetBytes("\r\n--" + boundary + "--\r\n");
                rs.Write(trailer, 0, trailer.Length);
            }

            #region CookieManagment

            if (this.gCookies != null && this.gCookies.Count > 0)
            {
                gRequest.CookieContainer.Add(gCookies);
            }

            #endregion

            WebResponse wresp = null;
            try
            {
                //wresp = gRequest.GetResponse();
                gResponse = (HttpWebResponse)gRequest.GetResponse();
                using (StreamReader reader = new StreamReader(gResponse.GetResponseStream()))
                {
                    string responseString = reader.ReadToEnd();
                    status = "okay";
                    return responseString;
                }
            }
            catch (Exception ex)
            {
                //log.Error("Error uploading file", ex);
                if (wresp != null)
                {
                    wresp.Close();
                    wresp = null;
                }
                return null;
            }
            finally
            {
                gRequest = null;
            }
            //}

        }


        public string postFormData(Uri formActionUrl, string postData)
        {
            // postData="charset_test=%E2%82%AC%2C%C2%B4%2C%E2%82%AC%2C%C2%B4%2C%E6%B0%B4%2C%D0%94%2C%D0%84&lsd=AVqEAf6F&locale=en_US&email=soni.sameer123%40rediffmail.com&pass=god@12345&persistent=1&default_persistent=1&charset_test=%E2%82%AC%2C%C2%B4%2C%E2%82%AC%2C%C2%B4%2C%E6%B0%B4%2C%D0%94%2C%D0%84&lsd=AVqEAf6F";

            gRequest = (HttpWebRequest)WebRequest.Create(formActionUrl);
            // gRequest.UserAgent = "User-Agent: Mozilla/5.0 (Windows; U; Windows NT 6.1; en-US; rv:1.9.2.16) Gecko/20110319 Firefox/3.6.16";
            //"Mozilla/5.0 (Windows NT 6.1; rv:20.0) Gecko/20100101 Firefox/20.0"
            // gRequest.UserAgent = "Mozilla/5.0 (Windows NT 6.1; rv:20.0) Gecko/20100101 Firefox/20.0";
            gRequest.UserAgent = "Mozilla/5.0 (Windows NT 6.1; rv:22.0) Gecko/20100101 Firefox/22.0";
            gRequest.CookieContainer = new CookieContainer();// gCookiesContainer;
            gRequest.Method = "POST";
            gRequest.Accept = " text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8, */*";
            gRequest.KeepAlive = true;
            gRequest.ContentType = @"application/x-www-form-urlencoded";
            //gRequest.Timeout = 2 * 30000;
            // gRequest.Referer = "https://www.facebook.com/checkpoint/";

            ChangeProxy(proxyAddress, port, proxyUsername, proxyPassword);

            #region CookieManagement
            if (this.gCookies != null && this.gCookies.Count > 0)
            {
                setExpect100Continue();
                gRequest.CookieContainer.Add(gCookies);
            }

            //logic to postdata to the form
            try
            {
                setExpect100Continue();
                string postdata = string.Format(postData);
                byte[] postBuffer = System.Text.Encoding.GetEncoding(1252).GetBytes(postData);
                gRequest.ContentLength = postBuffer.Length;
                Stream postDataStream = gRequest.GetRequestStream();
                postDataStream.Write(postBuffer, 0, postBuffer.Length);
                postDataStream.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                // Logger.LogText("Internet Connectivity Exception : "+ ex.Message,null);
            }
            //post data logic ends

            //Get Response for this request url
            try
            {
                gResponse = (HttpWebResponse)gRequest.GetResponse();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                //Logger.LogText("Response from "+formActionUrl + ":" + ex.Message,null);
            }



            //check if the status code is http 200 or http ok

            if (gResponse.StatusCode == HttpStatusCode.OK)
            {
                //get all the cookies from the current request and add them to the response object cookies
                setExpect100Continue();
                gResponse.Cookies = gRequest.CookieContainer.GetCookies(gRequest.RequestUri);

                if (gResponse.Cookies.Count > 0)
                {
                    //check if this is the first request/response, if this is the response of first request gCookies
                    //will be null
                    if (this.gCookies == null)
                    {
                        gCookies = gResponse.Cookies;
                    }
                    else
                    {
                        foreach (Cookie oRespCookie in gResponse.Cookies)
                        {
                            bool bMatch = false;
                            foreach (Cookie oReqCookie in this.gCookies)
                            {
                                if (oReqCookie.Name == oRespCookie.Name)
                                {
                                    oReqCookie.Value = oRespCookie.Value;
                                    bMatch = true;
                                    break; // 
                                }
                            }
                            if (!bMatch)
                                this.gCookies.Add(oRespCookie);
                        }
                    }
                }
            #endregion



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




        public string postFormDataWithCookie(Uri formActionUrl, string postData, string regCoockieval)
        {
            // postData="charset_test=%E2%82%AC%2C%C2%B4%2C%E2%82%AC%2C%C2%B4%2C%E6%B0%B4%2C%D0%94%2C%D0%84&lsd=AVqEAf6F&locale=en_US&email=soni.sameer123%40rediffmail.com&pass=god@12345&persistent=1&default_persistent=1&charset_test=%E2%82%AC%2C%C2%B4%2C%E2%82%AC%2C%C2%B4%2C%E6%B0%B4%2C%D0%94%2C%D0%84&lsd=AVqEAf6F";

            gRequest = (HttpWebRequest)WebRequest.Create(formActionUrl);
            // gRequest.UserAgent = "User-Agent: Mozilla/5.0 (Windows; U; Windows NT 6.1; en-US; rv:1.9.2.16) Gecko/20110319 Firefox/3.6.16";
            //"Mozilla/5.0 (Windows NT 6.1; rv:20.0) Gecko/20100101 Firefox/20.0"
            // gRequest.UserAgent = "Mozilla/5.0 (Windows NT 6.1; rv:20.0) Gecko/20100101 Firefox/20.0";
            gRequest.UserAgent = "Mozilla/5.0 (Windows NT 6.1; rv:22.0) Gecko/20100101 Firefox/22.0";
            gRequest.CookieContainer = new CookieContainer();// gCookiesContainer;
            gRequest.Method = "POST";
            gRequest.Accept = " text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8, */*";
            gRequest.KeepAlive = true;
            gRequest.ContentType = @"application/x-www-form-urlencoded";
            //gRequest.Timeout = 2 * 30000;
            // gRequest.Referer = "https://www.facebook.com/checkpoint/";

            ChangeProxy(proxyAddress, port, proxyUsername, proxyPassword);


            try
            {
                Cookie ck = new Cookie();
                ck.Name = "_js_datr";
                ck.Value = regCoockieval;
                ck.Domain = "facebook.com";
                gRequest.CookieContainer.Add(ck);
            }
            catch { };




            #region CookieManagement
            if (this.gCookies != null && this.gCookies.Count > 0)
            {
                setExpect100Continue();
                gRequest.CookieContainer.Add(gCookies);
            }

            //logic to postdata to the form
            try
            {
                setExpect100Continue();
                string postdata = string.Format(postData);
                byte[] postBuffer = System.Text.Encoding.GetEncoding(1252).GetBytes(postData);
                gRequest.ContentLength = postBuffer.Length;
                Stream postDataStream = gRequest.GetRequestStream();
                postDataStream.Write(postBuffer, 0, postBuffer.Length);
                postDataStream.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                // Logger.LogText("Internet Connectivity Exception : "+ ex.Message,null);
            }
            //post data logic ends

            //Get Response for this request url
            try
            {
                gResponse = (HttpWebResponse)gRequest.GetResponse();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                //Logger.LogText("Response from "+formActionUrl + ":" + ex.Message,null);
            }



            //check if the status code is http 200 or http ok

            if (gResponse.StatusCode == HttpStatusCode.OK)
            {
                //get all the cookies from the current request and add them to the response object cookies
                setExpect100Continue();
                gResponse.Cookies = gRequest.CookieContainer.GetCookies(gRequest.RequestUri);

                if (gResponse.Cookies.Count > 0)
                {
                    //check if this is the first request/response, if this is the response of first request gCookies
                    //will be null
                    if (this.gCookies == null)
                    {
                        gCookies = gResponse.Cookies;
                    }
                    else
                    {
                        foreach (Cookie oRespCookie in gResponse.Cookies)
                        {
                            bool bMatch = false;
                            foreach (Cookie oReqCookie in this.gCookies)
                            {
                                if (oReqCookie.Name == oRespCookie.Name)
                                {
                                    oReqCookie.Value = oRespCookie.Value;
                                    bMatch = true;
                                    break; // 
                                }
                            }
                            if (!bMatch)
                                this.gCookies.Add(oRespCookie);
                        }
                    }
                }
            #endregion



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







        public static string Get_fb_dtsg(string pgSrc)
        {
            string fb_dtsg = GlobusHttpHelper.GetParamValue(pgSrc, "fb_dtsg");//pageSourceHome.Substring(pageSourceHome.IndexOf("fb_dtsg") + 16, 8);
            if (string.IsNullOrEmpty(fb_dtsg))
            {
                fb_dtsg = GlobusHttpHelper.ParseJson(pgSrc, "fb_dtsg");
                if (fb_dtsg.Contains("value=\\"))
                {
                    if (pgSrc.Contains("fb_dtsg"))
                    {
                        string[] Arr = System.Text.RegularExpressions.Regex.Split(pgSrc, "fb_dtsg");
                        fb_dtsg =Utils.getBetween(Arr[1], "value=\\\"", "\\\"");
                    }

                }
            }
            return fb_dtsg;
        }

        public string postFormData(Uri formActionUrl, string postData, string Referes, string Token, string XRequestedWith, string XPhx, string Origin)
        {

            gRequest = (HttpWebRequest)WebRequest.Create(formActionUrl);
            gRequest.UserAgent = UserAgent;//"User-Agent: Mozilla/5.0 (Windows; U; Windows NT 6.1; en-US; rv:1.9.2.16) Gecko/20110319 Firefox/3.6.16";

            gRequest.CookieContainer = new CookieContainer();// gCookiesContainer;
            gRequest.Method = "POST";
            gRequest.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8, */*";


            //gRequest.Accept = "application/json, text/javascript, */*; q=0.01";

            //gRequest.ContentType= "application/x-www-form-urlencoded; charset=UTF-8";

            //gRequest.Headers["Accept-Language"] = "en-US,en;q=0.8";

            gRequest.KeepAlive = true;
            gRequest.ContentType = @"application/x-www-form-urlencoded";
            gRequest.Timeout = Timeout;

            gRequest.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;

            gRequest.Headers.Add("Javascript-enabled", "true");
            gRequest.Headers.Add("XRequestedWith", "XMLHttpRequest");

            if (!string.IsNullOrEmpty(Referes))
            {
                gRequest.Referer = Referes;
            }
            if (!string.IsNullOrEmpty(Token))
            {
                gRequest.Headers.Add("X-CSRFToken", Token);
            }
            if (!string.IsNullOrEmpty(XRequestedWith))
            {
                gRequest.Headers.Add("X-Requested-With", XRequestedWith);
            }
            if (!string.IsNullOrEmpty(XPhx))
            {
                gRequest.Headers.Add("X-PHX", XPhx);
            }
            if (!string.IsNullOrEmpty(Origin))
            {
                gRequest.Headers.Add("Origin", Origin);
            }

            ///Modified BySumit 18-11-2011
            ChangeProxy(proxyAddress, port, proxyUsername, proxyPassword);

            #region CookieManagement
            if (this.gCookies != null && this.gCookies.Count > 0)
            {
                setExpect100Continue();
                gRequest.CookieContainer.Add(gCookies);
            }

            //logic to postdata to the form
            try
            {
                setExpect100Continue();
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls;
                string postdata = string.Format(postData);
                byte[] postBuffer = System.Text.Encoding.GetEncoding(1252).GetBytes(postData);
                gRequest.ContentLength = postBuffer.Length;
                Stream postDataStream = gRequest.GetRequestStream();
                postDataStream.Write(postBuffer, 0, postBuffer.Length);
                postDataStream.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                // Logger.LogText("Internet Connectivity Exception : "+ ex.Message,null);
            }
            //post data logic ends

            //Get Response for this request url
            try
            {
                gResponse = (HttpWebResponse)gRequest.GetResponse();
            }
            catch (Exception ex)
            {
                //  Console.WriteLine(ex);
                //Logger.LogText("Response from "+formActionUrl + ":" + ex.Message,null);
            }

            //check if the status code is http 200 or http ok

            if (gResponse.StatusCode == HttpStatusCode.OK)
            {
                //get all the cookies from the current request and add them to the response object cookies
                setExpect100Continue();
                gResponse.Cookies = gRequest.CookieContainer.GetCookies(gRequest.RequestUri);
                //check if response object has any cookies or not
                //Added by sandeep pathak
                //gCookiesContainer = gRequest.CookieContainer;  

                if (gResponse.Cookies.Count > 0)
                {
                    //check if this is the first request/response, if this is the response of first request gCookies
                    //will be null
                    if (this.gCookies == null)
                    {
                        gCookies = gResponse.Cookies;
                    }
                    else
                    {
                        foreach (Cookie oRespCookie in gResponse.Cookies)
                        {
                            bool bMatch = false;
                            foreach (Cookie oReqCookie in this.gCookies)
                            {
                                if (oReqCookie.Name == oRespCookie.Name)
                                {
                                    oReqCookie.Value = oRespCookie.Value;
                                    bMatch = true;
                                    break; // 
                                }
                            }
                            if (!bMatch)
                                this.gCookies.Add(oRespCookie);
                        }
                    }
                }
            #endregion

                using (StreamReader reader = new StreamReader(gResponse.GetResponseStream()))
                {
                    string responseString = reader.ReadToEnd();
                    return responseString;
                }
            }
            else
            {
                return "Error in posting data";
            }
        }
        public string postFormDataUpdate(Uri formActionUrl, string postData)
        {
            // postData="charset_test=%E2%82%AC%2C%C2%B4%2C%E2%82%AC%2C%C2%B4%2C%E6%B0%B4%2C%D0%94%2C%D0%84&lsd=AVqEAf6F&locale=en_US&email=soni.sameer123%40rediffmail.com&pass=god@12345&persistent=1&default_persistent=1&charset_test=%E2%82%AC%2C%C2%B4%2C%E2%82%AC%2C%C2%B4%2C%E6%B0%B4%2C%D0%94%2C%D0%84&lsd=AVqEAf6F";

            gRequest = (HttpWebRequest)WebRequest.Create(formActionUrl);

            gRequest.UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64; rv:34.0) Gecko/20100101 Firefox/34.0";
            gRequest.CookieContainer = new CookieContainer();// gCookiesContainer;
            gRequest.Method = "POST";
            gRequest.Accept = " text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8, */*";
            gRequest.KeepAlive = true;
            gRequest.ContentType = @"application/x-www-form-urlencoded";
            // gRequest.Headers["Accept-Charset"] = "ISO-8859-1,utf-8;q=0.7,*;q=0.7";
            gRequest.Headers["Accept-Language"] = "en-us,en;q=0.5";



            ChangeProxy(proxyAddress, port, proxyUsername, proxyPassword);

            #region CookieManagement
            if (this.gCookies != null && this.gCookies.Count > 0)
            {
                setExpect100Continue();
                gRequest.CookieContainer.Add(gCookies);
            }

            //logic to postdata to the form
            try
            {
                setExpect100Continue();
                string postdata = string.Format(postData);
                byte[] postBuffer = System.Text.Encoding.GetEncoding(1252).GetBytes(postData);
                gRequest.ContentLength = postBuffer.Length;
                Stream postDataStream = gRequest.GetRequestStream();
                postDataStream.Write(postBuffer, 0, postBuffer.Length);
                postDataStream.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                // Logger.LogText("Internet Connectivity Exception : "+ ex.Message,null);
            }
            //post data logic ends

            //Get Response for this request url
            try
            {
                gResponse = (HttpWebResponse)gRequest.GetResponse();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                //Logger.LogText("Response from "+formActionUrl + ":" + ex.Message,null);
            }



            //check if the status code is http 200 or http ok

            if (gResponse.StatusCode == HttpStatusCode.OK)
            {
                //get all the cookies from the current request and add them to the response object cookies
                setExpect100Continue();
                gResponse.Cookies = gRequest.CookieContainer.GetCookies(gRequest.RequestUri);

                if (gResponse.Cookies.Count > 0)
                {
                    //check if this is the first request/response, if this is the response of first request gCookies
                    //will be null
                    if (this.gCookies == null)
                    {
                        gCookies = gResponse.Cookies;
                    }
                    else
                    {
                        foreach (Cookie oRespCookie in gResponse.Cookies)
                        {
                            bool bMatch = false;
                            foreach (Cookie oReqCookie in this.gCookies)
                            {
                                if (oReqCookie.Name == oRespCookie.Name)
                                {
                                    oReqCookie.Value = oRespCookie.Value;
                                    bMatch = true;
                                    break; // 
                                }
                            }
                            if (!bMatch)
                                this.gCookies.Add(oRespCookie);
                        }
                    }
                }
            #endregion



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



        public string postFormData1(Uri formActionUrl, string postData, string Referes, string Token, string XRequestedWith, string XPhx, string Origin)
        {

            gRequest = (HttpWebRequest)WebRequest.Create(formActionUrl);
            gRequest.UserAgent = UserAgent;//"User-Agent: Mozilla/5.0 (Windows; U; Windows NT 6.1; en-US; rv:1.9.2.16) Gecko/20110319 Firefox/3.6.16";

            gRequest.CookieContainer = new CookieContainer();// gCookiesContainer;
            gRequest.Method = "POST";
            gRequest.Accept = "application/json, text/javascript, */*; q=0.01";

            gRequest.Headers.Add("Accept-Language", "en-US,en;q=0.5");
            gRequest.KeepAlive = true;
            gRequest.ContentType = @"application/x-www-form-urlencoded; charset=UTF-8";
            gRequest.Timeout = Timeout;

            gRequest.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;


            //gRequest.Headers.Add("Javascript-enabled", "true");

            if (!string.IsNullOrEmpty(Referes))
            {
                gRequest.Referer = "https://twitter.com/settings/profile";
            }
            if (!string.IsNullOrEmpty(Token))
            {
                gRequest.Headers.Add("X-CSRFToken", Token);
            }
            if (!string.IsNullOrEmpty(XRequestedWith))
            {
                gRequest.Headers.Add("X-Requested-With", XRequestedWith);
            }
            if (!string.IsNullOrEmpty(XPhx))
            {
                gRequest.Headers.Add("X-PHX", XPhx);
            }
            if (!string.IsNullOrEmpty(Origin))
            {
                gRequest.Headers.Add("Origin", Origin);
            }

            ///Modified BySumit 18-11-2011
            ChangeProxy(proxyAddress, port, proxyUsername, proxyPassword);

            #region CookieManagement
            if (this.gCookies != null && this.gCookies.Count > 0)
            {
                setExpect100Continue();
                gRequest.CookieContainer.Add(gCookies);
            }

            //logic to postdata to the form
            try
            {
                setExpect100Continue();
                string postdata = string.Format(postData);
                byte[] postBuffer = System.Text.Encoding.GetEncoding(1252).GetBytes(postData);
                gRequest.ContentLength = postBuffer.Length;
                Stream postDataStream = gRequest.GetRequestStream();
                postDataStream.Write(postBuffer, 0, postBuffer.Length);
                postDataStream.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                // Logger.LogText("Internet Connectivity Exception : "+ ex.Message,null);
            }
            //post data logic ends

            //Get Response for this request url
            try
            {
                gResponse = (HttpWebResponse)gRequest.GetResponse();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                //Logger.LogText("Response from "+formActionUrl + ":" + ex.Message,null);
            }



            //check if the status code is http 200 or http ok

            if (gResponse.StatusCode == HttpStatusCode.OK)
            {
                //get all the cookies from the current request and add them to the response object cookies
                setExpect100Continue();
                gResponse.Cookies = gRequest.CookieContainer.GetCookies(gRequest.RequestUri);
                //check if response object has any cookies or not
                //Added by sandeep pathak
                //gCookiesContainer = gRequest.CookieContainer;  

                if (gResponse.Cookies.Count > 0)
                {
                    //check if this is the first request/response, if this is the response of first request gCookies
                    //will be null
                    if (this.gCookies == null)
                    {
                        gCookies = gResponse.Cookies;
                    }
                    else
                    {
                        foreach (Cookie oRespCookie in gResponse.Cookies)
                        {
                            bool bMatch = false;
                            foreach (Cookie oReqCookie in this.gCookies)
                            {
                                if (oReqCookie.Name == oRespCookie.Name)
                                {
                                    oReqCookie.Value = oRespCookie.Value;
                                    bMatch = true;
                                    break; // 
                                }
                            }
                            if (!bMatch)
                                this.gCookies.Add(oRespCookie);
                        }
                    }
                }
            #endregion


                using (StreamReader reader = new StreamReader(gResponse.GetResponseStream()))
                {
                    string responseString = reader.ReadToEnd();
                    return responseString;
                }

            }
            else
            {
                return "Error in posting data";
            }

        }

        public string postFormDataMobileData(Uri formActionUrl, string postData, string Referes, string Token, string XRequestedWith, string XPhx, string Origin, string UserAgent)
        {

            gRequest = (HttpWebRequest)WebRequest.Create(formActionUrl);

            gRequest.UserAgent = "Mozilla/5.0 (Windows NT 6.1; rv:12.0) Gecko/20100101 Firefox/12.0";//"User-Agent: Mozilla/5.0 (Windows; U; Windows NT 6.1; en-US; rv:1.9.2.16) Gecko/20110319 Firefox/3.6.16";

            if (!string.IsNullOrEmpty(UserAgent))
            {
                gRequest.UserAgent = UserAgent;
            }

            gRequest.CookieContainer = new CookieContainer();// gCookiesContainer;
            gRequest.Method = "POST";
            gRequest.Accept = " text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8, */*";
            gRequest.KeepAlive = true;
            gRequest.ContentType = @"application/x-www-form-urlencoded";
            gRequest.Timeout = Timeout;

            gRequest.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;

            if (!string.IsNullOrEmpty(Referes))
            {
                gRequest.Referer = Referes;
            }
            if (!string.IsNullOrEmpty(Token))
            {
                gRequest.Headers.Add("X-CSRFToken", Token);
            }
            if (!string.IsNullOrEmpty(XRequestedWith))
            {
                gRequest.Headers.Add("X-Requested-With", XRequestedWith);
            }
            if (!string.IsNullOrEmpty(XPhx))
            {
                gRequest.Headers.Add("X-PHX", XPhx);
            }
            if (!string.IsNullOrEmpty(Origin))
            {
                gRequest.Headers.Add("Origin", Origin);
            }

            ///Modified BySumit 18-11-2011
            ChangeProxy(proxyAddress, port, proxyUsername, proxyPassword);

            #region CookieManagement
            if (this.gCookies != null && this.gCookies.Count > 0)
            {
                setExpect100Continue();
                gRequest.CookieContainer.Add(gCookies);
            }

            //logic to postdata to the form
            try
            {
                setExpect100Continue();
                string postdata = string.Format(postData);
                byte[] postBuffer = System.Text.Encoding.GetEncoding(1252).GetBytes(postData);
                gRequest.ContentLength = postBuffer.Length;
                Stream postDataStream = gRequest.GetRequestStream();
                postDataStream.Write(postBuffer, 0, postBuffer.Length);
                postDataStream.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                // Logger.LogText("Internet Connectivity Exception : "+ ex.Message,null);
            }
            //post data logic ends

            //Get Response for this request url
            try
            {
                gResponse = (HttpWebResponse)gRequest.GetResponse();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                //Logger.LogText("Response from "+formActionUrl + ":" + ex.Message,null);
            }



            //check if the status code is http 200 or http ok

            if (gResponse.StatusCode == HttpStatusCode.OK)
            {
                //get all the cookies from the current request and add them to the response object cookies
                setExpect100Continue();
                gResponse.Cookies = gRequest.CookieContainer.GetCookies(gRequest.RequestUri);
                //check if response object has any cookies or not
                //Added by sandeep pathak
                //gCookiesContainer = gRequest.CookieContainer;  

                if (gResponse.Cookies.Count > 0)
                {
                    //check if this is the first request/response, if this is the response of first request gCookies
                    //will be null
                    if (this.gCookies == null)
                    {
                        gCookies = gResponse.Cookies;
                    }
                    else
                    {
                        foreach (Cookie oRespCookie in gResponse.Cookies)
                        {
                            bool bMatch = false;
                            foreach (Cookie oReqCookie in this.gCookies)
                            {
                                if (oReqCookie.Name == oRespCookie.Name)
                                {
                                    oReqCookie.Value = oRespCookie.Value;
                                    bMatch = true;
                                    break; // 
                                }
                            }
                            if (!bMatch)
                                this.gCookies.Add(oRespCookie);
                        }
                    }
                }
            #endregion



                using (StreamReader reader = new StreamReader(gResponse.GetResponseStream()))
                {
                    string responseString = reader.ReadToEnd();
                    return responseString;
                }

            }
            else
            {
                return "Error in posting data";
            }

        }

        public string postFormDataProxy(Uri formActionUrl, string postData, string Referes, string proxyAddress, int port, string proxyUsername, string proxyPassword)
        {

            gRequest = (HttpWebRequest)WebRequest.Create(formActionUrl);
            gRequest.UserAgent = UserAgent;

            gRequest.CookieContainer = new CookieContainer();// gCookiesContainer;
            gRequest.Method = "POST";
            gRequest.Accept = " text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8, */*";
            gRequest.KeepAlive = true;
            gRequest.ContentType = @"application/x-www-form-urlencoded";
            gRequest.Timeout = Timeout;

            gRequest.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;

            if (!string.IsNullOrEmpty(Referes))
            {
                gRequest.Referer = Referes;
            }
            ///Modified BySumit 18-11-2011
            ChangeProxy(proxyAddress, port, proxyUsername, proxyPassword);

            #region CookieManagement
            if (this.gCookies != null && this.gCookies.Count > 0)
            {
                setExpect100Continue();
                gRequest.CookieContainer.Add(gCookies);
            }

            //logic to postdata to the form
            try
            {
                setExpect100Continue();
                string postdata = string.Format(postData);
                byte[] postBuffer = System.Text.Encoding.GetEncoding(1252).GetBytes(postData);
                gRequest.ContentLength = postBuffer.Length;
                Stream postDataStream = gRequest.GetRequestStream();
                postDataStream.Write(postBuffer, 0, postBuffer.Length);
                postDataStream.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                // Logger.LogText("Internet Connectivity Exception : "+ ex.Message,null);
            }
            //post data logic ends

            //Get Response for this request url
            try
            {
                gResponse = (HttpWebResponse)gRequest.GetResponse();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                //Logger.LogText("Response from "+formActionUrl + ":" + ex.Message,null);
            }



            //check if the status code is http 200 or http ok

            if (gResponse.StatusCode == HttpStatusCode.OK)
            {
                //get all the cookies from the current request and add them to the response object cookies
                setExpect100Continue();
                gResponse.Cookies = gRequest.CookieContainer.GetCookies(gRequest.RequestUri);
                //check if response object has any cookies or not
                //Added by sandeep pathak
                //gCookiesContainer = gRequest.CookieContainer;  

                if (gResponse.Cookies.Count > 0)
                {
                    //check if this is the first request/response, if this is the response of first request gCookies
                    //will be null
                    if (this.gCookies == null)
                    {
                        gCookies = gResponse.Cookies;
                    }
                    else
                    {
                        foreach (Cookie oRespCookie in gResponse.Cookies)
                        {
                            bool bMatch = false;
                            foreach (Cookie oReqCookie in this.gCookies)
                            {
                                if (oReqCookie.Name == oRespCookie.Name)
                                {
                                    oReqCookie.Value = oRespCookie.Value;
                                    bMatch = true;
                                    break; // 
                                }
                            }
                            if (!bMatch)
                                this.gCookies.Add(oRespCookie);
                        }
                    }
                }
            #endregion

                using (StreamReader reader = new StreamReader(gResponse.GetResponseStream()))
                {
                    string responseString = reader.ReadToEnd();
                    return responseString;
                }

            }
            else
            {
                return "Error in posting data";
            }

        }

        public void setExpect100Continue()
        {
            if (ServicePointManager.Expect100Continue == true)
            {
                ServicePointManager.Expect100Continue = false;
            }
        }

        public void setExpect100ContinueToTrue()
        {
            if (ServicePointManager.Expect100Continue == false)
            {
                ServicePointManager.Expect100Continue = true;
            }
        }

        public void ChangeProxy(string proxyAddress, int port, string proxyUsername, string proxyPassword)
        {
            try
            {
                WebProxy myproxy = new WebProxy(proxyAddress, port);
                myproxy.BypassProxyOnLocal = false;

                if (!string.IsNullOrEmpty(proxyUsername) && !string.IsNullOrEmpty(proxyPassword))
                {
                    myproxy.Credentials = new NetworkCredential(proxyUsername, proxyPassword);
                }
                gRequest.Proxy = myproxy;
            }
            catch (Exception ex)
            {

            }

        }

       

        public static string ParseJson_DoubleQuotes(string data, string paramName)
        {
            int startIndx = data.IndexOf(paramName) + paramName.Length + 3;
            int endIndx = data.IndexOf("\"", startIndx);

            string value = data.Substring(startIndx, endIndx - startIndx).Replace(",", "");
            return value;
        }

        public static string ParseJson(string data, string paramName)
        {
            #region old code
            //string value = string.Empty;
            //int startIndx = data.IndexOf(paramName);
            //if (startIndx > 0)
            //{
            //    int indexstart = startIndx + paramName.Length + 3;
            //    int endIndx = data.IndexOf("\"", startIndx);

            //    value = data.Substring(startIndx, endIndx - startIndx).Replace(",", "");

            //    if (value.Contains(paramName))
            //    {
            //        try
            //        {
            //            string[] getOuthentication = System.Text.RegularExpressions.Regex.Split(data, "postAuthenticityToken");
            //            //value = getOuthentication[1].Substring(getOuthentication[1].IndexOf(":") + 2, getOuthentication[1].IndexOf(",\"currentUser") - getOuthentication[1].IndexOf(":") - 3);
            //            string[] authenticity = Regex.Split(getOuthentication[1], ",");
            //            value = authenticity[0].Replace("\"", "").Replace(":", "");
            //        }
            //        catch { };
            //    }

            //    return value;
            //}
            //else
            //{
            //    string[] array = Regex.Split(data , "<input type=\"hidden\"");
            //    foreach (string item in array)
            //    {
            //        if(item.Contains("authenticity_token"))
            //        {
            //            int startindex = item.IndexOf("value=\"");
            //            if (startindex > 0)
            //            {
            //                string start = item.Substring(startindex).Replace("value=\"", "");
            //                int endIndex = start.IndexOf("\"");
            //                string end = start.Substring(0, endIndex);
            //                value = end;
            //                break;
            //            }
            //        }
            //    }

            //}
            //return value; 
            #endregion
            int startIndx = data.IndexOf(paramName) + paramName.Length + 3;
            int endIndx = data.IndexOf("\"", startIndx);

            string value = data.Substring(startIndx, endIndx - startIndx).Replace(",", "");
            return value;
        }


        public static string parseText(string item)
        {
            try
            {
                int startIndex = item.IndexOf("text\":\"");
                string StartString = item.Substring(startIndex).Replace("text\":\"", "");
                int endIndex = StartString.IndexOf("\",\"");
                string EndString = StartString.Substring(0, endIndex);

                return EndString;
            }
            catch (Exception ex)
            {
              
                return "";
            }
        }

        public static string ParseEncodedJson(string data, string paramName)
        {
            data = data.Replace("&quot;", "\"");
            int startIndx = data.IndexOf("\"" + paramName + "\"") + ("\"" + paramName + "\"").Length + 1;
            int endIndx = data.IndexOf("\"", startIndx);

            string value = data.Substring(startIndx, endIndx - startIndx).Replace(",", "");
            return value;
        }


        public string GetDataWithTagValueByTagAndAttributeName(string pageSrcHtml, string TagName, string AttributeName)
        {
            string dataDescription = string.Empty;
            try
            {
                bool success = false;
                string xHtml = string.Empty;

                Chilkat.HtmlToXml htmlToXml = new Chilkat.HtmlToXml();

                //*** Check DLL working or not **********************
                success = htmlToXml.UnlockComponent("THEBACHtmlToXml_7WY3A57sZH3O");
                if ((success != true))
                {
                    Console.WriteLine(htmlToXml.LastErrorText);
                    return null;
                }

                htmlToXml.Html = pageSrcHtml;

                //** Convert Data Html to XML ******************************************* 
                xHtml = htmlToXml.ToXml();

                //******************************************
                Chilkat.Xml xNode = default(Chilkat.Xml);
                Chilkat.Xml xBeginSearchAfter = default(Chilkat.Xml);
                Chilkat.Xml xml = new Chilkat.Xml();
                xml.LoadXml(xHtml);

                #region Data Save in list From using XML Tag and Attribut
                string DescriptionMain = string.Empty;

                string dataDescriptionValue = string.Empty;


                xBeginSearchAfter = null;

                xNode = xml.SearchForAttribute(xBeginSearchAfter, TagName, "class", AttributeName);
                while ((xNode != null))
                {
                    //** Get Data Under Tag only Text Value**********************************



                    dataDescription = xNode.GetXml();//.AccumulateTagContent("text", "script|style");

                    dataDescriptionValue = dataDescriptionValue + dataDescription;
                    //    string text = xNode.AccumulateTagContent("text", "script|style");
                    //    lstData.Add(text);

                    //    //** Get Data Under Tag All  Html value * *********************************
                    //    //dataDescription = xNode.GetXml();

                    xBeginSearchAfter = xNode;
                    xNode = xml.SearchForAttribute(xBeginSearchAfter, TagName, "class", AttributeName);
                    //if (dataDescription.Length > 500)
                    //{
                    //    break;
                    //}
                }
                #endregion
                return dataDescriptionValue;
            }
            catch (Exception)
            {
                return dataDescription = null;

            }
        }

        public List<string> GetTextDataByTagAndAttributeName(string pageSrcHtml, string TagName, string AttributeName)
        {
            List<string> lstData = new List<string>();
            try
            {
                bool success = false;
                string xHtml = string.Empty;

                Chilkat.HtmlToXml htmlToXml = new Chilkat.HtmlToXml();

                //*** Check DLL working or not **********************
                success = htmlToXml.UnlockComponent("THEBACHtmlToXml_7WY3A57sZH3O");
                if ((success != true))
                {
                    Console.WriteLine(htmlToXml.LastErrorText);
                    return null;
                }

                htmlToXml.Html = pageSrcHtml;

                //** Convert Data Html to XML ******************************************* 
                xHtml = htmlToXml.ToXml();

                //******************************************
                Chilkat.Xml xNode = default(Chilkat.Xml);
                Chilkat.Xml xBeginSearchAfter = default(Chilkat.Xml);
                Chilkat.Xml xml = new Chilkat.Xml();
                xml.LoadXml(xHtml);

                #region Data Save in list From using XML Tag and Attribut
                string DescriptionMain = string.Empty;
                string dataDescription = string.Empty;

                xBeginSearchAfter = null;

                xNode = xml.SearchForAttribute(xBeginSearchAfter, TagName, "class", AttributeName);
                while ((xNode != null))
                {
                    //** Get Data Under Tag only Text Value**********************************
                    dataDescription = xNode.GetXml();//.AccumulateTagContent("text", "script|style");

                    string text = xNode.AccumulateTagContent("text", "script|style");
                    lstData.Add(text);

                    //** Get Data Under Tag All  Html value * *********************************
                    //dataDescription = xNode.GetXml();

                    xBeginSearchAfter = xNode;
                    xNode = xml.SearchForAttribute(xBeginSearchAfter, TagName, "class", AttributeName);
                }
                #endregion
                return lstData;
            }
            catch (Exception)
            {
                return lstData = null;

            }
        }

        public List<string> GetTextDataByTagAndAttributeID(string pageSrcHtml, string TagName, string AttributeName)
        {
            List<string> lstData = new List<string>();
            try
            {
                bool success = false;
                string xHtml = string.Empty;

                Chilkat.HtmlToXml htmlToXml = new Chilkat.HtmlToXml();

                //*** Check DLL working or not **********************
                success = htmlToXml.UnlockComponent("THEBACHtmlToXml_7WY3A57sZH3O");
                if ((success != true))
                {
                    Console.WriteLine(htmlToXml.LastErrorText);
                    return null;
                }

                htmlToXml.Html = pageSrcHtml;

                //** Convert Data Html to XML ******************************************* 
                xHtml = htmlToXml.ToXml();

                //******************************************
                Chilkat.Xml xNode = default(Chilkat.Xml);
                Chilkat.Xml xBeginSearchAfter = default(Chilkat.Xml);
                Chilkat.Xml xml = new Chilkat.Xml();
                xml.LoadXml(xHtml);

                #region Data Save in list From using XML Tag and Attribut
                string DescriptionMain = string.Empty;
                string dataDescription = string.Empty;

                xBeginSearchAfter = null;

                xNode = xml.SearchForAttribute(xBeginSearchAfter, TagName, "id", AttributeName);
                while ((xNode != null))
                {
                    //** Get Data Under Tag only Text Value**********************************
                    dataDescription = xNode.GetXml();//.AccumulateTagContent("text", "script|style");

                    string text = xNode.AccumulateTagContent("text", "script|style");
                    lstData.Add(text);

                    //** Get Data Under Tag All  Html value * *********************************
                    //dataDescription = xNode.GetXml();

                    xBeginSearchAfter = xNode;
                    xNode = xml.SearchForAttribute(xBeginSearchAfter, TagName, "id", AttributeName);
                }
                #endregion
                return lstData;
            }
            catch (Exception)
            {
                return lstData = null;

            }
        }

        public List<string> GetDataTag(string pageSrcHtml, string TagName)
        {
            bool success = false;
            string xHtml = string.Empty;
            List<string> list = new List<string>();

            try
            {


                Chilkat.HtmlToXml htmlToXml = new Chilkat.HtmlToXml();
                success = htmlToXml.UnlockComponent("THEBACHtmlToXml_7WY3A57sZH3O");
                if ((success != true))
                {
                    Console.WriteLine(htmlToXml.LastErrorText);
                    return null;
                }

                htmlToXml.Html = pageSrcHtml;

                //xHtml contain xml data 
                xHtml = htmlToXml.ToXml();

                //******************************************
                Chilkat.Xml xNode = default(Chilkat.Xml);
                Chilkat.Xml xBeginSearchAfter = default(Chilkat.Xml);
                Chilkat.Xml xml = new Chilkat.Xml();
                xml.LoadXml(xHtml);

                xBeginSearchAfter = null;
                xNode = xml.SearchForTag(xBeginSearchAfter, TagName);
                while ((xNode != null))
                {

                    string TagText = xNode.AccumulateTagContent("text", "script|style");

                    list.Add(TagText);

                    xBeginSearchAfter = xNode;
                    xNode = xml.SearchForTag(xBeginSearchAfter, TagName);

                }
                //xHtml.
            }
            catch (Exception ex)
            {
                //GlobusLogHelper.log.Error("Error : " + ex.StackTrace);
            }
            return list;
        }
       

        public string getBetween(string strSource, string strStart, string strEnd)
        {
            int Start, End;
            if (strSource.Contains(strStart) && strSource.Contains(strEnd))
            {
                Start = strSource.IndexOf(strStart, 0) + strStart.Length;
                End = strSource.IndexOf(strEnd, Start);
                return strSource.Substring(Start, End - Start);
            }
            else
            {
                return "";
            }
        }

    }
}

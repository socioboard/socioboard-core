using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SocioBoard.Model;
using SocioBoard.Domain;
using System.Net;
using System.IO;
using System.Text;


namespace SocioBoard.Helper
{
    public class SBUtils : System.Web.UI.Page
    {
        HttpWebRequest gRequest;
        HttpWebResponse gResponse;
        CookieCollection gCookies;

        public static bool IsUserWorkingDaysValid(DateTime ExpiryDate)
        {
            bool isUserWorkingDaysValid = false;
            //try
            //{
            //    TimeSpan span = DateTime.Now.Subtract(registrationDate);
            //    int totalDays = (int)span.TotalDays;

            //    if (totalDays < 30)
            //    {
            //        isUserWorkingDaysValid = true;
            //    }
            //}
            //catch (Exception ex)
            //{
            //    //logger.Error(ex.Message);

            //    Console.WriteLine("Error : " + ex.StackTrace);
            //}
            try
            {
                int daysremaining = (ExpiryDate - DateTime.Now).Days;
                if (daysremaining < 0)
                {
                    isUserWorkingDaysValid = false;
                }
                else
                {
                    isUserWorkingDaysValid = true;
                }
            }
            catch (Exception ex)
            {
                //logger.Error(ex.Message);

                Console.WriteLine("Error : " + ex.StackTrace);
            }

            return isUserWorkingDaysValid;
        }

        public static int DaysBetween(DateTime d1, DateTime d2)
        {
            TimeSpan span = d2.Subtract(d1);
            return (int)span.TotalDays;
        }

        public static string GetCouponStatus(string coupon)
        {
            string ret = string.Empty;
            try
            {
                Coupon objCoupon = new Coupon();
                CouponRepository objCouponRepository = new CouponRepository();
                objCoupon.CouponCode = coupon;
                List<Coupon> lstCoupon = objCouponRepository.GetCouponByCouponCode(objCoupon);
                if (lstCoupon.Count > 0)
                {
                    if (lstCoupon[0].Status == "0")
                    {
                        ret = "valid";
                    }
                    else
                    {
                        ret = "This Coupon is already Used!";
                    }
                }
                else
                {
                    ret = "Coupon is not Valid!";
                }
            }
            catch (Exception ex)
            {
                //logger.Error(ex.Message);

                Console.WriteLine("Error : " + ex.StackTrace);
            }

            return ret;
        }


        // GET REQUEST
        //----------------  



        public string GetHtmlfromUrl(Uri url)
        {
            string responseString = string.Empty;
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

                    string responseURI = gResponse.ResponseUri.AbsoluteUri;

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
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
            }
            return responseString;
        }

        //-------------------------------------------------------------------
        //PostRequest

        //-------------------------

        public string postFormData(Uri formActionUrl, string postData, string proxyAddress, int port, string proxyUsername, string proxyPassword)
        {
            string responseString = string.Empty;

            try
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
                    responseString = reader.ReadToEnd();
                    reader.Close();
                    //Console.Write("Response String:" + responseString);

                }
                else
                {
                    return "Error in posting data";
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error : " + ex.StackTrace);
            }

            return responseString;

        }


        public void setExpect100Continue()
        {
            try
            {
                if (ServicePointManager.Expect100Continue == true)
                {
                    ServicePointManager.Expect100Continue = false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error : " + ex.StackTrace);
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
                //GlobusLogHelper.log.Error(ex.StackTrace);
                Console.WriteLine("Error : " + ex.StackTrace);

            }

        }

        public string GetPayPalResponse(Dictionary<string, string> formVals, bool useSandbox)
        {
            string response = "";

            try
            {
                string paypalUrl = "https://www.sandbox.paypal.com/cgi-bin/webscr";

                HttpWebRequest req = (HttpWebRequest)WebRequest.Create(paypalUrl);
                req.Method = "POST";
                req.ContentType = "application/x-www-form-urlencoded";

                byte[] param = Request.BinaryRead(Request.ContentLength);
                string strRequest = Encoding.ASCII.GetString(param);

                StringBuilder sb = new StringBuilder();
                sb.Append(strRequest);

                foreach (string key in formVals.Keys)
                {
                    sb.AppendFormat("&{0}={1}", key, formVals[key]);
                }
                strRequest += sb.ToString();
                req.ContentLength = strRequest.Length;


                using (StreamWriter streamOut = new StreamWriter(req.GetRequestStream(), System.Text.Encoding.ASCII))
                {

                    streamOut.Write(strRequest);
                    streamOut.Close();
                    using (StreamReader streamIn = new StreamReader(req.GetResponse().GetResponseStream()))
                    {
                        response = streamIn.ReadToEnd();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error : " + ex.StackTrace);
            }
            return response;
        }

    }
}
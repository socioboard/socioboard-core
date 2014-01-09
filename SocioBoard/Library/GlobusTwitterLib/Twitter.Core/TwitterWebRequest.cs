using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.IO;
using GlobusTwitterLib.App.Core;
using GlobusLib;

namespace GlobusTwitterLib.Twitter.Core
{
    public class TwitterWebRequest
    {
        
        public string  PerformWebRequest(TwitterUser twitterUser,string uri, string HTTPMethod,bool IsAuthenticationRequire,string goodProxy)
        {
            HttpWebRequest Request = (HttpWebRequest)WebRequest.Create(uri );
            Request.Method = HTTPMethod;
            StreamReader readStream;
            Request.MaximumAutomaticRedirections = 4;
            Request.MaximumResponseHeadersLength = 4;
            Request.ContentLength = 0;
            Globals.RequestCount++;
            if(IsAuthenticationRequire)
            Request.Credentials = new NetworkCredential(twitterUser.TwitterUserName , twitterUser.TwitterPassword);
            HttpWebResponse Response;
            string strResponse="";
            try
            {
                Response = (HttpWebResponse)Request.GetResponse();
                Stream receiveStream = Response.GetResponseStream();
                readStream = new StreamReader(receiveStream);
                strResponse = readStream.ReadToEnd();
                Response.Close();
                readStream.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message );
                strResponse = ex.Message;
                //Logger.LogText("Exception from Twitter:" + ex.Message,"");               
            }

            return strResponse;
        }
        public string CheckProxy(List<string> proxies )
        {
            Logger.LogText("Checking proxies............");
            string pr="";
            foreach (string proxy in proxies)
            {
                HttpWebRequest Request = (HttpWebRequest)WebRequest.Create("http://www.google.com");
                Request.Method = "GET";
                Request.Proxy = new WebProxy("http://"+proxy);
                HttpWebResponse Response;
                try
                {
                    Response = (HttpWebResponse)Request.GetResponse();
                    if (Response.StatusCode.ToString()=="OK")
                    {
                        Logger.LogText("Proxy found : "+proxy);
                        pr= "http://" + proxy;
                    }
                    return proxy ;
                }
                catch
                {
                    return pr;
                }

            }
            return pr;
        }

    }


}

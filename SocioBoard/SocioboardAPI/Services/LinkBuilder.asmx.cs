using GlobusTwitterLib.Twitter.Core.SearchMethods;
using Ionic.Zlib;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Script.Services;
using System.Web.Services;
using log4net;
using System.Text;

namespace Api.Socioboard.Services
{
    /// <summary>
    /// Summary description for LinkBuilder
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class LinkBuilder : System.Web.Services.WebService
    {
        ILog logger = LogManager.GetLogger(typeof(LinkBuilder)); 
        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string TwitterLinkBuilder(string q)
        {
            string ret = string.Empty;
            JArray output = new JArray();
            SortedDictionary<string, string> requestParameters = new SortedDictionary<string, string>();
            try
            {
                var oauth_url = " https://api.twitter.com/1.1/search/tweets.json?q=" + q.Trim() + "&result_type=recent";
                var headerFormat = "Bearer {0}";
                var authHeader = string.Format(headerFormat, "AAAAAAAAAAAAAAAAAAAAAOZyVwAAAAAAgI0VcykgJ600le2YdR4uhKgjaMs%3D0MYOt4LpwCTAIi46HYWa85ZcJ81qi0D9sh8avr1Zwf7BDzgdHT");

                var postBody = requestParameters.ToWebString();
                ServicePointManager.Expect100Continue = false;

                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(oauth_url + "?"
                       + requestParameters.ToWebString());

                request.Headers.Add("Authorization", authHeader);
                request.Method = "GET";
                request.Headers.Add("Accept-Encoding", "gzip");
                HttpWebResponse response = request.GetResponse() as HttpWebResponse;
                Stream responseStream = new GZipStream(response.GetResponseStream(), CompressionMode.Decompress);
                using (var reader = new StreamReader(responseStream))
                {
                    var objText = reader.ReadToEnd();
                    output = JArray.Parse(JObject.Parse(objText)["statuses"].ToString());
                }
                List<string> _lst = new List<string>();
                foreach (var item in output)
                {
                    try
                    {
                        string _urls = item["entities"]["urls"][0]["expanded_url"].ToString();
                        if (!string.IsNullOrEmpty(_urls))
                            _lst.Add(_urls);
                    }
                    catch { }
                }

                ret = new JavaScriptSerializer().Serialize(_lst);
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                ret = "";
            }

            return ret; 
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string GPlusLinkBuilder(string q) 
        {
            string ret = string.Empty;
            string response = string.Empty;
            JArray output = new JArray();
            string Key = "AIzaSyCISaVFe_TJknn92J7xw2diFEi6Z_aroYE";
            string RequestUrl = " https://www.googleapis.com/plus/v1/activities?orderBy=recent&query=" + q + "&alt=json&key=" + Key;
            try
            {
                var gpluspagerequest = (HttpWebRequest)WebRequest.Create(RequestUrl);
                gpluspagerequest.Method = "GET";
                try
                {
                    using (var gplusresponse = gpluspagerequest.GetResponse())
                    {
                        using (var stream = new StreamReader(gplusresponse.GetResponseStream(), Encoding.GetEncoding(1252)))
                        {
                            response = stream.ReadToEnd();
                            output = JArray.Parse(JObject.Parse(response)["items"].ToString());
                        }
                    }
                    List<string> _lst = new List<string>();
                    foreach (var item in output)
                    {
                        try
                        {
                            string _urls = item["object"]["attachments"][0]["url"].ToString();
                            if (!string.IsNullOrEmpty(_urls) && !_urls.StartsWith("https://plus.google.com"))
                                _lst.Add(_urls);
                        }
                        catch { }
                    }
                    ret = new JavaScriptSerializer().Serialize(_lst);

                }
                catch (Exception e) { }
            }
            catch (Exception ex)
            {
                logger.Error(ex.StackTrace);
                ret = "";
            }
            return ret;
        }

    }
}

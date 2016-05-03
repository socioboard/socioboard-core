using Api.Socioboard.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Services;

namespace Api.Socioboard.Services
{
    /// <summary>
    /// Summary description for plugininfo
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class plugininfo : System.Web.Services.WebService
    {

        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }

        [WebMethod]
        public string AddPluginUrl(string Url)
        {

            #region UrlIformation
            string description = "";
            string title = "";
            string imgurl = "";
            PluginInfoRepository _plugininfoRepository = new PluginInfoRepository();
            Domain.Socioboard.Domain.PluginInfo _plugininfo = new Domain.Socioboard.Domain.PluginInfo();
            if (!_plugininfoRepository.IsUrlExist(Url))
            {
                if (!Url.Contains("socioboard"))
                {
                    string pagesource = GetHtml(Url);
                    if (pagesource.Contains("<img") && !string.IsNullOrEmpty(pagesource))
                    {

                        string[] atrr = Regex.Split(pagesource, "<img");
                        foreach (var item in atrr)
                        {
                            if (item.Contains("src") && !item.Contains("<!DOCTYPE html>"))
                            {
                                string url = "";
                                try
                                {
                                    url = getBetween(item, "src=\"", "alt=").Replace("\"", string.Empty);
                                }
                                catch (Exception ex)
                                {

                                    url = getBetween(item, "src=\"", "\"").Replace("\"", string.Empty);
                                }
                                imgurl = url + "," + imgurl;
                            }
                        }
                    }
                    if (pagesource.Contains("<meta"))
                    {
                        string[] metatag = Regex.Split(pagesource, "<meta");
                        foreach (var item in metatag)
                        {
                            string data = "";
                            if (item.Contains("description"))
                            {
                                data = getBetween(item, "content=", ">").Replace("\"", "").Replace("/", "");
                                description = data + "," + description;
                            }

                            if (item.Contains("site_name"))
                            {

                                title = getBetween(item, "content=\"", "\"");
                            }
                            if (string.IsNullOrEmpty(title))
                            {
                                if (item.Contains("title name"))
                                {

                                    title = getBetween(item, "content=\"", "\"");
                                }
                            }
                        }
                    }
                }
                else
                {

                    string pagesource = GetHtml(Url);
                    if (pagesource.Contains("<img") && !string.IsNullOrEmpty(pagesource))
                    {

                        string[] atrr = Regex.Split(pagesource, "<img");
                        foreach (var item in atrr)
                        {
                            if (item.Contains("src") && !item.Contains("<!DOCTYPE"))
                            {
                                string url = "";
                                if (item.Contains("/Themes"))
                                {
                                    url = getBetween(item, "src=", "alt=").Replace("\"", string.Empty);
                                    url = "https://www.socioboard.com" + url;

                                }
                                else
                                {
                                    url = getBetween(item, "src=", "class=").Replace("\"", string.Empty);
                                }

                                imgurl = url + "," + imgurl;
                            }
                        }
                    }
                    if (pagesource.Contains("<meta"))
                    {
                        string[] metatag = Regex.Split(pagesource, "<meta");
                        foreach (var item in metatag)
                        {
                            string data = "";
                            if (item.Contains("description"))
                            {
                                data = getBetween(item, "content=", ">").Replace("\"", "").Replace("/", "");
                                description = data + "," + description;

                            }
                            if (item.Contains("site_name"))
                            {

                                title = getBetween(item, "content=\"", "\"");
                            }
                            if (string.IsNullOrEmpty(title))
                            {
                                if (item.Contains("title name"))
                                {

                                    title = getBetween(item, "content=\"", "\"");
                                }
                            }
                        }
                    }

                }

                _plugininfo.id = Guid.NewGuid();
                _plugininfo.imageurl = imgurl;
                _plugininfo.url = Url;
                _plugininfo.description = description;
                _plugininfo.title = title;
                PluginInfoRepository.Add(_plugininfo);
                _plugininfo = _plugininfoRepository.getUrlInfo(Url);
                return new JavaScriptSerializer().Serialize(_plugininfo);

            }
            else
            {

                _plugininfo = _plugininfoRepository.getUrlInfo(Url);
                return new JavaScriptSerializer().Serialize(_plugininfo);
            }
            #endregion

        }


        public Chilkat.Http http = new Chilkat.Http();
        public string GetHtml(string URL)
        {
            string response = string.Empty;

            //ChangeProxy();

            if (!http.UnlockComponent("THEBACHttp_b3C9o9QvZQ06"))
            {
            }

            ///Save Cookies...
            http.CookieDir = "memory";
            //http.CookieDir = Application.StartupPath + "\\cookies";
            http.SendCookies = true;
            http.SaveCookies = true;

            http.SetRequestHeader("Accept-Encoding", "gzip,deflate");
            http.SetRequestHeader("Accept-Charset", "ISO-8859-1,utf-8;q=0.7,*;q=0.7");
            http.SetRequestHeader("User-Agent", "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/44.0.2403.107 Safari/537.36");
            http.SetRequestHeader("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8");

            //http.SetRequestHeader("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8");
            //http.SetRequestHeader("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8");
            http.SetRequestHeader("Connection", "keep-alive");

            http.AllowGzip = true;

            response = http.QuickGetStr(URL);
            if (string.IsNullOrEmpty(response))
            {
                response = http.QuickGetStr(URL);

            }
            if (string.IsNullOrEmpty(response))
            {
                response = http.QuickGetStr(URL);

            }

            return response;

        }

        public static string getBetween(string strSource, string strStart, string strEnd)
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

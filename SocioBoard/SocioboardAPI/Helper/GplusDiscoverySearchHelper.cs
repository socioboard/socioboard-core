using log4net;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Services;

namespace Api.Socioboard.Helper
{
    public static class GplusDiscoverySearchHelper
    {
        public static string GooglePlus(string keyword)
        {
            ILog logger = LogManager.GetLogger(typeof(GplusDiscoverySearchHelper));

            string ret = string.Empty;
            try
            {
                string Key = "AIzaSyASmXtuaErvVC0FGblSLUAzRcxRXLlBsgE";
                //AIzaSyCvTBnEDnr5DEpvlVDCuxz9K9TK84rX0fE
                //string RequestUrl = "https://www.googleapis.com/youtube/v3/search?part=" + part + "&maxResults=" + maxResults + "&q=" + keyword + "&key=" + accesstoken;
                string RequestUrl = "https://www.googleapis.com/plus/v1/activities?query=" + keyword + "&maxResults=15&orderBy=best&key=" + Key;



                var gpluslistpagerequest = (HttpWebRequest)WebRequest.Create(RequestUrl);
                gpluslistpagerequest.Method = "GET";
                string response = string.Empty;
                try
                {
                    using (var gplusresponse = gpluslistpagerequest.GetResponse())
                    {
                        using (var stream = new StreamReader(gplusresponse.GetResponseStream(), Encoding.GetEncoding(1252)))
                        {
                            response = stream.ReadToEnd();

                        }
                    }
                }
                catch (Exception ex)
                {

                    logger.Error(ex.StackTrace);
                }

                return response;
            }
            catch (Exception ex)
            {

                logger.Error(ex.StackTrace);
            }

            return ret;
        }

    }
}
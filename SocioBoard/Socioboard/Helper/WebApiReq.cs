using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;

namespace Socioboard.Helper
{
    public static class WebApiReq
    {
        public static async Task<HttpResponseMessage> PostReq(string Url, List<KeyValuePair<string, string>> Parameters, string AccessTokenType, string AccessToken)
        {
            HttpResponseMessage response;
            using (var client = new HttpClient())
            {
                // New code:
                client.BaseAddress = new Uri(System.Configuration.ConfigurationManager.AppSettings["apiDomainName"]);
                // client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Add("contentType", "application/x-www-form-urlencoded");
                client.DefaultRequestHeaders.Add("Accept", "application/json");
                if (string.IsNullOrEmpty(AccessTokenType) && !string.IsNullOrEmpty(AccessToken))
                {
                    client.DefaultRequestHeaders.Add("Authorization", AccessTokenType + " " + AccessToken);
                }
                var content = new FormUrlEncodedContent(Parameters);
                response = await client.PostAsync(Url, content);
            }
            return response;
        }

        public static async Task<HttpResponseMessage> GetReq(string Url, string AccessTokenType, string AccessToken)
        {
            HttpResponseMessage response;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(System.Configuration.ConfigurationManager.AppSettings["apiDomainName"]);
                client.DefaultRequestHeaders.Accept.Clear();
                if (!string.IsNullOrEmpty(AccessTokenType) && !string.IsNullOrEmpty(AccessToken))
                {
                    client.DefaultRequestHeaders.Add("Authorization", AccessTokenType + " " + AccessToken);
                }
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                response = await client.GetAsync(Url);
            }
            return response;
        }


        public static string GetShareathondata(string Url)
        {
            try
            {
                HttpWebRequest httpRequest = (HttpWebRequest)WebRequest.Create(Url);
                httpRequest.Method = "GET";
                httpRequest.ContentType = "application/x-www-form-urlencoded";
                HttpWebResponse httResponse = (HttpWebResponse)httpRequest.GetResponse();
                Stream responseStream = httResponse.GetResponseStream();
                StreamReader responseStreamReader = new StreamReader(responseStream, System.Text.Encoding.Default);
                string pageContent = responseStreamReader.ReadToEnd();
                responseStreamReader.Close();
                responseStream.Close();
                httResponse.Close();
                return pageContent;
            }
            catch (Exception ex)
            {
                return Url;
            }
        }

        public static async Task<HttpResponseMessage> DelReq(string Url, string AccessTokenType, string AccessToken)
        {
            HttpResponseMessage response;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(System.Configuration.ConfigurationManager.AppSettings["apiDomainName"]);
                client.DefaultRequestHeaders.Accept.Clear();
                if (!string.IsNullOrEmpty(AccessTokenType) && !string.IsNullOrEmpty(AccessToken))
                {
                    client.DefaultRequestHeaders.Add("Authorization", AccessTokenType + " " + AccessToken);
                }
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                response = await client.DeleteAsync(Url);
            }
            return response;
        }
    }
}
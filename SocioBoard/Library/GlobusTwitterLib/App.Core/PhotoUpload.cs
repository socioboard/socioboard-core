using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using OAuth;
using System.IO;
using System.Configuration;
using System.Web;
using GlobusTwitterLib.Authentication;
using System.Security.Permissions;
using log4net;


namespace GlobusTwitterLib.App.Core
{
   
 public class PhotoUpload
    {
     ILog logger = LogManager.GetLogger(typeof(PhotoUpload));
        OAuth.Manager oauth = new OAuth.Manager();


        string twitterUrl1 = Globals.StatusUpdateUrl;
        string twitterUrl2 = Globals.PostStatusUpdateWithMediaUrl;
        SortedDictionary<string, string> strdic = new SortedDictionary<string, string>();


        public string GetTwitterUpdateUrl(string imageFile, string message)
        {

            return (imageFile == null) ?
               twitterUrl1 : twitterUrl2;
        }

        public static string GetMimeType(String filename)
        {
          
            var extension = System.IO.Path.GetExtension(filename).ToLower();
            var regKey = Microsoft.Win32.Registry.ClassesRoot.OpenSubKey(extension);

            string result =
                ((regKey != null) && (regKey.GetValue("Content Type") != null))
                ? regKey.GetValue("Content Type").ToString()
                : "image/unknown";
            return result;
        }

        public bool Tweet(string imageFile, string message,oAuthTwitter oAuth )
        {
            bool bupdated = false;
            try
            {
                //HttpContext.Current.Response.Write("<script>alert(\""+imageFile+"\")</script>");
          
                oauth["consumer_key"] = oAuth.ConsumerKey;

                oauth["consumer_secret"] = oAuth.ConsumerKeySecret;
                oauth["token"] = oAuth.AccessToken;
                oauth["token_secret"] = oAuth.AccessTokenSecret;

                var url = GetTwitterUpdateUrl(imageFile, message);
                if (url == twitterUrl1)
                {
                    strdic.Add("status", message);
                }
                var authzHeader = oauth.GenerateAuthzHeader(url, "POST");
                var request = (HttpWebRequest)WebRequest.Create(url);

                request.Method = "POST";
                request.PreAuthenticate = true;
                request.AllowWriteStreamBuffering = true;
                request.Headers.Add("Authorization", authzHeader);

                if (imageFile != null)
                {
                    string boundary = "======" +
                                  Guid.NewGuid().ToString().Substring(18).Replace("-", "") +
                                  "======";

                    var separator = "--" + boundary;
                    var footer = "\r\n" + separator + "--\r\n";

                    string shortFileName = Path.GetFileName(imageFile);
                    string fileContentType = GetMimeType(shortFileName);
                    string fileHeader = string.Format("Content-Disposition: file; " +
                                                      "name=\"media\"; filename=\"{0}\"",
                                                      shortFileName);
                    var encoding = System.Text.Encoding.GetEncoding("iso-8859-1");

                    var contents = new System.Text.StringBuilder();
                    contents.AppendLine(separator);
                    contents.AppendLine("Content-Disposition: form-data; name=\"status\"");
                    contents.AppendLine();
                    contents.AppendLine(message);
                    contents.AppendLine(separator);
                    contents.AppendLine(fileHeader);
                    contents.AppendLine(string.Format("Content-Type: {0}", fileContentType));
                    contents.AppendLine();

                    request.ServicePoint.Expect100Continue = false;
                    request.ContentType = "multipart/form-data; boundary=" + boundary;
                    // actually send the request
                    using (var s = request.GetRequestStream())
                    {
                        byte[] bytes = encoding.GetBytes(contents.ToString());
                        s.Write(bytes, 0, bytes.Length);
                        bytes = File.ReadAllBytes(imageFile);
                        s.Write(bytes, 0, bytes.Length);
                        bytes = encoding.GetBytes(footer);
                        s.Write(bytes, 0, bytes.Length);
                    }
                }


                using (var response = (HttpWebResponse)request.GetResponse())
                {
                 //   HttpContext.Current.Response.Write("<script>alert(\"" + response.StatusCode + "\")</script>");

                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        bupdated = true;
                    }
                }


              
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                logger.Error(ex.Message);
                //using (StreamWriter _testData = new StreamWriter(HttpContext.Current.Server.MapPath("~/log.txt"), true))
                //{
                //    _testData.WriteLine("Error on PhotoUpload : " + ex.Message); // Write the file.    

                //}

            }


            return bupdated;
        }
    


    }
}

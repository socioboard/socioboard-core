using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Net;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Configuration;


namespace GlobusTumblerLib.Tumblr.Core.BlogMethods
{
   public class BlogInfo
    {

      public string getTumblrUserInfo(string tumblrusername)
       {
         
           string sURL;
           sURL = "http://api.tumblr.com/v2/blog/" + tumblrusername + ".tumblr.com/info?api_key=" + ConfigurationManager.AppSettings["TumblrClientKey"];

           WebRequest wrGETURL;
           wrGETURL = WebRequest.Create(sURL);

           Stream objStream;
           objStream = wrGETURL.GetResponse().GetResponseStream();

           StreamReader objReader = new StreamReader(objStream);

           string sLine = "";
           int i = 0;
           string str = string.Empty;
           while (sLine != null)
           {
               i++;
               sLine = objReader.ReadLine();
               if (sLine != null)
                   Console.WriteLine("{0}:{1}", i, sLine);
               str += sLine;
           }
         
           dynamic profile = str;
           JValue asas = (JValue)str;

           JObject abc = JObject.Parse(asas.Value.ToString());
          
           string likes = abc["response"]["blog"]["likes"].ToString();
           string Post = abc["response"]["blog"]["posts"].ToString();

          return (likes+"&"+Post);


       }


      //public string getTumblrUserfollower(string tumblrusername)
      //{

      //    string sURL;
      //    sURL = "http://api.tumblr.com/v2/blog/" + tumblrusername + ".tumblr.com/followers";
                  
      //    WebRequest wrGETURL;
      //    wrGETURL = WebRequest.Create(sURL);

      //    Stream objStream;
      //    objStream = wrGETURL.GetResponse().GetResponseStream();

      //    StreamReader objReader = new StreamReader(objStream);

      //    string sLine = "";
      //    int i = 0;
      //    string str = string.Empty;
      //    while (sLine != null)
      //    {
      //        i++;
      //        sLine = objReader.ReadLine();
      //        if (sLine != null)
      //            Console.WriteLine("{0}:{1}", i, sLine);
      //        str += sLine;
      //    }

      //    dynamic profile = str;
      //    JValue asas = (JValue)str;

      //    JObject abc = JObject.Parse(asas.Value.ToString());

      //    string total_users = abc["response"]["total_users"].ToString();

      //    return (total_users);


      //}


    }
}

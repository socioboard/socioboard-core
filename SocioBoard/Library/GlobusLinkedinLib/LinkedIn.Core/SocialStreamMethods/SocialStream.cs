using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using GlobusLinkedinLib.App.Core;
using System.IO;
using GlobusLinkedinLib.Authentication;

namespace GlobusLinkedinLib.LinkedIn.Core.SocialStreamMethods
{
    public class SocialStream
    {
         private XmlDocument xmlResult;


         public SocialStream()
         {
             xmlResult = new XmlDocument();

         }

         public XmlDocument Get_UserUpdates(oAuthLinkedIn OAuth, string LinkedInId, int Count)
         {
             string response = OAuth.APIWebRequest("GET", Global.GetNetworkUserUpdates + LinkedInId + "/network/updates?scope=self" + "&count=" + Count, null);
             xmlResult.Load(new StringReader(response));
             return xmlResult;
         }

         public XmlDocument Get_NetworkUpdates(oAuthLinkedIn OAuth,int Count)
         {
             string response = OAuth.APIWebRequest("GET", Global.GetNetworkUpdates, null);
             xmlResult.Load(new StringReader(response));
             return xmlResult;
         }

         public string SetStatusUpdate(oAuthLinkedIn OAuth, string msg)
         {
             string xml = "<?xml version=\"1.0\" encoding=\"UTF-8\"?>";
             xml += "<current-status>" + msg + "</current-status>";
             string response = OAuth.APIWebRequest("PUT", Global.StatusUpdate, xml);             
             return response;
         }

         public string SetFollowCountUpdate(oAuthLinkedIn OAuth,string postid,string msg)
         {
             string xml = "<?xml version=\"1.0\" encoding=\"UTF-8\"?>";
             xml += " <is-following>" + msg + "</is-following>";
                       
             string url = "https://api.linkedin.com/v1/posts/" + postid + "/relation-to-viewer/is-following";

             string response = OAuth.APIWebRequest("PUT", url, xml);
             return response;
         }

         public string SetLikeUpdate(oAuthLinkedIn OAuth, string postid, string msg)
         {
             string xml = "<?xml version=\"1.0\" encoding=\"UTF-8\"?>";
             xml += " <is-liked>" + msg + "</is-liked>";

             string url = "https://api.linkedin.com/v1/posts/" + postid + "/relation-to-viewer/is-liked";

             string response = OAuth.APIWebRequest("PUT", url, xml);
             return response;
         }

         public string SetPostUpdate(oAuthLinkedIn OAuth,string groupId, string msg,string title)
         {
             string xml = "<?xml version=\"1.0\" encoding=\"UTF-8\"?>";
           //  xml += "<current-status>" + msg + "</current-status>";
             xml += "<post><title>" + title + "</title><summary>" + msg + "</summary></post>";

           string url = "http://api.linkedin.com/v1/groups/" + groupId + "/posts";
       
             string response = OAuth.APIWebRequest("POST",url, xml);
             return response;
         }




    }
}

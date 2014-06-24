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
    }
}

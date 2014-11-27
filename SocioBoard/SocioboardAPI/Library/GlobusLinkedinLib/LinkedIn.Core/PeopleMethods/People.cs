using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using GlobusLinkedinLib.Authentication;
using GlobusLinkedinLib.App.Core;
using System.IO;

namespace GlobusLinkedinLib.LinkedIn.Core.PeopleMethods
{
    public class People
    {
        private XmlDocument xmlResult;
        public People()
        {
            xmlResult = new XmlDocument();
         }
        /// <summary>
        /// Displays the profile the requestor is allowed to see.
        /// </summary>
        /// <param name="OAuth"></param>
        /// <returns></returns>
        public XmlDocument Get_UserProfile(oAuthLinkedIn OAuth)
        {
            string response = OAuth.APIWebRequest("GET", Global.GetUserProfileUrl, null);
            xmlResult.Load(new StringReader(response));
            return xmlResult;
        }

        /// <summary>
        /// The People Search API returns information about people.
        /// </summary>
        /// <param name="OAuth"></param>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public XmlDocument Get_People_Search(oAuthLinkedIn OAuth, string keyword)
        {
            string response = string.Empty;
            try
            {
                response = OAuth.APIWebRequest("GET", Global.GetPeopleSearchUrl + keyword + "sort=distance", null);
            }
            catch { }
            xmlResult.Load(new StringReader(response));
            return xmlResult;
        }

        public XmlDocument Get_People_Connection(oAuthLinkedIn OAuth)
        {
            string response = string.Empty;
            try
            {
                response = OAuth.APIWebRequest("GET", Global.GetPeopleConnectionUrl, null);
            }
            catch { }
            xmlResult.Load(new StringReader(response));
            return xmlResult;
        }
    }
}

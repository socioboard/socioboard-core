using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using GlobusLinkedinLib.Authentication;
using GlobusLinkedinLib.LinkedIn.Core.PeopleMethods;

namespace GlobusLinkedinLib.App.Core
{
    public class LinkedInProfile
    {
        private XmlDocument xmlResult;

        public LinkedInProfile()
        {
            xmlResult = new XmlDocument();
        }

        public List<UserProfile> NetworkUpdatesList = new List<UserProfile>();

        //id,first-name,last-name,headline,picture_url,educations,location,date_of_birth

        public struct UserProfile
        {
            public string id { get; set; }
            public string first_name { get; set; }
            public string last_name { get; set; }
            public string headline { get; set; }
            public string picture_url { get; set; }
            public string email { get; set; }
            public int connections { get; set; }
            public string currentstatus { get; set; }
            public string profile_url { get; set; }
        }

        /// <summary>
        /// Displays the profile the requestor is allowed to see.
        /// </summary>
        /// <param name="OAuth"></param>
        /// <returns></returns>
        public UserProfile GetUserProfile(oAuthLinkedIn OAuth)
        {
            UserProfile UserProfile = new UserProfile();

            People peopleConnection = new People();
            
            xmlResult = peopleConnection.Get_UserProfile(OAuth);

            //XmlNodeList xmlNodeList = xmlResult.GetElementsByTagName("update");

            try
            {
                UserProfile.id = xmlResult.GetElementsByTagName("id")[0].InnerText;
            }
            catch { }

            try
            {
                UserProfile.email = xmlResult.GetElementsByTagName("email-address")[0].InnerText;
            }
            catch { }
            try
            {

                UserProfile.profile_url = xmlResult.GetElementsByTagName("url")[0].InnerText;

            }
            catch 
            { }
          
            try
            {
                UserProfile.first_name = xmlResult.GetElementsByTagName("first-name")[0].InnerText;
            }
            catch { }

            try
            {
                UserProfile.last_name = xmlResult.GetElementsByTagName("last-name")[0].InnerText;
            }
            catch { }


            try
            {
                UserProfile.headline = xmlResult.GetElementsByTagName("headline")[0].InnerText;
            }
            catch { }

            try
            {
                UserProfile.picture_url = xmlResult.GetElementsByTagName("picture-url")[0].InnerText;
            }
            catch { }
            try
            {
                XmlDocument xmlConnection = new XmlDocument();
                xmlConnection = peopleConnection.Get_People_Connection(OAuth);
                UserProfile.connections = Convert.ToInt32(xmlConnection.GetElementsByTagName("num-connections")[0].InnerText);
            }
            catch { }    
            
            return UserProfile;

        }

    }
}

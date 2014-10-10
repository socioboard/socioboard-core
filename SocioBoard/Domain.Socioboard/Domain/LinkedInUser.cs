using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace Domain.Socioboard.Domain
{
    public class LinkedInUser
    {
        private XmlDocument xmlResult;

        public LinkedInUser()
        {
            xmlResult = new XmlDocument();
        }

        public List<User_Updates> UserUpdatesList = new List<User_Updates>();

        public struct User_Updates
        {
            public string DateTime { get; set; }
            public string UpdateType { get; set; }
            public string PersonId { get; set; }
            public string PersonFirstName { get; set; }
            public string PersonLastName { get; set; }
            public string PersonHeadLine { get; set; }
            public string PictureUrl { get; set; }
            public string Message { get; set; }
            public string Comment { get; set; }

        }
    }
}

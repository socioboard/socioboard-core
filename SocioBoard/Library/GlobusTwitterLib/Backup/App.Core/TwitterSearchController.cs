using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.IO;
using GlobusTwitterLib.Twitter.Core;
using GlobusTwitterLib;

namespace GlobusTwitterLib.App.Core
{
    public class TwitterSearchController
    {
        private XmlDocument xmlResult;
        

        public TwitterSearchController()
        {
            xmlResult = new XmlDocument();
        }

        public void Trends(TwitterUser User)
        {
            string Trends = null;
            GlobusTwitterLib.Twitter.Core.SearchMethods.Search search = new GlobusTwitterLib.Twitter.Core.SearchMethods.Search();
            Trends = search.Trends(User);
        }


    }
}

using GlobusLinkedinLib.Authentication;
using GlobusLinkedinLib.LinkedIn.Core.CompanyMethods;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace GlobusLinkedinLib.App.Core
{
    public class LinkedinPageComment
    {
        private XmlDocument xmlResult;
        public LinkedinPageComment()
        {
            xmlResult = new XmlDocument();
        }

        public List<CompanyPageComment> CompanyPagePostsList = new List<CompanyPageComment>();

        public struct CompanyPageComment
        {
            public string Comment { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string CommentTime { get; set; }
            public string PictureUrl { get; set; }
        }


        public List<CompanyPageComment> GetPagePostscomment(oAuthLinkedIn OAuth, string updatekey)
        {
            CompanyPageComment companypage_postcmnt = new CompanyPageComment();

            Company companyConnection = new Company();
            xmlResult = companyConnection.GetCommentOnPagePost(OAuth, updatekey);

            XmlNodeList xmlNodeList = xmlResult.GetElementsByTagName("update-comment");
            foreach (XmlNode xn in xmlNodeList)
            {

                try
                {
                    XmlElement Element = (XmlElement)xn;



                    try
                    {
                        companypage_postcmnt.Comment = Element.GetElementsByTagName("comment")[0].InnerText;
                    }
                    catch
                    { }
                    try
                    {

                        companypage_postcmnt.FirstName = Element.GetElementsByTagName("first-name")[0].InnerText;


                    }
                    catch
                    {
                        companypage_postcmnt.FirstName = Element.GetElementsByTagName("name")[0].InnerText;
                    }
                    try
                    {
                        companypage_postcmnt.LastName = Element.GetElementsByTagName("last-name")[0].InnerText;
                    }
                    catch
                    {
                        companypage_postcmnt.LastName = null;
                    }

                    try
                    {
                        double timestamp = Convert.ToDouble(Element.GetElementsByTagName("timestamp")[0].InnerText);
                        companypage_postcmnt.CommentTime = JavaTimeStampToDateTime(timestamp);
                    }
                    catch
                    {

                    }
                    try
                    {
                        companypage_postcmnt.PictureUrl = Element.GetElementsByTagName("picture-url")[0].InnerText;
                    }
                    catch
                    {
                        companypage_postcmnt.PictureUrl = null;
                    }


                    CompanyPagePostsList.Add(companypage_postcmnt);
                }
                catch { }
            }
            return CompanyPagePostsList;

        }

        public static string JavaTimeStampToDateTime(double javaTimeStamp)
        {
            // Java timestamp is millisecods past epoch
            System.DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            dtDateTime = dtDateTime.AddSeconds(Math.Round(javaTimeStamp / 1000)).ToLocalTime();
            string date = dtDateTime.ToString("MMMM dd, yy H:mm:ss tt");
            return date;
        }

    }
}

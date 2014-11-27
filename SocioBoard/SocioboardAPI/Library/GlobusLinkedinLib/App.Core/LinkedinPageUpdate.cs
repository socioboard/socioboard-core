using GlobusLinkedinLib.Authentication;
using GlobusLinkedinLib.LinkedIn.Core.CompanyMethods;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace GlobusLinkedinLib.App.Core
{
    public class LinkedinPageUpdate
    {
        private XmlDocument xmlResult;
        public LinkedinPageUpdate()
        {
            xmlResult = new XmlDocument();
        }

        public List<CompanyPagePosts> CompanyPagePostsList = new List<CompanyPagePosts>();

        public struct CompanyPagePosts
        {
            public string Posts { get; set; }
            public string PostDate { get; set; }
            public string Type { get; set; }
            public string PostId { get; set; }
            public string PostImageUrl { get; set; }
            public string UpdateKey { get; set; }
            public int Likes { get; set; }
            public int Comments { get; set; }
            public int isLiked { get; set; }
        }
        public List<CompanyPagePosts> GetPagePosts(oAuthLinkedIn OAuth, string CompanyPageId)
        {
            CompanyPagePosts companypage_post = new CompanyPagePosts();

            Company companyConnection = new Company();
            xmlResult = companyConnection.Get_CompanyUpdateById(OAuth, CompanyPageId);

            XmlNodeList xmlNodeList = xmlResult.GetElementsByTagName("update");
            foreach (XmlNode xn in xmlNodeList)
            {

                try
                {
                    XmlElement Element = (XmlElement)xn;



                    try
                    {
                        companypage_post.Type = Element.GetElementsByTagName("update-type")[0].InnerText;
                    }
                    catch
                    { }
                    try
                    {
                        companypage_post.UpdateKey = Element.GetElementsByTagName("update-key")[0].InnerText;
                    }
                    catch
                    { }
                    try
                    {
                        companypage_post.PostId = Element.GetElementsByTagName("service-provider-share-id")[0].InnerText;
                    }
                    catch
                    {

                    }

                    try
                    {
                        double timestamp = Convert.ToDouble(Element.GetElementsByTagName("timestamp")[0].InnerText);
                        companypage_post.PostDate = JavaTimeStampToDateTime(timestamp);
                    }
                    catch
                    {

                    }
                    try
                    {
                        companypage_post.Posts = Element.GetElementsByTagName("comment")[0].InnerText;
                    }
                    catch
                    {

                    }
                    try
                    {
                        companypage_post.PostImageUrl = Element.GetElementsByTagName("shortened-url")[0].InnerText;
                    }
                    catch
                    {
                        companypage_post.PostImageUrl = null;
                    }

                    try
                    {
                        string likes = Element.GetElementsByTagName("num-likes")[0].InnerText;
                        companypage_post.Likes = Convert.ToInt16(likes);
                    }
                    catch
                    {
                    }
                    try
                    {
                        string cnt = string.Empty;
                        XmlElement numofcomments = xmlResult.DocumentElement;
                        if (numofcomments.HasAttribute("total"))
                        {
                            cnt = numofcomments.GetAttribute("total");
                        }
                        companypage_post.Comments = Convert.ToInt16(cnt);
                    }
                    catch
                    {
                    }



                    CompanyPagePostsList.Add(companypage_post);
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

        public CompanyPagePosts GetPostLike(oAuthLinkedIn OAuth, string UpdateKey, string PageId)
        {
            CompanyPagePosts objLicmpnypost = new CompanyPagePosts();
            Company companyConnection = new Company();
            xmlResult = companyConnection.GetLikeorNotOnPagePost(OAuth, UpdateKey, PageId);
            try
            {
                string like = xmlResult.GetElementsByTagName("is-liked")[0].InnerText;
                if (like == "true")
                {
                    objLicmpnypost.isLiked = 1;
                }
                else { objLicmpnypost.isLiked = 0; }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
            }
            return objLicmpnypost;
        }
    }
}

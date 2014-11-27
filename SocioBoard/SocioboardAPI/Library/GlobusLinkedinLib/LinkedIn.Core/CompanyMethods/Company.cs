using GlobusLinkedinLib.Authentication;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;

namespace GlobusLinkedinLib.LinkedIn.Core.CompanyMethods
{
    public class Company
    {
        private XmlDocument XmlResult;

        public Company()
        {
            XmlResult = new XmlDocument();
        }

        public XmlDocument Get_CompanyProfileById(oAuthLinkedIn OAuth, string CoampanyPageId)
        {
            //string url = "https://api.linkedin.com/v1/companies/" + CoampanyPageId + ":(id,name,email-domains,description,founded-year,end-year,locations,Specialties,website-url,status,employee-count-range,industries,company-type,logo-url,square-logo-url,blog-rss-url,num-followers,universal-name,locations:(description),locations:(is-headquarters),locations:(is-active),locations:(address),locations:(address:(street1)),locations:(address:(street2)),locations:(address:(city)),locations:(address:(state)),locations:(address:(postal-code)),locations:(address:(country-code)),locations:(address:(region-code)),locations:(contact-info),locations:(contact-info:(phone1)),locations:(contact-info:(phone2)),locations:(contact-info:(fax)))";
            string url = "https://api.linkedin.com/v1/companies/" + CoampanyPageId + ":(id,name,email-domains,description,founded-year,end-year,locations,Specialties,website-url,status,employee-count-range,industries,company-type,logo-url,square-logo-url,blog-rss-url,num-followers,universal-name)";


            string response = OAuth.APIWebRequest("GET", url, null);
            XmlResult.Load(new StringReader(response));
            return XmlResult;
        }
        public XmlDocument Get_CompanyUpdateById(oAuthLinkedIn OAuth, string CoampanyPageId)
        {
            string url = "https://api.linkedin.com/v1/companies/" + CoampanyPageId + "/updates";
            string response = OAuth.APIWebRequest("GET", url, null);
            XmlResult.Load(new StringReader(response));
            return XmlResult;
        }
        public string SetCommentOnPagePost(oAuthLinkedIn oauth, string PageId, string Updatekey, string comment)
        {
            string xml = "<?xml version=\"1.0\" encoding=\"UTF-8\"?>";
            xml += " <update-comment><comment>" + comment + "</comment></update-comment>";
            string url = "https://api.linkedin.com/v1/companies/" + PageId + "/updates/key=" + Updatekey + "/update-comments-as-company/";
            string response = oauth.APIWebRequest("POST", url, xml);
            return response;

        }


        public XmlDocument GetCommentOnPagePost(oAuthLinkedIn oauth, string Updatekey)
        {
            string url = "https://api.linkedin.com/v1/people/~/network/updates/key=" + Updatekey + "/update-comments/";
            string response = oauth.APIWebRequest("GET", url, null);
            XmlResult.Load(new StringReader(response));
            return XmlResult;
        }

        public XmlDocument GetLikeorNotOnPagePost(oAuthLinkedIn oauth, string Updatekey, string PageId)
        {
            string url = "https://api.linkedin.com/v1/companies/" + PageId + "/updates/key=" + Updatekey + "/is-liked/";
            //string url = "https://api.linkedin.com/v1/people/~/network/updates/key=" + Updatekey + "/is-liked/";
            string response = oauth.APIWebRequest("GET", url, null);
            XmlResult.Load(new StringReader(response));
            return XmlResult;
        }

        public string SetPostOnPage(oAuthLinkedIn oauth, string PageId, string post)
        {

            //string response1 = oauth.APIWebRequest("GET", GlobusLinkedinLib.App.Core.Global.GetCompanyUrl, null);
            //XmlDocument xmlCompany = new XmlDocument();
            //xmlCompany.Load(new StringReader(response1));
            //string xml = "<?xml version=\"1.0\" encoding=\"UTF-8\"?>";
            //xml += "<share><visibility><code>anyone</code></visibility><comment>"+post+"</comment></share>";

            string xml = "<?xml version=\"1.0\" encoding=\"UTF-8\"?><share><visibility><code>anyone</code></visibility><comment>" + post + "</comment></share>";


            string url = "https://api.linkedin.com/v1/companies/" + PageId + "/shares";
            string response = oauth.APIWebRequest("POST", url, xml);
            return response;

        }
        public string SetPostOnPageWithImage(oAuthLinkedIn oauth, string PageId, string imageurl, string post)
        {
            string xml = "<?xml version=\"1.0\" encoding=\"UTF-8\"?>";
            xml += " <share><visibility><code>anyone</code></visibility><comment>" + post + "</comment>";
            //xml += "<content><submitted-url>http://socioboard.com/</submitted-url><title>none</title> <submitted-image-url>" + imageurl + "</submitted-image-url></content></share>";
            xml += "<content><submitted-url>http://localhost:5334/</submitted-url><title>none</title> <submitted-image-url>" + imageurl + "</submitted-image-url></content></share>";


            string url = "https://api.linkedin.com/v1/companies/" + PageId + "/shares";
            string response = oauth.APIWebRequest("POST", url, xml);
            return response;

        }
        public string SetLikeUpdateOnPagePost(oAuthLinkedIn OAuth, string postid, string msg)
        {
            string xml = "<?xml version=\"1.0\" encoding=\"UTF-8\"?>";
            xml += " <is-liked>" + msg + "</is-liked>";

            string url = "https://api.linkedin.com/v1/people/~/network/updates/key=" + postid + "/is-liked";

            string response = OAuth.APIWebRequest("PUT", url, xml);
            return response;
        }

    }
}

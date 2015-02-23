using Api.Socioboard.Model;
using log4net;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Script.Services;
using System.Web.Services;
using System.Xml;

namespace Api.Socioboard.Services
{
    /// <summary>
    /// Summary description for CompanyDashboard
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class CompanyDashboard : System.Web.Services.WebService
    {

        ILog logger = LogManager.GetLogger(typeof(CompanyDashboard));
        // Domain.Socioboard.Domain.facebookpageevents ObjFbPageEvent = new Domain.Socioboard.Domain.facebookpageevents();
        private Companypage companypage = new Companypage();
        private CompanySocialProfilesRepository ObjCompanySocialProfilesRepository = new CompanySocialProfilesRepository();
        private Domain.Socioboard.Domain.CompanyProfiles ObjCompanyProfiles = new Domain.Socioboard.Domain.CompanyProfiles();



        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string getAllCompanyNames()
        {
            string output = string.Empty;
            try
            {
                List<string> companyNames = ObjCompanySocialProfilesRepository.getCompanyNames();
                return new JavaScriptSerializer().Serialize(companyNames);
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.StackTrace);
                return "Something Went Wrong";
            }
        }

        //[WebMethod]
        //[ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public Domain.Socioboard.Domain.CompanyProfiles AddCompanyInfo(string name)
        {
            string ret = string.Empty;
            Domain.Socioboard.Domain.CompanyProfiles companyProfile = new Domain.Socioboard.Domain.CompanyProfiles();
            companyProfile.Companyname = name;
            try
            {
                companyProfile.Fbprofileid = Getfbpage(name);
            }
            catch (Exception e) { }


            try
            {
                companyProfile.Instagramprofileid = GetInstagramPage(name);
            }
            catch (Exception e)
            {
            }
            try
            {
                companyProfile.Linkedinprofileid = GetLinkedinPage(name);
                //companyProfile.LinkedinProfileId = string.Empty;
            }
            catch (Exception e)
            {

                // throw;
            }
            try
            {
                companyProfile.Tumblrprofileid = GetTumblrPage(name);
            }
            catch (Exception e)
            {

                //throw;
            }
            try
            {
                companyProfile.Twitterprofileid = GetTwitterPage(name);
            }
            catch
            {
            }
            try
            {
                companyProfile.Youtubeprofileid = GetYoutubeChannel(name);
            }
            catch (Exception e)
            {

                //throw;
            }
            try
            {
                companyProfile.Gplusprofileid = GetGplusPage(name);
            }
            catch (Exception e)
            {

            }
            //try
            //{
            //    companyProfile.UserId = Guid.NewGuid().ToString();
            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine(ex.Message);
            //}
            companyProfile.Id = Guid.NewGuid();
            ObjCompanySocialProfilesRepository.AddcompanyProfileName(companyProfile);
            return companyProfile;
        }


        //Suresh
        #region  get company information from table
        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string getCompanyProfile(string keyword)
        {
            string output = string.Empty;
            Domain.Socioboard.Domain.CompanyProfiles companyProfile = new Domain.Socioboard.Domain.CompanyProfiles();
            try
            {
                Domain.Socioboard.Domain.CompanyProfiles objCompanyName = ObjCompanySocialProfilesRepository.SearchCompanyName(keyword);
                companyProfile.Userid = objCompanyName.Userid;
                companyProfile.Id = objCompanyName.Id;
                companyProfile.Companyname = objCompanyName.Companyname;
                try
                {
                    companyProfile.Fbprofileid = Getfbpage(objCompanyName.Fbprofileid);
                }
                catch (Exception e) { }



                if (!string.IsNullOrEmpty(objCompanyName.Instagramprofileid))
                {
                    try
                    {
                        companyProfile.Instagramprofileid = GetInstagramPage(objCompanyName.Instagramprofileid);
                    }
                    catch (Exception e)
                    {
                    }
                }
                if (!string.IsNullOrEmpty(objCompanyName.Linkedinprofileid))
                {
                    try
                    {
                        companyProfile.Linkedinprofileid = GetLinkedinPage(objCompanyName.Linkedinprofileid);
                        //companyProfile.LinkedinProfileId = string.Empty;
                    }
                    catch (Exception e)
                    {

                        // throw;
                    }
                }
                if (!string.IsNullOrEmpty(objCompanyName.Tumblrprofileid))
                {
                    try
                    {
                        companyProfile.Tumblrprofileid = GetTumblrPage(objCompanyName.Tumblrprofileid);
                    }
                    catch (Exception e)
                    {

                        //throw;
                    }
                }
                if (!string.IsNullOrEmpty(objCompanyName.Twitterprofileid))
                {
                    try
                    {
                        companyProfile.Twitterprofileid = GetTwitterPage(objCompanyName.Twitterprofileid);
                    }
                    catch
                    {
                    }
                }
                if (!string.IsNullOrEmpty(objCompanyName.Youtubeprofileid))
                {
                    try
                    {
                        companyProfile.Youtubeprofileid = GetYoutubeChannel(objCompanyName.Youtubeprofileid);
                    }
                    catch (Exception e)
                    {

                        //throw;
                    }
                }

                if (!string.IsNullOrEmpty(objCompanyName.Gplusprofileid))
                {
                    try
                    {
                        companyProfile.Gplusprofileid = GetGplusPage(objCompanyName.Gplusprofileid);
                    }
                    catch (Exception e)
                    {


                    }
                }
                return new JavaScriptSerializer().Serialize(companyProfile);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return "Something Went Wrong";
            }
        }


        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string SearchCompanyProfile(string keyword)
        {
            string output = string.Empty;

            try
            {
                Domain.Socioboard.Domain.CompanyProfiles objCompanyName = ObjCompanySocialProfilesRepository.SearchCompanyName(keyword);
                if (objCompanyName == null)
                {
                    objCompanyName = AddCompanyInfo(keyword);
                }
                return new JavaScriptSerializer().Serialize(objCompanyName);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return "Something Went Wrong";
            }
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string GetCompanyProfile(string id)
        {
            string output = string.Empty;
            try
            {
                Domain.Socioboard.Domain.CompanyProfiles objCompanyProfiles = ObjCompanySocialProfilesRepository.getCompanyProfiles(Guid.Parse(id));

                return new JavaScriptSerializer().Serialize(objCompanyProfiles);
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.StackTrace);
                return "Something Went Wrong";
            }
        }


        public string Getfbpage(string id)
        {
            string output = string.Empty;
            try
            {

                JObject fbPage = JObject.Parse(companypage.SearchFacebookPage(id));
                output = fbPage["id"].ToString();
            }
            catch { }
            return output;
        }


        public string GetTwitterPage(string id)
        {
            string output = string.Empty;
            try
            {
                JObject twitterPage = JObject.Parse(companypage.TwitterSearch(id));
                output = twitterPage["screen_name"].ToString();
            }
            catch { }
            return output;

        }


        public string GetLinkedinPage(string id)
        {
            string output = string.Empty;
            try
            {

                XmlNode ResultCompany = null;
                int followers = 0;
                string result = string.Empty;
                result = companypage.LinkedinSearch(id);
                XmlDocument XmlResult = new XmlDocument();
                XmlResult.Load(new StringReader(result));
                XmlNodeList Companies = XmlResult.SelectNodes("company-search/companies/company");
                foreach (XmlNode node in Companies)
                {
                    if (Convert.ToInt32(node.SelectSingleNode("num-followers").InnerText) > followers)
                    {
                        ResultCompany = node;
                        followers = Convert.ToInt32(node.SelectSingleNode("num-followers").InnerText);
                    }
                }



                output = ResultCompany.SelectSingleNode("universal-name").InnerText;
            }
            catch { }
            return output;
        }






        public string GetInstagramPage(string id)
        {
            string output = string.Empty;
            try
            {
                JObject instagramPage = JObject.Parse(companypage.getInstagramCompanyPage(id));


                output = instagramPage["data"]["username"].ToString();


            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.StackTrace);
                // return "Something Went Wrong";
            }
            return output;

        }



        public string GetTumblrPage(string id)
        {
            string output = string.Empty;
            try
            {
                JObject tumblrPage = JObject.Parse(companypage.TumblrSearch(id));
                output = tumblrPage["response"]["blog"]["url"].ToString();
            }
            catch { }
            return output;

        }










        public string GetGplusPage(string id)
        {
            string output = string.Empty;

            JObject GplusPage = JObject.Parse(companypage.GooglePlusSearch(id));
            try
            {

                output = GplusPage["id"].ToString();
            }
            catch (Exception e) { }

            return output;

        }






        public string GetYoutubeChannel(string id)
        {

            string result = string.Empty;
            result = companypage.YoutubeSearch(id);
            if (!result.StartsWith("["))
                result = "[" + result + "]";
            JArray youtubechannels = JArray.Parse(result);
            JObject resultPage = (JObject)youtubechannels[0];
            JObject YtubeChannel = (JObject)resultPage["items"][0];
            try
            {
                result = YtubeChannel["id"].ToString();
            }
            catch (Exception e) { }
            return result;

        }




        # endregion

        #region insert data to tables


        # endregion
        //Suresh 

    }
}

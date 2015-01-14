using Api.Socioboard.Model;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Script.Services;
using System.Web.Services;

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
        CompanySocialProfilesRepository ObjCompanySocialProfilesRepository = new CompanySocialProfilesRepository();
        Domain.Socioboard.Domain.CompanyProfiles ObjCompanyProfiles = new Domain.Socioboard.Domain.CompanyProfiles();
       // facebookPageInfoRepository ObjfacebookPageInfoRepository = new facebookPageInfoRepository();
        //Domain.Socioboard.Domain.facebookpageinfo Objfacebookpageinfo = new Domain.Socioboard.Domain.facebookpageinfo();
        //TwitterPageRepository ObjTwitterPageRepository = new TwitterPageRepository();
       // Domain.Socioboard.Domain.twitterpage Objtwitterpage = new Domain.Socioboard.Domain.twitterpage();
       // LinkedinPageRepository ObjLinkedinPageRepository = new LinkedinPageRepository();
       // GooglePlusInfoRepository ObjGooglePlusInfoRepository = new GooglePlusInfoRepository();
        //InstagramPageRepository ObjInstagramPageRepository = new InstagramPageRepository();
        //YoutubePageRepository ObjYoutubePageRepository = new YoutubePageRepository();




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

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string AddCompanyInfo(string name)
        {
            string ret = string.Empty;
            Domain.Socioboard.Domain.CompanyProfiles companyProfile = new Domain.Socioboard.Domain.CompanyProfiles();
            companyProfile.CompanyName = name;
            companyProfile.FbProfileId = "2356418";

            //try
            //{
            //    companyProfile.Id = Guid.Parse("0x7014C11B81087847B94B4E3A12EE97D1");
            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine(ex.Message);
            //}
            companyProfile.InstagramProfileId = string.Empty;
            companyProfile.LinkedinProfileId = string.Empty;
            companyProfile.TumblrProfileId = string.Empty;
            companyProfile.TwitterProfileId = string.Empty;
            companyProfile.YoutubeProfileId = string.Empty;
            companyProfile.GPlusProfileId = string.Empty;
            try
            {
                companyProfile.UserId = Guid.Parse("0xDFBD9BD7AB3D95448B807395E38F5C4C");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            ObjCompanySocialProfilesRepository.AddcompanyProfileName(companyProfile);
            return ret;
        }
        ////Anjani Choubey
        //[WebMethod]
        //[ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        //public bool addFbPageInfo(Domain.Socioboard.Domain.facebookpageinfo fbpageinfo) 
        //{
        //    bool output = false;
        //    try
        //    {
        //       output =  ObjfacebookPageInfoRepository.AddFbPageCompanyInfo(fbpageinfo);
               
        //    }
        //    catch (Exception ex)
        //    {
        //        logger.Error(ex.StackTrace);
        //        output = false;
        //    }
        //    return output;
        //}
        //[WebMethod]
        //[ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        //public bool AddTwitterPageInfo(Domain.Socioboard.Domain.twitterpage TwitterPageInfo)
        //{
        //    bool ret = false;
        //    try
        //    {
        //        ret = ObjTwitterPageRepository.AddTwitterPageInfo(TwitterPageInfo);
                
        //    }
        //    catch (Exception ex)
        //    {
        //        logger.Error(ex.StackTrace);
        //        ret = false;
        //    }
        //    return ret;
        //}
        //[WebMethod]
        //[ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        //public bool AddLinkedinPageInfo(Domain.Socioboard.Domain.linkedinpage LinkedinPageInfo)
        //{
        //    bool ret = false;
        //    try
        //    {
        //        ret = ObjLinkedinPageRepository.AddLinkedinPageInfo(LinkedinPageInfo);

        //    }
        //    catch (Exception ex)
        //    {
        //        logger.Error(ex.StackTrace);
        //        ret = false;
        //    }
        //    return ret;
        //}
        //[WebMethod]
        //[ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        //public bool AddInstagramPageInfo(Domain.Socioboard.Domain.instagrampage InstagramPageInfo)
        //{
        //    bool ret = false;
        //    try
        //    {
        //        ret = ObjInstagramPageRepository.AddInstagramPageInfo(InstagramPageInfo);

        //    }
        //    catch (Exception ex)
        //    {
        //        logger.Error(ex.StackTrace);
        //        ret = false;
        //    }
        //    return ret;
        //}
        //[WebMethod]
        //[ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        //public bool AddGPlusPageInfo(Domain.Socioboard.Domain.googleplusinfo GPlusPageInfo)
        //{
        //    bool ret = false;
        //    try
        //    {
        //        ret = ObjGooglePlusInfoRepository.AddGPlusCompanyInfo(GPlusPageInfo);

        //    }
        //    catch (Exception ex)
        //    {
        //        logger.Error(ex.StackTrace);
        //        ret = false;
        //    }
        //    return ret;
        //}
        //[WebMethod]
        //[ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        //public bool AddYoutubePageInfo(Domain.Socioboard.Domain.youtubepage YoutubePageInfo)
        //{
        //    bool ret = false;
        //    try
        //    {
        //        ret = ObjYoutubePageRepository.AddYoutubePageInfo(YoutubePageInfo);

        //    }
        //    catch (Exception ex)
        //    {
        //        logger.Error(ex.StackTrace);
        //        ret = false;
        //    }
        //    return ret;
        //}
        ////Anjani Choubey

        //Suresh
        //#region  get company information from table

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string SearchCompanyProfile(string keyword)
        {
            string output = string.Empty;

            try
            {
                Domain.Socioboard.Domain.CompanyProfiles objCompanyName = ObjCompanySocialProfilesRepository.SearchCompanyName(keyword);

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
        //[WebMethod]
        //[ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        //public string Getfbpage(Guid id)
        //{
        //    string output = string.Empty;
        //    try
        //    {
        //        Domain.Socioboard.Domain.facebookpageinfo objFbPage = ObjfacebookPageInfoRepository.getFbPageInfo(id);
               
        //        return new JavaScriptSerializer().Serialize(objFbPage);
        //    }
        //    catch (Exception ex)
        //    {

        //        Console.WriteLine(ex.StackTrace);
        //        return "Something Went Wrong";
        //    }
            
            
        //}
        //[WebMethod]
        //[ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        //public string GetTwitterPage(Guid id)
        //{
        //    string output = string.Empty;
        //    try
        //    {
        //        Domain.Socioboard.Domain.twitterpage  objTwitterPage = ObjTwitterPageRepository.getTwitterPageInfo(id);
                
        //        return new JavaScriptSerializer().Serialize(objTwitterPage);
        //    }
        //    catch (Exception ex)
        //    {

        //        Console.WriteLine(ex.StackTrace);
        //        return "Something Went Wrong";
        //    }
            
            
        //}
        //[WebMethod]
        //[ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        //public string GetLinkedinPage(Guid id)
        //{
        //    string output = string.Empty;
        //    try
        //    {
        //        Domain.Socioboard.Domain.linkedinpage objLinkedinPage =ObjLinkedinPageRepository.getLinkedinPageInfo(id);

        //        return new JavaScriptSerializer().Serialize(objLinkedinPage);
        //    }
        //    catch (Exception ex)
        //    {

        //        Console.WriteLine(ex.StackTrace);
        //        return "Something Went Wrong";
        //    }
           
        //}
        //[WebMethod]
        //[ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        //public string GetTumblrPage(Guid id)
        //{
        //    string output = string.Empty;

        //    //TODO: logic to retrive tumblr of company
        //    return output;
        //}
        //[WebMethod]
        //[ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        //public string GetGplusPage(Guid id)
        //{
        //    string output = string.Empty;
        //    try
        //    {
        //        Domain.Socioboard.Domain.googleplusinfo  ObjGplusInfo= ObjGooglePlusInfoRepository.getGooglePlusPageInfo(id);

        //        return new JavaScriptSerializer().Serialize(ObjGplusInfo);
        //    }
        //    catch (Exception ex)
        //    {

        //        Console.WriteLine(ex.StackTrace);
        //        return "Something Went Wrong";
        //    }
            
        //}
        //[WebMethod]
        //[ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        //public string GetYoutubePage(Guid id)
        //{
        //    string output = string.Empty;

        //    try
        //    {
        //        Domain.Socioboard.Domain.youtubepage ObjYoutubePage = ObjYoutubePageRepository.getYoutubePageInfo(id);
        //        return new JavaScriptSerializer().Serialize(ObjYoutubePage);
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex.StackTrace);
        //        return "Something Went Wrong";
        //    }
        //}
        //[WebMethod]
        //[ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        //public string GetInstagramPage(Guid id)
        //{
        //    string output = string.Empty;

        //    try
        //    {
        //        Domain.Socioboard.Domain.instagrampage ObjInstagramPage = ObjInstagramPageRepository.getInstagramPageInfo(id);
        //        return new JavaScriptSerializer().Serialize(ObjInstagramPage);
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex.StackTrace);
        //        return "Something Went Wrong";
        //    }
        //}
        //#endregion



        #region insert data to tables


        # endregion
        //Suresh 

    }
}

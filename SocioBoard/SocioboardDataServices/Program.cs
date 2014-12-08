using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.IO;
using Domain.Socioboard.Domain;
using System.Web.Script.Serialization;
using System.Configuration;
using log4net;

namespace SocioboardDataServices
{
    class Program
    {
        ISocialSiteData objSocialSiteData;
        static void Main(string[] args)
        {
            Program prog = new Program();
            prog.StartService(args);
            
        }
        public static void ErrorLog(string sPathName, string sErrMsg)
        {
            StreamWriter sw = new StreamWriter(sPathName, true);
            sw.WriteLine(sErrMsg);
            sw.Flush();
            sw.Close();
        }
         private static void RunDataService(string profiletype)
        {
            string LogPath = ConfigurationManager.AppSettings["LogPath"].ToString();
            DirectoryInfo di = Directory.CreateDirectory(LogPath);
            string filename = LogPath+ "\\" + "Log_" + DateTime.Now.ToString("dd-MM-yyyy") + ".txt";
             while (true)
            {
             
             if (profiletype.Equals("Page"))
             {
                 clsFacebookDataScraper objFacebookDataScraper = new clsFacebookDataScraper();
                 Api.SocialProfile.SocialProfile ApiobjSocialProfile = new Api.SocialProfile.SocialProfile();
                 Api.FacebookAccount.FacebookAccount ApiObjFacebookAccount = new Api.FacebookAccount.FacebookAccount();
                 List<Domain.Socioboard.Domain.FacebookAccount> lstFacebookAccount = new List<Domain.Socioboard.Domain.FacebookAccount>();
                 try
                 {
                     //lstSocialProfile = (List<Domain.Socioboard.Domain.SocialProfile>)(new JavaScriptSerializer().Deserialize(ApiobjSocialProfile.SocialProfileByProfilType(profiletype.ToString()), typeof(List<Domain.Socioboard.Domain.SocialProfile>)));
                     lstFacebookAccount = (List<Domain.Socioboard.Domain.FacebookAccount>)(new JavaScriptSerializer().Deserialize(ApiObjFacebookAccount.getFacebookAccountDetailByprofileType(profiletype.ToString()), typeof(List<Domain.Socioboard.Domain.FacebookAccount>)));                   
                 }
                 catch (Exception ex)
                 {

                     Console.WriteLine(ex.Message);
                     ErrorLog(filename, ex.Message);
                 }
                 
                 ThreadPool.SetMaxThreads(10, 4);
                 if (lstFacebookAccount != null)
                 {
                     if (lstFacebookAccount.Count != 0)
                     {
                         foreach (var item in lstFacebookAccount)
                         {
                             try
                             {
                                 clsSocialSiteDataFeedsFactory objclsSocialSiteDataFeedsFactory = new clsSocialSiteDataFeedsFactory(item.ProfileType);
                                 ISocialSiteData objSocialSiteDataFeeds = objclsSocialSiteDataFeedsFactory.CreateSocialSiteDataFeedsInstance();
                                 FacebookData ObjFacebookData = new FacebookData();
                                 if (objSocialSiteDataFeeds != null)
                                 {
                                     try
                                     {
                                         item.ProfileType.Equals("page");
                                         objFacebookDataScraper.GetFbPost(item.UserId.ToString(),item.FbUserId);
                                         //Console.WriteLine(ObjFacebookData.GetPageData((object)item.UserId, item.ProfileId));
                                         Console.WriteLine("facebook fanpage updated successfully");
                                     }
                                     catch (Exception ex)
                                     {
                                         Console.WriteLine(ex.Message);
                                         ErrorLog(filename, ex.Message);
                                     }
                                 }
                             }
                             catch (Exception ex)
                             {
                                 Console.WriteLine(ex.Message);
                                 ErrorLog(filename, ex.Message);
                             }
                         }
                     }
                     else
                     {

                         Console.WriteLine("No active record in Database");
                     }
                 }
                 else
                 {
                     Console.WriteLine("No active record in Database");
                 }

                 Thread.Sleep(15 * 1000);
             }
             else
             {
                 Api.SocialProfile.SocialProfile ApiobjSocialProfile = new Api.SocialProfile.SocialProfile();
                 List<Domain.Socioboard.Domain.SocialProfile> lstSocialProfile = new List<Domain.Socioboard.Domain.SocialProfile>();
                 try
                 {
                      lstSocialProfile = (List<Domain.Socioboard.Domain.SocialProfile>)(new JavaScriptSerializer().Deserialize(ApiobjSocialProfile.SocialProfileByProfilType(profiletype.ToString()), typeof(List<Domain.Socioboard.Domain.SocialProfile>)));
                 }
                 catch (Exception ex)
                 {
                     
                    Console.WriteLine(ex.Message);
                     ErrorLog(filename, ex.Message);
                 }
                 ThreadPool.SetMaxThreads(10, 4);
                 if (lstSocialProfile != null)
                 {
                     if (lstSocialProfile.Count != 0)
                     {
                         foreach (var item in lstSocialProfile)
                         {
                             try
                             {
                                 clsSocialSiteDataFeedsFactory objclsSocialSiteDataFeedsFactory = new clsSocialSiteDataFeedsFactory(item.ProfileType);
                                 ISocialSiteData objSocialSiteDataFeeds = objclsSocialSiteDataFeedsFactory.CreateSocialSiteDataFeedsInstance();
                                 if (objSocialSiteDataFeeds != null)
                                 {
                                     Console.WriteLine(objSocialSiteDataFeeds.GetData((object)item.UserId, item.ProfileId));
                                 }
                             }
                             catch (Exception ex)
                             {
                                 Console.WriteLine(ex.Message);
                                 ErrorLog(filename, ex.Message);
                             }
                         }
                     }
                     else
                     {

                         Console.WriteLine("No active record in Database");
                     }
                 }
                 else
                 {
                     Console.WriteLine("No active record in Database");
                 }
             }
             
            
                

                Thread.Sleep(15*1000);
            }
        }

        void StartService(string[] args)
        {
            string check = string.Empty;
             //string[] str = { };
            try
            {
                check = args[0];
            }
            catch (Exception)
            {
                check = null;
            }
            if (string.IsNullOrEmpty(check))
            {
                Console.WriteLine("1. Facebook");
                Console.WriteLine("2. Twitter");
                Console.WriteLine("3. Linkedin");
                Console.WriteLine("4. Instagram");
                Console.WriteLine("5. Tumblr");
                Console.WriteLine("6. Youtube");
                Console.WriteLine("7. Facebook Page");
                string[] str = {Console.ReadLine()};

                string profileType = str[0];

                switch (profileType)
                {
                    case "1":
                        profileType = "facebook";
                        break;
                    case "2":
                        profileType = "twitter";
                        break;
                    case "3":
                        profileType = "linkedin";
                        break;
                    case "4":
                        profileType = "instagram";
                        break;
                    case "5":
                        profileType = "tumblr";
                        break;
                    case "6":
                        profileType = "youtube";
                        break;
                    case "7":
                        profileType = "Page";
                        break;
                    default:
                        break;
                }

                RunDataService(profileType);
            }
           
           
        }
        
    }
}

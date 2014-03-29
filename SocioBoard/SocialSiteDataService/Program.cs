using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using SocioBoard.Helper;
using SocioBoard.Model;
using SocioBoard.Domain;
using GlobusTwitterLib.Authentication;
using System.Collections;
using GlobusLinkedinLib.Authentication;
using System.IO;
using System.Windows.Forms;

namespace SocialSiteDataService
{
    class Program
    {
        public Program()
        {
            try
            {
                log4net.Config.XmlConfigurator.Configure();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error : " + ex.StackTrace);
            }
        }
        static void Main(string[] args)
        {
            try
            {
                log4net.Config.XmlConfigurator.Configure();
                string dirPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\SocialScoupDataServiceSetup";
                string path = dirPath + "\\logs.txt";
                if (!File.Exists(path))
                {
                    //  File.Create(path);
                }
                GlobusLogHelper.log.Error("data");
                GlobusLogHelper.log.Info("dcwcw");
                SocialSiteData objSiteData = new SocialSiteData();
                Thread thread = new Thread(objSiteData.GetSocialSiteData);
                thread.Start();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error : " + ex.StackTrace);
            }
        }
    }

    
    public class SocialSiteData
    {
        public void GetSocialSiteData()
        {

            try
            {
                string dirPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\SocialScoupDataServiceSetup";
                string path = dirPath + "\\hibernate.cfg.xml";
                string startUpFilePath = Application.StartupPath + "\\hibernate.cfg.xml";
                Console.Write(dirPath + " " + path + " " + startUpFilePath);
                if (!Directory.Exists(dirPath))
                {
                    Directory.CreateDirectory(dirPath);
                }

                if (!File.Exists(path))
                {
                    File.Copy(startUpFilePath, path);
                }
                SessionFactory.configfilepath = path;
                SocialProfilesRepository objSocioRepo = new SocialProfilesRepository();


                //string path = System.IO.Path.GetFullPath("hibernate.cfg.xml");

                //  SessionFactory.configfilepath = path;
                NHibernate.ISession session = SessionFactory.GetNewSession();
                new Thread(() =>
                {
                    while (true)
                    {
                        ThreadPool.SetMaxThreads(10, 4);
                        List<SocialProfile> lstUser = objSocioRepo.getAllSocialProfiles();
                        if (lstUser != null)
                        {
                            if (lstUser.Count != 0)
                            {
                                foreach (var item in lstUser)
                                {
                                    switch (item.ProfileType)
                                    {

                                        case "twitter":
                                            try
                                            {
                                                TwitterData objTwitter = new TwitterData();
                                                ThreadPool.QueueUserWorkItem(new WaitCallback(objTwitter.getTwitterData), (object)item.UserId);
                                            }
                                            catch (Exception err)
                                            {
                                                Console.Write(err.StackTrace);
                                            }
                                            break;
                                        case "facebook":
                                            try
                                            {
                                                FacebookData objFacebook = new FacebookData();
                                                ThreadPool.QueueUserWorkItem(new WaitCallback(objFacebook.GetFacebookData), (object)item.UserId);
                                                // objFacebook.GetFacebookData((object)item.UserId);
                                            }
                                            catch (Exception err)
                                            {
                                                Console.Write(err.StackTrace);
                                            }
                                            break;
                                        case "linkedin":
                                            try
                                            {
                                                LinkedInData objLi = new LinkedInData();
                                                ThreadPool.QueueUserWorkItem(new WaitCallback(objLi.GetLinkedIndata), (object)item.UserId);
                                            }
                                            catch (Exception err)
                                            {
                                                Console.Write(err.StackTrace);
                                            }
                                            break;
                                        case "instagram": try
                                            {
                                                InstagramData objIns = new InstagramData();
                                                ThreadPool.QueueUserWorkItem(new WaitCallback(objIns.getIntagramImages), (object)item.UserId);
                                            }
                                            catch (Exception err)
                                            {
                                                Console.Write(err.StackTrace);
                                            }
                                            break;
                                        case "googleanalytics": try
                                            {
                                                GoogleAnalyticsData gaData = new GoogleAnalyticsData();
                                                ThreadPool.QueueUserWorkItem(new WaitCallback(gaData.getAnalytics), (object)item.UserId);
                                            }
                                            catch (Exception ex)
                                            {
                                                Console.WriteLine(ex.StackTrace);
                                            }
                                            break;
                                        case "googleplus": try
                                            {
                                                GplusData gpData = new GplusData();
                                                ThreadPool.QueueUserWorkItem(new WaitCallback(gpData.getGplusData), (object)item.UserId);
                                            }
                                            catch (Exception ex)
                                            {
                                                Console.WriteLine(ex.StackTrace);
                                            }
                                            break;
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

                        Thread.Sleep(1000 * 60 * 15);
                    }
                }).Start();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error Case Debug : " + ex.StackTrace);
                Console.WriteLine("Error Case Debug : " + ex.Message);
                GlobusLogHelper.log.Error("Error : " + ex.StackTrace);
            }
        }
    }

    public class GlobusLogAppender : log4net.Appender.AppenderSkeleton
    {
        private static readonly object lockerLog4AppendNew = new object();

        //MainWindow objMainWindow = MainWindow.mainFormReference;
        protected override void Append(log4net.Core.LoggingEvent loggingEvent)
        {
            try
            {
                string loggerName = loggingEvent.Level.Name;
                // EbayBin objMainWindow = EbayBin.EbayBinRef;
                lock (lockerLog4AppendNew)
                {
                    switch (loggingEvent.Level.Name)
                    {
                        case "DEBUG":
                            try
                            {

                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine("Error Case Debug : " + ex.StackTrace);
                                Console.WriteLine("Error Case Debug : " + ex.Message);
                                GlobusLogHelper.log.Error("Error : " + ex.StackTrace);
                            }
                            break;
                        case "INFO":
                            try
                            {

                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine("Error Case INFO : " + ex.StackTrace);
                                Console.WriteLine("Error Case INFO : " + ex.Message);
                                GlobusLogHelper.log.Error("Error : " + ex.StackTrace);
                            }
                            break;
                        default:
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                GlobusLogHelper.log.Error("Error : " + ex.StackTrace);
            }

        }
    }
}

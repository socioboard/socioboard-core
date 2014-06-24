using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.IO;
using SocioBoard.Helper;
using SocioBoard.Model;
using System.Windows.Forms;
using SocioBoard.Domain;

namespace RssService
{
    class Program
    {
        static void Main(string[] args)
        {
            RssClass objSiteData = new RssClass();
            Thread thread = new Thread(objSiteData.GetSocialSiteData);
            thread.Start();
        }
    }
    public class RssClass
    {

        public void GetSocialSiteData()
        {
            try
            {

                string dirPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\SocioBoardScheduler\SocioBoardScheduler\1.0.0.0";
                string path = dirPath + "\\hibernate.cfg.xml";
                string startUpFilePath = Application.StartupPath + "\\hibernate.cfg.xml";

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

                NHibernate.ISession session = SessionFactory.GetNewSession();
                new Thread(() =>
                {
                    while (true)
                    {
                        try
                        {
                            RssPosts objTwitter = new RssPosts();

                            CustomMultithreads multithreads = new CustomMultithreads(objTwitter.getRssFeeds, 10);

                            RssFeedsRepository rssFeedsRepo = new RssFeedsRepository();
                            IEnumerable<RssFeeds> lstRssFeeds = rssFeedsRepo.getAllActiveRssFeeds();
                            foreach (RssFeeds item in lstRssFeeds)
                            {
                                try
                                {
                                    multithreads.StartSingleMultithreadedMethod(item);
                                }
                                catch (Exception ex)
                                {
                                }
                            }
                            Thread.Sleep(1000 * 60);
                        }
                        catch (Exception ex)
                        {
                        }
                    }
                }).Start();

                new Thread(() =>
                {
                    while (true)
                    {
                        try
                        {

                            RssPosts objTwitter = new RssPosts();
                            CustomMultithreads multithreads = new CustomMultithreads(objTwitter.postRssFeeds, 1);
                            RssFeedsRepository rssFeedsRepo = new RssFeedsRepository();
                            IEnumerable<RssFeeds> lstRssFeeds = rssFeedsRepo.getAllActiveRssFeeds();
                            foreach (RssFeeds item in lstRssFeeds)
                            {
                                try
                                {

                                    multithreads.StartSingleMultithreadedMethod(item);
                                }
                                catch (Exception ex)
                                {
                                }
                            }
                            Thread.Sleep(1000 * 60);
                        }
                        catch (Exception ex)
                        {
                        }
                    }
                }).Start();
            }

            catch (Exception ex)
            {
            }
        }
    }
}

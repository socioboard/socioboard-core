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
        struct StructSeeds
        {
            public string url { get; set; }
            public System.Timers.Timer timer { get; set; }
            public int interval { get; set; }
        }

        static readonly object locker_queueSeeds = new object();
        static Queue<StructSeeds> queueSeeds = new Queue<StructSeeds>();
        static List<StructSeeds> listSeeds = new List<StructSeeds>();

        public int GetRssFeeds(object objParameters)
        {
            StructSeeds structSeeds = (StructSeeds)objParameters;

            RssPosts objTwitter = new RssPosts();
            objTwitter.getRssFeeds(structSeeds.url);

            structSeeds.timer.Interval = structSeeds.interval;
            structSeeds.timer.Start();

            return 0;
        }

        void timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            StructSeeds structSeeds;
            lock (locker_queueSeeds)
            {
                if (queueSeeds.Count==0)
                {
                    foreach (var seed in listSeeds)
                    {
                        queueSeeds.Enqueue(seed);
                    }
                }
                structSeeds = queueSeeds.Dequeue();
            }
            GetRssFeeds(structSeeds);
        }

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

                            //CustomMultithreads multithreads = new CustomMultithreads(objTwitter.getRssFeeds, 2);
                            CustomMultithreads multithreads = new CustomMultithreads(GetRssFeeds, 2);

                            RssFeedsRepository rssFeedsRepo = new RssFeedsRepository();
                            IEnumerable<RssFeeds> lstRssFeeds = rssFeedsRepo.getAllActiveRssFeeds();

                            foreach (RssFeeds item in lstRssFeeds)
                            {
                                try
                                {
                                    StructSeeds structSeeds = new StructSeeds();
                                    structSeeds.url = item.FeedUrl;
                                    structSeeds.interval = 15000;//item.Duration
                                    structSeeds.timer = new System.Timers.Timer(15000);//item.Duration
                                    structSeeds.timer.Elapsed += new System.Timers.ElapsedEventHandler(timer_Elapsed);

                                    listSeeds.Add(structSeeds);
                                    queueSeeds.Enqueue(structSeeds);

                                    multithreads.StartSingleMultithreadedMethod(structSeeds);
                                }
                                catch (Exception ex)
                                {
                                }
                            }

                            //foreach (RssFeeds item in lstRssFeeds)
                            //{
                            //    try
                            //    {
                            //        multithreads.StartSingleMultithreadedMethod(item);
                            //    }
                            //    catch (Exception ex)
                            //    {
                            //    }
                            //}
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

        //public void GetSocialSiteData()
        //{
        //    try
        //    {

        //        string dirPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\SocioBoardScheduler\SocioBoardScheduler\1.0.0.0";
        //        string path = dirPath + "\\hibernate.cfg.xml";
        //        string startUpFilePath = Application.StartupPath + "\\hibernate.cfg.xml";

        //        if (!Directory.Exists(dirPath))
        //        {
        //            Directory.CreateDirectory(dirPath);
        //        }

        //        if (!File.Exists(path))
        //        {
        //            File.Copy(startUpFilePath, path);
        //        }
        //        SessionFactory.configfilepath = path;
        //        SocialProfilesRepository objSocioRepo = new SocialProfilesRepository();

        //        NHibernate.ISession session = SessionFactory.GetNewSession();
        //        new Thread(() =>
        //        {
        //            while (true)
        //            {
        //                try
        //                {
        //                    RssPosts objTwitter = new RssPosts();

        //                    CustomMultithreads multithreads = new CustomMultithreads(objTwitter.getRssFeeds, 10);

        //                    RssFeedsRepository rssFeedsRepo = new RssFeedsRepository();
        //                    IEnumerable<RssFeeds> lstRssFeeds = rssFeedsRepo.getAllActiveRssFeeds();
        //                    foreach (RssFeeds item in lstRssFeeds)
        //                    {
        //                        try
        //                        {
        //                            multithreads.StartSingleMultithreadedMethod(item);
        //                        }
        //                        catch (Exception ex)
        //                        {
        //                        }
        //                    }
        //                    Thread.Sleep(1000 * 60);
        //                }
        //                catch (Exception ex)
        //                {
        //                }
        //            }
        //        }).Start();

        //        new Thread(() =>
        //        {
        //            while (true)
        //            {
        //                try
        //                {

        //                    RssPosts objTwitter = new RssPosts();
        //                    CustomMultithreads multithreads = new CustomMultithreads(objTwitter.postRssFeeds, 1);
        //                    RssFeedsRepository rssFeedsRepo = new RssFeedsRepository();
        //                    IEnumerable<RssFeeds> lstRssFeeds = rssFeedsRepo.getAllActiveRssFeeds();
        //                    foreach (RssFeeds item in lstRssFeeds)
        //                    {
        //                        try
        //                        {

        //                            multithreads.StartSingleMultithreadedMethod(item);
        //                        }
        //                        catch (Exception ex)
        //                        {
        //                        }
        //                    }
        //                    Thread.Sleep(1000 * 60);
        //                }
        //                catch (Exception ex)
        //                {
        //                }
        //            }
        //        }).Start();
        //    }

        //    catch (Exception ex)
        //    {
        //    }
        //}
    }
}

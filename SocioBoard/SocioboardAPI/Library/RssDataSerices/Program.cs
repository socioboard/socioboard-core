using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace RssDataSerices
{
    class Program
    {
        static void Main(string[] args)
        {
            Program prog = new Program();
            prog.StartScheduler(args);
        }
        void StartScheduler(string[] args)
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
                //Console.WriteLine("3. Linkedin");
                //Console.WriteLine("4. Instagram");
                //Console.WriteLine("5. Tumblr");
                //Console.WriteLine("6. Facebook_Group");
                //Console.WriteLine("7. Linkedin_Group");
                string[] str = { Console.ReadLine() };

                string profileType = str[0];

                switch (profileType)
                {
                    case "1":
                        profileType = "facebook";
                        break;
                    case "2":
                        profileType = "twitter";
                        break;
                    //case "3":
                    //    profileType = "linkedin";
                    //    break;
                    //case "4":
                    //    profileType = "instagram";
                    //    break;
                    //case "5":
                    //    profileType = "tumblr";
                    //    break;
                    //case "6":
                    //    profileType = "facebookgroup";
                    //    break;
                    //case "7":
                    //    profileType = "linkedingroup";
                    //    break;
                    default:
                        break;
                }


                if (!string.IsNullOrEmpty(profileType))
                {
                    PostRssData(profileType);
                }
                else
                {
                    UpdateRss();
                }

            }
        }

        public static void PostRssData(string ProfileType)
        {
            //Thread thread_PostRssDataThreadMethod = new Thread(PostRssDataThreadMethod);
            //thread_PostRssDataThreadMethod.Start(ProfileType);
            PostRssDataThreadMethod(ProfileType);

        }

        private static void PostRssDataThreadMethod(object objProfileType)
        {
            while (true)
            {
                string strProfileType = (string)objProfileType;
                RssDataScheduler objRssDataScheduler = new RssDataScheduler();
                Console.WriteLine(objRssDataScheduler.PostRssFeed(strProfileType));
                Thread.Sleep(1000*1*60);
            }
        }
        public static void UpdateRss()
        {
        //    Thread thread_UpdateRssThreadMethod = new Thread(UpdateRssThreadMethod);
        //    thread_UpdateRssThreadMethod.Start();
            UpdateRssThreadMethod();
        }

        private static void UpdateRssThreadMethod()
        {
            while (true)
            {
                RssDataService objRssDataService = new RssDataService();
                Console.WriteLine(objRssDataService.UpdateRss());
                Thread.Sleep(1000*2*60);
            }
        }



    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BoardMeDataServices.Api.BoardServices;
using System.Threading;

namespace BoardMeDataServices
{
    class Program
    {
        static void Main(string[] args)
        {
            Program prog = new Program();
            prog.StartService(args);
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
                Console.WriteLine("3. GPlus");
                Console.WriteLine("4. Instagram");
               // Console.WriteLine("5. Tumblr");
               // Console.WriteLine("6. Youtube");
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
                    case "3":
                        profileType = "gplus";
                        break;
                    case "4":
                        profileType = "instagram";
                        break;
                    //case "5":
                    //    profileType = "tumblr";
                    //    break;
                    //case "6":
                    //    profileType = "youtube";
                    //    break;

                    default:
                        break;
                }

                RunDataService(profileType);
            }


        }

        private static void RunDataService(string profiletype)
        {
            BoardServices boardservices = new BoardServices();
            while (true)
            {
                ThreadPool.SetMaxThreads(10, 4);
                try
                {
                    if (profiletype.Equals("facebook"))
                    {
                        boardservices.UpdateFbFeeds("kaiohal;asfiojaasjklkn_jkjk___jkskfjkh988");
                    }
                    if (profiletype.Equals("twitter"))
                    {
                        boardservices.UpdateTwitterFeeds("kaiohal;asfiojaasjklkn_jkjk___jkskfjkh988");
                    }
                    if (profiletype.Equals("gplus"))
                    {
                        boardservices.UpdateGplusFeeds("kaiohal;asfiojaasjklkn_jkjk___jkskfjkh988");
                    }
                    if (profiletype.Equals("instagram"))
                    {
                        boardservices.UpdateInstagramFeeds("kaiohal;asfiojaasjklkn_jkjk___jkskfjkh988");
                    }
                }
                catch (Exception ex)
                {
                    
                }
                Thread.Sleep(50 * 1000);
            }
        }
    }
}

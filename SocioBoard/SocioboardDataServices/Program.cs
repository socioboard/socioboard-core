using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Web.Script.Serialization;


namespace SocioboardDataServices
{
    class Program
    {
        ISocialSiteData objSocialSiteData;
        static void Main(string[] args)
        {
            Program prog = new Program();
            prog.StartService(args);
            //NegativeFeeds objNegativeFeeds = new NegativeFeeds();
            //objNegativeFeeds.getAllNegativeFeeds();
        }
         private static void RunDataService(string profiletype)
        {
            while (true)
            {
             Api.SocialProfile.SocialProfile ApiobjSocialProfile=new Api.SocialProfile.SocialProfile ();
             List<Domain.Socioboard.Domain.SocialProfile> lstSocialProfile = (List<Domain.Socioboard.Domain.SocialProfile>)(new JavaScriptSerializer().Deserialize(ApiobjSocialProfile.SocialProfileByProfilType(profiletype.ToString()), typeof(List<Domain.Socioboard.Domain.SocialProfile>)));
            
                ThreadPool.SetMaxThreads(10, 4);           
                if (lstSocialProfile != null)
                {
                    if (lstSocialProfile.Count != 0)
                    {
                        foreach (var item in lstSocialProfile)
                        {
                            try
                            {
                                if (item.UserId.ToString()!="bbc23ca1-28f1-452c-a3a8-e58c5cc5a0f8")
                                {
                                    continue;
                                }

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

                    default:
                        break;
                }

                RunDataService(profileType);
            }
           
           
        }
        
    }
}

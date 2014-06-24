using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using SocioBoard.Helper;
using SocioBoard.Model;
using SocioBoard.Domain;

namespace blackSheepScheduler
{
    class Program
    {
        static void Main(string[] args)
        {
            ShareMessage objShareMsg = new ShareMessage();
            Thread thread = new Thread(objShareMsg.shareMessage);
            thread.Start();
        }

        public class ShareMessage
        {
            public void shareMessage()
            {

                string path = System.IO.Path.GetFullPath("hibernate.cfg.xml");
                SessionFactory.configfilepath = path;
                Console.WriteLine("Before Session");
                NHibernate.ISession session = SessionFactory.GetNewSession();
                Console.WriteLine("Session Created");
                ScheduledMessageRepository schrepo = new ScheduledMessageRepository();

                new Thread(() =>
                {
                    while (true)
                    {
                        IEnumerable<ScheduledMessage> lstmsg = schrepo.getAllMessage();
                       

                        if (lstmsg != null)
                        {
                            if (lstmsg.Count() != 0)
                            {
                                foreach (var item in lstmsg)
                                {
                                    if (DateTime.Compare(DateTime.Now.AddHours(-5), item.ScheduleTime) >= 0)
                                    {
                                        string media = item.ProfileType;

                                        switch (media)
                                        {
                                            case "twitter":
                                                TwitterScheduler twtscheduler = new TwitterScheduler();
                                                Thread thread_TwtMailSend = new Thread(() => { twtscheduler.PostScheduleMessage(item); });
                                                thread_TwtMailSend.Start();

                                                break;
                                            case "facebook":
                                                FacebookScheduler facescheduler = new FacebookScheduler();

                                                Thread thread_FaceBook = new Thread(() => { facescheduler.PostScheduleMessage(item); });
                                                thread_FaceBook.Start();

                                                break;
                                            case "linkedin":
                                                LinkedInScheduler linkedscheduler = new LinkedInScheduler();
                                                Thread thread_LinkedIn = new Thread(() => { linkedscheduler.PostScheduleMessage(item); });
                                                thread_LinkedIn.Start();

                                                break;


                                        }
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

                        Thread.Sleep(10 * 12000);
                    }
                }).Start();
            }
        }
    }
}

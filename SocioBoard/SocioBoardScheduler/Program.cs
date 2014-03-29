using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SocioBoard;
using System.Threading;
using SocioBoard.Model;
using SocioBoard.Domain;
using SocioBoard.Helper;
using System.IO;
using System.Windows.Forms;

namespace SocioBoardScheduler
{
    public class Program
    {
        static void Main(string[] args)
        {
            try
            {
                ShareMessage objShareMsg = new ShareMessage();
                Thread thread = new Thread(objShareMsg.shareMessage);
                thread.Start();

                #region For Testing
                //objShareMsg.shareMessage(); 
                #endregion
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error : " + ex.StackTrace);
            }
        }

        public class ShareMessage
        {
            public void shareMessage()
            {

                try
                {


                    //string path = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\SocioBoardScheduler\SocioBoardScheduler\1.0.0.0\hibernate.cfg.xml";//Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\hibernate.cfg.xml";////System.IO.Path.GetFullPath("hibernate.cfg.xml");
                    string dirPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\SocialScoup\";
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
                    SessionFactory.configfilepath = path; //@"D:\Ajay\New_SocialBoard\New folder\socioboard\trunk\SocialScoup\hibernate.cfg.xml";// For Testing
                    NHibernate.ISession session = SessionFactory.GetNewSession();
                    ScheduledMessageRepository schrepo = new ScheduledMessageRepository();

                    new Thread(() =>
                    {
                        // while (true)
                        {
                            try
                            {
                                IEnumerable<ScheduledMessage> lstmsg = schrepo.getAllMessage();

                                if (lstmsg != null)
                                {
                                    if (lstmsg.Count() != 0)
                                    {
                                        foreach (var item in lstmsg)
                                        {

                                            try
                                            {
                                                string media = item.ProfileType;



                                                switch (media)
                                                {
                                                    case "twitter1":
                                                        try
                                                        {
                                                            TwitterScheduler twtscheduler = new TwitterScheduler();
                                                            ScheduledMessage twtSch = item;
                                                            //Thread thread_TwtMailSend = new Thread(() => { twtscheduler.PostScheduleMessage(twtSch); });
                                                            //thread_TwtMailSend.Start();


                                                            #region For Testing
                                                            twtscheduler.PostScheduleMessage(twtSch);
                                                            #endregion
                                                        }
                                                        catch (Exception ex)
                                                        {
                                                            Console.WriteLine(ex.StackTrace);
                                                        }
                                                        break;

                                                    case "facebook":
                                                        try
                                                        {
                                                            FacebookScheduler facescheduler = new FacebookScheduler();
                                                            ScheduledMessage fbSch = item;
                                                            // Thread thread_FaceBook = new Thread(() => { facescheduler.PostScheduleMessage(fbSch); });
                                                            // thread_FaceBook.Start();

                                                            #region For Testing
                                                            facescheduler.PostScheduleMessage(fbSch);
                                                            #endregion
                                                        }
                                                        catch (Exception ex)
                                                        {
                                                            Console.WriteLine(ex.StackTrace);
                                                        }
                                                        break;

                                                    case "linkedin1":
                                                        try
                                                        {
                                                            LinkedInScheduler linkedscheduler = new LinkedInScheduler();
                                                            ScheduledMessage linkedinSch = item;
                                                            Thread thread_LinkedIn = new Thread(() => { linkedscheduler.PostScheduleMessage(linkedinSch); });
                                                            thread_LinkedIn.Start();


                                                            #region For Testing
                                                            //linkedscheduler.PostScheduleMessage(linkedinSch);  
                                                            #endregion

                                                        }
                                                        catch (Exception ex)
                                                        {
                                                            Console.WriteLine(ex.StackTrace);
                                                        }
                                                        break;
                                                }


                                            }
                                            catch (Exception ex)
                                            {
                                                Console.WriteLine(ex.StackTrace);
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
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex.StackTrace);
                            }
                        }


                    }).Start();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.StackTrace);
                }
            }
        }
    }
}

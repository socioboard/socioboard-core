using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SocioBoard.Model;
using SocioBoard.Domain;
using SocioBoard.Helper;
using System.IO;
using System.Windows.Forms;
using System.Configuration;
using System.Threading;

namespace NewsLetterSheduler
{
    class Program
    {

        static System.Timers.Timer timer = new System.Timers.Timer();

        [STAThread]
        static void Main(string[] args)
        {
            try
            {
                HibernateConfiguration();

                InitialiseAndStartTimer();

                //StartProcess();
                Console.ReadLine();
                //Thread.Sleep(30 * 1000 * 50);
            }
            catch (Exception ex)
            {
                Console.WriteLine("error : " + ex.Message);
            }
        }

        private static void InitialiseAndStartTimer()
        {
            try
            {
                timer = new System.Timers.Timer();
                timer.Elapsed +=new System.Timers.ElapsedEventHandler(timer_Elapsed);
                timer.Interval = 5000;
                timer.Enabled = true;
                timer.Start();
            }
            catch (Exception ex)
            {
                Console.WriteLine("error : " + ex.Message);
            }
        }

        static void timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            try
            {
                StopTimer();

                StartProcess();
                //test();

                InitialiseAndStartTimer();
            }
            catch (Exception ex)
            {
                Console.WriteLine("error : " + ex.Message);
            }
        }

        private static void StopTimer()
        {
            try
            {
                timer.Stop();
                timer.Dispose();
            }
            catch (Exception ex)
            {
                Console.WriteLine("error : " + ex.Message);
            }
        }

        private static void test()
        {
            Console.WriteLine(DateTime.Now);
            Thread.Sleep(3000);
        }

        static void timer_Tick(object sender, EventArgs e)
        {
            try
            {
                timer.Stop();
                timer.Dispose();
                //StartProcess();
                test();
                //timer.Start();
            }
            catch (Exception ex)
            {
                Console.WriteLine("error : " + ex.Message);
            }
           
        }

        public static void HibernateConfiguration()
        {
            try
            {
                string dirPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\NewsLetterSheduler\";
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
            }
            catch (Exception ex)
            {
                Console.WriteLine("error : " + ex.Message);
            }
        }

        public static void StartProcess()
        {
            try
            {


                //UserRepository objUserRepository = new UserRepository();
                //User objUser = objUserRepository.getUsersById(Guid.Parse("62ebeaa8-0148-4e30-84fc-bf1af515dbf8"));
                NewsLetter objNewsLetter = new NewsLetter();
                NewsLetterRepository objNewsLetterRepository = new NewsLetterRepository();
                List<NewsLetter> lstNewsLetter = objNewsLetterRepository.GetAllNewsLetterByDate(DateTime.Now);

                foreach (NewsLetter item in lstNewsLetter)
                {
                    try
                    {
                        SendMail(item);
                    }
                    catch (Exception ex)
                    {

                        Console.WriteLine("Error : " + ex.StackTrace);
                    }

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("error : " + ex.Message);
            }
        }

        public static void SendMail(NewsLetter objNewsLetter)
        {

            try
            {
                string username = ConfigurationSettings.AppSettings["username"];
                string host = ConfigurationSettings.AppSettings["host"];
                string port = ConfigurationSettings.AppSettings["port"];
                string pass = ConfigurationSettings.AppSettings["password"];
                string from = ConfigurationSettings.AppSettings["fromemail"];
                //string sss = ConfigurationSettings.AppSettings["host"];

                UserRepository objUserRepository = new UserRepository();
                User objUser = objUserRepository.getUsersById(objNewsLetter.UserId);

                MailHelper.SendSendGridMail(host, Convert.ToInt32(port), from, "", objUser.EmailId, string.Empty, string.Empty, objNewsLetter.Subject, objNewsLetter.NewsLetterDetail, username, pass);
                NewsLetterRepository objNewsLetterRepository = new NewsLetterRepository();
                objNewsLetter.SendStatus = true;
                objNewsLetterRepository.UpdateNewsLetter(objNewsLetter);
                
            }
            catch (Exception ex)
            {

                Console.WriteLine("Error : " + ex.StackTrace);
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.IO;
using System.Windows.Forms;
using SocioBoard.Helper;
using SocioBoard.Domain;
using SocioBoard.Model;
using System.Configuration;
using GlobusMailLib;
using Mandrill;
using System.Text.RegularExpressions;

namespace InvitationScheduler
{
    class Program
    {
        static System.Timers.Timer timer = new System.Timers.Timer();

        static List<string> lstMandrillAccount = new List<string>();

        static int countmandrillAccount = 0;
        static int countMaxMailSendBy1Account = 0;

        static string mandrillAccount = string.Empty;

        static string host = string.Empty;
        static string port = string.Empty;
        static string pass = string.Empty;
        static string mandrillEmail = string.Empty;

        [STAThread]
        static void Main(string[] args)
        {
            try
            {
                lstMandrillAccount = ReadMandrillAccount();

                HibernateConfiguration();

                try
                {
                    Thread objSetInvitaionByRejectInfoStatus = new Thread(SetInvitaionByRejectInfoStatus);
                    objSetInvitaionByRejectInfoStatus.Start();
                    //SetInvitaionByRejectInfoStatus();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("error : " + ex.Message);
                }

                try
                {
                    Thread objResendMail = new Thread(ResendMail);
                    objResendMail.Start();
                    //SetInvitaionByRejectInfoStatus();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("error : " + ex.Message);
                }



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
                timer.Elapsed += new System.Timers.ElapsedEventHandler(timer_Elapsed);
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
                Console.WriteLine("Hibernate Configuration Process Starting...");

                string dirPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\DevInvitationScheduler\";
                //string dirPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\InvitationScheduler\";

                string path = dirPath + "\\hibernate.cfg.xml";
                string startUpFilePath = Application.StartupPath + "\\hibernate.cfg.xml";

                if (!Directory.Exists(dirPath))
                {
                    Directory.CreateDirectory(dirPath);
                }

                //if (!File.Exists(path))
                {
                    File.Copy(startUpFilePath, path,true);
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
                Console.WriteLine("Get Invitation Info where Status is 0  Process Starting...");

                Invitation objInvitation = new Invitation();
                InvitationRepository objInvitationRepository = new InvitationRepository();

                objInvitation.Status = "0";
                List<Invitation> lstInvitation = objInvitationRepository.GetInvitationInfoByStatus(objInvitation);

                Console.WriteLine("Count Invitation Info where Status is 0 : " + lstInvitation.Count);

                foreach (Invitation item in lstInvitation)
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

        


        public static void SendMail(Invitation Invitation)
        {

            try
            {
                //Invitation = new SocioBoard.Domain.Invitation();



                Console.WriteLine("SendMail Process Starting...");

                string username = ConfigurationSettings.AppSettings["username"];
                //string host = ConfigurationSettings.AppSettings["host"];
                //string port = ConfigurationSettings.AppSettings["port"];
                //string pass = ConfigurationSettings.AppSettings["password"];
                //string from = ConfigurationSettings.AppSettings["fromemail"];

                host = ConfigurationSettings.AppSettings["Mandrillhost"];

                port = ConfigurationSettings.AppSettings["Mandrillport"];

                pass = ConfigurationSettings.AppSettings["Mandrillpassword"];


                ManageMandrillAccount();

                Console.WriteLine(host + ":" + port + ":" + mandrillEmail+":"+pass);
                


                //from = ConfigurationManager.AppSettings["fromemail"];

                //GlobusMailLib.MailHelper objMailHelper = new GlobusMailLib.MailHelper();


                //MailHelper.SendSendGridMail(host, Convert.ToInt32(port), from, "", objUser.EmailId, string.Empty, string.Empty, objNewsLetter.Subject, objNewsLetter.NewsLetterDetail, username, pass);

                //MailHelper.SendSendGridMail(host, Convert.ToInt32(port), Invitation.SenderEmail, "", Invitation.FriendEmail, "", "", Invitation.Subject, Invitation.InvitationBody, username, pass);

                GlobusMailLib.MailHelper objMailHelper = new GlobusMailLib.MailHelper();

                //Invitation.FriendEmail
                string retStatus = objMailHelper.GetStatusFromSendMailByMandrill(host, Convert.ToInt32(port), Invitation.SenderEmail, "", Invitation.FriendEmail, "", "", Invitation.Subject, Invitation.InvitationBody, username, pass);

                Console.WriteLine("Mandrill Sent Mail Status : " + retStatus);


                if (retStatus.ToLower().Contains("reject") )
                {
                  Invitation.Status ="rejected";
                }
                else if(retStatus.ToLower().Contains("invalid"))
                {
                    Invitation.Status ="invalid";
                }
                else if (retStatus.ToLower().Contains("sent"))
                {
                    //Invitation.Status = "sent";

                    Invitation.Status = "1";
                }
                else if (retStatus.ToLower().Contains("queued"))
                {
                    Invitation.Status = "queued";
                }
                else if (retStatus.ToLower().Contains("scheduled"))
                {
                    Invitation.Status = "scheduled";
                }
                else 
                {
                    Invitation.Status = retStatus;
                }

                countMaxMailSendBy1Account++;

                Console.WriteLine("Mail Sent to : " + Invitation.FriendEmail);


                //Invitation objInvitation = new Invitation();
                InvitationRepository objInvitationRepository = new InvitationRepository();

                
                objInvitationRepository.SetInvitationStatusById(Invitation);



            }
            catch (Exception ex)
            {

                Console.WriteLine("Error : " + ex.StackTrace);
            }
        }


        public static void ManageMandrillAccount()
        {
            try
            {
                if (countMaxMailSendBy1Account == 0 || countMaxMailSendBy1Account == 1000)
                {
                    if (countmandrillAccount == lstMandrillAccount.Count - 1)
                    {
                        countmandrillAccount = 0;
                    }

                    mandrillAccount = lstMandrillAccount[countmandrillAccount];

                    if (countMaxMailSendBy1Account == 1000)
                    {
                        countmandrillAccount++;

                        countMaxMailSendBy1Account = 0;
                    }
                }

                if (!string.IsNullOrEmpty(mandrillAccount))
                {
                    string[] arrmandrillAccount = Regex.Split(mandrillAccount, "<:>");

                    if (arrmandrillAccount.Length > 3)
                    {
                        host = arrmandrillAccount[0];
                        port = arrmandrillAccount[1];
                        mandrillEmail = arrmandrillAccount[2];
                        pass = arrmandrillAccount[3];
                    }
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine("Error : " + ex.StackTrace);
            }
        }

        public static int UpdateInvitationStatusById(Invitation Invitation, string status)
        {
            int res = 0;
            try
            {
                //Invitation objInvitation = new Invitation();
                InvitationRepository objInvitationRepository = new InvitationRepository();

                if (status.ToLower().Contains("sent"))
                {
                    Invitation.Status = "10";
                }
                else if (status.ToLower().Contains("reject"))
                {
                    Invitation.Status = "2";
                }
                else if (status.ToLower().Contains("spam"))
                {
                    Invitation.Status = "3";
                }
                else if (status.ToLower().Contains("unsubscribe"))
                {
                    Invitation.Status = "4";
                }
                else if (status.ToLower().Contains("hard"))
                {
                    Invitation.Status = "5";
                }
                else if (status.ToLower().Contains("soft"))
                {
                    Invitation.Status = "6";
                }
                //else
                //{
                //    Invitation.Status = "11";
                //}

                res = objInvitationRepository.SetInvitationStatusOnlyById(Invitation);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error : " + ex.StackTrace);
            }
            return res;
        }

        

        public static void SetInvitaionByRejectInfoStatus()
        {

            try
            {
                while (true)
                {
                    try
                    {
                        Console.WriteLine("SetInvitaionByRejectInfoStatus Process Starting...");

                        string username = ConfigurationSettings.AppSettings["username"];
                        //string host = ConfigurationSettings.AppSettings["host"];
                        //string port = ConfigurationSettings.AppSettings["port"];
                        //string pass = ConfigurationSettings.AppSettings["password"];
                        //string from = ConfigurationSettings.AppSettings["fromemail"];

                        //username = ConfigurationManager.AppSettings["Mandrillusername"];
                        host = ConfigurationSettings.AppSettings["Mandrillhost"];

                        port = ConfigurationSettings.AppSettings["Mandrillport"];

                        pass = ConfigurationSettings.AppSettings["Mandrillpassword"];

                        ManageMandrillAccount();

                        Console.WriteLine(host + ":" + port + ":" + mandrillEmail + ":" + pass);

                        //from = ConfigurationManager.AppSettings["fromemail"];


                        //MailHelper.SendSendGridMail(host, Convert.ToInt32(port), from, "", objUser.EmailId, string.Empty, string.Empty, objNewsLetter.Subject, objNewsLetter.NewsLetterDetail, username, pass);

                        //MailHelper.SendSendGridMail(host, Convert.ToInt32(port), Invitation.SenderEmail, "", Invitation.FriendEmail, "", "", Invitation.Subject, Invitation.InvitationBody, username, pass);

                        GlobusMailLib.MailHelper objMailHelper = new GlobusMailLib.MailHelper();

                        List<Mandrill.RejectInfo> ri = objMailHelper.GetListRejectInfoByMandrill(host, Convert.ToInt32(port), "", "", "", "", "", "", "", username, pass);

                        Console.WriteLine("Rejected Mail Count : " + ri.Count);

                        string status = string.Empty;



                        Invitation objInvitation = new Invitation();
                        InvitationRepository objInvitationRepository = new InvitationRepository();
                        //Program objProgram = new Program();

                        foreach (RejectInfo item in ri)
                        {
                            try
                            {
                                objInvitation.FriendEmail = item.Email;
                                List<Invitation> lstInvitation = objInvitationRepository.GetInvitationInfoByFriendEmail(objInvitation);
                                status = item.Reason;

                                if (lstInvitation.Count > 0)
                                {
                                    if (lstInvitation[0].Status != "2" && lstInvitation[0].Status != "3" && lstInvitation[0].Status != "4" && lstInvitation[0].Status != "5" && lstInvitation[0].Status != "6")
                                    {
                                        int res = UpdateInvitationStatusById(lstInvitation[0], status);

                                        Console.WriteLine("Status : " + status + " Changed to Email : " + objInvitation.FriendEmail);
                                    }
                                }
                            }
                            catch (Exception ex)
                            {

                                Console.WriteLine("Error : " + ex.StackTrace);
                            }
                        }

                        Thread.Sleep(30 * 60 * 1000);

                        //objInvitationRepository.SetInvitationStatusById(Invitation,);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error : " + ex.StackTrace);
                    }
                }

            }
            catch (Exception ex)
            {

                Console.WriteLine("Error : " + ex.StackTrace);
            }
        }


        public static void ResendMail()
        {
            try
            {
                while (true)
                {
                    Console.WriteLine("Get Invitation Info where Status 1  Process Starting...");

                    Invitation objInvitation = new Invitation();
                    InvitationRepository objInvitationRepository = new InvitationRepository();

                    objInvitation.Status = "1";
                    List<Invitation> lstInvitation = objInvitationRepository.GetInvitationInfoByStatus(objInvitation);

                    Console.WriteLine("Count Invitation Info where Status is 1 : " + lstInvitation.Count);

                    foreach (Invitation item in lstInvitation)
                    {
                        try
                        {
                            if (TimeDefferenceInDays(item.LastEmailSendDate) > 7)
                            {
                                SendMail(item);
                            }
                        }
                        catch (Exception ex)
                        {

                            Console.WriteLine("Error : " + ex.StackTrace);
                        }

                    }

                    Thread.Sleep(5 * 60 * 60 * 1000); // for 5 hrs delay
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("error : " + ex.Message);
            }
        }

        public static int TimeDefferenceInDays(DateTime dateTime)
        {
            int res = 0;
            try
            {
                res = (DateTime.Now - dateTime).Days;
            }
            catch (Exception ex)
            {
                Console.WriteLine("error : " + ex.Message);
            }
            return res;
        }

        public static List<string> ReadMandrillAccount()
        {
            List<string> lstMandrillAccount = new List<string>();
            try
            {

                //string dirPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\InvitationScheduler\";

                string dirPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\DevInvitationScheduler\";


                string path = dirPath + "\\Mandrillapi.txt";
                string startUpFilePath = Application.StartupPath + "\\Mandrillapi.txt";

                if (!Directory.Exists(dirPath))
                {
                    Directory.CreateDirectory(dirPath);
                }

                //if (!File.Exists(path))
                {
                    File.Copy(startUpFilePath, path,true);
                }

                // string path = "";//Give path in this format D:\Suresh\MyTest.txt

                // This text is added only once to the file.
                if (!File.Exists(startUpFilePath))
                {
                    // Create a file to write to.
                    //string[] createText = { "Hello", "Welcome to aspdotnet-suresh", "Reading and writing text file" };
                    //File.WriteAllLines(path, createText);


                    Console.WriteLine("File not exist on this path : " + path);
                }
                else
                {
                    lstMandrillAccount = ReadLargeFile(path);

                    Console.WriteLine("Total loaded mandrill Account : " + lstMandrillAccount.Count);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("error : " + ex.Message);
            }
            return lstMandrillAccount;
        }


        private static int _bufferSize = 16384;

        public static List<string> ReadLargeFile(string filename)
        {
            List<string> listFileContent = new List<string>();

            try
            {
                StringBuilder stringBuilder = new StringBuilder();
                using (FileStream fileStream = new FileStream(filename, FileMode.Open, FileAccess.Read))
                {
                    using (StreamReader streamReader = new StreamReader(fileStream))
                    {
                        char[] fileContents = new char[_bufferSize];
                        int charsRead = streamReader.Read(fileContents, 0, _bufferSize);

                        // Can't do much with 0 bytes
                        //if (charsRead == 0)
                        //    throw new Exception("File is 0 bytes");

                        while (charsRead > 0)
                        {
                            stringBuilder.Append(fileContents);
                            charsRead = streamReader.Read(fileContents, 0, _bufferSize);
                        }

                        string[] contentArray = stringBuilder.ToString().Split(new char[] { '\r', '\n' });
                        foreach (string line in contentArray)
                        {
                            //listFileContent.Add(line.Replace("#", "").Replace("\0", "").Replace(" ", " "));//listFileContent.Add(line.Replace("#", ""));
                            listFileContent.Add(line.Replace("\0", "").Replace(" ", " ").Trim());

                        }
                        listFileContent.RemoveAll(s => string.IsNullOrEmpty(s));
                        listFileContent.RemoveAll(s => s.Equals("\t"));
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("error : " + ex.Message);
            }
            return listFileContent;
        }
    }
}

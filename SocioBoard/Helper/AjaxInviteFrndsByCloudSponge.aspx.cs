using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.IO;
using System.Text.RegularExpressions;
using SocioBoard.Domain;
using SocioBoard.Model;
using CloudSponge;
using System.Threading;
using CloudSpongeLib;

namespace SocioBoard.Helper
{
    public partial class AjaxInviteFrndsByCloudSponge : System.Web.UI.Page
    {
        string returnresponse =string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {

                if (Request.QueryString != null)
                {
                    if (Request.QueryString["type"] == "GetMails")
                    {
                        returnresponse = GetFriendsEmail(Request.QueryString["hint"].ToString());
                       // GetFriendsEmail(Request.QueryString["hint"].ToString());

                    }

                    if (Request.QueryString["type"] == "sendselectedmail")
                    {

                        string str = Request.Form["selectedmail"].ToString();

                       // Response.Write(SendAllSelectedMail(Request.Form["selectedmail"].ToString()));
                        returnresponse =  SendAllSelectedMail(Request.Form["selectedmail"].ToString());
                    }


                    if (Request.QueryString["type"] == "sendmail")
                    {
                        string mail = Request.QueryString["mail"].ToString();
                       // Response.Write(InviteMember("", "", mail));
                        returnresponse =InviteMember("", "", mail);
                    }


                    if (Request.QueryString["type"] == "getContacts")
                    {

                        if (Session["api"] != null && Session["consent"] != null)
                        {
                            Api api = (Api)Session["api"];
                            ConsentResponse consent=(ConsentResponse)Session["consent"];
                            Session["api"] = null;
                            Session["consent"] = null;

                            List<string> lst=new List<string>();
                            string mails = GetMails(api, consent);

                            //Response.Write(mails);
                            returnresponse =  mails;
                        }
                    }

                }


            }
            catch (Exception ex)
            {
                Console.WriteLine("Error : " + ex.StackTrace);
            }
            Response.Write(returnresponse);
        }

        public int AddInvitationInDB(string fname, string lname, string email)
        {
            int res = 0;
            try
            {
               if (Session["LoggedUser"] != null)
                {
                    SocioBoard.Domain.User user = (User)Session["LoggedUser"];


                    MailHelper mailhelper = new MailHelper();
                    string mailpath = HttpContext.Current.Server.MapPath("~/Layouts/Mails/FriendInvitation.htm");
                    string html = File.ReadAllText(mailpath);
                    string fromemail = ConfigurationManager.AppSettings["fromemail"];
                    string usernameSend = ConfigurationManager.AppSettings["username"];
                    string host = ConfigurationManager.AppSettings["host"];
                    string port = ConfigurationManager.AppSettings["port"];
                    string pass = ConfigurationManager.AppSettings["password"];
                    string urllogin = "http://socioboard.com/Default.aspx";
                    //string registrationurl = "http://dev.socioboard.com/Registration.aspx?refid=256f9c69-6b6a-4409-a309-b1f6d1f8e43b";

                    string registrationurl = "http://dev.socioboard.com/Registration.aspx?refid=" + user.Id;

                    string Body = mailhelper.InvitationMailByCloudSponge(html, fname + " " + lname, user.EmailId, "", urllogin, registrationurl);

                    string Subject = "You've been Invited to Socioboard " + email + " Socioboard Account";


                    #region Add Records in Invitation Table
                    Invitation objInvitation = new Invitation();
                    InvitationRepository objInvitationRepository = new InvitationRepository();

                    objInvitation.Id = Guid.NewGuid();
                    objInvitation.InvitationBody = Body;
                    objInvitation.Subject = Subject;
                    objInvitation.FriendEmail = email;
                    objInvitation.SenderEmail = user.EmailId;//"Abhaykumar@globussoft.com";
                    objInvitation.FriendName = fname + " " + lname;
                    objInvitation.SenderName = user.UserName;//"Abhaykumar";
                    objInvitation.Status = "0";

                    res = objInvitationRepository.Add(objInvitation);
                    #endregion
                }
                //else
                //{
                //    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Plaese login for this activity!');", true);

                //}

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error : " + ex.StackTrace);
            }
            return res;
        }



        private string GetMails(Api api, ConsentResponse consent)
        {
            string emails = string.Empty;
            try
            {
                

                GatherEmailByCloudSponge objGatherEmailByCloudSponge = new GatherEmailByCloudSponge();

                //List<string> lstFriendsEmail = objGatherEmailByCloudSponge.GetFriendsEmail(mailtype);
                // List<string> lstFriendsEmail = objGatherEmailByCloudSponge.GetFriendsEmail(domainKey, domainPassword);

                List<string> lstFriendsEmail = GetContext(api, consent);

                foreach (string item in lstFriendsEmail)
                {
                    try
                    {
                        emails += item + "<:>";
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error : " + ex.StackTrace);
                    }
                }

                emails = emails.Remove(emails.LastIndexOf("<:>"));

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error : " + ex.StackTrace);
            }
            return emails;
        }

        private string SendAllSelectedMail(string mails)
        {
            string res = "";
            try
            {

               // emails.Remove(emails.LastIndexOf("<:>"));

                //string [] mailarray=(mails.Remove(mails.LastIndexOf("<:>"))).Split['<:>'];

                string finalMails = mails.Remove(mails.LastIndexOf("<:>"));

                string[] mailsArr=Regex.Split(finalMails, "<:>");
                foreach (string item in mailsArr)
                {
                    try
                    {
                        string[] namemailsArr = Regex.Split(item, "<~>");

                        //InviteMember(namemailsArr[0], namemailsArr[1], namemailsArr[2]);

                        int i=AddInvitationInDB(namemailsArr[0], namemailsArr[1], namemailsArr[2]);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error : " + ex.StackTrace);
                    }
                }

                //MailSender.SendInvitationEmail(team.FirstName + " " + team.LastName, user.UserName, team.EmailId, team.Id);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error : " + ex.StackTrace);
            }
            return res;

        }
        private string InviteMember(string fname,string lname, string email)
        {
            string res = "";
            try
            {
               

                Registration reg = new Registration();
                string tid = reg.MD5Hash(email);
                MailHelper mailhelper = new MailHelper();
                string mailpath = HttpContext.Current.Server.MapPath("~/Layouts/Mails/FriendInvitation.htm");
                string html = File.ReadAllText(mailpath);
                string fromemail = ConfigurationManager.AppSettings["fromemail"];
                string usernameSend = ConfigurationManager.AppSettings["username"];
                string host = ConfigurationManager.AppSettings["host"];
                string port = ConfigurationManager.AppSettings["port"];
                string pass = ConfigurationManager.AppSettings["password"];
                string urllogin = "http://socioboard.com/Default.aspx";
                string registrationurl = "http://dev.socioboard.com/Registration.aspx?refid=256f9c69-6b6a-4409-a309-b1f6d1f8e43b";
                string Body = mailhelper.InvitationMailByCloudSponge(html, fname+" "+lname, "Abhaykumar@globussoft.com", "", urllogin, registrationurl);

                string Subject = "You've been Invited to " + email + " Socioboard Account";
                //   MailHelper.SendMailMessage(host, int.Parse(port.ToString()), fromemail, pass, email, string.Empty, string.Empty, Subject, Body);
                MailHelper objMailHelper = new MailHelper();
                res = objMailHelper.SendMailByMandrill(host, Convert.ToInt32(port), fromemail, "", email, "", "", Subject, Body, usernameSend, pass);
              //MailHelper.SendSendGridMail(host, Convert.ToInt32(port), fromemail, "", email, "", "", Subject, Body, usernameSend, pass);

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error : " + ex.StackTrace);
            }
            return res;

        }

        public string GetFriendsEmail(string mailType)
        {
            string res = string.Empty;
            List<string> lstEmail = new List<string>();
            try
            {
                try
                {
                    //Client ID 	767237347813-0decm2cgu5sab8pr50rr0hmerh9e0q73.apps.googleusercontent.com
                    //Client secret 	DQNCzBuiwo0U58rkAbo38Jvs

                    //string domainKey = ConfigurationSettings.AppSettings["DomainKey"];
                    //string domainPassword = ConfigurationSettings.AppSettings["DomainPassword"];

                    string domainKey = "BTUU28Y8FTW4UTXL44B8";
                    string domainPassword = "Yes6cLblk3lf4jWt"; 

                    //var api = new Api(Settings.Default.DomainKey, Settings.Default.DomainPassword);

                    var api = new Api(domainKey, domainPassword);

                    //var api = new Api("QHBFU6D43BDCTQJT3XVZ", "vtFIAaMaGkSYVZMY");

                    //var consent = api.Consent(ContactSource.Gmail);

                    ConsentResponse consent = null;

                    if (mailType == "gmail")
                    {
                        consent = api.Consent(ContactSource.Gmail);
                    }

                    if (mailType == "yahoo")
                    {
                        consent = api.Consent(ContactSource.Yahoo);
                    }

                    if (mailType == "hotmail")
                    {
                        consent = api.Consent(ContactSource.WindowsLive);
                    }


                    //Process.Start("https://accounts.google.com/o/oauth2/auth?response_type=code&client_id=767237347813-0decm2cgu5sab8pr50rr0hmerh9e0q73.apps.googleusercontent.com&scope=https%3A%2F%2Fwww.google.com%2Fm8%2Ffeeds&redirect_uri=https%3A%2F%2Fapi.cloudsponge.com%2Fauth&state=import_id%3D30938548");


                    //Process.Start(consent.Url);
                    try
                    {
                        //new Thread(() =>
                        //{
                        //    GetContext(lstEmail, api, consent);
                        //}).Start();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error : " + ex.StackTrace);
                    }
                    try
                    {
                       // Response.Write(consent.Url);
                        //Response.Redirect(consent.Url);
                        //Response.Redirect(consent.Url, true);
                        //Server.Transfer(consent.Url);

                        ///open url in new tab using javascript
                        //string url = "https://www.google.co.in";
                        ////string s = "window.open('" + url + "', '_blank', 'width=300,height=100,left=100,top=100,resizable=yes');";
                        //string s = "window.open('" + url + "', '_blank');";
                        //ClientScript.RegisterStartupScript(this.GetType(), "script", s, true);

                        Session["api"] = api;
                        Session["consent"] = consent;

                        //Response.Write("Successcloudspongeseperator");
                        

                       // Response.Write(consent.Url);
                        //Response.Write("http://www.google.com");
                       res=consent.Url;

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error : " + ex.StackTrace);
                    }


                    
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error : " + ex.StackTrace);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error : " + ex.StackTrace);
            }
            return res;
        }

        private static List<string> GetContext(Api api, ConsentResponse consent)
        {
            List<string> lstEmail = new List<string>();
            try
            {
                bool complete = false;
                while (!complete)
                {
                    var events = api.Events(consent.ImportId);

                    foreach (var item in events.Events)
                        Console.WriteLine("{0}-{1}", item.Type, item.Status);

                    complete = events.IsComplete;
                }

                var contacts = api.Contacts(consent.ImportId);

                foreach (var item in contacts.Contacts)
                {
                    try
                    {
                        Console.WriteLine("{1}, {0} ({2})", item.FirstName, item.LastName, item.EmailAddresses.FirstOrDefault());
                        string emailAddresses = item.FirstName + "<~>" + item.LastName + "<~>" + item.EmailAddresses.FirstOrDefault();
                        lstEmail.Add(emailAddresses);
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

            return lstEmail;
        }
    }
}
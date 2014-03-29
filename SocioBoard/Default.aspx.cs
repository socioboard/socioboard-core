using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using log4net;
using SocioBoard.Domain;
using SocioBoard.Model;
using SocioBoard.Admin.Scheduler;
using SocioBoard.Helper;
using System.Configuration;
using CloudSpongeLib;
using System.Security.Cryptography;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;
using GlobusLinkedinLib.Authentication;

namespace SocialSuitePro
{
    public partial class Default : System.Web.UI.Page
    {
        ILog logger = LogManager.GetLogger(typeof(Default));
        protected void Page_Load(object sender, EventArgs e)
        {

            //linkedincompanypagetest();
            //Dictionary<string, string> allinfo = ReadLargeFile("D:/registration.txt");
            //UserRepository objUserRepository=new UserRepository ();
            //List<string> stremail = new List<string>();
            //List<User> obj = objUserRepository.getAllUsers();
            //foreach (User item in obj)
            //{
            //    stremail.Add(item.EmailId);
            //}
            //foreach (KeyValuePair<string, string> keyvalue in allinfo)
            //{
            //    if (!stremail.Contains(keyvalue.Key))
            //    {
            //        //stremail.Add(keyvalue.Key);
            //        btnRegister_Click(keyvalue.Key, keyvalue.Value);
            //    }   
            //}

            

            try
            {
                

                if (Session["isemailexist"] != null)
                {
                    if (Session["isemailexist"].ToString() == "emailnotexist")
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Your email id was not returned by Facebook graph API!');", true);
                        Session["isemailexist"] = null;
                        return;
                    }
                }
                if (Session["fblogout"] != null)
                {
                    if (Session["fblogout"].ToString() == "NOTACTIVATED")
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('You are Blocked by Admin Please contact Admin!');", true);
                        Session["fblogout"] = null;
                        return;
                    }
                }
                if (Request.QueryString != null)
                {
                    if (Request.QueryString["type"] == "logout")
                    {
                        Session.Abandon();
                        Session.Clear();
                    }

                }
                if (Session["LoggedUser"] != null)
                    Response.Redirect("Home.aspx");





               // Session.Clear();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                logger.Error(ex.StackTrace);
            }
            try
            {
                Session.Abandon();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                logger.Error(ex.StackTrace);
            }
            try
            {
                Session.RemoveAll();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                logger.Error(ex.StackTrace);
            }

            try
            {
                Session["profilesforcomposemessage"] = null;
                Session["CountMessages"] = null;
                Session.Abandon();
            }
            catch
            {
            }
            try
            {
                Session.RemoveAll();
            }
            catch (Exception ex)
            {
                logger.Error(ex.StackTrace);
            }

        }



        public Dictionary<string,string> ReadLargeFile(string filename)
        {

            Dictionary<string, string> finallist = new Dictionary<string, string>();

            System.IO.StreamReader myFile = new System.IO.StreamReader(filename);

            string myString = myFile.ReadToEnd();
            List<string> lines = File.ReadLines(filename).ToList<string>();

            foreach (string item in lines)
            {
                string ss = item.Replace("\t", "");


                string[] arrRegex=Regex.Split(ss, "<:>");
                finallist.Add(arrRegex[0], arrRegex[1]);
            }
            return finallist;

        }
        public string MD5Hash(string text)
        {
            MD5 md5 = new MD5CryptoServiceProvider();

            //compute hash from the bytes of text
            md5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(text));

            //get hash result after compute it
            byte[] result = md5.Hash;

            StringBuilder strBuilder = new StringBuilder();
            for (int i = 0; i < result.Length; i++)
            {
                //change it into 2 hexadecimal digits
                //for each byte
                strBuilder.Append(result[i].ToString("x2"));
            }

            return strBuilder.ToString();
        }
        protected void btnRegister_Click(string email,string username)
        {
            try
            {
                User user = new User();
                UserRepository userrepo = new UserRepository();
                UserActivation objUserActivation = new UserActivation();
                Coupon objCoupon = new Coupon();
                CouponRepository objCouponRepository = new CouponRepository();
                SocioBoard.Helper.SessionFactory.configfilepath = Server.MapPath("~/hibernate.cfg.xml");
                try
                {

                            user.PaymentStatus = "unpaid";
                            user.AccountType = AccountType.Premium.ToString();
                            user.CreateDate = DateTime.Now;
                            user.ExpiryDate = DateTime.Now.AddMonths(1);
                            user.Id = Guid.NewGuid();
                            user.UserName = username;
                            user.Password = this.MD5Hash("Sb1234!@#$");
                            user.EmailId = email;
                            user.UserStatus = 1;
                            user.ActivationStatus = "1";

                            if (!userrepo.IsUserExist(user.EmailId))
                            {



                               
                                UserRepository.Add(user);


                                Session["LoggedUser"] = user;
                                objUserActivation.Id = Guid.NewGuid();
                                objUserActivation.UserId = user.Id;
                                objUserActivation.ActivationStatus = "1";
                                UserActivationRepository.Add(objUserActivation);

                                //add package start

                                UserPackageRelation objUserPackageRelation = new UserPackageRelation();
                                UserPackageRelationRepository objUserPackageRelationRepository = new UserPackageRelationRepository();
                                PackageRepository objPackageRepository = new PackageRepository();

                                Package objPackage = objPackageRepository.getPackageDetails(user.AccountType);
                                objUserPackageRelation.Id = new Guid();
                                objUserPackageRelation.PackageId = objPackage.Id;
                                objUserPackageRelation.UserId = user.Id;
                                objUserPackageRelation.ModifiedDate = DateTime.Now;
                                objUserPackageRelation.PackageStatus = true;

                                objUserPackageRelationRepository.AddUserPackageRelation(objUserPackageRelation);

                                //end package

                                //SocioBoard.Helper.MailSender.SendEMail(txtFirstName.Text, txtPassword.Text, txtEmail.Text, user.AccountType.ToString(), user.Id.ToString());
                            }
                            else
                            {
                                //lblerror.Text = "Email Already Exists " + "<a href=\"Default.aspx\">login</a>";
                            }
                        
                   
                }

                catch (Exception ex)
                {
                    logger.Error(ex.StackTrace);
                   // lblerror.Text = "Please Insert Correct Information";
                    Console.WriteLine(ex.StackTrace);
                    //Response.Redirect("Home.aspx");
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex.StackTrace);

                Console.WriteLine(ex.StackTrace);
                //Response.Redirect("Home.aspx");
            }
        }
        private static string Test()
        {
            string emails = string.Empty;
            try
            {
                string domainKey = ConfigurationManager.AppSettings["DomainKey"];
                string domainPassword = ConfigurationManager.AppSettings["DomainPassword"];

                GatherEmailByCloudSponge objGatherEmailByCloudSponge = new GatherEmailByCloudSponge();

                List<string> lstFriendsEmail = objGatherEmailByCloudSponge.GetFriendsEmail();
                // List<string> lstFriendsEmail = objGatherEmailByCloudSponge.GetFriendsEmail(domainKey, domainPassword);

                

                foreach (string item in lstFriendsEmail)
                {
                    try
                    {
                        emails += item+"<:>";
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


        public void linkedincompanypagetest()
        {
            oAuthLinkedIn Linkedin_oauth = new oAuthLinkedIn();
            string authLink = Linkedin_oauth.AuthorizationLinkGet();
            Session["reuqestToken"] = Linkedin_oauth.Token;
            Session["reuqestTokenSecret"] = Linkedin_oauth.TokenSecret;
            Response.Redirect(authLink);
        }




        
    }
    
}
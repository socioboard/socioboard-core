using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using log4net;
using SocioBoard.Model;
using SocioBoard.Domain;
using System.Security.Cryptography;
using System.Text;
using SocioBoard.Helper;
using SocioBoard.Domain;
using SocioBoard.Model;
using SocioBoard.Helper;
using System.Security.Cryptography;
using System.Text;
using log4net;
using SocioBoard.Domain;
using log4net;
using System.Security.Cryptography;
using System.Text;
using SocioBoard.Helper;


namespace SocialSuitePro
{
    public partial class SocialRegisterPage : System.Web.UI.Page
    {
      //  ILog logger = LogManager.GetLogger(typeof(Registration));
       // ILog logger = LogManager.GetLogger(typeof(Registration));
        ILog logger = LogManager.GetLogger(typeof(SocialRegisterPage));
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!string.IsNullOrEmpty(Request.QueryString["tid"]))
                {
                    Guid teamid = Guid.Parse(Request.QueryString["tid"]);
                    TeamRepository teamRepo = new TeamRepository();
                    Team team = teamRepo.getTeamById(teamid);
                    txtFirstName.Text = team.FirstName;
                    txtLastName.Text = team.LastName;
                    txtEmail.Text = team.EmailId;
                    txtEmail.Enabled = false;

                }
            }
            SocioBoard.Helper.SessionFactory.configfilepath = Server.MapPath("~/hibernate.cfg.xml");
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

        protected void btnRegister_Click(object sender, ImageClickEventArgs e)
        {
            User user = new User();
            UserRepository userrepo = new UserRepository();
            SocioBoard.Helper.SessionFactory.configfilepath = Server.MapPath("~/hibernate.cfg.xml");
            try
            {
                if (txtPassword.Text == txtConfirmPassword.Text)
                {



                    user.PaymentStatus = "unpaid";
                    user.AccountType = Request.QueryString["type"];
                    if (user.AccountType == string.Empty)
                    {
                        user.AccountType = AccountType.Deluxe.ToString();
                    }
                    user.CreateDate = DateTime.Now;
                    user.ExpiryDate = DateTime.Now.AddMonths(1);
                    user.Id = Guid.NewGuid();
                    user.UserName = txtFirstName.Text + " " + txtLastName.Text;
                    user.Password = this.MD5Hash(txtPassword.Text);
                    user.EmailId = txtEmail.Text;
                    user.UserStatus = 1;
                    if (!userrepo.IsUserExist(user.EmailId))
                    {
                        UserRepository.Add(user);
                        SocioBoard.Helper.MailSender.SendEMail(txtFirstName.Text + " " + txtLastName.Text, txtPassword.Text, txtEmail.Text);

                        TeamRepository teamRepo = new TeamRepository();
                        Team team = teamRepo.getTeamByEmailId(txtEmail.Text);
                        if (team != null)
                        {

                            Guid teamid = Guid.Parse(Request.QueryString["tid"]);
                            teamRepo.updateTeamStatus(teamid);


                            TeamMemberProfileRepository teamMemRepo = new TeamMemberProfileRepository();
                            List<TeamMemberProfile> lstteammember = teamMemRepo.getAllTeamMemberProfilesOfTeam(team.Id);
                            foreach (TeamMemberProfile item in lstteammember)
                            {
                                try
                                {
                                    SocialProfilesRepository socialRepo = new SocialProfilesRepository();
                                    SocialProfile socioprofile = new SocialProfile();
                                    socioprofile.Id = Guid.NewGuid();
                                    socioprofile.ProfileDate = DateTime.Now;
                                    socioprofile.ProfileId = item.ProfileId;
                                    socioprofile.ProfileType = item.ProfileType;
                                    socioprofile.UserId = user.Id;
                                    socialRepo.addNewProfileForUser(socioprofile);

                                    if (item.ProfileType == "facebook")
                                    {
                                        try
                                        {
                                            FacebookAccount fbAccount = new FacebookAccount();
                                            FacebookAccountRepository fbAccountRepo = new FacebookAccountRepository();
                                            FacebookAccount userAccount = fbAccountRepo.getUserDetails(item.ProfileId);
                                            fbAccount.AccessToken = userAccount.AccessToken;
                                            fbAccount.EmailId = userAccount.EmailId;
                                            fbAccount.FbUserId = item.ProfileId;
                                            fbAccount.FbUserName = userAccount.FbUserName;
                                            fbAccount.Friends = userAccount.Friends;
                                            fbAccount.Id = Guid.NewGuid();
                                            fbAccount.IsActive = true;
                                            fbAccount.ProfileUrl = userAccount.ProfileUrl;
                                            fbAccount.Type = userAccount.Type;
                                            fbAccount.UserId = user.Id;
                                            fbAccountRepo.addFacebookUser(fbAccount);
                                        }
                                        catch (Exception ex)
                                        {
                                            Console.WriteLine(ex.Message);
                                            logger.Error(ex.Message);
                                        }
                                    }
                                    else if (item.ProfileType == "twitter")
                                    {
                                        try
                                        {
                                            TwitterAccount twtAccount = new TwitterAccount();
                                            TwitterAccountRepository twtAccRepo = new TwitterAccountRepository();
                                            TwitterAccount twtAcc = twtAccRepo.getUserInfo(item.ProfileId);
                                            twtAccount.FollowersCount = twtAcc.FollowersCount;
                                            twtAccount.FollowingCount = twtAcc.FollowingCount;
                                            twtAccount.Id = Guid.NewGuid();
                                            twtAccount.IsActive = true;
                                            twtAccount.OAuthSecret = twtAcc.OAuthSecret;
                                            twtAccount.OAuthToken = twtAcc.OAuthToken;
                                            twtAccount.ProfileImageUrl = twtAcc.ProfileImageUrl;
                                            twtAccount.ProfileUrl = twtAcc.ProfileUrl;
                                            twtAccount.TwitterName = twtAcc.TwitterName;
                                            twtAccount.TwitterScreenName = twtAcc.TwitterScreenName;
                                            twtAccount.TwitterUserId = twtAcc.TwitterUserId;
                                            twtAccount.UserId = user.Id;
                                            twtAccRepo.addTwitterkUser(twtAccount);
                                        }
                                        catch (Exception ex)
                                        {
                                            Console.WriteLine(ex.StackTrace);
                                            logger.Error(ex.Message);
                                        }
                                    }
                                    else if (item.ProfileType == "instagram")
                                    {
                                        try
                                        {

                                            InstagramAccount insAccount = new InstagramAccount();
                                            InstagramAccountRepository insAccRepo = new InstagramAccountRepository();
                                            InstagramAccount InsAcc = insAccRepo.getInstagramAccountById(item.ProfileId);
                                            insAccount.AccessToken = InsAcc.AccessToken;
                                            insAccount.FollowedBy = InsAcc.FollowedBy;
                                            insAccount.Followers = InsAcc.Followers;
                                            insAccount.Id = Guid.NewGuid();
                                            insAccount.InstagramId = item.ProfileId;
                                            insAccount.InsUserName = InsAcc.InsUserName;
                                            insAccount.IsActive = true;
                                            insAccount.ProfileUrl = InsAcc.ProfileUrl;
                                            insAccount.TotalImages = InsAcc.TotalImages;
                                            insAccount.UserId = user.Id;
                                            insAccRepo.addInstagramUser(insAccount);
                                        }
                                        catch (Exception ex)
                                        {
                                            Console.WriteLine(ex.StackTrace);
                                            logger.Error(ex.Message);
                                        }
                                    }
                                    else if (item.ProfileType == "linkedin")
                                    {
                                        try
                                        {
                                            LinkedInAccount linkAccount = new LinkedInAccount();
                                            LinkedInAccountRepository linkedAccountRepo = new LinkedInAccountRepository();
                                            LinkedInAccount linkAcc = linkedAccountRepo.getLinkedinAccountDetailsById(item.ProfileId);
                                            linkAccount.Id = Guid.NewGuid();
                                            linkAccount.IsActive = true;
                                            linkAccount.LinkedinUserId = item.ProfileId;
                                            linkAccount.LinkedinUserName = linkAcc.LinkedinUserName;
                                            linkAccount.OAuthSecret = linkAcc.OAuthSecret;
                                            linkAccount.OAuthToken = linkAcc.OAuthToken;
                                            linkAccount.OAuthVerifier = linkAcc.OAuthVerifier;
                                            linkAccount.ProfileImageUrl = linkAcc.ProfileImageUrl;
                                            linkAccount.ProfileUrl = linkAcc.ProfileUrl;
                                            linkAccount.UserId = user.Id;
                                            linkedAccountRepo.addLinkedinUser(linkAccount);
                                        }
                                        catch (Exception ex)
                                        {
                                            Console.WriteLine(ex.StackTrace);
                                            logger.Error(ex.Message);
                                        }




                                    }

                                }
                                catch (Exception ex)
                                {
                                    logger.Error(ex.Message);
                                }



                            }
                        }


                        lblerror.Text = "Registered Successfully !" + "<a href=\"Default.aspx\">Login</a>";
                    }
                    else
                    {
                        lblerror.Text = "Email Already Exists " + "<a href=\"Default.aspx\">login</a>";
                    }
                }

            }
            catch (Exception ex)
            {
                logger.Error(ex.StackTrace);
                lblerror.Text = "Please Insert Correct Information";
                Console.WriteLine(ex.StackTrace);
            }
        }

        //protected void btnRegister_Click(object sender, ImageClickEventArgs e)
        //{
        //    User user = new User();
        //    UserRepository userrepo = new UserRepository();
        //    SocioBoard.Helper.SessionFactory.configfilepath = Server.MapPath("~/hibernate.cfg.xml");
        //    try
        //    {
        //        if (txtPassword.Text == txtConfirmPassword.Text)
        //        {
        //            user.PaymentStatus = "unpaid";
        //            if (string.IsNullOrEmpty(Request.QueryString["type"]))
        //            {
        //                Session["FirstRegistration"] = "firsttime";
        //            }
        //            else
        //            {
        //                user.AccountType = Request.QueryString["type"];
        //            }
        //            user.CreateDate = DateTime.Now;
        //            user.ExpiryDate = DateTime.Now.AddMonths(1);
        //            user.Id = Guid.NewGuid();
        //            user.UserName = txtFirstName.Text + " " + txtLastName.Text;
        //            user.Password = this.MD5Hash(txtPassword.Text);
        //            user.EmailId = txtEmail.Text;
        //            user.UserStatus = 1;
        //            if (!userrepo.IsUserExist(user.EmailId))
        //            {
        //                UserRepository.Add(user);
        //                Session["LoggedUser"] = user;
        //                SocialSuitePro.Helper.MailSender.SendEMail(txtFirstName.Text + " " + txtLastName.Text, txtPassword.Text, txtEmail.Text);

        //                TeamRepository teamRepo = new TeamRepository();
        //                Team team = teamRepo.getTeamByEmailId(txtEmail.Text);
        //                if (team != null)
        //                {

        //                    Guid teamid = Guid.Parse(Request.QueryString["tid"]);
        //                    teamRepo.updateTeamStatus(teamid);


        //                    TeamMemberProfileRepository teamMemRepo = new TeamMemberProfileRepository();
        //                    List<TeamMemberProfile> lstteammember = teamMemRepo.getAllTeamMemberProfilesOfTeam(team.Id);
        //                    foreach (TeamMemberProfile item in lstteammember)
        //                    {
        //                        try
        //                        {
        //                            SocialProfilesRepository socialRepo = new SocialProfilesRepository();
        //                            SocialProfile socioprofile = new SocialProfile();
        //                            socioprofile.Id = Guid.NewGuid();
        //                            socioprofile.ProfileDate = DateTime.Now;
        //                            socioprofile.ProfileId = item.ProfileId;
        //                            socioprofile.ProfileType = item.ProfileType;
        //                            socioprofile.UserId = user.Id;
        //                            socialRepo.addNewProfileForUser(socioprofile);

        //                            if (item.ProfileType == "facebook")
        //                            {
        //                                try
        //                                {
        //                                    FacebookAccount fbAccount = new FacebookAccount();
        //                                    FacebookAccountRepository fbAccountRepo = new FacebookAccountRepository();
        //                                    FacebookAccount userAccount = fbAccountRepo.getUserDetails(item.ProfileId);
        //                                    fbAccount.AccessToken = userAccount.AccessToken;
        //                                    fbAccount.EmailId = userAccount.EmailId;
        //                                    fbAccount.FbUserId = item.ProfileId;
        //                                    fbAccount.FbUserName = userAccount.FbUserName;
        //                                    fbAccount.Friends = userAccount.Friends;
        //                                    fbAccount.Id = Guid.NewGuid();
        //                                    fbAccount.IsActive = true;
        //                                    fbAccount.ProfileUrl = userAccount.ProfileUrl;
        //                                    fbAccount.Type = userAccount.Type;
        //                                    fbAccount.UserId = user.Id;
        //                                    fbAccountRepo.addFacebookUser(fbAccount);
        //                                }
        //                                catch (Exception ex)
        //                                {
        //                                    Console.WriteLine(ex.Message);
        //                                    logger.Error(ex.Message);
        //                                }
        //                            }
        //                            else if (item.ProfileType == "twitter")
        //                            {
        //                                try
        //                                {
        //                                    TwitterAccount twtAccount = new TwitterAccount();
        //                                    TwitterAccountRepository twtAccRepo = new TwitterAccountRepository();
        //                                    TwitterAccount twtAcc = twtAccRepo.getUserInfo(item.ProfileId);
        //                                    twtAccount.FollowersCount = twtAcc.FollowersCount;
        //                                    twtAccount.FollowingCount = twtAcc.FollowingCount;
        //                                    twtAccount.Id = Guid.NewGuid();
        //                                    twtAccount.IsActive = true;
        //                                    twtAccount.OAuthSecret = twtAcc.OAuthSecret;
        //                                    twtAccount.OAuthToken = twtAcc.OAuthToken;
        //                                    twtAccount.ProfileImageUrl = twtAcc.ProfileImageUrl;
        //                                    twtAccount.ProfileUrl = twtAcc.ProfileUrl;
        //                                    twtAccount.TwitterName = twtAcc.TwitterName;
        //                                    twtAccount.TwitterScreenName = twtAcc.TwitterScreenName;
        //                                    twtAccount.TwitterUserId = twtAcc.TwitterUserId;
        //                                    twtAccount.UserId = user.Id;
        //                                    twtAccRepo.addTwitterkUser(twtAccount);
        //                                }
        //                                catch (Exception ex)
        //                                {
        //                                    Console.WriteLine(ex.StackTrace);
        //                                    logger.Error(ex.Message);
        //                                }
        //                            }
        //                            else if (item.ProfileType == "instagram")
        //                            {
        //                                try
        //                                {

        //                                    InstagramAccount insAccount = new InstagramAccount();
        //                                    InstagramAccountRepository insAccRepo = new InstagramAccountRepository();
        //                                    InstagramAccount InsAcc = insAccRepo.getInstagramAccountById(item.ProfileId);
        //                                    insAccount.AccessToken = InsAcc.AccessToken;
        //                                    insAccount.FollowedBy = InsAcc.FollowedBy;
        //                                    insAccount.Followers = InsAcc.Followers;
        //                                    insAccount.Id = Guid.NewGuid();
        //                                    insAccount.InstagramId = item.ProfileId;
        //                                    insAccount.InsUserName = InsAcc.InsUserName;
        //                                    insAccount.IsActive = true;
        //                                    insAccount.ProfileUrl = InsAcc.ProfileUrl;
        //                                    insAccount.TotalImages = InsAcc.TotalImages;
        //                                    insAccount.UserId = user.Id;
        //                                    insAccRepo.addInstagramUser(insAccount);
        //                                }
        //                                catch (Exception ex)
        //                                {
        //                                    Console.WriteLine(ex.StackTrace);
        //                                    logger.Error(ex.Message);
        //                                }
        //                            }
        //                            else if (item.ProfileType == "linkedin")
        //                            {
        //                                try
        //                                {
        //                                    LinkedInAccount linkAccount = new LinkedInAccount();
        //                                    LinkedInAccountRepository linkedAccountRepo = new LinkedInAccountRepository();
        //                                    LinkedInAccount linkAcc = linkedAccountRepo.getLinkedinAccountDetailsById(item.ProfileId);
        //                                    linkAccount.Id = Guid.NewGuid();
        //                                    linkAccount.IsActive = true;
        //                                    linkAccount.LinkedinUserId = item.ProfileId;
        //                                    linkAccount.LinkedinUserName = linkAcc.LinkedinUserName;
        //                                    linkAccount.OAuthSecret = linkAcc.OAuthSecret;
        //                                    linkAccount.OAuthToken = linkAcc.OAuthToken;
        //                                    linkAccount.OAuthVerifier = linkAcc.OAuthVerifier;
        //                                    linkAccount.ProfileImageUrl = linkAcc.ProfileImageUrl;
        //                                    linkAccount.ProfileUrl = linkAcc.ProfileUrl;
        //                                    linkAccount.UserId = user.Id;
        //                                    linkedAccountRepo.addLinkedinUser(linkAccount);
        //                                }
        //                                catch (Exception ex)
        //                                {
        //                                    Console.WriteLine(ex.StackTrace);
        //                                    logger.Error(ex.Message);
        //                                }
        //                            }
        //                        }
        //                        catch (Exception ex)
        //                        {
        //                            logger.Error(ex.Message);
        //                        }
        //                    }
        //                }
        //                lblerror.Text = "Registered Successfully !" + "<a href=\"Default.aspx\">Login</a>";
        //                if (Session["FirstRegistration"] != null)
        //                {
        //                    Response.Redirect("/Plans.aspx");
        //                }
        //                else
        //                {
        //                    Response.Redirect("/Home.aspx");
        //                }

        //            }
        //            else
        //            {
        //                lblerror.Text = "Email Already Exists " + "<a href=\"Default.aspx\">login</a>";
        //            }
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        logger.Error(ex.StackTrace);
        //        lblerror.Text = "Please Insert Correct Information";
        //        Console.WriteLine(ex.StackTrace);
        //    }
        //}

        //public string MD5Hash(string text)
        //{
        //    MD5 md5 = new MD5CryptoServiceProvider();

        //    //compute hash from the bytes of text
        //    md5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(text));

        //    //get hash result after compute it
        //    byte[] result = md5.Hash;

        //    StringBuilder strBuilder = new StringBuilder();
        //    for (int i = 0; i < result.Length; i++)
        //    {
        //        //change it into 2 hexadecimal digits
        //        //for each byte
        //        strBuilder.Append(result[i].ToString("x2"));
        //    }

        //    return strBuilder.ToString();
        //}
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using log4net;
using SocioBoard.Model;
using SocialSuitePro;
using SocioBoard.Domain;
using System.Security.Cryptography;
using System.Text;
using SocioBoard.Helper;
using System.Configuration;
using System.Collections;


namespace SocioBoard
{
    public partial class Registration : System.Web.UI.Page
    {
      
        //  ILog logger = LogManager.GetLogger(typeof(Registration));
        // ILog logger = LogManager.GetLogger(typeof(Registration));
        ILog logger = LogManager.GetLogger(typeof(Registration));
        protected void Page_Load(object sender, EventArgs e)
        {
            try
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
                    if (!string.IsNullOrEmpty(Request.QueryString["type"]))
                    {
                        //DropDownList1.SelectedItem.Text = Request.QueryString["type"].ToString();

                        DropDownList1.SelectedValue = Request.QueryString["type"].ToString();
                    }
                }

                //if (!string.IsNullOrEmpty(Request.QueryString["type"]))
                //{
                //    //DropDownList1.SelectedItem.Text = Request.QueryString["type"].ToString();

                //    DropDownList1.SelectedValue = Request.QueryString["type"].ToString();
                //}

                SocioBoard.Helper.SessionFactory.configfilepath = Server.MapPath("~/hibernate.cfg.xml");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                logger.Error("Error : " + ex.Message);
                logger.Error("Error : " + ex.StackTrace);
            }
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
            try
            {
                User user = new User();
                UserRepository userrepo = new UserRepository();
                UserActivation objUserActivation = new UserActivation();
                Coupon objCoupon = new Coupon();
                CouponRepository objCouponRepository = new CouponRepository();
                Groups groups = new Groups();
                GroupRepository objGroupRepository = new GroupRepository();
                Team teams = new Team();
                TeamRepository objTeamRepository = new TeamRepository();
           

                SocioBoard.Helper.SessionFactory.configfilepath = Server.MapPath("~/hibernate.cfg.xml");
                try
                {

                 
                     if (DropDownList1.SelectedValue == "Free" || DropDownList1.SelectedValue == "Standard" || DropDownList1.SelectedValue == "Deluxe" || DropDownList1.SelectedValue == "Premium")

                   
                    {



                        if (TextBox1.Text.Trim() != "")
                        {
                            string resp = SBUtils.GetCouponStatus(TextBox1.Text).ToString();
                            if (resp != "valid")
                            {
                               
                                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('" + resp + "');", true);
                                return;
                            }
                        }




                        if (txtPassword.Text == txtConfirmPassword.Text)
                        {



                            user.PaymentStatus = "unpaid";
                            user.AccountType = DropDownList1.SelectedValue.ToString();
                            if (string.IsNullOrEmpty(user.AccountType))
                            {
                                user.AccountType = AccountType.Free.ToString();
                            }


                            user.CreateDate = DateTime.Now;
                            user.ExpiryDate = DateTime.Now.AddDays(30);
                            user.Id = Guid.NewGuid();
                            user.UserName = txtFirstName.Text + " " + txtLastName.Text;
                            user.Password = this.MD5Hash(txtPassword.Text);
                            user.EmailId = txtEmail.Text;
                            user.UserStatus = 1;
                            user.ActivationStatus = "0";
                                                     
                            if (TextBox1.Text.Trim() != "")
                            {
                                user.CouponCode = TextBox1.Text.Trim().ToString();
                            }


                            if (!userrepo.IsUserExist(user.EmailId))
                            {
                                logger.Error("Before User reg");
                                UserRepository.Add(user);

                               

                                try
                                {
                                    groups.Id = Guid.NewGuid();
                                    groups.GroupName = ConfigurationManager.AppSettings["DefaultGroupName"];
                                    groups.UserId = user.Id;
                                    groups.EntryDate = DateTime.Now;
                                    
                                    objGroupRepository.AddGroup(groups);


                                    teams.Id = Guid.NewGuid();
                                    teams.GroupId = groups.Id;
                                    teams.UserId = user.Id;
                                    teams.EmailId = user.EmailId;
                                  
                                    objTeamRepository.addNewTeam(teams);



                                    BusinessSettingRepository busnrepo = new BusinessSettingRepository();
                               
                                    SocioBoard.Domain.BusinessSetting objbsnssetting = new SocioBoard.Domain.BusinessSetting();

                                    if (!busnrepo.checkBusinessExists(user.Id, groups.GroupName))
                                    {
                                        objbsnssetting.Id = Guid.NewGuid();
                                        objbsnssetting.BusinessName = groups.GroupName;
                                        objbsnssetting.GroupId = groups.Id;
                                        objbsnssetting.AssigningTasks = false;
                                        objbsnssetting.AssigningTasks = false;
                                        objbsnssetting.TaskNotification = false;
                                        objbsnssetting.TaskNotification = false;
                                        objbsnssetting.FbPhotoUpload = 0;
                                        objbsnssetting.UserId = user.Id;
                                        objbsnssetting.EntryDate = DateTime.Now;
                                        busnrepo.AddBusinessSetting(objbsnssetting);

                                    }
                                                                                                  
                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine(ex.Message);
                                    logger.Error("Error : " + ex.Message);
                                    logger.Error("Error : " + ex.StackTrace);
                                }


                                try
                                {
                                    logger.Error("1 Request.QueryString[refid]");
                                    if (Request.QueryString["refid"] != null)
                                    {
                                        logger.Error("3 Request.QueryString[refid]");
                                        User UserValid = null;
                                        if (IsUserValid(Request.QueryString["refid"].ToString(), ref UserValid))
                                        {
                                           
                                            logger.Error("Inside IsUserValid");
                                            user.RefereeStatus = "1";
                                            UpdateUserReference(UserValid);
                                            AddUserRefreeRelation(user, UserValid);
                                          
                                            logger.Error("IsUserValid");
                                        }
                                        else
                                        {
                                            user.RefereeStatus = "0";
                                        }
                                    }
                                    logger.Error("2 Request.QueryString[refid]");
                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine(ex.Message);
                                    logger.Error("btnRegister_Click" + ex.Message);
                                    logger.Error("btnRegister_Click" + ex.StackTrace);
                                }
                              


                                if (TextBox1.Text.Trim() != "")
                                {
                                    objCoupon.CouponCode = TextBox1.Text.Trim();
                                    List<Coupon> lstCoupon = objCouponRepository.GetCouponByCouponCode(objCoupon);
                                    objCoupon.Id = lstCoupon[0].Id;
                                    objCoupon.EntryCouponDate = lstCoupon[0].EntryCouponDate;
                                    objCoupon.ExpCouponDate = lstCoupon[0].ExpCouponDate;
                                    objCoupon.Status = "1";
                                    objCouponRepository.SetCouponById(objCoupon);
                                }

                                Session["LoggedUser"] = user;
                                objUserActivation.Id = Guid.NewGuid();
                                objUserActivation.UserId = user.Id;
                                objUserActivation.ActivationStatus = "0";
                                UserActivationRepository.Add(objUserActivation);

                                //add package start

                                UserPackageRelation objUserPackageRelation = new UserPackageRelation();
                                UserPackageRelationRepository objUserPackageRelationRepository = new UserPackageRelationRepository();
                                PackageRepository objPackageRepository = new PackageRepository();

                                try
                                {
                                    Package objPackage = objPackageRepository.getPackageDetails(user.AccountType);
                                    objUserPackageRelation.Id = Guid.NewGuid();
                                    objUserPackageRelation.PackageId = objPackage.Id;
                                    objUserPackageRelation.UserId = user.Id;
                                    objUserPackageRelation.ModifiedDate = DateTime.Now;
                                    objUserPackageRelation.PackageStatus = true;

                                    objUserPackageRelationRepository.AddUserPackageRelation(objUserPackageRelation);
                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine(ex.StackTrace);
                                }

                                //end package

                                SocioBoard.Helper.MailSender.SendEMail(txtFirstName.Text, txtPassword.Text, txtEmail.Text, user.AccountType.ToString(), user.Id.ToString());
                                TeamRepository teamRepo = new TeamRepository();
                                try
                                {
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
                                                        fbAccount.IsActive = 1;
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

                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine(ex.StackTrace);
                                }

                                #region SetInvitationStatusAfterSuccessfulRegistration
                                try
                                {
                                    if (Request.QueryString["refid"] != null)
                                    {
                                        string refid = Request.QueryString["refid"];

                                        int res = SetInvitationStatusAfterSuccessfulRegistration(refid, txtEmail.Text);
                                    }
                                }
                                catch (Exception ex)
                                {
                                    logger.Error(ex.Message);
                                } 
                                #endregion


                                try
                                {
                                    lblerror.Text = "Registered Successfully !" + "<a href=\"Default.aspx\">Login</a>";
                                    Response.Redirect("~/Home.aspx");
                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine(ex.StackTrace);
                                }
                            }
                            else
                            {
                                lblerror.Text = "Email Already Exists " + "<a id=\"loginlink\"  href=\"#\">login</a>";
                            }
                        }
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please select Account Type!');", true);
                    }
                }

                catch (Exception ex)
                {
                    logger.Error(ex.StackTrace);
                    lblerror.Text = "Success!";
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

        public int SetInvitationStatusAfterSuccessfulRegistration(string refid,string userEmail)
        {
            int res = 0;
            try
            {
                

                UserRepository objUserRepository = new UserRepository();

                User user = objUserRepository.getUsersById(Guid.Parse(refid));

                Invitation objInvitation=new Invitation();
                objInvitation.SenderEmail=user.EmailId;
                objInvitation.FriendEmail=userEmail;
                objInvitation.Status = "12";

                InvitationRepository objInvitationRepository = new InvitationRepository();
                res = objInvitationRepository.SetInvitationStatusBySenderEmailFreiendEmail(objInvitation);
            }
            catch (Exception ex)
            {
                logger.Error(ex.StackTrace);

                Console.WriteLine(ex.StackTrace);
                //Response.Redirect("Home.aspx");
            }
            return res;
        }


        /// <summary>
        /// This function check Is User Exist or Not created by Abhay Kr 5-2-2014
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns>bool</returns>
        public bool IsUserValid(string UserId, ref User user)
        {
            bool ret = false;
            try
            {

                UserRepository objUserRepository = new UserRepository();
                user = objUserRepository.getUsersById(Guid.Parse(UserId));
                if (user != null)
                    ret = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                logger.Error(ex.Message);
            }
            return ret;
        }

        public void UpdateUserReference(User objUser)
        {
            try
            {
                UserRepository objUserRepository = new UserRepository();
                objUser.ReferenceStatus = "1";
                objUserRepository.UpdateReferenceUserByUserId(objUser);

                logger.Error("UpdateUserReference");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                logger.Error(ex.Message);
            }
        }

        public void AddUserRefreeRelation(User objReferee, User objReference)
        {
            try
            {
                logger.Error("Entered AddUserRefreeRelation");

                UserRefRelation objUserRefRelation = new UserRefRelation();
                UserRefRelationRepository objUserRefRelationRepository = new UserRefRelationRepository();

                objUserRefRelation.Id = Guid.NewGuid();
                objUserRefRelation.RefereeUserId = objReferee.Id;
                objUserRefRelation.ReferenceUserId = objReference.Id;
                objUserRefRelation.ReferenceUserEmail = objReference.EmailId;
                objUserRefRelation.RefereeUserEmail = objReferee.EmailId;
                objUserRefRelation.EntryDate = DateTime.Now;
                objUserRefRelation.Status = "0";

                objUserRefRelationRepository.AddUserRefRelation(objUserRefRelation);

                logger.Error("Coming out of AddUserRefreeRelation");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                logger.Error(ex.Message);
            }

        }


        
    }
}
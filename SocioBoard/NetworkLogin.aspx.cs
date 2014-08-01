using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SocioBoard.Domain;
using SocioBoard.Helper;
using SocioBoard.Model;
using log4net;
using SocioBoard;
using System.Configuration;
//using soc.Helper;

namespace SocialSuitePro
{
    public partial class NetworkLogin : System.Web.UI.Page
    {
        ILog logger = LogManager.GetLogger(typeof(NetworkLogin));

        protected void Page_Load(object sender, EventArgs e)
        {
          
            try
            {
                User user = (User)Session["LoggedUser"];
                if (!IsPostBack)
                {
                    if (user == null)
                        Response.Redirect("Default.aspx");

                    txtEmail.Text = user.EmailId;
                    txtEmail.Attributes.Add("readonly", "readonly");
                    if (user.UserName != null)
                    {
                        string[] name = user.UserName.Split(' ');
                        txtFirstName.Text = name[0];
                        txtLastName.Text = name[1];
                    }
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
            }
        }



        protected void btnRegister_Click(object sender, ImageClickEventArgs e)
        {
            Groups groups = new Groups();
            GroupRepository objGroupRepository = new GroupRepository();
            Team teams = new Team();
            TeamRepository objTeamRepository = new TeamRepository();


            try
            {
                Session["login"] = null;
                Registration regpage = new Registration();
                User user = (User)Session["LoggedUser"];

                if (DropDownList1.SelectedValue == "Free" || DropDownList1.SelectedValue == "Standard" || DropDownList1.SelectedValue == "Deluxe" || DropDownList1.SelectedValue == "Premium")
                {

                    if (TextBox1.Text.Trim() != "")
                    {
                        string resp = SBUtils.GetCouponStatus(TextBox1.Text).ToString();
                        if (resp != "valid")
                        {
                            // ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert(Not valid);", true);
                            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('" + resp + "');", true);
                            return;
                        }
                    }


                    if (user != null)
                    {
                        user.EmailId = txtEmail.Text;
                        user.UserName = txtFirstName.Text + " " + txtLastName.Text;
                        UserActivation objUserActivation = new UserActivation();
                        UserRepository userrepo = new UserRepository();
                        Coupon objCoupon = new Coupon();
                        CouponRepository objCouponRepository = new CouponRepository();
                        if (userrepo.IsUserExist(user.EmailId))
                        {

                            try
                            {
                                string acctype = string.Empty;
                                if (Request.QueryString["type"] != null)
                                {
                                    if (Request.QueryString["type"] == "INDIVIDUAL" || Request.QueryString["type"] == "CORPORATION" || Request.QueryString["type"] == "SMALL BUSINESS")
                                    {
                                        acctype = Request.QueryString["type"];
                                    }
                                    else
                                    {
                                        acctype = "INDIVIDUAL";
                                    }
                                }
                                else
                                {
                                    acctype = "INDIVIDUAL";
                                }

                                user.AccountType = Request.QueryString["type"];
                            }
                            catch (Exception ex)
                            {
                                logger.Error(ex.StackTrace);
                                Console.WriteLine(ex.StackTrace);
                            }

                            user.AccountType = DropDownList1.SelectedValue.ToString();
                            if (string.IsNullOrEmpty(user.AccountType))
                            {
                                user.AccountType = AccountType.Free.ToString();
                            }

                            if (string.IsNullOrEmpty(user.Password))
                            {
                                user.Password = regpage.MD5Hash(txtPassword.Text);
                                // userrepo.UpdatePassword(user.EmailId, user.Password, user.Id, user.UserName, user.AccountType);
                                string couponcode = TextBox1.Text.Trim();
                                userrepo.SetUserByUserId(user.EmailId, user.Password, user.Id, user.UserName, user.AccountType, couponcode);

                                try
                                {
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
                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine(ex.Message);
                                    logger.Error("Error : " + ex.Message);
                                    logger.Error("Error : " + ex.StackTrace);
                                }

                                //add userActivation

                                try
                                {
                                    objUserActivation.Id = Guid.NewGuid();
                                    objUserActivation.UserId = user.Id;
                                    objUserActivation.ActivationStatus = "0";
                                    UserActivationRepository.Add(objUserActivation);
                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine(ex.Message);
                                    logger.Error("Error : " + ex.Message);
                                    logger.Error("Error : " + ex.StackTrace);
                                }

                                //add package start

                                try
                                {
                                    UserPackageRelation objUserPackageRelation = new UserPackageRelation();
                                    UserPackageRelationRepository objUserPackageRelationRepository = new UserPackageRelationRepository();
                                    PackageRepository objPackageRepository = new PackageRepository();

                                    Package objPackage = objPackageRepository.getPackageDetails(user.AccountType);
                                    objUserPackageRelation.Id = Guid.NewGuid();
                                    objUserPackageRelation.PackageId = objPackage.Id;
                                    objUserPackageRelation.UserId = user.Id;
                                    objUserPackageRelation.ModifiedDate = DateTime.Now;
                                    objUserPackageRelation.PackageStatus = true;

                                    objUserPackageRelationRepository.AddUserPackageRelation(objUserPackageRelation);

                                    //end package



                                    MailSender.SendEMail(txtFirstName.Text + " " + txtLastName.Text, txtPassword.Text, txtEmail.Text, user.AccountType.ToString(), user.Id.ToString());

                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine(ex.Message);
                                    logger.Error("Error : " + ex.Message);
                                    logger.Error("Error : " + ex.StackTrace);
                                }

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
                                    // teams.FirstName = user.UserName;
                                    objTeamRepository.addNewTeam(teams);


                                    BusinessSettingRepository busnrepo = new BusinessSettingRepository();
                                    //SocioBoard.Domain.Team team = (SocioBoard.Domain.Team)Session["GroupName"];
                                    SocioBoard.Domain.BusinessSetting objbsnssetting = new SocioBoard.Domain.BusinessSetting();

                                    if (!busnrepo.checkBusinessExists(user.Id, groups.GroupName))
                                    {
                                        objbsnssetting.Id = Guid.NewGuid();
                                        objbsnssetting.BusinessName = groups.GroupName;
                                        //objbsnssetting.GroupId = team.GroupId;
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

                            }
                        }
                        Session["LoggedUser"] = user;

                        Response.Redirect("Home.aspx");
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
                Console.WriteLine(ex.StackTrace);
            }
        }
    }
}
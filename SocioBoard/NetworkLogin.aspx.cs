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


                                //add userActivation

                                objUserActivation.Id = Guid.NewGuid();
                                objUserActivation.UserId = user.Id;
                                objUserActivation.ActivationStatus = "0";
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



                                MailSender.SendEMail(txtFirstName.Text + " " + txtLastName.Text, txtPassword.Text, txtEmail.Text, user.AccountType.ToString(), user.Id.ToString());
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
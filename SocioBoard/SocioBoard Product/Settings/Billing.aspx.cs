using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SocioBoard.Model;
using SocioBoard.Domain;
using SocioBoard.Helper;
using System.Configuration;

namespace SocioBoard.Settings
{
    public partial class Billing : System.Web.UI.Page
    {
        DateTime chkdate;
        double datediff;
        string packagehtml = string.Empty;
        List<Package> lstPackage = new List<Package>();
        string type = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                User user = (User)Session["LoggedUser"];


                #region for You can use only 30 days as Unpaid User
                try
                {

                    if (user.PaymentStatus.ToLower() == "unpaid")
                    {
                        if (!SBUtils.IsUserWorkingDaysValid(user.ExpiryDate))
                        {
                            Session["GreaterThan30Days"] = "GreaterThan30Days";
                            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Your 30 Days trial period Over Please Upgrade your Package!');", true);
                        }
                    }
                    Session["GreaterThan30Days"] = null;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                #endregion




                if (user == null)
                    Response.Redirect("/Default.aspx");
                if (Request.QueryString != null)
                {
                    type = Request.QueryString["type"];
                }



                PackageRepository packageRepo = new PackageRepository();

                lstPackage = packageRepo.getAllPackage();

                if (user.AccountType == "Free")
                {
                    freediv.Attributes.CssStyle.Add("display", "block");
                    heading1.InnerHtml = lstPackage[0].PackageName.ToString();
                    if (user.AccountType == lstPackage[0].PackageName.ToString())
                    {
                        price1.InnerHtml = "<h3 style=\"color: #F4594F;\"> $ " + lstPackage[0].Pricing.ToString() + " Per User/Month</h3>";
                        ContentPlaceHolder1_freeplan.InnerHtml = "<span class=\"trail_btn active\">Current Package!</span>";
                        // ContentPlaceHolder1_standardplan.Attributes.Add("href", "#");
                    }
                    else
                    {
                        //price1.InnerHtml = "$" + lstPackage[0].Pricing.ToString() + " Per User/Month";
                        price1.InnerHtml = "<h3> $ " + lstPackage[0].Pricing.ToString() + " Per User/Month</h3>";
                        ContentPlaceHolder1_freeplan.InnerHtml = "<span class=\"trail_btn\">Upgrade</span>";
                    }
                }


                heading2.InnerHtml = lstPackage[1].PackageName.ToString();
                if (user.AccountType == lstPackage[1].PackageName.ToString())
                {
                    price2.InnerHtml = "<h3 style=\"color: #F4594F;\"> $ " + lstPackage[1].Pricing.ToString() + " Per User/Month</h3>";
                    ContentPlaceHolder1_standardplan.InnerHtml = "<span class=\"trail_btn active\">Current Package!</span>";
                    //  ContentPlaceHolder1_deluxeplan.Attributes.Add("href", "#");
                }
                else
                {
                    price2.InnerHtml = "<h3> $ " + lstPackage[1].Pricing.ToString() + " Per User/Month</h3>";
                    ContentPlaceHolder1_standardplan.InnerHtml = "<span class=\"trail_btn\">Upgrade</span>";
                }


                heading3.InnerHtml = lstPackage[2].PackageName.ToString();
                if (user.AccountType == lstPackage[2].PackageName.ToString())
                {
                    price3.InnerHtml = "<h3 style=\"color: #F4594F;\"> $ " + lstPackage[2].Pricing.ToString() + " Per User/Month</h3>";
                    ContentPlaceHolder1_premiumplan.InnerHtml = "<span class=\"trail_btn active\">Current Package!</span>";
                    // ContentPlaceHolder1_premiumplan.Attributes.Add("href", "#");
                }
                else
                {
                    price3.InnerHtml = "<h3> $ " + lstPackage[2].Pricing.ToString() + " Per User/Month</h3>";
                    ContentPlaceHolder1_premiumplan.InnerHtml = "<span class=\"trail_btn\">Upgrade</span>";
                }
                heading4.InnerHtml = lstPackage[3].PackageName.ToString();
                if (user.AccountType == lstPackage[3].PackageName.ToString())
                {
                    price4.InnerHtml = "<h3 style=\"color: #F4594F;\"> $ " + lstPackage[3].Pricing.ToString() + " Per User/Month</h3>";
                    ContentPlaceHolder1_deluxeplan.InnerHtml = "<span class=\"trail_btn active\">Current Package!</span>";
                    // ContentPlaceHolder1_premiumplan.Attributes.Add("href", "#");
                }
                else
                {
                    price4.InnerHtml = "<h3> $ " + lstPackage[3].Pricing.ToString() + " Per User/Month</h3>";
                    ContentPlaceHolder1_deluxeplan.InnerHtml = "<span class=\"trail_btn\">Upgrade</span>";
                }
                if (type == "Standard" || type == "Deluxe" || type == "Premium")
                {
                    Payment();
                }
            }
        }

        public void Payment()
        {
            User user = (User)Session["LoggedUser"];
            SocioBoard.Helper.Payment payme = new SocioBoard.Helper.Payment();
            string amount = string.Empty;
            string plantype = string.Empty;
            if (type == "Standard")
            {
                plantype = lstPackage[1].PackageName;
                amount = Convert.ToString(lstPackage[1].Pricing);
            }
            else if (type == "Premium")
            {
                plantype = lstPackage[2].PackageName;
                amount = Convert.ToString(lstPackage[2].Pricing);
            }

            else if (type == "Deluxe")
            {
                plantype = lstPackage[3].PackageName;
                amount = Convert.ToString(lstPackage[3].Pricing);
            }
           
            PackageRepository objPackageRepository = new PackageRepository();

            Package objPackage = objPackageRepository.getPackageDetails(plantype);

            if (objPackage != null)
            {
                HttpContext.Current.Session["PackageDetails"] = objPackage;
            }

            string pay = string.Empty;

            //#region DirectPaymentWithPayPal
            //pay = payme.PayWithPayPal(amount, plantype, user.UserName, "", user.EmailId, "USD", ConfigurationManager.AppSettings["paypalemail"], ConfigurationManager.AppSettings["SuccessURL"],
            //              ConfigurationManager.AppSettings["FailedURL"], ConfigurationManager.AppSettings["SuccessURL"], ConfigurationManager.AppSettings["cancelurl"], ConfigurationManager.AppSettings["notifyurl"], user.Id.ToString());

            //#endregion

            #region RecurringPaymentWithPayPal

            pay = payme.RecurringPaymentWithPayPal(amount, plantype, user.UserName, "", user.EmailId, "USD", ConfigurationManager.AppSettings["paypalemail"], ConfigurationManager.AppSettings["SuccessURL"],
                                 ConfigurationManager.AppSettings["FailedURL"], ConfigurationManager.AppSettings["SuccessURL"], ConfigurationManager.AppSettings["cancelurl"], ConfigurationManager.AppSettings["notifyurl"], user.Id.ToString());
            #endregion
            Response.Redirect(pay);
        }
    }
}
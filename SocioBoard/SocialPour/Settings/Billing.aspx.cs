using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SocioBoard.Domain;
using SocioBoard.Helper;
using System.Configuration;
using SocioBoard.Model;

namespace SocialSuitePro.Settings
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

                if (user == null)
                    Response.Redirect("/Default.aspx");
                if (Request.QueryString != null)
                {
                    type = Request.QueryString["type"];
                }

                #region check for 30 days
                chkdate = user.CreateDate;
                DateTime curDate = DateTime.Now;
                TimeSpan ts = curDate - chkdate;
                datediff = ts.TotalDays;

                if (datediff > 30)
                {
                    //ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Your 30 Days trial period Over Please Upgrade your Package');", true);
                    ScriptManager.RegisterStartupScript(this, GetType(), "Script", "MyJavascriptFunction();", true);

                    // System.Web.UI.ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "MyJavascriptFunction();", true);
                }

                #endregion

                PackageRepository packageRepo = new PackageRepository();

                lstPackage = packageRepo.getAllPackage();



                heading1.InnerHtml = lstPackage[0].PackageName.ToString();
                if (user.AccountType == lstPackage[0].PackageName.ToString())
                {
                    price1.InnerHtml = "<h3 style=\"color: #F4594F;\"> $ " + lstPackage[0].Pricing.ToString() + " Per User/Month</h3>";
                    ContentPlaceHolder1_standardplan.InnerHtml = "<span class=\"trail_btn active\">Current Package!</span>";
                    ContentPlaceHolder1_standardplan.Attributes.Add("href", "#");
                }
                else
                {
                    //price1.InnerHtml = "$" + lstPackage[0].Pricing.ToString() + " Per User/Month";
                    price1.InnerHtml = "<h3> $ " + lstPackage[0].Pricing.ToString() + " Per User/Month</h3>";
                    ContentPlaceHolder1_standardplan.InnerHtml = "<span class=\"trail_btn\">Upgrade</span>";
                }
               

                heading2.InnerHtml = lstPackage[1].PackageName.ToString();
                if (user.AccountType == lstPackage[1].PackageName.ToString())
                {
                    price2.InnerHtml = "<h3 style=\"color: #F4594F;\"> $ " + lstPackage[1].Pricing.ToString() + " Per User/Month</h3>";
                    ContentPlaceHolder1_deluxeplan.InnerHtml = "<span class=\"trail_btn active\">Current Package!</span>";
                    ContentPlaceHolder1_deluxeplan.Attributes.Add("href", "#");
                }
                else
                {
                    price2.InnerHtml = "<h3> $ " + lstPackage[1].Pricing.ToString() + " Per User/Month</h3>";
                    ContentPlaceHolder1_deluxeplan.InnerHtml = "<span class=\"trail_btn\">Upgrade</span>";
                }
                

                heading3.InnerHtml = lstPackage[2].PackageName.ToString();
                if (user.AccountType == lstPackage[2].PackageName.ToString())
                {
                    price3.InnerHtml = "<h3 style=\"color: #F4594F;\"> $ " + lstPackage[2].Pricing.ToString() + " Per User/Month</h3>";
                    ContentPlaceHolder1_premiumplan.InnerHtml = "<span class=\"trail_btn active\">Current Package!</span>";
                    ContentPlaceHolder1_premiumplan.Attributes.Add("href", "#");
                }
                else

                {
                    price3.InnerHtml = "<h3> $ " + lstPackage[2].Pricing.ToString() + " Per User/Month</h3>";
                    ContentPlaceHolder1_premiumplan.InnerHtml = "<span class=\"trail_btn\">Upgrade</span>";
                }
                

                
                if (type == "Standard"||type == "Deluxe"||type == "Premium")
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
                plantype = lstPackage[0].PackageName;
                amount = Convert.ToString(lstPackage[0].Pricing);
            }
            else if (type == "Deluxe")
            {
                plantype = lstPackage[1].PackageName;
                amount = Convert.ToString(lstPackage[1].Pricing);
            }
            else if (type == "Premium")
            {
                plantype = lstPackage[2].PackageName;
                amount = Convert.ToString(lstPackage[2].Pricing);
            }

            PackageRepository objPackageRepository = new PackageRepository();

            Package objPackage = objPackageRepository.getPackageDetails(plantype);

            if (objPackage != null)
            {
                HttpContext.Current.Session["PackageDetails"] = objPackage;
            }


            string pay = payme.PayWithPayPal(amount, plantype, user.UserName, "", user.EmailId, "USD", ConfigurationManager.AppSettings["paypalemail"], ConfigurationManager.AppSettings["SuccessURL"],
                                  ConfigurationManager.AppSettings["FailedURL"], ConfigurationManager.AppSettings["SuccessURL"], ConfigurationManager.AppSettings["cancelurl"], ConfigurationManager.AppSettings["notifyurl"], user.Id.ToString());

            //string pay = payme.TestPayWithPaypal(amount, plantype, user.UserName, "", user.EmailId, "USD", ConfigurationManager.AppSettings["paypalemail"], ConfigurationManager.AppSettings["SuccessURL"],
            //                      ConfigurationManager.AppSettings["FailedURL"], ConfigurationManager.AppSettings["SuccessURL"], ConfigurationManager.AppSettings["cancelurl"], ConfigurationManager.AppSettings["notifyurl"], user.Id.ToString());
            //Response.Redirect(pay);
        }
    }
}
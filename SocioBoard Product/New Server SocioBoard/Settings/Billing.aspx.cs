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
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                User user = (User)Session["LoggedUser"];

                if (user == null)
                    Response.Redirect("/Default.aspx");

                PackageRepository packageRepo = new PackageRepository();
                divcreatedate.InnerHtml = "Subscription begins - " + user.CreateDate.ToString();
                plantype.InnerHtml = user.AccountType;
                if (user.AccountType.ToLower() == AccountType.Standard.ToString().ToLower())
                {
                    Package pack = packageRepo.getPackageDetails(AccountType.Standard.ToString());
                    if (pack != null)
                    {
                        priceofplan.InnerHtml = "$" + pack.Pricing;
                        // rateExtra.InnerHtml = "$" + pack.Pricing;
                        monthly.InnerHtml = "$" + pack.Pricing;
                    }
                }
                else if (user.AccountType.ToLower() == AccountType.Deluxe.ToString().ToLower())
                {
                    Package pack = packageRepo.getPackageDetails(AccountType.Deluxe.ToString());
                    if (pack != null)
                    {
                        priceofplan.InnerHtml = "$" + pack.Pricing;
                        //  rateExtra.InnerHtml = "$" + pack.Pricing;
                        monthly.InnerHtml = "$" + pack.Pricing;
                    }
                }
                else if (user.AccountType.ToLower() == AccountType.Premium.ToString().ToLower())
                {
                    Package pack = packageRepo.getPackageDetails(AccountType.Premium.ToString());
                    if (pack != null)
                    {
                        priceofplan.InnerHtml = "$" + pack.Pricing;
                        //  rateExtra.InnerHtml = "$" + pack.Pricing;
                        monthly.InnerHtml = "$" + pack.Pricing;
                    }
                }
            }
        }

        public void Payment(object sender, EventArgs e)
        {
            User user = (User)Session["LoggedUser"];
            SocioBoard.Helper.Payment payme = new SocioBoard.Helper.Payment();
            string amount = string.Empty;
            string plantype = string.Empty;
            if (user.AccountType == "standard")
            {
                plantype = "StandardPlan of SocialSuitePro";
                amount = "39";
            }
            else if (user.AccountType == "deluxe")
            {
                plantype = "DeluxePlan of SocialSuitePro";
                amount = "59";
            }
            else if (user.AccountType == "premium")
            {
                plantype = "PremiumPlan of SocialSuitePro";
                amount = "99";
            }

            string pay = payme.PayWithPayPal(amount,plantype, user.UserName, "", user.EmailId, "USD", ConfigurationManager.AppSettings["paypalemail"], ConfigurationManager.AppSettings["SuccessURL"],
                                  ConfigurationManager.AppSettings["FailedURL"], ConfigurationManager.AppSettings["SuccessURL"], ConfigurationManager.AppSettings["cancelurl"],ConfigurationManager.AppSettings["notifyurl"],user.Id.ToString());
            Response.Redirect(pay);
        }
    }
}
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

namespace blackSheep.Settings
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
                    priceofplan.InnerHtml = "$" + pack.Pricing;
                    rateExtra.InnerHtml = "$" + pack.Pricing;
                    monthly.InnerHtml = "$" + pack.Pricing;
                }
                else if (user.AccountType.ToLower() == AccountType.Deluxe.ToString().ToLower())
                {
                    Package pack = packageRepo.getPackageDetails(AccountType.Deluxe.ToString());
                    priceofplan.InnerHtml = "$" + pack.Pricing;
                    rateExtra.InnerHtml = "$" + pack.Pricing;
                    monthly.InnerHtml = "$" + pack.Pricing;
                }
                else if (user.AccountType.ToLower() == AccountType.Premium.ToString().ToLower())
                {
                    Package pack = packageRepo.getPackageDetails(AccountType.Premium.ToString());
                    priceofplan.InnerHtml = "$" + pack.Pricing;
                    rateExtra.InnerHtml = "$" + pack.Pricing;
                    monthly.InnerHtml = "$" + pack.Pricing;

                }
            }
        }

        public void Payment(object sender, EventArgs e)
        {
            User user = (User)Session["LoggedUser"];
            SocioBoard.Helper.Payment payme = new SocioBoard.Helper.Payment();
            string amount = string.Empty;
            string plantype = string.Empty;
          
            PackageRepository packRepo = new PackageRepository();
            if (user.AccountType.ToLower() == AccountType.Standard.ToString().ToLower())
            {
               Package pack = packRepo.getPackageDetails(AccountType.Standard.ToString());
                plantype = "StandardPlan of blackSheep";
                amount = pack.Pricing.ToString();
            }
            else if (user.AccountType.ToLower() == AccountType.Deluxe.ToString().ToLower())
            {
                Package pack = packRepo.getPackageDetails(AccountType.Deluxe.ToString());
                plantype = "DeluxePlan of blackSheep";
                amount = pack.Pricing.ToString();
            }
            else if (user.AccountType.ToLower() == AccountType.Premium.ToString().ToLower())
            {
                Package pack = packRepo.getPackageDetails(AccountType.Premium.ToString());
                plantype = "PremiumPlan of blackSheep";
                amount = pack.Pricing.ToString();
            }

            string pay = payme.PayWithPayPal(amount, plantype, user.UserName, "", user.EmailId, "USD", ConfigurationManager.AppSettings["paypalemail"], ConfigurationManager.AppSettings["SuccessURL"],
                                  ConfigurationManager.AppSettings["FailedURL"], ConfigurationManager.AppSettings["SuccessURL"], ConfigurationManager.AppSettings["cancelurl"], ConfigurationManager.AppSettings["notifyurl"], user.Id.ToString());
            Response.Redirect(pay);
        }
    }
}
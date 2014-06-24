using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SocioBoard.Domain;
using SocioBoard.Helper;
using SocioBoard.Model;

namespace letTalkNew.Settings
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
    }
}
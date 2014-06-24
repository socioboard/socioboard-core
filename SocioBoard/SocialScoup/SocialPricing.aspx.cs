using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SocioBoard.Helper;
using SocioBoard.Model;
using SocioBoard.Domain;

namespace SocialSuitePro
{
    public partial class SocialPricing : System.Web.UI.Page
    {
        string packagehtml = string.Empty;
        List<Package> lstPackage = new List<Package>();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["login"] != null)
                {
                    standardplan.HRef = "SocialNetworkLogin.aspx?type=INDIVIDUAL"; //+ AccountType.Standard.ToString();
                    deluxeplan.HRef = "SocialNetworkLogin.aspx?type=SMALL BUSINESS"; //+ AccountType.Deluxe.ToString();
                    premiumplan.HRef = "SocialNetworkLogin.aspx?type=CORPORATION"; //+ AccountType.Premium.ToString();
                }
                else if (Session["AjaxLogin"] != null)
                {
                    standardplan.HRef = "Home.aspx?type=INDIVIDUAL";// + AccountType.Standard.ToString(); ;
                    deluxeplan.HRef = "Home.aspx?type=SMALL BUSINESS";// + AccountType.Deluxe.ToString();
                    premiumplan.HRef = "Home.aspx?type=CORPORATION"; //" + AccountType.Premium.ToString();
                    Session["AjaxLogin"] = null;
                }
                else
                {
                    standardplan.HRef = "SocialRegisterPage.aspx?type=INDIVIDUAL"; //" + AccountType.Standard.ToString();
                    deluxeplan.HRef = "SocialRegisterPage.aspx?type=SMALL BUSINESS"; //" + AccountType.Deluxe.ToString();
                    premiumplan.HRef = "SocialRegisterPage.aspx?type=CORPORATION"; //" + AccountType.Premium.ToString();
                }


                PackageRepository packageRepo = new PackageRepository();

                lstPackage = packageRepo.getAllPackage();



                heading1.InnerHtml = lstPackage[0].PackageName.ToString();
                price1.InnerHtml = "<h3> $ " + lstPackage[0].Pricing.ToString() + " Per User/Month</h3>";



                heading2.InnerHtml = lstPackage[1].PackageName.ToString();


                price2.InnerHtml = "<h3> $ " + lstPackage[1].Pricing.ToString() + " Per User/Month</h3>";



                heading3.InnerHtml = lstPackage[2].PackageName.ToString();


                price3.InnerHtml = "<h3> $ " + lstPackage[2].Pricing.ToString() + " Per User/Month</h3>";







            }
        }
    }
}
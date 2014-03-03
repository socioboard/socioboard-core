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
        //List<Package> lstPackage = new List<Package>();

        protected void Page_Load(object sender, EventArgs e)
        {

            try
            {
                if (!IsPostBack)
                {
                    User user = (User)Session["LoggedUser"];
                    #region for You can use only 30 days as Unpaid User

                    try
                    {
                        if (user.PaymentStatus.ToLower() == "unpaid")
                        {
                            if (!SBUtils.IsUserWorkingDaysValid(user.CreateDate))
                            {
                                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('You can use only 30 days as Unpaid User !');", true);

                                Session["GreaterThan30Days"] = "GreaterThan30Days";
                                // Response.Redirect("/Settings/Billing.aspx");
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
            catch (Exception ex)
            {
                Console.WriteLine("Error : " + ex.StackTrace);
            }
        }

        public void Payment(object sender, EventArgs e)
        {
            try
            {
                PackageRepository packageRepo = new PackageRepository();

                List<Package> lstPackage = packageRepo.getAllPackage();
                User user = (User)Session["LoggedUser"];
                SocioBoard.Helper.Payment payme = new SocioBoard.Helper.Payment();
                string amount = string.Empty;
                string plantype = string.Empty;
                if (lstPackage.Count > 0)
                {
                    if (user.AccountType.ToLower() == "standard")
                    {
                        plantype = lstPackage[0].PackageName;
                        amount = Convert.ToString(lstPackage[0].Pricing);
                    }
                    else if (user.AccountType.ToLower() == "deluxe")
                    {
                        plantype = lstPackage[1].PackageName;
                        amount = Convert.ToString(lstPackage[1].Pricing);
                    }
                    else if (user.AccountType.ToLower() == "premium")
                    {
                        plantype = lstPackage[2].PackageName;
                        amount = Convert.ToString(lstPackage[2].Pricing);
                    }
                }
                else
                {
                    if (user.AccountType.ToLower() == "standard")
                    {
                        plantype = "StandardPlan of Socioboard";
                        amount = "39";
                    }
                    else if (user.AccountType.ToLower() == "deluxe")
                    {
                        plantype = "DeluxePlan of Socioboard";
                        amount = "59";
                    }
                    else if (user.AccountType.ToLower() == "premium")
                    {
                        plantype = "PremiumPlan of Socioboard";
                        amount = "99";
                    }
                }

                string pay = string.Empty;

                #region DirectPaymentWithPayPal
                pay = payme.PayWithPayPal(amount, plantype, user.UserName, "", user.EmailId, "USD", ConfigurationManager.AppSettings["paypalemail"], ConfigurationManager.AppSettings["SuccessURL"],
                              ConfigurationManager.AppSettings["FailedURL"], ConfigurationManager.AppSettings["SuccessURL"], ConfigurationManager.AppSettings["cancelurl"], ConfigurationManager.AppSettings["notifyurl"], user.Id.ToString());
                
                #endregion

                //#region RecurringPaymentWithPayPal

                //pay = payme.RecurringPaymentWithPayPal(amount, plantype, user.UserName, "", user.EmailId, "USD", ConfigurationManager.AppSettings["paypalemail"], ConfigurationManager.AppSettings["SuccessURL"],
                //                     ConfigurationManager.AppSettings["FailedURL"], ConfigurationManager.AppSettings["SuccessURL"], ConfigurationManager.AppSettings["cancelurl"], ConfigurationManager.AppSettings["notifyurl"], user.Id.ToString());
                //#endregion

                Response.Redirect(pay);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error : " + ex.StackTrace);
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SocioBoard.Domain;
using SocioBoard.Model;

namespace SocialScoup.Settings
{
    public partial class BillingSuccess : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Request.QueryString != null || Request.Form !=null)
                {

                }
                
                if (HttpContext.Current.Session["PackageDetails"] != null && Session["LoggedUser"] != null)
                {
                    //ScriptManager.RegisterStartupScript(this, GetType(), "Paypall Success", "<script type=\"text/javascript\">alert('Your transaction has been Suceeded !');</script>", true);
                    //ScriptManager.RegisterStartupScript(this, GetType(), "Script", "MyJavascriptFunction();", true);

                    //if (Session["LoggedUser"] !=null)
                    {
                        User user = (User)Session["LoggedUser"];
                        Package packageDetails = (Package)HttpContext.Current.Session["PackageDetails"];

                        UserPackageRelation objUserPackageRelation = new UserPackageRelation();

                        objUserPackageRelation.Id = new Guid();
                        objUserPackageRelation.PackageStatus = true;
                        objUserPackageRelation.UserId = user.Id;
                        objUserPackageRelation.PackageId = packageDetails.Id;
                        objUserPackageRelation.ModifiedDate = DateTime.Now;

                        // Code for Update & Insert in UserPackageRelationRepository

                        UserPackageRelationRepository objUserPackageRelationRepository = new UserPackageRelationRepository();

                        objUserPackageRelationRepository.UpdateUserPackageRelation(user);

                        objUserPackageRelationRepository.AddUserPackageRelation(objUserPackageRelation);

                        // Code for Update in User

                        UserRepository objUserRepository = new UserRepository();
                        User objUser = new User();

                        objUser.Id = user.Id;
                        objUser.AccountType = packageDetails.PackageName;
                        objUser.PaymentStatus = "Paid";
                        objUser.CreateDate = DateTime.Now;
                        objUser.EmailId = user.EmailId;
                        objUser.UserName = user.UserName;
                        objUser.ProfileUrl = user.ProfileUrl;
                        objUser.ExpiryDate = user.ExpiryDate;
                        objUser.UserStatus = user.UserStatus;
                        objUser.Password = user.Password;
                        objUser.TimeZone = user.TimeZone; 
                        

                        objUserRepository.UpdateCreatDateByUserId(objUser);

                        Session["LoggedUser"] = objUser;

                        Response.Redirect("../Home.aspx?paymentTransaction=Success");

                    }




                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error : " + ex.StackTrace);
            }
        }
    }
}
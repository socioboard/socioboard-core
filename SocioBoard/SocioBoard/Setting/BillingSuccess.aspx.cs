using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SocioBoard.Domain;
using SocioBoard.Model;

namespace SocioBoard.Setting
{
    public partial class BillingSuccess : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (HttpContext.Current.Session["PackageDetails"] != null && Session["LoggedUser"] != null)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "Paypall Success", "alert('Your transaction has been Suceeded !');", true);
                    //ScriptManager.RegisterStartupScript(this, GetType(), "Script", "MyJavascriptFunction();", true);

                    //if (Session["LoggedUser"] !=null)
                    {
                        User user = (User)Session["LoggedUser"];
                        Package packageDetails = (Package)HttpContext.Current.Session["PackageDetails"];

                        UserPackageRelation objUserPackageRelation=new UserPackageRelation();

                        objUserPackageRelation.Id = new Guid();
                        objUserPackageRelation.PackageStatus = true;
                        objUserPackageRelation.UserId = user.Id;
                        objUserPackageRelation.PackageId = packageDetails.Id;

                        // Code for Update & Insert in UserPackageRelationRepository

                        UserPackageRelationRepository objUserPackageRelationRepository = new UserPackageRelationRepository();

                        objUserPackageRelationRepository.UpdateUserPackageRelation(user);

                        objUserPackageRelationRepository.AddUserPackageRelation(objUserPackageRelation);

                        Response.Redirect("Home.aspx");

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
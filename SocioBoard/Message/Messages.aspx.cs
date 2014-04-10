using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using log4net;
using SocioBoard.Helper;
using SocioBoard.Domain;

namespace SocioBoard.Message
{
    public partial class Messages : System.Web.UI.Page
    {
        ILog logger = LogManager.GetLogger(typeof(Messages));

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                
                #region for You can use only 30 days as Unpaid User

                SocioBoard.Domain.User user = (User)Session["LoggedUser"];

                if (user.PaymentStatus.ToLower() == "unpaid")
                {
                    if (!SBUtils.IsUserWorkingDaysValid(user.ExpiryDate))
                    {
                        // ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('You can use only 30 days as Unpaid User !');", true);

                        Session["GreaterThan30Days"] = "GreaterThan30Days";

                        Response.Redirect("../Settings/Billing.aspx");
                    }
                }
                #endregion

                if (!IsPostBack)
                {
                    try
                    {
                        blackcount.InnerHtml = Convert.ToString((int)Session["CountMessages"]);
                    }
                    catch (Exception ex)
                    {
                        logger.Error(ex.Message);
                        Console.WriteLine(ex.Message);
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
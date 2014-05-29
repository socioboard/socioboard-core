using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SocioBoard.Domain;

namespace SocioBoard
{
    public partial class Referrals1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                SocioBoard.Domain.User user = (User)Session["LoggedUser"];
                if (user == null)
                {

                    Response.Redirect("Default.aspx");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error : " + ex.StackTrace);
            }
        }
    }
}
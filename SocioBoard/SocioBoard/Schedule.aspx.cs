using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SocioBoard.Domain;

namespace SocioBoard
{
    public partial class Schedule : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {

                User user = (User)Session["LoggedUser"];
                if (user == null)
                    Response.Redirect("Login.aspx");
            }
            catch ( Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
            }
        }
    }
}
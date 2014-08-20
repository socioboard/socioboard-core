using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SocioBoard.Admin
{
    public partial class scheduling : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //string strHostName = HttpContext.Current.Request.UserHostAddress.ToString();
            //Label1.Text = strHostName;
            //Label2.Text = System.Net.Dns.GetHostAddresses(strHostName).GetValue(0).ToString();
            try
            {
                if (Session["AdminProfile"] == null)
                {
                    Response.Redirect("Default.aspx");
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
            }

        }
    }
}
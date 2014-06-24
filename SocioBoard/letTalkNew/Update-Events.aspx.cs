using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace letTalkNew
{
    public partial class Update_Events : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["type"] != null)
            { 
                if(Request.QueryString["type"].ToString()=="Event")
                {
                    tabs.InnerHtml = "<li><a class=\"current\" href=\"#\">Events</a></li><li><a href=\"#\">Updates</a></li>";
                }
                else if (Request.QueryString["type"].ToString() == "Updates")
                {
                    tabs.InnerHtml = "<li><a href=\"#\">Events</a></li><li><a class=\"current\" href=\"#\">Updates</a></li>";
                }
                else
                {
                    Response.Redirect("Default.aspx");
                }
            
            }

        }
    }
}
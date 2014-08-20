using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SocialSuitePro.MasterPage
{
    public partial class adminSite : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["AdminProfile"] != null)
            {
                #region For Preventing Back button in login stage
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                Response.Cache.SetExpires(DateTime.UtcNow.AddHours(-1));
                Response.Cache.SetNoStore(); 
                #endregion

                SocioBoard.Domain.Admin admin = (SocioBoard.Domain.Admin)Session["AdminProfile"];
                if (admin.Image != null)
                {
                    avtarimg.InnerHtml = "<img src=\"/" + admin.Image + "\" />";
                    
                }
                else
                {
                    avtarimg.InnerHtml = "<img src=\"../Contents/common/theme/images/avatar-style-light.jpg\" alt=\"Avatar\" />";
                }
            }
        }
    }
}
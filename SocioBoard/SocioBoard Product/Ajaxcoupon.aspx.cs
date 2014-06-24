using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SocioBoard.Helper;

namespace SocioBoard
{
    public partial class Ajaxcoupon : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.Form != null)
            {
                try
                {
                    string code = Request.Form["code"].ToString();
                    Response.Write(SBUtils.GetCouponStatus(code));
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

            }
        }
    }
}
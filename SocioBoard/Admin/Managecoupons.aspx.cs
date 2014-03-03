using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SocioBoard.Model;
using SocioBoard.Domain;
using log4net;

namespace SocioBoard.Admin
{
    public partial class Managecoupons : System.Web.UI.Page
    {

        ILog logger = LogManager.GetLogger(typeof(Managecoupons));

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                if (Session["AdminProfile"] == null)
                {
                    Response.Redirect("Default.aspx");
                }

                try
                {
                    //NewsRepository objNewsRepo = new NewsRepository();
                    //List<News> lstNews = objNewsRepo.getAllNews();
                    CouponRepository objCouponRepository = new CouponRepository();
                    List<Coupon> lstCoupon = objCouponRepository.GetAllCoupon();
                    string strNews = string.Empty;

                    foreach (Coupon item in lstCoupon)
                    {
                        strNews = strNews + "<tr class=\"gradeX\"><td><a href=\"EditCoupons.aspx?id=" + item.Id + "\">Edit</a></td><td>" + item.CouponCode + "</td><td>" + item.EntryCouponDate + "</td><td>" + item.ExpCouponDate + "</td><td class=\"center\">" + item.Status + "</td></tr>";
                    }
                    divNews.InnerHtml = strNews;
                }
                catch (Exception Err)
                {
                    logger.Error(Err.Message);
                    Response.Write(Err.StackTrace);
                }
            }
        }
    }
}
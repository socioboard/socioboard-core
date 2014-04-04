using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SocioBoard.Domain;
using SocioBoard.Model;
using log4net;

namespace SocioBoard.Admin
{
    public partial class Addcoupons : System.Web.UI.Page
    {
        ILog logger = LogManager.GetLogger(typeof(Addcoupons));

        protected void Page_Load(object sender, EventArgs e)
        {
            Coupon objCoupon = new Coupon();
            CouponRepository objCouponRepository = new CouponRepository();
            List<Coupon> lstCoupon = objCouponRepository.GetAllCoupon();
            Label2.Text = (lstCoupon.Count + 1).ToString();

            if (!IsPostBack)
            {
                //if (Session["AdminProfile"] == null)
                //{
                //    Response.Redirect("Default.aspx");
                //}
               
                if (Request.QueryString["Id"] != null)
                {
                    try
                    {
                        //Coupon objCoupon = new Coupon();
                        //CouponRepository objCouponRepository = new CouponRepository();
                        //List<Coupon> lstCoupon =  objCouponRepository.GetAllCoupon();
                        //Label2.Text = (lstCoupon.Count + 1).ToString();

                        //News news = objNewsRepo.getNewsDetailsbyId(Guid.Parse(Request.QueryString["Id"].ToString()));
                        //txtNews.Text = news.NewsDetail;
                       // datepicker.Text = news.ExpiryDate.ToString();
                       // ddlStatus.SelectedValue = news.Status.ToString();
                    }
                    catch (Exception ex)
                    {
                        logger.Error(ex.Message);
                    }
                }
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                //News objNews = new News();
                //NewsRepository objNewsRepo = new NewsRepository();
                Coupon objCoupon = new Coupon();
                CouponRepository objCouponRepository = new CouponRepository();

                objCoupon.Id = Guid.NewGuid();
                objCoupon.CouponCode = txtcoupons.Text;
                objCoupon.EntryCouponDate = DateTime.Now;
                objCoupon.ExpCouponDate = DateTime.Now;
                objCoupon.Status = "0";

                if (objCouponRepository.GetCouponByCouponCode(objCoupon).Count < 1 || objCouponRepository.GetCouponByCouponCode(objCoupon).Count==0)
                {
                    if (objCouponRepository.Add(objCoupon) == "Added")
                    {
                        Label1.Text = "Coupon Added";
                        txtcoupons.Text = "";
                    }
                    else
                    {
                        Label1.Text = "Not Added";
                    }
                }
                else
                {
                    Label1.Text = "Coupon already exist";
                }

                
            }
            catch (Exception Err)
            {
                logger.Error(Err.Message);
                Response.Write(Err.Message);
            }
        }

        //public string AddUpdateCoupon()
        //{
        //    string ret = string.Empty;
        //    if (Request.QueryString["Id"] != null)
        //    {
        //        ret = Request.QueryString["Id"].ToString();
        //    }
        //    else
        //    {
        //        ret = Guid.NewGuid().ToString();
        //    }
        //    return ret;
        //}
    }
}
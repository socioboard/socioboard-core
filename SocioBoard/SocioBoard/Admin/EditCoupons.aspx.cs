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
    public partial class EditCoupons : System.Web.UI.Page
    {
        ILog logger = LogManager.GetLogger(typeof(EditCoupons));

        protected void Page_Load(object sender, EventArgs e)
        {


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
                        //NewsRepository objNewsRepo = new NewsRepository();
                        //News news = objNewsRepo.getNewsDetailsbyId(Guid.Parse(Request.QueryString["Id"].ToString()));
                        CouponRepository objCouponRepository = new CouponRepository();
                        Coupon objCoupon=new Coupon ();
                        objCoupon.Id = (Guid.Parse(Request.QueryString["Id"].ToString()));
                        List<Coupon> lstcoupons = objCouponRepository.GetCouponByCouponId(objCoupon);


                        txtNews.Text = lstcoupons[0].CouponCode;
                        datepicker.Text = lstcoupons[0].EntryCouponDate.ToString();
                        datepicker1.Text = lstcoupons[0].ExpCouponDate.ToString();
                        ddlStatus.SelectedValue = lstcoupons[0].Status;
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
                Coupon objCoupon = new Coupon();
                CouponRepository objCouponRepository = new CouponRepository();
                objCoupon.Id = (Guid.Parse(Request.QueryString["Id"].ToString()));
                objCoupon.CouponCode =txtNews.Text;
                objCoupon.EntryCouponDate = Convert.ToDateTime(datepicker.Text);
                objCoupon.ExpCouponDate = Convert.ToDateTime(datepicker1.Text);
                objCoupon.Status = ddlStatus.SelectedValue;
                if (objCouponRepository.GetCouponByCouponCode(objCoupon).Count < 1 || objCouponRepository.GetCouponByCouponCode(objCoupon).Count == 0)
                {
                    objCouponRepository.SetCouponById(objCoupon);
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Modified Successfully');", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Coupon Already Exist');", true);
                }
               
            
            }
            catch (Exception Err)
            {
                logger.Error(Err.Message);
                Response.Write(Err.Message);
            }
        }

        
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using log4net;
using SocioBoard.Model;
using SocioBoard.Domain;

namespace SocioBoard.Admin
{
    public partial class EditMandrillAccount : System.Web.UI.Page
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
                        MandrillAccountRepository objMandrillAccountRepository = new MandrillAccountRepository();
                        MandrillAccount objMandrillAccount = new MandrillAccount();
                        objMandrillAccount.Id = (Guid.Parse(Request.QueryString["Id"].ToString()));
                        MandrillAccount lstMandrillAccount = objMandrillAccountRepository.GetMandrillAccountById(objMandrillAccount);


                        txtUsername.Text = lstMandrillAccount.UserName;
                        txtpassword.Text = lstMandrillAccount.Password;
                        ddlStatus.SelectedValue = lstMandrillAccount.Status;
                       
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
                MandrillAccountRepository objMandrillAccountRepository = new MandrillAccountRepository();
                MandrillAccount objMandrillAccount = new MandrillAccount();

                objMandrillAccount.Id = (Guid.Parse(Request.QueryString["Id"].ToString()));
                objMandrillAccount.UserName = txtUsername.Text;
                objMandrillAccount.Password = txtpassword.Text;
                objMandrillAccount.Status = ddlStatus.SelectedValue;
                objMandrillAccount.Status = ddlStatus.SelectedValue;
                if (objMandrillAccountRepository.GetMandrillAccountByStatus(objMandrillAccount).Count < 1 || objMandrillAccountRepository.GetMandrillAccountByStatus(objMandrillAccount).Count == 0)
                {
                    objMandrillAccountRepository.UpdateMandrillAccount(objMandrillAccount);
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
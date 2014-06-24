using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SocioBoard.Model;
using SocioBoard.Domain;
using log4net;

namespace SocialSuitePro.Admin
{
    public partial class EditUserDetail : System.Web.UI.Page
    {
        UserRepository objUerRepo = new UserRepository();
        ILog logger = LogManager.GetLogger(typeof(EditUserDetail));

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    User user = new User();
                    PackageRepository objPgeRepo = new PackageRepository();
                    ddlPackage.DataSource = objPgeRepo.getAllPackage();
                    ddlPackage.DataTextField = "PackageName";
                    ddlPackage.DataValueField = "PackageName";
                    ddlPackage.DataBind();
                    // ddlPackage.Items.Insert();
                    user = objUerRepo.getUsersById(Guid.Parse(Request.QueryString["id"].ToString()));
                    if (user != null)
                    {
                        txtName.Text = user.UserName;
                        txtEmail.Text = user.EmailId;
                        // txtdatepicker.Text = user.ExpiryDate.ToString();
                        datepicker.Text = user.ExpiryDate.ToString();
                        ddlPackage.SelectedValue = user.AccountType.ToString(); //user.PaymentStatus;
                        ddlStatus.SelectedValue = user.UserStatus.ToString();
                    }
                }
                catch (Exception ex)
                {
                    logger.Error(ex.StackTrace);
                }
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                User user = new User();
                user.Id =Guid.Parse(Request.QueryString["id"].ToString());
                user.EmailId = txtEmail.Text;
                user.ExpiryDate = Convert.ToDateTime(datepicker.Text);
                user.UserName = txtName.Text;
                user.UserStatus=1;
                UserRepository.Update(user);
            }
            catch (Exception Err)
            {
                logger.Error(Err.Message);
                Console.Write(Err.StackTrace);
            }
        }
    }
}
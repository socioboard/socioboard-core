using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SocioBoard.Domain;
using SocioBoard.Model;

namespace SocialSuitePro.Admin
{
    public partial class AdminSettings : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ddlTimeZone.DataSource = TimeZoneInfo.GetSystemTimeZones();
                ddlTimeZone.DataBind(); 
            }
        }
        public void changePassoword(object sender, EventArgs e)
        {
            if (txtPassword.Text != "" && txtConfirmPassword.Text != "")
            {
                if (txtPassword.Text == txtConfirmPassword.Text)
                {
                    SocioBoard.Domain.Admin admin = (SocioBoard.Domain.Admin)Session["AdminProfile"];
                    AdminRepository adminrepo = new AdminRepository();
                    adminrepo.ChangePassword(txtPassword.Text, admin.Password,admin.UserName);
                    txtConfirmPassword.Text = string.Empty;
                    txtPassword.Text = string.Empty;
                }
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {

            SocioBoard.Domain.Admin admin = (SocioBoard.Domain.Admin)Session["AdminProfile"];
            if (imgfileupload.HasFile)
            {
                string path = Server.MapPath("~/Contents/img/user_img/" + imgfileupload.FileName);
                imgfileupload.SaveAs(path);
                admin.Image = "../Contents/img/user_img/" + imgfileupload.FileName;
            }
            admin.FirstName = txtFirstName.Text;
            admin.LastName = txtLastName.Text;

            admin.UserName = txtUserName.Text;
            admin.TimeZone = ddlTimeZone.SelectedItem.Text;
            AdminRepository.Update(admin);
            Session["AdminProfile"] = admin;
            txtFirstName.Text = string.Empty;
            txtLastName.Text = string.Empty;
            txtPassword.Text = string.Empty;
            txtConfirmPassword.Text = string.Empty;
            txtUserName.Text = string.Empty;


        }


    }
}
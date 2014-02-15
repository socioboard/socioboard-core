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

                if (Session["AdminProfile"] == null)
                {
                    Response.Redirect("Default.aspx");
                }
                load();
            }
        }
        public void changePassoword(object sender, EventArgs e)
        {
            SocioBoard.Domain.Admin admin = (SocioBoard.Domain.Admin)Session["AdminProfile"];
            AdminRepository adminrepo = new AdminRepository();

            if (txtPassword.Text != "" && txtConfirmPassword.Text != "")
            {
                if (txtPassword.Text == admin.Password)
                {

                    adminrepo.ChangePwd(txtConfirmPassword.Text, admin.UserName);
                    txtConfirmPassword.Text = string.Empty;
                    txtPassword.Text = string.Empty;
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Password change successfully');", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Wrong Password');", true);
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Password must be blank');", true);
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
            if (txtFirstName.Text == "" || txtLastName.Text == "" || txtUserName.Text=="")
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please fill all the fields');", true);
            }
            admin.TimeZone = ddlTimeZone.SelectedItem.Text;
            AdminRepository.Update(admin);
            Session["AdminProfile"] = admin;
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Updated successfully');", true);
            load();
        }


        public void load()
        {

            SocioBoard.Domain.Admin admin = (SocioBoard.Domain.Admin)Session["AdminProfile"];
            txtFirstName.Text = admin.FirstName.ToString();
            txtLastName.Text = admin.LastName.ToString();
            txtUserName.Text = admin.UserName.ToString();
            txtPassword.Text = "";
            txtConfirmPassword.Text = ""; 
            custImg.ImageUrl = admin.Image.ToString();
            if (admin.TimeZone != null)
            {
                ddlTimeZone.DataSource = TimeZoneInfo.GetSystemTimeZones();
                ddlTimeZone.DataBind();
                ddlTimeZone.SelectedValue = admin.TimeZone.ToString(); //user.PaymentStatus;
            }
            else
            {
                ddlTimeZone.DataSource = TimeZoneInfo.GetSystemTimeZones();
                ddlTimeZone.DataBind();
            }
        
        }

    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Drawing.Imaging;
using System.Configuration;
using SocioBoard.Domain;
using SocioBoard.Model;
using System.Collections.ObjectModel;
namespace SocialSuitePro.Settings
{
    public partial class PersonalSettings : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    User user = (User)Session["LoggedUser"];
                    if (user == null)
                        Response.Redirect("/Default.aspx");

                    string[] username = user.UserName.Split(' ');
                    txtFirstName.Text = username[0];
                    txtLastName.Text = username[1];
                    memberName.InnerHtml = user.UserName;
                    txtEmail.Text = user.EmailId;
                    //ddlTimeZone.DataSource = TimeZoneInfo.GetSystemTimeZones();
                    //ddlTimeZone.DataBind();
                    ddlTimeZone.DataSource = GetTimeZones();
                    ddlTimeZone.DataTextField = "Name";
                    ddlTimeZone.DataValueField = "ID";
                    ddlTimeZone.DataBind();
                    //ListItem lst=new ListItem();
                    //lst.Text=user.TimeZone;
                    //lst.Value=user.TimeZone;
                    ddlTimeZone.SelectedValue = user.TimeZone.ToString();
                    email_personal_for_setting.InnerHtml = user.EmailId;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.StackTrace);   
                }
            }

        }

        public void changePassoword(object sender, EventArgs e)
        {
            if (txtPassword.Text != "" && txtConfirmPassword.Text != "")
            {
                if (txtPassword.Text == txtConfirmPassword.Text)
                {
                    User user = (User)Session["LoggedUser"];
                    Registration regpage = new Registration();
                    string changedpassword = regpage.MD5Hash(txtConfirmPassword.Text);
                    UserRepository userrepo = new UserRepository();
                    userrepo.ChangePassword(changedpassword, user.Password, user.EmailId);
                    txtConfirmPassword.Text = string.Empty;
                    txtPassword.Text = string.Empty;
                }
                else
                {
                
                }
            }
            else
            { 
            
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
          
            User user = (User)Session["LoggedUser"];
            if (imgfileupload.HasFile)
            {
                string path = Server.MapPath("~/Contents/img/user_img/" +imgfileupload.FileName);
                imgfileupload.SaveAs(path);
                user.ProfileUrl = "../Contents/img/user_img/" +imgfileupload.FileName;
            }
            user.UserName = txtFirstName.Text + " " + txtLastName.Text;
            user.TimeZone = ddlTimeZone.SelectedItem.Value;
            UserRepository.Update(user);
            Session["LoggedUser"] = user;
            //Response.Redirect(Request.RawUrl);
            Response.Redirect("PersonalSettings.aspx");
           }
        public Collection<MyStruct> GetTimeZones()
        {
            var myClass = new Collection<MyStruct>();
            foreach (var timeZoneInfo in TimeZoneInfo.GetSystemTimeZones())
            {
                myClass.Add(new MyStruct { Name = timeZoneInfo.DisplayName, ID = timeZoneInfo.Id });

            }
            return myClass;
        }
        public struct MyStruct
        {
            public string Name { get; set; }
            public string ID { get; set; }
        }
    }

}
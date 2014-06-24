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
using System.Collections.ObjectModel;
using SocioBoard;
using SocioBoard.Helper;
using System.Text.RegularExpressions;
using SocioBoard.Model;
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

                    #region for You can use only 30 days as Unpaid User

                    if (user.PaymentStatus.ToLower() == "unpaid")
                    {
                        if (!SBUtils.IsUserWorkingDaysValid(user.ExpiryDate))
                        {
                            // ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('You can use only 30 days as Unpaid User !');", true);

                            Session["GreaterThan30Days"] = "GreaterThan30Days";

                            Response.Redirect("/Settings/Billing.aspx");
                        }
                    }

                    Session["GreaterThan30Days"] = null;
                    #endregion

                    if (user == null)
                        Response.Redirect("/Default.aspx");
                    string lastname = string.Empty;
                    string[] username = user.UserName.Split(' ');
                    txtFirstName.Text = username[0];
                    for (int i = 1; i < username.Length;i++ )
                    {
                        lastname += username[i];
                    }

                    txtLastName.Text = lastname;
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
                   // email_personal_for_setting.InnerHtml = user.EmailId;
                    if (user.ProfileUrl != null)
                    {
                        custImg.Attributes.Add("src", user.ProfileUrl.ToString());
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.StackTrace);   
                }
            }

        }

        public void changePassoword(object sender, EventArgs e)
        {
            
            try
            {
                Registration regpage = new Registration();
                string OldPassword = regpage.MD5Hash(txtOldPassword.Text);
                if (txtOldPassword.Text != "")
                {
                    if (txtPassword.Text.Trim() != "" && txtConfirmPassword.Text.Trim() != "" && txtOldPassword.Text != "")
                    {
                        if (txtPassword.Text == txtConfirmPassword.Text)
                        {
                            User user = (User)Session["LoggedUser"];
                            if (OldPassword == user.Password)
                            {

                                string changedpassword = regpage.MD5Hash(txtConfirmPassword.Text);
                                UserRepository userrepo = new UserRepository();
                                userrepo.ChangePassword(changedpassword, user.Password, user.EmailId);
                                txtConfirmPassword.Text = string.Empty;
                                txtPassword.Text = string.Empty;
                                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Message", "alert('Your password has been changed successfully.')", true);
                            }
                            else
                            {
                                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Message", "alert('Your password is Incorrect.')", true);
                            }
                        }
                        else
                        {
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Message", "alert('Password Mismatch.')", true);
                        }
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Message", "alert('Invalid Password.')", true);
                    }
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Message", "alert('Please enter your old password.')", true);
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
            }
        }



        protected void btnSave_Click(object sender, EventArgs e)
        {
            UserRepository objUserRepository = new UserRepository();
            try
            {
                User user = (User)Session["LoggedUser"];
                if (imgfileupload.HasFile)
                {
                    if (imgfileupload.FileName != null)
                    {
                        //string[] strarr = imgfileupload.FileName.Split('.');
                        ////imgfileupload.FileName
                        string strarr = Path.GetExtension(imgfileupload.FileName);

                        if (strarr.ToLower() == ".png" || strarr.ToLower() == ".jpeg" || strarr.ToLower() == ".jpg")
                        {

                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please use jpeg ,jpg or png format image');", true);
                            return;
                        }

                    }

                    string path = Server.MapPath("~/Contents/img/user_img/" + imgfileupload.FileName);
                    imgfileupload.SaveAs(path);
                    user.ProfileUrl = "../Contents/img/user_img/" + imgfileupload.FileName;
                }
                if (txtEmail.Text != null || txtEmail.Text != "")
                {
                    bool isEmail = Regex.IsMatch(txtEmail.Text.Trim(), @"\A(?:[A-Za-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[A-Za-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[A-Za-z0-9](?:[A-Za-z0-9-]*[A-Za-z0-9])?\.)+[A-Za-z0-9](?:[A-Za-z0-9-]*[A-Za-z0-9])?)\Z");
                    if (isEmail)
                    {


                        if (txtEmail.Text.Trim()!=user.EmailId)
                        {
                             bool useremailcheck = objUserRepository.IsUserExist(txtEmail.Text.Trim());
                             if (useremailcheck != true)
                             {
                                 try
                                 {
                                     user.UserName = txtFirstName.Text + " " + txtLastName.Text;
                                     user.TimeZone = ddlTimeZone.SelectedItem.Value;
                                     user.EmailId = txtEmail.Text;
                                     UserRepository.Update(user);
                                     Session["LoggedUser"] = user;
                                 }
                                 catch (Exception ex)
                                 {

                                     Console.WriteLine(ex.Message);
                                 }
                             }
                             else
                             {

                                 ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('EmailId already Exist');", true);
                                 return;

                             }
                    }
                    else
                        {
                            try
                            {
                                user.UserName = txtFirstName.Text + " " + txtLastName.Text;
                                user.TimeZone = ddlTimeZone.SelectedItem.Value;
                                user.EmailId = txtEmail.Text;
                                UserRepository.Update(user);
                                Session["LoggedUser"] = user;
                            }
                            catch (Exception ex)
                            {

                                Console.WriteLine(ex.Message);
                            }
                        }
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please enter a valid emailId');", true);
                        return;
                        
                    }
                   
                   
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please enter a emailId');", true);
                    return;
                    
                   
                }
                //Response.Redirect(Request.RawUrl);
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
            }
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
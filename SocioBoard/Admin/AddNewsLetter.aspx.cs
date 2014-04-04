using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SocioBoard.Model;
using SocioBoard.Domain;
using log4net;
using System.Configuration;
using SocioBoard.Helper;

namespace SocialSuitePro.Admin
{
    public partial class AddNewsLetter : System.Web.UI.Page
    {
        ILog logger = LogManager.GetLogger(typeof(AddNewsLetter));

        protected void Page_Load(object sender, EventArgs e)
        {


            UserRepository objUerRepo = new UserRepository();
            if (!IsPostBack)
            {

                if (Session["AdminProfile"] == null)
                {
                    Response.Redirect("Default.aspx");
                }

                try
                {
                    chkUser.DataSource = objUerRepo.getAllUsers();
                    chkUser.DataTextField = "UserName";
                    chkUser.DataValueField = "Id";
                    chkUser.DataBind();
                    if (Request.QueryString["Id"] != null)
                    {
                        NewsLetterRepository objNewsRepo = new NewsLetterRepository();
                        NewsLetter news = objNewsRepo.getNewsLetterDetailsbyId(Guid.Parse(Request.QueryString["Id"].ToString()));
                        txtSubject.Text = news.Subject;
                        Editor.Text = news.NewsLetterBody;
                        //txtSendDate.Text = news.SendDate.ToString();

                        //  datepicker.Text = news.ExpiryDate.ToString();
                        //  ddlStatus.SelectedValue = news.Status.ToString();
                    }
                }
                catch (Exception Err)
                {
                    logger.Error(Err.Message);
                    Response.Write(Err.StackTrace);
                }
            }
        }

        //protected void btnSave_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        NewsLetter objNl = new NewsLetter();
        //        NewsLetterRepository objnlRepo = new NewsLetterRepository();
        //        foreach (ListItem listItem in chkUser.Items)
        //        {
        //            if (listItem.Selected)
        //            {
        //                objNl.Id = Guid.NewGuid();
        //                objNl.Subject = txtSubject.Text;
        //                objNl.NewsLetterDetail = Editor.Text;
        //               // objNl.SendDate = DateTime.Parse(txtSendDate.Text);
        //                objNl.SendStatus = false;
        //                objNl.UserId = Guid.Parse(listItem.Value);
        //                if (!objnlRepo.checkNewsLetterExists(Editor.Text))
        //                    objnlRepo.AddNewsLetter(objNl);
        //                else
        //                    objnlRepo.UpdateNewsLetter(objNl);
        //            }
        //        }

        //    }
        //    catch (Exception Err)
        //    {
        //        logger.Error(Err.Message);
        //        Response.Write(Err.StackTrace);
        //    }
        //}

        protected void sendmail_Click(object sender, EventArgs e)
        {
            string username = ConfigurationManager.AppSettings["username"];
            string host = ConfigurationManager.AppSettings["host"];
            string port = ConfigurationManager.AppSettings["port"];
            string pass = ConfigurationManager.AppSettings["password"];
            string from = ConfigurationManager.AppSettings["fromemail"];
            int noofuserselected = 0;

            foreach (ListItem listItem in chkUser.Items)
            {
                if (listItem.Selected == true)
                {
                    noofuserselected++;
                }
            }



            if (txtSubject.Text == "" || Editor.Text == "" || noofuserselected == 0)
            {
                if (noofuserselected == 0)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please filled subject/Email or select users!');", true);
                    return;
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please filled subject/Email content!');", true);
                    return;
                }
            }

            try
            {
                NewsLetter objNl = new NewsLetter();
                User objUser = new User();
                UserRepository objUserRepository = new UserRepository();
                NewsLetterRepository objnlRepo = new NewsLetterRepository();
                foreach (ListItem listItem in chkUser.Items)
                {
                    if (listItem.Selected)
                    {


                        objUser = objUserRepository.getUsersById(Guid.Parse(listItem.Value));
                        if (objUser != null)
                        {
                            MailHelper.SendSendGridMail(host, Convert.ToInt32(port), from, "", objUser.EmailId.ToString(), string.Empty, string.Empty, txtSubject.Text, Editor.Text, username, pass);


                            objNl.Id = Guid.NewGuid();
                            objNl.Subject = txtSubject.Text;
                            objNl.NewsLetterBody = Editor.Text;
                            objNl.SendDate = DateTime.Parse(txtSendDate.Text);
                            objNl.SendStatus = false;
                            objNl.UserId = Guid.Parse(listItem.Value);
                            objNl.EntryDate = DateTime.Now;
                            //if (!objnlRepo.checkNewsLetterExists(Editor.Text))
                            //    objnlRepo.AddNewsLetter(objNl);
                            //else
                                //objnlRepo.UpdateNewsLetter(objNl);
                            objnlRepo.AddNewsLetter(objNl);
                        }
                    }
                }
                txtSubject.Text = "";
                Editor.Text = "";
                clearAll();
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('News Letter has been Added!');", true);

            }
            catch (Exception Err)
            {
                txtSubject.Text = "";
                Editor.Text = "";
                clearAll();
                logger.Error(Err.Message);
                Console.Write(Err.StackTrace);
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert(" + Err.Message + "'!');", true);

            }
        }

        protected void all_Click(object sender, EventArgs e)
        {
            try
            {
                NewsLetter objNl = new NewsLetter();
                NewsLetterRepository objnlRepo = new NewsLetterRepository();
                foreach (ListItem listItem in chkUser.Items)
                {
                    listItem.Selected = true;

                }

            }
            catch (Exception Err)
            {
                logger.Error(Err.Message);
                Response.Write(Err.StackTrace);
            }
        }

        protected void unpaid_Click(object sender, EventArgs e)
        {
            clearAll();
            try
            {
                NewsLetter objNl = new NewsLetter();
                User objUser = new User();
                UserRepository objUserRepository = new UserRepository();
                NewsLetterRepository objnlRepo = new NewsLetterRepository();
                foreach (ListItem listItem in chkUser.Items)
                {
                    objUser = objUserRepository.getUsersById(Guid.Parse(listItem.Value));
                    if (objUser.PaymentStatus == "unpaid")
                    {
                        listItem.Selected = true;
                    }

                }

            }
            catch (Exception Err)
            {
                logger.Error(Err.Message);
                Response.Write(Err.StackTrace);
            }
        }

        protected void paid_Click(object sender, EventArgs e)
        {
            clearAll();
            try
            {
                NewsLetter objNl = new NewsLetter();
                User objUser = new User();
                UserRepository objUserRepository = new UserRepository();
                NewsLetterRepository objnlRepo = new NewsLetterRepository();
                foreach (ListItem listItem in chkUser.Items)
                {
                    objUser = objUserRepository.getUsersById(Guid.Parse(listItem.Value));
                    if (objUser.PaymentStatus == "Paid")
                    {
                        listItem.Selected = true;
                    }

                }

            }
            catch (Exception Err)
            {
                logger.Error(Err.Message);
                Response.Write(Err.StackTrace);
            }
        }

        protected void unselect_Click(object sender, EventArgs e)
        {
            clearAll();
        }

        public void clearAll()
        {
            try
            {
                NewsLetter objNl = new NewsLetter();
                NewsLetterRepository objnlRepo = new NewsLetterRepository();
                foreach (ListItem listItem in chkUser.Items)
                {
                    listItem.Selected = false;

                }

            }
            catch (Exception Err)
            {
                logger.Error(Err.Message);
                Response.Write(Err.StackTrace);
            }
        }
    }
}
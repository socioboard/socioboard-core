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
    public partial class AddNewsLetter : System.Web.UI.Page
    {
        ILog logger = LogManager.GetLogger(typeof(AddNewsLetter));

        protected void Page_Load(object sender, EventArgs e)
        {
            UserRepository objUerRepo = new UserRepository();
            if (!IsPostBack)
            {
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
                        Editor.Text = news.NewsLetterDetail;
                        txtSendDate.Text = news.SendDate.ToString();

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

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                NewsLetter objNl = new NewsLetter();
                NewsLetterRepository objnlRepo = new NewsLetterRepository();
                foreach (ListItem listItem in chkUser.Items)
                {
                    if (listItem.Selected)
                    { 
                           objNl.Id = Guid.NewGuid();
                            objNl.Subject = txtSubject.Text;
                            objNl.NewsLetterDetail = Editor.Text;
                            objNl.SendDate = DateTime.Parse(txtSendDate.Text);
                            objNl.SendStatus = false;
                            objNl.UserId = Guid.Parse(listItem.Value);
                            if (!objnlRepo.checkNewsLetterExists(Editor.Text))
                                objnlRepo.AddNewsLetter(objNl);
                            else
                                objnlRepo.UpdateNewsLetter(objNl);
                    }
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
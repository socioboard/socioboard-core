using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SocioBoard.Domain;
using SocioBoard.Model;
using log4net;

namespace WooSuite.Admin
{
    public partial class AddNews : System.Web.UI.Page
    {
        ILog logger = LogManager.GetLogger(typeof(AddNews));

        protected void Page_Load(object sender, EventArgs e)
        {


            if (!IsPostBack)
            {
                if (Session["AdminProfile"] == null)
                {
                    Response.Redirect("Default.aspx");
                }

                if (Request.QueryString["Id"] != null)
                {
                    try
                    {
                        NewsRepository objNewsRepo = new NewsRepository();
                        News news = objNewsRepo.getNewsDetailsbyId(Guid.Parse(Request.QueryString["Id"].ToString()));
                        txtNews.Text = news.NewsDetail;
                        datepicker.Text = news.ExpiryDate.ToString();
                        ddlStatus.SelectedValue = news.Status.ToString();
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
                News objNews = new News();
                NewsRepository objNewsRepo = new NewsRepository();
                objNews.NewsDetail = txtNews.Text;
                objNews.Status = bool.Parse(ddlStatus.SelectedValue);
                objNews.EntryDate = DateTime.Now;
                objNews.ExpiryDate = Convert.ToDateTime(datepicker.Text);
                objNews.Id = Guid.Parse(AddUpdateNews());
                if (objNewsRepo.checkNewsExists(txtNews.Text))
                    objNewsRepo.UpdateNews(objNews);
                else
                    objNewsRepo.AddNews(objNews);
            }
            catch (Exception Err)
            {
                logger.Error(Err.Message);
                Response.Write(Err.Message);
            }
        }

        public string AddUpdateNews()
        {
            string ret = string.Empty;
            if (Request.QueryString["Id"] != null)
            {
                ret = Request.QueryString["Id"].ToString();
            }
            else
            {
                ret = Guid.NewGuid().ToString();
            }
            return ret;
        }
    }
}
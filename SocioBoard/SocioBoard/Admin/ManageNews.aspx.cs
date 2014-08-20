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
    public partial class ManageNews : System.Web.UI.Page
    {
        ILog logger = LogManager.GetLogger(typeof(ManageNews));

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                if (Session["AdminProfile"] == null)
                {
                    Response.Redirect("Default.aspx");
                }

                try
                {
                    NewsRepository objNewsRepo = new NewsRepository();
                    List<News> lstNews = objNewsRepo.getAllNews();
                    string strNews = string.Empty;

                    foreach (News item in lstNews)
                    {
                        try
                        {
                            if (DateTime.Now > item.ExpiryDate)
                            {
                                item.Status = false;
                                objNewsRepo.UpdateNews(item);
                            }
                        }
                        catch (Exception Err)
                        {

                            logger.Error(Err.Message);
                            Response.Write(Err.StackTrace);
                        }


                    }


                    foreach (News item in lstNews)
                    {
                        strNews = strNews + "<tr class=\"gradeX\"><td><a href=\"AddNews.aspx?id=" + item.Id + "\">Edit</a></td><td>" + item.NewsDetail + "</td><td>" + item.EntryDate + "</td><td>" + item.ExpiryDate + "</td><td class=\"center\">" + item.Status + "</td></tr>";
                    }
                    divNews.InnerHtml = strNews;
                }
                catch (Exception Err)
                {
                    logger.Error(Err.Message);
                    Response.Write(Err.StackTrace);
                }
            }
        }
    }
}
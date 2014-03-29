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
    public partial class ManageNewsLetter : System.Web.UI.Page
    {
        ILog logger = LogManager.GetLogger(typeof(ManageNewsLetter));
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
                    NewsLetterRepository objNewsRepo = new NewsLetterRepository();
                    List<NewsLetter> lstNews = objNewsRepo.getAllNewsLetter();
                    string strNews = string.Empty;

                    foreach (NewsLetter item in lstNews)
                    {
                        strNews = strNews + "<tr class=\"gradeX\"><td><a href=\"AddNewsLetter.aspx?id=" + item.Id + "\">Resend</a></td><td>" + item.Subject + "</td><td>" + item.NewsLetterBody + "</td><td>" + item.SendDate + "</td><td class=\"center\">" + item.SendStatus + "</td></tr>";
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
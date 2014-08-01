using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SocioBoard.Model;
using SocioBoard.Domain;
using log4net;

namespace SocioBoard.Admin
{
    public partial class ManageMandrillAccount : System.Web.UI.Page
    {
        ILog logger = LogManager.GetLogger(typeof(ManageMandrillAccount));
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
                    //NewsRepository objNewsRepo = new NewsRepository();
                    //List<News> lstNews = objNewsRepo.getAllNews();
                    MandrillAccountRepository objMandrillAccountRepository = new MandrillAccountRepository();
                    List<MandrillAccount> lstMandrillAccount = objMandrillAccountRepository.GetAllMandrillAccount();
                    string strNews = string.Empty;

                    foreach (MandrillAccount item in lstMandrillAccount)
                    {
                        strNews = strNews + "<tr class=\"gradeX\"><td><a href=\"EditCoupons.aspx?id=" + item.Id + "\">Edit</a></td><td>" + item.UserName + "</td><td>" + item.Password + "</td><td>" + item.Total + "</td><td class=\"center\">" + item.Status + "</td><td class=\"center\">" + item.EntryDate + "</td></tr>";
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
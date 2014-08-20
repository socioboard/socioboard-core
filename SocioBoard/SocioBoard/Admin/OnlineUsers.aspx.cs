using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SocioBoard.Domain;
using log4net;

namespace SocialSuitePro.Admin
{
    public partial class OnlineUsers : System.Web.UI.Page
    {
        ILog logger = LogManager.GetLogger(typeof(OnlineUsers));
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
                    string strUser = string.Empty;

                    if (Application["OnlineUsers"] != null)
                    {
                        List<User> lstUser = (List<User>)Application["OnlineUsers"];
                        foreach (User item in lstUser)
                        {
                            strUser = strUser + "<tr class=\"gradeX\"><td><a href=\"EditUserDetail.aspx?id=" + item.Id + "\">Edit</a></td><td>" + item.UserName + "</td><td>" + item.PaymentStatus + "</td><td>" + item.TimeZone + "</td><td class=\"center\">" + item.AccountType + "</td></tr>";
                        }
                        divNews.InnerHtml = strUser;
                    }
                }
                catch (Exception ex)
                {
                    logger.Error(ex.Message);
                }
            }
        }
    }
}
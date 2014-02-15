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
    public partial class UserDetails : System.Web.UI.Page
    {
        ILog logger = LogManager.GetLogger(typeof(UserDetails));
        UserRepository objUserRepo = new UserRepository();
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
                    List<User> lstUser = objUserRepo.getAllUsers();
                    string strUser = string.Empty;
                    foreach (User item in lstUser)
                    {
                      //  strUser += "<tr class=\"gradeX\"><td><a href=\"EditUserDetail.aspx?id=" + item.Id + "\">Edit</a></td><td><a href=\"#\" onclick=\"del('" + item.Id + "')\">Delete</a></td><td>" + item.UserName + "</td><td>" + item.AccountType + "</td><td>" + item.CreateDate + "</td><td class=\"center\">" + item.EmailId + "</td><td class=\"center\">" + item.ExpiryDate + "</td><td class=\"center\">" + item.UserStatus + "</td></tr>";
                        strUser += "<tr class=\"gradeX\"><td><a href=\"EditUserDetail.aspx?id=" + item.Id + "\">Edit</a></td><td><a href=\"#\" onclick=\"del('" + item.Id + "')\">Delete</a></td><td>" + item.UserName + "</td><td>" + item.AccountType + "</td><td>" + item.CreateDate + "</td><td class=\"center\">" + item.EmailId + "</td><td class=\"center\">" + item.UserStatus + "</td></tr>";
                    }
                    Users.InnerHtml = strUser;
                }
                catch (Exception ex)
                {

                    logger.Error(ex.Message);
                }
            }
        }
    }
}
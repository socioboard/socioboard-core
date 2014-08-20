using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SocioBoard.Model;
using SocioBoard.Domain;

namespace SocioBoard.Admin
{
    public partial class UserLoginDetails : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
            try
            {
                if (Session["AdminProfile"] == null)
                {
                    Response.Redirect("Default.aspx");
                }
                string ret = string.Empty;
                if (Request.QueryString != null)
                {
                    LoginLogsRepository objLoginLogsRepository = new LoginLogsRepository();
                    List<LoginLogs> lstLoginLogs = objLoginLogsRepository.GetLoginDetailsByUserId(Guid.Parse(Request.QueryString["id"].ToString()));

                    foreach (LoginLogs item in lstLoginLogs)
                    {
                        try
                        {
                            ret += "<tr class=\"gradeX\"><td>" + item.UserName + "</td><td>" + item.LoginTime + "</td></tr>";
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("Error : " + ex.StackTrace);
                        }
                    }
                    dtls.InnerHtml = ret;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error : " + ex.StackTrace);
            }
        }
    }
}
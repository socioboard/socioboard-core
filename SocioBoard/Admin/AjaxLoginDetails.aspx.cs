using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SocioBoard.Domain;
using SocioBoard.Model;
using SocioBoard.Admin.Scheduler;

namespace SocioBoard.Admin
{
    public partial class AjaxLoginDetails : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string str = string.Empty;
            try
            {

                LoginLogs objLoginLogs = new LoginLogs();
                LoginLogsRepository objLoginLogsRepository = new LoginLogsRepository();
                List<LoginLogsTracker> lstLoginLogsTracker = objLoginLogsRepository.GetAllLoginLogsDetails();
                foreach (LoginLogsTracker item in lstLoginLogsTracker)
                {
                    try
                    {

                        str += "<tr class=\"gradeX\"><td><a href=\"UserLoginDetails.aspx?id=" + item._UserId + "\">" + item._UserName + "</a></td><td>" + item._count + "</td></tr>";

                    }
                    catch (Exception ex)
                    {

                        Console.WriteLine(ex.Message);
                    }

                }

            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
            }

            Response.Write(str);
        }
    }
}
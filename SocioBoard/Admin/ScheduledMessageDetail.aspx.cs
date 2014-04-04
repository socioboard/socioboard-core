using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SocioBoard.Domain;
using SocioBoard.Model;

namespace SocioBoard.Admin
{
    public partial class ScheduledMessageDetail : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                string ret = string.Empty;
                if (Request.QueryString != null)
                {
                    ScheduledMessageRepository objScheduledMessageRepository = new ScheduledMessageRepository();
                    List<ScheduledMessage> lstScheduledMessage = objScheduledMessageRepository.getAllMessagesOfUser(Guid.Parse(Request.QueryString["id"]));

                    foreach (ScheduledMessage item in lstScheduledMessage)
                    {
                        string tf = string.Empty;
                        if (item.Status == true)
                        {
                            tf = "TRUE";
                        }
                        else
                        {
                            tf = "FALSE";
                        }
                        ret += "<tr class=\"gradeX\"><td>" + item.ShareMessage + "</td><td>" + item.ScheduleTime + "</td><td>" + tf + "</td></tr>";
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
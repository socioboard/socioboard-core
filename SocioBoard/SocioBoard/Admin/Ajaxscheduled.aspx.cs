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
    public partial class Ajaxscheduled : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string ret = string.Empty;
            try
            {
                User objUser = new User();
                UserRepository objUserRepository = new UserRepository();
                scheduling objscheduling = new scheduling();
                ScheduledMessage objScheduledMessage = new ScheduledMessage();
                ScheduledMessageRepository objScheduledMessageRepository = new ScheduledMessageRepository();
                List<ScheduledTracker> lstScheduledTracker = objScheduledMessageRepository.GetAllScheduledDetails();
                foreach (ScheduledTracker item in lstScheduledTracker)
                {
                    try
                    {
                        //List<ScheduledMessage> lstScheduledMessage = objScheduledMessageRepository.getAllMessagesOfUser(Guid.Parse(item._Id));
                        List<ScheduledMessage> lstUnsentScheduledMessage = objScheduledMessageRepository.getAllIUnSentMessagesOfUser(Guid.Parse(item._Id));
                        objUser = objUserRepository.getUsersById(Guid.Parse(item._Id));
                        ret += "<tr class=\"gradeX\"><td><a href=\"ScheduledMessageDetail.aspx?id=" + objUser.Id + "\">" + objUser.UserName + "</a></td><td>" + item._count + "</td><td>" + (item._count - lstUnsentScheduledMessage.Count()) + "</td><td>" + lstUnsentScheduledMessage.Count() + "</td></tr>";

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

            Response.Write(ret);
        }
    }
}
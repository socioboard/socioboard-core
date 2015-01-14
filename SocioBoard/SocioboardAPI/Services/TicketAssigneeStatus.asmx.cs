using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Script.Services;
using System.Web.Services;
using Api.Socioboard.Model;

namespace Api.Socioboard.Services
{
    /// <summary>
    /// Summary description for TicketAssigneeStatus
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class TicketAssigneeStatus : System.Web.Services.WebService
    {
        Domain.Socioboard.Domain.TicketAssigneeStatus objTicketAssigneeStatus = new Domain.Socioboard.Domain.TicketAssigneeStatus();
        TicketAssigneeStatusRepository objTicketAssigneeStatusRepository = new TicketAssigneeStatusRepository(); 
 
        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string AddTicketAssigneeStatus(Guid AssignedUserId)
        {
            try
            {
                if (!objTicketAssigneeStatusRepository.IsAssigneeUserIdExist(AssignedUserId))
                {
                    Domain.Socioboard.Domain.TicketAssigneeStatus objTicketAssigneeStatus = new Domain.Socioboard.Domain.TicketAssigneeStatus();
                    objTicketAssigneeStatus.Id = Guid.NewGuid();
                    objTicketAssigneeStatus.AssigneeUserId = AssignedUserId;
                    objTicketAssigneeStatus.AssignedTicketCount = 0;
                    objTicketAssigneeStatusRepository.Add(objTicketAssigneeStatus);
                    return "Success";
                }
                else
                {
                    return "failure";
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return "failure";
            }
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string getAllAssignedMembers()
        {
            try
            {
                List<Domain.Socioboard.Domain.TicketAssigneeStatus> lstAllmembersofAssigned = new List<Domain.Socioboard.Domain.TicketAssigneeStatus>();
                lstAllmembersofAssigned = objTicketAssigneeStatusRepository.getAllAssignedMembers();
                return new JavaScriptSerializer().Serialize(lstAllmembersofAssigned);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return "Somnthing Went Wrong";
            }

        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string updateAssigneeCount(string AssigneeUserId, int Count)
        {
            int ret = 0;
            try
            {
                ret = objTicketAssigneeStatusRepository.updateAssigneeCount(Guid.Parse(AssigneeUserId), Count);
                if (ret == 1)
                {
                    return "Updated";
                }
                else
                {
                    return "Failure";
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return "Something Went Wrong";
            }
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string getAssignedMembers(string AssigneeUserId)
        {
            try
            {
                Domain.Socioboard.Domain.TicketAssigneeStatus AssigneeMember = new Domain.Socioboard.Domain.TicketAssigneeStatus();
                AssigneeMember = objTicketAssigneeStatusRepository.getAssignedMembersByUserId(Guid.Parse(AssigneeUserId));
                return new JavaScriptSerializer().Serialize(AssigneeMember);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return "Somnthing Went Wrong";
            }

        }
    }
}

using Api.Socioboard.Helper;
using Api.Socioboard.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Script.Services;
using System.Web.Services;

namespace Api.Socioboard.Services
{

    /// <summary>
    /// Summary description for Group
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [ScriptService]
    public class GroupScheduleMessage : System.Web.Services.WebService
    {

        Domain.Socioboard.Domain.GroupScheduleMessage _GroupScheduleMessage = new Domain.Socioboard.Domain.GroupScheduleMessage();
        GroupScheduleMessageRepository _GroupScheduleMessageRepository = new GroupScheduleMessageRepository();

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string AddGroupScheduleMessage(string schedulemessageid, string groupid)
        {
            try
            {
                _GroupScheduleMessage.Id = Guid.NewGuid();
                _GroupScheduleMessage.ScheduleMessageId = Guid.Parse(schedulemessageid);
                _GroupScheduleMessage.GroupId = groupid;
                _GroupScheduleMessageRepository.AddGroupScheduleMessage(_GroupScheduleMessage);
                return new JavaScriptSerializer().Serialize(_GroupScheduleMessage);
             
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return "Something Went Wrong";
            }
        }

    }
}

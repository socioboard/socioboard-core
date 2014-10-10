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
    public class TwitterDirectMessages : System.Web.Services.WebService
    {
        TwitterDirectMessageRepository objTwitterDirectMessageRepository = new TwitterDirectMessageRepository();


        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }

        //getAllDirectMessagesById
        [WebMethod]
        public string getAllDirectMessagesById(string Profileid)
        {
            List<Domain.Socioboard.Domain.TwitterDirectMessages> lsttwtmsg = objTwitterDirectMessageRepository.getAllDirectMessagesById(Profileid);
            return new JavaScriptSerializer().Serialize(lsttwtmsg);
        }

    }
}

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
   
    public class UserActivation : System.Web.Services.WebService
    {

        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }

        public static void AddUserActivation(Domain.Socioboard.Domain.User user)
        {
            Domain.Socioboard.Domain.UserActivation objUserActivation = new Domain.Socioboard.Domain.UserActivation();
            objUserActivation.Id = Guid.NewGuid();
            objUserActivation.UserId = user.Id;
            objUserActivation.ActivationStatus = "0";
            UserActivationRepository.Add(objUserActivation);
        }
    }
}

using Api.Socioboard.Helper;
using Api.Socioboard.Model;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
    public class LinkedinMessage : System.Web.Services.WebService
    {
        LinkedInMessageRepository objLinkedInMessageRepository = new LinkedInMessageRepository();
        MongoRepository linkedInMessageRepo = new MongoRepository("LinkedInMessage");

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string GetLinkedInMessages(string UserId, string LinkedInId, int count)
        {
            List<Domain.Socioboard.MongoDomain.LinkedInMessage> lstlinkedinmessages;
            try
            {
                var builder = Builders<Domain.Socioboard.MongoDomain.LinkedInMessage>.Sort;
                var sort = builder.Descending(t => t.CreatedDate);
                //List<Domain.Socioboard.Domain.LinkedInMessage> lstlinkedinmessages = objLinkedInMessageRepository.getLinkedInMessageDetail(LinkedInId, count.ToString(), Guid.Parse(UserId));
                var ret = linkedInMessageRepo.FindWithRange<Domain.Socioboard.MongoDomain.LinkedInMessage>(t => t.ProfileId.Equals(LinkedInId), sort, count, 10);
                var task = Task.Run(async () => {
                    return await ret;
                });
                IList<Domain.Socioboard.MongoDomain.LinkedInMessage> _lstlinkedinmessages = task.Result;
                lstlinkedinmessages = _lstlinkedinmessages.ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                lstlinkedinmessages = new List<Domain.Socioboard.MongoDomain.LinkedInMessage>();
            }
            return new JavaScriptSerializer().Serialize(lstlinkedinmessages);
        }
    }
}

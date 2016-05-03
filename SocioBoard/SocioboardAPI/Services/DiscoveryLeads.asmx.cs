using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using log4net;
using System.Web.Script.Serialization;
using System.Web.Script.Services;

namespace Api.Socioboard.Services
{
    /// <summary>
    /// Summary description for DiscoveryLeads
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class DiscoveryLeads : System.Web.Services.WebService
    {
        ILog logger = LogManager.GetLogger(typeof(DiscoveryLeads));

        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }

        [WebMethod]
        public string AddLeadKeyword(string Keyword,string UserId)
        {
            try
            {
                Domain.Socioboard.Domain.DiscoveryLeads _DiscoveryLeads = new Domain.Socioboard.Domain.DiscoveryLeads();
                discoveryleadsRepository _DiscoveryLeadsRepository = new discoveryleadsRepository();
                _DiscoveryLeads.Id = new Guid();
                _DiscoveryLeads.Keyword = Keyword;
                _DiscoveryLeads.UserId = Guid.Parse(UserId);
                _DiscoveryLeadsRepository.LeadKeyword(_DiscoveryLeads);
                List<string> lstlead = _DiscoveryLeadsRepository.GetLeadHistory(Guid.Parse(UserId));
                return new JavaScriptSerializer().Serialize(lstlead);
               
            }
            catch (Exception ex)
            {
                logger.Error(ex.StackTrace);
                logger.Error(ex.Message);
                 return new JavaScriptSerializer().Serialize("Error");
            }
        
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string GetAllSearchLead(string UserId)
        {
            discoveryleadsRepository _DiscoveryLeadsRepository = new discoveryleadsRepository();
            try
            {
                List<string> lstlead = _DiscoveryLeadsRepository.GetLeadHistory(Guid.Parse(UserId));
                return new JavaScriptSerializer().Serialize(lstlead);
            }
            catch (Exception ex)
            {
                logger.Error(ex.StackTrace);
                logger.Error(ex.Message);
                return "data not found";
            }
        }


    }
}

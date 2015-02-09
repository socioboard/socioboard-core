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
    /// Summary description for WordpressAccount
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class WordpressAccount : System.Web.Services.WebService
    {
        TeamRepository objTeamRepository = new TeamRepository();
        WordpressAccountRepository WpAccountRepo = new WordpressAccountRepository();
        TeamMemberProfileRepository objTeamMemberProfileRepository = new TeamMemberProfileRepository();
        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }
        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string GetWordpressAccountById(string userid, string wpid)
        {
            Domain.Socioboard.Domain.WordpressAccount wpacc = WpAccountRepo.GetWordpressAccountById(Guid.Parse(userid),wpid);
            return new JavaScriptSerializer().Serialize(wpacc);
        }
        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string GetAllWordpressAccount(string UserId)
        {
            List<Domain.Socioboard.Domain.WordpressAccount> lstWordpressAccount = WpAccountRepo.GetAllWordpressAccount(Guid.Parse(UserId));
            return new JavaScriptSerializer().Serialize(lstWordpressAccount);
        }
        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string GetAllWordpressAccountsByUserIdAndGroupId(string userid, string groupid)
        {
            try
            {
                List<Domain.Socioboard.Domain.WordpressAccount> lstWordpressAccount = new List<Domain.Socioboard.Domain.WordpressAccount>();
                Domain.Socioboard.Domain.Team objTeam = objTeamRepository.GetTeamByGroupId(Guid.Parse(groupid));
                List<Domain.Socioboard.Domain.TeamMemberProfile> lstTeamMemberProfile = objTeamMemberProfileRepository.GetTeamMemberProfileByTeamIdAndProfileType(objTeam.Id, "wordpress");
                foreach (var item in lstTeamMemberProfile)
                {
                    try
                    {
                        lstWordpressAccount.Add(WpAccountRepo.GetWordpressAccountById(Guid.Parse(userid),item.ProfileId));
                    }
                    catch (Exception)
                    {

                    }
                }
                return new JavaScriptSerializer().Serialize(lstWordpressAccount);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return "Something Went Wrong";
            }
        }


    }
}

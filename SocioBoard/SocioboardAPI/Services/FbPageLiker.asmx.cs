using Api.Socioboard.Helper;
using Api.Socioboard.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Script.Services;
using System.Web.Services;
using Domain.Socioboard.Domain;

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
    public class FbPageLiker : System.Web.Services.WebService
    {

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string GetLikeByPostId(string postId,string Userid)
        {
            try
            {
                Guid userid = Guid.Parse(Userid);
                FbPageLikerRepository objFbPageLikerRepository = new FbPageLikerRepository();
                List<Domain.Socioboard.Domain.FbPageLiker> LstDraft = objFbPageLikerRepository.GetLikeByPostId(postId, userid);
                return new JavaScriptSerializer().Serialize(LstDraft);
            }
            catch (Exception)
            {
                return new JavaScriptSerializer().Serialize("No Data available.");
            }
        }

    }
}

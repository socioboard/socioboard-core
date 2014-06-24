using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Script.Serialization;
using System.Web.Script.Services;

namespace SocioBaordAPI.API
{
    /// <summary>
    /// Summary description for DraftServices
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class DraftServices : System.Web.Services.WebService
    {
        /// <summary>
        /// Get Draft Data by user id. 
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        //[WebMethod]
        //public string GetDraftsByUserId(String Id)
        //{
        //    return "";
        //}


        /// <summary>
        /// Get draft by user id.
        /// </summary>
        /// <param name="UserId">UserId</param>
        /// <returns></returns>
        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string GetDraftsByUserId(string UserId)
        {
            try
            {
                SocioBoard.Model.DraftsRepository DraftsRepository = new SocioBoard.Model.DraftsRepository();
                SocioBoard.Domain.Drafts Draft = new SocioBoard.Domain.Drafts();

                Guid userid = Guid.Parse(UserId);
                List<SocioBoard.Domain.Drafts> LstDraft = DraftsRepository.getAllDrafts(userid);

                return new JavaScriptSerializer().Serialize(LstDraft);
            }
            catch (Exception)
            {
                return new JavaScriptSerializer().Serialize("No Data available.");
            }
        }


        /// <summary>
        /// Add New draft 
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="UserId"></param>
        /// <param name="CreatedDate"></param>
        /// <param name="ModifiedDate"></param>
        /// <param name="Message"></param>
        /// <returns></returns>
        [WebMethod]
        public string AddDraft(String UserId, DateTime CreatedDate, DateTime ModifiedDate, string Message)
        {
            try
            {
                SocioBoard.Model.DraftsRepository DraftsRepository = new SocioBoard.Model.DraftsRepository();
                SocioBoard.Domain.Drafts Draft = new SocioBoard.Domain.Drafts();

                Guid id = new Guid();
                Guid userid = Guid.Parse(UserId);

                Draft.Id = id;
                Draft.UserId = userid;
                Draft.CreatedDate = CreatedDate;
                Draft.ModifiedDate = ModifiedDate;
                Draft.Message = Message;

                DraftsRepository.AddDrafts(Draft);

                return "Success";
            }
            catch (Exception)
            {
                return "Failed";
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="message"></param>
        [WebMethod]
        public string UpdateDrafts(String Id, string message)
        {
            try
            {
                SocioBoard.Model.DraftsRepository DraftsRepository = new SocioBoard.Model.DraftsRepository();
                SocioBoard.Domain.Drafts Draft = new SocioBoard.Domain.Drafts();

                Guid DraftId = Guid.Parse(Id);
                Draft.Id = DraftId;
                Draft.Message = message;

                DraftsRepository.UpdateDrafts(Draft);

                return "Success";
            }
            catch (Exception)
            {
                return "Failed";
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="message"></param>
        [WebMethod]
        public string DeleteDrafts(String Id)
        {
            try
            {
                SocioBoard.Model.DraftsRepository DraftsRepository = new SocioBoard.Model.DraftsRepository();
                SocioBoard.Domain.Drafts Draft = new SocioBoard.Domain.Drafts();

                Guid DraftId = Guid.Parse(Id);

                int isUpdated = DraftsRepository.DeleteDrafts(DraftId);
                if (isUpdated > 0)
                {
                    return "Success";
                }
                else
                {
                    return "Failed";
                }
            }
            catch (Exception)
            {
              return "Failed";
            }
        }


    }
}

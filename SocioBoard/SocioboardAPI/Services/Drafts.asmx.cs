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
    public class Drafts : System.Web.Services.WebService
    {
        DraftsRepository objDraftsRepository = new DraftsRepository();
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
               
                Guid userid = Guid.Parse(UserId);
                List<Domain.Socioboard.Domain.Drafts> LstDraft = objDraftsRepository.getAllDrafts(userid);

                return new JavaScriptSerializer().Serialize(LstDraft);
            }
            catch (Exception)
            {
                return new JavaScriptSerializer().Serialize("No Data available.");
            }
        }


        /// <Add New draft >
        /// Add New draft 
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="UserId"></param>
        /// <param name="CreatedDate"></param>
        /// <param name="ModifiedDate"></param>
        /// <param name="Message"></param>
        /// <returns></returns>
        [WebMethod]
        public string AddDraft(String UserId, string groupid, DateTime ModifiedDate, string Message)
        {
            try
            {

                Domain.Socioboard.Domain.Drafts objDrafts = new Domain.Socioboard.Domain.Drafts();

                Guid id = new Guid();
                Guid userid = Guid.Parse(UserId);
                objDrafts.Id = id;
                objDrafts.UserId = userid;
                objDrafts.CreatedDate = DateTime.Now;
                objDrafts.ModifiedDate = ModifiedDate;
                objDrafts.Message = Message;
                objDrafts.GroupId = Guid.Parse(groupid);
                objDraftsRepository.AddDrafts(objDrafts);
                return "Success";
            }
            catch (Exception)
            {
                return "Failed";
            }
        }


        /// <UpdateDrafts>
        /// Update Drafts
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="message"></param>
        [WebMethod]
        public string UpdateDrafts(String Id, string message)
        {
            try
            {

                Domain.Socioboard.Domain.Drafts objDrafts = new Domain.Socioboard.Domain.Drafts();

                Guid DraftId = Guid.Parse(Id);

                objDraftsRepository.UpdateDrafts(DraftId, message);

                return "Success";
            }
            catch (Exception)
            {
                return "Failed";
            }
        }


        /// <DeleteDrafts>
        /// Delete Drafts
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="message"></param>
        [WebMethod]
        public string DeleteDrafts(String Id)
        {
            try
            {
                
                Domain.Socioboard.Domain.Drafts objDrafts = new Domain.Socioboard.Domain.Drafts();

                Guid DraftId = Guid.Parse(Id);

                int isUpdated = objDraftsRepository.DeleteDrafts(objDrafts);
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

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string GetDraftMessageByUserIdAndGroupId(string UserId, string GroupId)
        {
            string profileid = string.Empty;
            try
            {
                DraftsRepository objDraftsRepository = new DraftsRepository();
                List<Domain.Socioboard.Domain.Drafts> lstDrafts = objDraftsRepository.GetDraftMessageByUserIdAndGroupId(Guid.Parse(GroupId), Guid.Parse(UserId));
                return new JavaScriptSerializer().Serialize(lstDrafts);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return new JavaScriptSerializer().Serialize("Please try Again");
            }

        }

        /// <UpdateDrafts>
        /// Update Drafts
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="message"></param>
        [WebMethod]
        public string UpdateDraftsMessage(String draftId, string userid, string groupid, string message)
        {
            try
            {
                Domain.Socioboard.Domain.Drafts objDrafts = new Domain.Socioboard.Domain.Drafts();
                objDraftsRepository.UpdateDraftsMessage(Guid.Parse(draftId), Guid.Parse(userid), Guid.Parse(groupid), message);
                return "Success";
            }
            catch (Exception)
            {
                return "Failed";
            }
        }

    }
}

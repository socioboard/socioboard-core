using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using SocioBoard.Model;
using Domain.Socioboard.Domain;
using System.Web.Script.Serialization;
using GlobussDropboxLib.Authentication;
using System.Web.Services;
using System.Web.Script.Services;

namespace Api.Socioboard.Services
{
    /// <summary>
    /// Summary description for DropboxAccount
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class DropboxAccount : System.Web.Services.WebService
    {

        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }



        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string GetDropboxAccountDetailsByUserId(string UserId)
        {
            if (string.IsNullOrEmpty(UserId))
            {
                return "User Id is Null";
            }

            Domain.Socioboard.Domain.DropboxAccount _DropboxAccount = new Domain.Socioboard.Domain.DropboxAccount();
            DropboxAccountRepository _DropboxAccountRepository = new DropboxAccountRepository();

            try
            {
                // Convert User if in Guid 
                Guid _Userid = Guid.Parse(UserId);

                //IF USER NOT EXIST
                //CHECK AND GET USER DATA IN TABLE 

                _DropboxAccount = _DropboxAccountRepository.getDropboxAccountDetailsbyId(_Userid);

                if (_DropboxAccount != null)
                {
                    return new JavaScriptSerializer().Serialize(_DropboxAccount);
                }
                else
                {
                    return new JavaScriptSerializer().Serialize(null);
                }
            }
            catch (Exception)
            {
                return new JavaScriptSerializer().Serialize(null);
            }
        }


     


        //[WebMethod]
        //[ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public bool AddUserInfo(String _UserDetail, string UserId, string _AccessToken, GlobussDropboxLib.Authentication.oAuthToken _oAuthToken)
        {
            var _OBJ_User = Newtonsoft.Json.Linq.JObject.Parse(_UserDetail);
            DropboxAccountRepository _DropboxAccountRepository = new DropboxAccountRepository();

            Domain.Socioboard.Domain.DropboxAccount _DropboxAccount = new Domain.Socioboard.Domain.DropboxAccount();
            //User user = (User)Session["LoggedUser"];
            Guid _UserId = Guid.Parse(UserId);

            _DropboxAccount.Id = Guid.NewGuid();
            _DropboxAccount.UserId = _UserId;
            _DropboxAccount.DropboxUserName = _OBJ_User["display_name"].ToString();
            _DropboxAccount.DropboxUserId = _OBJ_User["uid"].ToString();
            _DropboxAccount.DropboxEmail = _OBJ_User["email"].ToString();
            _DropboxAccount.AccessToken = _AccessToken;
            _DropboxAccount.OauthToken = _oAuthToken.Token;
            _DropboxAccount.OauthTokenSecret = _oAuthToken.TokenSecret;
            _DropboxAccount.CreateDate = DateTime.Now;
            _DropboxAccountRepository.Add(_DropboxAccount);
            return true;
        }





    }
}

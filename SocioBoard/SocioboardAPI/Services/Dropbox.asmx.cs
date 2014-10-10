using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using Hammock;
using Hammock.Authentication.OAuth;
using GlobussDropboxLib;
using SocioBoard.Model;
using Domain.Socioboard.Domain;
using System.Web.Script.Serialization;
using GlobussDropboxLib.Authentication;
using System.Configuration;

namespace Api.Socioboard.Services
{
    /// <summary>
    /// Summary description for Dropbox
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    [ScriptService]
    public class Dropbox : System.Web.Services.WebService
    {

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string GetDropboxRedirectUrl(string consumerKey, string redirectUrl)
        {
            string authLink = string.Empty;
            authLink = GlobussDropboxLib.App.Core.Global._APP_AUTHORIZE_URL + "client_id=" + consumerKey + "&response_type=code&redirect_uri=" + redirectUrl;
            return authLink;
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string GetUserDropBoxData(string UserId)
        {

            List<string> _Images = new List<string>();
            Api.Socioboard.Services.DropboxAccount _DropboxAccount = new DropboxAccount();

            Domain.Socioboard.Domain.DropboxAccount _DropboxUserAccount = new Domain.Socioboard.Domain.DropboxAccount();

            _DropboxUserAccount = Newtonsoft.Json.JsonConvert.DeserializeObject<Domain.Socioboard.Domain.DropboxAccount>(_DropboxAccount.GetDropboxAccountDetailsByUserId(UserId));

            //string _Images = string.Empty;
            oAuthToken _oAuthToken = new oAuthToken();
            _oAuthToken.ConsumerKey = ConfigurationManager.AppSettings["DBX_Appkey"];
            _oAuthToken.ConsumerSecret = ConfigurationManager.AppSettings["DBX_Appsecret"];
            _oAuthToken.Token = _DropboxUserAccount.OauthToken;
            _oAuthToken.TokenSecret = _DropboxUserAccount.OauthTokenSecret;
            //GET USER DASHBOARD
            string _User_DBX_Home = GlobussDropboxLib.Dropbox.Core.Metadata.Metadata.GetDropBoxFolder(ref _oAuthToken, _DropboxUserAccount.AccessToken);

            //CONVERT IN JSON OBJECT
            var _OBJ_HOME = Newtonsoft.Json.Linq.JObject.Parse(_User_DBX_Home);

            //GET FOLDER AND FILES FROM USER HOME 
            foreach (var _OBJ_HOME_item in _OBJ_HOME["contents"])
            {//GET DROPBOX DASHBOARD DATA.

                string is_dir = string.Empty;

                if (_OBJ_HOME_item["is_dir"].ToString() == "true")
                    is_dir = _OBJ_HOME_item["is_dir"].ToString();
                else
                    is_dir = "false";

                string rev = _OBJ_HOME_item["rev"].ToString();
                string path = _OBJ_HOME_item["path"].ToString();
                string icon = _OBJ_HOME_item["icon"].ToString();


                //GET FILES FROM FOLDER 
                if (!string.IsNullOrEmpty(path) && !path.Contains(".pdf") && !path.Contains(".jpg"))//GET FOLDER FROM DROPBOX DASHBOARD.
                {//WHEN FOLDER
                    string _User_DBX_Folder_File = GlobussDropboxLib.Dropbox.Core.Metadata.Metadata.GetDropBoxFiles(ref _oAuthToken, _DropboxUserAccount.AccessToken, "dropbox", path.Replace("/", string.Empty));
                    //CONVERT IN JSON OBJECT
                    var _OBJ_HOME_FOLDER = Newtonsoft.Json.Linq.JObject.Parse(_User_DBX_Folder_File);

                    //GET FOLDER AND FILES FROM USER HOME 
                    foreach (var _OBJ_HOME_FOLDER_item in _OBJ_HOME_FOLDER["contents"])
                    {//GET IMAGE FROM FOLDER.
                        string Filepath = _OBJ_HOME_FOLDER_item["path"].ToString();
                        string _User_DBX_File_Media = GlobussDropboxLib.Dropbox.Core.Media.Media.GetDropBoxDirectlink(ref _oAuthToken, _DropboxUserAccount.AccessToken, Filepath);
                        string _LinkUrl = Newtonsoft.Json.Linq.JObject.Parse(_User_DBX_File_Media)["url"].ToString();

                        if (_LinkUrl.Contains(".jpg") || _LinkUrl.Contains(".png"))
                        {
                            //_Images += "<div class=\"span2\"><div class=\"checkbox check\">"
                            //        + "<input type=\"checkbox\"></div><img id=\"Img1\" src=\"" + _LinkUrl + "\" alt=\"\" style=\"height: 50px;\"></div>";

                            _Images.Add(_LinkUrl);
                        }

                    }//END FOREACH
                }//END IF
                else if (!string.IsNullOrEmpty(path) && !path.Contains(".pdf") && path.Contains(".jpg")) //GET PIC FILE WHEN ITS ADDED ON HOME NOT IN FOLDER.
                { //WHEN PHOTO ON DROPBOX DASHBOARD.
                    string _User_DBX_File_Media = GlobussDropboxLib.Dropbox.Core.Media.Media.GetDropBoxDirectlink(ref _oAuthToken, _DropboxUserAccount.AccessToken, path);
                    string _LinkUrl = Newtonsoft.Json.Linq.JObject.Parse(_User_DBX_File_Media)["url"].ToString();
                    //_Images += "<div class=\"span2\"><div class=\"checkbox check\">"
                    //        + "<input type=\"checkbox\">  </div><img id=\"Img1\" src=\"" + _LinkUrl + "\" alt=\"\" style=\"height: 50px;\"></div>";

                    _Images.Add(_LinkUrl);


                }//END ELSE IF
                else
                {

                }//END ELSE
            }//END FOREACH

            //Response.Write(_Images);
            //return _User_DBX_Home;
            return Newtonsoft.Json.JsonConvert.SerializeObject(_Images);
        }


        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string AddDropboxAccount(string code, string UserId)
        {
            GlobussDropboxLib.Authentication.oAuthToken _oAuthToken = new GlobussDropboxLib.Authentication.oAuthToken();

            _oAuthToken.ConsumerKey = ConfigurationManager.AppSettings["DBX_Appkey"];
            _oAuthToken.ConsumerSecret = ConfigurationManager.AppSettings["DBX_Appsecret"];

            Guid Userid = Guid.Parse(UserId);
            //GET RESPONCE CODE BY URL
            string _code = code;

            //POST DATA 
            string _PostData = ("code=" + _code + "&client_id=" + ConfigurationManager.AppSettings["DBX_Appkey"] + "&client_secret=" + ConfigurationManager.AppSettings["DBX_Appsecret"] + "&redirect_uri=" + HttpUtility.UrlEncode(ConfigurationManager.AppSettings["DBX_redirect_uri"]) + "&grant_type=authorization_code").Trim();

            var _StrAccess_Token = _oAuthToken.WebRequest(GlobussDropboxLib.Authentication.oAuthToken.Method.POST, GlobussDropboxLib.App.Core.Global._APP_TOEKN_URL, _PostData);

            string _OauthRequestToken = _oAuthToken.GetRequestToken();

            var _Jval_AccessToken = Newtonsoft.Json.Linq.JValue.Parse(_StrAccess_Token);

            var _AccessToken = ((Newtonsoft.Json.Linq.JValue)_Jval_AccessToken["access_token"]).Value.ToString();
            //_oAuthToken.AccessTokenGet(AccessToken);

            //GET USER INFO
            string _UserDetail = GlobussDropboxLib.Dropbox.Core.User.User.GetUserInfo(ref _oAuthToken, _AccessToken);

            //AddUserInfo(_UserDetail, _AccessToken, _oAuthToken);

            var _OBJ_User = Newtonsoft.Json.Linq.JObject.Parse(_UserDetail);
            DropboxAccountRepository _DropboxAccountRepository = new DropboxAccountRepository();

            Domain.Socioboard.Domain.DropboxAccount _DropboxAccount = new Domain.Socioboard.Domain.DropboxAccount();
            _DropboxAccount.Id = Guid.NewGuid();
            _DropboxAccount.UserId = Userid;
            _DropboxAccount.DropboxUserName = _OBJ_User["display_name"].ToString();
            _DropboxAccount.DropboxUserId = _OBJ_User["uid"].ToString();
            _DropboxAccount.DropboxEmail = _OBJ_User["email"].ToString();
            _DropboxAccount.AccessToken = _AccessToken;
            _DropboxAccount.OauthToken = _oAuthToken.Token;
            _DropboxAccount.OauthTokenSecret = _oAuthToken.TokenSecret;
            _DropboxAccount.CreateDate = DateTime.Now;
            _DropboxAccountRepository.Add(_DropboxAccount);


            return "";
        }



    }
}

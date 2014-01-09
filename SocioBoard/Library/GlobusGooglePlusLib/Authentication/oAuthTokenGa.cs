using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GlobusGooglePlusLib.App.Core;
using System.Configuration;
using System.Data;

namespace GlobusGooglePlusLib.Authentication
{
    public class oAuthTokenGa
    {
        oAuthToken objoAuth = new oAuthToken();

        /// <summary>
        ///  To get authentication Link
        /// </summary>
        /// <param name="scope">https://www.googleapis.com/auth/userinfo.email+https://www.googleapis.com/auth/userinfo.profile+https://www.googleapis.com/auth/analytics.readonly</param>
        /// <returns></returns>
        public string GetAutherizationLinkGa(string scope)
        {
            string strAuthUrl = Globals.strAuthentication;
            strAuthUrl += "?scope=" + scope + "&redirect_uri=" + ConfigurationManager.AppSettings["GaRedirectUrl"] + "&response_type=code&client_id=" + ConfigurationManager.AppSettings["GaClientId"] + "&approval_prompt=force&access_type=offline";
            return strAuthUrl;
        }

        /// <summary>
        /// After the web server receives the authorization code, it may exchange the authorization code for an access token and a refresh token.
        /// </summary>
        /// <param name="code">authorization code</param>
        /// <returns></returns>
        public string GetRefreshToken(string code)
        {
            string postData = "code=" + code + "&client_id=" + ConfigurationManager.AppSettings["GaClientId"] + "&client_secret=" + ConfigurationManager.AppSettings["GaClientSecretKey"] + "&redirect_uri=" + ConfigurationManager.AppSettings["GaRedirectUrl"] + "&grant_type=authorization_code";
            string result = objoAuth.WebRequest(GlobusGooglePlusLib.Authentication.oAuthToken.Method.POST, Globals.strRefreshToken, postData);
            return result;
        }

        /// <summary>
        ///  obtain a new access token by sending a refresh token to the Google OAuth 2.0 Authorization server.
        /// </summary>
        /// <param name="refreshToken">refreshToken</param>
        /// <returns></returns>
        public string GetAccessToken(string refreshToken)
        {
            string postData = "refresh_token=" + refreshToken + "&client_id=" + ConfigurationManager.AppSettings["GaClientId"] + "&client_secret=" + ConfigurationManager.AppSettings["GaClientSecretKey"] + "&grant_type=refresh_token";
            string[] header = { "token_type", "expires_in" };
            string[] val = { "Bearer", "3600" };
            Uri path = new Uri(Globals.strRefreshToken);
            //  string response = postWebRequest(path, postData, header, val);
            string response = objoAuth.WebRequest(GlobusGooglePlusLib.Authentication.oAuthToken.Method.POST, Globals.strRefreshToken, postData);
            return response;
        }

      


    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GlobusInstagramLib.App.Core;
using System.Collections.Specialized;
using GlobusInstagramLib.Authentication;


namespace GlobusInstagramLib.Instagram.Core.CommentsMethods
{
    class Comments
    {
        oAuthInstagram oAuthIns = new oAuthInstagram();
        /// <summary>
        /// Get a full list of comments on a media.
        /// </summary>
        /// <param name="mediaid"></param>
        /// <param name="accessToken"></param>
        /// <returns></returns>
        public InstagramResponse<Comment[]> GetComments(string mediaid, string accessToken)
        {
            string url = oAuthIns.Configuration.ApiBaseUrl + "media/" + mediaid + "/comments?access_token=" + accessToken;
            if (string.IsNullOrEmpty(accessToken))
                url = oAuthIns.Configuration.ApiBaseUrl + "media/" + mediaid + "/comments?client_id=" + oAuthIns.Configuration.ClientId;

            string json = oAuthIns.RequestGetToUrl(url, oAuthIns.Configuration.Proxy);
            if (string.IsNullOrEmpty(json))
                return null;

            InstagramResponse<Comment[]> res = Base.DeserializeObject<InstagramResponse<Comment[]>>(json);
            return res;
        }

        /// <summary>
        /// Create a comment on a media. Please email apidevelopers[at]instagram.com for access. 
        /// </summary>
        /// <param name="mediaids"></param>
        /// <param name="text"></param>
        /// <param name="accessToken"></param>
        public void CommentAdd(string[] mediaids, string text, string accessToken)
        {
            foreach (var mediaid in mediaids)
            {
                CommentAdd(mediaid, text, accessToken);
            }
        }

        /// <summary>
        /// Create a comment on a media. Please email apidevelopers[at]instagram.com for access. 
        /// </summary>
        /// <param name="mediaid"></param>
        /// <param name="text"></param>
        /// <param name="accessToken"></param>
        /// <returns></returns>
        public bool CommentAdd(string mediaid, string text, string accessToken)
        {
            string url = oAuthIns.Configuration.ApiBaseUrl + "media/" + mediaid + "/comments?access_token=" + accessToken;
            NameValueCollection post = new NameValueCollection
                                       {
                                               {"text",text},
                                               {"access_token", accessToken}
                                       };
            string json = oAuthIns.RequestPostToUrl(url, post, oAuthIns.Configuration.Proxy);
            if (string.IsNullOrEmpty(json))
                return true;

            InstagramResponse<Comment> res = Base.DeserializeObject<InstagramResponse<Comment>>(json);
            return res.meta.code == "200";
        }

        /// <summary>
        /// Remove a comment either on the authenticated user's media or authored by the authenticated user. 
        /// </summary>
        /// <param name="mediaid"></param>
        /// <param name="commentid"></param>
        /// <param name="accessToken"></param>
        /// <returns></returns>
        public bool CommentDelete(string mediaid, string commentid, string accessToken)
        {
            string url = oAuthIns.Configuration.ApiBaseUrl + "media/" + mediaid + "/comments/" + commentid + "?access_token=" + accessToken;
            string json = oAuthIns.RequestDeleteToUrl(url, oAuthIns.Configuration.Proxy);
            if (string.IsNullOrEmpty(json))
                return false;

            InstagramResponse<Comment> res = Base.DeserializeObject<InstagramResponse<Comment>>(json);
            return res.meta.code == "200";
        } 
    }
}

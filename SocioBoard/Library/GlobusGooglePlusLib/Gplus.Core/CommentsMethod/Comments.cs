using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json.Linq;
using GlobusGooglePlusLib.Authentication;
using GlobusGooglePlusLib.App.Core;

namespace GlobusGooglePlusLib.Gplus.Core.CommentsMethod
{
    public class Comments
    {
        /// <summary>
        /// List all of the comments for an activity
        /// </summary>
        /// <param name="ActivityId"></param>
        /// <param name="access"></param>
        /// <returns></returns>
        public JArray Get_CommentList_By_ActivityId(string ActivityId, string access)
        {
            oAuthToken objToken = new oAuthToken();
            string RequestUrl = Globals.strGetCommentListByActivityId + ActivityId + "/comments?access_token=" + access;
            Uri path = new Uri(RequestUrl);
            string[] header = { "token_type", "expires_in" };
            string[] val = { "Bearer", "3600" };
            string response = string.Empty;
            try
            {
                response = objToken.WebRequestHeader(path, header, val);
                if (!response.StartsWith("["))
                    response = "[" + response + "]";
            }
            catch (Exception Err)
            {
                Console.Write(Err.StackTrace);
            }

            return JArray.Parse(response);
        }

        /// <summary>
        /// Get a comment By Comment Id.
        /// </summary>
        /// <param name="CommentId"></param>
        /// <param name="access"></param>
        /// <returns></returns>
        public JArray Get_Comments_By_CommentId(string CommentId, string access)
        {
            oAuthToken objToken = new oAuthToken();
            string RequestUrl = Globals.strGetCommentByCommentId + CommentId + "?access_token=" + access;
            Uri path = new Uri(RequestUrl);
            string[] header = { "token_type", "expires_in" };
            string[] val = { "Bearer", "3600" };
            string response = string.Empty;
            try
            {
                response = objToken.WebRequestHeader(path, header, val);
                if (!response.StartsWith("["))
                    response = "[" + response + "]";
            }
            catch (Exception Err)
            {
                Console.Write(Err.StackTrace);
            }

            return JArray.Parse(response);
        }
    }
}

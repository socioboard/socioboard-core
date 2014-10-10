using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json.Linq;
using GlobusGooglePlusLib.Gplus.Core.CommentsMethod;

namespace GlobusGooglePlusLib.App.Core
{
    public class CommentsController
    {
        JArray objArr;
        public CommentsController()
        {
            objArr = new JArray();
        }

        /// <summary>
        /// List all of the comments for an activity
        /// </summary>
        /// <param name="ActivityId"></param>
        /// <param name="access"></param>
        /// <returns></returns>
        public JArray GetCommentListByActivityId(string ActivityId, string access)
        {
            Comments objComment = new Comments();
            try
            {
                objArr = objComment.Get_CommentList_By_ActivityId(ActivityId, access);
            }
            catch (Exception Err)
            {
                Console.Write(Err.StackTrace);
            }
            return objArr;
        }

        /// <summary>
        /// Get a comment By Comment Id.
        /// </summary>
        /// <param name="CommentId"></param>
        /// <param name="access"></param>
        /// <returns></returns>
        public JArray GetCommentsByCommentId(string CommentId, string access)
        {
            Comments objComment = new Comments();
            try
            {
                objArr = objComment.Get_Comments_By_CommentId(CommentId, access);
            }
            catch (Exception Err)
            {
                Console.Write(Err.StackTrace);
            }
            return objArr;
        }
    }
}

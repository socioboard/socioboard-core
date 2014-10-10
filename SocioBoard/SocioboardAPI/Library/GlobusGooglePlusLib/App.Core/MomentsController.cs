using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json.Linq;
using GlobusGooglePlusLib.Gplus.Core.MomentsMethod;

namespace GlobusGooglePlusLib.App.Core
{
    public class MomentsController
    {
        JArray objArr;
        public MomentsController()
        {
            objArr = new JArray();
        }

        /// <summary>
        /// Record a moment representing a user's activity such as making a purchase or commenting on a blog
        /// </summary>
        /// <param name="UserId"></param>
        /// <param name="access"></param>
        /// <returns></returns>
        public JArray PostInsertMoment(string UserId, string access)
        {
            Moments objMoment = new Moments();
            try
            {
                objArr = objMoment.Post_Insert_Moment(UserId, access);
            }
            catch (Exception Err)
            {
                Console.Write(Err.StackTrace);
            }
            return objArr;
        }

        /// <summary>
        /// List all of the moments that your app has written for the authenticated user
        /// </summary>
        /// <param name="UserId"></param>
        /// <param name="access"></param>
        /// <returns></returns>
        public JArray GetMomentList(string UserId, string access)
        {
            Moments objMoment = new Moments();
            try
            {
                objArr = objMoment.Get_Moment_List(UserId, access);
            }
            catch (Exception Err)
            {
                Console.Write(Err.StackTrace);
            }
            return objArr;
        }

        /// <summary>
        /// Delete a moment that your app has written for the authenticated user.
        /// </summary>
        /// <param name="UserId"></param>
        /// <param name="access"></param>
        /// <returns></returns>
        public JArray RemoveMoment(string UserId, string access)
        {
            Moments objMoment = new Moments();
            try
            {
                objArr = objMoment.Remove_Moment(UserId, access);
            }
            catch (Exception Err)
            {
                Console.Write(Err.StackTrace);
            }
            return objArr;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using GlobusGooglePlusLib.App.Core;

namespace GlobusGooglePlusLib.GAnalytics.Core.Accounts
{
    public class Accounts
    {
        /// <summary>
        /// Get the Google Analytics Account
        /// </summary>
        /// <param name="accesstoken"></param>
        /// <returns></returns>
        public DataSet getGaAccounts(string accesstoken)
        {
            DataSet dsAccount = new DataSet();
            try
            {
                dsAccount.ReadXml(Globals.strgetGaAccounts + "?access_token=" + accesstoken);
            }
            catch (Exception Err)
            {
                Console.Write(Err.StackTrace);
            }
            return dsAccount;
        }

        /// <summary>
        /// Get Profiles added in Google Analytics Account
        /// </summary>
        /// <param name="accesstoken"></param>
        /// <param name="strAccId"></param>
        /// <returns></returns>
        public DataSet getGaProfiles(string accesstoken, string strAccId)
        {
            DataSet dsAccount = new DataSet();
            try
            {
                dsAccount.ReadXml(Globals.strgetGaAccounts + strAccId + "/webproperties/~all/profiles?access_token=" + accesstoken);
            }
            catch (Exception Err)
            {
                Console.Write(Err.StackTrace);
            }
            return dsAccount;
        }
    }
}

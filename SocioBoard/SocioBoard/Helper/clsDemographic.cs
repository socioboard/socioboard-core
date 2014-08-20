using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json.Linq;
using System.Data;
using Facebook;

namespace SocioBoard.Helper
{
    public class clsDemographic
    {
        List<user_demographic> usergraphic;
       // List<user_home> UserHome;
        //OAuthFacebook objAuthentication = new OAuthFacebook();
        //FacebookWebRequest objWebRequest = new FacebookWebRequest();
        public struct user_demographic
        {
            #region Analytics
            public List<JToken> fansGenderAge { get; set; }
           // public List<string> convalue { get; set; }
            #endregion
        }

        public List<user_demographic> GetRecords(JObject output, string FacebookAccessToken)
        {

            usergraphic = new List<user_demographic>();
            user_demographic facebook_user;
            List<JToken> country = new List<JToken>();



            //Check Error

            string strError = (string)output["Message"];
            //if (strError == "Error")
            //{
            //    facebook_user = new user_home();
            //    facebook_user.ErrorCode = (string)output["ErroCode"];
            //    UserHome.Add(facebook_user);
            //}

            // put data into jarray

            JArray data = (JArray)output["data"];

            if (data != null)
            {
                foreach (JObject item in data)
                {
                    facebook_user = new user_demographic();

                    #region Analytics(If we want the information about apps)

                    JArray Values = (JArray)item["values"];
                    if (Values != null)
                    {
                        foreach (JObject value in Values)
                        {
                            var test = value["value"].Count();
                            if (test > 0)
                            {
                                facebook_user.fansGenderAge = value["value"].ToList();
                                usergraphic.Add(facebook_user);
                            }
                        }

                    }
                    #endregion
                }
            }

            return usergraphic;
        }

        public DataTable getgenderageLikes(string strToken, string strPageId)
        {
            DataTable dtgraphic = new DataTable();
            try
            {
                #region LIkes By Gender & age
                List<clsDemographic.user_demographic> usergraphic;
                //////////////////create URL for likes by country/////////////
                string strAge = "https://graph.facebook.com/" + strPageId + "/insights/page_fans_gender_age";

                ///////////////////////////////////////////////////
                FacebookClient fb = new FacebookClient();
                JObject result = (JObject)fb.Get(strAge);
               // string codedataurlgraphic = objAuthentication.RequestUrl(strAge, strToken);
               // usergraphic = new List<clsDemographic.user_demographic>();

                //JObject outputreg = objWebRequest.FacebookRequest(codedataurlgraphic, "Get");
                //System.Xml.XmlDocument output = objWebRequest.FacebookRequestxml(codedataurl, "Get");
                usergraphic = GetRecords(result, strToken);
              
                dtgraphic.Columns.Add("age");
                dtgraphic.Columns.Add("fvalue");
                dtgraphic.Columns.Add("mvalue");
                List<JToken> lstGenderAge = new List<JToken>();
                foreach (var item in usergraphic)
                {
                    if (item.fansGenderAge != null)
                    {
                        lstGenderAge = item.fansGenderAge;
                        if (lstGenderAge.Count() > 0)
                        {
                            string ageVal = string.Empty;
                            int index = 0;
                            int genderCount = 6;
                            if (lstGenderAge.Count() < 6)
                                genderCount = lstGenderAge.Count();
                            for (int i = 0; i < genderCount; i++)
                            {
                                DataRow dr = dtgraphic.NewRow();
                                ageVal = lstGenderAge[i].ToString();
                                index = ageVal.IndexOf(":");
                                if (ageVal.Substring(1, 1) == "M")
                                    dr["mvalue"] = ageVal.Substring(index + 1);
                                else
                                    dr["fvalue"] = ageVal.Substring(index + 1);
                                dr["age"] = ageVal.Substring(3, index - 4);

                                dtgraphic.Rows.Add(dr);
                            }
                        }
                    }
                }
                #endregion
            }
            catch (Exception Err)
            { }
            return dtgraphic;
        }
    }
}
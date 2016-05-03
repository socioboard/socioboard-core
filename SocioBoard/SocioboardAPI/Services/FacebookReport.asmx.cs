using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using Domain.Socioboard.Domain;
using Api.Socioboard.Model;
using System.Web.Script.Serialization;
using Api.Socioboard.Helper;
using System.Net;
using System.IO;
using System.Text;
using Newtonsoft.Json.Linq;
using System.Web.Script.Services;
using Facebook;
namespace Api.Socioboard.Services
{
    /// <summary>
    /// Summary description for FacebookReport
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class FacebookReport : System.Web.Services.WebService
    {
        FacebookAccountRepository objFacebookAccountRepository = new FacebookAccountRepository();
        FacebookReportRepository objFbReportRepository = new FacebookReportRepository();
        [WebMethod]
        public string GetFacebookdata(string FBProfileId, string AccessToken)
        {
            //List<Domain.Socioboard.Domain.FacebookAccount> lstFacebookAccount = new List<Domain.Socioboard.Domain.FacebookAccount>();
            //lstFacebookAccount = objFacebookAccountRepository.getFbAccounts();

            //foreach (Domain.Socioboard.Domain.FacebookAccount item in lstFacebookAccount)
            //{
            //    if ((item.Type == "page" || item.Type == "Page") && !string.IsNullOrEmpty(item.AccessToken))
            //    {
            string ret = string.Empty;
            string ProfileId = string.Empty;
            ProfileId = FBProfileId;
            string strlikes_90 = string.Empty;
            string strlikes_60 = string.Empty;
            string strlikes_30 = string.Empty;
            string strlikes_15 = string.Empty;
            string strunlikes_90 = string.Empty;
            string strunlikes_60 = string.Empty;
            string strunlikes_30 = string.Empty;
            string strunlikes_15 = string.Empty;
            string strimpression_90 = string.Empty;
            string strimpression_60 = string.Empty;
            string strimpression_30 = string.Empty;
            string strimpression_15 = string.Empty;
            string strimpressionbreakdown = string.Empty;
            string strstorysharing_90 = string.Empty;
            string strstorysharing_60 = string.Empty;
            string strstorysharing_30 = string.Empty;
            string strstorysharing_15 = string.Empty;
            int impressionfans_90 = 0;
            int impressionpagepost_90 = 0;
            int impressionuserpost_90 = 0;
            int impressioncoupn_90 = 0;
            int impressionother_90 = 0;
            int impressionmention_90 = 0;
            int impressioncheckin_90 = 0;
            int impressionquestion_90 = 0;
            int impressionevent_90 = 0;

            int impressionfans_60 = 0;
            int impressionpagepost_60 = 0;
            int impressionuserpost_60 = 0;
            int impressioncoupn_60 = 0;
            int impressionother_60 = 0;
            int impressionmention_60 = 0;
            int impressioncheckin_60 = 0;
            int impressionquestion_60 = 0;
            int impressionevent_60 = 0;

            int impressionfans_30 = 0;
            int impressionpagepost_30 = 0;
            int impressionuserpost_30 = 0;
            int impressioncoupn_30 = 0;
            int impressionother_30 = 0;
            int impressionmention_30 = 0;
            int impressioncheckin_30 = 0;
            int impressionquestion_30 = 0;
            int impressionevent_30 = 0;

            int impressionfans_15 = 0;
            int impressionpagepost_15 = 0;
            int impressionuserpost_15 = 0;
            int impressioncoupn_15 = 0;
            int impressionother_15 = 0;
            int impressionmention_15 = 0;
            int impressioncheckin_15 = 0;
            int impressionquestion_15 = 0;
            int impressionevent_15 = 0;

            int organic_90 = 0;
            int viral_90 = 0;
            int paid_90 = 0;

            int organic_60 = 0;
            int viral_60 = 0;
            int paid_60 = 0;

            int organic_30 = 0;
            int viral_30 = 0;
            int paid_30 = 0;

            int organic_15 = 0;
            int viral_15 = 0;
            int paid_15 = 0;

            int Newfans_90 = 0;
            int Newfans_60 = 0;
            int Newfans_30 = 0;
            int Newfans_15 = 0;

            int Unlike_90 = 0;
            int Unlike_60 = 0;
            int Unlike_30 = 0;
            int Unlike_15 = 0;

            int Impression_90 = 0;
            int Impression_60 = 0;
            int Impression_30 = 0;
            int Impression_15 = 0;

            int UniqueUser_90 = 0;
            int UniqueUser_60 = 0;
            int UniqueUser_30 = 0;
            int UniqueUser_15 = 0;

            int M_13_17_90 = 0;
            int M_18_24_90 = 0;
            int M_25_34_90 = 0;
            int M_35_44_90 = 0;
            int M_45_54_90 = 0;
            int M_55_64_90 = 0;
            int M_65_90 = 0;

            int F_13_17_90 = 0;
            int F_18_24_90 = 0;
            int F_25_34_90 = 0;
            int F_35_44_90 = 0;
            int F_45_54_90 = 0;
            int F_55_64_90 = 0;
            int F_65_90 = 0;

            int M_13_17_60 = 0;
            int M_18_24_60 = 0;
            int M_25_34_60 = 0;
            int M_35_44_60 = 0;
            int M_45_54_60 = 0;
            int M_55_64_60 = 0;
            int M_65_60 = 0;

            int F_13_17_60 = 0;
            int F_18_24_60 = 0;
            int F_25_34_60 = 0;
            int F_35_44_60 = 0;
            int F_45_54_60 = 0;
            int F_55_64_60 = 0;
            int F_65_60 = 0;

            int M_13_17_30 = 0;
            int M_18_24_30 = 0;
            int M_25_34_30 = 0;
            int M_35_44_30 = 0;
            int M_45_54_30 = 0;
            int M_55_64_30 = 0;
            int M_65_30 = 0;

            int F_13_17_30 = 0;
            int F_18_24_30 = 0;
            int F_25_34_30 = 0;
            int F_35_44_30 = 0;
            int F_45_54_30 = 0;
            int F_55_64_30 = 0;
            int F_65_30 = 0;

            int M_13_17_15 = 0;
            int M_18_24_15 = 0;
            int M_25_34_15 = 0;
            int M_35_44_15 = 0;
            int M_45_54_15 = 0;
            int M_55_64_15 = 0;
            int M_65_15 = 0;

            int F_13_17_15 = 0;
            int F_18_24_15 = 0;
            int F_25_34_15 = 0;
            int F_35_44_15 = 0;
            int F_45_54_15 = 0;
            int F_55_64_15 = 0;
            int F_65_15 = 0;

            int Sharing_90 = 0;
            int Sharing_60 = 0;
            int Sharing_30 = 0;
            int Sharing_15 = 0;

            int storyfans_90 = 0;
            int storypagepost_90 = 0;
            int storyuserpost_90 = 0;
            int storycoupn_90 = 0;
            int storyother_90 = 0;
            int storymention_90 = 0;
            int storycheckin_90 = 0;
            int storyquestion_90 = 0;
            int storyevent_90 = 0;

            int storyfans_60 = 0;
            int storypagepost_60 = 0;
            int storyuserpost_60 = 0;
            int storycoupn_60 = 0;
            int storyother_60 = 0;
            int storymention_60 = 0;
            int storycheckin_60 = 0;
            int storyquestion_60 = 0;
            int storyevent_60 = 0;

            int storyfans_30 = 0;
            int storypagepost_30 = 0;
            int storyuserpost_30 = 0;
            int storycoupn_30 = 0;
            int storyother_30 = 0;
            int storymention_30 = 0;
            int storycheckin_30 = 0;
            int storyquestion_30 = 0;
            int storyevent_30 = 0;

            int storyfans_15 = 0;
            int storypagepost_15 = 0;
            int storyuserpost_15 = 0;
            int storycoupn_15 = 0;
            int storyother_15 = 0;
            int storymention_15 = 0;
            int storycheckin_15 = 0;
            int storyquestion_15 = 0;
            int storyevent_15 = 0;

            int Sharing_M_13_17_90 = 0;
            int Sharing_M_18_24_90 = 0;
            int Sharing_M_25_34_90 = 0;
            int Sharing_M_35_44_90 = 0;
            int Sharing_M_45_54_90 = 0;
            int Sharing_M_55_64_90 = 0;
            int Sharing_M_65_90 = 0;

            int Sharing_F_13_17_90 = 0;
            int Sharing_F_18_24_90 = 0;
            int Sharing_F_25_34_90 = 0;
            int Sharing_F_35_44_90 = 0;
            int Sharing_F_45_54_90 = 0;
            int Sharing_F_55_64_90 = 0;
            int Sharing_F_65_90 = 0;

            int Sharing_M_13_17_60 = 0;
            int Sharing_M_18_24_60 = 0;
            int Sharing_M_25_34_60 = 0;
            int Sharing_M_35_44_60 = 0;
            int Sharing_M_45_54_60 = 0;
            int Sharing_M_55_64_60 = 0;
            int Sharing_M_65_60 = 0;

            int Sharing_F_13_17_60 = 0;
            int Sharing_F_18_24_60 = 0;
            int Sharing_F_25_34_60 = 0;
            int Sharing_F_35_44_60 = 0;
            int Sharing_F_45_54_60 = 0;
            int Sharing_F_55_64_60 = 0;
            int Sharing_F_65_60 = 0;

            int Sharing_M_13_17_30 = 0;
            int Sharing_M_18_24_30 = 0;
            int Sharing_M_25_34_30 = 0;
            int Sharing_M_35_44_30 = 0;
            int Sharing_M_45_54_30 = 0;
            int Sharing_M_55_64_30 = 0;
            int Sharing_M_65_30 = 0;

            int Sharing_F_13_17_30 = 0;
            int Sharing_F_18_24_30 = 0;
            int Sharing_F_25_34_30 = 0;
            int Sharing_F_35_44_30 = 0;
            int Sharing_F_45_54_30 = 0;
            int Sharing_F_55_64_30 = 0;
            int Sharing_F_65_30 = 0;

            int Sharing_M_13_17_15 = 0;
            int Sharing_M_18_24_15 = 0;
            int Sharing_M_25_34_15 = 0;
            int Sharing_M_35_44_15 = 0;
            int Sharing_M_45_54_15 = 0;
            int Sharing_M_55_64_15 = 0;
            int Sharing_M_65_15 = 0;

            int Sharing_F_13_17_15 = 0;
            int Sharing_F_18_24_15 = 0;
            int Sharing_F_25_34_15 = 0;
            int Sharing_F_35_44_15 = 0;
            int Sharing_F_45_54_15 = 0;
            int Sharing_F_55_64_15 = 0;
            int Sharing_F_65_15 = 0;

            string TotalPageLike = "0";
            string TalkinAbout = "0";
            FacebookClient fb = new FacebookClient();
            fb.AccessToken = AccessToken;
            #region checking Access token is valid or not
            try
            {
                System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls;
                dynamic friends = fb.Get("v2.0/" + FBProfileId);
                string fancountPage = friends["likes"].ToString();

            }
            catch (Exception)
            {
                return "";
            }
            #endregion
            try
            {
                #region likes

                string facebookpageUrl = "https://graph.facebook.com/v2.3/" + ProfileId + "?access_token=" + AccessToken;
                string outputfacepageUrl = getFacebookResponse(facebookpageUrl);
                JObject pageobj = JObject.Parse(outputfacepageUrl);

                try
                {
                    TotalPageLike = pageobj["likes"].ToString();
                }
                catch (Exception ex)
                {
                    TotalPageLike = "0";
                }
                try
                {
                    TalkinAbout = pageobj["talking_about_count"].ToString();
                }
                catch (Exception ex)
                {
                    TalkinAbout = "0";
                }

                //Likes
                long since = DateTime.Now.AddDays(-90).ToUnixTimestamp();
                long until = DateTime.Now.ToUnixTimestamp();
                string facebooknewfanUrl = "https://graph.facebook.com/v2.3/" + ProfileId + "/insights/page_fan_adds?pretty=0&since=" + since.ToString() + "&suppress_http_code=1&until=" + until.ToString() + "&access_token=" + AccessToken;
                string outputface = getFacebookResponse(facebooknewfanUrl);
                JArray likesobj = JArray.Parse(JArray.Parse(JObject.Parse(outputface)["data"].ToString())[0]["values"].ToString());

                int i = 0;
                foreach (JObject obj in likesobj)
                {
                    try
                    {
                        strlikes_90 += obj["value"].ToString() + ",";
                    }
                    catch (Exception ex)
                    {
                        strlikes_90 += "0,";
                    }
                    //Likes 90 days
                    try
                    {
                        Newfans_90 += Int32.Parse(obj["value"].ToString());
                    }
                    catch (Exception ex)
                    {
                        Newfans_90 += 0;
                    }
                    //Likes 60 days
                    if (i > 29)
                    {
                        try
                        {
                            strlikes_60 += obj["value"].ToString() + ",";
                        }
                        catch (Exception ex)
                        {
                            strlikes_60 += "0,";
                        }
                        try
                        {
                            Newfans_60 += Int32.Parse(obj["value"].ToString());
                        }
                        catch (Exception ex)
                        {
                            Newfans_60 += 0;
                        }
                    }
                    //Likes 30 days
                    if (i > 59)
                    {
                        try
                        {
                            strlikes_30 += obj["value"].ToString() + ",";
                        }
                        catch (Exception ex)
                        {
                            strlikes_30 += "0,";
                        }
                        try
                        {
                            Newfans_30 += Int32.Parse(obj["value"].ToString());
                        }
                        catch (Exception ex)
                        {
                            Newfans_30 += 0;
                        }
                    }
                    //Likes 15 days
                    if (i > 74)
                    {
                        try
                        {
                            strlikes_15 += obj["value"].ToString() + ",";
                        }
                        catch (Exception ex)
                        {
                            strlikes_15 += "0,";
                        }
                        try
                        {
                            Newfans_15 += Int32.Parse(obj["value"].ToString());
                        }
                        catch (Exception ex)
                        {
                            Newfans_15 += 0;
                        }
                    }
                    i++;
                }
                strlikes_90 = strlikes_90.TrimEnd(',');
                strlikes_60 = strlikes_60.TrimEnd(',');
                strlikes_30 = strlikes_30.TrimEnd(',');
                strlikes_15 = strlikes_15.TrimEnd(',');
                #endregion

                #region Unlike
                i = 0;
                string facebookunlikjeUrl = "https://graph.facebook.com/v2.3/" + ProfileId + "/insights/page_fan_removes?pretty=0&since=" + since.ToString() + "&suppress_http_code=1&until=" + until.ToString() + "&access_token=" + AccessToken;
                string outputfaceunlike = getFacebookResponse(facebookunlikjeUrl);
                JArray unlikesobj = JArray.Parse(JArray.Parse(JObject.Parse(outputfaceunlike)["data"].ToString())[0]["values"].ToString());
                foreach (JObject obj in unlikesobj)
                {
                    try
                    {
                        strunlikes_90 += obj["value"].ToString() + ",";
                    }
                    catch (Exception ex)
                    {
                        strunlikes_90 += "0,";
                    }
                    //Unlike 90 days
                    try
                    {
                        Unlike_90 += Int32.Parse(obj["value"].ToString());
                    }
                    catch (Exception ex)
                    {
                        Unlike_90 += 0;
                    }
                    //Unlike 60 days
                    if (i > 29)
                    {
                        try
                        {
                            strunlikes_60 += obj["value"].ToString() + ",";
                        }
                        catch (Exception ex)
                        {
                            strunlikes_60 += "0,";
                        }
                        try
                        {
                            Unlike_60 += Int32.Parse(obj["value"].ToString());
                        }
                        catch (Exception ex)
                        {
                            Unlike_60 += 0;
                        }
                    }
                    //Unlike 30 days
                    if (i > 59)
                    {
                        try
                        {
                            strunlikes_30 += obj["value"].ToString() + ",";
                        }
                        catch (Exception ex)
                        {
                            strunlikes_30 += "0,";
                        }
                        try
                        {
                            Unlike_30 += Int32.Parse(obj["value"].ToString());
                        }
                        catch (Exception ex)
                        {
                            Unlike_30 += 0;
                        }
                    }
                    //Unlike 15 days
                    if (i > 74)
                    {
                        try
                        {
                            strunlikes_15 += obj["value"].ToString() + ",";
                        }
                        catch (Exception ex)
                        {
                            strunlikes_15 += "0,";
                        }
                        try
                        {
                            Unlike_15 += Int32.Parse(obj["value"].ToString());
                        }
                        catch (Exception ex)
                        {
                            Unlike_15 += 0;
                        }
                    }
                    i++;
                }
                strunlikes_90 = strunlikes_90.TrimEnd(',');
                strunlikes_60 = strunlikes_60.TrimEnd(',');
                strunlikes_30 = strunlikes_30.TrimEnd(',');
                strunlikes_15 = strunlikes_15.TrimEnd(',');
                #endregion

                #region Impressions
                i = 0;
                string facebookimpressionUrl = "https://graph.facebook.com/v2.3/" + ProfileId + "/insights/page_impressions?pretty=0&since=" + since.ToString() + "&suppress_http_code=1&until=" + until.ToString() + "&access_token=" + AccessToken;
                string outputfaceunimpression = getFacebookResponse(facebookimpressionUrl);
                JArray impressionobj = JArray.Parse(JArray.Parse(JObject.Parse(outputfaceunimpression)["data"].ToString())[0]["values"].ToString());
                foreach (JObject obj in impressionobj)
                {
                    try
                    {
                        strimpression_90 += obj["value"].ToString() + ",";
                    }
                    catch (Exception ex)
                    {
                        strimpression_90 += "0,";
                    }
                    //Impressions 90 days
                    try
                    {
                        Impression_90 += Int32.Parse(obj["value"].ToString());
                    }
                    catch (Exception ex)
                    {
                        Impression_90 += 0;
                    }
                    //Impressions 60 days
                    if (i > 29)
                    {
                        try
                        {
                            strimpression_60 += obj["value"].ToString() + ",";
                        }
                        catch (Exception ex)
                        {
                            strimpression_60 += "0,";
                        }
                        try
                        {
                            Impression_60 += Int32.Parse(obj["value"].ToString());
                        }
                        catch (Exception ex)
                        {
                            Impression_60 += 0;
                        }
                    }
                    //Impressions 30 days
                    if (i > 59)
                    {
                        try
                        {
                            strimpression_30 += obj["value"].ToString() + ",";
                        }
                        catch (Exception ex)
                        {
                            strimpression_30 += "0,";
                        }
                        try
                        {
                            Impression_30 += Int32.Parse(obj["value"].ToString());
                        }
                        catch (Exception ex)
                        {
                            Impression_30 += 0;
                        }
                    }
                    //Impressions 15 days
                    if (i > 74)
                    {
                        try
                        {
                            strimpression_15 += obj["value"].ToString() + ",";
                        }
                        catch (Exception ex)
                        {
                            strimpression_15 += "0,";
                        }
                        try
                        {
                            Impression_15 += Int32.Parse(obj["value"].ToString());
                        }
                        catch (Exception ex)
                        {
                            Impression_15 += 0;
                        }
                    }
                    i++;
                }
                strimpression_90 = strimpression_90.TrimEnd(',');
                strimpression_60 = strimpression_60.TrimEnd(',');
                strimpression_30 = strimpression_30.TrimEnd(',');
                strimpression_15 = strimpression_15.TrimEnd(',');
                #endregion

                #region Impressions Users
                i = 0;
                string facebookuniqueUrl = "https://graph.facebook.com/v2.3/" + ProfileId + "/insights/page_impressions_unique?pretty=0&since=" + since.ToString() + "&suppress_http_code=1&until=" + until.ToString() + "&access_token=" + AccessToken;
                string outputfaceununoque = getFacebookResponse(facebookuniqueUrl);
                JArray uniqueobj = JArray.Parse(JArray.Parse(JObject.Parse(outputfaceununoque)["data"].ToString())[0]["values"].ToString());
                foreach (JObject obj in uniqueobj)
                {
                    //Impressions Users 90 days
                    try
                    {
                        UniqueUser_90 += Int32.Parse(obj["value"].ToString());
                    }
                    catch (Exception ex)
                    {
                        UniqueUser_90 += 0;
                    }
                    //Impressions Users 60 days
                    if (i > 29)
                    {
                        try
                        {
                            UniqueUser_60 += Int32.Parse(obj["value"].ToString());
                        }
                        catch (Exception ex)
                        {
                            UniqueUser_60 += 0;
                        }
                    }
                    //Impressions Users 30 days
                    if (i > 59)
                    {
                        try
                        {
                            UniqueUser_30 += Int32.Parse(obj["value"].ToString());
                        }
                        catch (Exception ex)
                        {
                            UniqueUser_30 += 0;
                        }
                    }
                    //Impressions Users 15 days
                    if (i > 74)
                    {
                        try
                        {
                            UniqueUser_15 += Int32.Parse(obj["value"].ToString());
                        }
                        catch (Exception ex)
                        {
                            UniqueUser_15 += 0;
                        }
                    }
                    i++;
                }
                #endregion

                #region Impressions Breakdown
                i = 0;
                string facebookstory_typeUrl90 = "https://graph.facebook.com/v2.3/" + ProfileId + "/insights/page_impressions_by_story_type?pretty=0&since=" + since.ToString() + "&suppress_http_code=1&until=" + until.ToString() + "&access_token=" + AccessToken;
                string outputfaceunstory_type90 = getFacebookResponse(facebookstory_typeUrl90);
                JArray facebookstory_typeUrlobj90 = JArray.Parse(JArray.Parse(JObject.Parse(outputfaceunstory_type90)["data"].ToString())[0]["values"].ToString());
                foreach (JObject obj in facebookstory_typeUrlobj90)
                {
                    //Impressions Breakdown 90 days
                    try
                    {
                        impressionfans_90 += Int32.Parse(obj["value"]["fan"].ToString());
                    }
                    catch (Exception ex)
                    {
                        impressionfans_90 += 0;
                    }
                    try
                    {
                        impressionpagepost_90 += Int32.Parse(obj["value"]["page post"].ToString());
                    }
                    catch (Exception ex)
                    {
                        impressionpagepost_90 += 0;
                    }
                    try
                    {
                        impressionuserpost_90 += Int32.Parse(obj["value"]["user post"].ToString());
                    }
                    catch (Exception ex)
                    {
                        impressionuserpost_90 += 0;
                    }
                    try
                    {
                        impressioncoupn_90 += Int32.Parse(obj["value"]["coupon"].ToString());
                    }
                    catch (Exception ex)
                    {
                        impressioncoupn_90 += 0;
                    }
                    try
                    {
                        impressionother_90 += Int32.Parse(obj["value"]["other"].ToString());
                    }
                    catch (Exception ex)
                    {
                        impressionother_90 += 0;
                    }
                    try
                    {
                        impressionmention_90 += Int32.Parse(obj["value"]["mention"].ToString());
                    }
                    catch (Exception ex)
                    {
                        impressionmention_90 += 0;
                    }
                    try
                    {
                        impressioncheckin_90 += Int32.Parse(obj["value"]["checkin"].ToString());
                    }
                    catch (Exception ex)
                    {
                        impressioncheckin_90 += 0;
                    }
                    try
                    {
                        impressionquestion_90 += Int32.Parse(obj["value"]["question"].ToString());
                    }
                    catch (Exception ex)
                    {
                        impressionquestion_90 += 0;
                    }
                    try
                    {
                        impressionevent_90 += Int32.Parse(obj["value"]["event"].ToString());
                    }
                    catch (Exception ex)
                    {
                        impressionevent_90 += 0;
                    }
                    //Impressions Breakdown 60 days
                    if (i > 29)
                    {
                        try
                        {
                            impressionfans_60 += Int32.Parse(obj["value"]["fan"].ToString());
                        }
                        catch (Exception ex)
                        {
                            impressionfans_60 += 0;
                        }
                        try
                        {
                            impressionpagepost_60 += Int32.Parse(obj["value"]["page post"].ToString());
                        }
                        catch (Exception ex)
                        {
                            impressionpagepost_60 += 0;
                        }
                        try
                        {
                            impressionuserpost_60 += Int32.Parse(obj["value"]["user post"].ToString());
                        }
                        catch (Exception ex)
                        {
                            impressionuserpost_60 += 0;
                        }
                        try
                        {
                            impressioncoupn_60 += Int32.Parse(obj["value"]["coupon"].ToString());
                        }
                        catch (Exception ex)
                        {
                            impressioncoupn_60 += 0;
                        }
                        try
                        {
                            impressionother_60 += Int32.Parse(obj["value"]["other"].ToString());
                        }
                        catch (Exception ex)
                        {
                            impressionother_60 += 0;
                        }
                        try
                        {
                            impressionmention_60 += Int32.Parse(obj["value"]["mention"].ToString());
                        }
                        catch (Exception ex)
                        {
                            impressionmention_60 += 0;
                        }
                        try
                        {
                            impressioncheckin_60 += Int32.Parse(obj["value"]["checkin"].ToString());
                        }
                        catch (Exception ex)
                        {
                            impressioncheckin_60 += 0;
                        }
                        try
                        {
                            impressionquestion_60 += Int32.Parse(obj["value"]["question"].ToString());
                        }
                        catch (Exception ex)
                        {
                            impressionquestion_60 += 0;
                        }
                        try
                        {
                            impressionevent_60 += Int32.Parse(obj["value"]["event"].ToString());
                        }
                        catch (Exception ex)
                        {
                            impressionevent_60 += 0;
                        }
                    }
                    //Impressions Breakdown 30 days
                    if (i > 59)
                    {
                        try
                        {
                            impressionfans_30 += Int32.Parse(obj["value"]["fan"].ToString());
                        }
                        catch (Exception ex)
                        {
                            impressionfans_30 += 0;
                        }
                        try
                        {
                            impressionpagepost_30 += Int32.Parse(obj["value"]["page post"].ToString());
                        }
                        catch (Exception ex)
                        {
                            impressionpagepost_30 += 0;
                        }
                        try
                        {
                            impressionuserpost_30 += Int32.Parse(obj["value"]["user post"].ToString());
                        }
                        catch (Exception ex)
                        {
                            impressionuserpost_30 += 0;
                        }
                        try
                        {
                            impressioncoupn_30 += Int32.Parse(obj["value"]["coupon"].ToString());
                        }
                        catch (Exception ex)
                        {
                            impressioncoupn_30 += 0;
                        }
                        try
                        {
                            impressionother_30 += Int32.Parse(obj["value"]["other"].ToString());
                        }
                        catch (Exception ex)
                        {
                            impressionother_30 += 0;
                        }
                        try
                        {
                            impressionmention_30 += Int32.Parse(obj["value"]["mention"].ToString());
                        }
                        catch (Exception ex)
                        {
                            impressionmention_30 += 0;
                        }
                        try
                        {
                            impressioncheckin_30 += Int32.Parse(obj["value"]["checkin"].ToString());
                        }
                        catch (Exception ex)
                        {
                            impressioncheckin_30 += 0;
                        }
                        try
                        {
                            impressionquestion_30 += Int32.Parse(obj["value"]["question"].ToString());
                        }
                        catch (Exception ex)
                        {
                            impressionquestion_30 += 0;
                        }
                        try
                        {
                            impressionevent_30 += Int32.Parse(obj["value"]["event"].ToString());
                        }
                        catch (Exception ex)
                        {
                            impressionevent_30 += 0;
                        }
                    }
                    //Impressions Breakdown 15 days
                    if (i > 74)
                    {
                        try
                        {
                            impressionfans_15 += Int32.Parse(obj["value"]["fan"].ToString());
                        }
                        catch (Exception ex)
                        {
                            impressionfans_15 += 0;
                        }
                        try
                        {
                            impressionpagepost_15 += Int32.Parse(obj["value"]["page post"].ToString());
                        }
                        catch (Exception ex)
                        {
                            impressionpagepost_15 += 0;
                        }
                        try
                        {
                            impressionuserpost_15 += Int32.Parse(obj["value"]["user post"].ToString());
                        }
                        catch (Exception ex)
                        {
                            impressionuserpost_15 += 0;
                        }
                        try
                        {
                            impressioncoupn_15 += Int32.Parse(obj["value"]["coupon"].ToString());
                        }
                        catch (Exception ex)
                        {
                            impressioncoupn_15 += 0;
                        }
                        try
                        {
                            impressionother_15 += Int32.Parse(obj["value"]["other"].ToString());
                        }
                        catch (Exception ex)
                        {
                            impressionother_15 += 0;
                        }
                        try
                        {
                            impressionmention_15 += Int32.Parse(obj["value"]["mention"].ToString());
                        }
                        catch (Exception ex)
                        {
                            impressionmention_15 += 0;
                        }
                        try
                        {
                            impressioncheckin_15 += Int32.Parse(obj["value"]["checkin"].ToString());
                        }
                        catch (Exception ex)
                        {
                            impressioncheckin_15 += 0;
                        }
                        try
                        {
                            impressionquestion_15 += Int32.Parse(obj["value"]["question"].ToString());
                        }
                        catch (Exception ex)
                        {
                            impressionquestion_15 += 0;
                        }
                        try
                        {
                            impressionevent_15 += Int32.Parse(obj["value"]["event"].ToString());
                        }
                        catch (Exception ex)
                        {
                            impressionevent_15 += 0;
                        }
                    }
                    i++;
                }
                #endregion

                #region commented Impressions Breakdown code
                //Impressions Breakdown 60 days
                //since = DateTime.Now.AddDays(-60).ToUnixTimestamp();
                //string facebookstory_typeUrl60 = "https://graph.facebook.com/v2.3/" + ProfileId + "/insights/page_impressions_by_story_type?pretty=0&since=" + since.ToString() + "&suppress_http_code=1&until=" + until.ToString() + "&access_token=" + AccessToken;
                //string outputfaceunstory_type60 = getFacebookResponse(facebookstory_typeUrl60);
                //JArray facebookstory_typeUrlobj60 = JArray.Parse(JArray.Parse(JObject.Parse(outputfaceunstory_type60)["data"].ToString())[0]["values"].ToString());
                //foreach (JObject obj in facebookstory_typeUrlobj60)
                //{
                //    try
                //    {
                //        impressionfans_60 += Int32.Parse(obj["value"]["fan"].ToString());
                //    }
                //    catch (Exception ex)
                //    {
                //        impressionfans_60 += 0;
                //    }
                //    try
                //    {
                //        impressionpagepost_60 += Int32.Parse(obj["value"]["page post"].ToString());
                //    }
                //    catch (Exception ex)
                //    {
                //        impressionpagepost_60 += 0;
                //    }
                //    try
                //    {
                //        impressionuserpost_60 += Int32.Parse(obj["value"]["user post"].ToString());
                //    }
                //    catch (Exception ex)
                //    {
                //        impressionuserpost_60 += 0;
                //    }
                //    try
                //    {
                //        impressioncoupn_60 += Int32.Parse(obj["value"]["coupon"].ToString());
                //    }
                //    catch (Exception ex)
                //    {
                //        impressioncoupn_60 += 0;
                //    }
                //    try
                //    {
                //        impressionother_60 += Int32.Parse(obj["value"]["other"].ToString());
                //    }
                //    catch (Exception ex)
                //    {
                //        impressionother_60 += 0;
                //    }
                //    try
                //    {
                //        impressionmention_60 += Int32.Parse(obj["value"]["mention"].ToString());
                //    }
                //    catch (Exception ex)
                //    {
                //        impressionmention_60 += 0;
                //    }
                //    try
                //    {
                //        impressioncheckin_60 += Int32.Parse(obj["value"]["checkin"].ToString());
                //    }
                //    catch (Exception ex)
                //    {
                //        impressioncheckin_60 += 0;
                //    }
                //    try
                //    {
                //        impressionquestion_60 += Int32.Parse(obj["value"]["question"].ToString());
                //    }
                //    catch (Exception ex)
                //    {
                //        impressionquestion_60 += 0;
                //    }
                //    try
                //    {
                //        impressionevent_60 += Int32.Parse(obj["value"]["event"].ToString());
                //    }
                //    catch (Exception ex)
                //    {
                //        impressionevent_60 += 0;
                //    }
                //}

                //Impressions Breakdown 30 days
                //since = DateTime.Now.AddDays(-30).ToUnixTimestamp();
                //string facebookstory_typeUrl30 = "https://graph.facebook.com/v2.3/" + ProfileId + "/insights/page_impressions_by_story_type?pretty=0&since=" + since.ToString() + "&suppress_http_code=1&until=" + until.ToString() + "&access_token=" + AccessToken;
                //string outputfaceunstory_type30 = getFacebookResponse(facebookstory_typeUrl30);
                //JArray facebookstory_typeUrlobj30 = JArray.Parse(JArray.Parse(JObject.Parse(outputfaceunstory_type30)["data"].ToString())[0]["values"].ToString());
                //foreach (JObject obj in facebookstory_typeUrlobj30)
                //{
                //    try
                //    {
                //        impressionfans_30 += Int32.Parse(obj["value"]["fan"].ToString());
                //    }
                //    catch (Exception ex)
                //    {
                //        impressionfans_30 += 0;
                //    }
                //    try
                //    {
                //        impressionpagepost_30 += Int32.Parse(obj["value"]["page post"].ToString());
                //    }
                //    catch (Exception ex)
                //    {
                //        impressionpagepost_30 += 0;
                //    }
                //    try
                //    {
                //        impressionuserpost_30 += Int32.Parse(obj["value"]["user post"].ToString());
                //    }
                //    catch (Exception ex)
                //    {
                //        impressionuserpost_30 += 0;
                //    }
                //    try
                //    {
                //        impressioncoupn_30 += Int32.Parse(obj["value"]["coupon"].ToString());
                //    }
                //    catch (Exception ex)
                //    {
                //        impressioncoupn_30 += 0;
                //    }
                //    try
                //    {
                //        impressionother_30 += Int32.Parse(obj["value"]["other"].ToString());
                //    }
                //    catch (Exception ex)
                //    {
                //        impressionother_30 += 0;
                //    }
                //    try
                //    {
                //        impressionmention_30 += Int32.Parse(obj["value"]["mention"].ToString());
                //    }
                //    catch (Exception ex)
                //    {
                //        impressionmention_30 += 0;
                //    }
                //    try
                //    {
                //        impressioncheckin_30 += Int32.Parse(obj["value"]["checkin"].ToString());
                //    }
                //    catch (Exception ex)
                //    {
                //        impressioncheckin_30 += 0;
                //    }
                //    try
                //    {
                //        impressionquestion_30 += Int32.Parse(obj["value"]["question"].ToString());
                //    }
                //    catch (Exception ex)
                //    {
                //        impressionquestion_30 += 0;
                //    }
                //    try
                //    {
                //        impressionevent_30 += Int32.Parse(obj["value"]["event"].ToString());
                //    }
                //    catch (Exception ex)
                //    {
                //        impressionevent_30 += 0;
                //    }
                //}

                //Impressions Breakdown 15 days
                //since = DateTime.Now.AddDays(-15).ToUnixTimestamp();
                //string facebookstory_typeUrl15 = "https://graph.facebook.com/v2.3/" + ProfileId + "/insights/page_impressions_by_story_type?pretty=0&since=" + since.ToString() + "&suppress_http_code=1&until=" + until.ToString() + "&access_token=" + AccessToken;
                //string outputfaceunstory_type15 = getFacebookResponse(facebookstory_typeUrl15);
                //JArray facebookstory_typeUrlobj15 = JArray.Parse(JArray.Parse(JObject.Parse(outputfaceunstory_type15)["data"].ToString())[0]["values"].ToString());
                //foreach (JObject obj in facebookstory_typeUrlobj15)
                //{
                //    try
                //    {
                //        impressionfans_15 += Int32.Parse(obj["value"]["fan"].ToString());
                //    }
                //    catch (Exception ex)
                //    {
                //        impressionfans_15 += 0;
                //    }
                //    try
                //    {
                //        impressionpagepost_15 += Int32.Parse(obj["value"]["page post"].ToString());
                //    }
                //    catch (Exception ex)
                //    {
                //        impressionpagepost_15 += 0;
                //    }
                //    try
                //    {
                //        impressionuserpost_15 += Int32.Parse(obj["value"]["user post"].ToString());
                //    }
                //    catch (Exception ex)
                //    {
                //        impressionuserpost_15 += 0;
                //    }
                //    try
                //    {
                //        impressioncoupn_15 += Int32.Parse(obj["value"]["coupon"].ToString());
                //    }
                //    catch (Exception ex)
                //    {
                //        impressioncoupn_15 += 0;
                //    }
                //    impressionother_15 += Int32.Parse(obj["value"]["other"].ToString());
                //    impressionmention_15 += Int32.Parse(obj["value"]["mention"].ToString());
                //    impressioncheckin_15 += Int32.Parse(obj["value"]["checkin"].ToString());
                //    impressionquestion_15 += Int32.Parse(obj["value"]["question"].ToString());
                //    impressionevent_15 += Int32.Parse(obj["value"]["event"].ToString());
                //}
                #endregion

                #region Impressions Breakdown Organic
                i = 0;
                since = DateTime.Now.AddDays(-90).ToUnixTimestamp();
                string facebookorganic90 = "https://graph.facebook.com/v2.3/" + ProfileId + "/insights/page_impressions_organic?pretty=0&since=" + since.ToString() + "&suppress_http_code=1&until=" + until.ToString() + "&access_token=" + AccessToken;
                string outputfaceorganic90 = getFacebookResponse(facebookorganic90);
                JArray facebookorganicobj90 = JArray.Parse(JArray.Parse(JObject.Parse(outputfaceorganic90)["data"].ToString())[0]["values"].ToString());
                foreach (JObject obj in facebookorganicobj90)
                {
                    //Impressions Breakdown Organic 90 days
                    try
                    {
                        organic_90 += Int32.Parse(obj["value"].ToString());
                    }
                    catch (Exception ex)
                    {
                        organic_90 += 0;
                    }
                    //Impressions Breakdown Organic 60 days
                    if (i > 29)
                    {
                        try
                        {
                            organic_60 += Int32.Parse(obj["value"].ToString());
                        }
                        catch (Exception ex)
                        {
                            organic_60 += 0;
                        }
                    }
                    //Impressions Breakdown Organic 30 days
                    if (i > 59)
                    {
                        try
                        {
                            organic_30 += Int32.Parse(obj["value"].ToString());
                        }
                        catch (Exception ex)
                        {
                            organic_30 += 0;
                        }
                    }
                    //Impressions Breakdown Organic 15 days
                    if (i > 74)
                    {
                        try
                        {
                            organic_15 += Int32.Parse(obj["value"].ToString());
                        }
                        catch (Exception ex)
                        {
                            organic_15 += 0;
                        }
                    }
                    i++;
                }
                #endregion

                #region commented code of Impressions Breakdown Organic
                ////Impressions Breakdown Organic 60 days
                //since = DateTime.Now.AddDays(-60).ToUnixTimestamp();
                //string facebookorganic60 = "https://graph.facebook.com/v2.3/" + ProfileId + "/insights/page_impressions_organic?pretty=0&since=" + since.ToString() + "&suppress_http_code=1&until=" + until.ToString() + "&access_token=" + AccessToken;
                //string outputfaceorganic60 = getFacebookResponse(facebookorganic60);
                //JArray facebookorganicobj60 = JArray.Parse(JArray.Parse(JObject.Parse(outputfaceorganic60)["data"].ToString())[0]["values"].ToString());
                //foreach (JObject obj in facebookorganicobj60)
                //{
                //    try
                //    {
                //        organic_60 += Int32.Parse(obj["value"].ToString());
                //    }
                //    catch (Exception ex)
                //    {
                //        organic_60 += 0;
                //    }
                //}

                ////Impressions Breakdown Organic 30 days
                //since = DateTime.Now.AddDays(-30).ToUnixTimestamp();
                //string facebookorganic30 = "https://graph.facebook.com/v2.3/" + ProfileId + "/insights/page_impressions_organic?pretty=0&since=" + since.ToString() + "&suppress_http_code=1&until=" + until.ToString() + "&access_token=" + AccessToken;
                //string outputfaceorganic30 = getFacebookResponse(facebookorganic30);
                //JArray facebookorganicobj30 = JArray.Parse(JArray.Parse(JObject.Parse(outputfaceorganic30)["data"].ToString())[0]["values"].ToString());
                //foreach (JObject obj in facebookorganicobj30)
                //{
                //    try
                //    {
                //        organic_30 += Int32.Parse(obj["value"].ToString());
                //    }
                //    catch (Exception ex)
                //    {
                //        organic_30 += 0;
                //    }
                //}

                ////Impressions Breakdown Organic 15 days
                //since = DateTime.Now.AddDays(-15).ToUnixTimestamp();
                //string facebookorganic15 = "https://graph.facebook.com/v2.3/" + ProfileId + "/insights/page_impressions_organic?pretty=0&since=" + since.ToString() + "&suppress_http_code=1&until=" + until.ToString() + "&access_token=" + AccessToken;
                //string outputfaceorganic15 = getFacebookResponse(facebookorganic15);
                //JArray facebookorganicobj15 = JArray.Parse(JArray.Parse(JObject.Parse(outputfaceorganic15)["data"].ToString())[0]["values"].ToString());
                //foreach (JObject obj in facebookorganicobj15)
                //{
                //    try
                //    {
                //        organic_15 += Int32.Parse(obj["value"].ToString());
                //    }
                //    catch (Exception ex)
                //    {
                //        organic_15 += 0;
                //    }
                //}
                #endregion

                #region Impressions Breakdown Viral
                i = 0;
                since = DateTime.Now.AddDays(-90).ToUnixTimestamp();
                string facebookviral90 = "https://graph.facebook.com/v2.3/" + ProfileId + "/insights/page_impressions_viral?pretty=0&since=" + since.ToString() + "&suppress_http_code=1&until=" + until.ToString() + "&access_token=" + AccessToken;
                string outputfaceviral90 = getFacebookResponse(facebookviral90);
                JArray facebookviralobj90 = JArray.Parse(JArray.Parse(JObject.Parse(outputfaceviral90)["data"].ToString())[0]["values"].ToString());
                foreach (JObject obj in facebookviralobj90)
                {
                    //Impressions Breakdown Viral 90 days
                    try
                    {
                        viral_90 += Int32.Parse(obj["value"].ToString());
                    }
                    catch (Exception ex)
                    {
                        viral_90 += 0;
                    }
                    //Impressions Breakdown Viral 60 days
                    if (i > 29)
                    {
                        try
                        {
                            viral_60 += Int32.Parse(obj["value"].ToString());
                        }
                        catch (Exception ex)
                        {
                            viral_60 += 0;
                        }
                    }
                    if (i > 59)
                    {
                        try
                        {
                            viral_30 += Int32.Parse(obj["value"].ToString());
                        }
                        catch (Exception ex)
                        {
                            viral_30 += 0;
                        }
                    }
                    if (i > 74)
                    {
                        try
                        {
                            viral_15 += Int32.Parse(obj["value"].ToString());
                        }
                        catch (Exception ex)
                        {
                            viral_15 += 0;
                        }
                    }
                    i++;
                }
                #endregion

                #region Commented Impressions Breakdown Viral
                //since = DateTime.Now.AddDays(-60).ToUnixTimestamp();
                //string facebookviral60 = "https://graph.facebook.com/v2.3/" + ProfileId + "/insights/page_impressions_viral?pretty=0&since=" + since.ToString() + "&suppress_http_code=1&until=" + until.ToString() + "&access_token=" + AccessToken;
                //string outputfaceviral60 = getFacebookResponse(facebookviral60);
                //JArray facebookviralobj60 = JArray.Parse(JArray.Parse(JObject.Parse(outputfaceviral60)["data"].ToString())[0]["values"].ToString());
                //foreach (JObject obj in facebookviralobj60)
                //{
                //    //Impressions Breakdown Viral 60 days
                //    try
                //    {
                //        viral_60 += Int32.Parse(obj["value"].ToString());
                //    }
                //    catch (Exception ex)
                //    {
                //        viral_60 += 0;
                //    }
                //}

                ////Impressions Breakdown Viral 30 days
                //since = DateTime.Now.AddDays(-30).ToUnixTimestamp();
                //string facebookviral30 = "https://graph.facebook.com/v2.3/" + ProfileId + "/insights/page_impressions_viral?pretty=0&since=" + since.ToString() + "&suppress_http_code=1&until=" + until.ToString() + "&access_token=" + AccessToken;
                //string outputfaceviral30 = getFacebookResponse(facebookviral30);
                //JArray facebookviralobj30 = JArray.Parse(JArray.Parse(JObject.Parse(outputfaceviral30)["data"].ToString())[0]["values"].ToString());
                //foreach (JObject obj in facebookviralobj30)
                //{
                //    try
                //    {
                //        viral_30 += Int32.Parse(obj["value"].ToString());
                //    }
                //    catch (Exception ex)
                //    {
                //        viral_30 += 0;
                //    }
                //}

                ////Impressions Breakdown Viral 15 days
                //since = DateTime.Now.AddDays(-15).ToUnixTimestamp();
                //string facebookviral15 = "https://graph.facebook.com/v2.3/" + ProfileId + "/insights/page_impressions_viral?pretty=0&since=" + since.ToString() + "&suppress_http_code=1&until=" + until.ToString() + "&access_token=" + AccessToken;
                //string outputfaceviral15 = getFacebookResponse(facebookviral15);
                //JArray facebookviralobj15 = JArray.Parse(JArray.Parse(JObject.Parse(outputfaceviral15)["data"].ToString())[0]["values"].ToString());
                //foreach (JObject obj in facebookviralobj15)
                //{
                //    try
                //    {
                //        viral_15 += Int32.Parse(obj["value"].ToString());
                //    }
                //    catch (Exception ex)
                //    {
                //        viral_15 += 0;
                //    }
                //}
                #endregion

                #region Impressions Breakdown Paid
                i = 0;
                since = DateTime.Now.AddDays(-90).ToUnixTimestamp();
                string facebookpaid90 = "https://graph.facebook.com/v2.3/" + ProfileId + "/insights/page_impressions_paid?pretty=0&since=" + since.ToString() + "&suppress_http_code=1&until=" + until.ToString() + "&access_token=" + AccessToken;
                string outputfacepaid90 = getFacebookResponse(facebookpaid90);
                JArray facebookpaidobj90 = JArray.Parse(JArray.Parse(JObject.Parse(outputfacepaid90)["data"].ToString())[0]["values"].ToString());
                foreach (JObject obj in facebookpaidobj90)
                {
                    //Impressions Breakdown Paid 90 days
                    try
                    {
                        paid_90 += Int32.Parse(obj["value"].ToString());
                    }
                    catch (Exception ex)
                    {
                        paid_90 += 0;
                    }
                    //Impressions Breakdown Paid 60 days
                    if (i > 29)
                    {
                        try
                        {
                            paid_60 += Int32.Parse(obj["value"].ToString());
                        }
                        catch (Exception ex)
                        {
                            paid_60 += 0;
                        }
                    }
                    //Impressions Breakdown Paid 30 days
                    if (i > 59)
                    {
                        try
                        {
                            paid_30 += Int32.Parse(obj["value"].ToString());
                        }
                        catch (Exception ex)
                        {
                            paid_30 += 0;
                        }
                    }
                    //Impressions Breakdown Paid 30 days
                    if (i > 74)
                    {
                        try
                        {
                            paid_15 += Int32.Parse(obj["value"].ToString());
                        }
                        catch (Exception ex)
                        {
                            paid_15 += 0;
                        }
                    }
                    i++;
                }
                #endregion

                #region Commented Code of Impressions Breakdown Paid
                //since = DateTime.Now.AddDays(-60).ToUnixTimestamp();
                //string facebookpaid60 = "https://graph.facebook.com/v2.3/" + ProfileId + "/insights/page_impressions_paid?pretty=0&since=" + since.ToString() + "&suppress_http_code=1&until=" + until.ToString() + "&access_token=" + AccessToken;
                //string outputfacepaid60 = getFacebookResponse(facebookpaid60);
                //JArray facebookpaidobj60 = JArray.Parse(JArray.Parse(JObject.Parse(outputfacepaid60)["data"].ToString())[0]["values"].ToString());
                //foreach (JObject obj in facebookpaidobj60)
                //{
                //    //Impressions Breakdown Paid 60 days
                //    try
                //    {
                //        paid_60 += Int32.Parse(obj["value"].ToString());
                //    }
                //    catch (Exception ex)
                //    {
                //        paid_60 += 0;
                //    }
                //}
                ////Impressions Breakdown Paid 30 days
                //since = DateTime.Now.AddDays(-30).ToUnixTimestamp();
                //string facebookpaid30 = "https://graph.facebook.com/v2.3/" + ProfileId + "/insights/page_impressions_paid?pretty=0&since=" + since.ToString() + "&suppress_http_code=1&until=" + until.ToString() + "&access_token=" + AccessToken;
                //string outputfacepaid30 = getFacebookResponse(facebookpaid30);
                //JArray facebookpaidobj30 = JArray.Parse(JArray.Parse(JObject.Parse(outputfacepaid30)["data"].ToString())[0]["values"].ToString());
                //foreach (JObject obj in facebookpaidobj30)
                //{
                //    try
                //    {
                //        paid_30 += Int32.Parse(obj["value"].ToString());
                //    }
                //    catch (Exception ex)
                //    {
                //        paid_30 += 0;
                //    }
                //}
                ////Impressions Breakdown Paid 15 days
                //since = DateTime.Now.AddDays(-15).ToUnixTimestamp();
                //string facebookpaid15 = "https://graph.facebook.com/v2.3/" + ProfileId + "/insights/page_impressions_paid?pretty=0&since=" + since.ToString() + "&suppress_http_code=1&until=" + until.ToString() + "&access_token=" + AccessToken;
                //string outputfacepaid15 = getFacebookResponse(facebookpaid15);
                //JArray facebookpaidobj15 = JArray.Parse(JArray.Parse(JObject.Parse(outputfacepaid15)["data"].ToString())[0]["values"].ToString());
                //foreach (JObject obj in facebookpaidobj15)
                //{
                //    try
                //    {
                //        paid_15 += Int32.Parse(obj["value"].ToString());
                //    }
                //    catch (Exception ex)
                //    {
                //        paid_15 += 0;
                //    }
                //}
                #endregion

                //Page Impressions by Age & Gender
                i = 0;
                since = DateTime.Now.AddDays(-90).ToUnixTimestamp();
                string facebookimpressionbyage = "https://graph.facebook.com/v2.3/" + ProfileId + "/insights/page_impressions_by_age_gender_unique?pretty=0&since=" + since.ToString() + "&suppress_http_code=1&until=" + until.ToString() + "&access_token=" + AccessToken;
                string outputfaceimpressionbyage = getFacebookResponse(facebookimpressionbyage);
                JArray facebookimpressionbyageobj = JArray.Parse(JArray.Parse(JObject.Parse(outputfaceimpressionbyage)["data"].ToString())[0]["values"].ToString());
                foreach (JObject obj in facebookimpressionbyageobj)
                {
                    //female 90 days
                    try
                    {
                        F_13_17_90 += Int32.Parse(obj["value"]["F.13-17"].ToString());
                    }
                    catch (Exception ex)
                    {
                        F_13_17_90 += 0;
                    }
                    try
                    {
                        F_18_24_90 += Int32.Parse(obj["value"]["F.18-24"].ToString());
                    }
                    catch (Exception ex)
                    {
                        F_18_24_90 += 0;
                    }
                    try
                    {
                        F_25_34_90 += Int32.Parse(obj["value"]["F.25-34"].ToString());
                    }
                    catch (Exception ex)
                    {
                        F_25_34_90 += 0;
                    }
                    try
                    {
                        F_35_44_90 += Int32.Parse(obj["value"]["F.35-44"].ToString());
                    }
                    catch (Exception ex)
                    {
                        F_35_44_90 += 0;
                    }
                    try
                    {
                        F_45_54_90 += Int32.Parse(obj["value"]["F.45-54"].ToString());
                    }
                    catch (Exception ex)
                    {
                        F_45_54_90 += 0;
                    }
                    try
                    {
                        F_55_64_90 += Int32.Parse(obj["value"]["F.55-64"].ToString());
                    }
                    catch (Exception ex)
                    {
                        F_55_64_90 += 0;
                    }
                    try
                    {
                        F_65_90 += Int32.Parse(obj["value"]["F.65+"].ToString());
                    }
                    catch (Exception ex)
                    {
                        F_65_90 += 0;
                    }
                    //male 90 days
                    try
                    {
                        M_13_17_90 += Int32.Parse(obj["value"]["M.13-17"].ToString());
                    }
                    catch (Exception ex)
                    {
                        M_13_17_90 += 0;
                    }
                    try
                    {
                        M_18_24_90 += Int32.Parse(obj["value"]["M.18-24"].ToString());
                    }
                    catch (Exception ex)
                    {
                        M_18_24_90 += 0;
                    }
                    try
                    {
                        M_25_34_90 += Int32.Parse(obj["value"]["M.25-34"].ToString());
                    }
                    catch (Exception ex)
                    {
                        M_25_34_90 += 0;
                    }
                    try
                    {
                        M_35_44_90 += Int32.Parse(obj["value"]["M.35-44"].ToString());
                    }
                    catch (Exception ex)
                    {
                        M_35_44_90 += 0;
                    }
                    try
                    {
                        M_45_54_90 += Int32.Parse(obj["value"]["M.45-54"].ToString());
                    }
                    catch (Exception ex)
                    {
                        M_45_54_90 += 0;
                    }
                    try
                    {
                        M_55_64_90 += Int32.Parse(obj["value"]["M.55-64"].ToString());
                    }
                    catch (Exception ex)
                    {
                        M_55_64_90 += 0;
                    }
                    try
                    {
                        M_65_90 += Int32.Parse(obj["value"]["M.65+"].ToString());
                    }
                    catch (Exception ex)
                    {
                        M_65_90 += 0;
                    }
                    if (i > 29)
                    {
                        //female 60 days
                        try
                        {
                            F_13_17_60 += Int32.Parse(obj["value"]["F.13-17"].ToString());
                        }
                        catch (Exception ex)
                        {
                            F_13_17_60 += 0;
                        }
                        try
                        {
                            F_18_24_60 += Int32.Parse(obj["value"]["F.18-24"].ToString());
                        }
                        catch (Exception ex)
                        {
                            F_18_24_60 += 0;
                        }
                        try
                        {
                            F_25_34_60 += Int32.Parse(obj["value"]["F.25-34"].ToString());
                        }
                        catch (Exception ex)
                        {
                            F_25_34_60 += 0;
                        }
                        try
                        {
                            F_35_44_60 += Int32.Parse(obj["value"]["F.35-44"].ToString());
                        }
                        catch (Exception ex)
                        {
                            F_35_44_60 += 0;
                        }
                        try
                        {
                            F_45_54_60 += Int32.Parse(obj["value"]["F.45-54"].ToString());
                        }
                        catch (Exception ex)
                        {
                            F_45_54_60 += 0;
                        }
                        try
                        {
                            F_55_64_60 += Int32.Parse(obj["value"]["F.55-64"].ToString());
                        }
                        catch (Exception ex)
                        {
                            F_55_64_60 += 0;
                        }
                        try
                        {
                            F_65_60 += Int32.Parse(obj["value"]["F.65+"].ToString());
                        }
                        catch (Exception ex)
                        {
                            F_65_60 += 0;
                        }
                        //male 60 days
                        try
                        {
                            M_13_17_60 += Int32.Parse(obj["value"]["M.13-17"].ToString());
                        }
                        catch (Exception ex)
                        {
                            M_13_17_60 += 0;
                        }
                        try
                        {
                            M_18_24_60 += Int32.Parse(obj["value"]["M.18-24"].ToString());
                        }
                        catch (Exception ex)
                        {
                            M_18_24_60 += 0;
                        }
                        try
                        {
                            M_25_34_60 += Int32.Parse(obj["value"]["M.25-34"].ToString());
                        }
                        catch (Exception ex)
                        {
                            M_25_34_60 += 0;
                        }
                        try
                        {
                            M_35_44_60 += Int32.Parse(obj["value"]["M.35-44"].ToString());
                        }
                        catch (Exception ex)
                        {
                            M_35_44_60 += 0;
                        }
                        try
                        {
                            M_45_54_60 += Int32.Parse(obj["value"]["M.45-54"].ToString());
                        }
                        catch (Exception ex)
                        {
                            M_45_54_60 += 0;
                        }
                        try
                        {
                            M_55_64_60 += Int32.Parse(obj["value"]["M.55-64"].ToString());
                        }
                        catch (Exception ex)
                        {
                            M_55_64_60 += 0;
                        }
                        try
                        {
                            M_65_60 += Int32.Parse(obj["value"]["M.65+"].ToString());
                        }
                        catch (Exception ex)
                        {
                            M_65_60 += 0;
                        }

                    }
                    if (i > 59)
                    {

                        //female 30 days
                        try
                        {
                            F_13_17_30 += Int32.Parse(obj["value"]["F.13-17"].ToString());
                        }
                        catch (Exception ex)
                        {
                            F_13_17_30 += 0;
                        }
                        try
                        {
                            F_18_24_30 += Int32.Parse(obj["value"]["F.18-24"].ToString());
                        }
                        catch (Exception ex)
                        {
                            F_18_24_30 += 0;
                        }
                        try
                        {
                            F_25_34_30 += Int32.Parse(obj["value"]["F.25-34"].ToString());
                        }
                        catch (Exception ex)
                        {
                            F_25_34_30 += 0;
                        }
                        try
                        {
                            F_35_44_30 += Int32.Parse(obj["value"]["F.35-44"].ToString());
                        }
                        catch (Exception ex)
                        {
                            F_35_44_30 += 0;
                        }
                        try
                        {
                            F_45_54_30 += Int32.Parse(obj["value"]["F.45-54"].ToString());
                        }
                        catch (Exception ex)
                        {
                            F_45_54_30 += 0;
                        }
                        try
                        {
                            F_55_64_30 += Int32.Parse(obj["value"]["F.55-64"].ToString());
                        }
                        catch (Exception ex)
                        {
                            F_55_64_30 += 0;
                        }
                        try
                        {
                            F_65_30 += Int32.Parse(obj["value"]["F.65+"].ToString());
                        }
                        catch (Exception ex)
                        {
                            F_65_30 += 0;
                        }
                        //male 30 days
                        try
                        {
                            M_13_17_30 += Int32.Parse(obj["value"]["M.13-17"].ToString());
                        }
                        catch (Exception ex)
                        {
                            M_13_17_30 += 0;
                        }
                        try
                        {
                            M_18_24_30 += Int32.Parse(obj["value"]["M.18-24"].ToString());
                        }
                        catch (Exception ex)
                        {
                            M_18_24_30 += 0;
                        }
                        try
                        {
                            M_25_34_30 += Int32.Parse(obj["value"]["M.25-34"].ToString());
                        }
                        catch (Exception ex)
                        {
                            M_25_34_30 += 0;
                        }
                        try
                        {
                            M_35_44_30 += Int32.Parse(obj["value"]["M.35-44"].ToString());
                        }
                        catch (Exception ex)
                        {
                            M_35_44_30 += 0;
                        }
                        try
                        {
                            M_45_54_30 += Int32.Parse(obj["value"]["M.45-54"].ToString());
                        }
                        catch (Exception ex)
                        {
                            M_45_54_30 += 0;
                        }
                        try
                        {
                            M_55_64_30 += Int32.Parse(obj["value"]["M.55-64"].ToString());
                        }
                        catch (Exception ex)
                        {
                            M_55_64_30 += 0;
                        }
                        try
                        {
                            M_65_30 += Int32.Parse(obj["value"]["M.65+"].ToString());
                        }
                        catch (Exception ex)
                        {
                            M_65_30 += 0;
                        }

                    }
                    if (i > 74)
                    {

                        //female 15 days
                        try
                        {
                            F_13_17_15 += Int32.Parse(obj["value"]["F.13-17"].ToString());
                        }
                        catch (Exception ex)
                        {
                            F_13_17_15 += 0;
                        }
                        try
                        {
                            F_18_24_15 += Int32.Parse(obj["value"]["F.18-24"].ToString());
                        }
                        catch (Exception ex)
                        {
                            F_18_24_15 += 0;
                        }
                        try
                        {
                            F_25_34_15 += Int32.Parse(obj["value"]["F.25-34"].ToString());
                        }
                        catch (Exception ex)
                        {
                            F_25_34_15 += 0;
                        }
                        try
                        {
                            F_35_44_15 += Int32.Parse(obj["value"]["F.35-44"].ToString());
                        }
                        catch (Exception ex)
                        {
                            F_35_44_15 += 0;
                        }
                        try
                        {
                            F_45_54_15 += Int32.Parse(obj["value"]["F.45-54"].ToString());
                        }
                        catch (Exception ex)
                        {
                            F_45_54_15 += 0;
                        }
                        try
                        {
                            F_55_64_15 += Int32.Parse(obj["value"]["F.55-64"].ToString());
                        }
                        catch (Exception ex)
                        {
                            F_55_64_15 += 0;
                        }
                        try
                        {
                            F_65_15 += Int32.Parse(obj["value"]["F.65+"].ToString());
                        }
                        catch (Exception ex)
                        {
                            F_65_15 += 0;
                        }
                        //male 15 days
                        try
                        {
                            M_13_17_15 += Int32.Parse(obj["value"]["M.13-17"].ToString());
                        }
                        catch (Exception ex)
                        {
                            M_13_17_15 += 0;
                        }
                        try
                        {
                            M_18_24_15 += Int32.Parse(obj["value"]["M.18-24"].ToString());
                        }
                        catch (Exception ex)
                        {
                            M_18_24_15 += 0;
                        }
                        try
                        {
                            M_25_34_15 += Int32.Parse(obj["value"]["M.25-34"].ToString());
                        }
                        catch (Exception ex)
                        {
                            M_25_34_15 += 0;
                        }
                        try
                        {
                            M_35_44_15 += Int32.Parse(obj["value"]["M.35-44"].ToString());
                        }
                        catch (Exception ex)
                        {
                            M_35_44_15 += 0;
                        }
                        try
                        {
                            M_45_54_15 += Int32.Parse(obj["value"]["M.45-54"].ToString());
                        }
                        catch (Exception ex)
                        {
                            M_45_54_15 += 0;
                        }
                        try
                        {
                            M_55_64_15 += Int32.Parse(obj["value"]["M.55-64"].ToString());
                        }
                        catch (Exception ex)
                        {
                            M_55_64_15 += 0;
                        }
                        try
                        {
                            M_65_15 += Int32.Parse(obj["value"]["M.65+"].ToString());
                        }
                        catch (Exception ex)
                        {
                            M_65_15 += 0;
                        }

                    }
                    i++;
                }

                //Sharing
                i = 0;
                string facebookstories = "https://graph.facebook.com/v2.3/" + ProfileId + "/insights/page_stories?pretty=0&since=" + since.ToString() + "&suppress_http_code=1&until=" + until.ToString() + "&access_token=" + AccessToken;
                string outputfacestories = getFacebookResponse(facebookstories);
                JArray facebookstoriesobj = JArray.Parse(JArray.Parse(JObject.Parse(outputfacestories)["data"].ToString())[0]["values"].ToString());
                foreach (JObject obj in facebookstoriesobj)
                {
                    try
                    {
                        strstorysharing_90 += obj["value"].ToString() + ",";
                    }
                    catch (Exception ex)
                    {
                        strstorysharing_90 += "0,";
                    }
                    //Sharing 90 days
                    try
                    {
                        Sharing_90 += Int32.Parse(obj["value"].ToString());
                    }
                    catch (Exception ex)
                    {
                        Sharing_90 += 0;
                    }
                    //Sharing 60 days
                    if (i > 29)
                    {
                        try
                        {
                            strstorysharing_60 += obj["value"].ToString() + ",";
                        }
                        catch (Exception ex)
                        {
                            strstorysharing_60 += "0,";
                        }
                        try
                        {
                            Sharing_60 += Int32.Parse(obj["value"].ToString());
                        }
                        catch (Exception ex)
                        {
                            Sharing_60 += 0;
                        }
                    }
                    //Sharing 30 days
                    if (i > 59)
                    {
                        try
                        {
                            strstorysharing_30 += obj["value"].ToString() + ",";
                        }
                        catch (Exception ex)
                        {
                            strstorysharing_30 += "0,";
                        }
                        try
                        {
                            Sharing_30 += Int32.Parse(obj["value"].ToString());
                        }
                        catch (Exception ex)
                        {
                            Sharing_30 += 0;
                        }
                    }
                    //Sharing 15 days
                    if (i > 74)
                    {
                        try
                        {
                            strstorysharing_15 += obj["value"].ToString() + ",";
                        }
                        catch (Exception ex)
                        {
                            strstorysharing_15 += "0,";
                        }
                        try
                        {
                            Sharing_15 += Int32.Parse(obj["value"].ToString());
                        }
                        catch (Exception ex)
                        {
                            Sharing_15 += 0;
                        }
                    }
                    i++;
                }
                strstorysharing_90 = strstorysharing_90.TrimEnd(',');
                strstorysharing_60 = strstorysharing_60.TrimEnd(',');
                strstorysharing_30 = strstorysharing_30.TrimEnd(',');
                strstorysharing_15 = strstorysharing_15.TrimEnd(',');
                //Sharing Share Type
                i = 0;
                string facebooksharing_typeUrl = "https://graph.facebook.com/v2.3/" + ProfileId + "/insights/page_stories_by_story_type?pretty=0&since=" + since.ToString() + "&suppress_http_code=1&until=" + until.ToString() + "&access_token=" + AccessToken;
                string outputfaceunsharing_type = getFacebookResponse(facebooksharing_typeUrl);
                JArray facebooksharing_typeUrlobj = JArray.Parse(JArray.Parse(JObject.Parse(outputfaceunsharing_type)["data"].ToString())[0]["values"].ToString());
                foreach (JObject obj in facebooksharing_typeUrlobj)
                {
                    //Sharing Share Type 90 days
                    try
                    {
                        storyfans_90 += Int32.Parse(obj["value"]["fan"].ToString());
                    }
                    catch (Exception ex)
                    {
                        storyfans_90 += 0;
                    }
                    try
                    {
                        storypagepost_90 += Int32.Parse(obj["value"]["page post"].ToString());
                    }
                    catch (Exception ex)
                    {
                        storypagepost_90 += 0;
                    }
                    try
                    {
                        storyuserpost_90 += Int32.Parse(obj["value"]["user post"].ToString());
                    }
                    catch (Exception eex)
                    {
                        storyuserpost_90 += 0;
                    }
                    try
                    {
                        storycoupn_90 += Int32.Parse(obj["value"]["coupon"].ToString());
                    }
                    catch (Exception ex)
                    {
                        storycoupn_90 += 0;
                    }
                    try
                    {
                        storyother_90 += Int32.Parse(obj["value"]["other"].ToString());
                    }
                    catch (Exception ex)
                    {
                        storyother_90 += 0;
                    }
                    try
                    {
                        storymention_90 += Int32.Parse(obj["value"]["mention"].ToString());
                    }
                    catch (Exception ex)
                    {
                        storymention_90 += 0;
                    }
                    try
                    {
                        storycheckin_90 += Int32.Parse(obj["value"]["checkin"].ToString());
                    }
                    catch (Exception ex)
                    {
                        storycheckin_90 += 0;
                    }
                    try
                    {
                        storyquestion_90 += Int32.Parse(obj["value"]["question"].ToString());
                    }
                    catch (Exception ex)
                    {
                        storyquestion_90 += 0;
                    }
                    try
                    {
                        storyevent_90 += Int32.Parse(obj["value"]["event"].ToString());
                    }
                    catch (Exception ex)
                    {
                        storyevent_90 += 0;
                    }
                    //Sharing Share Type 60 days
                    if (i > 29)
                    {
                        try
                        {
                            storyfans_60 += Int32.Parse(obj["value"]["fan"].ToString());
                        }
                        catch (Exception ex)
                        {
                            storyfans_60 += 0;
                        }
                        try
                        {
                            storypagepost_60 += Int32.Parse(obj["value"]["page post"].ToString());
                        }
                        catch (Exception ex)
                        {
                            storypagepost_60 += 0;
                        }
                        try
                        {
                            storyuserpost_60 += Int32.Parse(obj["value"]["user post"].ToString());
                        }
                        catch (Exception ex)
                        {
                            storyuserpost_60 += 0;
                        }
                        try
                        {
                            storycoupn_60 += Int32.Parse(obj["value"]["coupon"].ToString());
                        }
                        catch (Exception ex)
                        {
                            storycoupn_60 += 0;
                        }
                        try
                        {
                            storyother_60 += Int32.Parse(obj["value"]["other"].ToString());
                        }
                        catch (Exception ex)
                        {
                            storyother_60 += 0;
                        }
                        try
                        {
                            storymention_60 += Int32.Parse(obj["value"]["mention"].ToString());
                        }
                        catch (Exception ex)
                        {
                            storymention_60 += 0;
                        }
                        try
                        {
                            storycheckin_60 += Int32.Parse(obj["value"]["checkin"].ToString());
                        }
                        catch (Exception ex)
                        {
                            storycheckin_60 += 0;
                        }
                        try
                        {
                            storyquestion_60 += Int32.Parse(obj["value"]["question"].ToString());
                        }
                        catch (Exception ex)
                        {
                            storyquestion_60 += 0;
                        }
                        try
                        {
                            storyevent_60 += Int32.Parse(obj["value"]["event"].ToString());
                        }
                        catch (Exception ex)
                        {
                            storyevent_60 += 0;
                        }
                    }
                    //Sharing Share Type 30 days
                    if (i > 59)
                    {
                        try
                        {
                            storyfans_30 += Int32.Parse(obj["value"]["fan"].ToString());
                        }
                        catch (Exception ex)
                        {
                            storyfans_30 += 0;
                        }
                        try
                        {
                            storypagepost_30 += Int32.Parse(obj["value"]["page post"].ToString());
                        }
                        catch (Exception ex)
                        {
                            storypagepost_30 += 0;
                        }
                        try
                        {
                            storyuserpost_30 += Int32.Parse(obj["value"]["user post"].ToString());
                        }
                        catch (Exception ex)
                        {
                            storyuserpost_30 += 0;
                        }
                        try
                        {
                            storycoupn_30 += Int32.Parse(obj["value"]["coupon"].ToString());
                        }
                        catch (Exception ex)
                        {
                            storycoupn_30 += 0;
                        }
                        try
                        {
                            storyother_30 += Int32.Parse(obj["value"]["other"].ToString());
                        }
                        catch (Exception ex)
                        {
                            storyother_30 += 0;
                        }
                        try
                        {
                            storymention_30 += Int32.Parse(obj["value"]["mention"].ToString());
                        }
                        catch (Exception ex)
                        {
                            storymention_30 += 0;
                        }
                        try
                        {
                            storycheckin_30 += Int32.Parse(obj["value"]["checkin"].ToString());
                        }
                        catch (Exception ex)
                        {
                            storycheckin_30 += 0;
                        }
                        try
                        {
                            storyquestion_30 += Int32.Parse(obj["value"]["question"].ToString());
                        }
                        catch (Exception ex)
                        {
                            storyquestion_30 += 0;
                        }
                        try
                        {
                            storyevent_30 += Int32.Parse(obj["value"]["event"].ToString());
                        }
                        catch (Exception ex)
                        {
                            storyevent_30 += 0;
                        }
                    }
                    //Sharing Share Type 15 days
                    if (i > 74)
                    {
                        try
                        {
                            storyfans_15 += Int32.Parse(obj["value"]["fan"].ToString());
                        }
                        catch (Exception ex)
                        {
                            storyfans_15 += 0;
                        }
                        try
                        {
                            storypagepost_15 += Int32.Parse(obj["value"]["page post"].ToString());
                        }
                        catch (Exception ex)
                        {
                            storypagepost_15 += 0;
                        }
                        try
                        {
                            storyuserpost_15 += Int32.Parse(obj["value"]["user post"].ToString());
                        }
                        catch (Exception ex)
                        {
                            storyuserpost_15 += 0;
                        }
                        try
                        {
                            storycoupn_15 += Int32.Parse(obj["value"]["coupon"].ToString());
                        }
                        catch (Exception ex)
                        {
                            storycoupn_15 += 0;
                        }
                        try
                        {
                            storyother_15 += Int32.Parse(obj["value"]["other"].ToString());
                        }
                        catch (Exception ex)
                        {
                            storyother_15 += 0;
                        }
                        try
                        {
                            storymention_15 += Int32.Parse(obj["value"]["mention"].ToString());
                        }
                        catch (Exception ex)
                        {
                            storymention_15 += 0;
                        }
                        try
                        {
                            storycheckin_15 += Int32.Parse(obj["value"]["checkin"].ToString());
                        }
                        catch (Exception ex)
                        {
                            storycheckin_15 += 0;
                        }
                        try
                        {
                            storyquestion_15 += Int32.Parse(obj["value"]["question"].ToString());
                        }
                        catch (Exception ex)
                        {
                            storyquestion_15 += 0;
                        }
                        try
                        {
                            storyevent_15 += Int32.Parse(obj["value"]["event"].ToString());
                        }
                        catch (Exception ex)
                        {
                            storyevent_15 += 0;
                        }
                    }
                    i++;
                }

                //Sharing by Age & Gender
                i = 0;
                string facebooksharingagegenderUrl = "https://graph.facebook.com/v2.3/" + ProfileId + "/insights/page_storytellers_by_age_gender?pretty=0&since=" + since.ToString() + "&suppress_http_code=1&until=" + until.ToString() + "&access_token=" + AccessToken;
                string outputfaceunagegender = getFacebookResponse(facebooksharingagegenderUrl);
                JArray facebookagegenderUrlobj = JArray.Parse(JArray.Parse(JObject.Parse(outputfaceunagegender)["data"].ToString())[0]["values"].ToString());
                foreach (JObject obj in facebookagegenderUrlobj)
                {
                    //female 90 days
                    try
                    {
                        Sharing_F_13_17_90 += Int32.Parse(obj["value"]["F.13-17"].ToString());
                    }
                    catch (Exception ex)
                    {
                        Sharing_F_13_17_90 += 0;
                    }
                    try
                    {
                        Sharing_F_18_24_90 += Int32.Parse(obj["value"]["F.18-24"].ToString());
                    }
                    catch (Exception ex)
                    {
                        Sharing_F_18_24_90 += 0;
                    }
                    try
                    {
                        Sharing_F_25_34_90 += Int32.Parse(obj["value"]["F.25-34"].ToString());
                    }
                    catch (Exception ex)
                    {
                        Sharing_F_25_34_90 += 0;
                    }
                    try
                    {
                        Sharing_F_35_44_90 += Int32.Parse(obj["value"]["F.35-44"].ToString());
                    }
                    catch (Exception ex)
                    {
                        Sharing_F_35_44_90 += 0;
                    }
                    try
                    {
                        Sharing_F_45_54_90 += Int32.Parse(obj["value"]["F.45-54"].ToString());
                    }
                    catch (Exception ex)
                    {
                        Sharing_F_45_54_90 += 0;
                    }
                    try
                    {
                        Sharing_F_55_64_90 += Int32.Parse(obj["value"]["F.55-64"].ToString());
                    }
                    catch (Exception ex)
                    {
                        Sharing_F_55_64_90 += 0;
                    }
                    try
                    {
                        Sharing_F_65_90 += Int32.Parse(obj["value"]["F.65+"].ToString());
                    }
                    catch (Exception ex)
                    {
                        Sharing_F_65_90 += 0;
                    }
                    //male 90 days
                    try
                    {
                        Sharing_M_13_17_90 += Int32.Parse(obj["value"]["M.13-17"].ToString());
                    }
                    catch (Exception ex)
                    {
                        Sharing_M_13_17_90 += 0;
                    }
                    try
                    {
                        Sharing_M_18_24_90 += Int32.Parse(obj["value"]["M.18-24"].ToString());
                    }
                    catch (Exception ex)
                    {
                        Sharing_M_18_24_90 += 0;
                    }
                    try
                    {
                        Sharing_M_25_34_90 += Int32.Parse(obj["value"]["M.25-34"].ToString());
                    }
                    catch (Exception ex)
                    {
                        Sharing_M_25_34_90 += 0;
                    }
                    try
                    {
                        Sharing_M_35_44_90 += Int32.Parse(obj["value"]["M.35-44"].ToString());
                    }
                    catch (Exception ex)
                    {
                        Sharing_M_35_44_90 += 0;
                    }
                    try
                    {
                        Sharing_M_45_54_90 += Int32.Parse(obj["value"]["M.45-54"].ToString());
                    }
                    catch (Exception ex)
                    {
                        Sharing_M_45_54_90 += 0;
                    }
                    try
                    {
                        Sharing_M_55_64_90 += Int32.Parse(obj["value"]["M.55-64"].ToString());
                    }
                    catch (Exception ex)
                    {
                        Sharing_M_55_64_90 += 0;
                    }
                    try
                    {
                        Sharing_M_65_90 += Int32.Parse(obj["value"]["M.65+"].ToString());
                    }
                    catch (Exception ex)
                    {
                        Sharing_M_65_90 += 0;
                    }
                    if (i > 29)
                    {
                        //female 60 days
                        try
                        {
                            Sharing_F_13_17_60 += Int32.Parse(obj["value"]["F.13-17"].ToString());
                        }
                        catch (Exception ex)
                        {
                            Sharing_F_13_17_60 += 0;
                        }
                        try
                        {
                            Sharing_F_18_24_60 += Int32.Parse(obj["value"]["F.18-24"].ToString());
                        }
                        catch (Exception ex)
                        {
                            Sharing_F_18_24_60 += 0;
                        }
                        try
                        {
                            Sharing_F_25_34_60 += Int32.Parse(obj["value"]["F.25-34"].ToString());
                        }
                        catch (Exception ex)
                        {
                            Sharing_F_25_34_60 += 0;
                        }
                        try
                        {
                            Sharing_F_35_44_60 += Int32.Parse(obj["value"]["F.35-44"].ToString());
                        }
                        catch (Exception ex)
                        {
                            Sharing_F_35_44_60 += 0;
                        }
                        try
                        {
                            Sharing_F_45_54_60 += Int32.Parse(obj["value"]["F.45-54"].ToString());
                        }
                        catch (Exception ex)
                        {
                            Sharing_F_45_54_60 += 0;
                        }
                        try
                        {
                            Sharing_F_55_64_60 += Int32.Parse(obj["value"]["F.55-64"].ToString());
                        }
                        catch (Exception ex)
                        {
                            Sharing_F_55_64_60 += 0;
                        }
                        try
                        {
                            Sharing_F_65_60 += Int32.Parse(obj["value"]["F.65+"].ToString());
                        }
                        catch (Exception ex)
                        {
                            Sharing_F_65_60 += 0;
                        }
                        //male 60 days
                        try
                        {
                            Sharing_M_13_17_60 += Int32.Parse(obj["value"]["M.13-17"].ToString());
                        }
                        catch (Exception ex)
                        {
                            Sharing_M_13_17_60 += 0;
                        }
                        try
                        {
                            Sharing_M_18_24_60 += Int32.Parse(obj["value"]["M.18-24"].ToString());
                        }
                        catch (Exception ex)
                        {
                            Sharing_M_18_24_60 += 0;
                        }
                        try
                        {
                            Sharing_M_25_34_60 += Int32.Parse(obj["value"]["M.25-34"].ToString());
                        }
                        catch (Exception ex)
                        {
                            Sharing_M_25_34_60 += 0;
                        }
                        try
                        {
                            Sharing_M_35_44_60 += Int32.Parse(obj["value"]["M.35-44"].ToString());
                        }
                        catch (Exception ex)
                        {
                            Sharing_M_35_44_60 += 0;
                        }
                        try
                        {
                            Sharing_M_45_54_60 += Int32.Parse(obj["value"]["M.45-54"].ToString());
                        }
                        catch (Exception ex)
                        {
                            Sharing_M_45_54_60 += 0;
                        }
                        try
                        {
                            Sharing_M_55_64_60 += Int32.Parse(obj["value"]["M.55-64"].ToString());
                        }
                        catch (Exception ex)
                        {
                            Sharing_M_55_64_60 += 0;
                        }
                        try
                        {
                            Sharing_M_65_60 += Int32.Parse(obj["value"]["M.65+"].ToString());
                        }
                        catch (Exception ex)
                        {
                            Sharing_M_65_60 += 0;
                        }
                    }
                    if (i > 59)
                    {
                        //female 30 days
                        try
                        {
                            Sharing_F_13_17_30 += Int32.Parse(obj["value"]["F.13-17"].ToString());
                        }
                        catch (Exception ex)
                        {
                            Sharing_F_13_17_30 += 0;
                        }
                        try
                        {
                            Sharing_F_18_24_30 += Int32.Parse(obj["value"]["F.18-24"].ToString());
                        }
                        catch (Exception ex)
                        {
                            Sharing_F_18_24_30 += 0;
                        }
                        try
                        {
                            Sharing_F_25_34_30 += Int32.Parse(obj["value"]["F.25-34"].ToString());
                        }
                        catch (Exception ex)
                        {
                            Sharing_F_25_34_30 += 0;
                        }
                        try
                        {
                            Sharing_F_35_44_30 += Int32.Parse(obj["value"]["F.35-44"].ToString());
                        }
                        catch (Exception ex)
                        {
                            Sharing_F_35_44_30 += 0;
                        }
                        try
                        {
                            Sharing_F_45_54_30 += Int32.Parse(obj["value"]["F.45-54"].ToString());
                        }
                        catch (Exception ex)
                        {
                            Sharing_F_45_54_30 += 0;
                        }
                        try
                        {
                            Sharing_F_55_64_30 += Int32.Parse(obj["value"]["F.55-64"].ToString());
                        }
                        catch (Exception ex)
                        {
                            Sharing_F_55_64_30 += 0;
                        }
                        try
                        {
                            Sharing_F_65_30 += Int32.Parse(obj["value"]["F.65+"].ToString());
                        }
                        catch (Exception ex)
                        {
                            Sharing_F_65_30 += 0;
                        }
                        //male 30 days
                        try
                        {
                            Sharing_M_13_17_30 += Int32.Parse(obj["value"]["M.13-17"].ToString());
                        }
                        catch (Exception ex)
                        {
                            Sharing_M_13_17_30 += 0;
                        }
                        try
                        {
                            Sharing_M_18_24_30 += Int32.Parse(obj["value"]["M.18-24"].ToString());
                        }
                        catch (Exception ex)
                        {
                            Sharing_M_18_24_30 += 0;
                        }
                        try
                        {
                            Sharing_M_25_34_30 += Int32.Parse(obj["value"]["M.25-34"].ToString());
                        }
                        catch (Exception ex)
                        {
                            Sharing_M_25_34_30 += 0;
                        }
                        try
                        {
                            Sharing_M_35_44_30 += Int32.Parse(obj["value"]["M.35-44"].ToString());
                        }
                        catch (Exception ex)
                        {
                            Sharing_M_35_44_30 += 0;
                        }
                        try
                        {
                            Sharing_M_45_54_30 += Int32.Parse(obj["value"]["M.45-54"].ToString());
                        }
                        catch (Exception ex)
                        {
                            Sharing_M_45_54_30 += 0;
                        }
                        try
                        {
                            Sharing_M_55_64_30 += Int32.Parse(obj["value"]["M.55-64"].ToString());
                        }
                        catch (Exception ex)
                        {
                            Sharing_M_55_64_30 += 0;
                        }
                        try
                        {
                            Sharing_M_65_30 += Int32.Parse(obj["value"]["M.65+"].ToString());
                        }
                        catch (Exception ex)
                        {
                            Sharing_M_65_30 += 0;
                        }
                    }
                    if (i > 74)
                    {
                        //female 15 days
                        try
                        {
                            Sharing_F_13_17_15 += Int32.Parse(obj["value"]["F.13-17"].ToString());
                        }
                        catch (Exception ex)
                        {
                            Sharing_F_13_17_15 += 0;
                        }
                        try
                        {
                            Sharing_F_18_24_15 += Int32.Parse(obj["value"]["F.18-24"].ToString());
                        }
                        catch (Exception ex)
                        {
                            Sharing_F_18_24_15 += 0;
                        }
                        try
                        {
                            Sharing_F_25_34_15 += Int32.Parse(obj["value"]["F.25-34"].ToString());
                        }
                        catch (Exception ex)
                        {
                            Sharing_F_25_34_15 += 0;
                        }
                        try
                        {
                            Sharing_F_35_44_15 += Int32.Parse(obj["value"]["F.35-44"].ToString());
                        }
                        catch (Exception ex)
                        {
                            Sharing_F_35_44_15 += 0;
                        }
                        try
                        {
                            Sharing_F_45_54_15 += Int32.Parse(obj["value"]["F.45-54"].ToString());
                        }
                        catch (Exception ex)
                        {
                            Sharing_F_45_54_15 += 0;
                        }
                        try
                        {
                            Sharing_F_55_64_15 += Int32.Parse(obj["value"]["F.55-64"].ToString());
                        }
                        catch (Exception ex)
                        {
                            Sharing_F_55_64_15 += 0;
                        }
                        try
                        {
                            Sharing_F_65_15 += Int32.Parse(obj["value"]["F.65+"].ToString());
                        }
                        catch (Exception ex)
                        {
                            Sharing_F_65_15 += 0;
                        }
                        //male 15 days
                        try
                        {
                            Sharing_M_13_17_15 += Int32.Parse(obj["value"]["M.13-17"].ToString());
                        }
                        catch (Exception ex)
                        {
                            Sharing_M_13_17_15 += 0;
                        }
                        try
                        {
                            Sharing_M_18_24_15 += Int32.Parse(obj["value"]["M.18-24"].ToString());
                        }
                        catch (Exception ex)
                        {
                            Sharing_M_18_24_15 += 0;
                        }
                        try
                        {
                            Sharing_M_25_34_15 += Int32.Parse(obj["value"]["M.25-34"].ToString());
                        }
                        catch (Exception ex)
                        {
                            Sharing_M_25_34_15 += 0;
                        }
                        try
                        {
                            Sharing_M_35_44_15 += Int32.Parse(obj["value"]["M.35-44"].ToString());
                        }
                        catch (Exception ex)
                        {
                            Sharing_M_35_44_15 += 0;
                        }
                        try
                        {
                            Sharing_M_45_54_15 += Int32.Parse(obj["value"]["M.45-54"].ToString());
                        }
                        catch (Exception ex)
                        {
                            Sharing_M_45_54_15 += 0;
                        }
                        try
                        {
                            Sharing_M_55_64_15 += Int32.Parse(obj["value"]["M.55-64"].ToString());
                        }
                        catch (Exception ex)
                        {
                            Sharing_M_55_64_15 += 0;
                        }
                        try
                        {
                            Sharing_M_65_15 += Int32.Parse(obj["value"]["M.65+"].ToString());
                        }
                        catch (Exception ex)
                        {
                            Sharing_M_65_15 += 0;
                        }
                    }
                    i++;
                }

            }
            catch (Exception ex)
            {
                ret = ex.Message;
            }
            Domain.Socioboard.Domain.FacebookReport_15 _FacebookReport_15 = new FacebookReport_15();
            Domain.Socioboard.Domain.FacebookReport_30 _FacebookReport_30 = new FacebookReport_30();
            Domain.Socioboard.Domain.FacebookReport_60 _FacebookReport_60 = new FacebookReport_60();
            Domain.Socioboard.Domain.FacebookReport_90 _FacebookReport_90 = new FacebookReport_90();
            FacebookReportRepository _FacebookReportRepository = new FacebookReportRepository();
            _FacebookReport_15.Id = Guid.NewGuid();
            _FacebookReport_15.FacebookId = ProfileId;
            _FacebookReport_15.TotalLikes = TotalPageLike;
            _FacebookReport_15.TalkingAbout = TalkinAbout;
            _FacebookReport_15.Likes = Newfans_15;
            _FacebookReport_15.PerDayLikes = strlikes_15;
            _FacebookReport_15.Unlikes = Unlike_15;
            _FacebookReport_15.PerDayUnlikes = strunlikes_15;
            _FacebookReport_15.Impression = Impression_15;
            _FacebookReport_15.PerDayImpression = strimpression_15;
            _FacebookReport_15.UniqueUser = UniqueUser_15;
            _FacebookReport_15.StoryShare = Sharing_15;
            _FacebookReport_15.PerDayStoryShare = strstorysharing_15;
            _FacebookReport_15.ImpressionFans = impressionfans_15;
            _FacebookReport_15.ImpressionPagePost = impressionpagepost_15;
            _FacebookReport_15.ImpressionuserPost = impressionuserpost_15;
            _FacebookReport_15.ImpressionCoupn = impressioncoupn_15;
            _FacebookReport_15.ImpressionOther = impressionother_15;
            _FacebookReport_15.ImpressionMention = impressionmention_15;
            _FacebookReport_15.ImpressionCheckin = impressioncheckin_15;
            _FacebookReport_15.ImpressionQuestion = impressionquestion_15;
            _FacebookReport_15.ImpressionEvent = impressionevent_15;
            _FacebookReport_15.Organic = organic_15;
            _FacebookReport_15.Viral = viral_15;
            _FacebookReport_15.Paid = paid_15;
            _FacebookReport_15.M_13_17 = M_13_17_15;
            _FacebookReport_15.M_18_24 = M_18_24_15;
            _FacebookReport_15.M_25_34 = M_25_34_15;
            _FacebookReport_15.M_35_44 = M_35_44_15;
            _FacebookReport_15.M_45_54 = M_45_54_15;
            _FacebookReport_15.M_55_64 = M_55_64_15;
            _FacebookReport_15.M_65 = M_65_15;
            _FacebookReport_15.F_13_17 = F_13_17_15;
            _FacebookReport_15.F_18_24 = F_18_24_15;
            _FacebookReport_15.F_25_34 = F_25_34_15;
            _FacebookReport_15.F_35_44 = F_35_44_15;
            _FacebookReport_15.F_45_54 = F_45_54_15;
            _FacebookReport_15.F_55_64 = F_55_64_15;
            _FacebookReport_15.F_65 = F_65_15;
            _FacebookReport_15.Sharing_M_13_17 = Sharing_M_13_17_15;
            _FacebookReport_15.Sharing_M_18_24 = Sharing_M_18_24_15;
            _FacebookReport_15.Sharing_M_25_34 = Sharing_M_25_34_15;
            _FacebookReport_15.Sharing_M_35_44 = Sharing_M_35_44_15;
            _FacebookReport_15.Sharing_M_45_54 = Sharing_M_45_54_15;
            _FacebookReport_15.Sharing_M_55_64 = Sharing_M_55_64_15;
            _FacebookReport_15.Sharing_M_65 = Sharing_M_65_15;
            _FacebookReport_15.Sharing_F_13_17 = Sharing_F_13_17_15;
            _FacebookReport_15.Sharing_F_18_24 = Sharing_F_18_24_15;
            _FacebookReport_15.Sharing_F_25_34 = Sharing_F_25_34_15;
            _FacebookReport_15.Sharing_F_35_44 = Sharing_F_35_44_15;
            _FacebookReport_15.Sharing_F_45_54 = Sharing_F_45_54_15;
            _FacebookReport_15.Sharing_F_55_64 = Sharing_F_55_64_15;
            _FacebookReport_15.Sharing_F_65 = Sharing_F_65_15;
            _FacebookReport_15.Story_Fans = storyfans_15;
            _FacebookReport_15.Story_PagePost = storypagepost_15;
            _FacebookReport_15.Story_Other = storyother_15;
            _FacebookReport_15.Story_Checkin = storycheckin_15;
            _FacebookReport_15.Story_Coupon = storycoupn_15;
            _FacebookReport_15.Story_Event = storyevent_15;
            _FacebookReport_15.Story_Mention = storymention_15;
            _FacebookReport_15.Story_Question = storyother_15;
            _FacebookReport_15.Story_UserPost = storyuserpost_15;

            if (!_FacebookReportRepository.IsReport_15Exists(ProfileId))
            {
                _FacebookReportRepository.AddfacebookReport_15(_FacebookReport_15);
            }
            else
            {
                int ret15 = _FacebookReportRepository.UpdatefacebookReport_15(_FacebookReport_15);
            }
            _FacebookReport_30.Id = Guid.NewGuid();
            _FacebookReport_30.FacebookId = ProfileId;
            _FacebookReport_30.TotalLikes = TotalPageLike;
            _FacebookReport_30.TalkingAbout = TalkinAbout;
            _FacebookReport_30.Likes = Newfans_30;
            _FacebookReport_30.PerDayLikes = strlikes_30;
            _FacebookReport_30.Unlikes = Unlike_30;
            _FacebookReport_30.PerDayUnlikes = strunlikes_30;
            _FacebookReport_30.Impression = Impression_30;
            _FacebookReport_30.PerDayImpression = strimpression_30;
            _FacebookReport_30.UniqueUser = UniqueUser_30;
            _FacebookReport_30.StoryShare = Sharing_30;
            _FacebookReport_30.PerDayStoryShare = strstorysharing_30;
            _FacebookReport_30.ImpressionFans = impressionfans_30;
            _FacebookReport_30.ImpressionPagePost = impressionpagepost_30;
            _FacebookReport_30.ImpressionuserPost = impressionuserpost_30;
            _FacebookReport_30.ImpressionCoupn = impressioncoupn_30;
            _FacebookReport_30.ImpressionOther = impressionother_30;
            _FacebookReport_30.ImpressionMention = impressionmention_30;
            _FacebookReport_30.ImpressionCheckin = impressioncheckin_30;
            _FacebookReport_30.ImpressionQuestion = impressionquestion_30;
            _FacebookReport_30.ImpressionEvent = impressionevent_30;
            _FacebookReport_30.Organic = organic_30;
            _FacebookReport_30.Viral = viral_30;
            _FacebookReport_30.Paid = paid_30;
            _FacebookReport_30.M_13_17 = M_13_17_30;
            _FacebookReport_30.M_18_24 = M_18_24_30;
            _FacebookReport_30.M_25_34 = M_25_34_30;
            _FacebookReport_30.M_35_44 = M_35_44_30;
            _FacebookReport_30.M_45_54 = M_45_54_30;
            _FacebookReport_30.M_55_64 = M_55_64_30;
            _FacebookReport_30.M_65 = M_65_30;
            _FacebookReport_30.F_13_17 = F_13_17_30;
            _FacebookReport_30.F_18_24 = F_18_24_30;
            _FacebookReport_30.F_25_34 = F_25_34_30;
            _FacebookReport_30.F_35_44 = F_35_44_30;
            _FacebookReport_30.F_45_54 = F_45_54_30;
            _FacebookReport_30.F_55_64 = F_55_64_30;
            _FacebookReport_30.F_65 = F_65_30;
            _FacebookReport_30.Sharing_M_13_17 = Sharing_M_13_17_30;
            _FacebookReport_30.Sharing_M_18_24 = Sharing_M_18_24_30;
            _FacebookReport_30.Sharing_M_25_34 = Sharing_M_25_34_30;
            _FacebookReport_30.Sharing_M_35_44 = Sharing_M_35_44_30;
            _FacebookReport_30.Sharing_M_45_54 = Sharing_M_45_54_30;
            _FacebookReport_30.Sharing_M_55_64 = Sharing_M_55_64_30;
            _FacebookReport_30.Sharing_M_65 = Sharing_M_65_30;
            _FacebookReport_30.Sharing_F_13_17 = Sharing_F_13_17_30;
            _FacebookReport_30.Sharing_F_18_24 = Sharing_F_18_24_30;
            _FacebookReport_30.Sharing_F_25_34 = Sharing_F_25_34_30;
            _FacebookReport_30.Sharing_F_35_44 = Sharing_F_35_44_30;
            _FacebookReport_30.Sharing_F_45_54 = Sharing_F_45_54_30;
            _FacebookReport_30.Sharing_F_55_64 = Sharing_F_55_64_30;
            _FacebookReport_30.Sharing_F_65 = Sharing_F_65_30;
            _FacebookReport_30.Story_Fans = storyfans_30;
            _FacebookReport_30.Story_PagePost = storypagepost_30;
            _FacebookReport_30.Story_Other = storyother_30;
            _FacebookReport_30.Story_Checkin = storycheckin_30;
            _FacebookReport_30.Story_Coupon = storycoupn_30;
            _FacebookReport_30.Story_Event = storyevent_30;
            _FacebookReport_30.Story_Mention = storymention_30;
            _FacebookReport_30.Story_Question = storyother_30;
            _FacebookReport_30.Story_UserPost = storyuserpost_30;
            if (!_FacebookReportRepository.IsReport_30Exists(ProfileId))
            {
                _FacebookReportRepository.AddfacebookReport_30(_FacebookReport_30);
            }
            else
            {
                int ret30 = _FacebookReportRepository.UpdatefacebookReport_30(_FacebookReport_30);
            }
            _FacebookReport_60.Id = Guid.NewGuid();
            _FacebookReport_60.FacebookId = ProfileId;
            _FacebookReport_60.TotalLikes = TotalPageLike;
            _FacebookReport_60.TalkingAbout = TalkinAbout;
            _FacebookReport_60.Likes = Newfans_60;
            _FacebookReport_60.PerDayLikes = strlikes_60;
            _FacebookReport_60.Unlikes = Unlike_60;
            _FacebookReport_60.PerDayUnlikes = strunlikes_60;
            _FacebookReport_60.Impression = Impression_60;
            _FacebookReport_60.PerDayImpression = strimpression_60;
            _FacebookReport_60.UniqueUser = UniqueUser_60;
            _FacebookReport_60.StoryShare = Sharing_60;
            _FacebookReport_60.PerDayStoryShare = strstorysharing_60;
            _FacebookReport_60.ImpressionFans = impressionfans_60;
            _FacebookReport_60.ImpressionPagePost = impressionpagepost_60;
            _FacebookReport_60.ImpressionuserPost = impressionuserpost_60;
            _FacebookReport_60.ImpressionCoupn = impressioncoupn_60;
            _FacebookReport_60.ImpressionOther = impressionother_60;
            _FacebookReport_60.ImpressionMention = impressionmention_60;
            _FacebookReport_60.ImpressionCheckin = impressioncheckin_60;
            _FacebookReport_60.ImpressionQuestion = impressionquestion_60;
            _FacebookReport_60.ImpressionEvent = impressionevent_60;
            _FacebookReport_60.Organic = organic_60;
            _FacebookReport_60.Viral = viral_60;
            _FacebookReport_60.Paid = paid_60;
            _FacebookReport_60.M_13_17 = M_13_17_60;
            _FacebookReport_60.M_18_24 = M_18_24_60;
            _FacebookReport_60.M_25_34 = M_25_34_60;
            _FacebookReport_60.M_35_44 = M_35_44_60;
            _FacebookReport_60.M_45_54 = M_45_54_60;
            _FacebookReport_60.M_55_64 = M_55_64_60;
            _FacebookReport_60.M_65 = M_65_60;
            _FacebookReport_60.F_13_17 = F_13_17_60;
            _FacebookReport_60.F_18_24 = F_18_24_60;
            _FacebookReport_60.F_25_34 = F_25_34_60;
            _FacebookReport_60.F_35_44 = F_35_44_60;
            _FacebookReport_60.F_45_54 = F_45_54_60;
            _FacebookReport_60.F_55_64 = F_55_64_60;
            _FacebookReport_60.F_65 = F_65_60;
            _FacebookReport_60.Sharing_M_13_17 = Sharing_M_13_17_60;
            _FacebookReport_60.Sharing_M_18_24 = Sharing_M_18_24_60;
            _FacebookReport_60.Sharing_M_25_34 = Sharing_M_25_34_60;
            _FacebookReport_60.Sharing_M_35_44 = Sharing_M_35_44_60;
            _FacebookReport_60.Sharing_M_45_54 = Sharing_M_45_54_60;
            _FacebookReport_60.Sharing_M_55_64 = Sharing_M_55_64_60;
            _FacebookReport_60.Sharing_M_65 = Sharing_M_65_60;
            _FacebookReport_60.Sharing_F_13_17 = Sharing_F_13_17_60;
            _FacebookReport_60.Sharing_F_18_24 = Sharing_F_18_24_60;
            _FacebookReport_60.Sharing_F_25_34 = Sharing_F_25_34_60;
            _FacebookReport_60.Sharing_F_35_44 = Sharing_F_35_44_60;
            _FacebookReport_60.Sharing_F_45_54 = Sharing_F_45_54_60;
            _FacebookReport_60.Sharing_F_55_64 = Sharing_F_55_64_60;
            _FacebookReport_60.Sharing_F_65 = Sharing_F_65_60;
            _FacebookReport_60.Story_Fans = storyfans_60;
            _FacebookReport_60.Story_PagePost = storypagepost_60;
            _FacebookReport_60.Story_Other = storyother_60;
            _FacebookReport_60.Story_Checkin = storycheckin_60;
            _FacebookReport_60.Story_Coupon = storycoupn_60;
            _FacebookReport_60.Story_Event = storyevent_60;
            _FacebookReport_60.Story_Mention = storymention_60;
            _FacebookReport_60.Story_Question = storyother_60;
            _FacebookReport_60.Story_UserPost = storyuserpost_60;

            if (!_FacebookReportRepository.IsReport_60Exists(ProfileId))
            {
                _FacebookReportRepository.AddfacebookReport_60(_FacebookReport_60);
            }
            else
            {
                int ret60 = _FacebookReportRepository.UpdatefacebookReport_60(_FacebookReport_60);
            }
            _FacebookReport_90.Id = Guid.NewGuid();
            _FacebookReport_90.FacebookId = ProfileId;
            _FacebookReport_90.TotalLikes = TotalPageLike;
            _FacebookReport_90.TalkingAbout = TalkinAbout;
            _FacebookReport_90.Likes = Newfans_90;
            _FacebookReport_90.PerDayLikes = strlikes_90;
            _FacebookReport_90.Unlikes = Unlike_90;
            _FacebookReport_90.PerDayUnlikes = strunlikes_90;
            _FacebookReport_90.Impression = Impression_90;
            _FacebookReport_90.PerDayImpression = strimpression_90;
            _FacebookReport_90.UniqueUser = UniqueUser_90;
            _FacebookReport_90.StoryShare = Sharing_90;
            _FacebookReport_90.PerDayStoryShare = strstorysharing_90;
            _FacebookReport_90.ImpressionFans = impressionfans_90;
            _FacebookReport_90.ImpressionPagePost = impressionpagepost_90;
            _FacebookReport_90.ImpressionuserPost = impressionuserpost_90;
            _FacebookReport_90.ImpressionCoupn = impressioncoupn_90;
            _FacebookReport_90.ImpressionOther = impressionother_90;
            _FacebookReport_90.ImpressionMention = impressionmention_90;
            _FacebookReport_90.ImpressionCheckin = impressioncheckin_90;
            _FacebookReport_90.ImpressionQuestion = impressionquestion_90;
            _FacebookReport_90.ImpressionEvent = impressionevent_90;
            _FacebookReport_90.Organic = organic_90;
            _FacebookReport_90.Viral = viral_90;
            _FacebookReport_90.Paid = paid_90;
            _FacebookReport_90.M_13_17 = M_13_17_90;
            _FacebookReport_90.M_18_24 = M_18_24_90;
            _FacebookReport_90.M_25_34 = M_25_34_90;
            _FacebookReport_90.M_35_44 = M_35_44_90;
            _FacebookReport_90.M_45_54 = M_45_54_90;
            _FacebookReport_90.M_55_64 = M_55_64_90;
            _FacebookReport_90.M_65 = M_65_90;
            _FacebookReport_90.F_13_17 = F_13_17_90;
            _FacebookReport_90.F_18_24 = F_18_24_90;
            _FacebookReport_90.F_25_34 = F_25_34_90;
            _FacebookReport_90.F_35_44 = F_35_44_90;
            _FacebookReport_90.F_45_54 = F_45_54_90;
            _FacebookReport_90.F_55_64 = F_55_64_90;
            _FacebookReport_90.F_65 = F_65_90;
            _FacebookReport_90.Sharing_M_13_17 = Sharing_M_13_17_90;
            _FacebookReport_90.Sharing_M_18_24 = Sharing_M_18_24_90;
            _FacebookReport_90.Sharing_M_25_34 = Sharing_M_25_34_90;
            _FacebookReport_90.Sharing_M_35_44 = Sharing_M_35_44_90;
            _FacebookReport_90.Sharing_M_45_54 = Sharing_M_45_54_90;
            _FacebookReport_90.Sharing_M_55_64 = Sharing_M_55_64_90;
            _FacebookReport_90.Sharing_M_65 = Sharing_M_65_90;
            _FacebookReport_90.Sharing_F_13_17 = Sharing_F_13_17_90;
            _FacebookReport_90.Sharing_F_18_24 = Sharing_F_18_24_90;
            _FacebookReport_90.Sharing_F_25_34 = Sharing_F_25_34_90;
            _FacebookReport_90.Sharing_F_35_44 = Sharing_F_35_44_90;
            _FacebookReport_90.Sharing_F_45_54 = Sharing_F_45_54_90;
            _FacebookReport_90.Sharing_F_55_64 = Sharing_F_55_64_90;
            _FacebookReport_90.Sharing_F_65 = Sharing_F_65_90;
            _FacebookReport_90.Story_Fans = storyfans_90;
            _FacebookReport_90.Story_PagePost = storypagepost_90;
            _FacebookReport_90.Story_Other = storyother_90;
            _FacebookReport_90.Story_Checkin = storycheckin_90;
            _FacebookReport_90.Story_Coupon = storycoupn_90;
            _FacebookReport_90.Story_Event = storyevent_90;
            _FacebookReport_90.Story_Mention = storymention_90;
            _FacebookReport_90.Story_Question = storyother_90;
            _FacebookReport_90.Story_UserPost = storyuserpost_90;
            if (!_FacebookReportRepository.IsReport_90Exists(ProfileId))
            {
                _FacebookReportRepository.AddfacebookReport_90(_FacebookReport_90);
            }
            else
            {
                int ret90 = _FacebookReportRepository.UpdatefacebookReport_90(_FacebookReport_90);
            }
            //    }
            //}
            return "Facbook report Updated successfuly " + ret;
        }

        public static string getFacebookResponse(string Url)
        {
            var facebooklistpagerequest = (HttpWebRequest)WebRequest.Create(Url);
            facebooklistpagerequest.Method = "GET";
            facebooklistpagerequest.Credentials = CredentialCache.DefaultCredentials;
            facebooklistpagerequest.AllowWriteStreamBuffering = true;
            facebooklistpagerequest.ServicePoint.Expect100Continue = false;
            facebooklistpagerequest.PreAuthenticate = false;
            string outputface = string.Empty;
            try
            {
                using (var response = facebooklistpagerequest.GetResponse())
                {
                    using (var stream = new StreamReader(response.GetResponseStream(), Encoding.GetEncoding(1252)))
                    {
                        outputface = stream.ReadToEnd();
                    }
                }
            }
            catch (Exception e) { }
            return outputface;
        }



        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]

        public string GetFb_15(string profileid)
        {

            Domain.Socioboard.Domain.FacebookReport_15 fbreport = objFbReportRepository.GetFacebookReport_15(profileid);
            return new JavaScriptSerializer().Serialize(fbreport);


        }


        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]

        public string GetFb_30(string profileid)
        {

            Domain.Socioboard.Domain.FacebookReport_30 fbreport = objFbReportRepository.GetFacebookReport_30(profileid);
            return new JavaScriptSerializer().Serialize(fbreport);


        }


        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]

        public string GetFb_60(string profileid)
        {

            Domain.Socioboard.Domain.FacebookReport_60 fbreport = objFbReportRepository.GetFacebookReport_60(profileid);
            return new JavaScriptSerializer().Serialize(fbreport);


        }



        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]

        public string GetFb_90(string profileid)
        {

            Domain.Socioboard.Domain.FacebookReport_90 fbreport = objFbReportRepository.GetFacebookReport_90(profileid);
            return new JavaScriptSerializer().Serialize(fbreport);


        }





    }
}

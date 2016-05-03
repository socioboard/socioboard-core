using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using Api.Socioboard.Helper;

using System.Web.Script.Serialization;
using System.Web.Script.Services;
using NHibernate.Linq;
using NHibernate.Criterion;
using log4net;
using System.Configuration;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Text;
using System.Web.Script.Serialization;
using System.Xml;
using GlobusInstagramLib.Authentication;
using GlobusLinkedinLib.Authentication;
using GlobusTwitterLib.Authentication;
using log4net;
using Newtonsoft.Json.Linq;
using Facebook;
using Api.Socioboard.Helper;
using Api.Socioboard.Model;

namespace Api.Socioboard.Services
{
    /// <summary>
    /// Summary description for FacebookGroupReport
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class FacebookGroupReport : System.Web.Services.WebService
    {
        FacebookAccountRepository _FacebookAccountRepository = new FacebookAccountRepository();

        [WebMethod]
        public void FacebookGroupData(string groupid , string userid)
        {

            List<Domain.Socioboard.Domain.Team> teams;
            List<Domain.Socioboard.Domain.GroupProfile> teammemberprofiles;
            Domain.Socioboard.Domain.FacebookGroupReport_15 facebook_15 = new Domain.Socioboard.Domain.FacebookGroupReport_15();
            Domain.Socioboard.Domain.FacebookGroupReport_30 facebook_30 = new Domain.Socioboard.Domain.FacebookGroupReport_30();
            Domain.Socioboard.Domain.FacebookGroupReport_60 facebook_60 = new Domain.Socioboard.Domain.FacebookGroupReport_60();
             Domain.Socioboard.Domain.FacebookGroupReport_90 facebook_90 = new Domain.Socioboard.Domain.FacebookGroupReport_90();
            Domain.Socioboard.Domain.FacebookReport_15 facebook_15_temp = new Domain.Socioboard.Domain.FacebookReport_15();
            Domain.Socioboard.Domain.FacebookReport_30 facebook_30_temp = new Domain.Socioboard.Domain.FacebookReport_30();
            Domain.Socioboard.Domain.FacebookReport_60 facebook_60_temp = new Domain.Socioboard.Domain.FacebookReport_60();
            Domain.Socioboard.Domain.FacebookReport_90 facebook_90_temp = new Domain.Socioboard.Domain.FacebookReport_90();
            int TotalLikes_15 = 0;
            int TalkingAbout_15 = 0;
            int Likes_15 = 0;
            int[] PerDayLikes_15 = new int[15];
            int[] PerDayUnlikes_15 = new int[15];
            int[] PerDayImpressions_15 = new int[15];
            int[] PerDayStoryShare_15 = new int[15];
            string str_PerDayLikes_15 = string.Empty;
            string str_PerDayUnlikes_15 = string.Empty;
            string str_PerDayImpressions_15 = string.Empty;
            string str_PerDayStoryShare_15 = string.Empty;
             int Unlikes_15 = 0;
            int Impressions_15 = 0;
            int StoryShare_15 = 0;
            int ImpressionFans_15 = 0;
            int ImpressionPagePost_15 = 0;
            int ImpressionuserPost_15 = 0;
            int ImpressionCoupn_15 = 0;
            int ImpressionOther_15 = 0;
            int ImpressionMention_15 = 0;
            int ImpressionCheckin_15 = 0;
            int ImpressionQuestion_15 = 0;
            int ImpressionEvent_15 = 0;
            int Organic_15 = 0;
            int Viral_15 = 0;
            int Paid_15 = 0;
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
            int TotalLikes_30 = 0;
            int TalkingAbout_30 = 0;
            int Likes_30 = 0;
            int[] PerDayLikes_30 = new int[30];
            int[] PerDayUnlikes_30 = new int[30];
            int[] PerDayImpressions_30 = new int[30];
            int[] PerDayStoryShare_30 = new int[30];
            string str_PerDayLikes_30 = string.Empty;
            string str_PerDayUnlikes_30 = string.Empty;
            string str_PerDayImpressions_30 = string.Empty;
            string str_PerDayStoryShare_30 = string.Empty;
           
            int Unlikes_30 = 0;
            int Impressions_30 = 0;
            int StoryShare_30 = 0;
            int ImpressionFans_30 = 0;
            int ImpressionPagePost_30 = 0;
            int ImpressionuserPost_30 = 0;
            int ImpressionCoupn_30 = 0;
            int ImpressionOther_30 = 0;
            int ImpressionMention_30 = 0;
            int ImpressionCheckin_30 = 0;
            int ImpressionQuestion_30 = 0;
            int ImpressionEvent_30 = 0;
            int Organic_30 = 0;
            int Viral_30 = 0;
            int Paid_30 = 0;
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
            int Sharing_M_13_17_30= 0;
            int Sharing_M_18_24_30= 0;
            int Sharing_M_25_34_30= 0;
            int Sharing_M_35_44_30= 0;
            int Sharing_M_45_54_30= 0;
            int Sharing_M_55_64_30= 0;
            int Sharing_M_65_30 = 0;
            int Sharing_F_13_17_30= 0;
            int Sharing_F_18_24_30= 0;
            int Sharing_F_25_34_30= 0;
            int Sharing_F_35_44_30= 0;
            int Sharing_F_45_54_30= 0;
            int Sharing_F_55_64_30= 0;
            int Sharing_F_65_30 = 0;
    
            int TotalLikes_60 = 0;
            int TalkingAbout_60 = 0;
            int Likes_60 = 0;
            int[] PerDayLikes_60 = new int[60];
            int[] PerDayUnlikes_60 = new int[60];
            int[] PerDayImpressions_60 = new int[60];
            int[] PerDayStoryShare_60 = new int[60];
            string str_PerDayLikes_60 = string.Empty;
            string str_PerDayUnlikes_60 = string.Empty;
            string str_PerDayImpressions_60 = string.Empty;
            string str_PerDayStoryShare_60 = string.Empty;
           
            int Unlikes_60 = 0;
            int Impressions_60 = 0;
            int StoryShare_60 = 0;
            int ImpressionFans_60 = 0;
            int ImpressionPagePost_60 = 0;
            int ImpressionuserPost_60 = 0;
            
            int ImpressionCoupn_60 = 0;
            int ImpressionOther_60 = 0;
            int ImpressionMention_60 = 0;
            int ImpressionCheckin_60 = 0;
            int ImpressionQuestion_60 = 0;
            int ImpressionEvent_60 = 0;
            int Organic_60 = 0;
            int Viral_60 = 0;
            int Paid_60 = 0;
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
            int Sharing_M_13_17_60= 0;
            int Sharing_M_18_24_60= 0;
            int Sharing_M_25_34_60= 0;
            int Sharing_M_35_44_60= 0;
            int Sharing_M_45_54_60= 0;
            int Sharing_M_55_64_60= 0;
            int Sharing_M_65_60 = 0;
            int Sharing_F_13_17_60 = 0;
            int Sharing_F_18_24_60 = 0;
            int Sharing_F_25_34_60 = 0;
            int Sharing_F_35_44_60 = 0;
            int Sharing_F_45_54_60 = 0;
            int Sharing_F_55_64_60 = 0;
            int Sharing_F_65_60 = 0;
            int TotalLikes_90 = 0;
            int TalkingAbout_90 = 0;
            int Likes_90 = 0;
            int[] PerDayLikes_90 = new int[90];
            int[] PerDayUnlikes_90 = new int[90];
            int[] PerDayImpressions_90 = new int[90];
            int[] PerDayStoryShare_90 = new int[90];
            string str_PerDayLikes_90 = string.Empty;
            string str_PerDayUnlikes_90 = string.Empty;
            string str_PerDayImpressions_90 = string.Empty;
            string str_PerDayStoryShare_90 = string.Empty;
            
            int Unlikes_90 = 0;
            int Impressions_90 = 0;
            int StoryShare_90 = 0;
            int ImpressionFans_90 = 0;
            int ImpressionPagePost_90 = 0;
            int ImpressionuserPost_90 = 0;
            int UniqueUser_90 = 0;
            int UniqueUser_60 = 0;
            int UniqueUser_30 = 0;
            int UniqueUser_15 = 0;
           
            int ImpressionCoupn_90 = 0;
            int ImpressionOther_90 = 0;
            int ImpressionMention_90 = 0;
            int ImpressionCheckin_90 = 0;
            int ImpressionQuestion_90 = 0;
            int ImpressionEvent_90 = 0;
            int Organic_90 = 0;
            int Viral_90 = 0;
            int Paid_90 = 0;
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
            int Sharing_M_13_17_90= 0;
            int Sharing_M_18_24_90= 0;
            int Sharing_M_25_34_90= 0;
            int Sharing_M_35_44_90= 0;
            int Sharing_M_45_54_90= 0;
            int Sharing_M_55_64_90= 0;
            int Sharing_M_65_90 = 0;
            int Sharing_F_13_17_90 = 0;
            int Sharing_F_18_24_90 = 0;
            int Sharing_F_25_34_90 = 0;
            int Sharing_F_35_44_90 = 0;
            int Sharing_F_45_54_90 = 0;
            int Sharing_F_55_64_90 = 0;
            int Sharing_F_65_90 = 0;
            // no of fb accounts 
            //string fb_accounts = total_fb_accounts(groupid, userid);
            int fb_count = 0;

            try 
            {

                //using (NHibernate.ISession session4 = SessionFactory.GetNewSession())
                //{

                //    teams = session4.CreateQuery("from Team t where t.GroupId = : groupid and t.UserId = : userid ").SetParameter("groupid", Guid.Parse(groupid)).SetParameter("userid", Guid.Parse(userid)).List<Domain.Socioboard.Domain.Team>().ToList();

                //}

                //foreach (Domain.Socioboard.Domain.Team team in teams)
                //{

                    using (NHibernate.ISession session2 = SessionFactory.GetNewSession())
                    {

                        teammemberprofiles = session2.CreateQuery("from GroupProfile t where t.GroupId = : GroupId and t.ProfileType = : ProfileType").SetParameter("GroupId", Guid.Parse(groupid)).SetParameter("ProfileType", "facebook_page").List<Domain.Socioboard.Domain.GroupProfile>().ToList();
                    }

                    foreach (Domain.Socioboard.Domain.GroupProfile teammemberprofile in teammemberprofiles)
                    {
                        Domain.Socioboard.Domain.FacebookAccount objFacebookAccount = _FacebookAccountRepository.getFacebookAccountDetailsById(teammemberprofile.ProfileId, Guid.Parse(userid));
                        if (!string.IsNullOrEmpty(objFacebookAccount.AccessToken))
                        {
                            fb_count++;
                            using (NHibernate.ISession session2 = SessionFactory.GetNewSession())
                            {

                                try
                                {
                                    NHibernate.IQuery f_15 = session2.CreateQuery("from FacebookReport_15 f where f.FacebookId = : facebookid").SetParameter("facebookid", teammemberprofile.ProfileId);
                                    facebook_15_temp = (Domain.Socioboard.Domain.FacebookReport_15)f_15.UniqueResult();
                                    TotalLikes_15 += Int32.Parse(facebook_15_temp.TotalLikes);
                                    TalkingAbout_15 += Int32.Parse(facebook_15_temp.TalkingAbout);
                                    Likes_15 += facebook_15_temp.Likes;
                                    UniqueUser_15 += facebook_15_temp.UniqueUser;
                                    try
                                    {
                                        string[] arr1 = (facebook_15_temp.PerDayLikes).Split(',');

                                        for (int i = 0; i < 15; i++)
                                        {
                                            PerDayLikes_15[i] = PerDayLikes_15[i] + Int32.Parse(arr1[i]);
                                        }
                                    }
                                    catch (Exception)
                                    {

                                        for (int i = 0; i < 15; i++)
                                        {
                                            PerDayLikes_15[i] = PerDayLikes_15[i] + 0;
                                        }

                                    }

                                    Unlikes_15 += facebook_15_temp.Unlikes;
                                    try
                                    {
                                        string[] arr2 = (facebook_15_temp.PerDayUnlikes).Split(',');
                                        for (int i = 0; i < 15; i++)
                                        {
                                            PerDayUnlikes_15[i] = PerDayUnlikes_15[i] + Int32.Parse(arr2[i]);
                                        }
                                    }
                                    catch (Exception)
                                    {

                                        for (int i = 0; i < 15; i++)
                                        {
                                            PerDayUnlikes_15[i] = PerDayUnlikes_15[i] + 0;
                                        }
                                    }

                                    Impressions_15 += facebook_15_temp.Impression;
                                    try
                                    {
                                        string[] arr3 = (facebook_15_temp.PerDayImpression).Split(',');
                                        for (int i = 0; i < 15; i++)
                                        {
                                            PerDayImpressions_15[i] = PerDayImpressions_15[i] + Int32.Parse(arr3[i]);
                                        }
                                    }
                                    catch (Exception)
                                    {

                                        for (int i = 0; i < 15; i++)
                                        {
                                            PerDayImpressions_15[i] = PerDayImpressions_15[i] + 0;
                                        }
                                    }


                                    StoryShare_15 += facebook_15_temp.StoryShare;
                                    try
                                    {
                                        string[] arr4 = (facebook_15_temp.PerDayStoryShare).Split(',');
                                        for (int i = 0; i < 15; i++)
                                        {
                                            PerDayStoryShare_15[i] = PerDayStoryShare_15[i] + Int32.Parse(arr4[i]);
                                        }
                                    }
                                    catch (Exception)
                                    {
                                        for (int i = 0; i < 15; i++)
                                        {
                                            PerDayStoryShare_15[i] = PerDayStoryShare_15[i] + 0;
                                        }

                                    }

                                    ImpressionFans_15 += facebook_15_temp.ImpressionFans;
                                    ImpressionPagePost_15 += facebook_15_temp.ImpressionPagePost;
                                    ImpressionuserPost_15 += facebook_15_temp.ImpressionuserPost;
                                    ImpressionCoupn_15 += facebook_15_temp.ImpressionCoupn;
                                    ImpressionOther_15 += facebook_15_temp.ImpressionOther;
                                    ImpressionMention_15 += facebook_15_temp.ImpressionMention;
                                    ImpressionCheckin_15 += facebook_15_temp.ImpressionCheckin;
                                    ImpressionQuestion_15 += facebook_15_temp.ImpressionQuestion;
                                    ImpressionEvent_15 += facebook_15_temp.ImpressionEvent;
                                    Organic_15 += facebook_15_temp.Organic;
                                    Viral_15 += facebook_15_temp.Viral;
                                    Paid_15 += facebook_15_temp.Paid;
                                    M_13_17_15 += facebook_15_temp.M_13_17;
                                    M_18_24_15 += facebook_15_temp.M_18_24;
                                    M_25_34_15 += facebook_15_temp.M_25_34;
                                    M_35_44_15 += facebook_15_temp.M_35_44;
                                    M_45_54_15 += facebook_15_temp.M_45_54;
                                    M_55_64_15 += facebook_15_temp.M_55_64;
                                    M_65_15 += facebook_15_temp.M_65;

                                    F_13_17_15 += facebook_15_temp.F_13_17;
                                    F_18_24_15 += facebook_15_temp.F_18_24;
                                    F_25_34_15 += facebook_15_temp.F_25_34;
                                    F_35_44_15 += facebook_15_temp.F_35_44;
                                    F_45_54_15 += facebook_15_temp.F_45_54;
                                    F_55_64_15 += facebook_15_temp.F_55_64;
                                    F_65_15 += facebook_15_temp.F_65;

                                    Sharing_M_13_17_15 += facebook_15_temp.Sharing_M_13_17;
                                    Sharing_M_25_34_15 += facebook_15_temp.Sharing_M_25_34;
                                    Sharing_M_35_44_15 += facebook_15_temp.Sharing_M_35_44;
                                    Sharing_M_45_54_15 += facebook_15_temp.Sharing_M_45_54;
                                    Sharing_M_55_64_15 += facebook_15_temp.Sharing_M_55_64;
                                    Sharing_M_65_15 += facebook_15_temp.Sharing_M_65;

                                    Sharing_F_13_17_15 += facebook_15_temp.Sharing_F_13_17;
                                    Sharing_F_25_34_15 += facebook_15_temp.Sharing_F_25_34;
                                    Sharing_F_35_44_15 += facebook_15_temp.Sharing_F_35_44;
                                    Sharing_F_45_54_15 += facebook_15_temp.Sharing_F_45_54;
                                    Sharing_F_55_64_15 += facebook_15_temp.Sharing_F_55_64;
                                    Sharing_F_65_15 += facebook_15_temp.Sharing_F_65;
                                }
                                catch { }

                            }


                            using (NHibernate.ISession session3 = SessionFactory.GetNewSession())
                            {

                                try
                                {
                                    NHibernate.IQuery f_30 = session3.CreateQuery("from FacebookReport_30 f where f.FacebookId = : facebookid").SetParameter("facebookid", teammemberprofile.ProfileId);
                                    facebook_30_temp = (Domain.Socioboard.Domain.FacebookReport_30)f_30.UniqueResult();
                                    TotalLikes_30 += Int32.Parse(facebook_30_temp.TotalLikes);
                                    TalkingAbout_30 += Int32.Parse(facebook_30_temp.TalkingAbout);
                                    Likes_30 += facebook_30_temp.Likes;
                                    UniqueUser_30 += facebook_30_temp.UniqueUser;
                                    try
                                    {
                                        string[] arr1 = (facebook_30_temp.PerDayLikes).Split(',');

                                        for (int i = 0; i < 30; i++)
                                        {
                                            PerDayLikes_30[i] = PerDayLikes_30[i] + Int32.Parse(arr1[i]);
                                        }
                                    }
                                    catch (Exception)
                                    {

                                        for (int i = 0; i < 30; i++)
                                        {
                                            PerDayLikes_30[i] = PerDayLikes_30[i] + 0;
                                        }

                                    }

                                    Unlikes_30 += facebook_30_temp.Unlikes;
                                    try
                                    {
                                        string[] arr2 = (facebook_30_temp.PerDayUnlikes).Split(',');
                                        for (int i = 0; i < 30; i++)
                                        {
                                            PerDayUnlikes_30[i] = PerDayUnlikes_30[i] + Int32.Parse(arr2[i]);
                                        }
                                    }
                                    catch (Exception)
                                    {

                                        for (int i = 0; i < 30; i++)
                                        {
                                            PerDayUnlikes_30[i] = PerDayUnlikes_30[i] + 0;
                                        }
                                    }

                                    Impressions_30 += facebook_30_temp.Impression;
                                    try
                                    {
                                        string[] arr3 = (facebook_30_temp.PerDayImpression).Split(',');
                                        for (int i = 0; i < 30; i++)
                                        {
                                            PerDayImpressions_30[i] = PerDayImpressions_30[i] + Int32.Parse(arr3[i]);
                                        }
                                    }
                                    catch (Exception)
                                    {

                                        for (int i = 0; i < 30; i++)
                                        {
                                            PerDayImpressions_30[i] = PerDayImpressions_30[i] + 0;
                                        }
                                    }


                                    StoryShare_30 += facebook_30_temp.StoryShare;
                                    try
                                    {
                                        string[] arr4 = (facebook_30_temp.PerDayStoryShare).Split(',');
                                        for (int i = 0; i < 30; i++)
                                        {
                                            PerDayStoryShare_30[i] = PerDayStoryShare_30[i] + Int32.Parse(arr4[i]);
                                        }
                                    }
                                    catch (Exception)
                                    {
                                        for (int i = 0; i < 30; i++)
                                        {
                                            PerDayStoryShare_30[i] = PerDayStoryShare_30[i] + 0;
                                        }

                                    }

                                    ImpressionFans_30 += facebook_30_temp.ImpressionFans;
                                    ImpressionPagePost_30 += facebook_30_temp.ImpressionPagePost;
                                    ImpressionuserPost_30 += facebook_30_temp.ImpressionuserPost;
                                    ImpressionCoupn_30 += facebook_30_temp.ImpressionCoupn;
                                    ImpressionOther_30 += facebook_30_temp.ImpressionOther;
                                    ImpressionMention_30 += facebook_30_temp.ImpressionMention;
                                    ImpressionCheckin_30 += facebook_30_temp.ImpressionCheckin;
                                    ImpressionQuestion_30 += facebook_30_temp.ImpressionQuestion;
                                    ImpressionEvent_30 += facebook_30_temp.ImpressionEvent;
                                    Organic_30 += facebook_30_temp.Organic;
                                    Viral_30 += facebook_30_temp.Viral;
                                    Paid_30 += facebook_30_temp.Paid;
                                    M_13_17_30 += facebook_30_temp.M_13_17;
                                    M_18_24_30 += facebook_30_temp.M_18_24;
                                    M_25_34_30 += facebook_30_temp.M_25_34;
                                    M_35_44_30 += facebook_30_temp.M_35_44;
                                    M_45_54_30 += facebook_30_temp.M_45_54;
                                    M_55_64_30 += facebook_30_temp.M_55_64;
                                    M_65_30 += facebook_30_temp.M_65;

                                    F_13_17_30 += facebook_30_temp.F_13_17;
                                    F_18_24_30 += facebook_30_temp.F_18_24;
                                    F_25_34_30 += facebook_30_temp.F_25_34;
                                    F_35_44_30 += facebook_30_temp.F_35_44;
                                    F_45_54_30 += facebook_30_temp.F_45_54;
                                    F_55_64_30 += facebook_30_temp.F_55_64;
                                    F_65_30 += facebook_30_temp.F_65;

                                    Sharing_M_13_17_30 += facebook_30_temp.Sharing_M_13_17;
                                    Sharing_M_25_34_30 += facebook_30_temp.Sharing_M_25_34;
                                    Sharing_M_35_44_30 += facebook_30_temp.Sharing_M_35_44;
                                    Sharing_M_45_54_30 += facebook_30_temp.Sharing_M_45_54;
                                    Sharing_M_55_64_30 += facebook_30_temp.Sharing_M_55_64;
                                    Sharing_M_65_30 += facebook_30_temp.Sharing_M_65;

                                    Sharing_F_13_17_30 += facebook_30_temp.Sharing_F_13_17;
                                    Sharing_F_25_34_30 += facebook_30_temp.Sharing_F_25_34;
                                    Sharing_F_35_44_30 += facebook_30_temp.Sharing_F_35_44;
                                    Sharing_F_45_54_30 += facebook_30_temp.Sharing_F_45_54;
                                    Sharing_F_55_64_30 += facebook_30_temp.Sharing_F_55_64;
                                    Sharing_F_65_30 += facebook_30_temp.Sharing_F_65;
                                }
                                catch { }

                            }


                            using (NHibernate.ISession session4 = SessionFactory.GetNewSession())
                            {

                                try
                                {
                                    NHibernate.IQuery f_60 = session4.CreateQuery("from FacebookReport_60 f where f.FacebookId = : facebookid").SetParameter("facebookid", teammemberprofile.ProfileId);
                                    facebook_60_temp = (Domain.Socioboard.Domain.FacebookReport_60)f_60.UniqueResult();
                                    TotalLikes_60 += Int32.Parse(facebook_60_temp.TotalLikes);
                                    TalkingAbout_60 += Int32.Parse(facebook_60_temp.TalkingAbout);
                                    Likes_60 += facebook_60_temp.Likes;
                                    UniqueUser_60 += facebook_60.UniqueUser;
                                    try
                                    {
                                        string[] arr1 = (facebook_60_temp.PerDayLikes).Split(',');

                                        for (int i = 0; i < 60; i++)
                                        {
                                            PerDayLikes_60[i] = PerDayLikes_60[i] + Int32.Parse(arr1[i]);
                                        }
                                    }
                                    catch (Exception)
                                    {

                                        for (int i = 0; i < 60; i++)
                                        {
                                            PerDayLikes_60[i] = PerDayLikes_60[i] + 0;
                                        }

                                    }

                                    Unlikes_60 += facebook_60_temp.Unlikes;
                                    try
                                    {
                                        string[] arr2 = (facebook_60_temp.PerDayUnlikes).Split(',');
                                        for (int i = 0; i < 60; i++)
                                        {
                                            PerDayUnlikes_60[i] = PerDayUnlikes_60[i] + Int32.Parse(arr2[i]);
                                        }
                                    }
                                    catch (Exception)
                                    {

                                        for (int i = 0; i < 60; i++)
                                        {
                                            PerDayUnlikes_60[i] = PerDayUnlikes_60[i] + 0;
                                        }
                                    }

                                    Impressions_60 += facebook_60_temp.Impression;
                                    try
                                    {
                                        string[] arr3 = (facebook_60_temp.PerDayImpression).Split(',');
                                        for (int i = 0; i < 60; i++)
                                        {
                                            PerDayImpressions_60[i] = PerDayImpressions_60[i] + Int32.Parse(arr3[i]);
                                        }
                                    }
                                    catch (Exception)
                                    {

                                        for (int i = 0; i < 60; i++)
                                        {
                                            PerDayImpressions_60[i] = PerDayImpressions_60[i] + 0;
                                        }
                                    }


                                    StoryShare_60 += facebook_60_temp.StoryShare;
                                    try
                                    {
                                        string[] arr4 = (facebook_60_temp.PerDayStoryShare).Split(',');
                                        for (int i = 0; i < 60; i++)
                                        {
                                            PerDayStoryShare_60[i] = PerDayStoryShare_60[i] + Int32.Parse(arr4[i]);
                                        }
                                    }
                                    catch (Exception)
                                    {
                                        for (int i = 0; i < 60; i++)
                                        {
                                            PerDayStoryShare_60[i] = PerDayStoryShare_60[i] + 0;
                                        }

                                    }

                                    ImpressionFans_60 += facebook_60_temp.ImpressionFans;
                                    ImpressionPagePost_60 += facebook_60_temp.ImpressionPagePost;
                                    ImpressionuserPost_60 += facebook_60_temp.ImpressionuserPost;
                                    ImpressionCoupn_60 += facebook_60_temp.ImpressionCoupn;
                                    ImpressionOther_60 += facebook_60_temp.ImpressionOther;
                                    ImpressionMention_60 += facebook_60_temp.ImpressionMention;
                                    ImpressionCheckin_60 += facebook_60_temp.ImpressionCheckin;
                                    ImpressionQuestion_60 += facebook_60_temp.ImpressionQuestion;
                                    ImpressionEvent_60 += facebook_60_temp.ImpressionEvent;
                                    Organic_60 += facebook_60_temp.Organic;
                                    Viral_60 += facebook_60_temp.Viral;
                                    Paid_60 += facebook_60_temp.Paid;
                                    M_13_17_60 += facebook_60_temp.M_13_17;
                                    M_18_24_60 += facebook_60_temp.M_18_24;
                                    M_25_34_60 += facebook_60_temp.M_25_34;
                                    M_35_44_60 += facebook_60_temp.M_35_44;
                                    M_45_54_60 += facebook_60_temp.M_45_54;
                                    M_55_64_60 += facebook_60_temp.M_55_64;
                                    M_65_60 += facebook_60_temp.M_65;

                                    F_13_17_60 += facebook_60_temp.F_13_17;
                                    F_18_24_60 += facebook_60_temp.F_18_24;
                                    F_25_34_60 += facebook_60_temp.F_25_34;
                                    F_35_44_60 += facebook_60_temp.F_35_44;
                                    F_45_54_60 += facebook_60_temp.F_45_54;
                                    F_55_64_60 += facebook_60_temp.F_55_64;
                                    F_65_60 += facebook_60_temp.F_65;

                                    Sharing_M_13_17_60 += facebook_60_temp.Sharing_M_13_17;
                                    Sharing_M_25_34_60 += facebook_60_temp.Sharing_M_25_34;
                                    Sharing_M_35_44_60 += facebook_60_temp.Sharing_M_35_44;
                                    Sharing_M_45_54_60 += facebook_60_temp.Sharing_M_45_54;
                                    Sharing_M_55_64_60 += facebook_60_temp.Sharing_M_55_64;
                                    Sharing_M_65_60 += facebook_60_temp.Sharing_M_65;

                                    Sharing_F_13_17_60 += facebook_60_temp.Sharing_F_13_17;
                                    Sharing_F_25_34_60 += facebook_60_temp.Sharing_F_25_34;
                                    Sharing_F_35_44_60 += facebook_60_temp.Sharing_F_35_44;
                                    Sharing_F_45_54_60 += facebook_60_temp.Sharing_F_45_54;
                                    Sharing_F_55_64_60 += facebook_60_temp.Sharing_F_55_64;
                                    Sharing_F_65_60 += facebook_60_temp.Sharing_F_65;
                                }
                                catch { }

                            }





                            using (NHibernate.ISession session5 = SessionFactory.GetNewSession())
                            {

                                try
                                {
                                    NHibernate.IQuery f_90 = session5.CreateQuery("from FacebookReport_90 f where f.FacebookId = : facebookid").SetParameter("facebookid", teammemberprofile.ProfileId);
                                    facebook_90_temp = (Domain.Socioboard.Domain.FacebookReport_90)f_90.UniqueResult();
                                    TotalLikes_90 += Int32.Parse(facebook_90_temp.TotalLikes);
                                    TalkingAbout_90 += Int32.Parse(facebook_90_temp.TalkingAbout);
                                    Likes_90 += facebook_90_temp.Likes;
                                    UniqueUser_90 += facebook_90_temp.UniqueUser;
                                    try
                                    {
                                        string[] arr1 = (facebook_90_temp.PerDayLikes).Split(',');

                                        for (int i = 0; i < 90; i++)
                                        {
                                            PerDayLikes_90[i] = PerDayLikes_90[i] + Int32.Parse(arr1[i]);
                                        }
                                    }
                                    catch (Exception)
                                    {

                                        for (int i = 0; i < 90; i++)
                                        {
                                            PerDayLikes_90[i] = PerDayLikes_90[i] + 0;
                                        }

                                    }

                                    Unlikes_90 += facebook_90_temp.Unlikes;
                                    try
                                    {
                                        string[] arr2 = (facebook_90_temp.PerDayUnlikes).Split(',');
                                        for (int i = 0; i < 90; i++)
                                        {
                                            PerDayUnlikes_90[i] = PerDayUnlikes_90[i] + Int32.Parse(arr2[i]);
                                        }
                                    }
                                    catch (Exception)
                                    {

                                        for (int i = 0; i < 90; i++)
                                        {
                                            PerDayUnlikes_90[i] = PerDayUnlikes_90[i] + 0;
                                        }
                                    }

                                    Impressions_90 += facebook_90_temp.Impression;
                                    try
                                    {
                                        string[] arr3 = (facebook_90_temp.PerDayImpression).Split(',');
                                        for (int i = 0; i < 90; i++)
                                        {
                                            PerDayImpressions_90[i] = PerDayImpressions_90[i] + Int32.Parse(arr3[i]);
                                        }
                                    }
                                    catch (Exception)
                                    {

                                        for (int i = 0; i < 90; i++)
                                        {
                                            PerDayImpressions_90[i] = PerDayImpressions_90[i] + 0;
                                        }
                                    }


                                    StoryShare_90 += facebook_90_temp.StoryShare;
                                    try
                                    {
                                        string[] arr4 = (facebook_90_temp.PerDayStoryShare).Split(',');
                                        for (int i = 0; i < 90; i++)
                                        {
                                            PerDayStoryShare_90[i] = PerDayStoryShare_90[i] + Int32.Parse(arr4[i]);
                                        }
                                    }
                                    catch (Exception)
                                    {

                                        for (int i = 0; i < 90; i++)
                                        {
                                            PerDayStoryShare_90[i] = PerDayStoryShare_90[i] + 0;
                                        }
                                    }

                                    ImpressionFans_90 += facebook_90_temp.ImpressionFans;
                                    ImpressionPagePost_90 += facebook_90_temp.ImpressionPagePost;
                                    ImpressionuserPost_90 += facebook_90_temp.ImpressionuserPost;
                                    ImpressionCoupn_90 += facebook_90_temp.ImpressionCoupn;
                                    ImpressionOther_90 += facebook_90_temp.ImpressionOther;
                                    ImpressionMention_90 += facebook_90_temp.ImpressionMention;
                                    ImpressionCheckin_90 += facebook_90_temp.ImpressionCheckin;
                                    ImpressionQuestion_90 += facebook_90_temp.ImpressionQuestion;
                                    ImpressionEvent_90 += facebook_90_temp.ImpressionEvent;
                                    Organic_90 += facebook_90_temp.Organic;
                                    Viral_90 += facebook_90_temp.Viral;
                                    Paid_90 += facebook_90_temp.Paid;
                                    M_13_17_90 += facebook_90_temp.M_13_17;
                                    M_18_24_90 += facebook_90_temp.M_18_24;
                                    M_25_34_90 += facebook_90_temp.M_25_34;
                                    M_35_44_90 += facebook_90_temp.M_35_44;
                                    M_45_54_90 += facebook_90_temp.M_45_54;
                                    M_55_64_90 += facebook_90_temp.M_55_64;
                                    M_65_90 += facebook_90_temp.M_65;

                                    F_13_17_90 += facebook_90_temp.F_13_17;
                                    F_18_24_90 += facebook_90_temp.F_18_24;
                                    F_25_34_90 += facebook_90_temp.F_25_34;
                                    F_35_44_90 += facebook_90_temp.F_35_44;
                                    F_45_54_90 += facebook_90_temp.F_45_54;
                                    F_55_64_90 += facebook_90_temp.F_55_64;
                                    F_65_90 += facebook_90_temp.F_65;

                                    Sharing_M_13_17_90 += facebook_90_temp.Sharing_M_13_17;
                                    Sharing_M_25_34_90 += facebook_90_temp.Sharing_M_25_34;
                                    Sharing_M_35_44_90 += facebook_90_temp.Sharing_M_35_44;
                                    Sharing_M_45_54_90 += facebook_90_temp.Sharing_M_45_54;
                                    Sharing_M_55_64_90 += facebook_90_temp.Sharing_M_55_64;
                                    Sharing_M_65_90 += facebook_90_temp.Sharing_M_65;

                                    Sharing_F_13_17_90 += facebook_90_temp.Sharing_F_13_17;
                                    Sharing_F_25_34_90 += facebook_90_temp.Sharing_F_25_34;
                                    Sharing_F_35_44_90 += facebook_90_temp.Sharing_F_35_44;
                                    Sharing_F_45_54_90 += facebook_90_temp.Sharing_F_45_54;
                                    Sharing_F_55_64_90 += facebook_90_temp.Sharing_F_55_64;
                                    Sharing_F_65_90 += facebook_90_temp.Sharing_F_65;
                                }
                                catch { }

                            } 
                        }


                        

                    
                  }


                    facebook_15.TotalLikes = TotalLikes_15.ToString();
                    facebook_15.TalkingAbout = TalkingAbout_15.ToString();
                    facebook_15.Likes = Likes_15;
                    facebook_15.Unlikes = Unlikes_15;
                    facebook_15.Impression = Impressions_15;
                    facebook_15.UniqueUser = UniqueUser_15;
                    facebook_15.StoryShare = StoryShare_15;
                    facebook_15.ImpressionFans = ImpressionFans_15;
                    facebook_15.ImpressionPagePost = ImpressionPagePost_15;
                    facebook_15.ImpressionuserPost = ImpressionuserPost_15;
                    facebook_15.ImpressionCoupn = ImpressionCoupn_15;
                    facebook_15.ImpressionOther = ImpressionOther_15;
                    facebook_15.ImpressionMention = ImpressionMention_15;
                    facebook_15.ImpressionCheckin = ImpressionCheckin_15;
                    facebook_15.ImpressionQuestion = ImpressionQuestion_15;
                    facebook_15.ImpressionEvent = ImpressionEvent_15;
                    facebook_15.Organic = Organic_15;
                    facebook_15.Viral = Viral_15;
                    facebook_15.Paid = Paid_15;
                    facebook_15.M_13_17 = M_13_17_15;
                    facebook_15.M_18_24 = M_18_24_15;
                    facebook_15.M_25_34 = M_25_34_15;
                    facebook_15.M_35_44 = M_35_44_15;
                    facebook_15.M_45_54 = M_45_54_15;
                    facebook_15.M_55_64 = M_55_64_15;
                    facebook_15.M_65 = M_65_15;

                    facebook_15.F_13_17 = F_13_17_15;
                    facebook_15.F_18_24 = F_18_24_15;
                    facebook_15.F_25_34 = F_25_34_15;
                    facebook_15.F_35_44 = F_35_44_15;
                    facebook_15.F_45_54 = F_45_54_15;
                    facebook_15.F_55_64 = F_55_64_15;
                    facebook_15.F_65 = F_65_15;

                    facebook_15.Sharing_M_13_17 = Sharing_M_13_17_15;
                    facebook_15.Sharing_M_18_24 = Sharing_M_18_24_15;
                    facebook_15.Sharing_M_25_34 = Sharing_M_25_34_15;
                    facebook_15.Sharing_M_35_44 = Sharing_M_35_44_15;
                    facebook_15.Sharing_M_45_54 = Sharing_M_45_54_15;
                    facebook_15.Sharing_M_55_64 = Sharing_M_55_64_15;
                    facebook_15.Sharing_M_65 = Sharing_M_65_15;

                    facebook_15.Sharing_F_13_17 = Sharing_F_13_17_15;
                    facebook_15.Sharing_F_18_24 = Sharing_F_18_24_15;
                    facebook_15.Sharing_F_25_34 = Sharing_F_25_34_15;
                    facebook_15.Sharing_F_35_44 = Sharing_F_35_44_15;
                    facebook_15.Sharing_F_45_54 = Sharing_F_45_54_15;
                    facebook_15.Sharing_F_55_64 = Sharing_F_55_64_15;
                    facebook_15.Sharing_F_65 = Sharing_F_65_15;
                    //facebook_15.total_fb_accounts = Int32.Parse(fb_accounts);
                    facebook_15.total_fb_accounts = fb_count;

                    facebook_30.TotalLikes = TotalLikes_30.ToString();
                    facebook_30.TalkingAbout = TalkingAbout_30.ToString();
                    facebook_30.Likes = Likes_30;
                    facebook_30.Unlikes = Unlikes_30;
                    facebook_30.Impression = Impressions_30;
                    facebook_30.UniqueUser = UniqueUser_30;
                    facebook_30.StoryShare = StoryShare_30;
                    facebook_30.ImpressionFans = ImpressionFans_30;
                    facebook_30.ImpressionPagePost = ImpressionPagePost_30;
                    facebook_30.ImpressionuserPost = ImpressionuserPost_30;
                    facebook_30.ImpressionCoupn = ImpressionCoupn_30;
                    facebook_30.ImpressionOther = ImpressionOther_30;
                    facebook_30.ImpressionMention = ImpressionMention_30;
                    facebook_30.ImpressionCheckin = ImpressionCheckin_30;
                    facebook_30.ImpressionQuestion = ImpressionQuestion_30;
                    facebook_30.ImpressionEvent = ImpressionEvent_30;
                    facebook_30.Organic = Organic_30;
                    facebook_30.Viral = Viral_30;
                    facebook_30.Paid = Paid_30;
                    facebook_30.M_13_17 = M_13_17_30;
                    facebook_30.M_18_24 = M_18_24_30;
                    facebook_30.M_25_34 = M_25_34_30;
                    facebook_30.M_35_44 = M_35_44_30;
                    facebook_30.M_45_54 = M_45_54_30;
                    facebook_30.M_55_64 = M_55_64_30;
                    facebook_30.M_65 = M_65_30;

                    facebook_30.F_13_17 = F_13_17_30;
                    facebook_30.F_18_24 = F_18_24_30;
                    facebook_30.F_25_34 = F_25_34_30;
                    facebook_30.F_35_44 = F_35_44_30;
                    facebook_30.F_45_54 = F_45_54_30;
                    facebook_30.F_55_64 = F_55_64_30;
                    facebook_30.F_65 = F_65_30;

                    facebook_30.Sharing_M_13_17 = Sharing_M_13_17_30;
                    facebook_30.Sharing_M_18_24 = Sharing_M_18_24_30;
                    facebook_30.Sharing_M_25_34 = Sharing_M_25_34_30;
                    facebook_30.Sharing_M_35_44 = Sharing_M_35_44_30;
                    facebook_30.Sharing_M_45_54 = Sharing_M_45_54_30;
                    facebook_30.Sharing_M_55_64 = Sharing_M_55_64_30;
                    facebook_30.Sharing_M_65 = Sharing_M_65_30;

                    facebook_30.Sharing_F_13_17 = Sharing_F_13_17_30;
                    facebook_30.Sharing_F_18_24 = Sharing_F_18_24_30;
                    facebook_30.Sharing_F_25_34 = Sharing_F_25_34_30;
                    facebook_30.Sharing_F_35_44 = Sharing_F_35_44_30;
                    facebook_30.Sharing_F_45_54 = Sharing_F_45_54_30;
                    facebook_30.Sharing_F_55_64 = Sharing_F_55_64_30;
                    facebook_30.Sharing_F_65 = Sharing_F_65_30;
                    //facebook_30.total_fb_accounts = Int32.Parse(fb_accounts);
                    facebook_30.total_fb_accounts = fb_count;

                    facebook_60.TotalLikes = TotalLikes_60.ToString();
                    facebook_60.TalkingAbout = TalkingAbout_60.ToString();
                    facebook_60.Likes = Likes_60;
                    facebook_60.Unlikes = Unlikes_60;
                    facebook_60.Impression = Impressions_60;
                    facebook_60.UniqueUser = UniqueUser_60;
                    facebook_60.StoryShare = StoryShare_60;
                    facebook_60.ImpressionFans = ImpressionFans_60;
                    facebook_60.ImpressionPagePost = ImpressionPagePost_60;
                    facebook_60.ImpressionuserPost = ImpressionuserPost_60;
                    facebook_60.ImpressionCoupn = ImpressionCoupn_60;
                    facebook_60.ImpressionOther = ImpressionOther_60;
                    facebook_60.ImpressionMention = ImpressionMention_60;
                    facebook_60.ImpressionCheckin = ImpressionCheckin_60;
                    facebook_60.ImpressionQuestion = ImpressionQuestion_60;
                    facebook_60.ImpressionEvent = ImpressionEvent_60;
                    facebook_60.Organic = Organic_60;
                    facebook_60.Viral = Viral_60;
                    facebook_60.Paid = Paid_60;
                    facebook_60.M_13_17 = M_13_17_60;
                    facebook_60.M_18_24 = M_18_24_60;
                    facebook_60.M_25_34 = M_25_34_60;
                    facebook_60.M_35_44 = M_35_44_60;
                    facebook_60.M_45_54 = M_45_54_60;
                    facebook_60.M_55_64 = M_55_64_60;
                    facebook_60.M_65 = M_65_60;

                    facebook_60.F_13_17 = F_13_17_60;
                    facebook_60.F_18_24 = F_18_24_60;
                    facebook_60.F_25_34 = F_25_34_60;
                    facebook_60.F_35_44 = F_35_44_60;
                    facebook_60.F_45_54 = F_45_54_60;
                    facebook_60.F_55_64 = F_55_64_60;
                    facebook_60.F_65 = F_65_60;

                    facebook_60.Sharing_M_13_17 = Sharing_M_13_17_60;
                    facebook_60.Sharing_M_18_24 = Sharing_M_18_24_60;
                    facebook_60.Sharing_M_25_34 = Sharing_M_25_34_60;
                    facebook_60.Sharing_M_35_44 = Sharing_M_35_44_60;
                    facebook_60.Sharing_M_45_54 = Sharing_M_45_54_60;
                    facebook_60.Sharing_M_55_64 = Sharing_M_55_64_60;
                    facebook_60.Sharing_M_65 = Sharing_M_65_60;

                    facebook_60.Sharing_F_13_17 = Sharing_F_13_17_60;
                    facebook_60.Sharing_F_18_24 = Sharing_F_18_24_60;
                    facebook_60.Sharing_F_25_34 = Sharing_F_25_34_60;
                    facebook_60.Sharing_F_35_44 = Sharing_F_35_44_60;
                    facebook_60.Sharing_F_45_54 = Sharing_F_45_54_60;
                    facebook_60.Sharing_F_55_64 = Sharing_F_55_64_60;
                    facebook_60.Sharing_F_65 = Sharing_F_65_60;
                    ////facebook_60.total_fb_accounts = Int32.Parse(fb_accounts);

                    facebook_60.total_fb_accounts = fb_count;

                    facebook_90.TotalLikes = TotalLikes_90.ToString();
                    facebook_90.TalkingAbout = TalkingAbout_90.ToString();
                    facebook_90.Likes = Likes_90;
                    facebook_90.Unlikes = Unlikes_90;
                    facebook_90.Impression = Impressions_90;
                    facebook_90.UniqueUser = UniqueUser_90;
                    facebook_90.StoryShare = StoryShare_90;
                    facebook_90.ImpressionFans = ImpressionFans_90;
                    facebook_90.ImpressionPagePost = ImpressionPagePost_90;
                    facebook_90.ImpressionuserPost = ImpressionuserPost_90;
                    facebook_90.ImpressionCoupn = ImpressionCoupn_90;
                    facebook_90.ImpressionOther = ImpressionOther_90;
                    facebook_90.ImpressionMention = ImpressionMention_90;
                    facebook_90.ImpressionCheckin = ImpressionCheckin_90;
                    facebook_90.ImpressionQuestion = ImpressionQuestion_90;
                    facebook_90.ImpressionEvent = ImpressionEvent_90;
                    facebook_90.Organic = Organic_90;
                    facebook_90.Viral = Viral_90;
                    facebook_90.Paid = Paid_90;
                    facebook_90.M_13_17 = M_13_17_90;
                    facebook_90.M_18_24 = M_18_24_90;
                    facebook_90.M_25_34 = M_25_34_90;
                    facebook_90.M_35_44 = M_35_44_90;
                    facebook_90.M_45_54 = M_45_54_90;
                    facebook_90.M_55_64 = M_55_64_90;
                    facebook_90.M_65 = M_65_90;

                    facebook_90.F_13_17 = F_13_17_90;
                    facebook_90.F_18_24 = F_18_24_90;
                    facebook_90.F_25_34 = F_25_34_90;
                    facebook_90.F_35_44 = F_35_44_90;
                    facebook_90.F_45_54 = F_45_54_90;
                    facebook_90.F_55_64 = F_55_64_90;
                    facebook_90.F_65 = F_65_90;

                    facebook_90.Sharing_M_13_17 = Sharing_M_13_17_90;
                    facebook_90.Sharing_M_18_24 = Sharing_M_18_24_90;
                    facebook_90.Sharing_M_25_34 = Sharing_M_25_34_90;
                    facebook_90.Sharing_M_35_44 = Sharing_M_35_44_90;
                    facebook_90.Sharing_M_45_54 = Sharing_M_45_54_90;
                    facebook_90.Sharing_M_55_64 = Sharing_M_55_64_90;
                    facebook_90.Sharing_M_65 = Sharing_M_65_90;

                    facebook_90.Sharing_F_13_17 = Sharing_F_13_17_90;
                    facebook_90.Sharing_F_18_24 = Sharing_F_18_24_90;
                    facebook_90.Sharing_F_25_34 = Sharing_F_25_34_90;
                    facebook_90.Sharing_F_35_44 = Sharing_F_35_44_90;
                    facebook_90.Sharing_F_45_54 = Sharing_F_45_54_90;
                    facebook_90.Sharing_F_55_64 = Sharing_F_55_64_90;
                    facebook_90.Sharing_F_65 = Sharing_F_65_90;

                    facebook_15.GroupId = Guid.Parse(groupid);
                     facebook_30.GroupId = Guid.Parse(groupid);
                     facebook_60.GroupId = Guid.Parse(groupid);
                     facebook_90.GroupId = Guid.Parse(groupid);
                     //facebook_90.total_fb_accounts = Int32.Parse(fb_accounts);

                     facebook_90.total_fb_accounts = fb_count;

                    if(PerDayImpressions_15 !=null)
                    { 
                     str_PerDayImpressions_15 = String.Join(",", PerDayImpressions_15);
                    }
                    else
                    {
                        for (int i = 0; i < 15; i++)
                        {
                            str_PerDayImpressions_15 = str_PerDayImpressions_15 + "0,";
                        }
                    }

                    if (PerDayImpressions_30 != null)
                    {
                        str_PerDayImpressions_30 = String.Join(",", PerDayImpressions_30);
                    }
                    else
                    {
                        for (int i = 0; i < 30; i++)
                        {
                            str_PerDayImpressions_30 = str_PerDayImpressions_30 + "0,";
                        }
                    }

                    if (PerDayImpressions_60 != null)
                    {
                        str_PerDayImpressions_60 = String.Join(",", PerDayImpressions_60);
                    }
                    else
                    {
                        for (int i = 0; i < 60; i++)
                        {
                            str_PerDayImpressions_60 = str_PerDayImpressions_60 + "0,";
                        }
                    }


                    if (PerDayImpressions_90 != null)
                    {
                        str_PerDayImpressions_90 = String.Join(",", PerDayImpressions_90);
                    }
                    else
                    {
                        for (int i = 0; i < 90; i++)
                        {
                            str_PerDayImpressions_90 = str_PerDayImpressions_90 + "0,";
                        }
                    }





                    if (PerDayLikes_15 != null)
                    {
                        str_PerDayLikes_15 = String.Join(",", PerDayLikes_15);
                    }
                    else
                    {
                        for (int i = 0; i < 15; i++)
                        {
                            str_PerDayLikes_15 = str_PerDayLikes_15 + "0,";
                        }
                    }

                    if (PerDayLikes_30 != null)
                    {
                        str_PerDayLikes_30 = String.Join(",", PerDayLikes_30);
                    }
                    else
                    {
                        for (int i = 0; i < 30; i++)
                        {
                            str_PerDayLikes_30 = str_PerDayLikes_30 + "0,";
                        }
                    }

                    if (PerDayLikes_60 != null)
                    {
                        str_PerDayLikes_60 = String.Join(",", PerDayLikes_60);
                    }
                    else
                    {
                        for (int i = 0; i < 60; i++)
                        {
                            str_PerDayLikes_60 = str_PerDayLikes_60 + "0,";
                        }
                    }


                    if (PerDayLikes_90 != null)
                    {
                        str_PerDayLikes_90 = String.Join(",", PerDayLikes_90);
                    }
                    else
                    {
                        for (int i = 0; i < 90; i++)
                        {
                            str_PerDayLikes_90 = str_PerDayLikes_90 + "0,";
                        }
                    }




                    if (PerDayStoryShare_15 != null)
                    {
                        str_PerDayStoryShare_15 = String.Join(",", PerDayStoryShare_15);
                    }
                    else
                    {
                        for (int i = 0; i < 15; i++)
                        {
                            str_PerDayStoryShare_15 = str_PerDayStoryShare_15 + "0,";
                        }
                    }

                    if (PerDayStoryShare_30 != null)
                    {
                        str_PerDayStoryShare_30 = String.Join(",", PerDayStoryShare_30);
                    }
                    else
                    {
                        for (int i = 0; i < 30; i++)
                        {
                            str_PerDayStoryShare_30 = str_PerDayStoryShare_30 + "0,";
                        }
                    }

                    if (PerDayStoryShare_60 != null)
                    {
                        str_PerDayStoryShare_60 = String.Join(",", PerDayStoryShare_60);
                    }
                    else
                    {
                        for (int i = 0; i < 60; i++)
                        {
                            str_PerDayStoryShare_60 = str_PerDayStoryShare_60 + "0,";
                        }
                    }


                    if (PerDayStoryShare_90 != null)
                    {
                        str_PerDayStoryShare_90 = String.Join(",", PerDayStoryShare_90);
                    }
                    else
                    {
                        for (int i = 0; i < 90; i++)
                        {
                            str_PerDayStoryShare_90 = str_PerDayStoryShare_90 + "0,";
                        }
                    }



                    if (PerDayUnlikes_15 != null)
                    {
                        str_PerDayUnlikes_15 = String.Join(",", PerDayUnlikes_15);
                    }
                    else
                    {
                        for (int i = 0; i < 15; i++)
                        {
                            str_PerDayUnlikes_15 = str_PerDayUnlikes_15 + "0,";
                        }
                    }

                    if (PerDayUnlikes_30 != null)
                    {
                        str_PerDayUnlikes_30 = String.Join(",", PerDayUnlikes_30);
                    }
                    else
                    {
                        for (int i = 0; i < 30; i++)
                        {
                            str_PerDayUnlikes_30 = str_PerDayUnlikes_30 + "0,";
                        }
                    }

                    if (PerDayUnlikes_60 != null)
                    {
                        str_PerDayUnlikes_60 = String.Join(",", PerDayUnlikes_60);
                    }
                    else
                    {
                        for (int i = 0; i < 60; i++)
                        {
                            str_PerDayUnlikes_60 = str_PerDayUnlikes_60 + "0,";
                        }
                    }


                    if (PerDayUnlikes_90 != null)
                    {
                        str_PerDayUnlikes_90 = String.Join(",", PerDayUnlikes_90);
                    }
                    else
                    {
                        for (int i = 0; i < 90; i++)
                        {
                            str_PerDayUnlikes_90 = str_PerDayUnlikes_90 + "0,";
                        }
                    }



                 


                     facebook_15.PerDayImpression = str_PerDayImpressions_15;
                     facebook_15.PerDayLikes = str_PerDayLikes_15;
                     facebook_15.PerDayStoryShare = str_PerDayStoryShare_15;
                     facebook_15.PerDayUnlikes = str_PerDayUnlikes_15;

                     facebook_30.PerDayImpression = str_PerDayImpressions_30;
                     facebook_30.PerDayLikes = str_PerDayLikes_30;
                     facebook_30.PerDayStoryShare = str_PerDayStoryShare_30;
                     facebook_30.PerDayUnlikes = str_PerDayUnlikes_30;

                     facebook_60.PerDayImpression = str_PerDayImpressions_60;
                     facebook_60.PerDayLikes = str_PerDayLikes_60;
                     facebook_60.PerDayStoryShare = str_PerDayStoryShare_60;
                     facebook_60.PerDayUnlikes = str_PerDayUnlikes_60;

                     facebook_90.PerDayImpression = str_PerDayImpressions_90;
                     facebook_90.PerDayLikes = str_PerDayLikes_90;
                     facebook_90.PerDayStoryShare = str_PerDayStoryShare_90;
                     facebook_90.PerDayUnlikes = str_PerDayUnlikes_90;




                     insert_15(facebook_15);
                     insert_30(facebook_30);
                     insert_60(facebook_60);
                     insert_90(facebook_90);


                }
           // }
            catch(Exception e)
            {

            }


        }


        public void insert_15(Domain.Socioboard.Domain.FacebookGroupReport_15 _FacebookReport)
        {
            bool exist = false;
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                 exist = session.Query<Domain.Socioboard.Domain.FacebookGroupReport_15>()
                             .Any(x => x.GroupId == _FacebookReport.GroupId);
            }

            
                using (NHibernate.ISession session1 = SessionFactory.GetNewSession())
                {
                    //After Session creation, start and open Transaction. 
                    using (NHibernate.ITransaction transaction = session1.BeginTransaction())
                    {
                        if (exist)
                        {
                        //Proceed action to save data.
                            int i = session1.CreateQuery("update FacebookGroupReport_15 set TotalLikes =:TotalLikes,TalkingAbout=:TalkingAbout,Likes=:Likes,PerDayLikes=:PerDayLikes,Unlikes=:Unlikes,PerDayUnlikes=:PerDayUnlikes,Impression=:Impression,PerDayImpression=:PerDayImpression,UniqueUser=:UniqueUser,StoryShare=:StoryShare,PerDayStoryShare=:PerDayStoryShare,ImpressionFans=:ImpressionFans,ImpressionPagePost=:ImpressionPagePost,ImpressionuserPost=:ImpressionuserPost,ImpressionCoupn=:ImpressionCoupn,ImpressionOther=:ImpressionOther,ImpressionMention=:ImpressionMention,ImpressionCheckin=:ImpressionCheckin,ImpressionQuestion=:ImpressionQuestion,ImpressionEvent=:ImpressionEvent,Organic=:Organic,Viral=:Viral,Paid=:Paid,M_13_17=:M_13_17,M_18_24=:M_18_24,M_25_34=:M_25_34,M_35_44=:M_35_44,M_45_54=:M_45_54,M_55_64=:M_55_64,M_65=:M_65,F_13_17=:F_13_17,F_18_24=:F_18_24,F_25_34=:F_25_34,F_35_44=:F_35_44,F_45_54=:F_45_54,F_55_64=:F_55_64,F_65=:F_65,Sharing_M_13_17=:Sharing_M_13_17,Sharing_M_18_24=:Sharing_M_18_24,Sharing_M_25_34=:Sharing_M_25_34,Sharing_M_35_44=:Sharing_M_35_44,Sharing_M_45_54=:Sharing_M_45_54,Sharing_M_55_64=:Sharing_M_55_64,Sharing_M_65=:Sharing_M_65,Sharing_F_13_17=:Sharing_F_13_17,Sharing_F_18_24=:Sharing_F_18_24,Sharing_F_25_34=:Sharing_F_25_34,Sharing_F_35_44=:Sharing_F_35_44,Sharing_F_45_54=:Sharing_F_45_54,Sharing_F_55_64=:Sharing_F_55_64,Sharing_F_65=:Sharing_F_65, total_fb_accounts = : total_fb_accounts where GroupId =:GroupId")
                            .SetParameter("TotalLikes", _FacebookReport.TotalLikes)
                            .SetParameter("TalkingAbout", _FacebookReport.TalkingAbout)
                            .SetParameter("Likes", _FacebookReport.Likes)
                            .SetParameter("PerDayLikes", _FacebookReport.PerDayLikes)
                            .SetParameter("Unlikes", _FacebookReport.Unlikes)
                            .SetParameter("PerDayUnlikes", _FacebookReport.PerDayUnlikes)
                            .SetParameter("Impression", _FacebookReport.Impression)
                            .SetParameter("PerDayImpression", _FacebookReport.PerDayImpression)
                            .SetParameter("UniqueUser", _FacebookReport.UniqueUser)
                            .SetParameter("StoryShare", _FacebookReport.StoryShare)
                            .SetParameter("PerDayStoryShare", _FacebookReport.PerDayStoryShare)
                            .SetParameter("ImpressionFans", _FacebookReport.ImpressionFans)
                            .SetParameter("ImpressionPagePost", _FacebookReport.ImpressionPagePost)
                            .SetParameter("ImpressionuserPost", _FacebookReport.ImpressionuserPost)
                            .SetParameter("ImpressionCoupn", _FacebookReport.ImpressionCoupn)
                            .SetParameter("ImpressionOther", _FacebookReport.ImpressionOther)
                            .SetParameter("ImpressionMention", _FacebookReport.ImpressionMention)
                            .SetParameter("ImpressionCheckin", _FacebookReport.ImpressionCheckin)
                            .SetParameter("ImpressionQuestion", _FacebookReport.ImpressionQuestion)
                            .SetParameter("ImpressionEvent", _FacebookReport.ImpressionEvent)
                            .SetParameter("Organic", _FacebookReport.Organic)
                            .SetParameter("Viral", _FacebookReport.Viral)
                            .SetParameter("Paid", _FacebookReport.Paid)
                            .SetParameter("M_13_17", _FacebookReport.M_13_17)
                            .SetParameter("M_18_24", _FacebookReport.M_18_24)
                            .SetParameter("M_25_34", _FacebookReport.M_25_34)
                            .SetParameter("M_35_44", _FacebookReport.M_35_44)
                            .SetParameter("M_45_54", _FacebookReport.M_45_54)
                            .SetParameter("M_55_64", _FacebookReport.M_55_64)
                            .SetParameter("M_65", _FacebookReport.M_65)
                            .SetParameter("F_13_17", _FacebookReport.F_13_17)
                            .SetParameter("F_18_24", _FacebookReport.F_18_24)
                            .SetParameter("F_25_34", _FacebookReport.F_25_34)
                            .SetParameter("F_35_44", _FacebookReport.F_35_44)
                            .SetParameter("F_45_54", _FacebookReport.F_45_54)
                            .SetParameter("F_55_64", _FacebookReport.F_55_64)
                            .SetParameter("F_65", _FacebookReport.F_65)
                            .SetParameter("Sharing_M_13_17", _FacebookReport.Sharing_M_13_17)
                            .SetParameter("Sharing_M_18_24", _FacebookReport.Sharing_M_18_24)
                            .SetParameter("Sharing_M_25_34", _FacebookReport.Sharing_M_25_34)
                            .SetParameter("Sharing_M_35_44", _FacebookReport.Sharing_M_35_44)
                            .SetParameter("Sharing_M_45_54", _FacebookReport.Sharing_M_45_54)
                            .SetParameter("Sharing_M_55_64", _FacebookReport.Sharing_M_55_64)
                            .SetParameter("Sharing_M_65", _FacebookReport.Sharing_M_65)
                            .SetParameter("Sharing_F_13_17", _FacebookReport.Sharing_F_13_17)
                            .SetParameter("Sharing_F_18_24", _FacebookReport.Sharing_F_18_24)
                            .SetParameter("Sharing_F_25_34", _FacebookReport.Sharing_F_25_34)
                            .SetParameter("Sharing_F_35_44", _FacebookReport.Sharing_F_35_44)
                            .SetParameter("Sharing_F_45_54", _FacebookReport.Sharing_F_45_54)
                            .SetParameter("Sharing_F_55_64", _FacebookReport.Sharing_F_55_64)
                            .SetParameter("Sharing_F_65", _FacebookReport.Sharing_F_65)
                            .SetParameter("total_fb_accounts",_FacebookReport.total_fb_accounts)
                            .SetParameter("GroupId", _FacebookReport.GroupId)
                             .ExecuteUpdate();
                        transaction.Commit();

                    }

                        else
                        {
                            session1.Save(_FacebookReport);
                            transaction.Commit();
                        }        
                    }
                }//End Using trasaction
             

            
        }



        
        public string total_fb_accounts(string groupid, string userid)
        {
            List<Domain.Socioboard.Domain.Team> teams = new List<Domain.Socioboard.Domain.Team>();
            List<Domain.Socioboard.Domain.GroupProfile> teammemberprofiles = new List<Domain.Socioboard.Domain.GroupProfile>();
            string ret_string = string.Empty;

            int i = 0;
            try
            {
                //using (NHibernate.ISession session1 = SessionFactory.GetNewSession())
                //{
                //    teams = session1.CreateQuery("from Team t where t.GroupId = : groupid and t.UserId = : userid ").SetParameter("groupid", Guid.Parse(groupid)).SetParameter("userid", Guid.Parse(userid)).List<Domain.Socioboard.Domain.Team>().ToList();

                //}
                //foreach (Domain.Socioboard.Domain.Team team in teams)
                //{
                    using (NHibernate.ISession session2 = SessionFactory.GetNewSession())
                    {

                        teammemberprofiles = session2.CreateQuery("from GroupProfile t where t.GroupId = : GroupId and t.ProfileType =: ProfileType").SetParameter("ProfileType", "facebook_page").SetParameter("GroupId", groupid).List<Domain.Socioboard.Domain.GroupProfile>().ToList();
                        foreach (Domain.Socioboard.Domain.GroupProfile _TeamMemberProfile in teammemberprofiles)
                        {

                            Domain.Socioboard.Domain.FacebookAccount objFacebookAccount = _FacebookAccountRepository.getFacebookAccountDetailsById(_TeamMemberProfile.ProfileId, Guid.Parse(userid));
                            if (!string.IsNullOrEmpty(objFacebookAccount.AccessToken))
                            {
                                i++;
                            }
                        }
                    }

                //}

                ret_string = i.ToString();


            }
            catch (Exception e)
            {
               
            }

            return ret_string;


        }



        public void insert_30(Domain.Socioboard.Domain.FacebookGroupReport_30 _FacebookReport)
        {
            bool exist = false;
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                exist = session.Query<Domain.Socioboard.Domain.FacebookGroupReport_30>()
                            .Any(x => x.GroupId == _FacebookReport.GroupId);
            }


            using (NHibernate.ISession session1 = SessionFactory.GetNewSession())
            {
                //After Session creation, start and open Transaction. 
                using (NHibernate.ITransaction transaction = session1.BeginTransaction())
                {
                    if (exist)
                    {
                        //Proceed action to save data.
                        int i = session1.CreateQuery("update FacebookGroupReport_30 set TotalLikes =:TotalLikes,TalkingAbout=:TalkingAbout,Likes=:Likes,PerDayLikes=:PerDayLikes,Unlikes=:Unlikes,PerDayUnlikes=:PerDayUnlikes,Impression=:Impression,PerDayImpression=:PerDayImpression,UniqueUser=:UniqueUser,StoryShare=:StoryShare,PerDayStoryShare=:PerDayStoryShare,ImpressionFans=:ImpressionFans,ImpressionPagePost=:ImpressionPagePost,ImpressionuserPost=:ImpressionuserPost,ImpressionCoupn=:ImpressionCoupn,ImpressionOther=:ImpressionOther,ImpressionMention=:ImpressionMention,ImpressionCheckin=:ImpressionCheckin,ImpressionQuestion=:ImpressionQuestion,ImpressionEvent=:ImpressionEvent,Organic=:Organic,Viral=:Viral,Paid=:Paid,M_13_17=:M_13_17,M_18_24=:M_18_24,M_25_34=:M_25_34,M_35_44=:M_35_44,M_45_54=:M_45_54,M_55_64=:M_55_64,M_65=:M_65,F_13_17=:F_13_17,F_18_24=:F_18_24,F_25_34=:F_25_34,F_35_44=:F_35_44,F_45_54=:F_45_54,F_55_64=:F_55_64,F_65=:F_65,Sharing_M_13_17=:Sharing_M_13_17,Sharing_M_18_24=:Sharing_M_18_24,Sharing_M_25_34=:Sharing_M_25_34,Sharing_M_35_44=:Sharing_M_35_44,Sharing_M_45_54=:Sharing_M_45_54,Sharing_M_55_64=:Sharing_M_55_64,Sharing_M_65=:Sharing_M_65,Sharing_F_13_17=:Sharing_F_13_17,Sharing_F_18_24=:Sharing_F_18_24,Sharing_F_25_34=:Sharing_F_25_34,Sharing_F_35_44=:Sharing_F_35_44,Sharing_F_45_54=:Sharing_F_45_54,Sharing_F_55_64=:Sharing_F_55_64,Sharing_F_65=:Sharing_F_65, total_fb_accounts = : total_fb_accounts where GroupId =:GroupId")
                        .SetParameter("TotalLikes", _FacebookReport.TotalLikes)
                        .SetParameter("TalkingAbout", _FacebookReport.TalkingAbout)
                        .SetParameter("Likes", _FacebookReport.Likes)
                        .SetParameter("PerDayLikes", _FacebookReport.PerDayLikes)
                        .SetParameter("Unlikes", _FacebookReport.Unlikes)
                        .SetParameter("PerDayUnlikes", _FacebookReport.PerDayUnlikes)
                        .SetParameter("Impression", _FacebookReport.Impression)
                        .SetParameter("PerDayImpression", _FacebookReport.PerDayImpression)
                        .SetParameter("UniqueUser", _FacebookReport.UniqueUser)
                        .SetParameter("StoryShare", _FacebookReport.StoryShare)
                        .SetParameter("PerDayStoryShare", _FacebookReport.PerDayStoryShare)
                        .SetParameter("ImpressionFans", _FacebookReport.ImpressionFans)
                        .SetParameter("ImpressionPagePost", _FacebookReport.ImpressionPagePost)
                        .SetParameter("ImpressionuserPost", _FacebookReport.ImpressionuserPost)
                        .SetParameter("ImpressionCoupn", _FacebookReport.ImpressionCoupn)
                        .SetParameter("ImpressionOther", _FacebookReport.ImpressionOther)
                        .SetParameter("ImpressionMention", _FacebookReport.ImpressionMention)
                        .SetParameter("ImpressionCheckin", _FacebookReport.ImpressionCheckin)
                        .SetParameter("ImpressionQuestion", _FacebookReport.ImpressionQuestion)
                        .SetParameter("ImpressionEvent", _FacebookReport.ImpressionEvent)
                        .SetParameter("Organic", _FacebookReport.Organic)
                        .SetParameter("Viral", _FacebookReport.Viral)
                        .SetParameter("Paid", _FacebookReport.Paid)
                        .SetParameter("M_13_17", _FacebookReport.M_13_17)
                        .SetParameter("M_18_24", _FacebookReport.M_18_24)
                        .SetParameter("M_25_34", _FacebookReport.M_25_34)
                        .SetParameter("M_35_44", _FacebookReport.M_35_44)
                        .SetParameter("M_45_54", _FacebookReport.M_45_54)
                        .SetParameter("M_55_64", _FacebookReport.M_55_64)
                        .SetParameter("M_65", _FacebookReport.M_65)
                        .SetParameter("F_13_17", _FacebookReport.F_13_17)
                        .SetParameter("F_18_24", _FacebookReport.F_18_24)
                        .SetParameter("F_25_34", _FacebookReport.F_25_34)
                        .SetParameter("F_35_44", _FacebookReport.F_35_44)
                        .SetParameter("F_45_54", _FacebookReport.F_45_54)
                        .SetParameter("F_55_64", _FacebookReport.F_55_64)
                        .SetParameter("F_65", _FacebookReport.F_65)
                        .SetParameter("Sharing_M_13_17", _FacebookReport.Sharing_M_13_17)
                        .SetParameter("Sharing_M_18_24", _FacebookReport.Sharing_M_18_24)
                        .SetParameter("Sharing_M_25_34", _FacebookReport.Sharing_M_25_34)
                        .SetParameter("Sharing_M_35_44", _FacebookReport.Sharing_M_35_44)
                        .SetParameter("Sharing_M_45_54", _FacebookReport.Sharing_M_45_54)
                        .SetParameter("Sharing_M_55_64", _FacebookReport.Sharing_M_55_64)
                        .SetParameter("Sharing_M_65", _FacebookReport.Sharing_M_65)
                        .SetParameter("Sharing_F_13_17", _FacebookReport.Sharing_F_13_17)
                        .SetParameter("Sharing_F_18_24", _FacebookReport.Sharing_F_18_24)
                        .SetParameter("Sharing_F_25_34", _FacebookReport.Sharing_F_25_34)
                        .SetParameter("Sharing_F_35_44", _FacebookReport.Sharing_F_35_44)
                        .SetParameter("Sharing_F_45_54", _FacebookReport.Sharing_F_45_54)
                        .SetParameter("Sharing_F_55_64", _FacebookReport.Sharing_F_55_64)
                        .SetParameter("Sharing_F_65", _FacebookReport.Sharing_F_65)
                        .SetParameter("total_fb_accounts",_FacebookReport.total_fb_accounts)
                        .SetParameter("GroupId", _FacebookReport.GroupId)
                         .ExecuteUpdate();
                        transaction.Commit();

                    }

                    else
                    {
                        session1.Save(_FacebookReport);
                        transaction.Commit();
                    }
                }
            }//End Using trasaction



        }




        public void insert_60(Domain.Socioboard.Domain.FacebookGroupReport_60 _FacebookReport)
        {
            bool exist = false;
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                exist = session.Query<Domain.Socioboard.Domain.FacebookGroupReport_60>()
                            .Any(x => x.GroupId == _FacebookReport.GroupId);
            }


            using (NHibernate.ISession session1 = SessionFactory.GetNewSession())
            {
                //After Session creation, start and open Transaction. 
                using (NHibernate.ITransaction transaction = session1.BeginTransaction())
                {
                    if (exist)
                    {
                        //Proceed action to save data.
                        int i = session1.CreateQuery("update FacebookGroupReport_60 set TotalLikes =:TotalLikes,TalkingAbout=:TalkingAbout,Likes=:Likes,PerDayLikes=:PerDayLikes,Unlikes=:Unlikes,PerDayUnlikes=:PerDayUnlikes,Impression=:Impression,PerDayImpression=:PerDayImpression,UniqueUser=:UniqueUser,StoryShare=:StoryShare,PerDayStoryShare=:PerDayStoryShare,ImpressionFans=:ImpressionFans,ImpressionPagePost=:ImpressionPagePost,ImpressionuserPost=:ImpressionuserPost,ImpressionCoupn=:ImpressionCoupn,ImpressionOther=:ImpressionOther,ImpressionMention=:ImpressionMention,ImpressionCheckin=:ImpressionCheckin,ImpressionQuestion=:ImpressionQuestion,ImpressionEvent=:ImpressionEvent,Organic=:Organic,Viral=:Viral,Paid=:Paid,M_13_17=:M_13_17,M_18_24=:M_18_24,M_25_34=:M_25_34,M_35_44=:M_35_44,M_45_54=:M_45_54,M_55_64=:M_55_64,M_65=:M_65,F_13_17=:F_13_17,F_18_24=:F_18_24,F_25_34=:F_25_34,F_35_44=:F_35_44,F_45_54=:F_45_54,F_55_64=:F_55_64,F_65=:F_65,Sharing_M_13_17=:Sharing_M_13_17,Sharing_M_18_24=:Sharing_M_18_24,Sharing_M_25_34=:Sharing_M_25_34,Sharing_M_35_44=:Sharing_M_35_44,Sharing_M_45_54=:Sharing_M_45_54,Sharing_M_55_64=:Sharing_M_55_64,Sharing_M_65=:Sharing_M_65,Sharing_F_13_17=:Sharing_F_13_17,Sharing_F_18_24=:Sharing_F_18_24,Sharing_F_25_34=:Sharing_F_25_34,Sharing_F_35_44=:Sharing_F_35_44,Sharing_F_45_54=:Sharing_F_45_54,Sharing_F_55_64=:Sharing_F_55_64,Sharing_F_65=:Sharing_F_65 , total_fb_accounts = : total_fb_accounts where GroupId =:GroupId")
                        .SetParameter("TotalLikes", _FacebookReport.TotalLikes)
                        .SetParameter("TalkingAbout", _FacebookReport.TalkingAbout)
                        .SetParameter("Likes", _FacebookReport.Likes)
                        .SetParameter("PerDayLikes", _FacebookReport.PerDayLikes)
                        .SetParameter("Unlikes", _FacebookReport.Unlikes)
                        .SetParameter("PerDayUnlikes", _FacebookReport.PerDayUnlikes)
                        .SetParameter("Impression", _FacebookReport.Impression)
                        .SetParameter("PerDayImpression", _FacebookReport.PerDayImpression)
                        .SetParameter("UniqueUser", _FacebookReport.UniqueUser)
                        .SetParameter("StoryShare", _FacebookReport.StoryShare)
                        .SetParameter("PerDayStoryShare", _FacebookReport.PerDayStoryShare)
                        .SetParameter("ImpressionFans", _FacebookReport.ImpressionFans)
                        .SetParameter("ImpressionPagePost", _FacebookReport.ImpressionPagePost)
                        .SetParameter("ImpressionuserPost", _FacebookReport.ImpressionuserPost)
                        .SetParameter("ImpressionCoupn", _FacebookReport.ImpressionCoupn)
                        .SetParameter("ImpressionOther", _FacebookReport.ImpressionOther)
                        .SetParameter("ImpressionMention", _FacebookReport.ImpressionMention)
                        .SetParameter("ImpressionCheckin", _FacebookReport.ImpressionCheckin)
                        .SetParameter("ImpressionQuestion", _FacebookReport.ImpressionQuestion)
                        .SetParameter("ImpressionEvent", _FacebookReport.ImpressionEvent)
                        .SetParameter("Organic", _FacebookReport.Organic)
                        .SetParameter("Viral", _FacebookReport.Viral)
                        .SetParameter("Paid", _FacebookReport.Paid)
                        .SetParameter("M_13_17", _FacebookReport.M_13_17)
                        .SetParameter("M_18_24", _FacebookReport.M_18_24)
                        .SetParameter("M_25_34", _FacebookReport.M_25_34)
                        .SetParameter("M_35_44", _FacebookReport.M_35_44)
                        .SetParameter("M_45_54", _FacebookReport.M_45_54)
                        .SetParameter("M_55_64", _FacebookReport.M_55_64)
                        .SetParameter("M_65", _FacebookReport.M_65)
                        .SetParameter("F_13_17", _FacebookReport.F_13_17)
                        .SetParameter("F_18_24", _FacebookReport.F_18_24)
                        .SetParameter("F_25_34", _FacebookReport.F_25_34)
                        .SetParameter("F_35_44", _FacebookReport.F_35_44)
                        .SetParameter("F_45_54", _FacebookReport.F_45_54)
                        .SetParameter("F_55_64", _FacebookReport.F_55_64)
                        .SetParameter("F_65", _FacebookReport.F_65)
                        .SetParameter("Sharing_M_13_17", _FacebookReport.Sharing_M_13_17)
                        .SetParameter("Sharing_M_18_24", _FacebookReport.Sharing_M_18_24)
                        .SetParameter("Sharing_M_25_34", _FacebookReport.Sharing_M_25_34)
                        .SetParameter("Sharing_M_35_44", _FacebookReport.Sharing_M_35_44)
                        .SetParameter("Sharing_M_45_54", _FacebookReport.Sharing_M_45_54)
                        .SetParameter("Sharing_M_55_64", _FacebookReport.Sharing_M_55_64)
                        .SetParameter("Sharing_M_65", _FacebookReport.Sharing_M_65)
                        .SetParameter("Sharing_F_13_17", _FacebookReport.Sharing_F_13_17)
                        .SetParameter("Sharing_F_18_24", _FacebookReport.Sharing_F_18_24)
                        .SetParameter("Sharing_F_25_34", _FacebookReport.Sharing_F_25_34)
                        .SetParameter("Sharing_F_35_44", _FacebookReport.Sharing_F_35_44)
                        .SetParameter("Sharing_F_45_54", _FacebookReport.Sharing_F_45_54)
                        .SetParameter("Sharing_F_55_64", _FacebookReport.Sharing_F_55_64)
                        .SetParameter("Sharing_F_65", _FacebookReport.Sharing_F_65)
                         .SetParameter("total_fb_accounts", _FacebookReport.total_fb_accounts)
                       
                        .SetParameter("GroupId", _FacebookReport.GroupId)
                         .ExecuteUpdate();
                        transaction.Commit();

                    }

                    else
                    {
                        session1.Save(_FacebookReport);
                        transaction.Commit();
                    }
                }
            }//End Using trasaction



        }



        public void insert_90(Domain.Socioboard.Domain.FacebookGroupReport_90 _FacebookReport)
        {
            bool exist = false;
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                exist = session.Query<Domain.Socioboard.Domain.FacebookGroupReport_90>()
                            .Any(x => x.GroupId == _FacebookReport.GroupId);
            }


            using (NHibernate.ISession session1 = SessionFactory.GetNewSession())
            {
                //After Session creation, start and open Transaction. 
                using (NHibernate.ITransaction transaction = session1.BeginTransaction())
                {
                    if (exist)
                    {
                        //Proceed action to save data.
                        int i = session1.CreateQuery("update FacebookGroupReport_90 set TotalLikes =:TotalLikes,TalkingAbout=:TalkingAbout,Likes=:Likes,PerDayLikes=:PerDayLikes,Unlikes=:Unlikes,PerDayUnlikes=:PerDayUnlikes,Impression=:Impression,PerDayImpression=:PerDayImpression,UniqueUser=:UniqueUser,StoryShare=:StoryShare,PerDayStoryShare=:PerDayStoryShare,ImpressionFans=:ImpressionFans,ImpressionPagePost=:ImpressionPagePost,ImpressionuserPost=:ImpressionuserPost,ImpressionCoupn=:ImpressionCoupn,ImpressionOther=:ImpressionOther,ImpressionMention=:ImpressionMention,ImpressionCheckin=:ImpressionCheckin,ImpressionQuestion=:ImpressionQuestion,ImpressionEvent=:ImpressionEvent,Organic=:Organic,Viral=:Viral,Paid=:Paid,M_13_17=:M_13_17,M_18_24=:M_18_24,M_25_34=:M_25_34,M_35_44=:M_35_44,M_45_54=:M_45_54,M_55_64=:M_55_64,M_65=:M_65,F_13_17=:F_13_17,F_18_24=:F_18_24,F_25_34=:F_25_34,F_35_44=:F_35_44,F_45_54=:F_45_54,F_55_64=:F_55_64,F_65=:F_65,Sharing_M_13_17=:Sharing_M_13_17,Sharing_M_18_24=:Sharing_M_18_24,Sharing_M_25_34=:Sharing_M_25_34,Sharing_M_35_44=:Sharing_M_35_44,Sharing_M_45_54=:Sharing_M_45_54,Sharing_M_55_64=:Sharing_M_55_64,Sharing_M_65=:Sharing_M_65,Sharing_F_13_17=:Sharing_F_13_17,Sharing_F_18_24=:Sharing_F_18_24,Sharing_F_25_34=:Sharing_F_25_34,Sharing_F_35_44=:Sharing_F_35_44,Sharing_F_45_54=:Sharing_F_45_54,Sharing_F_55_64=:Sharing_F_55_64,Sharing_F_65=:Sharing_F_65 , total_fb_accounts = : total_fb_accounts where GroupId =:GroupId")
                        .SetParameter("TotalLikes", _FacebookReport.TotalLikes)
                        .SetParameter("TalkingAbout", _FacebookReport.TalkingAbout)
                        .SetParameter("Likes", _FacebookReport.Likes)
                        .SetParameter("PerDayLikes", _FacebookReport.PerDayLikes)
                        .SetParameter("Unlikes", _FacebookReport.Unlikes)
                        .SetParameter("PerDayUnlikes", _FacebookReport.PerDayUnlikes)
                        .SetParameter("Impression", _FacebookReport.Impression)
                        .SetParameter("PerDayImpression", _FacebookReport.PerDayImpression)
                        .SetParameter("UniqueUser", _FacebookReport.UniqueUser)
                        .SetParameter("StoryShare", _FacebookReport.StoryShare)
                        .SetParameter("PerDayStoryShare", _FacebookReport.PerDayStoryShare)
                        .SetParameter("ImpressionFans", _FacebookReport.ImpressionFans)
                        .SetParameter("ImpressionPagePost", _FacebookReport.ImpressionPagePost)
                        .SetParameter("ImpressionuserPost", _FacebookReport.ImpressionuserPost)
                        .SetParameter("ImpressionCoupn", _FacebookReport.ImpressionCoupn)
                        .SetParameter("ImpressionOther", _FacebookReport.ImpressionOther)
                        .SetParameter("ImpressionMention", _FacebookReport.ImpressionMention)
                        .SetParameter("ImpressionCheckin", _FacebookReport.ImpressionCheckin)
                        .SetParameter("ImpressionQuestion", _FacebookReport.ImpressionQuestion)
                        .SetParameter("ImpressionEvent", _FacebookReport.ImpressionEvent)
                        .SetParameter("Organic", _FacebookReport.Organic)
                        .SetParameter("Viral", _FacebookReport.Viral)
                        .SetParameter("Paid", _FacebookReport.Paid)
                        .SetParameter("M_13_17", _FacebookReport.M_13_17)
                        .SetParameter("M_18_24", _FacebookReport.M_18_24)
                        .SetParameter("M_25_34", _FacebookReport.M_25_34)
                        .SetParameter("M_35_44", _FacebookReport.M_35_44)
                        .SetParameter("M_45_54", _FacebookReport.M_45_54)
                        .SetParameter("M_55_64", _FacebookReport.M_55_64)
                        .SetParameter("M_65", _FacebookReport.M_65)
                        .SetParameter("F_13_17", _FacebookReport.F_13_17)
                        .SetParameter("F_18_24", _FacebookReport.F_18_24)
                        .SetParameter("F_25_34", _FacebookReport.F_25_34)
                        .SetParameter("F_35_44", _FacebookReport.F_35_44)
                        .SetParameter("F_45_54", _FacebookReport.F_45_54)
                        .SetParameter("F_55_64", _FacebookReport.F_55_64)
                        .SetParameter("F_65", _FacebookReport.F_65)
                        .SetParameter("Sharing_M_13_17", _FacebookReport.Sharing_M_13_17)
                        .SetParameter("Sharing_M_18_24", _FacebookReport.Sharing_M_18_24)
                        .SetParameter("Sharing_M_25_34", _FacebookReport.Sharing_M_25_34)
                        .SetParameter("Sharing_M_35_44", _FacebookReport.Sharing_M_35_44)
                        .SetParameter("Sharing_M_45_54", _FacebookReport.Sharing_M_45_54)
                        .SetParameter("Sharing_M_55_64", _FacebookReport.Sharing_M_55_64)
                        .SetParameter("Sharing_M_65", _FacebookReport.Sharing_M_65)
                        .SetParameter("Sharing_F_13_17", _FacebookReport.Sharing_F_13_17)
                        .SetParameter("Sharing_F_18_24", _FacebookReport.Sharing_F_18_24)
                        .SetParameter("Sharing_F_25_34", _FacebookReport.Sharing_F_25_34)
                        .SetParameter("Sharing_F_35_44", _FacebookReport.Sharing_F_35_44)
                        .SetParameter("Sharing_F_45_54", _FacebookReport.Sharing_F_45_54)
                        .SetParameter("Sharing_F_55_64", _FacebookReport.Sharing_F_55_64)
                        .SetParameter("Sharing_F_65", _FacebookReport.Sharing_F_65)
                         .SetParameter("total_fb_accounts", _FacebookReport.total_fb_accounts)
                       
                        .SetParameter("GroupId", _FacebookReport.GroupId)
                         .ExecuteUpdate();
                        transaction.Commit();


                    }

                    else
                    {
                        session1.Save(_FacebookReport);
                        transaction.Commit();
                    }
                }
            }//End Using trasaction



        }


        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
       
        public string retrieve_15(string groupid)
        {


            string ret_string = string.Empty;
            Domain.Socioboard.Domain.FacebookGroupReport_15 ret = new Domain.Socioboard.Domain.FacebookGroupReport_15();
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {

                try
                {

                    NHibernate.IQuery retrieve = session.CreateQuery("from FacebookGroupReport_15 where GroupId =: grpid").SetParameter("grpid", Guid.Parse(groupid));
                    ret = (Domain.Socioboard.Domain.FacebookGroupReport_15)retrieve.UniqueResult();
                    if (ret == null)
                    {
                        ret = new Domain.Socioboard.Domain.FacebookGroupReport_15();
                    }
                    ret_string = new JavaScriptSerializer().Serialize(ret);

                }
                catch (Exception e)
                {
                    ret = new Domain.Socioboard.Domain.FacebookGroupReport_15();
                    ret_string = new JavaScriptSerializer().Serialize(ret);
                }

            }
            return ret_string;

        }



        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
       
        public string retrieve_30(string groupid)
        {


            string ret_string = string.Empty;
            Domain.Socioboard.Domain.FacebookGroupReport_30 ret = new Domain.Socioboard.Domain.FacebookGroupReport_30();
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {

                try
                {

                    NHibernate.IQuery retrieve = session.CreateQuery("from FacebookGroupReport_30 where GroupId =: grpid").SetParameter("grpid", Guid.Parse(groupid));
                    ret = (Domain.Socioboard.Domain.FacebookGroupReport_30)retrieve.UniqueResult();
                    if (ret == null)
                    {
                        ret = new Domain.Socioboard.Domain.FacebookGroupReport_30();
                    }
                    ret_string = new JavaScriptSerializer().Serialize(ret);

                }
                catch (Exception e)
                {
                    ret = new Domain.Socioboard.Domain.FacebookGroupReport_30();
                    ret_string = new JavaScriptSerializer().Serialize(ret);
                }

            }
            return ret_string;

        }


        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
       
        public string retrieve_60(string groupid)
        {


            string ret_string = string.Empty;
            Domain.Socioboard.Domain.FacebookGroupReport_60 ret = new Domain.Socioboard.Domain.FacebookGroupReport_60();
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {

                try
                {

                    NHibernate.IQuery retrieve = session.CreateQuery("from FacebookGroupReport_60 where GroupId =: grpid").SetParameter("grpid", Guid.Parse(groupid));
                    ret = (Domain.Socioboard.Domain.FacebookGroupReport_60)retrieve.UniqueResult();
                    if (ret == null)
                    {
                        ret = new Domain.Socioboard.Domain.FacebookGroupReport_60();
                    }
                    ret_string = new JavaScriptSerializer().Serialize(ret);

                }
                catch (Exception e)
                {
                    ret = new Domain.Socioboard.Domain.FacebookGroupReport_60();
                    ret_string = new JavaScriptSerializer().Serialize(ret);
                }

            }
            return ret_string;

        }



        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
       
        public string retrieve_90(string groupid)
        {


            string ret_string = string.Empty;
            Domain.Socioboard.Domain.FacebookGroupReport_90 ret = new Domain.Socioboard.Domain.FacebookGroupReport_90();
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {

                try
                {

                    NHibernate.IQuery retrieve = session.CreateQuery("from FacebookGroupReport_90 where GroupId =: grpid").SetParameter("grpid", Guid.Parse(groupid));
                    ret = (Domain.Socioboard.Domain.FacebookGroupReport_90)retrieve.UniqueResult();
                    if (ret == null)
                    {
                        ret = new Domain.Socioboard.Domain.FacebookGroupReport_90();
                    }
                    ret_string = new JavaScriptSerializer().Serialize(ret);

                }
                catch (Exception e)
                {
                    ret = new Domain.Socioboard.Domain.FacebookGroupReport_90();
                    ret_string = new JavaScriptSerializer().Serialize(ret);
                }

            }
            return ret_string;

        }





    }
}

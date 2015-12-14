using Api.Socioboard.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NHibernate.Linq;

namespace Api.Socioboard.Model
{
    public class FacebookReportRepository
    {
        public void AddfacebookReport_15(Domain.Socioboard.Domain.FacebookReport_15 _FacebookReport_15)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start and open Transaction. 
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    //Proceed action to save data.
                    session.Save(_FacebookReport_15);
                    transaction.Commit();

                }//End Using trasaction
            }//End Using session
        }

        public void AddfacebookReport_30(Domain.Socioboard.Domain.FacebookReport_30 _FacebookReport_30)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start and open Transaction. 
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    //Proceed action to save data.
                    session.Save(_FacebookReport_30);
                    transaction.Commit();

                }//End Using trasaction
            }//End Using session
        }

        public void AddfacebookReport_60(Domain.Socioboard.Domain.FacebookReport_60 _FacebookReport_60)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start and open Transaction. 
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    //Proceed action to save data.
                    session.Save(_FacebookReport_60);
                    transaction.Commit();

                }//End Using trasaction
            }//End Using session
        }

        public void AddfacebookReport_90(Domain.Socioboard.Domain.FacebookReport_90 _FacebookReport_90)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start and open Transaction. 
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    //Proceed action to save data.
                    session.Save(_FacebookReport_90);
                    transaction.Commit();

                }//End Using trasaction
            }//End Using session
        }

        public bool IsReport_90Exists(string ProfileId)
        { 
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                bool exist = session.Query<Domain.Socioboard.Domain.FacebookReport_90>()
                             .Any(x => x.FacebookId == ProfileId);
                return exist;
            }
        }

        public bool IsReport_60Exists(string ProfileId)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                bool exist = session.Query<Domain.Socioboard.Domain.FacebookReport_60>()
                             .Any(x => x.FacebookId == ProfileId);
                return exist;
            }
        }

        public bool IsReport_30Exists(string ProfileId)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                bool exist = session.Query<Domain.Socioboard.Domain.FacebookReport_30>()
                             .Any(x => x.FacebookId == ProfileId);
                return exist;
            }
        }

        public bool IsReport_15Exists(string ProfileId)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                bool exist = session.Query<Domain.Socioboard.Domain.FacebookReport_15>()
                             .Any(x => x.FacebookId == ProfileId);
                return exist;
            }
        }
        public int UpdatefacebookReport_90(Domain.Socioboard.Domain.FacebookReport_90 _FacebookReport)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start and open Transaction. 
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    //Proceed action to save data.
                    int i = session.CreateQuery("update FacebookReport_90 set TotalLikes =:TotalLikes,TalkingAbout=:TalkingAbout,Likes=:Likes,PerDayLikes=:PerDayLikes,Unlikes=:Unlikes,PerDayUnlikes=:PerDayUnlikes,Impression=:Impression,PerDayImpression=:PerDayImpression,UniqueUser=:UniqueUser,StoryShare=:StoryShare,PerDayStoryShare=:PerDayStoryShare,ImpressionFans=:ImpressionFans,ImpressionPagePost=:ImpressionPagePost,ImpressionuserPost=:ImpressionuserPost,ImpressionCoupn=:ImpressionCoupn,ImpressionOther=:ImpressionOther,ImpressionMention=:ImpressionMention,ImpressionCheckin=:ImpressionCheckin,ImpressionQuestion=:ImpressionQuestion,ImpressionEvent=:ImpressionEvent,Organic=:Organic,Viral=:Viral,Paid=:Paid,M_13_17=:M_13_17,M_18_24=:M_18_24,M_25_34=:M_25_34,M_35_44=:M_35_44,M_45_54=:M_45_54,M_55_64=:M_55_64,M_65=:M_65,F_13_17=:F_13_17,F_18_24=:F_18_24,F_25_34=:F_25_34,F_35_44=:F_35_44,F_45_54=:F_45_54,F_55_64=:F_55_64,F_65=:F_65,Sharing_M_13_17=:Sharing_M_13_17,Sharing_M_18_24=:Sharing_M_18_24,Sharing_M_25_34=:Sharing_M_25_34,Sharing_M_35_44=:Sharing_M_35_44,Sharing_M_45_54=:Sharing_M_45_54,Sharing_M_55_64=:Sharing_M_55_64,Sharing_M_65=:Sharing_M_65,Sharing_F_13_17=:Sharing_F_13_17,Sharing_F_18_24=:Sharing_F_18_24,Sharing_F_25_34=:Sharing_F_25_34,Sharing_F_35_44=:Sharing_F_35_44,Sharing_F_45_54=:Sharing_F_45_54,Sharing_F_55_64=:Sharing_F_55_64,Sharing_F_65=:Sharing_F_65,Story_Fans=:Story_Fans,Story_PagePost=:Story_PagePost,Story_Other=:Story_Other,Story_Checkin=:Story_Checkin,Story_Coupon=:Story_Coupon,Story_Event=:Story_Event,Story_Mention=:Story_Mention,Story_Question=:Story_Question,Story_UserPost=:Story_UserPost where FacebookId =:FacebookId")
                        .SetParameter("TotalLikes",_FacebookReport.TotalLikes)
                        .SetParameter("TalkingAbout", _FacebookReport.TalkingAbout)
                        .SetParameter("Likes",_FacebookReport.Likes)
                        .SetParameter("PerDayLikes",_FacebookReport.PerDayLikes)
                        .SetParameter("Unlikes",_FacebookReport.Unlikes)
                        .SetParameter("PerDayUnlikes",_FacebookReport.PerDayUnlikes)
                        .SetParameter("Impression",_FacebookReport.Impression)
                        .SetParameter("PerDayImpression",_FacebookReport.PerDayImpression)
                        .SetParameter("UniqueUser",_FacebookReport.UniqueUser)
                        .SetParameter("StoryShare",_FacebookReport.StoryShare)
                        .SetParameter("PerDayStoryShare",_FacebookReport.PerDayStoryShare)
                        .SetParameter("ImpressionFans",_FacebookReport.ImpressionFans)
                        .SetParameter("ImpressionPagePost",_FacebookReport.ImpressionPagePost)
                        .SetParameter("ImpressionuserPost",_FacebookReport.ImpressionuserPost)
                        .SetParameter("ImpressionCoupn",_FacebookReport.ImpressionCoupn)
                        .SetParameter("ImpressionOther",_FacebookReport.ImpressionOther)
                        .SetParameter("ImpressionMention",_FacebookReport.ImpressionMention)
                        .SetParameter("ImpressionCheckin",_FacebookReport.ImpressionCheckin)
                        .SetParameter("ImpressionQuestion",_FacebookReport.ImpressionQuestion)
                        .SetParameter("ImpressionEvent",_FacebookReport.ImpressionEvent)
                        .SetParameter("Organic",_FacebookReport.Organic)
                        .SetParameter("Viral",_FacebookReport.Viral)
                        .SetParameter("Paid",_FacebookReport.Paid)
                        .SetParameter("M_13_17",_FacebookReport.M_13_17)
                        .SetParameter("M_18_24",_FacebookReport.M_18_24)
                        .SetParameter("M_25_34",_FacebookReport.M_25_34)
                        .SetParameter("M_35_44",_FacebookReport.M_35_44)
                        .SetParameter("M_45_54",_FacebookReport.M_45_54)
                        .SetParameter("M_55_64",_FacebookReport.M_55_64)
                        .SetParameter("M_65",_FacebookReport.M_65)
                        .SetParameter("F_13_17",_FacebookReport.F_13_17)
                        .SetParameter("F_18_24",_FacebookReport.F_18_24)
                        .SetParameter("F_25_34", _FacebookReport.F_25_34)
                        .SetParameter("F_35_44", _FacebookReport.F_35_44)
                        .SetParameter("F_45_54",_FacebookReport.F_45_54)
                        .SetParameter("F_55_64", _FacebookReport.F_55_64)
                        .SetParameter("F_65",_FacebookReport.F_65)
                        .SetParameter("Sharing_M_13_17",_FacebookReport.Sharing_M_13_17)
                        .SetParameter("Sharing_M_18_24",_FacebookReport.Sharing_M_18_24)
                        .SetParameter("Sharing_M_25_34",_FacebookReport.Sharing_M_25_34)
                        .SetParameter("Sharing_M_35_44",_FacebookReport.Sharing_M_35_44)
                        .SetParameter("Sharing_M_45_54",_FacebookReport.Sharing_M_45_54)
                        .SetParameter("Sharing_M_55_64",_FacebookReport.Sharing_M_55_64)
                        .SetParameter("Sharing_M_65",_FacebookReport.Sharing_M_65)
                        .SetParameter("Sharing_F_13_17",_FacebookReport.Sharing_F_13_17)
                        .SetParameter("Sharing_F_18_24",_FacebookReport.Sharing_F_18_24)
                        .SetParameter("Sharing_F_25_34",_FacebookReport.Sharing_F_25_34)
                        .SetParameter("Sharing_F_35_44",_FacebookReport.Sharing_F_35_44)
                        .SetParameter("Sharing_F_45_54",_FacebookReport.Sharing_F_45_54)
                        .SetParameter("Sharing_F_55_64",_FacebookReport.Sharing_F_55_64)
                        .SetParameter("Sharing_F_65",_FacebookReport.Sharing_F_65)
                        .SetParameter("Story_Fans", _FacebookReport.Story_Fans)
                        .SetParameter("Story_PagePost", _FacebookReport.Story_PagePost)
                        .SetParameter("Story_Other", _FacebookReport.Story_Other)
                        .SetParameter("Story_Checkin", _FacebookReport.Story_Checkin)
                        .SetParameter("Story_Coupon", _FacebookReport.Story_Coupon)
                        .SetParameter("Story_Event", _FacebookReport.Story_Event)
                        .SetParameter("Story_Mention", _FacebookReport.Story_Mention)
                        .SetParameter("Story_Question", _FacebookReport.Story_Question)
                        .SetParameter("Story_UserPost", _FacebookReport.Story_UserPost)
                        .SetParameter("FacebookId",_FacebookReport.FacebookId)
                         .ExecuteUpdate();
                    transaction.Commit();
                    return i;
                }//End Using trasaction
            }//End Using session
        }

        public int UpdatefacebookReport_60(Domain.Socioboard.Domain.FacebookReport_60 _FacebookReport)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start and open Transaction. 
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    //Proceed action to save data.
                    int i = session.CreateQuery("update FacebookReport_60 set TotalLikes =:TotalLikes,TalkingAbout=:TalkingAbout,Likes=:Likes,PerDayLikes=:PerDayLikes,Unlikes=:Unlikes,PerDayUnlikes=:PerDayUnlikes,Impression=:Impression,PerDayImpression=:PerDayImpression,UniqueUser=:UniqueUser,StoryShare=:StoryShare,PerDayStoryShare=:PerDayStoryShare,ImpressionFans=:ImpressionFans,ImpressionPagePost=:ImpressionPagePost,ImpressionuserPost=:ImpressionuserPost,ImpressionCoupn=:ImpressionCoupn,ImpressionOther=:ImpressionOther,ImpressionMention=:ImpressionMention,ImpressionCheckin=:ImpressionCheckin,ImpressionQuestion=:ImpressionQuestion,ImpressionEvent=:ImpressionEvent,Organic=:Organic,Viral=:Viral,Paid=:Paid,M_13_17=:M_13_17,M_18_24=:M_18_24,M_25_34=:M_25_34,M_35_44=:M_35_44,M_45_54=:M_45_54,M_55_64=:M_55_64,M_65=:M_65,F_13_17=:F_13_17,F_18_24=:F_18_24,F_25_34=:F_25_34,F_35_44=:F_35_44,F_45_54=:F_45_54,F_55_64=:F_55_64,F_65=:F_65,Sharing_M_13_17=:Sharing_M_13_17,Sharing_M_18_24=:Sharing_M_18_24,Sharing_M_25_34=:Sharing_M_25_34,Sharing_M_35_44=:Sharing_M_35_44,Sharing_M_45_54=:Sharing_M_45_54,Sharing_M_55_64=:Sharing_M_55_64,Sharing_M_65=:Sharing_M_65,Sharing_F_13_17=:Sharing_F_13_17,Sharing_F_18_24=:Sharing_F_18_24,Sharing_F_25_34=:Sharing_F_25_34,Sharing_F_35_44=:Sharing_F_35_44,Sharing_F_45_54=:Sharing_F_45_54,Sharing_F_55_64=:Sharing_F_55_64,Sharing_F_65=:Sharing_F_65,Story_Fans=:Story_Fans,Story_PagePost=:Story_PagePost,Story_Other=:Story_Other,Story_Checkin=:Story_Checkin,Story_Coupon=:Story_Coupon,Story_Event=:Story_Event,Story_Mention=:Story_Mention,Story_Question=:Story_Question,Story_UserPost=:Story_UserPost where FacebookId =:FacebookId")
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
                        .SetParameter("Story_Fans", _FacebookReport.Story_Fans)
                        .SetParameter("Story_PagePost", _FacebookReport.Story_PagePost)
                        .SetParameter("Story_Other", _FacebookReport.Story_Other)
                        .SetParameter("Story_Checkin", _FacebookReport.Story_Checkin)
                        .SetParameter("Story_Coupon", _FacebookReport.Story_Coupon)
                        .SetParameter("Story_Event", _FacebookReport.Story_Event)
                        .SetParameter("Story_Mention", _FacebookReport.Story_Mention)
                        .SetParameter("Story_Question", _FacebookReport.Story_Question)
                        .SetParameter("Story_UserPost", _FacebookReport.Story_UserPost)
                        .SetParameter("FacebookId", _FacebookReport.FacebookId)
                         .ExecuteUpdate();
                    transaction.Commit();
                    return i;
                }//End Using trasaction
            }//End Using session
        }

        public int UpdatefacebookReport_30(Domain.Socioboard.Domain.FacebookReport_30 _FacebookReport)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start and open Transaction. 
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    //Proceed action to save data.
                    int i = session.CreateQuery("update FacebookReport_30 set TotalLikes =:TotalLikes,TalkingAbout=:TalkingAbout,Likes=:Likes,PerDayLikes=:PerDayLikes,Unlikes=:Unlikes,PerDayUnlikes=:PerDayUnlikes,Impression=:Impression,PerDayImpression=:PerDayImpression,UniqueUser=:UniqueUser,StoryShare=:StoryShare,PerDayStoryShare=:PerDayStoryShare,ImpressionFans=:ImpressionFans,ImpressionPagePost=:ImpressionPagePost,ImpressionuserPost=:ImpressionuserPost,ImpressionCoupn=:ImpressionCoupn,ImpressionOther=:ImpressionOther,ImpressionMention=:ImpressionMention,ImpressionCheckin=:ImpressionCheckin,ImpressionQuestion=:ImpressionQuestion,ImpressionEvent=:ImpressionEvent,Organic=:Organic,Viral=:Viral,Paid=:Paid,M_13_17=:M_13_17,M_18_24=:M_18_24,M_25_34=:M_25_34,M_35_44=:M_35_44,M_45_54=:M_45_54,M_55_64=:M_55_64,M_65=:M_65,F_13_17=:F_13_17,F_18_24=:F_18_24,F_25_34=:F_25_34,F_35_44=:F_35_44,F_45_54=:F_45_54,F_55_64=:F_55_64,F_65=:F_65,Sharing_M_13_17=:Sharing_M_13_17,Sharing_M_18_24=:Sharing_M_18_24,Sharing_M_25_34=:Sharing_M_25_34,Sharing_M_35_44=:Sharing_M_35_44,Sharing_M_45_54=:Sharing_M_45_54,Sharing_M_55_64=:Sharing_M_55_64,Sharing_M_65=:Sharing_M_65,Sharing_F_13_17=:Sharing_F_13_17,Sharing_F_18_24=:Sharing_F_18_24,Sharing_F_25_34=:Sharing_F_25_34,Sharing_F_35_44=:Sharing_F_35_44,Sharing_F_45_54=:Sharing_F_45_54,Sharing_F_55_64=:Sharing_F_55_64,Sharing_F_65=:Sharing_F_65,Story_Fans=:Story_Fans,Story_PagePost=:Story_PagePost,Story_Other=:Story_Other,Story_Checkin=:Story_Checkin,Story_Coupon=:Story_Coupon,Story_Event=:Story_Event,Story_Mention=:Story_Mention,Story_Question=:Story_Question,Story_UserPost=:Story_UserPost where FacebookId =:FacebookId")
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
                        .SetParameter("Story_Fans", _FacebookReport.Story_Fans)
                        .SetParameter("Story_PagePost", _FacebookReport.Story_PagePost)
                        .SetParameter("Story_Other", _FacebookReport.Story_Other)
                        .SetParameter("Story_Checkin", _FacebookReport.Story_Checkin)
                        .SetParameter("Story_Coupon", _FacebookReport.Story_Coupon)
                        .SetParameter("Story_Event", _FacebookReport.Story_Event)
                        .SetParameter("Story_Mention", _FacebookReport.Story_Mention)
                        .SetParameter("Story_Question", _FacebookReport.Story_Question)
                        .SetParameter("Story_UserPost", _FacebookReport.Story_UserPost)
                        .SetParameter("FacebookId", _FacebookReport.FacebookId)
                         .ExecuteUpdate();
                    transaction.Commit();
                    return i;
                }//End Using trasaction
            }//End Using session
        }

        public int UpdatefacebookReport_15(Domain.Socioboard.Domain.FacebookReport_15 _FacebookReport)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start and open Transaction. 
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    //Proceed action to save data.
                    int i = session.CreateQuery("update FacebookReport_15 set TotalLikes =:TotalLikes,TalkingAbout=:TalkingAbout,Likes=:Likes,PerDayLikes=:PerDayLikes,Unlikes=:Unlikes,PerDayUnlikes=:PerDayUnlikes,Impression=:Impression,PerDayImpression=:PerDayImpression,UniqueUser=:UniqueUser,StoryShare=:StoryShare,PerDayStoryShare=:PerDayStoryShare,ImpressionFans=:ImpressionFans,ImpressionPagePost=:ImpressionPagePost,ImpressionuserPost=:ImpressionuserPost,ImpressionCoupn=:ImpressionCoupn,ImpressionOther=:ImpressionOther,ImpressionMention=:ImpressionMention,ImpressionCheckin=:ImpressionCheckin,ImpressionQuestion=:ImpressionQuestion,ImpressionEvent=:ImpressionEvent,Organic=:Organic,Viral=:Viral,Paid=:Paid,M_13_17=:M_13_17,M_18_24=:M_18_24,M_25_34=:M_25_34,M_35_44=:M_35_44,M_45_54=:M_45_54,M_55_64=:M_55_64,M_65=:M_65,F_13_17=:F_13_17,F_18_24=:F_18_24,F_25_34=:F_25_34,F_35_44=:F_35_44,F_45_54=:F_45_54,F_55_64=:F_55_64,F_65=:F_65,Sharing_M_13_17=:Sharing_M_13_17,Sharing_M_18_24=:Sharing_M_18_24,Sharing_M_25_34=:Sharing_M_25_34,Sharing_M_35_44=:Sharing_M_35_44,Sharing_M_45_54=:Sharing_M_45_54,Sharing_M_55_64=:Sharing_M_55_64,Sharing_M_65=:Sharing_M_65,Sharing_F_13_17=:Sharing_F_13_17,Sharing_F_18_24=:Sharing_F_18_24,Sharing_F_25_34=:Sharing_F_25_34,Sharing_F_35_44=:Sharing_F_35_44,Sharing_F_45_54=:Sharing_F_45_54,Sharing_F_55_64=:Sharing_F_55_64,Sharing_F_65=:Sharing_F_65,Story_Fans=:Story_Fans,Story_PagePost=:Story_PagePost,Story_Other=:Story_Other,Story_Checkin=:Story_Checkin,Story_Coupon=:Story_Coupon,Story_Event=:Story_Event,Story_Mention=:Story_Mention,Story_Question=:Story_Question,Story_UserPost=:Story_UserPost where FacebookId =:FacebookId")
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
                        .SetParameter("Story_Fans", _FacebookReport.Story_Fans)
                        .SetParameter("Story_PagePost", _FacebookReport.Story_PagePost)
                        .SetParameter("Story_Other", _FacebookReport.Story_Other)
                        .SetParameter("Story_Checkin", _FacebookReport.Story_Checkin)
                        .SetParameter("Story_Coupon", _FacebookReport.Story_Coupon)
                        .SetParameter("Story_Event", _FacebookReport.Story_Event)
                        .SetParameter("Story_Mention", _FacebookReport.Story_Mention)
                        .SetParameter("Story_Question", _FacebookReport.Story_Question)
                        .SetParameter("Story_UserPost", _FacebookReport.Story_UserPost)
                        .SetParameter("FacebookId", _FacebookReport.FacebookId)
                         .ExecuteUpdate();
                    transaction.Commit();
                    return i;

                }//End Using trasaction
            }//End Using session
        }

        public Domain.Socioboard.Domain.FacebookReport_15 GetFacebookReport_15(string FbProfileId)
        { 
            Domain.Socioboard.Domain.FacebookReport_15 fbreport = new Domain.Socioboard.Domain.FacebookReport_15();
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                try
                {
                    NHibernate.IQuery result = session.CreateQuery("from FacebookReport_15 f where FacebookId = : FacebookId").SetParameter("FacebookId", FbProfileId);
                    fbreport = (Domain.Socioboard.Domain.FacebookReport_15)result.UniqueResult();
                    if (fbreport == null)
                    {
                        fbreport = new Domain.Socioboard.Domain.FacebookReport_15();
                    }
                }
                catch (Exception ex)
                {
                    fbreport = new Domain.Socioboard.Domain.FacebookReport_15();
                }

            }

            return fbreport;

        }


        public Domain.Socioboard.Domain.FacebookReport_30 GetFacebookReport_30(string FbProfileId)
        {
            Domain.Socioboard.Domain.FacebookReport_30 fbreport = new Domain.Socioboard.Domain.FacebookReport_30();
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                try
                {
                    NHibernate.IQuery result = session.CreateQuery("from FacebookReport_30 f where FacebookId = : FacebookId").SetParameter("FacebookId", FbProfileId);
                    fbreport = (Domain.Socioboard.Domain.FacebookReport_30)result.UniqueResult();
                    if (fbreport == null)
                    {
                        fbreport = new Domain.Socioboard.Domain.FacebookReport_30();
                    }
                }
                catch (Exception ex)
                {
                    fbreport = new Domain.Socioboard.Domain.FacebookReport_30();
                }

            }

            return fbreport;

        }



        public Domain.Socioboard.Domain.FacebookReport_60 GetFacebookReport_60(string FbProfileId)
        {
            Domain.Socioboard.Domain.FacebookReport_60 fbreport = new Domain.Socioboard.Domain.FacebookReport_60();
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                try
                {
                    NHibernate.IQuery result = session.CreateQuery("from FacebookReport_60 f where FacebookId = : FacebookId").SetParameter("FacebookId", FbProfileId);
                    fbreport = (Domain.Socioboard.Domain.FacebookReport_60)result.UniqueResult();
                    if (fbreport == null)
                    {
                        fbreport = new Domain.Socioboard.Domain.FacebookReport_60();
                    }
                }
                catch (Exception ex)
                {
                    fbreport = new Domain.Socioboard.Domain.FacebookReport_60();
                }

            }

            return fbreport;

        }



        public Domain.Socioboard.Domain.FacebookReport_90 GetFacebookReport_90(string FbProfileId)
        {
            Domain.Socioboard.Domain.FacebookReport_90 fbreport = new Domain.Socioboard.Domain.FacebookReport_90();
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                    try
                    {
                        NHibernate.IQuery result = session.CreateQuery("from FacebookReport_90 f where FacebookId = : FacebookId").SetParameter("FacebookId", FbProfileId);
                        fbreport = (Domain.Socioboard.Domain.FacebookReport_90)result.UniqueResult();
                        if (fbreport == null)
                        {
                            fbreport = new Domain.Socioboard.Domain.FacebookReport_90();
                        }
                    }
                    catch (Exception ex)
                    {
                        fbreport = new Domain.Socioboard.Domain.FacebookReport_90();
                    }

            }

            return fbreport;

        }




    }
}
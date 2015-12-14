using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Socioboard.Domain;
using System.Web.Script.Serialization;
using ReportDataServices.Models;
using System.Threading;
namespace ReportDataServices
{
    class Program
    {
        
        public static void Main(string[] args)
        {
            SentimentalAnalysis _SentimentalAnalysis = new SentimentalAnalysis();
            Console.WriteLine("1. GroupReport");
            Console.WriteLine("2. TwitterReport");
            Console.WriteLine("3. FacebookReport");
            Console.WriteLine("4. InstagramReport");
            Console.WriteLine("5. Message sentiments");
            Console.WriteLine("6. FacebookFeed sentiments");
            Console.WriteLine("7. FacebookMessage sentiments");
            Console.WriteLine("8. TwitterFeed sentiments");
            Console.WriteLine("9. TwitterMessage sentiments");
            Console.WriteLine("10. DailyMotionPost");
            string[] str = { Console.ReadLine() };
            string reporttype = str[0];
            string type = string.Empty;
            switch (reporttype)
            {
                case "1":
                    type = "groupreport";
                    break;
                case "2":
                    type = "twitterreport";
                    break;
                case "3":
                    type = "facebookreport";
                    break;
                case "4":
                    type = "instagramreport";
                    break;
                case "5":
                    type = "Messagesentiments";
                    break;
                case "6":
                    type = "FacebookFeedsentiments";
                    break;
                case "7":
                    type = "FacebookMessagesentiments";
                    break;
                case "8":
                    type = "TwitterFeedsentiments";
                    break;
                case "9":
                    type = "TwitterMessagesentiments";
                    break;
                case "10":
                    type = "DailyMotionPost";
                    break;
                default:
                    break;
            }

            if (type == "groupreport")
            {
                while (true)
                {
                    try
                    {
                        groupreports();
                        Thread.Sleep(60 * 1000);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
            }
            else if (type == "twitterreport")
            {
                while (true)
                {
                    try
                    {
                        twitterreport();
                        Thread.Sleep(60 * 1000);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
            }
            else if (type == "facebookreport")
            {
                while (true)
                {
                    try
                    {
                        facebookreport();
                        Thread.Sleep(60 * 1000);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
            }
            else if (type == "instagramreport")
            {
                while (true)
                {
                    try
                    {
                        instagramreport();
                        Thread.Sleep(60 * 1000);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
            }
            else if (type == "Messagesentiments")
            {
                _SentimentalAnalysis.GetSentiments(); 
            
            }
            else if (type == "FacebookFeedsentiments")
            {
                _SentimentalAnalysis.GetFacebookFeedSentiments();
            }
            else if (type == "FacebookMessagesentiments")
            {
                _SentimentalAnalysis.GetFacebookMessageSentiments();
            }
            else if (type == "TwitterFeedsentiments")
            {
                _SentimentalAnalysis.GetTwitterFeedSentiments();
            }
            else if (type == "TwitterMessagesentiments")
            {
                _SentimentalAnalysis.GetTwitterMessageSentiments();
            }
            else if (type == "DailyMotionPost")
            {
                _SentimentalAnalysis.GetDailyMotionData();
            
            }
            
        }

        private static void groupreports()
        {
            Api.GroupReports.GroupReports groups = new Api.GroupReports.GroupReports();
            Api.FacebookGroupReport.FacebookGroupReport ApiFacebookGroupReport = new Api.FacebookGroupReport.FacebookGroupReport();
            List<Domain.Socioboard.Domain.Groups> grouplst = (List<Domain.Socioboard.Domain.Groups>)new JavaScriptSerializer().Deserialize(groups.getgroups(), typeof(List<Domain.Socioboard.Domain.Groups>));
            foreach (Domain.Socioboard.Domain.Groups grpid in grouplst)
            {
                try
                {
                    Console.WriteLine("Hereeee");
                    Domain.Socioboard.Domain.GroupReports insert = new Domain.Socioboard.Domain.GroupReports();
                    insert.Id = Guid.NewGuid();
                    insert.GroupId = grpid.Id;

                    groups.Timeout = -1;
                    Domain.Socioboard.Domain.ReturnData r = (Domain.Socioboard.Domain.ReturnData)new JavaScriptSerializer().Deserialize(groups.getinboxcount(grpid.Id.ToString(), grpid.UserId.ToString()), typeof(Domain.Socioboard.Domain.ReturnData));
                    Console.WriteLine("1");
                    groups.Timeout = -1;

                    Domain.Socioboard.Domain.ReturnData r2 = (Domain.Socioboard.Domain.ReturnData)new JavaScriptSerializer().Deserialize(groups.gettwitterfollowers(grpid.Id.ToString(), grpid.UserId.ToString()), typeof(Domain.Socioboard.Domain.ReturnData));
                    Console.WriteLine("3");
                    groups.Timeout = -1;

                    Domain.Socioboard.Domain.ReturnData r3 = (Domain.Socioboard.Domain.ReturnData)new JavaScriptSerializer().Deserialize(groups.getfbfans(grpid.Id.ToString(), grpid.UserId.ToString()), typeof(Domain.Socioboard.Domain.ReturnData));
                    Console.WriteLine("4");
                    groups.Timeout = -1;

                    Domain.Socioboard.Domain.ReturnData r4 = (Domain.Socioboard.Domain.ReturnData)new JavaScriptSerializer().Deserialize(groups.getinteractions(grpid.Id.ToString(), grpid.UserId.ToString()), typeof(Domain.Socioboard.Domain.ReturnData));
                    Console.WriteLine("5");
                    groups.Timeout = -1;

                    Domain.Socioboard.Domain.ReturnData r1 = (Domain.Socioboard.Domain.ReturnData)new JavaScriptSerializer().Deserialize(groups.getsentmessage(grpid.Id.ToString(), grpid.UserId.ToString()), typeof(Domain.Socioboard.Domain.ReturnData));
                    Console.WriteLine("2");
                    groups.Timeout = -1;

                    Domain.Socioboard.Domain.ReturnData r5 = (Domain.Socioboard.Domain.ReturnData)new JavaScriptSerializer().Deserialize(groups.gettwtmentions(grpid.Id.ToString(), grpid.UserId.ToString()), typeof(Domain.Socioboard.Domain.ReturnData));
                    Console.WriteLine("6");
                    groups.Timeout = -1;

                    Domain.Socioboard.Domain.ReturnData r6 = (Domain.Socioboard.Domain.ReturnData)new JavaScriptSerializer().Deserialize(groups.gettwtretweets(grpid.Id.ToString(), grpid.UserId.ToString()), typeof(Domain.Socioboard.Domain.ReturnData));
                    Console.WriteLine("7");

                    string sexratio = groups.gettwittersexdivision(grpid.Id.ToString(), grpid.UserId.ToString());
                    Console.WriteLine("8");

                    string twitter_account = groups.total_twitter_accounts(grpid.Id.ToString(), grpid.UserId.ToString());


                    insert.inbox_15 = r._15;
                    insert.inbox_30 = r._30;
                    insert.inbox_60 = r._60;
                    insert.inbox_90 = r._90;
                    insert.perday_inbox_15 = r.perday_15;
                    insert.perday_inbox_30 = r.perday_30;
                    insert.perday_inbox_60 = r.perday_60;
                    insert.perday_inbox_90 = r.perday_90;

                    Console.WriteLine("After 2");

                    insert.sent_15 = r1._15;
                    insert.sent_30 = r1._30;
                    insert.sent_60 = r1._60;
                    insert.sent_90 = r1._90;
                    insert.perday_sent_15 = r1.perday_15;
                    insert.perday_sent_30 = r1.perday_30;
                    insert.perday_sent_60 = r1.perday_60;
                    insert.perday_sent_90 = r1.perday_90;

                    insert.twitterfollower_15 = r2._15;
                    insert.twitterfollower_30 = r2._30;
                    insert.twitterfollower_60 = r2._60;
                    insert.twitterfollower_90 = r2._90;
                    insert.perday_twitterfollower_15 = r2.perday_15;
                    insert.perday_twitterfollower_30 = r2.perday_30;
                    insert.perday_twitterfollower_60 = r2.perday_60;
                    insert.perday_twitterfollower_90 = r2.perday_90;


                    insert.fbfan_15 = r3._15;
                    insert.fbfan_30 = r3._30;
                    insert.fbfan_60 = r3._60;
                    insert.fbfan_90 = r3._90;
                    insert.perday_fbfan_15 = r3.perday_15;
                    insert.perday_fbfan_30 = r3.perday_30;
                    insert.perday_fbfan_60 = r3.perday_60;
                    insert.perday_fbfan_90 = r3.perday_90;


                    insert.interaction_15 = r4._15;
                    insert.interaction_30 = r4._30;
                    insert.interaction_60 = r4._60;
                    insert.interaction_90 = r4._90;
                    insert.perday_interaction_15 = r4.perday_15;
                    insert.perday_interaction_30 = r4.perday_30;
                    insert.perday_interaction_60 = r4.perday_60;
                    insert.perday_interaction_90 = r4.perday_90;

                    insert.twtmentions_15 = r5._15;
                    insert.perday_twtmentions_15 = r5.perday_15;

                    insert.twtmentions_30 = r5._30;
                    insert.perday_twtmentions_30 = r5.perday_30;

                    insert.twtmentions_60 = r5._60;
                    insert.perday_twtmentions_60 = r5.perday_60;

                    insert.twtmentions_90 = r5._90;
                    insert.perday_twtmentions_90 = r5.perday_90;

                    insert.twtretweets_15 = r6._15;
                    insert.perday_twtretweets_15 = r6.perday_15;

                    insert.twtretweets_30 = r6._30;
                    insert.perday_twtretweets_30 = r6.perday_30;

                    insert.twtretweets_60 = r6._60;
                    insert.perday_twtretweets_60 = r6.perday_60;

                    insert.twtretweets_90 = r6._90;
                    insert.perday_twtretweets_90 = r6.perday_90;
                    insert.sexratio = sexratio;
                    insert.twitter_account_count = long.Parse(twitter_account);

                    string senddata = new JavaScriptSerializer().Serialize(insert);
                    //    Console.WriteLine("Insert");
                    groups.insertdata(senddata);
                    //      Console.WriteLine("Insert Completed");

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                try
                {
                    ApiFacebookGroupReport.Timeout = -1;
                    ApiFacebookGroupReport.FacebookGroupData(grpid.Id.ToString(), grpid.UserId.ToString());
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }


        }

        private static void twitterreport()
        {
            Api.TwitterReport.TwitterReport profiles = new Api.TwitterReport.TwitterReport();
            List<string> profileids = (List<string>)new JavaScriptSerializer().Deserialize(profiles.getprofileids(), typeof(List<string>));
            string totalfollowers = string.Empty;
            string totalfollowing = string.Empty;
            foreach (string profileid in profileids)
            {

                Domain.Socioboard.Domain.TwitterReport insert = new Domain.Socioboard.Domain.TwitterReport();
                profiles.Timeout = -1;
                try
                {

                    totalfollowers = profiles.gettotalfollowers(profileid);
                    totalfollowing = profiles.gettotalfollowing(profileid);
                    Console.WriteLine("totalfollowers =" + totalfollowers);
                    Console.WriteLine("totalfollowing =" + totalfollowing);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

                profiles.Timeout = -1;

                try
                {
                    Domain.Socioboard.Domain.ReturnData r = (Domain.Socioboard.Domain.ReturnData)(new JavaScriptSerializer().Deserialize(profiles.newfollower(profileid), typeof(Domain.Socioboard.Domain.ReturnData)));
                    Console.WriteLine("1");
                    profiles.Timeout = -1;

                    Domain.Socioboard.Domain.ReturnData r0 = (Domain.Socioboard.Domain.ReturnData)(new JavaScriptSerializer().Deserialize(profiles.youfollowed(profileid), typeof(Domain.Socioboard.Domain.ReturnData)));
                    Console.WriteLine("1");
                    profiles.Timeout = -1;


                    Domain.Socioboard.Domain.ReturnData r1 = (Domain.Socioboard.Domain.ReturnData)(new JavaScriptSerializer().Deserialize(profiles.directmessage(profileid), typeof(Domain.Socioboard.Domain.ReturnData)));
                    Console.WriteLine("2");
                    profiles.Timeout = -1;

                    Domain.Socioboard.Domain.ReturnData r2 = (Domain.Socioboard.Domain.ReturnData)(new JavaScriptSerializer().Deserialize(profiles.directmessagesent(profileid), typeof(Domain.Socioboard.Domain.ReturnData)));
                    Console.WriteLine("3");
                    profiles.Timeout = -1;

                    Domain.Socioboard.Domain.ReturnData r3 = (Domain.Socioboard.Domain.ReturnData)(new JavaScriptSerializer().Deserialize(profiles.twittermention(profileid), typeof(Domain.Socioboard.Domain.ReturnData)));
                    Console.WriteLine("4");
                    profiles.Timeout = -1;

                    Domain.Socioboard.Domain.ReturnData r4 = (Domain.Socioboard.Domain.ReturnData)(new JavaScriptSerializer().Deserialize(profiles.twitterretweets(profileid), typeof(Domain.Socioboard.Domain.ReturnData)));
                    Console.WriteLine("4");
                    profiles.Timeout = -1;

                    Domain.Socioboard.Domain.ReturnData r5 = (Domain.Socioboard.Domain.ReturnData)(new JavaScriptSerializer().Deserialize(profiles.clicks(profileid), typeof(Domain.Socioboard.Domain.ReturnData)));
                    Console.WriteLine("5");
                    profiles.Timeout = -1;

                    Domain.Socioboard.Domain.ReturnData r6 = (Domain.Socioboard.Domain.ReturnData)(new JavaScriptSerializer().Deserialize(profiles.messagerecieved(profileid), typeof(Domain.Socioboard.Domain.ReturnData)));
                    Console.WriteLine("6");
                    profiles.Timeout = -1;

                    Domain.Socioboard.Domain.ReturnData r7 = (Domain.Socioboard.Domain.ReturnData)(new JavaScriptSerializer().Deserialize(profiles.messagesent(profileid), typeof(Domain.Socioboard.Domain.ReturnData)));
                    Console.WriteLine("7");


                    insert.Id = Guid.NewGuid();
                    insert.twitterprofileid = profileid;
                    insert.totalfollower = Int32.Parse(totalfollowers);
                    insert.totalconnection = Int32.Parse(totalfollowing);

                    insert.newfollower_15 = r._15;
                    insert.newfollower_30 = r._30;
                    insert.newfollower_60 = r._60;
                    insert.newfollower_90 = r._90;
                    insert.perday_newfollower_15 = r.perday_15;
                    insert.perday_newfollower_30 = r.perday_30;
                    insert.perday_newfollower_60 = r.perday_60;
                    insert.perday_newfollower_90 = r.perday_90;

                    insert.following_15 = r0._15;
                    insert.following_30 = r0._30;
                    insert.following_60 = r0._60;
                    insert.following_90 = r0._90;
                    insert.perday_following_15 = r0.perday_15;
                    insert.perday_following_30 = r0.perday_30;
                    insert.perday_following_60 = r0.perday_60;
                    insert.perday_following_90 = r0.perday_90;


                    insert.directmessage_15 = r1._15;
                    insert.directmessage_30 = r1._30;
                    insert.directmessage_60 = r1._60;
                    insert.directmessage_90 = r1._90;
                    insert.perday_directmessage_15 = r1.perday_15;
                    insert.perday_directmessage_30 = r1.perday_30;
                    insert.perday_directmessage_60 = r1.perday_60;
                    insert.perday_directmessage_90 = r1.perday_90;

                    insert.directmessagesent_15 = r2._15;
                    insert.directmessagesent_30 = r2._30;
                    insert.directmessagesent_60 = r2._60;
                    insert.directmessagesent_90 = r2._90;
                    insert.perday_directmessagesent_15 = r2.perday_15;
                    insert.perday_directmessagesent_30 = r2.perday_30;
                    insert.perday_directmessagesent_60 = r2.perday_60;
                    insert.perday_directmessagesent_90 = r2.perday_90;


                    insert.mention_15 = r3._15;
                    insert.mention_30 = r3._30;
                    insert.mention_60 = r3._60;
                    insert.mention_90 = r3._90;
                    insert.perday_mention_15 = r3.perday_15;
                    insert.perday_mention_30 = r3.perday_30;
                    insert.perday_mention_60 = r3.perday_60;
                    insert.perday_mention_90 = r3.perday_90;

                    insert.retweets_15 = r4._15;
                    insert.retweets_30 = r4._30;
                    insert.retweets_60 = r4._60;
                    insert.retweets_90 = r4._90;
                    insert.perday_retweets_15 = r4.perday_15;
                    insert.perday_retweets_30 = r4.perday_30;
                    insert.perday_retweets_60 = r4.perday_60;
                    insert.perday_retweets_90 = r4.perday_90;

                    insert.click_15 = r5._15;
                    insert.click_30 = r5._30;
                    insert.click_60 = r5._60;
                    insert.click_90 = r5._90;
                    insert.perday_click_15 = r5.perday_15;
                    insert.perday_click_30 = r5.perday_30;
                    insert.perday_click_60 = r5.perday_60;
                    insert.perday_click_90 = r5.perday_90;

                    insert.messagerecieved_15 = r6._15;
                    insert.messagerecieved_30 = r6._30;
                    insert.messagerecieved_60 = r6._60;
                    insert.messagerecieved_90 = r6._90;
                    insert.perday_messagerecieved_15 = r6.perday_15;
                    insert.perday_messagerecieved_30 = r6.perday_30;
                    insert.perday_messagerecieved_60 = r6.perday_60;
                    insert.perday_messagerecieved_90 = r6.perday_90;

                    insert.messagesent_15 = r7._15;
                    insert.messagesent_30 = r7._30;
                    insert.messagesent_60 = r7._60;
                    insert.messagesent_90 = r7._90;
                    insert.perday_messagesent_15 = r7.perday_15;
                    insert.perday_messagesent_30 = r7.perday_30;
                    insert.perday_messagesent_60 = r7.perday_60;
                    insert.perday_messagesent_90 = r7.perday_90;

                   

                    string senddata = new JavaScriptSerializer().Serialize(insert);
                    //    Console.WriteLine("Insert");
                    profiles.insertdata(senddata);
                    profiles.top_five_fans(profileid);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

                Thread.Sleep(5 * 1000);
            }


        }

        private static void facebookreport()
        {

            List<Domain.Socioboard.Domain.FacebookAccount> lstFacebookAccount = new List<Domain.Socioboard.Domain.FacebookAccount>();
            Api.FacebookAccount.FacebookAccount ApiFacebookAccount = new Api.FacebookAccount.FacebookAccount();
            Api.FacebookReport.FacebookReport ApiFacebookReport = new Api.FacebookReport.FacebookReport();
            lstFacebookAccount = (List<Domain.Socioboard.Domain.FacebookAccount>)new JavaScriptSerializer().Deserialize(ApiFacebookAccount.AllFacebookPageDetails(), typeof(List<Domain.Socioboard.Domain.FacebookAccount>));

            foreach (Domain.Socioboard.Domain.FacebookAccount item in lstFacebookAccount)
            {
                if ((item.Type == "page" || item.Type == "Page") && !string.IsNullOrEmpty(item.AccessToken))
                {
                    ApiFacebookReport.Timeout = -1;
                    try
                    {
                        string ret = ApiFacebookReport.GetFacebookdata(item.FbUserId, item.AccessToken);
                    }
                    catch (Exception ex)
                    {
                    }
                    Thread.Sleep(5 * 1000);
                }
            }
        }

        private static void instagramreport()
        {

            Api.InstagramReports.InstagramReports ApiInstaReportObj = new Api.InstagramReports.InstagramReports();
            Api.Instagram.Instagram ApiInstaObj = new Api.Instagram.Instagram();

            Domain.Socioboard.Domain.InstagramReport insert = new Domain.Socioboard.Domain.InstagramReport();
            List<Domain.Socioboard.Domain.InstagramAccount> accounts = (List<Domain.Socioboard.Domain.InstagramAccount>)new JavaScriptSerializer().Deserialize(ApiInstaObj.getInstaAccounts(), typeof(List<Domain.Socioboard.Domain.InstagramAccount>));
            foreach (Domain.Socioboard.Domain.InstagramAccount account in accounts)
            {
                try
                {

                    Guid Id = Guid.NewGuid();
                    string Profile_Id = account.InstagramId;
                    Domain.Socioboard.Domain.InstagramUserDetails user_details = (Domain.Socioboard.Domain.InstagramUserDetails)new JavaScriptSerializer().Deserialize(ApiInstaReportObj.InstagramUserDetail(account.InstagramId), typeof(Domain.Socioboard.Domain.InstagramUserDetails));
                    string Insta_Name = user_details.Insta_Name;
                    string Full_Name = user_details.Full_Name;
                    string Media_count = user_details.Media_Count;
                    string Follower = user_details.Follower;
                    string Following = user_details.Following;
                    ApiInstaReportObj.Timeout = -1;
                    Domain.Socioboard.Domain.ReturnData r = (Domain.Socioboard.Domain.ReturnData)new JavaScriptSerializer().Deserialize(ApiInstaReportObj.GetVideoPosts(account.InstagramId), typeof(Domain.Socioboard.Domain.ReturnData));
                    Console.WriteLine("1");
                    ApiInstaReportObj.Timeout = -1;
                    Domain.Socioboard.Domain.ReturnData r1 = (Domain.Socioboard.Domain.ReturnData)new JavaScriptSerializer().Deserialize(ApiInstaReportObj.GetImagePosts(account.InstagramId), typeof(Domain.Socioboard.Domain.ReturnData));
                    Console.WriteLine("2");
                    ApiInstaReportObj.Timeout = -1;
                    Domain.Socioboard.Domain.ReturnData r2 = (Domain.Socioboard.Domain.ReturnData)new JavaScriptSerializer().Deserialize(ApiInstaReportObj.InstagramFollowerGained(account.InstagramId), typeof(Domain.Socioboard.Domain.ReturnData));
                    Console.WriteLine("3");
                    ApiInstaReportObj.Timeout = -1;
                    Domain.Socioboard.Domain.ReturnData r3 = (Domain.Socioboard.Domain.ReturnData)new JavaScriptSerializer().Deserialize(ApiInstaReportObj.InstagramFollowingGained(account.InstagramId), typeof(Domain.Socioboard.Domain.ReturnData));
                    Console.WriteLine("4");
                    ApiInstaReportObj.Timeout = -1;
                    Domain.Socioboard.Domain.ReturnData r4 = (Domain.Socioboard.Domain.ReturnData)new JavaScriptSerializer().Deserialize(ApiInstaReportObj.InstagramPostCommentGained(account.InstagramId), typeof(Domain.Socioboard.Domain.ReturnData));
                    Console.WriteLine("4");
                    ApiInstaReportObj.Timeout = -1;
                    Domain.Socioboard.Domain.ReturnData r5 = (Domain.Socioboard.Domain.ReturnData)new JavaScriptSerializer().Deserialize(ApiInstaReportObj.InstagramPostLikesGained(account.InstagramId), typeof(Domain.Socioboard.Domain.ReturnData));
                    Console.WriteLine("4");

                    insert.Id = Id;
                    insert.Profile_Id = Profile_Id;
                    insert.Insta_Name = Insta_Name;
                    insert.Full_Name = Full_Name;
                    insert.Media_Count = Media_count;
                    insert.Follower = Follower;
                    insert.Following = Following;

                    insert.follow_15 = r2._15;
                    insert.perday_follow_15 = r2.perday_15;
                    insert.follow_30 = r1._30;
                    insert.perday_follow_30 = r2.perday_30;
                    insert.follow_60 = r2._60;
                    insert.perday_follow_60 = r2.perday_60;
                    insert.follow_90 = r2._90;
                    insert.perday_follow_90 = r2.perday_90;

                    insert.following_15 = r3._15;
                    insert.perday_following_15 = r3.perday_15;
                    insert.following_30 = r3._30;
                    insert.perday_following_30 = r3.perday_30;
                    insert.following_60 = r3._60;
                    insert.perday_following_60 = r3.perday_60;
                    insert.following_90 = r3._90;
                    insert.perday_following_90 = r3.perday_90;

                    insert.postcomment_15 = r4._15;
                    insert.perday_postcomment_15 = r4.perday_15;
                    insert.postcomment_30 = r4._30;
                    insert.perday_postcomment_30 = r4.perday_30;
                    insert.postcomment_60 = r4._60;
                    insert.perday_postcomment_60 = r4.perday_60;
                    insert.postcomment_90 = r4._90;
                    insert.perday_postcomment_90 = r4.perday_90;

                    insert.postlike_15 = r5._15;
                    insert.perday_postlike_15 = r5.perday_15;
                    insert.postlike_30 = r5._30;
                    insert.perday_postlike_30 = r5.perday_30;
                    insert.postlike_60 = r5._60;
                    insert.perday_postlike_60 = r5.perday_60;
                    insert.postlike_90 = r5._90;
                    insert.perday_postlike_90 = r5.perday_90;

                    insert.videopost_15 = r._15;
                    insert.perday_videopost_15 = r.perday_15;
                    insert.videopost_30 = r._30;
                    insert.perday_videopost_30 = r.perday_30;
                    insert.videopost_60 = r._60;
                    insert.perday_videopost_60 = r.perday_60;
                    insert.videopost_90 = r._90;
                    insert.perday_videopost_90 = r.perday_90;

                    insert.imagepost_15 = r1._15;
                    insert.perday_imagepost_15 = r1.perday_15;
                    insert.imagepost_30 = r1._30;
                    insert.perday_imagepost_30 = r1.perday_30;
                    insert.imagepost_60 = r1._60;
                    insert.perday_imagepost_60 = r1.perday_60;
                    insert.imagepost_90 = r1._90;
                    insert.perday_imagepost_90 = r1.perday_90;

                    string i = new JavaScriptSerializer().Serialize(insert);

                    ApiInstaReportObj.insertdata(i);
                }
                catch (Exception ex)
                {
                }

            }


        }

    }
}

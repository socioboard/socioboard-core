using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text.RegularExpressions;

namespace Api.Socioboard.Helper
{
    public static class FbDiscoverySearchHelper
    {
        #region facebook login

        public static void LoginUsingGlobusHttp(ref FacebookUser facebookUser)
        {
            ///Sign In
            try
            {
                GlobusHttpHelper objGlobusHttpHelper = facebookUser.globusHttpHelper;
                #region Post variable

                string fbpage_id = string.Empty;
                string fb_dtsg = string.Empty;
                string __user = string.Empty;
                string xhpc_composerid = string.Empty;
                string xhpc_targetid = string.Empty;
                string xhpc_composerid12 = string.Empty;
                #endregion

                int intProxyPort = 80;

                if (!string.IsNullOrEmpty(facebookUser.proxyport) && Utils.IdCheck.IsMatch(facebookUser.proxyport))
                {
                    intProxyPort = int.Parse(facebookUser.proxyport);
                }
                Console.WriteLine("Logging in with " + facebookUser.username);
                Console.WriteLine("Logging in with " + facebookUser.username);
                string pageSource = string.Empty;
                try
                {
                    pageSource = objGlobusHttpHelper.getHtmlfromUrl(new Uri("https://www.facebook.com/login.php"),"","");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error : " + ex.StackTrace);
                }

                if (pageSource == null || string.IsNullOrEmpty(pageSource))
                {
                    pageSource = objGlobusHttpHelper.getHtmlfromUrl(new Uri("https://www.facebook.com/login.php"), "", "");
                }

                if (pageSource == null)
                {
                    pageSource = objGlobusHttpHelper.getHtmlfromUrl(new Uri("https://www.facebook.com/login.php"), "", "");
                    return;
                }

                string valueLSD = GlobusHttpHelper.GetParamValue(pageSource, "lsd");
                string ResponseLogin = string.Empty;
                try
                {
                    ResponseLogin = objGlobusHttpHelper.postFormData(new Uri("https://www.facebook.com/login.php?login_attempt=1"), "charset_test=%E2%82%AC%2C%C2%B4%2C%E2%82%AC%2C%C2%B4%2C%E6%B0%B4%2C%D0%94%2C%D0%84&lsd=" + valueLSD + "&locale=en_US&email=" + facebookUser.username.Split('@')[0].Replace("+", "%2B") + "%40" + facebookUser.username.Split('@')[1] + "&pass=" + Uri.EscapeDataString(facebookUser.password) + "&persistent=1&default_persistent=1&charset_test=%E2%82%AC%2C%C2%B4%2C%E2%82%AC%2C%C2%B4%2C%E6%B0%B4%2C%D0%94%2C%D0%84&lsd=" + valueLSD + "","","","","","");           
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error : " + ex.StackTrace);
                }
                if (string.IsNullOrEmpty(ResponseLogin))
                {
                    ResponseLogin = objGlobusHttpHelper.postFormData(new Uri("https://www.facebook.com/login.php?login_attempt=1"), "charset_test=%E2%82%AC%2C%C2%B4%2C%E2%82%AC%2C%C2%B4%2C%E6%B0%B4%2C%D0%94%2C%D0%84&lsd=" + valueLSD + "&locale=en_US&email=" + facebookUser.username.Split('@')[0].Replace("+", "%2B") + "%40" + facebookUser.username.Split('@')[1] + "&pass=" + Uri.EscapeDataString(facebookUser.password) + "&persistent=1&default_persistent=1&charset_test=%E2%82%AC%2C%C2%B4%2C%E2%82%AC%2C%C2%B4%2C%E6%B0%B4%2C%D0%94%2C%D0%84&lsd=" + valueLSD + "","","","","","");           //"https://www.facebook.com/login.php?login_attempt=1"
                }
                if (ResponseLogin == null)
                {
                    return;
                }

                string loginStatus = "";
                if (CheckLogin(ResponseLogin, facebookUser.username, facebookUser.password, facebookUser.proxyip, facebookUser.proxyport, facebookUser.proxyusername, facebookUser.proxypassword, ref loginStatus))
                {
                    Console.WriteLine("Logged in with Username : " + facebookUser.username);
                    Console.WriteLine("Logged in with Username : " + facebookUser.username);

                    facebookUser.isloggedin = true;

                }
                else
                {
                    Console.WriteLine("Couldn't login with Username : " + facebookUser.username);
                    facebookUser.isloggedin = false;


                    if (loginStatus == "account has been disabled")
                    {
                        // GlobusFileHelper.AppendStringToTextfileNewLine(Username + ":" + Password + ":" + proxyAddress + ":" + proxyPort + ":" + proxyUsername + ":" + proxyPassword, Globals.path_DisabledAccount);
                    }

                    if (loginStatus == "Please complete a security check")
                    {
                        //GlobusFileHelper.AppendStringToTextfileNewLine(Username + ":" + Password + ":" + proxyAddress + ":" + proxyPort + ":" + proxyUsername + ":" + proxyPassword, Globals.path_SecurityCheckAccounts);
                    }


                    if (loginStatus == "Your account is temporarily locked")
                    {
                        //GlobusFileHelper.AppendStringToTextfileNewLine(Username + ":" + Password + ":" + proxyAddress + ":" + proxyPort + ":" + proxyUsername + ":" + proxyPassword, Globals.path_TemporarilyLockedAccount);

                    }
                    if (loginStatus == "have been blocked")
                    {
                        //GlobusFileHelper.AppendStringToTextfileNewLine(Username + ":" + Password + ":" + proxyAddress + ":" + proxyPort + ":" + proxyUsername + ":" + proxyPassword, Globals.path_havebeenblocked);

                    }
                    if (loginStatus == "For security reasons your account is temporarily locked")
                    {
                        // GlobusFileHelper.AppendStringToTextfileNewLine(Username + ":" + Password + ":" + proxyAddress + ":" + proxyPort + ":" + proxyUsername + ":" + proxyPassword, Globals.path_SecurityCheckAccountsforsecurityreason);
                    }

                    if (loginStatus == "Account Not Confirmed")
                    {
                        //GlobusFileHelper.AppendStringToTextfileNewLine(Username + ":" + Password + ":" + proxyAddress + ":" + proxyPort + ":" + proxyUsername + ":" + proxyPassword, Globals.path_AccountNotConfirmed);
                    }
                    if (loginStatus == "Temporarily Blocked for 30 Days")
                    {
                        // GlobusFileHelper.AppendStringToTextfileNewLine(Username + ":" + Password + ":" + proxyAddress + ":" + proxyPort + ":" + proxyUsername + ":" + proxyPassword, Globals.path_30daysBlockedAccount);
                    }
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine("Error : " + ex.StackTrace);

            }
        }

        public static bool CheckLogin(string response, string username, string password, string proxyAddress, string proxyPort, string proxyUsername, string proxyPassword, ref string loginStatus)
        {

            try
            {
                if (!string.IsNullOrEmpty(response))
                {


                    if (response.ToLower().Contains("unusual login activity"))
                    {
                        loginStatus = "unusual login activity";
                        Console.WriteLine("Unusual Login Activity: " + username);
                        return false;
                    }
                    if (response.ToLower().Contains("incorrect username"))
                    {
                        loginStatus = "incorrect username";
                        Console.WriteLine("Incorrect username: " + username);
                        return false;
                    }
                    if (response.ToLower().Contains("Choose a verification method".ToLower()))
                    {
                        loginStatus = "Choose a verification method";
                        Console.WriteLine("Choose a verification method: " + username);
                        return false;
                    }
                    if (response.ToLower().Contains("not logged in".ToLower()))
                    {
                        loginStatus = "not logged in";
                        Console.WriteLine("not logged in: " + username);
                        return false;
                    }
                    if (response.Contains("Please log in to continue".ToLower()))
                    {
                        loginStatus = "Please log in to continue";
                        Console.WriteLine("Please log in to continue: " + username);
                        return false;
                    }
                    if (response.Contains("re-enter your password"))
                    {
                        loginStatus = "re-enter your password";
                        Console.WriteLine("Wrong password for: " + username);
                        return false;
                    }
                    if (response.Contains("Incorrect Email"))
                    {
                        loginStatus = "Incorrect Email";
                        Console.WriteLine("Incorrect email: " + username);

                        try
                        {
                            ///Write Incorrect Emails in text file
                            //GlobusFileHelper.AppendStringToTextfileNewLine(username + ":" + password, incorrectEmailFilePath);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }


                        return false;
                    }
                    if (response.Contains("have been blocked"))
                    {
                        loginStatus = "have been blocked";
                        Console.WriteLine("you have been blocked: " + username);
                        return false;
                    }
                    if (response.Contains("account has been disabled"))
                    {
                        loginStatus = "account has been disabled";
                        Console.WriteLine("your account has been disabled: " + username);
                        return false;
                    }
                    if (response.Contains("Please complete a security check"))
                    {
                        loginStatus = "Please complete a security check";
                        Console.WriteLine("Please complete a security check: " + username);
                        return false;
                    }
                    if (response.Contains("Please complete a security check"))
                    {
                        loginStatus = "Please complete a security check";
                        Console.WriteLine("You must log in to see this page: " + username);
                        return false;
                    }
                    if (response.Contains("<input value=\"Sign Up\" onclick=\"RegistrationBootloader.bootloadAndValidate();"))
                    {
                        loginStatus = "RegistrationBootloader.bootloadAndValidate()";
                        Console.WriteLine("Not logged in with: " + username);
                        return false;
                    }
                    if (response.Contains("Account Not Confirmed"))
                    {
                        loginStatus = "Account Not Confirmed";
                        Console.WriteLine("Account Not Confirmed " + username);
                        return false;
                    }
                    if (response.Contains("Your account is temporarily locked"))
                    {
                        loginStatus = "Your account is temporarily locked";
                        Console.WriteLine("Your account is temporarily locked: " + username);
                        return false;
                    }
                    if (response.Contains("Your account has been temporarily suspended"))
                    {
                        loginStatus = "Your account has been temporarily suspended";
                        Console.WriteLine("Your account has been temporarily suspended: " + username);
                        return false;
                    }
                    if (response.Contains("You must log in to see this page"))
                    {
                        Console.WriteLine("You must log in to see this page: " + username);
                        return false;
                    }
                    if (response.ToLower().Contains("you must log in to see this page"))
                    {
                        Console.WriteLine("You must log in to see this page: " + username);
                        return false;
                    }
                    if (response.ToLower().Contains("you entered an old password"))
                    {
                        loginStatus = "you entered an old password";
                        Console.WriteLine("You Entered An Old Password: " + username);
                        return false;
                    }
                    if (response.Contains("For security reasons your account is temporarily locked"))
                    {
                        loginStatus = "For security reasons your account is temporarily locked";
                        Console.WriteLine("For security reasons your account is temporarily locked: " + username);
                        return false;
                    }
                    if (response.Contains("Please Verify Your Identity") || response.Contains("please Verify Your Identity"))
                    {
                        loginStatus = "Please Verify Your Identity";
                        Console.WriteLine("Please Verify Your Identity: " + username);
                        return false;
                    }
                    if (response.Contains("Temporarily Blocked for 30 Days"))
                    {
                        loginStatus = "Temporarily Blocked for 30 Days";
                        Console.WriteLine("You're Temporarily Blocked for 30 Days: " + username);
                        return false;
                    }

                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error : " + ex.StackTrace);
            }
            return true;
        }

        #endregion
        #region  facebook hashtag scraper

        public static void Scraper(ref FacebookUser fbUser, string Hash)
        {


            GlobusHttpHelper HttpHelper = fbUser.globusHttpHelper;
            string pageSource_Home = HttpHelper.getHtmlfromUrl(new Uri("https://www.facebook.com"),"","");


            string __user = string.Empty;
            string fb_dtsg = string.Empty;
            string KeyWord = string.Empty;

            __user = GlobusHttpHelper.GetParamValue(pageSource_Home, "user");
            if (string.IsNullOrEmpty(__user))
            {
                __user = GlobusHttpHelper.ParseJson(pageSource_Home, "user");
            }


            fb_dtsg = GlobusHttpHelper.Get_fb_dtsg(pageSource_Home);



            string PostData = "__user=" + __user + "&__a=1&__dyn=7nmanEyl2lm9o-t2u5bGya4Au74qbx2mbAKGiyEyut9LRwxBxem9V8CdwIhEyfyUnwPUS2O4K5e8GQ8GqcFoy8ACxtpm&__req=h&fb_dtsg=" + fb_dtsg + "&ttstamp=26581701181109510510483495368&__rev=1719057";
            string PostUrl = "https://www.facebook.com/pubcontent/trending/see_more/?topic_ids[0]=105471716153186&topic_ids[1]=109659645720566&topic_ids[2]=108332329190557&position=3";
            string Pageresponce = HttpHelper.postFormData(new Uri(PostUrl), PostData);
            Pageresponce = Pageresponce.Replace("\\u003C", "<");


            string[] trendingArr = System.Text.RegularExpressions.Regex.Split(Pageresponce, "li data-topicid=");
            trendingArr = trendingArr.Skip(1).ToArray();
            foreach (var item_trendingArr in trendingArr)
            {
                string trendingId = string.Empty;
                string trendingName = string.Empty;
                string trendingLinkUrl = string.Empty;
                try
                {
                    try
                    {
                        trendingId = Utils.getBetween(item_trendingArr, "\\\"", "\\\"");
                    }
                    catch { }
                    try
                    {
                        trendingName = Utils.getBetween(item_trendingArr, "_5v0s _5my8\\\">", "<");
                    }
                    catch { };

                    string LinkUrl = Utils.getBetween(item_trendingArr, "_4qzh _5v0t _7ge\\\" href=\\\"", "\" id=").Replace("\\", string.Empty).Replace("amp;", string.Empty);
                    if (!LinkUrl.Contains("www.facebook.com/"))
                    {
                        trendingLinkUrl = "https://www.facebook.com" + LinkUrl;
                    }
                    else
                    {
                        trendingLinkUrl = LinkUrl;
                    }

                    if (trendingName.Contains(KeyWord))
                    {
                        string FindPageSource = string.Empty;


                        FindPageSource = HttpHelper.getHtmlfromUrl(new Uri(trendingLinkUrl));

                        string postID = string.Empty;
                        string postName = string.Empty;
                        string postImage = string.Empty;
                        string postDescriptions = string.Empty;
                        string posttitle = string.Empty;

                        string[] ContentArr = System.Text.RegularExpressions.Regex.Split(FindPageSource, "userContentWrapper _5pcr _3ccb");
                        ContentArr = ContentArr.Skip(1).ToArray();
                        foreach (var ContentArr_item in ContentArr)
                        {
                            try
                            {
                                postName = trendingName;
                                postID = trendingId;
                                // class="_4-eo _2t9n"
                                postImage = Utils.getBetween(ContentArr_item, "scaledImageFitWidth img\" src=\"", "alt=").Replace("amp;", string.Empty).Replace("\"", string.Empty);

                            }
                            catch (Exception)
                            {

                            }
                            string profileName = posttitle;//2
                            string Message = Utils.getBetween(ContentArr_item, "<p>", "<").Replace("&#064;", "@").Replace("&amp;", "&").Replace("u0025", "%").Replace("&#039;", "'"); ;//7
                            string[] DetailedInfo = System.Text.RegularExpressions.Regex.Split(ContentArr_item, "<div class=\"_6m7\">");
                            string detail = "-" + DetailedInfo[1];//8
                            detail = Utils.getBetween(detail, "-", "</div>").Replace("&amp;", "&").Replace("u0025", "%").Replace("&#039;", "'");

                            if (detail.Contains("<a "))
                            {
                                string GetVisitUrl = Utils.getBetween(detail, "\">", "</a>");

                                detail = Utils.getBetween("$$$####" + detail, "$$$####", "<a href=") + "-" + GetVisitUrl;

                            }

                            string[] ArrDetail = System.Text.RegularExpressions.Regex.Split(ContentArr_item, "<div class=\"mbs _6m6\">");

                            string Titles = Utils.getBetween(ArrDetail[1], ">", "</a>").Replace("&#064;", "@").Replace("&amp;", "&").Replace("u0025", "%").Replace("&#039;", "'");
                            string SiteRedirectionUrl = Utils.getBetween(ArrDetail[1], "LinkshimAsyncLink.swap(this, &quot;", ");");
                            SiteRedirectionUrl = Uri.UnescapeDataString(SiteRedirectionUrl).Replace("\\u0025", "%").Replace("\\", "");//4
                            string websiteUrl = Utils.getBetween(SiteRedirectionUrl, "//", "/");

                            string[] adImg = System.Text.RegularExpressions.Regex.Split(ContentArr_item, "<img class=\"scaledImageFitWidth img\"");
                            string redirectionImg = Utils.getBetween(adImg[1], "src=\"", "\"").Replace("&amp;", "&");

                            string[] profImg = System.Text.RegularExpressions.Regex.Split(ContentArr_item, "<img class=\"_s0 5xib 5sq7 _rw img\"");
                            string profileImg = Utils.getBetween(profImg[0], "src=\"", "\"").Replace("&amp;", "&");

                        }

                    }

                }
                catch { };

            }


        }

        //public void ScraperHasTage(ref FacebookUser fbUser, string Hash, Guid BoardfbPageId)
        //{


        //    GlobusHttpHelper HttpHelper = fbUser.globusHttpHelper;
        //    string KeyWord = Hash;
        //    string pageSource_Home = HttpHelper.getHtmlfromUrl(new Uri("https://www.facebook.com/hashtag/" + KeyWord));
        //    List<string> pageSouceSplit = new List<string>();
        //    string[] trendingArr = System.Text.RegularExpressions.Regex.Split(pageSource_Home, "li data-topicid=");
        //    string[] PagesLink = System.Text.RegularExpressions.Regex.Split(pageSource_Home, "uiLikePageButton");
        //    foreach (var item in PagesLink)
        //    {
        //        pageSouceSplit.Add(item);
        //    }
        //    PagesLink = PagesLink.Skip(1).ToArray();

        //    foreach (var item_pageSouceSplit in pageSouceSplit)
        //    {
        //        try
        //        {
        //            if (item_pageSouceSplit.Contains("<!DOCTYPE html>"))
        //            {
        //                continue;
        //            }
        //            Dictionary<string, string> listContent = ScrapHasTagPages(item_pageSouceSplit);
        //            Domain.Socioboard.Domain.Boardfbfeeds fbfeed = new Domain.Socioboard.Domain.Boardfbfeeds();
        //            fbfeed.Id = Guid.NewGuid();
        //            fbfeed.Isvisible = true;
        //            fbfeed.Message = listContent["Message"];
        //            fbfeed.Image = listContent["PostImage"];
        //            fbfeed.Description = listContent["Title"];
        //            string[] splitdate = System.Text.RegularExpressions.Regex.Split(listContent["Time"], "at");
        //            string d = splitdate[0].Trim();
        //            string t = splitdate[1].Trim();
        //            fbfeed.Createddate = Convert.ToDateTime(d + " " + t);
        //            fbfeed.Feedid = listContent["PostId"];
        //            fbfeed.Type = listContent["Type"];
        //            fbfeed.Type = listContent["Link"];
        //            fbfeed.Fbpageprofileid = BoardfbPageId;
        //             if (!boardrepo.checkFacebookFeedExists(fbfeed.Feedid, BoardfbPageId))
        //             {
        //                 boardrepo.addBoardFbPageFeed(fbfeed);
        //             }

        //            // Please Write Code get Dictionary data 

        //            //


        //        }
        //        catch { };



        //    }


        //    try
        //    {


        //        string ajaxpipe_token = Utils.getBetween(pageSource_Home, "\"ajaxpipe_token\":\"", "\"");
        //        string[] data_c = System.Text.RegularExpressions.Regex.Split(pageSource_Home, "data-cursor=");
        //        string cursor = Utils.getBetween(data_c[4], "\"", "=");
        //        if (cursor.Contains("data-dedupekey"))
        //        {
        //            cursor = "-" + cursor;
        //            cursor = Utils.getBetween(cursor, "-", "\"");
        //        }
        //        string sectionid = Utils.getBetween(pageSource_Home, "section_id\\\":", ",");
        //        string userid = Utils.getBetween(pageSource_Home, "USER_ID\":\"", "\"");
        //        string feed_Id = "90842368";
        //        string pager_id = "u_ps_0_0_1n";
        //        for (int i = 2; i < 50; i++)
        //        {
        //            try
        //            {
        //                Thread.Sleep(30 * 1000);

        //                List<string> pageSouceSplitPagination = new List<string>();
        //                if (string.IsNullOrEmpty(fbUser.username))
        //                {
        //                    break;
        //                }
        //                //GlobusLogHelper.log.Info("Please wait... Searching for data from Page :" + i + "   with User Name : " + fbUser.username);


        //                string req = "https://www.facebook.com/ajax/pagelet/generic.php/LitestandMoreStoriesPagelet?ajaxpipe=1&ajaxpipe_token=" + ajaxpipe_token + "&no_script_path=1&data=%7B%22cursor%22%3A%22" + cursor + "%22%2C%22preload_next_cursor%22%3Anull%2C%22pager_config%22%3A%22%7B%5C%22edge%5C%22%3Anull%2C%5C%22source_id%5C%22%3Anull%2C%5C%22section_id%5C%22%3A" + sectionid + "%2C%5C%22pause_at%5C%22%3Anull%2C%5C%22stream_id%5C%22%3Anull%2C%5C%22section_type%5C%22%3A1%2C%5C%22sizes%5C%22%3Anull%2C%5C%22most_recent%5C%22%3Afalse%2C%5C%22unread_session%5C%22%3Afalse%2C%5C%22continue_top_news_feed%5C%22%3Afalse%2C%5C%22ranking_model%5C%22%3Anull%2C%5C%22unread_only%5C%22%3Afalse%7D%22%2C%22pager_id%22%3A%22" + pager_id + "%22%2C%22scroll_count%22%3A1%2C%22start_unread_session%22%3Afalse%2C%22start_continue_top_news_feed%22%3Afalse%2C%22feed_stream_id%22%3A" + feed_Id + "%2C%22snapshot_time%22%3Anull%7D&__user=" + userid + "&__a=1&__dyn=7nm8RW8BgCBynzpQ9UoHaEWCueyrhEK49oKiWFaaBGeqrYw8popyujhElx2ubhHximmey8szoyfwgo&__req=jsonp_2&__rev=1583304&__adt=" + i + "";      //
        //                string respReq = HttpHelper.getHtmlfromUrl(new Uri(req));

        //                respReq = respReq.Replace("\\", "").Replace("u003C", "<");
        //                string[] arrrespReq = System.Text.RegularExpressions.Regex.Split(respReq, "source_id");
        //                feed_Id = Utils.getBetween(respReq, "feed_stream_id", "snapshot_time");
        //                feed_Id = Utils.getBetween(feed_Id, "A", "u");

        //                string[] pager_id1 = System.Text.RegularExpressions.Regex.Split(respReq, "_4-u2 mbl ");
        //                pager_id = Utils.getBetween(pager_id1[2], "id=\"", "\"");
        //                data_c = System.Text.RegularExpressions.Regex.Split(respReq, "data-cursor=");
        //                if (data_c.Length < 8)
        //                {
        //                    cursor = Utils.getBetween(data_c[data_c.Length - 1], "\"", "=");
        //                }
        //                cursor = Utils.getBetween(data_c[8], "\"", "=");
        //                if (cursor.Contains("data-dedupekey"))
        //                {
        //                    cursor = "-" + cursor;
        //                    cursor = Utils.getBetween(cursor, "-", "\"");
        //                }

        //                string[] PagesLinkPagination = System.Text.RegularExpressions.Regex.Split(respReq, "<span>Suggested Post</span>");

        //                foreach (var item in PagesLinkPagination)
        //                {
        //                    pageSouceSplitPagination.Add(item);
        //                }

        //                PagesLink = System.Text.RegularExpressions.Regex.Split(respReq, "uiLikePageButton");
        //                foreach (var item in PagesLink)
        //                {
        //                    pageSouceSplitPagination.Add(item);
        //                }


        //                foreach (var item_pageSouceSplit in pageSouceSplit)
        //                {
        //                    try
        //                    {
        //                        if (item_pageSouceSplit.Contains("<!DOCTYPE html>"))
        //                        {
        //                            continue;
        //                        }
        //                        Dictionary<string, string> listContent = ScrapHasTagPages(item_pageSouceSplit);
        //                        Domain.Socioboard.Domain.Boardfbfeeds fbfeed = new Domain.Socioboard.Domain.Boardfbfeeds();
        //                        fbfeed.Id = Guid.NewGuid();
        //                        fbfeed.Isvisible = true;
        //                        fbfeed.Message = listContent["Message"];
        //                        fbfeed.Image = listContent["PostImage"];
        //                        fbfeed.Description = listContent["Title"];
        //                        string[] splitdate = System.Text.RegularExpressions.Regex.Split(listContent["Time"], "at");
        //                        string d = splitdate[0].Trim();
        //                        string t = splitdate[1].Trim();
        //                        fbfeed.Createddate = Convert.ToDateTime(d + " " + t);
        //                        fbfeed.Feedid = listContent["PostId"];
        //                        fbfeed.Type = listContent["Type"];
        //                        fbfeed.Type = listContent["Link"];
        //                        fbfeed.Fbpageprofileid = BoardfbPageId;
        //                        if (!boardrepo.checkFacebookFeedExists(fbfeed.Feedid, BoardfbPageId))
        //                        {
        //                            boardrepo.addBoardFbPageFeed(fbfeed);
        //                        }
        //                        // Please Write Code get Dictionary data 

        //                        //

        //                    }
        //                    catch { };



        //                }

        //            }
        //            catch (Exception ex)
        //            { //GlobusLogHelper.log.Error(ex.StackTrace); 
        //            }
        //        }
        //    }
        //    catch
        //    { }





        //}



        //public Dictionary<string, string> ScrapHasTagPages(string Value)
        //{
        //    string redirectionHref = string.Empty;
        //    string title = string.Empty;
        //    List<string[]> Likedata = new List<string[]>();
        //    Dictionary<string, string> HasTagData = new Dictionary<string, string>();
        //    //  foreach (var Value in Likepages)
        //    {
        //        try
        //        {

        //            redirectionHref = Utils.getBetween(Value, "href=\"", "\"");
        //            string profileUrl = redirectionHref;//1
        //            if (redirectionHref.Contains("https://www.facebook.com"))
        //            {

        //                string[] Arr_Title = System.Text.RegularExpressions.Regex.Split(Value, "<span class=\"fwb fcg\"");
        //                foreach (var valuetitle in Arr_Title)
        //                {
        //                    try
        //                    {


        //                        // title = Utils.getBetween(valuetitle, "<a", "/a>");
        //                        title = Utils.getBetween(valuetitle, "\">", "/a>");
        //                        if (!title.Equals(string.Empty))
        //                        {
        //                            title = Utils.getBetween(title, "\">", "<");
        //                            if (!string.IsNullOrEmpty(title))
        //                            {
        //                                break;
        //                            }

        //                        }
        //                    }
        //                    catch (Exception)
        //                    {

        //                    }
        //                }
        //                string profileName = title;//2
        //                string Message = Utils.getBetween(Value, "<p>", "<").Replace("&#064;", "@").Replace("&amp;", "&").Replace("u0025", "%");//7

        //                string[] timeDetails = Regex.Split(Value, "<abbr");
        //                string postedTime = string.Empty;
        //                try
        //                {
        //                    postedTime = Utils.getBetween(timeDetails[1], "=\"", "\"");
        //                }
        //                catch { };
        //                string postid = string.Empty;

        //                try
        //                {
        //                    postid = Utils.getBetween(timeDetails[0], "fbid=", "&amp;");
        //                    if (postid == "")
        //                    {
        //                        postid = Utils.getBetween(timeDetails[0], "/posts/", "\" target=");
        //                    }
        //                }
        //                catch
        //                {
        //                }

        //                string[] DetailedInfo = System.Text.RegularExpressions.Regex.Split(Value, "<div class=\"_6m7\">");
        //                string detail = string.Empty;
        //                try
        //                {
        //                    detail = "-" + DetailedInfo[1];//8
        //                    detail = Utils.getBetween(detail, "-", "</div>").Replace("&amp;", "&").Replace("u0025", "%");
        //                    if (detail.Contains("<a "))
        //                    {
        //                        string GetVisitUrl = Utils.getBetween(detail, "\">", "</a>");
        //                        detail = Utils.getBetween("$$$####" + detail, "$$$####", "<a href=") + "-" + GetVisitUrl;
        //                    }
        //                }
        //                catch
        //                { };

        //                string[] ArrDetail = System.Text.RegularExpressions.Regex.Split(Value, "<div class=\"mbs _6m6\">");
        //                string Titles = string.Empty;
        //                // string Url = Utils.getBetween(ArrDetail[0], "", "");
        //                try
        //                {
        //                    Titles = Utils.getBetween(ArrDetail[1], ">", "</a>").Replace("&#064;", "@").Replace("&amp;", "&").Replace("u0025", "%");//6
        //                    if (Titles.Contains("Sachin Tendulkar"))
        //                    {

        //                    }
        //                }
        //                catch { };
        //                string SiteRedirectionUrl = string.Empty;
        //                try
        //                {
        //                    SiteRedirectionUrl = Utils.getBetween(ArrDetail[1], "LinkshimAsyncLink.swap(this, &quot;", ");");
        //                }
        //                catch { };
        //                try
        //                {
        //                    SiteRedirectionUrl = Uri.UnescapeDataString(SiteRedirectionUrl).Replace("\\u0025", "%").Replace("\\", "");//4
        //                }
        //                catch { };
        //                string websiteUrl = string.Empty;
        //                try
        //                {
        //                    websiteUrl = Utils.getBetween(SiteRedirectionUrl, "//", "/");
        //                }
        //                catch { };
        //                string redirectionImg = string.Empty;
        //                try
        //                {
        //                    string[] adImg = System.Text.RegularExpressions.Regex.Split(Value, "<img class=\"scaledImageFitWidth img\"");
        //                       redirectionImg = Utils.getBetween(adImg[1], "src=\"", "\"").Replace("&amp;", "&");
        //                }
        //                catch { };


        //                string[] profImg = System.Text.RegularExpressions.Regex.Split(Value, "<img class=\"_s0 5xib 5sq7 _rw img\"");
        //                string profileImg = string.Empty;
        //                try
        //                {
        //                    profileImg = Utils.getBetween(profImg[0], "src=\"", "\"").Replace("&amp;", "&");
        //                }
        //                catch { };
        //                HasTagData.Add("Title", title);
        //                HasTagData.Add("Time", postedTime);
        //                HasTagData.Add("Type", "link");
        //                HasTagData.Add("Message", Message);
        //                HasTagData.Add("Image", profileImg);
        //                HasTagData.Add("PostImage", redirectionImg);
        //                HasTagData.Add("PostId", postid);
        //                HasTagData.Add("Link", SiteRedirectionUrl);
        //            }
        //        }
        //        catch { };
        //    }

        //    return HasTagData;
        //}

        public static List<Domain.Socioboard.Domain.DiscoverySearch> ScraperHasTage(string Hash)
        {
            GlobusHttpHelper HttpHelper = new GlobusHttpHelper();
            string KeyWord = Hash;
            string pageSource_Home = HttpHelper.getHtmlfromUrl(new Uri("https://www.facebook.com/hashtag/" + KeyWord));
            List<string> pageSouceSplit = new List<string>();
            string[] PagesLink = System.Text.RegularExpressions.Regex.Split(pageSource_Home, "_4-u2 mbm _5jmm _5pat _5v3q _4-u8");
            foreach (var item in PagesLink)
            {
                pageSouceSplit.Add(item);
            }
            PagesLink = PagesLink.Skip(1).ToArray();
            List<Domain.Socioboard.Domain.DiscoverySearch> discSearchList = new List<Domain.Socioboard.Domain.DiscoverySearch>();
            foreach (var item_pageSouceSplit in pageSouceSplit)
            {
                Domain.Socioboard.Domain.DiscoverySearch discSearchObj = new Domain.Socioboard.Domain.DiscoverySearch();
                try
                {
                    if (item_pageSouceSplit.Contains("<!DOCTYPE html>"))
                    {
                        continue;
                    }
                    Dictionary<string, string> listContent = ScrapHasTagPages(item_pageSouceSplit);
                    // Domain.Socioboard.Domain.Boardfbfeeds fbfeed = new Domain.Socioboard.Domain.Boardfbfeeds();
                    discSearchObj.Id = Guid.NewGuid();

                    discSearchObj.Message = listContent["Message"];
                    //discSearchObj.Image = listContent["PostImage"];
                    //discSearchObj.Description = listContent["Title"];
                    string[] splitdate = System.Text.RegularExpressions.Regex.Split(listContent["Time"], "at");
                    string d = splitdate[0].Trim();
                    string t = splitdate[1].Trim();
                    discSearchObj.CreatedTime = Convert.ToDateTime(d + " " + t);
                    discSearchObj.MessageId = listContent["PostId"];
                    //discSearchObj.Type = listContent["Type"];
                    //discSearchObj.Link = listContent["Link"];
                    discSearchObj.FromId = listContent["FromId"];
                    discSearchObj.FromName = listContent["FromName"];
                    //discSearchObj.Fbpageprofileid = BoardfbPageId;

                    discSearchList.Add(discSearchObj);

                }
                catch { };


            }
            return discSearchList;

        }
        public static Dictionary<string, string> ScrapHasTagPages(string Value)
        {
            string redirectionHref = string.Empty;
            string title = string.Empty;
            string profileid = string.Empty;
            List<string[]> Likedata = new List<string[]>();
            Dictionary<string, string> HasTagData = new Dictionary<string, string>();
            //  foreach (var Value in Likepages)
            {
                try
                {
                    profileid = Utils.getBetween(Value, "entity_id&quot;:&quot;", "&quot;,&quot;entity_path").Trim();
                    if (profileid == "")
                    {
                        profileid = Utils.getBetween(Value, "target_profile_id&quot;:&quot;", "&quot;,&quot;type_id").Trim();
                    }
                    redirectionHref = Utils.getBetween(Value, "href=\"", "\"");
                    string profileUrl = redirectionHref;//1
                    if (redirectionHref.Contains("https://www.facebook.com"))
                    {

                        string[] Arr_Title = System.Text.RegularExpressions.Regex.Split(Value, "<span class=\"fwb fcg\"");
                        foreach (var valuetitle in Arr_Title)
                        {
                            try
                            {


                                // title = Utils.getBetween(valuetitle, "<a", "/a>");
                                title = Utils.getBetween(valuetitle, "\">", "/a>");
                                if (!title.Equals(string.Empty))
                                {
                                    title = Utils.getBetween(title, "\">", "<");
                                    if (!string.IsNullOrEmpty(title))
                                    {
                                        break;
                                    }

                                }
                            }
                            catch (Exception)
                            {

                            }
                        }
                        try
                        {
                            if (title.Trim() == "")
                            {

                                title = Utils.getBetween(Arr_Title[0], "data-ft=\"&#123;&quot;tn&quot;:&quot;k&quot;&#125;\">", "</a></span>");

                            }
                        }
                        catch { }
                        string profileName = title;//2
                        string Message = Utils.getBetween(Value, "<p>", "<").Replace("&#064;", "@").Replace("&amp;", "&").Replace("u0025", "%");//7

                        string[] timeDetails = Regex.Split(Value, "<abbr");
                        string postedTime = string.Empty;
                        try
                        {
                            postedTime = Utils.getBetween(timeDetails[1], "=\"", "\"");
                        }
                        catch { };


                        string[] DetailedInfo = System.Text.RegularExpressions.Regex.Split(Value, "<div class=\"_6m7\">");
                        string detail = string.Empty;
                        try
                        {
                            detail = "-" + DetailedInfo[1];//8
                            detail = Utils.getBetween(detail, "-", "</div>").Replace("&amp;", "&").Replace("u0025", "%");

                            if (detail.Contains("<a "))
                            {
                                string GetVisitUrl = Utils.getBetween(detail, "\">", "</a>");

                                detail = Utils.getBetween("$$$####" + detail, "$$$####", "<a href=") + "-" + GetVisitUrl;

                            }

                        }
                        catch
                        { };

                        string[] ArrDetail = System.Text.RegularExpressions.Regex.Split(Value, "<div class=\"mbs _6m6\">");
                        string Titles = string.Empty;
                        // string Url = Utils.getBetween(ArrDetail[0], "", "");
                        try
                        {
                            Titles = Utils.getBetween(ArrDetail[1], ">", "</a>").Replace("&#064;", "@").Replace("&amp;", "&").Replace("u0025", "%");//6
                            if (Titles.Contains("Sachin Tendulkar"))
                            {

                            }
                        }
                        catch { };
                        string SiteRedirectionUrl = string.Empty;
                        try
                        {
                            SiteRedirectionUrl = Utils.getBetween(ArrDetail[1], "LinkshimAsyncLink.swap(this, &quot;", ");");
                        }
                        catch { };
                        try
                        {
                            SiteRedirectionUrl = Uri.UnescapeDataString(SiteRedirectionUrl).Replace("\\u0025", "%").Replace("\\", "");//4
                        }
                        catch { };
                        string websiteUrl = string.Empty;
                        try
                        {
                            websiteUrl = Utils.getBetween(SiteRedirectionUrl, "//", "/");
                        }
                        catch { };

                        string[] adImg = System.Text.RegularExpressions.Regex.Split(Value, "<img class=\"scaledImageFitWidth img\"");
                        string redirectionImg = string.Empty;
                        try
                        {
                            redirectionImg = Utils.getBetween(adImg[1], "src=\"", "\"").Replace("&amp;", "&");
                        }
                        catch { };

                        string[] profImg = System.Text.RegularExpressions.Regex.Split(Value, "<img class=\"_s0 5xib 5sq7 _rw img\"");
                        string profileImg = string.Empty;
                        try
                        {
                            profileImg = Utils.getBetween(profImg[0], "src=\"", "\"").Replace("&amp;", "&");
                        }
                        catch { };

                        string PostId = string.Empty;
                        try
                        {

                            PostId = Utils.getBetween(Value, "story_id=", "data-ft=").Replace("\"", string.Empty);
                        }
                        catch { };

                        HasTagData.Add("Title", Titles);
                        HasTagData.Add("Time", postedTime);

                        HasTagData.Add("Message", Message);

                        HasTagData.Add("Image", profileImg);

                        try
                        {
                            if (redirectionImg.Contains("/video/"))
                            {
                                HasTagData.Add("Type", "video");
                            }
                            else if (redirectionImg.Contains("/photo/"))
                            {
                                HasTagData.Add("Type", "image");
                            }
                            else
                            {
                                HasTagData.Add("Type", "link");
                            }
                        }
                        catch { };

                        HasTagData.Add("PostImage", redirectionImg);
                        HasTagData.Add("PostId", PostId);
                        HasTagData.Add("Link", redirectionHref);
                        HasTagData.Add("FromId", profileid);
                        HasTagData.Add("FromName", profileName);



                    }
                }
                catch { };
            }

            return HasTagData;
        }
        #endregion


    }
}
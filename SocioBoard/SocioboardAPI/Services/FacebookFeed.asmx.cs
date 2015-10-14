using Api.Socioboard.Model;
using Domain.Socioboard.Domain;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Script.Services;
using System.Web.Services;

namespace Api.Socioboard.Services
{
    /// <summary>
    /// Summary description for FacebookFeed
    /// </summary>
   
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]

    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    
    [ScriptService]
    public class FacebookFeed : System.Web.Services.WebService
    {
        FacebookFeedRepository objFacebookFeedRepository = new FacebookFeedRepository();
        
        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string getAllFacebookFeedsByUserIdAndProfileId(string UserId, string ProfileId)
        {
             //List<Domain.Socioboard.Domain.FacebookFeed> lstFacebookFeed=new List<Domain.Socioboard.Domain.FacebookFeed> ();
            //try
            //{
            //    if (objFacebookFeedRepository.checkFacebookUserExists(ProfileId, Guid.Parse(UserId)))
            //    {
            //        lstFacebookFeed = objFacebookFeedRepository.getAllFacebookFeeds(Guid.Parse(UserId), ProfileId);
            //    }
            //    else
            //    {
            //         lstFacebookFeed = objFacebookFeedRepository.getAllFacebookUserFeeds(ProfileId);
            //    }
            //    return new JavaScriptSerializer().Serialize(lstFacebookFeed);
            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine(ex.StackTrace);
            //    return "Something Went Wrong";
            //}

            MongoRepository boardrepo = new MongoRepository("MongoFacebookFeed");
            try
            {

                var result = boardrepo.Find<MongoFacebookFeed>(t => t.UserId.Equals(UserId)&&t.ProfileId.Equals(ProfileId)).ConfigureAwait(false);

                var task = Task.Run(async () =>
                {
                    return await result;
                });
                IList<MongoFacebookFeed> objfbfeeds = task.Result;
                if (objfbfeeds.Count() == 0)
                {

                    result = boardrepo.Find<MongoFacebookFeed>(x => x.ProfileId.Equals(ProfileId)).ConfigureAwait(false);

                    task = Task.Run(async () =>
                    {
                        return await result;
                    });
                }
                return new JavaScriptSerializer().Serialize(objfbfeeds);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return "Something Went Wrong";
            }
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string getAllFacebookFeedsByUserIdAndProfileIdUsingLimit(string UserId, string ProfileId, string noOfDataToSkip, string noOfResultsFromTop)
        {
            //List<Domain.Socioboard.Domain.FacebookFeed> lstFacebookFeed = new List<Domain.Socioboard.Domain.FacebookFeed>();
            //try
            //{
            //    if (objFacebookFeedRepository.checkFacebookUserExists(ProfileId, Guid.Parse(UserId)))
            //    {
            //        lstFacebookFeed = objFacebookFeedRepository.getAllFacebookFeedsOfSBUserWithRangeAndProfileId(UserId, ProfileId, noOfDataToSkip, noOfResultsFromTop);
            //    }
            //    else
            //    {
            //        lstFacebookFeed = objFacebookFeedRepository.getAllFacebookFeedsOfSBUserWithRangeByProfileId(ProfileId, noOfDataToSkip, noOfResultsFromTop);
            //    }
            //    return new JavaScriptSerializer().Serialize(lstFacebookFeed);
            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine(ex.StackTrace);
            //    return "Something Went Wrong";
            //}

            MongoRepository boardrepo = new MongoRepository("MongoFacebookFeed");
            try
            {

                var result = boardrepo.Find<MongoFacebookFeed>(x => x.ProfileId.Equals(ProfileId)&&x.UserId.Equals(UserId)).ConfigureAwait(false);

                var task = Task.Run(async () =>
                {
                    return await result;
                });
                IList<MongoFacebookFeed> objfbfeeds = task.Result;
                if (objfbfeeds.Count() == 0) 
                {

                     result = boardrepo.Find<MongoFacebookFeed>(x => x.ProfileId.Equals(ProfileId)).ConfigureAwait(false);

                     task = Task.Run(async () =>
                    {
                        return await result;
                    });
                }
                List<MongoFacebookFeed> fbfeeds = objfbfeeds.OrderByDescending(x => x.FeedDate).Skip(Convert.ToInt32(noOfDataToSkip)).Take(Convert.ToInt32(noOfResultsFromTop)).ToList();
                return new JavaScriptSerializer().Serialize(fbfeeds);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return "Something Went Wrong";
            }
        }

        // Edited by Antima

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string getAllFacebookFeedsByUserIdAndProfileId1(string UserId, string ProfileId, int count)
        {
            //try
            //{
             //   List<Domain.Socioboard.Domain.FacebookFeed> lstFacebookFeed = objFacebookFeedRepository.getAllFacebookFeeds(Guid.Parse(UserId), ProfileId, count);
            //    return new JavaScriptSerializer().Serialize(lstFacebookFeed);
            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine(ex.StackTrace);
            //    return "Something Went Wrong";
            //}

            MongoRepository boardrepo = new MongoRepository("MongoFacebookFeed");
            try
            {

                var result = boardrepo.Find<MongoFacebookFeed>(x => x.ProfileId.Equals(ProfileId) && x.UserId.Equals(UserId)).ConfigureAwait(false);

                var task = Task.Run(async () =>
                {
                    return await result;
                });
                IList<MongoFacebookFeed> objfbfeeds = task.Result;
                List<MongoFacebookFeed> fbfeeds = objfbfeeds.OrderByDescending(x => x.FeedDate).Skip(count).Take(Convert.ToInt32(10)).ToList();
                return new JavaScriptSerializer().Serialize(fbfeeds);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return "Something Went Wrong";
            }
        }


        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string getUnreadMessages(string ProfileId)
        {
            //try
            //{
            //    List<Domain.Socioboard.Domain.FacebookFeed> lstFacebookFeed = FacebookFeedRepository.getUnreadMessages(ProfileId);
            //    return new JavaScriptSerializer().Serialize(lstFacebookFeed);
            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine(ex.StackTrace);
            //    return "Something Went Wrong";
            //}
            MongoRepository boardrepo = new MongoRepository("MongoFacebookFeed");
            try
            {

                var result = boardrepo.Find<MongoFacebookFeed>(x => x.ProfileId.Equals(ProfileId)&& x.ReadStatus.Equals("0") ).ConfigureAwait(false);

                var task = Task.Run(async () =>
                {
                    return await result;
                });
                IList<MongoFacebookFeed> objfbfeeds = task.Result;
                List<MongoFacebookFeed> fbfeeds = objfbfeeds.OrderByDescending(x => x.FeedDate).ToList();
                return new JavaScriptSerializer().Serialize(fbfeeds);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return "Something Went Wrong";
            }
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string getAllReadFbFeeds(string ProfileId)
        {
            try
            {
                List<Domain.Socioboard.Domain.FacebookFeed> lstFacebookFeed = objFacebookFeedRepository.getAllReadFbFeeds(ProfileId);
                return new JavaScriptSerializer().Serialize(lstFacebookFeed);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return "Something Went Wrong";
            }
        }

        //getAllFeedDetail
        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string getAllFeedDetail(string ProfileId)
        {
            try
            {
                List<Domain.Socioboard.Domain.FacebookFeed> lstFacebookFeed = objFacebookFeedRepository.getAllFeedDetail(ProfileId);
                return new JavaScriptSerializer().Serialize(lstFacebookFeed);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return "Something Went Wrong";
            }
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string getAllFeedDetail1(string ProfileId, string userid)
        {
            try
            {
                List<Domain.Socioboard.Domain.FacebookFeed> lstFacebookFeed = objFacebookFeedRepository.getAllFeedDetail1(ProfileId, Guid.Parse(userid));
                return new JavaScriptSerializer().Serialize(lstFacebookFeed);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return "Something Went Wrong";
            }
        }


        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string GetFacebookFeedByFeedId(string userid, string feedid)
        {
            ObjectId fid = ObjectId.Parse(feedid);
            MongoRepository boardrepo = new MongoRepository("MongoFacebookFeed");
            try
            {

                var result = boardrepo.Find<MongoFacebookFeed>(x => x.Id.Equals(fid) && x.UserId.Equals(userid)).ConfigureAwait(false);

                var task = Task.Run(async () =>
                {
                    return await result;
                });
                IList<MongoFacebookFeed> objfbfeeds = task.Result;
               MongoFacebookFeed fbfeeds = objfbfeeds.FirstOrDefault();
                return new JavaScriptSerializer().Serialize(fbfeeds);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return "Something Went Wrong";
            }
            //try
            //{
            //    Domain.Socioboard.Domain.FacebookFeed _FacebookFeed = objFacebookFeedRepository.GetFacebookFeedByFeedId(Guid.Parse(userid), feedid);
            //    return new JavaScriptSerializer().Serialize(_FacebookFeed);
            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine(ex.StackTrace);
            //    return "Something Went Wrong";
            //}
        }

        // Edited by Antima[20/12/2014]

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string getFacebookFeedByProfileId(string ProfileId, string FeedId)
        {
            ObjectId fid = ObjectId.Parse(FeedId);
            MongoRepository boardrepo = new MongoRepository("MongoFacebookFeed");
            try
            {

                var result = boardrepo.Find<MongoFacebookFeed>(x => x.Id.Equals(fid) && x.ProfileId.Equals(ProfileId)).ConfigureAwait(false);

                var task = Task.Run(async () =>
                {
                    return await result;
                });
                IList<MongoFacebookFeed> objfbfeeds = task.Result;
                MongoFacebookFeed fbfeeds = objfbfeeds.FirstOrDefault();
                return new JavaScriptSerializer().Serialize(fbfeeds);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return "Something Went Wrong";
            }

            //try
            //{
            //    Domain.Socioboard.Domain.FacebookFeed facebookfeed = objFacebookFeedRepository.getFacebookFeedByProfileId(ProfileId, FeedId);
            //    return new JavaScriptSerializer().Serialize(facebookfeed);
            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine(ex.StackTrace);
            //    return null;
            //}
        }


        //Added by Sumit Gupta [12-02-15]
        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string getAllFacebookFeedsByUserIdWithRange(string UserId, string keyword, string noOfDataToSkip)
        {
            List<Domain.Socioboard.Domain.FacebookFeed> lstFacebookFeed = new List<Domain.Socioboard.Domain.FacebookFeed>();
            try
            {
                //if (objFacebookFeedRepository.checkFacebookUserExists(ProfileId, Guid.Parse(UserId)))
                {
                    lstFacebookFeed = objFacebookFeedRepository.getAllFacebookFeedsOfSBUserWithRange(UserId, keyword, noOfDataToSkip);
                }
                //else
                //{
                //    lstFacebookFeed = objFacebookFeedRepository.getAllFacebookUserFeeds(ProfileId);
                //}
                return new JavaScriptSerializer().Serialize(lstFacebookFeed);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return "Something Went Wrong";
            }
        }

        //getAllFacebookFeedsByUserIdAndProfileId1
        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string getAllFacebookFeedsByUserIdAndProfileId1WithRange(string UserId, string keyword, string ProfileId, string count)
        {
            //try
            //{
            //    List<Domain.Socioboard.Domain.FacebookFeed> lstFacebookFeed = objFacebookFeedRepository.getAllFacebookFeedsOfSBUserWithProfileIdAndRange(UserId, ProfileId, keyword, count);
            //    return new JavaScriptSerializer().Serialize(lstFacebookFeed);
            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine(ex.StackTrace);
            //    return "Something Went Wrong";
            //}




            MongoRepository boardrepo = new MongoRepository("MongoFacebookFeed");
            try
            {

                var result = boardrepo.Find<MongoFacebookFeed>(x =>  x.UserId.Equals(Guid.Parse(UserId)) && x.ProfileId.Equals(ProfileId)&&x.FeedDescription.Contains(keyword)).ConfigureAwait(false);

                var task = Task.Run(async () =>
                {
                    return await result;
                });
                IList<MongoFacebookFeed> objfbfeeds = task.Result;
                return new JavaScriptSerializer().Serialize(objfbfeeds.OrderByDescending(x => x.FeedDate).Take(20));

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return "Something Went Wrong";
            }
        }

        [WebMethod]
        public string GetPageFeed(string ProfileId, string days)
        {
            try
            {
                return new JavaScriptSerializer().Serialize(objFacebookFeedRepository.GetFacebookPagePostByDay(ProfileId, Int32.Parse(days)));
            }
            catch (Exception ex)
            {
                return new JavaScriptSerializer().Serialize(new List<Domain.Socioboard.Domain.FacebookPagePost>());
            }
        }

        [WebMethod]
        //[ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public int GetFeedCountByProfileIdAndUserId(string UserId, string ProfileIds)
        {
            try 
            {
                return objFacebookFeedRepository.GetFeedCountByProfileIdAndUserId(Guid.Parse(UserId), ProfileIds);
            }catch(Exception ex){
                return 0;
            }
        }

    }
}

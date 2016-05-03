using Api.Socioboard.Model;
using Domain.Socioboard.Domain;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;

namespace Api.Socioboard.Controllers
{
    [Api.Socioboard.App_Start.AllowCrossSiteJson]
    public class ApiFacebookAccountController : ApiController
    {
        ILog logger = LogManager.GetLogger(typeof(ApiGroupMembersController));
        FacebookAccountRepository objFacebookAccountRepository = new FacebookAccountRepository();

        [HttpGet]
        public IHttpActionResult GetFacebookAcoount(string ProfileId)
        {
            try
            {
                Domain.Socioboard.Domain.FacebookAccount objFacebookAccount = objFacebookAccountRepository.getFacebookAccountDetailsById(ProfileId);
                return Ok(objFacebookAccount);
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                logger.Error(ex.StackTrace);
                return BadRequest("Something Went Wrong");
            }
        }

        [HttpGet]
        public IHttpActionResult GetFacebookPublicPages(string UserId)
        {
            List<Domain.Socioboard.Domain.FacebookAccount> lstFbAcc = objFacebookAccountRepository.GetAllFacebookPublicPagesOfUser(Guid.Parse(UserId));
            return Ok(lstFbAcc);
        }


        [HttpGet]
        public IHttpActionResult GetFacebookPostComment(string PostId)
        {
            MongoRepository fbPostRepo = new MongoRepository("FbPostComment");
            try
            {

                var result = fbPostRepo.Find<Domain.Socioboard.MongoDomain.FbPostComment>(t => t.PostId.Equals(PostId)).ConfigureAwait(false);

                var task = Task.Run(async () =>
                {
                    return await result;
                });
                IList<Domain.Socioboard.MongoDomain.FbPostComment> lstFbPostComments = task.Result;
                return Ok(lstFbPostComments.OrderByDescending(x => x.Commentdate).ToList());
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                logger.Error(ex.StackTrace);
                return BadRequest("Something Went Wrong");
            }
        }


        [ApiExplorerSettings(IgnoreApi = true)]
        [HttpGet]
        public IHttpActionResult FacebookPostsCommentsService()
        {
            Facebook.FacebookClient fb = new Facebook.FacebookClient();
            UserRepository userRepo = new UserRepository();
            FacebookAccountRepository fbAccRepo = new FacebookAccountRepository();
            Api.Socioboard.Services.Facebook fbService = new Api.Socioboard.Services.Facebook();
            List<Domain.Socioboard.Domain.User> lstUser = new List<Domain.Socioboard.Domain.User>();
            lstUser = userRepo.getAllUsers();
            foreach (var user in lstUser)
            {
                List<Domain.Socioboard.Domain.FacebookAccount> lstFacebookAccount = fbAccRepo.GetAllFacebookAccountByUserId(user.Id);

                foreach (var fbAcc in lstFacebookAccount)
                {
                    if (!string.IsNullOrEmpty(fbAcc.AccessToken))
                    {
                        fb.AccessToken = fbAcc.AccessToken;
                        MongoRepository boardrepo = new MongoRepository("MongoFacebookFeed");
                        try
                        {

                            var result = boardrepo.Find<MongoFacebookFeed>(x => x.ProfileId.Equals(fbAcc.FbUserId) && x.UserId.Equals(user.Id.ToString())).ConfigureAwait(false);

                            var task = Task.Run(async () =>
                            {
                                return await result;
                            });
                            IList<MongoFacebookFeed> objfbfeeds = task.Result;
                            if (objfbfeeds.Count() == 0)
                            {

                                result = boardrepo.Find<MongoFacebookFeed>(x => x.ProfileId.Equals(fbAcc.FbUserId)).ConfigureAwait(false);

                                task = Task.Run(async () =>
                                {
                                    return await result;
                                });
                            }
                            List<MongoFacebookFeed> fbfeeds = objfbfeeds.ToList();
                            foreach (var post in fbfeeds)
                            {
                                fbService.AddFbPostComments(post.FeedId, fb, user.Id.ToString());
                            }

                        }
                        catch (Exception ex)
                        {
                            logger.Error(ex.Message);
                            logger.Error(ex.StackTrace);
                            return BadRequest("Something Went Wrong");
                        }
                    }



                }



            }


            return Ok();
        }


        [ApiExplorerSettings(IgnoreApi = true)]
        [HttpGet]
        public IHttpActionResult FacebookPostsCommentsServiceByEmail(string Email)
        {
            Facebook.FacebookClient fb = new Facebook.FacebookClient();
            UserRepository userRepo = new UserRepository();
            FacebookAccountRepository fbAccRepo = new FacebookAccountRepository();
            Api.Socioboard.Services.Facebook fbService = new Api.Socioboard.Services.Facebook();
            Domain.Socioboard.Domain.User lstUser = new Domain.Socioboard.Domain.User();
            lstUser = userRepo.getUserInfoByEmail(Email);

            List<Domain.Socioboard.Domain.FacebookAccount> lstFacebookAccount = fbAccRepo.GetAllFacebookAccountByUserId(lstUser.Id);

            foreach (var fbAcc in lstFacebookAccount)
            {
                if (!string.IsNullOrEmpty(fbAcc.AccessToken))
                {
                    fb.AccessToken = fbAcc.AccessToken;
                    MongoRepository boardrepo = new MongoRepository("MongoFacebookFeed");
                    try
                    {

                        var result = boardrepo.Find<MongoFacebookFeed>(x => x.ProfileId.Equals(fbAcc.FbUserId) && x.UserId.Equals(lstUser.Id.ToString())).ConfigureAwait(false);

                        var task = Task.Run(async () =>
                        {
                            return await result;
                        });
                        IList<MongoFacebookFeed> objfbfeeds = task.Result;
                        if (objfbfeeds.Count() == 0)
                        {

                            result = boardrepo.Find<MongoFacebookFeed>(x => x.ProfileId.Equals(fbAcc.FbUserId)).ConfigureAwait(false);

                            task = Task.Run(async () =>
                            {
                                return await result;
                            });
                        }
                        List<MongoFacebookFeed> fbfeeds = objfbfeeds.ToList();
                        foreach (var post in fbfeeds)
                        {
                            fbService.AddFbPostComments(post.FeedId, fb, lstUser.Id.ToString());
                        }

                    }
                    catch (Exception ex)
                    {
                        logger.Error(ex.Message);
                        logger.Error(ex.StackTrace);
                        return BadRequest("Something Went Wrong");
                    }
                }
            }
            return Ok();
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

namespace Api.Socioboard.Controllers
{
    /// <summary> 
    /// public page reports 
    /// </summary>
    public class ApiFbPublicPageReportsController : ApiController
    {
        private    Api.Socioboard.Model.FbPublicpageReportsRepository fbPublicPageRepo = new  Model.FbPublicpageReportsRepository();

        [ApiExplorerSettings(IgnoreApi = true)]
        [HttpGet]
        public IHttpActionResult CreateDailyReports()
        {
            Api.Socioboard.Model.FacebookAccountRepository fbAccRepo = new Api.Socioboard.Model.FacebookAccountRepository();
            Api.Socioboard.Model.FbPagePostRepository fbPagePostRepo = new Api.Socioboard.Model.FbPagePostRepository();
            List<Domain.Socioboard.Domain.FacebookAccount> lstFbAccs = fbAccRepo.GetAllFacebookPublicPages();
            foreach (var item in lstFbAccs)
            {
                List<Domain.Socioboard.Domain.FbPagePost> lstFbPagePost = fbPagePostRepo.GetPostsByDate(DateTime.UtcNow, item.FbUserId, item.UserId);
                if (lstFbPagePost != null && lstFbPagePost.Count() > 0)
                {

                    Domain.Socioboard.Domain.Fbpublicpagereports fbPublicPagereports = new Domain.Socioboard.Domain.Fbpublicpagereports();
                    fbPublicPagereports.Id = Guid.NewGuid();
                    fbPublicPagereports.Date = DateTime.UtcNow;
                    fbPublicPagereports.Pageid = item.FbUserId;
                    fbPublicPagereports.Commentscount = lstFbPagePost.Sum(t => t.Comments);
                    fbPublicPagereports.Likescount = lstFbPagePost.Sum(t => t.Likes);
                    fbPublicPagereports.Sharescount = lstFbPagePost.Sum(t => t.Shares);
                    fbPublicPagereports.Postscount = lstFbPagePost.Count();

                    if (!fbPublicPageRepo.IsReportExist(DateTime.UtcNow, item.FbUserId)) 
                    {
                        fbPublicPageRepo.addFacebookPageReports(fbPublicPagereports);
                    }

                }


            }


            return Ok();
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        [HttpGet]
        public IHttpActionResult CretePreviousReports() 
        {
            Api.Socioboard.Helper.FbPublicPageHelper.CreatePagePerviousDayReports();
            return Ok();
        }

        /// <summary>Gets the public page reports for given number of days</summary>
        /// <param name="days">The number of days from current date.</param>
        /// <param name="PageID">Facebook public page Id.</param>
        [HttpGet]
        public IHttpActionResult GetPageReports(int days, string PageID) 
        {
            List<Domain.Socioboard.Domain.Fbpublicpagereports> fbPublicPageReports =fbPublicPageRepo.GetReports(PageID,DateTime.UtcNow,DateTime.UtcNow.AddDays(-1 *days));

            return Ok(fbPublicPageReports);
        }


        public IHttpActionResult GetPages(string UserId) 
        {
            return Ok();
        }

        
    }
}

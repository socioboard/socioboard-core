using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Api.Socioboard.Helper
{
    public static class FbPublicPageHelper
    {
        public static bool CreatePagePerviousDayReports() 
        {
            Api.Socioboard.Model.FacebookAccountRepository fbAccRepo = new Api.Socioboard.Model.FacebookAccountRepository();
            Api.Socioboard.Model.FbPagePostRepository fbPagePostRepo = new Api.Socioboard.Model.FbPagePostRepository();
            Api.Socioboard.Model.FbPublicpageReportsRepository fbPublicPageRepo = new Model.FbPublicpageReportsRepository();
            List<Domain.Socioboard.Domain.FacebookAccount> lstFbAccs = fbAccRepo.GetAllFacebookPublicPages();
            for (int i = 1; i <= 90; i++) 
            {
              
                foreach (var item in lstFbAccs)
                {
                    List<Domain.Socioboard.Domain.FbPagePost> lstFbPagePost = fbPagePostRepo.GetPostsByDate(DateTime.UtcNow.AddDays(-1 *i), item.FbUserId, item.UserId);
                    if (lstFbPagePost != null && lstFbPagePost.Count() > 0)
                    {

                        Domain.Socioboard.Domain.Fbpublicpagereports fbPublicPagereports = new Domain.Socioboard.Domain.Fbpublicpagereports();
                        fbPublicPagereports.Id = Guid.NewGuid();
                        fbPublicPagereports.Date = DateTime.UtcNow.AddDays(-1 * i);
                        fbPublicPagereports.Pageid = item.FbUserId;
                        fbPublicPagereports.Commentscount = lstFbPagePost.Sum(t => t.Comments);
                        fbPublicPagereports.Likescount = lstFbPagePost.Sum(t => t.Likes);
                        fbPublicPagereports.Sharescount = lstFbPagePost.Sum(t => t.Shares);
                        fbPublicPagereports.Postscount = lstFbPagePost.Count();

                        if (!fbPublicPageRepo.IsReportExist(DateTime.UtcNow.AddDays(-1 * i), item.FbUserId))
                        {
                            fbPublicPageRepo.addFacebookPageReports(fbPublicPagereports);
                        }

                    }


                }

            }
            return true;
        }
    }
}
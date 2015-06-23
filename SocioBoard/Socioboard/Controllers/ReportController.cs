using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using System.Web.UI.WebControls;
using Domain.Socioboard.Domain;
using log4net;
using Socioboard.App_Start;

namespace Socioboard.Controllers
{
    [Authorize]
    [CustomAuthorize]
    public class ReportController : BaseController
    {
        ILog logger = LogManager.GetLogger(typeof(ReportController));
        //
        // GET: /Report/

        public ActionResult Index()
        {

            if (Session["User"] != null)
            {
                if (Session["Paid_User"].ToString() == "Unpaid")
                {
                    return RedirectToAction("Billing", "PersonalSetting");
                }
                else
                {
                    return View();
                }
            }
            else
            {
                return RedirectToAction("Index", "Index");
            }
            //return View();
        }

        public ActionResult loadmenu()
        {
            return PartialView("_ReportMenuPartial", Helper.SBUtils.GetReportsMenuAccordingToGroup());
        }
     
        public ActionResult TwitterReportPartial(FormCollection frmcollection)
        {
            string twtProfileId = frmcollection["twtProfileId"].ToString();

            int days = Convert.ToInt32(frmcollection["days"]);
            return PartialView("_TwitterReportPartial",Helper.SBUtils.GetTwitterReportData(twtProfileId,days));        
        }

        public ActionResult Teamreportpartial(FormCollection frmcollection)
        {
            int days = Convert.ToInt32(frmcollection["days"]);
            return PartialView("_TeamReportPartial", Helper.SBUtils.GetTeamReportData(days));  
            //return PartialView("_TeamReportPartial");
        }

        public ActionResult GroupStatPartial(FormCollection frmcollection)
        {
            int days = Convert.ToInt32(frmcollection["days"]);
            return PartialView("_GroupStatPartial", Helper.SBUtils.GetGroupStatsData(days));              
        }

        public ActionResult FbPagePostDetails(string AccessToken, string FbUserId, string UserId)
        {
            List<FbPagePost> _FbPagePost= new List<FbPagePost>();
            _FbPagePost=Helper.SBUtils.FbPagePostDetails(FbUserId, UserId);

            if (_FbPagePost.Count > 0)
                return PartialView("_FbPageReport", _FbPagePost);
            else
                return Content("");
        }



        public ActionResult FbPageReports(string AccessToken, string FbUserId, int days)
        {
            // AccessToken = "CAACEdEose0cBAGAedAd7bVYNydZCCWkpId9v4MLtZBXv9VjF7nHszkAhqonnoKfKwzxnMzvqOhc75P3TMugZBAnVGShE7b3ZBhjVSpzHeDRZCxiliZBLRC7jSi538SKh69nZCL3W5czZAgw5weWub4ZBEcKhZCMFS8toWm3PJA8ZBol3XaStd3v6NPXt5QXTZCS2Md1hPrGmkMOZCccrUZBcQfmT4BLxZBViPGSRaoZD";
            //FbUserId = "370359689834664";
            ViewBag.AccessToken = AccessToken;
            ViewBag.FbUserId = FbUserId;
            ViewBag.days = days;
            Helper.CompanyProfiles cp = new Helper.CompanyProfiles();
            ViewBag.Likes = cp.getFacebookPageLikes(FbUserId, AccessToken, days);
            ViewBag.Unlikes = cp.getFacebookPageUnLikes(FbUserId, AccessToken, days);
            ViewBag.Impressions = cp.getFacebookPageImpressions(FbUserId, AccessToken, days);
            ViewBag.ImpressionsOrganic = cp.getFacebookpageImpressionsOrganic(FbUserId, AccessToken, days);
            ViewBag.ImpressionsPaid = cp.getFacebookpageImpressionsPaid(FbUserId, AccessToken, days);
            ViewBag.ImpressionsViral = cp.getFacebookpageImpressionsviral(FbUserId, AccessToken, days);
            // ViewBag.PageImpressionByGender = cp.getFacebookPageImpressionsByAgenGender(FbUserId, AccessToken, days);
            ViewBag.ImpressionsByCountry = cp.getFacebookPageImpressionsByCountry(FbUserId, AccessToken, days);
            ViewBag.ImpressionsByCity = cp.getFacebookPageImpressionsByCity(FbUserId, AccessToken, days);
            return PartialView("_FbPageReports");
        }

        public ActionResult loadPostDetails(string id)
        {
            return PartialView("_FbPostDetails",id);   
        }
        public ActionResult Export(string pageid)
        {
            try
            {
                User objUser = (User)Session["User"];
                var products = new System.Data.DataTable("teste");
                products.Columns.Add("Post", typeof(string));
                products.Columns.Add("Type", typeof(string));
                products.Columns.Add("Like Count", typeof(string));
                products.Columns.Add("Comment Count", typeof(string));
                products.Columns.Add("Share Count", typeof(string));
                products.Columns.Add("Comments", typeof(string));
                products.Columns.Add("User Liked", typeof(string));

                List<FbPagePost> FbPagePostDetails =new List<FbPagePost> ();
                try
                {
                    FbPagePostDetails = Helper.SBUtils.FbPagePostDetails(pageid, objUser.Id.ToString());
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    logger.Error(ex.Message);
                    logger.Error(ex.StackTrace);
                }


                foreach (var item in FbPagePostDetails)
                {
                    try
                    {
                        string postscomments = string.Empty;
                        string postslikes = string.Empty;

                        Socioboard.Api.FbPageComment.FbPageComment ApiobjFbPageComment = new Socioboard.Api.FbPageComment.FbPageComment();
                        List<Domain.Socioboard.Domain.FbPageComment> lstFbPageComment = (List<Domain.Socioboard.Domain.FbPageComment>)(new System.Web.Script.Serialization.JavaScriptSerializer().Deserialize(ApiobjFbPageComment.GetPostComments(item.PostId.ToString()), typeof(List<Domain.Socioboard.Domain.FbPageComment>)));

                        foreach (var FbPageComment in lstFbPageComment)
                        {
                            try
                            {
                                postscomments += FbPageComment .FromName + "-->" + FbPageComment.Comment + ", ";
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex.Message);
                                logger.Error(ex.Message);
                                logger.Error(ex.StackTrace);
                            }
                        }

                        try
                        {
                            postscomments = postscomments.Substring(0, postscomments.Length - 1);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                            logger.Error(ex.Message);
                            logger.Error(ex.StackTrace);
                        }

                        Socioboard.Api.FbPageLiker.FbPageLiker apiobjFbPageLiker = new Socioboard.Api.FbPageLiker.FbPageLiker();
                        List<Domain.Socioboard.Domain.FbPageLiker> lstFbPageLiker = (List<Domain.Socioboard.Domain.FbPageLiker>)(new System.Web.Script.Serialization.JavaScriptSerializer().Deserialize(apiobjFbPageLiker.GetLikeByPostId(item.PostId.ToString(), item.UserId.ToString()), typeof(List<Domain.Socioboard.Domain.FbPageLiker>)));
                        foreach (var FbPageLiker in lstFbPageComment)
                        {
                            try
                            {
                                postslikes += FbPageLiker.FromName + ", ";
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex.Message);
                                logger.Error(ex.Message);
                                logger.Error(ex.StackTrace);
                            }
                        }
                        try
                        {
                            postslikes = postslikes.Substring(0, postslikes.Length - 1);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                            logger.Error(ex.Message);
                            logger.Error(ex.StackTrace);
                        }


                        products.Rows.Add(item.Post, item.Type, item.Likes, item.Comments, item.Shares, postscomments, postslikes);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                        logger.Error(ex.Message);
                        logger.Error(ex.StackTrace);
                    }
                }
                var grid = new GridView();
                grid.DataSource = products;
                grid.DataBind();

                Response.ClearContent();
                Response.Buffer = true;
                Response.AddHeader("content-disposition", "attachment; filename=MyExcelFile.xls");
                Response.ContentType = "application/ms-excel";

                Response.Charset = "";
                StringWriter sw = new StringWriter();
                HtmlTextWriter htw = new HtmlTextWriter(sw);

                grid.RenderControl(htw);

                Response.Output.Write(sw.ToString());
                Response.Flush();
                Response.End();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                logger.Error(ex.Message);
                logger.Error(ex.StackTrace);
            }
            return View();
        }


       
    }
}

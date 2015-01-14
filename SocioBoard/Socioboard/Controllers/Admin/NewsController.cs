using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace Socioboard.Controllers.Admin
{
    //[Authorize(Users = "Aby Kumar")]
    public class NewsController : Controller
    {
        //
        // GET: /News/

        public ActionResult ManageNews()
        {
            return View();
        }
        public ActionResult LoadNews()
        {
            Api.AdminNews.AdminNews apiobjNews = new Api.AdminNews.AdminNews();
            List<Domain.Socioboard.Domain.News> lstPackage = (List<Domain.Socioboard.Domain.News>)(new JavaScriptSerializer().Deserialize(apiobjNews.GetAllNews(), typeof(List<Domain.Socioboard.Domain.News>)));

            return View("_ManageNewsPartial", lstPackage);
        }
        public ActionResult EditNews(string Id)
        {
            Api.AdminNews.AdminNews apiobjNews = new Api.AdminNews.AdminNews();
            Domain.Socioboard.Domain.News ObjNews = (Domain.Socioboard.Domain.News)(new JavaScriptSerializer().Deserialize(apiobjNews.GetNewsById(Id), typeof(Domain.Socioboard.Domain.News)));
            Session["NewsToUpdate"] = ObjNews;
            return View(ObjNews);
        }

        public ActionResult UpdateNews(string News,string ExpiryDate,string Status)
        {
            Domain.Socioboard.Domain.News objNews = (Domain.Socioboard.Domain.News)Session["NewsToUpdate"];
            objNews.NewsDetail = News;
            objNews.ExpiryDate = Convert.ToDateTime(ExpiryDate);
            objNews.Status = bool.Parse(Status);
            string ObjPackage = (new JavaScriptSerializer().Serialize(objNews));
            Api.AdminNews.AdminNews apiobjNews = new Api.AdminNews.AdminNews();
            string NewsUpdateMessage = (string)(new JavaScriptSerializer().Deserialize(apiobjNews.UpdateNews(ObjPackage), typeof(string)));
            return Content(NewsUpdateMessage);
        }
        public ActionResult CreateNews()
        {
            return View();
        }
        public ActionResult AddNews(string News, string ExpiryDate, string Status)
        {
            Domain.Socioboard.Domain.News objNews = new Domain.Socioboard.Domain.News();
            objNews.NewsDetail = News;
            objNews.ExpiryDate = Convert.ToDateTime(ExpiryDate);
            objNews.Status = bool.Parse(Status);
            objNews.Id = Guid.NewGuid();
            string ObjPackage = (new JavaScriptSerializer().Serialize(objNews));
            Api.AdminNews.AdminNews objApiNews = new Api.AdminNews.AdminNews();
            string NewsAddedMessage = (string)(new JavaScriptSerializer().Deserialize(objApiNews.AddNews(ObjPackage,News), typeof(string)));
            return Content(NewsAddedMessage);
        }

    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace Socioboard.Controllers.Admin
{
    //[Authorize(Users = "Aby Kumar")]
    public class AdvertisementController : Controller
    {
        //
        // GET: /Advertisement/

        public ActionResult ManageAdvertisement()
        {
            if (Session["User"] != null)
            {
                Domain.Socioboard.Domain.User _User = (Domain.Socioboard.Domain.User)Session["User"];
                if (_User.UserType != "SuperAdmin")
                {
                    return RedirectToAction("Index", "Index");
                }
            }
            else
            {
                return RedirectToAction("Index", "Index");
            }

            return View();
        }

        public ActionResult LoadAdvertisement()
        {
            if (Session["User"] != null)
            {
                Domain.Socioboard.Domain.User _User = (Domain.Socioboard.Domain.User)Session["User"];
                if (_User.UserType != "SuperAdmin")
                {
                    return RedirectToAction("Index", "Index");
                }
            }
            else
            {
                return RedirectToAction("Index", "Index");
            }

            Api.Ads.Ads apiobjAds = new Api.Ads.Ads();
            List<Domain.Socioboard.Domain.Ads> lstPackage = (List<Domain.Socioboard.Domain.Ads>)(new JavaScriptSerializer().Deserialize(apiobjAds.GetAllAds(), typeof(List<Domain.Socioboard.Domain.Ads>)));

            return View("_ManageAdvertisementPartial", lstPackage);
        }
        public ActionResult EditAdvertisement(string Id)
        {
            if (Session["User"] != null)
            {
                Domain.Socioboard.Domain.User _User = (Domain.Socioboard.Domain.User)Session["User"];
                if (_User.UserType != "SuperAdmin")
                {
                    return RedirectToAction("Index", "Index");
                }
            }
            else
            {
                return RedirectToAction("Index", "Index");
            }

            Api.Ads.Ads apiobjNews = new Api.Ads.Ads();
            Domain.Socioboard.Domain.Ads ObjAdvertise = (Domain.Socioboard.Domain.Ads)(new JavaScriptSerializer().Deserialize(apiobjNews.GetAdsdetailsById(Id), typeof(Domain.Socioboard.Domain.Ads)));
            Session["AdvertiseToToUpdate"] = ObjAdvertise;
            return View(ObjAdvertise);
        }
        public ActionResult UpdateAdvertisement(string Advertisement, string AdsExpiryDate, string Status, string AdsImageUrl)
        {
            if (Session["User"] != null)
            {
                Domain.Socioboard.Domain.User _User = (Domain.Socioboard.Domain.User)Session["User"];
                if (_User.UserType != "SuperAdmin")
                {
                    return RedirectToAction("Index", "Index");
                }
            }
            else
            {
                return RedirectToAction("Index", "Index");
            }

            Domain.Socioboard.Domain.Ads objAds = (Domain.Socioboard.Domain.Ads)Session["AdvertiseToToUpdate"];
            objAds.Advertisment = Advertisement;
            objAds.ExpiryDate = Convert.ToDateTime(AdsExpiryDate);
            objAds.Status = bool.Parse(Status);
            var fi = Request.Files["advfile"];
            string file = string.Empty;
            if (Request.Files.Count > 0)
            {
                if (fi != null)
                {
                    var path = Server.MapPath("~/Themes/" + System.Configuration.ConfigurationManager.AppSettings["domain"] + "/Contents/img/admin/AdvertiseImage");

                    // var path = System.Configuration.ConfigurationManager.AppSettings["MailSenderDomain"]+"Contents/img/upload";
                    file = path + "/" + fi.FileName;
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }
                    fi.SaveAs(file);
                    path = path + "/" + fi.FileName;
                    objAds.ImageUrl = path.ToString();
                }
            }
            else
            {
                var path = Server.MapPath("~/Themes/" + System.Configuration.ConfigurationManager.AppSettings["domain"] + "/Contents/img/admin/AdvertiseImage");

                file = path + "/" + AdsImageUrl;
                objAds.ImageUrl = file;
            }



            string ObjAdvertisement = (new JavaScriptSerializer().Serialize(objAds));
            Api.Ads.Ads apiobjNews = new Api.Ads.Ads();
            string NewsAdsMessage = (string)(new JavaScriptSerializer().Deserialize(apiobjNews.UpdateAdvertisement(ObjAdvertisement), typeof(string)));
            return Content(NewsAdsMessage);
        }

        public ActionResult CreateAdvertisement()
        {
            if (Session["User"] != null)
            {
                Domain.Socioboard.Domain.User _User = (Domain.Socioboard.Domain.User)Session["User"];
                if (_User.UserType != "SuperAdmin")
                {
                    return RedirectToAction("Index", "Index");
                }
            }
            else
            {
                return RedirectToAction("Index", "Index");
            }
            return View();
        }
        public ActionResult AddAdvertisement(string Advertisement, string AdsExpiryDate, string Status, string AdsImageUrl)
        {
            if (Session["User"] != null)
            {
                Domain.Socioboard.Domain.User _User = (Domain.Socioboard.Domain.User)Session["User"];
                if (_User.UserType != "SuperAdmin")
                {
                    return RedirectToAction("Index", "Index");
                }
            }
            else
            {
                return RedirectToAction("Index", "Index");
            }

            Domain.Socioboard.Domain.Ads objAds = new Domain.Socioboard.Domain.Ads();
            objAds.Id = Guid.NewGuid();
            objAds.Advertisment = Advertisement;
            objAds.EntryDate = DateTime.Now;
            objAds.ExpiryDate = Convert.ToDateTime(AdsExpiryDate);
            objAds.Status = bool.Parse(Status);
            var fi = Request.Files["advsfile"];
            string file = string.Empty;
            if (Request.Files.Count > 0)
            {
                if (fi != null)
                {
                    var path = Server.MapPath("~/Themes/" + System.Configuration.ConfigurationManager.AppSettings["domain"] + "/Contents/img/admin/AdvertiseImage");

                    // var path = System.Configuration.ConfigurationManager.AppSettings["MailSenderDomain"]+"Contents/img/upload";
                    file = path + "/" + fi.FileName;
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }
                    fi.SaveAs(file);
                    path = path + "/" + fi.FileName;
                    objAds.ImageUrl = path.ToString();
                }
            }
            else
            {
                var path = Server.MapPath("~/Themes/" + System.Configuration.ConfigurationManager.AppSettings["domain"] + "/Contents/img/admin/AdvertiseImage");

                file = path + "/" + AdsImageUrl;
                objAds.ImageUrl = file;
            }



            string ObjAdvertisement = (new JavaScriptSerializer().Serialize(objAds));
            Api.Ads.Ads apiobjNews = new Api.Ads.Ads();
            string NewsAdsMessage = (string)(new JavaScriptSerializer().Deserialize(apiobjNews.AddAdvertisement(ObjAdvertisement, Advertisement), typeof(string)));
            return Content(NewsAdsMessage);
        }

    }
}

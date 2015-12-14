using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Socioboard.Controllers
{
    public class GoogleAnalyticsController : Controller
    {
        //
        // GET: /GoogleAnalytics/
        public ActionResult googleanalytics()
        {

            if (Session["User"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Index");
            }

        }
        public ActionResult googleacquisition()
        {

            if (Session["User"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Index");
            }

        }

        public ActionResult browserandMobiles()
        {

            if (Session["User"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Index");
            }

        }

        public ActionResult sitespeed()
        {
            if (Session["User"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Index");
            }
        }

        //public ActionResult googleadwords()
        //{

        //    if (Session["User"] != null)
        //    {
        //        return View();
        //    }
        //    else
        //    {
        //        return RedirectToAction("Index", "Index");
        //    }
        //}

        //public ActionResult socials()
        //{

        //    if (Session["User"] != null)
        //    {
        //        return View();
        //    }
        //    else
        //    {
        //        return RedirectToAction("Index", "Index");
        //    }
        //}
	}
}
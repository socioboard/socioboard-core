using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Socioboard.Controllers
{
    public class AdminHomeController : Controller
    {
        //
        // GET: /AdminHome/

        public ActionResult Dashboard()
        {
            return View();
        }

    }
}

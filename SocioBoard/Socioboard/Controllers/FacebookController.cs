using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Socioboard.Controllers
{
    public class FacebookController : BaseController
    {
        //
        // GET: /Facebook/

        public ActionResult Index(string code)
        {
           // string str = Request.QueryString["code"].ToString();
            return View();
        }

    }
}

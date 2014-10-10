using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Domain.Socioboard.Domain;

namespace Socioboard.Controllers
{
    public class PersonalSettingController : Controller
    {
        //
        // GET: /PersonalSetting/

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult LoadPersonalSetting()
        {
              User objUser = (User)Session["User"];
              return PartialView("_PersonalSettingPartial", objUser);
        }
        public ActionResult EditUserInfo(string id,string fname,string lname,string email,string dt)
        {
              User objUser = (User)Session["User"];
              Api.User.User ApiobjUser = new Api.User.User();
              string ret = ApiobjUser.UpdateUser(id, fname, lname, dt);
              if (ret == "1")
              {
                  objUser.UserName = fname + " " + lname;
                  objUser.TimeZone = dt;
                  Session["User"] = objUser;
              }
              return Content(ret);
        }


        
        

    }
}

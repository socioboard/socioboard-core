using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Domain.Socioboard.Domain;
using Socioboard.Helper;
using System.Web.Script.Serialization;
using System.IO;

namespace Socioboard.Controllers.Admin
{
    [Authorize(Users = "Aby Kumar")]
    public class AdminSettingController : Controller
    {
        //
        // GET: /AdminSetting/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult LoadAdminSetting()
        {
            Domain.Socioboard.Domain.Admin objAdmin = (Domain.Socioboard.Domain.Admin)Session["AdminProfile"];
            return PartialView("_AdminSettingPartial");
        }

        [HttpPost]
        public ActionResult ChangeAdminPassword()
        {
            string NewPassword = Request.Form["NewPassword"].ToString();
            string OldPassword = Request.Form["OldPassword"].ToString();
            string returnmsg=string.Empty;
            Api.Admin.Admin ObjApiAdmin = new Api.Admin.Admin();
            Domain.Socioboard.Domain.Admin objAdmin = (Domain.Socioboard.Domain.Admin)Session["AdminProfile"];
            if (objAdmin.Password == OldPassword)
            {
                if (NewPassword != OldPassword)
                {
                    string ChngePasswordMessage = (string)(new JavaScriptSerializer().Deserialize(ObjApiAdmin.ChangeAdminPassword(NewPassword, OldPassword, objAdmin.UserName.ToString()), typeof(string)));
                    returnmsg = ChngePasswordMessage;
                    if (ChngePasswordMessage == "Password Changed Successfully")
                    {
                        objAdmin.Password = NewPassword;
                    }
                }
                else
                {
                    returnmsg = "You can't Use Old password as New Password!";
                }
            }
            else
            {
                returnmsg = "Entered Old Password Not Match!";
            }
            return Content(returnmsg);
        }

        public ActionResult UpdateAdminSettingData()
        {
            Domain.Socioboard.Domain.Admin objAdmin = (Domain.Socioboard.Domain.Admin)Session["AdminProfile"];
            objAdmin.FirstName = Request.Form["Adminfname"];
            objAdmin.LastName = Request.Form["Adminlname"];
            objAdmin.UserName = Request.Form["Adminusername"];
            objAdmin.TimeZone = Request.Form["AdminTimeZone"];
            var fi = Request.Files["adminprofileimage"];
            string file = string.Empty;
            if (Request.Files.Count > 0)
            {
                if (fi != null)
                {
                    var path = Server.MapPath("~/Themes/" + System.Configuration.ConfigurationManager.AppSettings["domain"] + "/Contents/img/admin");

                    // var path = System.Configuration.ConfigurationManager.AppSettings["MailSenderDomain"]+"Contents/img/upload";
                    file = path + "/" + fi.FileName;
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }
                    fi.SaveAs(file);
                    path = path + "/" + fi.FileName;
                    objAdmin.Image = path.ToString();
                }
            }



            string ObjAdminUpdate = (new JavaScriptSerializer().Serialize(objAdmin));
             Api.Admin.Admin ObjApiAdmin = new Api.Admin.Admin();
            string UpdateChnfesMessage = (string)(new JavaScriptSerializer().Deserialize(ObjApiAdmin.UpdateAdminSetting(ObjAdminUpdate), typeof(string)));
            if(UpdateChnfesMessage=="Setting Updated Successfully")
            {
                Session["AdminProfile"]=objAdmin;
            }
            return Content(UpdateChnfesMessage);
        }

    }
}

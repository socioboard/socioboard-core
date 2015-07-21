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
using System.Security.Cryptography;
using System.Text;

namespace Socioboard.Controllers.Admin
{
    //[Authorize(Users = "Aby Kumar")]
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
            Domain.Socioboard.Domain.User ObjUser = (Domain.Socioboard.Domain.User)Session["User"];
            return PartialView("_AdminSettingPartial");
        }

        [HttpPost]
        public ActionResult ChangeAdminPassword()
        {
            string NewPassword = Request.Form["NewPassword"].ToString();
            string OldPassword = Request.Form["OldPassword"].ToString();
            string returnmsg=string.Empty;
            Api.User.User ApiobjUser = new Api.User.User();
            Domain.Socioboard.Domain.User ObjUser = (Domain.Socioboard.Domain.User)Session["User"];
            string OldPaswrd = EncodePassword(OldPassword);
            if (ObjUser.Password == OldPaswrd)
            {
                if (NewPassword != OldPaswrd)
                {
                    string ChngePasswordMessage = ApiobjUser.ChangePassword(ObjUser.EmailId.ToString(), OldPassword, NewPassword, Session["access_token"].ToString());
                    returnmsg = ChngePasswordMessage;
                    if (ChngePasswordMessage == "Password Changed Successfully")
                    {
                        ObjUser.Password = EncodePassword(NewPassword);
                        Session["User"] = ObjUser;
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
            Domain.Socioboard.Domain.User ObjUser = (Domain.Socioboard.Domain.User)Session["User"];
            string FirstName = Request.Form["Adminfname"];
            string LastName = Request.Form["Adminlname"];
            string TimeZone = Request.Form["AdminTimeZone"];
            var fi = Request.Files["adminprofileimage"];
            string file = string.Empty;
            string UpdateChnfesMessage=string.Empty;
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
                    ObjUser.ProfileUrl = path.ToString();
                }
            }



            //string ObjAdminUpdate = (new JavaScriptSerializer().Serialize(ObjUser));
            Api.User.User ApiobjUser = new Api.User.User();
            string ret = ApiobjUser.UpdateAdminUser(ObjUser.Id.ToString(), FirstName, LastName, TimeZone, ObjUser.ProfileUrl);
            if (ret == "1")
            {
                ObjUser.UserName = FirstName + " " + LastName;
                ObjUser.TimeZone = TimeZone;
                Session["User"] = ObjUser;
                UpdateChnfesMessage = "Setting Updated Successfully";
            }
            return Content(UpdateChnfesMessage);
        }

        public string EncodePassword(string originalPassword)
        {
            MD5 md5 = new MD5CryptoServiceProvider();

            //compute hash from the bytes of text
            md5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(originalPassword));

            //get hash result after compute it
            byte[] result = md5.Hash;

            StringBuilder strBuilder = new StringBuilder();
            for (int i = 0; i < result.Length; i++)
            {
                //change it into 2 hexadecimal digits
                //for each byte
                strBuilder.Append(result[i].ToString("x2"));
            }

            return strBuilder.ToString();
        }


    }
}

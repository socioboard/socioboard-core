using Domain.Socioboard.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace Socioboard.Controllers.Admin
{
    //[Authorize(Users = "Aby Kumar")]
    public class AddNewsLatterController : Controller
    {
        
        //
        // GET: /AddNewsLatter/

        //public ActionResult ManageAddNewsLatter()
        //{
        //    return View();
        //}
        public ActionResult LoadAddNewsLatter()
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
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            serializer.MaxJsonLength = 2147483647;

            Domain.Socioboard.Domain.User ObjUser = (Domain.Socioboard.Domain.User)Session["User"];
            string Objuser = (new JavaScriptSerializer().Serialize(ObjUser));
            Api.AdminUserDetails.AdminUserDetails ApiObjuserdetails = new Api.AdminUserDetails.AdminUserDetails();
            List<User> lstUser = (List<User>)(serializer.Deserialize(ApiObjuserdetails.GetAllUsers(Objuser), typeof(List<User>)));
            return View(lstUser);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult SendAddNewsLatter(string Subject, string NewsBody, string SendDate, string UserIds, string UserEmails, string UserNames) 
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
            DateTime dt = Convert.ToDateTime(SendDate).Date;
            string[] UsersId = UserIds.Split(',');
            string[] UserEmail = UserEmails.Split(',');
            string[] UserName = UserNames.Split(','); 
            //string mailsender = null;
            string returnmsg = string.Empty;
            int i = 0;
            foreach (var item in UsersId)
            {
                Api.User.User ApiObjUser=new Api.User.User();
                //Domain.Socioboard.Domain.User objUser = (Domain.Socioboard.Domain.User)(new JavaScriptSerializer().Deserialize(ApiObjUser.getUsersById(item.ToString(), Session["access_token"].ToString()), typeof(Domain.Socioboard.Domain.User)));
                Domain.Socioboard.Domain.NewsLetter objNewsLatter = new NewsLetter();
                //if (dt == DateTime.Now.Date)
                //{

                //    try
                //    {
                //        Api.MailSender.MailSender ApiObjMailForNewsLtr = new Api.MailSender.MailSender();
                //        mailsender = ApiObjMailForNewsLtr.SendAddNewsLatterMail(objUser.EmailId.ToString(), NewsBody, Subject);
                //        ObjNewsLatter.Id = Guid.NewGuid();
                //        ObjNewsLatter.Subject = Subject;
                //        ObjNewsLatter.NewsLetterBody = NewsBody;
                //        ObjNewsLatter.SendDate = DateTime.Parse(SendDate);
                //        ObjNewsLatter.SendStatus = true;
                //        ObjNewsLatter.UserId = Guid.Parse(item.ToString());
                //        ObjNewsLatter.EntryDate = DateTime.Now;
                //        string Objltr=new JavaScriptSerializer().Serialize(ObjNewsLatter);
                //        Api.NewsLetter.NewsLetter objApiNewsltr = new Api.NewsLetter.NewsLetter();
                //        returnmsg = (string)(new JavaScriptSerializer().Deserialize(objApiNewsltr.AddNewsLatter(Objltr), typeof(string)));

                //    }
                //    catch (Exception ex)
                //    {
                //        Console.WriteLine(ex.StackTrace);
                //    }
                //}
                //else {
                    objNewsLatter.Id = Guid.NewGuid();
                    objNewsLatter.Subject = Subject;
                    objNewsLatter.NewsLetterBody = NewsBody;
                    objNewsLatter.SendDate = DateTime.Parse(SendDate);
                    objNewsLatter.SendStatus = false;
                    objNewsLatter.UserId = Guid.Parse(item.ToString());
                    objNewsLatter.Email = UserEmail[i];
                    objNewsLatter.Name=UserName[i];
                    objNewsLatter.EntryDate = DateTime.Now;
                    string objltr = new JavaScriptSerializer().Serialize(objNewsLatter);
                    Api.NewsLetter.NewsLetter objApiNewsltr = new Api.NewsLetter.NewsLetter();
                    returnmsg = objApiNewsltr.AddNewsLatter(objltr);
                //}   
                    i++;
            }
            

            return Content(returnmsg);
        }
    }
}

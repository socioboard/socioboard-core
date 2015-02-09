using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Domain.Socioboard.Domain;
using Socioboard.App_Start;


namespace Socioboard.Controllers
{
    [Authorize]
    [CustomAuthorize]
    public class DropboxManagerController : BaseController
    {
        //
        // GET: /DropboxManagerController/

        public ActionResult Index()
        {
            return View();
        }


        public ActionResult DropBox()
        {
            Api.Dropbox.Dropbox _Dropbox = new Api.Dropbox.Dropbox();
            Api.DropboxAccount.DropboxAccount _DropboxAccount = new Api.DropboxAccount.DropboxAccount();
            Domain.Socioboard.Domain.User objUser = (Domain.Socioboard.Domain.User)Session["User"];
            DropboxAccount _UserDetails = Newtonsoft.Json.JsonConvert.DeserializeObject<DropboxAccount>(_DropboxAccount.GetDropboxAccountDetailsByUserId(objUser.Id.ToString()));
             
            if (_UserDetails != null)
            {
                var _DropboxImageLast = Newtonsoft.Json.JsonConvert.DeserializeObject<List<string>>(_Dropbox.GetUserDropBoxData(objUser.Id.ToString()));

                return PartialView("_DropBoxImagePartial", _DropboxImageLast);
            }
            else
            {
                string _DropboxredirectUri = _Dropbox.GetDropboxRedirectUrl(ConfigurationManager.AppSettings["DBX_Appkey"], ConfigurationManager.AppSettings["DBX_redirect_uri"]);
                return Content(_DropboxredirectUri);
            }
        }

        public ActionResult DropBoxManager()
        {
            Api.Dropbox.Dropbox _Dropbox = new Api.Dropbox.Dropbox();
            Domain.Socioboard.Domain.User objUser = (Domain.Socioboard.Domain.User)Session["User"];
            string _code = Request.QueryString["code"];

            _Dropbox.AddDropboxAccount(_code, objUser.Id.ToString());
            //System.Web.UI.Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Close", "window.close()", true);

            //return RedirectToAction("DropBox");
            return Content("");

        }


    }
}

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
    public class UserDetailsController : Controller
    {

        //
        // GET: /UserDetails/
        public ActionResult ManageUser()
        {
            return View();
        }
        public ActionResult LoadManageUser()
        {
            Domain.Socioboard.Domain.User ObjUser = (Domain.Socioboard.Domain.User)Session["User"];
            string Objuser = (new JavaScriptSerializer().Serialize(ObjUser));
            Api.AdminUserDetails.AdminUserDetails ApiObjuserdetails = new Api.AdminUserDetails.AdminUserDetails();
            List<User> lstUser = (List<User>)(new JavaScriptSerializer().Deserialize(ApiObjuserdetails.GetAllUsers(Objuser), typeof(List<User>)));
            return PartialView("_ManageUserPartial", lstUser);
        }

        public ActionResult EditUserDetails(string Id)
        {
            Api.AdminUserDetails.AdminUserDetails ApiobjUser = new Api.AdminUserDetails.AdminUserDetails();
            Domain.Socioboard.Domain.User user = (Domain.Socioboard.Domain.User)(new JavaScriptSerializer().Deserialize(ApiobjUser.GetUserById(Id), typeof(Domain.Socioboard.Domain.User)));

            return View(user);
        }

        public ActionResult UpdateUserDetails(string Id, string UserName, string EmailId, string Package, string Status)
        {
            Api.AdminUserDetails.AdminUserDetails apiobjUserUpdate = new Api.AdminUserDetails.AdminUserDetails();
            string UpdateMessage = (string)(new JavaScriptSerializer().Deserialize(apiobjUserUpdate.UpdateUserAccount(Id, UserName, EmailId, Package, Status), typeof(string)));
            return Content(UpdateMessage);
        }

        public ActionResult DeleteUser(string Id)
        {
            Api.AdminUserDetails.AdminUserDetails ApiobjUser = new Api.AdminUserDetails.AdminUserDetails();
            int deletemessage = (int)(new JavaScriptSerializer().Deserialize(ApiobjUser.DeleteUser(Id), typeof(int)));
            return Content(deletemessage.ToString());
        }

        public ActionResult ManageProfile()
        {
            return View();
        }

        public ActionResult LoadManageProfile()
        {
            return PartialView("_ManageProfilePartial");
        }
        public ActionResult EditProfileDetails(string ProfileId, string UserId, string Network)
        {
            Dictionary<string, object> objProfileToEdit = new Dictionary<string, object>();
            if (Network == "Facebook")
            {
                Api.FacebookAccount.FacebookAccount Apiobjfb = new Api.FacebookAccount.FacebookAccount();
                FacebookAccount objFbAccount = (FacebookAccount)(new JavaScriptSerializer().Deserialize(Apiobjfb.getFacebookAccountDetailsById(UserId, ProfileId), typeof(FacebookAccount)));
                Session["UpdateProfileData"] = objFbAccount;
                objProfileToEdit.Add("Facebook", objFbAccount);
            }
            if (Network == "Twitter")
            {
                Api.TwitterAccount.TwitterAccount Apiobjtwt = new Api.TwitterAccount.TwitterAccount();
                TwitterAccount objtwtAccount = (TwitterAccount)(new JavaScriptSerializer().Deserialize(Apiobjtwt.GetTwitterAccountDetailsById(UserId, ProfileId), typeof(TwitterAccount)));
                Session["UpdateProfileData"] = objtwtAccount;
                objProfileToEdit.Add("Twitter", objtwtAccount);
            }
            if (Network == "Linkedin")
            {
                Api.LinkedinAccount.LinkedinAccount Apiobjlin = new Api.LinkedinAccount.LinkedinAccount();
                LinkedInAccount objLinAccount = (LinkedInAccount)(new JavaScriptSerializer().Deserialize(Apiobjlin.GetLinkedinAccountDetailsById(UserId, ProfileId), typeof(LinkedInAccount)));
                Session["UpdateProfileData"] = objLinAccount;
                objProfileToEdit.Add("Linkedin", objLinAccount);
            }
            if (Network == "Instagram")
            {
                Api.InstagramAccount.InstagramAccount ApiobjIns = new Api.InstagramAccount.InstagramAccount();
                InstagramAccount objInsAccount = (InstagramAccount)(new JavaScriptSerializer().Deserialize(ApiobjIns.UserInformation(UserId, ProfileId), typeof(InstagramAccount)));
                Session["UpdateProfileData"] = objInsAccount;
                objProfileToEdit.Add("Instagram", objInsAccount);
            }
            if (Network == "Tumblr")
            {
                Api.TumblrAccount.TumblrAccount Apiobjtmb = new Api.TumblrAccount.TumblrAccount();
                TumblrAccount objTmbAccount = (TumblrAccount)(new JavaScriptSerializer().Deserialize(Apiobjtmb.GetTumblrAccountDetailsById(UserId, ProfileId), typeof(TumblrAccount)));
                Session["UpdateProfileData"] = objTmbAccount;
                objProfileToEdit.Add("Tumblr", objTmbAccount);
            }
            if (Network == "Youtube")
            {
                Api.YoutubeAccount.YoutubeAccount ApiobjYoutb = new Api.YoutubeAccount.YoutubeAccount();
                YoutubeAccount objYouTbAccount = (YoutubeAccount)(new JavaScriptSerializer().Deserialize(ApiobjYoutb.GetYoutubeAccountDetailsById(UserId, ProfileId), typeof(YoutubeAccount)));
                Session["UpdateProfileData"] = objYouTbAccount;
                objProfileToEdit.Add("Youtube", objYouTbAccount);
            }
            if (Network == "GooglePlus")
            {
                Api.GooglePlusAccount.GooglePlusAccount Apiobjgplus = new Api.GooglePlusAccount.GooglePlusAccount();
                GooglePlusAccount objGPAccount = (GooglePlusAccount)(new JavaScriptSerializer().Deserialize(Apiobjgplus.GetGooglePlusAccountDetailsById(UserId, ProfileId), typeof(GooglePlusAccount)));
                Session["UpdateProfileData"] = objGPAccount;
                objProfileToEdit.Add("GooglePlus", objGPAccount);
            }

            return View(objProfileToEdit);
        }


        public ActionResult UpdateProfileDetails(string Network, string ProfileName)
        {
            string ReturnMessage = string.Empty;
            bool Status = false;
            if (Network == "Facebook")
            {
                Domain.Socioboard.Domain.FacebookAccount objfb = (FacebookAccount)Session["UpdateProfileData"];
                objfb.IsActive = 1;
                objfb.FbUserName = ProfileName;
                string objFacebook = new JavaScriptSerializer().Serialize(objfb);
                Api.Facebook.Facebook ApiObjfb = new Api.Facebook.Facebook();
                string FbMessage = (string)(new JavaScriptSerializer().Deserialize(ApiObjfb.UpdateFacebookAccountByAdmin(objFacebook), typeof(string)));
                ReturnMessage = FbMessage;

            }
            if (Network == "Twitter")
            {
                Status = true;
                Domain.Socioboard.Domain.TwitterAccount objfb = (TwitterAccount)Session["UpdateProfileData"];
                objfb.IsActive = Status;
                objfb.TwitterScreenName = ProfileName;
                string objFacebook = new JavaScriptSerializer().Serialize(objfb);
                Api.Twitter.Twitter ApiObjfb = new Api.Twitter.Twitter();
                string FbMessage = (string)(new JavaScriptSerializer().Deserialize(ApiObjfb.UpdateTwitterAccountByAdmin(objFacebook), typeof(string)));
                ReturnMessage = FbMessage;

            }
            if (Network == "Linkedin")
            {
                Status = true;
                Domain.Socioboard.Domain.LinkedInAccount objfb = (LinkedInAccount)Session["UpdateProfileData"];
                objfb.IsActive = Status;
                objfb.LinkedinUserName = ProfileName;
                string objFacebook = new JavaScriptSerializer().Serialize(objfb);
                Api.Twitter.Twitter ApiObjfb = new Api.Twitter.Twitter();
                string FbMessage = (string)(new JavaScriptSerializer().Deserialize(ApiObjfb.UpdateTwitterAccountByAdmin(objFacebook), typeof(string)));
                ReturnMessage = FbMessage;
            }
            if (Network == "Instagram")
            {
                Status = true;
                Domain.Socioboard.Domain.InstagramAccount objfb = (InstagramAccount)Session["UpdateProfileData"];
                objfb.IsActive = Status;
                objfb.InsUserName = ProfileName;
                string objFacebook = new JavaScriptSerializer().Serialize(objfb);
                Api.Instagram.Instagram ApiObjfb = new Api.Instagram.Instagram();
                string FbMessage = (string)(new JavaScriptSerializer().Deserialize(ApiObjfb.UpdateInstagramAccountByAdmin(objFacebook), typeof(string)));
                ReturnMessage = FbMessage;
            }
            //if (Network == "Tumblr")
            //{
            //    Status = true;
            //    Domain.Socioboard.Domain.TwitterAccount objfb = (TwitterAccount)Session["UpdateProfileData"];
            //    objfb.IsActive = Status;
            //    objfb.TwitterScreenName = ProfileName;
            //    string objFacebook = new JavaScriptSerializer().Serialize(objfb);
            //    Api.Twitter.Twitter ApiObjfb = new Api.Twitter.Twitter();
            //    string FbMessage = (string)(new JavaScriptSerializer().Deserialize(ApiObjfb.UpdateTwitterAccountByAdmin(objFacebook), typeof(string)));
            //    ReturnMessage = FbMessage;
            //}
            if (Network == "Youtube")
            {
                Status = true;
                Domain.Socioboard.Domain.YoutubeAccount objfb = (YoutubeAccount)Session["UpdateProfileData"];
                objfb.Isactive = 1;
                objfb.Ytusername = ProfileName;
                string objFacebook = new JavaScriptSerializer().Serialize(objfb);
                Api.Youtube.Youtube ApiObjfb = new Api.Youtube.Youtube();
                string FbMessage = (string)(new JavaScriptSerializer().Deserialize(ApiObjfb.UpdateYouTubeAccountByAdmin(objFacebook), typeof(string)));
                ReturnMessage = FbMessage;
            }
            if (Network == "GooglePlus")
            {
                Status = true;
                Domain.Socioboard.Domain.GooglePlusAccount objfb = (GooglePlusAccount)Session["UpdateProfileData"];
                objfb.IsActive = 1;
                objfb.GpUserName = ProfileName;
                string objFacebook = new JavaScriptSerializer().Serialize(objfb);
                Api.Twitter.Twitter ApiObjfb = new Api.Twitter.Twitter();
                string FbMessage = (string)(new JavaScriptSerializer().Deserialize(ApiObjfb.UpdateTwitterAccountByAdmin(objFacebook), typeof(string)));
                ReturnMessage = FbMessage;
            }
            return Content(ReturnMessage);
        }

        public ActionResult LoadDeletedUsers()
        {
            Domain.Socioboard.Domain.User ObjUser = (Domain.Socioboard.Domain.User)Session["User"];
            string Objuser = (new JavaScriptSerializer().Serialize(ObjUser));
            Api.AdminUserDetails.AdminUserDetails ApiObjuserdetails = new Api.AdminUserDetails.AdminUserDetails();
            List<User> lstUser = (List<User>)(new JavaScriptSerializer().Deserialize(ApiObjuserdetails.getAllDeletedUsers(Objuser), typeof(List<User>)));
            return View(lstUser);
        }

    }
}

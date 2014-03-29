using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using GlobusGooglePlusLib.Authentication;
using System.Configuration;
using GlobusGooglePlusLib.Gplus.Core.PeopleMethod;
using Newtonsoft.Json.Linq;
using GlobusGooglePlusLib.App.Core;
using SocioBoard.Model;
using SocioBoard.Domain;
using SocioBoard.Helper;
using log4net;

namespace SocialSuitePro
{
    public partial class GplusManager : System.Web.UI.Page
    {

        ILog logger = LogManager.GetLogger(typeof(GplusManager));

        protected void Page_Load(object sender, EventArgs e)
        {

            oAuthToken objToken = new oAuthToken();
            GplusHelper objGpHelper = new GplusHelper();
            UserRepository objUserRepo = new UserRepository();
            PeopleController obj = new PeopleController();
            GooglePlusAccount objgpAcc = new GooglePlusAccount();
            User user = new User();
            try
            {
                string objRefresh = objToken.GetRefreshToken(Request.QueryString["code"]);
                if (!objRefresh.StartsWith("["))
                    objRefresh = "[" + objRefresh + "]";
                JArray objArray = JArray.Parse(objRefresh);
                if (Session["login"] != null)
                {
                    if (Session["login"].ToString() == "googleplus")
                    {
                        user = new User();
                        user.CreateDate = DateTime.Now;
                        user.ExpiryDate = DateTime.Now.AddMonths(1);
                        user.Id = Guid.NewGuid();
                        user.PaymentStatus = "unpaid";
                    }

                }
                else
                {
                    /*User class in SocioBoard.Domain to check authenticated user*/
                    user = (User)Session["LoggedUser"];
                }
                foreach (var item in objArray)
                {
                    try
                    {
                        JArray objEmail = objToken.GetUserInfo("me", item["access_token"].ToString());
                        JArray objProfile = obj.GetPeopleProfile("me", item["access_token"].ToString());
                        // user = (User)HttpContext.Current.Session["LoggedUser"];
                        foreach (var itemEmail in objEmail)
                        {
                            objgpAcc.EmailId = itemEmail["email"].ToString();

                        }
                        foreach (var itemProfile in objProfile)
                        {
                            objgpAcc.GpUserId = itemProfile["id"].ToString();
                            objgpAcc.AccessToken = item["access_token"].ToString();
                            objgpAcc.EntryDate = DateTime.Now;
                            objgpAcc.GpProfileImage = itemProfile["image"]["url"].ToString();
                            objgpAcc.GpUserName = itemProfile["displayName"].ToString();
                            objgpAcc.Id = Guid.NewGuid();
                            objgpAcc.IsActive = 1;
                            objgpAcc.RefreshToken = item["refresh_token"].ToString();
                            objgpAcc.UserId = user.Id;


                        }

                        if (Session["login"] != null)
                        {
                            if (string.IsNullOrEmpty(user.Password))
                            {
                                if (Session["login"].ToString() == "googleplus")
                                {
                                    if (objUserRepo.IsUserExist(objgpAcc.EmailId))//user.EmailId
                                    {
                                        // user = null;
                                        user = objUserRepo.getUserInfoByEmail(objgpAcc.EmailId);//user.EmailId
                                        if (!string.IsNullOrEmpty(user.Password))
                                        {
                                            //Session["LoggedUser"] = user;
                                            //Response.Redirect("Home.aspx");

                                            if (user.UserStatus == 1)
                                            {
                                                Session["LoggedUser"] = user;
                                                Response.Redirect("Home.aspx");
                                            }
                                            else
                                            {
                                                //check user is block or not
                                                Session["fblogout"] = "NOTACTIVATED";

                                                Response.Redirect("Default.aspx");
                                            }
                                        }
                                    }
                                    else
                                    {
                                        user.EmailId = objgpAcc.EmailId;
                                        user.UserName = objgpAcc.GpUserName;
                                        user.ProfileUrl = objgpAcc.GpProfileImage;
                                        user.UserStatus = 1;
                                        UserRepository.Add(user);
                                    }
                                    Session["LoggedUser"] = user;
                                    objgpAcc.UserId = user.Id;

                                }
                            }
                        }
                        objGpHelper.GetUerProfile(objgpAcc, item["access_token"].ToString(), item["refresh_token"].ToString(), user.Id);



                        if (Session["login"] != null)
                        {
                            if (string.IsNullOrEmpty(user.Password))
                            {
                                if (Session["login"].ToString() == "googleplus")
                                {
                                   // Response.Redirect("Pricing.aspx");
                                    Response.Redirect("NetworkLogin.aspx?type=Free");
                                }
                            }
                            else
                            {
                                Response.Redirect("Home.aspx");
                            }
                        }
                        else
                        {
                            Response.Redirect("Home.aspx");
                        }


                    }
                    catch (Exception ex)
                    {
                        logger.Error(ex.StackTrace);
                        Console.WriteLine(ex.StackTrace);


                    }
                }

            }
            catch (Exception Err)
            {
                Console.Write(Err.Message.ToString());
                logger.Error(Err.StackTrace);
            }
        }
    }
}
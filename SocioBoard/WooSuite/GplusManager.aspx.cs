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

namespace WooSuite
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
                try
                {
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
                }
                    catch (Exception ex)
                    {
                        Response.Write("Session[login]"+ex.StackTrace + ex.Message);
                    }
                foreach (var item in objArray)
                {
                    try
                    {
                        Response.Write(objArray.Count);
                    }
                    catch (Exception Err)
                    {
                        Response.Write("objArray" + Err.Message + Err.StackTrace);
                    }
                    try
                    {
                        JArray objEmail = null;
                        JArray objProfile = null;
                        try
                        {
                            Response.Write(item["access_token"].ToString());
                            objEmail = objToken.GetUserInfo("me", item["access_token"].ToString());
                        }
                        catch (Exception Err)
                        {
                            Response.Write("objEmail" + Err.StackTrace + Err.Message);
                        }
                        try
                        {
                            Response.Write(item["access_token"].ToString());
                            objProfile = obj.GetPeopleProfile("me", item["access_token"].ToString());
                        }
                        catch (Exception Err)
                        {
                            Response.Write("objProfile" + Err.StackTrace + Err.Message);
                        }
                        // user = (User)HttpContext.Current.Session["LoggedUser"];
                        try
                        {
                            Response.Write(objEmail.Count);
                            foreach (var itemEmail in objEmail)
                            {
                                try
                                {
                                    Response.Write(itemEmail["email"].ToString());
                                    objgpAcc.EmailId = itemEmail["email"].ToString();
                                }
                                catch (Exception ex)
                                {
                                    Response.Write("itemEmail[email]" + ex.StackTrace + ex.Message);
                                }

                            }
                        }
                        catch (Exception Err)
                        {
                            Response.Write("objArray" + Err.Message);
                        }
                        foreach (var itemProfile in objProfile)
                        {
                            objgpAcc.GpUserId = itemProfile["id"].ToString();
                            objgpAcc.AccessToken = item["access_token"].ToString();
                            objgpAcc.EntryDate = DateTime.Now;
                            objgpAcc.GpProfileImage = itemProfile["image"]["url"].ToString();
                            objgpAcc.GpUserName = itemProfile["displayName"].ToString();
                            objgpAcc.Id =Guid.NewGuid();
                            objgpAcc.IsActive = 1;
                            objgpAcc.RefreshToken = item["refresh_token"].ToString();
                            objgpAcc.UserId = user.Id;

                        
                        }
                        try
                        {
                            if (Session["login"] != null)
                            {
                                if (string.IsNullOrEmpty(user.Password))
                                {
                                    if (Session["login"].ToString() == "googleplus")
                                    {
                                        if (objUserRepo.IsUserExist(user.EmailId))
                                        {
                                            // user = null;
                                            user = objUserRepo.getUserInfoByEmail(user.EmailId);
                                        }
                                        else
                                        {
                                            user.EmailId = objgpAcc.EmailId;
                                            user.UserName = objgpAcc.GpUserName;
                                            user.ProfileUrl = objgpAcc.GpProfileImage;
                                            UserRepository.Add(user);
                                        }
                                        Session["LoggedUser"] = user;
                                        objgpAcc.UserId = user.Id;
                                    }
                                }
                            }
                        }
                        catch (Exception Err)
                        {
                            Response.Write("Session[login]" + Err.Message + Err.StackTrace);
                        }
                        try
                        {
                            objGpHelper.GetUerProfile(objgpAcc, item["access_token"].ToString(), item["refresh_token"].ToString(), user.Id);
                        }
                        catch (Exception Err)
                        {
                            Response.Write("getuserProfile" + Err.Message + Err.StackTrace);
                        }
                       

                        if (Session["login"] != null)
                        {
                            if (string.IsNullOrEmpty(user.Password))
                            {
                                if (Session["login"].ToString() == "googleplus")
                                {
                                    Response.Redirect("Plans.aspx");
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
                        Response.Write("foreach" + ex.StackTrace + ex.Message);
                        logger.Error(ex.StackTrace);
                        Console.WriteLine(ex.StackTrace);
                       
                    }
                }
               
            }
            catch (Exception Err)
            {
                Response.Write("outertry"+Err.StackTrace + Err.Message);
                Console.Write(Err.Message.ToString());
                logger.Error(Err.StackTrace);
            }
        }
    }
}
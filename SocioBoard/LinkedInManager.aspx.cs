using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SocioBoard.Domain;
using GlobusLinkedinLib.Authentication;
using GlobusLinkedinLib.App.Core;
using SocioBoard.Helper;
using log4net;
using SocioBoard.Model;

namespace SocialSuitePro
{
    public partial class LinkedInManager : System.Web.UI.Page
    {
        ILog logger = LogManager.GetLogger(typeof(LinkedInManager));

        oAuthLinkedIn _oauth = new oAuthLinkedIn();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                User user = (User)Session["LoggedUser"];
                if (user == null)
                { Response.Redirect("Default.aspx"); }

                try
                {
                    GetAccessToken();
                    Session["profilesforcomposemessage"] = null;
                    Response.Redirect("Home.aspx", false);
                }

                catch (Exception ex)
                {
                    logger.Error(ex.StackTrace);
                    Console.WriteLine(ex.StackTrace);
                    Session["profilesforcomposemessage"] = null;
                    Response.Redirect("Home.aspx");
                }

            }
        }
        public void GetAccessToken()
        {
            LinkedInProfile objProfile = new LinkedInProfile();
            LinkedInProfile.UserProfile objUserProfile = new LinkedInProfile.UserProfile();
            LinkedInHelper liHelper = new LinkedInHelper();
            User user = (User)Session["LoggedUser"];
            string oauth_token = Request.QueryString["oauth_token"];
            string oauth_verifier = Request.QueryString["oauth_verifier"];
            try
            {

                if (oauth_token != null && oauth_verifier != null)
                {

                    try
                    {
                        _oauth.Token = oauth_token;
                    }
                    catch (Exception ex)
                    {
                        logger.Error(ex.StackTrace);
                    }
                    try
                    {
                        _oauth.TokenSecret = Session["reuqestTokenSecret"].ToString();
                    }
                    catch (Exception ex)
                    {
                        logger.Error(ex.StackTrace);

                    }
                    try
                    {
                        _oauth.Verifier = oauth_verifier;
                    }
                    catch (Exception ex)
                    {
                        logger.Error(ex.StackTrace);

                    }



                    try
                    {
                        _oauth.AccessTokenGet(oauth_token);
                    }
                    catch (Exception ex)
                    {
                        logger.Error(ex.StackTrace);

                    }


                    // Update Access Token in DB 
                    try
                    {
                        int res = UpdateLDToken(user.Id.ToString(), _oauth.Token);
                    }
                    catch { };
                    //***********************

                    Session.Remove("oauth_token");
                    Session.Remove("oauth_TokenSecret");

                    try
                    {
                        objUserProfile = objProfile.GetUserProfile(_oauth);
                    }
                    catch (Exception ex)
                    {
                        logger.Error(ex.StackTrace);

                    }
                    try
                    {

                        liHelper.GetLinkedInUserProfile(objUserProfile, _oauth, user);
                    }
                    catch (Exception ex)
                    {
                        logger.Error(ex.StackTrace);

                    }
                    try
                    {
                        // liHelper.getLinkedInNetworkUpdate(_oauth,user);
                    }
                    catch (Exception ex)
                    {
                        logger.Error(ex.StackTrace);

                    }
                    Session["LinkedInUser"] = _oauth;
                    Session["datatable"] = null;
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex.StackTrace);

            }
        }

        public int UpdateLDToken(string LDUserId, string LDAccessToken)
        {
            int res = 0;
            try
            {
                LinkedInAccountRepository objldAR = new LinkedInAccountRepository();

                res = objldAR.UpdateLDAccessTokenByLDUserId(LDUserId, LDAccessToken);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error : " + ex.StackTrace);

            }
            return res;
        }
    }
}
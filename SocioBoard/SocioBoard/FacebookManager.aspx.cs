using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SocioBoard.Domain;
using System.Configuration;
using Facebook;
using SocioBoard.Model;
using SocioBoard.Helper;

namespace SocioBoard
{
    public partial class FacebookManager : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                User user = (User)Session["LoggedUser"];
                if (user == null)
                { Response.Redirect("Login.aspx"); }

                try
                {
                    GetAccessToken();
                    Session["profilesforcomposemessage"] = null;

                    if (Session["UserAndGroupsForFacebook"] != null)
                    {
                        if (Session["UserAndGroupsForFacebook"].ToString() == "facebook")
                        {
                            try
                            {

                                Session["UserAndGroupsForFacebook"] = null;
                                Response.Redirect("Setting/UsersAndGroups.aspx");
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex.StackTrace);
                                Session["UserAndGroupsForFacebook"] = null;
                            }
                        }
                    }
                    else
                    {

                        try
                        {
                            Response.Redirect("Home.aspx");
                        }
                        catch (Exception ex)
                        {
                            Session["profilesforcomposemessage"] = null;
                            Response.Redirect("Home.aspx");
                        }

                    }

                }

                catch (Exception ex)
                {
                    Console.WriteLine(ex.StackTrace);
                   
                }

            }

        }


        //Getting AccessToken,FacebookMessages,FacebookFeeds and UserProfile for  Authenticated user.

        private void GetAccessToken()
        {
            string code = Request.QueryString["code"];

            /*User class in SocioBoard.Domain to check authenticated user*/
            User user = (User)Session["LoggedUser"];
           
            
            /*Replacing Code With AccessToken*/
            // Facebook.dll using for 
            FacebookHelper fbhelper = new FacebookHelper();
            FacebookClient fb = new FacebookClient();
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("client_id", ConfigurationManager.AppSettings["ClientId"]);
            parameters.Add("redirect_uri", ConfigurationManager.AppSettings["RedirectUrl"]);
            parameters.Add("client_secret", ConfigurationManager.AppSettings["ClientSecretKey"]);
            parameters.Add("code", code);

            JsonObject result = (JsonObject)fb.Get("/oauth/access_token", parameters);
            string accessToken = result["access_token"].ToString();
            //string accessToken = "CAACEdEose0cBAM9Ywj520Tzt3wzTlwvIQw327SRdxHMpHdBgB57qyvuEinrfZB1EUFCWs18vspacYFcH780voXAoDU9X53ciCGQ5836HuKOKZBIF0QjNdCmqm3McQsv2Vsb8ZCtq69wZCw8Y1Eq7k23dFAnaT3VCZA9yScPQu1sZCjQ6qa9xUXNswE2aXLFcxz1YZA5GhYkxQZDZD";
            fb.AccessToken = accessToken;


            // For long Term Fb access_token

            // GET /oauth/access_token?  
            //grant_type=fb_exchange_token&           
            //client_id={app-id}&
            //client_secret={app-secret}&
            //fb_exchange_token={short-lived-token}


            parameters.Clear();

            parameters.Add("grant_type", "fb_exchange_token");
            parameters.Add("client_id", ConfigurationManager.AppSettings["ClientId"]);
            parameters.Add("client_secret", ConfigurationManager.AppSettings["ClientSecretKey"]);
            parameters.Add("fb_exchange_token", accessToken);

            result = (JsonObject)fb.Get("/oauth/access_token", parameters);
            accessToken = result["access_token"].ToString();
            fb.AccessToken = accessToken;

            var feeds = fb.Get("/me/feed");
          //  var friends = fb.Get("/me/friends");
            var home = fb.Get("me/home");

            dynamic profile = fb.Get("me");



            int res = UpdateFbToken(profile["id"], fb.AccessToken);

            dynamic groups = fb.Get("me/groups");
            long friendscount = 0;
            try
            {
               dynamic friedscount = fb.Get("fql", new { q = "SELECT friend_count FROM user WHERE uid=me()" });

               foreach (var friend in friedscount.data)
               { 
                   friendscount = friend.friend_count;
              
               }

                //friendscount = Convert.ToInt32(friedscount["data"]["friend_count"].ToString());

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                
            }









            //call function for check group exist/not if not then insert
            try
            {

                List<FacebookGroup> temp = new List<FacebookGroup>();
                List<FacebookGroup> lstfacebookgroup = new List<FacebookGroup>();
                FacebookGroup objfacegroup = new FacebookGroup();
                FacebookHelper fbgrouphelper = new FacebookHelper();

                if (groups != null)
                {
                    foreach (var result1 in groups["data"])
                    {
                        objfacegroup.GroupId = result1["id"];
                        Dictionary<string, object> parameters1 = new Dictionary<string, object>();
                        parameters1.Add("link", "https://www.facebook.com/");
                        object msg= fb.Post(objfacegroup.GroupId + "/feed", parameters1);

                        temp.Add(objfacegroup);
                    }
                }

                foreach (var groupid in temp)
                {
                    try
                    {
                        dynamic groupsdetails = fb.Get(groupid.GroupId);
                        fbgrouphelper.GetFacebookGroups(groupsdetails, profile);
                    }
                    catch (Exception ex)
                    {
                     Console.WriteLine(ex.StackTrace);
                    }  
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);

            }













              //this.getFacebookFriendsCount(friends);

            try
            {
                fbhelper.getFacebookUserProfile(profile, accessToken, friendscount, user.Id);

            }
            catch (Exception exx)
            {
                Console.WriteLine(exx.StackTrace);
                
            }

            try
            {
               fbhelper.getFacebookUserFeeds(feeds, profile);

            }
            catch (Exception exxx)
            {
                Console.WriteLine(exxx.StackTrace);                
            }

            try
            {
               fbhelper.getFacebookUserHome(home, profile);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                
            }
            
        }

        public int UpdateFbToken(string fbUserId, string fbAccessToken)
        {
            int res = 0;
            try
            {
                FacebookAccountRepository objfbAR = new FacebookAccountRepository();

                res = objfbAR.UpdateFBAccessTokenByFBUserId(fbUserId, fbAccessToken);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error : " + ex.StackTrace);

            }
            return res;
        }
    }
}
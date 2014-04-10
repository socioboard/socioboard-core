using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SocioBoard.Domain;
using System.Configuration;
using Facebook;
using SocioBoard.Model;
using SocioBoard.Helper;

namespace SocialScoup.Group
{
    public partial class Group : System.Web.UI.Page
    {
        FacebookAccountRepository fbAccRepo = new FacebookAccountRepository();
        public string groupname = string.Empty;
        
        string leftsidedata = string.Empty;
        string accesstoken = "CAACEdEose0cBAK6gw0TzXgMQcVIZCO1W3w8R0cxAZAMnC44FiR0S3lbbG7I01BWinxsYl7KjQZBUwGs9lPKeOO1pVG8anP2vpNza9Y68kzXbocbQaysu6OdGCAf6ckMYFZB5ZAak3nLHjdGtcXWorLEMJIUZCwi4ws7ZAvNr4ZBWeIO7YEZCW2GdN39dU5syiatZC3qAO3wp3uAwZDZD";
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                User user = (User)Session["LoggedUser"];
                try
                {
                    #region for You can use only 30 days as Unpaid User

                    //SocioBoard.Domain.User user = (User)Session["LoggedUser"];
                    if (user.PaymentStatus.ToLower() == "unpaid")
                    {
                        if (!SBUtils.IsUserWorkingDaysValid(user.ExpiryDate))
                        {
                            // ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('You can use only 30 days as Unpaid User !');", true);

                            Session["GreaterThan30Days"] = "GreaterThan30Days";

                            Response.Redirect("../Settings/Billing.aspx");
                        }
                    }
                    #endregion
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error : " + ex.StackTrace);
                }

                if (!IsPostBack)
                {
                   

                    if (user == null)
                        Response.Redirect("/Default.aspx");

                    SocioBoard.Domain.FacebookAccount objFacebookAccount;
                    ArrayList arrFacebookAccount = fbAccRepo.getAllFacebookAccountsOfUser(user.Id);

                    foreach (var item in arrFacebookAccount)
                    {
                        objFacebookAccount = new FacebookAccount();
                        objFacebookAccount = (SocioBoard.Domain.FacebookAccount)item;
                        leftsidedata += "<div class=\"accordion-group\"><div class=\"accordion-heading\">"
                            + "<a href=\"#" + objFacebookAccount.Id + "\" data-parent=\"#accordion2\" data-toggle=\"collapse\" class=\"accordion-toggle\">"
                               + "<img class=\"fesim\" src=\"http://graph.facebook.com/"+objFacebookAccount.FbUserId+"/picture?type=small\" alt=\"\" />" + objFacebookAccount.FbUserName + " <i class=\"icon-sort-down pull-right hidden\">"
                                + "</i></a></div><div id=\"" + objFacebookAccount.Id + "\" class=\"accordion-body collapse\" ><div class=\"accordion-inner\"><ul>";
                        List<FacebookGroup> lstFacebookGroup = GetGroupName(objFacebookAccount.AccessToken);
                        if (lstFacebookGroup.Count == 0)
                        {
                            leftsidedata += "<li><a  fbUserId=\"" + objFacebookAccount.FbUserId + "\" href=\"#\">No Group Found</a> </li>";
                        }
                        else
                        {
                            foreach (var item1 in lstFacebookGroup)
                            {
                                leftsidedata += "<li><a gid=\"" + item1.GroupId + "\" onclick=\"facebookgroupdetails('" + item1.GroupId + "','" + objFacebookAccount.AccessToken + "');\" fbUserId=\"" + objFacebookAccount.FbUserId + "\" href=\"#\">" + item1.Name + "</a> </li>";
                            }
                        }
                        leftsidedata += "</ul></div></div></div>";
                    }
                    
                    accordion2.InnerHtml = leftsidedata;

                    //    List<FacebookGroup> lstFacebookGroup = GetGroupName(accesstoken);

                    //    string grpName = string.Empty;
                    //    foreach (FacebookGroup item in lstFacebookGroup)
                    //    {
                    //        try
                    //        {
                    //            grpName += "<div><a href=\"" + GetGroupInfo(accesstoken, item.GroupId) + "\">" + item.Name + "</a></div>";
                    //        }
                    //        catch (Exception ex)
                    //        {
                    //            Console.WriteLine(ex.StackTrace);
                    //        }
                    //    }

                    //    groupname = grpName;
                    //}
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
            }

        }




        public List<FacebookGroup> GetGroupName(string accesstoken)
        {
            List<FacebookGroup> lstGroupName = new List<FacebookGroup>();


            try
            {
                FacebookClient fb = new FacebookClient();
                fb.AccessToken = accesstoken;
                dynamic groups = fb.Get("me/groups");

                foreach (var item in groups["data"])
                {
                    try
                    {
                        FacebookGroup objFacebookGroup = new FacebookGroup();

                        objFacebookGroup.Name = item["name"].ToString();
                        objFacebookGroup.GroupId = item["id"].ToString();

                        lstGroupName.Add(objFacebookGroup);
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

            return lstGroupName;
        }

        public object GetGroupInfo(string accesstoken, string grpId)
        {
            List<FacebookGroup> lstGroupName = new List<FacebookGroup>();

            object objgroupInfo = new object();

            try
            {
                FacebookClient fb = new FacebookClient();
                fb.AccessToken = accesstoken;
                objgroupInfo = fb.Get(grpId);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);

            }

            return objgroupInfo;
        }

        //public List<FacebookAccount> GetFacebookAccount()
        //{ 

        //}
    }
}
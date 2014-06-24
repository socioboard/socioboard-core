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

namespace SocioBoard.Group
{
    public partial class Group : System.Web.UI.Page
    {
        FacebookAccountRepository fbAccRepo = new FacebookAccountRepository();
        public string groupname=string.Empty;
        string accesstoken = "CAACEdEose0cBAK6gw0TzXgMQcVIZCO1W3w8R0cxAZAMnC44FiR0S3lbbG7I01BWinxsYl7KjQZBUwGs9lPKeOO1pVG8anP2vpNza9Y68kzXbocbQaysu6OdGCAf6ckMYFZB5ZAak3nLHjdGtcXWorLEMJIUZCwi4ws7ZAvNr4ZBWeIO7YEZCW2GdN39dU5syiatZC3qAO3wp3uAwZDZD";
        protected void Page_Load(object sender, EventArgs e)
        {

            try
            {
                if (!IsPostBack)
                {
                    User user = (User)Session["LoggedUser"];

                    if (user == null)
                        Response.Redirect("/Default.aspx");


                    ArrayList arrFacebookAccount = fbAccRepo.getAllFacebookPagesOfUser(user.Id);

                    foreach (var item in arrFacebookAccount)
                    {
                        
                    }

                    List<FacebookGroup> lstFacebookGroup = GetGroupName(accesstoken);

                    string grpName = string.Empty;
                    foreach (FacebookGroup item in lstFacebookGroup)
                    {
                        try
                        {
                            grpName += "<div><a href=\"" + GetGroupInfo(accesstoken, item.GroupId) + "\">" + item.Name + "</a></div>";
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.StackTrace);
                        }
                    }

                    groupname = grpName;

                    //string 

                    //<div class="title">
                    //        PROFILES</div>
                    //</div>
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

        public object GetGroupInfo(string accesstoken,string grpId)
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
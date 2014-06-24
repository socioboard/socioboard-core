using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SocioBoard.Domain;
using SocioBoard.Helper;

namespace WooSuite
{
    public partial class _Default : System.Web.UI.Page
    {
        log4net.ILog logger = log4net.LogManager.GetLogger(typeof(_Default));
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Session["isemailexist"] != null)
                {
                    if (Session["isemailexist"].ToString() == "emailnotexist")
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Your email id was not returned by Facebook graph API!');", true);
                        Session["isemailexist"] = null;
                        return;
                    }


                }


                if (Session["fblogout"] != null)
                {
                    if (Session["fblogout"].ToString() == "NOTACTIVATED")
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('You are Blocked by Admin Please contact Admin!');", true);
                        return;
                    }

                }


                if (Request.QueryString != null)
                {
                    if (Request.QueryString["type"] == "logout")
                    {
                        Session.Abandon();
                        Session.Clear();
                    }

                }
                if (Session["LoggedUser"] != null)
                    Response.Redirect("Home.aspx");
                try
                {
                    Session.Abandon();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.StackTrace);
                    logger.Error(ex.StackTrace);
                }
                try
                {
                    Session.RemoveAll();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.StackTrace);
                    logger.Error(ex.StackTrace);
                }

                try
                {
                    Session["profilesforcomposemessage"] = null;
                    Session["CountMessages"] = null;
                    Session.Abandon();
                }
                catch
                {
                }
                try
                {
                    Session.RemoveAll();
                }
                catch (Exception ex)
                {
                    logger.Error(ex.StackTrace);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                logger.Error(ex.StackTrace);
            }
        }
    }
}

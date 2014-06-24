using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using log4net;
using SocioBoard.Domain;
using SocioBoard.Model;
using Newtonsoft.Json.Linq;

namespace SocialSuitePro
{
    public partial class Default : System.Web.UI.Page
    {
        ILog logger = LogManager.GetLogger(typeof(Default));

        protected void Page_Load(object sender, EventArgs e)
        {
           
            //ScheduledMessageRepository sch = new ScheduledMessageRepository();
            //sch.UpdateProfileScheduleMessageStat(Guid.Parse("5a8a6900-2640-434f-9a63-3f507b1f9f54"), false);
            
            try
            {
                //SocioBoard.Helper.SessionFactory.configfilepath = Server.MapPath("~/hibernate.cfg.xml");
                //UserRepository userrepo = new Us}erRepository();
                //List<User> lst = userrepo.testing();


                if (Session["isemailexist"] != null)
                {
                    if (Session["isemailexist"].ToString() == "emailnotexist")
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Your email id was not returned by Facebook graph API!');", true);
                        Session["isemailexist"] = null;
                        return;
                    }


                }


                if (Session["fblogout"]!=null)
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


            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                logger.Error(ex.StackTrace);
            }
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

    }
}
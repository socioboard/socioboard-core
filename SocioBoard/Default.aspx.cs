using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using log4net;
using SocioBoard.Domain;
using SocioBoard.Model;
using SocioBoard.Admin.Scheduler;

namespace SocialSuitePro
{
    public partial class Default : System.Web.UI.Page
    {
        ILog logger = LogManager.GetLogger(typeof(Default));
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {

                //UserRepository objUserRepository = new UserRepository();
                //User objUser = objUserRepository.getUsersById(Guid.Parse("62ebeaa8-0148-4e30-84fc-bf1af515dbf8"));
               // UserRepository objUserRepository = new UserRepository();
               //// List<User> lstuser = objUserRepository.getAllUsers();
               // User objUser = objUserRepository.getUsersById(Guid.Parse("62ebeaa8-0148-4e30-84fc-bf1af515dbf8"));


                //SocioBoard.Helper.SessionFactory.configfilepath = Server.MapPath("~/hibernate.cfg.xml");
                //UserRepository userrepo = new UserRepository();
                //List<User> lst = userrepo.testing();

                

                

                //if (Session["objUserActivationException"] != null)
                //{
                //    //UserActivation userActivation = (UserActivation)Session["objUserActivation"];
                //    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('objUserActivationException');", true);
                //    //return;
                //}
                //else
                //{
                //    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('objUserActivationException Is null');", true);
                //}

                //if (Session["objUserActivation"] != null)
                //{
                //    UserActivation userActivation = (UserActivation)Session["objUserActivation"];
                //    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('ActivationStatus: " + userActivation.ActivationStatus + " UserId : " + userActivation.UserId + "');", true);
                //    //return;
                //}
                //else
                //{
                //    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('objUserActivation Is null');", true);
                //}

               

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
                        Session["fblogout"] = null;
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





               // Session.Clear();

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
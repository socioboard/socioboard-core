using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SocioBoard.Model;
using SocioBoard.Domain;
using log4net;

namespace letTalkNew
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                //  ILog logger = LogManager.GetLogger(typeof(AjaxLogin));
                if (Session["LoggedUser"] != null)
                {
                    if (Request.QueryString["hint"] != null)
                    {
                        if (Request.QueryString["hint"].ToString() == "logout")
                        {
                            Session.Abandon();
                            Response.Redirect("Default.aspx");
                        }
                    }
                    else
                    {
                        Response.Redirect("Home.aspx");
                    }

                }
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                //string email = txtEmail.Text;
                //string password = txtPassword.Text;
                //SocioBoard.Helper.SessionFactory.configfilepath = Server.MapPath("~/hibernate.cfg.xml");
                //UserRepository userrepo = new UserRepository();
                //User user = userrepo.GetUserInfo(email, password);
                //if (user == null)
                //{
                //    Response.Write("Invalid Email or Password");
                //}
                //else
                //{
                //    Session["LoggedUser"] = user;
                //    // List<User> lstUser = new List<User>();
                //    if (Session["LoggedUser"] != null)
                //    {
                //        SocioBoard.Domain.User.lstUser.Add((User)Session["LoggedUser"]);
                //        Application["OnlineUsers"] = SocioBoard.Domain.User.lstUser;
                //    }
                //    Response.Write("user");
                //}
            }
            catch (Exception ex)
            {
                Response.Write("Error: " + ex.Message);
                Console.WriteLine(ex.StackTrace);
              //  logger.Error(ex.StackTrace);
            }
        }
    }
}
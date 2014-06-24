using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using log4net;
using SocioBoard.Domain;
using SocioBoard.Model;

namespace SocialSuitePro
{
    public partial class Default : System.Web.UI.Page
    {
        ILog logger = LogManager.GetLogger(typeof(Default));
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                //SocioBoard.Helper.SessionFactory.configfilepath = Server.MapPath("~/hibernate.cfg.xml");
                //UserRepository userrepo = new UserRepository();
                //List<User> lst = userrepo.testing();

                Session.Clear();

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
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
                logger.Info("Page Load");
                if (Session["LoggedUser"] != null)
                {
                    SocioBoard.Domain.User.lstUser.Remove((User)Session["LoggedUser"]);   
                }
              
                try
                {
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
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                logger.Error(ex.StackTrace);
            }
        }
    }
}

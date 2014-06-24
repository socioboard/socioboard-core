using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WooSuite
{
    public partial class Welcome : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {


                try
                {
                    Session.Clear();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.StackTrace);
                }
                try
                {
                    Session.Abandon();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.StackTrace);
                }
                try
                {
                    Session.RemoveAll();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.StackTrace);
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
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
            }
        }
    }
}
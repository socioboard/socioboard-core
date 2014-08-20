using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SocioBoard.Model;
using log4net;

namespace SocioBoard.Admin
{
    public partial class Ajaxadmin : System.Web.UI.Page
    {
        UserRepository objUerRepo = new UserRepository();
        ILog logger = LogManager.GetLogger(typeof(Ajaxadmin));
        protected void Page_Load(object sender, EventArgs e)
        {
            string msg = string.Empty;
            if (!IsPostBack)
            {

                if (Session["AdminProfile"] == null)
                {
                    Response.Redirect("Default.aspx");
                }

                try
                {
                    //User user = new User();
                    PackageRepository objPgeRepo = new PackageRepository();

                    // ddlPackage.Items.Insert();
                    int aa = objUerRepo.DeleteUser(Guid.Parse(Request.QueryString["id"]));
                    if (aa > 0)
                    {
                        msg = "success";
                    }
                    else
                    {
                        msg = "Not Deleted";
                    }
                }
                catch (Exception ex)
                {
                    logger.Error(ex.StackTrace);
                    msg = ex.Message;
                }
            }
            Response.Write(msg);
        }
    }
}
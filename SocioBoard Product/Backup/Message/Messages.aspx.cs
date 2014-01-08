using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using log4net;

namespace SocialSuitePro.Message
{
    public partial class Messages : System.Web.UI.Page
    {
        ILog logger = LogManager.GetLogger(typeof(Messages));

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    blackcount.InnerHtml = Convert.ToString((int)Session["CountMessages"]);
                }
                catch (Exception ex)
                {
                    logger.Error(ex.Message);
                    Console.WriteLine(ex.Message);
                }       
            }
        }

        
    }
}
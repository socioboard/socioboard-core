using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SocioBoard.Discovery
{
    public partial class AjaxDiscovery : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                ProcessRequest();
            }
            catch
            { }
        }
        public void ProcessRequest()
        {
            if (!string.IsNullOrEmpty(Request.QueryString["op"]))
            {

            }
        }
    }
}
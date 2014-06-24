using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using SocioBoard.Domain;
using SocioBoard.Model;
using SocioBoard.Feeds;
using SocioBoard.Message;

namespace SocioBoard.Helper
{
    public partial class AjaxHelper : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                ProcessRequest();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
            }
        }

        void ProcessRequest()
        {
            if (!string.IsNullOrEmpty(Request.QueryString["op"]))
            {
                if (Request.QueryString["op"] == "removedata")
                {

                    // var types = Request.QueryString["messages[]"].Split(',');


                    string network = Request.QueryString["network"];
                    string message = string.Empty;
                    var users = Request.QueryString["data[]"].Split(',');
                    Messages mstable = new Messages();
                    DataSet ds = DataTableGenerator.CreateDataSetForTable(mstable);
                    DataTable dtt = ds.Tables[0];

                    string page = Request.QueryString["page"];
                    if (page == "feed")
                    {
                        AjaxFeed ajxfed = new AjaxFeed();
                        DataTable dt = null;
                        if (network == "facebook")
                        {
                             dt = (DataTable)Session["FacebookFeedDataTable"];
                        }
                        else if (network == "twitter")
                        {
                            dt = (DataTable)Session["TwitterFeedDataTable"];
                        }
                        else if (network == "linkedin")
                        {
                            dt = (DataTable)Session["LinkedInFeedDataTable"];
                        }

                        foreach (var parent in users)
                        {
                            DataView dv = new DataView(dtt);
                            DataRow[] foundRows = dt.Select("ProfileId = '" + parent + "'");
                            foreach (var child in foundRows)
                            {
                                dtt.ImportRow(child);
                            }
                        }
                         message = ajxfed.BindData(dtt);
                      
                    }

                    else if (page == "message")
                    {
                        AjaxMessage ajxmes = new AjaxMessage();
                        DataSet dss = (DataSet)Session["MessageDataTable"];
                        foreach (var parent in users)
                        {
                            DataView dv = new DataView(dtt);
                            DataRow[] foundRows = dss.Tables[0].Select("ProfileId = '" + parent + "'");
                            foreach (var child in foundRows)
                            {
                                dtt.ImportRow(child);
                            }

                        }
                        message = ajxmes.BindData(dtt);
                    } 
                 
                    Response.Write(message);
                   

                }
            }
        }
    }
}
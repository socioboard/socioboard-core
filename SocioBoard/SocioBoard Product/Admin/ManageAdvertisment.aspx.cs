using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SocioBoard.Model;
using SocioBoard.Domain;
using log4net;

namespace SocialSuitePro.Admin
{
    public partial class ManageAdvertisment : System.Web.UI.Page
    {
        ILog logger = LogManager.GetLogger(typeof(ManageAdvertisment));

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                if (Session["AdminProfile"] == null)
                {
                    Response.Redirect("Default.aspx");
                }

                try
                {
                    AdsRepository objAdsRepo = new AdsRepository();
                    List<Ads> lstAds = objAdsRepo.getAllAds();
                    string strAds = string.Empty;
                    string strSrc = string.Empty;
                    foreach (var item in lstAds)
                    {
                        if (item.ImageUrl != null)
                            strSrc = item.ImageUrl;
                        else if (item.Script != null)
                            strSrc = item.Script;
                        strAds = strAds + "<tr class=\"gradeX\"><td><a href=\"AddAdvertisement.aspx?id=" + item.Id + "\">Edit</a></td><td>" + item.Advertisment + "</td><td>" + strSrc + "</td><td>" + item.EntryDate + "</td><td>" + item.ExpiryDate + "</td><td class=\"center\">" + item.Status + "</td></tr>";
                    }
                    divAds.InnerHtml = strAds;
                }
                catch (Exception Err)
                {
                    logger.Error(Err.Message);
                    Console.Write(Err.StackTrace);
                }
            }
        }
    }
}
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
    public partial class ManagePackage : System.Web.UI.Page
    {
        ILog logger = LogManager.GetLogger(typeof(ManagePackage));
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
                    PackageRepository objPkgRepo = new PackageRepository();
                    List<Package> lstPkg = objPkgRepo.getAllPackage();
                    string strPackage = string.Empty;
                    foreach (var item in lstPkg)
                    {
                        //strPackage += "<tr class=\"gradeX\"><td><a href=\"AddPackage.aspx?id=" + item.Id + "\">Edit</a></td><td>" + item.PackageName + "</td><td>" + item.Pricing + "</td><td>" + item.EntryDate + "</td><td class=\"center\">" + item.Status + "</td></tr>";
                        strPackage += "<tr class=\"gradeX\"><td><a href=\"AddPackage.aspx?id=" + item.Id + "\">Edit</a></td><td>" + item.PackageName + "</td><td>" + item.Pricing + "</td><td>" + item.EntryDate + "</td></tr>";
                    }
                    divPackage.InnerHtml = strPackage;
                }
                catch (Exception ex)
                {
                    logger.Error(ex.Message);
                }
            }
        }
    }
}
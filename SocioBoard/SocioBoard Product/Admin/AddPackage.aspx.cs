using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SocioBoard.Domain;
using SocioBoard.Model;
using log4net;

namespace SocialSuitePro.Admin
{
    public partial class AddPackage : System.Web.UI.Page
    {
        ILog logger = LogManager.GetLogger(typeof(AddPackage));

        PackageRepository objPkgRepo = new PackageRepository();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["AdminProfile"] == null)
                {
                    Response.Redirect("Default.aspx");
                }

                if (Request.QueryString["Id"] != null)
                {
                    try
                    {
                        Guid packageid = Guid.Parse(Request.QueryString["Id"].ToString());
                        Package pkg = objPkgRepo.getPackageDetailsbyId(packageid);
                        if (pkg != null)
                        {
                            txtPackage.Text = pkg.PackageName;
                            txtPricing.Text = pkg.Pricing.ToString();
                            //ddltatus.SelectedValue = pkg.Status.ToString();
                        }
                    }
                    catch (Exception ex)
                    {
                        logger.Error(ex.Message);
                    }
                }
            }

        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                Package objPkg = new Package();

                if (Request.QueryString["Id"] != null)
                    objPkg.Id = Guid.Parse(Request.QueryString["Id"].ToString());
                else
                    objPkg.Id = Guid.NewGuid();

                objPkg.PackageName = txtPackage.Text;
                objPkg.Pricing = double.Parse(txtPricing.Text);
                objPkg.EntryDate = DateTime.Now;
                objPkg.Status = true;//bool.Parse(ddltatus.SelectedValue);
                if (objPkgRepo.checkPackageExists(objPkg.Id))
                    objPkgRepo.UpdatePackage(objPkg);
                else
                    objPkgRepo.AddPackage(objPkg);
                Response.Redirect("ManagePackage.aspx");
            }
            catch (Exception Err)
            {
                logger.Error(Err.Message);
                Response.Write(Err.StackTrace);
            }
        }
    }
}
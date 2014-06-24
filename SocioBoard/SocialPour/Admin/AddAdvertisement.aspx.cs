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
    public partial class AddAdvertisement : System.Web.UI.Page
    {
        ILog logger = LogManager.GetLogger(typeof(AddAdvertisement));

        protected void Page_Load(object sender, EventArgs e)
        {
            
            if (!IsPostBack)
            {
                if (Session["AdminProfile"] == null)
                {
                    Response.Redirect("Default.aspx");
                }
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                Ads objAds = new Ads();
                AdsRepository objAdsRepo = new AdsRepository();
                string advImg=string.Empty;
                if (txtUrl.Text != "")
                    objAds.ImageUrl = txtUrl.Text;
                else if (fuAdv.HasFile)
                {
                   // fuAdv.SaveAs(Server.MapPath("/AdvImage/") + fuAdv.FileName);
                    string FileFullPath = Server.MapPath("/Admin/AdvImage/") + fuAdv.FileName;
                    fuAdv.PostedFile.SaveAs(FileFullPath);

                    objAds.ImageUrl = "/Admin/AdvImage/" + fuAdv.FileName;
                }
                if (txtScript.Text != "")
                    objAds.Script = txtScript.Text;

                objAds.Status = bool.Parse(ddlStatus.SelectedValue);
                objAds.EntryDate = DateTime.Now;
                objAds.ExpiryDate = Convert.ToDateTime(datepicker.Text);
                objAds.Id = Guid.Parse(AddUpdateNews());
                if (objAdsRepo.checkAdsExists(txtAdv.Text))
                    objAdsRepo.UpdateAds(objAds);
                else
                    objAdsRepo.AddAds(objAds);
            }
            catch (Exception Err)
            {
                logger.Error(Err.Message);
                Response.Write(Err.Message);
            }
        }


        public string AddUpdateNews()
        {
            string ret = string.Empty;
            if (Request.QueryString["id"] != null)
            {
                ret = Request.QueryString["id"].ToString();
            }
            else
            {
                ret = Guid.NewGuid().ToString();
            }
            return ret;
        }

    }
}
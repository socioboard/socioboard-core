using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SocioBoard.Domain;
using SocioBoard.Model;


namespace blackSheep.Settings
{
    public partial class BusinessSetting : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                User user = (User)Session["LoggedUser"];
                memberName.Text = user.UserName;
            }
        }

       
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
           // BusinessSettingRepository

            try
            {
                BusinessSettingRepository objBusinessSettingRepository = new BusinessSettingRepository();
                SocioBoard.Domain.BusinessSetting objBusinessSetting = new SocioBoard.Domain.BusinessSetting();

                objBusinessSetting.Id = Guid.NewGuid();

                objBusinessSetting.BusinessName = txtBusnName.Text;

                if (rbDisableAssignTask.Checked)
                {
                    objBusinessSetting.FbPhotoUpload = 0;
                }
                if (rbEnableAssignTask.Checked)
                {
                    objBusinessSetting.FbPhotoUpload = 1;
                }

                if (rbDisableTaskNoti.Checked)
                {
                    objBusinessSetting.TaskNotification = 0;
                }
                if (rbEnableTaskNoti.Checked)
                {
                    objBusinessSetting.TaskNotification = 1;
                }

                if (rbFbTimeLine.Checked)
                {
                    objBusinessSetting.FbPhotoUpload = 1;
                }
                if (rbFbblackSheepPhotos.Checked)
                {
                    objBusinessSetting.FbPhotoUpload = 0;
                }

                User user = (User)Session["LoggedUser"];

                objBusinessSetting.UserId = user.Id;
                
                objBusinessSetting.EntryDate = DateTime.Now;

                objBusinessSettingRepository.AddBusinessSetting(objBusinessSetting);

                lblBusinessSettingSubmitStatus.Text = "Record Saved Successfully.";
                
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error : " + ex.StackTrace);
            }
        }
    }
}
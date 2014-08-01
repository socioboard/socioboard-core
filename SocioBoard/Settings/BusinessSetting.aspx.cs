using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SocioBoard.Domain;
using SocioBoard.Helper;
using SocioBoard.Model;

namespace SocialSuitePro.Settings
{
    public partial class BusinessSetting : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                User user = (User)Session["LoggedUser"];
                #region for You can use only 30 days as Unpaid User

                if (user.PaymentStatus.ToLower() == "unpaid")
                {
                    if (!SBUtils.IsUserWorkingDaysValid(user.ExpiryDate))
                    {
                        // ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('You can use only 30 days as Unpaid User !');", true);

                        Session["GreaterThan30Days"] = "GreaterThan30Days";

                        Response.Redirect("/Settings/Billing.aspx");
                    }
                }

                Session["GreaterThan30Days"] = null;
                #endregion
                SocioBoard.Domain.Team team = (SocioBoard.Domain.Team)Session["GroupName"];
                BusinessSettingRepository objBsnsSettingRepo = new BusinessSettingRepository();
                SocioBoard.Domain.BusinessSetting objbsns = objBsnsSettingRepo.IsAssignTaskEnable(team.GroupId);
                if (team.UserId == user.Id)
                {
                    GroupRepository objGroupRepo = new GroupRepository();
                    Groups grps = objGroupRepo.getGroupName(team.GroupId);
                    txtBusnName.Text = grps.GroupName;
                    memberName.Text = user.UserName;

                    if (objbsns.AssigningTasks == true)
                    {
                        rbEnableAssignTask.Checked = true;
                        rbDisableAssignTask.Checked = false;
                    }
                    else
                    {
                        rbEnableAssignTask.Checked = false;
                        rbDisableAssignTask.Checked = true;
                    }

                    if (objbsns.TaskNotification == true)
                    {
                        rbEnableTaskNoti.Checked = true;
                        rbDisableTaskNoti.Checked = false;
                    }
                    else
                    {
                        rbEnableTaskNoti.Checked = false;
                        rbDisableTaskNoti.Checked = true;
                    }
                }
                else
                {
                    GroupRepository objGroupRepo = new GroupRepository();
                    Groups grps = objGroupRepo.getGroupName(team.GroupId);
                    txtBusnName.Text = grps.GroupName;
                    memberName.Text = user.UserName;
                    txtBusnName.Enabled = false;
                    btnSubmit.Enabled = false;
                    rbDisableAssignTask.Enabled = false;
                    rbEnableAssignTask.Enabled = false;
                    rbDisableTaskNoti.Enabled = false;
                    rbEnableTaskNoti.Enabled = false;
                    //rbDoNotShowPromt.Enabled = false;
                    //rbShowPromt.Enabled = false;
                    //rbFbTimeLine.Enabled = false;
                    //rbFbWooSuitePhotos.Enabled = false;
                    if (objbsns.AssigningTasks == true)
                    {
                        rbEnableAssignTask.Checked = true;
                        rbDisableAssignTask.Checked = false;
                    }
                    else
                    {
                        rbEnableAssignTask.Checked = false;
                        rbDisableAssignTask.Checked = true;
                    }

                    if (objbsns.TaskNotification == true)
                    {
                        rbEnableTaskNoti.Checked = true;
                        rbDisableTaskNoti.Checked = false;
                    }
                    else
                    {
                        rbEnableTaskNoti.Checked = false;
                        rbDisableTaskNoti.Checked = true;
                    }
                }
            }
        }

       
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            User user = (User)Session["LoggedUser"];
            SocioBoard.Domain.Team team = (SocioBoard.Domain.Team)Session["GroupName"];
            SocioBoard.Domain.BusinessSetting objbsnssetting = new SocioBoard.Domain.BusinessSetting();
            SocioBoard.Model.BusinessSettingRepository objbsnsrepo = new BusinessSettingRepository();
            objbsnssetting.Id = Guid.NewGuid();
            objbsnssetting.BusinessName = txtBusnName.Text;
            objbsnssetting.GroupId = team.GroupId;
            if(rbDisableAssignTask.Checked)
            {
                objbsnssetting.AssigningTasks = false;
            }
            if (rbEnableAssignTask.Checked)
            {
                objbsnssetting.AssigningTasks = true;
            }
            if (rbDisableTaskNoti.Checked)
            {
                objbsnssetting.TaskNotification = false;
            }
            if(rbEnableTaskNoti.Checked)
            {
                objbsnssetting.TaskNotification = true;
            }
            objbsnssetting.FbPhotoUpload=0;
            objbsnssetting.UserId = user.Id;
            objbsnssetting.EntryDate = DateTime.Now;
            objbsnsrepo.AddBusinessSetting(objbsnssetting);
            
        }
    }
}
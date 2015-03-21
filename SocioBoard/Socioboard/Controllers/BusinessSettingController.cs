using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace Socioboard.Controllers
{
    public class BusinessSettingController : Controller
    {
        //
        // GET: /BusinessSetting/

        public ActionResult Index()
        {

            Guid GroupId = Guid.Parse(Session["group"].ToString());
            Socioboard.Api.Groups.Groups ApiobjGroup = new Socioboard.Api.Groups.Groups();
            Domain.Socioboard.Domain.Groups ObjGroup = (Domain.Socioboard.Domain.Groups)(new System.Web.Script.Serialization.JavaScriptSerializer().Deserialize(ApiobjGroup.GetGroupDetailsByGroupId(GroupId.ToString()), typeof(Domain.Socioboard.Domain.Groups)));
            if (ObjGroup.GroupName == ConfigurationManager.AppSettings["DefaultGroupName"].ToString())
            {
                //return Content("Default Group Can't Access");
                ViewBag.DefaultFroup = "DefaultGroup";
                return View();
            }
            else{
                return View();
            }
        }

        public ActionResult UpdateBuisnessSetting(FormCollection ObjFrmcollction)
        {
            Api.BusinessSetting.BusinessSetting ApiobjAddBusiness = new Api.BusinessSetting.BusinessSetting();
            Domain.Socioboard.Domain.BusinessSetting ObjbusinessSetting = new Domain.Socioboard.Domain.BusinessSetting();

            string BuisnessName = string.Empty;
            bool AssignTask;
            bool TaskNotification;

            BuisnessName = ObjFrmcollction["BuisnessName"].ToString();
            AssignTask = Convert.ToBoolean(ObjFrmcollction["IsTaskAssigned"].ToString());
            TaskNotification = Convert.ToBoolean(ObjFrmcollction["IsNotificationEnabled"].ToString());
            var SelectedGroupId = "";
            SelectedGroupId = ObjFrmcollction["BuisNessId"].ToString();
            ObjbusinessSetting.Id = Guid.Parse(SelectedGroupId);
            Domain.Socioboard.Domain.User ObjUser = (Domain.Socioboard.Domain.User)Session["User"];
            ObjbusinessSetting.UserId = ObjUser.Id;
            ObjbusinessSetting.GroupId = Guid.Parse(Session["group"].ToString());
            ObjbusinessSetting.BusinessName = BuisnessName;
            ObjbusinessSetting.EntryDate = DateTime.Now;
            ObjbusinessSetting.AssigningTasks = AssignTask;
            ObjbusinessSetting.TaskNotification = TaskNotification;
            ObjbusinessSetting.FbPhotoUpload = 0;
            string ObjBsnsstng= (new JavaScriptSerializer().Serialize(ObjbusinessSetting));
            ApiobjAddBusiness.UpdateBuisnessSetting(ObjBsnsstng);
            return Content("Updated Successfully");
        }

    }
}

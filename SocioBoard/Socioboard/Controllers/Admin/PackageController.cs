using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace Socioboard.Controllers.Admin
{
    //[Authorize(Users = "Aby Kumar")]
    public class PackageController : Controller
    {
        //
        // GET: /Package/

        public ActionResult ManagePackage()
        {
            return View();
        }
        public ActionResult LoadPackage()
        {
            Api.AdminPackageDetails.AdminPackageDetails apiobjpackage =new Api.AdminPackageDetails.AdminPackageDetails();
            List<Domain.Socioboard.Domain.Package> lstPackage = (List<Domain.Socioboard.Domain.Package>)(new JavaScriptSerializer().Deserialize(apiobjpackage.GetAllPackage(), typeof(List<Domain.Socioboard.Domain.Package>)));

            return View("_ManagePackagePartial",lstPackage);
        }
        public ActionResult EditPackage(string Id)
        {
            Api.AdminPackageDetails.AdminPackageDetails ApiobjPackage = new Api.AdminPackageDetails.AdminPackageDetails();
            Domain.Socioboard.Domain.Package objPackage = (Domain.Socioboard.Domain.Package)(new JavaScriptSerializer().Deserialize(ApiobjPackage.GetPackageDetailsById(Id), typeof(Domain.Socioboard.Domain.Package)));
            Session["PackageToUpdate"] = objPackage;
            return View(objPackage);
        }

        public ActionResult UpdatePackage(string Price)
        {
            Domain.Socioboard.Domain.Package objPackage = (Domain.Socioboard.Domain.Package)Session["PackageToUpdate"];
            objPackage.Pricing = double.Parse(Price);
            string ObjPackage=(new JavaScriptSerializer().Serialize(objPackage));
            Api.AdminPackageDetails.AdminPackageDetails ApiobjPackage = new Api.AdminPackageDetails.AdminPackageDetails();
            string PkgUpdateMessage=(string)(new JavaScriptSerializer().Deserialize(ApiobjPackage.UpdatePackage(ObjPackage),typeof(string)));
            return Content(PkgUpdateMessage);
        }

    }
}

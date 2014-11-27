using Api.Socioboard.Model;
using Domain.Socioboard.Domain;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Script.Services;
using System.Web.Services;

namespace Api.Socioboard.Services
{
    /// <summary>
    /// Summary description for AdminPackageDetails
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class AdminPackageDetails : System.Web.Services.WebService
    {
        PackageRepository objPackagerepo = new PackageRepository();

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string AddPackage(string ObjPackage)
        {
            try
            {
                Domain.Socioboard.Domain.Package objPackage = (Domain.Socioboard.Domain.Package)(new JavaScriptSerializer().Deserialize(ObjPackage, typeof(Domain.Socioboard.Domain.Package)));
                objPackagerepo.AddPackage(objPackage);
                return new JavaScriptSerializer().Serialize("Package Added Successfully");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return null;
            }
        }



        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string GetAllPackage()
        {
            try
            {
                List<Domain.Socioboard.Domain.Package> lstPackage = objPackagerepo.getAllPackage();
                if (lstPackage != null)
                {
                    return new JavaScriptSerializer().Serialize(lstPackage);
                }
                else
                {
                    return new JavaScriptSerializer().Serialize("Not Package Found");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return null;
            }
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string UpdatePackage(string ObjPackage)
        {
            try
            {
                Domain.Socioboard.Domain.Package objPackage = (Domain.Socioboard.Domain.Package)(new JavaScriptSerializer().Deserialize(ObjPackage, typeof(Domain.Socioboard.Domain.Package)));
                objPackagerepo.UpdatePackage(objPackage);
                return new JavaScriptSerializer().Serialize("Package Updated Successfully");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return null;
            }
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string GetPackageDetailsById(string PackageId)
        {
            Guid Packageid = Guid.Parse(PackageId);
            try
            {
                Domain.Socioboard.Domain.Package objPackage = objPackagerepo.getPackageDetailsbyId(Packageid);

                return new JavaScriptSerializer().Serialize(objPackage);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return null;
            }
        }
    }
}

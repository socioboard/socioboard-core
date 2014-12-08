using Api.Socioboard.Helper;
using Api.Socioboard.Model;
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
    /// Summary description for Group
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [ScriptService]
    public class Ads : System.Web.Services.WebService
    {


        AdsRepository objAdsRepo = new AdsRepository();

        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string GetAllAds()
        {
            try
            {
                List<Domain.Socioboard.Domain.Ads> objAds = objAdsRepo.getAllAds();
                return new JavaScriptSerializer().Serialize(objAds);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return null;
            }
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string GetAdsdetailsById(string AdsId)
        {
            try
            {
                Domain.Socioboard.Domain.Ads objAds = objAdsRepo.getAdsDetailsbyId(Guid.Parse(AdsId));
                return new JavaScriptSerializer().Serialize(objAds);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string UpdateAdvertisement(string ObjAdvertisement)
        {
            try
            {
                Domain.Socioboard.Domain.Ads ObjAds = (Domain.Socioboard.Domain.Ads)(new JavaScriptSerializer().Deserialize(ObjAdvertisement, typeof(Domain.Socioboard.Domain.Ads)));
                objAdsRepo.UpdateAds(ObjAds);
                return new JavaScriptSerializer().Serialize("Updated Successfully");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return null;
            }
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string AddAdvertisement(string ObjAdvertisement, string Advertise)
        {
            try
            {
                if (objAdsRepo.checkAdsExists(Advertise))
                {
                    Domain.Socioboard.Domain.Ads ObjAds = (Domain.Socioboard.Domain.Ads)(new JavaScriptSerializer().Deserialize(ObjAdvertisement, typeof(Domain.Socioboard.Domain.Ads)));
                    objAdsRepo.UpdateAds(ObjAds);
                    return new JavaScriptSerializer().Serialize("Success");
                }
                else
                {
                    Domain.Socioboard.Domain.Ads ObjAds = (Domain.Socioboard.Domain.Ads)(new JavaScriptSerializer().Deserialize(ObjAdvertisement, typeof(Domain.Socioboard.Domain.Ads)));
                    objAdsRepo.AddAds(ObjAds);
                    return new JavaScriptSerializer().Serialize("Success");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return null;
            }
        }
    }
}

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
    /// Summary description for Affiliates
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class Affiliates : System.Web.Services.WebService
    {
        AffiliatesRepository Affiliaterepo = new AffiliatesRepository();
        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }
        [WebMethod]
        public void AddAffiliateDetail(string UserId, string FriendsUserId, DateTime AffiliateDate, string Amount)
        {
            Domain.Socioboard.Domain.Affiliates Affiliate = new Domain.Socioboard.Domain.Affiliates();
            try
            {
                Affiliate.Id = Guid.NewGuid();
                Affiliate.UserId = Guid.Parse(UserId);
                Affiliate.FriendUserId = Guid.Parse(FriendsUserId);
                Affiliate.AffiliateDate = AffiliateDate;
                Affiliate.Amount = Amount;
                Affiliaterepo.Add(Affiliate);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string GetAffilieteDetailbyUserId(string UserId, string FriendsUserId) 
        {
            try
            {
                return new JavaScriptSerializer().Serialize(Affiliaterepo.GetAffiliateDataByUserId(Guid.Parse(UserId), Guid.Parse(FriendsUserId)));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string GetAffilieteDetailbyUserIdTrans(string UserId)
        {
            try
            {
                return new JavaScriptSerializer().Serialize(Affiliaterepo.GetAffiliateDataByUserId(Guid.Parse(UserId)));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Script.Services;
using System.Web.Services;
using Api.Socioboard.Model;
using log4net;

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
        ILog logger = LogManager.GetLogger(typeof(Affiliates));
        UserRepository _UserRepository = new UserRepository();
        EwalletWithdrawRequestRepository _EwalletWithdrawRequestRepository = new EwalletWithdrawRequestRepository();
        AffiliatesRepository Affiliaterepo = new AffiliatesRepository();
        
        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
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
                logger.Error(ex.Message);
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
                logger.Error(ex.Message);
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
                logger.Error(ex.Message);
                return null;
            }
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string AddRequestToWithdraw(string WithdrawAmount, string PaymentMethod, string PaypalEmail,string IbanCode, string SwiftCode, string Other, string UserID)
        {
            Domain.Socioboard.Domain.User _User;
            try
            {
                _User = _UserRepository.getUsersById(Guid.Parse(UserID));
                Domain.Socioboard.Domain.EwalletWithdrawRequest _EwalletWithdrawRequest = new Domain.Socioboard.Domain.EwalletWithdrawRequest();
                _EwalletWithdrawRequest.Id = Guid.NewGuid();
                _EwalletWithdrawRequest.UserName = _User.UserName;
                _EwalletWithdrawRequest.UserEmail = _User.EmailId;
                _EwalletWithdrawRequest.PaypalEmail = PaypalEmail;
                _EwalletWithdrawRequest.PaymentMethod = PaymentMethod;
                _EwalletWithdrawRequest.IbanCode = IbanCode;
                _EwalletWithdrawRequest.SwiftCode = SwiftCode;
                _EwalletWithdrawRequest.Other = Other;
                _EwalletWithdrawRequest.Status = 0;
                _EwalletWithdrawRequest.WithdrawAmount = WithdrawAmount;
                _EwalletWithdrawRequest.UserId = Guid.Parse(UserID);
                _EwalletWithdrawRequest.RequestDate = DateTime.Now;
                _EwalletWithdrawRequestRepository.Add(_EwalletWithdrawRequest);
                _UserRepository.UpdateEwalletAmount(Guid.Parse(UserID),(Double.Parse(_User.Ewallet)-Double.Parse(WithdrawAmount)).ToString());
               
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
            }
            _User = _UserRepository.getUsersById(Guid.Parse(UserID));
            return new JavaScriptSerializer().Serialize(_User);
        }
        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string GetEwalletWithdraw(string UserId)
        {
            return (new JavaScriptSerializer().Serialize(_EwalletWithdrawRequestRepository.GetEwalletWithdraw(Guid.Parse(UserId))));
        }
        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string GetAllEwalletWithdraw()
        {
            return (new JavaScriptSerializer().Serialize(_EwalletWithdrawRequestRepository.GetAllEwalletWithdraw()));
        }
        [WebMethod]
        public int UpdatePaymentStatus(string id, int status)
        {
            return _EwalletWithdrawRequestRepository.UpdatePaymentStatus(Guid.Parse(id), status);
        }
    }
}

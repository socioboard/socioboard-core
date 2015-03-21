using Api.Socioboard.Helper;
using Api.Socioboard.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Script.Services;
using System.Web.Services;
using SocioBoard.Model;

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
    public class PaymentTransaction : System.Web.Services.WebService
    {

        PaymentTransactionRepository objPaymentTransactionRepository = new PaymentTransactionRepository();

        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }

        [WebMethod]
        public string SavePayPalTransaction(string userID, string amountPaid)
        {
            Domain.Socioboard.Domain.PaymentTransaction objPaymentTransaction = new Domain.Socioboard.Domain.PaymentTransaction();
            objPaymentTransaction.AmountPaid = amountPaid;
            objPaymentTransaction.PaymentDate = DateTime.Now;
            objPaymentTransaction.PaymentStatus="paid";
            objPaymentTransaction.UserId = Guid.Parse(userID);

            string paymentResponse = objPaymentTransactionRepository.SavePayPalTransaction(objPaymentTransaction);

            return new JavaScriptSerializer().Serialize(paymentResponse);
        }
        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string GetPaymentDataByUserId(string UserId)
        {
            try
            {
                return new JavaScriptSerializer().Serialize(objPaymentTransactionRepository.GetPaymentDataByUserId(Guid.Parse(UserId)));
            }
            catch (Exception ex)
            {

                return null; 
            }
        }

    }
}

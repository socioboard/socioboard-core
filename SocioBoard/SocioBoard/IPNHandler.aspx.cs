using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net;
using System.Text;
using System.IO;
using SocioBoard.Model;
using SocioBoard.Domain;
using PayPal;
using System.Collections.Specialized;
using log4net;

namespace SocialSuitePro
{
    public partial class IPNHandler : System.Web.UI.Page
    {

        ILog logger = LogManager.GetLogger(typeof(IPNHandler));

        protected void Page_Load(object sender, EventArgs e)
        {

            try
                {
                        byte[] parameters = Request.BinaryRead(HttpContext.Current.Request.ContentLength);
                        PaymentTransaction paymentTransaction = new PaymentTransaction();
                        PaymentTransactionRepository paymentRepo = new PaymentTransactionRepository();

                        if (parameters.Length > 0)
                        {
                            IPNMessage ipn = new IPNMessage(parameters);
                            bool isIpnValidated = ipn.Validate();
                            string transactionType = ipn.TransactionType;
                            NameValueCollection map = ipn.IpnMap;
                            
                            paymentTransaction.AmountPaid = map["payment_gross"];
                            paymentTransaction.PayPalTransactionId = map["txn_id"];
                            paymentTransaction.UserId = Guid.Parse(map["custom"].ToString());
                            paymentTransaction.Id = Guid.NewGuid();
                            paymentTransaction.IPNTrackId = map["ipn_track_id"];
                            
                            paymentTransaction.PayerEmail = map["payer_email"];
                            paymentTransaction.PayerId = map["payer_id"];
                            paymentTransaction.PaymentStatus = map["payment_status"];

                            
                            
                                logger.Info("Payment Status : " + paymentTransaction.PaymentStatus);
                                logger.Info("User Id : " +paymentTransaction.UserId);
                            

                            paymentTransaction.PaymentDate = DateTime.Now;
                            paymentTransaction.PaypalPaymentDate = map["payment_date"];
                            paymentTransaction.ReceiverId = map["receiver_id"];
                            paymentRepo.SavePayPalTransaction(paymentTransaction);
                            UserRepository userrepo = new UserRepository();
                            if (paymentTransaction.PaymentStatus == "Completed")
                            {
                                userrepo.changePaymentStatus(paymentTransaction.UserId, "paid");
                            }
                        }
                    }
                
                catch (System.Exception ex)
                {
                    logger.Error(ex.StackTrace);
                }
            
            
        }
        string GetPayPalResponse(Dictionary<string, string> formVals, bool useSandbox)
        {

            string paypalUrl = "https://www.sandbox.paypal.com/cgi-bin/webscr";

            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(paypalUrl);
            req.Method = "POST";
            req.ContentType = "application/x-www-form-urlencoded";

            byte[] param = Request.BinaryRead(Request.ContentLength);
            string strRequest = Encoding.ASCII.GetString(param);

            StringBuilder sb = new StringBuilder();
            sb.Append(strRequest);

            foreach (string key in formVals.Keys)
            {
                sb.AppendFormat("&{0}={1}", key, formVals[key]);
            }
            strRequest += sb.ToString();
            req.ContentLength = strRequest.Length;

            string response = "";
            using (StreamWriter streamOut = new StreamWriter(req.GetRequestStream(), System.Text.Encoding.ASCII))
            {

                streamOut.Write(strRequest);
                streamOut.Close();
                using (StreamReader streamIn = new StreamReader(req.GetResponse().GetResponseStream()))
                {
                    response = streamIn.ReadToEnd();
                }
            }
            return response;
        }
    }
}
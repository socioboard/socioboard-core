using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SocioBoard.Domain
{
    public class PaymentTransaction
    {

        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string PayPalTransactionId { get; set; }
        public string AmountPaid { get; set; }
       // public string OrderId { get; set; }
        public DateTime PaymentDate { get; set; }
        public string PaymentStatus { get; set; }
        public string PayerId { get; set; }
        public string ReceiverId { get; set; }
        public string PayerEmail { get; set; }
        public string PaypalPaymentDate { get; set; }
        public string IPNTrackId { get; set; }
        public string VersionType { get; set; }
        public string DetailsInfo { get; set; }
    }
}
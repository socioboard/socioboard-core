using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain.Socioboard.Domain
{
    public class EwalletWithdrawRequest
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string UserEmail { get; set; }
        public string WithdrawAmount { get; set; }
        public string PaymentMethod { get; set; }
        public string PaypalEmail { get; set; }
        public string IbanCode { get; set; }
        public string SwiftCode { get; set; }
        public string Other { get; set; }
        public int Status { get; set; }
        public DateTime RequestDate { get; set; }
        public Guid UserId { get; set; }
    }
}

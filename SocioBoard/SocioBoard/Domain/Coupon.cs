using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SocioBoard.Domain
{
    public class Coupon
    {
        public Guid Id { get; set; }
        public string CouponCode { get; set; }
        public DateTime EntryCouponDate { get; set; }
        public DateTime ExpCouponDate { get; set; }
        public string Status { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SocioBoard.Domain
{
    public class User
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string AccountType { get; set; }
        public string ProfileUrl { get; set; }
        public string EmailId { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime ExpiryDate { get; set; }
        public int UserStatus { get; set; }
        public string Password { get; set; }
        public string TimeZone { get; set; }
        public string PaymentStatus { get; set; }
        public string ActivationStatus { get; set; }
        public string CouponCode { get; set; }
        public string ReferenceStatus { get; set; }
        public string RefereeStatus { get; set; }
        public string UserType { get; set; }
        
        public static List<User> lstUser=new List<User>(); 
    }
}
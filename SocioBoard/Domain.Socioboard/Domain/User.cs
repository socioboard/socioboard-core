using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Domain.Socioboard.Domain
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

        public string ChangePasswordKey { get; set; }
        public int IsKeyUsed { get; set; }
        public string ChangeEmailKey { get; set; }
        public int IsEmailKeyUsed { get; set; }
        public string Ewallet { get; set; }
        public string UserCode { get; set; }
        public string SocialLogin { get; set; }
        public DateTime LastLoginTime { get; set; }
        public static List<User> lstUser = new List<User>();
    }
}
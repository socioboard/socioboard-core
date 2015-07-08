using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Api.Socioboard.Helper.UserManager
{
    public class User : IUser<Guid>
    {
        public User()
        {
            this.Roles = new List<string>();
          //  this.Claims = new List<UserClaim>();
        }

        public User(string userName)
            : this()
        {
            this.UserName = userName;
        }

        public User(int id, string userName)
            : this()
        {
            this.Id = Id;
            this.UserName = userName;
        }

        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string AccountType { get; set; }
        public string ProfileUrl { get; set; }
        public string EmailId { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime ExpiryDate { get; set; }
        public int UserStatus { get; set; }
        public string Password { get; set; }

        public IList<string> Roles { get;  set; }
        //public IList<UserClaim> Claims { get; private set; }
    }
}
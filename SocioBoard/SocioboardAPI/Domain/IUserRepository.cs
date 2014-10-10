using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Api.Socioboard.Domain
{
    public interface IUserRepository
    {
        User GetUserInfo(string Emailid, string password);
        bool IsUserExist(string Emailid);
        void UpdatePassword(string emailid, string password, Guid id, string username, string accounttype);
        User getUserInfoByEmail(string emailId);
        int ChangePassword(string newpassword, string oldpassword, string Emailid);

    }
}
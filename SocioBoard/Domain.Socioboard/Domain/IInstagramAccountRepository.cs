using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace Domain.Socioboard.Domain
{
   public interface IInstagramAccountRepository
    {
        void addInstagramUser(InstagramAccount insaccount);
        int deleteInstagramUser(string fbuserid, Guid userid);
        void updateInstagramUser(InstagramAccount fbaccount);
        ArrayList getAllInstagramAccountsOfUser(Guid UserId);
        bool checkInstagramUserExists(string FbUserId, Guid Userid);
        InstagramAccount getInstagramAccountDetailsById(string Fbuserid, Guid userId);
    }
}

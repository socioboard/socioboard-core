using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace Domain.Socioboard.Domain
{
    public interface IGooglePlusAccountRepository
    {
        void addGooglePlusUser(GooglePlusAccount gpaccount);
        int deleteGooglePlusUser(string gpuserid, Guid userid);
        void updateGooglePlusUser(GooglePlusAccount gpaccount);
        ArrayList getAllGooglePlusAccountsOfUser(Guid UserId);
        bool checkGooglePlusUserExists(string FbUserId, Guid Userid);
        GooglePlusAccount getGooglePlusAccountDetailsById(string Fbuserid, Guid userId);
        GooglePlusAccount getUserDetails(string GpUserId);
       
    }
}

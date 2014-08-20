using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace SocioBoard.Domain
{
    interface ILinkedInAccountRepository
    {
        void addLinkedinUser(LinkedInAccount liaccount);
        int deleteLinkedinUser(string liuserid, Guid userid);
        void updateLinkedinUser(LinkedInAccount liaccount);
        ArrayList getAllLinkedinAccountsOfUser(Guid UserId);
        bool checkLinkedinUserExists(string liUserId, Guid Userid);
    }
}

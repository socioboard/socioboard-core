using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace SocioBoard.Domain
{
    public interface ITwitterMessageRepository
    {
        void addTwitterMessage(TwitterMessage fbaccount);
        int deleteTwitterMessage(TwitterMessage fbaccount);
        int updateTwitterMessage(TwitterMessage fbaccount);
        List<TwitterMessage> getAllTwitterMessagesOfUser(Guid UserId,string profileid);
        bool checkTwitterMessageExists(string Id, Guid Userid,string messageId);
        List<TwitterMessage> getAllTwitterMessages(Guid userid);
    }
}

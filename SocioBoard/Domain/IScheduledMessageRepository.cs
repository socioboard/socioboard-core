using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SocioBoard.Domain
{
    public interface IScheduledMessageRepository
    {
        void addNewMessage(ScheduledMessage schmesg);
        void deleteMessage(ScheduledMessage schmesg);
        void updateMessage(Guid TableId);
        IEnumerable<ScheduledMessage> getAllMessage();
    }
}
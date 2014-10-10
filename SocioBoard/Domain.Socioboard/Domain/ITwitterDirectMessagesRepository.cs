using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain.Socioboard.Domain
{
  public interface ITwitterDirectMessagesRepository
    {

      void addNewDirectMessage(TwitterDirectMessages twtDirectMessages);
      int deleteDirectMessage(Guid userid, string profileid);
      void updateDirectMessage(TwitterDirectMessages twtDirectMessages);

      List<TwitterDirectMessages> getAllDirectMessagesByScreenName(string screenName);
      List<TwitterDirectMessages> getAllDirectMessagesById(string profileid);
      
    }
}

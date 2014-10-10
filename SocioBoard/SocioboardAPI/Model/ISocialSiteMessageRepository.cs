using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Domain.Socioboard.Domain;

namespace Api.Socioboard.Model
{
    public interface ISocialSiteMessageRepository
    {
        void AddMessage(Domain.Socioboard.Domain.ISocialSiteMessage objSocialSiteMessage);

        void DeleteMessage(Domain.Socioboard.Domain.ISocialSiteMessage objSocialSiteMessage);

        void UpdateMessage(Domain.Socioboard.Domain.ISocialSiteMessage objSocialSiteMessage);

        List<Domain.Socioboard.Domain.ISocialSiteMessage> GetAllMessagesOfUser(Guid UserId, string profileId);

        bool CheckMessageExists(string Id, Guid Userid);

        void deleteAllMessagesOfUser(string fbuserid, Guid userid);

        List<Domain.Socioboard.Domain.FacebookMessage> getAllMessageOfProfile(string profileid);
    }
}

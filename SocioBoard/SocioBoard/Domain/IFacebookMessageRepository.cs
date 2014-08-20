using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;

namespace SocioBoard.Domain
{
	public interface IFacebookMessageRepository
	{
        void addFacebookMessage(FacebookMessage fbaccount);
        int deleteFacebookMessage(FacebookMessage fbaccount);
        int updateFacebookMessage(FacebookMessage fbaccount);
        List<FacebookMessage> getAllFacebookMessagesOfUser(Guid UserId, string Profileid);
        bool checkFacebookMessageExists(string Id, Guid Userid);
        void deleteAllMessagesOfUser(string fbuserid, Guid userid);
        List<FacebookMessage> getAllMessageOfProfile(string profileid);
        List<FacebookMessage> getAllWallpostsOfProfile(string profileid);
        bool checkFacebookMessageExists(string Id);
        List<FacebookMessage> getFacebookUserWallPost(Guid userid, string profileid);
        void getAllFacebookMessagesOfUsers(Guid UserId, string profileId);
	}
}
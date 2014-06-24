using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SocioBoard.Domain
{
    interface IInstagramComment
    {
        void addInstagramComment(InstagramComment inscomment);
        int deleteInstagramComment(InstagramComment inscomment);
        int updateInstagramComment(InstagramComment inscomment);
        List<InstagramComment> getAllInstagramCommentsOfUser(Guid UserId, string profileid,string feedid);
        bool checkInstagramCommentExists(string feedid, Guid Userid);
        void deleteAllCommentsOfUser(string fbuserid, Guid userid);
    }
}

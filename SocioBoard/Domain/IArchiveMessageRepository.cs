using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SocioBoard.Domain
{
    public interface IArchiveMessageRepository
    {
        void AddArchiveMessage(ArchiveMessage archive);
        int DeleteArchiveMessage(ArchiveMessage archive);
        void UpdateArchiveMessage(ArchiveMessage archive);
        List<ArchiveMessage> getAllArchiveMessage(Guid Userid);
        bool checkArchiveMessageExists(Guid userid, string archive);

        ArchiveMessage getArchiveMessageDetails(Guid userid, string archive);
    }
}

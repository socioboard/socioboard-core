using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SocioBoard.Domain
{
    public class BusinessSetting
    {
        public Guid Id { get; set; }
        public string BusinessName { get; set; }
        public int AssigningTasks { get; set; }
        public int TaskNotification { get; set; }
        public int FbPhotoUpload { get; set; }
        public Guid UserId { get; set; }
        public DateTime EntryDate { get; set; }
    }
}
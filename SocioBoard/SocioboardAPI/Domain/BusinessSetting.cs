using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Api.Socioboard.Domain
{
    public class BusinessSetting
    {
        public Guid Id { get; set; }
        public string BusinessName { get; set; }
        public Guid GroupId { get; set; }
        public bool AssigningTasks { get; set; }
        public bool TaskNotification { get; set; }
        public int FbPhotoUpload { get; set; }
        public Guid UserId { get; set; }
        public DateTime EntryDate { get; set; }
    }
}
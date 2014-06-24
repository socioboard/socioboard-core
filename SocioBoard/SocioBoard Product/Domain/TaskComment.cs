using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SocioBoard.Domain
{
    public class TaskComment
    {
        public Guid Id { get; set; }
        public string Comment { get; set; }
        public Guid UserId { get; set; }
        public Guid TaskId { get; set; }
        public DateTime CommentDate { get; set; }
        public DateTime EntryDate{ get; set; }
    }
}
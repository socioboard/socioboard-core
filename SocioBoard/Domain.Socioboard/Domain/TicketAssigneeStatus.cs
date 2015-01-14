using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain.Socioboard.Domain
{
    public class TicketAssigneeStatus
    {
       public Guid Id { get; set; }
       public Guid AssigneeUserId { get; set; }
       public int AssignedTicketCount { get; set; }
    }
}

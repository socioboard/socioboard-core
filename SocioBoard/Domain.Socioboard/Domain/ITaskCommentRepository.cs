using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace Domain.Socioboard.Domain
{
   public interface ITaskCommentRepository
    {
        void addTaskComment(TaskComment taskcomment);
        ArrayList getAllTasksCommentOfUser(Guid TaskId);
    }
}

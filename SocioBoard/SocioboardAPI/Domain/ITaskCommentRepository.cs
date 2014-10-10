using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace Api.Socioboard.Domain
{
    interface ITaskCommentRepository
    {
        void addTaskComment(TaskComment taskcomment);
        ArrayList getAllTasksCommentOfUser(Guid TaskId);
    }
}

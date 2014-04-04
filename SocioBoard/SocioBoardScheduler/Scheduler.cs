using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SocioBoardScheduler
{
 public  abstract class Scheduler
    {
       public abstract void PostScheduleMessage(dynamic data);
       public abstract void PostScheduleMessageWithImage(dynamic data);
    }
}

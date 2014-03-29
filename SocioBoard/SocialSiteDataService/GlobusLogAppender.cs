using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using log4net;
using System.Windows.Forms;

namespace SocialSiteDataService.Logger.Appender
{
    public class GlobusLogAppender:log4net.Appender.AppenderSkeleton
    {
        protected override void Append(log4net.Core.LoggingEvent loggingEvent)
        {
           
            //toolStripStatusLabel1.Text = loggingEvent.RenderedMessage;
        }
        
    }
}

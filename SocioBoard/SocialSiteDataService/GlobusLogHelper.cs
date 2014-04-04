using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using log4net;
using log4net.Config;

namespace SocialSiteDataService
{
    public sealed class GlobusLogHelper
    {
        private static volatile GlobusLogHelper globusLogHelper = null;
        private static object syncRoot = new object();
        private static log4net.ILog logger = null;

        /// <summary>
        /// Singleton Instance
        /// </summary>
        public static log4net.ILog log
        {
            get
            {
                lock (syncRoot)
                {
                    if (globusLogHelper == null || logger==null)
                    {
                        globusLogHelper = new GlobusLogHelper();
                        logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                     }
                        
                }
                return logger;
            }
        }

        /// <summary>
        /// Private Constructer for Singleton Implementation
        /// </summary>
        private GlobusLogHelper()
        { 
        
        }
    }
}

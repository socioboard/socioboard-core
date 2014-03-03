using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Reflection;


namespace SocioBoard.Helper
{
    public class SessionFactory
    {
        public static NHibernate.ISessionFactory sFactory;
        /// <summary>
        /// initializes the session for database
        /// 
        /// 
        /// 
        /// 
        /// 
        /// 
        /// </summary>



        public static string configfilepath { get; set; }
        
        public static void Init()
        {
            try
            {
                NHibernate.Cfg.Configuration config = new NHibernate.Cfg.Configuration();
                string path = string.Empty;
                if (string.IsNullOrEmpty(configfilepath))
                {
                    path = HttpContext.Current.Server.MapPath("~/hibernate.cfg.xml");
                }
                else
                {
                    path = configfilepath;
                }
                config.Configure(path);
                config.AddAssembly("SocioBoard 1.0");//adds all the embedded resources .hbm.xml
                sFactory = config.BuildSessionFactory();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error : " + ex.StackTrace);
            }
        }



        /// <summary>
        /// checks wheteher the session already exists. if not then creates it
        /// </summary>
        /// <returns></returns>
        public static NHibernate.ISessionFactory GetSessionFactory()
        {
            if (sFactory == null)
            {
                Init();
            }
            return sFactory;

        }

        /// <summary>
        /// creates a database connection and opens up a session
        /// </summary>
        /// <returns></returns>
        public static NHibernate.ISession GetNewSession()
        {

            return GetSessionFactory().OpenSession();
        }

        //public static string SetConfigFilePath(string path)
        //{
        //    if (string.IsNullOrEmpty(path) && string.IsNullOrEmpty(configfilepath))
        //    {
        //        //  configfilepath = HttpContext.Current.Server.MapPath("~/hibernate.cfg.xml");

        //        if (object.Equals(HttpContext.Current,null))
        //        {
        //            configfilepath = System.IO.Path.GetFullPath("~/hibernate.cfg.xml");
        //        }
        //        else
        //        {
        //            configfilepath = HttpContext.Current.Server.MapPath("~/hibernate.cfg.xml");
        //        }
        //    }
        //    else
        //    {
        //        if (!string.IsNullOrEmpty(path))
        //            configfilepath = path;

        //    }
        //    return configfilepath;
        //}

    }
}
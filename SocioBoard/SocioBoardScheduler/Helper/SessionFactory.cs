using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SocioBoard.Helper;

namespace SocioBoardScheduler
{
    public class SocioBoardSchedulerSessionFactory 
    {
        public static void Init()
        {
            string path = System.IO.Path.GetFullPath("~/hibernate.cfg.xml");
           
             SessionFactory.Init();
             NHibernate.ISessionFactory session = SessionFactory.sFactory;   
        } 

    }
}
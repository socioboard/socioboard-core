using Api.Socioboard.Helper;
using Domain.Socioboard.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NHibernate.Criterion;
using NHibernate.Linq;
namespace Api.Socioboard.Model
{
    public class PluginInfoRepository
    {
        public static void Add(PluginInfo plugin)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start and open Transaction. 
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    //Proceed action to save data.
                    session.Save(plugin);
                    transaction.Commit();

                }//End Using trasaction
            }//End Using session
        }



        //public bool IsUrlExist(string Url)
        //{
        //    using (NHibernate.ISession session = SessionFactory.GetNewSession())
        //    {
        //        using (NHibernate.ITransaction transaction = session.BeginTransaction())
        //        {
        //            try
        //            {
        //                NHibernate.IQuery query = session.CreateQuery("from  plugininfo  where url = url");
        //                query.SetParameter("url", Url);
        //                var result = query.UniqueResult();
        //                if (result == null)
        //                {
        //                    return false;
        //                }
        //                else
        //                {
        //                    return true;
        //                }
        //            }
        //            catch (Exception ex)
        //            {
        //                Console.WriteLine(ex.StackTrace);
        //                return true;
        //            }
        //        }
        //    }
        //}

        public bool IsUrlExist(string Url)
        {
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                bool exist = session.Query<Domain.Socioboard.Domain.PluginInfo>()
                                 .Any(x => x.url.Contains(Url));
                return exist;
            }
        }



        public Domain.Socioboard.Domain.PluginInfo getUrlInfo(string Url)
        {
            List<PluginInfo> lstUser = new List<PluginInfo>();
            PluginInfo user = new PluginInfo();
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        lstUser = session.Query<Domain.Socioboard.Domain.PluginInfo>().Where(x => x.url.Contains(Url)).ToList();
                            user=lstUser[0];
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                        user = null;
                    }
                }
            }

            return user;
        }
    }
}
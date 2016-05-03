using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Domain.Socioboard.Domain;
using Api.Socioboard.Helper;
using NHibernate.Linq;


namespace Api.Socioboard.Model
{
    public class dailymotionpostRepository
    {
        
        public static void Add(Domain.Socioboard.Domain.dailymotionpost user)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start and open Transaction. 
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    //Proceed action to save data.
                    session.Save(user);
                    transaction.Commit();

                }//End Using trasaction
            }//End Using session
        }


        public bool checkdailymotionpostExists(string Url)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        bool exist = session.Query<Domain.Socioboard.Domain.dailymotionpost>()
                                .Any(x => x.Url.Contains(Url));
                        return exist;

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                        return true;
                    }

                }//End Transaction
            }//End session
        }

        public bool checkpostexist(string videoid, string VideoUrl)
        {
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        bool exist = session.Query<Domain.Socioboard.Domain.dailymotionpost>().Any(x => x.VideoId.Contains(videoid) && x.VideoUrl.Contains(VideoUrl));
                        return exist;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                        return true;
                    }
                
                
                }
            
            }
        
        }


        public List<Domain.Socioboard.Domain.dailymotionpost> getAlldailymotionpost(string url)
        {

            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {


                        List<Domain.Socioboard.Domain.dailymotionpost> lstfbmsg = session.CreateQuery("from dailymotionpost where Url = :Url")
                        .SetParameter("Url", url)
                        .List<Domain.Socioboard.Domain.dailymotionpost>()
                        .ToList<Domain.Socioboard.Domain.dailymotionpost>();
                        #region oldcode
                        
                        #endregion
                        return lstfbmsg;

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                        return null;
                    }

                }//End Transaction
            }//End session
        }

        public List<Domain.Socioboard.Domain.dailymotionpost> getalldailymotionurl()
        {

            using (NHibernate.ISession session = SessionFactory.GetNewSession()) {

                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {

                    try
                    {
                        List<Domain.Socioboard.Domain.dailymotionpost> lsturl = session.CreateQuery("from dailymotionpost group by Url").List<Domain.Socioboard.Domain.dailymotionpost>().ToList<Domain.Socioboard.Domain.dailymotionpost>();
                        return lsturl;

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                        Console.WriteLine(ex.Message);
                        return null;
                    }
                }
            
            }
        
        }


    }
}
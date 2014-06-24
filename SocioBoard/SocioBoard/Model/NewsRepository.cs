using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SocioBoard.Domain;
using SocioBoard.Helper;
using NHibernate.Criterion;

namespace SocioBoard.Model
{
    public class NewsRepository : INewsRepository
    {
        public void AddNews(News news)
        {
            try
            {
                using (NHibernate.ISession session = SessionFactory.GetNewSession())
                {
                    using (NHibernate.ITransaction transaction = session.BeginTransaction())
                    {
                        session.Save(news);
                        transaction.Commit();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("error : " + ex.StackTrace);
            }
        }


        public int DeleteNews(Guid newsid)
        {
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        NHibernate.IQuery query = session.CreateQuery("delete from News where Id = :newsid")
                                        .SetParameter("newsid", newsid);
                        int isUpdated = query.ExecuteUpdate();
                        transaction.Commit();
                        return isUpdated;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                        return 0;
                    }
                }
            }
        }

        public void UpdateNews(News news)
        {
            try
            {
                using (NHibernate.ISession session = SessionFactory.GetNewSession())
                {
                    using (NHibernate.ITransaction transaction = session.BeginTransaction())
                    {
                        try
                        {
                            session.CreateQuery("Update News set NewsDetail =:newsdetail,ExpiryDate=:expirydate,Status=:status where Id = :newsid")
                                .SetParameter("newsdetail", news.NewsDetail)
                                .SetParameter("status", news.Status)
                                .SetParameter("newsid", news.Id)
                                .SetParameter("expirydate", news.ExpiryDate)
                                .ExecuteUpdate();
                            transaction.Commit();


                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.StackTrace);
                            // return 0;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error : " + ex.StackTrace);
            }
        }


      //public void UpdateNewsStatus(News news)
      //  {
      //      try
      //      {
      //          using (NHibernate.ISession session = SessionFactory.GetNewSession())
      //          {
      //              using (NHibernate.ITransaction transaction = session.BeginTransaction())
      //              {
      //                  try
      //                  {
      //                      session.CreateQuery("Update News set NewsDetail =:newsdetail,ExpiryDate=:expirydate,Status=:status where Id = :newsid")
      //                          .SetParameter("newsdetail", news.NewsDetail)
      //                          .SetParameter("status", news.Status)
      //                          .SetParameter("newsid", news.Id)
      //                          .SetParameter("expirydate", news.ExpiryDate)
      //                          .ExecuteUpdate();
      //                      transaction.Commit();


      //                  }
      //                  catch (Exception ex)
      //                  {
      //                      Console.WriteLine(ex.StackTrace);
      //                      // return 0;
      //                  }
      //              }
      //          }
      //      }
      //      catch (Exception ex)
      //      {
      //          Console.WriteLine("Error : " + ex.StackTrace);
      //      }
      //  }


        public List<News> getAllNews()
        {
            List<News> alstFBAccounts = new List<News>();
            try
            {
                using (NHibernate.ISession session = SessionFactory.GetNewSession())
                {
                    using (NHibernate.ITransaction transaction = session.BeginTransaction())
                    {
                        alstFBAccounts = session.CreateQuery("from News").List<News>().ToList<News>();

                        //List<News> alstFBAccounts = new List<News>();

                        //foreach (News item in query.Enumerable())
                        //{
                        //    alstFBAccounts.Add(item);
                        //}


                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error : " + ex.StackTrace);
            }
            return alstFBAccounts;
        }

        public bool checkNewsExists(string newsdetail)
        {
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        NHibernate.IQuery query = session.CreateQuery("from News where NewsDetail =:newsdetail");
                        query.SetParameter("newsdetail", newsdetail);
                        var result = query.UniqueResult();
                        if (result == null)
                            return false;
                        else
                            return true;

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                        return true;
                    }


                }
            }
        }

        public bool checkNewsExists(Guid newsid)
        {
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        NHibernate.IQuery query = session.CreateQuery("from News where Id =:newsid");
                        query.SetParameter("newsid", newsid);
                        var result = query.UniqueResult();
                        if (result == null)
                            return false;
                        else
                            return true;

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                        return true;
                    }


                }
            }
        }

        public News getNewsDetails(string newsdetail)
        {
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        NHibernate.IQuery query = session.CreateQuery("from Package where NewsDetail=:newsdetail");
                        query.SetParameter("newsdetail", newsdetail);
                        News grou = query.UniqueResult<News>();
                        return grou;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                        return null;
                    }


                }
            }
        }




        public News getNewsDetailsbyId(Guid newsid)
        {
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        NHibernate.IQuery query = session.CreateQuery("from News where Id=:newsid");

                        query.SetParameter("newsid", newsid);
                        News grou = query.UniqueResult<News>();
                        return grou;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                        return null;
                    }


                }
            }
        }

        public News getNewsForHome()
        {
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    News nws = new News();
                    try
                    {
                        var query = session.CreateSQLQuery("Select Id,NewsDetail,Status from News Where ExpiryDate>CURDATE() and Status=1 order by Entrydate Desc");
                        foreach (var item in query.List())
                        {
                            Array temp = (Array)item;
                         
                            nws.Id = Guid.Parse(temp.GetValue(0).ToString());
                            nws.NewsDetail = temp.GetValue(1).ToString();
                          //  nws.Status = bool.Parse(temp.GetValue(2).ToString());
                            break;
                        }
                    }
                    catch (Exception Err)
                    {
                        Console.Write(Err.StackTrace);
                    }
                    return nws;
                }                
            }
        }
    }
}
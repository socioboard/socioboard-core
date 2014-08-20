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

        /// <AddNews>
        /// Add New News
        /// </summary>
        /// <param name="news">Set Values in a News Class Property and Pass the Object of News Class.(Domein.News)</param>
        public void AddNews(News news)
        {
            try
            {
                //Creates a database connection and opens up a session
                using (NHibernate.ISession session = SessionFactory.GetNewSession())
                {
                    //After Session creation, start Transaction. 
                    using (NHibernate.ITransaction transaction = session.BeginTransaction())
                    {
                        session.Save(news);
                        transaction.Commit();
                    }//End Transaction
                }//End Session
            }
            catch (Exception ex)
            {
                Console.WriteLine("error : " + ex.StackTrace);
            }
        }


        /// <DeleteNews>
        /// Delete news
        /// </summary>
        /// <param name="newsid">News id.(Guid)</param>
        /// <returns>Return integer 1 for true and 0 for false.(int)</returns>
        public int DeleteNews(Guid newsid)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction. 
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        //Proceed action, to delete news.
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
                }//End Transaction
            }//End Session
        }


        /// <UpdateNews>
        /// Update News
        /// </summary>
        /// <param name="news">Set the values in a News Class Property and Pass the Object of News Class.(Domein.News)</param>
        public void UpdateNews(News news)
        {
            try
            {
                //Creates a database connection and opens up a session
                using (NHibernate.ISession session = SessionFactory.GetNewSession())
                {
                    //After Session creation, start Transaction. 
                    using (NHibernate.ITransaction transaction = session.BeginTransaction())
                    {
                        try
                        {
                            //Proceed action, to update details of news.
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
                    }//End Transaction
                }//End Session
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


        /// <getAllNews>
        /// Get all news
        /// </summary>
        /// <returns>Return object of News Class with  value of each member in the form of list.(List<News>)</returns>
        public List<News> getAllNews()
        {
            List<News> alstFBAccounts = new List<News>();
            try
            {
                //Creates a database connection and opens up a session
                using (NHibernate.ISession session = SessionFactory.GetNewSession())
                {
                    //After Session creation, start Transaction. 
                    using (NHibernate.ITransaction transaction = session.BeginTransaction())
                    {
                        alstFBAccounts = session.CreateQuery("from News").List<News>().ToList<News>();

                        //List<News> alstFBAccounts = new List<News>();

                        //foreach (News item in query.Enumerable())
                        //{
                        //    alstFBAccounts.Add(item);
                        //}

                    }//End Transaction
                }//End Session
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error : " + ex.StackTrace);
            }
            return alstFBAccounts;
        }


        /// <checkNewsExists>
        /// Check the existing news
        /// </summary>
        /// <param name="newsdetail">News detail. (string)</param>
        /// <returns>True or False.(bool)</returns>
        public bool checkNewsExists(string newsdetail)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction. 
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        //Proceed action, to get details of news.
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


                }//End Transaction
            }//End Session
        }


        /// <checkNewsExists>
        /// Check the news details is exist or not.
        /// </summary>
        /// <param name="newsid">News id. (Guid)</param>
        /// <returns>True or False. (bool)</returns>
        public bool checkNewsExists(Guid newsid)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction. 
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        //Proceed action, to find news by id.
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


                }//End Transaction
            }//End Session
        }


        /// <getNewsDetails>
        /// Get the all info of news by news details.
        /// </summary>
        /// <param name="newsdetail">New details.</param>
        /// <returns>Return object of News Class with  all news info.(List<News>)</returns>
        public News getNewsDetails(string newsdetail)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction. 
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        //Proceed action, to get the all info of news by news details.
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


                }//End Transaction
            }//End Session
        }


        /// <getNewsDetailsbyId>
        /// Get News Details by Id
        /// </summary>
        /// <param name="newsid">News id. (Guid)</param>
        /// <returns>Return object of News Class with  all news info.(List<News>)</returns>
        public News getNewsDetailsbyId(Guid newsid)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction. 
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        //Proceed action, to get news info by news id.
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


                }//End Transaction
            }//End Session
        }


        /// <getNewsForHome>
        /// Get News For Home
        /// </summary>
        /// <returns>Return object of News Class with  all news info.(List<News>)</returns>
        public News getNewsForHome()
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction. 
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    News nws = new News();
                    try
                    {
                        //Proceed action to get all home news.
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
                }//End Transaction
            }//End Session
        }
    }
}
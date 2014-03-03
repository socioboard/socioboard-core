using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SocioBoard.Domain;
using SocioBoard.Helper;

namespace SocioBoard.Model
{
    public class NewsLetterRepository : INewsLetterRepository
    {
        public void AddNewsLetter(NewsLetter nl)
        {
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    session.Save(nl);
                    transaction.Commit();
                }
            }
        }


        public int DeleteNewsLetter(Guid nlid)
        {
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        NHibernate.IQuery query = session.CreateQuery("delete from NewsLetter where Id = :adsid")
                                        .SetParameter("adsid", nlid);
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

        public void UpdateNewsLetter(NewsLetter nl)
        {
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        session.CreateQuery("Update NewsLetter set NewsLetterDetail =:NewsLetterDetail,UserId=:UserId,SendDate=:SendDate,SendStatus=:SendStatus where Id = :adsid")
                            .SetParameter("NewsLetterDetail", nl.NewsLetterDetail)
                            .SetParameter("UserId", nl.UserId)
                            .SetParameter("SendDate", nl.SendDate)
                            .SetParameter("SendStatus", nl.SendStatus)
                              .SetParameter("adsid", nl.Id)
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

        public List<NewsLetter> getAllNewsLetter()
        {
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    List<NewsLetter> alstFBAccounts = session.CreateQuery("from NewsLetter").List<NewsLetter>().ToList<NewsLetter>();

                    //List<NewsLetter> alstFBAccounts = new List<NewsLetter>();

                    //foreach (NewsLetter item in query.Enumerable())
                    //{
                    //    alstFBAccounts.Add(item);
                    //}
                    return alstFBAccounts;

                }
            }
        }

        public bool checkNewsLetterExists(string nldetail)
        {
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        NHibernate.IQuery query = session.CreateQuery("from NewsLetter where NewsLetterDetail =:adsdetail");
                        query.SetParameter("adsdetail", nldetail);
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

        public bool checkNewsLetterExists(Guid nlid)
        {
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        NHibernate.IQuery query = session.CreateQuery("from NewsLetter where Id =:adsid");
                        query.SetParameter("adsid", nlid);
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

        public List<NewsLetter> GetAllNewsLetterByDate(DateTime dt)
        {
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    List<NewsLetter> alstFBAccounts = session.CreateQuery("from NewsLetter where SendDate<= :dtime and SendStatus=0").SetParameter("dtime", dt)
                        .List<NewsLetter>().ToList<NewsLetter>();

                    //List<NewsLetter> alstFBAccounts = new List<NewsLetter>();

                    //foreach (NewsLetter item in query.Enumerable())
                    //{
                    //    alstFBAccounts.Add(item);
                    //}
                    return alstFBAccounts;

                }
            }
        }

        public NewsLetter getNewsLetterDetailsbyId(Guid nlid)
        {
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        NHibernate.IQuery query = session.CreateQuery("from NewsLetter where Id=:adsid");

                        query.SetParameter("adsid", nlid);
                        NewsLetter grou = query.UniqueResult<NewsLetter>();
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

        public NewsLetter getNewsLetterDetails(string nlDetail)
        {
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        NHibernate.IQuery query = session.CreateQuery("from NewsLetter where NewsLetterDetail=:nlDetail");
                        query.SetParameter("nlDetail", nlDetail);
                        NewsLetter grou = query.UniqueResult<NewsLetter>();
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
    }
}
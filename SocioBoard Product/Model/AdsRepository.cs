using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SocioBoard.Domain;
using SocioBoard.Helper;
using System.Collections;

namespace SocioBoard.Model
{
    public class AdsRepository :IAdsRepository
    {
        public void AddAds(Ads ads)
        {
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    session.Save(ads);
                    transaction.Commit();
                }
            }
        }


        public int DeleteAds(Guid adsid)
        {
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        NHibernate.IQuery query = session.CreateQuery("delete from Ads where Id = :adsid")
                                        .SetParameter("adsid", adsid);
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

        public void UpdateAds(Ads ads)
        {
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        session.CreateQuery("Update Ads set ImageUrl =:imageurl,Script=:script,ExpiryDate=:expirydate,Status=:status where Id = :adsid")
                            .SetParameter("script",ads.Script)
                            .SetParameter("imageurl", ads.ImageUrl)
                            .SetParameter("status", ads.Status)
                            .SetParameter("id", ads.Id)
                            .SetParameter("expirydate", ads.ExpiryDate)
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

        public List<Ads> getAllAds()
        {
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    List<Ads> alstFBAccounts = session.CreateQuery("from Ads").List<Ads>().ToList<Ads>();
                    return alstFBAccounts;

                    #region oldmethod
                    //List<Ads> alstFBAccounts = new List<Ads>();

                    //foreach (Ads item in query.Enumerable())
                    //{
                    //    alstFBAccounts.Add(item);
                    //} 
                    #endregion
                }
            }
        }

        public bool checkAdsExists(string adsdetail)
        {
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        NHibernate.IQuery query = session.CreateQuery("from Ads where Advertisment =:adsdetail");
                        query.SetParameter("adsdetail", adsdetail);
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

        public bool checkAdsExists(Guid adsid)
        {
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        NHibernate.IQuery query = session.CreateQuery("from Ads where Id =:adsid");
                        query.SetParameter("adsid", adsid);
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

        public Ads getAdsDetails(string adsUrl)
        {
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        NHibernate.IQuery query = session.CreateQuery("from Package where ImageUrl=:adsUrl");
                        query.SetParameter("adsUrl", adsUrl);
                        Ads grou = query.UniqueResult<Ads>();
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

        public Ads getAdsDetailsbyId(Guid adsid)
        {
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        NHibernate.IQuery query = session.CreateQuery("from Ads where Id=:adsid");

                        query.SetParameter("adsid", adsid);
                        Ads grou = query.UniqueResult<Ads>();
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

        public ArrayList getAdsForHome()
        {
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    ArrayList lstAd = new ArrayList();
                    try
                    {
                        var query = session.CreateSQLQuery("Select Id,Advertisment,ImageUrl,Script,Status from Ads Where ExpiryDate>CURDATE() order by EntryDate Desc limit 2");
                       
                        foreach (var item in query.List())
                        {
                            //Array temp = (Array)item;
                            lstAd.Add(item);
                            //ads.Id = Guid.Parse(temp.GetValue(0).ToString());
                            //ads.Advertisment = temp.GetValue(1).ToString();
                            //ads.ImageUrl = temp.GetValue(2).ToString();
                            //ads.Script = temp.GetValue(3).ToString();
                            //ads.Status = bool.Parse(temp.GetValue(2).ToString());
                        }
                    }
                    catch (Exception Err)
                    {
                        Console.Write(Err.StackTrace);
                    }
                    return lstAd;
                }
            }
        }
    }
}
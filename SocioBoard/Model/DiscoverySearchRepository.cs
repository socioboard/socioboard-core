using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SocioBoard.Domain;
using SocioBoard.Helper;
using System.Collections;
using NHibernate;
using NHibernate.Criterion;
using NHibernate.Linq;
namespace SocioBoard.Model
{
    public class DiscoverySearchRepository :IDiscoverySearchRepository
    {

        public void addNewSearchResult(DiscoverySearch dis)
        {
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    session.Save(dis);
                    transaction.Commit();
                }
            }
        }


        public List<string> getAllSearchKeywords(Guid Userid)
        {
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {

                   List<string> searchresults = session.Query<DiscoverySearch>().Where(x => x.UserId == Userid).Select(x => x.SearchKeyword).Distinct().ToList();


                   #region OldCode


                   //ICriteria criteria = session.CreateCriteria(typeof(DiscoverySearch));
                   //criteria.SetProjection(
                   //    Projections.Distinct(Projections.ProjectionList()
                   //        .Add(Projections.Alias(Projections.Property("SearchKeyword"), "SearchKeyword"))));

                   //criteria.SetResultTransformer(
                   //    new NHibernate.Transform.AliasToBeanResultTransformer(typeof(DiscoverySearch)));

                   //IList people = criteria.List(); 
                   #endregion

                    //ArrayList alstsearch = new ArrayList();
                    //foreach (DiscoverySearch item in s)
                    //{
                    //    alstsearch.Add(item.SearchKeyword);
                    //}
                    //return alstsearch;

                   return searchresults;
                }
            }
        }

        public void updateNewSearchResult(DiscoverySearch dis)
        {
            throw new NotImplementedException();
        }

        public void deleteSearchResult(DiscoverySearch dis)
        {
            throw new NotImplementedException();
        }

        public List<DiscoverySearch> getResultsFromKeyword(string keyword)
        {
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        List<DiscoverySearch> lst = session.CreateQuery("from DiscoverySearch where SearchKeyword = :keyword")
                         .SetParameter("keyword", keyword)
                          .List<DiscoverySearch>()
                          .ToList<DiscoverySearch>();

                        #region Oldcode
                        //foreach (DiscoverySearch item in query.Enumerable())
                        //{
                        //    lst.Add(item);
                        //} 
                        #endregion
                        return lst;
                        
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                        return null;
                    }

                }
            }
        }

        public bool isKeywordPresent(string keyword,string messageid)
        {
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        NHibernate.IQuery query = session.CreateQuery("from DiscoverySearch where SearchKeyword = :keyword and MessageId = :messageid");

                        query.SetParameter("keyword", keyword);
                        query.SetParameter("messageid", messageid);
                        List<DiscoverySearch> lst = new List<DiscoverySearch>();

                        foreach (DiscoverySearch item in query.Enumerable())
                        {
                            lst.Add(item);
                        }

                        if (lst.Count == 0)
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

        public bool isResultsPresent(string keyword)
        {
            return false;
        }

        public int DeleteDiscoverySearchByUserid(Guid userid)
        {
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        NHibernate.IQuery query = session.CreateQuery("delete from DiscoverySearch where UserId = :userid")
                                        .SetParameter("userid", userid);
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

    }
}
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
    public class DiscoverySearchRepository : IDiscoverySearchRepository
    {

        /// <addNewSearchResult>
        /// Add a new search result in a database.
        /// </summary>
        /// <param name="dis">Set Values in a DiscoverySearch Class Property and Pass the same Object of DiscoverySearch Class.(Domain.DiscoverySearch)</param>
        public void addNewSearchResult(DiscoverySearch dis)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    //Proceed action to save data.
                    session.Save(dis);
                    transaction.Commit();
                }//End Transaction
            }//End session
        }



        /// <getAllSearchKeywords>
        /// Get all SerchKeywords from database By UserId(Guid).
        /// </summary>
        /// <param name="Userid">id of the DiscoverySearch(Guid)</param>
        /// <returns>List of all SerchKeywords</returns>
        public List<string> getAllSearchKeywords(Guid Userid)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {

                    //Proceed action to get all SearchKeywords By UserId(Guid).
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
                }//End Transaction
            }//End session
        }

        public void updateNewSearchResult(DiscoverySearch dis)
        {
            throw new NotImplementedException();
        }

        public void deleteSearchResult(DiscoverySearch dis)
        {
            throw new NotImplementedException();
        }


        /// <getResultsFromKeyword>
        /// Get result from database by keyword
        /// </summary>
        /// <param name="keyword">Keyword of DiscoverySearch(string)</param>
        /// <returns>List of all results by Keyword</returns>
        public List<DiscoverySearch> getResultsFromKeyword(string keyword)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        //proceed action to get all results by Keyword.
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

                }//End Transaction
            }//End session
        }


        /// <isKeywordPresent>
        /// Check The Keyword is present in Database or not.
        /// </summary>
        /// <param name="keyword">Keyword of DiscoverySearch(SearchKeyword)</param>
        /// <param name="messageid">MessageId of DiscoverySearch(MessageId)</param>
        /// <returns>Return True if keyword is Exist in database otherwise false.</returns>
        public bool isKeywordPresent(string keyword, string messageid)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        //proceed action to check if keyword is present in database or not.
                        // And Set the reuired paremeters to find the specific values.
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

                }//End Transaction
            }//End session
        }

        public bool isResultsPresent(string keyword)
        {
            return false;
        }


        /// <DeleteDiscoverySearchByUserid>
        /// Delete DiscoverySearch from database by userId(Guid)
        /// </summary>
        /// <param name="userid">UserId of DiscoverySearch(Guid)</param>
        /// <returns>Return integer 1 for true 0 for false</returns>
        public int DeleteDiscoverySearchByUserid(Guid userid)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        //Proceed action to delete discoverysearch by userid.
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
                }//End Transaction
            }//End session
        }

    }
}
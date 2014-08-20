using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SocioBoard.Domain;
using SocioBoard.Helper;
using System.Collections;

namespace SocioBoard.Model
{
    public class AdsRepository : IAdsRepository
    {

        /// <AddAds>
        /// Add a new Advertisement in a Database.
        /// </summary>
        /// <param name="ads">Set Values in a Ads Class Property and Pass the Object of Ads Class.(Domain.Ads)</param>
        public void AddAds(Ads ads)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction. 
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    //Process action to save data.
                    session.Save(ads);
                    transaction.Commit();
                }//End using transaction.
            }//End using session.
        }


        /// <DeleteAds>
        /// Delete a Advertisement from a Database by Id.
        /// </summary>
        /// <param name="adsid">Id of the Ads (Guid)</param>
        /// <returns>int</returns>
        public int DeleteAds(Guid adsid)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction. 
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        // Proceed action to Delete Data.
                        // And Set the reuired paremeters to find the specific values.
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
                }//End using transaction.
            }//End using session.
        }


        /// <UpdateAds>
        /// update/change a ImageUrl,Script,ExpiryDate and Status of existing Ads.
        /// </summary>
        /// <param name="ads">Set Values in a Ads Class Property and Pass the Object of Ads Class.(Domain.Ads)</param>
        public void UpdateAds(Ads ads)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction. 
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        // Proceed action to Delete Data.
                        // And Set the reuired paremeters to find the specific values.
                        session.CreateQuery("Update Ads set ImageUrl =:imageurl,Script=:script,ExpiryDate=:expirydate,Status=:status where Id = :adsid")
                            .SetParameter("script", ads.Script)
                            .SetParameter("imageurl", ads.ImageUrl)
                            .SetParameter("status", ads.Status)
                            .SetParameter("adsid", ads.Id)
                            .SetParameter("expirydate", ads.ExpiryDate)
                            .ExecuteUpdate();
                        transaction.Commit();


                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                        // return 0;
                    }
                }//End using transaction.
            }//End using session.
        }


        /// <getAllAds>
        /// get all existing Ads.
        /// </summary>
        /// <returns>return all advertisement from Ads Table of Database</returns>
        public List<Ads> getAllAds()
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction. 
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {

                    //Proceed to get all data.
                    List<Ads> alstFBAccounts = session.CreateQuery("from Ads").List<Ads>().ToList<Ads>();
                    return alstFBAccounts;

                    #region oldmethod
                    //List<Ads> alstFBAccounts = new List<Ads>();

                    //foreach (Ads item in query.Enumerable())
                    //{
                    //    alstFBAccounts.Add(item);
                    //} 
                    #endregion
                }//End using transaction.
            }//End using session.
        }


        /// <checkAdsExists>
        /// check if Ads is Exist or Not by a Advertisement.
        /// </summary>
        /// <param name="adsdetail">Advertisement of Ads (string).</param>
        /// <returns>Return true if result contain value otherwise false. </returns>
        public bool checkAdsExists(string adsdetail)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction. 
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        //Proceed to Check for Ads is Exist or Not.
                        // And Set the reuired paremeters to find the specific values.
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
                }//End using transaction.
            }//End using session. 
        }


        /// <checkAdsExists>
        /// To Check Existing Add bt Id..
        /// </summary>
        /// <param name="adsid">Id of Ads (Guid).</param>
        /// <returns>Return true if result contain value otherwise false.</returns>
        public bool checkAdsExists(Guid adsid)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {

                //After Session creation, start Transaction. 
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        //Proceed to Check, Ads is Exist or Not.
                        //And Set the reuired paremeters to find the specific values.
                        // Returns True and false.
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

                }//End using transaction.
            }//End using session. 
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


        /// <getAdsDetailsbyId>
        /// get ads details by Ads Guid.
        /// </summary>
        /// <param name="adsid">Id of the Ads(Guid)</param>
        /// <returns>Return Unique object of Ads</returns>
        public Ads getAdsDetailsbyId(Guid adsid)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {

                //After Session creation, start Transaction. 
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        // Proceed the action to get data by query.
                        // Where we set the parameter
                        // And return unique result of Ad 
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
                }// End using transaction 
            }// End using session
        }


        /// <getAdsForHome>
        /// Get Latest two Ads for Home Page.
        /// </summary>
        /// <returns>Return Latest two Ads in the Form of Array List.</returns>
        public ArrayList getAdsForHome()
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {

                //After Session creation, start Transaction. 
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    ArrayList lstAd = new ArrayList();
                    try
                    {
                        // Proceed action to get ads 
                        // And return list of ad's 
                        var query = session.CreateSQLQuery("Select Id,Advertisment,ImageUrl,Script,Status from Ads Where ExpiryDate>CURDATE() and Status=1 order by EntryDate Desc limit 2");

                        // Get list from query 
                        foreach (var item in query.List())
                        {
                            // Add the all return value in list from query.
                            //Array temp = (Array)item;
                            lstAd.Add(item);
                            //ads.Id = Guid.Parse(temp.GetValue(0).ToString());
                            //ads.Advertisment = temp.GetValue(1).ToString();
                            //ads.ImageUrl = temp.GetValue(2).ToString();
                            //ads.Script = temp.GetValue(3).ToString();
                            //ads.Status = bool.Parse(temp.GetValue(2).ToString());
                        }// End ForEach
                    }
                    catch (Exception Err)
                    {
                        Console.Write(Err.StackTrace);
                    }
                    return lstAd;
                }// End using Transaction 
            }// End using session
        }
    }
}
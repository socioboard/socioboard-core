using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Api.Socioboard.Helper;
using Domain.Socioboard.Domain;




namespace SocioBoard.Model
{
    public class DropboxAccountRepository
    {

        /// <Add>
        /// Add a new admin in DataBase. 
        /// </summary>
        /// <param name="user">Set Values in a Admin Class Property and Pass the Object of Admin Class (SocioBoard.Domain.Admin).</param>
        public void Add(DropboxAccount _DropboxAccount)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start and open Transaction. 
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    //Proceed action to save data.
                    session.Save(_DropboxAccount);
                    transaction.Commit();

                }//End Using trasaction
            }//End Using session
        }

        /// <getAdsDetailsbyId>
        /// get ads details by Ads Guid.
        /// </summary>
        /// <param name="adsid">Id of the Ads(Guid)</param>
        /// <returns>Return Unique object of Ads</returns>
        public DropboxAccount getDropboxAccountDetailsbyId(Guid Gid)
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
                        NHibernate.IQuery query = session.CreateQuery("from DropboxAccount where UserId=:Userid");
                        query.SetParameter("Userid", Gid);
                        DropboxAccount _DropboxAccount = query.UniqueResult<DropboxAccount>();
                        return _DropboxAccount;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                        return null;
                    }
                }// End using transaction 
            }// End using session
        }


    }
}
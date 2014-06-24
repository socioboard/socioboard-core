using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SocioBoard.Domain;
using SocioBoard.Helper;

namespace SocioBoard.Model
{
    public class FacebookGroupRepository
    {


        /// <checkFacebookGroupExists>
        /// Check Exists Facebook Group By group id and profile Id. 
        /// </summary>
        /// <param name="GroupId">Group id (String)</param>
        /// <param name="ProfileId">Profile Id(String)</param>
        /// <returns></returns>
        public bool checkFacebookGroupExists(string GroupId, string ProfileId)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //Begin session trasaction and opens up.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        //Proceed action, to Check facebook feed
                        NHibernate.IQuery query = session.CreateQuery("from FacebookGroup where ProfileId = :profileid and GroupId = :gid");

                        query.SetParameter("profileid", ProfileId);
                        query.SetParameter("gid", GroupId);
                        var resutl = query.UniqueResult();

                        if (resutl == null)
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


        /// <AddFacebookGroup>
        /// Add New Facebook Group
        /// </summary>
        /// <param name="group">Set Values in a FacebookGroup Class Property and Pass the same Object of FacebookGroup Class as a parameter (SocioBoard.Domain.FacebookGroup).</param>
        public void AddFacebookGroup( FacebookGroup group)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //Begin session trasaction and opens up.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    //Proceed action, to save new facebook Group.
                    session.Save(group);
                    transaction.Commit();
                }//End trsaction
            }//End session
        }

       

    }
}
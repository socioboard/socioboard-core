using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Domain.Socioboard.Domain;
using Api.Socioboard.Helper;
using NHibernate;
using NHibernate.Criterion;
using NHibernate.Linq;

namespace Api.Socioboard.Services
{
    public class discoveryleadsRepository 
    {
        /// <AddDrafts>
        /// Add new draft
        /// </summary>
        /// <param name="d">Set Values in a Draft Class Property and Pass the Object of Draft Class (SocioBoard.Domain.Draft).</param>
        public void LeadKeyword(Domain.Socioboard.Domain.DiscoveryLeads lead)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //Begin session trasaction and opens up.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    //Proceed action , to save data.
                    session.Save(lead);
                    transaction.Commit();
                }//End transaction
            }//End session
        }


        public List<string> GetLeadHistory(Guid UserId)
        {

            using (NHibernate.ISession session = SessionFactory.GetNewSession())

            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {

                    List<string> lstlead = session.Query<Domain.Socioboard.Domain.DiscoveryLeads>().Where(x => x.UserId == UserId).Select(y => y.Keyword).Distinct().ToList();
                    return lstlead;
                }
            
            }
        }



        
    }


}
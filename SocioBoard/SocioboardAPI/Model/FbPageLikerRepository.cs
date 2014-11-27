using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Domain.Socioboard.Domain;
using NHibernate.Transform;
using System.Collections;
using System.Data;
using NHibernate.Linq;
using Api.Socioboard.Helper;

namespace Api.Socioboard.Services
{
    public class FbPageLikerRepository
    {

        /// <addFbPageLiker>
        /// Add new FbPageLiker
        /// </summary>
        /// <param name="fbmsg">Set Values in a FbPageLiker Class Property and Pass the same Object of FbPageLiker Class.(Domain.FbPageLiker)</param>
        public void addFbPageLiker(Domain.Socioboard.Domain.FbPageLiker _FbPageLiker)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    //Proceed action, to save data.
                    session.Save(_FbPageLiker);
                    transaction.Commit();
                }//End Transaction
            }//End session
        }



        public List<Domain.Socioboard.Domain.FbPageLiker> GetLikeByPostId(string postid, Guid Userid)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction. 
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    //Proceed action to get Archive messages
                    // And return list of archive messages.
                    List<Domain.Socioboard.Domain.FbPageLiker> alstFBAccounts = session.CreateQuery("from FbPageLiker where UserId = :userid and PostId=:postid")
                    .SetParameter("postid", postid)
                        .SetParameter("userid", Userid)
                   .List<Domain.Socioboard.Domain.FbPageLiker>()
                   .ToList<Domain.Socioboard.Domain.FbPageLiker>();
                    return alstFBAccounts;

                }//End using transaction  
            }//Using using session
        }

        public bool IsLikeByPostExist(Domain.Socioboard.Domain.FbPageLiker _FbPageLiker)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction. 
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    //Proceed action to get Archive messages
                    // And return list of archive messages.
                    List<Domain.Socioboard.Domain.FbPageLiker> alst = session.CreateQuery("from FbPageLiker where UserId = :userid and PostId=:postid")
                     .SetParameter("postid", _FbPageLiker.PostId)
                     .SetParameter("userid", _FbPageLiker.UserId)
                     .SetParameter("fromid", _FbPageLiker.FromId)
                     .List<Domain.Socioboard.Domain.FbPageLiker>()
                     .ToList<Domain.Socioboard.Domain.FbPageLiker>();
                    if (alst.Count > 0)
                        return true;
                    else
                        return false;

                }//End using transaction  
            }//Using using session
        }

    }
}
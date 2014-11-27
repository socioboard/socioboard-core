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
    public class FbPagePostCommentLikerRepository
    {
        /// <addFbPagePostCommentLiker>
        /// Add new FbPagePostCommentLiker
        /// </summary>
        /// <param name="fbmsg">Set Values in a FbPagePostCommentLiker Class Property and Pass the same Object of FbPagePostCommentLiker Class.(Domain.FbPagePostCommentLiker)</param>
        public void addFbPagePostCommentLiker(Domain.Socioboard.Domain.FbPagePostCommentLiker _FbPagePostCommentLiker)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    //Proceed action, to save data.
                    session.Save(_FbPagePostCommentLiker);
                    transaction.Commit();
                }//End Transaction
            }//End session
        }

        public bool IsFbPagePostCommentLikerExist(Domain.Socioboard.Domain.FbPagePostCommentLiker _FbPagePostCommentLiker)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    //Proceed action, to save data.
                    try
                    {
                        //Proceed action to, get all wall post of Facebook User.
                        List<Domain.Socioboard.Domain.FbPagePostCommentLiker> alst = session.CreateQuery("from FbPagePostCommentLiker where UserId = :userid and FromId = :fromid and CommentId = :commentid")
                         .SetParameter("userid", _FbPagePostCommentLiker.UserId)
                         .SetParameter("fromid", _FbPagePostCommentLiker.FromId)
                         .SetParameter("commentid", _FbPagePostCommentLiker.CommentId)
                         .List<Domain.Socioboard.Domain.FbPagePostCommentLiker>()
                         .ToList<Domain.Socioboard.Domain.FbPagePostCommentLiker>();
                        if (alst.Count > 0)
                            return true;
                        else
                            return false;

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                        return true;
                    }
                }//End Transaction
            }//End session
        }
    }
}
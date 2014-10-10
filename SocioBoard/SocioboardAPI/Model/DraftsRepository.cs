using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Domain.Socioboard.Domain;
using Api.Socioboard.Helper;

namespace Api.Socioboard.Services
{
    public class DraftsRepository : IDraftsRepository
    {
        /// <AddDrafts>
        /// Add new draft
        /// </summary>
        /// <param name="d">Set Values in a Draft Class Property and Pass the Object of Draft Class (SocioBoard.Domain.Draft).</param>
        public void AddDrafts(Domain.Socioboard.Domain.Drafts d)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //Begin session trasaction and opens up.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    //Proceed action , to save data.
                    session.Save(d);
                    transaction.Commit();
                }//End transaction
            }//End session
        }


        /// <DeleteDrafts>
        /// Delete Draft
        /// </summary>
        /// <param name="d">Set Values in a Draft Class Property and Pass the Object of Draft Class (SocioBoard.Domain.Draft).</param>
        /// <returns>Return 1 for successfully deleted or 0 for failed.</returns>
        public int DeleteDrafts(Domain.Socioboard.Domain.Drafts d)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //Begin session trasaction and opens up.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        //Proceed action , 
                        //To delete draft, by user id and message.
                        NHibernate.IQuery query = session.CreateQuery("delete from Drafts where UserId = :userid and Message =:mess")
                                        .SetParameter("userid", d.UserId)
                                        .SetParameter("mess", d.Message);
                        int isUpdated = query.ExecuteUpdate();
                        transaction.Commit();
                        return isUpdated;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                        return 0;
                    }
                }//End transaction
            }//End session
        }


        /// <DeleteDrafts>
        /// Delete Draft
        /// </summary>
        /// <param name="d">Set Values in a Draft Class Property and Pass the same Object of Draft Class (SocioBoard.Domain.Draft).</param>
        /// <returns>Return 1 for successfully deleted or 0 for failed.</returns>
        public int DeleteDrafts(Guid Id)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //Begin session trasaction and opens up.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        //Proceed action , 
                        //To delete draft, by Draft id.
                        NHibernate.IQuery query = session.CreateQuery("delete from Drafts where Id = :id")
                                        .SetParameter("id", Id);

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
            }//End Session
        }


        /// <UpdateDrafts>
        /// Update existing Draft
        /// </summary>
        /// <param name="d">Set Values in a Draft Class Property and Pass the same Object of Draft Class (SocioBoard.Domain.Draft).</param>
        public void UpdateDrafts(Domain.Socioboard.Domain.Drafts d)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //Begin session trasaction and opens up.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        session.CreateQuery("Update Drafts set Message =:mess,ModifiedDate =:mod where UserId =:userid")
                            .SetParameter("mess", d.Message)
                            .SetParameter("mod", d.ModifiedDate)
                            .SetParameter("userid", d.UserId)
                            .ExecuteUpdate();
                        transaction.Commit();


                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                        // return 0;
                    }
                }//End Trasaction
            }//End session
        }


        /// <UpdateDrafts>
        /// Update existing Draft message by draft id(Guid).
        /// </summary>
        /// <param name="Id">Guid of draft</param>
        /// <param name="message">Message of Draft</param>
        public void UpdateDrafts(Guid Id, string message)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //Begin session trasaction and opens up.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    //Proceed action, to update existing Draft message by draft id.
                    session.CreateQuery("Update Drafts set Message =:mess,ModifiedDate =:mod where Id =:id")
                        .SetParameter("mess", message)
                        .SetParameter("id", Id)
                        .SetParameter("mod", DateTime.Now)
                        .ExecuteUpdate();
                    transaction.Commit();


                }//End Transaction
            }//End Session

        }


        /// <getAllDrafts>
        /// Get all Draft 
        /// </summary>
        /// <param name="UserId">Pass the Guid of user.</param>
        /// <returns>List of Draft messages.</returns>
        public List<Domain.Socioboard.Domain.Drafts> getAllDrafts(Guid groupid)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //Begin session trasaction and opens up.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        //Proceed action, to check draft is exist by user id.
                        List<Domain.Socioboard.Domain.Drafts> lst = session.CreateQuery("From Drafts where GroupId = :groupid")
                                           .SetParameter("groupid", groupid)
                                           .List<Domain.Socioboard.Domain.Drafts>()
                                           .ToList<Domain.Socioboard.Domain.Drafts>();
                        #region oldcode
                        //List<Drafts> lst = new List<Drafts>();
                        //foreach (Drafts item in query.Enumerable<Drafts>())
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
                }//End Trasaction
            }//End session

        }


        /// <IsDraftsMessageExist>
        /// Check the draft message is exist or not.
        /// </summary>
        /// <param name="UserId">Guid of user</param>
        /// <param name="message">Draft message</param>
        /// <returns>It is return bool value. If draft is exist it returns true otherwise false </returns>
        public bool IsDraftsMessageExist(Guid UserId, string message)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //Begin session trasaction and opens up.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        //Proceed action , to check draft by user id and message.
                        NHibernate.IQuery query = session.CreateQuery("From Drafts where UserId = :userid and Message =:mess");
                        query.SetParameter("userid", UserId);
                        query.SetParameter("mess", message);
                        List<Domain.Socioboard.Domain.Drafts> lst = new List<Domain.Socioboard.Domain.Drafts>();
                        foreach (Domain.Socioboard.Domain.Drafts item in query.Enumerable<Domain.Socioboard.Domain.Drafts>())
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
                }//End trasaction 
            }//End session
        }


        /// <DeleteDraftsByUserid>
        /// Delete all draft messages by User id.
        /// </summary>
        /// <param name="userid">User id (Guid)</param>
        /// <returns>1 for successfully deleted and 0 for failed deletion.</returns>
        public int DeleteDraftsByUserid(Guid userid)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //Begin session trasaction and opens up.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        //Proceed action, to delete user draft. 
                        NHibernate.IQuery query = session.CreateQuery("delete from Drafts where UserId = :userid")
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
                }//End transaction
            }//End Session
        }

        /// <getAllDrafts>
        /// Get all Draft 
        /// </summary>
        /// <param name="UserId">Pass the Guid of user.</param>
        /// <returns>List of Draft messages.</returns>
        public List<Domain.Socioboard.Domain.Drafts> GetDraftMessageByUserIdAndGroupId(Guid groupid, Guid UserId)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //Begin session trasaction and opens up.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        //Proceed action, to check draft is exist by user id.
                        List<Domain.Socioboard.Domain.Drafts> lst = session.CreateQuery("From Drafts where GroupId = :groupid and UserId=:userid")
                                           .SetParameter("groupid", groupid)
                                           .SetParameter("userid", UserId)
                                           .List<Domain.Socioboard.Domain.Drafts>()
                                           .ToList<Domain.Socioboard.Domain.Drafts>();
                        return lst;

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                        return null;
                    }
                }//End Trasaction
            }//End session

        }

        /// <UpdateDrafts>
        /// Update existing Draft message by draft id(Guid).
        /// </summary>
        /// <param name="Id">Guid of draft</param>
        /// <param name="message">Message of Draft</param>
        public void UpdateDraftsMessage(Guid draftid, Guid userid, Guid groupid, string message)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //Begin session trasaction and opens up.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    //Proceed action, to update existing Draft message by draft id.
                    session.CreateQuery("Update Drafts set Message =:mess,ModifiedDate =:mod where Id =:id and UserId=:userid and GroupId=:groupid")
                        .SetParameter("userid", userid)
                        .SetParameter("groupid", groupid)
                        .SetParameter("mess", message)
                        .SetParameter("id", draftid)
                        .SetParameter("mod", DateTime.Now)
                        .ExecuteUpdate();
                    transaction.Commit();


                }//End Transaction
            }//End Session

        }

    }


}
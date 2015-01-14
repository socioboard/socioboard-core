using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Api.Socioboard.Helper;

namespace Api.Socioboard.Model
{
    public class TicketAssigneeStatusRepository
    {
        /// <AddDrafts>
        /// Add new draft
        /// </summary>
        /// <param name="d">Set Values in a Draft Class Property and Pass the Object of Draft Class (SocioBoard.Domain.Draft).</param>
        public void Add(Domain.Socioboard.Domain.TicketAssigneeStatus _TicketAssigneeStatus)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //Begin session trasaction and opens up.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    //Proceed action , to save data.
                    session.Save(_TicketAssigneeStatus);
                    transaction.Commit();
                }//End transaction
            }//End session
        }

        public bool IsAssigneeUserIdExist(Guid AssigneeUserId)
        {
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        NHibernate.IQuery query = session.CreateQuery("from  TicketAssigneeStatus u where u.AssigneeUserId = : AssigneeUserId");
                        query.SetParameter("AssigneeUserId", AssigneeUserId);
                        var result = query.UniqueResult();
                        if (result == null)
                        {
                            return false;
                        }
                        else
                        {
                            return true;
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                        return true;
                    }
                }
            }
        }

        //public List<Domain.Socioboard.Domain.TicketAssigneeStatus> getAllAssignedMembers()
        //{
        //    //Creates a database connection and opens up a session
        //    using (NHibernate.ISession session = SessionFactory.GetNewSession())
        //    {
        //        //After Session creation, start Transaction.
        //        using (NHibernate.ITransaction transaction = session.BeginTransaction())
        //        {
        //            //Proceed action, to get details of team by team id.
        //            List<Domain.Socioboard.Domain.TicketAssigneeStatus> lstAllmembersofAssigned = session.CreateQuery("from TicketAssigneeStatus")

        //            .List<Domain.Socioboard.Domain.TicketAssigneeStatus>()
        //            .ToList<Domain.Socioboard.Domain.TicketAssigneeStatus>();

        //            return lstAllmembersofAssigned;

        //        }//End Transaction
        //    }//End Session
        //}

        public int updateAssigneeCount(Guid AssigneeUserId, int AssignedTicketCount)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {

                    try
                    {
                        //Update status
                        int i = session.CreateQuery("Update TicketAssigneeStatus set AssignedTicketCount = :AssignedTicketCount  where AssigneeUserId = :AssigneeUserId")
                          .SetParameter("AssigneeUserId", AssigneeUserId)
                          .SetParameter("AssignedTicketCount", AssignedTicketCount)
                          .ExecuteUpdate();
                        transaction.Commit();
                        return i;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                        return 0;

                    }
                }//End Transaction
            }//End Session
        }

        public Domain.Socioboard.Domain.TicketAssigneeStatus getAssignedMembersByUserId(Guid AssigneeUserId)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        //Proceed action, to get details of team by team id.
                        NHibernate.IQuery query = session.CreateQuery("from TicketAssigneeStatus where AssigneeUserId =: AssigneeUserId");
                        query.SetParameter("AssigneeUserId", AssigneeUserId);
                        var result = query.UniqueResult();
                        return (Domain.Socioboard.Domain.TicketAssigneeStatus)result;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                        return null;
                    }
                }//End Transaction
            }//End Session
        }

        public List<Domain.Socioboard.Domain.TicketAssigneeStatus> getAllAssignedMembers()
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    //Proceed action, to get details of team by team id.
                    List<Domain.Socioboard.Domain.TicketAssigneeStatus> lstAllmembersofAssigned = session.CreateQuery("from TicketAssigneeStatus order by AssignedTicketCount")

                    .List<Domain.Socioboard.Domain.TicketAssigneeStatus>()
                    .ToList<Domain.Socioboard.Domain.TicketAssigneeStatus>();

                    return lstAllmembersofAssigned;

                }//End Transaction
            }//End Session
        }
    }
}
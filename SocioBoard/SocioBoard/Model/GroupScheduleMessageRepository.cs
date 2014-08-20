using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SocioBoard.Domain;
using SocioBoard.Helper;
using SocioBoard.Admin.Scheduler;
using log4net;

namespace SocioBoard.Model
{
    public class GroupScheduleMessageRepository
    {
        ILog logger = LogManager.GetLogger(typeof(GroupScheduleMessageRepository));


        public void addNewGroupMessage(GroupScheduleMessage grpschmesg)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    //Proceed action, to save new data.
                    session.Save(grpschmesg);
                    transaction.Commit();
                }//End Transaction
            }//End Session
        }

        public GroupScheduleMessage GetScheduleMessageId(Guid ScheduleMessageId)
        {
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        NHibernate.IQuery query = session.CreateQuery("from GroupScheduleMessage  where ScheduleMessageId = : schedulemessageid");
                        query.SetParameter("schedulemessageid", ScheduleMessageId);
                        
                        GroupScheduleMessage result = (GroupScheduleMessage)query.UniqueResult();
                        return result;

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                        return null;
                    }



                }
            }
        }
    }
}
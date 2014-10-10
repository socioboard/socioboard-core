using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Api.Socioboard.Helper;
using Domain.Socioboard.Domain;
namespace Api.Socioboard.Model
{
    public class GroupScheduleMessageRepository
    {

        public void AddGroupScheduleMessage(Domain.Socioboard.Domain.GroupScheduleMessage _GroupScheduleMessage)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    //Proceed action to save data.
                    session.Save(_GroupScheduleMessage);
                    transaction.Commit();
                }//End Transaction
            }//End Session
        }



    }
}
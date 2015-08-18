using Api.Socioboard.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NHibernate.Linq;

namespace Api.Socioboard.Model
{
    public class TwitterFollowerNamesRepository
    {
        public void addTwitterAccountFollower(Domain.Socioboard.Domain.TwitterFollowerNames _TwitterFollowerNames)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        //Proceed action, to save new data.
                        session.Save(_TwitterFollowerNames);
                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                    }
                }//End Transaction
            }//End Session
        }

        public bool IsFollowerExist(string profileid, string followerid)
        {
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                try
                {
                    bool isexist = session.Query<Domain.Socioboard.Domain.TwitterFollowerNames>()
                                                 .Any(x => x.TwitterProfileId == profileid && x.FollowerId == followerid);
                    return isexist;
                }
                catch (Exception ex)
                {
                    return true;
                }
            }
        }

    }
}
using Api.Socioboard.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Domain.Socioboard.Domain;


namespace Api.Socioboard.Services
{
    public class YoutubeChannelRepository
    {
        public static void Add(Domain.Socioboard.Domain.YoutubeChannel YoutubeChannel)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start and open Transaction. 
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    //Proceed action to save data.
                    session.Save(YoutubeChannel);
                    transaction.Commit();

                }//End Using trasaction
            }//End Using session
        }

        public Domain.Socioboard.Domain.YoutubeChannel getYoutubeChannelDetailsById(string youtubeId)
        {

            Domain.Socioboard.Domain.YoutubeChannel result = new Domain.Socioboard.Domain.YoutubeChannel();
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {

                    // proceed action, to get all Facebook Account of User by UserId(Guid) and FbUserId(string).
                    List<Domain.Socioboard.Domain.YoutubeChannel> objlstfb = session.CreateQuery("from YoutubeChannel where Googleplususerid = :youtubeId ")
                            .SetParameter("youtubeId", youtubeId)
                       .List<Domain.Socioboard.Domain.YoutubeChannel>().ToList<Domain.Socioboard.Domain.YoutubeChannel>();
                    if (objlstfb.Count > 0)
                    {
                        result = objlstfb[0];
                    }
                    return result;
                }//End Transaction
            }//End session
        }

        public Domain.Socioboard.Domain.YoutubeChannel getYoutubeChannelDetailsById(string youtubeId, Guid userid)
        {

            Domain.Socioboard.Domain.YoutubeChannel result = new Domain.Socioboard.Domain.YoutubeChannel();
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {

                    // proceed action, to get all Facebook Account of User by UserId(Guid) and FbUserId(string).
                    List<Domain.Socioboard.Domain.YoutubeChannel> objlstfb = session.CreateQuery("from YoutubeChannel where Googleplususerid = :youtubeId and UserId=:userid ")
                            .SetParameter("youtubeId", youtubeId)
                            .SetParameter("userid", userid)
                       .List<Domain.Socioboard.Domain.YoutubeChannel>().ToList<Domain.Socioboard.Domain.YoutubeChannel>();
                    if (objlstfb.Count > 0)
                    {
                        result = objlstfb[0];
                    }
                    return result;
                }//End Transaction
            }//End session
        }

        public int DeleteProfileDataByUserid(string ProfileId)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        //Proceed action, to delete records of user.
                        NHibernate.IQuery query = session.CreateQuery("delete from YoutubeChannel where Googleplususerid = :ProfileId")
                                        .SetParameter("ProfileId", ProfileId);
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

        public int DeleteProfileDataByUserid(string ProfileId, Guid userid)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        //Proceed action, to delete records of user.
                        NHibernate.IQuery query = session.CreateQuery("delete from YoutubeChannel where Googleplususerid = :ProfileId and UserId=:userid")
                                        .SetParameter("userid", userid)
                        .SetParameter("ProfileId", ProfileId);
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

        public bool checkYoutubeChannelExists(string channelid, Guid Userid)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //Begin session trasaction and opens up.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        //Proceed action, to get all data of facebook feed by user feed id and User id(Guid).
                        NHibernate.IQuery query = session.CreateQuery("from YoutubeChannel where UserId = :userid and ChannelId = :channelid");
                        query.SetParameter("userid", Userid);
                        query.SetParameter("channelid", channelid);
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
            }//End session
        }


    }
}
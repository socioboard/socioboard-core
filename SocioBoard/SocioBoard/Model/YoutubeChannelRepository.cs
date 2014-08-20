using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SocioBoard.Domain;
using SocioBoard.Helper;

namespace SocioBoard.Model
{
    public class YoutubeChannelRepository
    {
        public static void Add(YoutubeChannel YoutubeChannel)
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

        public YoutubeChannel getYoutubeChannelDetailsById(string youtubeId)
        {

            YoutubeChannel result = new YoutubeChannel();
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {

                    // proceed action, to get all Facebook Account of User by UserId(Guid) and FbUserId(string).
                    List<YoutubeChannel> objlstfb = session.CreateQuery("from YoutubeChannel where Googleplususerid = :youtubeId ")
                            .SetParameter("youtubeId", youtubeId)
                       .List<YoutubeChannel>().ToList<YoutubeChannel>();
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




    }
}
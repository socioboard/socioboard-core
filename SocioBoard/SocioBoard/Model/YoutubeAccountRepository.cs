using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SocioBoard.Helper;
using SocioBoard.Domain;

namespace SocioBoard.Model
{
    public class YoutubeAccountRepository
    {
        public static void Add(YoutubeAccount YoutubeAccount)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start and open Transaction. 
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    //Proceed action to save data.
                    try
                    {
                        session.Save(YoutubeAccount);
                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {

                        Console.WriteLine(ex.Message);
                    }

                }//End Using trasaction
            }//End Using session
        }


        public bool checkYoutubeUserExists(YoutubeAccount YoutubeAccount)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        //Proceed action, to Check if FacebookUser is Exist in database or not by UserId and FbuserId.
                        // And Set the reuired paremeters to find the specific values.
                        NHibernate.IQuery query = session.CreateQuery("from YoutubeAccount where YtUserId = :uidd and UserId = :Userid");
                        query.SetParameter("uidd", YoutubeAccount.Ytuserid);
                        query.SetParameter("Userid", YoutubeAccount.UserId);
                        var result = query.UniqueResult();

                        if (result == null)
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

        public YoutubeAccount getYoutubeAccountDetailsById(string youtubeId)
        {

            YoutubeAccount result = new YoutubeAccount();
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {

                    // proceed action, to get all Facebook Account of User by UserId(Guid) and FbUserId(string).
                    List<YoutubeAccount> objlstfb = session.CreateQuery("from YoutubeAccount where YtUserId = :Fbuserid ")
                            .SetParameter("Fbuserid", youtubeId)
                       .List<YoutubeAccount>().ToList<YoutubeAccount>();
                    if (objlstfb.Count > 0)
                    {
                        result = objlstfb[0];
                    }
                    return result;
                }//End Transaction
            }//End session
        }


        public int deleteYoutubeUser(Guid userid, string youtubeuserid)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        //Proceed action, to delete existing account by user id and twitter id.
                        NHibernate.IQuery query = session.CreateQuery("delete from YoutubeAccount where YtUserId = :youtubeuserid and UserId=:Userid")
                                        .SetParameter("youtubeuserid", youtubeuserid)
                                        .SetParameter("Userid", userid);
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
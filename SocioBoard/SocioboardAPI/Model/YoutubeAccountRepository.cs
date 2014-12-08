using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Domain.Socioboard.Domain;
using Api.Socioboard.Helper;
using System.Collections;

namespace Api.Socioboard.Services
{
    public class YoutubeAccountRepository
    {
        public static void Add(Domain.Socioboard.Domain.YoutubeAccount YoutubeAccount)
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


        public bool checkYoutubeUserExists(Domain.Socioboard.Domain.YoutubeAccount YoutubeAccount)
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
                        NHibernate.IQuery query = session.CreateQuery("from YoutubeAccount where YtUserId = :uidd and YtUserName = :tbuname and UserId=:userid");
                        query.SetParameter("uidd", YoutubeAccount.Ytuserid);
                        query.SetParameter("tbuname", YoutubeAccount.Ytusername);
                        query.SetParameter("userid", YoutubeAccount.UserId);
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


        public bool checkTubmlrUserExists(Domain.Socioboard.Domain.YoutubeAccount objTumblrAccount)
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
                        NHibernate.IQuery query = session.CreateQuery("from YoutubeAccount where UserId = :uidd and YtUserId = :tbuname");
                        query.SetParameter("uidd", objTumblrAccount.UserId);
                        query.SetParameter("tbuname", objTumblrAccount.Ytuserid);
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


        public Domain.Socioboard.Domain.YoutubeAccount getYoutubeAccountDetailsById(string youtubeId)
        {

            Domain.Socioboard.Domain.YoutubeAccount result = new Domain.Socioboard.Domain.YoutubeAccount();
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {

                    // proceed action, to get all Facebook Account of User by UserId(Guid) and FbUserId(string).
                    List<Domain.Socioboard.Domain.YoutubeAccount> objlstfb = session.CreateQuery("from YoutubeAccount where YtUserId = :Fbuserid ")
                            .SetParameter("Fbuserid", youtubeId)
                       .List<Domain.Socioboard.Domain.YoutubeAccount>().ToList<Domain.Socioboard.Domain.YoutubeAccount>();
                    if (objlstfb.Count > 0)
                    {
                        result = objlstfb[0];
                    }
                    return result;
                }//End Transaction
            }//End session
        }


        public Domain.Socioboard.Domain.YoutubeAccount getYoutubeAccountDetailsById(string youtubeId,Guid userid)
        {

            Domain.Socioboard.Domain.YoutubeAccount result = new Domain.Socioboard.Domain.YoutubeAccount();
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {

                    // proceed action, to get all Facebook Account of User by UserId(Guid) and FbUserId(string).
                    List<Domain.Socioboard.Domain.YoutubeAccount> objlstfb = session.CreateQuery("from YoutubeAccount where YtUserId = :Fbuserid and UserId=:userid")
                            .SetParameter("Fbuserid", youtubeId)
                            .SetParameter("userid", userid)
                       .List<Domain.Socioboard.Domain.YoutubeAccount>().ToList<Domain.Socioboard.Domain.YoutubeAccount>();
                    if (objlstfb.Count > 0)
                    {
                        result = objlstfb[0];
                    }
                    return result;
                }//End Transaction
            }//End session
        }

        public List<Domain.Socioboard.Domain.YoutubeAccount> getYoutubeAccountDetailsById(Guid userId)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, open up a Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        //Proceed action, to get all twitter messages of profile by profile id
                        // And order by Entry date.
                        List<Domain.Socioboard.Domain.YoutubeAccount> lstmsg = session.CreateQuery("from YoutubeAccount where UserId=:userId")
                        .SetParameter("userId", userId)
                        .List<Domain.Socioboard.Domain.YoutubeAccount>()
                        .ToList<Domain.Socioboard.Domain.YoutubeAccount>();
                        return lstmsg;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                        return null;
                    }

                }//End Transaction
            }//End Session
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
                        NHibernate.IQuery query = session.CreateQuery("delete from YoutubeAccount where YtUserId = :youtubeuserid and UserId = :userid")
                                        .SetParameter("youtubeuserid", youtubeuserid)
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
                }//End Transaction
            }//End Session
        }

        public List<Domain.Socioboard.Domain.YoutubeAccount> getAllYoutubeAccounts()
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction. 
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    //Proceed action, to get all linkedin accounts.
                    List<Domain.Socioboard.Domain.YoutubeAccount> lstmsg = session.CreateQuery("from YoutubeAccount")
                         .List<Domain.Socioboard.Domain.YoutubeAccount>()
                         .ToList<Domain.Socioboard.Domain.YoutubeAccount>();
                    return lstmsg;

                }//End Transaction
            }//End Session
        }

        public void updateYoutubeUser(Domain.Socioboard.Domain.YoutubeAccount youtubeAccount)
        {
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        session.CreateQuery("Update YoutubeAccount set YtUserName =:Ytusername,YtUserId=:Ytuserid,YtProfileImage=:Ytprofileimage,AccessToken=:Accesstoken,RefreshToken=:Refreshtoken,IsActive=:IsActive,EmailId=:Emailid,EntryDate=:Entrydate,UserId=:UserId where YtUserId =:Ytuserid and UserId = :UserId")
                            .SetParameter("Ytusername", youtubeAccount.Ytusername)
                            .SetParameter("Ytuserid", youtubeAccount.Ytuserid)
                            .SetParameter("Accesstoken", youtubeAccount.Accesstoken)
                            .SetParameter("Refreshtoken", youtubeAccount.Refreshtoken)
                            .SetParameter("UserId", youtubeAccount.UserId)
                            .SetParameter("Emailid", youtubeAccount.Emailid)
                            .SetParameter("Entrydate", youtubeAccount.Entrydate)
                            .SetParameter("Ytprofileimage", youtubeAccount.Ytprofileimage)
                            .SetParameter("IsActive", "1")
                            .ExecuteUpdate();
                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                        // return 0;
                    }
                }
            }
        }


    }
}
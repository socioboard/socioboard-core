using Api.Socioboard.Helper;
using Domain.Socioboard.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NHibernate.Linq;

namespace Api.Socioboard.Model
{
    public class ShareathonRepository
    {
        public bool AddShareathon(Shareathon shareathon)
        {
            bool IsSuccess = false;
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    //Proceed action, to Save data.
                    try
                    {
                        session.Save(shareathon);
                        transaction.Commit();
                        IsSuccess = true;
                        return IsSuccess;
                    }
                    catch (Exception ex)
                    {

                        Console.WriteLine(ex.StackTrace);
                        return IsSuccess;
                    }

                }//End Transaction
            }//End Session
        }

        public bool IsShareathonExist(Guid Userid, Guid Facebookaccountid, string Facebookpageid) 
        {
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                bool ret = session.Query<Shareathon>().Any(t => t.Userid == Userid && t.Facebookaccountid == Facebookaccountid && t.Facebookpageid.Contains(Facebookpageid));
                //List<Shareathon> objlstfb = session.CreateQuery("from Shareathon where UserId = :UserId and FacebookAccountId =: FacebookAccountId and FacebookPageId =: Facebookpageid and IsHidden = false ")
                //        .SetParameter("UserId", Userid)
                //        .SetParameter("FacebookAccountId", Facebookaccountid)
                //        .SetParameter("Facebookpageid", Facebookpageid)
                //   .List<Shareathon>().ToList<Shareathon>();

                //if (objlstfb.Count() > 0)
                //{
                //    return true;
                //}
                //else 
                //{
                //    return false;
                //}
                return ret;
            }//End session
        }

        public List<Shareathon> getUserShareathon(Guid UserId)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                    List<Shareathon> objlstfb = session.CreateQuery("from Shareathon where UserId = :UserId and IsHidden = false ")
                            .SetParameter("UserId", UserId)
                       .List<Shareathon>().ToList<Shareathon>();
                 
                    return objlstfb;
            }//End session
        }


        public List<Shareathon> getShareathons()
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                List<Shareathon> objlstfb = session.CreateQuery("from Shareathon where IsHidden = false ")
                   .List<Shareathon>().ToList<Shareathon>();

                return objlstfb;
            }//End session
        }

        public Domain.Socioboard.Domain.Shareathon getShareathon(Guid Id)
        {
            Domain.Socioboard.Domain.Shareathon shareathon = null;
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        NHibernate.IQuery query = session.CreateQuery("from Shareathon where Id = :ID");
                        query.SetParameter("ID", Id);
                        Domain.Socioboard.Domain.Shareathon result = (Domain.Socioboard.Domain.Shareathon)query.UniqueResult();
                        shareathon = result;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                    }
                }//End Transaction
            }//End session
            return shareathon;
        }
       



        public Domain.Socioboard.Domain.FacebookAccount getFbAccount(Guid Id)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {

                        //Proceed action to, Get User's all Detail from FacebookAccount by FbUserId.
                        NHibernate.IQuery query = session.CreateQuery("from FacebookAccount where Id = :fbuserid");

                        query.SetParameter("fbuserid", Id);
                        List<Domain.Socioboard.Domain.FacebookAccount> lst = new List<Domain.Socioboard.Domain.FacebookAccount>();

                        foreach (Domain.Socioboard.Domain.FacebookAccount item in query.Enumerable())
                        {

                            lst.Add(item);
                            break;
                        }
                        Domain.Socioboard.Domain.FacebookAccount fbacc = lst[0];
                        return fbacc;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                        return null;
                    }

                }//End Transaction
            }//End session
        }


        public bool updateShareathon(Domain.Socioboard.Domain.Shareathon shareathon)
        {
            bool isUpdate = false;
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        // Proceed action to Update Data.
                        // And Set the reuired paremeters to find the specific values.
                        session.CreateQuery("Update Shareathon set Facebookaccountid=:Facebookaccountid,Facebookpageid=:Facebookpageid,Timeintervalminutes=:Timeintervalminutes where Id = :Id")
                            .SetParameter("Facebookaccountid", shareathon.Facebookaccountid)
                            .SetParameter("Facebookpageid", shareathon.Facebookpageid)
                            .SetParameter("Timeintervalminutes", shareathon.Timeintervalminutes)
                            .SetParameter("Id", shareathon.Id)
                            .ExecuteUpdate();
                        transaction.Commit();
                        isUpdate = true;
                        return isUpdate;

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                        return isUpdate;
                    }
                }//End Transaction
            }//End session
        }
       

        public int DeletePageShareathon(Guid Id)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        //Proceed action, to delete a FacebookAccount by FbUserId and UserId.
                        NHibernate.IQuery query = session.CreateQuery("delete from Shareathon where Id = :Id")
                                        .SetParameter("Id", Id);

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
            }//End session
        }

        public bool IsPostExist(string Facebookaccountid, string Facebookpageid, string PostId)
        {
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                bool ret = session.Query<SharethonPost>().Any(t => t.Facebookaccountid == Facebookaccountid && t.Facebookpageid == Facebookpageid && t.PostId == PostId);
                return ret;
                //List<SharethonPost> objlstfb = session.CreateQuery("from SharethonPost where Facebookpageid = :Facebookpageid and Facebookaccountid =: Facebookaccountid and PostId =: PostId ")
                //        .SetParameter("Facebookaccountid", Facebookaccountid)
                //        .SetParameter("Facebookpageid", Facebookpageid)
                //        .SetParameter("PostId", PostId)
                //   .List<SharethonPost>().ToList<SharethonPost>();

                //if (objlstfb.Count() > 0)
                //{
                //    return true;
                //}
                //else
                //{
                //    return false;
                //}
            }//End session
        }
        public bool AddShareathonPost(SharethonPost shareathon)
        {
            bool IsSuccess = false;
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    //Proceed action, to Save data.
                    try
                    {
                        session.Save(shareathon);
                        transaction.Commit();
                        IsSuccess = true;
                        return IsSuccess;
                    }
                    catch (Exception ex)
                    {

                        Console.WriteLine(ex.StackTrace);
                        return IsSuccess;
                    }

                }//End Transaction
            }//End Session
        }

        public int deletepageshraethonpost()
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        DateTime dt = DateTime.UtcNow.Date.AddDays(-1);
                        //Proceed action, to delete a FacebookAccount by FbUserId and UserId.
                        //NHibernate.IQuery query = session.CreateQuery("delete from SharethonGroupPost where PostedTime like'%Id%'")
                        //                .SetParameter("Id", dt);

                        NHibernate.IQuery query = session.CreateQuery("Delete from SharethonPost where PostedTime < DATE_ADD(UTC_TIMESTAMP(),INTERVAL -1 DAY)");
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
            }//End session
        }

    }
}
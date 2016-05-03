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
        private FacebookAccountRepository _FacebookAccountRepository = new FacebookAccountRepository();
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

        public bool IsShareathonExist(Guid Userid, string Facebookaccountid, string Facebookpageid) 
        {
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                bool ret = session.Query<Shareathon>().Any(t => t.Userid == Userid && t.Facebookaccountid == Facebookaccountid && t.Facebookpageid.Contains(Facebookpageid));
               
                return ret;
            }//End session
        }


        public bool IsShareathonExistFbUserId(Guid Userid, string Facebookaccountid)
        {
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                bool ret = session.Query<Shareathon>().Any(t => t.Userid == Userid && (t.Facebookaccountid == Facebookaccountid ||t.Facebookpageid.Contains(Facebookaccountid)));

                return ret;
            }//End session
        }


        public bool IsShareathonExistById(Guid Id)
        {
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                bool ret = session.Query<Shareathon>().Any(t => t.Id==Id);
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
                List<Shareathon> objlstfb = session.CreateQuery("from Shareathon where UserId = :UserId and IsHidden = false and FacebookStatus = :FacebookStatus")
                            .SetParameter("UserId", UserId).SetParameter("FacebookStatus",1)
                       .List<Shareathon>().ToList<Shareathon>();
                 
                    return objlstfb;
            }//End session
        }


        public List<Shareathon> getShareathons()
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                List<Shareathon> objlstfb = session.CreateQuery("from Shareathon where IsHidden = false and FacebookStatus = :FacebookStatus")
                    .SetParameter("FacebookStatus", 1)
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
                        NHibernate.IQuery query = session.CreateQuery("from Shareathon where Id = :ID and FacebookStatus = :FacebookStatus");
                        query.SetParameter("ID", Id).SetParameter("FacebookStatus", 1);
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


        public Domain.Socioboard.Domain.FacebookAccount getFacebookAccountDetailsByUserProfileId(string Fbuserid, Guid userId)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {

                    // proceed action, to get all Facebook Account of User by UserId(Guid) and FbUserId(string).
                    NHibernate.IQuery query = session.CreateQuery("from FacebookAccount where FbUserId = :Fbuserid and UserId=:userId");
                    query.SetParameter("Fbuserid", Fbuserid);
                    query.SetParameter("userId", userId);
                    Domain.Socioboard.Domain.FacebookAccount result = (Domain.Socioboard.Domain.FacebookAccount)query.UniqueResult();
                    return result;
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
                bool ret = session.Query<SharethonPost>().Any(t => t.Facebookaccountid == Facebookaccountid && t.Facebookpageid == Facebookpageid && t.PostId == PostId && t.PostedTime > DateTime.UtcNow.AddHours(-12));
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
                try
                {

                    List<Guid> lstids = session.Query<Domain.Socioboard.Domain.SharethonPost>().Where(t => t.PostedTime < DateTime.UtcNow.AddMonths(-3)).Select(t => t.Id).ToList();
                    //After Session creation, start Transaction.
                    if (lstids.Count > 0)
                    {
                        using (NHibernate.ITransaction transaction = session.BeginTransaction())
                        {
                            try
                            {

                                int isUpdated = session.CreateQuery("Delete from SharethonPost where Id in (:ids)")
                                .SetParameterList("ids", lstids)
                               .ExecuteUpdate();
                                transaction.Commit();
                                return isUpdated;
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex.StackTrace);
                                return 0;
                            }
                        }
                    }//End Transaction
                    else
                    {
                        return 0;
                    }
                }
                catch {
                    return 0;
                }
            }
            }//End session


        public int deletepageshraethonpostByFacebookId(string facebookaccountId)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                try
                {

                    List<Guid> lstids = session.Query<Domain.Socioboard.Domain.SharethonPost>().Where(t =>t.Facebookaccountid==facebookaccountId).Select(t => t.Id).ToList();
                    //After Session creation, start Transaction.
                    if (lstids.Count > 0)
                    {
                        using (NHibernate.ITransaction transaction = session.BeginTransaction())
                        {
                            try
                            {

                                int isUpdated = session.CreateQuery("Delete from SharethonPost where Id in (:ids)")
                                .SetParameterList("ids", lstids)
                               .ExecuteUpdate();
                                transaction.Commit();
                                return isUpdated;
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex.StackTrace);
                                return 0;
                            }
                        }
                    }//End Transaction
                    else
                    {
                        return 0;
                    }
                }
                catch
                {
                    return 0;
                }
            }
        }


        public List<Shareathon> getUserShareathonByUserId(Guid UserId)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                List<Shareathon> objlstfb = session.CreateQuery("from Shareathon where UserId = :UserId and IsHidden = false ")
                            .SetParameter("UserId", UserId)
                       .List<Shareathon>().ToList<Shareathon>().GroupBy(t => t.Facebookaccountid).Select(t => t.First()).ToList(); 

                return objlstfb;
            }//End session
        }


        public List<SharethonPost> GetGroupPostReport(string profileid, int day)
        {
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {

                    List<SharethonPost> lstpost = session.Query<SharethonPost>().Where(t => t.PostedTime >= DateTime.UtcNow.AddDays(-day) && t.Facebookaccountid == profileid).ToList();
                    return lstpost;
                }
            }

        }

        public int DeleteShareathonByFacebookId(string FacebookAccountid, Guid UserId)
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
                        //NHibernate.IQuery query = session.CreateQuery("delete from Shareathon where Facebookaccountid = :Id")
                        //                .SetParameter("Id", FacebookAccountid);

                        //int isUpdated = query.ExecuteUpdate();
                        //transaction.Commit();
                        //return isUpdated;
                        int update = session.CreateQuery("Update Shareathon set FacebookStatus = :FacebookStatus where  Facebookaccountid = :Facebookaccountid and UserId = :UserId")
                               .SetParameter("FacebookStatus", 0).SetParameter("Facebookaccountid", FacebookAccountid).SetParameter("UserId", UserId)
                               .ExecuteUpdate();

                        transaction.Commit();
                        return update;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                        return 0;
                    }
                }//End Transaction
            }//End session
        }

        public int UpadteShareathonByFacebookUserId(string FacebookAccountid, Guid UserId)
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
                        //NHibernate.IQuery query = session.CreateQuery("delete from Shareathon where Facebookaccountid = :Id")
                        //                .SetParameter("Id", FacebookAccountid);

                        //int isUpdated = query.ExecuteUpdate();
                        //transaction.Commit();
                        //return isUpdated;
                        int update = session.CreateQuery("Update Shareathon set FacebookStatus = :FacebookStatus where  Facebookaccountid = :Facebookaccountid and UserId = :UserId")
                               .SetParameter("FacebookStatus", 1).SetParameter("Facebookaccountid", FacebookAccountid).SetParameter("UserId", UserId)
                               .ExecuteUpdate();

                        transaction.Commit();
                        return update;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                        return 0;
                    }
                }//End Transaction
            }//End session
        }


        public int UpdateShareathonByFacebookPageId(string FacebookPageid, Guid UserId)
        {
            //Creates a database connection and opens up a session


            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                List<Guid> lstids = session.Query<Domain.Socioboard.Domain.Shareathon>().Where(t => t.Facebookpageid.Contains(FacebookPageid) && t.Userid == UserId).Select(t => t.Id).ToList();
                if (lstids.Count > 0)
                {
                    //After Session creation, start Transaction.
                    using (NHibernate.ITransaction transaction = session.BeginTransaction())
                    {
                        try
                        {
                            //Proceed action, to delete a FacebookAccount by FbUserId and UserId.
                            int isUpdated = session.CreateQuery("Update Shareathon set FacebookStatus = :FacebookStatus where Id in (:ids)")
                            .SetParameterList("ids", lstids).SetParameter("FacebookStatus", 1)
                           .ExecuteUpdate();
                            transaction.Commit();
                            return isUpdated;
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.StackTrace);
                            return 0;
                        }
                    }//End Transaction
                }
                else
                {
                    return 0;
                }
            }//End session

        }

        public int DeleteShareathonByFacebookPageId(string FacebookPageid,Guid UserId)
        {
            //Creates a database connection and opens up a session
           
           
                using (NHibernate.ISession session = SessionFactory.GetNewSession())
                {
                    List<Guid> lstids = session.Query<Domain.Socioboard.Domain.Shareathon>().Where(t => t.Facebookpageid.Contains(FacebookPageid) && t.Userid == UserId).Select(t => t.Id).ToList();
                    if (lstids.Count > 0)
                    {
                        //After Session creation, start Transaction.
                        using (NHibernate.ITransaction transaction = session.BeginTransaction())
                        {
                            try
                            {
                                //Proceed action, to delete a FacebookAccount by FbUserId and UserId.
                                int isUpdated = session.CreateQuery("Update Shareathon set FacebookStatus = :FacebookStatus where Id in (:ids)")
                                .SetParameterList("ids", lstids).SetParameter("FacebookStatus",0)
                               .ExecuteUpdate();
                                transaction.Commit();
                                return isUpdated;
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex.StackTrace);
                                return 0;
                            }
                        }//End Transaction
                    }
                    else {
                        return 0;
                    }
                }//End session
            
        }

        public List<Domain.Socioboard.Helper.SharethonPageData> GetShareCountByFacebookId(string profileIds, int days)
        {
            string[] arrIds=profileIds.Split(','); 
            List<Domain.Socioboard.Helper.SharethonPageData> lstSharethonPageData=new List<Domain.Socioboard.Helper.SharethonPageData>(); 
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                try
                {
                    List<Domain.Socioboard.Domain.SharethonPost> lstSharethonPost = session.Query<Domain.Socioboard.Domain.SharethonPost>().Where(t => arrIds.Contains(t.Facebookaccountid) && t.PostedTime > DateTime.UtcNow.AddDays(-days).Date.AddSeconds(-1)).ToList();
                    dynamic _shareData = lstSharethonPost.GroupBy(p => p.Facebookaccountid).Select(group => new Domain.Socioboard.Helper.SharethonPageData(group.First().Facebookaccountid, group.Count())).OrderByDescending(t => t.postCount).ToList();
                    lstSharethonPageData = (List<Domain.Socioboard.Helper.SharethonPageData>)_shareData;
                }
                catch (Exception ex)
                {
                    lstSharethonPageData = new List<Domain.Socioboard.Helper.SharethonPageData>();
                }
            }
            return lstSharethonPageData;
        }

    }
}
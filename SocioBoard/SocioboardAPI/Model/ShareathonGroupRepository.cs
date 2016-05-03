using Api.Socioboard.Helper;
using Domain.Socioboard.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NHibernate.Linq;
using NHibernate.Criterion;

namespace Api.Socioboard.Model
{
    public class ShareathonGroupRepository
    {
        public bool AddShareathon(ShareathonGroup shareathon)
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

        public bool IsShareathonExist(Guid Userid, string Facebookagroupid, string  Facebookpageid) 
        {
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {

                bool ret = session.Query<ShareathonGroup>().Any(t => t.Facebookgroupid.Contains(Facebookagroupid) && t.Facebookpageid == Facebookpageid && t.Userid == Userid);
                return ret;

                //List<Shareathon> objlstfb = session.CreateQuery("from ShareathonGroup where UserId = :UserId and FacebookGroupId =: FacebookAccountId and FacebookPageId =: Facebookpageid and IsHidden = false ")
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
            }//End session
        }

        public bool IsShareathonExistFbUserId(Guid Userid, string Facebookaccountid)
        {
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                bool ret = session.Query<ShareathonGroup>().Any(t => t.Userid == Userid && (t.Facebookaccountid == Facebookaccountid || t.Facebookpageid.Contains(Facebookaccountid)));

                return ret;
            }//End session
        }
        public bool IsShareathonExistById(Guid Id)
        {
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {

                bool ret = session.Query<ShareathonGroup>().Any(t => t.Id == Id);
                return ret;

                //List<Shareathon> objlstfb = session.CreateQuery("from ShareathonGroup where UserId = :UserId and FacebookGroupId =: FacebookAccountId and FacebookPageId =: Facebookpageid and IsHidden = false ")
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
            }//End session
        }

        public List<ShareathonGroup> getUserShareathon(Guid UserId)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                List<ShareathonGroup> objlstfb = session.CreateQuery("from ShareathonGroup where UserId = :UserId and IsHidden = false and FacebookStatus = :FacebookStatus")
                            .SetParameter("UserId", UserId).SetParameter("FacebookStatus",1)
                       .List<ShareathonGroup>().ToList<ShareathonGroup>().GroupBy(t=>t.Facebookaccountid).Select(t=>t.First()).ToList();
                 
                    return objlstfb;
            }//End session
        }


        public Domain.Socioboard.Domain.ShareathonGroup getShareathon(Guid Id)
        {
            Domain.Socioboard.Domain.ShareathonGroup shareathon = null;
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        NHibernate.IQuery query = session.CreateQuery("from ShareathonGroup where Id = :ID and FacebookStatus = :FacebookStatus");
                        query.SetParameter("ID", Id).SetParameter("FacebookStatus", 1);
                        Domain.Socioboard.Domain.ShareathonGroup result = (Domain.Socioboard.Domain.ShareathonGroup)query.UniqueResult();
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

        public List<ShareathonGroup> getShareathons()
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                List<ShareathonGroup> objlstfb = session.CreateQuery("from ShareathonGroup where IsHidden = false and FacebookStatus = :FacebookStatus").SetParameter("FacebookStatus", 1)
                   .List<ShareathonGroup>().ToList<ShareathonGroup>();

                return objlstfb;
            }//End session
        }

        public ShareathonGroup getshareathonsbyaccount(Guid UserId, string FacebookAccountId, string Facebookpageid)
               {
                            using (NHibernate.ISession session = SessionFactory.GetNewSession())
                            {
                                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                                {
                                    List<ShareathonGroup> lstgrp = session.CreateQuery("from ShareathonGroup where Id = :UserId and FacebookGroupId =: FacebookAccountId and FacebookPageId =: Facebookpageid and IsHidden = false ")
                                        .SetParameter("UserId", UserId).SetParameter("FacebookAccountId", FacebookAccountId).SetParameter("Facebookpageid", Facebookpageid)
                                    .List<ShareathonGroup>().ToList<ShareathonGroup>();
                                    return lstgrp[0];
                                }
            
                            }
        
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


        public bool updateShareathon(Domain.Socioboard.Domain.ShareathonGroup shareathon)
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
                        session.CreateQuery("Update ShareathonGroup set Facebookaccountid=:Facebookaccountid,Facebookpageid=:Facebookpageid,Timeintervalminutes=:Timeintervalminutes,FacebookPageUrl=:FacebookPageUrl,Facebookgroupid=:Facebookgroupid,Facebooknameid=:Facebooknameid where Id = :Id")
                            .SetParameter("Facebookaccountid", shareathon.Facebookaccountid)
                            .SetParameter("Facebookpageid", shareathon.Facebookpageid)
                            .SetParameter("Timeintervalminutes", shareathon.Timeintervalminutes).SetParameter("FacebookPageUrl", shareathon.FacebookPageUrl).SetParameter("Facebookgroupid", shareathon.Facebookgroupid).SetParameter("Facebooknameid",shareathon.Facebooknameid)
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



        public bool IsPostExist(string Facebookgroupid, string PostId, string faceaccountId)
        {
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                try
                {
                    bool rt = session.Query<Domain.Socioboard.Domain.SharethonGroupPost>().Any(t => t.Facebookaccountid == faceaccountId && t.Facebookgroupid == Facebookgroupid && t.PostId == PostId && t.PostedTime>DateTime.UtcNow.AddHours(-12));
                    return rt;
                }
                catch (Exception ex)
                {

                    return true;
                }

                
            }//End session
        }



        public bool AddShareathonPost(SharethonGroupPost shareathon)
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


        public int DeleteGroupShareathon(Guid Id)
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
                        NHibernate.IQuery query = session.CreateQuery("delete from ShareathonGroup where Id = :Id")
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

        public int deletegroupshraethonpost()
        { 
         //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                try
                {
                    List<Guid> lstId = session.Query<Domain.Socioboard.Domain.SharethonGroupPost>().Where(t => t.PostedTime < DateTime.UtcNow.AddMonths(-3)).Select(t => t.Id).ToList();
                    //List<Guid> lstId = lstSharethonGroupPost.Select(t => t.Id).ToList();
                    if (lstId.Count > 0)
                    {
                        //After Session creation, start Transaction.
                        using (NHibernate.ITransaction transaction = session.BeginTransaction())
                        {
                            try
                            {
                                int isUpdated = session.CreateQuery("delete from SharethonGroupPost where Id in (:ids)")
                                .SetParameterList("ids", lstId)
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
                }
                catch (Exception ex)
                {
                    return 0;
                }
            }//End session
        }

        public List<SharethonGroupPost> GetGroupPostReport(string profileid,int day)
        {
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {

                    List<SharethonGroupPost> lstpost = session.Query<SharethonGroupPost>().Where(t => t.PostedTime >= DateTime.UtcNow.AddDays(-day) && t.Facebookaccountid == profileid).ToList();
                    return lstpost;
                }
            }
        
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

                        int update = session.CreateQuery("Update ShareathonGroup set FacebookStatus = :FacebookStatus where  Facebookaccountid = :Facebookaccountid and Userid = :UserId")
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

        public int DeleteGroupShareathonByFacebookId(string FacebookAccountid, Guid UserId)
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
                        //NHibernate.IQuery query = session.CreateQuery("delete from ShareathonGroup where Facebookaccountid = :Id")
                        //                .SetParameter("Id", FacebookAccountid);

                        //int isUpdated = query.ExecuteUpdate();
                        //transaction.Commit();
                        //return isUpdated;
                        int update = session.CreateQuery("Update ShareathonGroup set FacebookStatus = :FacebookStatus where  Facebookaccountid = :Facebookaccountid and Userid = :UserId")
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


        public int UpdateShareathonByFacebookPageId(string FacebookPageid, Guid UserId)
        {
            //Creates a database connection and opens up a session


            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                List<Guid> lstids = session.Query<Domain.Socioboard.Domain.ShareathonGroup>().Where(t => t.Facebookpageid.Contains(FacebookPageid) && t.Userid == UserId).Select(t => t.Id).ToList();
                if (lstids.Count > 0)
                {
                    //After Session creation, start Transaction.
                    using (NHibernate.ITransaction transaction = session.BeginTransaction())
                    {
                        try
                        {
                            //Proceed action, to delete a FacebookAccount by FbUserId and UserId.
                            int isUpdated = session.CreateQuery("Update ShareathonGroup set FacebookStatus = :FacebookStatus where Id in (:ids)")
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

       public List<Domain.Socioboard.Helper.SharethonGroupData> GetShareCountByFacebookId(string profileIds, int days)
       {
           List<Domain.Socioboard.Helper.SharethonGroupData> lstSharethonGroupData =new List<Domain.Socioboard.Helper.SharethonGroupData>();
           string[] arrId=profileIds.Split(',');
           using (NHibernate.ISession session = SessionFactory.GetNewSession())
           {
               try
               {
                   List<Domain.Socioboard.Domain.SharethonGroupPost> lstSharethonGroupPost = session.Query<Domain.Socioboard.Domain.SharethonGroupPost>().Where(t => arrId.Contains(t.Facebookaccountid) && t.PostedTime >= DateTime.Now.AddDays(-days).Date).ToList();
                  
                   dynamic _shareData = lstSharethonGroupPost.GroupBy(p => p.Facebookgroupid).Select(group => new Domain.Socioboard.Helper.SharethonGroupData(group.First().Facebookgroupid, group.First().Facebookgroupname, group.Count())).OrderByDescending(t => t.postCount).ToList();
                   lstSharethonGroupData = (List<Domain.Socioboard.Helper.SharethonGroupData>)_shareData;
               }
               catch (Exception ex)
               {
                   lstSharethonGroupData = new List<Domain.Socioboard.Helper.SharethonGroupData>();
               }
           }
           return lstSharethonGroupData; 
       }

    }
}
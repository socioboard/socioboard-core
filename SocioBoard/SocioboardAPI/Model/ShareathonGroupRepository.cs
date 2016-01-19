using Api.Socioboard.Helper;
using Domain.Socioboard.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NHibernate.Linq;

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

        public bool IsShareathonExist(Guid Userid, string Facebookagroupid, string Facebookpageid)
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

        public List<ShareathonGroup> getUserShareathon(Guid UserId)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                List<ShareathonGroup> objlstfb = session.CreateQuery("from ShareathonGroup where UserId = :UserId and IsHidden = false ")
                            .SetParameter("UserId", UserId)
                       .List<ShareathonGroup>().ToList<ShareathonGroup>();

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
                        NHibernate.IQuery query = session.CreateQuery("from ShareathonGroup where Id = :ID");
                        query.SetParameter("ID", Id);
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
                List<ShareathonGroup> objlstfb = session.CreateQuery("from ShareathonGroup where IsHidden = false ")
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
                            .SetParameter("Timeintervalminutes", shareathon.Timeintervalminutes).SetParameter("FacebookPageUrl", shareathon.FacebookPageUrl).SetParameter("Facebookgroupid", shareathon.Facebookgroupid).SetParameter("Facebooknameid", shareathon.Facebooknameid)
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
                    bool rt = session.Query<Domain.Socioboard.Domain.SharethonGroupPost>().Any(t => t.Facebookaccountid == faceaccountId && t.Facebookgroupid == Facebookgroupid && t.PostId == PostId);
                    //List<SharethonGroupPost> objlstfb = session.CreateQuery("from SharethonGroupPost where Facebookgroupid = :Facebookgroupid and Facebookaccountid=:faceaccountId and PostId =: PostId ")
                    //               .SetParameter("Facebookgroupid", Facebookgroupid).SetParameter("faceaccountId", faceaccountId)
                    //               .SetParameter("PostId", PostId)
                    //          .List<SharethonGroupPost>().ToList<SharethonGroupPost>();
                    //if (objlstfb.Count() > 0)
                    //{
                    //    return true;
                    //}
                    //else
                    //{
                    //    return false;
                    //}
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
                //After Session creation, start Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        DateTime dt = DateTime.UtcNow.Date.AddDays(-1);
                        //Proceed action, to delete a FacebookAccount by FbUserId and UserId.
                        //NHibernate.IQuery query = session.CreateQuery("delete from SharethonGroupPost where PostedTime like'%Id%'")
                        //                .SetParameter("Id", dt);

                        NHibernate.IQuery query = session.CreateQuery("Delete from SharethonGroupPost where PostedTime < DATE_ADD(UTC_TIMESTAMP(),INTERVAL -1 DAY)");
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
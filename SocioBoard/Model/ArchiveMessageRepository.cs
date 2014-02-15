using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SocioBoard.Helper;
using SocioBoard.Domain;

namespace SocioBoard.Model
{
    public class ArchiveMessageRepository
    {
        public void AddArchiveMessage(ArchiveMessage archive)
        {
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    session.Save(archive);
                    transaction.Commit();
                }
            }
        }

        public int DeleteArchiveMessage(ArchiveMessage archive)
        {
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        NHibernate.IQuery query = session.CreateQuery("delete from ArchiveMessage where UserId = :userid and Message = :message")
                                        .SetParameter("message", archive.Message)
                                        .SetParameter("userid", archive.UserId);
                        int isUpdated = query.ExecuteUpdate();
                        transaction.Commit();
                        return isUpdated;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                        return 0;
                    }
                }
            }
        }


        public int DeleteArchiveMessage(Guid archiveid)
        {
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        NHibernate.IQuery query = session.CreateQuery("delete from ArchiveMessage where Id = :archiveid")
                                        .SetParameter("archiveid", archiveid);
                        int isUpdated = query.ExecuteUpdate();
                        transaction.Commit();
                        return isUpdated;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                        return 0;
                    }
                }
            }
        }

        public void UpdateArchiveMessage(ArchiveMessage archive)
        {
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        session.CreateQuery("Update ArchiveMessage set Message =:message where UserId = :userid")
                            .SetParameter("message", archive.Message)
                            .SetParameter("userid", archive.UserId)
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

        public List<ArchiveMessage> getAllArchiveMessage(Guid Userid)
        {
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                   List<ArchiveMessage> alstFBAccounts = session.CreateQuery("from ArchiveMessage where UserId = :userid")
                   .SetParameter("userid", Userid)
                   .List<ArchiveMessage>()
                   .ToList<ArchiveMessage>();
                   return alstFBAccounts;

                   #region oldcode
                   //List<ArchiveMessage> alstFBAccounts = new List<ArchiveMessage>();

                   //foreach (ArchiveMessage item in query.Enumerable())
                   //{
                   //    alstFBAccounts.Add(item);
                   //} 
                   #endregion
                   

                }
            }
        }

        public bool checkArchiveMessageExists(Guid userid, string messageid)
        {
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        NHibernate.IQuery query = session.CreateQuery("from ArchiveMessage where UserId = :userid and MessageId =:messageid");
                        //  query.SetParameter("userid", group.UserId);  UserId =:userid and
                        query.SetParameter("userid", userid);
                        query.SetParameter("messageid", messageid);
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


                }
            }
        }

        public ArchiveMessage getArchiveMessageDetails(Guid userid, string archiveId)
        {
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        NHibernate.IQuery query = session.CreateQuery("from ArchiveMessage where UserId = :userid and Id=:archiveId");

                        query.SetParameter("userid", userid);
                        query.SetParameter("archiveId", archiveId);
                        ArchiveMessage grou = query.UniqueResult<ArchiveMessage>();
                        return grou;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                        return null;
                    }


                }
            }
        }

        public ArchiveMessage getArchiveMessagebyId(Guid userid, Guid archiveid)
        {
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        NHibernate.IQuery query = session.CreateQuery("from ArchiveMessage where UserId = :userid and Id=:archivename");

                        query.SetParameter("userid", userid);
                        query.SetParameter("archivename", archiveid);
                        ArchiveMessage grou = query.UniqueResult<ArchiveMessage>();
                        return grou;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                        return null;
                    }
                }
            }
        }

        public int DeleteArchiveMessageByUserid(Guid userid)
        {
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        NHibernate.IQuery query = session.CreateQuery("delete from ArchiveMessage where UserId = :userid")
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
                }
            }
        }

    }
}
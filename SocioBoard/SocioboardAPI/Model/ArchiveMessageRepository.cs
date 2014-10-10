using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Api.Socioboard.Helper;
using Domain.Socioboard.Domain;

namespace Api.Socioboard.Services
{
    public class ArchiveMessageRepository
    {
        /// <AddArchiveMessage>
        /// Add a new  ArchieveMEssage in a database.
        /// </summary>
        /// <param name="archive">et Values in a ArchieveMEssage Class Property and Pass the Object of ArchieveMEssage Class.(Domain.ArchieveMEssage)</param>
        public void AddArchiveMessage(Domain.Socioboard.Domain.ArchiveMessage archive)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction. 
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    //Proceed action, to save data.
                    session.Save(archive);
                    transaction.Commit();

                }// End Using Trasaction
            }// End using session
        }


        /// <DeleteArchiveMessage>
        /// Delete a ArchieveMessage From Database by UserId and Message.
        /// </summary>
        /// <param name="archive">the object of the ArchieveMessage class(Domain.ArchieveMEssage)</param>
        /// <returns>Return 1 for True and 0 for False</returns>
        public int DeleteArchiveMessage(Domain.Socioboard.Domain.ArchiveMessage archive)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction. 
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        // Proceed action to Detele specific data.
                        // return the integer value when it is success or not (0 or 1).
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
                }// End using trasaction
            }// End using session
        }


        /// <DeleteArchiveMessage>
        /// Delete a ArchieveMessage From Database by Id.
        /// </summary>
        /// <param name="archiveid">Id of the ArchieveMessage(Guid).</param>
        /// <returns>Return 1 for True and 0 for False</returns>
        public int DeleteArchiveMessage(Guid archiveid)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction. 
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        // Proceed action, to delete data 
                        // And return integer value when it is success or failed (0 or 1).
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
                }// End using trasaction
            }// End using session
        }


        public int DeleteArchiveMessage(Guid userid, string profileid)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction. 
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        // Proceed action, to delete data 
                        // And return integer value when it is success or failed (0 or 1).
                        NHibernate.IQuery query = session.CreateQuery("delete from ArchiveMessage where UserId = :userid and ProfileId = :Pro")
                            .SetParameter("userid", userid)
                            .SetParameter("Pro", profileid);
                        int isUpdated = query.ExecuteUpdate();
                        transaction.Commit();
                        return isUpdated;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                        return 0;
                    }
                }// End using trasaction
            }// End using session
        }











        /// <UpdateArchiveMessage>
        /// update ArchieveMessage by UserId.
        /// </summary>
        /// <param name="archive">the object of the ArchieveMessage class(Domain.ArchieveMEssage).</param>
        public void UpdateArchiveMessage(Domain.Socioboard.Domain.ArchiveMessage archive)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction. 
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        //Proceed action, to update Message by Id
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
                }//End using transaction 
            }// End using session 
        }


        /// <getAllArchiveMessage>
        /// Get all Message from ArchieverMessage by userId.
        /// </summary>
        /// <param name="Userid">Id of the ArchieveMessage(Guid).</param>
        /// <returns>Return all Message from ArchieverMessage in the form of List Type.</returns>
        public List<Domain.Socioboard.Domain.ArchiveMessage> getAllArchiveMessage(Guid Userid)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction. 
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    //Proceed action to get Archive messages
                    // And return list of archive messages.
                    List<Domain.Socioboard.Domain.ArchiveMessage> alstFBAccounts = session.CreateQuery("from ArchiveMessage where UserId = :userid order by CreatedDateTime desc")
                   .SetParameter("userid", Userid)
                   .List<Domain.Socioboard.Domain.ArchiveMessage>()
                   .ToList<Domain.Socioboard.Domain.ArchiveMessage>();
                   return alstFBAccounts;

                   #region oldcode
                   //List<ArchiveMessage> alstFBAccounts = new List<ArchiveMessage>();

                   //foreach (ArchiveMessage item in query.Enumerable())
                   //{
                   //    alstFBAccounts.Add(item);
                   //} 
                   #endregion
                   

                }//End using transaction  
            }//Using using session
        }

        public List<Domain.Socioboard.Domain.ArchiveMessage> getAllArchiveMessage(string profileid)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction. 
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    //Proceed action to get Archive messages
                    // And return list of archive messages.
                    List<Domain.Socioboard.Domain.ArchiveMessage> alstFBAccounts = session.CreateQuery("from ArchiveMessage where ProfileId = :profileid order by CreatedDateTime desc")
                   .SetParameter("profileid", profileid)
                   .List<Domain.Socioboard.Domain.ArchiveMessage>()
                   .ToList<Domain.Socioboard.Domain.ArchiveMessage>();
                    return alstFBAccounts;

                    #region oldcode
                    //List<ArchiveMessage> alstFBAccounts = new List<ArchiveMessage>();

                    //foreach (ArchiveMessage item in query.Enumerable())
                    //{
                    //    alstFBAccounts.Add(item);
                    //} 
                    #endregion


                }//End using transaction  
            }//Using using session
        }
        /// <checkArchiveMessageExists>
        /// Get all ArchieveMessage by UserId and MessageId.
        /// </summary>
        /// <param name="userid">Id of the ArchieveMessage(Guid)</param>
        /// <param name="messageid">MessageId of the ArchieveMessage(string)</param>
        /// <returns>Return true if result contain value otherwise false.</returns>
        public bool checkArchiveMessageExists(Guid userid, string messageid)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction. 
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        //Proceed action, to get Archive messages 
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
                }//End using transaction
            }//End using session
        }


        /// <getArchiveMessageDetails>
        /// Get ArchieveMessage by UserId and ArchieveId.
        /// </summary>
        /// <param name="userid">UserId of the ArchieveMessage(Guid)</param>
        /// <param name="archiveId">Id of the ArchieveMessage(string)</param>
        /// <returns>Return Unique object of Ads</returns>
        public Domain.Socioboard.Domain.ArchiveMessage getArchiveMessageDetails(Guid userid, string archiveId)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction. 
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        //Proceed Action, to get Archive messages
                        NHibernate.IQuery query = session.CreateQuery("from ArchiveMessage where UserId = :userid and Id=:archiveId");
                        query.SetParameter("userid", userid);
                        query.SetParameter("archiveId", archiveId);
                        Domain.Socioboard.Domain.ArchiveMessage grou = query.UniqueResult<Domain.Socioboard.Domain.ArchiveMessage>();
                        return grou;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                        return null;
                    }
                }//End Using transaction 
            }//End using session
        }


        /// <getArchiveMessagebyId>
        /// Get ArchieveMessage by UserId and ArchieveId.
        /// </summary>
        /// <param name="userid">UserId of the ArchieveMessage(Guid)</param>
        /// <param name="archiveId">ArchieveId of the ArchieveMessage(Guid)</param>
        /// <returns>Return Unique object of Ads</returns>
        public Domain.Socioboard.Domain.ArchiveMessage getArchiveMessagebyId(Guid userid, Guid archiveid)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction. 
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        //Proceed action, to get archive mesaages by user id and archive id.
                        NHibernate.IQuery query = session.CreateQuery("from ArchiveMessage where UserId = :userid and Id=:archivename");
                        query.SetParameter("userid", userid);
                        query.SetParameter("archivename", archiveid);
                        Domain.Socioboard.Domain.ArchiveMessage grou = query.UniqueResult<Domain.Socioboard.Domain.ArchiveMessage>();
                        return grou;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                        return null;
                    }
                }// End using transaction
            }//End Using session
        }


        /// <DeleteArchiveMessageByUserid>
        /// delete from ArchieveMessage by UserId.
        /// </summary>
        /// <param name="userid">UserId of the ArchieveMessage(Guid)</param>
        /// <returns>Return 0 for False and 1 for True.</returns>
        public int DeleteArchiveMessageByUserid(Guid userid)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction. 
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        //Proceed action, to delete Message by user id  
                        //retrun Int value when it is success or not (0 or 1)
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
                }//End using Trasaction
            }//End using session
        }

        public List<Domain.Socioboard.Domain.ArchiveMessage> getAllArchiveMessageDetail(string profileid, Guid userid)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //Begin session trasaction and opens up.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        string str = "from ArchiveMessage where UserId=:userid and ProfileId IN(";
                        string[] arrsrt = profileid.Split(',');
                        foreach (string sstr in arrsrt)
                        {
                            str += "'" + sstr + "'" + ",";
                        }
                        str = str.Substring(0, str.Length - 1);
                        str += ") order by CreatedDateTime desc";
                        List<Domain.Socioboard.Domain.ArchiveMessage> alst = session.CreateQuery(str).SetParameter("userid", userid)
                       .List<Domain.Socioboard.Domain.ArchiveMessage>()
                       .ToList<Domain.Socioboard.Domain.ArchiveMessage>();
                        return alst;

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                        return null;
                    }

                }//End Trasaction
            }//End session
        }

    }
}
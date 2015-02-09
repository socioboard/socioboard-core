using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Domain.Socioboard.Domain;
using Api.Socioboard.Helper;
namespace Api.Socioboard.Services
{
    public class InvitationRepository
    {
        /// <Add>
        /// Add a new invitation in DataBase. 
        public void Add(Domain.Socioboard.Domain.Invitation invitation)
        {
            try
            {
                //Creates a database connection and opens up a session
                using (NHibernate.ISession session = SessionFactory.GetNewSession())
                {
                    //After Session creation, start Transaction. 
                    using (NHibernate.ITransaction transaction = session.BeginTransaction())
                    {
                        //Proceed action to save data.
                        session.Save(invitation);
                        transaction.Commit();
                    }//End Using trasaction
                }//End Using session
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
            }
        }

        public bool IsFriendAlreadydInvited(Guid UserId, string FriendEmail)
        {
            bool ret = false;
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction. 
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        List<Domain.Socioboard.Domain.Invitation> query = session.CreateQuery("from Invitation where SenderUserId=:UserId and FriendEmail=:FriendEmail")
                                        .SetParameter("UserId", UserId)
                                        .SetParameter("FriendEmail", FriendEmail)
                                        .List<Domain.Socioboard.Domain.Invitation>().ToList<Domain.Socioboard.Domain.Invitation>();
                        if (query.Count > 0) { ret = true; } else { ret = false; }
                    }
                    catch (Exception ex)
                    {
                        ret = true;
                    }

                }
            }
            return ret;
        }

        public Domain.Socioboard.Domain.Invitation GetInvitationInfoBycode(string code)
        {
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction. 
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        NHibernate.IQuery query = session.CreateQuery("from Invitation where InvitationCode=:code")
                                        .SetParameter("code", code);
                        Domain.Socioboard.Domain.Invitation _Invitation = (Domain.Socioboard.Domain.Invitation)query.UniqueResult();
                        return _Invitation;
                    }
                    catch (Exception ex)
                    {
                        return null;
                    }
                }
            }
        }

        public int UpdateInvitatoinStatus(Guid invitattionid, Guid userid, DateTime regtime)
        {
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction. 
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        int res = session.CreateQuery("Update Invitation set FriendUserId=:FriendUserId, Status=1, AcceptInvitationDate=:AcceptInvitationDate where Id=:Id")
                            .SetParameter("FriendUserId", userid)
                            .SetParameter("AcceptInvitationDate",regtime)
                            .SetParameter("Id",invitattionid)
                            .ExecuteUpdate();
                        transaction.Commit();
                        return res;
                    }
                    catch (Exception ex)
                    {
                        return 0;
                    }
                }
            }
        }
        public List<Domain.Socioboard.Domain.Invitation> GetAllInvitedDataOfUser(Guid userid)
        { 
             using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction. 
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        List<Domain.Socioboard.Domain.Invitation> lstInvite = session.CreateQuery("from Invitation where SenderUserId=:Id")
                            .SetParameter("Id", userid)
                            .List<Domain.Socioboard.Domain.Invitation>().ToList<Domain.Socioboard.Domain.Invitation>();
                        return lstInvite;
                    }
                    catch (Exception ex)
                    {
                        return null;
                    }
                }
            }
        }

        public List<Domain.Socioboard.Domain.Invitation> GetInvitedDataOfAcceptedUser(Guid userid)
        {
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction. 
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        List<Domain.Socioboard.Domain.Invitation> lstInvite = session.CreateQuery("from Invitation where SenderUserId=:Id and Status=1")
                            .SetParameter("Id", userid)
                            .List<Domain.Socioboard.Domain.Invitation>().ToList<Domain.Socioboard.Domain.Invitation>();
                        return lstInvite;
                    }
                    catch (Exception ex)
                    {
                        return null;
                    }
                }
            }
        }

        public Domain.Socioboard.Domain.Invitation UserInvitedInfo(Guid UserId)
        { 
              using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction. 
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        NHibernate.IQuery query = session.CreateQuery("from Invitation where FriendUserId=:FriendUserId and Status=1")
                            .SetParameter("FriendUserId", UserId);
                        return (Domain.Socioboard.Domain.Invitation)query.UniqueResult();
                    }
                    catch (Exception ex)
                    {
                        return null;
                    }
                }
            }
        }


        /// < Admin Update>
        /// Update a existing admin.
        /// </summary>
        /// <param name="invitation">Set Values in a Invitation Class Property and Pass the Object of Invitation Class.(Domein.Invitation)</param>
        /// <returns>Return integer 1 for true and 0 for false.</returns>
        //public  int SetAllInvitationById(Invitation invitation)
        //{
        //    int res = 0;
        //    //Creates a database connection and opens up a session
        //    try
        //    {
        //        using (NHibernate.ISession session = SessionFactory.GetNewSession())
        //        {
        //            //After Session creation, start Transaction. 
        //            using (NHibernate.ITransaction transaction = session.BeginTransaction())
        //            {
        //                try
        //                {
        //                    // Proceed Sction to update Data.
        //                    // And Set the reuired paremeters to find the specific values. 

        //                    res = session.CreateQuery("Update Invitation set FriendEmail =:friendEmail, FriendName =: friendName , InvitationBody=:invitationBody, SenderEmail =:senderEmail, SenderName =:senderName, Status =:status, Subject =:subject  where Id = :id")
        //                              .SetParameter("id", invitation.Id)
        //                              .SetParameter("friendEmail", invitation.FriendEmail)
        //                              .SetParameter("friendName", invitation.FriendName)
        //                              .SetParameter("invitationBody", invitation.InvitationBody)
        //                              .SetParameter("senderEmail", invitation.SenderEmail)
        //                              .SetParameter("senderName", invitation.SenderName)
        //                              .SetParameter("status", invitation.Status)
        //                              .SetParameter("subject", invitation.Subject)

        //                              .ExecuteUpdate();
        //                    transaction.Commit();
        //                }
        //                catch (Exception ex)
        //                {
        //                    Console.WriteLine(ex.StackTrace);

        //                }
        //            }//End Using trasaction
        //        }//End Using session
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex.StackTrace);
        //    }

        //    return res;
        //}


        /// <SetInvitationStatusById>
        /// Update Invitation Status By Id
        /// </summary>
        /// <param name="invitation">Set Values in a Invitation Class Property and Pass the Object of Invitation Class.(Domein.Invitation)</param>
        /// <returns>Return integer 1 for true and 0 for false.</returns>
        //public int SetInvitationStatusById(Invitation invitation)
        //{
        //    int res = 0;
        //    //Creates a database connection and opens up a session
        //    try
        //    {
        //        using (NHibernate.ISession session = SessionFactory.GetNewSession())
        //        {
        //            //After Session creation, start Transaction. 
        //            using (NHibernate.ITransaction transaction = session.BeginTransaction())
        //            {
        //                try
        //                {
        //                    // Proceed Sction to update Data.
        //                    // And Set the reuired paremeters to find the specific values. 

        //                    res = session.CreateQuery("Update Invitation set Status =:status, LastEmailSendDate=:lastEmailSendDate where Id = :id")
        //                              .SetParameter("status", invitation.Status)
        //                              .SetParameter("lastEmailSendDate", DateTime.Now)
        //                              .SetParameter("id", invitation.Id)
                                      
        //                              .ExecuteUpdate();
        //                    transaction.Commit();
        //                }
        //                catch (Exception ex)
        //                {
        //                    Console.WriteLine(ex.StackTrace);

        //                }
        //            }//End Using trasaction
        //        }//End Using session
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex.StackTrace);

        //    }

        //    return res;
        //}


        /// <SetInvitationStatusById>
        /// Update Invitation Status By Id
        /// </summary>
        /// <param name="invitation">Set Values in a Invitation Class Property and Pass the Object of Invitation Class.(Domein.Invitation)</param>
        /// <returns>Return integer 1 for true and 0 for false.</returns>
        //public int SetInvitationStatusOnlyById(Invitation invitation)
        //{
        //    int res = 0;
        //    //Creates a database connection and opens up a session
        //    try
        //    {
        //        using (NHibernate.ISession session = SessionFactory.GetNewSession())
        //        {
        //            //After Session creation, start Transaction. 
        //            using (NHibernate.ITransaction transaction = session.BeginTransaction())
        //            {
        //                try
        //                {
        //                    // Proceed Sction to update Data.
        //                    // And Set the reuired paremeters to find the specific values. 

        //                    res = session.CreateQuery("Update Invitation set Status =:status where Id = :id")
        //                              .SetParameter("status", invitation.Status)
        //                              .SetParameter("id", invitation.Id)

        //                              .ExecuteUpdate();
        //                    transaction.Commit();
        //                }
        //                catch (Exception ex)
        //                {
        //                    Console.WriteLine(ex.StackTrace);

        //                }
        //            }//End Using trasaction
        //        }//End Using session
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex.StackTrace);

        //    }

        //    return res;
        //}



        /// <SetInvitationStatusById>
        /// Update Invitation Status By Sender Email and Freiend Email
        /// </summary>
        /// <param name="invitation">Set Values in a Invitation Class Property and Pass the Object of Invitation Class.(Domein.Invitation)</param>
        /// <returns>Return integer 1 for true and 0 for false.</returns>
        //public int SetInvitationStatusBySenderEmailFreiendEmail(Invitation invitation)
        //{
        //    int res = 0;
        //    //Creates a database connection and opens up a session
        //    try
        //    {
        //        using (NHibernate.ISession session = SessionFactory.GetNewSession())
        //        {
        //            //After Session creation, start Transaction. 
        //            using (NHibernate.ITransaction transaction = session.BeginTransaction())
        //            {
        //                try
        //                {
        //                    // Proceed Sction to update Data.
        //                    // And Set the reuired paremeters to find the specific values. 

        //                    res = session.CreateQuery("Update Invitation set Status =:status where SenderEmail = :senderEmail and FriendEmail = :friendEmail")
        //                              .SetParameter("status", invitation.Status)
        //                              .SetParameter("senderEmail", invitation.SenderEmail)
        //                              .SetParameter("friendEmail", invitation.FriendEmail)

        //                              .ExecuteUpdate();
        //                    transaction.Commit();
        //                }
        //                catch (Exception ex)
        //                {
        //                    Console.WriteLine(ex.StackTrace);

        //                }
        //            }//End Using trasaction
        //        }//End Using session
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex.StackTrace);

        //    }

        //    return res;
        //}

        /// <GetUserInfo>
        /// Get the all information of existing admin by UserName and Password.
        /// </summary>
        /// <param name="UserName">Admin UserName </param>
        /// <param name="Password">Admin Password</param>
        /// <returns>Return object of Invitation Class with  value of each member in the form of list.(List<Invitation>)</returns>
        //public List<Invitation> GetInvitationInfoByStatus(Invitation invitation)
        //{
        //    List<Invitation> lstInvitation = new List<Invitation>();
        //    try
        //    {
        //        //Creates a database connection and opens up a session
        //        using (NHibernate.ISession session = SessionFactory.GetNewSession())
        //        {
        //            //After Session creation, start Transaction. 
        //            using (NHibernate.ITransaction transaction = session.BeginTransaction())
        //            {
        //                try
        //                {
        //                    // Proceed action to Get Data from Query
        //                    //Set the parameters to find the specific Data.
        //                    lstInvitation = session.CreateQuery("from Invitation u where u.Status= :status")
        //                    .SetParameter("status", invitation.Status)
        //                    .List<Invitation>().ToList<Invitation>();

        //                }
        //                catch (Exception ex)
        //                {
        //                    Console.WriteLine(ex.StackTrace);

        //                }
        //            }// End using transaction
        //        }//End Using Session
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex.StackTrace);

        //    }
        //    return lstInvitation;
        //}


        //public List<Invitation> GetInvitationInfoByFriendEmail(Invitation invitation)
        //{
        //    List<Invitation> lstInvitation = new List<Invitation>();
        //    try
        //    {
        //        //Creates a database connection and opens up a session
        //        using (NHibernate.ISession session = SessionFactory.GetNewSession())
        //        {
        //            //After Session creation, start Transaction. 
        //            using (NHibernate.ITransaction transaction = session.BeginTransaction())
        //            {
        //                try
        //                {
        //                    // Proceed action to Get Data from Query
        //                    //Set the parameters to find the specific Data.
        //                    lstInvitation = session.CreateQuery("from Invitation u where u.FriendEmail= :friendEmail")
        //                    .SetParameter("friendEmail", invitation.FriendEmail)
        //                    .List<Invitation>().ToList<Invitation>();

        //                }
        //                catch (Exception ex)
        //                {
        //                    Console.WriteLine(ex.StackTrace);

        //                }
        //            }// End using transaction
        //        }//End Using Session
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex.StackTrace);

        //    }
        //    return lstInvitation;
        //}


        /// <GetInvitationInfoById>
        /// GetInvitationInfoById
        /// </summary>
        /// <param name="invitation">Set Values in a Invitation Class Property and Pass the Object of Invitation Class.(Domein.Invitation)</param>
        /// <returns>Return objects of Invitation Class with  value of each member in the form of list.(List<Invitation>)</returns>
        //public List<Invitation> GetInvitationInfoById(Invitation invitation)
        //{
        //    List<Invitation> lstInvitation = new List<Invitation>();
        //    try
        //    {
        //        //Creates a database connection and opens up a session
        //        using (NHibernate.ISession session = SessionFactory.GetNewSession())
        //        {
        //            //After Session creation, start Transaction. 
        //            using (NHibernate.ITransaction transaction = session.BeginTransaction())
        //            {
        //                try
        //                {
        //                    // Proceed action to Get Data from Query
        //                    //Set the parameters to find the specific Data.
        //                    lstInvitation = session.CreateQuery("from Invitation u where u.Id= :id")
        //                    .SetParameter("id", invitation.Id)
        //                    .List<Invitation>().ToList<Invitation>();

        //                }
        //                catch (Exception ex)
        //                {
        //                    Console.WriteLine(ex.StackTrace);

        //                }
        //            }// End using transaction
        //        }//End Using Session
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex.StackTrace);

        //    }
        //    return lstInvitation;
        //}


        /// <GetAllInvitationInfo>
        /// Get All Invitation Info
        /// </summary>
        /// <param name="invitation">Set Values in a Invitation Class Property and Pass the Object of Invitation Class.(Domein.Invitation)</param>
        /// <returns>Return a object of Invitation Class with  value of each member in form of List type.(List<Invitation>)</returns>
        //public List<Invitation> GetAllInvitationInfo(Invitation invitation)
        //{
        //    List<Invitation> lstInvitation = new List<Invitation>();
        //    try
        //    {
        //        //Creates a database connection and opens up a session
        //        using (NHibernate.ISession session = SessionFactory.GetNewSession())
        //        {
        //            //After Session creation, start Transaction. 
        //            using (NHibernate.ITransaction transaction = session.BeginTransaction())
        //            {
        //                try
        //                {
        //                    // Proceed action to Get Data from Query
        //                    //Set the parameters to find the specific Data.
        //                    lstInvitation = session.CreateQuery("from Invitation")
        //                    .SetParameter("id", invitation.Id)
        //                    .List<Invitation>().ToList<Invitation>();

        //                }
        //                catch (Exception ex)
        //                {
        //                    Console.WriteLine(ex.StackTrace);

        //                }
        //            }// End using transaction
        //        }//End Using Session
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex.StackTrace);

        //    }

        //    return lstInvitation;
        //}
    }
}
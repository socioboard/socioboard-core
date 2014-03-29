using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SocioBoard.Helper;
using SocioBoard.Domain;

namespace SocioBoard.Model
{
    public class InvitationRepository
    {
        /// <Add>
        /// Add a new invitation in DataBase. 
        /// </summary>
        /// <param name="user">Set Values in a invitation Class Property and Pass the Object of invitation Class (SocioBoard.Domain.Admin).</param>
        public int Add(Invitation invitation)
        {
            int res = 0;
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

                        res = 1;

                    }//End Using trasaction
                }//End Using session
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);

            }

            return res;
        }

        /// < Admin Update>
        /// Update a existing admin.
        /// </summary>
        /// <param name="invitation">Set Values in a Invitation Class Property and Pass the Object of Invitation Class.(Domein.Invitation)</param>
        /// <returns>Return integer 1 for true and 0 for false.</returns>
        public  int SetAllInvitationById(Invitation invitation)
        {
            int res = 0;
            //Creates a database connection and opens up a session
            try
            {
                using (NHibernate.ISession session = SessionFactory.GetNewSession())
                {
                    //After Session creation, start Transaction. 
                    using (NHibernate.ITransaction transaction = session.BeginTransaction())
                    {
                        try
                        {
                            // Proceed Sction to update Data.
                            // And Set the reuired paremeters to find the specific values. 

                            res = session.CreateQuery("Update Invitation set FriendEmail =:friendEmail, FriendName =: friendName , InvitationBody=:invitationBody, SenderEmail =:senderEmail, SenderName =:senderName, Status =:status, Subject =:subject  where Id = :id")
                                      .SetParameter("id", invitation.Id)
                                      .SetParameter("friendEmail", invitation.FriendEmail)
                                      .SetParameter("friendName", invitation.FriendName)
                                      .SetParameter("invitationBody", invitation.InvitationBody)
                                      .SetParameter("senderEmail", invitation.SenderEmail)
                                      .SetParameter("senderName", invitation.SenderName)
                                      .SetParameter("status", invitation.Status)
                                      .SetParameter("subject", invitation.Subject)

                                      .ExecuteUpdate();
                            transaction.Commit();
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.StackTrace);

                        }
                    }//End Using trasaction
                }//End Using session
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);

            }

            return res;
        }


        /// <SetInvitationStatusById>
        /// Update Invitation Status By Id
        /// </summary>
        /// <param name="invitation">Set Values in a Invitation Class Property and Pass the Object of Invitation Class.(Domein.Invitation)</param>
        /// <returns>Return integer 1 for true and 0 for false.</returns>
        public int SetInvitationStatusById(Invitation invitation)
        {
            int res = 0;
            //Creates a database connection and opens up a session
            try
            {
                using (NHibernate.ISession session = SessionFactory.GetNewSession())
                {
                    //After Session creation, start Transaction. 
                    using (NHibernate.ITransaction transaction = session.BeginTransaction())
                    {
                        try
                        {
                            // Proceed Sction to update Data.
                            // And Set the reuired paremeters to find the specific values. 

                            res = session.CreateQuery("Update Invitation set Status =:status where Id = :id")
                                      .SetParameter("status", invitation.Status)
                                      .SetParameter("id", invitation.Id)
                                      
                                      .ExecuteUpdate();
                            transaction.Commit();
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.StackTrace);

                        }
                    }//End Using trasaction
                }//End Using session
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);

            }

            return res;
        }


        /// <SetInvitationStatusById>
        /// Update Invitation Status By Sender Email and Freiend Email
        /// </summary>
        /// <param name="invitation">Set Values in a Invitation Class Property and Pass the Object of Invitation Class.(Domein.Invitation)</param>
        /// <returns>Return integer 1 for true and 0 for false.</returns>
        public int SetInvitationStatusBySenderEmailFreiendEmail(Invitation invitation)
        {
            int res = 0;
            //Creates a database connection and opens up a session
            try
            {
                using (NHibernate.ISession session = SessionFactory.GetNewSession())
                {
                    //After Session creation, start Transaction. 
                    using (NHibernate.ITransaction transaction = session.BeginTransaction())
                    {
                        try
                        {
                            // Proceed Sction to update Data.
                            // And Set the reuired paremeters to find the specific values. 

                            res = session.CreateQuery("Update Invitation set Status =:status where SenderEmail = :senderEmail and FriendEmail = :friendEmail")
                                      .SetParameter("status", invitation.Status)
                                      .SetParameter("senderEmail", invitation.SenderEmail)
                                      .SetParameter("friendEmail", invitation.FriendEmail)

                                      .ExecuteUpdate();
                            transaction.Commit();
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.StackTrace);

                        }
                    }//End Using trasaction
                }//End Using session
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);

            }

            return res;
        }

        /// <GetUserInfo>
        /// Get the all information of existing admin by UserName and Password.
        /// </summary>
        /// <param name="UserName">Admin UserName </param>
        /// <param name="Password">Admin Password</param>
        /// <returns>Return object of Invitation Class with  value of each member in the form of list.(List<Invitation>)</returns>
        public List<Invitation> GetInvitationInfoByStatus(Invitation invitation)
        {
            List<Invitation> lstInvitation = new List<Invitation>();
            try
            {
                //Creates a database connection and opens up a session
                using (NHibernate.ISession session = SessionFactory.GetNewSession())
                {
                    //After Session creation, start Transaction. 
                    using (NHibernate.ITransaction transaction = session.BeginTransaction())
                    {
                        try
                        {
                            // Proceed action to Get Data from Query
                            //Set the parameters to find the specific Data.
                            lstInvitation = session.CreateQuery("from Invitation u where u.Status= :status")
                            .SetParameter("status", invitation.Status)
                            .List<Invitation>().ToList<Invitation>();

                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.StackTrace);

                        }
                    }// End using transaction
                }//End Using Session
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);

            }
            return lstInvitation;
        }


        /// <GetInvitationInfoById>
        /// GetInvitationInfoById
        /// </summary>
        /// <param name="invitation">Set Values in a Invitation Class Property and Pass the Object of Invitation Class.(Domein.Invitation)</param>
        /// <returns>Return objects of Invitation Class with  value of each member in the form of list.(List<Invitation>)</returns>
        public List<Invitation> GetInvitationInfoById(Invitation invitation)
        {
            List<Invitation> lstInvitation = new List<Invitation>();
            try
            {
                //Creates a database connection and opens up a session
                using (NHibernate.ISession session = SessionFactory.GetNewSession())
                {
                    //After Session creation, start Transaction. 
                    using (NHibernate.ITransaction transaction = session.BeginTransaction())
                    {
                        try
                        {
                            // Proceed action to Get Data from Query
                            //Set the parameters to find the specific Data.
                            lstInvitation = session.CreateQuery("from Invitation u where u.Id= :id")
                            .SetParameter("id", invitation.Id)
                            .List<Invitation>().ToList<Invitation>();

                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.StackTrace);

                        }
                    }// End using transaction
                }//End Using Session
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);

            }
            return lstInvitation;
        }


        /// <GetAllInvitationInfo>
        /// Get All Invitation Info
        /// </summary>
        /// <param name="invitation">Set Values in a Invitation Class Property and Pass the Object of Invitation Class.(Domein.Invitation)</param>
        /// <returns>Return a object of Invitation Class with  value of each member in form of List type.(List<Invitation>)</returns>
        public List<Invitation> GetAllInvitationInfo(Invitation invitation)
        {
            List<Invitation> lstInvitation = new List<Invitation>();
            try
            {
                //Creates a database connection and opens up a session
                using (NHibernate.ISession session = SessionFactory.GetNewSession())
                {
                    //After Session creation, start Transaction. 
                    using (NHibernate.ITransaction transaction = session.BeginTransaction())
                    {
                        try
                        {
                            // Proceed action to Get Data from Query
                            //Set the parameters to find the specific Data.
                            lstInvitation = session.CreateQuery("from Invitation")
                            .SetParameter("id", invitation.Id)
                            .List<Invitation>().ToList<Invitation>();

                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.StackTrace);

                        }
                    }// End using transaction
                }//End Using Session
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);

            }

            return lstInvitation;
        }
    }
}
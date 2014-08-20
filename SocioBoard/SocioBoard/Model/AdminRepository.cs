using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SocioBoard.Domain;
using SocioBoard.Helper;


namespace SocioBoard.Model
{
    public class AdminRepository
    {
        /// <Add>
        /// Add a new admin in DataBase. 
        /// </summary>
        /// <param name="user">Set Values in a Admin Class Property and Pass the Object of Admin Class (SocioBoard.Domain.Admin).</param>
        public static void Add(SocioBoard.Domain.Admin user)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start and open Transaction. 
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    //Proceed action to save data.
                    session.Save(user);
                    transaction.Commit();

                }//End Using trasaction
            }//End Using session
        }

        /// < Admin Update>
        /// Update a existing admin.
        /// </summary>
        /// <param name="user">Set Values in a Admin Class Property and Pass the Object of Admin Class.</param>
        public static void Update(SocioBoard.Domain.Admin user)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction. 
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        // Proceed Sction to update Data.
                        // And Set the reuired paremeters to find the specific values. 
                        int i = session.CreateQuery("Update Admin set Image =:profileurl, UserName =: username , TimeZone=:timezone,FirstName =:first,LastName =:last  where Id = :twtuserid")
                                  .SetParameter("twtuserid", user.Id)
                                  .SetParameter("profileurl", user.Image)
                                  .SetParameter("username", user.UserName)
                                  .SetParameter("timezone", user.TimeZone)
                                  .SetParameter("first",user.FirstName)
                                  .SetParameter("last",user.LastName)
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

        /// <GetUserInfo>
        /// Get the all information of existing admin by UserName and Password.
        /// </summary>
        /// <param name="UserName">Admin UserName </param>
        /// <param name="Password">Admin Password</param>
        /// <returns>Return Latest Information of Admin</returns>
        public SocioBoard.Domain.Admin GetUserInfo(string UserName, string Password)
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
                        NHibernate.IQuery query = session.CreateQuery("from Admin u where u.UserName = : email and u.Password= :password");
                        query.SetParameter("email", UserName);
                        query.SetParameter("password", Password);

                        // Get the UniqueResult and return.
                        SocioBoard.Domain.Admin result = (SocioBoard.Domain.Admin)query.UniqueResult();
                        return result;

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                        return null;
                    }
                }// End using transaction
            }//End Using Session
        }

        /// <ChangePassword>
        /// Update/change Password of existing admin.
        /// </summary>
        /// <param name="newpassword">New Password of Admin </param>
        /// <param name="oldpassword">Old Password of Admin</param>
        /// <param name="UserName">Pass the UserName Of Admin</param>
        /// <returns>Return 0 for False and 1 for True</returns>
        public int ChangePassword(string newpassword, string oldpassword, string UserName)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction. 
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        //NHibernate.IQuery query = session.CreateQuery("Update User set Password=:password where EmailId = :email and Password = :oldpass");
                        //query.SetParameter("email", Emailid);
                        //query.SetParameter("oldpass",oldpassword);
                        //query.SetParameter("password",newpassword);
                        //query.ExecuteUpdate();


                        // Update Values of given parameters in the Database.
                        // And returns the integer value when its is succes or faild (0 or 1).
                        int i = session.CreateQuery("Update Admin set Password=:password where UserName = :email and Password = :oldpass")
                                  .SetParameter("email", UserName)
                                  .SetParameter("oldpass", oldpassword)
                                  .SetParameter("password", newpassword)
                                  .ExecuteUpdate();
                        transaction.Commit();
                        return i;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                        return 0;
                    }
                }// End using trasaction
            }// End Using session.
        }

        /// <ChangePwd>
        /// Update/change password of existing admin.
        /// </summary>
        /// <param name="newpassword">New Password of Admin</param>
        /// <param name="UserName">UserName Of Admin</param>
        /// <returns>Return 0 for False and 1 for True</returns>
        public int ChangePwd(string newpassword, string UserName)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction. 
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        //NHibernate.IQuery query = session.CreateQuery("Update User set Password=:password where EmailId = :email and Password = :oldpass");
                        //query.SetParameter("email", Emailid);
                        //query.SetParameter("oldpass",oldpassword);
                        //query.SetParameter("password",newpassword);
                        //query.ExecuteUpdate();


                        // Update Table value of Given parameters.
                        // And returns th integer value when it is succes or failed (0 or 1).
                        int i = session.CreateQuery("Update Admin set Password=:password where UserName = :email")
                                  .SetParameter("email", UserName)
                                  .SetParameter("password", newpassword)
                                  .ExecuteUpdate();
                        transaction.Commit();
                        return i;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                        return 0;
                    }
                }// End using trasation 
            }// End using session
        }

    }
}
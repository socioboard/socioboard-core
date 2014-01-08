using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SocioBoard.Domain;
using SocioBoard.Helper;
using NHibernate.Criterion;
using NHibernate;
using NHibernateHelper;
using NHibernate.Transform;
using System.Collections;
namespace SocioBoard.Model
{
    public class UserRepository : IUserRepository
    {
        public static ICollection<User> GetAllUsers()
        {

            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {

                return session.CreateCriteria(typeof(User)).List<User>();

            }

        }



        /// <summary>
        /// Add a new student in the database. 
        /// </summary>
        /// <param name="student">Student object</param>
        public static void Add(User user)
        {
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    session.Save(user);
                    transaction.Commit();
                }
            }
        }
        //public User GetById(int id)
        //{
        //    using (ISession session = NHibernateHelper.OpenSession())
        //    {
        //        User user = session
        //            .CreateCriteria(typeof(User))
        //            .Add(Restrictions.Eq("UserId", id))
        //            .UniqueResult<User>();
        //        return user;
        //    }
        //}
        public static void Update(User user)
        {
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        int i = session.CreateQuery("Update User set ProfileUrl =:profileurl, UserName =: username , EmailId=:emailid,UserStatus=:userstatus,ExpiryDate=:expirydate,TimeZone=:timezone where Id = :twtuserid")
                                  .SetParameter("twtuserid", user.Id)
                                  .SetParameter("profileurl", user.ProfileUrl)
                                  .SetParameter("username", user.UserName)
                                  .SetParameter("emailid", user.EmailId)
                                  .SetParameter("userstatus", user.UserStatus)
                                  .SetParameter("expirydate", user.ExpiryDate)
                                  .SetParameter("timezone", user.TimeZone)
                                  .ExecuteUpdate();
                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);

                    }


                }
            }
        }

        public int  UpdateCreatDateByUserId(User user)
        {
            int i=0;
            try
            {
                using (NHibernate.ISession session = SessionFactory.GetNewSession())
                {
                    using (NHibernate.ITransaction transaction = session.BeginTransaction())
                    {
                        try
                        {
                            i = session.CreateQuery("Update User set CreateDate =:createDate, AccountType =: accountType , PaymentStatus=:paymentStatus where Id = :id")
                                      .SetParameter("createDate", user.CreateDate)
                                      .SetParameter("accountType", user.AccountType)
                                      .SetParameter("paymentStatus", user.PaymentStatus)
                                      .SetParameter("id", user.Id)
                                      .ExecuteUpdate();
                            transaction.Commit();
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.StackTrace);

                        }


                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
            }

            return i;
        }

        public User GetUserInfo(string EmailId, string Password)
        {
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        NHibernate.IQuery query = session.CreateQuery("from User u where u.EmailId = : email and u.Password= :password");
                        query.SetParameter("email", EmailId);
                        query.SetParameter("password", Password);
                        User result = (User)query.UniqueResult();
                        return result;

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                        return null;
                    }



                }
            }
        }
        public bool IsUserExist(string Emailid)
        {
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        NHibernate.IQuery query = session.CreateQuery("from  User u where u.EmailId = : email");
                        query.SetParameter("email", Emailid);
                        var result = query.UniqueResult();
                        if (result == null)
                        {
                            return false;
                        }
                        else
                        {
                            return true;
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                        return true;
                    }
                }
            }
        }
        public int ChangePassword(string newpassword, string oldpassword, string Emailid)
        {
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        //NHibernate.IQuery query = session.CreateQuery("Update User set Password=:password where EmailId = :email and Password = :oldpass");
                        //query.SetParameter("email", Emailid);
                        //query.SetParameter("oldpass",oldpassword);
                        //query.SetParameter("password",newpassword);
                        //query.ExecuteUpdate();

                        int i = session.CreateQuery("Update User set Password=:password where EmailId = :email and Password = :oldpass")
                                  .SetParameter("email", Emailid)
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
                }
            }
        }
        public User getUserInfoByEmail(string emailId)
        {
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        NHibernate.IQuery query = session.CreateQuery("from  User u where u.EmailId = : email");
                        query.SetParameter("email", emailId);
                        User result = query.UniqueResult<User>();
                        return result;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                        return null;

                    }
                }
            }

        }
        public void UpdatePassword(string emailid, string password, Guid id, string username, string accounttype)
        {
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        int i = session.CreateQuery("Update User set EmailId=:email, UserName =: username, Password =:pass, AccountType= :acctype where Id = :twtuserid")
                                  .SetParameter("twtuserid", id)
                                  .SetParameter("email", emailid)
                                  .SetParameter("pass", password)
                                  .SetParameter("acctype", accounttype)
                                  .SetParameter("username", username)
                                  .ExecuteUpdate();
                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                    }
                }
            }

        }
        public int ResetPassword(Guid id, string password)
        {
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        int i = session.CreateQuery("Update User set Password =:pass where Id = :userid")
                                  .SetParameter("userid", id)
                                  .SetParameter("pass", password)
                                  .ExecuteUpdate();
                        transaction.Commit();
                        return i;

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                        return 0;
                    }


                }
            }

        }

        public List<User> getAllUsers()
        {
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        NHibernate.IQuery query = session.CreateQuery("from User");
                        List<User> alstUser = new List<User>();
                        foreach (User item in query.Enumerable())
                        {
                            alstUser.Add(item);
                        }

                        return alstUser;

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                        return null;
                    }
                }
            }
        }
        public User getUsersById(Guid userId)
        {
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        NHibernate.IQuery query = session.CreateQuery("from User where Id =: userid");
                        query.SetParameter("userid", userId);
                        User usr = query.UniqueResult<User>();
                        return usr;

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                        return null;
                    }
                }
            }
        }
        public int changePaymentStatus(Guid UserId, string status)
        {
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        int i = session.CreateQuery("Update User set PaymentStatus =:status where Id=:userid")
                                  .SetParameter("status", status)
                                  .SetParameter("userid", UserId)
                                  .ExecuteUpdate();
                        transaction.Commit();
                        return i;

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                        return 0;
                    }

                }
            }
        }
        public ArrayList UserCountByMonth()
        {
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    ArrayList alstUser = new ArrayList();
                    try
                    {
                        NHibernate.IQuery query = session.CreateSQLQuery("Select month(CreateDate),Count(*) from User group by month(CreateDate)");
                        foreach (var item in query.List())
                        {
                            alstUser.Add(item);
                        }
                        return alstUser;
                    }
                    catch (Exception Err)
                    {
                        Console.Write(Err.StackTrace);
                        return alstUser;
                    }
                }
            }
        }

        public ArrayList UserCountByAccTypeMonth()
        {
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    ArrayList alstUser = new ArrayList();
                    try
                    {
                        NHibernate.IQuery query = session.CreateSQLQuery("Select Month(CreateDate),count(*),Id,AccountType from User where PaymentStatus='unpaid' group by AccountType,Month(CreateDate)");
                        foreach (var item in query.List())
                        {
                            alstUser.Add(item);
                        }
                        return alstUser;
                    }
                    catch (Exception Err)
                    {
                        Console.Write(Err.StackTrace);
                        return alstUser;
                    }
                }
            }
        }

        public void UpdateAccountType(Guid Userid, string AccountType)
        {
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {

                    try
                    {
                        int i = session.CreateQuery("Update User set AccountType=:acctype  where Id = :userid")
                                  .SetParameter("userid", Userid)
                                  .SetParameter("acctype", AccountType)
                                  .ExecuteUpdate();
                        transaction.Commit();


                    }
                    catch { }
                }
            }

        }


        /********************/

        public List<User> testing()
        {
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                      List<User> s  = session.CreateQuery("from User").List<User>().ToList<User>();
                      return s;
                        //List<User> alstUser = new List<User>();
                        //foreach (User item in query.Enumerable())
                        //{
                        //    alstUser.Add(item);
                        //}

                        //return alstUser;

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                        return null;
                    }
                }
            }
        }

        //public int ResetPassword(Guid id, string password)
        //{
        //    using (NHibernate.ISession session = SessionFactory.GetNewSession())
        //    {
        //        using (NHibernate.ITransaction transaction = session.BeginTransaction())
        //        {
        //            try
        //            {
        //                int i = session.CreateQuery("Update User set Password =:pass where Id = :userid")
        //                          .SetParameter("userid", id)
        //                          .SetParameter("pass", password)
        //                          .ExecuteUpdate();
        //                transaction.Commit();
        //                return i;

        //            }
        //            catch (Exception ex)
        //            {
        //                Console.WriteLine(ex.StackTrace);
        //                return 0;
        //            }


        //        }
        //    }

        //}

    }
}

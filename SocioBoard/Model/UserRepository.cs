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

        public  int UpdateReferenceUserByUserId(User user)
        {
            int i = 0;
            try
            {
                using (NHibernate.ISession session = SessionFactory.GetNewSession())
                {
                    using (NHibernate.ITransaction transaction = session.BeginTransaction())
                    {
                        try
                        {
                            i = session.CreateQuery("Update User set ReferenceStatus =:referenceStatus where Id = :id")
                                      .SetParameter("referenceStatus", user.ReferenceStatus)
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

        public int UpdateActivationStatusByUserId(User user)
        {
            int i = 0;
            try
            {
                using (NHibernate.ISession session = SessionFactory.GetNewSession())
                {
                    using (NHibernate.ITransaction transaction = session.BeginTransaction())
                    {
                        try
                        {
                            i = session.CreateQuery("Update User set ActivationStatus =:activationStatus where Id = :id")
                                      .SetParameter("activationStatus", user.ActivationStatus)
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
            List<User> lstUser = new List<User>();
            User user = new User();
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        lstUser = session.CreateQuery("from  User u where u.EmailId = : email")
                        .SetParameter("email", emailId).List<User>().ToList<User>();
                          user=lstUser[0];
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                    }
                }
            }

            return user;
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

        public int UpdateUserExpiryDateById(User user)
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
                            i = session.CreateQuery("Update User set ExpiryDate=:expirydate where Id = :id")
                                      .SetParameter("id", user.Id)
                                      .SetParameter("expirydate", user.ExpiryDate)
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


        public void SetUserByUserId(string emailid, string password, Guid id, string username, string accounttype,string couponcode)
        {
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        int i = session.CreateQuery("Update User set EmailId=:email, UserName =: username, Password =:pass, AccountType= :acctype,CouponCode=:couponCode where Id = :twtuserid")
                                  .SetParameter("twtuserid", id)
                                  .SetParameter("email", emailid)
                                  .SetParameter("pass", password)
                                  .SetParameter("acctype", accounttype)
                                  .SetParameter("username", username)
                                  .SetParameter("couponCode", couponcode)
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

        public int DeleteUser(Guid id)
        {
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        int i = session.CreateQuery("Delete from User where Id = :userid")
                                  .SetParameter("userid", id)
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
                        NHibernate.IQuery query = session.CreateQuery("from User where Id !=null");
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

        public ArrayList PaidUserCountByMonth()
        {
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    ArrayList alstUser = new ArrayList();
                    try
                    {
                        NHibernate.IQuery query = session.CreateSQLQuery("Select month(CreateDate),Count(*) from User where PaymentStatus='Paid' group by month(CreateDate)");
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


        public ArrayList UnPaidUserCountByMonth()
        {
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    ArrayList alstUser = new ArrayList();
                    try
                    {
                        NHibernate.IQuery query = session.CreateSQLQuery("Select month(CreateDate),Count(*) from User where PaymentStatus='unpaid' group by month(CreateDate)");
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


        public static void UpdateAccountType(User user)
        {
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        int i = session.CreateQuery("Update User set ProfileUrl =:profileurl, UserName =: username , EmailId=:emailid,AccountType=:accounttype,UserStatus=:userstatus,ExpiryDate=:expirydate,TimeZone=:timezone where Id = :twtuserid")
                                  .SetParameter("twtuserid", user.Id)
                                  .SetParameter("profileurl", user.ProfileUrl)
                                  .SetParameter("username", user.UserName)
                                  .SetParameter("emailid", user.EmailId)
                                .SetParameter("accounttype", user.AccountType)
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


        public int UpdatePaymentStatusByUserId(User user)
        {
            int i = 0;
            try
            {
                using (NHibernate.ISession session = SessionFactory.GetNewSession())
                {
                    using (NHibernate.ITransaction transaction = session.BeginTransaction())
                    {

                        try
                        {
                            i = session.CreateQuery("Update User set PaymentStatus=:paymentStatus  where Id = :userid")
                                     .SetParameter("userid", user.Id)
                                     .SetParameter("paymentStatus", user.PaymentStatus)
                                     .ExecuteUpdate();
                            transaction.Commit();


                        }
                        catch { }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error : " + ex.StackTrace);
            }

            return i;

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



        public List<User> GetAllUsersByCreateDate(string date)
        {
            List<User> alstUser = new List<User>();
            try
            {
                using (NHibernate.ISession session = SessionFactory.GetNewSession())
                {
                    using (NHibernate.ITransaction transaction = session.BeginTransaction())
                    {
                        try
                        {
                            alstUser = session.CreateQuery("from User where CreateDate < '2014-02-10' order by CreateDate desc").List<User>().ToList<User>();
                            //.SetParameter("date", date).List<User>().ToList<User>();

                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.StackTrace);
                            return null;
                        }
                    }
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine("Error : " + ex.StackTrace);
            }

            return alstUser;
        }

        public List<User> GetUserByCouponCode(User user)
        {
            List<User> alstUser = new List<User>();
            try
            {
                using (NHibernate.ISession session = SessionFactory.GetNewSession())
                {
                    using (NHibernate.ITransaction transaction = session.BeginTransaction())
                    {
                        try
                        {
                            alstUser = session.CreateQuery("from User where CouponCode=:couponCode")
                            .SetParameter("couponCode", user.CouponCode)
                            .List<User>().ToList<User>();

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

                Console.WriteLine("Error : " + ex.StackTrace);
            }

            return alstUser;
        }


        public int DeleteUserByUserid(Guid userid)
        {
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        NHibernate.IQuery query = session.CreateQuery("delete from User where Id = :userid")
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

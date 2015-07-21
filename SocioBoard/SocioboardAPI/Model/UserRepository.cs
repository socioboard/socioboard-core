using Domain.Socioboard.Domain;
using Api.Socioboard.Helper;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NHibernate.Linq;

namespace Api.Socioboard.Model
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

        public int UpdateReferenceUserByUserId(User user)
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

        public int UpdateCreatDateByUserId(User user)
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

        public int ChangePasswordWithoutOldPassword(string newpassword, string Emailid)
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

                        int i = session.CreateQuery("Update User set Password=:password where EmailId = :email")
                                  .SetParameter("email", Emailid)
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
                        user = lstUser[0];
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                        user = null;
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
            int i = 0;
            try
            {
                using (NHibernate.ISession session = SessionFactory.GetNewSession())
                {
                    using (NHibernate.ITransaction transaction = session.BeginTransaction())
                    {
                        try
                        {
                            i = session.CreateQuery("Update User set ExpiryDate=:expirydate, AccountType=:accounttype where Id = :id")
                                      .SetParameter("id", user.Id)
                                      .SetParameter("accounttype", user.AccountType)
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


        public void SetUserByUserId(string emailid, string password, Guid id, string username, string accounttype, string couponcode)
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

        public int DeleteUserByAdmin(Guid id)
        {
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        int i = session.CreateQuery("Update User set ActivationStatus=:status where Id = :userid")
                                  .SetParameter("userid", id)
                                  .SetParameter("status", "2")
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

        public List<User> getAllUsersByAdmin()
        {
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        List<User> alstUser = session.CreateQuery("from User where Id !=null and ActivationStatus!=2")
                        .List<User>().ToList<User>();

                        Domain.Socioboard.Domain.User ObjUser = alstUser.Single(U => U.UserType == "SuperAdmin");
                        if (ObjUser != null)
                        {
                            alstUser.Remove(ObjUser);
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

        public List<User> getAllDeletedUsersByAdmin()
        {
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        List<User> alstUser = session.CreateQuery("from User")
                        .List<User>().Where(U => U.ActivationStatus == "2").ToList<User>();

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
                        int i = session.CreateQuery("Update User set PaymentStatus =:status, Ewallet=:0 where Id=:userid")
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


        public int UpdatePaymentandEwalletStatusByUserId(Guid userid, string ewallet, string accounttype, string paymentstatus)
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
                            i = session.CreateQuery("Update User set PaymentStatus=:paymentStatus, Ewallet=:ewallet, AccountType=:accounttype where Id = :userid")
                                     .SetParameter("userid", userid)
                                     .SetParameter("paymentStatus", paymentstatus)
                                     .SetParameter("ewallet", ewallet)
                                     .SetParameter("accounttype", accounttype)
                                     .ExecuteUpdate();
                            transaction.Commit();
                        }
                        catch
                        {
                        }
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
                        List<User> s = session.CreateQuery("from User").List<User>().ToList<User>();
                        return s;
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

        public int UpdateUserById(Guid Userid, string username, string timezone, string picurl)
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
                            i = session.CreateQuery("Update User set TimeZone=:tz, UserName=:username, ProfileUrl=:ProfileUrl where Id = :userid")
                                     .SetParameter("userid", Userid)
                                     .SetParameter("tz", timezone)
                                     .SetParameter("username", username)
                                     .SetParameter("ProfileUrl", picurl)
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


        // Edited by Hozefa[17/12/2014]
        public int UpdateAdminUserById(Guid Userid, string username, string timezone, string ProfileUrl)
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
                            i = session.CreateQuery("Update User set TimeZone=:tz , UserName=:username , ProfileUrl=:profileurl  where Id = :userid")
                                     .SetParameter("userid", Userid)
                                     .SetParameter("tz", timezone)
                                     .SetParameter("username", username)
                                     .SetParameter("profileurl", ProfileUrl)
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

        // Edited by Antima[1/11/2014]
        public int UpdateUserbyUserId(Guid UserId, string ActivationStatus)
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

                            i = session.CreateQuery("Update User set ActivationStatus=:ActivationStatus where Id = :UserId")
                                     .SetParameter("UserId", UserId)
                                     .SetParameter("ActivationStatus", ActivationStatus)
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

        // Edited by Vikash[20/11/2014]
        public int UpdateUserAccountInfoByUserId(string userid, string AccountType, DateTime ExpiryDate, string PaymentStatus)
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

                            i = session.CreateQuery("Update User set AccountType =: AccountType, ExpiryDate =: ExpiryDate, PaymentStatus =: PaymentStatus where Id = :UserId")
                                     .SetParameter("UserId", userid)
                                     .SetParameter("AccountType", AccountType)
                                     .SetParameter("ExpiryDate", ExpiryDate)
                                     .SetParameter("PaymentStatus", PaymentStatus)
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

        // Added by Sumit Gupta[3/12/2014]
        public int UpdateChangePasswordKey(Guid Userid, string ChangePasswordKey)
        {
            int i = 0;
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        i = session.CreateQuery("Update User set ChangePasswordKey=:ChangePasswordKey, IsKeyUsed=0 where Id = :userid")
                                  .SetParameter("userid", Userid)
                                  .SetParameter("ChangePasswordKey", ChangePasswordKey)
                                  .ExecuteUpdate();
                        transaction.Commit();
                    }
                    catch { }
                }
            }
            return i;

        }

        // Added by Antima[5/1/2015]

        public int UpdateChangeEmailKey(Guid Userid, string ChangeEmailKey)
        {
            int i = 0;
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {

                    try
                    {
                        i = session.CreateQuery("Update User set ChangeEmailKey=:ChangeEmailKey  where Id = :userid")
                                  .SetParameter("userid", Userid)
                                  .SetParameter("ChangeEmailKey", ChangeEmailKey)
                                  .ExecuteUpdate();
                        transaction.Commit();


                    }
                    catch { }
                }
            }
            return i;

        }
        public int UpdateEmailId(Guid Id, string NewEmailId)
        {
            int i = 0;
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        i = session.CreateQuery("Update User set EmailId=:NewEmailId  where Id = :Id")
                                  .SetParameter("Id", Id)
                                  .SetParameter("NewEmailId", NewEmailId)
                                  .ExecuteUpdate();
                        transaction.Commit();
                    }
                    catch { }
                }
            }
            return i;
        }

        public int UpdateIsEmailKeyUsed(Guid Id, string ChangeEmailKey)
        {
            int i = 0;
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        int IsEmailKey = 1;
                        i = session.CreateQuery("Update User set IsEmailKeyUsed=:IsEmailKey  where Id = :Id and ChangeEmailKey =:ChangeEmailKey")
                                  .SetParameter("IsEmailKey", IsEmailKey)
                                  .SetParameter("Id", Id)
                                  .SetParameter("ChangeEmailKey", ChangeEmailKey)
                                  .ExecuteUpdate();
                        transaction.Commit();
                    }
                    catch { }
                }
            }
            return i;
        }

        //IsKeyUsed
        public int UpdateIsKeyUsed(Guid Id)
        {
            int i = 0;
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        i = session.CreateQuery("Update User set IsKeyUsed=1  where Id = :Id")
                                  .SetParameter("Id", Id)
                                  .ExecuteUpdate();
                        transaction.Commit();
                    }
                    catch { }
                }
            }
            return i;
        }

        public int UpdateEwalletAmount(Guid userid, string amount)
        {
            int i = 0;
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        i = session.CreateQuery("Update User set Ewallet=:ewallet where Id = :Id")
                                  .SetParameter("ewallet", amount)
                                  .SetParameter("Id", userid)
                                  .ExecuteUpdate();
                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        i = 0;
                    }
                }
            }
            return i;
        }


        public Domain.Socioboard.Domain.User GetUserInfoByCode(string code)
        {
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        NHibernate.IQuery query = session.CreateQuery("from User Where UserCode = : code")
                            .SetParameter("code", code);
                        return (Domain.Socioboard.Domain.User)query.UniqueResult();
                    }
                    catch (Exception ex)
                    {
                        return null;
                    }
                }
            }
        }


        public int UpdateCode(Guid userid, string code)
        {
            int i = 0;
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        i = session.CreateQuery("Update User set UserCode=:code where Id = :Id")
                                  .SetParameter("code", code)
                                  .SetParameter("Id", userid)
                                  .ExecuteUpdate();
                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        i = 0;
                    }
                }
            }
            return i;
        }

        public List<User> GetAllExpiredUser()
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction. 
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    //Proceed action to get Archive messages
                    // And return list of archive messages.
                    try
                    {
                        List<User> alsdata = session.CreateQuery("from User")
                                        .List<User>()
                                        .ToList<User>()
                                        .Where(e => e.ExpiryDate.Date == DateTime.Now.Date)
                                        .ToList<User>();
                        return alsdata;

                    }
                    catch (Exception ex)
                    {
                        Console.Write(ex.StackTrace);
                        return null;
                    }
                }//End using transaction  
            }//Using using session
        }


        //vikash [06/04/2015]
        public User getUserInfoForSocialLogin(string logintype)
        {
            List<User> lstUser = new List<User>();
            User user = new User();
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        lstUser = session.CreateQuery("from  User u where u.SocialLogin = : SocialLogin")
                        .SetParameter("SocialLogin", logintype).List<User>().ToList<User>();
                        user = lstUser[0];
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                        user = null;
                    }
                }
            }

            return user;
        }

        public int UpdateLastLoginTime(Guid UserId)
        {
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    int i = 0;
                    try
                    {
                        i = session.CreateQuery("Update User Set LastLoginTime = : LastLoginTime where Id=: UserId")
                            .SetParameter("LastLoginTime", DateTime.Now)
                            .SetParameter("UserId", UserId)
                            .ExecuteUpdate();
                        transaction.Commit();


                    }
                    catch (Exception ex)
                    {
                        return i;
                    }
                    return i;
                }
            }
            
        }
        public List<Domain.Socioboard.Domain.User> InactiveUser() {
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                try
                {
                    List<Domain.Socioboard.Domain.User> lstuser = session.Query<Domain.Socioboard.Domain.User>()
                                .Where(x => x.LastLoginTime <= DateTime.Now.AddDays(-7)).ToList();
                    return lstuser;
                }
                catch (Exception ex)
                {
                    return new List<Domain.Socioboard.Domain.User>();
                }
            }
        }

    }
}
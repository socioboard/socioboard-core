using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SocioBoard.Helper;
using SocioBoard.Domain;


namespace SocioBoard.Model
{
    public class AdminRepository
    {

        public static void Add(SocioBoard.Domain.Admin user)
        {
            try
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
            catch (Exception ex)
            {
                Console.WriteLine("Error : " + ex.StackTrace);
            }
        }
        public static void Update(SocioBoard.Domain.Admin user)
        {
            try
            {
                using (NHibernate.ISession session = SessionFactory.GetNewSession())
                {
                    using (NHibernate.ITransaction transaction = session.BeginTransaction())
                    {
                        try
                        {
                            int i = session.CreateQuery("Update Admin set Image =:profileurl, UserName =: username , TimeZone=:timezone,FirstName =:first,LastName =:last  where Id = :twtuserid")
                                      .SetParameter("twtuserid", user.Id)
                                      .SetParameter("profileurl", user.Image)
                                      .SetParameter("username", user.UserName)
                                      .SetParameter("timezone", user.TimeZone)
                                      .SetParameter("first", user.FirstName)
                                      .SetParameter("last", user.LastName)
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
                Console.WriteLine("Error : " + ex.StackTrace);
            }
        }
        public SocioBoard.Domain.Admin GetUserInfo(string UserName, string Password)
        {
            try
            {
                using (NHibernate.ISession session = SessionFactory.GetNewSession())
                {
                    using (NHibernate.ITransaction transaction = session.BeginTransaction())
                    {
                        try
                        {
                            NHibernate.IQuery query = session.CreateQuery("from Admin u where u.UserName = : email and u.Password= :password");
                            query.SetParameter("email", UserName);
                            query.SetParameter("password", Password);
                            SocioBoard.Domain.Admin result = (SocioBoard.Domain.Admin)query.UniqueResult();
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
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return null;
            }
        }
        public int ChangePassword(string newpassword, string oldpassword, string UserName)
        {
            try
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
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return 0;
            }
        }
    }
}
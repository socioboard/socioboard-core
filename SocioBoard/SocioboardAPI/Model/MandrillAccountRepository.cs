using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SocioBoard.Domain;
using SocioBoard.Helper;

namespace SocioBoard.Model
{
    public class MandrillAccountRepository
    {
          public string Add(MandrillAccount MandrillAccount)
        {
            string res = null;
            try
            {
                //Creates a database connection and opens up a session
                using (NHibernate.ISession session = SessionFactory.GetNewSession())
                {
                    //After Session creation, start Transaction.
                    using (NHibernate.ITransaction transaction = session.BeginTransaction())
                    {
                        //Proceed action, to add new row.
                        session.Save(MandrillAccount);
                        transaction.Commit();
                        res = "Added";
                    }//End transaction
                }//End session
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error : " + ex.StackTrace);
            }
            return res;
        }


        //  public List<Coupon> GetCouponByCouponCode(MandrillAccount MandrillAccount)
        //{
        //    List<MandrillAccount> res = new List<MandrillAccount>();
        //    try
        //    {
        //        //Creates a database connection and opens up a session
        //        using (NHibernate.ISession session = SessionFactory.GetNewSession())
        //        {
        //            //After Session creation, start Transaction.
        //            using (NHibernate.ITransaction transaction = session.BeginTransaction())
        //            {
        //                //Proceed action, to get coupon detail by coupon code.
        //                res = session.CreateQuery("from Coupon u where u.CouponCode = : couponCode")
        //                .SetParameter("couponCode", MandrillAccount.CouponCode)
        //                .List<Coupon>().ToList<Coupon>();
        //            }//End transaction
        //        }//End session
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine("Error : " + ex.StackTrace);
        //    }
        //    return res;
        //}


        /// <GetCouponByCouponId>
        /// Get the coupon detail by coupon Id.
        /// </summary>
        /// <param name="coupon">Set coupon value in a coupon Property Class and Pass the Object of coupon Class as a paremeter.(SocioBoard.Domain.Admin).</param>
        /// <returns>coupon values in the list of coupon properties classes.(List<Domain.Coupon>) </returns>
          public MandrillAccount GetMandrillAccountById(MandrillAccount MandrillAccount)
          {
              //Coupon res = null;
              List<MandrillAccount> res = new List<MandrillAccount>();
              MandrillAccount objMandrillAccount = new MandrillAccount();
              try
              {
                  //Creates a database connection and opens up a session
                  using (NHibernate.ISession session = SessionFactory.GetNewSession())
                  {
                      using (NHibernate.ITransaction transaction = session.BeginTransaction())
                      {
                          //Proceed action, to get coupon detail by coupon id.
                          res = session.CreateQuery("from MandrillAccount where Id= : id")
                          .SetParameter("id", MandrillAccount.Id)
                          .List<MandrillAccount>().ToList<MandrillAccount>();
                      }//End transaction
                  }//End session
                  objMandrillAccount = res[0];
              }
              catch (Exception ex)
              {
                  Console.WriteLine("Error : " + ex.StackTrace);
              }
              return objMandrillAccount;
          }

          public List<MandrillAccount> GetMandrillAccountByMandrillUsernamePassword(MandrillAccount MandrillAccount)
          {
              //Coupon res = null;
              List<MandrillAccount> res = new List<MandrillAccount>();
              try
              {
                  //Creates a database connection and opens up a session
                  using (NHibernate.ISession session = SessionFactory.GetNewSession())
                  {
                      using (NHibernate.ITransaction transaction = session.BeginTransaction())
                      {
                          //Proceed action, to get coupon detail by coupon id.
                          res = session.CreateQuery("from MandrillAccount where UserName= : uname and Password=:pwd")
                          .SetParameter("uname", MandrillAccount.UserName)
                          .SetParameter("pwd", MandrillAccount.UserName)
                          .List<MandrillAccount>().ToList<MandrillAccount>();
                      }//End transaction
                  }//End session
                 
              }
              catch (Exception ex)
              {
                  Console.WriteLine("Error : " + ex.StackTrace);
              }
              return res;
          }

          public List<MandrillAccount> GetMandrillAccountByStatus(MandrillAccount MandrillAccount)
          {
              //Coupon res = null;
              List<MandrillAccount> res = new List<MandrillAccount>();
              try
              {
                  //Creates a database connection and opens up a session
                  using (NHibernate.ISession session = SessionFactory.GetNewSession())
                  {
                      using (NHibernate.ITransaction transaction = session.BeginTransaction())
                      {
                          //Proceed action, to get coupon detail by coupon id.
                          res = session.CreateQuery("from MandrillAccount where UserName= : uname and Password=:pwd and Status=:status")
                          .SetParameter("uname", MandrillAccount.UserName)
                          .SetParameter("pwd", MandrillAccount.UserName)
                           .SetParameter("status", MandrillAccount.Status)
                          .List<MandrillAccount>().ToList<MandrillAccount>();
                      }//End transaction
                  }//End session

              }
              catch (Exception ex)
              {
                  Console.WriteLine("Error : " + ex.StackTrace);
              }
              return res;
          }

          public int UpdateMandrillAccount(MandrillAccount MandrillAccount)
          {
              int res = 0;

              try
              {
                  //Creates a database connection and opens up a session
                  using (NHibernate.ISession session = SessionFactory.GetNewSession())
                  {
                      using (NHibernate.ITransaction transaction = session.BeginTransaction())
                      {
                          //Proceed action, to update existing data by coupon id.
                          res = session.CreateQuery("Update MandrillAccount set UserName=:username,Password=:pwd,Status=:status where Id = :id")
                                     .SetParameter("id", MandrillAccount.Id)
                                     .SetParameter("username", MandrillAccount.UserName)
                                     .SetParameter("pwd", MandrillAccount.Password)
                                     .SetParameter("status", MandrillAccount.Status)
                                     .ExecuteUpdate();
                          transaction.Commit();

                      }//End transaction
                  }//End session
              }
              catch (Exception ex)
              {
                  Console.WriteLine("Error : " + ex.StackTrace);
              }
              return res;
          }






        /// <GetAllCoupon>
        /// Get the all Coupons
        /// </summary>
        /// <returns>List of coupons.(List<Domain.Coupon>)</returns>
          public List<MandrillAccount> GetAllMandrillAccount()
        {

            List<MandrillAccount> res = new List<MandrillAccount>();
            try
            {
                //Creates a database connection and opens up a session
                using (NHibernate.ISession session = SessionFactory.GetNewSession())
                {
                    //Begin trasaction and opens up a trasaction.
                    using (NHibernate.ITransaction transaction = session.BeginTransaction())
                    {
                        //Proceed action to get all coupons.
                        res = session.CreateQuery("from MandrillAccount").List<MandrillAccount>().ToList<MandrillAccount>();

                    }//End transaction
                }//End session
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error : " + ex.StackTrace);
            }
            return res;
        }


      
    }
}
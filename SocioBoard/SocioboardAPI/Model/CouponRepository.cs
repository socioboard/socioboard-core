using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Domain.Socioboard.Domain;
using Api.Socioboard.Helper;

namespace Api.Socioboard.Services
{
    public class CouponRepository
    {

        /// <Add>
        /// Add new Coupon. 
        /// </summary>
        /// <param name="user">Set Values in a coupon Class Property and Pass the Object of coupon Class (SocioBoard.Domain.Admin).</param>
        /// <returns>When successfully added it return Added otherwise  it return null.</returns>
        public string Add(Domain.Socioboard.Domain.Coupon coupon)
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
                        session.Save(coupon);
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


        /// <GetCouponByCouponCode>
        /// Get the coupon by coupon code
        /// </summary>
        /// <param name="coupon">Set coupon value in a coupon Property Class and Pass the Object of coupon Class as a paremeter.(SocioBoard.Domain.Admin).</param>
        /// <returns>coupon values in the list of coupon properies class object.(List<Coupon>) </returns>
        public List<Domain.Socioboard.Domain.Coupon> GetCouponByCouponCode(Domain.Socioboard.Domain.Coupon coupon)
        {
            List<Domain.Socioboard.Domain.Coupon> res = new List<Domain.Socioboard.Domain.Coupon>();
            try
            {
                //Creates a database connection and opens up a session
                using (NHibernate.ISession session = SessionFactory.GetNewSession())
                {
                    //After Session creation, start Transaction.
                    using (NHibernate.ITransaction transaction = session.BeginTransaction())
                    {
                        //Proceed action, to get coupon detail by coupon code.
                        res = session.CreateQuery("from Coupon u where u.CouponCode = : couponCode")
                        .SetParameter("couponCode", coupon.CouponCode)
                        .List<Domain.Socioboard.Domain.Coupon>().ToList<Domain.Socioboard.Domain.Coupon>();
                    }//End transaction
                }//End session
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error : " + ex.StackTrace);
            }
            return res;
        }


        /// <GetCouponByCouponId>
        /// Get the coupon detail by coupon Id.
        /// </summary>
        /// <param name="coupon">Set coupon value in a coupon Property Class and Pass the Object of coupon Class as a paremeter.(SocioBoard.Domain.Admin).</param>
        /// <returns>coupon values in the list of coupon properties classes.(List<Domain.Coupon>) </returns>
        public List<Domain.Socioboard.Domain.Coupon> GetCouponByCouponId(Domain.Socioboard.Domain.Coupon coupon)
        {
            //Coupon res = null;
            List<Domain.Socioboard.Domain.Coupon> res = new List<Domain.Socioboard.Domain.Coupon>();
            try
            {
                //Creates a database connection and opens up a session
                using (NHibernate.ISession session = SessionFactory.GetNewSession())
                {
                    using (NHibernate.ITransaction transaction = session.BeginTransaction())
                    {
                        //Proceed action, to get coupon detail by coupon id.
                        res = session.CreateQuery("from Coupon u where u.Id = : id")
                        .SetParameter("id", coupon.Id)
                        .List<Domain.Socioboard.Domain.Coupon>().ToList<Domain.Socioboard.Domain.Coupon>();
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
        public List<Domain.Socioboard.Domain.Coupon> GetAllCoupon()
        {

            List<Domain.Socioboard.Domain.Coupon> res = new List<Domain.Socioboard.Domain.Coupon>();
            try
            {
                //Creates a database connection and opens up a session
                using (NHibernate.ISession session = SessionFactory.GetNewSession())
                {
                    //Begin trasaction and opens up a trasaction.
                    using (NHibernate.ITransaction transaction = session.BeginTransaction())
                    {
                        //Proceed action to get all coupons.
                        res = session.CreateQuery("from Coupon").List<Domain.Socioboard.Domain.Coupon>().ToList<Domain.Socioboard.Domain.Coupon>();

                    }//End transaction
                }//End session
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error : " + ex.StackTrace);
            }
            return res;
        }


        /// <SetCouponById>
        /// Update coupon by coupon id (Guid).
        /// </summary>
        /// <param name="coupon">Set coupon value in a coupon Property Class and Pass the Object of coupon Class as a paremeter.(SocioBoard.Domain.Admin).</param>
        /// <returns>1 for success and 0 for fail</returns>
        //public int SetCouponById(Domain.Socioboard.Domain.Coupon coupon)
        //{
        //    int res = 0;
            
        //    try
        //    {
        //        //Creates a database connection and opens up a session
        //        using (NHibernate.ISession session = SessionFactory.GetNewSession())
        //        {
        //            using (NHibernate.ITransaction transaction = session.BeginTransaction())
        //            {
        //                //Proceed action, to update existing data by coupon id.
        //                res = session.CreateQuery("Update Coupon set CouponCode=:couponCode,EntryCouponDate=:entryCouponDate,ExpCouponDate=:expCouponDate,Status=:status where Id = :id")
        //                           .SetParameter("couponCode", coupon.CouponCode)
        //                           .SetParameter("entryCouponDate", coupon.EntryCouponDate)
        //                           .SetParameter("expCouponDate", coupon.ExpCouponDate)
        //                           .SetParameter("status", coupon.Status)
        //                           .SetParameter("id", coupon.Id)
        //                           .ExecuteUpdate();
        //                transaction.Commit();

        //            }//End transaction
        //        }//End session
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine("Error : " + ex.StackTrace);
        //    }
        //    return res;
        //}

        public int SetCouponById(Domain.Socioboard.Domain.Coupon coupon)
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
                        res = session.CreateQuery("Update Coupon set EntryCouponDate=:entryCouponDate,ExpCouponDate=:expCouponDate,Status=:status where CouponCode=:couponCode")
                                   .SetParameter("couponCode", coupon.CouponCode)
                                   .SetParameter("entryCouponDate", coupon.EntryCouponDate)
                                   .SetParameter("expCouponDate", coupon.ExpCouponDate)
                                   .SetParameter("status", coupon.Status)
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
    }



}
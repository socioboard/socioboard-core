using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SocioBoard.Domain;
using SocioBoard.Helper;

namespace SocioBoard.Model
{
    public class CouponRepository
    {
        public string Add(Coupon coupon)
        {
            string res = null;
            try
            {
                using (NHibernate.ISession session = SessionFactory.GetNewSession())
                {
                    using (NHibernate.ITransaction transaction = session.BeginTransaction())
                    {
                        session.Save(coupon);
                        transaction.Commit();
                        res = "Added";
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error : " + ex.StackTrace);
            }
            return res;
        }

        public List<Coupon> GetCouponByCouponCode(Coupon coupon)
        {
            //Coupon res = null;
            List<Coupon> res = new List<Coupon>();
            try
            {
                using (NHibernate.ISession session = SessionFactory.GetNewSession())
                {
                    using (NHibernate.ITransaction transaction = session.BeginTransaction())
                    {
                        res = session.CreateQuery("from Coupon u where u.CouponCode = : couponCode")
                        .SetParameter("couponCode", coupon.CouponCode)
                        .List<Coupon>().ToList<Coupon>(); 
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error : " + ex.StackTrace);
            }
            return res;
        }

        public List<Coupon> GetCouponByCouponId(Coupon coupon)
        {
            //Coupon res = null;
            List<Coupon> res = new List<Coupon>();
            try
            {
                using (NHibernate.ISession session = SessionFactory.GetNewSession())
                {
                    using (NHibernate.ITransaction transaction = session.BeginTransaction())
                    {
                        res = session.CreateQuery("from Coupon u where u.Id = : id")
                        .SetParameter("id", coupon.Id)
                        .List<Coupon>().ToList<Coupon>();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error : " + ex.StackTrace);
            }
            return res;
        }
        public List<Coupon> GetAllCoupon()
        {

            List<Coupon> res = new List<Coupon>();
            try
            {
                using (NHibernate.ISession session = SessionFactory.GetNewSession())
                {
                    using (NHibernate.ITransaction transaction = session.BeginTransaction())
                    {
                        res = session.CreateQuery("from Coupon").List<Coupon>().ToList<Coupon>();
                        
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error : " + ex.StackTrace);
            }
            return res;
        }


        public int SetCouponById(Coupon coupon)
        {
            int res = 0;
            
            try
            {
                using (NHibernate.ISession session = SessionFactory.GetNewSession())
                {
                    using (NHibernate.ITransaction transaction = session.BeginTransaction())
                    {
                        res = session.CreateQuery("Update Coupon set CouponCode=:couponCode,EntryCouponDate=:entryCouponDate,ExpCouponDate=:expCouponDate,Status=:status where Id = :id")
                                   .SetParameter("couponCode", coupon.CouponCode)
                                   .SetParameter("entryCouponDate", coupon.EntryCouponDate)
                                   .SetParameter("expCouponDate", coupon.ExpCouponDate)
                                   .SetParameter("status", coupon.Status)
                                   .SetParameter("id", coupon.Id)
                                   .ExecuteUpdate();
                        transaction.Commit();

                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error : " + ex.StackTrace);
            }
            return res;
        }
    }
}
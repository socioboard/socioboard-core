using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SocioBoard
{
    public partial class EnterPriseSuccessPaypal : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //try
            //{
            //    if (Request.QueryString != null)
            //    {
            //        try
            //        {
            //            string custom = Request.QueryString["custom"];
            //        }
            //        catch (Exception ex)
            //        {
            //            Console.WriteLine("Error : " + ex.StackTrace);
            //        }
            //    }

            //    if (Request.Form != null || Request.Form!="{}")
            //    {
            //        successlink.InnerHtml = "<a href=\"#\">Click here to Download the zip code</a>";

            //        //Get O/P >> Request.Form {mc_gross=89.00&protection_eligibility=Eligible&address_status=confirmed&payer_id=HYNG9RCLH48WC&tax=0.00&address_street=1+Main+St&payment_date=04%3a05%3a11+Feb+14%2c+2014+PST&payment_status=Completed&charset=windows-1252&address_zip=95131&first_name=Babita&mc_fee=2.88&address_country_code=US&address_name=Babita+Sinha&notify_version=3.7&custom=256f9c69-6b6a-4409-a309-b1f6d1f8e43b&payer_status=unverified&business=pbpraveen%40globussoft.com&address_country=United+States&address_city=San+Jose&quantity=1&payer_email=babitasinha102%40yahoo.com&verify_sign=AudjwUiCo.wy3HNpdy6W2f1OTj7HAMzUhH.XfOvEQoXh3Jg8DE1dsZLc&txn_id=20X29418LJ642962P&payment_type=instant&last_name=Sinha&address_state=CA&receiver_email=pbpraveen%40globussoft.com&payment_fee=2.88&receiver_id=AF2RVCTNXRVHA&txn_type=web_accept&item_name=Deluxe&mc_currency=USD&item_number=&residence_country=US&test_ipn=1&handling_amount=0.00&transaction_subject=256f9c69-6b6a-4409-a309-b1f6d1f8e43b&payment_gross=89.00&shipping=0.00&merchant_return_link=click+here&auth=AWBkWTCIt.vP.rsV.Pgb3ZpjH10upSH98oRXgsj.ZmWOGUNmMf50qaZ4Jq.rEcQNtFpYp0DJpbStsLJlkXfYxig}

            //        //Guid custom = Guid.Parse(Request.Form["custom"].ToString());
            //        //int res = SetPaymentStatus(custom);

            //        //UserRepository objUserRepository = new UserRepository();

            //        //Session["LoggedUser"] = objUserRepository.getUsersById(custom);

            //        //Response.Redirect("Home.aspx");
            //    }


            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine("Error : " + ex.StackTrace);
            //}
        }
    }
}
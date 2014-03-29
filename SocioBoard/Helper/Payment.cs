using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SocioBoard.Helper
{
    public class Payment
    {
        public string PayWithPayPal(string amount, string itemInfo, string name, string phone, string email, string currency, string paypalemail, string successUrl, string failUrl, string callBackUrl, string cancelurl,string notifyurl,string custom)
        {
            string redirecturl = "";

            try
            {

                //Mention URL to redirect content to paypal site
                redirecturl += "https://www.paypal.com/cgi-bin/webscr?cmd=_xclick&business=" +
                                       paypalemail;

                //redirecturl += "https://www.sandbox.paypal.com/cgi-bin/webscr?cmd=_xclick&business=" +
                //                       paypalemail;

                //First name i assign static based on login details assign this value
                redirecturl += "&first_name=" + name;

                redirecturl += "&rm=2";
                //City i assign static based on login user detail you change this value
                //  redirecturl += "&city=bhubaneswar";

                //State i assign static based on login user detail you change this value
                //  redirecturl += "&state=Odisha";

                //Product Name
                redirecturl += "&item_name=" + itemInfo;

                //Product Name
                redirecturl += "&amount=" + amount;

                //Phone No
                redirecturl += "&night_phone_a=" + phone;

                //Product Name
                //            redirecturl += "&item_name=" + itemInfo;

                //Address 
                redirecturl += "&address1=" + email;

                //Business contact id
                // redirecturl += "&business=k.tapankumar@gmail.com";

                //Shipping charges if any
                redirecturl += "&shipping=0";

                //Handling charges if any
                redirecturl += "&handling=0";

                //Tax amount if any
                redirecturl += "&tax=0";

                //Add quatity i added one only statically 
                redirecturl += "&quantity=1";

                //Currency code 
                redirecturl += "&currency=" + currency;

                //Success return page url
                redirecturl += "&return=" +
                              callBackUrl;
                //Failed return page url
                redirecturl += "&cancel_return=" +
                             cancelurl;
                redirecturl += "&notify_url=" + notifyurl;

                redirecturl += "&custom=" + custom;
                
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error : " + ex.StackTrace);
            }
                return   redirecturl;
        }

        /// <summary>
        /// Recurring Payment With Paypal By Ajay Pandey
        /// </summary>
        /// <param name="amount"></param>
        /// <param name="itemInfo"></param>
        /// <param name="name"></param>
        /// <param name="phone"></param>
        /// <param name="email"></param>
        /// <param name="currency"></param>
        /// <param name="paypalemail"></param>
        /// <param name="successUrl"></param>
        /// <param name="failUrl"></param>
        /// <param name="callBackUrl"></param>
        /// <param name="cancelurl"></param>
        /// <param name="notifyurl"></param>
        /// <param name="custom"></param>
        /// <returns></returns>
        public string RecurringPaymentWithPayPal(string amount, string itemInfo, string name, string phone, string email, string currency, string paypalemail, string successUrl, string failUrl, string callBackUrl, string cancelurl, string notifyurl, string custom)
        {
            string redirecturl = "";

            try
            {

                #region Format of Parameters for Recurring Payment With Paypal

                // Reference Url >> https://www.paypal.com/in/cgi-bin/webscr?cmd=_pdn_subscr_techview_outside

                //"<form action='$url' method='post' name='frmPayPal'>\n".
                //"<input type='hidden' name='business' value='$ppAcc'>\n".
                //"<input type='hidden' name='custom' value='$orderId'>\n".
                //"<input type='hidden' name='cmd' value='_xclick-subscriptions'>\n".
                //"<input type='hidden' name='item_name' value='$itemName'>\n".
                //"<input type='hidden' name='item_number' value='$orderno'>\n".
                //"<input type='hidden' name='amount' value='$nettotal'>\n".
                //"<input type='hidden' name='currency_code' value='USD'>\n".
                //"<input type='hidden' name='cancel_return' value='$cancelURL'>\n".
                //"<input type='hidden' name='notify_url' value='$notifyUrl'>\n".
                //"<input type='hidden' name='return' value='$returnURL'>\n".
                // //Subscription Params       
                //"<input type='hidden' name='a3' value=$nettotal>".
                //"<input type='hidden' name='p3' value='1'>". 
                //"<input type='hidden' name='t3' value='M'>". 

                ////<!-- Set recurring payments until canceled. -->  
                //"<input type='hidden' name='src' value='1'>".
                ////<!-- Set recurring payments Retry if Failed  -->
                //"<input type='hidden' name='sra' value='1'>".
                //"</form>\n". 
                #endregion

                //Mention URL to redirect content to paypal site
                //redirecturl += "https://www.sandbox.paypal.com/cgi-bin/webscr?cmd=_xclick-subscriptions&business=" +
                //                       paypalemail;

                redirecturl += "https://www.paypal.com/cgi-bin/webscr?cmd=_xclick-subscriptions&business=" +
                                      paypalemail;

                //First name i assign static based on login details assign this value
                redirecturl += "&first_name=" + name;

                //redirecturl += "&rm=2";
                //City i assign static based on login user detail you change this value
                //  redirecturl += "&city=bhubaneswar";

                //State i assign static based on login user detail you change this value
                //  redirecturl += "&state=Odisha";

                //Product Name
                redirecturl += "&item_name=" + itemInfo;

                //item_number
                redirecturl += "&item_number=" + 1;

                //Product Name
                redirecturl += "&amount=" + amount;

                //Phone No
                redirecturl += "&night_phone_a=" + phone;

                //Product Name
                //            redirecturl += "&item_name=" + itemInfo;

                //Address 
                redirecturl += "&address1=" + email;

                //Business contact id
                // redirecturl += "&business=k.tapankumar@gmail.com";

                //Shipping charges if any
                redirecturl += "&shipping=0";

                //Handling charges if any
                redirecturl += "&handling=0";

                //Tax amount if any
                redirecturl += "&tax=0";

                //Add quatity i added one only statically 
                redirecturl += "&quantity=1";

                //Currency code 
                //redirecturl += "&currency=" + currency;

                redirecturl += "&currency_code=" + currency;

                

                //Success return page url
                redirecturl += "&return=" +
                              callBackUrl;
                //Failed return page url
                redirecturl += "&cancel_return=" +
                             cancelurl;
                redirecturl += "&notify_url=" + notifyurl;

                redirecturl += "&custom=" + custom;


                 //Subscription Params   
    
                //"<input type='hidden' name='a3' value=$nettotal>".
                //"<input type='hidden' name='p3' value='1'>". 
                //"<input type='hidden' name='t3' value='M'>". 


                redirecturl += "&a3="+amount;
                redirecturl += "&p3=1";

                redirecturl += "&t3=M";


                //<!-- Set recurring payments until canceled. -->  

                //"<input type='hidden' name='src' value='1'>".

                redirecturl += "&src=1";

                //<!-- Set recurring payments Retry if Failed  -->

                //"<input type='hidden' name='sra' value='1'>".
                
                redirecturl += "&sra=3";

                

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error : " + ex.StackTrace);
            }
            return redirecturl;
        }

    }
}
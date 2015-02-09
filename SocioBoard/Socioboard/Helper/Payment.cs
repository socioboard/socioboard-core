using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Socioboard.Helper
{
    public class Payment
    {
        public string PayWithPayPal(string amount, string itemInfo, string name, string phone, string email, string currency, string paypalemail, string successUrl, string failUrl, string callBackUrl, string cancelurl, string notifyurl, string custom)
        {
            string redirecturl = "";

            try
            {

                //Mention URL to redirect content to paypal site
                //redirecturl += "https://www.paypal.com/cgi-bin/webscr?cmd=_xclick&business=" +
                //                       paypalemail;

                redirecturl += ConfigurationManager.AppSettings["PaypalURL"] + "/cgi-bin/webscr?cmd=_xclick&business=" +
                paypalemail;

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
            return redirecturl;
        }

        public static int ReferedAmountDetails(string plan)
        {
            int bonous = 0;
            if (plan == "FREE" || plan == "Free") {
                bonous = 20;
            }
            else if (plan == "Standard") {
                bonous = 30;
            }
            else if (plan == "Premium") {
                bonous = 40;
            }
            else if (plan == "Deluxe") {
                bonous = 40;
            }
            else if (plan == "SocioBasic") {
                bonous = 40;
            }
            else if (plan == "SocioStandard") {
                bonous = 40;
            }
            else if (plan == "SocioPremium") {
                bonous = 40;
            }
            else if (plan == "SocioDeluxe") {
                bonous = 40;
            }

            return bonous;
        }
    }
}
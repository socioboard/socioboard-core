using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Script.Services;
using System.Web.Services;

namespace Api.Socioboard.Services
{
    /// <summary>
    /// Summary description for Group
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [ScriptService]
    public class FacebookFanPage : System.Web.Services.WebService
    {
        FacebookFanPageRepository objrepo = new FacebookFanPageRepository();
      


      
        public int FacebookFans(string userid, string profileid, string days)
        {
            int counts = 0;
            int count = 0;
            int Totalcount = 0;
            Domain.Socioboard.Domain.FacebookFanPage lstFacebookFanPage = new Domain.Socioboard.Domain.FacebookFanPage();
            Domain.Socioboard.Domain.FacebookFanPage lstFacebookFancount = new Domain.Socioboard.Domain.FacebookFanPage();
            try
            {
                string[] arr = profileid.Split(',');
                foreach (var item in arr)
                {
                    counts += objrepo.getAllFancountDetail(Guid.Parse(userid), item, Convert.ToInt32(days));
                    count += objrepo.getAllFancountDetailbeforedays(Guid.Parse(userid), item, Convert.ToInt32(days));
                }                     
            }             
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
            }
            Totalcount = Math.Abs(counts - count);
            return Totalcount;
  
        }







    }
}

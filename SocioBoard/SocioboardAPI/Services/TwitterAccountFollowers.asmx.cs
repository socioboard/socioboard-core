using Api.Socioboard.Helper;
using Api.Socioboard.Model;
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
    public class TwitterAccountFollowers : System.Web.Services.WebService
    {

        TwitterAccountFollowersRepository objrepo = new TwitterAccountFollowersRepository();
        public int FollowerCount(string userid, string profileid, string days)
        {
            int counts = 0;
            int count = 0;
            int Totalcount = 0;
           Domain.Socioboard.Domain.TwitterAccountFollowers lstcount = new Domain.Socioboard.Domain.TwitterAccountFollowers();
           Domain.Socioboard.Domain.TwitterAccountFollowers lstcountbefore = new Domain.Socioboard.Domain.TwitterAccountFollowers();
            try
            {
                string[] arr = profileid.Split(',');
                foreach (var item in arr)
                {
                    counts  += objrepo.getAllFollower1(Guid.Parse(userid), item, Convert.ToInt32(days));
                    count  += objrepo.getAllFollowerbeforedays1(Guid.Parse(userid), item, Convert.ToInt32(days));

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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocioboardDataServices
{
    public interface ISocialSiteData
    {
        string GetData(object UserId,string profileid);

        void GetSearchData(object parameters);
    }
}

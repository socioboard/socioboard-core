using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain.Socioboard.Domain
{
    public interface ISocialSite
    {
        string ProfileType { get; set; }

        //ISocialSiteAccount BindJArray(JObject jObject);
    }
}

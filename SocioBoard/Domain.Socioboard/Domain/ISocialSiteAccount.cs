using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Domain.Socioboard.Domain
{
    public interface ISocialSiteAccount
    {
        string ProfileType { get; set; }

        //ISocialSiteAccount BindJArray(JObject jObject);
       
    }
}

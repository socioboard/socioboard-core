using Socioboard.Api.Linkedin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Socioboard.Helper
{
    public class AddliPage
    {
        public string PageId { get; set; }
        public string PageName { get; set; }
        public oAuthLinkedIn _Oauth { get; set; }
    }
}
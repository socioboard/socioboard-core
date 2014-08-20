using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GlobusInstagramLib.Authentication;
using GlobusInstagramLib.Instagram.Core.GeographiesMethods;

namespace GlobusInstagramLib.App.Core
{
    class GeographiesController
    {
        public InstagramMedia[] GeographyMedia(string geographyid, string accessToken)
        {
            Geographies objGeo = new Geographies();
            return objGeo.GeographyMedia(geographyid, accessToken);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using GlobusGooglePlusLib.Authentication;
using System.Configuration;
using GlobusGooglePlusLib.Gplus.Core.PeopleMethod;
using Newtonsoft.Json.Linq;
using GlobusGooglePlusLib.App.Core;
using SocioBoard.Model;
using SocioBoard.Domain;
using SocioBoard.Helper;

namespace SocioBoard
{
    public partial class GplusManager : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            oAuthToken objToken = new oAuthToken();
            GplusHelper objGpHelper = new GplusHelper();
            string objRefresh= objToken.GetRefreshToken(Request.QueryString["code"]);






            if (!objRefresh.StartsWith("["))
                objRefresh = "[" + objRefresh + "]";
            JArray objArray= JArray.Parse(objRefresh);
            User user=(User)Session["LoggedUser"];
            foreach (var item in objArray)
            {
               //string objAccess = objToken.GetAccessToken(item["refresh_token"].ToString());
                //if (!objAccess.StartsWith("["))
                //    objAccess = "[" + objAccess + "]";
                //JArray objArrayAccess = JArray.Parse(objAccess);
             //   objGpHelper.GetUerProfile(item["access_token"].ToString(), item["refresh_token"].ToString(), user.Id);
              
            }
        }
    }
}
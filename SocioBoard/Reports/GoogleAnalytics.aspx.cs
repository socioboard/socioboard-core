using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SocioBoard;
using System.Collections;
using SocioBoard.Model;
using SocioBoard.Domain;
using System.Data;
using GlobusGooglePlusLib.GAnalytics.Core.Accounts;
using SocioBoard.Helper;

namespace SocialSuitePro.Reports
{
    public partial class GoogleAnalytics : System.Web.UI.Page
    {
        public string strCntryVal = string.Empty;
        public string strYearVal = string.Empty;
        public string strdurationVal = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                User user = (User)HttpContext.Current.Session["LoggedUser"];
             
                // objGaHelper.getAnalytics();
                GoogleAnalyticsStatsRepository objGaStatsRepo = new GoogleAnalyticsStatsRepository();
                GoogleAnalyticsAccountRepository objGaAccRepo = new GoogleAnalyticsAccountRepository();
                ArrayList arrAcc = objGaAccRepo.getGoogelAnalyticsAccountsOfUser(user.Id);
                foreach (var item in arrAcc)
                {
                    Array temp = (Array)item;
                    //objGaHelper.getYearWiseAnalyticsApi(item);
                    ddlAccounts.Items.Add(new ListItem(temp.GetValue(1).ToString(), temp.GetValue(0).ToString()));
                }
                getProfilesofAccount(ddlAccounts.SelectedValue);
                getanalytics();
                //ddlAccounts.Items.Insert(0, "--Select--");
            }
        }

        public void getProfilesofAccount(string accountid)
        {
            GoogleAnalyticsAccountRepository objGaAccRepo = new GoogleAnalyticsAccountRepository();
              User user = (User)HttpContext.Current.Session["LoggedUser"];
           // if (ddlAccounts.SelectedIndex > -1)
           // {
                ArrayList arrProfile = objGaAccRepo.getGoogelAnalyticsProfilesOfUser(accountid, user.Id);
                foreach(GoogleAnalyticsAccount item in arrProfile)
                {
                    ddlProfile.Items.Add(new ListItem(item.GaProfileName, item.GaProfileId));
                }
               // ddlProfile.Items.Insert(0, "--Select--");
                
          //  }
        }
        protected void btnAnalytics_Click(object sender, ImageClickEventArgs e)
        {
            getanalytics();
        }
        protected void ddlAccounts_SelectedIndexChanged(object sender, EventArgs e)
        {
            getProfilesofAccount(ddlAccounts.SelectedValue);
        }
        protected void getanalytics()
        {
            GanalyticsHelper objGaHelper = new GanalyticsHelper();
            User user = (User)HttpContext.Current.Session["LoggedUser"];
            try
            {
                strCntryVal = objGaHelper.getCountryWiseAnalytics(ddlProfile.SelectedValue);
                if (ddlPeriod.SelectedItem.ToString() == "Day")
                    strYearVal = objGaHelper.getDayWiseAnalytics(ddlProfile.SelectedValue);
                else if (ddlPeriod.SelectedItem.ToString() == "Month")
                    strYearVal = objGaHelper.getMonthWiseAnalytics(ddlProfile.SelectedValue);
                else if (ddlPeriod.SelectedItem.ToString() == "Year")
                    strYearVal = objGaHelper.getYearWiseAnalytics(ddlProfile.SelectedValue);
                strdurationVal = ddlPeriod.SelectedItem.ToString();
                //  strYearVal = objGaHelper.getYearWiseAnalytics(ddlProfile.SelectedValue);
                //DataTable strmonth=objGaHelper.getMonthWiseAnalyticsApi(ddlProfile.SelectedValue,user.Id);
                // DataTable strday = objGaHelper.getDayWiseAnalyticsApi(ddlProfile.SelectedValue, user.Id);
            }
            catch (Exception Err)
            {
                Console.Write(Err.StackTrace);
            }
        }
    }
}
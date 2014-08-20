using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using SocioBoard.Domain;
using SocioBoard.Model;
using GlobusGooglePlusLib.GAnalytics.Core.AnalyticsMethod;
using System.Collections;
using Newtonsoft.Json.Linq;
using GlobusGooglePlusLib.Authentication;
using System.Xml;

namespace SocioBoard.Helper
{
    public class GanalyticsHelper
    {
        
        public DataTable getCountryAnalyticsApi(string profileId,Guid user)
        {
            DataTable dtAnalytics = new DataTable();
            try
            {
               
                Analytics objAlyt = new Analytics();
                oAuthTokenGa obj = new oAuthTokenGa();
                GoogleAnalyticsAccountRepository objGaAccRepo=new GoogleAnalyticsAccountRepository();
                GoogleAnalyticsStats objGaStats = new GoogleAnalyticsStats();
                GoogleAnalyticsStatsRepository objGaStatsRepo=new GoogleAnalyticsStatsRepository();
                GoogleAnalyticsAccount objGaAcc = objGaAccRepo.getGoogelAnalyticsAccountHomeDetailsById(profileId, user);

                string strRefresh = obj.GetAccessToken(objGaAcc.RefreshToken);
                    if (!strRefresh.StartsWith("["))
                        strRefresh = "[" + strRefresh + "]";
                    JArray objArray = JArray.Parse(strRefresh);
                    foreach (var itemRefresh in objArray)
                    {
                        objGaAcc.AccessToken = itemRefresh["access_token"].ToString();
                    }
                    XmlDocument xDoc = new XmlDocument();
                    xDoc.LoadXml(objAlyt.getAnalyticsData(objGaAcc.GaProfileId, "metrics=ga:visits&dimensions=ga:country", "2013-01-01", "2013-07-15", objGaAcc.AccessToken));
                    dtAnalytics= getdatafromXml(xDoc);
                    for (int i = 0; i < dtAnalytics.Rows.Count; i++)
                    {
                        objGaStats.EntryDate = DateTime.Now;
                        objGaStats.GaAccountId = objGaAcc.GaAccountId;
                        if (dtAnalytics.Rows[i]["title"].ToString().Contains("ga:country"))
                            objGaStats.gaCountry = dtAnalytics.Rows[i]["title"].ToString().Substring(11);
                        objGaStats.gaVisits = dtAnalytics.Rows[i]["ga:visitors"].ToString();
                        objGaStats.GaProfileId = objGaAcc.GaProfileId;
                        objGaStats.Id = Guid.NewGuid();
                        objGaStats.UserId = user;
                        if (!objGaStatsRepo.checkGoogleAnalyticsDateStatsExists(objGaAcc.GaProfileId, "country", dtAnalytics.Rows[i]["title"].ToString(), user))
                            objGaStatsRepo.addGoogleAnalyticsStats(objGaStats);
                        else
                            objGaStatsRepo.updateGoogleAnalyticsStats(objGaStats);
                    }
            }
            catch (Exception Err)
            {
                Console.Write(Err.StackTrace);
            }
            return dtAnalytics;
        }

        public DataTable getYearWiseAnalyticsApi(string profileId,Guid user)
        {
            DataTable dtAnalytics = new DataTable();
            try
            {
               
                Analytics objAlyt = new Analytics();
                oAuthTokenGa obj = new oAuthTokenGa();
                GoogleAnalyticsAccountRepository objGaAccRepo = new GoogleAnalyticsAccountRepository();
                GoogleAnalyticsStats objGaStats = new GoogleAnalyticsStats();
                GoogleAnalyticsStatsRepository objGaStatsRepo = new GoogleAnalyticsStatsRepository();
                GoogleAnalyticsAccount objGaAcc = objGaAccRepo.getGoogelAnalyticsAccountDetailsById(profileId, user);
                string strRefresh = obj.GetAccessToken(objGaAcc.RefreshToken);
                    if (!strRefresh.StartsWith("["))
                        strRefresh = "[" + strRefresh + "]";
                    JArray objArray = JArray.Parse(strRefresh);
                    foreach (var itemRefresh in objArray)
                    {
                        objGaAcc.AccessToken = itemRefresh["access_token"].ToString();
                    }
                    XmlDocument xDoc = new XmlDocument();
                    xDoc.LoadXml(objAlyt.getAnalyticsData(objGaAcc.GaProfileId, "metrics=ga:visits&dimensions=ga:year", "2013-01-01", "2013-07-15", objGaAcc.AccessToken));
                    dtAnalytics = getdatafromXml(xDoc);
                    for (int i = 0; i < dtAnalytics.Rows.Count; i++)
                    {
                        objGaStats.EntryDate = DateTime.Now;
                        objGaStats.GaAccountId = objGaAcc.GaAccountId;
                        if (dtAnalytics.Rows[i]["title"].ToString().Contains("ga:year"))
                            objGaStats.gaYear = dtAnalytics.Rows[i]["title"].ToString().Substring(8);
                        objGaStats.gaVisits = dtAnalytics.Rows[i]["ga:visitors"].ToString();
                        objGaStats.Id = Guid.NewGuid();
                        objGaStats.UserId = user;
                        objGaStats.GaProfileId = objGaAcc.GaProfileId;
                        if (!objGaStatsRepo.checkGoogleAnalyticsDateStatsExists(objGaAcc.GaProfileId, "year", dtAnalytics.Rows[i]["title"].ToString(), user))
                            objGaStatsRepo.addGoogleAnalyticsStats(objGaStats);
                        else
                            objGaStatsRepo.updateGoogleAnalyticsStats(objGaStats);
                    }
                }
            catch (Exception Err)
            {
                Console.Write(Err.StackTrace);
            }
            return dtAnalytics;
        }

        public DataTable getMonthWiseAnalyticsApi(string profileId, Guid user)
        {
            DataTable dtAnalytics = new DataTable();
            try
            {

                Analytics objAlyt = new Analytics();
                oAuthTokenGa obj = new oAuthTokenGa();
                GoogleAnalyticsAccountRepository objGaAccRepo = new GoogleAnalyticsAccountRepository();
                GoogleAnalyticsStats objGaStats = new GoogleAnalyticsStats();
                GoogleAnalyticsStatsRepository objGaStatsRepo = new GoogleAnalyticsStatsRepository();
                GoogleAnalyticsAccount objGaAcc = objGaAccRepo.getGoogelAnalyticsAccountDetailsById(profileId, user);
                string strRefresh = obj.GetAccessToken(objGaAcc.RefreshToken);
                if (!strRefresh.StartsWith("["))
                    strRefresh = "[" + strRefresh + "]";
                JArray objArray = JArray.Parse(strRefresh);
                foreach (var itemRefresh in objArray)
                {
                    objGaAcc.AccessToken = itemRefresh["access_token"].ToString();
                }
                XmlDocument xDoc = new XmlDocument();
                DateTime startdt = DateTime.Now.AddMonths(-3);
                DateTime enddt = DateTime.Now;
                xDoc.LoadXml(objAlyt.getAnalyticsData(objGaAcc.GaProfileId, "metrics=ga:visits&dimensions=ga:month", "2013-01-01", "2013-07-25", objGaAcc.AccessToken));
                dtAnalytics = getdatafromXml(xDoc);
                for (int i = 0; i < dtAnalytics.Rows.Count; i++)
                {
                    objGaStats.EntryDate = DateTime.Now;
                    objGaStats.GaAccountId = objGaAcc.GaAccountId;
                    if (dtAnalytics.Rows[i]["title"].ToString().Contains("ga:month"))
                        objGaStats.gaMonth = dtAnalytics.Rows[i]["title"].ToString().Substring(9);
                    objGaStats.gaVisits = dtAnalytics.Rows[i]["ga:visitors"].ToString();
                    objGaStats.Id = Guid.NewGuid();
                    objGaStats.UserId = user;
                    objGaStats.GaProfileId = objGaAcc.GaProfileId;
                    if (!objGaStatsRepo.checkGoogleAnalyticsDateStatsExists(objGaAcc.GaProfileId, "month", dtAnalytics.Rows[i]["title"].ToString(), user))
                        objGaStatsRepo.addGoogleAnalyticsStats(objGaStats);
                    else
                        objGaStatsRepo.updateGoogleAnalyticsStats(objGaStats);
                }
            }
            catch (Exception Err)
            {
                Console.Write(Err.StackTrace);
            }
            return dtAnalytics;
        }

        public DataTable getDayWiseAnalyticsApi(string profileId, Guid user)
        {
            DataTable dtAnalytics = new DataTable();
            try
            {

                Analytics objAlyt = new Analytics();
                oAuthTokenGa obj = new oAuthTokenGa();
                GoogleAnalyticsAccountRepository objGaAccRepo = new GoogleAnalyticsAccountRepository();
                GoogleAnalyticsStats objGaStats = new GoogleAnalyticsStats();
                GoogleAnalyticsStatsRepository objGaStatsRepo = new GoogleAnalyticsStatsRepository();
                GoogleAnalyticsAccount objGaAcc = objGaAccRepo.getGoogelAnalyticsAccountDetailsById(profileId, user);
                string strRefresh = obj.GetAccessToken(objGaAcc.RefreshToken);
                if (!strRefresh.StartsWith("["))
                    strRefresh = "[" + strRefresh + "]";
                JArray objArray = JArray.Parse(strRefresh);
                foreach (var itemRefresh in objArray)
                {
                    objGaAcc.AccessToken = itemRefresh["access_token"].ToString();
                }
                XmlDocument xDoc = new XmlDocument();
                DateTime startdt = DateTime.Now.AddMonths(-3);
                DateTime enddt = DateTime.Now;
                xDoc.LoadXml(objAlyt.getAnalyticsData(objGaAcc.GaProfileId, "metrics=ga:visits&dimensions=ga:day", startdt.ToShortDateString().Replace("/","-"), "2013-07-25", objGaAcc.AccessToken));
                dtAnalytics = getdatafromXml(xDoc);
                for (int i = 0; i < dtAnalytics.Rows.Count; i++)
                {
                    objGaStats.EntryDate = DateTime.Now;
                    objGaStats.GaAccountId = objGaAcc.GaAccountId;
                    if (dtAnalytics.Rows[i]["title"].ToString().Contains("ga:day"))
                        objGaStats.gaDate = dtAnalytics.Rows[i]["title"].ToString().Substring(8);
                    objGaStats.gaVisits = dtAnalytics.Rows[i]["ga:visitors"].ToString();
                    objGaStats.Id = Guid.NewGuid();
                    objGaStats.UserId = user;
                    objGaStats.GaProfileId = objGaAcc.GaProfileId;
                    if (!objGaStatsRepo.checkGoogleAnalyticsDateStatsExists(objGaAcc.GaProfileId, "day", dtAnalytics.Rows[i]["title"].ToString(), user))
                        objGaStatsRepo.addGoogleAnalyticsStats(objGaStats);
                    else
                        objGaStatsRepo.updateGoogleAnalyticsStats(objGaStats);
                }
            }
            catch (Exception Err)
            {
                Console.Write(Err.StackTrace);
            }
            return dtAnalytics;
        }

        public DataTable getRegionWiseAnalyticsApi(string profileId)
        {
            DataTable dtAnalytics = new DataTable();
            try
            {
                User user = (User)HttpContext.Current.Session["LoggedUser"];
                Analytics objAlyt = new Analytics();
                oAuthTokenGa obj = new oAuthTokenGa();
                GoogleAnalyticsAccountRepository objGaAccRepo = new GoogleAnalyticsAccountRepository();
                GoogleAnalyticsStats objGaStats = new GoogleAnalyticsStats();
                GoogleAnalyticsStatsRepository objGaStatsRepo = new GoogleAnalyticsStatsRepository();
                GoogleAnalyticsAccount objGaAcc = objGaAccRepo.getGoogelAnalyticsAccountDetailsById(profileId, user.Id);
                string strRefresh = obj.GetAccessToken(objGaAcc.RefreshToken);
                if (!strRefresh.StartsWith("["))
                    strRefresh = "[" + strRefresh + "]";
                JArray objArray = JArray.Parse(strRefresh);
                foreach (var itemRefresh in objArray)
                {
                    objGaAcc.AccessToken = itemRefresh["access_token"].ToString();
                }
                XmlDocument xDoc = new XmlDocument();
                xDoc.LoadXml(objAlyt.getAnalyticsData(objGaAcc.GaProfileId, "metrics=ga:visits&dimensions=ga:year", "2013-01-01", "2013-07-15", objGaAcc.AccessToken));
                dtAnalytics = getdatafromXml(xDoc);
                for (int i = 0; i < dtAnalytics.Rows.Count; i++)
                {
                    objGaStats.EntryDate = DateTime.Now;
                    objGaStats.GaAccountId = objGaAcc.GaAccountId;
                    if (dtAnalytics.Rows[i]["title"].ToString().Contains("ga:year"))
                        objGaStats.gaYear = dtAnalytics.Rows[i]["title"].ToString().Substring(8);
                    objGaStats.gaVisits = dtAnalytics.Rows[i]["ga:visitors"].ToString();
                    objGaStats.Id = Guid.NewGuid();
                    objGaStats.UserId = user.Id;
                    objGaStats.GaProfileId = objGaAcc.GaProfileId;
                    if (!objGaStatsRepo.checkGoogleAnalyticsDateStatsExists(objGaAcc.GaProfileId, "year", dtAnalytics.Rows[i]["title"].ToString(), user.Id))
                        objGaStatsRepo.addGoogleAnalyticsStats(objGaStats);
                    else
                        objGaStatsRepo.updateGoogleAnalyticsStats(objGaStats);
                }
            }
            catch (Exception Err)
            {
                Console.Write(Err.StackTrace);
            }
            return dtAnalytics;
        }

        public string getCountryWiseAnalytics(string gaAccountId)
        {
            string strCountryVal = string.Empty;
            try
            {
                User user = (User)HttpContext.Current.Session["LoggedUser"];

                GoogleAnalyticsStats objGaStats = new GoogleAnalyticsStats();
                GoogleAnalyticsStatsRepository objGaStatsRepo = new GoogleAnalyticsStatsRepository();
                ArrayList arrCountry = objGaStatsRepo.getGoogleAnalyticsStatsById(gaAccountId, user.Id, 7);
               
                string strCountry = string.Empty;
                string strVal = string.Empty;
                int cnt = 0;
                foreach (var item in arrCountry)
                {
                    cnt++;
                    Array temp = (Array)item;
                    strCountry = strCountry + temp.GetValue(6).ToString() + ",";
                    strVal = strVal + temp.GetValue(8).ToString() + ",";
                }
                if (cnt < 7)
                {
                    for (int j = 0; j < 7 - cnt; j++)
                    {
                        strCountry = strCountry + "0,";
                        strVal = strVal + "0,";
                    }
                }
                strCountryVal = strCountry.Substring(0, strCountry.Length - 1) + "@" + strVal.Substring(0, strVal.Length - 1);
            }
            catch (Exception Err)
            {
                Console.Write(Err.StackTrace);
            }
            return strCountryVal;
        }

        public string getYearWiseAnalytics(string gaAccountId)
        {
            string strYearVal = string.Empty;
            try
            {
                User user = (User)HttpContext.Current.Session["LoggedUser"];

                GoogleAnalyticsStats objGaStats = new GoogleAnalyticsStats();
                GoogleAnalyticsStatsRepository objGaStatsRepo = new GoogleAnalyticsStatsRepository();
                ArrayList arrCountry = objGaStatsRepo.getGoogleAnalyticsStatsYearById(gaAccountId, user.Id, 7,"year");

                string strYear = string.Empty;
                string strVal = string.Empty;
                int cnt = 0;
                foreach (var item in arrCountry)
                {
                    cnt++;
                    Array temp = (Array)item;
                    strYear = strYear + temp.GetValue(5).ToString() + ",";
                    strVal = strVal + temp.GetValue(8).ToString() + ",";
                }
                if (cnt < 7)
                {
                    for (int j = 0; j < 7 - cnt; j++)
                    {
                        strYear = strYear + "0,";
                        strVal = strVal + "0,";
                    }
                }
                strYearVal = strYear.Substring(0, strYear.Length - 1) + "@" + strVal.Substring(0, strVal.Length - 1);
            }
            catch (Exception Err)
            {
                Console.Write(Err.StackTrace);
            }
            return strYearVal;
        }

        public string getMonthWiseAnalytics(string gaAccountId)
        {
            string strYearVal = string.Empty;
            try
            {
                User user = (User)HttpContext.Current.Session["LoggedUser"];

                GoogleAnalyticsStats objGaStats = new GoogleAnalyticsStats();
                GoogleAnalyticsStatsRepository objGaStatsRepo = new GoogleAnalyticsStatsRepository();
                ArrayList arrCountry = objGaStatsRepo.getGoogleAnalyticsStatsYearById(gaAccountId, user.Id, 7,"month");

                string strYear = string.Empty;
                string strVal = string.Empty;
                int cnt = 0;
                foreach (var item in arrCountry)
                {
                    cnt++;
                    Array temp = (Array)item;
                    strYear = strYear + temp.GetValue(4).ToString() + ",";
                    strVal = strVal + temp.GetValue(8).ToString() + ",";
                }
                if (cnt < 7)
                {
                    for (int j = 0; j < 7 - cnt; j++)
                    {
                        strYear = strYear + "0,";
                        strVal = strVal + "0,";
                    }
                }
                strYearVal = strYear.Substring(0, strYear.Length - 1) + "@" + strVal.Substring(0, strVal.Length - 1);
            }
            catch (Exception Err)
            {
                Console.Write(Err.StackTrace);
            }
            return strYearVal;
        }

        public string getDayWiseAnalytics(string gaAccountId)
        {
            string strYearVal = string.Empty;
            try
            {
                User user = (User)HttpContext.Current.Session["LoggedUser"];

                GoogleAnalyticsStats objGaStats = new GoogleAnalyticsStats();
                GoogleAnalyticsStatsRepository objGaStatsRepo = new GoogleAnalyticsStatsRepository();
                ArrayList arrCountry = objGaStatsRepo.getGoogleAnalyticsStatsYearById(gaAccountId, user.Id, 7, "day");

                string strYear = string.Empty;
                string strVal = string.Empty;
                int cnt = 0;
                foreach (var item in arrCountry)
                {
                    cnt++;
                    Array temp = (Array)item;
                    strYear = strYear + temp.GetValue(3).ToString() + ",";
                    strVal = strVal + temp.GetValue(8).ToString() + ",";
                }
                if (cnt < 7)
                {
                    for (int j = 0; j < 7 - cnt; j++)
                    {
                        strYear = strYear + "0,";
                        strVal = strVal + "0,";
                    }
                }
                strYearVal = strYear.Substring(0, strYear.Length - 1) + "@" + strVal.Substring(0, strVal.Length - 1);
            }
            catch (Exception Err)
            {
                Console.Write(Err.StackTrace);
            }
            return strYearVal;
        }

        public DataTable getdatafromXml(XmlDocument xmlDoc)
        {
            System.Data.DataTable dtAnalytics = new System.Data.DataTable();
            dtAnalytics.Columns.Add("id");
            dtAnalytics.Columns.Add("updated");
            dtAnalytics.Columns.Add("title");
            dtAnalytics.Columns.Add("link");
            dtAnalytics.Columns.Add("ga:visitors");
            dtAnalytics.Columns.Add("ga:bounces");
            dtAnalytics.Columns.Add("ga:newVisits");
            dtAnalytics.Columns.Add("ga:timeOnSite");
            dtAnalytics.Columns.Add("ga:pageviews");

            XmlNodeList nodeList = xmlDoc.ChildNodes;
            string strVisits = string.Empty;
            foreach (XmlNode node in nodeList)
            {
                XmlNodeList nodeList1 = node.ChildNodes;
                foreach (XmlNode nodeEntry in nodeList1)
                {
                    if (nodeEntry.Name == "entry")
                    {
                        System.Data.DataRow dr = dtAnalytics.NewRow();
                        XmlNodeList nodelistEntry = nodeEntry.ChildNodes;
                        foreach (XmlNode nodeEntry1 in nodelistEntry)
                        {
                            if (nodeEntry1.Name == "id")
                                dr["id"] = nodeEntry1.InnerXml.ToString();
                            else if (nodeEntry1.Name == "updated")
                                dr["updated"] = nodeEntry1.InnerXml.ToString();
                            else if (nodeEntry1.Name == "title")
                                dr["title"] = nodeEntry1.InnerXml.ToString();
                            else if (nodeEntry1.Name == "link")
                                dr["link"] = nodeEntry1.InnerXml.ToString();
                            else if (nodeEntry1.Name == "dxp:metric")
                            {
                                if (nodeEntry1.Attributes[0].Value == "ga:visits")
                                {
                                    strVisits = nodeEntry1.Attributes[2].Value.ToString();
                                    dr["ga:visitors"] = nodeEntry1.Attributes[2].Value.ToString();
                                }
                                if (nodeEntry1.Attributes[0].Value == "ga:bounces")
                                    dr["ga:bounces"] = nodeEntry1.Attributes[2].Value.ToString();
                                if (nodeEntry1.Attributes[0].Value == "ga:newVisits")
                                    dr["ga:newVisits"] = nodeEntry1.Attributes[2].Value.ToString();
                                if (nodeEntry1.Attributes[0].Value == "ga:timeOnSite")
                                    dr["ga:timeOnSite"] = Convert.ToDouble(nodeEntry1.Attributes[2].Value.ToString()) / Convert.ToDouble(strVisits);
                                if (nodeEntry1.Attributes[0].Value == "ga:pageviews")
                                    dr["ga:pageviews"] = nodeEntry1.Attributes[2].Value.ToString();
                                //string str= nodeEntry1.Attributes[2].Value.ToString();
                                //int cnt = nodeEntry1.ChildNodes.Count;

                            }
                            //dr["dxp:metric"] = nodeEntry1["dxp:metric"].GetElementsByTagName("ga:visitors").Item(0).Value;
                        }
                        dtAnalytics.Rows.Add(dr);
                    }
                }
            }
            return dtAnalytics;
        }

    }
}
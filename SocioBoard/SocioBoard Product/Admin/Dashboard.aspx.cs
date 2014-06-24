using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SocioBoard.Model;
using System.Collections;
using log4net;

namespace SocialSuitePro.Admin
{
    public partial class Dashboard : System.Web.UI.Page
    {
        ILog logger = LogManager.GetLogger(typeof(Dashboard));

        public string strUser = string.Empty;
        public string strStandard = string.Empty;
        public string strDelux = string.Empty;
        public string strPremium = string.Empty;
        public string strMonth = string.Empty;
        public string strAccMonth = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                if (Session["AdminProfile"] == null)
                {
                    Response.Redirect("Default.aspx");
                }

                #region User Count By Month
                try
                {
                    UserRepository objUserRepo = new UserRepository();
                    ArrayList lstUser = objUserRepo.UserCountByMonth();

                    foreach (var item in lstUser)
                    {
                        Array temp = (Array)item;
                        strUser = strUser + temp.GetValue(1).ToString() + ",";
                        if (temp.GetValue(0).ToString() == "1")
                            strMonth = strMonth + "'Jan',";
                        else if (temp.GetValue(0).ToString() == "2")
                            strMonth = strMonth + "'Feb',";
                        else if (temp.GetValue(0).ToString() == "3")
                            strMonth = strMonth + "'March',";
                        else if (temp.GetValue(0).ToString() == "4")
                            strMonth = strMonth + "'April',";
                        else if (temp.GetValue(0).ToString() == "5")
                            strMonth = strMonth + "'May',";
                        else if (temp.GetValue(0).ToString() == "6")
                            strMonth = strMonth + "'June',";
                        else if (temp.GetValue(0).ToString() == "7")
                            strMonth = strMonth + "'July',";
                        else if (temp.GetValue(0).ToString() == "8")
                            strMonth = strMonth + "'Aug',";
                        else if (temp.GetValue(0).ToString() == "9")
                            strMonth = strMonth + "'Sep',";
                        else if (temp.GetValue(0).ToString() == "10")
                            strMonth = strMonth + "'Oct',";
                        else if (temp.GetValue(0).ToString() == "11")
                            strMonth = strMonth + "'Nov',";
                        else if (temp.GetValue(0).ToString() == "12")
                            strMonth = strMonth + "'Dec'";
                    }

                    if (strMonth[strMonth.Length - 1] == ',')
                    {
                        strMonth = strMonth.Substring(0, strMonth.Length - 1);
                    }


                    strUser = strUser.Substring(0, strUser.Length - 1);
                }
                catch (Exception Err)
                {
                    logger.Error(Err.Message);
                    Console.Write(Err.StackTrace);
                }
                #endregion

                #region User Count By Month & Account Type
                try
                {
                    UserRepository objUserRepo = new UserRepository();
                    ArrayList lstUser = objUserRepo.UserCountByAccTypeMonth();

                    foreach (var item in lstUser)
                    {
                        Array temp = (Array)item;
                        if (temp.GetValue(0).ToString() == "1")
                            strAccMonth = strAccMonth + "'Jan',";
                        else if (temp.GetValue(0).ToString() == "2")
                            strAccMonth = strAccMonth + "'Feb',";
                        else if (temp.GetValue(0).ToString() == "3")
                            strAccMonth = strAccMonth + "'March',";
                        else if (temp.GetValue(0).ToString() == "4")
                            strAccMonth = strAccMonth + "'April',";
                        else if (temp.GetValue(0).ToString() == "5")
                            strAccMonth = strAccMonth + "'May',";
                        else if (temp.GetValue(0).ToString() == "6")
                            strAccMonth = strAccMonth + "'June',";
                        else if (temp.GetValue(0).ToString() == "7")
                            strAccMonth = strAccMonth + "'July',";
                        else if (temp.GetValue(0).ToString() == "8")
                            strAccMonth = strAccMonth + "'Aug',";
                        else if (temp.GetValue(0).ToString() == "9")
                            strAccMonth = strAccMonth + "'Sep',";
                        else if (temp.GetValue(0).ToString() == "10")
                            strAccMonth = strAccMonth + "'Oct',";
                        else if (temp.GetValue(0).ToString() == "11")
                            strAccMonth = strAccMonth + "'Nov',";
                        else if (temp.GetValue(0).ToString() == "12")
                            strAccMonth = strAccMonth + "'Dec'";
                        if (temp.GetValue(3) != null)
                        {
                            if (temp.GetValue(3).ToString() == "INDIVIDUAL")
                                strStandard = strStandard + temp.GetValue(1).ToString() + ",";
                            else if (temp.GetValue(3).ToString() == "SMALL BUSINESS")
                                strDelux = strDelux + temp.GetValue(1).ToString() + ",";
                            else if (temp.GetValue(3).ToString() == "CORPORATION")
                                strPremium = strPremium + temp.GetValue(1).ToString() + ",";
                        }
                    }
                    if (strAccMonth[strAccMonth.Length - 1] == ',')
                    {
                        strAccMonth = strAccMonth.Substring(0, strAccMonth.Length - 1);
                    }
                    strStandard = strStandard.Substring(0, strStandard.Length - 1);
                    strDelux = strDelux.Substring(0, strDelux.Length - 1);
                    strPremium = strPremium.Substring(0, strPremium.Length - 1);
                }
                catch (Exception Err)
                {
                    logger.Error(Err.Message);
                    Console.Write(Err.StackTrace);
                }
                #endregion
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            SocioBoard.Helper.delete obj = new SocioBoard.Helper.delete();
           Label1.Text= obj.DeleteAllUsersByCreateDate("2014-02-10");
            
        }
    }
}
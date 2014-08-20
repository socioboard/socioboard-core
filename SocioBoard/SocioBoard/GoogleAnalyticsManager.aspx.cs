using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using GlobusGooglePlusLib.GAnalytics.Core.Accounts;
using GlobusGooglePlusLib.GAnalytics.Core.AnalyticsMethod;
using GlobusGooglePlusLib.Authentication;
using Newtonsoft.Json.Linq;
using SocioBoard.Domain;
using System.Data;
using SocioBoard.Model;
using SocioBoard.Helper;
using log4net;

namespace SocialSuitePro
{
    public partial class GoogleAnalyticsManager : System.Web.UI.Page
    {
        ILog logger = LogManager.GetLogger(typeof(GoogleAnalyticsManager));
        protected void Page_Load(object sender, EventArgs e)
        {
            oAuthTokenGa obj = new oAuthTokenGa();
            Accounts objAcc = new Accounts();
            Analytics objAlyt = new Analytics();
            User user = (User)Session["LoggedUser"];
            if (!IsPostBack)
            {
                if (Session["login"] == null)
                {
                    if (user == null)
                    { Response.Redirect("Default.aspx"); }
                }

                try
                {
                    string strRefresh = obj.GetRefreshToken(Request.QueryString["code"].ToString());
                    if (!strRefresh.StartsWith("["))
                        strRefresh = "[" + strRefresh + "]";
                    JArray objArray = JArray.Parse(strRefresh);
                    GoogleAnalyticsAccount objGaAcc = new GoogleAnalyticsAccount();
                    GoogleAnalyticsAccountRepository objGaAccRepo = new GoogleAnalyticsAccountRepository();
                    GanalyticsHelper objGaHelper=new GanalyticsHelper();
                    SocialProfilesRepository socioprofilerepo = new SocialProfilesRepository();
                    SocialProfile socioprofile = new SocialProfile();
                    foreach (var item in objArray)
                    {
                        DataSet dsAccount = objAcc.getGaAccounts(item["access_token"].ToString());
                        objGaAcc.RefreshToken = item["refresh_token"].ToString();
                        objGaAcc.AccessToken = item["access_token"].ToString();
                        objGaAcc.EmailId = dsAccount.Tables["title"].Rows[0]["title_Text"].ToString();
                        objGaAcc.EntryDate = DateTime.Now;
                        objGaAcc.GaAccountId = dsAccount.Tables["property"].Rows[0]["value"].ToString();
                        objGaAcc.GaAccountName = dsAccount.Tables["property"].Rows[1]["value"].ToString();
                        objGaAcc.Id = Guid.NewGuid();
                        objGaAcc.IsActive = true;
                        objGaAcc.UserId = user.Id;
                        DataSet dsProfile = objAcc.getGaProfiles(item["access_token"].ToString(), objGaAcc.GaAccountId);
                        int profileCount = dsProfile.Tables["property"].Select("name='ga:profileId'").Length;
                        string[,] names =new string[profileCount,2];
                       

                        int tempCount = 0;
                        foreach (DataRow dRow in dsProfile.Tables["property"].Rows)
                        {
                            //if (dRow["name"].Equals("ga:profileId") || dRow["name"].Equals("ga:profileName"))
                            {
                                if (dRow["name"].Equals("ga:profileId"))
                                {
                                    objGaAcc.GaProfileId = dRow["value"].ToString();
                                    names[tempCount, 0] = dRow["value"].ToString();
                                }
                                if (dRow["name"].Equals("ga:profileName"))
                                {
                                    names[tempCount, 1] = dRow["value"].ToString();
                                    objGaAcc.GaProfileName = dRow["value"].ToString();
                                }
                                if (tempCount>=profileCount)
                                {
                                    break;
                                }
                                if (names[tempCount, 0] != null && names[tempCount, 1] != null)
                                {
                                    tempCount++;
                                }
                                
                            }
                        }

                            socioprofile.Id = Guid.NewGuid();
                            socioprofile.ProfileDate = DateTime.Now;
                            socioprofile.ProfileId = objGaAcc.GaAccountId;
                            socioprofile.ProfileType = "googleanalytics";
                            socioprofile.UserId = user.Id;

                            if (!objGaAccRepo.checkGoogelAnalyticsUserExists(objGaAcc.GaAccountId, user.Id))
                            {
                                for (int i = 0; i < profileCount; i++)
                                {
                                    objGaAcc.GaProfileId = names[i,0];
                                    objGaAcc.GaProfileName = names[i,1];
                                    if(!objGaAccRepo.checkGoogelAnalyticsProfileExists(objGaAcc.GaAccountId,objGaAcc.GaProfileId,user.Id))
                                        objGaAccRepo.addGoogleAnalyticsUser(objGaAcc);
                                    else
                                        objGaAccRepo.updateGoogelAnalyticsUser(objGaAcc);


                                    objGaHelper.getCountryAnalyticsApi(objGaAcc.GaProfileId,user.Id);
                                    objGaHelper.getYearWiseAnalyticsApi(objGaAcc.GaProfileId, user.Id);
                                    objGaHelper.getMonthWiseAnalyticsApi(objGaAcc.GaProfileId, user.Id);
                                    objGaHelper.getDayWiseAnalyticsApi(objGaAcc.GaProfileId, user.Id);

                                }
                                
                                if (!socioprofilerepo.checkUserProfileExist(socioprofile))
                                {
                                    socioprofilerepo.addNewProfileForUser(socioprofile);
                                }
                                else
                                {
                                    socioprofilerepo.updateSocialProfile(socioprofile);
                                }
                            }
                            else
                            {
                                //objGaAccRepo.updateGoogelAnalyticsUser(objGaAcc);
                                 for (int i = 0; i < names.Length; i++)
                                {
                                    objGaAcc.GaProfileId = names[i,0];
                                    objGaAcc.GaProfileName = names[i,1];
                                    if(!objGaAccRepo.checkGoogelAnalyticsProfileExists(objGaAcc.GaAccountId,objGaAcc.GaProfileId,user.Id))
                                        objGaAccRepo.addGoogleAnalyticsUser(objGaAcc);
                                    else
                                        objGaAccRepo.updateGoogelAnalyticsUser(objGaAcc);
                                }
                                if (!socioprofilerepo.checkUserProfileExist(socioprofile))
                                {
                                    socioprofilerepo.addNewProfileForUser(socioprofile);
                                }
                                else
                                {
                                    socioprofilerepo.updateSocialProfile(socioprofile);
                                }
                            }
                        Response.Redirect("Home.aspx");
                    }
                }
                catch (Exception Err)
                {

                    logger.Error(Err.StackTrace);
                    try
                    {
                        Response.Redirect("/Home.aspx");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                        
                    }
                }
            }
        }
    }
}
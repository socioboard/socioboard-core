using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GlobusGooglePlusLib.App.Core;
using SocioBoard.Model;
using SocioBoard.Domain;
using Newtonsoft.Json.Linq;
using System.Collections;
using GlobusGooglePlusLib.Authentication;

namespace SocialSiteDataService
{
    public class GplusData
    {

        public void getGplusData(object UserId)
        {
            try
            {
                Guid userId = (Guid)UserId;
                GooglePlusAccountRepository objgpRepo=new GooglePlusAccountRepository();
                oAuthToken objToken = new oAuthToken();
                ArrayList arrGpAcc = objgpRepo.getAllGooglePlusAccountsOfUser(userId);
                foreach (GooglePlusAccount itemGp in arrGpAcc)
                {
                      string objRefresh = objToken.GetRefreshToken(itemGp.RefreshToken);
                        if (!objRefresh.StartsWith("["))
                            objRefresh = "[" + objRefresh + "]";
                        JArray objArray = JArray.Parse(objRefresh);
                    foreach(var item in objArray)
                    {

                        GetUserActivities(itemGp.GpUserId, item["access_token"].ToString(), userId);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
            }

        }
        public void GetUserActivities(string GpUserId, string acces_token, Guid UserId)
        {
            ActivitiesController obj = new ActivitiesController();
            GooglePlusActivitiesRepository objgpActRepo = new GooglePlusActivitiesRepository();
            GooglePlusActivities objgpAct = new GooglePlusActivities();
            JArray objActivity = obj.GetActivitiesList(GpUserId, acces_token);
            foreach (var itemActivity in objActivity)
            {
                foreach (var itemAct in itemActivity["items"])
                {
                    objgpAct.ActivityId = itemAct["id"].ToString();
                    objgpAct.ActivityUrl = itemAct["url"].ToString();
                    objgpAct.Content = itemAct["object"]["content"].ToString();
                    objgpAct.EntryDate = DateTime.Now;
                    objgpAct.FromId = itemAct["actor"]["id"].ToString();
                    objgpAct.FromProfileImage = itemAct["actor"]["image"]["url"].ToString();
                    objgpAct.FromUserName = itemAct["actor"]["displayName"].ToString();
                    objgpAct.GpUserId = GpUserId;
                    objgpAct.Id = Guid.NewGuid();
                    objgpAct.PlusonersCount = int.Parse(itemAct["object"]["plusoners"]["totalItems"].ToString());
                    objgpAct.RepliesCount = int.Parse(itemAct["object"]["replies"]["totalItems"].ToString());
                    objgpAct.ResharersCount = int.Parse(itemAct["object"]["resharers"]["totalItems"].ToString());
                    objgpAct.PublishedDate = itemAct["published"].ToString();
                    objgpAct.Title = itemActivity["title"].ToString();
                    objgpAct.UserId = UserId;
                    if (!objgpActRepo.checkgoogleplusActivityExists(itemAct["id"].ToString(), UserId))
                        objgpActRepo.addgoogleplusActivity(objgpAct);
                    else
                        objgpActRepo.updategoogleplusActivity(objgpAct);

                }

            }
        }
    }
}

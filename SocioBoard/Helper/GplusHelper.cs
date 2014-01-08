using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json.Linq;
using GlobusGooglePlusLib.App.Core;
using SocioBoard.Model;
using SocioBoard.Domain;
using GlobusGooglePlusLib.Authentication;

namespace SocioBoard.Helper
{
    public class GplusHelper
    {
        public void GetUerProfile(GooglePlusAccount objgpAcc,string acces_token,string refresh_token,Guid UserId)
        { 
              PeopleController obj = new PeopleController();
              oAuthToken objtoken = new oAuthToken();
              GooglePlusAccountRepository objgpRepo = new GooglePlusAccountRepository();
           
              SocialProfilesRepository socioprofilerepo = new SocialProfilesRepository();
              SocialProfile socioprofile = new SocialProfile();
                     
                    socioprofile.Id = Guid.NewGuid();
                    socioprofile.ProfileDate = DateTime.Now;
                    socioprofile.ProfileId = objgpAcc.GpUserId;
                    socioprofile.ProfileType = "googleplus";
                    socioprofile.UserId = UserId;

                  
                    JArray objPeopleList = obj.GetPeopleList(objgpAcc.GpUserId, acces_token, "visible");
                    objgpAcc.PeopleCount = objPeopleList.Count();


                    if (!objgpRepo.checkGooglePlusUserExists(objgpAcc.GpUserId, UserId))
                    {
                        objgpRepo.addGooglePlusUser(objgpAcc);
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
                        objgpRepo.updateGooglePlusUser(objgpAcc);
                        if (!socioprofilerepo.checkUserProfileExist(socioprofile))
                        {
                            socioprofilerepo.addNewProfileForUser(socioprofile);
                        }
                        else
                        {
                            socioprofilerepo.updateSocialProfile(socioprofile);
                        }
                    }
                    GetUserActivities(objgpAcc.GpUserId, acces_token, UserId);
                }

        public void GetUserActivities(string GpUserId, string acces_token,Guid UserId)
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
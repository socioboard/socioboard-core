using Api.Socioboard.Helper;
using Api.Socioboard.Model;
using Domain.Socioboard.Domain;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading;
using System.Web.Http;
using System.Web.Script.Serialization;

namespace Api.Socioboard.Controllers
{
    [Api.Socioboard.App_Start.AllowCrossSiteJson]
    public class ApiShareathonController : ApiController
    {
        private ShareathonRepository sharepo = new ShareathonRepository();
        private ShareathonGroupRepository sharegrprepo = new ShareathonGroupRepository();

        [HttpPost]
        public IHttpActionResult AddShareathon(ShareathonViewModel shareathon)
        {
            Shareathon _shreathon = new Shareathon();
            string id = "";
            for (int i = 0; i < shareathon.FacebookPageId.Length; i++)
            {
                string dataid = shareathon.FacebookPageId[i];
                id = dataid + "," + id;
            }
            _shreathon.Facebookpageid = id.TrimEnd(',');
            _shreathon.Facebookaccountid = shareathon.Facebookaccountid;
            _shreathon.Timeintervalminutes = shareathon.Timeintervalminutes;
            _shreathon.Id = Guid.NewGuid();
            _shreathon.Userid = shareathon.Userid;
            if (!sharepo.IsShareathonExist(shareathon.Userid, shareathon.Facebookaccountid, _shreathon.Facebookpageid))
            {
                if (sharepo.AddShareathon(_shreathon))
                {
                    return Ok();
                }
                else
                {
                    return BadRequest();
                }
            }
            else
            {
                return BadRequest("Shareathon exist");
            }


        }


        [HttpPost]
        public IHttpActionResult AddGroupSharethon(ShareathonGroupViewModel sharethon)
        {
            Domain.Socioboard.Domain.ShareathonGroup _ShareathonGroup = new ShareathonGroup();
            string groupId = "";
            string nameId = "";
            FacebookAccount facebookAccount = sharepo.getFbAccount(sharethon.Facebookaccountid);
            string pageid = FacebookHelper.GetFbPageDetails(sharethon.FacebookPageUrl, facebookAccount.AccessToken);
            _ShareathonGroup.Id = Guid.NewGuid();
            _ShareathonGroup.Facebookpageid = pageid.TrimEnd(',');
            _ShareathonGroup.FacebookPageUrl = sharethon.FacebookPageUrl;
            _ShareathonGroup.AccessToken = facebookAccount.AccessToken;
            _ShareathonGroup.Facebookaccountid = facebookAccount.Id;
            _ShareathonGroup.Userid = sharethon.Userid;
            _ShareathonGroup.Timeintervalminutes = sharethon.Timeintervalminutes;
            _ShareathonGroup.IsHidden = false;
            for (int i = 0; i < sharethon.FacebookGroupId.Length; i++)
            {
                string dataid = sharethon.FacebookGroupId[i];
                string[] grpid = Regex.Split(dataid, "###");
                groupId = grpid[0] + "," + groupId;
                nameId = sharethon.FacebookGroupId[i] + "," + nameId;
            }
            _ShareathonGroup.Facebookgroupid = groupId.TrimEnd(',');
            _ShareathonGroup.Facebooknameid = nameId.TrimEnd(',');
            if (!sharegrprepo.IsShareathonExist(sharethon.Userid, groupId, pageid))
            {
                if (sharegrprepo.AddShareathon(_ShareathonGroup))
                {
                    return Ok();
                }
                else
                {
                    return BadRequest();
                }
            }
            else
            {
                return BadRequest("Shareathon exist");
            }
            return Ok();

        }



        [HttpGet]
        public IHttpActionResult GetShareathons(string UserId)
        {
            Guid userId = Guid.Empty;
            try
            {
                userId = Guid.Parse(UserId);
            }
            catch
            {
                return BadRequest();
            }
            if (userId == Guid.Empty)
            {
                return BadRequest();
            }
            else
            {
                List<Shareathon> shareatons = sharepo.getUserShareathon(userId);
                List<ShareathonViewModel> shareathonviewModels = new List<ShareathonViewModel>();
                foreach (var item in shareatons)
                {
                    ShareathonViewModel svmodel = new ShareathonViewModel();
                    svmodel.Id = item.Id;
                    svmodel.IsHidden = item.IsHidden;
                    svmodel.Lastpostid = item.Lastpostid;
                    svmodel.Lastsharetimestamp = item.Lastsharetimestamp;
                    svmodel.Timeintervalminutes = item.Timeintervalminutes;
                    svmodel.Userid = item.Userid;
                    svmodel.Facebookaccount = sharepo.getFbAccount(item.Facebookaccountid);
                    List<FacebookAccount> Facebookpages = new List<FacebookAccount>();
                    try {
                        string[] fbids = item.Facebookpageid.Split(',');
                        foreach (var id in fbids)
                        {
                            try 
                            {
                                Facebookpages.Add(sharepo.getFbAccount(Guid.Parse(id)));
                            }
                            catch { }
                        }
                    }
                    catch { }
                    svmodel.Facebookpages = Facebookpages;
                    svmodel.pageid = item.Facebookpageid;
                    shareathonviewModels.Add(svmodel);
                }
                return Ok(shareathonviewModels);
            }

        }

        [HttpGet]
        public IHttpActionResult GetGroupShareaton(string Id)
        {
            Guid id = Guid.Empty;
            try
            {
                id = Guid.Parse(Id);
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
            if (id == Guid.Empty)
            {
                return BadRequest();
            }
            else
            {

                ShareathonGroup sharegrp = sharegrprepo.getShareathon(id);
                return Ok(sharegrp);
            }

        }


        [HttpGet]
        public IHttpActionResult GetShareaton(string Id)
        {
            Guid id = Guid.Empty;
            try
            {
                id = Guid.Parse(Id);
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
            if (id == Guid.Empty)
            {
                return BadRequest();
            }
            else
            {

                Shareathon sharegrp = sharepo.getShareathon(id);
                return Ok(sharegrp);
            }

        }



        [HttpGet]
        public IHttpActionResult GetGroupShareathons(string UserId)
        {
            Guid userId = Guid.Empty;
            try
            {
                userId = Guid.Parse(UserId);
            }
            catch
            {
                return BadRequest();
            }
            if (userId == Guid.Empty)
            {
                return BadRequest();
            }
            else
            {
                List<ShareathonGroup> shareatons = sharegrprepo.getUserShareathon(userId);
                List<ShareathonViewModel> shareathonviewModels = new List<ShareathonViewModel>();
                foreach (var item in shareatons)
                {
                    ShareathonViewModel svmodel = new ShareathonViewModel();
                    svmodel.Id = item.Id;
                    svmodel.IsHidden = item.IsHidden;
                    svmodel.Lastpostid = item.Lastpostid;
                    svmodel.Lastsharetimestamp = item.Lastsharetimestamp;
                    svmodel.Timeintervalminutes = item.Timeintervalminutes;
                    svmodel.Userid = item.Userid;
                    svmodel.Facebookaccount = sharepo.getFbAccount(item.Facebookaccountid);
                    shareathonviewModels.Add(svmodel);
                }
                return Ok(shareathonviewModels);
            }

        }

        [HttpGet]
        public List<Shareathon> ShareShareathons()
        {
            List<Shareathon> shareathon = sharepo.getShareathons();
            return shareathon;
        }

        [HttpGet]
        public List<ShareathonGroup> ShareGroupShareathons()
        {
            List<ShareathonGroup> shareathongrp = sharegrprepo.getShareathons();
            return shareathongrp;

        }

        [HttpPost]
        public void PostData(ShareathonGroup item)
        {
            FacebookAccount facebookAccount = sharegrprepo.getFbAccount(item.Facebookaccountid);
            string feedId = string.Empty;
            string[] pageid = item.Facebookpageid.Split(',');

            foreach (string item_str in pageid)
            {

                string feeds = FacebookHelper.getFacebookRecentPost(item.AccessToken, item_str);

                try
                {
                    if (!string.IsNullOrEmpty(feeds) && !feeds.Equals("[]"))
                    {
                        JObject fbpageNotes = JObject.Parse(feeds);
                        foreach (JObject obj in JArray.Parse(fbpageNotes["data"].ToString()))
                        {
                            try
                            {
                                string feedid = obj["id"].ToString();
                                feedid = feedid.Split('_')[1];
                                feedId = feedid + "," + feedId;
                            }
                            catch { }

                        }
                    }

                }
                catch (Exception ex)
                {


                }
            }
            FacebookHelper.postfeedGroup(item.AccessToken, item.Facebookgroupid, feedId, facebookAccount.FbUserId, item.Timeintervalminutes);
        }


        [HttpPost]
        public void PostDataPage(Shareathon item, string pageid)
        {
            FacebookAccount facebookpage = sharegrprepo.getFbAccount(Guid.Parse(pageid));
            FacebookAccount facebookAccount = sharegrprepo.getFbAccount(item.Facebookaccountid);

            string feeds = FacebookHelper.getFacebookRecentPost(facebookpage.AccessToken, facebookpage.FbUserId);


            string feedId = string.Empty;
            try
            {
                if (!string.IsNullOrEmpty(feeds) && !feeds.Equals("[]"))
                {
                    JObject fbpageNotes = JObject.Parse(feeds);
                    foreach (JObject obj in JArray.Parse(fbpageNotes["data"].ToString()))
                    {
                        try
                        {
                            string feedid = obj["id"].ToString();
                            feedid = feedid.Split('_')[1];
                            FacebookHelper.ShareFeed(facebookAccount.AccessToken, feedid, facebookpage.FbUserId, "", facebookAccount.FbUserId, "", item.Timeintervalminutes);
                        }
                        catch { }
                        Thread.Sleep(1000 * 60 * item.Timeintervalminutes);
                    }
                }

            }
            catch (Exception ex)
            {


            }
        }



        [HttpGet]
        public IHttpActionResult DeletePageShareathon(string Id)
        {
            int i = sharepo.DeletePageShareathon(Guid.Parse(Id));
            if (i == 1)
            {
                return Ok("Success");
            }
            else
            {
                return BadRequest();
            }


        }


        [HttpGet]
        public IHttpActionResult DeleteGroupShareathon(string Id)
        {
            int i = sharegrprepo.DeleteGroupShareathon(Guid.Parse(Id));
            if (i == 1)
            {
                return Ok("Success");
            }
            else
            {
                return BadRequest();
            }


        }


        [ActionName("EditShareathonGroup")]
        [HttpPost]
        public IHttpActionResult EditShareathon(ShareathonGroupViewModel sharethon)
        {
            string groupId = "";
            string nameId = "";
            ShareathonGroup eidtShareathon = sharegrprepo.getShareathon(sharethon.Id);
            FacebookAccount facebookAccount = sharepo.getFbAccount(sharethon.Facebookaccountid);
            string pageid = FacebookHelper.GetFbPageDetails(sharethon.FacebookPageUrl, facebookAccount.AccessToken);
            eidtShareathon.Facebookaccountid = facebookAccount.Id;
            eidtShareathon.Facebookpageid = pageid;
            eidtShareathon.FacebookPageUrl = sharethon.FacebookPageUrl;
            eidtShareathon.Timeintervalminutes = sharethon.Timeintervalminutes;
            for (int i = 0; i < sharethon.FacebookGroupId.Length; i++)
            {
                string dataid = sharethon.FacebookGroupId[i];
                string[] grpid = Regex.Split(dataid, "###");
                groupId = grpid[0] + "," + groupId;
                nameId = sharethon.FacebookGroupId[i] + "," + nameId;
            }
            eidtShareathon.Facebookgroupid = groupId.TrimEnd(',');
            eidtShareathon.Facebooknameid = nameId.TrimEnd(',');
            if (sharegrprepo.updateShareathon(eidtShareathon))
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }



        [ActionName("EditShareathon")]
        [HttpPost]
        public IHttpActionResult EditShareathon(ShareathonViewModel sharethon)
        {
            Shareathon eidtShareathon = sharepo.getShareathon(sharethon.Id);
            FacebookAccount facebookAccount = sharepo.getFbAccount(sharethon.Facebookaccountid);
            eidtShareathon.Facebookaccountid = facebookAccount.Id;
            string id = "";
            for (int i = 0; i < sharethon.FacebookPageId.Length; i++)
            {
                string dataid = sharethon.FacebookPageId[i];
                id = dataid + "," + id;
            }
            eidtShareathon.Facebookpageid = id.TrimEnd(',');
            eidtShareathon.Timeintervalminutes = sharethon.Timeintervalminutes;

            if (sharepo.updateShareathon(eidtShareathon))
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }


        [HttpGet]
        public IHttpActionResult DeleteGroupShareathon()
        {
            int i = sharegrprepo.deletegroupshraethonpost();
            if (i == 1) { return Ok(); }
            else { return BadRequest(); }

        }



        [HttpGet]
        public IHttpActionResult DeleteGrPagehareathon()
        {
            int i = sharepo.deletepageshraethonpost();
            if (i == 1) { return Ok(); }
            else { return BadRequest(); }

        }






    }
}

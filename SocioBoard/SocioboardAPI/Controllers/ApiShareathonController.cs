using Api.Socioboard.Helper;
using Api.Socioboard.Model;
using Api.Socioboard.Services;
using Domain.Socioboard.Domain;
using Domain.Socioboard.Helper;
using log4net;
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
        ILog logger = LogManager.GetLogger(typeof(ApiShareathonController));
        private ShareathonRepository sharepo = new ShareathonRepository();
        private ShareathonGroupRepository sharegrprepo = new ShareathonGroupRepository();
        private FacebookAccountRepository facebookrepo = new FacebookAccountRepository();

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
            _shreathon.FacebookStatus = 1;
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
            Domain.Socioboard.Domain.FacebookAccount facebookAccount = sharegrprepo.getFacebookAccountDetailsByUserProfileId(sharethon.Facebookaccountid, sharethon.Userid);
            string pageid = FacebookHelper.GetFbPageDetails(sharethon.FacebookPageUrl, facebookAccount.AccessToken);
            _ShareathonGroup.Id = Guid.NewGuid();
            _ShareathonGroup.Facebookpageid = pageid.TrimEnd(',');
            _ShareathonGroup.FacebookPageUrl = sharethon.FacebookPageUrl;
            _ShareathonGroup.AccessToken = facebookAccount.AccessToken;
            _ShareathonGroup.Facebookaccountid = facebookAccount.FbUserId;
            _ShareathonGroup.Userid = sharethon.Userid;
            _ShareathonGroup.Timeintervalminutes = sharethon.Timeintervalminutes;
            _ShareathonGroup.IsHidden = false;
            _ShareathonGroup.FacebookStatus = 1;
            for (int i = 0; i < sharethon.FacebookGroupId.Length; i++)
            {
                string dataid = sharethon.FacebookGroupId[i];
                string[] grpid = Regex.Split(dataid, "###");
                groupId = grpid[0] + "," + groupId;

            }
            _ShareathonGroup.Facebooknameid = sharethon.Facebooknameid.TrimEnd(',');
            _ShareathonGroup.Facebookgroupid = groupId.TrimEnd(',');
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
                    svmodel.Facebookaccount = sharepo.getFacebookAccountDetailsByUserProfileId(item.Facebookaccountid, item.Userid);
                    List<Domain.Socioboard.Domain.FacebookAccount> Facebookpages = new List<Domain.Socioboard.Domain.FacebookAccount>();
                    try
                    {
                        string[] fbids = item.Facebookpageid.Split(',');
                        foreach (var id in fbids)
                        {
                            try
                            {
                                Domain.Socioboard.Domain.FacebookAccount fbaccount = sharepo.getFacebookAccountDetailsByUserProfileId(id, item.Userid);
                                if (fbaccount != null)
                                {
                                    Facebookpages.Add(fbaccount);
                                }
                               
                            }
                            catch { }
                        }
                    }
                    catch { }
                    svmodel.Facebookpages = Facebookpages.Where(t=>t.FbUserId!="").ToList();
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
                    svmodel.Facebookaccount = sharepo.getFacebookAccountDetailsByUserProfileId(item.Facebookaccountid, item.Userid);
                    shareathonviewModels.Add(svmodel);
                }
                return Ok(shareathonviewModels);
            }

        }

        [HttpGet]
        public IHttpActionResult ShareShareathons()
        {
            List<Shareathon> shareatons = sharepo.getShareathons();
            foreach (var item in shareatons)
            {
                Domain.Socioboard.Domain.FacebookAccount facebookAccount = sharepo.getFacebookAccountDetailsByUserProfileId(item.Facebookaccountid, item.Userid);
                try
                {
                    string[] ids = item.Facebookpageid.Split(',');
                    foreach (string id in ids)
                    {
                        try
                        {
                            Domain.Socioboard.Domain.FacebookAccount facebookPage = sharepo.getFbAccount(Guid.Parse(id));
                            if (facebookPage != null)
                            {
                                string feeds = FacebookHelper.getFacebookRecentPost(facebookAccount.AccessToken, facebookPage.FbUserId);
                                string feedId = string.Empty;
                                if (!string.IsNullOrEmpty(feeds) && !feeds.Equals("[]"))
                                {
                                    JObject fbpageNotes = JObject.Parse(feeds);
                                    foreach (JObject obj in JArray.Parse(fbpageNotes["data"].ToString()))
                                    {
                                        try
                                        {
                                            feedId = obj["id"].ToString();
                                            feedId = feedId.Split('_')[1];
                                        }
                                        catch { }
                                        break;
                                    }
                                    if (item.Lastpostid == null || (!item.Lastpostid.Equals(feedId) && item.Lastsharetimestamp.AddMinutes(item.Timeintervalminutes) >= DateTime.UtcNow))
                                    {
                                        FacebookHelper.ShareFeed(facebookAccount.AccessToken, feedId, facebookPage.FbUserId, "", facebookAccount.FbUserId, facebookPage.FbUserName);
                                    }
                                }
                            }
                        }

                        catch { }
                    }
                }
                catch (Exception e)
                {
                    logger.Error(e.Message);
                    logger.Error(e.StackTrace);
                }

            }

            return Ok();
        }


        [HttpGet]
        public List<Shareathon> ShareathonPage()
        {
            List<Shareathon> shareatons = sharepo.getShareathons();
            return shareatons;
        }

        [HttpGet]
        public List<Shareathon> ShareathonByUserId(string UserId)
        {
            List<Shareathon> lstshareathongrp = sharepo.getUserShareathonByUserId(Guid.Parse(UserId));
            return lstshareathongrp;
        }

        [HttpGet]
        public List<ShareathonGroup> ShareGroupShareathons()
        {
            List<ShareathonGroup> shareathongrp = sharegrprepo.getShareathons();
            return shareathongrp;

        }

        [HttpGet]
        public List<ShareathonGroup> ShareathonGroupByUserId(string UserId)
        {
            List<ShareathonGroup> lstshareathongrp = sharegrprepo.getUserShareathon(Guid.Parse(UserId));
            return lstshareathongrp;
        }

        [HttpPost]
        public void PostData(ShareathonGroup item)
        {

            Domain.Socioboard.Domain.FacebookAccount facebookAccount = sharegrprepo.getFacebookAccountDetailsByUserProfileId(item.Facebookaccountid, item.Userid);

            string feedId = string.Empty;
            string[] pageid = item.Facebookpageid.Split(',');

            foreach (string item_str in pageid)
            {

                string feeds = FacebookHelper.getFacebookRecentPost(facebookAccount.AccessToken, item_str);

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
                        FacebookHelper.postfeedGroup(item.AccessToken, item.Facebookgroupid, feedId, facebookAccount.FbUserId, item.Timeintervalminutes, item.Id);

                    }
                    else
                    {
                        facebookAccount.IsActive = 2;
                        facebookrepo.updateFacebookUserStatus(facebookAccount);
                        sharegrprepo.UpadteShareathonByFacebookUserId(facebookAccount.FbUserId, facebookAccount.UserId);
                    }

                }
                catch (Exception ex)
                {


                }
            }


        }


        [HttpPost]
        public void PostDataPage(Shareathon item, string pageid)
        {

            Domain.Socioboard.Domain.FacebookAccount facebookpage = sharepo.getFacebookAccountDetailsByUserProfileId(pageid, item.Userid);
                Domain.Socioboard.Domain.FacebookAccount facebookAccount = sharepo.getFacebookAccountDetailsByUserProfileId(item.Facebookaccountid,item.Userid);

            if (facebookpage != null)
            {
                string feeds = FacebookHelper.getFacebookRecentPost(facebookAccount.AccessToken, facebookpage.FbUserId);



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
                                if (sharepo.IsShareathonExistById(item.Id))
                                {
                                    string ret = FacebookHelper.ShareFeed(facebookAccount.AccessToken, feedid, facebookpage.FbUserId, "", facebookAccount.FbUserId, facebookpage.FbUserName);
                                    if (ret == "success")
                                    {

                                        Thread.Sleep(1000 * 60 * item.Timeintervalminutes);
                                    }
                                    else if (ret == "Error validating access token")
                                    {
                                        facebookAccount.IsActive = 2;
                                        facebookrepo.updateFacebookUserStatus(facebookAccount);
                                    }
                                }
                            }
                            catch { }


                        }
                    }
                    else
                    {
                        if (!feeds.Contains("The remote server returned an error: (400) Bad Request."))
                        {
                            facebookpage.IsActive = 2;
                            facebookrepo.updateFacebookUserStatus(facebookpage);
                        }
                        else {

                            facebookpage.IsActive = 2;
                            facebookrepo.updateFacebookUserStatus(facebookAccount);
                            sharepo.UpadteShareathonByFacebookUserId(facebookAccount.FbUserId, facebookAccount.UserId);
                        }
                    }

                }
                catch (Exception ex)
                {


                }
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
            Domain.Socioboard.Domain.FacebookAccount facebookAccount = sharepo.getFacebookAccountDetailsByUserProfileId(sharethon.Facebookaccountid, sharethon.Userid);
            string pageid = FacebookHelper.GetFbPageDetails(sharethon.FacebookPageUrl, facebookAccount.AccessToken);
            eidtShareathon.Facebookaccountid = facebookAccount.FbUserId;
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
            eidtShareathon.Facebooknameid = sharethon.Facebooknameid.TrimEnd(',');
            eidtShareathon.Facebookgroupid = groupId.TrimEnd(',');
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
            Domain.Socioboard.Domain.FacebookAccount facebookAccount = sharepo.getFacebookAccountDetailsByUserProfileId(sharethon.Facebookaccountid, sharethon.Userid);
            eidtShareathon.Facebookaccountid = facebookAccount.FbUserId;
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
            return Ok();

        }

        [HttpGet]
        public IHttpActionResult DeleteGrPagehareathon()
        {
            int i = sharepo.deletepageshraethonpost();
            return Ok();

        }


        [HttpGet]
        public IHttpActionResult GetGroupShareathonReport(string profileid, string days)
        {
            string strCount = string.Empty;
            List<SharethonGroupPost> lstgrppost = sharegrprepo.GetGroupPostReport(profileid, Int32.Parse(days));

            List<GroupPostDetails> lstGrpPost = lstgrppost.GroupBy(t => t.Facebookgroupid).Select(t => new GroupPostDetails(t.First(), t.Count())).ToList();

            DateTime present_date = DateTime.UtcNow;
            while (present_date.Date != DateTime.UtcNow.Date.AddDays(-Int32.Parse(days)))
            {
                List<SharethonGroupPost> _lstgrppost = new List<SharethonGroupPost>();
                _lstgrppost = lstgrppost.Where(m => m.PostedTime >= present_date.Date.AddSeconds(1) && m.PostedTime <= present_date.AddDays(1).Date.AddSeconds(-1)).ToList();
                strCount += _lstgrppost.Count.ToString() + ",";
                present_date = present_date.AddDays(-1);
            }

            string data = new JavaScriptSerializer().Serialize(lstGrpPost) + "#_#" + strCount.TrimEnd(',');

            return Ok(data);
        }


        [HttpGet]
        public IHttpActionResult GePageShareathonReport(string profileid, string days)
        {
            string strCount = string.Empty;
            List<SharethonPost> lstgrppost = sharepo.GetGroupPostReport(profileid, Int32.Parse(days));
            List<PagePostDetails> lstGrpPost = lstgrppost.GroupBy(t => t.Facebookpageid).Select(t => new PagePostDetails(t.First(), t.Count())).ToList();
            DateTime present_date = DateTime.UtcNow;
            while (present_date.Date != DateTime.UtcNow.Date.AddDays(-Int32.Parse(days)))
            {
                List<SharethonPost> _lstgrppost = new List<SharethonPost>();
                _lstgrppost = lstgrppost.Where(m => m.PostedTime >= present_date.Date.AddSeconds(1) && m.PostedTime <= present_date.AddDays(1).Date.AddSeconds(-1)).ToList();
                strCount += _lstgrppost.Count.ToString() + ",";
                present_date = present_date.AddDays(-1);
            }
            string data = new JavaScriptSerializer().Serialize(lstGrpPost) + "#_#" + strCount.TrimEnd(',');
            return Ok(data);
        }

        [HttpGet]
        public IHttpActionResult GetShareCountGroupSharethon(string profileIds, string days)
        {
            try
            {
                List<Domain.Socioboard.Helper.SharethonGroupData> lstSharethonGroupData = sharegrprepo.GetShareCountByFacebookId(profileIds, Int32.Parse(days));
                return Ok(lstSharethonGroupData);
            }
            catch (Exception e)
            {
                return BadRequest("something went wrong");
            }

        }

        [HttpGet]
        public IHttpActionResult GetShareCountPageSharethon(string profileIds, string days)
        {
            try
            {
                List<Domain.Socioboard.Helper.SharethonPageData> lstSharethonPageData = sharepo.GetShareCountByFacebookId(profileIds, Int32.Parse(days));
                return Ok(lstSharethonPageData);
            }
            catch (Exception e)
            {
                return BadRequest("something went wrong");
            }

        }

    }
}

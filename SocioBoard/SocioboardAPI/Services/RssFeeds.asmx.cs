using Api.Socioboard.Helper;
using Api.Socioboard.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Script.Services;
using System.Web.Services;
using log4net;
using System.Xml;
using System.Text.RegularExpressions;
using SocioBoard.Model;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Net;
using System.IO;
using System.Configuration;
using Newtonsoft.Json.Linq;
namespace Api.Socioboard.Services
{
    /// <summary>
    /// Summary description for Group
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [ScriptService]
    public class RssFeeds : System.Web.Services.WebService
    {
        ILog logger = LogManager.GetLogger(typeof(RssFeeds));
        //Domain.Socioboard.Domain.RssFeeds objRssFeeds = new Domain.Socioboard.Domain.RssFeeds();
        RssFeedsRepository objRssFeedsRepository = new RssFeedsRepository();
        RssReaderRepository objRssReaderRepository = new RssReaderRepository();
        //Domain.Socioboard.Domain.RssReader objRssReader = new Domain.Socioboard.Domain.RssReader();
        MongoRepository _RssRepository = new MongoRepository("Rss");
        MongoRepository _RssFeedRepository = new MongoRepository("RssFeed");
        FacebookAccountRepository _FacebookAccountRepository = new FacebookAccountRepository();
        TwitterAccountRepository _TwitterAccountRepository = new TwitterAccountRepository();
        [WebMethod]
        public string AddRssFeed(string TextUrl, string Userid, string typeandid)
        {
             int UrlAdded = 0;
             string RetMsg = string.Empty;
            try
            {
               
                string[] profileandidarr = Regex.Split(typeandid, "<:>");
                foreach (var item_data in profileandidarr)
                {
                    string[] profileandid = item_data.Split('~');
                    string profiletype = profileandid[1];
                    string profileid = profileandid[0];
                    string profilename = string.Empty;
                    string profileimageurl = string.Empty;
                    if (profiletype == "facebook")
                    {
                        Domain.Socioboard.Domain.FacebookAccount _FacebookAccount = _FacebookAccountRepository.getFacebookAccountDetailsById(profileid);
                        profilename = _FacebookAccount.FbUserName;
                        profileimageurl = "http://graph.facebook.com/"+profileid+"/picture?type=small";
                    }
                    else if (profiletype == "twitter")
                    {
                        Domain.Socioboard.Domain.TwitterAccount _TwitterAccount = _TwitterAccountRepository.getUserInformation(profileid);
                        profilename = _TwitterAccount.TwitterScreenName;
                        profileimageurl = _TwitterAccount.ProfileImageUrl;
                    }
                    //bool checkurl = objRssFeedsRepository.RssfeddExitbyurl(TextUrl, Guid.Parse(User.Identity.Name),profileid);
                    string rt = ParseFeedUrl(TextUrl, profiletype, profileid, Userid, profilename, profileimageurl);
                    var ret = _RssRepository.Find<Domain.Socioboard.MongoDomain.Rss>(t => t.RssFeedUrl.Equals(TextUrl) && t.ProfileId.Equals(profileid) && t.ProfileType.Equals(profiletype) && t.UserId.Equals(Userid));
                    var task = Task.Run(async () =>
                    {
                        return await ret;
                    });
                    int count = task.Result.Count;

                    if (count<1)
                    {
                        
                        if (rt == "ok")
                        {
                            Domain.Socioboard.MongoDomain.Rss _Rss = new Domain.Socioboard.MongoDomain.Rss();
                            _Rss.Id = ObjectId.GenerateNewId();
                            _Rss.strId = ObjectId.GenerateNewId().ToString();
                            _Rss.RssFeedUrl = TextUrl;
                            _Rss.ProfileType = profiletype;
                            _Rss.ProfileId = profileid;
                            _Rss.UserId = Userid;
                            _Rss.ProfileImageUrl = profileimageurl;
                            _Rss.ProfileName = profilename;

                            _Rss.CreatedOn = DateTime.UtcNow.ToString("yyyy/MM/dd HH:mm:ss");
                            _RssRepository.Add(_Rss);
                            UrlAdded++;
                        }
                        else
                        {
                            return "Please Fill Correct Url For Feeds";
                        }

                    }
                    else
                    {
                        
                    }

                }
            }
            catch (Exception ex)
            {
            }
            if (UrlAdded==1)
            {
                RetMsg ="Url for "+ UrlAdded.ToString() + " account is added";
            }
            else if (UrlAdded > 1)
            {
                RetMsg = "Url for " + UrlAdded.ToString() + " accounts is added";
            }
            else {
                RetMsg = "Url has already added";
            }
            return RetMsg;
        }

        [WebMethod]
        public string ParseFeedUrl(string TextUrl, string profiletype, string profileid, string userId, string profileName, string profileImageUrl)
        {
            try
            {
                
                XmlDocument xmlDoc = new XmlDocument(); // Create an XML document object
                xmlDoc.Load(TextUrl);
                var abc = xmlDoc.DocumentElement.GetElementsByTagName("item");

                foreach (XmlElement item in abc)
                {
                    Domain.Socioboard.MongoDomain.RssFeed objRssFeeds = new Domain.Socioboard.MongoDomain.RssFeed();
                    try
                    {
                        objRssFeeds.Id = ObjectId.GenerateNewId();
                        objRssFeeds.strId = ObjectId.GenerateNewId().ToString();
                        objRssFeeds.ProfileName = profileName;
                        objRssFeeds.ProfileImageUrl = profileImageUrl;
                        //objRssFeeds.UserId = Guid.Parse(User.Identity.Name);

                        try
                        {
                            objRssFeeds.Message = item.ChildNodes[9].InnerText;
                            objRssFeeds.Message = Regex.Replace(objRssFeeds.Message, "<.*?>", string.Empty).Replace("[&#8230;]", "");
                            objRssFeeds.Message = Regex.Replace(objRssFeeds.Message, "@<[^>]+>|&nbsp;", string.Empty);

                        }
                        catch (Exception ex)
                        {
                            try
                            {
                                if (item.ChildNodes[2].InnerText.Contains("www") && item.ChildNodes[2].InnerText.Contains("http"))
                                {
                                    objRssFeeds.Message = item.ChildNodes[1].InnerText;
                                    objRssFeeds.Message = Regex.Replace(objRssFeeds.Message, "<.*?>", string.Empty).Replace("[&#8230;]", "");
                                    objRssFeeds.Message = Regex.Replace(objRssFeeds.Message, "@<[^>]+>|&nbsp;", string.Empty);
                                }
                                else
                                {
                                    objRssFeeds.Message = item.ChildNodes[2].InnerText;
                                    objRssFeeds.Message = Regex.Replace(objRssFeeds.Message, "<.*?>", string.Empty).Replace("[&#8230;]", "");
                                    objRssFeeds.Message = Regex.Replace(objRssFeeds.Message, "@<[^>]+>|&nbsp;", string.Empty);
                                }
                            }
                            catch
                            {
                                objRssFeeds.Message = item.ChildNodes[1].InnerText;
                                objRssFeeds.Message = Regex.Replace(objRssFeeds.Message, "<.*?>", string.Empty).Replace("[&#8230;]", "");
                                objRssFeeds.Message = Regex.Replace(objRssFeeds.Message, "@<[^>]+>|&nbsp;", string.Empty);
                                logger.Error(ex.Message);
                            }
                        }

                        //objRssFeeds.ProfileScreenName = "";

                        try
                        {
                            objRssFeeds.PublishingDate = DateTime.Parse(item.ChildNodes[4].InnerText).ToString("yyyy/MM/dd HH:mm:ss");
                        }
                        catch (Exception ex)
                        {
                            objRssFeeds.PublishingDate = DateTime.Parse(item.ChildNodes[3].InnerText).ToString("yyyy/MM/dd HH:mm:ss");
                            logger.Error(ex.Message);
                        }

                        objRssFeeds.Title = item.ChildNodes[0].InnerText;
                        //objRssFeeds.FeedUrl = TextUrl;
                        //objRssFeeds.Duration = "";
                        //objRssFeeds.CreatedDate = DateTime.Now;

                        if (item.ChildNodes[1].InnerText.Contains("www") || item.ChildNodes[1].InnerText.Contains("http"))
                        {
                            try
                            {
                                objRssFeeds.Link = item.ChildNodes[1].InnerText;

                            }
                            catch (Exception ex)
                            {
                                objRssFeeds.Link = item.ChildNodes[2].InnerText;
                            }
                        }
                        else
                        {
                            objRssFeeds.Link = item.ChildNodes[2].InnerText;
                        }
                        objRssFeeds.RssFeedUrl = TextUrl;
                        objRssFeeds.ProfileId = profileid;
                        objRssFeeds.ProfileType = profiletype;
                        objRssFeeds.Status = false;
                        var ret = _RssFeedRepository.Find<Domain.Socioboard.MongoDomain.RssFeed>(t => t.Link.Equals(objRssFeeds.Link) && t.ProfileId.Equals(profileid) && t.ProfileType.Equals(profiletype));
                        var task = Task.Run(async () =>
                        {
                            return await ret;
                        });
                        int count = task.Result.Count;
                        if (count < 1)
                        {
                            _RssFeedRepository.Add(objRssFeeds);
                        }
                        //objRssFeedsRepository.AddRssFeed(objRssFeeds);
                    }
                    catch (Exception ex)
                    {
                        logger.Error(ex.Message);
                    }

                }
                return "ok";
            }
            catch (Exception ex)
            {
                return "invalid url";
            }
        }
        [WebMethod]

        public string GetRssDataByUser(string UserId)
        {
            List<Domain.Socioboard.MongoDomain.Rss> lst = new List<Domain.Socioboard.MongoDomain.Rss>();
            try
            {
                var ret = _RssRepository.Find<Domain.Socioboard.MongoDomain.Rss>(t => t.UserId.Equals(UserId));
                var task = Task.Run(async () =>
                {
                    return await ret;
                });
                IList<Domain.Socioboard.MongoDomain.Rss> _lst = task.Result;
                 lst = _lst.ToList();
            }
            catch (Exception ex)
            {
                lst = new List<Domain.Socioboard.MongoDomain.Rss>();
            }
            return new JavaScriptSerializer().Serialize(lst);
        }

        [WebMethod]
        public string GetRssData()
        { 
            List<Domain.Socioboard.MongoDomain.Rss> lstrss=new List<Domain.Socioboard.MongoDomain.Rss>();
            var ret = _RssRepository.Find<Domain.Socioboard.MongoDomain.Rss>(t => t.RssFeedUrl != "");
            var task = Task.Run(async () => {
                return await ret;
            });
            IList<Domain.Socioboard.MongoDomain.Rss> _lstrss = task.Result;
            lstrss = _lstrss.OrderByDescending(t => t.CreatedOn).ToList();

            return new JavaScriptSerializer().Serialize(lstrss);
        }


        //[WebMethod]
        //    public string UpdateRssFeed()
        //    {
           
        //        List<Domain.Socioboard.Domain.RssFeeds> _objrssfeed = new List<Domain.Socioboard.Domain.RssFeeds>();
        //        string description = "";
        //        _objrssfeed = objRssFeedsRepository.getAllActiveRssFeedsbystatus();
        //        foreach (var item in _objrssfeed)
        //        {
        //            XmlDocument xmlDoc = new XmlDocument(); // Create an XML document object
        //            xmlDoc.Load(item.FeedUrl);
        //            var _rssdata = xmlDoc.DocumentElement.GetElementsByTagName("item");
        //            foreach (XmlElement item_rss in _rssdata)
        //            {
        //                try
        //                {
        //                    try
        //                    {
                  
        //            string dataDescription = string.Empty;

        //                        description = item_rss.ChildNodes[9].InnerText.Replace("</p>", "").Replace("<p>", "");
        //                        description = Regex.Replace(description, "<.*?>", string.Empty).Replace("[&#8230;]", "");
        //                        description = Regex.Replace(description, "@<[^>]+>|&nbsp;", string.Empty);
        //                        if (description.Contains("<a href=") && description.Contains("/></a>"))
        //                        {
        //                            description = getBetween(description, "/></a>", "<img width=");
        //                            if (description.Contains("<br/><a href="))
        //                            {
        //                                description = Regex.Replace(description, "<.*?>", string.Empty).Replace("[&#8230;]", "");
        //                                description = Regex.Replace(description, "@<[^>]+>|&nbsp;", string.Empty);
        //                            }


        //                        }
        //                        else if (description.Contains("<img width="))
        //                        {
        //                            try
        //                            {
        //                                description = description.Substring(0, description.IndexOf("<img width="));
        //                                description = Regex.Replace(description, "<.*?>", string.Empty).Replace("[&#8230;]", "");
        //                                description = Regex.Replace(description, "@<[^>]+>|&nbsp;", string.Empty);
        //                            }
        //                            catch (Exception ex)
        //                            {
        //                                logger.Error(ex.Message);
        //                                return "";
        //                            }
        //                        }
        //                        else if (description.Contains("[&#8230;]"))
        //                        {
        //                            try
        //                            {
        //                                description = description.Substring(0, description.IndexOf("[&#8230;]"));
        //                                description = Regex.Replace(description, "<.*?>", string.Empty).Replace("[&#8230;]", "");
        //                                description = Regex.Replace(description, "@<[^>]+>|&nbsp;", string.Empty);
        //                            }
        //                            catch (Exception ex)
        //                            {
        //                                logger.Error(ex.Message);
        //                                return "";
        //                            }
        //                        }


        //                    }

        //                    catch (Exception ex)
        //                    {
        //                        try
        //                        {
        //                            if (item_rss.ChildNodes[2].InnerText.Contains("www") && item_rss.ChildNodes[2].InnerText.Contains("http"))
        //                            {
        //                                description = item_rss.ChildNodes[1].InnerText.Replace("</p>", "").Replace("<p>", "");
        //                                description = Regex.Replace(description, "<.*?>", string.Empty).Replace("[&#8230;]", "");
        //                                description = Regex.Replace(description, "@<[^>]+>|&nbsp;", string.Empty);
        //                                if (description.Contains("<a href=") && description.Contains("/></a>"))
        //                                {
        //                                    description = getBetween(description, "/></a>", "<img width=");
        //                                    if (description.Contains("<br/><a href="))
        //                                    {
        //                                        description = Regex.Replace(description, "<.*?>", string.Empty).Replace("[&#8230;]", "");
        //                                        description = Regex.Replace(description, "@<[^>]+>|&nbsp;", string.Empty);
        //                                    }


        //                                }
        //                                else if (description.Contains("<img width="))
        //                                {
        //                                    try
        //                                    {
        //                                        description = description.Substring(0, description.IndexOf("<img width="));
        //                                        description = Regex.Replace(description, "<.*?>", string.Empty).Replace("[&#8230;]", "");
        //                                        description = Regex.Replace(description, "@<[^>]+>|&nbsp;", string.Empty);
        //                                    }
        //                                    catch (Exception er)
        //                                    {
        //                                        logger.Error(er.Message);
        //                                        return "";
        //                                    }
        //                                }
        //                                else if (description.Contains("[&#8230;]"))
        //                                {
        //                                    try
        //                                    {
        //                                        description = description.Substring(0, description.IndexOf("[&#8230;]"));
        //                                        description = Regex.Replace(description, "<.*?>", string.Empty).Replace("[&#8230;]", "");
        //                                        description = Regex.Replace(description, "@<[^>]+>|&nbsp;", string.Empty);
        //                                    }
        //                                    catch (Exception exx)
        //                                    {
        //                                        logger.Error(exx.Message);
        //                                        return "";
        //                                    }
        //                                }

        //                            }
        //                            else
        //                            {
        //                                description = item_rss.ChildNodes[2].InnerText.Replace("</p>", "").Replace("<p>", "");
        //                                description = Regex.Replace(description, "<.*?>", string.Empty).Replace("[&#8230;]", "");
        //                                description = Regex.Replace(description, "@<[^>]+>|&nbsp;", string.Empty);
        //                                if (description.Contains("<a href=") && description.Contains("/></a>"))
        //                                {
        //                                    description = getBetween(description, "/></a>", "<img width=");
        //                                    if (description.Contains("<br/><a href="))
        //                                    {
        //                                        description = Regex.Replace(description, "<.*?>", string.Empty).Replace("[&#8230;]", "");
        //                                        description = Regex.Replace(description, "@<[^>]+>|&nbsp;", string.Empty);
        //                                    }


        //                                }
        //                                else if (description.Contains("<img width="))
        //                                {
        //                                    try
        //                                    {
        //                                        description = description.Substring(0, description.IndexOf("<img width="));
        //                                        description = Regex.Replace(description, "<.*?>", string.Empty).Replace("[&#8230;]", "");
        //                                        description = Regex.Replace(description, "@<[^>]+>|&nbsp;", string.Empty);
        //                                    }
        //                                    catch (Exception eex)
        //                                    {
        //                                        logger.Error(eex.Message);
        //                                        return "";
        //                                    }
        //                                }
        //                                else if (description.Contains("[&#8230;]"))
        //                                {
        //                                    try
        //                                    {
        //                                        description = description.Substring(0, description.IndexOf("[&#8230;]"));
        //                                        description = Regex.Replace(description, "<.*?>", string.Empty).Replace("[&#8230;]", "");
        //                                        description = Regex.Replace(description, "@<[^>]+>|&nbsp;", string.Empty);
        //                                    }
        //                                    catch (Exception erx)
        //                                    {
        //                                        logger.Error(erx.Message);
        //                                        return "";
        //                                    }
        //                                }

        //                            }
        //                        }
        //                        catch
        //                        {
        //                            description = item_rss.ChildNodes[1].InnerText.Replace("</p>", "").Replace("<p>", "");
        //                            description = Regex.Replace(description, "<.*?>", string.Empty).Replace("[&#8230;]", "");
        //                            description = Regex.Replace(description, "@<[^>]+>|&nbsp;", string.Empty);
        //                            if (description.Contains("<a href=") && description.Contains("/></a>"))
        //                            {
        //                                description = getBetween(description, "/></a>", "<img width=");
        //                                if (description.Contains("<br/><a href="))
        //                                {
        //                                    description = Regex.Replace(description, "<.*?>", string.Empty).Replace("[&#8230;]", "");
        //                                    description = Regex.Replace(description, "@<[^>]+>|&nbsp;", string.Empty);
        //                                }


        //                            }
        //                            else if (description.Contains("<img width="))
        //                            {
        //                                try
        //                                {
        //                                    description = description.Substring(0, description.IndexOf("<img width="));
        //                                    description = Regex.Replace(description, "<.*?>", string.Empty).Replace("[&#8230;]", "");
        //                                    description = Regex.Replace(description, "@<[^>]+>|&nbsp;", string.Empty);
        //                                }
        //                                catch (Exception exxx)
        //                                {
        //                                    logger.Error(exxx.Message);
        //                                    return "";
        //                                }
        //                            }
        //                            else if (description.Contains("[&#8230;]"))
        //                            {
        //                                try
        //                                {
        //                                    description = description.Substring(0, description.IndexOf("[&#8230;]"));
        //                                    description = Regex.Replace(description, "<.*?>", string.Empty).Replace("[&#8230;]", "");
        //                                    description = Regex.Replace(description, "@<[^>]+>|&nbsp;", string.Empty);
        //                                }
        //                                catch (Exception eex)
        //                                {
        //                                    logger.Error(eex.Message);
        //                                    return "";
        //                                }
        //                            }

        //                        }
        //                    }
        //                }
        //                catch (Exception ex)
        //                {
        //                    logger.Error(ex.Message);
        //                    return "";
        //                }
        //                bool checkmessage = objRssFeedsRepository.RssfeddExit(description,item.UserId);
        //                if (checkmessage == true)
        //                {
        //                    //return "";
        //                }
        //                else
        //                {
        //                    objRssFeeds.Id = Guid.NewGuid();
        //                    objRssFeeds.Duration = "";
        //                    objRssFeeds.CreatedDate = DateTime.Now;
        //                    objRssFeeds.FeedUrl = item.FeedUrl;
        //                    objRssFeeds.UserId = item.UserId;
        //                    try
        //                    {
        //                        objRssFeeds.PublishingDate = DateTime.Parse(item_rss.ChildNodes[4].InnerText);
        //                    }
        //                    catch (Exception ex)
        //                    {
        //                        objRssFeeds.PublishingDate = DateTime.Parse(item_rss.ChildNodes[3].InnerText);
        //                        logger.Error(ex.Message);
        //                    }

        //                    objRssFeeds.Title = item_rss.ChildNodes[0].InnerText;
                       
                     
        //                    if (item_rss.ChildNodes[1].InnerText.Contains("www") || item_rss.ChildNodes[1].InnerText.Contains("http"))
        //                    {
        //                        try
        //                        {
        //                            objRssFeeds.Link = item_rss.ChildNodes[1].InnerText;

        //                        }
        //                        catch (Exception ex)
        //                        {
        //                            objRssFeeds.Link = item_rss.ChildNodes[2].InnerText;

        //                        }
        //                    }
        //                    else
        //                    {
        //                        objRssFeeds.Link = item_rss.ChildNodes[2].InnerText;
        //                    }

        //                    objRssFeeds.ProfileId = item.ProfileId;
        //                    objRssFeeds.Profiletype = item.Profiletype;
        //                    objRssFeeds.Duration = "";
        //                    objRssFeeds.ProfileScreenName = "";
        //                    objRssFeeds.Message = description;
        //                    objRssFeedsRepository.AddRssFeed(objRssFeeds);
        //                }
        //            }

                
        //        }
        //        return "Added Successfully";
        //    }


        [WebMethod]
        public string PostRssfeed(string profiletype)
        {
            string ret = "";
            //List<Domain.Socioboard.Domain.RssFeeds> _objrssdata=objRssFeedsRepository.GetUnsentRssFeedByProfileType(profiletype);
            List<Domain.Socioboard.MongoDomain.RssFeed> objrssdata = new List<Domain.Socioboard.MongoDomain.RssFeed>();
            var rt = _RssFeedRepository.Find<Domain.Socioboard.MongoDomain.RssFeed>(t => t.Status == false && t.ProfileType.Equals(profiletype));
            var task = Task.Run(async () => {
                return await rt;
            });
            IList<Domain.Socioboard.MongoDomain.RssFeed> _objrssdata = task.Result;
            objrssdata = _objrssdata.ToList();
            foreach (var item in objrssdata)
            {
                if (item.ProfileType=="facebook")
                {
                    try
                    {
                        Facebook objFacebook = new Facebook();
                       ret= objFacebook.FacebookComposeMessageRss(item.Message, item.ProfileId, item.Title, item.Link, item.strId);
                      
                    }
                    catch (Exception ex)
                    {
                        logger.Error(ex.Message);
                        return "";
                    }
                }
              else  if (item.ProfileType == "twitter")
                {
                    try
                    {
                        Twitter objTwitter = new Twitter();
                        string message = "";
                        string UrlShortendata = GetShortenUrl(item.Link);
                        string shortenUrl=string.Empty;
                        try
                        {
                            JObject JData = JObject.Parse(UrlShortendata);
                            if (JData["status_txt"].ToString() == "OK")
                            shortenUrl = JData["data"]["url"].ToString();
                        }
                        catch (Exception ex)
                        {
                        
                        }

                        if (item.Message.Length > 115)
                        {
                             message = item.Message.Substring(0, 115);
                        }
                        else
                        {
                            message = item.Message;
                        }
                        message += " " + shortenUrl;
                        ret = objTwitter.TwitterComposeMessageRss(message, item.ProfileId, item.strId);
                        
                    }
                    catch (Exception ex)
                    {
                        logger.Error(ex.Message);
                        return "";
                    }
                }
               //else if(item.ProfileType == "linkedin")
               //{
               //    try
               //    {
               //        Linkedin objLinkedin = new Linkedin();
               //        string message="";
               //        if (item.Message.Length>600)
               //       {
               //             message= item.Message.Substring(0,590);
		 
               //     }
               //        else
               //        {
               //            message = item.Message;
               //        }
                          
               //        ret =objLinkedin.LinkedinComposeMessageRss(item.Message, item.ProfileId, item.UserId.ToString());
                      
               //    }
               //    catch (Exception ex)
               //    {
               //        logger.Error(ex.Message);
               //        return "";
               //    }
               // }
                //objRssReader.Id = Guid.NewGuid();
                //objRssReader.FeedsUrl = item.FeedUrl;
                //objRssReader.Link = item.Link;
                //objRssReader.Title = item.Title;
                //objRssReader.Status = true;
                //objRssReader.PublishedDate = item.PublishingDate.ToString();
                //objRssReader.CreatedDate = DateTime.Now;
                //objRssReader.Description = item.Message;
                //objRssReader.ProfileId = item.ProfileId;
                //objRssReader.UserId = item.UserId.ToString();
                //objRssReader.ProfileType = item.ProfileType;
                //objRssReaderRepository.AddRssReader(objRssReader);
                
             }
            return ret;
           
        }


        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string GetAllRssPostFeed(string RssUrls)
        {
            //try
            //{
            //    Guid Userid = Guid.Parse(User.Identity.Name);
            //    List<Domain.Socioboard.Domain.RssReader> objRssReader = new List<Domain.Socioboard.Domain.RssReader>();
            //    objRssReader = objRssReaderRepository.getAllRss(Userid);
            //    return new JavaScriptSerializer().Serialize(objRssReader);
            //}
            //catch (Exception ex)
            //{
            //    logger.Error(ex.Message);
            //    return "";
            //}
            List<Domain.Socioboard.MongoDomain.RssFeed> lst;
            try
            {
                
                string[] arrUrl = RssUrls.Split(',');
                var ret = _RssFeedRepository.Find<Domain.Socioboard.MongoDomain.RssFeed>(t => arrUrl.Contains(t.RssFeedUrl));
                var task = Task.Run(async () =>
                {
                    return await ret;
                });
                IList<Domain.Socioboard.MongoDomain.RssFeed> _lst = task.Result;
                lst = _lst.OrderByDescending(t => t.PublishingDate).ToList();
            }
            catch (Exception ex)
            {
                lst = new List<Domain.Socioboard.MongoDomain.RssFeed>();
            }

            return new JavaScriptSerializer().Serialize(lst);

        }
        [WebMethod]
        public string GetAllFeedInfo(string access_token)
        {
            try
            {
                Guid Userid = Guid.Parse(User.Identity.Name);
                List<Domain.Socioboard.Domain.RssFeeds> objRssReader = new List<Domain.Socioboard.Domain.RssFeeds>();
                objRssReader = objRssFeedsRepository.getAllRssFeedInfo(Userid);
                return new JavaScriptSerializer().Serialize(objRssReader);
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                return "";
            }
        }

        [WebMethod]
        public string EditFeedUrl(string NewFeedUrl,string OldFeedUrl,string RssId)
        {
            //int str = 0;
            //try
            //{
            //    str = objRssFeedsRepository.updateFeedurl(Guid.Parse(User.Identity.Name), NewFeedUrl, OldFeedUrl, ProfileId);
            //    if (str!=0)
            //    {
            //        return "Success";
            //    }
            //    else
            //    {
            //        return "Error";
            //    }
            //}
            //catch (Exception ex)
            //{
            //    logger.Error(ex.Message);
            //    return "";
            //}


            try
            {
                var builders = Builders<BsonDocument>.Filter;
                FilterDefinition<BsonDocument> filter = builders.Eq("strId", RssId);
                var update = Builders<BsonDocument>.Update.Set("RssFeedUrl", NewFeedUrl);
                _RssRepository.Update<Domain.Socioboard.MongoDomain.Rss>(update, filter);
                return "Success";
            }
            catch (Exception ex)
            {
                return "Error";
            }
            
        }

        [WebMethod]
        public string DeletePofile(string RssId)
        {
            //string str = string.Empty;
            //int i = 0;
            //try
            //{
            //    i = objRssFeedsRepository.DeleteProfile(Guid.Parse(User.Identity.Name), FeedUrl, ProfileId);
            //        if (i!=0)
            //        {
            //            return str = "success";
            //        }
            //        else
            //        {
            //            return str;
            //        }
            //  }
                
          
            //catch (Exception ex)
            //{
            //    logger.Error(ex.Message);
            //    return str;
            //}
            try
            {
                var builders = Builders<Domain.Socioboard.MongoDomain.Rss>.Filter;

                FilterDefinition<Domain.Socioboard.MongoDomain.Rss> filter = builders.Eq("strId", RssId);
                _RssRepository.Delete<Domain.Socioboard.MongoDomain.Rss>(filter);
                return "success";
            }
            catch (Exception ex)
            {
                return "Error";
            }
        }


        public static string getBetween(string strSource, string strStart, string strEnd)
        {
            int Start, End;
            if (strSource.Contains(strStart) && strSource.Contains(strEnd))
            {
                Start = strSource.IndexOf(strStart, 0) + strStart.Length;
                End = strSource.IndexOf(strEnd, Start);
                return strSource.Substring(Start, End - Start);
            }
            else
            {
                return "";
            }
        }
        [WebMethod]
        public string GetShortenUrl(string Url)
        {
            try
            {
                string url = "https://api-ssl.bitly.com/v3/shorten?access_token=" + ConfigurationManager.AppSettings["bitlyaccesstoken"].ToString() + "&longUrl=" + Url + "&domain=bit.ly&format=json";
                HttpWebRequest httpRequest = (HttpWebRequest)WebRequest.Create(url);
                httpRequest.Method = "GET";
                httpRequest.ContentType = "application/x-www-form-urlencoded";
                HttpWebResponse httResponse = (HttpWebResponse)httpRequest.GetResponse();
                Stream responseStream = httResponse.GetResponseStream();
                StreamReader responseStreamReader = new StreamReader(responseStream, System.Text.Encoding.Default);
                string pageContent = responseStreamReader.ReadToEnd();
                responseStreamReader.Close();
                responseStream.Close();
                httResponse.Close();
                return pageContent;
            }
            catch (Exception ex)
            {
                return Url;
            }
        }

    }
}


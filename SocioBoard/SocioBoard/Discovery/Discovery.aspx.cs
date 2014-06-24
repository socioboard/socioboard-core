using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SocioBoard.Model;
using SocioBoard.Helper;
using Newtonsoft.Json.Linq;
using GlobusTwitterLib.Authentication;
using GlobusTwitterLib.App.Core;
using SocioBoard.Domain;
using System.Net;
using System.IO;
using System.Text;

namespace SocioBoard.Discovery
{
    public partial class Discovery : System.Web.UI.Page
    {

        User user = new User();

        protected void Page_Load(object sender, EventArgs e)
        {
            user = (User)Session["LoggedUser"];

            DiscoverySearchRepository disreop = new DiscoverySearchRepository();
            disreop.getAllSearchKeywords(user.Id);
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            
                    int i = 0;
            string searchRes = string.Empty;
            if (!string.IsNullOrEmpty(txtSearchText.Text))
            {
                DiscoverySearch dissearch = new DiscoverySearch();
                DiscoverySearchRepository dissearchrepo = new DiscoverySearchRepository();

                List<DiscoverySearch> discoveryList = dissearchrepo.getResultsFromKeyword(txtSearchText.Text);

                if (discoveryList.Count == 0)
                {
                        #region TwitterSearch

                    try
                    {
                        string twitterSearchUrl = "http://search.twitter.com/search.json?q=" + txtSearchText.Text;
                        var request = (HttpWebRequest)WebRequest.Create(twitterSearchUrl);
                        request.Method = "GET";
                        string output = string.Empty;
                        try
                        {
                            using (var response = request.GetResponse())
                            {
                                using (var stream = new StreamReader(response.GetResponseStream(), Encoding.GetEncoding(1252)))
                                {
                                    output = stream.ReadToEnd();
                                }
                            }
                            if (!output.StartsWith("["))
                                output = "[" + output + "]";


                            JArray twitterSearchResult = JArray.Parse(output);

                            foreach (var item in twitterSearchResult)
                            {
                                var results = item["results"];

                                foreach (var chile in results)
                                {
                                    try
                                    {
                                        dissearch.CreatedTime = SocioBoard.Helper.Extensions.ParseAnotherTwitterTime(chile["created_at"].ToString().TrimStart('"').TrimEnd('"')); ;
                                        dissearch.EntryDate = DateTime.Now;
                                        dissearch.FromId = chile["from_user_id_str"].ToString().TrimStart('"').TrimEnd('"');
                                        dissearch.FromName = chile["from_user_name"].ToString().TrimStart('"').TrimEnd('"');
                                        dissearch.ProfileImageUrl = chile["profile_image_url"].ToString().TrimStart('"').TrimEnd('"');
                                        dissearch.SearchKeyword = txtSearchText.Text;
                                        dissearch.Network = "twitter";
                                        dissearch.Message = chile["text"].ToString().TrimStart('"').TrimEnd('"');
                                        dissearch.MessageId = chile["id_str"].ToString().TrimStart('"').TrimEnd('"');
                                        dissearch.Id = Guid.NewGuid();
                                        dissearch.UserId = user.Id;


                                        if (!dissearchrepo.isKeywordPresent(dissearch.SearchKeyword, dissearch.MessageId))
                                        {
                                            dissearchrepo.addNewSearchResult(dissearch);
                                        }

                                        searchRes += this.BindData(dissearch, i);


                                        i++;
                                    }
                                    catch (Exception ex)
                                    {
                                        Console.WriteLine(ex.StackTrace);
                                    }
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.StackTrace);
                        }
                    #endregion
                        #region FacebookSearch
                        try
                        {
                            int j = 0;
                            string facebookSearchUrl = "https://graph.facebook.com/search?q= " + txtSearchText.Text + " &type=post";
                            var facerequest = (HttpWebRequest)WebRequest.Create(facebookSearchUrl);
                            facerequest.Method = "GET";
                            string outputface = string.Empty;
                            using (var response = facerequest.GetResponse())
                            {
                                using (var stream = new StreamReader(response.GetResponseStream(), Encoding.GetEncoding(1252)))
                                {
                                    outputface = stream.ReadToEnd();
                                }
                            }
                            if (!outputface.StartsWith("["))
                                outputface = "[" + outputface + "]";


                            JArray facebookSearchResult = JArray.Parse(outputface);

                            foreach (var item in facebookSearchResult)
                            {
                                var data = item["data"];

                                foreach (var chile in data)
                                {
                                    try
                                    {
                                        dissearch.CreatedTime = DateTime.Parse(chile["created_time"].ToString());
                                        dissearch.EntryDate = DateTime.Now;
                                        dissearch.FromId = chile["from"]["id"].ToString();
                                        dissearch.FromName = chile["from"]["name"].ToString();
                                        dissearch.ProfileImageUrl = "http://graph.facebook.com/" + chile["from"]["id"] + "/picture?type=small";
                                        dissearch.SearchKeyword = txtSearchText.Text;
                                        dissearch.Network = "facebook";
                                        dissearch.Message = chile["message"].ToString();
                                        dissearch.MessageId = chile["id"].ToString();
                                        dissearch.Id = Guid.NewGuid();
                                        dissearch.UserId = user.Id;


                                        if (!dissearchrepo.isKeywordPresent(dissearch.SearchKeyword, dissearch.MessageId))
                                        {
                                            dissearchrepo.addNewSearchResult(dissearch);
                                        }

                                        searchRes += this.BindData(dissearch, i);






                                    }
                                    catch (Exception ex)
                                    {
                                        Console.WriteLine(ex.StackTrace);
                                    }


                                }




                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.StackTrace);
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                    }
                        #endregion
                }
                else
                {
                    foreach (DiscoverySearch item in discoveryList)
                    {
                      searchRes +=   this.BindData(item, i);
                        i++;
                    }
                }
                searchresults.InnerHtml = searchRes;
               

            }

        }

        public string BindData(DiscoverySearch dissearch,int i)
        {
            string message = string.Empty;
            if (dissearch.Network == "facebook")
            {
                message += "<div  class=\"messages\"><section>" +
                                       "<aside>" +
                                       "<section data-sstip_class=\"twt_avatar_tip\" class=\"js-avatar_tip\">" +
                                      "<a class=\"avatar_link view_profile\">" +
                                      " <img width=\"54\" height=\"54\" border=\"0\" src=\"" + dissearch.ProfileImageUrl + "\" class=\"avatar\" id=\" " + dissearch.FromId + "\">" +
                                       "<article class=\"message-type-icon\">" +
                                          "<span class=\"twitter_bm\">" +
                                              "<img width=\"16\" height=\"16\" src=\"../Contents/Images/fb_icon.png\">" +
                                              "</span>" +
                                         "</article>" +
                                               "</a>" +
                                         "</section>" +
                                          "<ul></ul>" +
                                                "</aside>" +
                                               "<article>" +
                                    "<div class=\"\"><a href=\"\" class=\"language\"></a></div>" +
                                    "<div class=\"message_actions\"><a href=\"#\" class=\"gear_small\"><span class=\"ficon\" title=\"Options\">?</span></a></div>" +
                                    "<div class=\"message-text font-14\" id=\"messagedescription_1\">" + dissearch.Message + "</div>" +
                                    "<section class=\"bubble-meta\">" +
                                    "<article class=\"threefourth text-overflow\">" +
                                      "<section class=\"floatleft\">" +
                                       "<a data-sstip_class=\"twt_avatar_tip\" class=\"js-avatar_tip view_profile profile_link\">" +
                                        "<span id=\"rowname_" + i + "\">" + dissearch.FromName + "</span>" +
                                                       "</a>&nbsp;" +
                                       "<a title=\"View message on Twitter\" target=\"_blank\" class=\"time\" data-msg-time=\"1363926699000\">" + dissearch.CreatedTime + "</a>" +
                                       "<span  class=\"location\">&nbsp;</span>" +
                                       "</section>" +
                                     "</article>" +
                                     "<ul class=\"message-buttons quarter clearfix\">" +
                                              "</ul>" +
                                    "</section>" +
                                   "</article>" +
                                  "</section></div>";
                i++;
            }
            else if (dissearch.Network == "twitter")
            {
                message += "<div  class=\"messages\"><section>" +
                                          "<aside>" +
                                          "<section data-sstip_class=\"twt_avatar_tip\" class=\"js-avatar_tip\">" +
                                         "<a class=\"avatar_link view_profile\">" +
                                         " <img width=\"54\" height=\"54\" border=\"0\" src=\"" + dissearch.ProfileImageUrl + "\" class=\"avatar\" id=\" " + dissearch.FromId + "\">" +
                                          "<article class=\"message-type-icon\">" +
                                             "<span class=\"twitter_bm\">" +
                                                 "<img width=\"16\" height=\"16\" src=\"../Contents/Images/twticon.png\">" +
                                                 "</span>" +
                                            "</article>" +
                                                  "</a>" +
                                            "</section>" +
                                             "<ul></ul>" +
                                                   "</aside>" +
                                                  "<article>" +
                                       "<div class=\"\"><a href=\"\" class=\"language\"></a></div>" +
                                       "<div class=\"message_actions\"><a href=\"#\" class=\"gear_small\"><span class=\"ficon\" title=\"Options\">?</span></a></div>" +
                                       "<div class=\"message-text font-14\" id=\"messagedescription_1\">" + dissearch.Message + "</div>" +
                                       "<section class=\"bubble-meta\">" +
                                       "<article class=\"threefourth text-overflow\">" +
                                         "<section class=\"floatleft\">" +
                                          "<a data-sstip_class=\"twt_avatar_tip\" class=\"js-avatar_tip view_profile profile_link\">" +
                                           "<span id=\"rowname_" + i + "\">" + dissearch.FromName + "</span>" +
                                                          "</a>&nbsp;" +
                                          "<a title=\"View message on Twitter\" target=\"_blank\" class=\"time\" data-msg-time=\"1363926699000\">" + dissearch.CreatedTime + "</a>" +
                                          "<span  class=\"location\">&nbsp;</span>" +
                                          "</section>" +
                                        "</article>" +
                                        "<ul class=\"message-buttons quarter clearfix\">" +
                                                 "</ul>" +
                                       "</section>" +
                                      "</article>" +
                                     "</section></div>";
            }
            return message;
        }


    }
}
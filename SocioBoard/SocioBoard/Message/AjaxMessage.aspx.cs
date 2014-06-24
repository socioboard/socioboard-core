using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SocioBoard.Model;
using SocioBoard.Domain;
using System.Collections;
using System.Data;
using SocioBoard.Helper;

namespace SocioBoard.Message
{
    public partial class AjaxMessage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            ProcessRequest();

        }

        void ProcessRequest()
        {
            User user = (User)Session["LoggedUser"];
            if (Request.QueryString["op"] != null)
            {
                if (Request.QueryString["op"] == "bindMessages")
                {
                     DataSet ds = null;
                    if (Session["MessageDataTable"] == null)
                    {
                        clsFeedsAndMessages clsfeedsandmess = new clsFeedsAndMessages();
                        ds = clsfeedsandmess.bindMessagesIntoDataTable(user);
                        Session["MessageDataTable"] = ds;

                    }
                    else
                    {
                        ds = (DataSet)Session["MessageDataTable"];
                    }
                        string message = this.BindData(ds.Tables[0]);
                    Response.Write(message);

                }
                else if (Request.QueryString["op"] == "bindProfiles")
                {

                    string profiles = string.Empty;
                    int i = 0;
                    profiles += "<ul class=\"options_list\">";

                    /*Binding facebook profiles in Accordian*/
                    FacebookAccountRepository facerepo = new FacebookAccountRepository();
                    ArrayList alstfacebookprofiles = facerepo.getAllFacebookAccountsOfUser(user.Id);
                    foreach (FacebookAccount item in alstfacebookprofiles)
                    {
                        profiles += "<li><a id=\"greencheck_" + i + "\" ><span class=\"network_icon\">" +
                                    "<img src=\"../Contents/Images/fb_icon.png\" width=\"17\" height=\"16\" alt=\"\" /></span>" +
                                 "<span id=\"profileusername_" + i + "\" class=\"user_name\">" + item.FbUserName + "</span><input type=\"hidden\" id=\"fbhidden_" + i + "\" value=\"" + item.FbUserId + "\" /> <span id=\"checkid_" + i + "\" class=\"checkbx_green\">" +
                                     "<img id=\"checkimg_" + i + "\" src=\"../Contents/Images/msg/network_click.png\" width=\"17\" height=\"17\" alt=\"\" onclick=\"checkprofile(this.id,'message','facebook');\" /></span>" +
                             "</a></li> ";
                        i++;
                    }


                    /*Binding TwitterProfiles in Accordian*/
                    TwitterAccountRepository twtaccountrepo = new TwitterAccountRepository();
                    ArrayList alsttwt = twtaccountrepo.getAllTwitterAccountsOfUser(user.Id);
                    foreach (TwitterAccount item in alsttwt)
                    {
                         profiles += "<li><a id=\"greencheck_" + i + "\" ><span class=\"network_icon\">" +
                                      "<img src=\"../Contents/Images/msg/network_twt.png\" width=\"17\" height=\"16\" alt=\"\" /></span>" +
                                   "<span id=\"profileusername_" + i + "\" class=\"user_name\">" + item.TwitterScreenName + "</span> <span id=\"checkid_" + i + "\" class=\"checkbx_green\"><input type=\"hidden\" id=\"twthidden_" + i + "\" value=\"" + item.TwitterUserId+ "\">" +
                                       "<img id=\"checkimg_" + i + "\" src=\"../Contents/Images/msg/network_click.png\" width=\"17\" height=\"17\" alt=\"\"  onclick=\"checkprofile(this.id,'message','twitter');\"/></span>" +
                               "</a></li> ";
                         i++;
                    }



                    profiles += "</ul><input type=\"hidden\" id=\"profilecounter\" value=\"" + i + "\">";
                    Response.Write(profiles);                
                }
                else if (Request.QueryString["op"] == "savetask")
                {
                    string descritption = Request.QueryString["description"];
                    Guid idtoassign = Guid.Empty;
                    try
                    {
                        if (Request.QueryString["memberid"] != string.Empty)
                        {
                            idtoassign = Guid.Parse(Request.QueryString["memberid"].ToString());
                        }                       
                    }
                    catch
                    {
                       // idtoassign = 0;
                    }
                    Tasks objTask = new Tasks();
                    TaskRepository objTaskRepo = new TaskRepository();
                    objTask.AssignDate = DateTime.Now.ToString("yyyy-MM-dd H:mm:ss").ToString();
                    objTask.AssignTaskTo = idtoassign;
                    objTask.TaskStatus = false;
                    objTask.TaskMessage = descritption;
                    objTask.UserId = user.Id;
                    Guid taskid = Guid.NewGuid();
                    objTask.Id = taskid;
                    objTaskRepo.addTask(objTask);
                  
                    /////////////////       
                    string comment = Request.QueryString["comment"];
                    if (!string.IsNullOrEmpty(comment))
                    {
                        string curdate = DateTime.Now.ToString("yyyy-MM-dd H:mm:ss").ToString();
                        TaskComment objcmt = new TaskComment();
                        TaskCommentRepository objcmtRepo = new TaskCommentRepository();
                        objcmt.Comment = comment;
                        objcmt.CommentDate = DateTime.Now;
                        objcmt.EntryDate = DateTime.Now;
                        objcmt.Id = Guid.NewGuid();
                        objcmt.TaskId = objTask.Id;
                        objcmt.UserId = user.Id;
                        objcmtRepo.addTaskComment(objcmt);
                    }

                }
                else if (Request.QueryString["op"] == "bindteam")
                {
                    TeamRepository objTeam = new TeamRepository();
                    string message = string.Empty;
                    message += "<ul>";
                    IEnumerable<dynamic> result = objTeam.getAllTeamsOfUser(user.Id);

                    if (result != null)
                    {
                        foreach (Team item in result)
                        {
                            message += "<li><a><img src=\"../Contents/Images/blank_img.png\" alt=\"\" />" +
                                            "<span class=\"name\">" +
                                                item.FirstName + " " + item.LastName+
                                            "</span>" +
                                         " <span>" +
                                         "<input id=\"customerid_" + item.Id + "\" type=\"radio\" name=\"team_members\" value=\"customerid_" + item.Id + "\">" +
                                         "</span>" +
                                        "</a></li>";
                        }
                        message += "<li><a><img src=\"../Contents/Images/blank_img.png\" alt=\"\" />" +
                                        "<span class=\"name\">" +
                                           user.UserName +
                                        "</span>" +
                                     " <span>" +
                                     "<input id=\"customerid_" + user.Id + "\" type=\"radio\" name=\"team_members\" value=\"customerid_" + user.Id + "\">" +
                                     "</span>" +
                                    "</a></li>";

                    }
                    else
                    {
                        message += "<li><a><img src=\"../Contents/Images/blank_img.png\" alt=\"\" />" +
                                         "<span class=\"name\">" +
                                            user.UserName +
                                         "</span>" +
                                      " <span>" +
                                      "<input id=\"customerid_" + user.Id + "\" type=\"radio\" name=\"team_members\" value=\"customerid_" + user.Id + "\">" +
                                      "</span>" +
                                     "</a></li>";

                    }
                    message += "</ul>";
                    Response.Write(message);
                }
            }
        }

        public string BindData(DataTable dt)
        {
            string message = string.Empty;
            DataView dv = dt.DefaultView;
            dv.Sort = "MessageDate desc";
            DataTable sortedDT = dv.ToTable();
            int sorteddatacount = 0;
            DataRow[] array_dr = dt.Select("MessageDate like '%'", "MessageDate desc");
            foreach (DataRow row in array_dr)
            {

                if (row["Network"].ToString() == "twitter")
                    {
                        message += "<div id=\"messagetaskable_" + sorteddatacount + "\" class=\"messages taskable\"><section><aside><section class=\"js-avatar_tip\" data-sstip_class=\"twt_avatar_tip\">" +
                                    "<a class=\"avatar_link view_profile\" href=\"javascript:void(0)\"><img id=\"formprofileurl_" + sorteddatacount + "\" onclick=\"detailsprofile(this.alt);\" width=\"54\" height=\"54\" border=\"0\" class=\"avatar\" src=\"" + row["FromProfileUrl"] + "\" alt=\"" + row["FromName"] + "\">" +
                                     "<article class=\"message-type-icon\"><span class=\"twitter_bm\"><img src=\"../Contents/Images/twticon.png\" width=\"16\" height=\"16\" /></span></article></a></section><ul></ul></aside><article><div class=\"\">" +
                                     "<a class=\"language\" href=\"\"></a></div><div class=\"message_actions\"><a class=\"gear_small\" href=\"#\"><span title=\"Options\" class=\"ficon\">?</span>" +
                                     "</a></div>";

                        message += "<div id=\"messagedescription_" + sorteddatacount + "\" class=\"message-text font-14\">" + row["Message"] + "</div>" +
                                        "<section class=\"bubble-meta\">" +
                                           "<article class=\"threefourth text-overflow\">" +
                                               "<section class=\"floatleft\">" +
                                                   "<a data-tip=\"View Yaroslav Lukashev's Profile\"  class=\"js-avatar_tip view_profile profile_link\" data-sstip_class=\"twt_avatar_tip\"><span id=\"network_" + sorteddatacount + "\" style=\"display:none;\">twitter</span><span style=\"display:none;\" id=\"messageid_" + sorteddatacount + "\">" + row["MessageId"] + "</span><span style=\"display:none;\" id=\"fromid_" + sorteddatacount + "\">" + row["FromId"] + "</span><span style=\"display:none;\" id=\"rowid_" + sorteddatacount + "\">" + row["ProfileId"] + "</span><span id=\"rowname_" + sorteddatacount + "\" onclick=\"detailsprofile(this.id);\">" + row["FromName"] + "</span>" +
                                                   "</a>&nbsp;<a id=\"createdtime_" + sorteddatacount + "\" data-msg-time=\"1363926699000\" class=\"time\" target=\"_blank\" title=\"View message on Twitter\" href=\"#\">" + row["MessageDate"].ToString() + "</a>, <span class=\"location\">&nbsp;</span>" +
                                        "</section></article><ul class=\"message-buttons quarter clearfix\"><li><a href=\"#\"><img src=\"../Contents/Images/replay.png\" alt=\"\" width=\"17\" height=\"24\" border=\"none\"  onclick=replyfunction(" + sorteddatacount + ") ></a></li>" +
                                        "<li><a id=\"createtasktwt_" + sorteddatacount + "\" href=\"#\" onclick=\"createtask(this.id);\"><img src=\"../Contents/Images/pin.png\" alt=\"\" width=\"14\" height=\"17\" border=\"none\"></a></li>" +
                                          "<li><a id=\"savearchive_" + sorteddatacount + "\" href=\"#\" onclick=\"savearchivemsg(" + sorteddatacount + ");\"><img src=\"../Contents/Images/archive.png\" alt=\"\" width=\"14\" height=\"17\" border=\"none\"></a></li>" +
                                           "</ul></section></article></section> </div>";
                    }
                else if (row["Network"].ToString() == "facebook")
                    {
                        message += "<div id=\"messagetaskable_" + sorteddatacount + "\" class=\"messages taskable\"><section><aside><section class=\"js-avatar_tip\" data-sstip_class=\"twt_avatar_tip\">" +
                                        "<a class=\"avatar_link view_profile\" href=\"javascript:void(0)\"><img id=\"formprofileurl_" + sorteddatacount + "\" width=\"54\" height=\"54\" border=\"0\" class=\"avatar\" src=\"" + row["FromProfileUrl"] + "\" alt=\"" + row["FromId"] + "\" onclick=\"getFacebookProfiles(this.alt);\" >" +
                                         "<article class=\"message-type-icon\"><span class=\"facebook_bm\"><img src=\"../Contents/Images/fb_icon.png\"  width=\"16\" height=\"16\"/></span></article></a></section><ul></ul></aside><article><div class=\"\">" +
                                         "<a class=\"language\" href=\"\"></a></div><div class=\"message_actions\"><a class=\"gear_small\" href=\"#\"><span title=\"Options\" class=\"ficon\">?</span>" +
                                         "</a></div><div id=\"messagedescription_" + sorteddatacount + "\" class=\"message-text font-14\">" + row["Message"] + "</div>" +
                                          "<section class=\"bubble-meta\">" +
                                             "<article class=\"threefourth text-overflow\">" +
                                                 "<section class=\"floatleft\">" +
                                                     "<a data-tip=\"View Yaroslav Lukashev's Profile\"  class=\"js-avatar_tip view_profile profile_link\"  data-sstip_class=\"twt_avatar_tip\"   ><span id=\"network_" + sorteddatacount + "\" style=\"display:none;\">facebook</span><span style=\"display:none;\" id=\"rowid_" + sorteddatacount + "\">" + row["ProfileId"] + "</span><span style=\"display:none;\" id=\"rowname_" + sorteddatacount + "\">" + row["FromName"] + "</span><span style=\"display:none;\" id=\"messageid_" + sorteddatacount + "\">" + row["MessageId"] + "</span><span style=\"display:none;\" id=\"fromid_" + sorteddatacount + "\">" + row["FromId"] + "</span><span id=\"" + row["FromId"] + "\"onclick=\"getFacebookProfiles(this.id);\" >" + row["FromName"] + "</span>" +
                                                     "</a>&nbsp;<a id=\"createdtime_" + sorteddatacount + "\" data-msg-time=\"1363926699000\" class=\"time\" target=\"_blank\" title=\"View message on Twitter\" href=\"#\">" + row["MessageDate"].ToString() + "</a><span class=\"location\">&nbsp;</span>" +
                                          "</section></article><ul class=\"message-buttons quarter clearfix\"><li><a href=\"#\"><img src=\"../Contents/Images/replay.png\" alt=\"\" width=\"17\" height=\"24\" border=\"none\" onclick=replyfunction(" + sorteddatacount + ") ></a></li>" +
                                          "<li><a  id=\"createtasktwt_" + sorteddatacount + "\" href=\"#\" onclick=\"createtask(this.id);\"><img src=\"../Contents/Images/pin.png\" alt=\"\" width=\"14\" height=\"17\" border=\"none\"></a></li>" +
                                          "<li><a id=\"savearchive_" + sorteddatacount + "\" href=\"#\" onclick=\"savearchivemsg(" + sorteddatacount + ");\"><img src=\"../Contents/Images/archive.png\" alt=\"\" width=\"14\" height=\"17\" border=\"none\"></a></li>" +
                                            "</ul></section></article></section> </div>";

                    }
                    
                    sorteddatacount++;
                
            }
            return message;


        }
    }
}
<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Site.Master" AutoEventWireup="true"
    CodeBehind="~/Message/Task.aspx.cs" Inherits="SocioBoard.Message.Task" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../Contents/Styles/Style.css" rel="stylesheet" type="text/css" />
      <script src="../Contents/Scripts/Home.js" type="text/javascript"></script>
       <script src="../Contents/Scripts/Message.js" type="text/javascript"></script>
  
    <script type="text/javascript">
        var tmid = "";
        var taskid = "";
        var comment_task_id = "";
        var assigntasktoid = "";
        function getmemberdata(tmemberid) {
            debugger;
            try {
                $("#inbox_comments").show();
                $("#inbox_msgs").hide();
                tmid = tmemberid;
              //  assigntasktoid = tmid.split('_')[0];
                var j = "";
               // j = tmid.split('_')[1];
                var tmhtml = document.getElementById("Section" + tmemberid).innerHTML;
                $("#divtaskcomment").html("<section id=\"Section_1\">" + tmhtml + "</section>");
                $("#taskcomment").html("");
                var Taskcmt = document.getElementById("commentId");

                taskid = $("#task_comment_" + tmemberid).val();
                $("#<%=hdnTask_id.ClientID%>").val(tmemberid);
                comment_task_id = $("#hdncommentsid").val();
                var tmhtml = "";
                if (comment_task_id != null) {
                    $("#" + tmemberid).css('display', 'block');
                }
            }
            catch (e) {
                //  alert(e);
            }
        }

        $(document).ready(function () {
            $("#home").removeClass('active');
            $("#feeds").removeClass('active');
            $("#message").addClass('active');
            $("#publishing").removeClass('active');
            $("#discovery").removeClass('active');

         //   CallInbox();
        });

        function PerformClick(id) {
//            alert("id :" + id);
            debugger;
            taskid = id.split('_')[1];
            var status = id.split('_')[2];
            $("#<%=hdnstatus.ClientID %>").val(status);
            $("#<%=hdnTask_id.ClientID %>").val(taskid);
            document.getElementById('<%=btnchangestatus.ClientID %>').click();


        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container_main">
        <div class="container_wrapper">
            <div class="ws_container_page">
                <aside role="complementary" class="unselectable" id="actions">
                                   <ul class="msgs_nav">
                <li id="inboxli" class="accordion single" onclick ="CallInbox();">
                    <a  href="Message.aspx" onclick="" >
                        <span class="nav_icon">
                            <span data-tip="" class="responseRate proxima ss_tip tip_left">
                                <span class="heart dark active">
                                    <img src="../Contents/Images/heart-dark-mask-outline02.png" class="mask" alt="" />
                                        <span><span style="height: 0%;" class="fill"></span></span>
                                    <img src="../Contents/Images/heart-dark-fill.png" class="heart-fill" alt="" />
                                </span>
                                <span class="heart dark broken" style="display: none;"><img src="../Contents/Images/heart-broken-small.png" alt="" /></span>
                            </span>
                        </span>
                        <span class="text">
                                <span class="label">Smart Inbox</span>  
                                <span class="numeric" id="smartinbox_count" style="display:none"><span>0</span></span>
                        </span>
                    </a>
                </li>
                    <li id="tasksli" class="accordion single selected">
                    <a href="Task.aspx">
                        <span class="nav_icon">
                            <span class="msg_queue"></span>
                        </span>
                        <span class="text">
                            <span class="label">My Tasks</span>
                        </span>
                    </a>
                </li>
                    <li id="sentli" class="accordion  single">
                    <a  href="#">
                        <span class="nav_icon">
                            <span class="msg_sent"></span>
                        </span>
                        <span class="text">Sent Messages</span>
                    </a>
                
                </li>
                    <li id="instagramli" class="accordion  single">
                    <a  href="#" onclick="instagramcall();">
                        <span class="nav_icon">
                            <span class="msg_sent"></span>
                        </span>
                        <span class="text">Instagram</span>
                    </a>
      
                </li>

                <li class="accordion  single">
                    <a  href="#" >
                        <span class="nav_icon">
                            <span class="msg_sent"></span>
                        </span>
                        <span class="text">Google Plus</span>
                    </a>
          
                </li>
                         
                      <li id="archivemsg" class="accordion  single">
                    <a  href="#">
                        <span class="nav_icon">
                            <span class="msg_archive"></span>
                        </span>
                        <span class="text">Archive</span>
                    </a>
                </li>

            </ul>

    				</aside>
                <div id="content" role="main">
                    <section id="inbox_msgs" class="threefourth messages msg_view">
                            <div class="tasks-header">
                            	<div class="task-owner">Owner</div>
                                <div class="task-activity">Last Activity</div>
                                <div class="task-message">Task</div>
                                <div class="task-status">Status</div>
                            </div>
                            <div id="taskdiv" class="messages taskable" runat="server">
                            	<%--<section>
                                	<div class="js-task-cont read">
                                        <section class="task-owner">
                                        	<img width="32" height="32" border="0" class="avatar" src="../Contents/Images/blank_img.png" />
                                        </section>
                                        <section class="task-activity third">
                                        	<div>Apr 6, 2013 10:46 am</div>
                                            <p title="Assigned by anagha deshpande">Assigned by anagha d.</p>
                                        </section>
                                        <section class="task-message font-13 third">
                                        	<a class="tip_left">Anagha</a>
                                            <p>globus_auction • 2 days ago</p>
                                         </section>
                                         <section class="task-status">
                                         	<span id="taskcomment" class="ficon task_active"><img src="../Contents/Images/mytask/task_pin.png" width="14" height="17" alt="" /></span>
                                            
                                            <div class="ui_light floating task_status_change">
                                            	<a class="ui-sproutmenu" href="#nogo">
                                                	<span class="ui-sproutmenu-status">Incomplete</span>
                                                </a>
                                            </div>
                                          </section>
                                      </div>
                                  </section>--%>
                            </div>
                        </section>
                    <section id="inbox_comments" class="threefourth messages msg_view" style="display: none;">
                    <asp:ValidationSummary id="ValidationSummary1"  ShowMessageBox="false" ShowSummary="true"   Runat="server" />
                             <div id="divtaskcomment"  class="messages taskable">
                                     <%--  <section id="Section_1">
                                        <div class="js-task-cont read">
                                            <section class="task-owner">
                                                <img width="32" height="32" border="0" src="../Contents/user_image/Jellyfish_3_th.jpg" class="avatar">
                                           </section>
                                           <section class="task-activity third">
                                                <p>prabhat sinha</p>
                                                <div>3/30/2013 1:20:09 PM</div>
                                                <p>Assigned by shobhit</p>
                                          </section>
                                          <section class="task-message font-13 third">
                                            <a class="tip_left">hi....all</a>
                                         </section>
                                         <section class="task-status">
                                            <span class="ficon task_active">
                                                <img width="14" height="17" alt="" src="../Contents/Images/task/task_pin.png" onclick="getmemberdata(1);" id="taskcomment">
                                           </span>
                                           <div class="ui_light floating task_status_change">
                                                <a href="#nogo" class="ui-sproutmenu"><span class="ui-sproutmenu-status">Incomplete</span></a>
                                           </div>
                                        </section>
                                      </div>
                                  </section>--%>
                           <input type="hidden" id="hdntaskid"/>
                            </div>
                            <div class="task_leave_comment">
	                            <div class="sub_small">Task Activity</div>
                            </div>
                            <div id="prevComments" runat="server" class="task_leave_comment" >
                                <%--<div class="assign_comments">
                                    <section>
                                        <article class="task_assign">                                            
                                            <img src="../Contents/Images/00031502_normal.jpg" width="30" height="30" alt=""  />                                            
                                            <article>
                                            <input id="hdncommentsid" type="hidden" />
                                                <p class="msg_article">ghjghjghjghj</p>
                                                <aside class="days_ago">Assigned to anagha deshpande  by anagha deshpande  | 10 days ago at 10:46 AM </aside>
                                            </article>
                                         </article>
                                    </section>
                                </div>--%>
                            </div>
                            <div class="task_leave_comment">
	                            <div class="sub_small">Leave a Comment</div>
                            </div>
                            <div class="assign_task_to">
                            	<img src="../Contents/Images/blank_img.png"  />
                                <%--<textarea rows="" cols="" name="" placeholder="Your comment (viewable only to team members)"></textarea>--%>
                                <asp:TextBox ID="txtComment" runat="server" TextMode="MultiLine" ></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvtxtComment" runat="server" ControlToValidate="txtComment" ErrorMessage="Please Write Comment"
                                Display="Dynamic" ValidationGroup="task"></asp:RequiredFieldValidator>
                            </div>
                            <div class="task_ws_tm_button_div">
                            <asp:Button ID="btnSave" runat="server" Text="SAVE" ValidationGroup="task" onclick="btnSave_Click" />
                            <asp:Button ID="btnCancel" runat="server" Text="CANCEL" onclick="btnCancel_Click"/>
                            	<%--<input type="submit" name="" value="SAVE" />
                                <input type="submit" name="" value="CANCEL" />--%>
                            </div>
                        </section>
                    <div id="another-load" class="another-loader">
                        <%-- <img src="../Contents/Images/360.gif" />--%>
                    </div>
                    <section class="threefourth messages msg_view">
                        <div id="Div2" runat="server" ></div>
                                   <%-- <div class="loader_div">
                <img src="../Contents/Images/328.gif" width="90" height="90" alt="" />
            </div>--%>
                                    <%--     <div class="messages taskable">
                            <section>
                                <aside>
                                    <section class="js-avatar_tip" data-sstip_class="twt_avatar_tip">
                                        <a class="avatar_link view_profile" href="javascript:void(0)">
                                            <img width="54" height="54" border="0" class="avatar" src="../Contents/images/00031502_normal.jpg">
                                            <article class="message-type-icon"><span class="twitter_bm"> cvcxv</span>
                
                                            </article>
                                        </a>
                                  </section>
                                    <ul></ul>
                                </aside>
                                <article>
                          <div class="">
                                        <a class="language" href=""></a>
                                    </div>
                                    <div class="message_actions">
                                        <a class="gear_small" href="#"><span title="Options" class="ficon">⚙</span>
                
                                        </a>
                                    </div>
                                  <div class="message-text font-14">Утро -13 весна продолжает удивлять!</div>
                                    <section class="bubble-meta">
                                        <article class="threefourth text-overflow">
                                            <section class="floatleft">
                                                <a data-tip="View Yaroslav Lukashev's Profile" class="js-avatar_tip view_profile profile_link" href="#" data-sstip_class="twt_avatar_tip"><span>globus_net</span>
                
                                                </a>&nbsp;
                                                <a data-msg-time="1363926699000" class="time" target="_blank" title="View message on Twitter" href="#">30 minutes ago</a>, in<span class="location" title="Moscow">&nbsp;Moscow</span>
                
                                            </section>
                                        </article>
                                        <ul class="message-buttons quarter clearfix">
                                            <li><a href="#"><img src="../Contents/Images/replay.png" alt="" width="17" height="24" border="none" /></a></li>
                                            <li><a href="#"><img src="../Contents/Images/pin.png" alt="" width="14"  height="17" border="none" /></a></li>
                                            
                                            <li><a href="#"><img src="../Contents/Images/archive.png" alt="" width="21" height="16" border="none" /></a></li>
                                        </ul>
                                    </section>
                                </article>
                            </section>
                        </div>--%>
                    </section>
                </div>
                <div class="ws_msg_right">
                    <div class="quarter">
                        <div class="sub_small">
                            Manage Task
                        </div>
                    </div>
                    <div class="task_user_assign">
                        <ul>
                             <li>
                                <asp:RadioButton ID="rdbtnmytask" runat="server" AutoPostBack="true" GroupName="task"
                                    Text="My task" OnCheckedChanged="rdbtnmytask_CheckedChanged" 
                                    Checked="True" />
                            </li>
                            <li>
                                <asp:RadioButton ID="rdbtnteamtask" runat="server" AutoPostBack="true" GroupName="task"
                                    Text="Team task" OnCheckedChanged="rdbtnteamtask_CheckedChanged" /></li>
                            <li>
                                <asp:CheckBox ID="chkincomplete" runat="server" Text="Incomplete" AutoPostBack="true"
                                    OnCheckedChanged="chkincomplete_CheckedChanged" />
                            </li>
                            <li>
                                <asp:CheckBox ID="CheckBox1" runat="server" Text="Complete" AutoPostBack="true" OnCheckedChanged="CheckBox1_CheckedChanged" /></li>
                        </ul>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <asp:Button ID="btnchangestatus" runat="server" Style="display: none;" OnClick="btn_Click"
        OnClientClick="return confirm('Are you sure want to change status?');" />
    <asp:HiddenField ID="hdnTask_id" runat="server" />
    <asp:HiddenField ID="hdnstatus" runat="server" />
    <asp:HiddenField ID="hdntaskcommentid" runat="server" />
    <%-- <asp:HiddenField  ID="hdnComment_Date" runat="server"  />--%>
    <div id="instaAccounts">
        <span class="close_button b-close"><span id="Span4">X</span></span>
        <div class="usertweets">
            Instagram Accounts</div>
        <div id="accountsins">
            <img src="../Contents/Images/00031502_normal.jpg" />
            <img src="../Contents/Images/00031502_normal.jpg" />
            <img src="../Contents/Images/00031502_normal.jpg" />
            <img src="../Contents/Images/00031502_normal.jpg" />
        </div>
    </div>
</asp:Content>

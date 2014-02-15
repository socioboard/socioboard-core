<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Site.Master" AutoEventWireup="true"
    CodeBehind="~/Message/Task.aspx.cs" Inherits="SocioBoard.Message.Task" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../Contents/css/Style.css" rel="stylesheet" type="text/css" />
    <%-- <script src="../Contents/Scripts/Home.js" type="text/javascript"></script>
       <script src="../Contents/Scripts/Message.js" type="text/javascript"></script>--%>
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
            //debugger;
                       //alert("id :" + id);
            //debugger;
            taskid = id.split('_')[1];
            var status = id.split('_')[2];
            $("#<%=hdnstatus.ClientID %>").val(status);
            $("#<%=hdnTask_id.ClientID %>").val(taskid);
            checkStatusInfo(status, taskid);
        }

        function checkStatusInfo(status,taskid) {
            debugger;
            alertify.confirm('Are you sure want to change status?', function (e) {
                if (e) {
                    debugger;
                    $.ajax
        ({
            type: "POST",
            url: "../Message/AjaxMessage.aspx?op=changeTaskStatus&taskid=" + taskid + "&status=" + status,
            data: '',
            contentType: "application/json; charset=utf-8",
            dataType: "html",
            success: function (msg) {
                debugger;
                location.reload();

            }
        });

                    return true;
                } else {
                    return false;
                }
            });
        }
    </script>
    <style type="text/css">
        #content
        {
            position: inherit;
            margin: 0 0 15px;
            left: 0;
        }
        #contentcontainer1-message
        {
            width: 100%;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div>
        <div id="mainwrapper-message" class="container">
            <div id="sidebar">
                <div class="sidebar-inner">
                    <a class="btn" href="Messages.aspx">
                        <img alt="" src="../Contents/img/admin/chatbtn.png">Smart Inbox
                        <span class="info" runat="server" id="blackcount"></span></a> <a class="btn active" href="Task.aspx">
                            <img alt="" src="../Contents/img/admin/markerbtn.png">My Task</a> <a class="btn"
                                href="Messages.aspx?q=sent">
                                <img alt="" src="../Contents/img/admin/envbtn.png">Sent Message</a>
                                 <a id="archive_message" href="#" onclick="BindArchiveMessages();" class="btn"><img src="../Contents/img/admin/envbtn.png" alt="" >Archive Message</a>
                    <%--<a class="btn" href="#"><img alt="" src="../Contents/img/admin/cambtn.png">Instagram</a>
					    <a class="btn" href="#"><img alt="" src="../Contents/img/admin/g+btn.png">Google</a>
					    <a class="btn" href="#"><img alt="" src="../Contents/img/admin/archivebtn.png">Archive</a>		--%>
                </div>
            </div>
            <div id="contentcontainer2">
                <div id="contentcontainer1-message">
                    <div id="content">
                        <%--class was included before  and class name is : threefourth, removed on 13/07/2013 by praveen --%>
                        <section id="inbox_msgs" class=" messages msg_view">
                                    <%--<div class="tasks-header">
                            	        <div class="task-owner">Owner</div>
                                        <div class="task-activity">Last Activity</div>
                                        <div class="task-message">Task</div>
                                        <div class="task-status">Status</div>
                                    </div>--%>
                                    <div id="taskdiv" class="messages taskable" runat="server" style="margin-top:15px;">
                            	        <section class="section">
                                	        <div class="js-task-cont read">
                                                <section class="task-owner">
                                        	        <img width="32" height="32" border="0" class="avatar" src="../Contents/img/blank_img.png" />
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
                                         	        <span id="taskcomment" class="ficon task_active"><img src="../Contents/img/task/task_pin.png" width="14" height="17" alt="" /></span>
                                            
                                                    <div class="ui_light floating task_status_change">
                                            	        <a class="ui-sproutmenu" href="#nogo">
                                                	        <span class="ui-sproutmenu-status">Incomplete</span>
                                                        </a>
                                                    </div>
                                                  </section>
                                              </div>
                                        </section>
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
                            	<img src="../Contents/img/blank_img.png"  />
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
                                        Text="My task" OnCheckedChanged="rdbtnmytask_CheckedChanged" Checked="True" />
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
                <asp:Button ID="btnchangestatus" runat="server" Style="display: none;" OnClick="btn_Click" />
                <asp:HiddenField ID="hdnTask_id" runat="server" />
                <asp:HiddenField ID="hdnstatus" runat="server" />
                <asp:HiddenField ID="hdntaskcommentid" runat="server" />
                <asp:HiddenField ID="hdnComment_Date" runat="server" />
                <%--<div id="instaAccounts">
        <span class="close_button b-close"><span id="Span4">X</span></span>
        <div class="usertweets">
            Instagram Accounts</div>
        <div id="accountsins">
            <img src="../Contents/Images/00031502_normal.jpg" />
            <img src="../Contents/Images/00031502_normal.jpg" />
            <img src="../Contents/Images/00031502_normal.jpg" />
            <img src="../Contents/Images/00031502_normal.jpg" />
        </div>
    </div>--%>
</asp:Content>

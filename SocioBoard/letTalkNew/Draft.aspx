<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Home.Master" AutoEventWireup="true" CodeBehind="Draft.aspx.cs" Inherits="letTalkNew.Draft" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<style type="text/css">
    .container_right nav #indicator {left: 255px;}
</style>
<script type="text/javascript" language="javascript">
    $(document).ready(function () {
        try {
            publishcontent("drafts");
        }
        catch (e) {
            alert(e);
        }
    });
   
</script>
 <nav>
        <ul>
              <li><a href="Publishing.aspx">Schedule Message </a></li>
              <li><a href="Queue.aspx">Queue</a></li>
              <li><a href="Draft.aspx">Drafts</a></li>
              <li><a href="#"> Post Via RSS</a></li>
        </ul>
        <span id="indicator"></span> 
  </nav>


        	<div id="">
            <div class=" messages msg_view inbox_msgs">
                  <div class="tasks-header">
                <div class="task-owner"></div>
                <div class="task-activity">Send From</div>
                <div class="task-message">Message</div>
                <div class="task-status" style="width:55px;">Edit</div>
                <div class="task-status">Status</div>
                <div class="task-status">Network</div>
              </div>
                  <div id="drafts_messages" class="messages taskable">
                    <div class="section">
                            <div class="js-task-cont read">
                                <div class="task-owner" style="display:block;"> <img class="avatar" width="32" height="32" border="0" src="../Contents/img/blank_img.png"> </div>
                                <div class="task-activity third">
                                      <p>Praveen Kumar</p>
                                      <div></div>
                                      <p></p>
                                </div>
                                <div class="task-message font-13 third" style="margin-right: 6px; width: 40%; height: auto;"> <a class="tip_left">No Schduled Messages</a> </div>
                                <div class="task-status" style="width:113px;"> <span class="ficon task_active"></span>
                                      <div class="ui_light floating task_status_change"> <a class="ui-sproutmenu" href="#nogo"> <span class="ui-sproutmenu-status"></span> </a> </div>
                                </div>
                          </div>
                  </div>
                </div>
                </div>
            </div>
        
</asp:Content>

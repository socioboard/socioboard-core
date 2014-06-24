<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Home.Master" AutoEventWireup="true" CodeBehind="Events.aspx.cs" Inherits="letTalkNew.Event.Events" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<link rel="stylesheet" href="http://code.jquery.com/ui/1.10.3/themes/smoothness/jquery-ui.css">

<script src="../Contents/js/Events.js" type="text/javascript"></script>
<script src="http://code.jquery.com/jquery-1.9.1.js"></script>
<script src="http://code.jquery.com/ui/1.10.3/jquery-ui.js"></script>

<script type="text/javascript">
    $(document).ready(function () {
//        $(".btn").click(function () {
//            alert('asd');
//        })

    });
</script>
<style>
    .event_page {width: 98%;}
   
</style>
    <!--nav-->
    <nav>
       <ul id="eventtabs">
            <li><a href="#" class="current" onclick="getEvntDetails();">Events</a></li>
            <%--<li><a href="#">Updates</a></li>--%>
            <a class="create_event">Create Event</a>
       </ul>
       <span id="indicator"></span> 
   </nav>
   <!--end nav-->

   <!--event_page-->
   <div id="content" class="event_page">
         <section>
            <div class="eve-up">               
                <ul id="addEvntDetails">

                      <%--<li>
                        <img src="../Contents/img/1hb7sy12.jpg">
                        <span>Lorem ipsum dolor sit amet, consectetur adipiscing elit. </span>
                        <p>Lorem ipsum dolor sit amet, consectetur adipiscing elit.</p>
                        <p><span class="date-eve">12-11-2013</span><span class="place-eve">Location</span></p>
                     </li>
                     
                     <li>
                        <img src="../Contents/img/1hb7sy12.jpg">
                        <span>Lorem ipsum dolor sit amet, consectetur adipiscing elit. </span>
                        <p>Lorem ipsum dolor sit amet, consectetur adipiscing elit.</p>
                        <p><span class="date-eve">12-11-2013</span><span class="place-eve">Location</span></p>
                     </li>
                     
                     <li>
                        <img src="../Contents/img/1hb7sy12.jpg">
                        <span>Lorem ipsum dolor sit amet, consectetur adipiscing elit. </span>
                        <p>Lorem ipsum dolor sit amet, consectetur adipiscing elit.</p>
                        <p><span class="date-eve">12-11-2013</span><span class="place-eve">Location</span></p>
                     </li>
                      
                     <li>
                         <img src="../Contents/img/1hb7sy12.jpg">
                        <span>Lorem ipsum dolor sit amet, consectetur adipiscing elit. </span>
                        <p>Lorem ipsum dolor sit amet, consectetur adipiscing elit.</p>
                        <p><span class="date-eve">12-11-2013</span><span class="place-eve">Location </span></p>
                     </li>--%>
                 
                </ul>                
            </div>
         </section>
         
         <%--<section>
           <div class="eve-up">               
                <ul>
                    <li>
                        <img src="../Contents/img/female_img.png">
                        <span>Lorem ipsum dolor sit amet, consectetur adipiscing elit. </span>
                        <p>Lorem ipsum dolor sit amet, consectetur adipiscing elit. Curabitur sit amet ligula ut arcu pellentesque condimentum.  </p>
                    </li>
                    <li>
                        <img src="../Contents/img/male_img.png">
                        <span>Lorem ipsum dolor sit amet, consectetur adipiscing elit. </span>
                        <p>Lorem ipsum dolor sit amet, consectetur adipiscing elit. Curabitur sit amet ligula ut arcu pellentesque condimentum.  </p>
                    </li>
                    <li>
                        <img src="../Contents/img/female_img.png">
                        <span>Lorem ipsum dolor sit amet, consectetur adipiscing elit. </span>
                        <p>Lorem ipsum dolor sit amet, consectetur adipiscing elit. Curabitur sit amet ligula ut arcu pellentesque condimentum.  </p>
                    </li>
                    <li>
                        <img src="../Contents/img/male_img.png">
                        <span>Lorem ipsum dolor sit amet, consectetur adipiscing elit. </span>
                        <p>Lorem ipsum dolor sit amet, consectetur adipiscing elit. Curabitur sit amet ligula ut arcu pellentesque condimentum. </p>
                    </li>
                </ul>
           </div>
       </section>  --%>           
   </div>
   <!--end event_page-->

   <!--popup for event-->
   <div class="open_create_event" style="display:none;">
        <span class="button b-close"><span>X</span></span>
        <div class="event_popup">
                <div class="title">Create Event</div>
                <div class="row">
                    <div class="txt">Name</div>
                    <div class="txtbx"><asp:TextBox ID="txtName" placeholder="birthday" runat="server"></asp:TextBox></div>
                </div>

                <div class="row">
                    <div class="txt">Details</div>
                    <div class="txtbx"><asp:TextBox ID="txtDetails" placeholder="Add more info" runat="server"></asp:TextBox></div>
                </div>

                <div class="row">
                    <div class="txt">Where</div>
                    <div class="txtbx"><asp:TextBox ID="txtWhere" placeholder="Add a place?" runat="server"></asp:TextBox></div>
                </div>

                <div class="row">
                    <div class="txt">When</div>
                    <div class="txtbx">
                        <div class="date_box"><asp:TextBox ID="txtDate" placeholder="YYYY-MM-DD" runat="server"></asp:TextBox></div>
                        <div class="time_box"><asp:TextBox ID="TextBox4" placeholder="Add a Time?" runat="server"></asp:TextBox></div>
                    </div>
                </div>

                <div class="row">
                    <div class="txt">Privacy</div>
                    <div class="txtbx">
                        <select name="">
                            <option>Invite Only</option>
                            <option>Friends of Guests</option>
                            <option>Public</option>
                        </select>
                    </div>
                </div>

                <div class="row">
                    <a class="btn">Cancel</a>                    
                    <a class="btn" onclick="createEvnt();">Create Event</a>
                </div>
        </div>
   </div>
   <!--end popup for event-->
</asp:Content>

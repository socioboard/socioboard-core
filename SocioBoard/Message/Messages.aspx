<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Site.Master" AutoEventWireup="true" CodeBehind="Messages.aspx.cs" Inherits="SocioBoard.Message.Messages" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
   <style type="text/css">
   

.demo-4 .loaderwrapper {
	font-size: 25px; /* 1em */

    width: 8em;
    height: 8em;
    position: relative;
    margin: 100px auto;

    border-radius: 50%;
    background: rgba(255,255,255,0.1);
    border: 1em dashed rgba(62, 91, 133,0.5);
    box-shadow: 
    	inset 0 0 2em rgba(255,255,255,0.3),
    	0 0 0 0.7em rgba(255,255,255,0.3);

    line-height: 6em;
    text-align: center;
    font-family: 'Racing Sans One', "HelveticaNeue-Light","Helvetica Neue Light","Helvetica Neue","Helvetica","Arial","Lucida Grande",sans-serif;
    color: #444;
    text-shadow: 0 .04em rgba(255,255,255,0.9);

    -webkit-animation: steam 3.5s linear infinite;
    -moz-animation: steam 3.5s linear infinite;
    -ms-animation: steam 3.5s linear infinite;
    -o-animation: steam 3.5s linear infinite;
    animation: steam 3.5s linear infinite;
}

.demo-4 .loaderwrapper:after, 
.demo-4 .loaderwrapper:before {
    content: "";
    position: absolute;
    left: 0; right: 0; top: 0; bottom: 0;
    z-index: -1;
    border-radius: inherit;
    box-shadow: inset 0 0 2em rgba(255,255,255,0.3);
    border: 1em dashed rgba(62, 91, 133,0.2);
}

.demo-4 .loaderwrapper:before {
    top: 1em; bottom: 1em; right: 1em; left: 1em; 
    border: 1em dashed rgba(62, 91, 133,0.4);
}

.demo-4 .inner {
    width: 100%;
    height: 100%;
    -webkit-animation: steam 3.5s linear reverse infinite;
    -moz-animation: steam 3.5s linear reverse infinite;
    -ms-animation: steam 3.5s linear reverse infinite;
    -o-animation: steam 3.5s linear reverse infinite;
    animation: steam 3.5s linear reverse infinite;
}

.demo-4 .inner span {
    display: inline-block;

    -webkit-animation: loading-1 1.5s ease-out infinite;
    -moz-animation: loading-1 1.5s ease-out infinite;
    -ms-animation: loading-1 1.5s ease-out infinite;
    -o-animation: loading-1 1.5s ease-out infinite;
    animation: loading-1 1.5s ease-out infinite;
}

.demo-4 .inner span:nth-child(1)  { 
	-webkit-animation-name: loading-1;
	-moz-animation-name: loading-1;
	-ms-animation-name: loading-1;
	-o-animation-name: loading-1;
	animation-name: loading-1;
}

.demo-4 .inner span:nth-child(2)  { 
	-webkit-animation-name: loading-2;
	-moz-animation-name: loading-2;
	-ms-animation-name: loading-2;
	-o-animation-name: loading-2;
	animation-name: loading-2;
}

.demo-4 .inner span:nth-child(3)  { 
	-webkit-animation-name: loading-3;
	-moz-animation-name: loading-3;
	-ms-animation-name: loading-3;
	-o-animation-name: loading-3;
	animation-name: loading-3;
}

.demo-4 .inner span:nth-child(4)  { 
	-webkit-animation-name: loading-4;
	-moz-animation-name: loading-4;
	-ms-animation-name: loading-4;
	-o-animation-name: loading-4;
	animation-name: loading-4;
}

.demo-4 .inner span:nth-child(5)  { 
	-webkit-animation-name: loading-5;
	-moz-animation-name: loading-5;
	-ms-animation-name: loading-5;
	-o-animation-name: loading-5;
	animation-name: loading-5;
}

.demo-4 .inner span:nth-child(6)  { 
	-webkit-animation-name: loading-6;
	-moz-animation-name: loading-6;
	-ms-animation-name: loading-6;
	-o-animation-name: loading-6;
	animation-name: loading-6;
}

.demo-4 .inner span:nth-child(7)  { 
	-webkit-animation-name: loading-7;
	-moz-animation-name: loading-7;
	-ms-animation-name: loading-7;
	-o-animation-name: loading-7;
	animation-name: loading-7;
}

@-webkit-keyframes steam {
    to { -webkit-transform: rotate(360deg); }
}

@-moz-keyframes steam {
    to { -moz-transform: rotate(360deg); }
}

@-ms-keyframes steam {
    to { -ms-transform: rotate(360deg); }
}

@-o-keyframes steam {
    to { -o-transform: rotate(360deg); }
}

@keyframes steam {
    to { transform: rotate(360deg); }
}

@-webkit-keyframes loading-1 {
    14.28% { opacity: 0.3; }
}

@-webkit-keyframes loading-2 {
    28.57% { opacity: 0.3; }
}

@-webkit-keyframes loading-3 {
    42.86% { opacity: 0.3; }
}

@-webkit-keyframes loading-4 {
    57.14% { opacity: 0.3; }
}

@-webkit-keyframes loading-5 {
    71.43% { opacity: 0.3; }
}

@-webkit-keyframes loading-6 {
    85.71% { opacity: 0.3; }
}

@-webkit-keyframes loading-7 {
    100% { opacity: 0.3; }
}

@-moz-keyframes loading-1 {
    14.28% { opacity: 0.3; }
}

@-moz-keyframes loading-2 {
    28.57% { opacity: 0.3; }
}

@-moz-keyframes loading-3 {
    42.86% { opacity: 0.3; }
}

@-moz-keyframes loading-4 {
    57.14% { opacity: 0.3; }
}

@-moz-keyframes loading-5 {
    71.43% { opacity: 0.3; }
}

@-moz-keyframes loading-6 {
    85.71% { opacity: 0.3; }
}

@-moz-keyframes loading-7 {
    100% { opacity: 0.3; }
}

@-ms-keyframes loading-1 {
    14.28% { opacity: 0.3; }
}

@-ms-keyframes loading-2 {
    28.57% { opacity: 0.3; }
}

@-ms-keyframes loading-3 {
    42.86% { opacity: 0.3; }
}

@-ms-keyframes loading-4 {
    57.14% { opacity: 0.3; }
}

@-ms-keyframes loading-5 {
    71.43% { opacity: 0.3; }
}

@-ms-keyframes loading-6 {
    85.71% { opacity: 0.3; }
}

@-ms-keyframes loading-7 {
    100% { opacity: 0.3; }
}

@-o-keyframes loading-1 {
    14.28% { opacity: 0.3; }
}

@-o-keyframes loading-2 {
    28.57% { opacity: 0.3; }
}

@-o-keyframes loading-3 {
    42.86% { opacity: 0.3; }
}

@-o-keyframes loading-4 {
    57.14% { opacity: 0.3; }
}

@-o-keyframes loading-5 {
    71.43% { opacity: 0.3; }
}

@-o-keyframes loading-6 {
    85.71% { opacity: 0.3; }
}

@-o-keyframes loading-7 {
    100% { opacity: 0.3; }
}

@keyframes loading-1 {
    14.28% { opacity: 0.3; }
}

@keyframes loading-2 {
    28.57% { opacity: 0.3; }
}

@keyframes loading-3 {
    42.86% { opacity: 0.3; }
}

@keyframes loading-4 {
    57.14% { opacity: 0.3; }
}

@keyframes loading-5 {
    71.43% { opacity: 0.3; }
}

@keyframes loading-6 {
    85.71% { opacity: 0.3; }
}

@keyframes loading-7 {
    100% { opacity: 0.3; }
}

   
   
   </style>
    <script src="../Contents/js/modernizr.custom.63321.js" type="text/javascript"></script>
<link href='http://fonts.googleapis.com/css?family=Racing+Sans+One' rel='stylesheet' type='text/css'>
        <div id="mainwrapper-message" class="container">
			<div id="sidebar">
				<div class="sidebar-inner">
                	<a id="smart_inbox" href="Messages.aspx" class="btn active"><img src="../Contents/img/admin/chatbtn.png" alt="" >Smart Inbox <span runat="server" id="blackcount" class="info"></span></a>
                	<a id="task_href" href="Task.aspx" class="btn"><img src="../Contents/img/admin/markerbtn.png" alt="" >My Task</a>
                    <a id="sent_messages" href="#" onclick="BindInboxMessageonMessageTab();" class="btn"><img src="../Contents/img/admin/envbtn.png" alt="" >Sent Message</a>
                    <a id="archive_message" href="#" onclick="BindArchiveMessages();" class="btn"><img src="../Contents/img/admin/envbtn.png" alt="" >Archive Message</a>
					<%--<a href="#" class="btn"><img src="../Contents/img/admin/cambtn.png" alt="" >Instagram</a>
					<a href="#" class="btn"><img src="../Contents/img/admin/g+btn.png" alt="" >Google</a>
					<a href="#" class="btn"><img src="../Contents/img/admin/archivebtn.png" alt="" >Archive</a>		--%>		</div>				
		  	</div>
            <div id="contentcontainer2">
            	<div id="contentcontainer1-message">
                	<div id="inbox_msgs">
                    <%--	<ul id="message-list">
                        	<li>
                                <div class="userpictiny">
                                    <img src="../Contents/img/admin/user.png" height="48" width="48" alt="" title="PRAB KUMAR" />
                                    <a href="#" class="userurlpic" title=""><img src="../Contents/img/admin/searchmini.png" alt=""></a>
                                </div>
                                <div class="message-list-content">
                                <div class="leftarrow">&nbsp;</div>
                                <p>You know when you're so angry at people you hope they are immediately transported to the inner depths of Hell to burn for eternity?</p>
                                <div class="message-list-info">
	                                <a href="#">leesteabler</a> 6 minutes ago, in sunderland
                                    <div class="scl">
                                    	<a href="#"><img src="../Contents/img/admin/print.png" alt=""/></a>
                                    	<a href="#"><img src="../Contents/img/admin/todo.png" alt=""/></a>
                                    	<a href="#"><img src="../Contents/img/admin/goto.png" alt=""/></a>
                                    </div>
                                </div>
                                </div>
							</li>
                        	<li>
                                <div class="userpictiny">
                                    <img src="../Contents/img/admin/user3.png" height="48" width="48" alt="" title="PRAB KUMAR" />
                                    <a href="#" class="userurlpic" title=""><img src="../Contents/img/admin/searchmini.png" alt=""></a>
                                </div>
                                <div class="message-list-content">
                                <div class="leftarrow">&nbsp;</div>
                                <p>@gretaasmith well if you think yours are big than what the hell do you consider mine</p>
                                <div class="message-list-info">
	                                <a href="#">leesteabler</a> 6 minutes ago, in sunderland
                                    <div class="scl">
                                    	<a href="#"><img src="../Contents/img/admin/print.png" alt=""/></a>
                                    	<a href="#"><img src="../Contents/img/admin/todo.png" alt=""/></a>
                                    	<a href="#"><img src="../Contents/img/admin/goto.png" alt=""/></a>
                                    </div>
                                </div>
                                </div>
							</li>
                        	<li>
                                <div class="userpictiny">
                                    <img src="../Contents/img/admin/user3.png" height="48" width="48" alt="" title="PRAB KUMAR" />
                                    <a href="#" class="userurlpic" title=""><img src="../Contents/img/admin/searchmini.png" alt=""></a>
                                </div>
                                <div class="message-list-content">
                                <div class="leftarrow">&nbsp;</div>
                                <p>#Smoothie hell...Wish I'd spent more $$ on a new blender!</p>
                                <div class="message-list-info">
	                                <a href="#">leesteabler</a> 6 minutes ago, in sunderland
                                    <div class="scl">
                                    	<a href="#"><img src="../Contents/img/admin/print.png" alt=""/></a>
                                    	<a href="#"><img src="../Contents/img/admin/todo.png" alt=""/></a>
                                    	<a href="#"><img src="../Contents/img/admin/goto.png" alt=""/></a>
                                    </div>
                                </div>
                                </div>
							</li>
                        	<li>
                                <div class="userpictiny">
                                    <img src="../Contents/img/admin/user.png" height="48" width="48" alt="" title="PRAB KUMAR" />
                                    <a href="#" class="userurlpic" title=""><img src="../Contents/img/admin/searchmini.png" alt=""></a>
                                </div>
                                <div class="message-list-content">
                                <div class="leftarrow">&nbsp;</div>
                                <p>Venezuela threatens oil, trade in election war w/U.S...Soros financier from Hell </p>
                                <div class="message-list-info">
	                                <a href="#">leesteabler</a> 6 minutes ago, in sunderland
                                    <div class="scl">
                                    	<a href="#"><img src="../Contents/img/admin/print.png" alt=""/></a>
                                    	<a href="#"><img src="../Contents/img/admin/todo.png" alt=""/></a>
                                    	<a href="#"><img src="../Contents/img/admin/goto.png" alt=""/></a>
                                    </div>
                                </div>
                                </div>
							</li>
                        	<li>
                                <div class="userpictiny">
                                    <img src="../Contents/img/admin/user.png" height="48" width="48" alt="" title="PRAB KUMAR" />
                                    <a href="#" class="userurlpic" title=""><img src="../Contents/img/admin/searchmini.png" alt=""></a>
                                </div>
                                <div class="message-list-content">
                                <div class="leftarrow">&nbsp;</div>
                                <p>You know when you're so angry at people you hope they are immediately transported to the inner depths of Hell to burn for eternity?</p>
                                <div class="message-list-info">
	                                <a href="#">leesteabler</a> 6 minutes ago, in sunderland
                                    <div class="scl">
                                    	<a href="#"><img src="img/admin/print.png" alt=""/></a>
                                    	<a href="#"><img src="img/admin/todo.png" alt=""/></a>
                                    	<a href="#"><img src="img/admin/goto.png" alt=""/></a>
                                    </div>
                                </div>
                                </div>
							</li>
                        	<li>
                                <div class="userpictiny">
                                    <img src="../Contents/img/admin/user3.png" height="48" width="48" alt="" title="PRAB KUMAR" />
                                    <a href="#" class="userurlpic" title=""><img src="../Contents/img/admin/searchmini.png" alt=""></a>
                                </div>
                                <div class="message-list-content">
                                <div class="leftarrow">&nbsp;</div>
                                <p>@gretaasmith well if you think yours are big than what the hell do you consider mine</p>
                                <div class="message-list-info">
	                                <a href="#">leesteabler</a> 6 minutes ago, in sunderland
                                    <div class="scl">
                                    	<a href="#"><img src="../Contents/img/admin/print.png" alt=""/></a>
                                    	<a href="#"><img src="../Contents/img/admin/todo.png" alt=""/></a>
                                    	<a href="#"><img src="../Contents/img/admin/goto.png" alt=""/></a>
                                    </div>
                                </div>
                                </div>
							</li>
                        	<li>
                                <div class="userpictiny">
                                    <img src="../Contents/img/admin/user3.png" height="48" width="48" alt="" title="PRAB KUMAR" />
                                    <a href="#" class="userurlpic" title=""><img src="../Contents/img/admin/searchmini.png" alt=""></a>
                                </div>
                                <div class="message-list-content">
                                <div class="leftarrow">&nbsp;</div>
                                <p>#Smoothie hell...Wish I'd spent more $$ on a new blender!</p>
                                <div class="message-list-info">
	                                <a href="#">leesteabler</a> 6 minutes ago, in sunderland
                                    <div class="scl">
                                    	<a href="#"><img src="../Contents/img/admin/print.png" alt=""/></a>
                                    	<a href="#"><img src="../Contents/img/admin/todo.png" alt=""/></a>
                                    	<a href="#"><img src="../Contents/img/admin/goto.png" alt=""/></a>
                                    </div>
                                </div>
                                </div>
							</li>
                        	<li>
                                <div class="userpictiny">
                                    <img src="../Contents/img/admin/user.png" height="48" width="48" alt="" title="PRAB KUMAR" />
                                    <a href="#" class="userurlpic" title=""><img src="../Contents/img/admin/searchmini.png" alt=""></a>
                                </div>
                                <div class="message-list-content">
                                <div class="leftarrow">&nbsp;</div>
                                <p>Venezuela threatens oil, trade in election war w/U.S...Soros financier from Hell </p>
                                <div class="message-list-info">
	                                <a href="#">leesteabler</a> 6 minutes ago, in sunderland
                                    <div class="scl">
                                    	<a href="#"><img src="../Contents/img/admin/print.png" alt=""/></a>
                                    	<a href="#"><img src="../Contents/img/admin/todo.png" alt=""/></a>
                                    	<a href="#"><img src="../Contents/img/admin/goto.png" alt=""/></a>
                                    </div>
                                </div>
                                </div>
							</li>                        	
                        	<li>
                                <div class="userpictiny">
                                    <img src="../Contents/img/admin/user.png" height="48" width="48" alt="" title="PRAB KUMAR" />
                                    <a href="#" class="userurlpic" title=""><img src="../Contents/img/admin/searchmini.png" alt=""></a>
                                </div>
                                <div class="message-list-content">
                                <div class="leftarrow">&nbsp;</div>
                                <p>You know when you're so angry at people you hope they are immediately transported to the inner depths of Hell to burn for eternity?</p>
                                <div class="message-list-info">
	                                <a href="#">leesteabler</a> 6 minutes ago, in sunderland
                                    <div class="scl">
                                    	<a href="#"><img src="../Contents/img/admin/print.png" alt=""/></a>
                                    	<a href="#"><img src="../Contents/img/admin/todo.png" alt=""/></a>
                                    	<a href="#"><img src="../Contents/img/admin/goto.png" alt=""/></a>
                                    </div>
                                </div>
                                </div>
							</li>
                        	<li>
                                <div class="userpictiny">
                                    <img src="../Contents/img/admin/user3.png" height="48" width="48" alt="" title="PRAB KUMAR" />
                                    <a href="#" class="userurlpic" title=""><img src="../Contents/img/admin/searchmini.png" alt=""></a>
                                </div>
                                <div class="message-list-content">
                                <div class="leftarrow">&nbsp;</div>
                                <p>@gretaasmith well if you think yours are big than what the hell do you consider mine</p>
                                <div class="message-list-info">
	                                <a href="#">leesteabler</a> 6 minutes ago, in sunderland
                                    <div class="scl">
                                    	<a href="#"><img src="../Contents/img/admin/print.png" alt=""/></a>
                                    	<a href="#"><img src="../Contents/img/admin/todo.png" alt=""/></a>
                                    	<a href="#"><img src="../Contents/img/admin/goto.png" alt=""/></a>
                                    </div>
                                </div>
                                </div>
							</li>
                        	<li>
                                <div class="userpictiny">
                                    <img src="../Contents/img/admin/user3.png" height="48" width="48" alt="" title="PRAB KUMAR" />
                                    <a href="#" class="userurlpic" title=""><img src="../Contents/img/admin/searchmini.png" alt=""></a>
                                </div>
                                <div class="message-list-content">
                                <div class="leftarrow">&nbsp;</div>
                                <p>#Smoothie hell...Wish I'd spent more $$ on a new blender!</p>
                                <div class="message-list-info">
	                                <a href="#">leesteabler</a> 6 minutes ago, in sunderland
                                    <div class="scl">
                                    	<a href="#"><img src="../Contents/img/admin/print.png" alt=""/></a>
                                    	<a href="#"><img src="../Contents/img/admin/todo.png" alt=""/></a>
                                    	<a href="#"><img src="../Contents/img/admin/goto.png" alt=""/></a>
                                    </div>
                                </div>
                                </div>
							</li>
                        	<li>
                                <div class="userpictiny">
                                    <img src="../Contents/img/admin/user.png" height="48" width="48" alt="" title="PRAB KUMAR" />
                                    <a href="#" class="userurlpic" title=""><img src="../Contents/img/admin/searchmini.png" alt=""></a>
                                </div>
                                <div class="message-list-content">
                                <div class="leftarrow">&nbsp;</div>
                                <p>Venezuela threatens oil, trade in election war w/U.S...Soros financier from Hell </p>
                                <div class="message-list-info">
	                                <a href="#">leesteabler</a> 6 minutes ago, in sunderland
                                    <div class="scl">
                                    	<a href="#"><img src="../Contents/img/admin/print.png" alt=""/></a>
                                    	<a href="#"><img src="../Contents/img/admin/todo.png" alt=""/></a>
                                    	<a href="#"><img src="../Contents/img/admin/goto.png" alt=""/></a>
                                    </div>
                                </div>
                                </div>
							</li>
                        </ul>--%>                 
                              
					</div>
                        <div class="home_loader">
               <%-- <img id="home_loader" src="../Contents/img/328.gif" width="50" height="50" style="margin-left:0;"  alt=""/>--%>
                       <section class="main" >
				<!-- the component -->
                <div class="demo-4">
				<div class="loaderwrapper" style="width:120px;height:120px;>
					<div class="inner">
						<span>L</span>
						<span>o</span>
						<span>a</span>
						<span>d</span>
						<span>i</span>
						<span>n</span>
						<span>g</span>
					</div>
				</div>
                </div>
			</section>
                     </div>
                
                    
				</div>

                <div class="right-sidebar1">
			<div id="accordion" class="accordion">
                <div class="accordion-group">
                  <div class="accordion-heading">
                    <a href="#collapseOne" id="profile" data-parent="#accordion" data-toggle="collapse" class="accordion-toggle">
                      <h2>Profile</h2>
                    </a>
                  </div>
                  <div class="accordion-body collapse in" id="collapseOne">
                    <div id="accordianprofiles" class="accordion-inner">
                      <ul>
                      <li>
                      <img src="../Contents/img/ajax-loader.gif" alt="" />
                      </li>

                      <%--	<li>
                        	<a href="#"><img src="../Contents/img/admin/twittericon.png" width="15" height="15" alt=""> WooSuite</a>
                        </li>
                      	<li>
                        	<a href="#"><img src="../Contents/img/admin/twittericon.png" width="15" height="15" alt=""> yashwant05</a>
                        </li>
                      	<li>
                        	<a href="#"><img src="../Contents/img/admin/twittericon.png" width="15" height="15" alt=""> shobhit_sss</a>
                        </li>
                      	<li>
                        	<a href="#"><img src="../Contents/img/admin/twittericon.png" width="15" height="15" alt=""> Test_globuss</a>
                        </li>--%>
                      </ul>
                    </div>
                  </div>
                </div>
                 <div class="accordion-group">
                  <div class="accordion-heading">
                    <a href="#collapseTwo" id="messagetype" data-parent="#accordion" data-toggle="collapse" class="accordion-toggle collapsed">
                      <h2>Message type</h2>
                    </a>
                  </div>
                  <div class="accordion-body collapse" id="collapseTwo">
                    <div class="accordion-inner">
                        <ul>                        
                      	<li class="messagetype">
                        	<a href="#"><img src="../Contents/img/admin/twittericon.png" width="15" height="15" alt=""/>Mentions</a>
                            <img onclick="chkMessage(this.id);" id="check_mentions" src="../Contents/img/check_click.png" alt="" />
                        </li>
                      	<li class="messagetype">
                        	<a href="#"><img src="../Contents/img/admin/twittericon.png" width="15" height="15" alt=""/>User Tweets</a>
                      <img onclick="chkMessage(this.id);" id="check_Retweets" src="../Contents/img/check_click.png" alt="" />
                        </li>
                      	<%--<li class="messagetype">
                        	<a href="#"><img src="../Contents/img/google_plus.png" width="15" height="15" alt="" />Activities</a>
                       <img onclick="chkMessage(this.id);" id="check_Activities" src="../Contents/img/check_click.png" alt="" />
                        </li>--%>
                      	<li class="messagetype">
                        	<a href="#"><img src="../Contents/img/admin/fbicon.png" width="15" height="15" alt=""/>User Feeds</a>
                         <img onclick="chkMessage(this.id);" id="check_WallPosts" src="../Contents/img/check_click.png" alt="" />
                        </li>
                      </ul>
                    </div>
                  </div>
                </div>
             <%--   <div class="accordion-group">
                  <div class="accordion-heading">
                    <a href="#collapseThree" data-parent="#accordion" data-toggle="collapse" class="accordion-toggle collapsed">
                      <h2>Brand Keywords</h2>
                    </a>
                  </div>
                  <div class="accordion-body collapse" id="collapseThree">
                    <div class="accordion-inner">
                      Anim pariatur cliche reprehenderit
                    </div>
                  </div>
                </div>--%>
              </div>                   
					</div>

			</div>
		</div>
        <script type="text/javascript" language="javascript">
            $(document).ready(function () {
                debugger;

                $("#home").removeClass('active');
                $("#message").addClass('active');
                $("#feeds").removeClass('active');
                $("#discovery").removeClass('active');
                $("#publishing").removeClass('active');

                $("#aad").data();

                if (document.URL.indexOf('q=sent') == -1) {
                    BindSmartInbox();

                } else {
                    BindSent();
                }


                function BindSmartInbox() {
                    debugger;
                    BindMessages();
                    BindProfilesInMessageTab();

                }

                function BindSent() {
                    BindInboxMessageonMessageTab();
                    BindProfilesInMessageTab();

                }


                function Updatemessages() {
                    debugger;



                }

                $("#messagetype").click(function () {
                    $('#profile').addClass('collapsed');
                });

                $("#profile").click(function () {
                    $('#messagetype').addClass('collapsed');
                });
            });
        </script>
</asp:Content>

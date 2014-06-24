<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/socialsuite.Master" AutoEventWireup="true" CodeBehind="Feature.aspx.cs" Inherits="SocialSuitePro.Feature" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<script src="Contents/js/jquery-1.6.3.min.js" type="text/javascript"></script>
<script src="Contents/js/easyResponsiveTabs.js" type="text/javascript"></script>
<script type="text/javascript" src="../Contents/js/jquery.lightbox-0.5.js"></script>
 <!-- Ativando o jQuery lightBox plugin -->
<%--    <script type="text/javascript">
        $(function () {
            $('.features-page-preview').lightBox();
        });
    </script>--%>
<!--feature_body-->
            <%--<div class="arrow" style="top: 100px; bottom:0;">
                <ul class="mainnav">
                    <li><a href="#p5"></a></li>
                </ul>
            </div>--%>


        <div class="feature_body" id="p6">
            
            <div class="row-content">
                <div class="welcome-built" id="for-everyone">
                    <div class="features-page">
                        <h2>Features</h2>
                        <div class="features-page-header">
                        	Socioboard is an advanced easy open source social media marketing tool to engage your visitors with great features such as: 
                            <span class="features-list">
                            live chat, news feed, timeline, profiles, events, notifications, likes, and so much more...</span> 
                            <%--<a href="#list">See full list »</a>--%>
                        </div>
                        <div id="verticalTab">
                        <ul class="resp-tabs-list">
                            <li>Home</li>
                            <li>Messages</li>
                            <li>Feeds</li>
                            <li>Publishing</li>
                
                            <li>Discovery</li>
                            <li>Reports</li>
                            <li>Engagement</li>
                            <li>Monitoring</li>
                
                            <li>Collaboration</li>
                            <li>CRM</li>
                            <li>HelpDesk</li>
                            <li>Mobile</li>
                        </ul>
                        
                        
                     
                        
                        <div class="resp-tabs-container">
                        <div><div class="six columns">
                            <div class="features-page-preview" href="../Contents/img/ssp/home_page.png">
                                <img src="../Contents/img/ssp/home_page.png" onclick="" alt="" />
                            </div>
                        </div>
                        
                        <div class="six columns">
                            <div class="features-page-desc">
                                <h3>Home</h3><br />
                                Shows your friend activity such as new messages, events, visited locations and so much more. The entire content is filterable 
                                by date or by events.<br /><br />You can comment the activity of your friends, like posts, comment and even share them.<br /><br />
                                Also you have quick access to online friends, your friends list and even more...
                            </div>
                        </div>
                        </div>
                      
                        <div>
                        <div class="six columns">
                            <div class="features-page-desc">
                                <h3>Messages</h3>
                                <br />
                                The Message page features cover and Message picture, all the public posts made by a user, places visited, filterable content by 
                                events and date, friends, and other informations like gender, born date, city and more...<br /><br /> Users and friends can 
                                comment on the Message posts, report, like, share, and even send you messages.
                            </div>
                        </div>
                        <div class="six columns">
                            <div class="features-page-preview" href="../Contents/img/ssp/message_page.png">
                                <img src="../Contents/img/ssp/message_page.png" alt="" />
                            </div>
                        </div>
                        </div>
                       
                        <div>
                        <div class="six columns">
                            <div class="features-page-preview" href="../Contents/img/ssp/feeds_page.png">
                                <img src="../Contents/img/ssp/feeds_page.png" alt="" />
                            </div>
                        </div>
                        <div class="six columns">
                            <div class="features-page-desc">
                                <h3>Feeds</h3><br />
                                Keep in touch with your family and friends with the live chat function, share links and emoticons.<br /><br />Engage with any 
                                other users from the platform, get notifications on missed messages and even more.<br><br>Users can be blocked from live 
                                chatting and also the messages can be deleted.
                            </div>
                        </div>
                       </div> 
                        
                        <div>
                        <div class="six columns">
                            <div class="features-page-desc">
                                <h3>Publishing</h3>
                                <br />
                                Receive live visual and email Publishing about your friends activity, see what they like, where they comment, what they are 
                                posting and even more...<br /><br/>
                                Never miss any news about what they are doing with the dedicated page for notifications with filters for their activities.
                            </div>
                        </div>
                        <div class="six columns">
                            <div class="features-page-preview" href="../Contents/img/ssp/publishing_page.png">
                                <img src="../Contents/img/ssp/publishing_page.png" alt="" />
                            </div>
                        </div>
                        </div>
                        <div>                       
                        <div class="six columns">
                            <div class="features-page-preview" href="../Contents/img/ssp/disocvery_page.png">
                                <img src="../Contents/img/ssp/disocvery_page.png" alt="" />
                            </div>
                        </div>
                        <div class="six columns">
                            <div class="features-page-desc">
                                <h3>Discovery</h3><br />
                                Keep in touch with your family and friends with the live chat function, share links and emoticons.<br /><br />Engage with any 
                                other users from the platform, get notifications on missed messages and even more.<br><br>Users can be blocked from live 
                                chatting and also the messages can be deleted.
                            </div>
                        </div>
                        </div>
                        
                        <div>
                        <div class="six columns">
                            <div class="features-page-desc">
                                <h3>Reports</h3>
                                <br />
                                Receive live visual and email notifications about your friends activity, see what they like, where they comment, what they are 
                                posting and even more...<br /><br/>
                                Never miss any news about what they are doing with the dedicated page for notifications with filters for their activities.
                            </div>
                        </div>
                        <div class="six columns">
                            <div class="features-page-preview" href="../Contents/img/ssp/reports.png">
                                <img src="../Contents/img/ssp/reports.png" alt="" />
                            </div>
                        </div>
                        </div> 

                        <div>
                        <div class="six columns">
                            <div class="features-page-desc">
                                <h3>Engagement</h3>
                                <br />
                               Socioboard has capabilities to engage audiences across various platforms, it gives every platform its due importance. The degree of engagement is customized as per the platform’s importance and number of connections in it.
                            </div>
                        </div>
                        <%--<div class="six columns">
                            <div class="features-page-preview" href="../Contents/img/ssp/reports.png">
                                <img src="../Contents/img/ssp/reports.png" alt="" />
                            </div>
                        </div>--%>
                        </div> 

                        <div>
                        <div class="six columns">
                            <div class="features-page-desc">
                                <h3>Monitoring</h3>
                                <br />
                               Socioboard monitors data continuously across various platforms, and gives regular feeds to the end users. Feeds allow you to be in touch with family and friends with the live chat function, share links, emoticons… Users can engage each other using the platform, get notifications on missed messages and even more. You can stop someone from bothering you on this platform, we’d take utmost care of your privacy. 
                            </div>
                        </div>
                        <%--<div class="six columns">
                            <div class="features-page-preview" href="../Contents/img/ssp/reports.png">
                                <img src="../Contents/img/ssp/reports.png" alt="" />
                            </div>
                        </div>--%>
                        </div> 

                        <div>
                        <div class="six columns">
                            <div class="features-page-desc">
                                <h3>Collaboration</h3>
                                <br />
                               Collaboration is a very important term in social media marketing, people can collaborate with each other to make things a mutually beneficial win-win, socioboard comes up with a marvelous functionality by virtue of which users can reach out to each other using a highly efficient collaborative mechanism. It can serve as a platform to share information with other team members and increase workability and efficiency of all teams.
                            </div>
                        </div>
                        <%--<div class="six columns">
                            <div class="features-page-preview" href="../Contents/img/ssp/reports.png">
                                <img src="../Contents/img/ssp/reports.png" alt="" />
                            </div>
                        </div>--%>
                        </div>
                        
                        <div>
                        <div class="six columns">
                            <div class="features-page-desc">
                                <h3>CRM</h3>
                                <br />
                               Customer Relationship Management is an indispensable exercise for any business which is into practice of offering genuine value to its customers, it is an integral part of sales as well as after sales service. It not only helps to retain customers but also gets new leads mainly by virtue of word of mouth. Social Media platforms are increasingly getting popular among the masses and socioboard taps on to this phenomena and offers an excellent base to launch your CRM initiatives.
                            </div>
                        </div>
                        <%--<div class="six columns">
                            <div class="features-page-preview" href="../Contents/img/ssp/reports.png">
                                <img src="../Contents/img/ssp/reports.png" alt="" />
                            </div>
                        </div>--%>
                        </div> 

                        <div>
                        <div class="six columns">
                            <div class="features-page-desc">
                                <h3>HelpDesk</h3>
                                <br />
                               Socioboard has a world class support mechanism, help will always be given at Socioboard to anyone who asks for it. We are always ready with technical or non-technical support. 
                            </div>
                        </div>
                        <%--<div class="six columns">
                            <div class="features-page-preview" href="../Contents/img/ssp/reports.png">
                                <img src="../Contents/img/ssp/reports.png" alt="" />
                            </div>
                        </div>--%>
                        </div> 

                        <div>
                        <div class="six columns">
                            <div class="features-page-desc">
                                <h3>Mobile</h3>
                                <br />
                               Socioboard is available on web, desktop and mobiles. We have Android and iOS apps which work in tandem with the web app.
                            </div>
                        </div>
                        <%--<div class="six columns">
                            <div class="features-page-preview" href="../Contents/img/ssp/reports.png">
                                <img src="../Contents/img/ssp/reports.png" alt="" />
                            </div>
                        </div>--%>
                        </div> 
                       
                        </div>
                       </div> 
                    </div>
                </div>
            
                
        	</div>
            
           
		</div>
        <!--end feature_body-->
         <script type="text/javascript">
             $(document).ready(function () {
                 $('#verticalTab').easyResponsiveTabs({
                     type: 'vertical',
                     width: 'auto',
                     fit: true
                 });
             });
</script>
</asp:Content>

<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/socialsuite.Master" AutoEventWireup="true" CodeBehind="Features.aspx.cs" Inherits="SocioBoard.Features" %>
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
                        <div class="features-page-header" style=" margin-bottom:65px;">
                        	Social media marketers face great difficulty in accessing technology that is flexible enough to meet their needs for approval workflow, compliance, asset management, moderation, etc. Socioboard helps you leverage your social media marketing with great features such as advanced scheduling and publishing tools, prompt news feeds, interactive social discovery, sophisticated analytics, social CRM, help desk integration, collaboration tools, customizable reports and much more.<br /><br />
The dashboard displays activities of your friends such as shares, new messages, events, visited locations to name a few. Content can be put into sections as per date or by events.You can engage your friends on their activities, like posts, comment and even share them. Socioboard can help you set reminders about tasks related to friends’ updates shared.You can customize stats to view according to days, groups etc.

 <br />
                        </div>
                        <div id="verticalTab">
                        <ul class="resp-tabs-list" style="border-right: 1px solid #999999; width:30%;">
                            <li>Smart Inbox</li>
                            <li>Prompt Feeds</li>
                            <li>Advanced Scheduling and Publishing</li>
                            <li>Unique Social Discovery</li>
                
                            <li>In-depth Analytics and Reports</li>
                            <li>Effective and efficient Engagement</li>
                            <li>Constant Real-time Monitoring</li>
                            <li>Team Collaboration Tools</li>
                
                            <li>Social CRM</li>
                            <li>Helpdesk Integration</li>
                            <li>Mobile Apps</li>
                        </ul>
                        
                        
                     
                        
                        <div class="resp-tabs-container" style="width:64%;">
                        <div> 
                        <div class="six columns">
                            <div class="features-page-preview" href="../Contents/img/ssp/smart_inbox.png">
                                <img src="../Contents/img/ssp/smart_inbox.png" onclick="" alt="" />
                            </div>
                        </div>                       
                        <div class="six columns">
                            <div class="features-page-desc">
                                <h3>Smart Inbox</h3><br />
                                All the public posts by a user and much more would be featured on the message page along with the message picture.User can views task and archived messages easily. 
                            </div>
                        </div>
                        </div>
                      
                        <div>
                        <div class="six columns">
                            <div class="features-page-preview" href="../Contents/img/ssp/prompt_feeds.png">
                                <img src="../Contents/img/ssp/prompt_feeds.png" onclick="" alt="" />
                            </div>
                        </div>
                        <div class="six columns">
                            <div class="features-page-desc">
                                <h3>Prompt Feeds</h3>
                                <br />
                                Feeds allow you to get latest status updates from friends. As of now facebook, twitter and linkedin profile can be connected to socioboard account. Users can get wallposts, status updates and scheduled messages
Users can engage each other using the platform, get notifications on missed messages and much more.

                            </div>
                        </div>
                       
                        </div>
                       
                        <div>
                        <div class="six columns">
                            <div class="features-page-preview" href="../Contents/img/ssp/advanced_scheduling_publishing.png">
                                <img src="../Contents/img/ssp/advanced_scheduling_publishing.png" onclick="" alt="" />
                            </div>
                        </div>
                        <div class="six columns">
                            <div class="features-page-desc">
                                <h3>Advanced Scheduling and Publishing</h3><br />
                                Publishing data is important part of engaging audience through social media platforms, through socioboard’s publishing tool users can schedule messages, maintain a queue and save drafted messages. You can’t miss any news about what your friends are doing having a dedicated page for notifications.
                            </div>
                        </div>
                       </div> 
                        
                        <div>
                        <div class="six columns">
                            <div class="features-page-preview" href="../Contents/img/ssp/unique_social_discovery.png">
                                <img src="../Contents/img/ssp/unique_social_discovery.png" onclick="" alt="" />
                            </div>
                        </div>
                        <div class="six columns">
                            <div class="features-page-desc">
                                <h3>Unique Social Discovery</h3>
                                <br />
                                Discovery is a marvelous function which socioboard offers by virtue of which users can avail a highly efficient search mechanism. 
                            </div>
                        </div>
                        
                        </div>
                        <div>                       
                        <div class="six columns">
                            <div class="features-page-preview" href="../Contents/img/ssp/reports.png">
                                <img src="../Contents/img/ssp/reports.png" onclick="" alt="" />
                            </div>
                        </div>
                        <div class="six columns">
                            <div class="features-page-desc">
                                <h3>In-depth Analytics and Reports</h3><br />
                                Socioboard uses its statistics engine and generates analytics about various activities related to your account. It incorporates group stats, facebook engagement data, twitter stats and task related stats. All these analytics can be custom-viewed according to dates, groups etc.
                            </div>
                        </div>
                        </div>
                        
                        <div>
                        <div class="six columns">
                            <div class="features-page-preview" href="../Contents/img/ssp/effective_and_effi.png">
                                <img src="../Contents/img/ssp/effective_and_effi.png" onclick="" alt="" />
                            </div>
                        </div>
                        <div class="six columns">
                            <div class="features-page-desc">
                                <h3>Effective and efficient Engagement</h3>
                                <br />
                                Socioboard has capabilities to engage audiences across various platforms.It gives every platform its due importance. The degree of engagement is customized as per the platform’s importance and number of connections in it.
                            </div>
                        </div>
                        
                        </div> 

                        <div>
                        <div class="six columns">
                            <div class="features-page-preview" href="../Contents/img/ssp/constant_real.png">
                                <img src="../Contents/img/ssp/constant_real.png" onclick="" alt="" />
                            </div>
                        </div>
                        <div class="six columns">
                            <div class="features-page-desc">
                                <h3>Constant Real-time Monitoring</h3>
                                <br />
                               Socioboard monitors data continuously across various platforms, and gives regular feeds to the end users. Feeds allow you to be in touch with family and friends. Users can engage each other using the platform, get notifications on missed messages and even more.You can also stop someone from bothering you on this platform. We respect your privacy.
                            </div>
                        </div>
                        
                        </div> 

                        <div>
                        <div class="six columns">
                            <div class="features-page-preview" href="../Contents/img/ssp/team_coll.png">
                                <img src="../Contents/img/ssp/team_coll.png" onclick="" alt="" />
                            </div>
                        </div>
                        <div class="six columns">
                            <div class="features-page-desc">
                                <h3>Team Collaboration Tools</h3>
                                <br />
                               Collaboration is a very important term in social media marketing, cross-functional departments can collaborate with each other to make the marketing experience more fruitful and synergistic. Socioboard comes up with a marvelous functionality by virtue of which users can reach out to each otherusing a highly efficient collaborationmechanism. It can serve as a platform to share information with other team members and increase workability and efficiency of all teams.
                            </div>
                        </div>
                        
                        </div> 

                        <div>
                        <div class="six columns">
                            <div class="features-page-preview" href="../Contents/img/ssp/social_crm.png">
                                <img src="../Contents/img/ssp/social_crm.png" onclick="" alt="" />
                            </div>
                        </div>
                        <div class="six columns">
                            <div class="features-page-desc">
                                <h3>Social CRM</h3>
                                <br />
                               Collaboration is a very important term in social media marketing, people can collaborate with each other to make things a mutually beneficial win-win, socioboard comes up with a marvelous functionality by virtue of which users can reach out to each other using a highly efficient collaborative mechanism. It can serve as a platform to share information with other team members and increase workability and efficiency of all teams.

                            </div>
                        </div>
                        
                        </div>
                        
                        <div>
                        <div class="six columns" style=" width:100%;">
                            <div class="features-page-desc">
                                <h3>Helpdesk Integration</h3>
                                <br />
                               Socioboard not only has a world class support mechanism but also offers you helpdesk integration with your socioboard account.
                            </div>
                        </div>
                      
                        </div> 

                        <div>
                        <div class="six columns" style=" width:100%;">
                            <div class="features-page-desc">
                                <h3>Mobile Apps</h3>
                                <br />
                               Fully functional version of Socioboard is available on web. Our Android and iOS apps work in tandem with the web app.
                            </div>
                        </div>
                        </div> 

                        <div>
                        
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

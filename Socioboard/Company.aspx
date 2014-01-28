<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/socialsuite.Master" AutoEventWireup="true" CodeBehind="Company.aspx.cs" Inherits="SocioBoard.Company" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<script src="Contents/js/jquery-1.6.3.min.js" type="text/javascript"></script>
<script src="Contents/js/easyResponsiveTabs.js" type="text/javascript"></script>
<script type="text/javascript" src="../Contents/js/jquery.lightbox-0.5.js"></script>

  <div class="feature_body" id="p6">
            
            <div class="row-content">
                <div class="welcome-built" id="for-everyone">
                    <div class="features-page">
                        <h2>Company</h2>
                        <%--<div class="features-page-header">
                        	Socioboard is an advanced easy open source social media marketing tool to engage your visitors with great features such as: 
                            <span class="features-list">
                            live chat, news feed, timeline, profiles, events, notifications, likes, and so much more...</span> 
                            <a href="#list">See full list »</a>
                        </div>--%>
                        <div id="verticalTab">
                        <ul class="resp-tabs-list">
                            <li>About</li>
                            <li>Customers</li>
                            <li>Partners</li>
                            <li>Careers</li>
                        </ul>
                        <div class="resp-tabs-container" style="border-left: 1px solid #999999; float:left;">
                        <div>
                          <div class="six columns" style=" width:100%;">
                            <div class="features-page-desc">
                                <h3>About</h3><br />
                               Socioboard was started in December 2013 by Mr. Sumit Ghosh, with a vision to provide open source social media management services with world class customer support.
Sumit dons more than a decade of experience in Product Development, Architecture Planning and Project Management. His primary focus areas include Microsoft Technologies, Java and PHP (Open Source) and he’s an expert programmer on various platforms like .NET 2,3.5,4.0, Wordpress, Joomla, Magento. He’s adept in programming languages like VC++ 9, C# 3.0, PHP 5, Java, Coldfusion 8, Assembly, VB.NET, VB 6.0 etc.
While working with TCS for more than a year he was involved in researching on latest MS Technologies, making prototypes for enterprise grade applications, performance modelling and benchmarking of applications on latest .NET technologies like WCF/WPF/WF and Windows Cardspace.
Besides being a leader in his software development, Sumit is a member of Corporate Technology Excellence Group of Microsoft Practices.<br /><br /> 
Linkedin: <a href="in.linkedin.com/in/sumitghosh007/" style="color:Navy;">in.linkedin.com/in/sumitghosh007/</a>
<br />
 Objective of Socioboard is to solve the chaos surrounding social media platform management. Most applications in this domain have a very narrow and limited approach towards end users, people feel caught up in a whirlpool, Socioboard plans to help users out of this.

                            </div>
                        </div>
                        </div>
                      
                        <div>
                        <div class="six columns" style=" width:100%;">
                            <div class="features-page-desc">
                                <h3>Customers</h3>
                                <br />
                                Socioboard is primarily a service oriented organization, it’s our vision to impart any level of assistance needed. Socioboard has been striving hard to give world class support to its customers. We are committed to give any support required to our end users. <br />
                                <br />
                                Some of Socioboard’s esteemed customers include: 
                                MarketMongoose, MarketMongoose Social, YouRank, TweetRank, LinkRank, FaceRank, PinRank, Globussoft, Brandzter, AuraRank, Botguruz, Social Signifier, RebrandOne, Socialscoup, Socialpour, Mooplesocial and Blacksheep.

                            </div>
                        </div>
                       
                        </div>
                       
                        <div>
                        
                        <div class="six columns" style=" width:100%;">
                            <div class="features-page-desc">
                                <h3>Partners</h3><br />
                               Socioboard is in course of establishing partnerships with Cloud Services Providers as MS Azure, Amazon AWS etc. We are also in process of hosting plugins and applications from us as well as partners. We will continuously improve features as the feedback comes from our partners.
                               <br /><br /><br />
                               <p><img src="/Contents/img/ssp/amazonaws.png"  alt="" width="100"/>&nbsp;&nbsp;
                               <img src="/Contents/img/ssp/azure.jpg"  alt="" width="100"/> 
                               </p>
                            </div>
                        </div>
                       </div> 
                        
                        <div>
                        <div class="six columns" style=" width:100%;">
                            <div class="features-page-desc">
                                <h3>Careers</h3>
                                <br />
                                Socioboard offers you opportunities and challenges to help you advance on your career path and achieve personal and professional development. Join our team. We’ll help you develop and accelerate your career by providing you with opportunities to amplify your talents. You can expect us to nurture your strengths and value your unique perspectives, and in return we expect you to share our passion for ‘excellence’ in everything we do. 
                                <div style="width:100%; height:auto;">
                                     <form id="send" action="">
            	
                                            <p>
                                            <label for="name">First Name *</label>
                                            <input id="name" type="text" name="name" value="" />
                                            </p>
                
                                            <p>
                                            <label for="company">Last Name</label>
                                            <input id="lname" type="text" name="company" value="" />
                                            </p>
                
                                            <p>

                                            <label for="email">Email *</label>
                                            <input id="email" type="text" name="email" value="" />
                                            </p>
                
                                            <p>
                                            <label for="website">Your Resume</label>
                                            <input id="Subject" type="file" name="Subject" value="" />
                                            </p>
                             
                                            <p>
                                            <label for="profile">Message *</label>
                                            <textarea name="profile" id="profile" cols="30" rows="10"></textarea>

                                            </p>
                                             <br />
                                            <p>

                                            <button id="submit" type="button">Submit</button>
                                            </p>
                
                                        </form>
                                </div>
                            </div>
                        </div>
                       
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

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
                            <li>Our Team</li>
                        </ul>
                        <div class="resp-tabs-container" style="border-left: 1px solid #999999; float:left;">
                        <div>
                          <div class="six columns" style=" width:100%;">
                            <div class="features-page-desc">
                                <h3>About</h3><br />
                              Socioboard is a next generation social media management application which leverages handling umpteen number of social media platforms through a revolutionary open source lite version and higher end versions which have unmatched world class technical support available round the clock.
                              <br /><br />
                              We are trying to solve the problem which an average netizen faces while handling many social network profiles on the web. The mass generally has at least three social profiles, and businesses have a more complex social networking structure. The process of managing many social networks at once is very cumbersome and it takes its toll, Socioboard is here to settle all these issues.
                              <br /><br />
                              Socioboard offers the following values to your business: <br /><br />
                              Objective of Socioboard is to solve the chaos surrounding social media platform management.
                              <br />
                              <br />
                              Most applications in this domain have a very narrow and limited approach towards end users, people feel caught up in a whirlpool, Socioboard plans to help users out of this vision.
                              <br />
                              <br />
                              World class support would be offered to customers no matter what situation they are in.
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
                                MarketMongoose, MarketMongoose Social, YouRank, TweetRank, LinkRank, FaceRank, PinRank, Socioboard, Brandzter, AuraRank, Botguruz, Social Signifier, RebrandOne, Socialscoup, Socialpour, Mooplesocial and #db3f1asheep.

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
                                           <%-- <input runat="server" id="fname" type="text" name="name" value="" />--%>
                                             <asp:TextBox ID="fname" runat="server"></asp:TextBox>
                                            </p>
                
                                            <p>
                                            <label for="company">Last Name</label>
                                           <%-- <input runat="server" id="lname" type="text" name="company" value="" />--%>
                                            <asp:TextBox ID="lname" runat="server"></asp:TextBox>
                                            </p>
                
                                            <p>

                                            <label for="email">Email *</label>
                                            <%--<input runat="server" id="email" type="text" name="email" value="" />--%>
                                            <asp:TextBox ID="email" runat="server"></asp:TextBox>
                                            </p>
                
                                            <p>
                                            <label for="website">Your Resume</label>
                                            <%--<input id="cvfile" type="file" name="Subject" value="" />--%>
                                                <asp:FileUpload ID="cvfile" runat="server" />

                                            </p>
                             
                                            <p>
                                            <label for="profile">Message *</label>
                                            <textarea runat="server" name="profile" id="message" cols="30" rows="10"></textarea>

                                            </p>
                                             <br />
                                            <p>

                                            <%--<button id="submit" type="button">Submit</button>--%>
                                                <asp:Button  ID="submit" runat="server" Text="Submit" style="width:90px" 
                                                    onclick="submit_Click" /><asp:Label ID="Label1" runat="server" Text=""></asp:Label>
                                            </p>
                
                                        </form>
                                </div>
                            </div>
                        </div>
                       
                        </div>

                        <div>
                        <div class="six columns" style=" width:100%;">
                            <div class="features-page-desc">
                                <h3>Our Team</h3>
                                <br />
                                
<div>

<div style="width: 100%; height: 138px;">
<span style=" float:left;"><img height="107" width="107" src="Contents/img/ssp/sumit-sir-460x460-ver-5.jpg" alt="Sumit Ghosh" style="border: 1px solid #db3f1a; margin: 5px;" class="alignleft size-full wp-image-3912"></span>
<span style=" float:left; margin: 62px 0 0 9px;"><h2 class="MsoNormal" style=" margin:0; text-align:left; padding:0;"><strong style="font-size: 24px;font-weight: normal; color:#a02002;"> Sumit Ghosh</strong></h2>
<strong style=" color:#a02002;">Founder CEO &ndash; Socioboard </strong>
</span>
</div>
<p style="margin-bottom: 8pt;" class="MsoNormal">Sumit started Socioboard in 2009 in a garage with a couple of his friends. He has taken the company from an idea to a full-fledged software development company with a customer base of more than 4500, an employee count of 200 and a net worth of USD 10 million in just 6 years. Sumit was 23 when he started his dream company Socioboard while pursuing his 5th semester of his BE Degree at Bhilai Institute of Technology.</p>
<p style="margin-bottom: 8pt;" class="MsoNormal">He is an Entrepreneur, a Geek, an Ace Programmer, a Strategist, a Visionary, a Thinker and a Hard Core Planner and jokingly likes to call himself a Nasty Boss!</p>
<p style="margin-bottom: 8pt;" class="MsoNormal">Sumit dons more than a decade of experience in Product Development, Architecture Planning and Project Management. His primary focus areas include Microsoft Technologies, Java and PHP (Open Source) and he’s an expert programmer on various platforms like .NET 2,3.5,4.0, WordPress, Joomla, Magento. He’s adept in programming languages like VC++ 9, C# 3.0, PHP 5, Java, Coldfusion 8, Assembly, VB.NET, VB 6.0 etc.</p>
<p style="margin-bottom: 8pt;" class="MsoNormal">While working with TCS for more than a year, he was involved in researching on latest MS Technologies, making prototypes for enterprise grade applications, performance modelling and benchmarking of applications on latest .NET technologies like WCF/WPF/WF and Windows Cardspace.</p>
<p style="margin-bottom: 8pt;" class="MsoNormal">At TCS Sumit got an opportunity to work with a bunch of Fortune 100 companies like Citi Group, Ferrari, Meryll Lynch and NSE India. While at TCS, Sumit got an offer from Microsoft IDC (India Development Center) based out of Bangalore. It was a very lucrative offer with a handsome package and greater learning opportunities. However, at the peak of his career and with a dream Job in hand, he left TCS and turned down the offer from Microsoft to recreate and shape his dream company Socioboard.</p>
<p style="margin-bottom: 8pt;" class="MsoNormal">Besides being a leader in his software development, Sumit is a member of Corporate Technology Excellence Group of Microsoft Practices. He loves writing for his blog <a href="http://sumitghosh.co.in">http://sumitghosh.co.in.</a></p>
<p style="margin-bottom: 8pt;" class="MsoNormal">Sumit enjoys an admirable position in Digital Marketing space. He is quite friendly with his employees and enjoys hanging out with his family and friends. Besides, he’s also a celebrated speaker at various tech seminars, meets and conferences.</p>
<p style="margin-bottom: 8pt;" class="MsoNormal">Linkedin: <a target="_blank" href="http://in.linkedin.com/in/sumitghosh007" title="in.linkedin.com/in/sumitghosh007">in.linkedin.com/in/sumitghosh007</a></p>
</div>
<br /><br />

<div>
<div style="width: 100%; height: 138px;">
<span style=" float:left;"><img height="107" width="107" src="Contents/img/ssp/Alexey-V-Abramov1.jpg" alt="Steve" style="border: 1px solid #db3f1a; margin: 5px;" class="alignleft size-full wp-image-3912"></span>
<span style=" float:left; margin: 62px 0 0 9px;"><h2 class="MsoNormal" style=" margin:0; text-align:left; padding:0;"><strong style="font-size: 24px;font-weight: normal; color:#a02002;"> Alexey Abraham </strong></h2>
<strong style=" color:#a02002;">CTO &ndash; Socioboard</strong>
</span>
</div>
<p style="margin-bottom: 8pt;" class="MsoNormal">Alexey Abraham has over 20 years of software development experience. He has worked as Chief Technical Officer, Project &amp; Product Manager for a wide variety of business applications and complex software projects across different environments, countries and cultures.</p>
<p style="margin-bottom: 8pt;" class="MsoNormal">Alexey has worked with Fortune 500 companies like Intel, Orange UK, Comverse, NOVA, Matrix, and also Zoral Labs, Control&amp;Robotics Solutions, Alawar, Melesta Games, and several others.</p>
<p style="margin-bottom: 8pt;" class="MsoNormal">Alexey brings with him a rich experience in handling diverse backend and frontend development, computer security, complex web systems, mobile development for different platforms, low level programming for Windows &amp; Linux, surveillance and image recognition systems, banking etc. He excels in Project and Product Management, Scheduling, Risk Management, Algorithms, Computer Security Software, Networking, and Mobile Development for different platforms.</p>
<p style="margin-bottom: 8pt;" class="MsoNormal">The projects Alexey has handled are now running on millions of desktops and mobiles (as games) and on more than 200 Telephone Stations worldwide, 3 Intel FABs, several Internet Providers worldwide, and at least, 2 Israeli banks.</p>
<p style="margin-bottom: 8pt;" class="MsoNormal">At Socioboard, Alexey is responsible for chasing, enlarging, and enriching the company’s technical vision and leading all aspects of technology development in sync with the company’s strategic direction and growth objectives. He makes sure that the company’s technology strategy serves its business strategy. He supervises software development process, including problems solving and communication with clients as a part of his day-to-day job at Socioboard.</p>
<p style="margin-bottom: 8pt;" class="MsoNormal">As a leader Alexey focuses on clarity, unity and agility of all technical operations. As a person, Alexey is eazy-going, jovial and supportive to all team members. New to India, Alexey is finding interesting ways to adjust to Indian food and language while learning more about Yoga and Indian Gods.</p>
<p style="margin-bottom: 8pt;" class="MsoNormal">Linkedin: <a target="_blank" href="http://in.linkedin.com/in/towndwarf" title=" in.linkedin.com/in/towndwarf">in.linkedin.com/in/towndwarf</a></p>
</div>
<br /><br />


<div>
<div style="width: 100%; height: 138px;">
<span style=" float:left;"><img height="107" width="99" src="Contents/img/ssp/Sheebani-Chavan.jpg" alt="Sumit Ghosh" style="border: 1px solid #db3f1a; margin: 5px;" class="alignleft size-full wp-image-3912"></a></span>
<span style=" float:left; margin: 62px 0 0 9px;"><h2 class="MsoNormal" style=" margin:0; text-align:left; padding:0;"><strong style="font-size: 24px;font-weight: normal; color:#a02002;"> Shibani Chavan </strong></h2>
<strong style=" color:#a02002;"> Head- Business Operations</strong>
</span>
</div>
<p style="margin-bottom: 8pt;" class="MsoNormal">Shibani loves attending tech conferences and bar-camps. Ideating and brainstorming on simple ideas that could change the most important aspects of our day-to-day lives is her passion and she aspires to create a difference through them.</p>
<p style="margin-bottom: 8pt;" class="MsoNormal">An Engineering Graduate from Nagpur University, she brings her vast understanding and experience in managing development teams, overall Operations and Human Resources at Socioboard.</p>
</div>
<br /><br />
<div>
<div style="width: 100%; height: 138px;">
<span style=" float:left;"><img height="107" width="107" src="Contents/img/ssp/shibu1.png" alt="Shibu John" style="border: 1px solid #db3f1a; margin: 5px;" class="alignleft size-full wp-image-3912"></span>
<span style=" float:left; margin: 62px 0 0 9px;"><h2 class="MsoNormal" style=" margin:0; text-align:left; padding:0;"><strong style="font-size: 24px;font-weight: normal; color:#a02002;"> Shibu John</strong></h2>
<strong style=" color:#a02002;"> Sr. Manager &ndash; Business Operations</strong>
</span>
</div>
<p style="margin-bottom: 8pt;" class="MsoNormal">With his 10+ years of experience in IT industry and graduate in Computer Applications, Shibu brings his immense experience in programming, designing, finance and accounts and human resources to Socioboard.</p>
<p style="margin-bottom: 8pt;" class="MsoNormal">Shibu has worked with reputed organizations like Boston Analytics, Integreon Managed Solutions ,and Transworks Information Services. His expertise lies in handling various processes at Socioboard viz. Client Consulting, Business Development, Project and Process Management.</p>
<p style="margin-bottom: 8pt;" class="MsoNormal">Shibu has deep understanding, experience and proven track record in Software Project Management, Digital Marketing, Online Lead Generation, Sales Operations, and IT Operations and Solutions.</p>
<p style="margin-bottom: 8pt;" class="MsoNormal">Linkedin:<a href="http://in.linkedin.com/pub/shibu-john/21/4b6/239" title=" in.linkedin.com/pub/shibu-john/21/4b6/239"> in.linkedin.com/pub/shibu-john/21/4b6/239</a></p>
</div>
<br /><br />
<div>
<div style="width: 100%; height: 138px;">
<span style=" float:left;"><img height="107" width="94" src="Contents/img/ssp/naresh.jpg" alt="Naresh Verma" style="border: 1px solid #db3f1a; margin: 5px;" class="alignleft size-full wp-image-3912"></span>
<span style=" float:left; margin: 62px 0 0 9px;"><h2 class="MsoNormal" style=" margin:0; text-align:left; padding:0;"><strong style="font-size: 24px;font-weight: normal; color:#a02002;"> Naresh Verma</strong></h2>
<strong style=" color:#a02002;"> Sr. Manager &ndash; Brand Communications</strong>
</span>
</div>
<p style="margin-bottom: 8pt;" class="MsoNormal">Naresh is a young, ambitious and amicable man who strongly believes in ‘kaizan’ &ndash; the Japanese word for continuous personal and professional growth. Naresh’s expertise lies in managing company’s Branding, PR &amp; CSR strategies and practices as a brand communications expert.</p>
<p style="margin-bottom: 8pt;" class="MsoNormal">With his 13+ years of work experience in language, corporate communications and branding and his Masters in English Language, Mass Communication, and Management (Marketing and Strategy), Naresh drives all brand &amp; marketing communications, strategic &amp; tactical initiatives, promotion programs, related industry events and competitions, internal communications, competitive analysis, digital marketing and public relations for Socioboard and its various business units.</p>
<p style="margin-bottom: 8pt;" class="MsoNormal">Along with his Executive PGP in Marketing and Strategy from Indian Institute of Management (IIM), Raipur, Naresh continues to play a significant role in helping Socioboard create, communicate and deliver values for its stakeholders.</p>
<p style="margin-bottom: 8pt;" class="MsoNormal">Linkedin: <a target="_blank" href="http://in.linkedin.com/pub/naresh-verma/3b/36a/b36/" title="in.linkedin.com/pub/naresh-verma/3b/36a/b36/">in.linkedin.com/pub/naresh-verma/3b/36a/b36/</a></p>
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

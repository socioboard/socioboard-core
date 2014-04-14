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
                        <div id="company">
                        <ul class="resp-tabs-list">
                            <li>About</li>
                            <li>Customers</li>
                            <li>Partners</li>
                            <li>Careers</li>
                            <%--<li>Our Team</li>--%>
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
                                 Some of Socioboard's agency customers are:
                                <br /> 
                              <span><a href="http://www.socialpour.com/" target="_blank"><img src="/Contents/img/ssp/socialscoup_logo.png"  alt="Socialscoup" /></a></span>
                              <br /> <br /> <br /> <br /> 
                              <span><a href="http://socialscoup.com/" target="_blank"><img src="/Contents/img/ssp/socialpour_logo.png"  alt="Socialpour" /></a></span>
                              <br /> <br /> <br /> <br /> 
                             <span><a href="http://mooplesocial.net/" target="_blank"><img src="/Contents/img/ssp/msocial_logo.png"  alt="Mooplesocial" /></a></span>

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
                                                <label for="website" style="float: right; width: 24%; margin-top: 0px;">Supported Formats: doc, docx Max file size: 300 Kb</label>
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

                        <%--<div>
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
<p style="margin-bottom: 8pt;" class="MsoNormal">Sumit started Socioboard in December 2013 with a vision to provide a versatile, customizable, and scalable open-source social media dashboard for agencies and organization needing solutions beyond what existing social dashboards are providing . 
Sumit is deeply involved with the conception, development and branding of  Socioboard since its inception. This shows up in numerous  ways Socioboard features are being improved every day. Socioboard is ever expanding with increasing global customer base and continuous software development beating current market standards. 
Sumit is an Entrepreneur, a Geek, an Ace Programmer, a Strategist, a Visionary, a Thinker and a Hard Core Planner and jokingly likes to call himself a Nasty Boss!
Sumit dons more than a decade of experience in Product Development, Architecture Planning and Project Management. His primary focus areas include Microsoft Technologies, Java and PHP (Open Source) and he’s an expert programmer on various platforms like .NET 2, 3.5, 4.0, WordPress, Joomla, and Magento. He’s adept in programming languages like VC++ 9, C# 3.0, PHP 5, Java, Coldfusion 8, Assembly, VB.NET, VB 6.0 etc.
Besides being a leader in his software development, Sumit is a member of Corporate Technology Excellence Group of Microsoft Practices. Sumit enjoys an admirable position in Digital Marketing space. He is quite friendly with his employees and enjoys hanging out with his family and friends. Besides, he’s also a celebrated speaker at various tech seminars, meets and conferences.
Connect with Sumit: <a href="http://in.linkedin.com/in/sumitghosh007"></a>in.linkedin.com/in/sumitghosh007</a>
</p>

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

<div>
<div style="width: 100%; height: 138px;">
<span style=" float:left;"><img height="107" width="94" src="Contents/img/ssp/ajay-pandey.gif" alt="Ajay Kumar Pandey" style="border: 1px solid #db3f1a; margin: 5px;" class="alignleft size-full wp-image-3912"></span>
<span style=" float:left; margin: 62px 0 0 9px;"><h2 class="MsoNormal" style=" margin:0; text-align:left; padding:0;"><strong style="font-size: 24px;font-weight: normal; color:#a02002;">Ajay Kumar Pandey</strong></h2>
<strong style=" color:#a02002;"> .Net Lead Developer</strong>
</span>
</div>
<p style="margin-bottom: 8pt;" class="MsoNormal">Ajay Kumar Pandey has almost two years of experience on Desktop Applications & ASP.Net platform including e-Commerce domain, he has knowledge on Social Network Applications, Facebook , Twitter , LinkedIn , Instagram SDKs and other social APIs.
Ajay has expertise in ASP-C#.Net, WPF, WCF, MVC4.0, Multithreading, Delegate, Event, Java, C, C++, Ruby, JQuery, HTML5,XML, and XAML
Microsoft Dos 6.0 to Windows,SQL Server, MS Access,MySQL,PostGreSQL,MySQL tuning.</p>
<p style="margin-bottom: 8pt;" class="MsoNormal">Ajay has worked on various projects namely:</p>
<p style="margin-bottom: 8pt;" class="MsoNormal">
Search Engine Ranker<br />
WooSuite<br />
Mashup Deals<br />
Dashboard Brandzter<br />
FaceDominator <br />
LinkedDominator<br />
TweetDominator <br />
YouTube Manager<br />
</p>
<p style="margin-bottom: 8pt;" class="MsoNormal">Ajay brings in youthful energy and punch in the Socioboard team.
At socioboard Ajay works as the lead developer and is responsible for development of various APIs. He is the person you want on your side to keep things up and running and to assist you if in need of support. </p>
<p style="margin-bottom: 8pt;" class="MsoNormal">Linkedin: <a target="_blank" href="http://in.linkedin.com/in/ajaypandeymca" title="http://in.linkedin.com/in/ajaypandeymca">in.linkedin.com/in/ajaypandeymca</a></p>
</div>

<div>
<div style="width: 100%; height: 138px;">
<span style=" float:left;"><img height="107" width="94" src="Contents/img/ssp/abhay-mondal.jpg" alt="Abhay Kr Mondal" style="border: 1px solid #db3f1a; margin: 5px;" class="alignleft size-full wp-image-3912"></span>
<span style=" float:left; margin: 62px 0 0 9px;"><h2 class="MsoNormal" style=" margin:0; text-align:left; padding:0;"><strong style="font-size: 24px;font-weight: normal; color:#a02002;">Abhay Kr Mondal</strong></h2>
<strong style=" color:#a02002;">Associate .Net Developer</strong>
</span>
</div>
<p style="margin-bottom: 8pt;" class="MsoNormal">Abhay has almost three years of experience in the analysis, design, development and implementation on Client/Web Server development with HTML, CSS, C, C#, VB, ASP.NET, Silverlight, WPF, ADO.NET,XAML, Windows 8 Metro app, Java Script, AJAX, Jquery, SQL Server 2006/08, ORACLE ,MYSQL. Abhay has domain knowledge in e-Commerce and Social Network Applications and Social Mashups of Facebook , Twitter , LinkedIn, Instagram SDKs and other social APIs.</p>
<p style="margin-bottom: 8pt;" class="MsoNormal">Abhay has worked on various projects namely:</p>
<p style="margin-bottom: 8pt;" class="MsoNormal">
PowerGrid<br />
ONGC<br />
Heritage for Mount Carmel<br />
Woosuite<br />

</p>
<p style="margin-bottom: 8pt;" class="MsoNormal">At socioboard Abhay works as the associate developer and develops various APIs. Abhay continues to offer immense benefits to socioboard by virtue of his experience and competencies.</p>
<p style="margin-bottom: 8pt;" class="MsoNormal">Linkedin: <a target="_blank" href="http://in.linkedin.com/in/abhaymondal/" title="http://in.linkedin.com/in/abhaymondal/">in.linkedin.com/in/abhaymondal/</a></p>
</div>
                                
                            </div>
                        </div>
                       
                        </div>--%>
                       
                    </div>
                       </div> 
                    </div>
                </div>
            
                
        	</div>
            
           
		</div>
        <!--end feature_body-->
         <script type="text/javascript">
             $(document).ready(function () {
                 $('#company').easyResponsiveTabs({
                     type: 'vertical',
                     width: 'auto',
                     fit: true
                 });
             });
</script>

</asp:Content>

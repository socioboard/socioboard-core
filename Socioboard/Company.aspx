<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/socialsuite.Master"
    AutoEventWireup="true" CodeBehind="Company.aspx.cs" Inherits="SocioBoard.Company" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="feature_body" id="p6">
        <div class="row-content">
            <div class="welcome-built" id="for-everyone">
                <div class="features-page">
                    <h2>Company</h2>
                   
                    <div id="company">
                        <ul class="resp-tabs-list">
                            <li>About</li>
                            <li>Customers</li>
                            <li>Partners</li>
                            <li>Careers</li>
                            <%--<li>Our Team</li>--%>
                        </ul>
                        <div class="resp-tabs-container" style="border-left: 1px solid #999999; float: left;">
                            <div>
                                <div class="six columns" style="width: 100%;">
                                    <div class="features-page-desc">
                                        <h3>
                                            About</h3>
                                        <br />
                                        Socioboard is a next generation social media management application which leverages
                                        handling umpteen number of social media platforms through a revolutionary open source
                                        lite version and higher end versions which have unmatched world class technical
                                        support available round the clock.
                                        <br />
                                        <br />
                                        We are trying to solve the problem which an average netizen faces while handling
                                        many social network profiles on the web. The mass generally has at least three social
                                        profiles, and businesses have a more complex social networking structure. The process
                                        of managing many social networks at once is very cumbersome and it takes its toll,
                                        Socioboard is here to settle all these issues.
                                        <br />
                                        <br />
                                        Socioboard offers the following values to your business:
                                        <br />
                                        <br />
                                        Objective of Socioboard is to solve the chaos surrounding social media platform
                                        management.
                                        <br />
                                        <br />
                                        Most applications in this domain have a very narrow and limited approach towards
                                        end users, people feel caught up in a whirlpool, Socioboard plans to help users
                                        out of this vision.
                                        <br />
                                        <br />
                                        World class support would be offered to customers no matter what situation they
                                        are in.
                                    </div>
                                </div>
                            </div>
                            <div>
                                <div class="six columns" style="width: 100%;">
                                    <div class="features-page-desc">
                                        <h3>
                                            Customers</h3>
                                        Some of Socioboard's agency customers are:
                                        <br />
                                        <span><a href="http://www.socialpour.com/" target="_blank">
                                            <img src="/Contents/img/ssp/socialscoup_logo.png" alt="Socialscoup" /></a></span>
                                        <br />
                                        <br />
                                        <br />
                                        <br />
                                        <span><a href="http://socialscoup.com/" target="_blank">
                                            <img src="/Contents/img/ssp/socialpour_logo.png" alt="Socialpour" /></a></span>
                                        <br />
                                        <br />
                                        <br />
                                        <br />
                                        <span><a href="http://mooplesocial.net/" target="_blank">
                                            <img src="/Contents/img/ssp/msocial_logo.png" alt="Mooplesocial" /></a></span>
                                    </div>
                                </div>
                            </div>
                            <div>
                                <div class="six columns" style="width: 100%;">
                                    <div class="features-page-desc">
                                        <h3>
                                            Partners</h3>
                                        <br />
                                        Socioboard is in course of establishing partnerships with Cloud Services Providers
                                        as MS Azure, Amazon AWS etc. We are also in process of hosting plugins and applications
                                        from us as well as partners. We will continuously improve features as the feedback
                                        comes from our partners.
                                        <br />
                                        <br />
                                        <br />
                                        <p>
                                            <img src="/Contents/img/ssp/amazonaws.png" alt="" width="100" />&nbsp;&nbsp;
                                            <img src="/Contents/img/ssp/azure.jpg" alt="" width="100" />
                                        </p>
                                    </div>
                                </div>
                            </div>
                            <div>
                                <div class="six columns" style="width: 100%;">
                                    <div class="features-page-desc">
                                        <h3>
                                            Careers</h3>
                                        <br />
                                        Socioboard offers you opportunities and challenges to help you advance on your career
                                        path and achieve personal and professional development. Join our team. We’ll help
                                        you develop and accelerate your career by providing you with opportunities to amplify
                                        your talents. You can expect us to nurture your strengths and value your unique
                                        perspectives, and in return we expect you to share our passion for ‘excellence’
                                        in everything we do.
                                        <div style="width: 100%; height: auto;">
                                            <form id="send" action="">
                                            <p>
                                                <label for="name">
                                                    First Name *</label>
                                                <%-- <input runat="server" id="fname" type="text" name="name" value="" />--%>
                                                <asp:TextBox ID="fname" runat="server" required="required"></asp:TextBox>
                                            </p>
                                            <p>
                                                <label for="company">
                                                    Last Name *</label>
                                                <%-- <input runat="server" id="lname" type="text" name="company" value="" />--%>
                                                <asp:TextBox ID="lname" runat="server" required="required"></asp:TextBox>
                                            </p>
                                            <p>
                                                <label for="email">
                                                    Email *</label>
                                                <%--<input runat="server" id="email" type="text" name="email" value="" />--%>
                                                <asp:TextBox ID="email" runat="server" required="required"></asp:TextBox>
                                            </p>
                                            <p>
                                                <label for="website">
                                                    Your Resume *</label>
                                                <%--<input id="cvfile" type="file" name="Subject" value="" />--%>
                                                <asp:FileUpload ID="cvfile" runat="server" required="required" />
                                                <label for="website" style="float: right; width: 24%; margin-top: 0px;">
                                                    Supported Formats: doc, docx Max file size: 300 Kb</label>
                                            </p>
                                            <p>
                                                <label for="profile">
                                                    Message *</label>
                                                <textarea runat="server" name="profile" id="message" cols="30" rows="10" required="required"></textarea>
                                            </p>
                                            <br />
                                            <p>
                                                <asp:Button ID="submit" runat="server" Text="Submit" Style="width: 90px" OnClick="submit_Click" />
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
    <script src="../Contents/js/easyResponsiveTabs.js" type="text/javascript"></script>
    <script type="text/javascript" src="../Contents/js/jquery.lightbox-0.5.js"></script>
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

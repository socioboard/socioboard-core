<%@ Page Title="Home Page" Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs"
    Inherits="blackSheep._Default" %>

<!DOCTYPE html>
<html>
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8">
    <title>blackSheep</title>
    <link rel="stylesheet" type="text/css" href="../Contents/css/index.css" media="all">
    <!-- HTML5 shim, for IE6-8 support of HTML5 elements -->
    <!--[if lt IE 9]>
		<script src="../Contents/js/html5shiv.js" type="text/javascript"></script>
	<![endif]-->
    <script type="text/javascript" src="Contents/js/jquery-1.9.1.min.js"></script>
    <script src="Contents/js/md5.js" type="text/javascript"></script>
    <!--this scipt is used for video popup-->
    <script type="text/javascript" src="Contents/js/jquery.bpopup.min.js"></script>
    <script type="text/javascript" src="Contents/js/jquery.easing.1.3.js"></script>


    <link rel="apple-touch-icon-precomposed" sizes="144x144" href="../Contents/img/apple-touch-icon-144-precomposed.png">
  <link rel="apple-touch-icon-precomposed" sizes="114x114" href="../Contents/img/apple-touch-icon-114-precomposed.png">
  <link rel="apple-touch-icon-precomposed" sizes="72x72" href="../Contents/img/apple-touch-icon-72-precomposed.png">
  <link rel="apple-touch-icon-precomposed" href="../Contents/img/apple-touch-icon-57-precomposed.png">
  <link rel="shortcut icon" href="../Contents/img/favicon.png">  
  
    <script src="Contents/js/JSlogin.js" type="text/javascript"></script>
    <script type="text/javascript">	
		$(document).ready(function(){	
         $("#txtPassword").bind("keydown", function (event) {
            var keycode = (event.keyCode ? event.keyCode : (event.which ? event.which : event.charCode));
            if (keycode == 13) {
                signinFunction();
                return false;
            } else {
                return true;
            }
        });


        $("#signupbtn").click(function(){
        $('#popupplans').bPopup({
					easing: 'easeOutBack',
					positionStyle: 'fixed',
					speed: 650,
					transition: 'slideDown'
				});
        });
        

			$.fn.placeholder();		
			
			$("#signinpopup").click(function(){
			document.getElementById('signinemailMessages').innerHTML = "";
				document.getElementById('signinpasswordMessages').innerHTML = "";
				$('#logindiv').bPopup({
					easing: 'easeOutBack',
					positionStyle: 'fixed',
					speed: 650,
					transition: 'slideDown'
				});
			
			});
			


			//this script is used for video popup
			$('#blackSheep_video_link').click(function()
			{					
				$('#popupdiv').bPopup({
					content:'iframe', //'ajax', 'iframe' or 'image'					
				});			
			});
			
		});
		// placeholder script here for Ie
		(function(cash) {
		  $.fn.placeholder = function() {
			if(typeof document.createElement("input").placeholder == 'undefined') {
			  $('[placeholder]').focus(function() {
				var input = $(this);
				if (input.val() == input.attr('placeholder')) {
				  input.val('');
				  input.removeClass('placeholder');
				}
			  }).blur(function() {
				var input = $(this);
				if (input.val() == '' || input.val() == input.attr('placeholder')) {
				  input.addClass('placeholder');
				  input.val(input.attr('placeholder'));
				}
			  }).blur().parents('form').submit(function() {
				$(this).find('[placeholder]').each(function() {
				  var input = $(this);
				  if (input.val() == input.attr('placeholder')) {
					input.val('');
				  }
			  })
			});
		  }
		}
		})(jQuery);
    </script>
</head>
<body>
    <!--wrapper-->
    <div class="wrapper">
        <!--container-->
        <div class="container">
            <!--header-->
            <header class="header">
			<!--bg1-->
			<div class="bg1">
				<div class="logo">
					<a href="#"><img alt="blackSheep" width="254" height="51" src="Contents/img/blackSheep_logo.png" height="54" width="142"></a>
				</div>
				<nav class="auth-links">
					<a class="btn bg-auth" id="signinpopup">Sign In</a>
				</nav>
				<ul class="menu">
					<li>
						<a href="#">About</a>
					</li>
					<li>
						<a href="#">Pricing</a>
					</li>
					<li>
						<a href="#">Contacts</a>
					</li>
				</ul>
			</div>
			<!--end bg1-->
			
			<!--bg2-->
			<div class="bg2">
				<div class="bg-in">
					<header class="title">
						<strong>All-in-one solution to manage your Social Media</strong>
						blackSheep takes care of your Social Media like nobody else
					</header>
					
					<div class="reg-form">                                        
						<fieldset>
							<ul class="form">
							  <li>
									<div class="input">
										<input value="" name="user[email]" id="signinFirstName" onblur="if(this.value == ''){document.getElementById('signinFirstNameError').innerHTML = 'Please Enter FirstName';document.getElementById('signinFirstNameError').style.display ='block';}else{document.getElementById('signinFirstNameError').innerHTML = '';}"
										 placeholder="FirstName" size="30" type="text">
									</div>
								</li>
								<li>
									<div style="display:none;" id="signinFirstNameError" class="input">
										
									</div>
								</li>
								  <li>
									<div class="input">
										<input value="" name="user[email]" onblur="if(this.value == ''){document.getElementById('signinLastNameError').innerHTML = 'Please Enter LastName';document.getElementById('signinLastNameError').style.display ='block';}else{document.getElementById('signinLastNameError').innerHTML = '';}"
										 id="signinLastName" placeholder="LastName" size="30" type="text">
									</div>
								</li>
								 <li>
									<div style="display:none;" id="signinLastNameError" class="input">
										
									</div>
								</li>
								<li>
									<div class="input">
										<input value="" name="user[email]" id="signinEmail" onblur="checkEmailDefault(this.value);" placeholder="E-mail" size="30" type="text">
									</div>
								</li>
								 <li>
									<div style="display:none;" id="signinEmailError" class="input">
										
									</div>
								</li>
								<li>              
								
									<div class="input">
										<input  name="user[password]" id="signinpassword" onblur="if(this.value == ''){document.getElementById('signinpasswordError').innerHTML = 'Please Enter Password';document.getElementById('signinpasswordError').style.display ='block';}else{document.getElementById('signinpasswordError').innerHTML = '';}"
										 placeholder="Password" size="30" type="password">
									</div>                    
								</li>
								<li>
									<div style="display:none;" id="signinpasswordError" class="input">
										
									</div>
								</li>
								<li>
									<div class="input">
										<input name="user[password_confirmation]"  onblur="checkPasswordConfirmtion(this.value);" id="signinpasswordConfirmation" placeholder="Password confirmation" size="30" type="password">
									</div>
								</li>
								<li>
									<div style="display:none;" id="signinpasswordConfirmationError" class="input">
										
									</div>
								</li>
								<li>
								<div style="display:none;" id="completeError"></div>
								</li>

			<%--	onclick="RegisterDefault();"	--%>			<li id="signupbtn">
									<input class="btn" name="commit" value="Sign up for free" type="button" />
								</li>
								
								<li class="info">
									By clicking on "Sign up for free" button, you agree to the
									<a href="#" target="_top">Terms of Service</a>
								</li>
							</ul>
							<div class="text"></div>
						</fieldset>
					</div>
				
					<section class="video">
						<a href="#" id="blackSheep_video_link"><img alt="Vid" src="Contents/img/video_run.png" height="299" width="504"></a>            
						<div class="text"></div>
					</section>
				</div>
			</div>
			<!--end bg2-->
		</header>
            <!--end header-->
            <!--content-->
            <div class="content">
                <!--benefits-->
                <section class="benefits">
				<article>
					<a class="icon bg1" href="#"><span></span></a>
					<strong><a href="#">Dashboard</a></strong>
					Complete dashboard with preview of your social accounts
				</article>
				
				<article>
					<a class="icon bg2" href="#"><span></span></a>
					<strong><a href="#">Messages</a></strong>
					 View messages for all your social accounts in one tab
				</article>
				
				<article>
					<a class="icon bg3" href="#"><span></span></a>
					<strong><a href="#">Feeds</a></strong>
					Have all your social accounts displayed in one ordered feed
				</article>
				
				<article>
					<a class="icon bg4" href="#"><span></span></a>
					<strong><a href="#">Publish</a></strong>
					Publish messages in all your social accounts or schedule for later
				</article>
				
				<article>
					<a class="icon bg5" href="#"><span></span></a>
					<strong><a href="#">Discover</a></strong>
					Discover new trends and customers with a powerful social search
				</article>
				
				<article>
					<a class="icon bg6" href="#"><span></span></a>
					<strong><a href="#">Reports</a></strong>
					Full information on your social networks in easy to read reports
				</article>
			</section>
                <!--end benefits-->
                <header class="title">
				<strong>Why pay more?</strong>
				All important tools for your Social Networks in one convenient dashboard
			</header>
                <div class="screen">
                    <img alt="1" src="Contents/img/dashborad.jpg" height="496" width="990" />
                </div>
                <!--screens-->
                <section class="screens">
				<header class="title">
					<strong>Easy-to-use interface</strong>
					For beginners and professionals
				</header>
				
				<!--<article>
					<img alt="2" src="Contents/img/rays.jpg" height="260" width="260" />
					<span class="cover"></span>
				</article>
				
				<article>
					<img alt="3" src="Contents/img/antivirus.jpg" height="260" width="260" />
					<span class="cover"></span>
				</article>
				
				<article>
					<img alt="4" src="Contents/img/pricing.jpg" height="260" width="260" />
					<span class="cover"></span>
				</article>-->
				
				
				<!--social_media_links-->
				<div class="social_media_links">
					<div class="social_link">
						<a class="scl_links fb_icon"></a>                        
						<a class="scl_links google_icon"></a>
						<a class="scl_links twt_icon"></a>
						<a class="scl_links instagram_icon"></a>
						<a class="scl_links linked_icon"></a>
					</div>
				</div>
				<!--end social_media_links-->
			</section>
                <!--end screens-->
                <header class="title">
				<strong>What people say about blackSheep</strong>
				Real testimonials from real people
			</header>
                <!--testimonials-->
                <section class="testimonials">
				<article>
					I have a lot of domains through namecheap, but with an interface like this, I might turn notifications off 
					there and rely on this as it is much more user friendly.
					<div class="author">Brian Jackson</div>
				</article>
				
				<article>
					That’s a great tool, finally I will manage all my domains in one place.
					<div class="author">Amandeep Singh</div>
				</article>
				
				<article>
					Great tool! Was getting VERY sick of all the spam from Godaddy, Namecheap and the like… but was useful so 
					didn’t lose a domain. Good solution – appreciate it.
					<div class="author">David Smith</div>
				</article>            
			</section>
                <!--end testimonials-->
                <nav class="links">
				
				<a class="btn" href="Plans.aspx">Sign up!</a>
			   <!-- <a class="btn btn-grey login-demo" href="#">Demo account</a> -->
			</nav>
            </div>
            <!--end content-->
            <div style="height: 56px;" class="footer-place">
            </div>
        </div>
        <!--end container-->
    </div>
    <!--wrapper-->
    <!--footer-->
    <footer class="footer">
	<!--container-->
	<div class="container">
		<nav class="bnav">
		
			<section>
				<strong>Information</strong>
				<ul>
					<li>
						<a href="#">Home</a>
					</li>
					<li>
						<a href="#">About</a>
					</li>
					<li>
						<a href="#">Pricing</a>
					</li>
					<li>
						<a href="#">Terms of Use</a>
					</li>
					<li>
						<a href="#">Sign Up</a>
					</li>
				</ul>
			</section>
		   
			<section>
				<strong>Features</strong>
				<ul>
					<li>
						<a href="#">Reminders</a>
					</li>
					<li>
						<a href="#">Monitoring</a>
					</li>
					<li>
						<a href="#">Security</a>
					</li>
					<li>
						<a href="#">Whois</a>
					</li>
					<li>
						<a href="#">Finances</a>
					</li>
					<li>
						<a href="#">Notes &amp; Files</a>
					</li>
				</ul>
			</section>
			
			<section>
				<strong>Advanced users</strong>
				<ul>
					<li>
						<a href="#">Affiliate program</a>
					</li>
					<li>
						<a href="#">Public Whois</a>
					</li>
				</ul>
			</section>
			
			<section>
				<strong>Communication</strong>
				<ul>
					<li>
						<a href="#">Contacts</a>
					</li>
					<li>
						<a href="#" rel="nofollow" target="_top">Facebook</a>
					</li>
					<li>
						<a href="#" rel="nofollow" target="_top">Twitter</a>
					</li>
				</ul>
			</section>
			
			<section>
				<strong>Language</strong>
					<ul>
					<li><img alt="Flag-en" src="Contents/img/englishflag.png" height="11" width="16"> English</li>
				   <!-- <li><a href="#"><img alt="Flag-ru" src="Contents/img/rusian.png.png" height="11" width="16"> Russian</a></li> -->
				</ul>
			</section>
			
		</nav>
		
		<div class="hr"></div>
		Copyright © 2013 blackSheep Ltd.
	</div>
	<!--end container-->
</footer>
    <!--end footer-->

     <%--plans popup--%>
      <div id="popupplans" style="background-color: #FFFFFF; border-radius: 10px 10px 10px 10px;
        box-shadow: 0 0 25px 5px #999999; color: #111111; display: none; min-width: 450px;
        padding: 25px; min-height: 330px;">
        <span class="button b-close">x</span>
       
        <div class="pricing-container">
            <div class="pricing-inner">
                <h2>
                    All Plans Include a Free 30-Day Trial</h2>
                <h4>
                    Upgrade, Change or Cancel Anytime</h4>
                <div class="pricing-list-container">
                    <div class="price-shadow-1">
                        <div class="pricing-list">
                            <div class="price-head">
                                Standard</div>
                            <h3>
                                $39 Per User/Month</h3>
                            <p>
                                Every plan includes remarkable social tools. This one is great for small teams.</p>
                            <ul>
                                <li><strong>All-In-One Social Inbox</strong></li>
                                <li>Real-time Brand Monitoring</li>
                                <li>Advanced Publishing Features</li>
                                <li>Social CRM Tools</li>
                                <li>Comprehensive Reporting Tools</li>
                                <li>Complimentary Training & Support</li>
                                <li>Complimentary Training & Support</li>
                            </ul>
                            <a id="standard" onclick="RegisterDefault(this.id);" class="trial-butn">
                                <img src="../Contents/img/trial-butn-1.png" alt="" /></a>
                        </div>
                    </div>
                    <div class="price-shadow-1">
                        <div class="pricing-list">
                            <div class="price-head">
                                Deluxe</div>
                            <h3 style="color: #3491da;">
                                $59 Per User/Month</h3>
                            <p>
                                Everything you need to effectively manage a growing social presence.</p>
                            <ul>
                                <li><strong>All Standard Plan Features</strong></li>
                                <li>Complete Publishing & Engagement</li>
                                <li>Helpdesk Integration</li>
                                <li>Deluxe Reporting Package</li>
                                <li>Google Analytics Integration</li>
                                <li>Complimentary Training & Support</li>
                                <li>Manage up to 20 profiles</li>
                            </ul>
                            <a id="deluxe" onclick="RegisterDefault(this.id);" class="trial-butn">
                                <img src="../Contents/img/trial-butn-2.png" alt="" /></a>
                        </div>
                    </div>
                    <div class="price-shadow-2">
                        <div class="pricing-list">
                            <div class="price-head">
                                Premium</div>
                            <h3>
                                $99 Per User/Month</h3>
                            <p>
                                Includes advanced tools for more sophisticated objectives</p>
                            <ul>
                                <li><strong>All Standard & Deluxe Features</strong></li>
                                <li>Social Care Suite</li>
                                <li><strong>ViralPost</strong>&trade; Send Time Optimization</li>
                                <li>Premium Reporting Package</li>
                                <li>Custom Branded Interface</li>
                                <li>Complimentary Training & Support</li>
                                <li>Manage up to 50 profiles</li>
                            </ul>
                            <a id="premium" onclick="RegisterDefault(this.id);" class="trial-butn">
                                <img src="../Contents/img/trial-butn-1.png" alt="" /></a>
                        </div>
                    </div>
                    <div class="clearfix">
                    </div>
                </div>
            </div>
        </div>
        <div class="clearfix">
        </div>
    </div>

    <!--login popup-->
    <div id="logindiv">
        <span class="button b-close"><span>X</span></span>
        <div class="signin">
            Sign in</div>
        <!--signin_temp-->
        <div class="signin_temp">
            <div class="left_area">
                <div class="inputbg">
                    <input type="text" id="txtEmail" placeholder="Email" onblur="checkEmail(this.value);" /></div>
                <div class="error">
                    <div id="signinemailMessages">
                    </div>
                </div>
                <div class="input_pswd_bg">
                    <input type="password" id="txtPassword" placeholder="Password" onblur="if(this.value == ''){document.getElementById('signinpasswordMessages').innerHTML='Please Enter Password';}else{document.getElementById('signinpasswordMessages').innerHTML='';}" />
                </div>
                <div class="error">
                    <div id="signinpasswordMessages">
                    </div>
                </div>
                <%--<div class="forgot_pswd"><input type="checkbox"  />Remember me</div>--%>
                <div class="forgot_pswd">
                    Forgot password? <a href="../ForgotPassword.aspx">Click here to restore</a></div>
                <div class="loginbtn">
                    <a onclick="signinFunction();">
                        <img id="btnLogin" src="../Contents/img/login_btn.png" alt="" /></a>
                </div>
            </div>
            <div class="right_area">
                <div class="social_bg">
                    <div class="you_cantext">
                        You can also login with your social account</div>
                    <div class="social_icon_bg">
                        <div class="fb_icon">
                            <a id="fb_account" href="#" onclick="facebookLogin();">
                                <img src="../Contents/img/fb_icon_lgn.png" alt="" />
                            </a>
                        </div>
                        <div class="google_icon">
                            <a id="gp_account" href="#" onclick="googleplusLogin();">
                                <img src="../Contents/img/google_icon.png" alt="" />
                            </a>
                        </div>
                    </div>
                    <%-- <div class="register_text">
						Don`t registered? <a href="../Plans.aspx">Register now!</a></div>--%>
                </div>
            </div>
        </div>
        <!--signin_temp-->
    </div>
    <!--end login popup-->
    <!--video popup-->
    <div id="popupdiv">
        <span class="button b-close"></span>
        <div class="content">
            <iframe width="640" height="360" frameborder="0" allowfullscreen="" src="http://www.youtube.com/embed/pPb2lIap6Es?rel=0" />
        </div>
    </div>
    <!--end video popup-->
   
  
</body>
</html>

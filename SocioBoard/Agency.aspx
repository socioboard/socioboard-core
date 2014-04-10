<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/socialsuite.Master"
    AutoEventWireup="true" CodeBehind="Agency.aspx.cs" Inherits="SocioBoard.Agency" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

<script type="text/javascript">
    $(document).ready(function () {
        //alert("asdads");
        $('#submit').click(function () {
            //alert('asdasd');
            var fname = $('#fname').val();
            var lname = $('#lname').val();
            var email = $('#email').val();
            var company = $('#company').val();
            var profile = $('#profile').val();
            var phone = $('#phone').val();
            if (fname == "" || email == "" || company == "" || profile == "" || phone == "") {
                alert('Please fill all the fields')
                return false;
            }
            else {
                if (!validateEmail(email)) {
                    alert('not valid email');
                    return false;
                }
                else if (!validatePhone(phone))
                {
                alert('Phone not valid');
                    return false;
                }

                else {
                    //alert('come on ajax');
                    //ajax start

                    $.ajax({
                        url: "../Helper/Ajaxagency.aspx",
                        type: "post",
                        data: 'fname=' + fname + '&lname=' + lname + '&email=' + email + '&company=' + company + '&profile=' + profile + '&phone=' + phone,
                        success: function (data) {
                            if (data == "success") {
                                alert('Mail Send');
                                $('#fname').val('');
                                $('#lname').val('');
                                $('#email').val('');
                                $('#company').val('');
                                $('#profile').val('');
                                $('#phone').val('');
                            }
                            else {
                                alert("failure");
                                return false;
                            }
                        },
                        error: function () {
                            alert("failure");
                            //$("#result").html('There is error while submit');
                        }
                    });


                    //ajax end
                }

            }




        });
    });
    function validateEmail($email) {
        var emailReg = /^([\w-\.]+@([\w-]+\.)+[\w-]{2,4})?$/;
        if (!emailReg.test($email)) {
            return false;
        } else {
            return true;
        }
    }

    function validatePhone(field) {
        var mobile = field;
        var pattern = /^\d{10}$/;
        if (pattern.test(mobile)) {
            //alert("Your mobile number : " + mobile);
            return true;
        }
       // alert("It is not valid mobile number.input 10 digits number!");
        return false;
    }

</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="p6" class="feature_body">
        <div class="row-content">
            <div id="for-everyone" class="welcome-built1" style="text-align: left;">
                <div class="features-page">
                   
                        <div class="entry-form">
                        <div class="left_contact" id="signup-inner">
                        <h2>Now a Social Media Dashboard You Can Call Your Own!</h2>
                        <h3>Interested? One of our account managers <br /> will be in touch...</h3>
            	     <p>
                        <label for="name">First Name *</label>
                        <input id="fname" type="text" value="" name="name" id="name">
                    </p>
                    <p>
                        <label for="company">Last Name</label>
                        <input id="lname" type="text" value="" name="company" id="lname">
                    </p>
                    <p>
                        <label for="email">Email *</label>
                        <input id="email" type="text" value="" name="email" id="email">
                    </p>
                    <p>
                        <label for="website">Company *</label>
                        <input id="company" type="text" value="" name="Company" id="Company">
                    </p>
                    <p>
                        <label for="profile">Message *</label>
                        <textarea rows="6" cols="30" id="profile" name="profile"></textarea>
                     </p>
                     <p>
                        <label for="phone">Phone.no *</label>
                        <input id="phone" type="text" value="" name="phone">
                    </p>
                    <br />
                     <p>
                        <button type="button" id="submit">Submit</button>
                    </p>
                
            </div>
                        </div>
                    <div class="entry-content1">
                    
                            <h2>Are you an Agency?</h2> <br />
<p><span style="color: #A02002; font-size:16px; font-weight:bold;"> Increase and nurture</span> your clientele with the Socioboard’s offerings designed uniquely for the Agencies. These versions have a <span style="color: #A02002; font-size:16px; font-weight:bold;">great interface </span> that anybody can understand, <span style="color: #A02002;font-size:16px; font-weight:bold;">powerful engagement tools </span> and <span style="color: #A02002;font-size:16px; font-weight:bold;">customizable analytics</span>  to monitor, analyze and report across countless social profiles. What’s more, you can feature <span style="color: #A02002;font-size:16px; font-weight:bold;">your own logo and domain name</span>, so your clients consider it your product, not to mention Socioboard anywhere.</p> <br>
<p>We offer two distinct variations to cater to our agency clients:</p>
<ol style="none" class="d4pbbc-ol"><p></p>
<ul>
<li>SaaS</li>
<li>White Label</li>
</ul>
<p></p></ol>

<br />
<img src="Contents/img/ssp/phone-banner-new1.png" alt="" width="84%" />
<br /><br />
<p><span style="color: #A02002;font-size:16px; font-weight:bold;">Software as a Service (SaaS)</span> version would be suitable for small agencies and also individual end users. It has a very versatile pricing plan which includes:</p>
<ol style="none" class="d4pbbc-ol"><p></p>
<ul>
<li>Basic (FREE)</li>
<li>Standard</li>
<li>Deluxe</li>
<li>Premium</li>
</ul>
<p></p></ol>
<p>Click <a href="http://socioboard.com/Pricing.aspx">here </a>to check out the pricing and advantages of the SaaS versions.</p>
<br />
<p><span style="color: #A02002;font-size:16px; font-weight:bold;">White Label </span> version is best suited to Small and Medium Enterprises (SMEs) who’d need support, updates, upgrades and related apps for being able to use Socioboard effectively to continue to build their clientele and their brands.</p>
<p><strong>&nbsp;Benefits:</strong></p>
<ol style="none" class="d4pbbc-ol"><p></p>
<ul>
<li>Customizable as per your needs</li>
<li>Flexible Client Management</li>
<li>Competitive price</li>
<li>World Class Support</li>
</ul>
<p></p></ol>
<p>The White Label version is priced at $2999 per annum. Click <a href="http://www.socioboard.com/Agency.aspx?id=wlabel">here</a> to buy the While label version now!</p>
                                                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/socialsuite.Master"
    AutoEventWireup="true" CodeBehind="Enterprise.aspx.cs" Inherits="SocioBoard.Enterprise" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script type="text/javascript">
        //        function _doPostback() {
        //            alert("Hello");
        //        }
        var ccode = '';
        $(document).ready(function () {
            //alert('Abhay');
            RefreshCaptcha();
            // alert(ccode);

            $('#submit').click(function () {
                // alert('asd');
                var name = $('#name').val();
                var designation = $('#designation').val();
                var company = $('#company').val();
                var location = $('#location').val();
                var website = $('#website').val();
                var email = $('#email').val();
                var phone = $('#phone').val();
                var message = $('#message').val();
                var captcha = $('#captcha').val();

                if (name == "" || designation == "" || company == "" || location == "" || website == "" || email == "" || phone == "" || message == "" || captcha == "") {
                    alert('Please fill all the fields');
                    return false;
                }
                if (IsEmail(email) == false) {
                    alert('Invalid email Id');
                    return false;
                }


                $.ajax({
                    type: 'POST',
                    url: '../Helper/Ajaxenterprise.aspx?name=' + name + '&designation=' + designation + '&company=' + company + '&location=' + location + '&website=' + website + '&email=' + email + '&phone=' + phone + '&message=' + message + '&captcha=' + captcha,
                    success: function (msg) {
                        debugger;
                        if (msg == "success") {
                            alert('Message send Successfully');
                            $('#name').val('');
                            $('#designation').val('');
                            $('#company').val('');
                            $('#location').val('');
                            $('#website').val('');
                            $('#email').val('');
                            $('#phone').val('');
                            $('#message').val('');
                            RefreshCaptcha();
                        }
                        else if (msg = "Wrong captcha") {
                            alert(msg);
                            debugger;
                            RefreshCaptcha();
                        }

                        else {
                            alert(msg);
                        }
                    }
                });
            });
        });

        function IsEmail(email) {
            var regex = /^([a-zA-Z0-9_\.\-\+])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$/;
            if (!regex.test(email)) {
                return false;
            } else {
                return true;
            }
        }
        function RefreshCaptcha() {
           // alert('asdasd');
            //            var img = document.getElementById("imgCaptcha");
            //            ccode = Math.random();
            //            img.src = "";
            //           img.src = "../Helper/captcha.ashx";
            $('#divcaptcha').html('');
           // alert('asdasd1');
            $('#divcaptcha').html('<img src=../Helper/captcha.ashx?query=' + Math.random()+'/>');
           // alert('asdasd2');
           // $('#imgCaptcha').attr('src', '../Helper/captcha.ashx');
        }
    </script>
    <style type="text/css">
        label
        {
            color: #A02002;
            font-family: Arial;
            font-size: 16px;
            padding-bottom: 5px;
        }
    </style>
    <div id="p6" class="feature_body">
        <div class="row-content">
            <div id="for-everyone" class="welcome-built" style="text-align: left;">
                <div class="features-page">
                    <h2>
                        Enterprise</h2>
                    <div class="entry-content">
                        <p>
                            <span style="color: #fff; font-family: Arial; text-align: left;">Enterprise Edition
                                is best suited to businesses or groups looking at versatile social media dashboard
                                capable of serving their niches well. Any institution with growing social media
                                presence would want to have an enterprise edition of Socioboard. The degree of independence
                                and hassle free experience which it offers to the end users is unmatched and it
                                allows customers to focus on their business instead of wandering in the maze of
                                social media management. Enterprise version offers support, updates and applications
                                for unlimited profiles to be managed at an office location. The enterprise version
                                starts from $9999 per location and is priced depending on your enterprises' custom
                                requirements.
                                <br />
                                <br />
                                Interested in using Socioboard Enterprise version for your business now? Post your
                                enquiry here.
                                <br />
                                <br />
                                <div id="signup-inner" style="width: 700px; height: auto; float: left;">
                                    <form id="send" action="">
                                    <p>
                                        <label>
                                            Name</label>
                                        <input id="name" type="text" name="name" value="" />
                                    </p>
                                    <p>
                                        <label>
                                            Designation</label>
                                        <input id="designation" type="text" name="Designation" value="" />
                                    </p>
                                    <p>
                                        <label>
                                            Company</label>
                                        <input id="company" type="text" name="Company" value="" />
                                    </p>
                                    <p>
                                        <label>
                                            Location</label>
                                        <input id="location" type="text" name="Location" value="" />
                                    </p>
                                    <p>
                                        <label>
                                            Company Website</label>
                                        <input id="website" type="text" name="Company Website" value="" />
                                    </p>
                                    <p>
                                        <label>
                                            Contact email id</label>
                                        <input id="email" type="text" name="Contact email id" value="" />
                                    </p>
                                    <p>
                                        <label>
                                            Phone</label>
                                        <input id="phone" type="text" name="Phone" value="" />
                                    </p>
                                    <p>
                                        <label>
                                            Message</label>
                                        <textarea name="Message" id="message" cols="30" rows="10" style="width: 50%;"></textarea>
                                    </p>
                                    <p>
                                        <label>
                                            Captcha</label>
                                        <br />
                                        <div id="divcaptcha" style="width: 90px; height: 30px; border: 1px solid #999; background: #fff;">
                                            <%--<img id="imgCaptcha" />--%></div>
                                        <br />
                                        <input id="captcha" type="text" value="" style="width: 15%;" />
                                    </p>
                                    <br />
                                    <p>
                                        <button id="submit" type="button">
                                            Submit</button>
                                    </p>
                                    </form>
                                </div>
                                <br />
                                </br >
                                <div class="g1-divider g1-divider--none g1-divider--noicon " id="g1-divider-1">
                                </div>
                                <span style="height: 1.5em;" class="g1-space " id="g1-space-2"></span>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

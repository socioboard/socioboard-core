<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/socialsuite.Master" AutoEventWireup="true" CodeBehind="Referrals.aspx.cs" Inherits="SocioBoard.Referrals" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%--<script type="text/javascript">
        $(document).ready(function () {

            $('.b-close').click(function () {
                $('.loaderr').css('display', 'none');
            });


            getuserid();
            $('#facebook').click(function () {
                var msg = $('#url').val();
                //alert('Click Abhay');
                $.ajax({
                    type: "POST",
                    url: "../Helper/Ajaxfacetwt.aspx?type=fb&msg=" + msg,
                    data: '',
                    contentType: "application/json; charset=utf-8",
                    dataType: "html",
                    success: function (msg) {
                        // debugger;
                        window.location = msg;
                        //alert('Posted successfully');
                    }
                });
            });
            $('#twitter').click(function () {
                var msg = $('#url').val();
                //alert('Click Abhay');
                $.ajax({
                    type: "POST",
                    url: "../Helper/Ajaxfacetwt.aspx?type=twt&msg=" + msg,
                    data: '',
                    contentType: "application/json; charset=utf-8",
                    dataType: "html",
                    success: function (msg) {
                        // debugger;
                        window.location = msg;
                        //alert('Posted successfully');
                    }
                });
            });

            $('.btnclick').click(function () {
                // alert('asdsd');
                var type = $(this).attr('name');
                //                alert(type);
                //                return false;
                var allmsg = '';
                $('.loaderr').css('display', 'block');
                $.ajax({
                    type: "POST",
                    url: "../Helper/AjaxInviteFrndsByCloudSponge.aspx?type=GetMails&hint=" + type,
                    data: '',
                    success: function (msg) {
                        //                        window.open(msg,'_blank');
                        //window.location = msg;
                        //alert(msg);

                        window.open(msg, '_blank');
                        //alert(msg);
                        //window.location = msg;

                        getcontacts();

                        //                        var arr = msg.split('cloudspongeseperator');
                        //                        alert(arr[0]);
                        //                        alert(arr[1]);
                        //                        return false;
                        //                        allmsg = msg;
                        // bindemail(msg);
                    }
                });
            });


            $('#sendmaill').click(function () {
                //alert('asdasd');
                var mail = $('#txtmail').val();
                $.ajax({
                    type: "POST",
                    url: "../Helper/AjaxInviteFrndsByCloudSponge.aspx?type=sendmail&mail=" + mail,
                    data: '',
                    contentType: "application/json; charset=utf-8",
                    dataType: "html",
                    success: function (msg) {
                        alert('Mail saved successfully!');
                    }
                });
            });


            $('#selecctall').on('click', function (event) {  //on click
                if (this.checked) { // check select status
                    $('.mailcheckbox').each(function () { //loop through each checkbox
                        this.checked = true;  //select all checkboxes with class "checkbox1"              
                    });
                } else {
                    $('.mailcheckbox').each(function () { //loop through each checkbox
                        this.checked = false; //deselect all checkboxes with class "checkbox1"                      
                    });
                }
            });

            $('#clksendmail').click(function () {
                $("#sendmail").bPopup().close();
                var allcheckedmail = '';
                $("#mailappend").find("input:checked").each(function (i, ob) {
                    //                    alert($(ob).val());
                    //                    alert($(ob).attr('fname'));
                    //                    alert($(ob).attr('lname'));
                    allcheckedmail += $(ob).attr('fname') + '<~>' + $(ob).attr('lname') + '<~>' + $(ob).val() + '<:>';
                });
                //  alert(allcheckedmail);
                if (allcheckedmail != "") {
                    //$('.loaderr').css('display', 'block');
                    sendAllSectedMail(allcheckedmail);
                }
                else {
                    alert('Please select mail!');
                }
            });


        });



        function getcontacts() {
            //alert('getcontacts');
            $.ajax({
                type: "POST",
                url: "../Helper/AjaxInviteFrndsByCloudSponge.aspx?type=getContacts",
                data: '',
                contentType: "application/json; charset=utf-8",
                dataType: "html",
                success: function (msg) {
                    //alert(msg);
                    bindemail(msg);


                    // alert('Posted successfully');
                }
            });
        };


        function getuserid() {
           // alert('asdasd');
            $.ajax({
                type: "POST",
                url: "../Helper/Ajaxfacetwt.aspx?type=getuserid",
                data: '',
                success: function (msg) {
                    debugger;
                    //alert(msg);
                    var arr = msg.split('<:>');
                    if (arr[0] == "success") {
                        //alert('success');
                        $('#txturl').val('');
                        $('#txturl').val(arr[1]);
                    }
                    else {
                        alert('Problem');
                        //  window.location = "Default.aspx";
                    }

                    // alert('Posted successfully');
                }
            });
        };

        function bindemail(strmail) {
            var htmlappend = '';
            var arr = strmail.split('<:>');
            $.each(arr, function (index, value) {
                //alert(value);
                var namearr = value.split('<~>');
                //alert(namearr[0]);
                htmlappend += '<div class="input_text"><input fname="' + namearr[0] + '" lname="' + namearr[1] + '" class="mailcheckbox" type="checkbox" name=' + namearr[2] + ' value=' + namearr[2] + '>' + namearr[2] + '</div>';
                //alert(htmlappend);
            });
            //alert(htmlappend);
            $('#mailappend').html(htmlappend);

            //alert(arr[0]);

            $('#sendmail').bPopup({
                easing: 'easeOutBack',
                positionStyle: 'fixed',
                speed: 650,
                transition: 'slideDown'
            });
        };

        function sendAllSectedMail(strmail) {
            $.ajax({
                type: "POST",
                url: "../Helper/AjaxInviteFrndsByCloudSponge.aspx?type=sendselectedmail",
                data: "selectedmail=" + strmail,
                success: function (msg) {
                    //alert('Mail Saved Successfully!');
                    $('.loaderr').css('display', 'none');
                    alert('All Message Send Successfully!')
                }
            });
        };


    </script>--%>
     
       <script type="text/javascript">
           $(document).ready(function () {

               getuserid();



               var csPageOptions = {
                   domain_key: "PPLVMKEAKUS87BJDU7J3",
                   afterSubmitContacts: function (array_of_contacts) {
                       var allcheckedmail = '';
                       for (i in array_of_contacts) {
                           var email = array_of_contacts[i].email[0].address;
                           var firstname = array_of_contacts[i].first_name;
                           if (firstname == undefined) {
                               firstname = '';
                           }

                           var lastname = array_of_contacts[i].last_name;
                           if (lastname == undefined) {
                               lastname = '';
                           }

                           allcheckedmail += firstname + '<~>' + lastname + '<~>' + email + '<:>';
                       };
                       if (allcheckedmail != "") {
                           //$('.loaderr').css('display', 'block');
                           sendAllSectedMail(allcheckedmail);
                       }
                       else {
                           alert('Please select mail!');
                       }

                   }
               };



           });



            
            



          

           function getuserid() {
               // alert('asdasd');
               $.ajax({
                   type: "POST",
                   url: "../Helper/Ajaxfacetwt.aspx?type=getuserid",
                   data: '',
                   success: function (msg) {
                       debugger;
                       //alert(msg);
                       var arr = msg.split('<:>');
                       if (arr[0] == "success") {
                           //alert('success');
                           $('#txturl').val('');
                           $('#txturl').val(arr[1]);
                       }
                       else {
                           alert('Problem');
                           //  window.location = "Default.aspx";
                       }

                       // alert('Posted successfully');
                   }
               });
           };

         
           function sendAllSectedMail(strmail) {
               $.ajax({
                   type: "POST",
                   url: "../Helper/AjaxInviteFrndsByCloudSponge.aspx?type=sendselectedmail",
                   data: "selectedmail=" + strmail,
                   success: function (msg) {
                       //alert('Mail Saved Successfully!');
                       $('.loaderr').css('display', 'none');
                       alert('All Message Send Successfully!')
                   }
               });
           };


    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <link href="../Contents/css/socialsuite.css" rel="stylesheet" type="text/css" />
    <script> !function (d, s, id) { var js, fjs = d.getElementsByTagName(s)[0], p = /^http:/.test(d.location) ? 'http' : 'https'; if (!d.getElementById(id)) { js = d.createElement(s); js.id = id; js.src = p + '://platform.twitter.com/widgets.js'; fjs.parentNode.insertBefore(js, fjs); } } (document, 'script', 'twitter-wjs');</script>
    
    
    
    <div id="fb-root">
    </div>
    <script>        (function (d, s, id) {
            var js, fjs = d.getElementsByTagName(s)[0];
            if (d.getElementById(id)) return;
            js = d.createElement(s); js.id = id;
            js.src = "//connect.facebook.net/en_GB/all.js#xfbml=1&appId=517105231670294";
            fjs.parentNode.insertBefore(js, fjs);
        } (document, 'script', 'facebook-jssdk'));</script>




    <div id="sendmail" style="display: none;">
        <span class="button b-close"><span>X</span></span>
        <div class="signin">
            Invite Friends</div>
        <!--signin_temp-->
        <div class="signin_temp">
            <div class="left_area">
                <div class="inputbg">
                    <input id="selecctall" type="checkbox" />Select All<br>
                </div>
                <div id="mailappend" class="inputbg">
                </div>
                <div class="loginbtn">
                    <a id="clksendmail">
                        <%--<img alt="" src="../Contents/img/login_btn.png" id="btnLogin">--%><img alt="" src="Contents/img/sendbtn.png" id="btnLogin"></a>
                </div>
            </div>
        </div>
        <!--signin_temp-->
    </div>

    <div id="p6" class="feature_body">
        <div class="row-content">
            <div id="for-everyone" class="welcome-built" style="text-align: left;">
                <div class="features-page">
                   <%-- <h2>
                        Agency</h2>--%>
                    <div class="entry-content">
                        <div id="invit">
                            <div class="invit_text">
                                <h1>
                                    Get free <strong>Premium Account</strong> by inviting Your Friends to SocioBoard!</h1>
                                <p>
                                    For every friend who joins Socioboard ,we'll give
                                    you 1 month of free <strong>Premium Account</strong>. Get 12 friends to
                                    signup and enjoy a full year of Free Premium SocioBoard Account!</p>
                                   
                                <p>
                                   <%-- If you need even more space,<a href="#">upgrade your account</a></p>--%>
                            </div>
                            <div class="loaderr"><img alt="" src="Contents/img/ssp/Newloader.gif"></div>
                            <div class="invit_form">
                                <div class="invit_emailform">
                                   <a href="#" onclick="return parent.cloudsponge.launch();">Invite your friends by email</a>

                                      <div class="ygmailbg">
                                         <a class="btnclick" name="gmail" href="#"><img src="../Contents/img/ssp/gmail.png" alt="" /></a>
                                         <a class="btnclick" name="yahoo" href="#"><img src="../Contents/img/ssp/yahoomail.png" alt="" /></a>
                                         <a class="btnclick" name="hotmail" href="#"><img src="../Contents/img/ssp/hotmail.png" alt="" /></a>
                                      </div>

                                    <input id="txtmail" type="text" placeholder="Add names or emails" />
                                    <input id="sendmaill" type="button" class="button" value="Send" />
                                </div>
                            </div>
                            <div class="invit_share">
                                <h3>
                                    More ways to invite your friends</h3>
                                <ul>
                                    <li>
                                        <div class="copylinks">
                                            <a id="cpoylink" class="links">
                                                <img src="../Contents/img/ssp/link.png" alt="" />
                                                Copy Link</a>
                                            <input id="txturl" type="text" class="url" placeholder="url" />
                                        </div>
                                    </li>
                                    <%-- <li><a id="facebook" href="#">
                                        <img src="../Contents/img/ssp/facebook.png" alt="" /></a></li>
                                    <li><a id="twitter" href="#">
                                        <img src="../Contents/img/ssp/twitter_1.png" alt="" /></a></li>--%>
                                    <li>
                                        <div class="fb-share-button" data-href="http://www.socioboard.com" data-type="button_count">
                                        </div>
                                    </li>
                                    <li><a href="https://twitter.com/share" class="twitter-share-button" data-url="http://www.socioboad.com"
                                        data-via="Socioboard">Tweet</a></li>
                                </ul>
                            </div>
                        </div>
                        <br />
                    </div>
                </div>
            </div>
        </div>
    </div>
      <script type="text/javascript" src="https://api.cloudsponge.com/address_books.js"></script>
</asp:Content>

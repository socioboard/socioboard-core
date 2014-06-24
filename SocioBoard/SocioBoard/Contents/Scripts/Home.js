
var profilescountingtosend = 0;
var singleprofileid = '';
var profilesofcomposeinsession = '';
var singleprofileIdforscheduler = '';

/*Show Facebook pages dialog to redirect 
on facebook to add facebook accounts or fan pages*/

function ShowFacebookDialog(modal) {
    debugger;
    $("#overlay").show();
    $("#fbConnect").fadeIn(300);

    if (modal) {
        $("#overlay").unbind("click");
    }
    else {
        $("#overlay").click(function (e) {
            HideFacebookDialog();
        });
    }
}



/*Hide Facebook pages dialog which redirect 
on facebook to add facebook accounts or fan pages*/
function HideFacebookDialog() {

    $("#overlay").hide();
    $("#fbConnect").fadeOut(300);
    $("#fbConnect").hide();
}



/*Bind the social profiles of all networks,
will display on home page*/
function BindSocialProfiles() {
    try {
        $.ajax
        ({
            type: "POST",
            url: "../AjaxHome.aspx?op=social_connectivity",
            data: '',
            contentType: "application/json; charset=utf-8",
            dataType: "html",
            success: function (msg) {
                // alert(msg);
                debugger;
                $("#manageprofiles").html(msg);
            }
        });
    } catch (e) {

    }
}


/*bind the divs which has the basic user profile in
home page according to Social Profiles*/
function BindMidSnaps(loadtype) {
    $("#home-load").html('<img src="../../Contents/Images/360.gif" />');

    $.ajax
        ({
            type: "POST",
            url: "../AjaxHome.aspx?op=midsnaps&loadtype=" + loadtype,
            data: '',
            contentType: "application/json; charset=utf-8",
            dataType: "html",
            success: function (msg) {

                debugger;
                $(document).on('scroll', onScroll);
                $("#midsnaps").append(msg);
                $("#home-load").html("");
            }
        });
}



/*this function will call when user want to scroll down to see all 
the user profiles div according to social profiles*/
function onScroll(event) {
    var closeToBottom = ($(window).scrollTop() + $(window).height() > $(document).height() - 100);
    if (closeToBottom) {
        $(document).off('scroll', onScroll);
        BindMidSnaps("scroll");
    }
}


/*This function will delete the profile from our database 
and remove profile from Network Profiles**/
function confirmDel(profileid, profile, userid) {
    try {
        debugger;
        var confir = confirm("Are you Sure want to delete the account.And your data will be erased completely");


        if (confir) {
            $('#' + profileid).hide();
            $('#midsnap_' + profileid).hide();

            $.ajax
        ({
            type: "POST",
            url: "../AjaxHome.aspx?op=accountdelete&profile=" + profile + "&profileid=" + profileid,
            data: '',
            contentType: "application/json; charset=utf-8",
            dataType: "html",
            success: function (msg) {
                debugger;
            }
        });
        }
    } catch (e) {

    }
}



/*Binding All profiles for 
compose message to post on Social Networks */
function bindProfilesComposeMessage() {
    debugger;
    try {

        try {
            profilesofcomposeinsession = $.session('compose');
        } catch (e) {

        }

        if (profilesofcomposeinsession == undefined) {

            $.ajax
        ({
            type: "GET",
            url: "../AjaxHome.aspx?op=MasterCompose",
            data: '',
            contentType: "application/json; charset=utf-8",
            dataType: "html",
            success: function (msg) {
                debugger;
                $.session('compose', msg);

                var addmsg = msg.replace(/composemessage/g, "addAnotherProfileforMessage");
                $("#addBox").html(addmsg);
                $("#loginBox").html(msg);

                var countinguserids = document.getElementById('loginBox');
                var countdivofloginbox = countinguserids.getElementsByTagName('li');
                var firstid = '';
                for (var i = 0; i < countdivofloginbox.length; i++) {
                    firstid = countdivofloginbox[i].id;
                    break;
                }

                composemessage(firstid, 'fb');
            }
        });
        } else {
            debugger;
            var foraddboxprofiles = profilesofcomposeinsession.replace(/composemessage/g, "addAnotherProfileforMessage");

            $("#addBox").html(foraddboxprofiles);
            $("#loginBox").html(profilesofcomposeinsession);

            var countinguserids = document.getElementById('loginBox');
            var countdivofloginbox = countinguserids.getElementsByTagName('li');
            var firstid = '';
            for (var i = 0; i < countdivofloginbox.length; i++) {
                firstid = countdivofloginbox[i].id;
                break;
            }

            composemessage(firstid, 'fb');
        }

    } catch (e) {

    }
}


/*This array will contain the all selected profiles
to post message on Social Netowork*/
var chkidforusertest = new Array();


/*Function to Compose Message for single 
user to send the message on SocialNetwork*/
function composemessage(id, network) {
    debugger;
    try {
        var userid = id.split('_');
        var imageurl = document.getElementById('imgurl_' + userid[1]).innerHTML;
        try {
            document.getElementById('imageofuser').src = imageurl;
        } catch (e) {

        }

        try {
            document.getElementById('imageofuser_scheduler').src = imageurl;
        } catch (e) {

        }
        if (network == 'fb') {
            document.getElementById('socialIcon').src = "../Contents/Images/facebook.png";
            try {
                document.getElementById('socialIcon_scheduler').src = "../Contents/Images/facebook.png";
            } catch (e) {

            }
            singleprofileid = 'fb_' + userid[1];
            singleprofileIdforscheduler = 'fbscheduler_' + userid[1];
        } else if (network == 'twt') {
            document.getElementById('socialIcon').src = "../Contents/Images/twitter.png";
            try {
                document.getElementById('socialIcon_scheduler').src = "../Contents/Images/twitter.png";
            } catch (e) {

            }
            singleprofileid = 'twt_' + userid[1];
            singleprofileIdforscheduler = 'twtscheduler_' + userid[1];

        } else if (network == 'lin') {
            document.getElementById('socialIcon').src = "../Contents/Images/link.png";
            try {
                document.getElementById('socialIcon_scheduler').src = "../Contents/Images/link.png";
            } catch (e) {

            }
            singleprofileid = 'lin_' + userid[1];
            singleprofileIdforscheduler = 'linscheduler_' + userid[1];
        }

        try {
            $("#loginBox_scheduler").hide();
        } catch (e) {

        }

    } catch (e) {
        debugger;
    }
}





/*Post a single message for Multiple user of different social networks*/
function addAnotherProfileforMessage(id, network) {
    debugger;
    var divbindforscheduler = '';
    var divbind = '';
    var innerhtmlofMulti = $("#divformultiusers").html();
    var userid = id.split('_');
    var username = document.getElementById('composename_' + userid[1]).innerHTML;
    var innerhtmlofmultischeduler = $("#divformultiusers_scheduler").html();



    try {
        if (innerhtmlofMulti.indexOf(username) == -1) {
            debugger;



            if (network == 'fb') {
                divbind = '<div class="profilesonComposebox" id="fb_' + userid[1] + '"  onclick="delProfilesFromMultiusers(this.id)" ><img src="../Contents/Images/facebook.png" alt="" width="15"/>' + username + '<div class="closewprofiles"></div></div>';

            } else if (network == 'twt') {
                divbind = '<div class="profilesonComposebox" id="twt_' + userid[1] + '" onclick="delProfilesFromMultiusers(this.id)"><img src="../Contents/Images/twitter.png" alt="" width="15"/>' + username + '<div class="closewprofiles"></div></div>';

            } else if (network == 'lin') {
                divbind = '<div class="profilesonComposebox" id="lin_' + userid[1] + '" onclick="delProfilesFromMultiusers(this.id)"><img src="../Contents/Images/link.png" alt="" width="15"/>' + username + '<div class="closewprofiles"></div></div>';

            }
        }
    } catch (e) {

    }



    try {
        if (innerhtmlofmultischeduler.indexOf(username) == -1) {
            if (network == 'fb') {
                divbindforscheduler = '<div class="profilesonComposebox" id="fbscheduler_' + userid[1] + '"  onclick="delProfilesFromMultiusers(this.id)" ><img src="../Contents/Images/facebook.png" alt="" width="15"/>' + username + '<div class="closewprofiles"></div></div>';

            } else if (network == 'twt') {
                divbindforscheduler = '<div class="profilesonComposebox" id="twtscheduler_' + userid[1] + '" onclick="delProfilesFromMultiusers(this.id)"><img src="../Contents/Images/twitter.png" alt="" width="15"/>' + username + '<div class="closewprofiles"></div></div>';

            } else if (network == 'lin') {
                divbindforscheduler = '<div class="profilesonComposebox" id="linscheduler_' + userid[1] + '" onclick="delProfilesFromMultiusers(this.id)"><img src="../Contents/Images/link.png" alt="" width="15"/>' + username + '<div class="closewprofiles"></div></div>';
            }
        }
    } catch (e) {

    }
    try {
        $("#divformultiusers").append(divbind);
    } catch (e) {

    }
    try {
        $("#divformultiusers_scheduler").append(divbindforscheduler);
    } catch (e) {

    }
    try {
        $("#addBox").hide();
    } catch (e) {

    }
    try {
        $("#ab_scheduler").hide();
    } catch (e) {

    }
        
    }


    /*To clear the popup on close of Compose popup*/
    function closeonCompose() {
        debugger;
        $("#adddates_scheduler").html('');
        chkidforusertest.length = 0;

        try {
            datearr.length = 0
        } catch (e) {

        }

        debugger;
        try {
            $("#divformultiusers").html('');
        } catch (e) {

        }
        try {
            $("#divformultiusers_scheduler").html('');
        } catch (e) {

        }
        try {
            $("#textareavaluetosendmessage").val('');
        } catch (e) {

        }
        try {
            $("#textareavaluetosendmessage_scheduler").val('');
        } catch (e) {

        }
        try {
            $('#messageCount').html(140);
        } catch (e) {

        }
        try {
            $('#messageCount_scheduler').html(140);
        } catch (e) {

        }
        chkidforusertest.length = 0;
        try {
            $("#sendMessageBtn").html('<img src="../Contents/Images/sendbtn.png" alt="" />');
        } catch (e) {

        }
        try {
            $("#sendMessageBtn_scheduler").html('<img src="../Contents/Images/sendbtn.png" alt="" />');
        } catch (e) {

        }
    }




    /*This function  sends the composed 
    message of  SocialNetwork*/
    function SendMessage() {
        debugger;
        var message = $("#textareavaluetosendmessage").val();
        if (message != '') {


            $("#sendMessageBtn").html('<img src="../Contents/Images/325.gif" alt="" />');
            var bindingofdata = document.getElementById('divformultiusers');
            var countdiv = bindingofdata.getElementsByTagName('div');

            for (var i = 0; i < countdiv.length; i++) {
                chkidforusertest.push(countdiv[i].id);
            }

            if (chkidforusertest.indexOf(singleprofileid) == -1) {
                chkidforusertest.push(singleprofileid);
            }


            debugger;

            $.ajax
        ({
            type: 'POST',
            url: '../AjaxHome.aspx?op=sendmessage&message=' + message + '&userid[]=' + chkidforusertest,
            contentType: "application/json; charset=utf-8",
            success: function (data) {
                debugger;

                $("#composeBox").bPopup().close();
                closeonCompose();
            }
        });
        } else {

            alert("please enter the message");
        }

    }




function delProfilesFromMultiusers(id) {
    debugger;
    alert(id);
    

 

    try {
        debugger;
        $("#divformultiusers_scheduler").find("#" + id).remove();
        $("#"+id).remove();
    } catch (e) {
    debugger;
    alert(e);
    }

}
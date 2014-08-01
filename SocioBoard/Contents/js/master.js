$("#addprofilesfromMaster").click(function (e) {
    debugger;
    $("#expanderContentForMaster").slideToggle();
});
$("#facebook_connect_master").click(function (e) {
    debugger;
    ShowFacebookDialog(false);
    e.preventDefault();
});

$("#composecontent").click(function () {
    debugger;
    //  ("#composeBox").bPopup();
    //closeonCompose();
    $("#showBlock").css('display', 'none');
    $('#composeBox').bPopup({
        fadeSpeed: 'slow', //can be a string ('slow'/'fast') or int
        followSpeed: 1500, //can be a string ('slow'/'fast') or int
        modalColor: 'black',
        modalClose: false,
        opacity: 0.6,
        positionStyle: 'fixed'
    });

    var totalmessagewords = 140;
    // totalmessagewords = Countmessagewords();

    bindProfilesComposeMessage();

    $('#textareavaluetosendmessage').bind('keyup', function () {


        totalmessagewords = Countmessagewords();


        var charactersUsed = $(this).val().length;

        if (charactersUsed > totalmessagewords) {
            charactersUsed = totalmessagewords;
            $(this).val($(this).val().substr(0, totalmessagewords));
            $(this).scrollTop($(this)[0].scrollHeight);
        }

        var charactersRemaining = totalmessagewords - charactersUsed;

        $('#messageCount').html(charactersRemaining);

    });
});



function Countmessagewords() {
    var Fbidcount = 0;
    var twtIdcount = 0;
    var LinkedInIdcount = 0;
    var tumblrIdcount = 0;
    var totalmessagewords1 = 0;
    var chkidforusertest = new Array();

    var bindingofdata = document.getElementById('divformultiusers');
    var countdiv = bindingofdata.getElementsByTagName('div');

    for (var i = 0; i < countdiv.length; i++) {
        chkidforusertest.push(countdiv[i].id);
    }

    if (chkidforusertest.indexOf(singleprofileid) == -1) {
        chkidforusertest.push(singleprofileid);
    }


    for (var i = 0; i < chkidforusertest.length; i++) {

        try {
            var arr = chkidforusertest[i].split('_');


            if (arr[0].indexOf("fb") != -1) {
                Fbidcount++;
            }
            if (arr[0].indexOf("twt") != -1) {
                twtIdcount++;
            }
            if (arr[0].indexOf("lin") != -1) {
                LinkedInIdcount++;
            }
            if (arr[0].indexOf("lin") != -1) {
                tumblrIdcount++;
            }
        } catch (e) {

        }
    }

    if (Fbidcount > 0 && twtIdcount == 0 && LinkedInIdcount == 0 && tumblrIdcount == 0) {
        totalmessagewords1 = 5000;
    }
    else if (Fbidcount >= 0 && twtIdcount == 0 && LinkedInIdcount > 0 && tumblrIdcount == 0) {
        totalmessagewords1 = 700;
    }

    else {
        totalmessagewords1 = 140;
    }

    return totalmessagewords1;
}

$('#commonmenuforAll').click(function () {
    $('#commonmenuforAllClick').toggle();
});


$("#searchcontent").click(function () {
    $('#contactvalue').val('');
    $("#contactsection").html('');
    $("#contactsearch").bPopup({
        positionStyle: 'fixed'
    });

    $.ajax
        ({
            type: "POST",
            url: "../Helper/AjaxHelper.aspx?op=usersearchresults",
            data: '',
            contentType: "application/json; charset=utf-8",
            dataType: "html",
            success: function (msg) {
                debugger;
            },
            Error: function (err) { debugger; }
        });

});
var searchajax = '';

$("#contactvalue").keyup(function () {

    debugger;
    try {
        searchajax.abort();
    } catch (e) {

    }
    searchajax = $.ajax
        ({
            type: "POST",
            url: "../Helper/AjaxHelper.aspx?op=searchingresults&txtvalue=" + document.getElementById('contactvalue').value,
            data: '',
            contentType: "application/json; charset=utf-8",
            dataType: "html",
            success: function (msg) {
                debugger;
                $("#contactsection").html('');
                $("#contactsection").html(msg);
            },
            Error: function (err) {
                debugger;
                alert(err);
            }
        });

});




$("#resendmail").click(function () {
    //alert('hua');
    var uid = $(this).attr('uid');
    $.ajax
        ({
            type: "POST",
            url: "../Helper/AjaxMailSender.aspx?op=resendmail",
            data: 'uid=' + uid,
            contentType: "application/json; charset=utf-8",
            dataType: "html",
            success: function (msg) {
                var arr = msg.split('~');
                if (arr[0] == "Success") {
                    alert('Mail Sent to :' + arr[1] + '');
                }
                else {
                    alert('Message not send');
                }
            }
        });
});




$(document).ready(function () {

    $('#usersetting').click(function () {
        $('.userset').slideToggle();
    });
});
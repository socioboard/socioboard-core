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
    closeonCompose();
    $('#composeBox').bPopup({
        fadeSpeed: 'slow', //can be a string ('slow'/'fast') or int
        followSpeed: 1500, //can be a string ('slow'/'fast') or int
        modalColor: 'black',
        modalClose: false,
        opacity: 0.6,
        positionStyle: 'fixed'
    });

    var totalmessagewords = 140;
    bindProfilesComposeMessage();

    $('#textareavaluetosendmessage').bind('keyup', function () {

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

$('#commonmenuforAll').click(function () {
    $('#commonmenuforAllClick').toggle();
});


$("#searchcontent").click(function () {

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

            }
        });

});
var searchajax = '';

$("#contactvalue").keyup(function () {

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
            }
        });

});


$(document).ready(function () {

    $('#usersetting').click(function () {
        $('.userset').slideToggle();
    });
});
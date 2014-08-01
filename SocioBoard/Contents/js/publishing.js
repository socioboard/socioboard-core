
function publishcontent(id,msg,msgid) {
    debugger;
    if (id == "schedulemessage") {
        $("#content").css('display', 'block');
        $("#content_wooqueue").css('display', 'none');
        $("#content_drafts").css('display', 'none');
        $("#content_rsspost").css('display', 'none');
        var mmsg = '';
        if (msg != "") {
            mmsg = msg;
            $("#textareavaluetosendmessage_scheduler").val(mmsg);
            $('#schedulemessage').attr('msgid', msgid);
        }
    }
    else if (id == "wooqueue") {
        debugger;
        $("#content").css('display', 'none');
        $("#content_wooqueue").css('display', 'block');
        $("#content_drafts").css('display', 'none');
        $("#content_rsspost").css('display', 'none');


        $.ajax
        ({
            type: "POST",
            url: "../AjaxHome.aspx?op=wooqueuemessages",
            data: '',
            contentType: "application/json; charset=utf-8",
            dataType: "html",
            success: function (msg) {
                debugger;
                $("#wooqueue_messages").html(msg);
            }
        });
    }
    else if (id == "drafts") {
        $("#content").css('display', 'none');
        $("#content_wooqueue").css('display', 'none');
        $("#content_drafts").css('display', 'block');
        $("#content_rsspost").css('display', 'none');

        $.ajax
        ({
            type: "POST",
            url: "../AjaxHome.aspx?op=woodrafts",
            data: '',
            contentType: "application/json; charset=utf-8",
            dataType: "html",
            success: function (msg) {
                debugger;
                $("#drafts_messages").html(msg);
            }
        });



    } else if (id == "rsspost") {
        $("#content").css('display', 'none');
        $("#content_wooqueue").css('display', 'none');
        $("#content_drafts").css('display', 'none');
        $("#content_rsspost").css('display', 'block');


    }
}





function saveDrafts() {
    debugger;
    try {
        var msgid = $('#schedulemessage').attr('msgid');
        var message = $("#textareavaluetosendmessage_scheduler").val().trim();
        if (message == "") {
            alert('Please enter Message');
            return false;
        }
        if (msgid !== undefined) {
            editDraftsMessg(msgid, message);
            $('#schedulemessage').removeAttr('msgid');
            $('#textareavaluetosendmessage_scheduler').val('');
            alertify.success("Updated Successfully");
        }
        else {
            if (message != '') {
                debugger;
                $.ajax
        ({
            type: "POST",
            url: "../AjaxHome.aspx?op=savedrafts&message=" + message,
            data: 'messagee=' + encodeURIComponent(message),
            dataType: "html",
            success: function (msg) {
                debugger;
                $("#timepickerforScheduler").val('');
                $("#adddates_scheduler").html('');
                closeonCompose();
                alertify.success("Message Saved Successfully in Draft");
            }
        });

            }
            else {
                debugger;
                $("#messageCount_scheduler").css('color', 'pink');
                $("#messageCount_scheduler").html('Please Enter Message To Save Drafts');
            }

        }
    } catch (e) {

    }
}
















function writemessage(message) {
    debugger;
    $("#content").css('display', 'block');
    $("#content_wooqueue").css('display', 'none');
    $("#content_drafts").css('display', 'none');
    $("#content_rsspost").css('display', 'none');
    $("#textareavaluetosendmessage_scheduler").val(message);
}



function val_url(url) {
    debugger;
    try {
        var urlPattern = /^(http:\/\/|https:\/\/|ftp:\/\/|){1}([0-9A-Za-z]+\.)/;

        if (!urlPattern.test(url)) {
            $('.error_section').css('display', 'block');
            return false;
        }
        else {
            $('.error_section').css('display', 'none');
            $.ajax({
                url: '../Helper/AjaxHelper.aspx?op=chkrssurl&url=' + url,
                type: 'POST',
                dataType: 'html',
                success: function (msg) {
                    if (msg == "true") {
                        $("#rssfeedsurlerror").html('<img src="../Contents/img/msg/correct.png" alt="" />');
                        $("#rssfeedsurlerror").css('display', 'block');
                        $("#saveRssFeeds").on('click', saveRss);
                        $("#saveRssFeeds").removeClass('inactive');
                        $("#saveRssFeeds").addClass('active');
                    } else {
                        $("#rssfeedsurlerror").html('<img src="../Contents/img/msg/wrong.png" alt="" />');
                        $("#rssfeedsurlerror").css('display', 'block');
                    }
                }

            });




            return true;
        }
    } catch (e) {

    }
}





function saveRss() {
    try {
        debugger;
        var user = $("#rss_users").val();
        var feedsurl = $("#rssfeedurl").val();
        var message = $("#rssmessage").val();
        var duration = $("#rssduration").val();

        if (user != '' && feedsurl != '' && duration != '') {
            $.ajax({
                url: '../Helper/AjaxHelper.aspx?op=saveRss&message=' + message + '&feedsurl=' + feedsurl + '&duration=' + duration + '&user=' + user,
                type: 'POST',
                data: '',
                success: function (msg) {
                    alertify.success("Added Successfully");
                    $('.rssfeed').bPopup().close();
                    $("#rss").css('display') = 'block';
                    $("#rdata").css('display') = 'none';



                }



            });

        }
    } catch (e) {

    }

}

function pauseFunction(id) {
    debugger;
    try {

        if ($("#pause_" + id).hasClass('small_pause')) {
            $("#pause_" + id).removeClass('small_pause');
            $("#pause_" + id).addClass('small_play');

            $.ajax({
                url: '../Helper/AjaxHelper.aspx?op=pauseRssMessage&id=' + id,
                type: 'POST',
                data: '',
                success: function (msg) { }
            });
        }
        else {

            playRssFunction(id);

        }


    } catch (e) {

    }
}


function deleteRssFunction(id) {
    debugger;
    try {
        $.ajax({
            url: '../Helper/AjaxHelper.aspx?op=deleteRssMessage&id=' + id,
            type: 'POST',
            data: '',
            success: function (msg) {

                publishcontent("rsspost");
            }
        });
    } catch (e) {

    }
}

function playRssFunction(id) {

    try {
        $("#pause_" + id).addClass('small_pause');
        $("#pause_" + id).removeClass('small_play');


        $.ajax({
            url: '../Helper/AjaxHelper.aspx?op=playRssMessage&id=' + id,
            type: 'POST',
            data: '',
            success: function (msg) { }
        });
    } catch (e) {

    }


}


function editWooQueue(id) {
    debugger;


    try {
        var messme = $("#woomsg_" + id).html();
        var proifielid = $("#img_" + id).attr('alt');
        var imgsrc = $("#img_" + id).attr('src');
        $("#forid").html(id);
        $("#profileidwithtype").html(proifielid);


        $.ajax
        ({
            type: "GET",
            url: "../AjaxHome.aspx?op=MasterCompose",
            data: '',
            contentType: "application/json; charset=utf-8",
            dataType: "html",
            success: function (msg) {
                var addmsg = msg.replace(/composemessage/g, "addprofilesforwoomsg");

                $("#loginBox_Woo").html(addmsg);

            }
        });
        var ne = proifielid.split('_');
        if (ne[0] == 'fb') {
            $("#socialIcon_Woo").attr('src', '../Contents/img/facebook.png');
        } else if (ne[0] == 'twt') {
            $("#socialIcon_Woo").attr('src', '../Contents/img/twitter.png');
        } else if (ne[0] == 'lin') {
            $("#socialIcon_Woo").attr('src', '../Contents/img/linkedin.png');
        }
        $("#imageofuser_Woo").attr('src', imgsrc);
        $("#textareavaluetosendmessage_Woo").val(messme);
        $("#woopopup").bPopup();

    } catch (e) {

    }
    //    $.ajax({
    //        url: '../Helper/AjaxHelper.aspx?op=editWooQueue&id='+id,
    //        dataType: "html",
    //        type: "GET",
    //        success: function (msg) { 
    //        
    //        }

    //    });




}

function saveWooQueue() {
    try {

        debugger;
        var prof = $("#profileidwithtype").html();
        var id = $("#forid").html();
        var network = $("#profiletypeforwoo").html();
        var messagewoo = $("#textareavaluetosendmessage_Woo").val()
        // var datawith = prof.split('_');
        $.ajax({
            url: '../Helper/AjaxHelper.aspx?op=saveWooQueue&id=' + id + '&profid=' + prof + '&message=' + messagewoo + '&network=' + network,
            dataType: "html",
            type: "GET",
            success: function (msg) {
                $("#woopopup").bPopup().close();
                alertify.success("Updated Successfully");
                publishcontent("wooqueue");

            }

        });

    } catch (e) {

    }


}


function addprofilesforwoomsg(id, network) {
    debugger;
    try {
        var ids = id.split('_');
        $("#profiletypeforwoo").html(network);
        $("#profileidwithtype").html(ids[1]);

        var u = $("#imgurl_" + ids[1]).html();

        if (u != '') {
            $("#imageofuser_Woo").attr('src', u);
        } else {
            $("#imageofuser_Woo").attr('src', '../Contents/img/blank_img.png');
        }

        if (network == 'fb') {
            $("#socialIcon_Woo").attr('src', '../Contents/img/facebook.png');

        } else if (network == 'twt') {
            $("#socialIcon_Woo").attr('src', '../Contents/img/twitter.png');
        } else if (network == 'lin') {
            $("#socialIcon_Woo").attr('src', '../Contents/img/link.png');
        }

    } catch (e) {
        debugger;
    }


}



function deleteDraftMessage(id) {

    try {
        $.ajax({
            url: '../Helper/AjaxHelper.aspx?op=deletedrafts&id=' + id,
            type: "GET",
            dataType: "html",
            success: function (msg) {
                alertify.success("Deleted Successfully");
                publishcontent("drafts");
            }


        });
    } catch (e) {

    }
}


function deleteDraftMsg(id) {

    try {
        $.ajax({
            url: '../Helper/AjaxHelper.aspx?op=deletedrafts&id=' + id,
            type: "GET",
            dataType: "html",
            success: function (msg) {
            }


        });
    } catch (e) {

    }
}






function editDraftsMessage(id, msg) {
    publishcontent("schedulemessage",msg,id).click();
    alert('adasdasd');
}

    function editDraftsMessage1(id,msg) {
    try {
        debugger;

        //var defautl = $("#message_" + id).html();
        var defautl = $("#message_" + id).html(msg);

        var msgg = msg;

        alertify.prompt("Please Enter New Message", function (e, str) {

            if (e) {
                //alert(parseInt(str.length));
                if (str == "") {
                    alert('Please enter The Message, you cant leave Empty Message box..!!');
                    editDraftsMessage(id, msg);
                    // return;
                }
                else if (parseInt(str.length) > 140) {
                    alert('Message length should not be greater than 140 characters');
                    editDraftsMessage(id, str);
                }
                else {


                    $.ajax({
                        url: '../Helper/AjaxHelper.aspx?op=savedrafts&id=' + id + '&newstr=' + str,
                        type: 'GET',
                        dataType: 'html',
                        success: function (mg) {

                            alertify.success("Updated Successfully");
                            publishcontent("drafts");
                        }

                    });
                }

            } else {

            }
        }, msgg);
    } catch (e) {

    }

}



function editDraftsMessg(id, msg) {
    try {
        debugger;
        //var defautl = $("#message_" + id).html();
        var defautl = $("#message_" + id).html(msg);
        var msgg = msg;
            $.ajax({
                url: '../Helper/AjaxHelper.aspx?op=savedrafts&id=' + id + '&newstr=' + msg,
                type: 'GET',
                dataType: 'html',
                success: function (mg) {
                }

            });
   


    } catch (e) {

    }

}
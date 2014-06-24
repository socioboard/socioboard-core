var bindmessageajax = '';
var bindprofilesajax = '';
var bindSentmessagesajax = '';


function BindMessages() {

    debugger;
    try {
        bindSentmessagesajax.abort();
    } catch (e) {

    }
    bindmessageajax = $.ajax
        ({
            type: "POST",
            url: "../Message/AjaxMessage.aspx?op=bindMessages",
            data: '',
            contentType: "application/json; charset=utf-8",
            dataType: "html",
            success: function (msg) {
                try {
                    if (msg.toString().contains("logout")) {
                        //                        alert("logout");
                        window.location = "../Default.aspx?hint=logout";
                        return false;
                    }
                }
                catch (e) {
                }  
                $("#message").html(msg);
           //     $(".home_loader").hide();

                UpdateReadStatus();
            }
        });


}


function BindArchiveMessages() {

    //        try {
    //            bindSentmessagesajax.abort();
    //        } catch (e) {

    //        }
    $("#sent_messages").removeClass('active');
    $("#smart_inbox").removeClass('active');
    $("#archive_message").addClass('active');

    bindmessageajax = $.ajax
        ({
            type: "POST",
            url: "../Message/AjaxMessage.aspx?op=bindarchive",
            data: '',
            contentType: "application/json; charset=utf-8",
            dataType: "html",
            success: function (msg) {
                try {
                    if (msg.toString().contains("logout")) {
                        //                        alert("logout");
                        window.location = "../Default.aspx?hint=logout";
                        return false;
                    }
                }
                catch (e) {
                }  
                $("#inbox_msgs").html(msg);
                $("#home_loader").hide();

                //  UpdateReadStatus();


            }
        });
}

function savearchivemsg(id, sociotype, messageId, profileId) {

    try {

        debugger;

        var imgurl = document.getElementById('formprofileurl_' + id).src;
        var username = document.getElementById('rowname_' + id).innerHTML;
        var message = document.getElementById('msgdescription_' + id).innerHTML;
      
      
        var network = sociotype;
        var datetime = document.getElementById('createdtime_' + id).innerHTML;

        var jstring = "{Network:'" + network + "',CreatedTime:'" + datetime + "',UserName:'" + username + "'}";
        $.ajax({
            type: "POST",
            url: "../Message/AjaxMessage.aspx?op=savearchivemsg&imgurl=" + imgurl + "&Network=" + network + "&CreatedTime=" + datetime + "&ProfileId=" + profileId + "&MessageId=" + messageId + "&Msg=" + message,
            data: jstring,
            contentType: "application/json; charset=utf-8",
            dataType: "html",
            success: function (msg) {
                try {
                    if (msg.toString().contains("logout")) {
                        //                        alert("logout");
                        window.location = "../Default.aspx?hint=logout";
                        return false;
                    }
                }
                catch (e) {
                }  
                alertify.success("Message Archieved Successfully");
                //                $("#accountsins").html(msg);

            }
        });

    } catch (e) {

    }


}

function replyfunction(id, profiletype, messageid, userid) {

    debugger;
    try {
        $.session('mess_id', id + ',' + profiletype + ',' + messageid + ',' + userid);
        if (id != $.session('mess_id')) {
            $.session('mess_id', id + ',' + profiletype + ',' + messageid + ',' + userid);
        }
        // document.getElementById('replytext').value = '';
        try {
            var messageid = document.getElementById("messageid_" + id).innerHTML;

        } catch (e) {

        }

        try {
            var name = document.getElementById("rowname_" + id).innerHTML;

        } catch (e) {

        }

        try {
            var messagedescription = document.getElementById("messagedescription_" + id).innerHTML;

        } catch (e) {

        }

        try {
            var msg = document.getElementById('messagetaskable' + id).innerHTML;

        } catch (e) {

        }

        $("#replyMessages").html = "";
        $("#replyMessages").html(messagedescription);

        try {
            var msgid = document.getElementById('messageid_' + id).innerHTML;
            var network = document.getElementById('network_' + id).innerHTML;
        }
        catch (e) {
        }


        try {
            var userid = document.getElementById('rowid_' + id).innerHTML;



        } catch (e) {
      //  alert(e);
        }


        //        $.ajax
        //            ({
        //                type: "POST",
        //                url: "../Messages/AjaxMessage.aspx?op=getFacebookComments&postid=" + msgid,
        //                data: '',
        //                contentType: "application/json; charset=utf-8",
        //                dataType: "html",
        //                success: function (msg) {
        //                    // $("#inbox_msgs").html(msg);
        //                }
        //            });



        $('#replysection').bPopup({
            fadeSpeed: 'slow',
            followSpeed: 1500,
            modalColor: 'black'
        });

        if (network == 'facebook') {
            try {

                $.ajax
                         ({
                             type: "POST",
                             url: "../Messages/AjaxMessage.aspx?op=getFacebookComments&postid=" + msgid + "&userid=" + userid,
                             data: '',
                             contentType: "application/json; charset=utf-8",
                             dataType: "html",
                             success: function (msg) {
                                 try {
                                     if (msg.toString().contains("logout")) {
                                         //                        alert("logout");
                                         window.location = "../Default.aspx?hint=logout";
                                         return false;
                                     }
                                 }
                                 catch (e) {
                                 }  
                                 debugger;
                                 if (msg == '') {
                                     $("#replycomments").html('No Comments are available');
                                 } else {
                                     $("#replycomments").html(msg);
                                 }
                             },
                             error: function (ee)
                             { }
                         });

            } catch (e) {

            }
        } else if (network == 'twitter') {

            $("#replycomments").html('No Comments are available');
        }

    } catch (e) {

    alert(e);
    }

}

function twittercomments() {
    alert("twittercomments");
    debugger;
    var replytext = document.getElementById('Textarea1').value;
    var commentvl = $.session('mess_id').split(',');
    var mess_id = commentvl[0];
    var replyid = commentvl[2];
    var userid = commentvl[3];
    var username = document.getElementById('rowname_' + mess_id).innerHTML;
    var network = commentvl[1];

    //   var rowid = document.getElementById('rowid_' + mess_id).innerHTML;
    //   $.ajax
    //            ({
    //                type: "POST",
    //                url: "https://api.twitter.com/1/statuses/update.json",
    //                data: { in_reply_to_status_id: replyid, status: replytext ,screen_name='yashwant05'},
    //                crossDomain: true,
    //                contentType: "application/json; charset=utf-8",
    //                dataType: "jsonp",
    //                success: function (msg) 
    //                {
    //                    debugger;
    //                },
    //                error: function (e)
    //                {
    //                    alert(e);
    //                }
    //            });

    if (network == 'twitter') {
        $.ajax
            ({
                type: "POST",
                url: "../Message/AjaxMessage.aspx?op=twittercomments&messid=" + mess_id + "&replytext=" + replytext + "&replyid=" + replyid + "&userid=" + userid + "&username=" + username,
                data: '',
                contentType: "application/json; charset=utf-8",
                dataType: "html",
                success: function (msg) {
                    try {
                        if (msg.toString().contains("logout")) {
                            //                        alert("logout");
                            window.location = "../Default.aspx?hint=logout";
                            return false;
                        }
                    }
                    catch (e) {
                    }  
                    document.getElementById('Textarea1').value = 'Commented successfully';
                    $('#replysection').bPopup().close();
                    closeonCompose();
                }
            });
    } else if (network == 'facebook') {

        $.ajax
            ({
                type: "POST",
                url: "../Message/AjaxMessage.aspx?op=createfacebookcomments&messid=" + mess_id + "&replytext=" + replytext + "&replyid=" + replyid + "&userid=" + userid + "&username=" + username,
                data: '',
                contentType: "application/json; charset=utf-8",
                dataType: "html",
                success: function (msg) {
                    try {
                        if (msg.toString().contains("logout")) {
                            //                        alert("logout");
                            window.location = "../Default.aspx?hint=logout";
                            return false;
                        }
                    }
                    catch (e) {
                    }  
                    document.getElementById('Textarea1').value = 'Commented successfully';
                    $("#replysection").hide();
                    $(".__b-popup1__").hide();
                    // closeonCompose();
                    $.ajax
                         ({
                             type: "POST",
                             url: "../Message/AjaxMessage.aspx?op=getFacebookComments&postid=" + replyid + "&userid=" + userid,
                             data: '',
                             contentType: "application/json; charset=utf-8",
                             dataType: "html",
                             success: function (msg) {
                                 try {
                                     if (msg.toString().contains("logout")) {
                                         //                        alert("logout");
                                         window.location = "../Default.aspx?hint=logout";
                                         return false;
                                     }
                                 }
                                 catch (e) {
                                 }  
                                 debugger;
                                 if (msg == '') {
                                     $("#replycomments").html('No Comments are available');
                                 } else {
                                     $("#replycomments").html(msg);
                                 }
                             },
                             error: function (ee)
                             { }
                         });
                },
                error: function (ee) {
                    console.log(ee);
                }
            });




    }


}


function UpdateReadStatus() {

    $.ajax
        ({
            type: "POST",
            url: "../Message/AjaxMessage.aspx?op=updatedstatus",
            data: '',
            contentType: "application/json; charset=utf-8",
            dataType: "html",
            success: function (msg) {
                try {
                    if (msg.toString().contains("logout")) {
                        //                        alert("logout");
                        window.location = "../Default.aspx?hint=logout";
                        return false;
                    }
                }
                catch (e) {
                }  

            }
        });

}


function BindInboxMessageonMessageTab() {
    debugger;


    try {
        $("#sent_messages").addClass('active');
    } catch (e) {

    }
    try {
        $("#task_href").removeClass('active');
    } catch (e) {

    }
    try {
        $("#smart_inbox").removeClass('active');
    } catch (e) {

    }
    // $("#inbox_msgs").html('<img src="../Contents/img/328.gif" alt="" height="50" width="50" style="margin-left: 350px;" />');

    $("#home_loader").show();
    try {
        $.ajax
        ({
            type: "POST",
            url: "../Message/AjaxMessage.aspx?op=inbox_messages",
            data: '',
            contentType: "application/json; charset=utf-8",
            dataType: "html",
            success: function (msg) {
                try {
                    if (msg.toString().contains("logout")) {
                        //                        alert("logout");
                        window.location = "../Default.aspx?hint=logout";
                        return false;
                    }
                }
                catch (e) {
                }  
                $("#inbox_msgs").html(msg);
                $("#home_loader").hide();
            }
        });
    } catch (e) {
        debugger;

    }
}


function BindProfilesInMessageTab() {
   // alert("BindProfilesInMessageTab");
    bindprofilesajax = $.ajax
        ({
            type: "POST",
            url: "../Message/AjaxMessage.aspx?op=bindProfiles",
            data: '',
            contentType: "application/json; charset=utf-8",
            dataType: "html",
            success: function (msg) {
                try {
                    if (msg.toString().contains("logout")) {
                        //                        alert("logout");
                        window.location = "../Default.aspx?hint=logout";
                        return false;
                    }
                }
                catch (e) {
                }  
                //alert("success");
                $("#messageProfiles").html(msg);

                if (msg == "No Records Found") {
                    $("#message").html('');
                } else {
                    BindMessages();
                }

            },
            error: function () {
                alert("Failure !");
            }
        });
    }



    function deleteWooQueueMessage(id) {
        try {

            $.ajax({
                type: "GET",
                url: "../Helper/AjaxHelper.aspx?op=deletewooqueuemessage&id=" + id,
                dataType: "html",
                success: function (e) {
                    try {
                        if (msg.toString().contains("logout")) {
                            //                        alert("logout");
                            window.location = "../Default.aspx?hint=logout";
                            return false;
                        }
                    }
                    catch (e) {
                    }  
                    alertify.success("Deleted Successfully");
                    publishcontent("wooqueue");
                }

            });


        } catch (e) {

        }
    }



    function BindTasks() {
        $.ajax
        ({
            type: "POST",
            url: "../Message/AjaxMessage.aspx?op=bindtasks",
            data: '',
            contentType: "application/json; charset=utf-8",
            dataType: "html",
            success: function (msg) {
                try {
                    if (msg.toString().contains("logout")) {
                        //                        alert("logout");
                        window.location = "../Default.aspx?hint=logout";
                        return false;
                    }
                }
                catch (e) {
                }  

                $("#bindtasks").html(msg);

               

            }
        });
    }


    function BindSentMessages() {
        $.ajax
        ({
            type: "POST",
            url: "../Message/AjaxMessage.aspx?op=inbox_messages",
            data: '',
            contentType: "application/json; charset=utf-8",
            dataType: "html",
            success: function (msg) {
                try {
                    if (msg.toString().contains("logout")) {
                        //                        alert("logout");
                        window.location = "../Default.aspx?hint=logout";
                        return false;
                    }
                }
                catch (e) {
                }  

                $("#bindsent").html(msg);



            }
        });
    }

    function BindArchieveMessage() {

        $.ajax
        ({
            type: "POST",
            url: "../Message/AjaxMessage.aspx?op=bindarchive",
            data: '',
            contentType: "application/json; charset=utf-8",
            dataType: "html",
            success: function (msg) {
                try {
                    if (msg.toString().contains("logout")) {
                        //                        alert("logout");
                        window.location = "../Default.aspx?hint=logout";
                        return false;
                    }
                }
                catch (e) {
                }  

                $("#bindarchive").html(msg);



            }
        });
    }

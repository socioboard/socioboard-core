var chkid = new Array();


//alert("helper");




/*display the selected user data which is checked*/
function checkprofile(tagid, id, page, network) {

    debugger;
    try {

        alert(tagid);
        $("#another-load").html('<img src="../Contents/img/360.gif" />');

        $('#accordianprofiles a').removeAttr('class');
        $('#' + tagid).attr('class', 'selected_Li');


        chkid = [];
        var chkcount = 0;
        var underscore = id.indexOf('_');
        var dd = id.substring(underscore + 1, id.length);
        var s = document.getElementById('checkimg_' + dd);


        //        /// chk box img
        //        if (s.src.indexOf("network_click") != -1) {
        //            s.src = s.src.replace("network_click", "Network_non_click");
        //        } else if (s.src.indexOf("Network_non_click") != -1) {
        //            s.src = s.src.replace("Network_non_click", "network_click");
        //        }



        /*return id which is uncheck*/

        //        var hdn = document.getElementById("fbhidden_" + dd);
        //        if (hdn != null) {
        //            var hdnval = hdn.value;
        //        } else {
        //            var usernameval = document.getElementById("profileusername_" + dd).innerHTML;
        //        }

        //        var profcount = document.getElementById("profilecounter").value;
        //        for (var i = 0; i < profcount; i++) {
        //            if (document.getElementById("checkimg_" + i).src.indexOf("network_click") != -1) {
        //                var hdn = document.getElementById("fbhidden_" + i);
        //                var hbn = document.getElementById("twthidden_" + i);

        //                if (hbn != null || hdn != null) {
        //                    if (hdn != null) {

        //                        chkid[chkcount] = hdn.value;
        //                    } else if (hbn != null) {
        //                        chkid[chkcount] = hbn.value;

        //                    }

        //                    chkcount++;
        //                }
        //            }
        //        }
        debugger;
        $("#home_loader").attr('src', '../Contents/img/328.gif');
        $.ajax
        ({
            type: "POST",
            url: "../Helper/AjaxHelper.aspx?op=removedata&data[]=" + id + "&page=" + page + "&network=" + network,
            data: '',
            contentType: "application/json; charset=utf-8",
            dataType: "html",
            success: function (msg) {
                debugger;
                var msgss = msg.replace(/Images/g, "img");
                $("#inbox_msgs").html(msgss);
                $("#another-load").html("");
                $("#home_loader").attr('src', '');
            }
        });



    }
    catch (e)
        { }
}







function getMemberId(memberId) {
    $("#<%=hdnMemberId.ClientID%>").val(memberId);
}

/*popup will display to save the task*/
function createtask(id) {

    debugger;
    var dd = id.indexOf('_');
    var name = '';
    try {
        var ss = id.substring(dd + 1, id.length);

        if (id.indexOf('createtasktwt_') != -1) {
            name = document.getElementById("rowname_" + ss).innerHTML;
        } else {
            name = document.getElementById(id).innerHTML;
        }
        var messagedescription = document.getElementById("messagedescription_" + ss).innerHTML;

        var msg = document.getElementById('messagetaskable_' + ss).innerHTML;

        $("#inbox_messages").html = "";

        if (messagedescription.indexOf("Contents/img/pin") != -1) {
            messagedescription = messagedescription.replace("../Contents/img/pin.png", "");
        }
        $.ajax
                ({
                    type: "POST",
                    url: "../Message/AjaxMessage.aspx?op=bindteam",
                    data: '',
                    contentType: "application/json; charset=utf-8",
                    dataType: "html",
                    success: function (msg) {
                        debugger;
                        $("#tasksteam").html(msg);
                        $("#another-load").html("");

                    }
                });
        $("#inbox_messages").html(msg + messagedescription);

        $('#popupchk').bPopup({
            fadeSpeed: 'slow', //can be a string ('slow'/'fast') or int
            followSpeed: 1500, //can be a string ('slow'/'fast') or int
            position: 'fixed',
            modalColor: 'black'
        });
    } catch (e)
        { }
}


/*save task into database*/
function savetask() {
    debugger;
    try {

        var msg = document.getElementById('inbox_messages').innerHTML;
        var dd = msg.indexOf('msgdescription_');
        var ss = msg.substring(dd + 15, dd + 17);
        var id = "";
        if (ss.indexOf("") == -1) {
            id = ss;
        } else {
            id = ss.substring(0, 1);

        }
        var name = document.getElementById("rowname_" + id).innerHTML;
        var desc = document.getElementById("msgdescription_" + id).innerHTML.indexOf('</p>');
        var description = document.getElementById("msgdescription_" + id).innerHTML.substring(3, desc);
        var comment = $("#txttaskcomment").val();
        var chk = beforeDelete();
        var dat = '';
        if (chk) {

            var chkboxid = $("input[type='radio']:checked").val();
            if (chkboxid.indexOf('customerid') != -1) {
                dat = chkboxid.split('_');
                chkboxid = '';
            }

            //  var memId = $("#<%=hdnMemberId.ClientID %>").val();
            $.ajax
            ({
                type: "POST",
                url: "../Message/AjaxMessage.aspx?op=savetask&description=" + description + "&memberid=" + dat[1] + "&comment=" + comment,
                data: '',
                contentType: "application/json; charset=utf-8",
                dataType: "html",
                success: function (msg) {
                    $("#popupchk").bPopup().close();
                }
            });
        } else {
            alert('Please select the user to assign task');
        }




    } catch (e) {
        //  alert(e);
    }

}

/*check whether any of input is checked or not*/
function beforeDelete() {
    if ($("input:checked").length == 0) {

        return false;
    }
    return true;
}


/*facebook profiles will display of user in popup window*/

function getFacebookProfiles(id) {
    try {
        debugger;
        var jas = '';
        $.ajax({
            type: "GET",
            url: '../Helper/AjaxHelper.aspx?op=facebookProfileDetails&profileid=' + id,
            data: '',
            dataType: 'html',
            success: function (msg) {
                $("#facebookuserDetails").html(msg);
                $("#facebookuserDetails").bPopup();
            }
        });
        //            $.ajax
        //                            ({
        //                                type: "GET",
        //                                url: "https://graph.facebook.com/" + id + "",
        //                                data: '',
        //                                crossDomain: true,
        //                                contentType: "application/json; charset=utf-8",
        //                                dataType: "jsonp",
        //                                success: function (data) {
        //                                    debugger;
        //                                    jas += '<div class="big-puff">';
        //                                    jas += '<article><dl>';
        //                                    jas += '<img src="http://graph.facebook.com/' + data.id + '/picture?type=small" alt="" class="photo">';
        //                                    jas += '<div class="descrption">';
        //                                    jas += '<h3 title="Carlos Ullon" class="fn">' + data.name + '<span class="screenname prof_meta">';
        //                                    jas += '<span class="ficon blue_bird_sm nickname"></span></span>';
        //                                    jas += '</h3>';
        //                                    jas += '</div></dl><section class="profile_sub_wrap" style=" margin-left:0;margin-top:0;">';
        //                                    jas += '<ul class="follow">';
        //                                    jas += '<li><span class="followers filter"><span style="width:54px;">Link</span>';
        //                                    jas += '<a data-msg_type="followers" target="_blank" href="' + data.link + '">' + data.link + '</a></span></li>'
        //                                     + '<li><span class="followers filter"><span style="width:100px;">UserName</span>'
        //                                    jas += '<a data-msg_type="followers" href="javascript:void(0)">@' + data.name + '</a></span></li>' +
        //                                    '<li><span class="followers filter"><span style="width:100px;">FirstName</span>';
        //                                    jas += '<a data-msg_type="followers" href="javascript:void(0)">' + data.first_name + '</a></span></li>' +
        //                                    '<li><span class="followers filter"><span style="width:100px;">LastName</span>';
        //                                    jas += '<a data-msg_type="followers" href="javascript:void(0)">' + data.last_name + '</a></span></li>' +
        //                                    '<li><span class="followers filter"><span style="width:100px;">Gender</span>';
        //                                    jas += '<a data-msg_type="followers" href="javascript:void(0)">' + data.gender + '</a></span></li>';
        //                                    jas += '</ul></section></article>';
        //                                    jas += '</div></div>';
        //                                    $('#detailsfacebook').html(jas);
        //                                    $('#facebookuserDetails').bPopup();

        //                                },
        //                                error: function (e) {

        //                                }
        //                            });
    } catch (e) {
        //  alert(e);
    }
}

function getGooglePlusProfiles(id) {
    try {
        debugger;
        var jas = '';
        $.ajax
                            ({
                                type: "GET",
                                url: "../Message/AjaxMessage.aspx?op=gpProfile&gpid=" + id + "",
                                data: '',
                                crossDomain: true,
                                contentType: "application/json; charset=utf-8",
                                dataType: "html",
                                success: function (data) {
                                    debugger;

                                    $('#detailsfacebook').html(data);
                                    $('#facebookuserDetails').bPopup();

                                },
                                error: function (e) {

                                }
                            });
    } catch (e) {
        //  alert(e);
    }
}

/*display the user information*/
function detailsprofile(id) {
    try {
        debugger;
        $("#another-load").html('<img src="../Contents/img/360.gif" />');
        debugger;
        var sd = '';
        var msgname = '';
        //            if (id.indexOf('rowname_') != -1) {
        //                sd = document.getElementById(id).innerHTML;
        //            } else {
        //                sd = id;
        //            }

        $.ajax
        ({
            type: "GET",
            url: "../Helper/AjaxHelper.aspx?op=getTwitterUserDetails&profileid=" + id,
            crossDomain: true,
            contentType: "application/json; charset=utf-8",
            dataType: "html",
            success: function (msg) {
                debugger;
                $("#details").html(msg);
                $("#another-load").html("");
                $("#profile_popup").bPopup();

                $.ajax
        ({
            type: "GET",
            url: "../Helper/AjaxHelper.aspx?op=getTwitterUserTweets&profileid=" + id,
            crossDomain: true,
            contentType: "application/json; charset=utf-8",
            dataType: "html",
            success: function (msg) {
            }
        });


            }

        });
    }
    catch (e) {
        //    alert("catch " + e);
    }

}





/*display the Discovery detail information for twitter*/
function detailsdiscoverytwitter(id) {
    try {
        debugger;
        $("#another-load").html('<img src="../Contents/img/360.gif" />');
        debugger;
        var sd = '';
        var msgname = '';
        //            if (id.indexOf('rowname_') != -1) {
        //                sd = document.getElementById(id).innerHTML;
        //            } else {
        //                sd = id;
        //            }

        $.ajax
        ({
            type: "GET",
            url: "../Helper/AjaxHelper.aspx?op=detailsdiscoverytwitter&profileid=" + id,
            crossDomain: true,
            contentType: "application/json; charset=utf-8",
            dataType: "html",
            success: function (msg) {
                debugger;
                //alert('alert');
                //alert(msg);
                $("#facebookuserDetails").html(msg);
                $("#facebookuserDetails").bPopup();
                //                                $("#details").html(msg);
                //                                $("#another-load").html("");
                //                                $("#profile_popup").bPopup();
            },
            error: function (err) {
                alert(err);
                debugger;
            }
        });
    } catch (e) {
        //  alert(e);
    }
}





/*display the Discovery detail information for facebook*/
function detailsdiscoveryfacebook(id) {
    try {
        debugger;
        $("#another-load").html('<img src="../Contents/img/360.gif" />');
        debugger;
        var sd = '';
        var msgname = '';
        //            if (id.indexOf('rowname_') != -1) {
        //                sd = document.getElementById(id).innerHTML;
        //            } else {
        //                sd = id;
        //            }

        $.ajax
        ({
            type: "GET",
            url: "../Helper/AjaxHelper.aspx?op=detailsdiscoveryfacebook&profileid=" + id,
            crossDomain: true,
            contentType: "application/json; charset=utf-8",
            dataType: "html",
            success: function (msg) {
                debugger;
                //alert('alert');
                //alert(msg);
                $("#facebookuserDetails").html(msg);
                $("#facebookuserDetails").bPopup();
                //                                $("#details").html(msg);
                //                                $("#another-load").html("");
                //                                $("#profile_popup").bPopup();
            },
            error: function (e) {

            }
        });
    } catch (e) {
        //  alert(e);
    }
}




















/*************************************************************************************************************/
function GetSearchedKeyword() {
    $.ajax
            ({
                type: "POST",
                url: "../Helper/AjaxHelper.aspx?op=searchkeyword",
                data: '',
                contentType: "application/json; charset=utf-8",
                dataType: "html",
                success: function (msg) {
                    $("#searchkeyword").html(msg);
                }
            });
}

function getSearchResults(key) {
    $("#DivAll").html('');
    debugger;
    try {
        var e = document.getElementById("searchfilteration");
        var strUser = e.options[e.selectedIndex].text;

    } catch (e) {

        debugger;
    }

    if (strUser == undefined) {
        strUser = "All";
    } else if (strUser == '') {
        strUser = "All";
    }
    $.ajax
            ({
                type: "POST",
                url: "../Helper/AjaxHelper.aspx?op=getResults&keyword=" + key + "&type=" + strUser,
                data: '',
                contentType: "application/json; charset=utf-8",
                dataType: "html",
                success: function (msg) {
                    $("#DivAll").addClass('messages');
                    $("#DivAll").html(msg);

                }
            });
}


var jqueryAjaxMessage = '';



/******************************************/

function chkMessage(id) {
    debugger;
    try {



        var messageArray = new Array();
        //                document.getElementById(id).src = '../Contents/img/uncheck_click.png';

        if (document.getElementById(id).src.indexOf('uncheck_click') != -1) {
            document.getElementById(id).src = "../Contents/img/check_click.png";
        } else {
            document.getElementById(id).src = "../Contents/img/uncheck_click.png";
        }


        if (document.getElementById('check_mentions').src.indexOf('uncheck_click') == -1) {
            messageArray.push('twt_mentions');
        }
        if (document.getElementById('check_Retweets').src.indexOf('uncheck_click') == -1) {
            messageArray.push('twt_usertweets');
        }

        //                if (document.getElementById('check_Activities').src.indexOf('uncheck_click') == -1) {
        //                    messageArray.push('activities');
        //                }

        if (document.getElementById('check_WallPosts').src.indexOf('uncheck_click') == -1) {
            messageArray.push('fb_feed');
        }


        try {
            jqueryAjaxMessage.abort();
        } catch (e) {

        }


        try {
            jqueryAjaxMessage = $.ajax({
                url: '../AjaxHome.aspx?op=messagechk&type[]=' + messageArray,
                type: 'POST',
                data: '',
                success: function (msg) {
                    debugger;
                    $("#message-list").html(msg);
                },
                error: function (err) {
                    alert(err);
                }
            });
        } catch (e) {

        }

    } catch (e) {

    }
}














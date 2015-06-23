var chkid = new Array();

document.writeln("<script type='text/javascript' src='/Themes/Socioboard/Contents/js/reloadfbgraph.js'></script>");
//alert("helper");


var fbAgeArr;
var fbImpArr;
var fbLocArr;
var fbstory;

var likeunlikedt;
var pageimpression;
var PgId;
var NumberOfDays = 15;
var nm;
var Img;
var Acces;





/*display the selected user data which is checked*/
function checkprofile(tagid, id, page, network) {

    debugger;
    try {

        //alert(tagid);
        $("#another-load").html('<img src="/Themes/Socioboard/Contents/img/360.gif" />');

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
        if (messagedescription.indexOf("Contents/img/admin/goto.png") != -1) {
            messagedescription = messagedescription.replace("../Contents/img/admin/goto.png", "");
        }
        if (messagedescription.indexOf("Contents/img/archive.png") != -1) {
            messagedescription = messagedescription.replace("../Contents/img/archive.png", "");
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
//function savetask() {
//    debugger;
//    try {

//        var msg = document.getElementById('inbox_messages').innerHTML;
//        var dd = msg.indexOf('msgdescription_');
//        var ss = msg.substring(dd + 15, dd + 17);
//        var id = "";
//        if (ss.indexOf("") == -1) {
//            id = ss;
//        } else {
//            id = ss.substring(0, 1);

//        }
//        var name = document.getElementById("rowname_" + id).innerHTML;
//        var desc = document.getElementById("msgdescription_" + id).innerHTML.indexOf('</p>');
//        var description = document.getElementById("msgdescription_" + id).innerHTML.substring(3, desc);
//        var comment = $("#txttaskcomment").val();
//        var chk = beforeDelete();
//        var dat = '';
//        if (chk) {

//            var chkboxid = $("input[type='radio']:checked").val();
//            if (chkboxid.indexOf('customerid') != -1) {
//                dat = chkboxid.split('_');
//                chkboxid = '';
//            }

//            if (comment == "" || comment == null) {
//                alert("Please write comment then click save!")
//                return false;
//            }


//            //  var memId = $("#<%=hdnMemberId.ClientID %>").val();
//            $.ajax
//            ({
//                type: "POST",
//                url: "../Message/AjaxMessage.aspx?op=savetask&description=" + description + "&memberid=" + dat[1] + "&comment=" + comment,
//                data: '',
//                contentType: "application/json; charset=utf-8",
//                dataType: "html",
//                success: function (msg) {
//                    $("#popupchk").bPopup().close();
//                    $("#txttaskcomment").val() = "";
//                    $("input[type='radio']:checked").val() = "";
//                }
//            });
//        } else {
//            alert('Please select the user to assign task');
//        }
function savetask() {
    debugger;
    var divTask = document.getElementById('savetask');
    var description = $('#savetask').attr('msgdesc');
    var msgdate = $('#savetask').attr('msgdate');
    var msgdata = msgdate.split("/");
    var time
    var mm = msgdata[0];
    var dd = msgdata[1];
    var yy=msgdata[2]
    if (mm < 10) {
        if (mm.indexOf('0') == -1) {
            mm = '0' + msgdata[0]
        } else {
            mm=msgdata[0];
        }
    }
    else {
        mm = msgdata[0];
    }
    if (dd < 10) {
        if (dd.indexOf('0') == -1) {
            dd = '0' + dd;
        } else {
            dd = msgdata[1];
        }
    }
    else {
        dd = msgdata[1];
    }
    var yeartime = yy.split(" ");
    var yy = yeartime[0];
    var tt = yeartime[1].split(":")
    var hh = tt[0];
    var min = tt[1];
    var sec = tt[2];
    if (hh < 10)
    {
        if (hh.indexOf('0') == -1) {
            hh = '0' + hh;
        } else {
            hh = tt[0];
        }
    }
    else {
        hh = tt[0];
    }
    if (min < 10) {
        if (min.indexOf('0') == -1) {
            min = '0' + min;
        } else {
            min=tt[1];
        }
    }
    else {
        min = tt[1];
    }
    if (sec<10 && sec!='00') {
        if (sec.indexOf('0') == -1) {
            sec = '0' + sec;
        } else {
            sec = tt[2];
        }
    }
    else {
        sec = tt[2];
    }
    msgdate = yy + "/" + mm + "/" + dd + " " + hh + ":" + min + ":" + sec + " " + yeartime[2];
    var comment = $("#txtsavetask").val();
    if (comment == "" || comment == null) {
        alert("Please write comment then click save!")
        return;
    }
    var chk = beforeDelete();
    var dat = '';
    var chkboxid = $("input[type='radio']:checked").val();
    if (chkboxid != undefined) {
        if (chkboxid.indexOf('customerid') != -1) {
            dat = chkboxid.split('_');
            chkboxid = '';
        }
        $.ajax
               ({
                   type: "POST",
                   url: "../Messages/savetask?description=" + encodeURIComponent(description) + "&memberid=" + dat[1] + "&comment=" + encodeURIComponent(comment) + "&msgdate=" + msgdate,
                   data: '',
                   contentType: "application/json; charset=utf-8",
                   dataType: "html",
                   success: function (msg) {
                       $("#task").modal("hide");
                       $("#txtsavetask").val("");
                       $("input[type='radio']:checked").prop('checked', false);
                   }
               });

    } else {
        alert('Please select the user to assign task');
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

                //                $("#facebookuserDetails").html(msg);
                //                $("#facebookuserDetails").bPopup();

                //                $.ajax
                //        ({
                //            type: "GET",
                //            url: "../Helper/AjaxHelper.aspx?op=getTwitterUserTweets&profileid=" + id,
                //            crossDomain: true,
                //            contentType: "application/json; charset=utf-8",
                //            dataType: "html",
                //            success: function (msg) {
                //                $("#facebookuserDetails").html(msg);
                //                $("#facebookuserDetails").bPopup();
                //            }
                //        });


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
                    //alert(err);
                }
            });
        } catch (e) {

        }

    } catch (e) {

    }
}



/******************************************/


function getProfilefbGraph(id, name, img, access) {
    PgId = id;
    Img = img;
    Acces = access;
    nm = name;
    debugger;
    $.ajax
                        ({
                            type: "GET",
                            //url: "AjaxReport.aspx?op=facebook&id=" + id+ "&access=" + access+ "&NumberOfDays=" + NumberOfDays,
                            url: "AjaxReport.aspx?op=facebook&id=" + id + "&access=" + access,
                            data: '',
                            contentType: "application/text; charset=utf-8",
                            success: function (msg) {
                                debugger;
                                //  alert(msg);
                                //var c=document.getElementById("cvs");
                                //var ctx=c.getContext("2d");
                                //ctx.clearRect(0,0,739,100);
                                var fbData = msg.split("_");
                                fbAgeArr = JSON.parse(fbData[0]);

                                debugger;
                                fbImpArr = fbData[1].split("@");
                                fbLocArr = fbData[2].split("@");
                                var ImAgE = fbData[6];
                                likeunlikedt = fbData[5].split("@");
                                fbstory = fbData[7].split("@");



                                $('#ContentPlaceHolder1_divPageName').html(name);
                                $('#ContentPlaceHolder1_divPageLikes').html(fbData[3]);
                                $('#ContentPlaceHolder1_fbProfileImg').attr('src', ImAgE);
                                $('#ContentPlaceHolder1_spanTalking').html(fbData[4]);
                                $('#ContentPlaceHolder1_divpost').html(fbData[8]);


                                reloadfbGraph(likeunlikedt);
                                reloadimpresGraph(fbImpArr);
                                reloadStoriesGraph(fbstory);
                                // alert(twtArr);
                            }

                        });
}


function addTaskComment(taskid) {
    //var taskid=taskid;
    debugger;
    var comment = $("#txtTaskComment_" + taskid).val();
    var curdate = new Date();
    var dd = curdate.getDate();
    if (dd < 10) {
        dd = '0' + dd
    }
    else {
        dd = curdate.getDate();
    }
    var mm = curdate.getMonth() + 1;
    if (mm < 10) {
        mm = '0' + mm
    }
    else {
        mm = curdate.getMonth() + 1;
    }
    var hh = curdate.getHours();
    if (hh < 10) {
        hh = '0' + hh;
    }
    else {
        hh = curdate.getHours();
    }
    var minm = curdate.getMinutes();
    if (minm < 10) {
        minm = '0' + minm;
    }
    else {
        minm = curdate.getMinutes();
    }
    var sec = curdate.getSeconds();
    if (sec < 10) {
        sec = '0' + sec;
    }
    else {
        sec = curdate.getSeconds();
    }

    //var CommentDateTime = (curdate.getMonth() + 1) + "/" + curdate.getDate() + "/" + curdate.getFullYear() + " " + curdate.getHours() + ":" + curdate.getMinutes() + ":" + (curdate.getSeconds() < 10 ? '0' + curdate.getSeconds() : curdate.getSeconds());
    //Pad given value to the left with "0"
    var CommentDateTime = curdate.getFullYear() + "/" + mm + "/" + dd + " " + hh + ":" + minm + ":" + sec;
    $.ajax
          ({
              type: "POST",
              url: "../Messages/addTaskComment?taskid=" + taskid + "&comment=" + comment + "&CommentDateTime=" + CommentDateTime,
              data: '',
              //contentType: "application/json; charset=utf-8",
              dataType: "html",
              success: function (msg) {
                  //$("#popupchk").bPopup().close();
                  //$("#txttaskcomment").val() = "";
                  //$("input[type='radio']:checked").val() = "";
                  //$('#' + taskid).css("display", "none");
                  debugger;
                  //alert(msg);
                  if (msg.indexOf("Success") != -1) {
                      alertify.success("Comment SuccessFully");
                  }
                  else {
                      alertify.error("Some Error");
                  }
                  hideTask(taskid);

                  $('#page-wrapper').load('../Messages/loadtask');
              }
          });
}

function Show_task(id) {
    debugger;

    var menu = document.getElementById(id);

    if (menu.style.display == 'block') {
        menu.style.display = 'none';
        $("#" + id).removeClass('in');
    } else {
        menu.style.display = 'block';
        $("#" + id).addClass('in');
    }
}


function saveCommentReply(network, messageid, ProfileId) {
    debugger;
    //var messageid = messageid.split("_");
    var comment = document.getElementById("comment_text").value;
    if (comment == "" || comment == undefined) {
        alert("Please write Some text then click Comment!")
        return false;
    }
    if (network == "facebook") {
        $.ajax
            ({
                type: "POST",
                url: "../Messages/FacebokReply?ProfileId=" + ProfileId + "&messageid=" + messageid + "&comment=" + comment,
                data: '',
                contentType: "application/json; charset=utf-8",
                dataType: "html",
                success: function (msg) {
                    debugger;
                    $("#comment_text").val('');
                    $("#closecommentpopup").click();
                    if (msg == "success") {
                        alertify.success('Commented successfully');
                    }
                    else {
                        alertify.error('Somthing went Wrong');
                    }


                }
            });
    }
    else if (network == "twitter") {
        $.ajax
            ({
                type: "POST",
                url: "../Messages/TwitterReply?ProfileId=" + ProfileId + "&messageid=" + messageid + "&comment=" + comment,
                data: '',
                contentType: "application/json; charset=utf-8",
                dataType: "html",
                success: function (msg) {
                    debugger;
                    $("#comment_text").val('');
                    $("#closecommentpopup").click();
                    if (msg == "success") {
                        alertify.success('Replied successfully');
                    }
                    else {
                        alertify.error('Somthing went Wrong');
                    }

                }
            });

    }
    else if (network == "linkedin") {
    }

}
//function SaveArchive(id, network, ProfileId, MessageId, MessageDate) {
//    debugger;
//    var profileurl = $("#formprofileurl_" + id).attr('src');
//    var message = $("#msg_" + id).html();
//    var username = $("#screanName_" + id).html();

//    $.ajax
//           ({
//               type: "POST",
//               url: "../Messages/SaveArchiveMessage?ProfileId=" + ProfileId + "&MessageId=" + MessageId + "&network=" + network + "&username=" + username + "&MessageDate=" + MessageDate + "&profileurl=" + profileurl + "&message=" + encodeURIComponent(message),
//               data: '',
//               contentType: "application/json; charset=utf-8",
//               dataType: "html",
//               success: function (msg) {
//                   debugger;
//                   if (msg == "Archived successfully") {
//                       alertify.success(msg);
//                   }
//                   else {
//                       alertify.error(msg);
//                   }

//               }
//           });


//}

function SaveArchive(id, network, ProfileId, MessageId, MessageDate) {
    debugger;
    var profileurl = $("#formprofileurl_" + id).attr('src');
    var message = $("#msg_" + id).html();
    var username = $("#screanName_" + id).html();

    $.ajax
           ({
               type: "POST",
               url: "../Messages/SaveArchiveMessage?ProfileId=" + ProfileId + "&MessageId=" + MessageId + "&network=" + network + "&username=" + username + "&MessageDate=" + MessageDate + "&profileurl=" + profileurl + "&message=" + encodeURIComponent(message),
               //data: '',
               data: { "message": message },
               //contentType: "application/json; charset=utf-8",
               dataType: "html",
               success: function (msg) {
                   debugger;
                   if (msg == "Archived successfully") {
                       $('#' + 'messagetaskable_' + id).fadeOut(900);
                       //deletearchmesage(id, network, ProfileId, MessageId, MessageDate);
                       alertify.success(msg);
                   }
                   else {
                       alertify.error(msg);
                   }

               }
           });


}




function deletearchmesage(id, network, ProfileId, MessageId, MessageDate) {
    debugger;
    var profileurl = $("#formprofileurl_" + id).attr('src');
    var message = $("#msg_" + id).html();
    var username = $("#screanName_" + id).html();
    $.ajax
          ({
              type: "POST",
              url: "../Messages/DeleteArchiveMessage?ProfileId=" + ProfileId + "&MessageId=" + MessageId + "&network=" + network + "&username=" + username + "&MessageDate=" + MessageDate + "&profileurl=" + profileurl + "&message=" + encodeURIComponent(message),
              data: '',
              contentType: "application/json; charset=utf-8",
              dataType: "html",
              success: function (msg) {

              }
          });


}






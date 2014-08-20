
var grpId = "";
var accToken = "";
var linkedInUserId = "";

var grpId = "";
var accToken = "";
var linkedInUserId = "";
var grpIdPost = [];
var fbuserid = "";
var linuserid = "";
var fileimage = "";



function postmessage() {
    debugger;
    grpIdPost = [];
    var userid = "";
    var checkboxes = document.getElementsByTagName('input');
    for (var i = 0; i < checkboxes.length; i++) {
        if (checkboxes[i].type == 'checkbox') {

            if (checkboxes[i].checked == true) {
                grpIdPost.push(checkboxes[i].id);
                userid = checkboxes[i].value;
                if (userid.indexOf("lin_") > -1) {
                    linuserid = checkboxes[i].value;
                }
                if (userid.indexOf("fb_") > -1) {
                    fbuserid = checkboxes[i].value;
                }


            }
            else {
                //alert("Please must select Record to Delete!");
            }

        }
    }

    //alert(grpIdPost);
    var cnt = grpIdPost.length;
    if (cnt == 0) {
        alert("Please Select Group to Post!");
        //$("#EditBox").bPopup().close();
        return false;


    }
    debugger;
    // $("#datepicker").datepicker();
    $("#showBlock").css('display', 'none');
    $('#postmessagepopup').bPopup({
        fadeSpeed: 'slow', //can be a string ('slow'/'fast') or int
        followSpeed: 1500, //can be a string ('slow'/'fast') or int
        modalColor: 'black',
        modalClose: false,
        opacity: 0.6,
        positionStyle: 'fixed'

    });

    // $("#datepicker").datepicker();
}
function Sendgroupmessage() {
    debugger;
    fileimage = document.getElementById('fileuploadImage1').files[0];
    debugger;
    var fd1 = new FormData();
    var fileExtension = ['jpeg', 'jpg', 'png', 'gif', 'bmp'];
    if (fileimage != null) {
        if (hasExtension('fileuploadImage1', fileExtension)) {
            fd1.append('files', fileimage);
        }
        else {
            alert("File Extention is not current. Please upload any image file");
            return;
        }
    }
    var title = $("#txtTitle").val();
    var msg = $("#txtmessage").val();
    var timeforsch = $("#timepicker").val();
    var dateforsch = $("#datepicker").val();

    var e = document.getElementById("ddlIntervalTime");
    var intervaltime = e.options[e.selectedIndex].value;




    if (msg == "" || msg == null) {
        alert("Please enter in Comment Box");
        return false;
    }
    var curdate = new Date();
    var now = (curdate.getMonth() + 1) + "/" + curdate.getDate() + "/" + curdate.getFullYear() + " " + curdate.getHours() + ":" + curdate.getMinutes() + ":" + curdate.getSeconds();
    fd1.append('gid', grpIdPost);
    fd1.append('msg', msg);
    fd1.append('title', title);
    fd1.append('intervaltime', intervaltime);
    fd1.append('fbuserid', fbuserid);
    fd1.append('linuserid', linuserid);
    fd1.append('clienttime', now);
    fd1.append('timeforsch', timeforsch);
    fd1.append('dateforsch', dateforsch);





    $.ajax({
        url: "../Group/AjaxGroup.aspx?op=postonselectedgroup",
        type: "post",
        data: fd1,
        //data: "{'gid':'" + grpIdPost + "','msg':'" + msg + "','title':'" + title + "','intervaltime':'" + intervaltime + "','fbuserid':'" + fbuserid + "','linuserid':'" + linuserid + "','fd1':'"+fd1+"'}",
        processData: false,
        contentType: false,
        dataType: "json",
        success: function (ret) {
            debugger;
            if (ret == "success") {
                //    alert("Success !!");
                //    facebookgroupdetails(gid, fbUserId);
                //to close a popupbox begin
                document.getElementById('txtmessage').value = "";
                document.getElementById('txtTitle').value = "";
                document.getElementById('fileuploadImage1').value = "";
              
                // $('#close').live('click', function (e) {

                $('#postmessagepopup').bPopup().close();

                //to close a popupbox end

            }
            //}
            //else {
            //    // alert("failure");
            //    //alert("Please enter in Comment Box");
            //}

        }
    });
    document.getElementById('txtmessage').value = "";
    document.getElementById('txtTitle').value = "";
    document.getElementById('fileuploadImage1').value = "";
    // $('#close').live('click', function (e) {

    $('#postmessagepopup').bPopup().close();
    $('input:checkbox').removeAttr('checked');
    grpIdPost = [];
}




function showimage1() {

    try {
        var filesinput = $('#fileuploadImage1');

        debugger;

        if (filesinput !== 'undefined' && filesinput[0].files[0] !== null) {
            $('#showBlock').css('display', 'block');

        }
    } catch (e) {

    }
}
function deleteimage1() {

    try {
        var filesinput = $('#fileuploadImage');

        debugger;

        if (filesinput !== 'undefined') {
            $('#showBlock').css('display', 'none');
            // $('#fileuploadImage').val() = "";
            document.getElementById('fileuploadImage').value = "";

        }
    } catch (e) {

    }
}










function facebookgroupdetails(gid, fbUserId) {
    $(".gcontent").empty();
    //alert("abhay");
    //alert(gid);
    //alert(fbUserId);







    grpId = gid;
    accToken = fbUserId;

    $.ajax({
        url: "../Group/AjaxGroup.aspx",
        type: "post",
        data: "{'gid':'" + gid + "','ack':'" + fbUserId + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        // data: 'blog_id=' + blog_id + '&type=C_GetAllComment',
        success: function (message) {

            $('div .alert_suite_title > a').attr("gid", gid);
            debugger;
            if (message != "") {
                //  console.log(datain["data"]);
                $.each(message["data"], function (i, val) {
                    console.log(val.from.name);
                    console.log(val.message);
                    var fbmsg = "";
                    if (val.picture === undefined) {
                        if (val.message === undefined) {

                        }
                        else {
                            fbmsg = val.message;
                        }

                        $(".gcontent").append('<div class="storyContent"><a class="actorPhoto">'
                                + '<img src="http://graph.facebook.com/' + val.from.id + '/picture?type=small"></a>'
                            + '<div class="storyInnerContent">'
                                + '<div class="actordescription">'
                                    + '<a href="http://facebook.com/' + val.from.id + '" target="_blank" class="passiveName">' + val.from.name + '</a> ' + myDate(val.created_time) + ''
                               + '</div>'
                                + '<div class="messagebody">' + fbmsg + '</div>'
                            + '</div>'
                       + '</div>');
                    }
                    else {
                        var pic = val.picture;

                        if (pic.indexOf("?") != -1) {
                            pic = pic;
                        }
                        else {
                            if (pic.indexOf("_s.jpg") != -1) {
                                pic = pic.replace(/_s.jpg/g, '_n.jpg');
                            }
                        }

//                        if (pic.indexOf("_s.jpg") != -1) {
//                            pic = pic.replace(/_s.jpg/g, '_n.jpg');
//                        }
                        if (val.message === undefined) {

                        }
                        else {
                            fbmsg = val.message;
                        }



                        $(".gcontent").append('<div class="storyContent"><a class="actorPhoto">'
                                + '<img src="http://graph.facebook.com/' + val.from.id + '/picture?type=small"></a>'
                            + '<div class="storyInnerContent">'
                                + '<div class="actordescription">'
                                    + '<a href="http://facebook.com/' + val.from.id + '" target="_blank" class="passiveName">' + val.from.name + '</a> ' + myDate(val.created_time) + ''
                               + '</div>'
                                + '<div class="messagebody">' + fbmsg + '</div>'
                                  + '<img src="' + pic + '">'
                            + '</div>'
                       + '</div>');


                    }


                    //                    $(".gcontent").append('<div class="storyContent"><a class="actorPhoto">'
                    //                                + '<img src="http://graph.facebook.com/' + val.from.id + '/picture?type=small"></a>'
                    //                            + '<div class="storyInnerContent">'
                    //                                + '<div class="actordescription">'
                    //                                    + '<a href="http://facebook.com/' + val.from.id + '" target="_blank" class="passiveName">' + val.from.name + '</a> updated the description.'
                    //                               + '</div>'
                    //                                + '<div class="messagebody">' + val.message + '</div>'
                    //                            + '</div>'
                    //                       + '</div>');





                    //var ob = jQuery.parseJSON(val)
                    //console.log(ob.id);
                });
            }



            else {
                alert('No comment found');

            }
        },
        error: function () {
            alert("failure");
            //$("#result").html('There is error while submit');
        }
    });
}


function myDate(dt) {
    var msgarr = dt.split('T');
    var arrmsg1 = msgarr[0].split('-');

    var month = new Array();
    month[1] = "January";
    month[2] = "February";
    month[3] = "March";
    month[4] = "April";
    month[5] = "May";
    month[6] = "June";
    month[7] = "July";
    month[8] = "August";
    month[9] = "September";
    month[10] = "October";
    month[11] = "November";
    month[12] = "December";
    var n = month[parseInt(arrmsg1[1])];
    return arrmsg1[2] + " " + n;
}


function postFBGroupFeeds() {
    debugger;
    var gid = grpId;
    //alert(gid);

    if (gid.indexOf("lin_") > -1) {

        gid = gid.split('_')[1];
        //  alert(gid);
        var title = $('#txttitle').val();
        var msg = $('#txtcmt').val();

        if (title == "" || title == null) {
            alert("Please enter title");
            return false;
        }


        if (msg == "" || msg == null) {
            alert("Please enter in Comment Box");
            return false;
        }


        linkedInUserId = linkedInUserId;

        $.ajax({
            url: "../Group/AjaxGroup.aspx?op=postLinkedInGroupFeeds&groupid=" + gid + "&LinkedinUserId=" + linkedInUserId + "&msg=" + msg + "&title=" + title,
            type: "post",

            success: function (msg) {

                if (msg != "") {
                    alert("Success !!");
                    // facebookgroupdetails(gid, fbUserId);
                    //to close a popupbox begin
                    document.getElementById('txtcmt').value = "";
                    // $('#close').live('click', function (e) {
                    $('#close').click('click', function (e) {
                        $('#popupchk12').bPopup().close();
                    });
                    //to close a popupbox end

                    $('#close').click();
                }
                else {
                }

            },
            error: function () {
                alert("failure");

            }

        });

    }
    else {


        var fbUserId = accToken;
        var msg = $('#txtcmt').val();
        if (msg == "" || msg == null) {
            alert("Please enter in Comment Box");
            return false;
        }
        //alert(msg);


        $.ajax({
            url: "../Group/AjaxGroup.aspx?op=postFBGroupFeeds",
            type: "post",
            data: "{'gid':'" + gid + "','ack':'" + fbUserId + "','msg':'" + msg + "'}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (msg) {
                //alert(msg);
                if (msg != "") {
                    alert("Success !!");
                    facebookgroupdetails(gid, fbUserId);
                    //to close a popupbox begin
                    document.getElementById('txtcmt').value = "";
                    // $('#close').live('click', function (e) {
                    $('#close').click('click', function (e) {
                        $('#popupchk12').bPopup().close();
                    });
                    //to close a popupbox end

                    $('#close').click();
                }
                else {
                    // alert("failure");
                    //alert("Please enter in Comment Box");
                }

            },
            error: function () {
                alert("failure");

            }

        });
    }

}











function abc() {
    $(".gcontent").append('<div id="abhay" class="storyContent"><a class="actorPhoto">'
                                + '<img src="https://fbcdn-profile-a.akamaihd.net/hprofile-ak-prn2/211863_569212492_1347003669_q.jpg" alt=""></a>'
                            + '<div class="storyInnerContent">'
                                + '<div class="actordescription">'
                                    + '<a class="passiveName">Tommy Skaue </a>updated the description.'
                               + '</div>'
                                + '<div class="messagebody">'
                                    + 'Facebook group for all you Asp.Net developers out there who find MVC (Model View'
                                   + 'Controller) interesting. My intention for this group is to share interesting links'
                                    + 'to blogs, articles or demonstration-code related to Asp.Net MVC. Users might offer'
                                    + 'answers and hints if you have certain questions or issues, but you may want to consider'
                                    + 'visiting the forum found here <a href="#">http://www.asp.net/mvc</a>'
                                + '</div>'
                            + '</div>'
                       + '</div>');
}



//=================linkedin group---------------------
function linkedingroupdetails(groupid, linUserId) {
    // alert(groupid);

    grpId = "lin_" + groupid;
    linkedInUserId = linUserId;
    $.ajax({

        type: "post",
        //   type: "POST",
        url: "../Group/AjaxGroup.aspx?op=getlinkedInGroupDetails&groupid=" + groupid + "&linkuserid=" + linUserId,
        data: '',
        contentType: "application/json; charset=utf-8",
        dataType: "html",
        success: function (message) {

            $('div .alert_suite_title > a').attr("gid", grpId);

            $(".gcontent").html(message);

        }
    });
}

function FollowPosts(groupid, GpPostid, LinkedinUserId, isFollowing) {

    // alert(GpPostid);
    $.ajax({

        type: "post",
        type: "POST",
        url: "../Group/AjaxGroup.aspx?op=FollowPost&groupid=" + GpPostid + "&LinkedinUserId=" + LinkedinUserId + "&isFollowing=" + isFollowing,
        data: '',
        contentType: "application/json; charset=utf-8",
        dataType: "html",
        success: function (message) {
            linkedingroupdetails(groupid, LinkedinUserId);

            // $(".gcontent").html(message);

        }
    });
}
function LikePosts(groupid, GpPostid, LinkedinUserId, isLike) {

    // alert(GpPostid);
    $.ajax({

        type: "post",
        type: "POST",
        url: "../Group/AjaxGroup.aspx?op=LikePost&groupid=" + GpPostid + "&LinkedinUserId=" + LinkedinUserId + "&isLike=" + isLike,
        data: '',
        contentType: "application/json; charset=utf-8",
        dataType: "html",
        success: function (message) {
            linkedingroupdetails(groupid, LinkedinUserId);

            // $(".gcontent").html(message);

        }
    });
}











function CommentOnPosts(Mid) {


    $("#textlin_" + Mid).removeClass('put_comments').addClass('put_commentss');
    $("#oklin_" + Mid).removeClass('ok').addClass('ok_display');
    $("#cancellin_" + Mid).removeClass('cancel').addClass('cancel_display');


}

function cancelLin(Mid) {
    debugger;
    try {
        $("#textlin_" + Mid).addClass('put_comments').removeClass('put_commentss');
        $("#oklin_" + Mid).addClass('ok').removeClass('ok_display');
        $("#cancellin_" + Mid).addClass('cancel').removeClass('cancel_display');
    } catch (e) {

    }

}


function commentLin(groupid, GpPostid, LinkedinUserId) {

    // $("#okfb_" + fbid).hide();
    //alert('dsf');
    var message = $("#textlin_" + GpPostid).val();
    //alert(message);
    if (message == "") {
        alertify.alert("Please write something to Post!");
        return false;
    }
    $("#textlin_" + GpPostid).addClass('put_comments').removeClass('put_commentss');
    $("#oklin_" + GpPostid).addClass('ok').removeClass('ok_display');
    $("#cancellin_" + GpPostid).addClass('cancel').removeClass('cancel_display');
    $.ajax({
        type: "POST",
        url: "../Group/AjaxGroup.aspx?op=linkedCommentOnPost&groupid=" + groupid + "&GpPostid=" + GpPostid + "&message=" + message + "&LinkedinUserId=" + LinkedinUserId,
        data: '',
        contentType: "application/json; charset=utf-8",
        success: function (e) {
            debugger;

            //  $("#okfb_" + fbid).css('display', '');
            alertify.success("Commented Successfully");
            linkedingroupdetails(groupid, LinkedinUserId);
            //  cancelFB(fbid);

        }
    });



}



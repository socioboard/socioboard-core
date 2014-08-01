
var grpId = "";
var accToken = "";
var linkedInUserId = "";



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






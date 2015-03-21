
var grpId = "";
var accToken = "";
var linkedInUserId = "";

var grpIdPost = [];
var fbuserid = "";
var linuserid = "";
var fileimage = "";



function facebookgroupdetails(gid, fbaccesstoken ,profileid) {
   
    grpId = gid;
    accToken = fbaccesstoken;
    //$('#btngrpsend').attr('fbgpid', gid);
    $('#postongp > a').attr("gid", gid);

    $.ajax({
        url: "../Group/GroupPosts?grpId=" + grpId + "&accToken=" + accToken + "&ProfileId=" + profileid,
        type: "post",
        data: '',
        contentType: "application/json; charset=utf-8",
        dataType: "html",
        success: function (message) {

          
        
            $('#groupinfo').html(message);
           // alert(message);
            debugger;
        },
        error: function (err) {
            debugger;
        }

    });
    $(".panel-body").scrollTop(o);
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
   
    if (gid.indexOf("lin_") > -1) {

        gid = gid.split('_')[1];
   
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
            url: "../Group/postLinkedInGroupFeeds?gid=" + gid + "&linkedInUserId=" + linkedInUserId + "&msg=" + msg + "&title=" + title,
            type: "post",

            success: function (msg) {

                if (msg != "") {
                    alert("Success !!");

                    $('#txttitle').empty();
                    $('#txtcmt').empty();
                    linkedingroupdetails(groupid, linUserId)
                    
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


        var AccToken = accToken;
        var msg = $('#txtcmt').val();
        if (msg == "" || msg == null) {
            alert("Please enter in Comment Box");
            return false;
        }
       
        $.ajax({
            url: "../Group/postFBGroupFeeds?gid=" + gid + "&AccToken=" + AccToken + "&msg=" + msg,
            type: "post",
            data: '',
            contentType: "application/json; charset=utf-8",
            dataType: "html",
            success: function (msg) {            
                if (msg != "") {
                    alert("Success !!");   
                    $('#txtcmt').empty();
                    facebookgroupdetails(gid, fbUserId);
                }
                else {
                   
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
   // alert(linUserId);

    grpId = "lin_" + groupid;
    linkedInUserId = linUserId;
    linkeid = linUserId;

    $('#postongp > a').attr("gid", grpId);

    $.ajax({

        url: "../Group/LinkedinGroupPosts?groupid=" + groupid + "&linkeid=" + linkeid,
        type: "post",
        data: '',
        contentType: "application/json; charset=utf-8",
        dataType: "html",
        success: function (message) {
            $('#groupinfo').html(message);
        }
    });
    $(".panel-body").scrollTop(o);
}

function FollowPosts(GpPostid, LinkedinUserId, groupid, isFollowing) {
    //alert(GpPostid);

    $.ajax({

        type: "post",
        url: "../Group/FollowLinkedinPost?GpPostid=" + GpPostid + "&LinkedinUserId=" + LinkedinUserId + "&isFollowing=" + isFollowing,
        data: '',
        contentType: "application/json; charset=utf-8",
        dataType: "html",
        success: function (message) {
            linkedingroupdetails(groupid, LinkedinUserId);

        }
    });
}
function LikePosts(GpPostid, LinkedinUserId, groupid, isLike) {

    // alert(GpPostid);
    $.ajax({

        type: "post",
      
        url: "../Group/LikeOnLinkedinPost?GpPostid=" + GpPostid + "&LinkedinUserId=" + LinkedinUserId + "&isLike=" + isLike,
        data: '',
        contentType: "application/json; charset=utf-8",
        dataType: "html",
        success: function (message) {
            if (isLike == 0) {
                alertify.success("Liked Successfully");
            } else if (isLike == 1) {
                alertify.success("Unliked Successfully");
            }
            linkedingroupdetails(groupid, LinkedinUserId);
        }
    });
}



function commentLin( GpPostid, LinkedinUserId,groupid) {

    var message = $("#textlin_" + GpPostid).val();
    if (message == "") {
        alertify.alert("Please write something to Post!");
        return false;
    }
  
    $.ajax({
        type: "POST",
        url: "../Group/CommentOnLinkedinPost?groupid=" + groupid + "&GpPostid=" + GpPostid + "&message=" + message + "&LinkedinUserId=" + LinkedinUserId,
        data: '',
        contentType: "application/json; charset=utf-8",
        success: function (e) {
            debugger;
            
            $("#textlin_" + GpPostid).empty();
            alertify.success("Commented Successfully");
            linkedingroupdetails(groupid, LinkedinUserId);          
        }
    });
}



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
                
            }
        }
    }  
    var cnt = grpIdPost.length;
    if (cnt == 0) {
        alert("Please Select Group to Post!");
        return false;
    }
    else {
       // jQuery.noConflict();
        $('#RplyAllModal').modal('show');
    }
   
}

function Sendgroupmessage() {
    debugger;
    var fileimage = document.getElementById('MltigroupfileuploadImage').files[0];

    debugger;
    var fd1 = new FormData();
    var title = $("#txtTitle").val();
    var msg = $("#txtmessage").val();
    var timeforsch = $("#grouptimepicker").val();
    var dateforsch = $("#datepic").val();
    //var e = document.getElementById("ddlIntervalTime");
    //var intervaltime = e.options[e.selectedIndex].value;
    var intervaltime = $("#ddlIntervalTime").val();
    if (msg == "" || msg == null) {
        alert("Please enter in Comment Box");
        return false;
    }
    if (dateforsch == "" || dateforsch == null) {
        alert("Please Select date!");
        return false;
    }


    debugger;
    var curdate = new Date();
    var dd = curdate.getDate();
    if (dd < 10) {
        dd = '0' + dd;
    }
    else {
        dd = curdate.getDate();
    }
    var mm = curdate.getMonth() + 1;
    if (mm < 10) {
        mm = '0' + mm;
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

    var now = curdate.getFullYear() + "/" + mm + "/" + dd + " " + hh + ":" + minm + ":" + sec;

    //var curdate = new Date();
    //var now = (curdate.getMonth() + 1) + "/" + curdate.getDate() + "/" + curdate.getFullYear() + " " + curdate.getHours() + ":" + curdate.getMinutes() + ":" + curdate.getSeconds();

   
    var scheduledtime2 = timeforsch.split(" ");
    var scheduledtime3 = scheduledtime2[0] + ':00';
    var schdd = scheduledtime3.split(":");

    var shdd = schdd[0];

    var shmin = schdd[1];
    var shsec = schdd[2];
    if (shdd < 10) {
        shdd = '0' + shdd;
    }
    else {
        shdd = schdd[0];
    }
    if (shmin < 10) {
        //shmin = '0' + shmin;
        shmin = shmin;
    }
    else {
        shmin = schdd[1];
    }
    if (scheduledtime2[1] == "PM") {
        if (shdd != "12") {
            shdd = +shdd + +12;
        }
    }
    var timeforsch = shdd + ":" + shmin + ":" + shsec;



    fd1.append('files', fileimage);
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
        url: "../Group/PostOnselectedGroup",
        type: "post",
        data: fd1,
        processData: false,
        contentType: false,
        dataType: "json",
        success: function (ret) {
            debugger;
            if (ret == "success") {
                alert("Success !!");
                document.getElementById('txtmessage').value = "";
                document.getElementById('txtTitle').value = "";
                document.getElementById('fileuploadImages').value = "";
                $('#postmessagepopup').bPopup().close();
            }
        }
    });
    $('._isgroupchecked').prop('checked', false);
    alertify.success("Posted Successfully!");
    document.getElementById('txtmessage').value = "";
    document.getElementById('txtTitle').value = "";
    document.getElementById('fileuploadImages').value = "";
    //$('#postmessagepopup').bPopup().close();
    //$('input:checkbox').removeAttr('checked');
    grpIdPost = [];
}

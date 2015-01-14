
var profilescountingtosend = 0;
var singleprofileid = '';
var profilesofcomposeinsession = '';
var singleprofileIdforscheduler = '';
var composeuseridcheck = '';
var addanotheridcheck = '';
var composeuseridcheck_scheduler = '';
var addanotheridcheck_scheduler = '';



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


function removeFollowers(removeid, userid) {
    try {
        debugger;

        var confirms = confirm("Are you sure to unfollow the user");

        if (confirms) {
            debugger;
            $.ajax
        ({
            type: "POST",
            url: "../AjaxHome.aspx?op=removefollowers&removeid=" + removeid + "&userid=" + userid,
            data: '',
            contentType: "application/json; charset=utf-8",
            dataType: "html",
            success: function (msg) {
                // alert(msg);
                debugger;

            }
        });
        }
    } catch (e) {

    }

}



function RecentFollowersOnHome() {

    try {
        $.ajax
        ({
            type: "POST",
            url: "../AjaxHome.aspx?op=recentfollowers",
            data: '',
            contentType: "application/json; charset=utf-8",
            dataType: "html",
            success: function (msg) {
                // alert(msg);
                debugger;
                $("#recentfollowers").html(msg);
            }
        });
    } catch (e) {

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
    $("#hm_loader").css('display', 'block');
    $("#hm_loader").attr('src', '../Contents/img/43px_on_transparent1.gif');

    $.ajax
        ({
            type: "POST",
            url: "../AjaxHome.aspx?op=midsnaps&loadtype=" + loadtype,
            data: '',
            contentType: "application/json; charset=utf-8",
            dataType: "html",
            success: function (msg) {

                debugger;
                // $(document).on('scroll', onScroll);
                $("#midsnaps").append(msg);
                $("#hm_loader").attr("src", "").css('display', 'none');
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



var reset = function () {
    $("#toggleCSS").href = "../Contents/css/alertify.default.css";
    alertify.set({
        labels: {
            ok: "OK",
            cancel: "Cancel"
        },
        delay: 5000,
        buttonReverse: false,

    });
};



function SimpleMessageAlert(msg) {
    try {
        alertify.alert(msg);
    } catch (e) {

    }
}

/*This function will delete the profile from our database 
and remove profile from Network Profiles**/
function confirmDel(profileid, profiletype, profile) {
    try {
        debugger;
        reset();
        //if (profile == "facebook")
        //{
        //profiletype=profile;
        //}

        alertify.set({ buttonReverse: true });

        if (profiletype == "facebook_page") {
            alertify.confirm("Are you Sure want to delete this Page ?", function (e) {
                if (e) {
                    debugger;
                    try {
                        $('#' + profileid).hide();
                    } catch (e) {

                    }
                    try {
                        $('#mid_' + profileid).hide();
                    } catch (e) {

                    }
                    try {
                        $('#so_' + profileid).remove();
                    } catch (e) {

                    }
                    $.ajax
            ({
                type: "POST",
                url: "../Home/DeleteAccount?profile=" + profile + "&profileid=" + profileid,
                data: '',
                contentType: "application/json; charset=utf-8",
                dataType: "html",
                success: function (msg) {
                    debugger;
                    window.location = "../Home/Index";
                    if (msg = "Deleted") {
                        alertify.success("Page Deleted Successfully");
                    }
                    try {
                        var s = $("#ContentPlaceHolder1_usedAccount").html(msg);
                    } catch (e) {
                    }
                }
            });
                } else {
                    debugger;

                }
            });
        }
        else {
            alertify.confirm("Are you sure,you want to delete this account ?", function (e) {
                if (e) {
                    debugger;
                    try {
                        $('#' + profileid).hide();
                    } catch (e) {

                    }
                    try {
                        $('#mid_' + profileid).hide();
                    } catch (e) {

                    }
                    try {
                        $('#so_' + profileid).remove();
                    } catch (e) {

                    }
                    $.ajax
            ({
                type: "POST",
                url: "../Home/DeleteAccount?profile=" + profile + "&profileid=" + profileid,
                data: '',
                contentType: "application/json; charset=utf-8",
                dataType: "html",
                success: function (msg) {
                    debugger;
                    window.location = "../Home/Index";
                    if (msg == "Deleted") {
                        alertify.success("Account Deleted Successfully");
                    }
                    try {
                        var s = $("#ContentPlaceHolder1_usedAccount").html(msg);



                    } catch (e) {

                    }

                }
            });




                } else {
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

        //        if (profilesofcomposeinsession == undefined) {

        $.ajax
    ({
        type: "GET",
        url: "../AjaxHome.aspx?op=MasterCompose",
        data: '',
        contentType: "application/json; charset=utf-8",
        dataType: "html",
        success: function (msg) {
            debugger;
            // $.session('compose', msg);
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

        composeuseridcheck = userid[1];
        if (addanotheridcheck == composeuseridcheck) {
            alert('Already added!')
            return false;

        }


        try {
            document.getElementById('imageofuser').src = imageurl;
        } catch (e) {

        }

        if (network == 'fb') {
            document.getElementById('socialIcon').src = "../Contents/img/facebook.png";

            singleprofileid = 'fb_' + userid[1];
            singleprofileIdforscheduler = 'fbscheduler_' + userid[1];
        } else if (network == 'twt') {
            document.getElementById('socialIcon').src = "../Contents/img/twitter.png";
            singleprofileid = 'twt_' + userid[1];
            singleprofileIdforscheduler = 'twtscheduler_' + userid[1];

        } else if (network == 'lin') {
            document.getElementById('socialIcon').src = "../Contents/img/link.png";
            singleprofileid = 'lin_' + userid[1];
            singleprofileIdforscheduler = 'linscheduler_' + userid[1];
        }

        else if (network == 'tumb') {
            document.getElementById('socialIcon').src = "../Contents/img/tumblr.png";
            singleprofileid = 'tumb_' + userid[1];
            singleprofileIdforscheduler = 'tumbscheduler_' + userid[1];
        }

        var totalmessagewords = Countmessagewords();
        $("#messageCount").html(totalmessagewords.toString());

    } catch (e) {
        debugger;
    }
}








function composemessage_scheduler(id, network) {


    debugger;
    try {
        var userid = id.split('_');
        var imageurl = document.getElementById('imgurl_' + userid[1]).innerHTML;

        addanotheridcheck_scheduler = userid[1];

        if (addanotheridcheck_scheduler == composeuseridcheck_scheduler) {
            alert('Already added!')
            return false;

        }

        try {
            document.getElementById('imageofuser_scheduler').src = imageurl;
        } catch (e) {

        }
        if (network == 'fb') {
            try {
                document.getElementById('socialIcon_scheduler').src = "../Contents/img/facebook.png";
            } catch (e) {

            }
            singleprofileid = 'fb_' + userid[1];
            singleprofileIdforscheduler = 'fbscheduler_' + userid[1];
        } else if (network == 'twt') {
            try {
                document.getElementById('socialIcon_scheduler').src = "../Contents/img/twitter.png";
            } catch (e) {

            }
            singleprofileid = 'twt_' + userid[1];
            singleprofileIdforscheduler = 'twtscheduler_' + userid[1];

        } else if (network == 'lin') {
            try {
                document.getElementById('socialIcon_scheduler').src = "../Contents/img/link.png";
            } catch (e) {

            }
            singleprofileid = 'lin_' + userid[1];
            singleprofileIdforscheduler = 'linscheduler_' + userid[1];
        }
        else if (network == 'tumb') {
            try {
                document.getElementById('socialIcon_scheduler').src = "../Contents/img/tumblr.png";
            } catch (e) {

            }
            singleprofileid = 'tumb_' + userid[1];
            singleprofileIdforscheduler = 'tumbscheduler_' + userid[1];
        }
        try {
            $("#loginBox_scheduler").hide();
        } catch (e) {

        }

        var totalmessagewords = Countmessagewordsc();
        $("#messageCount_scheduler").html(totalmessagewords.toString());

    } catch (e) {
        debugger;
    }
}





function addAnotherProfileforMessage(id, network) {
    debugger;
    var divbindforscheduler = '';
    var divbind = '';
    var innerhtmlofMulti = $("#divformultiusers").html();
    var userid = id.split('_');
    var str = ''
    for (i = 1; i < userid.length; i++) {
        str += userid[i] + '_';
    }
    str = str.substring(0, str.length - 1);
    userid[1] = str;



    addanotheridcheck = userid[1];

    if (composeuseridcheck == addanotheridcheck) {
        alert('Already added!')
        //  composeuseridcheck='';
        return false;

    }



    var username = document.getElementById('composename_' + userid[1]).innerHTML;

    try {
        if (innerhtmlofMulti.indexOf(username) == -1) {
            debugger;



            if (network == 'fb') {
                divbind = '<div style="height:21px;width:auto;min-width:22%" class="btn span12" id="fb_' + userid[1] + '"  onclick="delProfilesFromMultiusers(this.id)" ><img src="../Contents/img/facebook.png" alt="" width="15"/>' + username + '<span data-dismiss="alert" class="close pull-right">×</span></div>';

            } else if (network == 'twt') {
                divbind = '<div style="height:21px;width:auto;min-width:22%" class="btn span12" id="twt_' + userid[1] + '" onclick="delProfilesFromMultiusers(this.id)"><img src="../Contents/img/twitter.png" alt="" width="15"/>' + username + '<span data-dismiss="alert" class="close pull-right">×</span></div></div>';

            } else if (network == 'lin') {
                divbind = '<div style="height:21px;width:auto;min-width:22%" class="btn span12" id="lin_' + userid[1] + '" onclick="delProfilesFromMultiusers(this.id)"><img src="../Contents/img/link.png" alt="" width="15"/>' + username + '<span data-dismiss="alert" class="close pull-right">×</span></div></div>';

            }

            else if (network == 'tumb') {
                divbind = '<div style="height:21px;width:auto;min-width:22%" class="btn span12" id="tumb_' + userid[1] + '" onclick="delProfilesFromMultiusers(this.id)"><img src="../Contents/img/tumblr.png" alt="" width="15"/>' + username + '<span data-dismiss="alert" class="close pull-right">×</span></div></div>';

            }


        }
    } catch (e) {

    }


    try {
        $("#divformultiusers").append(divbind);
    } catch (e) {

    }

    try {
        $("#addBox").hide();
    } catch (e) {

    }

    var totalmessagewords = Countmessagewords();
    $("#messageCount").html(totalmessagewords.toString());

}


function addAnotherProfileforMessage_scheduler(id, network) {
    debugger;
    var divbindforscheduler = '';
    var divbind = '';

    var userid = id.split('_');
    var str = ''
    for (i = 1; i < userid.length; i++) {
        str += userid[i] + '_';
    }
    str = str.substring(0, str.length - 1);
    userid[1] = str;


    addanotheridcheck_scheduler = userid[1];
    if (composeuseridcheck_scheduler == addanotheridcheck_scheduler) {
        alert('Already added!')
        //  composeuseridcheck='';
        return false;

    }


    var username = document.getElementById('composename_' + userid[1]).innerHTML;
    var innerhtmlofmultischeduler = $("#divformultiusers_scheduler").html();

    try {
        if (innerhtmlofmultischeduler.indexOf(username) == -1) {
            if (network == 'fb') {
                divbindforscheduler = '<div style="height:31px;" class="btn span12" id="fbscheduler_' + userid[1] + '"  onclick="delProfilesFromMultiusers(this.id)" ><img src="../Contents/img/facebook.png" alt="" width="15"/>' + username + '<span data-dismiss="alert" class="close pull-right">×</span></div></div>';

            } else if (network == 'twt') {
                divbindforscheduler = '<div style="height:31px;" class="btn span12" id="twtscheduler_' + userid[1] + '" onclick="delProfilesFromMultiusers(this.id)"><img src="../Contents/img/twitter.png" alt="" width="15"/>' + username + '<span data-dismiss="alert" class="close pull-right">×</span></div></div>';

            } else if (network == 'lin') {
                divbindforscheduler = '<div style="height:31px;" class="btn span12" id="linscheduler_' + userid[1] + '" onclick="delProfilesFromMultiusers(this.id)"><img src="../Contents/img/link.png" alt="" width="15"/>' + username + '<span data-dismiss="alert" class="close pull-right">×</span></div>';
            }
            else if (network == 'tumb') {
                divbindforscheduler = '<div style="height:31px;" class="btn span12" id="tumbscheduler_' + userid[1] + '" onclick="delProfilesFromMultiusers(this.id)"><img src="../Contents/img/tumb.png" alt="" width="15"/>' + username + '<span data-dismiss="alert" class="close pull-right">×</span></div>';
            }
            //$("#ab_scheduler").attr("display", "none");
        }
    } catch (e) {

    }

    try {
        $("#divformultiusers_scheduler").append(divbindforscheduler);
    } catch (e) {

    }

    try {
        $("#ab_scheduler").hide();
    } catch (e) {

    }


    var totalmessagewords = Countmessagewordsc();
    $("#messageCount_scheduler").html(totalmessagewords.toString());

}








/*To clear the popup on close of Compose popup*/
function closeonCompose() {
    debugger;
    $("#adddates_scheduler").html('');
    document.getElementById('fileuploadImage').value = "";
    chkidforusertest.length = 0;
    $("#showBlock").css('display', 'none');
    try {

        datearr.length = 0;

        deleteimage();

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

    var messagecnt = Countmessagewords();
    var messagecountsc = Countmessagewordsc();



    try {
        $('#messageCount').html(messagecnt.toString());
    } catch (e) {

    }
    try {
        $('#messageCount_scheduler').html(messagecountsc.toString() + ' Characters Remaining');
    } catch (e) {

    }
    chkidforusertest.length = 0;
    try {
        $("#sendMessageBtn").html('<img src="../Contents/img/sendbtn.png" alt="" />');
    } catch (e) {

    }
    try {
        $("#sendMessageBtn_scheduler").html('<img src="../Contents/img/sendbtn.png" alt="" />');
    } catch (e) {

    }
}


/*This function upload the file into folder to send message with file*/
function ajaxFileUpload() {
    //starting setting some animation when the ajax starts and completes
    //    $("#loading")
    //        .ajaxStart(function () {
    //            $(this).show();
    //        })
    //        .ajaxComplete(function () {
    //            $(this).hide();
    //        });

    /*
    prepareing ajax file upload
    url: the url of script file handling the uploaded files
    fileElementId: the file type of input element id and it will be the index of  $_FILES Array()
    dataType: it support json, xml
    secureuri:use secure protocol
    success: call back function when the ajax complete
    error: callback function when the ajax failed
           
    */
    try {


        $('#fileuploadImage').fileupload({
            dataType: 'json',
            url: '../FileUpload.ashx',
            done: function (e, data) {
                data.context.text('Upload finished.');
            }
        });

    } catch (e) {
        debugger;
    }



}



/*This function  sends the composed 
message of  SocialNetwork*/
function hasExtension(inputID, exts) {
    var fileName = document.getElementById(inputID).value;
    return (new RegExp('(' + exts.join('|').replace(/\./g, '\\.') + ')$')).test(fileName);
}

function Countmessagewordsc() {
    var Fbidcount = 0;
    var twtIdcount = 0;
    var LinkedInIdcount = 0;
    var totalmessagewords1 = 0;
    var chkidforusertest = new Array();

    var bindingofdata = document.getElementById('divformultiusers_scheduler');
    var countdiv = bindingofdata.getElementsByTagName('div');

    for (var i = 0; i < countdiv.length; i++) {
        chkidforusertest.push(countdiv[i].id);
    }

    if (chkidforusertest.indexOf(singleprofileid) == -1) {
        chkidforusertest.push(singleprofileid);
    }


    for (var i = 0; i < chkidforusertest.length; i++) {

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
    }

    if (Fbidcount > 0 && twtIdcount == 0 && LinkedInIdcount == 0) {
        totalmessagewords1 = 5000;
    }
    else if (Fbidcount >= 0 && twtIdcount == 0 && LinkedInIdcount > 0) {
        totalmessagewords1 = 700;
    }

    else {
        totalmessagewords1 = 140;
    }

    return totalmessagewords1;
}



function SendMessage() {
    debugger;
    var fd = new FormData();
    try {


        var filesimage = document.getElementById('fileuploadImage').files[0];

        debugger;

        var fileExtension = ['jpeg', 'jpg', 'png', 'gif', 'bmp'];
        if (filesimage != null) {
            if (hasExtension('fileuploadImage', fileExtension)) {
                fd.append('file', filesimage);
            }
            else {
                alert("File Extention is not current. Please upload any image file");
                return;
            }
        }

    }
    catch (e) {

    }

    try {
        var message = $("#textareavaluetosendmessage").val().trim();
        if (message != '' || filesimage != null) {


            $("#sendMessageBtn").html('<img src="../Contents/img/325.gif" alt="" />');
            var bindingofdata = document.getElementById('divformultiusers');
            var countdiv = bindingofdata.getElementsByTagName('div');

            for (var i = 0; i < countdiv.length; i++) {
                chkidforusertest.push(countdiv[i].id);
            }

            if (chkidforusertest.indexOf(singleprofileid) == -1) {
                chkidforusertest.push(singleprofileid);
            }
            if (chkidforusertest[0] == "") {
                //alert("Please select Account");
                alertify.alert("Please select Account");
                $("#sendMessageBtn").html('<img src="../Contents/img/sendbtn.png" alt="" />');
                return false;
            }


            $("#composeBox").bPopup().close();

            debugger;

            $.ajax
            ({
                type: 'POST',
                url: '../AjaxHome.aspx?op=sendmessage&message=' + encodeURIComponent(message) + '&userid[]=' + chkidforusertest,
                data: fd,
                // data:fd+'&massagee='+encodeURIComponent(message)+'&userid[]='+chkidforusertest,
                processData: false,
                contentType: false,
                // contentType: "application/json; charset=utf-8",
                success: function (data) {
                    $("#sendMessageBtn").html('<img src="../Contents/img/sendbtn.png" alt="" />');
                    $("#composeBox").bPopup().close();
                    filesimage = "";
                    document.getElementById('fileuploadImage').value = "";
                    alertify.success("Message Sent Successfully");
                    closeonCompose();
                    var arrmsg = data.split('~');
                    $('#ContentPlaceHolder1_spanSent').html(arrmsg[1]);
                }
            });
        } else {

            alert("please enter the message");
        }

    }
    catch (e) {

    }
}


function delProfilesFromMultiusers(id) {
    debugger;

    try {
        debugger;
        $("#divformultiusers_scheduler").find("#" + id).remove();
        $("#" + id).remove();



    } catch (e) {
        debugger;
        alert(e);
    }
    var totalmessagewords = Countmessagewords();
    $("#messageCount").html(totalmessagewords.toString());


    var totalmessagewords = Countmessagewordsc();
    $("#messageCount_scheduler").html(totalmessagewords.toString());


}



function CountMessages() {
    debugger;
    var gotajax = $.ajax({

        type: 'POST',
        url: '../AjaxHome.aspx?op=countmessages',
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            debugger;
            $("#incom_messages").html(data);
            $("#incomMessages").html(data);
        }

    });

}


function AddLinPage(id, name, oauth) {
    debugger;
    $.ajax({
        type: 'POST',
        url: '../LinkedinManager/AddLinkedinCompanyPage?id=' + id + '&oauth=' + oauth,
        contentType: false,
        success: function (data) {
            alertify.set({ delay: 3000 });
            $('#linkedincompanypageinfo').css({ 'display': 'none' });
            $('#linkedinpopup').modal('hide');
            alertify.success("Linkedin Company Page Added Successfully");
            window.location = '../Home/Index';

        }

    });
}



function DetailsofProfile(id, network) {
    debugger;
    try {
        debugger;
        debugger;

        $("#ProfileDetailBody").html("");
        $.ajax
        ({
            type: "GET",
            url: "../Messages/getProfileDetails?Profileid=" + id + '&Network=' + network,
            crossDomain: true,
            contentType: "application/json; charset=utf-8",
            dataType: "html",
            success: function (msg) {
                debugger;

                $("#ProfileDetailBody").append(msg);
                $("#ProfileDetail").modal('show');

            },
            error: function (err) {
                alert(err);
                debugger;
            }
        });
    } catch (e) {

    }
}

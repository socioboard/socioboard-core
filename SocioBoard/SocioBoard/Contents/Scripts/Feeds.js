var feedajax = '';
var bindfeedsprofiles = '';
var updatingwallposts = '';
var feedsdataforfacebook = '';
var feedsdatafortwitter = '';
var feedprofilesforfacebook = '';
var feedprofilesfortwitter = '';



function BindFeeds(network) {

    var checkbool = true;


    debugger;

    if (feedsdataforfacebook.indexOf('<div id=\"messagetaskable_') != -1 && network == "facebook") {
        checkbool = false;
    } else if (feedsdatafortwitter.indexOf('<div id=\"messagetaskable_') != -1 && network == "twitter") {
    checkbool = false;
    }

    if (checkbool) {
        feedajax = $.ajax
        ({
            type: "POST",
            url: "../Feeds/AjaxFeed.aspx?op=bindFeeds&network=" + network,
            data: '',
            contentType: "application/json; charset=utf-8",
            dataType: "html",
            cache:true,
            success: function (msg) {
                $("#inbox_msgs").html(msg);
                if (network == 'facebook') {
                    feedsdataforfacebook = msg;
                } else if (network == "twitter") {
                    feedsdatafortwitter = msg;
                }

                BindProfilesInFeedsTab(network);
                //     UpdatingWallPostfromFacebook();
            }
        });

    } else if (feedsdataforfacebook.indexOf('<div id=\"messagetaskable_') != -1 && network == "facebook") {
        $("#inbox_msgs").html(feedsdataforfacebook);
        BindProfilesInFeedsTab("facebook");
    } else if (feedsdatafortwitter.indexOf('<div id=\"messagetaskable_') != -1 && network == "twitter") {
        $("#inbox_msgs").html(feedsdatafortwitter);
        BindProfilesInFeedsTab("twitter");
    }
}


function BindProfilesInFeedsTab(network) {
    var boolcheck = true;

    debugger;
    bindfeedsprofiles.abort();
    if (feedprofilesforfacebook.indexOf('<li><a id=\"greencheck_') != -1 && network == "facebook") {
        boolcheck = false;
    } else if (feedprofilesfortwitter.indexOf('<li><a id=\"greencheck_') != -1 && network == "twitter") {
    boolcheck = false;
    }
    if (boolcheck) {
        bindfeedsprofiles = 
    $.ajax
        ({
            type: "POST",
            url: "../Feeds/AjaxFeed.aspx?op=bindProfiles&network=" + network,
            data: '',
            contentType: "application/json; charset=utf-8",
            dataType: "html",
            success: function (msg) {
                $("#accordianprofiles").html(msg);
                if (network == "facebook") {
                    feedprofilesforfacebook = msg;
                } else if (network == "twitter") {
                    feedprofilesfortwitter = msg;
                }
            }
        });
    } else if (feedprofilesforfacebook.indexOf('<li><a id=\"greencheck_') != -1 && network == "facebook") {
        $("#accordianprofiles").html(feedprofilesforfacebook);
    } else if (feedprofilesfortwitter.indexOf('<li><a id=\"greencheck_') != -1 && network == "twitter") {
        $("#accordianprofiles").html(feedprofilesfortwitter);
    }
     
}

function UpdatingWallPostfromFacebook() {
    updatingwallposts = $.ajax({

        type: "POST",
        url: "../Feeds/AjaxFeed.aspx?op=updatewallposts",
        data: '',
        contentType: "application/json; charset=utf-8",
        dataType: "html",
        success: function (msg) {
            $("inbox_msgs").html(msg);

        }
    });
}

function showinsprof(id) {
    debugger;
    $("#instaAccounts").css('display', 'block');
    $(".b-modal").css('display', 'block');
    $.ajax({
        type: "POST",
        url: "../Feeds/AjaxFeed.aspx?op=IntagramProfiles&mediaId=" + id,
        data: '',
        contentType: "application/json; charset=utf-8",
        dataType: "html",
        success: function (msg) {
            alert(msg);
            $("#accountsins").html(msg);
            $("#instaAccounts").bPopup();
        }
    });
   // $.session('id', id);
}

function postLikeRequest(mediaid, IntagramId, accessToken) {
    debugger;
    alert(mediaid);
    $.ajax({
        type: "POST",
        url: "../Feeds/AjaxFeed.aspx?op=postLike&mediaId="+mediaid+"InstagramId"+IntagramId + "access="+accessToken,
        data: '',
        contentType: "application/json; charset=utf-8",
        dataType: "html",
        success: function (msg) {
            alert(msg);
            $("#accountsins").html(msg);
            $("#instaAccounts").bPopup();
        }
    });
}
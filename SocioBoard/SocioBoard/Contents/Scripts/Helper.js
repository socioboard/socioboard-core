var chkid = new Array();







/*display the selected user data which is checked*/
function checkprofile(id,page,network) {

    debugger;
    try {


        $("#another-load").html('<img src="../Contents/Images/360.gif" />');

        chkid = [];
        var chkcount = 0;
        var underscore = id.indexOf('_');
        var dd = id.substring(underscore + 1, id.length);
        var s = document.getElementById('checkimg_' + dd);


        /// chk box img
        if (s.src.indexOf("network_click") != -1) {
            s.src = s.src.replace("network_click", "Network_non_click");
        } else if (s.src.indexOf("Network_non_click") != -1) {
            s.src = s.src.replace("Network_non_click", "network_click");
        }



        /*return id which is uncheck*/

        var hdn = document.getElementById("fbhidden_" + dd);
        if (hdn != null) {
            var hdnval = hdn.value;
        } else {
            var usernameval = document.getElementById("profileusername_" + dd).innerHTML;
        }

        var profcount = document.getElementById("profilecounter").value;
        for (var i = 0; i < profcount; i++) {
            if (document.getElementById("checkimg_" + i).src.indexOf("network_click") != -1) {
                var hdn = document.getElementById("fbhidden_" + i);
                var hbn = document.getElementById("twthidden_" + i);

                if (hbn != null || hdn != null) {
                    if (hdn != null) {

                        chkid[chkcount] = hdn.value;
                    } else if (hbn != null) {
                        chkid[chkcount] = hbn.value;

                    }

                    chkcount++;
                }
            }
        }
        debugger;
        $.ajax
        ({
            type: "POST",
            url: "../Helper/AjaxHelper.aspx?op=removedata&data[]=" + chkid + "&page=" + page+"&network="+network,
            data: '',
            contentType: "application/json; charset=utf-8",
            dataType: "html",
            success: function (msg) {
                debugger;
                $("#inbox_msgs").html(msg);
                $("#another-load").html("");

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

            if (msg.indexOf("Contents/Images/pin") != -1) {
                msg = msg.replace("../Contents/Images/pin.png", "");
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
            $("#inbox_messages").html(msg);

            $('#popupchk').bPopup({
                fadeSpeed: 'slow', //can be a string ('slow'/'fast') or int
                followSpeed: 1500, //can be a string ('slow'/'fast') or int
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
            var dd = msg.indexOf('messagedescription_');
            var ss = msg.substring(dd + 19, dd + 21);
            var id = "";
            if (ss.indexOf("") == -1) {
                id = ss;
            } else {
                id = ss.substring(0, 1);

            }
            var name = document.getElementById("rowname_" + id).innerHTML;
            var description = document.getElementById("messagedescription_" + id).innerHTML;
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
            alert(e);
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
            $.ajax
                            ({
                                type: "GET",
                                url: "https://graph.facebook.com/" + id + "",
                                data: '',
                                crossDomain: true,
                                contentType: "application/json; charset=utf-8",
                                dataType: "jsonp",
                                success: function (data) {
                                    debugger;
                                    jas += '<div class="big-puff">';
                                    jas += '<article><dl>';
                                    jas += '<img src="http://graph.facebook.com/' + data.id + '/picture?type=small" alt="" class="photo">';
                                    jas += '<div class="descrption">';
                                    jas += '<h3 title="Carlos Ullon" class="fn">' + data.name + '<span class="screenname prof_meta">';
                                    jas += '<span class="ficon blue_bird_sm nickname"></span></span>';
                                    jas += '</h3>';
                                    jas += '</div></dl><section class="profile_sub_wrap" style=" margin-left:0;margin-top:0;">';
                                    jas += '<ul class="follow">';
                                    jas += '<li><span class="followers filter"><span style="width:54px;">Link</span>';
                                    jas += '<a data-msg_type="followers" target="_blank" href="' + data.link + '">' + data.link + '</a></span></li>'
                                     + '<li><span class="followers filter"><span style="width:100px;">UserName</span>'
                                    jas += '<a data-msg_type="followers" href="javascript:void(0)">@' + data.name + '</a></span></li>' +
                                    '<li><span class="followers filter"><span style="width:100px;">FirstName</span>';
                                    jas += '<a data-msg_type="followers" href="javascript:void(0)">' + data.first_name + '</a></span></li>' +
                                    '<li><span class="followers filter"><span style="width:100px;">LastName</span>';
                                    jas += '<a data-msg_type="followers" href="javascript:void(0)">' + data.last_name + '</a></span></li>' +
                                    '<li><span class="followers filter"><span style="width:100px;">Gender</span>';
                                    jas += '<a data-msg_type="followers" href="javascript:void(0)">' + data.gender + '</a></span></li>';
                                    jas += '</ul></section></article>';
                                    jas += '</div></div>';
                                    $('#detailsfacebook').html(jas);
                                    $('#facebookuserDetails').bPopup();

                                },
                                error: function (e) {

                                }
                            });
        } catch (e) {
            alert(e);
        }
    }



    /*display the user information*/
    function detailsprofile(id) {
        try {
            debugger;
            $("#another-load").html('<img src="../Contents/Images/360.gif" />');
            debugger;
            var sd = '';
            var msgname = '';
            if (id.indexOf('rowname_') != -1) {
                sd = document.getElementById(id).innerHTML;
            } else {
                sd = id;
            }

            $.ajax
        ({
            type: "GET",
            url: "https://api.twitter.com/1/users/lookup.json",
            data: 'screen_name=' + sd,
            crossDomain: true,
            contentType: "application/json; charset=utf-8",
            dataType: "jsonp",
            success: function (msg) {

                debugger;
                try {
                    msgname = msg[0].screen_name;
                    var jstring = '<div class="big-puff">';
                    jstring += '<article><dl>';
                    jstring += '<img src="' + msg[0].profile_image_url + '" alt="" class="photo">';
                    jstring += '<div class="descrption">';
                    jstring += '<h3 title="Carlos Ullon" class="fn">' + msg[0].name + '<span class="screenname prof_meta">' + msg[0].screen_name;
                    jstring += '<span class="ficon blue_bird_sm nickname"></span></span>';
                    jstring += '</h3><p class="note"></p>';
                    jstring += '<ul class="prof_meta">';
                    try {
                        jstring += '<li>' + msg[0].status.text + '</li>';
                    } catch (e) { }
                    jstring += '<li></li>';
                    jstring += '</ul></div></dl><section class="profile_sub_wrap"><a class="klout_link" target="_blank" href="http://www.klout.com/carlosullon">';
                    jstring += '<div class="klout_container"><span class="score"></span>';
                    jstring += '<div class="icon klout_score"></div>';
                    jstring += '</div></a>';
                    jstring += '<ul class="follow">';
                    jstring += '<li><span class="followers filter"><span>Followers</span>';
                    jstring += '<a data-msg_type="followers" href="javascript:void(0)">' + msg[0].followers_count + '</a></span></li>';
                    jstring += '<li><span class="friends filter"><span>Following</span> <a data-msg_type="friends" href="javascript:void(0)">' + msg[0].friends_count + '</a></span></li>';
                    jstring += '</ul></section></article>';
                    jstring += '<div class="usertweets">';
                    jstring += '<div class="tweetstitle">User Tweets</div>';
                    jstring += '<div id="offmessages" class="usertweets_div">';

                    $.ajax
                    ({
                        type: "GET",
                        url: "https://api.twitter.com/1/statuses/user_timeline.json",
                        data: 'screen_name=' + msg[0].screen_name,
                        crossDomain: true,
                        contentType: "application/json; charset=utf-8",
                        dataType: "jsonp",
                        success: function (data) {
                            debugger;
                            var js = '';
                            for (var item in data) {
                                try {

                                    js += '<div  class="messages"><section>' +
                                     '<aside>' +
                                     ' <section data-sstip_class="twt_avatar_tip" class="js-avatar_tip">' +
                                    '<a class="avatar_link view_profile">' +
                                    ' <img width="54" height="54" border="0" src="' + data[item].user.profile_image_url + '" class="avatar" id="' + data[item].id + '">' +
                                     '<article class="message-type-icon">' +
                                       '</article>' +
                                                                           '</a>' +
                                                               '</section>' +
                                                             '<ul></ul>' +
                                              '</aside>' +
                                             '<article>' +
		                                '<div class=""><a href="" class="language"></a></div>' +
		                                '<div class="message_actions"><a href="#" class="gear_small"><span class="ficon" title="Options">?</span></a></div>' +
		                                '<div class="message-text font-14" id="messagedescription_1">' + data[item].text + '</div>' +
		                                '<section class="bubble-meta">' +
			                               '<article class="threefourth text-overflow">' +
				                                '<section class="floatleft">' +
					                                '<a data-sstip_class="twt_avatar_tip" class="js-avatar_tip view_profile profile_link">' +
						                                '<span id="rowname_1">' + data[item].user.screen_name + '</span>' +
                                                     '</a>&nbsp;' +
					                                '<a title="View message on Twitter" target="_blank" class="time" data-msg-time="1363926699000">' + data[item].created_at + '</a>,' +
					                                '<span title="Moscow" class="location">&nbsp;</span>' +
				                                 '</section>' +
			                                '</article>' +
			                                '<ul class="message-buttons quarter clearfix">' +
                                    //				                                '<li><a href="#"><img width="17" height="24" border="none" alt="" src="../Contents/Images/replay.png"></a></li>' +
                                    //				                                '<li><a onclick="createtask(this.id);" id="createtasktwt_1">' +
                                    //					                                '<img width="14" height="17" border="none" alt="" src="../Contents/Images/pin.png">' +
                                    //				                                    '</a>' +
                                    //				                                '</li>' +
			                                '</ul>' +
		                                '</section>' +
	                                '</article>' +
                                '</section></div>';

                                } catch (e) {
                                    alert(e);
                                } // end for
                                $("#offmessages").html(js);
                            }

                        },
                        error: function (err) {
                            alert("error : " + err);
                        }
                    });

                    jstring += '</div>';
                    jstring += '</div>';
                    jstring += '</div></div>';



                    $("#details").html(jstring);
                    $("#another-load").html("");
                    $("#userdetails").bPopup();
                } catch (ee) {
                    alert(ee);
                }
            },
            error: function (error) {
                alert("error " + error);
            }
        });
        }
        catch (e) {
            alert("catch " + e);
        }

    }

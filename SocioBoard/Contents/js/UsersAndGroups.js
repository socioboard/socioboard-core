
/*this function will change the users profiles according to the Group. hozefa*/

var GrpId = "";

$(document).ready(function () {

    var totalgroups = $("#totalgroups").html();





    if (totalgroups > 0) {
        debugger;
        changeClassandProfilesOfGroup('group_0')
    }

    $("#Button1").click(function () {
        $("#Insertgroupname").css('display', 'block');

    });

    $("#close").click(function () {
        $("#Insertgroupname").css('display', 'none');

    });

    $("#Button2").click(function () {
        $("#insertFbPopup").css('display', 'block');

    });
    $("#bclose").click(function () {
        $("#insertFbPopup").css('display', 'none');
    });



    function gettingSessionForGroup() {

    }


});

$("#home").removeClass('active');
$("#message").removeClass('active');
$("#feeds").removeClass('active');
$("#discovery").removeClass('active');
$("#publishing").removeClass('active');


function DeleteGroup(groupid, i) {
    debugger;
    $("#group_" + i).hide();

    $.ajax
                        ({
                            type: "post",
                            url: "../Settings/AjaxInsertGroup.aspx?op=deleteGroupName&groupId=" + groupid,
                            data: '',
                            contenttype: "application/json; charset=utf-8",
                            datatype: "html",
                            success: function (msg) {
                                window.location.reload();
                            }
                        });

}


//modified by hozefa 4-7-2014
function RemoveProfileFromGroup(id) {

    try {
        $.ajax
                        ({
                            type: "post",
                            url: "../Settings/AjaxInsertGroup.aspx?op=deleteGroupProfiles&profileid=" + id,
                            data: '',
                            contenttype: "application/json; charset=utf-8",
                            datatype: "html",
                            success: function (msg) {
                                // window.location.reload();
                                GrpId = msg;
                                //bind all profill of group after new profile adding.
                                gettingallProfileafternewAdded(msg);

                            }
                        });

    }
    catch (e) {
        try {
            console.write(e);
        } catch (ee) {

        }
    }

}





function transfertoGroup(network, id) {
    debugger;
    try {
        $.ajax
        ({
            type: "POST",
            url: "../Settings/AjaxInsertGroup.aspx?op=addProfilestoGroup&network=" + network + "&profileid=" + id,
            data: '',
            contentType: "application/json; charset=utf-8",
            dataType: "html",
            success: function (msg) {
                //alert(msg);
                GrpId = msg;
                //bind all profill of group after new profile adding.
                gettingallProfileafternewAdded(msg);
                // $("#usergroups_" + id).hide();
                // window.location.reload();

            }
        });
    } catch (e) {

        //alert(e);
    }


}


function gettingallProfileafternewAdded(gpid) {


    $.ajax({
        type: "POST",
        url: "../Settings/AjaxInsertGroup.aspx?op=bindGroupProfiles&groupId=" + gpid,
        data: '',
        contentType: "application/json; charset=utf-8",
        dataType: "html",
        success: function (msg) {
            // document.getElementById("ContentPlaceHolder1_lblSelectedGroup").innerHTML = 'To ' + groupname;
            document.getElementById("ContentPlaceHolder1_SelectedGroupProfiles").innerHTML = msg;

            // var chkeckinngdata = document.getElementById("ContentPlaceHolder1_SelectedGroupProfiles");
            //var countdiv = chkeckinngdata.getElementsByTagName('div');
            debugger;
            //                for (var i = 0; i < countdiv.length; i++) {
            //                    var stringidchk = countdiv[i].id.split('_');

            //                    //$("#usergroups_" + stringidchk[1]).hide();
            //                }
        }


    });
}




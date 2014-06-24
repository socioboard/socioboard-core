
/*this function will change the users profiles according to the Group.*/

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
                                window.location.reload();
                            }
                        });

    } catch (e) {
        try {
            console.write(e);
        } catch (ee) {

        }
    }

}


function transfertoGroup(network, id) {
    debugger;
    $.ajax
        ({
            type: "POST",
            url: "../Settings/AjaxInsertGroup.aspx?op=addProfilestoGroup&network=" + network + "&profileid=" + id,
            data: '',
            contentType: "application/json; charset=utf-8",
            dataType: "html",
            success: function (msg) {
                $("#usergroups_" + id).hide();
                window.location.reload();
            }
        });

}

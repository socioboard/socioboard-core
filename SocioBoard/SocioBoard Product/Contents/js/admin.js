$(document).ready(function () {

});

function del(id) {
    var r = confirm("Are you sure to want to Delete!");
    if (r == true) {
//        window.location = self.location;
//        return false;
        $.ajax({
            type: "POST",
            url: "../Admin/Ajaxadmin.aspx?id=" + id + "",
            data: '',
            contentType: "application/json; charset=utf-8",
            dataType: "html",
            success: function (msg) {
                if (msg == "success") {
                    //alert("User Deleted Successfully");
                    window.location.reload();
                }
                else {
                    alert(msg);
                }
            }
        });
    }
    else {
       
    }
}
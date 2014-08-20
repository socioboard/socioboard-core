
function ShowDialog(modal) {
    $("#overlay").show();
    $("#fbConnect").fadeIn(300);

    if (modal) {
        $("#overlay").unbind("click");
    }
    else {
        $("#overlay").click(function (e) {
            HideDialog();
        });
    }
}

function HideDialog() {
    
    $("#overlay").hide();
    $("#fbConnect").fadeOut(300);
    $("#fbConnect").hide();
}

function ShowDialogHome(modal) {
    $("#overlay").show();
    $("#fbConnect1").fadeIn(300);

    if (modal) {
        $("#overlay").unbind("click");
    }
    else {
        $("#overlay").click(function (e) {
            HideDialog();
        });
    }
}

function HideDialogHome() {

    $("#overlay").hide();
    $("#fbConnect1").fadeOut(300);
    $("#fbConnect1").hide();
} 
        
        
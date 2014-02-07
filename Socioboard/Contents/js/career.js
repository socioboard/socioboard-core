$(document).ready(function () {
    $("#submit").click(function () {
        //alert("asdasd");
        //asd('Abhay123');
        var fname = "";
        var lname = "";
        var email = "";
        var message = "";

        var fileInput = $('#cvfile');
        var fileData = fileInput.prop("files")[0];   // Getting the properties of file from file field
        var formData = new window.FormData();                  // Creating object of FormData class
        formData.append("file", fileData); // Appending parameter named file with properties of file_field to form_data
        formData.append("user_email", email);
        $.ajax({
            url: '/Helper/upload.ashx',
            data: formData,
            processData: false,
            contentType: false,
            type: 'POST',
            success: function (data) {
                alert(data);
            },
            error: function (errorData) {
                $('.result-message').html("there was a problem uploading the file.").show();
            }
        });















    });
});

    function asd(asd1) {
        alert(asd1);
    };

    function validateEmail(email) {
        var emailReg = /^([\w-\.]+@([\w-]+\.)+[\w-]{2,4})?$/;
        if (!emailReg.test(email)) {
            return false;
        } else {
            return true;
        }
    }
<?php
/*
 * Simple PHP File Upload EXAMPLE
 */

/* THIS FILE IS USED IN DROPZONE FILE UPLOAD AS THE FORM ACTION
 * UNCOMMENT TO ENABLE THE ACTUAL FILE UPLOAD
 * 
$uploaddir = '/var/www/uploads/';
$uploadfile = $uploaddir . basename($_FILES['userfile']['name']);

echo '<pre>';
if (move_uploaded_file($_FILES['userfile']['tmp_name'], $uploadfile)) {
    echo "File is valid, and was successfully uploaded.\n";
} else {
    echo "Possible file upload attack!\n";
}

echo 'Here is some more debugging info:';
print_r($_FILES);

print "</pre>";
*/
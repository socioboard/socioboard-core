
//$.getScript('/path/to/imported/script.js', function () {
//    // script is now loaded and executed.
//    // put your dependent JS here.
//});
var normalizedparametres = '';
var signatureBaseString = '';

var signature = '';
var authorizationHeader = '';


var importing = document.createElement('script');
importing.src = '../Contents/js/sha1.js';
document.head.appendChild(importing);

var imported = document.createElement('script');
imported.src = '../Contents/js/oauth.js';
document.head.appendChild(imported);

function getOauthSignature(consumerKey, consumerSecret, token, tokenSecret, method, url, parameters) {
    debugger;

    var accessor = { consumerSecret: consumerSecret
                   , tokenSecret: tokenSecret
    };
    var message = { method: method
                  , action: url
                  , parameters: OAuth.decodeForm(parameters)
    };

    message.parameters.push(["oauth_version", "1.0"]);
    message.parameters.push(["oauth_timestamp", OAuth.timestamp()]);
    message.parameters.push(["oauth_nonce", OAuth.nonce(11)]);
    message.parameters.push(["oauth_signature_method", "HMAC-SHA1"]);
    message.parameters.push(["oauth_consumer_key", consumerKey]);
    message.parameters.push(["oauth_token", token]);

    OAuth.SignatureMethod.sign(message, accessor);
    normalizedparametres = OAuth.SignatureMethod.normalizeParameters(message.parameters);
    signatureBaseString = OAuth.SignatureMethod.getBaseString(message);
    signature = OAuth.getParameter(message.parameters, "oauth_signature");
    debugger;
    authorizationHeader = OAuth.getAuthorizationHeader("", message.parameters);
    debugger;
    loadXMLDoc();
}



function loadXMLDoc() {
    try {


        $.ajax({
            type: "GET",
            beforeSend: function (request) {
                debugger;
                request.setRequestHeader("Authorization", authorizationHeader);
            },
            dataType: 'jsonp',
            url: "https://api.twitter.com/1.1/users/lookup.json",
            data: normalizedparametres,
            crossDomain: true,
            success: function (msg) {
                debugger;
            }
        });


    } catch (e) {
    alert(e);
    }













    try {
        debugger;
        var xmlhttp;
        if (window.XMLHttpRequest) {
            xmlhttp = new XMLHttpRequest();
        }
        else {
            xmlhttp = new ActiveXObject("Microsoft.XMLHTTP");
        }
        xmlhttp.onreadystatechange = function () {
            if (xmlhttp.readyState == 4 && xmlhttp.status == 200) {
                var rettt = xmlhttp.responseText;
            }
        }
        xmlhttp.open("GET", "https://api.twitter.com/1.1/users/lookup.json", true);
        xmlhttp.setRequestHeader("Authorization", authorizationHeader);
        xmlhttp.send("screen_name=babbi1988");
        debugger;
        var ret = xmlhttp.responseText;

    } catch (e) {
    alert(e);
    }

}






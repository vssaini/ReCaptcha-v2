/*---------------------------------------------
   HANDLE AJAX RELATED FUNCTIONS
   --------------------------------------------*/
var widgetId;

function HandleBegin()
{
    // Reset all
    $("#msgArea").html("");

    // Ref - http://fellowtuts.com/jquery/change-class-using-jquery/
    $("#msgArea").attr("class", "error"); // Change class irrespective of existing one
}

function HandleComplete()
{
    var msgArea = $("#msgArea");
    var msgAreaText = $("#hidMsgArea").val();
    
    if (/error/i.test(msgAreaText)) // Ref - http://stackoverflow.com/questions/3480771/how-to-see-if-string-contains-substring
    {
        msgArea.attr("class", "error");
    } else {
        msgArea.attr("class", "success");
    }
}

function HandleError(request, status, error)
{
    alert("Error occured while processing ajax request : " + request + "Error Status: " + status + " Error Detail: " + error);
}

function ShowReCaptcha()
{
    var reCaptchaPublicKey = $("#ReCaptchaPublicKey").val();

    if ($("#ReCaptchaPublicKey").length > 0)
    {
        // Ref - https://developers.google.com/recaptcha/docs/display#js_api
        widgetId = grecaptcha.render("captchaContainer", {
            'sitekey': reCaptchaPublicKey,
            'callback': function (response) { console.log(response); },
            'theme': "dark"
        });
    } else
    {
        console.log("ReCaptcha public key not found.");
    }
}


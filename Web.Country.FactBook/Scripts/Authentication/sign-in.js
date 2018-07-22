$(document).ready(function () {
    GetRememberedCrdentails();
});

function GetRememberedCrdentails() {
    var result = AjaxCall("/api/cookies", "", "GET");
    if (result != null) {
        $("#input-email").val(result.email);
        $("#input-password").val(result.password);
        $("#chk-rememberme").prop("checked", true);
    }
}

function ValidateSingInUser() {
    if ($("#input-email").val() == "") {
        $("#alert-error").html("Please enter username or email address");
        $("#alert-error").show();
        return false;
    }
    if ($("#input-password").val() == "") {
        $("#alert-error").html("Please enter password");
        $("#alert-error").show();
        return false;
    }
    $("#alert-error").hide();
    var data = {
        email: $("#input-email").val(),
        password: $("#input-password").val()
    }
    var rememberme = 0;
    if ($("#chk-rememberme").is(':checked')) {
        rememberme = 1;
    }
    $("#btn-sign-in").attr("disabled", "disabled");
    var result = AjaxCall("/api/authenticate/" + rememberme, JSON.stringify(data), "POST");
    if (result.id > 0) {
        window.location.href = "/dashbord";
    }
    else {
        $("#btn-sign-in").removeAttr("disabled");
        $("#alert-error").html("Invalid email address or password");
        $("#alert-error").show();
        return false;
    }
    return false;
}
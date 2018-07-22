$(document).ready(function () {
    localStorage.removeItem('user');
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

$("#btn-sign-in").click(function () {
    if ($("#input-email").val() == "") {
        $("#alert-error").html("Please enter email address");
        $("#alert-error").show();
        return false;
    }
    if (!ValidateEmail($("#input-email").val())) {
        $("#alert-error").html("Please enter valid email address");
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
    $("#btn-sign-in").button('loading');
    var result = AjaxCall("/api/authenticate/" + rememberme, JSON.stringify(data), "POST");
    if (result != null) {
        localStorage.setItem('user', JSON.stringify(result));
        window.location.href = "/dashbord";
    }
    else {
        $("#btn-sign-in").button('reset');
        $("#alert-error").html("Invalid email address or password");
        $("#alert-error").show();
        return false;
    }
    return false;
});
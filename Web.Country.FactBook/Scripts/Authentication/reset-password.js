$(document).ready(function () {
    ValidateResetUserDetails();
});

function ValidateResetUserDetails() {
    if (localStorage.getItem('user') != null) {
        var user = JSON.parse(localStorage.getItem('user'));
        var result = AjaxCall("/api/validate/reseruser/" + user.altId, "", "GET");
        if (result == null) {
            $("#alert-error").html("Invalid user details.");
            $("#alert-error").show();
        }
    }
    else {
        window.location.href = "/authentication/forgotpassword";
    }
}

$("#btn-reset-pwd").click(function () {
    var $btn = $(this);
    if ($("#input-password").val() == "") {
        $("#alert-error").html("Please enter password");
        $("#alert-error").show();
        return false;
    }
    if ($("#input-confirm-password").val() == "") {
        $("#alert-error").html("Please enter confirm password");
        $("#alert-error").show();
        return false;
    }
    if ($("#input-confirm-password").val() != $("#input-password").val()) {
        $("#alert-error").html("Please enter valid confirm password. Password mismatch.");
        $("#alert-error").show();
        return false;
    }
    $("#alert-error").hide();
    var user = JSON.parse(localStorage.getItem('user'));
    var data = {
        email: user.email,
        altId: user.altId,
        password: $("#input-password").val()
    }
    $btn.button('loading');
    var result = AjaxCall("/api/authenticate/resetpassword", JSON.stringify(data), "POST");
    if (result != null) {
        $("#alert-error").html("Your password is reseted successfully, please click here to <a href='/authentication'>login</a>.");
        $("#alert-error").show();
        return false;
    }
    else {
        $btn.button('reset');
        $("#alert-error").html("Password is not reseted. Please try again.");
        $("#alert-error").show();
        return false;
    }
    return false;
});
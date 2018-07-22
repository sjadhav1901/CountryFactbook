
$("#btn-forgot-pwd").click(function () {
    if ($("#input-email").val() == "") {
        $("#alert-error").html("Please enter email address");
        $("#alert-error").show();
        return false;
    }

    $("#alert-error").hide();
    $("#btn-forgot-pwd").button('loading'); // bootstrap loading
    var result = AjaxCall("/api/authenticate/forgot/" + $("#input-email").val(), "", "GET");
    if (result != null) {
        localStorage.setItem('user', JSON.stringify(result));
        window.location.href = "/authentication/resetpassword/" + result.altId;
    }
    else {
        $("#btn-forgot-pwd").button('reset');
        $("#alert-error").html("Invalid email address.");
        $("#alert-error").show();
        return false;
    }
    return false;
});
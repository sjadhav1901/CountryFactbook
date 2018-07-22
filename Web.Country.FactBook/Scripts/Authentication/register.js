
$("#btn-sign-up").click(function () {
    if ($("#input-first-name").val() == "") {
        $("#alert-error").html("Please enter first name");
        $("#alert-error").show();
        return false;
    }
    if ($("#input-last-name").val() == "") {
        $("#alert-error").html("Please enter last name");
        $("#alert-error").show();
        return false;
    }
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
        firstName: $("#input-first-name").val(),
        lastName: $("#input-last-name").val(),
        email: $("#input-email").val(),
        password: $("#input-password").val()
    }
    
    $("#btn-sign-up").button('loading');
    var result = AjaxCall("/api/authenticate/forgot/" + $("#input-email").val(), "", "GET");
    if (result == null) {
        var resultRegister = AjaxCall("/api/register", JSON.stringify(data), "POST");
        if (resultRegister != null) {
            $("#alert-error").html("Your account has been successfully registered. please click here to <a href='/'>Sign in</a>. ");
            $("#alert-error").show();
        }
        else {
            $("#btn-sign-up").button('reset');
            $("#alert-error").html("");
            $("#alert-error").show();
            return false;
        }
    }
    else {
        $("#btn-sign-up").button('reset');
        $("#alert-error").html("Email address already exists. please click here to <a href='/authentication'>Sign in</a>. ");
        $("#alert-error").show();
        return false;
    }
    return false;
});

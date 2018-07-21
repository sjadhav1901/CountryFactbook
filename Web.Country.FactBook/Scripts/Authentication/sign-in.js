
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
    var result = AjaxCall("/api/authenticate", JSON.stringify(data), "POST");
    if (result.id > 0) {
        alert("Hello");
    }
    else {
        $("#alert-error").html("Invalid email address or password");
        $("#alert-error").show();
        return false;
    }
}
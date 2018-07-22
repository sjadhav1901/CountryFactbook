function AjaxCall(url, data, method) {
    var ajaxResult = "";
    $.ajax({
        type: method,
        url: url,
        data: data,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: false,
        failure: function (data) {
            hideLoader();
            alert(data.responseText);
        }, //End of AJAX failure function  
        error: function (data) {
            hideLoader();
            alert(data.responseText);
        }, //End of AJAX error function  
        beforeSend: function () {
            showLoader();
        },
        success: function (data) {
            hideLoader();
            ajaxResult = data;
        }
    });
    return ajaxResult;
}

function showLoader() {
    $("#dvLoader").css("display", "");
}

function hideLoader() {
    setTimeout(function () {
        $("#dvLoader").css("display", "none");
    }, 1000);
}

function ValidateEmail(email) {
    var isNumber = email[0].match(/\d+/g);
    if (isNumber) {
        return false;
    }
    var expr = /^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$/;
    return expr.test(email);
};

function IsNumberKey(evt) {
    var charCode = (evt.which) ? evt.which : event.keyCode
    if (charCode > 31 && (charCode < 48 || charCode > 57))
        return false;

    return true;
}
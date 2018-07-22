
$(document).ready(function () {
    if (localStorage.getItem('user') != null) {
        $("#dvLoader").show();
        var user = JSON.parse(localStorage.getItem('user'));
        $.ajax({
            url: "http://countryfactbook.azurewebsites.net/api/authednavmenu/" + user.altId,
            type: "GET",
            cache: false,
            success: function (response, status, xhr) {
                var nvContainer = $('#div-authed-nav-menu');
                nvContainer.html(response);
                $("#dvLoader").hide();
            },
            error: function (XMLHttpRequest, textStatus, errorThrown) {
                var nvContainer = $('#div-authed-nav-menu');
                nvContainer.html(errorThrown);
                $("#dvLoader").hide();
            }
        });
    }
    else {
        window.location.href = "/";
    }
});

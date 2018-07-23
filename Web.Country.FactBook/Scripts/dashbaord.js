$(document).ready(function () {
    DashBoard();
});

function DashBoard() {
    if (localStorage.getItem('user') != null) {
        var user = JSON.parse(localStorage.getItem('user'));
        $("#div-recent").show();
        $("#div-favourite").hide();
        var result = AjaxCall("/api/dashboard/" + user.altId, "", "GET");
        if (result != null) {
            if (result.recentActivity != null && result.recentActivity.length > 0) {
                var sb = "";
                result.recentActivity.forEach(function (val, index) {
                    sb += "<div class=\"box mb-3\">" +
                        "<div class=\"col-sm-12\">" +
                        "<i class=\"icon-activity\"></i> " + val.name + "" +
                        "</div>" +
                        "<div class=\"col-sm-12 f6 text-gray\">" +
                        "" + val.description + "" +
                        "</div>" +
                        "</div>";
                });
                $("#div-activity").html(sb);
            }
            else {
                $("#div-activity").html("No recent activities");
            }

            if (result.country != null && result.country.length > 0) {
                var sbConutry = "";
                result.country.forEach(function (val, index) {

                    sbConutry += "<div class=\"box mb-3\">" +
                        "<div class=\"col-sm-12\">" +
                        "<i class=\"icon-activity\"></i> Country Name" +
                        "</div>" +
                        "<div class=\"col-sm-12 f6 text-gray\">" +
                        "" + val.name + "" +
                        "</div>" +
                        "<div class=\"col-sm-12\">" +
                        "<i class=\"icon-activity\"></i> Alpha Three Code" +
                        "</div>" +
                        "<div class=\"col-sm-12 f6 text-gray\">" +
                        "" + val.alphaThreeCode + "" +
                        "</div>" +
                        "<div class=\"col-sm-12\">" +
                        "<i class=\"icon-activity\"></i> Capital City" +
                        "</div>" +
                        "<div class=\"col-sm-12 f6 text-gray\">" +
                        "" + val.capital + "" +
                        "</div>" +
                        "<div class=\"col-sm-12\">" +
                        "<i class=\"icon-activity\"></i> Flag Image" +
                        "</div>" +
                        "<div class=\"col-sm-12 f6 text-gray\">" +
                        "<img src='" + val.flags + "' style='width: 50px;'>" +
                        "</div>" +
                        "</div>";
                });
                $("#div-favour").html(sbConutry);
            }
            else {
                $("#div-favour").html("No favourites available");
            }

            var userProfile = "";
            var role = result.user.roleId == 1 ? "Admin" : result.user.roleId == 2 ? "Editor" : "User";
            userProfile += "<p><i class=\"fa fa-user\" aria-hidden=\"true\"></i> " + result.user.firstName + " " + result.user.lastName + "</p>";
            userProfile += "<p><i class=\"fa fa-envelope\" aria-hidden=\"true\"></i> " + result.user.email + "</p>";
            userProfile += "<p><i class=\"fa fa-users\" aria-hidden=\"true\"></i> " + role + "</p>";
            $("#div-user-profile").html(userProfile);
        }
    }
    else {
        window.location.href = "/";
    }
}

function ShowHide(div1, div2) {
    $("#" + div1).show();
    $("#" + div2).hide();
    $("#" + div2).removeClass("selected");
    $("#" + div1).addClass("selected");
}


function toggleClass(div1, div2) {
    $("#" + div2).removeClass("selected");
    $("#" + div1).addClass("selected");
}
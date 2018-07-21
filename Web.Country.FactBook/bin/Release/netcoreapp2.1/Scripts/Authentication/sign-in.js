$(document).ready(function () {
    ValidateSingInUser();
});

function ValidateSingInUser() {
    var result = AjaxCall("/api/authenticate", "", "GET");
    console.log(result);
}
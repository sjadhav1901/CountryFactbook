$("#tags").autocomplete({
    source: function (request, response) {
        $.ajax({
            type: "GET",
            url: "/api/search/countries/" + request.term,
            data: "",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (result) {
                response($.map(result, function (item) {
                    if (item.name.toLowerCase().indexOf(request.term.toLowerCase()) != -1) {
                        return {
                            label: item.name,
                            val: item.altId
                        }
                    }
                }))
            },
            error: function (response) {
                //alert(response.responseText);
            },
            failure: function (response) {
                //alert(response.responseText);
            }
        });
    },
    select: function (e, i) {
        $("#hdnCountryId").val(i.item.val);
        GetCountryDetails();
    },
    minLength: 0
}).focus(function () {
    if ($(this).autocomplete("widget").is(":visible")) {
        return;
    }
});

function GetCountryDetails() {
    $("#div-details").hide();
    var result = AjaxCall("/api/get/countries/" + $("#hdnCountryId").val(), "", "GET");
    if (result != null && result.country != null && result.country.id > 0) {
        $("#div-details").show();
        var tBody = "";
        var sb = "<div class=\"col-sm-12\">" +
            " <i class=\"icon-activity\"></i> Country Name" +
            "  </div>" +
            " <div class=\"col-sm-12 f6 text-gray\">" + result.country.name +
            " </div>";
        sb += "<div class=\"col-sm-12\">" +
            " <i class=\"icon-activity\"></i> Alpha Two Code" +
            "  </div>" +
            " <div class=\"col-sm-12 f6 text-gray\">" + result.country.alphaTwoCode +
            " </div>";
        sb += "<div class=\"col-sm-12\">" +
            " <i class=\"icon-activity\"></i> Alpha Three Code" +
            "  </div>" +
            " <div class=\"col-sm-12 f6 text-gray\">" + result.country.alphaThreeCode +
            " </div>";
        sb += "<div class=\"col-sm-12\">" +
            " <i class=\"icon-activity\"></i> Capital City" +
            "  </div>" +
            " <div class=\"col-sm-12 f6 text-gray\">" + result.country.capital +
            " </div>";
        sb += "<div class=\"col-sm-12\">" +
            " <i class=\"icon-activity\"></i> Flag Image" +
            "  </div>" +
            " <div class=\"col-sm-12 f6 text-gray\"><img src='" + result.country.flags + "' style='width: 100px;'>" +
            " </div>";
        $("#div-country").html(sb);
        var languageMappings = result.countryLanguageMappings.filter(function (el) {
            return el.countryId == result.country.id && el.isEnabled == true;
        });
        var strLanguages = "";
        languageMappings.forEach(function (valLanguage, index) {
            var objLanguage = result.languages.filter(function (el) {
                return el.id == valLanguage.languageId;
            });
            strLanguages += objLanguage[0].name + ", ";
        });
        if (strLanguages != "") {
            $("#dvLanguages").html(strLanguages.substring(0, strLanguages.length - 2));
        }
        else {
            $("#dvLanguages").html("No language details available.");
        }
        var currenyMappings = result.countryCurrencyMappings.filter(function (el) {
            return el.countryId == result.country.id && el.isEnabled == true;
        });
        var strCurrencies = "";
        currenyMappings.forEach(function (valCurrency, index) {
            var objCurrency = result.currencies.filter(function (el) {
                return el.id == valCurrency.currencyId;
            });
            strCurrencies += objCurrency[0].code + ", ";
        });
        if (strCurrencies != "") {
            $("#dvCurrencies").html(strCurrencies.substring(0, strCurrencies.length - 2));
        }
        else {
            $("#dvCurrencies").html("Np currency details available.");
        }
    }
}


$("#btn-favorite").click(function () {
    var user = JSON.parse(localStorage.getItem('user'));
    var data = {
        countryAltId: $("#hdnCountryId").val(),
        createdBy: user.altId
    }
    var result = AjaxCall("/api/favorite", JSON.stringify(data), "POST");
    if (result != null) {
        alert("Country is added successfully to favourite list");
    }
    else {
        alert("Please try again. Country is not added to favourite list")
        return false;
    }
    return false;
});
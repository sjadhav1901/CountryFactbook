$(document).ready(function () {
    GetAllCountries();
});

function GetAllCountries() {
    var result = AjaxCall("/api/all/countries", "", "GET");
    if (result != null) {
        var tBody = "";
        result.countries.forEach(function (val, index) {
            var languageMappings = result.countryLanguageMappings.filter(function (el) {
                return el.countryId == val.id && el.isEnabled == true;
            });
            var strLanguages = "";
            languageMappings.forEach(function (valLanguage, index) {
                var objLanguage = result.languages.filter(function (el) {
                    return el.id == valLanguage.languageId;
                });
                strLanguages += objLanguage[0].name + ", ";
            });

            var currenyMappings = result.countryCurrencyMappings.filter(function (el) {
                return el.countryId == val.id && el.isEnabled == true;
            });
            var strCurrencies = "";
            currenyMappings.forEach(function (valCurrency, index) {
                var objCurrency = result.currencies.filter(function (el) {
                    return el.id == valCurrency.currencyId;
                });
                strCurrencies += objCurrency[0].code + ", ";
            });

            tBody += "<tr>";
            tBody += "<td>" + val.name + "</td>";
            tBody += "<td>" + val.alphaTwoCode + "</td>";
            tBody += "<td>" + val.alphaThreeCode + "</td>";
            tBody += "<td>" + val.capital + "</td>";
            tBody += "<td>" + strLanguages.substring(0, strLanguages.length - 2) + "</td>";
            tBody += "<td>" + strCurrencies.substring(0, strCurrencies.length - 2) + "</td>";
            tBody += "<td><img src='" + val.flags + "' style='width: 40px;'></td>";
            tBody += "<td>" + val.timeZone + "</td>";
            tBody += "</tr>";
        });
        $("#table-body").html(tBody);
        $('#example').DataTable();
    }
}
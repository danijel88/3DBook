$(document).ready(function () {
    $("#3dCode, #machine, #plm, #itemType").keyup(function () {
        var getFilter = function (el, selector) {
            var txt = $(el).val().toLowerCase();
            return txt
                ? function (i, p) { return $(p).find(selector).text().toLowerCase().indexOf(txt) !== -1; }
                : function (i, p) { return true; };
        };

        $('.toHide').hide()
            // .filter(getFilter('#srcInternalCode', '.panel-heading'))
            .filter(getFilter('#3dCode', '#dest3DCode'))
            .filter(getFilter('#plm', '#destPlm'))
            .filter(getFilter('#machine', '#destMachine'))
            .filter(getFilter('#itemType', '#destItemType'))
            .show();
    });
});
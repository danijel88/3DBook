$(document).ready(function () {
    $("#folds, #machine, #enter, #exit").keyup(function () {
        var getFilter = function (el, selector) {
            var txt = $(el).val().toLowerCase();
            return txt
                ? function (i, p) { return $(p).find(selector).text().toLowerCase().indexOf(txt) !== -1; }
                : function (i, p) { return true; };
        };

        $('.toHide').hide()
           // .filter(getFilter('#srcInternalCode', '.panel-heading'))
            .filter(getFilter('#machine', '#destMachine'))
            .filter(getFilter('#folds', '#destFolds'))
            .filter(getFilter('#enter', '#destEnter'))
            .filter(getFilter('#exit', '#destExit'))
            .show();
    });
});
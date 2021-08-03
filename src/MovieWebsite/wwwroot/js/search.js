/* Programmed by Tushar on 2021-08-03 */

var tk = {

    filters : {
        years  : ko.observableArray([]),
        genres : ko.observableArray([])
    }
};

tk.getYears = function (callback) {
    jQuery.ajax({
        type: "GET",
        url: "/Home/GetYears",
        success: function (response) {
            tk.filters.years(response);
        },
        failure: function (response) {
            console.warn("GetYears failure with: " + response);
        },
        error: function (response) {
            console.error("GetYears error with: " + response);
        }
    }).done(function (msg) {
        if (callback) {
            callback();
        }
    });
};

tk.getGenres = function (callback) {
    jQuery.ajax({
        type: "GET",
        url: "/Home/GetGenres",
        success: function (response) {
            tk.filters.genres(response);
        },
        failure: function (response) {
            console.warn("GetGenres failure with: " + response);
        },
        error: function (response) {
            console.error("GetGenres error with: " + response);
        }
    }).done(function (msg) {
        if (callback) {
            callback();
        }
    });
};

jQuery(document).ready(function () {

    ko.applyBindings(tk.filters);

    tk.getYears(null);

    tk.getGenres(null);

});

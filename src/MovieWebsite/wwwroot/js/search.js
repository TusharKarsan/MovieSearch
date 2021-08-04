/* Programmed by Tushar on 2021-08-03 */

var tk = {

    searchBox: ko.observable(""),

    filters : {
        years  : ko.observableArray([]),
        genres : ko.observableArray([])
    },

    searchResult: ko.observableArray([])
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

tk.getResults = function (theControl) {

    var genres = [];
    var filterGenre = "#filterGenre input[type='checkbox']:checked";

    var years = [];
    var filterYear = "#filterYear input[type='checkbox']:checked";

    jQuery(filterGenre).each(function (i2, e2) {
        genres.push(e2.value);
    });

    jQuery(filterYear).each(function (i2, e2) {
        years.push(Number(e2.value));
    });

    var searchCriteria = tk.searchBox();

    tk.searchResult([]);
    if (searchCriteria.length == 0) {
        return 1;
    }

    var payload = {
        search: searchCriteria,
        genres: genres,
        years: years
    };

    jQuery.ajax({
        type: "POST",
        url: "/Home/FindMovies",
        data: payload,
        success: function (response) {
            tk.searchResult(response);
        },
        failure: function (response) {
            console.warn("GetGenres failure with: " + response);
        },
        error: function (response) {
            console.error("GetGenres error with: " + response);
        }
    });
};

jQuery(document).ready(function () {

    ko.applyBindings(tk.filters);

    tk.getYears(null);

    tk.getGenres(null);

});

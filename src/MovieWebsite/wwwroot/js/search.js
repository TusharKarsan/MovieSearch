/* Programmed by Tushar on 2021-08-03 */

var tk = {

};

tk.getYears = function (callback) {
    jQuery.ajax({
        type: "GET",
        url: "/Home/GetYears",
        success: function (response) {
            //sd.categories.categories(response.categories);
            console.log("got it");
            console.log(response);
        },
        failure: function (response) {
            console.warn("failure with: " + response);
        },
        error: function (response) {
            console.error("error with: " + response);
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
            //sd.categories.categories(response.categories);
            console.log("got it");
            console.log(response);
        },
        failure: function (response) {
            console.warn("failure with: " + response);
        },
        error: function (response) {
            console.error("error with: " + response);
        }
    }).done(function (msg) {
        if (callback) {
            callback();
        }
    });
};

jQuery(document).ready(function () {

    tk.getYears(null);

    tk.getGenres(null);

});

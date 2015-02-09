
$(document).ready(function () {
    var isAdmin = $.ajax({
        type: 'Get',
        url: '/User/isAdmin',
        cache: false
    });

    isAdmin.done(function (result) {
        if (result == "False") {
            $("#UserLink").hide();
        }
        else {
            $("#UserLink").show();
        }
    });

    isAdmin.fail(function (xhr, ajaxOptions, thrownError) {
        console.log(thrownError);
        console.log(xhr);
    });
});

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


    time();

});


setInterval(function () {
    time();
}, 10000)

function time(){
    var timeLog = $.ajax({
        url: '/TimeEntry/getHours',
        cache: false
    });

    timeLog.done(function (result) {
        console.log(result)
        $("#hoursLogged").html(result);

    });

    timeLog.fail(function (xhr, ajaxOptions, thrownError) {
        console.log(thrownError);
        console.log(xhr);
    });
};
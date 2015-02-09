/// <reference path="../jquery-1.10.2.intellisense.js" />

$(function () {
    setInterval(function () {
        var divUtc = $('#divUTC');
        var divLocal = $('#divLocal');
        //put UTC time into divUTC  
        divUtc.text(moment.utc().format('YYYY-MM-DD HH:mm:ss'));

        //get text from divUTC and conver to local timezone  
        var localTime = moment.utc(divUtc.text()).toDate();
        localTime = moment(localTime).format('YYYY-MM-DD HH:mm:ss');
        divLocal.text(localTime);
    }, 1000);
});

$(document).ready(function () {
    partial();
    $.ajax({
        type: 'Get',
        url: '/TimeEntry/getClockBool',
        cache: false,
        success: function (result) {
            var temp = result;
            var what = (temp != true);
            if (temp != "True") {
                $("#punchOut").hide();
                $("#punchIn").hide();
                $('#punchIn').toggle();
                
            }
            else {
                $("#punchIn").hide();
                $("#punchOut").hide();
                $('#punchOut').toggle();
                
            }

        }
    });
    partial();
});

$(document).ready(function () {

    $("#punchIn").click(function () {


        var timeIn = $('#timeIn');
        timeIn.text(moment.utc().format('YYYY-MM-DD HH:mm:ss'));


        ajaxPost();


        $("#punchOut").toggle();

        $("#punchIn").toggle();

        var enableSubmit = function (ele) {
            $(ele).removeAttr("disabled");
        }

        $("#punchOut").click(function () {
            var that = this;
            $(this).attr("disabled", true);
            setTimeout(function () { enableSubmit(that) }, 80000);
        });
        $("#punchIn").click(function () {
            var that = this;
            $(this).attr("disabled", true);
            setTimeout(function () { enableSubmit(that) }, 80000);
        });

    });
});


$(document).ready(function () {
    $("#punchOut").click(function () {
        partial();
        var timeOut = $('#timeOut');
        timeOut.text(moment.utc().format('YYYY-MM-DD HH:mm:ss'));


        ajaxPost();



        $("#punchIn").toggle();

        $("#punchOut").toggle();

        var enableSubmit = function (ele) {
            $(ele).removeAttr("disabled");
        }

        $("#punchIn").click(function () {
            var that = this;
            $(this).attr("disabled", true);
            setTimeout(function () { enableSubmit(that) }, 80000);
        });
        $("#punchOut").click(function () {
            var that = this;
            $(this).attr("disabled", true);
            setTimeout(function () { enableSubmit(that) }, 80000);
        });
    });

});

function ajaxPost() {
    var table = $("#timesTable");
    $.ajax({
        type: 'Post',
        data: {
            "timeDate": moment.utc().format('YYYY-MM-DD HH:mm:ss').toString()
        },
        url: '/TimeEntry/PunchCard',
        cache: false,
        success: function (result) {
            table.html(result);
        }
    });

}



function partial() {
    console.log("rendering partial");
    var table = $("#timesTable");

    var ajaxHandler = $.ajax({
        type: 'Get',
        url: '/TimeEntry/getPartial',
        cache: false,
    });
    ajaxHandler.done(function (result) {


        table.html(result);
    });
    ajaxHandler.fail(function (xhr, ajaxOptions, thrownError) {

        alert('Fail');
    });
}

setInterval(function () {
    function r(el, deg) {
        el.setAttribute('transform', 'rotate(' + deg + ' 50 50)')
    }
    var d = new Date()
    r(sec, 6 * d.getSeconds())
    r(min, 6 * d.getMinutes())
    r(hour, 30 * (d.getHours() % 12) + d.getMinutes() / 2)
}, 1000)
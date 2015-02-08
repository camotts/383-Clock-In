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
            console.log(temp);
            var what = (temp != true);
            console.log(what);
            if (temp != "True") {
                console.log("Clock In");
                $("#punchOut").hide();
                $("#punchIn").hide();
                $('#punchIn').toggle();
                
            }
            else {
                console.log("Clock OUt");
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

        console.log("Out Hit");
        var timeIn = $('#timeIn');
        timeIn.text(moment.utc().format('YYYY-MM-DD HH:mm:ss'));


        ajaxPost();

        console.log("Punch In button click");
        console.log("Toggle punch put on");
        $("#punchOut").toggle();
        console.log("Toggle Punch In off");
        $("#punchIn").toggle();

        var enableSubmit = function (ele) {
            $(ele).removeAttr("disabled");
        }

        $("#punchOut").click(function () {
            var that = this;
            $(this).attr("disabled", true);
            setTimeout(function () { enableSubmit(that) }, 60000);
        });

    });
});


$(document).ready(function () {
    $("#punchOut").click(function () {
        partial();
        console.log("Out Hit");
        var timeOut = $('#timeOut');
        timeOut.text(moment.utc().format('YYYY-MM-DD HH:mm:ss'));


        ajaxPost();


        console.log("Punch out button click");
        console.log("Toggle punch in on");

        $("#punchIn").toggle();
        console.log("Toggle punch out off");
        $("#punchOut").toggle();

        var enableSubmit = function (ele) {
            $(ele).removeAttr("disabled");
        }

        $("#punchIn").click(function () {
            var that = this;
            $(this).attr("disabled", true);
            setTimeout(function () { enableSubmit(that) }, 60000);
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
        console.log(result);

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
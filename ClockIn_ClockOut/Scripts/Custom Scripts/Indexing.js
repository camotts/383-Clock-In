
$(document).ready(function () {

    var count = countRows();    
    console.log("after countrows " + count);

    

    for (var i = 0; i < count; i++) {
        console.log("LOOOOOOOP")
        console.log(i);
        renderPartial(i + 1);
        changeNames(i + 1);
        timeId(i + 1);
    }

});

function renderPartial(id) {
    console.log("rendering partial");
    console.log(id);
    var table = $("#html" + id);
    var ajaxHandler = $.ajax({
        type: 'Get',
        url: '/TimeEntry/getPartialId',
        data:{
            "id": id.toString()
        },
        cache: false,
    });
    console.log(id.toString());
    ajaxHandler.done(function (result) {
        console.log(result)
        table.html(result);
        
    });
    ajaxHandler.fail(function (xhr, ajaxOptions, thrownError) {
        console.log(xhr)
        console.log(ajaxOptions)
        console.log(thrownError)
        alert('Fail');
    });
}

function countRows() {
    var toReturn;
    $.ajax({
        url: '/User/getCount',
        cache: true,
        async: false,
        success: function (result) {
            console.log("I ajax" + result);
            toReturn = result;
        }
    });
    
    return toReturn;
}

function changeNames(id) {
    var table = $("#collapseName" + id);
    var ajaxHandler = $.ajax({
        type: 'Get',
        url: '/TimeEntry/getName',
        data: {
            "id": id.toString()
        },
        cache: false,
    });
    console.log(id.toString());
    ajaxHandler.done(function (result) {
        console.log(result)
        table.html(result);

    });
    ajaxHandler.fail(function (xhr, ajaxOptions, thrownError) {
        console.log(xhr)
        console.log(ajaxOptions)
        console.log(thrownError)
        alert('Fail');
    });
}

function timeId(id) {
    var timeLog = $.ajax({
        url: '/TimeEntry/getHoursId',
        data:{
            "id": id.toString()
        },
        cache: false
    });

    timeLog.done(function (result) {
        console.log(result)
        $("#collapseTime" + id).html("Total Hours: " + result);

    });

    timeLog.fail(function (xhr, ajaxOptions, thrownError) {
        console.log(thrownError);
        console.log(xhr);
    });
};
var count = 0;

$(document).ready(function () {
    countRows();
    count = 1;
    console.log(count);

    

    for (var i = 0; i < count; i++) {
        console.log("LOOOOOOOP")
        console.log(i);
        renderPartial(i + 1);
    }

});

function renderPartial(id) {
    console.log("rendering partial");
    console.log(id);
    var table = $("#html" + id);
    var ajaxHandler = $.ajax({
        type: 'Get',
        url: '/TimeEntry/getPartial',
        data:{
            "id": id.toString()
        },
        cache: false,
    });
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
    $.ajax({
        url: '/User/getCount',
        cache: false,
        success: function (result) {
            console.log(result);
            count = result;
        }
    });
}
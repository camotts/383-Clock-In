/// <reference path="../jquery-1.10.2.intellisense.js" />

    $(function () {
        setInterval(function () {
            var divUtc = $('#divUTC');
            var divLocal = $('#divLocal');  
            //put UTC time into divUTC  
            divUtc.text(moment.utc().format('YYYY-MM-DD HH:mm:ss'));

            //get text from divUTC and conver to local timezone  
            var localTime  = moment.utc(divUtc.text()).toDate();
            localTime = moment(localTime).format('YYYY-MM-DD HH:mm:ss');
            divLocal.text(localTime);        
        },1000);
    });

    $(document).ready(function () {
        partial();
            $.ajax({
                type: 'Get',
                url: '/TimeEntry/getClockBool',
                cache: false,
                success: function (result) {
                    var temp = result;
                    console.log(temp)
                    if (temp!=true) {
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


            var timeIn = $('#timeIn');
            timeIn.text(moment.utc().format('YYYY-MM-DD HH:mm:ss'));

            

            
            



            ajaxPost();

            console.log("Punch In button click");
                console.log("Toggle punch put on");
                $("#punchOut").toggle();
                console.log("Toggle Punch In off");
                $("#punchIn").toggle();
                
                partial();
            
        });

        $("#punchOut").click(function () {
            console.log("Out Hit");
            var timeOut = $('#timeOut');
            timeOut.text(moment.utc().format('YYYY-MM-DD HH:mm:ss'));

            

            ajaxPost();

            console.log("Punch out button click");
            console.log("Toggle punch in on");

            $("#punchIn").toggle();
            console.log("Toggle punch out off");
            $("#punchOut").toggle();
                

            partial();
        });
        
    });

    function ajaxPost() {
        $.ajax({
            type: 'Post',
            url: '/TimeEntry/PunchCard',
            cache: false,
            success: function (result) {
                console.log("post post");
            }
        });
    }

 

    function partial() {
        var table = $("#timesTable");
        console.log("hit after var table");
        var ajaxHandler = $.ajax({
            type: 'Get',
            url: '/TimeEntry/getPartial',
            cache: false
        });
        ajaxHandler.done(function (result) {
            console.log(result);
            table.html(result);
        });
        ajaxHandler.fail(function (xhr, ajaxOptions, thrownError) {
            console.log(thrownError);
            console.log(xhr);
            alert('Fail');
        });
    }

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
        $("#punchIn").click(function () {
            console.log("In Hit");

            var timeIn = $('#timeIn');
            timeIn.text(moment.utc().format('YYYY-MM-DD HH:mm:ss'));

            console.log("pre post");
            ajaxPost();

            

            

            
            

            
        });

        $("#punchOut").click(function () {
            console.log("Out Hit");
            var timeOut = $('#timeOut');
            timeOut.text(moment.utc().format('YYYY-MM-DD HH:mm:ss'));

            
            ajaxPost();

        });
    });

    function ajaxPost() {
        $.ajax({
            type: 'Post',
            url: '/TimeEntry/PunchCard',
            cache: false,
            success: function (result) {
                console.log("post post");
                console.log("pre get");
                ajaxGet();
            }
        });
    }

    function ajaxGet() {
        $.ajax({
            type: 'Get',
            url: '/TimeEntry/PunchCard',
            cache: false,
            success: function (result) {
                console.log(result);
            }
        });
    }
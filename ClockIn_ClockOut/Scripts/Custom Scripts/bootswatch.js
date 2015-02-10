
try{
    var theme = JSON.parse(localStorage.getItem("savedTheme"));
    var pageLoad = false;
} catch (ex) {
    var theme = null;
    var pageload = true;
}


//Initial load of the api
$.get("http://api.bootswatch.com/3/", function (data) {
    var themes = data.themes;
    var select = $("#API-DropDown");

    themes.forEach(function (value, index) {
        select.append($("<option />")
                .val(index)
                .text(value.name));
    });

    //put the default value of the theme if the theme is null
    if (theme == null) {
        document.getElementById("API-DropDown").selectedIndex = "6";
    }

    select.change(function () {
        if (pageLoad)
        {
            var theme1 = themes[$(this).val()];
            localStorage.setItem("savedTheme", JSON.stringify(theme1));
            $("link").attr("href", theme1.css);
        }
        else
        {
            pageLoad = true;
            $("link").attr("href", theme.css);
        }
    }).change();
}, "json")


//For the buttons to change the themes
function change(id) {
    document.getElementById("API-DropDown").selectedIndex = id;

    $.get("http://api.bootswatch.com/3/", function (data) {
        var themes = data.themes;
        var select = $("#API-DropDown");

        select.change(function () {
            if (pageLoad) {
                var theme1 = themes[$(this).val()];
                localStorage.setItem("savedTheme", JSON.stringify(theme1));
                $("link").attr("href", theme1.css);
            }
            else {
                pageLoad = true;
                $("link").attr("href", theme.css);
            }
        }).change();

        themes.forEach(function (value, index) {
            select.append($("<option />")
                    .val(index)
                    .text(value.name));
        });

    }, "json")
}
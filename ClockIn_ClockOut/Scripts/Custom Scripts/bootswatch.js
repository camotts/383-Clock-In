
var theme = JSON.parse(localStorage.getItem("savedTheme"));
var pageLoad = false;

$.get("http://api.bootswatch.com/3/", function (data) {
    var themes = data.themes;
    var select = $("select");

    select.show();
   
    themes.forEach(function (value, index) {
        select.append($("<option />")
                .val(index)
                .text(value.name));
    });

        select.change(function () {
        if (pageLoad) {
            var theme1 = themes[$(this).val()];
            localStorage.setItem("savedTheme", JSON.stringify(theme1));
            $("link").attr("href", theme1.css);
        }
        else {
            pageLoad = true;
            console.log(theme.css);
            $("link").attr("href", theme.css);
        }
    }).change();

}, "json").fail(function () {

});

//For the buttons to change the themes
function change(id) {
    console.log("Hit");
    document.getElementById("API-DropDown").selectedIndex = id;
}
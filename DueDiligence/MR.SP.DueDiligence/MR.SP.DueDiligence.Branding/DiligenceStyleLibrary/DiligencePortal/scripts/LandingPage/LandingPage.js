$(function () {
    $("tr[class='ms-viewheadertr ms-vhltr']").css("background-color", "#1780C7");
    $("a[class='ms-headerSortTitleLink']").css("color", "white");
    $("div.ms-vh-div").not($("a[class='ms-headerSortTitleLink']")).css("color", "white");


    $(".ms-toolbar").find("a:contains(Data & Appearance)").parents("table.ms-menutoolbar").hide();
})
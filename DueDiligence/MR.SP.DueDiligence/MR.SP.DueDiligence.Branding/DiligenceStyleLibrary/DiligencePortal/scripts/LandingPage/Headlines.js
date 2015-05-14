function formattedDate(date) {
    var d = new Date(date || Date.now()),
        month = '' + (d.getMonth() + 1),
        day = '' + d.getDate(),
        year = d.getFullYear();

    if (month.length < 2) month = '0' + month;
    if (day.length < 2) day = '0' + day;

    return [month, day, year].join('/');
}

$(document).ready(function () {
    var content = "";
    var query = "<Query><Where><Eq><FieldRef Name='_ModerationStatus' /><Value Type='ModStat'>0</Value></Eq></Where></Query>";
    $().SPServices({
        operation: "GetListItems",
        async: false,
        listName: "Headlines",
        CAMLQuery: query,
        completefunc: function (xData, Status) {
            $(xData.responseXML).SPFilterNode("z:row").each(function () {
                content += ("<p>" + formattedDate(($(this).attr("ows_Created").substring(0, 11))) + " : " + $(this).attr("ows_Title") + "</p>");

            });
        }
    });
    $("#tabs-1").children().html(content);



});

$(document).ready(function () {
    var count = 0;
    $().SPServices({
        operation: "GetListItems",
        async: false,
        listName: "DueDiligenceProjects",
        completefunc: function (xData, Status) {
            $(xData.responseXML).SPFilterNode("z:row").each(function () {
                count++;
            });
        }

    });
    $(".count").html(count + "<div class=\"txt\">Projects to Date</div>");
});

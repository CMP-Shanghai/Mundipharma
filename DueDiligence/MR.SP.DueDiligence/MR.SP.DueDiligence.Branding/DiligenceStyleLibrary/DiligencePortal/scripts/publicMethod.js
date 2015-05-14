function GetCurrentWebUrl() {
    if (dd_global_variable_web_url != "/") {
        return dd_global_variable_web_url;
    }
    else { return "" }

}

function OpenDDLibraryFile(listID, fileName, absUrl) {
    var fileUrl = absUrl;
    //fileUrl = GetCurrentWebUrl() + "/_layouts/15/WopiFrame2.aspx?sourcedoc=%7B" + listID + "%7D&file=" + fileName + "&action=default";
    window.location.href = fileUrl;
    return false;
    //alert(listID + "---" + fileName);
}

//hide the link of the navigation
jQuery(document).ready(function () {
    jQuery(".static.menu-item.ms-core-listMenu-item.ms-displayInline.ms-navedit-linkNode").filter(function () { return jQuery(this).text() == "Projects" || jQuery(this).text() == "Configuration" }).attr("href", "#");
    //chart style
    jQuery(".ms-toolbar").find("a:contains(Data & Appearance)").parents("table.ms-menutoolbar").hide();
});

jQuery(document).ready(function () { jQuery(".static").filter(function () { return (jQuery(this).text() == "Projects" || jQuery(this).text() == "Configuration") }).attr("href", "#"); });
jQuery(document).ready(function () { jQuery(".static.selected").filter(function () { return (jQuery(this).text() == "Projects" || jQuery(this).text() == "Configuration") }).attr("href", "#"); });
jQuery(document).ready(function () { jQuery(".static.menu-item.ms-core-listMenu-item.ms-displayInline.ms-navedit-linkNode").filter(function () { return (jQuery(this).text() == "Projects" || jQuery(this).text() == "Configuration") }).attr({ style: "cursor:default" }); });
jQuery(document).ready(function () {
    jQuery.each(jQuery('.ms-listviewtable'), function () {
        jQuery(this).children().children("tr:gt(0)").attr("class", "ms-alternatingstrong");
    });
});

//homepage style
jQuery(document).ready(function () {
    jQuery("div[id$='QuickLaunchMenu'] > ul > li > a").css("background-color", "#1780C7");
    jQuery("div[id$='QuickLaunchMenu'] > ul > li > a").css("color", "#FFF");
    jQuery("div[id$='QuickLaunchMenu'] > ul > li > a").css("font-weight", "bold");
    jQuery("div[id$='QuickLaunchMenu'] > ul > li > a").css("margin-bottom", "1px");


    //documentUpload

    if (window.location.href.indexOf("ddupload=1") != -1) {
        //$("table.ms-descriptiontext tbody tr:eq(3)").hide();
        $("tr#ctl00_PlaceHolderMain_ctl04").hide();
    }


});



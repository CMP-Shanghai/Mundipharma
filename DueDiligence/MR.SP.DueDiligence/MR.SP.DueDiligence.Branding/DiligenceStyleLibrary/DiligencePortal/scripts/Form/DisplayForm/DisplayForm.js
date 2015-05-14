
jQuery(function () {
    //Top documentlibrary
    jQuery("tr[class='ms-viewheadertr ms-vhltr']").css("background-color", "#1780C7");
    jQuery("a[class='ms-headerSortTitleLink']").css("color", "white");
    jQuery("div.ms-vh-div").not(jQuery("a[class='ms-headerSortTitleLink']")).css("color", "white");
    jQuery(".ms-viewheadertr").css("background-color", "#1780C7");
    jQuery("div[ctxNum='1']").parent().css("color", "white");
    jQuery("div[ctxNum='1']").css("font-weight", "bold");
    jQuery(".ms-viewheadertr").css("border", "1px solid #666");

    //jQuery("#Ribbon\\.ListForm\\.Display-title").hide();
    //jQuery("#Ribbon\\.ListForm\\.Display").hide();

    //load field
    jQuery(".UDDisplayForm tbody tr").each(function () {
        //alert("0");
        jQuery(this).find("td").each(function () {
            //alert()
            if (jQuery(this).find("h3").find("nobr").text() == "Need External Participants") {
                //alert(jQuery(this).parent().children("td:eq(1)").text());
                //alert(jQuery(this).parent().children("td:eq(1)").text().toString().indexOf("No"))
                //alert(jQuery(this).parent().children("td:eq(1)").text().toString().indexOf("No") != -1);
                if (jQuery(this).parent().children("td:eq(1)").text().toString().indexOf("No") != -1)
                { jQuery(this).parent().children("td:eq(2)").hide(); jQuery(this).parent().children("td:eq(3)").hide(); }
            }
        })


    })
    
    jQuery("td.lu").each(function() {
        var tdText = jQuery(this).children("a").text();
        jQuery(this).text(tdText);
    })



})


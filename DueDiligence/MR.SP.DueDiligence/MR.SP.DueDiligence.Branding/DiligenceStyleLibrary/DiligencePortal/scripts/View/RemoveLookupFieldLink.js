jQuery(function(){

    jQuery(".ms-listviewtable").each(function () {


        jQuery(this).find("thead th").each(function (j) {

            var typeName = jQuery(this).children("div").attr("fieldtype");
            //alert(typeName);
            if (typeName == "LookupMulti" || typeName == "Lookup") {
                jQuery("table.ms-listviewtable").find(".ms-itmhover").each(function () { var tdText = jQuery(this).children("td:eq(" + j + ")").text(); jQuery(this).children("td:eq(" + j + ")").html("<span>" + tdText + "</span>") })
            }


        })

        jQuery(this).find("tbody tr th").each(function (j) {

            var typeName = jQuery(this).children("div").attr("fieldtype");
            //alert(typeName);
            if (typeName == "LookupMulti" || typeName == "Lookup") {
                jQuery("table.ms-listviewtable tbody tr:not(0)").each(function () { var tdText = jQuery(this).children("td:eq(" + j + ")").text(); jQuery(this).children("td:eq(" + j + ")").html("<span>" + tdText + "</span>") })
            }


        })



    })
    



})

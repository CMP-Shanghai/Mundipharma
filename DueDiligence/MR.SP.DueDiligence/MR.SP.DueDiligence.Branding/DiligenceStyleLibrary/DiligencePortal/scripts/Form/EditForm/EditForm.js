// you can change this to control the fields you want to delete
var removeValue = "CFPA completion due,TPP completion due,SWOT completion due,F2F agenda to be sent out,Meeting minutes to be reviewed,DD Report Draft 2 completion due";




jQuery(function () {


    jQuery("span[title='This is a required field.']").css("color", "red");
    jQuery("td[class='ms-formbody']").css("padding-left", "10px");

    jQuery(".ms-viewheadertr").css("background-color", "#1780C7");
    jQuery("div[ctxNum='1']").parent().css("color", "white");
    jQuery("div[ctxNum='1']").css("font-weight", "bold");

    jQuery("td.ms-toolbar").hide();

    //hide project status specify value control
    jQuery("span[title='Project Status: Choose Option']").hide();

    jQuery("span[title='Project Status: Specify your own value:'] input:radio").parent().parent().parent().hide();
    jQuery("span[title='Project Status: Specify your own value:'] input:radio").parent().parent().parent().next().hide();
    //jQuery("input[title='Project Status: Choice Drop Down']").parent().parent().prev("tr").hide();

    //jQuery("#Ribbon\\.ListForm\\.Edit-title").hide();
    //jQuery("#Ribbon\\.ListForm\\.Edit").hide();
    //jQuery("a[title='Browse']").click();

    //load field
    jQuery(".UDEditForm tbody tr").each(function () {
        //alert("0");
        jQuery(this).find("td").each(function () {
            //alert()
            if (jQuery(this).find("h3").find("nobr").text() == "Need External Participants") {
                if (jQuery(this).parent().find("td").eq(1).find("span").find("input").attr("checked") != "checked") {
                    jQuery(this).parent().find("td").eq(2).hide();
                    jQuery(this).parent().find("td").eq(3).hide();
                    //alert(jQuery(this).parent().html());
                }
                else {
                    jQuery(this).parent().find("td").eq(2).show();
                    jQuery(this).parent().find("td").eq(3).show();

                }
                //alert();

            }
        })
    })

    //external participants
    //var external = jQuery("norb");
    var needExternal = jQuery("input[id$='_BooleanField']").eq(0);
    jQuery(needExternal).bind("click dblclick", function () {
        //alert("");
        jQuery("tr td h3 nobr").each(function () {
            if (jQuery(this).text() == "External Participants") {
                jQuery(this).parent().parent().toggle();
                jQuery(this).parent().parent().next().toggle();
                if ($("textarea[title='External Participants']").is(":hidden")) {
                    $("textarea[title='External Participants']").val("");
                }
                //alert(jQuery(this).parent().parent().parent().parent().html());
            }

        })


    })





    //alert(jQuery("select[title='Therapeutic Area']").children('option:selected').text());
    if (jQuery("select[title='Therapeutic Area']").children('option:selected').text() != "Other") {
        jQuery("select[title='Therapeutic Area']").parent().parent().find("span:eq(1)").hide();
    }

    jQuery("select[title='Therapeutic Area']").change(function () {
        //alert(jQuery(this).children('option:selected').text()=="Other");
        if (jQuery(this).children('option:selected').text() == "Other") {
            //alert(jQuery(this).children('option:selected').text())
            jQuery(this).parent("span").next().show();
        }
        else {
            //alert(jQuery(this).parent("span").next().find("div > p").html());
            jQuery(this).parent("span").next().hide();
            jQuery(this).parent("span").next().find("div > p").text("");
        }

    })



    //load field

    if (jQuery("input[title='Need External Participants']").attr("checked") != "checked") {
        jQuery(external).parent().parent().parent().hide();
    }





    //click save and cancel
    jQuery("button#btnSave").click(function (event) {

        jQuery("input[id$='SaveItem']").click();

    });

    jQuery("#btnCancel").click(function () {
        jQuery("input[id$='GoBack']").click();

    });





    jQuery("input[title='Actual End']").blur(function () {
        var outcomeValue = jQuery("select[title='Outcome'] option:selected").text();
        if (outcomeValue == "Approved" || outcomeValue == "Ongoing" || outcomeValue == "Non-Progressed")
            confirm("Whether to update the Outcome field?");
    });


    jQuery("select[title='Outcome']").blur(function () {
        var outcomeValue = jQuery(this).children("option:selected").text();
        if (outcomeValue == "Approved" || outcomeValue == "Ongoing" || outcomeValue == "Non-Progressed")
            confirm("Whether to update the Actual End field?");
    });




    //select form type and change status




    var fullCheckbox = jQuery("span[title='Full']");
    var shortCheckbox = jQuery("span[title='Short']");
    var statusSelect = jQuery("select[title='Status']");
    var selectAllOption = jQuery(statusSelect).html();
    var removeValueArray = new Array();
    removeValueArray = removeValue.split(",");


    //select type
    jQuery(fullCheckbox).click(function () {
        jQuery(statusSelect).empty();
        jQuery(statusSelect).append(selectAllOption);
    })

    jQuery(shortCheckbox).click(function () {
        RemoveOption(removeValueArray);
    })


    //load

    if ($(shortCheckbox).children("input").attr("checked") === "checked") {
        $(shortCheckbox).click();
    };



    function RemoveOption(array) {


        for (var i = 0; i < array.length; i++) {
            jQuery("select[title='Status']").children("option").each(function () {
                if (jQuery(this).text().toString().toLocaleLowerCase() == array[i].toString().toLocaleLowerCase()) {
                    jQuery(this).remove();
                }
            })
        }
    }

})

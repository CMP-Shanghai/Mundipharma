$(function () {
    $("table[class='UDNewForm']").css("background-color", "#EEE");
    $("span[title='This is a required field.']").css("color", "red");
    $("td[class='ms-formlabel']").addClass("tdLeft");
    $("table[class='ms-formtable'] ").attr("cellspacing", 1);
    $("td[class='ms-formbody']").css("padding-left", "10px");

    //$("#Ribbon\\.ListForm\\.Edit-title").css("display","none")
    //$("#Ribbon\\.ListForm\\.Edit").parent().css("display", "none");
    //SelectRibbonTab("Ribbon.Read", true);
    //hide project status field
    $("nobr").each(function () {
        if ($(this).text() == "Project Status") {
            $(this).parent().parent().parent().hide();
        }
    })

    //external participants
    var external = $("norb");
    var needExternal = $("input[id$='_BooleanField']");
    $(needExternal).bind("click dblclick", function () {
        //alert("");
        $("tr td h3 nobr").each(function () {
            if ($(this).text() == "External Participants") {
                $(this).parent().parent().toggle();
                $(this).parent().parent().next().toggle();
                //alert($("textarea[title='External Participants']").is(":hidden"));
                if ($("textarea[title='External Participants']").is(":hidden")) {
                    $("textarea[title='External Participants']").val("");
                }
                //$("textarea[title='External Participants']").val("");
                //alert($(this).parent().parent().parent().parent().html());
            }

        })


    })


    $("select[title='Therapeutic Area']").parent("span").next().hide();
    $("select[title='Therapeutic Area']").change(function () {
        //alert($(this).children('option:selected').text()=="Other");
        if ($(this).children('option:selected').text() == "Other") {
            //alert($(this).children('option:selected').text())
            $(this).parent("span").next().show();
        }
        else {
            //alert($(this).parent("span").next().find("div > p").html());
            $(this).parent("span").next().hide();
            $(this).parent("span").next().find("div > p").text("");
        }

    })


    $("input[id$='SaveItem']").attr("value", "Submit");


});







var ddlink=null;
var isCancel=false;
var weburl = null;
$(function () {
    $("table.ms-listviewtable").find("input[type='checkbox']").hide()
    weburl=GetCurrentWebUrl();
        changeLinks();


})


function changeLinks()
{
    var trSize=$("table.ms-listviewtable tbody tr").size();
    trSize = parseInt(trSize.toString())-1;
    $("table.ms-listviewtable tbody tr").each(function(i){
        //$(this).children("td:eq(4)").hide();
        var url= weburl + "/DueDiligenceLibrary/" +  $(this).find("td").eq(0).text() + "/";
        if ($(this).children("td:eq(4)").text() == "Full") {
            url=url+"Full%20Due%20Diligence%20Report.docx";
        }
        else { url = url + "Short%20Form%20Due%20Diligence%20Report.docx"; }

        $(this).children("td:eq(5)").html("<a href='" + url + "'>Due Diligence report</a>");
    })


}



var weburl = GetCurrentWebUrl();
$(document).ready(function () {
    $('#mySelect').change(function () {
        var faName = $(this).children('option:selected').val();//
        faName = faName.replace(' ', '');
        if (faName != "homepage") {
            window.location.href = weburl + "/SitePages/" + faName + ".aspx";
        }
    });
});
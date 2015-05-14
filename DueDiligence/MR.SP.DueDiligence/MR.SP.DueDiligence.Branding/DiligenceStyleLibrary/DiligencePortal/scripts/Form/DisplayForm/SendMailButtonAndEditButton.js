var weburl = null;
jQuery(function () {
    weburl = GetCurrentWebUrl();
})
function getParameterByName(name) {
    name = name.replace(/[\[]/, "\\[").replace(/[\]]/, "\\]");
    var regex = new RegExp("[\\?&]" + name + "=([^&#]*)"),
        results = regex.exec(location.search);
    return results === null ? "" : decodeURIComponent(results[1].replace(/\+/g, " "));
}

function postUrl() {
    var strUrl = weburl + "/Lists/DueDiligenceProjects/UD_EditForm.aspx?Source=" + escape(window.location.href + "&random=" + Math.random()) + "&ID=" + getParameterByName('ID');
    window.location = strUrl;
}

function sendEmail() {
    var strUrl = weburl + "/_layouts/15/MR.SP.DueDiligence.Pages/mail/sendmail.aspx?id=" + getParameterByName('ID') + "&reurl=" +escape(window.location.href);
    window.location = strUrl;
}

function background() {
    var strUrl = weburl + "/Lists/DueDiligenceProjects/Background.aspx?id=" + getParameterByName('ID') + "&Source=" +escape(window.location.href) + "&page=UD_DisplayForm.aspx";
    window.location = strUrl;
}

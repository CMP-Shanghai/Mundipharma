
$(function () {
    $("table[id$='_txtBody_toolbar']").css("width", "700px");
    $("iframe[id$='_txtBody_iframe']").css("width", "700px");
    $("table[id$='_txtBody_iframe']").css("height", "300px");
})

function rueurl() {
    var url = getParameterByName("reurl");
    window.location.href = url;
}


function getParameterByName(name) {
    name = name.replace(/[\[]/, "\\[").replace(/[\]]/, "\\]");
    var regex = new RegExp("[\\?&]" + name + "=([^&#]*)"),
        results = regex.exec(location.search);
    return results === null ? "" : decodeURIComponent(results[1].replace(/\+/g, " "));
}

$(function () {
    $("table[id$='txtBody_toolbar']").css("width", "70%");
    $("iframe[id$='txtBody_iframe']").css("width", "70%");
})


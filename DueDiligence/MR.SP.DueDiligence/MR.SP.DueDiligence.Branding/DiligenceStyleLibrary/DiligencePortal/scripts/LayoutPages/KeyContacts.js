$(document).ready(function () {
    $("td[nowrap='nowrap'][colspan='100']").each(
        function ( index, element) {
            var groupText = element.innerText;
            element.innerText = groupText.substring(7);//remove Group: string
        });
});
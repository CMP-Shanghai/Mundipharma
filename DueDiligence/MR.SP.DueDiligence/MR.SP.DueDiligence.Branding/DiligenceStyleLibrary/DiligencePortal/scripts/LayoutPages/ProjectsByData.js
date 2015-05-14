
        $(function () {
            
            $("table[id$='_gvReport'] tbody").children("tr").each(function () {
                var startDate = $(this).find("td").eq(4).children("span").text();
                //var territories = $(this).find("td").eq(9).children("span").text();
                startDate = startDate.split(" ")[0];
                $(this).find("td").eq(4).children("span").text(startDate);

            })
        })

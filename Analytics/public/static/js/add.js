function link (i, v) {
    return '<div class="span3">' +
             '<a class="btn btn-mini btn-block btn-yellow"' +
                'href="/detail/' + v[0] + '" id="l' + i + '">' +
               v[1] +
             '</a>' +
           '</div>';
}

function display_measures(measures) {
    $("#measures").html("");
    len = measures.length;
    cols = 3;
    rows = Math.ceil(len / cols);
    for (row = 0; row < rows; row++) {
        html = '<div class="row">';
        for (col = 0; col < cols; col++) {
            n = row + rows * col;
            // alert(n + " of " + len + " in row " + row);
            if (n < len) {
                html += link(n, measures[n]);
            }
        }
        html += '</div>';
        // alert(html);
        $("#measures").append(html);
    }
}

$(function() {
    display_measures_from_text();
    $("#search").typeahead({
        source: function (query, process) {
            $.post("/get_tags", { q: query }, function (data) {
                // console.log("typeahead");
                // console.log(data);
                display_measures(data[1]);
                return process(data[0]);
            }, "json")
        }
    });
});

function display_measures_from_text() {
    $.post("/get_tags", { q: $("#search").val() }, function (data) {
        // console.log("change");
        // console.log(data);
        display_measures(data[1]);
    }, "json")
}

$("#search").on("change", function() { display_measures_from_text() });

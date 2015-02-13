var Data_cache = [];

function chart(type, mid, title, ylabel, uid) {
    chartname = type + mid;
    jsonurl   = "/chart_data/" + uid + "/" + mid;
    // alert(jsonurl);
    try {
        var plot = $.jqplot(chartname, jsonurl, {
            title: title,
            seriesColors: [
                "#049cdb", "#ffc40d", "#46a546", "#f89406",
                "#7a43b6", "#c3325f", "#0064cd", "#9d261d",
            ],
            // series: [
                // { color: "#049cdb" },
                // { color: "#ffc40d" },
                // { color: "#46a546" },
                // { color: "#f89406" },
                // { color: "#7a43b6" },
                // { color: "#c3325f" },
                // { color: "#0064cd" },
                // { color: "#9d261d" },
            // ],
            // legend:
            // {
                // show:     1,
                // labels:   [ "checkin", "goal" ],
                // location: "w",
            // },
            dataRenderer: function(url, plot, options) {
                if (Data_cache[mid]) {
                    console.log(Data_cache[mid]);
                    return Data_cache[mid];
                }
                var ret = null;
                $.ajax({
                    // have to use synchronous here, else the function
                    // will return before the data is fetched
                    async:    false,
                    url:      url,
                    dataType: "json",
                    success:  function(data) { Data_cache[mid] = ret = data },
                    error:    function(data) { alert("error"); console.log(data) },
                });
                // console.log(ret);
                return ret;
            },
            axesDefaults: {
                pad: 1.05,
            },
            axes: {
                xaxis: {
                    renderer: $.jqplot.DateAxisRenderer,
                    // tickInterval: 600000,
                    // tickOptions:{ formatString: "%b %#d, %y %T" },
                },
                yaxis: {
                    // label:         ylabel,
                    labelRenderer: $.jqplot.CanvasAxisLabelRenderer,
                }
            },
            // highlighter: {
                // show:         true,
                // sizeAdjust:   7.5,
                // yvalues:      5,
                // formatString: '<div class="jqplot-highlighter">' +
                              // '%.0s%.0s%.0s%s<br />%s ' + ylabel +
                              // '<br />%s</div>',
            // },
            cursor: {
                show:        true,
                zoom:        true,
                showTooltip: true,
            },
            seriesDefaults: {
                trendline: {
                    show:        false,
                    // color:       "#AAAAAA",
                    // label:       "trend",
                    lineWidth:   1,
                },
            },
            // dragable: {
                // color:       "#FF0000",
            // },
        });
    } catch (e) {
        $("#" + chartname).html("<h3>Your chart for \"" + title + "\" will " +
                                "appear here </h3>" + e.message);
    }
}

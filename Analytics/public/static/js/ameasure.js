var Page = "/";

function do_help() {
    $("#help_modal").modal({
        keyboard: true,
        show:     true,
    });
    $.ajax({
        async:    true,
        url:      "/context_help",
        // data:     { path: $(location).attr("pathname") },
        data:     { path: Page },
        dataType: "json",
        success:  function(data) { $("#help_text").html(data[0]) },
        error:    function(data) { $("#help_text").html("Sorry, help is currently unavailable.") }
    });
    return false;
}

$(".help").attr({"href": "/help", "onclick": "$('.help').tooltip('hide'); return do_help()"});
$(".help").append('<i class="icon-question-sign"></i>');
$(".help").tooltip({trigger: "hover"});

function set_page(page) { Page = page; history.replaceState("", "", page) }

function set_submit() {
    $("input").keydown(function(event) {
        // https://developer.mozilla.org/en-US/docs/DOM/event.which
        var keycode = event.which || event.keyCode;
        if (keycode == 13) {  // keycode for enter key
            $("#default_submit").click();
            return false;
        }
        return true;
    });
}

$(function() { setTimeout(function() { $("#flash").slideDown(800) }, 500) });

function no_twitter() {
    $("#twitter_modal").modal({
        keyboard: true,
        show:     true,
    });
    return false;
}


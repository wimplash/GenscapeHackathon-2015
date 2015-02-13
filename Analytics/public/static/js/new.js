$(function() {
    $("#new").validate({
        rules: {
            measure: "required",
        },
        messages: {
            measure: "You need to tell us what you want to measure",
        },
    });
});

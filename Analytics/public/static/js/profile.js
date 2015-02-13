$(function() {
    $("#profile").validate({
        rules: {
            email: {
                email:    true,
            },
            name:      "required",
            page_size: {
                required: "true",
                number:   "true",
            },
        },
        messages: {
            email:     "Enter your email address",
            name:      "Enter your name",
            page_size: "Enter the number of charts per page",
        },
    });
});

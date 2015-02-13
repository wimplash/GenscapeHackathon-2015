$(function() {
    $("#forgotten").validate({
        rules: {
            email: {
                required: true,
                email:    true,
            },
        },
        messages: {
            email:    "Enter your email address",
        }
    });
});

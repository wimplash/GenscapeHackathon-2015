$(function() {
    $("#contact").validate({
        rules: {
            email: {
                required: true,
                email:    true,
            },
            message:  "required"
        },
        messages: {
            email:    "Enter your email address",
            message:  "Enter your message"
        }
    });
});

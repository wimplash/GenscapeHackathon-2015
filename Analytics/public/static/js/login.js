$(function() {
    $("#login").validate({
        rules: {
            email: {
                required: true,
                email:    true,
            },
            password: "required"
        },
        messages: {
            email:    "Enter your email address",
            password: "Enter your password"
        }
    });
});

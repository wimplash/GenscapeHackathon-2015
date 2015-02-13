$(function() {
    // validate signup form on keyup and submit
    $("#signup").validate({
        rules: {
            email: {
                required: true,
                email:    true,
            },
            name:     "required",
            password: "required"
        },
        messages: {
            email:    "Enter your email address",
            name:     "Enter your name",
            password: "Enter your password"
        }
        // the errorPlacement has to take the table layout into account
        // errorPlacement: function(error, element) {
            // if ( element.is(":radio") )
                // error.appendTo( element.parent().next().next() );
            // else if ( element.is(":checkbox") )
                // error.appendTo ( element.next() );
            // else
                // error.appendTo( element.parent().next() );
        // },
        // specifying a submitHandler prevents the default submit, good for the demo
        // submitHandler: function() {
            // alert("submitted!");
        // },
        // set this class to error-labels to indicate valid fields
        // success: function(label) {
            // set   as text for IE
            // label.html(" ").addClass("checked");
        // }
    });
});

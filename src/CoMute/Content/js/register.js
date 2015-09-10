
var FormValidation = function () {
    var formValidatorVar;
    var handleValidation = function () {

        var form1 = $('#register');

        formValidatorVar = form1.validate({
            errorClass: "error-class",
            validClass: "valid-class",
            rules: {
                Name: {
                    minlength: 2,
                    required: true
                },
                Surname: {
                    minlength: 2,
                    required: true
                },
                PhoneNumber: {
                    minlength: 10,
                    required: false,
                    digits: true
                },
                Email: {
                    email: true,
                    required: true
                },
                Password: {
                    minlength: 6,
                    required: true
                },
                ConfirmPassword: {
                    equalTo: "#Password",
                    required: true
                }
            },

            invalidHandler: function (event, validator) { //display error alert on form submit              
                var $alert = $("#error");
                var $p = $alert.find("p");
                $p.text('You have ' + validator.numberOfInvalids() + ' errors in your form');
                $alert.removeClass('hidden');

                setTimeout(function () {
                    $p.text('');
                    $alert.addClass('hidden');
                }, 3000);
            },

            submitHandler: function (form) {
                $("#sign-in").prop('disabled', true);
                $.ajax({
                    type: 'POST',
                    url: '/api/accounts/create',
                    data: $('#register').serialize(),
                    success: function (result) {

                        window.location.href = "Index";
                    },
                    fail: function (data) {
                        var $alert = $("#error");
                        var $p = $alert.find("p");
                        $p.text(data.val());
                        $alert.removeClass('hidden');

                        setTimeout(function () {
                            $p.text('');
                            $alert.addClass('hidden');
                        }, 3000);
                    }
                });
            }
        });


    }

    return {
        //main function to initiate the module
        init: function () {
            handleValidation();
        },

        resetForm: function () {
            formValidatorVar.resetForm();
        }
    };
}();
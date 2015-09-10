
var FormValidation = function () {
    var formValidatorVar;
    var handleValidation = function () {

        var form1 = $('#login');

        formValidatorVar = form1.validate({
            errorClass: "error-class",
            validClass: "valid-class",
            rules: {
                Username: {
                    email: true,
                    required: true
                },
                Password: {
                    required: true
                }
            },

            invalidHandler: function (event, validator) { //display error alert on form submit              
                var $alert = $("#error");
                var $p = $alert.find("p");
                $p.text("You made " + validator.numberOfInvalids() + " errors");
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
                    url: '/oauth/token',
                    data: $('#login').serialize(),
                    success: function (result) {
                        self.user(result.Username);

                        sessionStorage.setItem(tokenKey, result.access_token)

                        window.location.href = "Index";
                        return false;
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
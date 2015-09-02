; (function (root, $) {
    $('#register').on('submit', function (ev) {
        ev.preventDefault();

        var name = $('#name').val();
        if (!name) {
            return;
        }

        var surname = $('#surname').val();
        if (!surname) {
            return;
        }

        var phone = $('#phone').val();
        if (!phone) {
            return;
        }

        var email = $('#email').val();
        if (!email) {
            return;
        }

        var pswd = $('#password').val();
        if (!pswd) {
            return;
        }

        var cpswd = $('#confirm-password').val();
        if (!email) {
            return;
        }

        $.post('/user/add', { name: name, surname: surname, phoneNumber: phone, emailAddress: email, password: pswd }, function (data) {
            var $success = $("#success");
            var $p = $success.find("p");
            $p.text('Registration Successful... Redirecting to Login in page');// redirects to login page once user has been successful
            $success.removeClass('hidden');

            setTimeout(function () {
                $p.text('');
                $success.addClass('hidden');
                window.location.href = '/'
            }, 6000);
          
        }).fail(function (data) {
            var $alert = $("#error");
            var $p = $alert.find("p");
            $p.text('Registration failed.If all information has been put correctly  Check if Email Address Has Not Already Been  Registered');
            $alert.removeClass('hidden');

            setTimeout(function () {
                $p.text('');
                $alert.addClass('hidden');
            }, 3000);
        });
    });
})(window, jQuery);
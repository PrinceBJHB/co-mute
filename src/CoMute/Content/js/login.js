﻿(function (root, $) {
    $('#login').on('submit', function (ev) {
        ev.preventDefault();
        var email = $('#inputEmail').val();
        if (!email) {
            return;
        }

        var pswd = $('#inputPassword').val();
        if (!pswd) {
            return;
        }

        $.post('Home/Login', { email: email, password: pswd }, function (data) {
            // TODO: Navigate away...
            var message = data.Message;
            alert("got back! " + data.Message);
            if (data.Success == "1") {
                //direct to the profile page
                window.location.href = "User/profile";

            } else {
                var $alert = $("#error");
                var $p = $alert.find("p");
                $p.text(message);
                $alert.removeClass('hidden');

                setTimeout(function () {
                    $p.text('');
                    $alert.addClass('hidden');
                }, 10000);
            }

        }).fail(function (data,status,err) {
            var $alert = $("#error");
            var $p = $alert.find("p");
            $p.text('Incorrect email and password combination '+data.responseText);
            $alert.removeClass('hidden');

            /*setTimeout(function () {
                $p.text('');
                $alert.addClass('hidden');
            }, 10000); */
        });
    });
})(window, jQuery);
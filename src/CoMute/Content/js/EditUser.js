﻿; (function (root, $) {
    $('#register').on('submit', function (ev) {
        ev.preventDefault();

        var id = $('#id').val();
        if (!id) {
            return;
        }

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
            phone = '';
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

        $.post('/user/edit', { id: id, name: name, surname: surname, phoneNumber: phone, emailAddress: email, password: pswd, confirmPassword: cpswd }, function (data) {
        }).fail(function (data) {
            var $alert = $("#error");
            var $p = $alert.find("p");
            $p.text(data.responseText);
            $alert.removeClass('hidden');

            setTimeout(function () {
                $p.text('');
                $alert.addClass('hidden');
            }, 3000);
        }).success(function (data) {
            var $alert = $("#success");
            var $p = $alert.find("p");
            $p.text('Success');
            $alert.removeClass('hidden');

            setTimeout(function () {
                $p.text('');
                $alert.addClass('hidden');
            }, 3000);
        });
    });
})(window, jQuery);
(function (root, $) {
    $('#update').on('submit', function (ev) {
        ev.preventDefault();

        //regular expression to check phone number
        var reNum = /^\d+$/;
        var reName = /^\w+$/;

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

        var oldpswd = $('#old-password').val();
        if (!oldpswd) {
            return;
        }

        var newpswd = $('#new-password').val();
        if (!newpswd) {
            return;
        }

        if (!reNum.test(phone)) {

            var $alert = $("#error");
            var $p = $alert.find("p");
            $p.text('PhoneNumber should contain only digits. Please try again');
            $alert.removeClass('hidden');

            setTimeout(function () {
                $p.text('');
                $alert.addClass('hidden');
            }, 5000);

            return;

        } else if (!reName.test(name) || !reName.test(surname)) {

            var $alert = $("#error");
            var $p = $alert.find("p");
            $p.text('Username and Surname must contain only letters, numbers and underscores. Please try again');
            $alert.removeClass('hidden');

            setTimeout(function () {
                $p.text('');
                $alert.addClass('hidden');
            }, 5000);

            return;
        }

        var parsed;

        $.post('Update', { name: name, surname: surname, phone: phone, email: email, oldpassword: oldpswd,newpassword:newpswd }, function (data) {
            // TODO: Navigate away...
            var message = data.Message;

            var $alert = $("#error");
            var $p = $alert.find("p");
            $p.text(message);
            $alert.removeClass('hidden');

            setTimeout(function () {
                $p.text('');
                $alert.addClass('hidden');
            }, 10000);

            alert("got back! " + data.Message);


        }).fail(function (data,status,err) {
            var $alert = $("#error");
            var $p = $alert.find("p");
            $p.text('Update failed '+data.responseText);
            $alert.removeClass('hidden');

            /*setTimeout(function () {
                $p.text('');
                $alert.addClass('hidden');
            }, 10000);*/
            
        });
    });

    $('#logout').click(function (ev) {
        alert("clicked");
        $.post('Logout', {}, function (data) {
            // TODO: Navigate away...

            window.location.href = "/";

        }).fail(function (data, status, err) {
            var $alert = $("#error");
            var $p = $alert.find("p");
            $p.text('Logout failed ' + data.responseText);
            $alert.removeClass('hidden');


        });
    });

})(window, jQuery);
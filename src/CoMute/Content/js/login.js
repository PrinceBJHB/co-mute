; (function (root, $) {
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

        $.post('/api/Authentication', { email: email, password: pswd }, function(data) {
            // TODO: Navigate away...
            $.post('/api/user/profile/',
                { email: email },
                function (data) {

                    sessionStorage['email'] = email;
                    window.alert('viewing profile' + data["Name"]);
                    window.location = '/home/profile'

                    var $list = text.find("#proName");
                    list.append(data["Name"]);

                    $("proName").html(data["Name"]);


                }).fail(
                    function (data) {
                        window.alert('cannot view profile');
                    }
                );

    }).fail(function (data) {
            var $alert = $("#error");
            var $p = $alert.find("p");
            $p.text('Incorrect email and password combination');
            $alert.removeClass('hidden');

            setTimeout(function () {
                $p.text('');
                $alert.addClass('hidden');
            }, 3000);
        });
    });
})(window, jQuery);
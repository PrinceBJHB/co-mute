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

        $.post('http://localhost:49542/api/login/', { email: email, password: pswd }, function (data) {
            if(data == true)
            {
                
            }
            else
            {
                var $alert = $("#error");
                var $p = $alert.find("p");
                $p.text('Incorrect email and password combination');
                $alert.removeClass('hidden');
            }
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
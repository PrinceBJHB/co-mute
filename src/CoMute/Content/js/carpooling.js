(function (root, $) {
    $('#carpool').on('submit', function (ev) {
        ev.preventDefault();

        //alert("in script");
        //regular expression to check phone number
        var reNum = /^\d+$/;
        var reName = /^\w+$/;

        var cname = $('#cname').val();
        if (!cname) {
            return;
        }

        var dtime = $('#dtime').val();
        if (!dtime) {
            return;
        }

        var atime = $('#atime').val();
        if (!atime) {
            return;
        }

        var origin = $('#origin').val();
        if (!origin) {
            return;
        }

        var availabled = $('#availabled').val();
        if (!availabled) {
            return;
        }

        var destination = $('#destination').val();
        if (!destination) {
            return;
        }

        var availables = $('#availables').val();
        if (!availables) {
            return;
        }

        var owner = $('#owner').val();
        if (!owner) {
            return;
        }

        var notes = $('#notes').val();
        if (!notes) {
            return;
        }

        if (!reName.test(cname)) {

            var $alert = $("#error");
            var $p = $alert.find("p");
            $p.text('Name should contain only Letters and Underscores');
            $alert.removeClass('hidden');

            setTimeout(function () {
                $p.text('');
                $alert.addClass('hidden');
            }, 5000);

            return;

        }else if (!reNum.test(availables)){
            var $alert = $("#error");
            var $p = $alert.find("p");
            $p.text('Number of seats should contain only Numbers');
            $alert.removeClass('hidden');

            setTimeout(function () {
                $p.text('');
                $alert.addClass('hidden');
            }, 5000);

            return;
        }


        var parsed;

        $.post('RegisterCarPool', {
            cname:cname,dtime: dtime, atime: atime, origin: origin, availabled: availabled, destination: destination,
            availables: availables,owner: owner,notes: notes
        }, function (data) {
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

    $('#logout').on('click', function (ev) {
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
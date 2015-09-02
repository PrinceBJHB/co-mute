; (function (root, $) {
    $('#new-car-pool').on('submit', function (ev) {
        ev.preventDefault();

        var departure = $('#departure-time').val();
        if (!departure) {
            return;
        }

        var arrival = $('#arrival-time').val();
        if (!arrival) {
            return;
        }

        var origin = $('#origin').val();
        if (!origin) {
            return;
        }

        var destination = $('#destination').val();
        if (!destination) {
            return;
        }

        var days = $('#days').val();
        if (!days) {
            return;
        }

        var notes = $('#notes').val();
        if (!notes) {
            return;
        }
        
        var seats = $('#seats').val();
        if (!seats) {
            return;
        }

        $.post('/api/carpool', { 
                DepartTime: departure, 
                ArriveTime: arrival, 
                Origin: origin, 
                Destination: destination, 
                DaysAvailable: days ,
                Notes: notes,
                Email: user.email,
                SeatsAvailable : seats,
            }, function (data) {
                var $success = $("#success");
                var $p = $success.find("p");
                $p.text('Car Pool Opportunity has been created Successfully');// redirects to  users list of car pools 
                $success.removeClass('hidden');

                setTimeout(function () {
                    $p.text('');
                    $success.addClass('hidden');
                    window.location.href = '/'
                }, 9000);
        }).fail(function (data) {
            var $alert = $("#error");
            var $p = $alert.find("p");
            $p.text('Failed to create car pool');
            $alert.removeClass('hidden');

            setTimeout(function () {
                $p.text('');
                $alert.addClass('hidden');
            }, 9000);
        });
    });
})(window, jQuery);
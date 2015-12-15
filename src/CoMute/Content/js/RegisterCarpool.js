; (function (root, $) {
    $('#register').on('submit', function (ev) {
        ev.preventDefault();

        availableSeats = $('#availableSeats').val();
        if (!availableSeats) {
            return;
        }

        daysAvailable = $('#daysAvailable').val();
        if (!daysAvailable) {
            return;
        }

        expectedArrivalTime = $('#expectedArrivalTime').val();
        if (!expectedArrivalTime) {
            return;
        }

        departureTime = $('#departureTime').val();
        if (!departureTime) {
            return;
        }

        destination = $('#destination').val();
        if (!destination) {
            return;
        }

        notes = $('#notes').val();
        if (!notes) {
            notes = '';
        }

        Origin = $('#Origin').val();
        if (!Origin) {
            return;
        }

        $.post('/api/user/registerCarpool', { availableSeats: availableSeats, daysAvailable: daysAvailable, expectedArrivalTime: expectedArrivalTime, departureTime: departureTime, destination: destination, notes: notes, Origin: Origin }, function (data) {
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
function joinCarPool(carpool) {

    //alert("in joining");

    $.post('JoinCarPool', { cname:carpool }, function (data) {
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

        //alert("got back! " + data.Message);


    }).fail(function (data) {
        var $alert = $("#error");
        var $p = $alert.find("p");
        $p.text('Joining failed');
        $alert.removeClass('hidden');

        setTimeout(function () {
            $p.text('');
            $alert.addClass('hidden');
        }, 10000);

    });

}

function leaveCarPool(carpool) {

    //alert("in leaving");

    $.post('LeaveCarPool', { cname: carpool }, function (data) {
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

        //alert("got back! " + data.Message);


    }).fail(function (data) {
        var $alert = $("#error");
        var $p = $alert.find("p");
        $p.text('Joining failed');
        $alert.removeClass('hidden');

        setTimeout(function () {
            $p.text('');
            $alert.addClass('hidden');
        }, 10000);

    });

}

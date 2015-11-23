(function (root, $) {
    $('#logout').on('click', function (ev) {
        ev.preventDefault();
        
        alert("clicked");
        $.post('Logout', {}, function (data) {
            // TODO: Navigate away...
           
            window.location.href = "/";

        }).fail(function (data,status,err) {
            var $alert = $("#error");
            var $p = $alert.find("p");
            $p.text('Logout failed '+data.responseText);
            $alert.removeClass('hidden');

            
        });
    });
})(window, jQuery);
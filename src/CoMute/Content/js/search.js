; (function (root, $) {
    $('#search').on('submit', function (ev) {
        ev.preventDefault();

        var search = $('#searchText').val();
        if (!search) {
            search = '';
        }

        $.get('/user/search/'+search, function (data) {
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
           //populate table on page
        });
    });
})(window, jQuery);
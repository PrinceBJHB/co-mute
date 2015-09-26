//function AjaxLoaderStart() {
//	$('body').addClass("loading");
//}

//function AjaxLoaderEnd() {
//	$('body').removeClass("loading");
//}

$.fn.serializeObject = function () {

    var self = this,
        json = {},
        push_counters = {},
        patterns = {
            "validate": /^[a-zA-Z][a-zA-Z0-9_]*(?:\[(?:\d*|[a-zA-Z0-9_]+)\])*$/,
            "key": /[a-zA-Z0-9_]+|(?=\[\])/g,
            "push": /^$/,
            "fixed": /^\d+$/,
            "named": /^[a-zA-Z0-9_]+$/
        };


    this.build = function (base, key, value) {
        base[key] = value;
        return base;
    };

    this.push_counter = function (key) {
        if (push_counters[key] === undefined) {
            push_counters[key] = 0;
        }
        return push_counters[key]++;
    };

    $.each($(this).serializeArray(), function () {
        // skip invalid keys
        if (!patterns.validate.test(this.name)) {
            return;
        }

        var k,
            keys = this.name.match(patterns.key),
            merge = this.value,
            reverse_key = this.name;

        while ((k = keys.pop()) !== undefined) {

            // adjust reverse_key
            reverse_key = reverse_key.replace(new RegExp("\\[" + k + "\\]$"), '');

            // push
            if (k.match(patterns.push)) {
                merge = self.build([], self.push_counter(reverse_key), merge);
            }

                // fixed
            else if (k.match(patterns.fixed)) {
                merge = self.build([], k, merge);
            }

                // named
            else if (k.match(patterns.named)) {
                merge = self.build({}, k, merge);
            }
        }

        json = $.extend(true, json, merge);
    });

    return json;
};
function isFunction(v) {
    return typeof (v) == "function";
}

function CallAPI(uri, method, data, callback) {
    $.ajax({
        url: uri,
        type: method,
        async: true,
        cache: false,
        data: JSON.stringify(data),
        contentType: "application/json",
        success: function (response, status, request) {
            if (response && response.Redirect) {
                window.location = response.Redirect;
            } else {
                if (isFunction(callback)) {
                    callback(response);
                } else if (window[callback]) {
                    window[callback](response);
                }
            }
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            alert("Failed to Post Form Data : " + errorThrown);
        }
    });
}

function LoadPartialView(uri, method, data, dest, callback) {
    $.ajax({
        url: uri,
        type: method,
        async: true,
        cache: false,
        data: JSON.stringify(data),
        contentType: "application/json",
        success: function (response, status, request) {
            if (response && response.redirect) {
                window.location = response.redirect;
            } else {
                if (dest != null) {
                    $(dest).empty();
                    $(dest).html(response);
                }

                if (isFunction(callback)) {
                    callback(response);
                } else if (window[callback]) {
                    window[callback](response);
                }
            }
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            alert("Failed to Post Form Data : " + errorThrown);
        }
    });
}
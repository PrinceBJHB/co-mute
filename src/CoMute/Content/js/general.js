function sectionFadeIn(form) {
    $(form).hide().fadeIn();
}

function formSlideDown(form) {
    $(form).hide().slideDown("slow");
}

function formSlideUp(form) {
    $(form).slideUp();
}

function formUnhide(form) {
    $('#' + form).display = "block";
    document.getElementById(form).style.visibility = "visible";
    formSlideDown('#' + form);
}

function formHide(form) {
    $('#' + form).display = "none";
    document.getElementById(form).style.visibility = "hidden";
    formSlideUp('#' + form);
}

$("body").on("click", "#btnFormCancel", function () {
    $(this).closest("section").slideUp();
});


function UpdateViewSection(data, form, section) {
    if ((data.indexOf("field-validation-error") > -1) || (data.indexOf("validation-summary-errors") > -1)) {
        return;
    }
    else {
        formSlideUp(form);
        // Refresh table
        $.ajax({
            type: 'GET',
            url: '../Profile/View' + section + '/',
            cache: false,
            datatype: 'html',
            contentType: 'application/json; charset=utf-8',
            success: function (result) {
                if (result != null) {
                    $('#' + section).html(result);
                }
                else {
                    alert("Something went kinda wrong.");
                }
            },
            error: function (req) {
                alert("Something went terribly wrong.");
            }
        });
    }
};

function UpdateViewNoSlide(data, formSuccess, formFailure) {
    if ((data.indexOf("field-validation-error") > -1) || (data.indexOf("validation-summary-errors") > -1)) {
        $(formFailure).html(data);
    }
    else {
        $(formSuccess).html(data);
    }
};
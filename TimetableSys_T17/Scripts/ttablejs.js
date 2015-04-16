$(function () {

    var ajaxFormSubmit = function () {

        var $form = $(this);
        // Reference to the form submitted. Wrapped in jQuery so that it
        // can be used by jQuery.

        var options = {

            url: $form.attr("action"),
            type: $form.attr("method"),
            data: $form.seriaize()

        };

        // Above this line, collecting data.

        $.ajax(options).done(function(data) {

            var $target = $($form.attr("data-ttablejs-target")); // Find target in DOM.
            $target.replace(data); // DOM target - update or replaceWith

        });


        // async call.

        return false;
        // stops the browser navigating to another page.
    };

    var createAutocomplete = function () {

        var $input = $(this);
        

        //wrap in jQuery

        var options = {

            source: $input.attr("data-ttablejs-autocomplete")

        };

        $input.autocomplete(options);

    };

    $("form[data-ttablejs-ajax='true']").submit(ajaxFormSubmit);
    $("input[data-ttablejs-autocomplete]").each(createAutocomplete);

    /*

        jQuery selector, find all the forms with data attribute
        evaluted to true.

        Invokes function ajaxFormSubmit.


    */


});
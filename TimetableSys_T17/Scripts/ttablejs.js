$(function () {

    /* Find away to make this function generic? */

    var parks = function () {

        $host = $(this);
        //var target = $("#" + $host.attr("data-ttablejs-target")); // Find target in DOM.
        var input = $host.val();

      $.ajax({

          url: "ReturnParks",
          type: "GET",
          data: {

              input: input
          },
          contentType: "application/json",
          success: function (data) {

              $("#park_input").autocomplete({

                  source: data.parkName,
                  minLength: 0

              }).mouseenter(function () { if (input == "") { $host.autocomplete("search"); } });
            
          }

      });
    }

    $("input[data-ttablejs-autocomplete2]").keyup(parks);
    $("input[data-ttablejs-autocomplete2]").mouseenter(parks);
    

    

    /*

        jQuery selector, find all the forms with data attribute
        evaluted to true.

        Invokes function ajaxFormSubmit.


    */


});
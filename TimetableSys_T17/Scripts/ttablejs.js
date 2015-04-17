$(function () {

    var test = function () {

        $host = $(this);
        var target = $("#" + $host.attr("data-ttablejs-target")); // Find target in DOM.
        var input = $host.val();


      $.ajax({

          url: "bark",
          type: "GET",
          data: {

              input: input
          },
          contentType: "application/json",
          success: function (data) {

              $("#test2").autocomplete({

                  source: data.parkName
              });
            
          }

      });
    }


    $("input[data-ttablejs-autocomplete2]").keyup(test);
    

    

    /*

        jQuery selector, find all the forms with data attribute
        evaluted to true.

        Invokes function ajaxFormSubmit.


    */


});
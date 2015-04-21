$(function () {

    /* Find away to make this function generic? */
    /* problem source: model var - does not like value passed as string  ^^^ */

   /* var parks = function () {

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

    var buildings = function () {

        $host = $(this);
        if ($host.val() == "") {

            var input = $("input[data-ttablejs-park]").val();

        } else {

            var input = $host.val();

        }

        $.ajax({
            url: "ReturnBuildings",
            type: "GET",
            data: {
                input: input
            },
            contentType: "application/json",
            success: function (data) {
                
                $("#building_input").autocomplete({
                    source: data.buildingName,
                    minLength: 0
                }).mouseenter(function () { if (input == "") { $host.autocomplete("search"); } });
            }
        });
    }*/

    var request_model_data = function () {

        //var target = $("#" + $host.attr("data-ttablejs-target")); // Find target in DOM.
        var host_dom = $(this);
        var park = $("input[data-ttablejs-park").val();
        var building = $("input[data-ttablejs-building").val();
        var roomcode = $("input[data-ttablejs-roomcode").val();
        var facilities = $("input[data-tablejs-facilites").val(); // Could run into problems with this, as it needs to be multi-select.

        
        $.ajax({
            url: "RequestModelUpdater",
            type: "GET",
            data: {

                park: park,
                building: building,
                roomcode: roomcode,
                facilities: facilities
                
            },
            contentType: "application/json",
            success: function (data) {

                var target_auto_elem = "#" + host_dom.attr("id");   
                var target_auto_data = null;

                switch (host_dom.attr("id")) {

                    case "park_input": target_auto_data = data.parkName; break;
                    case "building_input": target_auto_data = data.buildingName; break
                    case "rooms_input": target_auto_data = data.roomCode; break;
                    case "facilities_input": target_auto_data = data.facilities; break;

                }

                $(target_auto_elem).autocomplete({
                    source: target_auto_data,
                    minLength: 0
                }).mouseenter(function () { if (host_dom.val() == "") { host_dom.autocomplete("search"); } });
            }
        });

    }
    
    $("input[data-ttablejs-park]").keyup(request_model_data);
    $("input[data-ttablejs-park]").mouseenter(request_model_data);
    $("input[data-ttablejs-building]").keyup(request_model_data);
    $("input[data-ttablejs-building]").mouseenter(request_model_data);
    $("input[data-ttablejs-roomcode]").keyup(request_model_data);
    $("input[data-ttablejs-roomcode]").mouseenter(request_model_data);
 
    // Facilities to b done soon.
    

    /*

        jQuery selector, find all the forms with data attribute
        evaluted to true.

        Invokes function ajaxFormSubmit.


    */


});
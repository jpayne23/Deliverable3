$(document).ready(function () {

    $("#facilities").multiselect();
    $("#rooms_input").multiselect();



});
var request_model_data_optional = function () {
  
    //var target = $("#" + $host.attr("data-ttablejs-target")); // Find target in DOM.
    var host_dom = $(this);
    var park = $("input[data-ttablejs-park").val();
    var building = $("input[data-ttablejs-building").val();
    //var roomcode = $("input[data-ttablejs-roomcode").val();
    var facilities = $("input[data-tablejs-facilites").val(); // Could run into problems with this, as it needs to be multi-select.


    $.ajax({
        url: "RequestModelUpdaterOptional",
        type: "GET",
        data: {

            park: park,
            building: building,
            //roomcode: roomcode,
            facilities: facilities

        },
        contentType: "application/json",
        success: function (data) {

            var target_auto_elem = "#" + host_dom.attr("id");
            var target_auto_data = null;

            switch (host_dom.attr("id")) {

                case "park_input": target_auto_data = data.parkName; break;
                case "building_input": target_auto_data = data.buildingName; break
                case "facilities_input": target_auto_data = data.facilities; break;

            }

            // Update all the time, as the user may not be bothered where
            // They're situatied, aslong as they have available facilities.
            data_facilities = data.facilities;
            data_rooms = data.roomCode;

            if (data.facilities != null) {
                var new_facility_list = [];

                data_facilities.forEach(function (entry) {

                    var stepping = { label: entry, title: entry, value: entry }
                    new_facility_list.push(stepping);
                })
                $("#facilities").multiselect("dataprovider", new_facility_list);

            }

            if (data.roomCode != null) {
                var new_roomCode_list = [];

                data_rooms.forEach(function (entry) {

                    var stepping = { label: entry, title: entry, value: entry }
                    new_roomCode_list.push(stepping);

                });

                $("#rooms_input").multiselect("dataprovider", new_roomCode_list).on("change", function () { returnSelect(this, "#rooms_input", 5); });
                

                $(target_auto_elem).autocomplete({
                    source: target_auto_data,
                    minLength: 0
                })

                if (host_dom.val() == "") { host_dom.autocomplete("search"); };
            }

        }
        
    });
}
    var request_model_data_compulsory = function () {

        host_dom = $(this);
        var mod_code = $("input[data-ttablejs-mcode]").val();
        var mod_title = $("input[data-ttablejs-mname]").val();
        var rm_type = $("input[data-ttablejs-rtype]").val();

        $.ajax({

            url: "RequestModelUpdaterCompulsory",
            type: "GET",
            data: {

                module_code: mod_code,
                module_title: mod_title,
                room_type: rm_type

            },
            contentType: "application/json",
            success: function (data) {

                if (data.moduleTitle != null && data.moduleTitle.length == 1 && mod_code.length > 0) {

                    AutoPopulate(data.moduleTitle[0], "#name_input");

                } else if (data.moduleCode != null && data.moduleCode.length == 1 && mod_title.length > 0) {

                    AutoPopulate(data.moduleCode[0], "#code_input");

                };

                var target_auto_elem = "#" + host_dom.attr("id");
                var target_auto_data = null;

                switch (host_dom.attr("id")) {

                    case "code_input": target_auto_data = data.moduleCode; break;
                    case "name_input": target_auto_data = data.moduleTitle; break
                    case "type_input": target_auto_data = data.roomType; break;

                }

                $(target_auto_elem).autocomplete({
                    source: target_auto_data,
                    minLength: 0
                })
                
                if (host_dom.val() == "") { host_dom.autocomplete("search"); };

            }
        });
    }

    var submitData = function () {


        var mod_code = $("input[data-ttablejs-mcode]").val();
        var mod_title = $("input[data-ttablejs-mname]").val();
        var rm_type = $("input[data-ttablejs-rtype]").val();
        var park = $("input[data-ttablejs-park").val();
        var building = $("input[data-ttablejs-building").val();
        var roomcode = $("input[data-ttablejs-roomcode").val();
        var facilities = $("input[data-tablejs-facilites").val();

        $.ajax({

            url: "SubmitRoundI",
            type: "GET",
            data: {
                //string room, List<string> facilities, string module_code, string module_title, string session_type
                room: roomcode,
                facilities: facilities,
                module_code: mod_code,
                module_title: mod_title,
                session_type: rm_type

            },

            success: function (data) {

                alert("Done Request");

            }
        });

    }

    function AutoPopulate(value, target) {

        $(target).val(value);

    }

    function returnSelect(element, target, limit) {

        var count = 0;

        for (var i = 0; i < element.length; i++) {
            
            element.options[i].selected ? count++ : null;

            if (count > limit) {


                alert("Cannot select: " + element.options[i].value + ", as there's a limit of " + limit + ".");
                $(target).multiselect("deselect", element.options[i].value);
                $(target).multiselect("refresh");

                break;
                
            }

        }


    }
    
    $("input[data-ttablejs-park]").keyup(request_model_data_optional);
    $("input[data-ttablejs-park]").mouseenter(request_model_data_optional);
    $("input[data-ttablejs-building]").keyup(request_model_data_optional);
    $("input[data-ttablejs-building]").mouseenter(request_model_data_optional);

 //request_model_data_optional);

 

    $("input[data-ttablejs-mcode]").keyup(request_model_data_compulsory);
    $("input[data-ttablejs-mcode]").mouseenter(request_model_data_compulsory);
    $("input[data-ttablejs-mname]").keyup(request_model_data_compulsory);
    $("input[data-ttablejs-mname]").mouseenter(request_model_data_compulsory);
    $("input[data-ttablejs-rtype]").keyup(request_model_data_compulsory);
    $("input[data-ttablejs-rtype]").mouseenter(request_model_data_compulsory);
    
    $("#submitter").click(submitData);

    // Facilities to be done soon.
    

    /*

        jQuery selector, find all the forms with data attribute
        evaluted to true.

        Invokes function ajaxFormSubmit.


    */



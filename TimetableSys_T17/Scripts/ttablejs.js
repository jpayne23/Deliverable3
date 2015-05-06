var parks_container = [];
var buildings_container = [];
var rooms_container = [];
var facilities_container = [];
var module_code_container = [];
var module_title_container = [];
var session_type_container = [];



/********************************************
KNOWN BUGS - ADAM 

WHEN UNSELECT OPTION, WHEN FILTERED, CLEAR SELECTED LIST, AS VALUES STILL EXIST

*********************************************/

$(document).ready(function () {
    $.fn.bootstrapBtn = $.fn.button.noConflict(); // To remedy Bootstrap - jQueryUI conflict
    $("#parks_input").multiselect({

        enableFiltering: true,
        maxHeight: 250,
        buttonWidth: 250,
        buttonText: function (options, select) {

            if (options.length == 0) {

                return "Select a Park";

            }
            else
            {

                var labels = [];

                options.each(function () {

                    if ($(this).attr('label') !== undefined) {

                        labels.push($(this).attr('label'));

                    }
                    else
                    {

                        labels.push($(this).html());

                    }
                });

                return labels.join(', ') + '';
            }
        },
        onDropdownShow: function (event) {

            if ($("#parks_input").children("option").length == 0) {

                AjaxCall(1);
            }

        },
        onChange: function (option, checked, select) {

            parks_container = arr_builder($(option).val(), checked, parks_container);
            AjaxCall(5);

        }

    });

    $("#buildings_input").multiselect({

        enableFiltering: true,
        maxHeight: 250,
        buttonWidth: 250,
        buttonText: function (options, select) {

            if (options.length == 0) {

                return "Select a Building";

            }
            else {

                var labels = [];

                options.each(function () {

                    if ($(this).attr('label') !== undefined) {

                        labels.push($(this).attr('label'));

                    }
                    else {

                        labels.push($(this).html());

                    }
                });

                return labels.join(', ') + '';
            }
        },
        onDropdownShow: function () {
         
            if ($("#buildings_input").children("option").length == 0) {

                AjaxCall(2);
            }

        },
        onChange: function (option, checked, select) {

            buildings_container = arr_builder($(option).val(), checked, buildings_container);
            AjaxCall(6);

        }
    });

    $("#rooms_input").multiselect({

        enableFiltering: true,
        maxHeight: 250,
        buttonWidth: 250,
        buttonText: function (options, select) {

            if (options.length == 0) {

                return "Select Rooms";

            }
            else {

                var labels = [];

                options.each(function () {

                    if ($(this).attr('label') !== undefined) {

                        labels.push($(this).attr('label'));

                    }
                    else {

                        labels.push($(this).html());

                    }
                });

                return labels.join(', ') + '';
            }
        },
        onDropdownShow: function () {

            if ($("#rooms_input").children("option").length == 0) {

                AjaxCall(3);
            }

        },
        onChange: function (option, checked, select) {

            rooms_container = arr_builder($(option).val(), checked, rooms_container);
            AjaxCall(7);
        }
    });

    $("#facilities_input").multiselect({

        enableFiltering: true,
        maxHeight: 250,
        buttonWidth: 250,
        buttonText: function (options, select) {

            if (options.length == 0) {

                return "Select Facilities";

            }
            else {

                var labels = [];

                options.each(function () {

                    if ($(this).attr('label') !== undefined) {

                        labels.push($(this).attr('label'));

                    }
                    else {

                        labels.push($(this).html());

                    }
                });

                return labels.join(', ') + '';
            }
        },
        onDropdownShow: function () {

            if ($("#facilities_input").children("option").length == 0) {

                AjaxCall(4);
            }

        },
        onChange: function (option, checked, select) {

            facilities_container = arr_builder($(option).val(), checked, facilities_container);
            AjaxCall(8);

        }
    });


    $("#module_code_input").multiselect({

        enableFiltering: true,
        maxHeight: 250,
        buttonWidth: 250,
        buttonText: function (options, select) {

            if (options.length == 0) {

                return "Select a Module Code";

            }
            else {

                var labels = [];

                options.each(function () {

                    if ($(this).attr('label') !== undefined) {

                        labels.push($(this).attr('label'));

                    }
                    else {

                        labels.push($(this).html());

                    }
                });

                return labels.join(', ') + '';
            }
        },
        onDropdownShow: function () {

            if ($("#module_code_input").children("option").length == 0) {

                AjaxCall(9);
            }

        },
        onChange: function (option, checked, select) {

            module_code_container = arr_builder($(option).val(), checked, module_code_container);
            AjaxCall(10);

        }
    });

    $('#module-code-input').on('click', function () {
        $('#module_code_input').multiselect('deselectAll', false);
        $('#module_code_input').multiselect('updateButtonText');
    });

    $("#module_title_input").multiselect({ 

        enableFiltering: true,
        maxHeight: 250,
        buttonWidth: 250,
        buttonText: function (options, select) {

            if (options.length == 0) {

                return "Select a Module Title";

            }
            else {

                var labels = [];

                options.each(function () {

                    if ($(this).attr('label') !== undefined) {

                        labels.push($(this).attr('label'));

                    }
                    else {

                        labels.push($(this).html());

                    }
                });

                return labels.join(', ') + '';
            }
        },
        onDropdownShow: function () {

            if ($("#module_title_input").children("option").length == 0) {

                AjaxCall(11);
            }

        },
        onChange: function (option, checked, select) {

            module_title_container = arr_builder($(option).val(), checked, module_title_container);
            AjaxCall(12);

        }
    });

    $("#session_type_input").multiselect({

        enableFiltering: true,
        maxHeight: 250,
        buttonWidth: 250,
        buttonText: function (options, select) {

            if (options.length == 0) {

                return "Select a Session Type";

            }
            else {

                var labels = [];

                options.each(function () {

                    if ($(this).attr('label') !== undefined) {

                        labels.push($(this).attr('label'));

                    }
                    else {

                        labels.push($(this).html());

                    }
                });

                return labels.join(', ') + '';
            }
        },
        onDropdownShow: function () {

            if ($("#session_type_input").children("option").length == 0) {

                AjaxCall(13);
            }

        },
        onChange: function (option, checked, select) {

            session_type_container = arr_builder($(option).val(), checked, session_type_container);

        }
    });

    $(function () {


        $("#special_requirements").resizable({

            animate: true,
            animateDuration: "slow",
            animateEasing: "easeOutBounce",
            delay: 150,
            minHeight: 200,
            minWidth: 500,
            maxHeight: 400,
            maxWidth: 700,
            handles: "se"
        });

    });

  

    $(function () {

        $("#radio").buttonset();

    });

    
});



var room_i_spinner = $("#room_i").spinner({ min: 1 });
var room_ii_spinner = $("#room_ii").spinner({ min: 1 });
var room_iii_spinner = $("#room_iii").spinner({ min: 1 });


function arr_builder(val, checked, array) {

    
    if (!checked) {

        array.splice(array.indexOf(val), 1);

    } else {

        array.push(val);

    }

    return array;

}

function dropDownConstructor(input, recieved_data, target) {

    placeholder = [];
    if (input.length != 0) {

        recieved_data.forEach(function (value) {

            if (input.indexOf(value) != -1) {

                var stepping = { label: value, title: value, value: value, selected: true }
                placeholder.push(stepping);


            } else {

                var stepping = { label: value, title: value, value: value }
                placeholder.push(stepping);

            }
        });

    } else {

        recieved_data.forEach(function (value) {

            var stepping = { label: value, title: value, value: value }
            placeholder.push(stepping);

        });

    }

    $(target).multiselect("dataprovider", placeholder);

}


function AjaxCall(call) {

    place_holder = [];

    $.ajax({

        url: "RequestModelUpdaterOptional2",
        type: "GET",
        data: {

            which_call: call,
            park_names: JSON.stringify(parks_container),
            building_names: JSON.stringify(buildings_container),
            room_names: JSON.stringify(rooms_container),
            facility_names: JSON.stringify(facilities_container),
            module_code: JSON.stringify(module_code_container),
            module_title: JSON.stringify(module_title_container),
            session_type: JSON.stringify(session_type_container)

        },
        contentType: "application/json",
        success: function (data) {

            if (data.parkName != null) { dropDownConstructor(parks_container, data.parkName, "#parks_input"); }
            if (data.buildingName != null) { dropDownConstructor(buildings_container, data.buildingName, "#buildings_input"); }
            if (data.roomCode != null) { dropDownConstructor(rooms_container, data.roomCode, "#rooms_input"); }
            if (data.facilities != null) { dropDownConstructor(facilities_container, data.facilities, "#facilities_input"); }
            if (data.moduleCode != null) { dropDownConstructor(module_code_container, data.moduleCode, "#module_code_input"); }
            if (data.moduleTitle != null) { dropDownConstructor(module_title_container, data.moduleTitle, "#module_title_input"); }
            if (data.sessionType != null) { dropDownConstructor(session_type_container, data.sessionType, "#session_type_input"); }
            
            
        }
    })


}

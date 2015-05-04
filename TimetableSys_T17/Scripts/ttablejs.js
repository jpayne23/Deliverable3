var parks_container = [];
var buildings_container = [];
var rooms_container = [];
var facilities_container = [];

$(document).ready(function () {

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
        onDropdownShow: function () {

            if (parks_container.length == 0) {

                AjaxCall(1);
            }

        },
        onChange: function (option, checked, select) {

            parks_container = arr_builder($(option).val(), checked, parks_container); 

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

            if (buildings_container.length == 0) {

                AjaxCall(2);
            }

        },
        onChange: function (option, checked, select) {

            buildings_container = arr_builder($(option).val(), checked, buildings_container);

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

            if (rooms_container.length == 0) {

                AjaxCall(3);
            }

        },
        onChange: function (option, checked, select) {

            rooms_container = arr_builder($(option).val(), checked, rooms_container);

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

            if (facilities_container.length == 0) {

                AjaxCall(4);
            }

        },
        onChange: function (option, checked, select) {

            facilities_container = arr_builder($(option).val(), checked, facilities_container);

        }
    });

});


function arr_builder(val, checked, array) {

    
    if (!checked) {

        array.splice(array.indexOf(val), 1);

    } else {

        array.push(val);

    }

    return array;

}

function dropDownConstructor(input, placeholder, recieved_data, target) {

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

    console.log(placeholder);
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
            facility_names: JSON.stringify(facilities_container)

        },
        contentType: "application/json",
        success: function (data) {
            
            
            if (data.parkName != null) { dropDownConstructor(parks_container, place_holder, data.parkName, "#parks_input"); }
            if (data.buildingName != null) { dropDownConstructor(buildings_container, place_holder, data.buildingName, "#buildings_input"); }
            if (data.roomCode != null) { dropDownConstructor(rooms_container, place_holder, data.roomCode, "#rooms_input"); }
            if (data.facilities != null) { dropDownConstructor(facilities_container, place_holder, data.facilities, "#facilities_input"); }
            
        }
    })


}

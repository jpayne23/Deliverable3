var parks_container = [];
var buildings_container = [];

$(document).ready(function () {

    $("#parks_input").multiselect({

        enableFiltering: true,
        maxHeight: 250,
        buttonWidth: 250,
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
        onDropdownShow: function () {

            if (buildings_container.length == 0) {

                AjaxCall(2);
            }

        },
        onChange: function (option, checked, select) {

            buildings_container = arr_builder($(option).val(), checked, buildings_container);

        }
    });

    $("#rooms_input").multiselect({ enableFiltering: true });
    $("#facilities_input").multiselect({ enableFiltering: true });

});


function arr_builder(val, checked, array) {

    
    if (!checked) {

        array.splice(array.indexOf(val), 1);

    } else {

        array.push(val);

    }

    return array;

}



function AjaxCall(call) {

    place_holder_parks = [];
    place_holder_buildings = [];

    $.ajax({

        url: "RequestModelUpdaterOptional2",
        type: "GET",
        data: {

            which_call: call,
            park_names: JSON.stringify(parks_container),
            buiding_names: JSON.stringify(buildings_container)

        },
        contentType: "application/json",
        success: function (data) {

    
            if (parks_container.length != 0) {

                data.parkName.forEach(function (value) {

                    if (parks_container.indexOf(value) != -1) {

                        var stepping = { label: value, title: value, value: value, selected: true }
                        place_holder_parks.push(stepping);


                    } else {

                        var stepping = { label: value, title: value, value: value }
                        place_holder_parks.push(stepping);

                    }
                });

            } else {

                data.parkName.forEach(function (value) {

                    var stepping = { label: value, title: value, value: value }
                    place_holder_parks.push(stepping);

                });



            }

            $("#parks_input").multiselect("dataprovider", place_holder_parks);
        }
    })


}


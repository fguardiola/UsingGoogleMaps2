
var geocoder;
var map;
$(document).ready(function () {
         
    initialize();

});

function initialize() {
          
    //$("#submit_button").attr('disabled', 'disabled');
    //document.getElementById("submit_button").disabled = true;
    //$('input[type=submit]', this).attr('disabled', 'disabled');

    var submit = $("#submit_button");
    console.log(submit);
    //submit.css("background-color", "red");
    submit.attr('disabled', 'disabled');

    //$("#checkBox").prop("checked", true);
    geocoder = new google.maps.Geocoder();
    var latlng = new google.maps.LatLng(53.350073, -6.261553);
    var mapOptions = {
        zoom: 13,
        center: latlng,
        mapTypeId: google.maps.MapTypeId.ROADMAP
    }
    map = new google.maps.Map(document.getElementById("map_canvas"), mapOptions);
}

function codeAddress() {
    var address = document.getElementById("address").value;
    geocoder.geocode({ 'address': address }, function (results, status) {
        if (status == google.maps.GeocoderStatus.OK) {
            map.setCenter(results[0].geometry.location);
            // set longitude and latitude in format (latitude,longitude) ex-> (53.3835946, -6.24807850000002)
            document.getElementById('latLng').value = results[0].geometry.location;


            var marker = new google.maps.Marker({
                map: map,
                position: results[0].geometry.location
            });
            //enable submit button
            var submit = $("#submit_button");
            submit.attr('disabled', false);
        } else {
            alert("Geocode was not successful for the following reason: " + status);
        }
    });
}

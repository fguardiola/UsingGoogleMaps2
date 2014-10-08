var geocoder;
var map;
$(document).ready(function () {
    initialize();
});
function initialize() {

                
    var dublinCoordinates = new google.maps.LatLng(53.350073, -6.261553);

    var mapOptions = {
        center: dublinCoordinates,
        zoom: 13,
        mapTypeId: google.maps.MapTypeId.ROADMAP
    };
    // This makes the div with id "map_canvas" a google map
    var map = new google.maps.Map(document.getElementById("map_canvas"),
        mapOptions);


    //To show the markers of the pubs with the information
    var pubs = @Html.Raw(Json.Encode(Model));


    // Using the JQuery "each" selector to iterate through the JSON list and drop marker pins


    //array to store the locations of deals
    var cluster=[];
    $.each(pubs, function (i, item) {
                
                   
        cluster.push(item);//
                
                
    });
    console.log(cluster);
    //alert("cluster created ");

    $.each(pubs, function (i, item) {
        var srtingLatLong= item.LatLng.replace(/[()]/g,'');// we have stored LatLong like (lat,long) and the function requires Lat and Long separated
        var str=srtingLatLong.split(",");
        var sameLatLongFlag=false;
        //recorrer array
        var text="";
        for (i=0; i < cluster.length; i++) {
                       
            if (item.LatLng==cluster[i].LatLng) {
                           
                text = text + " ------------------------------------- " + cluster[i].InfoWindowContentString;
            }                   
                      
        }
                   
        var pubmarker = new google.maps.Marker({
            'position': new google.maps.LatLng(parseFloat(str[0]),parseFloat(str[1])),
            'map': map,
            'title': item.PubName
        });

        pubmarker.setIcon('http://maps.google.com/mapfiles/ms/icons/red-dot.png');


        // put in some information about each json object - in this case, the opening hours.
        var infowindow = new google.maps.InfoWindow({
            content:text //content string
        });

        // finally hook up an "OnClick" listener to the map so it pops up out info-window when the marker-pin is clicked!
        google.maps.event.addListener(pubmarker, 'click', function () {
            infowindow.open(map, pubmarker);
        });
    })

}
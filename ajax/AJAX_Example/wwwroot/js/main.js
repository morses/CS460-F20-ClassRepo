
console.log("hello");

$("#request").click(function () {
    let n = $("#count").val();
    let address = "/apiv3/random/numbers/" + n;
    $.ajax({
        type: "GET",
        dataType: "json",
        url: address,
        success: displayNumbers,
        error: errorOnAjax
    });
});

$("#earthquake").click(function () {
    console.log("earthquake button pressed");
    let address = "apiv3/earthquakes/recent";
    let params = { hour: 4, day: 56 };
    $.ajax({
        type: "POST",
        dataType: "json",
        url: address,
        data: params,
        success: displayEarthquakes,
        error: errorOnAjax
    });
});

function errorOnAjax() {
    console.log("ERROR in ajax request");
}

function displayNumbers(data) {
    console.log(data);
    $("#message").text(data["message"]);
    $("#num").text(data.num);
    $("#numbers").text(data["range"].join(","));
    var trace = {
        x: data.domain,
        y: data.range,
        mode: 'lines',
        type: 'scatter'
    };
    var plotData = [trace];
    var layout = {};
    Plotly.newPlot('theplot', plotData, layout);
}

function displayEarthquakes(data) {
    $("#theQuakes").empty();
    for (let i = 0; i < data.length; ++i) {
        $("#theQuakes").append($("<li>" + data[i]["magnitude"] + ": " + data[i]["location"] + "</li>"));
    }
}
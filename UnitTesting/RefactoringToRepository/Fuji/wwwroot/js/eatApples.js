// For the home page where the logged in user can eat apples and see
// total apples consumed

$('#listOfApples > button').click(function () {
    var appleID = this.id.substring(1);      // remove leading 'a'
    console.log('Button with id = ' + appleID + '  clicked');
    // send of an ajax request to our ate-an-apple endpoint
    $.ajax({
        url: '/Apples/Ate',
        data: { id: appleID },
        method: 'POST',
        success: updateApplesEaten
    });
});

function updateApplesEaten(data) {
    console.log(data);
    console.log('Updating apples eaten');
    $.ajax({
        url: '/Apples/Eaten',
        method: 'GET',
        success: function (data) {
            console.log(data);
            if (!data.success)
                return;
            let appleList = $('<ul class="list-group">');
            $.each(data.apples, function (i) {
                let li = $(`<li class="list-group-item"><span class="badge badge-primary badge-pill">${data.apples[i].total}</span> ${data.apples[i].varietyName}</li>`).appendTo(appleList);
            });
            $('#appleTotals')
                .empty()
                .append($('<p>Total apples eaten:</p>'))
                .append(appleList);
        }
    });
}

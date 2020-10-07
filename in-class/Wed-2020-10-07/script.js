console.log("Yes, it's being run");

function svgCircle(width,height,radius,index){
    const cx = width/2;
    const cy = height/2;
    const svg = `<svg width="${width}" height="${height}" xmlns="http://www.w3.org/2000/svg">
    <circle cx="${cx}" cy="${cy}" r="${radius}" fill="gray" />
    <text x="${0.8*cx}" y="${1.2*cy}" class="bigText">${index}</text>
</svg>`;
    return svg
}

$("#generateButton").click(function(){
    console.log("button clicked!");

    let input = $('#programInput').val().split("x");
    let rows = input[0];
    let cols = input[1];
    console.log(rows,cols);

    let table = $("#output");
    for(let i = 0; i < rows; i++)
    {
        let row = $("<tr></tr>");
        for(let j = 0; j < cols; j++)
        {
            row.append(`<td>${svgCircle(100,100,50,i*j)}</td>`)
        }
        table.append(row);
    }
    

});
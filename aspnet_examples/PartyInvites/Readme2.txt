@model string

@{
    // in the controller
    ViewBag.htmlColor = "#AA34F3";
    ViewData["htmlColor2"] = "#123456";
}


In RGBColor.cshtml:


<div id="colorSquare" style="background-color:@Model"></div>
or if using ViewBag
<div id="colorSquare" style="background-color:@ViewBag.htmlColor"></div>

would render as

<div id="colorSquare">#AA34F3</div>

// Color interpolation View
@model HW3Project.Models.ColorInterpolation

@Model.firstColor
@Model.lastColor
@Model.count
@Model.outputColorList

<table>
    @foreach(string color in Model.outputColorList)
    {
        <tr>@color</tr>
    }
</table>

// some other View
@model IEnumerable<int>

<p>@Model.Count()</p>
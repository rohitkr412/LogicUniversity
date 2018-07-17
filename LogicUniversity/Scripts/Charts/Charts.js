
/* 
 * Chart.js usage:
 * Just add in <div id="testChart"></div> in the html file
 * 
 */
window.onload = function () {

    // A test chart to show that the javascript is running
    if ($("#testChart")) {
        var chart = new CanvasJS.Chart("testChart", {
            theme: "theme2",
            animationEnabled: true,
            title: {
                text: "Simple Column Chart in ASP.NET MVC"
            },
            subtitles: [
                { text: "Try Resizing the Browser" }
            ],
            data: [
                {
                    type: "column", //change type to bar, line, area, pie, etc
                    dataPoints: [
                        { x: 10, y: 71 },
                        { x: 20, y: 55 },
                        { x: 30, y: 50 },
                        { x: 40, y: 65 },
                        { x: 50, y: 95 },
                        { x: 60, y: 68 },
                        { x: 70, y: 28 },
                        { x: 80, y: 34 },
                        { x: 90, y: 14 }
                    ]
                }
            ]
        });
        chart.render();
    }

    // A chart showing purchase orders
    // A test chart to show that the javascript is running
    // TODO: Implement the chart.
    if ($("#purchaseOrderChart")) {
        var chart = new CanvasJS.Chart("purchaseOrderChart", {
            theme: "theme2",
            animationEnabled: true,
            title: {
                text: "Simple Column Chart in ASP.NET MVC"
            },
            subtitles: [
                { text: "Try Resizing the Browser" }
            ],
            data: [
                {
                    type: "line", //change type to bar, line, area, pie, etc
                    dataPoints: [
                        { x: 10, y: 71 },
                        { x: 20, y: 55 },
                        { x: 30, y: 50 },
                        { x: 40, y: 65 },
                        { x: 50, y: 95 },
                        { x: 60, y: 68 },
                        { x: 70, y: 28 },
                        { x: 80, y: 34 },
                        { x: 90, y: 14 }
                    ]
                }
            ]
        });
        chart.render();
    }
};
$(document).ready(function () {
    $("#QuestionPaperId").val()
    var pieData = [
        { data: $("#leftQuestions").val(), color: '#FFC107', label: 'Left Questions' },
        { data: $("#correctQuestions").val(), color: '#8BC34A', label: 'Correct Answers' },
        { data: $("#wrongQuestions").val(), color: '#F44336', label: 'Wrong Answers' },


    ];

    var pieDataAll = [
        { data: ((100 * $("#TimeTaken").val()) / $("#Duration").val()), color: '#03A9F4', label: 'Time Taken' },
        { data: ((100 * $("#leftTime").val()) / $("#Duration").val()), color: '#8BC34A', label: 'Time Left' },
    ];
    /* Pie Chart */

    if ($('#pie-chart')[0]) {
        $.plot('#pie-chart', pieData, {
            series: {
                pie: {
                    show: true,
                    stroke: {
                        width: 2,
                    },
                },
            },
            legend: {
                container: '.flc-pie',
                backgroundOpacity: 0.5,
                noColumns: 0,
                backgroundColor: "white",
                lineWidth: 0
            },
            grid: {
                hoverable: true,
                clickable: true
            },
            tooltip: true,
            tooltipOpts: {
                content: "%p.0%, %s", // show percentages, rounding to 2 decimal places
                shifts: {
                    x: 20,
                    y: 0
                },
                defaultTheme: false,
                cssClass: 'flot-tooltip'
            }

        });
    }

    /* Donut Chart */

    if ($('#donut-chart')[0]) {
        $.plot('#donut-chart', pieDataAll, {
            series: {
                pie: {
                    innerRadius: 0.5,
                    show: true,
                    stroke: {
                        width: 2,
                    },
                },
            },
            legend: {
                container: '.flc-donut',
                backgroundOpacity: 0.5,
                noColumns: 0,
                backgroundColor: "white",
                lineWidth: 0
            },
            grid: {
                hoverable: true,
                clickable: true
            },
            tooltip: true,
            tooltipOpts: {
                content: "%p.0%, %s", // show percentages, rounding to 2 decimal places
                shifts: {
                    x: 20,
                    y: 0
                },
                defaultTheme: false,
                cssClass: 'flot-tooltip'
            }

        });
    }
});
"use strict";

var ChromosomeAnalysis = function () {
    // Maintain chart state
    var chart = { self: null, rendered: false };

    // Card content update function
    var updateCardContent = function (determinedSex, fileName) {
        var sampleNameElement = document.getElementById("sampleName");
        var sexElement = document.getElementById("determinedSex");
        var imageElement = document.getElementById("genderImage");
        var processingStateElement = document.getElementById("processingState");

        // Handle processing state visibility
        if (processingStateElement) {
            if (window.geneticData && window.geneticData.length > 0) {
                processingStateElement.classList.add('d-none');
            }
        }

        // Update sample name
        if (sampleNameElement) {
            sampleNameElement.textContent = fileName || 'Unknown Sample';
        }

        // Handle sex determination display
        if (sexElement) {
            if (!determinedSex || determinedSex.trim() === '' || determinedSex === 'None') {
                sexElement.innerHTML = `
                    <div class="d-flex align-items-center gap-2">
                        <div class="spinner-border spinner-border-sm text-primary" style="width: 1rem; height: 1rem;"></div>
                        <span>Processing genetic data... Please wait.</span>
                    </div>`;
                if (imageElement) {
                    imageElement.src = "/assets/media/svg/avatars/blank.svg";
                }
                return "#808080"; // Gray for undetermined
            }

            // Handle known states
            switch (determinedSex.trim()) {
                case "Possible Female":
                    sexElement.textContent = determinedSex;
                    if (imageElement) {
                        imageElement.src = "/assets/media/svg/avatars/014-girl-7.svg";
                    }
                    return "#FF69B4"; // Pink

                case "Possible Male":
                    sexElement.textContent = determinedSex;
                    if (imageElement) {
                        imageElement.src = "/assets/media/svg/avatars/029-boy-11.svg";
                    }
                    return "#003DA5"; // Blue

                default:
                    sexElement.innerHTML = `
                        <div class="d-flex align-items-center gap-2">
                            <div class="spinner-border spinner-border-sm text-primary" style="width: 1rem; height: 1rem;"></div>
                            <span>Processing genetic data... Please wait.</span>
                        </div>`;
                    if (imageElement) {
                        imageElement.src = "/assets/media/svg/avatars/blank.svg";
                    }
                    return "#808080"; // Gray
            }
        }
    };

    var updateChart = function (selectedData) {
        if (!chart.self || !selectedData) return;

        var chartColor = updateCardContent(selectedData.determinedSex, selectedData.fileName);

        // Update chart data with animation
        chart.self.updateOptions({
            colors: [chartColor],
            series: [{
                name: "Depth",
                data: selectedData.depths || []
            }]
        }, true, true); // Animate the update
    };

    var initChart = function () {
        var element = document.getElementById("kt_charts_widget_18");
        if (!element) {
            console.error("Chart element not found");
            return;
        }

        // Validate genetic data
        if (!window.geneticData || !Array.isArray(window.geneticData) || window.geneticData.length === 0) {
            element.style.height = "200px";
            element.innerHTML = `
                <div class="d-flex flex-column align-items-center justify-content-center h-100 p-8">
                    <div class="mb-3">
                        <i class="bi bi-bar-chart fs-3x text-muted"></i>
                    </div>
                    <h3 class="text-gray-600 fs-4 mb-3">No Data Available</h3>
                    <div class="text-gray-500 text-center fs-6">
                        We are working on your genetic samples to view chromosome depth analysis
                    </div>
                </div>`;
            return;
        }

        var selector = document.getElementById('sampleSelector');
        if (!selector) {
            console.error("Sample selector not found");
            return;
        }

        // Get selected data
        var selectedFileName = selector.value;
        var selectedData = window.geneticData.find(sample => sample.fileName === selectedFileName);

        if (!selectedData) {
            console.error("No data found for selected sample:", selectedFileName);
            return;
        }

        // Update UI and get chart color
        var chartColor = updateCardContent(selectedData.determinedSex, selectedData.fileName);

        // Chart configuration
        var options = {
            series: [{
                name: "Depth",
                data: selectedData.depths || []
            }],
            chart: {
                fontFamily: "inherit",
                type: "bar",
                height: 325,
                toolbar: { show: false },
                animations: {
                    enabled: true,
                    easing: 'easeinout',
                    speed: 500,
                    animateGradually: {
                        enabled: true,
                        delay: 50
                    },
                    dynamicAnimation: {
                        enabled: true,
                        speed: 500
                    }
                }
            },
            plotOptions: {
                bar: {
                    horizontal: false,
                    columnWidth: "70%",
                    borderRadius: 5,
                    dataLabels: { position: "top" },
                    startingShape: "flat"
                },
            },
            dataLabels: {
                enabled: false,
                formatter: function (val) {
                    return val.toFixed(2);
                }
            },
            xaxis: {
                categories: [
                    "chr1", "chr2", "chr3", "chr4", "chr5", "chr6", "chr7", "chr8",
                    "chr9", "chr10", "chr11", "chr12", "chr13", "chr14", "chr15", "chr16",
                    "chr17", "chr18", "chr19", "chr20", "chr21", "chr22", "chrX", "chrY"
                ],
                axisBorder: { show: false },
                axisTicks: { show: false },
                labels: {
                    style: {
                        colors: '#A1A5B7',
                        fontSize: '12px',
                    },
                    rotate: -45
                }
            },
            yaxis: {
                labels: {
                    style: {
                        colors: '#A1A5B7',
                        fontSize: '12px'
                    },
                    formatter: function (val) {
                        return val.toFixed(2);
                    }
                }
            },
            fill: { opacity: 1 },
            states: {
                normal: { filter: { type: 'none', value: 0 } },
                hover: { filter: { type: 'none', value: 0 } },
                active: {
                    allowMultipleDataPointsSelection: false,
                    filter: { type: 'none', value: 0 }
                }
            },
            tooltip: {
                style: { fontSize: '12px' },
                y: {
                    formatter: function (val) {
                        return val.toFixed(2);
                    }
                }
            },
            colors: [chartColor],
            grid: {
                borderColor: '#E4E6EF',
                strokeDashArray: 4,
                padding: { top: 0, bottom: 0, left: 0, right: 0 }
            }
        };

        // Create new chart only if it doesn't exist
        if (!chart.self) {
            chart.self = new ApexCharts(element, options);
            chart.self.render().then(() => {
                chart.rendered = true;
            }).catch((error) => {
                console.error('Error rendering chart:', error);
            });
        } else {
            // Update existing chart
            updateChart(selectedData);
        }
    };

    return {
        init: function () {
            try {
                // Validate genetic data availability
                if (typeof window.geneticData === 'undefined') {
                    console.error('Genetic data not found');
                    return;
                }

                // Initialize chart
                initChart();

                // Handle sample selection change
                var selector = document.getElementById('sampleSelector');
                if (selector) {
                    selector.addEventListener('change', function () {
                        var selectedData = window.geneticData.find(sample => sample.fileName === this.value);
                        if (selectedData) {
                            updateChart(selectedData);
                        }
                    });
                }

            } catch (error) {
                console.error('Error initializing chromosome analysis:', error);
            }
        }
    };
}();

// Initialize when DOM is loaded
document.addEventListener('DOMContentLoaded', function () {
    ChromosomeAnalysis.init();
});
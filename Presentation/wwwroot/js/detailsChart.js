$(function () {
    var state = {
        webSiteId: null,
        PageViewCountChartNode: null,
        URLRoutesVisitedChartNode: null,
        BrowserTypeStatsChartNode: null,
        SystemStatsChartNode: null,
        ScreenSizeStatsChartNode: null,
        LocationOfVisitorsStatsChartNode: null,
        InteractionStats_NotFoundText: null,
        BrowserStats_NotFoundText: null,
        SystemStats_NotFoundText: null,
        ScreenStats_NotFoundText: null,
        LocationStats_NotFoundText: null,
        newGradArr: ['#CC2936', '#08415C'],
        Error_Message: 'Nothing Found Just Yet',
        StatsSerachBtn: document.getElementById("StatsSerach"),
        WebSiteTimeScaleTitle: $("#DateTimeDisplay"),
        CurrentStartDate: null,
        CurrentEndDate: null,
        baseDateTime: null,
        Chart_PageViewStats: null,
        Chart_InteractionStats: null,
        Chart_BrowserStats: null,
        Chart_SystemStats: null,
        Chart_ScreenSizeStats: null,
        Chart_LocationStats: null,
        Set_BaseDate: function () {
            if (state.isDemo) {
                state.CurrentStartDate = moment().subtract(200, "days").format();
            } else {
                state.CurrentStartDate = moment().subtract(7, "days").format();
            }
            state.CurrentEndDate = moment().format();

            state.WebSiteTimeScaleTitle.text(moment(state.CurrentStartDate).format('MMMM d, YYYY') + " " + " to " + " " + moment(state.CurrentEndDate).format('MMMM d, YYYY'));

            state.baseDateTime = {
                CurrentStartDate: state.CurrentStartDate,
                CurrentEndDate: state.CurrentEndDate
            }
        },
        Hid_NotFoundText: function () {
            state.InteractionStats_NotFoundText.hidden = true;
            state.BrowserStats_NotFoundText.hidden = true;
            state.SystemStats_NotFoundText.hidden = true;
            state.ScreenStats_NotFoundText.hidden = true;
            state.LocationStats_NotFoundText.hidden = true;
        },
        Display_Error_Message: function (Data, Element, Error_Msg) {
           
            if (Data === null || (Data.length === 0 && typeof Data != 'object')) {
                Element.innerHTML = Error_Msg;
                Element.hidden = false;
            }
            sum = 0;
            for (var i = 0; i < Data.length; i++) {
                sum = sum + Data[i];
            }
            if (sum === 0 && typeof Data != 'object') {
                Element.innerHTML = Error_Msg;
                Element.hidden = false;
            }
        },
        GetDeviceType: function () {
            var ua = navigator.userAgent;
            if (/(tablet|ipad|playbook|silk)|(android(?!.*mobi))/i.test(ua)) {
                return "tablet";
            }
            if (
                /Mobile|iP(hone|od|ad)|Android|BlackBerry|IEMobile|Kindle|Silk-Accelerated|(hpw|web)OS|Opera M(obi|ini)/.test(
                    ua
                )
            ) {
                return "mobile";
            }
            return "desktop";
        },
        Refresh_Chart_UI: function (methodType, urlName, data, chart,
            ChartDataTranslator, NotFoundText, dataMapper) {
            axios({ method: methodType, url: urlName, data: data, headers: state.headers })
                .then(function (response) {
                    var data = response.data;
                    if (NotFoundText) {
                        state.Display_Error_Message(data, NotFoundText, state.Error_Message)
                    }
                    chart.data = ChartDataTranslator(data, dataMapper);
                    chart.update();
                })
        },
        SetUp_Chart_UI: function () {
            state.Hid_NotFoundText();
            var methodType = 'post';
            var PageView_DataTranslator = function (Data, dataMapper) {
                var MappedData = dataMapper(Data);
                return {
                    labels: MappedData.GLabels,
                    datasets: [
                        {
                            borderColor: 'rgb(255, 99, 132)',
                            label: "Counts",
                            data: MappedData.GData
                        }
                    ],
                    fill: false
                }
            };
            var Generic_DataTranslator = function (Data, dataMapper) {
                var MappedData = dataMapper(Data);
                return {
                    labels: MappedData.GLabels,
                    datasets: [
                        {
                            label: "Counts",
                            backgroundColor: chroma.scale(state.newGradArr).mode('lch').colors(MappedData.GData.length),
                            data: MappedData.GData
                        }
                    ]
                }
            };
            var Location_Generic_DataTranslator = function (Data, dataMapper) {
                var MappedData = dataMapper(Data);
                return {
                    labels: MappedData.GLabels,
                    datasets: [
                        {
                            label: "Counts",
                            backgroundColor: chroma.scale(state.newGradArr).mode('lch').colors(MappedData.GData.length),
                            data: MappedData.GData
                        }
                    ]
                }
            };
            var dataMapper_ForPageView = function (data) {
                var GLabels = [];
                var GData = [];
                //display: false,
                var count = 0;
                data.forEach(function (stat) {
                    if (stat.count > 0) {
                        count++;
                        GLabels.push(moment(stat.createdAt).local().format('MMMM Do YYYY, h:mm:ss a'));
                        GData.push(stat.count);
                    }
                });
                if (count > 60) {
                    state.Chart_PageViewStats.options.scales = state.scaleOptionsBigCountViews;
                }
                return {
                    GLabels: GLabels,
                    GData: GData
                }
            }
            var dataMapper_ScreenSize = function (data) {
                var GLabels = [
                    'Phones',
                    'Large Phones',
                    'Tablets',
                    'Desktops',
                    'Monitors 4K Plus'
                ];
                var hashName = {
                    'numberOfPhones': 'Phones',
                    'largePhonesSmallTablets': 'Large Phones',
                    'tabletsSmallLaptops': 'Tables',
                    'computerMonitors': 'Desktops',
                    'computerMonitors4K': 'Monitors 4K Plus'
                }
                var GData = [];
                for (var key in data) {
                    for (var label in GLabels) {
                        if (hashName[key] === GLabels[label]) {
                            GData.push(data[key])
                        }
                    }
                }
                
                return {
                    GLabels: GLabels,
                    GData: GData
                }
            }
            var dataMapper_InteractionStats = function (data) {
                var GLabels = [];
                var GData = [];
                data.forEach(function (stat) {
                    GLabels.push(stat.path);
                    GData.push(stat.total);
                });
                return {
                    GLabels: GLabels,
                    GData: GData
                }
            }
            var dataMapper_BrowserStats = function (data) {
                var GLabels = []
                var GData = []
                data.forEach(function (stat) {
                    GLabels.push(stat.browser + " " + stat.version);
                    GData.push(stat.count);
                });
                return {
                    GLabels: GLabels,
                    GData: GData
                }
            }
            var dataMapper_SystemStats = function (data) {
                var GLabels = [];
                var GData = [];
                data.forEach(function (stat) {
                    GLabels.push(stat.platform + " " + stat.version);
                    GData.push(stat.count);
                });
                return {
                    GLabels: GLabels,
                    GData: GData
                }
            }
            var dataMapper_LocationStats = function (data) {
                var GLabels = []
                var GData = []
                data.forEach(function (stat) {
                    GLabels.push(stat.location);
                    GData.push(stat.count);
                });
                return {
                    GLabels: GLabels,
                    GData: GData
                }
            }
            var url_Charts = [
                {
                    chart: state.Chart_PageViewStats,
                    data_function: PageView_DataTranslator,
                    chart_url: '/api/Stats/PageViewCountStats/' + state.webSiteId,
                    notFoundText: null,
                    dataMapper: dataMapper_ForPageView,
                },
                {
                    chart: state.Chart_InteractionStats,
                    data_function: Generic_DataTranslator,
                    chart_url: '/api/Stats/InteractionStats/' + state.webSiteId,
                    notFoundText: state.InteractionStats_NotFoundText,
                    dataMapper: dataMapper_InteractionStats,
                },
                {
                    chart: state.Chart_BrowserStats,
                    data_function: Generic_DataTranslator,
                    chart_url: '/api/Stats/BrowserStats/' + state.webSiteId,
                    notFoundText: state.BrowserStats_NotFoundText,
                    dataMapper: dataMapper_BrowserStats,
                },
                {
                    chart: state.Chart_SystemStats,
                    data_function: Generic_DataTranslator,
                    chart_url: '/api/Stats/SystemStats/' + state.webSiteId,
                    notFoundText: state.SystemStats_NotFoundText,
                    dataMapper: dataMapper_SystemStats,
                },
                {
                    chart: state.Chart_ScreenSizeStats,
                    data_function: Generic_DataTranslator,
                    chart_url: '/api/Stats/ScreenSizeStats/' + state.webSiteId,
                    notFoundText: state.ScreenStats_NotFoundText,
                    dataMapper: dataMapper_ScreenSize,
                },
                {
                    chart: state.Chart_LocationStats,
                    data_function: Location_Generic_DataTranslator,
                    chart_url: '/api/Stats/LocationStats/' + state.webSiteId,
                    notFoundText: state.LocationStats_NotFoundText,
                    dataMapper: dataMapper_LocationStats,
                }
            ];
            var dateData = {
                CurrentStartDate: state.CurrentStartDate,
                CurrentEndDate: state.CurrentEndDate
            };

            for (var i = 0; i < url_Charts.length; i++) {
                state.Refresh_Chart_UI(
                    methodType, url_Charts[i].chart_url,
                    dateData, url_Charts[i].chart,
                    url_Charts[i].data_function,
                    url_Charts[i].notFoundText,
                    url_Charts[i].dataMapper
                );
            }
        },
        SetUp_DateTimePickers: function () {
            jQuery.datetimepicker.setLocale('en');

            $('#date_timepicker_start')[0].value = moment(state.CurrentStartDate).format("MM/DD/YYYY");;
            $('#date_timepicker_start').datetimepicker({
                startDate: state.CurrentStartDate,
                format: 'm/d/y',
                onShow: function (ct) {
                    this.setOptions({
                        maxDate: $('#date_timepicker_end').val() ? jQuery('#date_timepicker_end').val() : false
                    })
                },
                timepicker: false,
                onChangeDateTime: function (dp, $input) {
                    state.CurrentStartDate = moment($input.val()).format();
                }
            });

            $('#date_timepicker_end')[0].value = moment(state.CurrentEndDate).format("MM/DD/YYYY");;
            $('#date_timepicker_end').datetimepicker({
                startDate: state.CurrentEndDate,
                format: 'm/d/y',
                onShow: function (ct) {
                    this.setOptions({
                        minDate: $('#date_timepicker_start').val() ? jQuery('#date_timepicker_start').val() : false
                    })
                },
                timepicker: false,
                onChangeDateTime: function (dp, $input) {
                    state.CurrentEndDate = moment($input.val()).add(1, "days").format();
                }
            });
        },
        SetUp_DateTime_Event_Listeners: function () {
            state.StatsSerachBtn.addEventListener("click", function (e) {
                e.preventDefault()
                state.SetUp_Chart_UI();
            });
        },
        headers: {
            'Content-Type': 'application/json; charset=utf-8',
            Accept: 'application/json'
        },
        titleOptions: {
            fontSize: 14,
            fontColor: 'black',
            display: true,
        },
        scaleOptions: {
            yAxes: [{
                ticks: {
                    beginAtZero: true,
                    fontSize: 14,
                    fontFamily: "'Roboto', sans-serif",
                    fontColor: 'black',
                    fontStyle: '500'
                }
            }],
            xAxes: [{
                ticks: {
                    beginAtZero: true,
                    stepSize: 10,
                    fontSize: 14, fontFamily: "'Roboto', sans-serif", fontColor: 'black', fontStyle: '500'
                }
            }]
        },
        scaleOptionsBigCountViews: {
            yAxes: [{
                ticks:
                {
                    beginAtZero: true,
                    fontSize: 14,
                    fontFamily: "'Roboto', sans-serif",
                    fontColor: 'black',
                    fontStyle: '500'
                }
            }],
            xAxes: [{
                ticks: {
                    display: false,
                }
            }]
        },
        scaleOptionsCountViews: {
            yAxes: [{
                ticks:
                {
                    beginAtZero: true,
                    fontSize: 14,
                    fontFamily: "'Roboto', sans-serif",
                    fontColor: 'black',
                    fontStyle: '500'
                }
            }],
            xAxes: [{
                ticks: {
                    beginAtZero: true,
                    stepSize: 30,
                    fontSize: 13,
                    fontFamily: "'Roboto', sans-serif",
                    fontColor: 'black',
                    fontStyle: '500',
                    autoSkip: true,
                    maxRotation: 300,
                }
            }]
        },
        SetUp_Refs: function () {
            state.webSiteId = document.querySelector('script[data-website-id]').getAttribute("data-website-id");
            state.isDemo = (document.querySelector('script[data-is-demo]').getAttribute("data-is-demo") === 'true') || false;
            state.PageViewCountChartNode = document.getElementById("bar-chart-PageViewCounts");
            state.URLRoutesVisitedChartNode = document.getElementById("bar-chart-InteractionStats");
            state.BrowserTypeStatsChartNode = document.getElementById("bar-chart-BrowserStats");
            state.SystemStatsChartNode = document.getElementById("bar-chart-SystemStats");
            state.ScreenSizeStatsChartNode = document.getElementById("bar-chart-ScreenStats");
            state.LocationOfVisitorsStatsChartNode = document.getElementById("bar-chart-LocationStats");
            state.InteractionStats_NotFoundText = document.getElementById("InteractionStats-NotFoundText");
            state.BrowserStats_NotFoundText = document.getElementById("BrowserStats-NotFoundText");
            state.SystemStats_NotFoundText = document.getElementById("SystemStats-NotFoundText");
            state.ScreenStats_NotFoundText = document.getElementById("ScreenStats-NotFoundText");
            state.LocationStats_NotFoundText = document.getElementById("LocationStats-NotFoundText");
            state.Chart_PageViewStats = new Chart(state.PageViewCountChartNode, {
                type: 'line',
                options: {
                    legend: { display: false },
                    title: {
                        fontSize: state.titleOptions.fontSize,
                        fontColor: state.titleOptions.fontColor,
                        display: state.titleOptions.display,
                        text: 'Page View Counts'
                    },
                    scales: state.scaleOptionsCountViews
                }
            });
            state.Chart_InteractionStats = new Chart(state.URLRoutesVisitedChartNode, {
                type: 'horizontalBar',
                options: {
                    legend: { display: false },
                    title: {
                        fontSize: state.titleOptions.fontSize,
                        fontColor: state.titleOptions.fontColor,
                        display: state.titleOptions.display,
                        text: 'Page Interactions'
                    },
                    scales: state.scaleOptions
                }
            });
            state.Chart_BrowserStats = new Chart(state.BrowserTypeStatsChartNode, {
                type: 'horizontalBar',
                options: {
                    legend: { display: false },
                    title: {
                        fontSize: state.titleOptions.fontSize,
                        fontColor: state.titleOptions.fontColor,
                        display: state.titleOptions.display,
                        text: 'Browser Types'
                    },
                    scales: state.scaleOptions
                }
            });
            state.Chart_SystemStats = new Chart(state.SystemStatsChartNode, {
                type: 'horizontalBar',
                options: {
                    legend: { display: false },
                    title: {
                        fontSize: state.titleOptions.fontSize,
                        fontColor: state.titleOptions.fontColor,
                        display: state.titleOptions.display,
                        text: 'System Types'
                    },
                    scales: state.scaleOptions
                }
            });
            state.Chart_ScreenSizeStats = new Chart(state.ScreenSizeStatsChartNode, {
                type: 'horizontalBar',
                options: {
                    legend: { display: false },
                    title: {
                        fontSize: state.titleOptions.fontSize,
                        fontColor: state.titleOptions.fontColor,
                        display: state.titleOptions.display,
                        text: 'Screen Sizes'
                    },
                    scales: state.scaleOptions
                }
            });
            state.Chart_LocationStats = new Chart(state.LocationOfVisitorsStatsChartNode, {
                type: 'horizontalBar',
                options: {
                    legend: { display: false },
                    title: {
                        fontSize: state.titleOptions.fontSize,
                        fontColor: state.titleOptions.fontColor,
                        display: state.titleOptions.display,
                        text: 'Location Stats'
                    },
                    scales: state.scaleOptions
                }
            });
        },
        SetUp_ChartHeight: function () {
            if (state.GetDeviceType() === 'mobile') {
                Chart_InteractionStats.height = 80;
                Chart_BrowserStats.height = 40;
                Chart_SystemStats.height = 40;
                Chart_ScreenSizeStats.height = 40;
                Chart_LocationStats.height = 40;
            }
        },
        StartUp_View: function () {
            state.SetUp_Refs();
            state.Set_BaseDate();
            state.Hid_NotFoundText();
            state.SetUp_DateTimePickers();
            state.SetUp_DateTime_Event_Listeners();
            state.SetUp_Chart_UI();
            state.SetUp_ChartHeight();
        }
    }
    state.StartUp_View();
});
$(function () {
    var state = {
        webSiteId: document.querySelector('script[data-website-id]').getAttribute("data-website-id"),
        PageViewCountChartNode: document.getElementById("bar-chart-PageViewCounts"),
        URLRoutesVisitedChartNode: document.getElementById("bar-chart-InteractionStats"),
        BrowserTypeStatsChartNode: document.getElementById("bar-chart-BrowserStats"),
        SystemStatsChartNode: document.getElementById("bar-chart-SystemStats"),
        ScreenSizeStatsChartNode: document.getElementById("bar-chart-ScreenStats"),
        LocationOfVisitorsStatsChartNode: document.getElementById("bar-chart-LocationStats"),
        newGradArr: ['#CC2936', '#08415C'],
        InteractionStats_NotFoundText: document.getElementById("InteractionStats-NotFoundText"),
        BrowserStats_NotFoundText: document.getElementById("BrowserStats-NotFoundText"),
        SystemStats_NotFoundText: document.getElementById("SystemStats-NotFoundText"),
        ScreenStats_NotFoundText: document.getElementById("ScreenStats-NotFoundText"),
        LocationStats_NotFoundText: document.getElementById("LocationStats-NotFoundText"),
        Error_Message: 'Nothing Found Just Yet',
        StatsSerachBtn: document.getElementById("StatsSerach"),
        CurrentStartDate: null,
        CurrentEndDate: null,
        baseDateTime: null,
        SetBaseDate: function () {
            state.CurrentStartDate = moment().subtract(7, "days").format();
            state.CurrentEndDate = moment().format();
            state.baseDateTime = {
                CurrentStartDate: state.CurrentStartDate,
                CurrentEndDate: state.CurrentEndDate
            }
        },
        PageViewStatsChart: null,
        Hid_NotFoundText: function () {
            state.InteractionStats_NotFoundText.hidden = true;
            state.BrowserStats_NotFoundText.hidden = true;
            state.SystemStats_NotFoundText.hidden = true;
            state.ScreenStats_NotFoundText.hidden = true;
            state.LocationStats_NotFoundText.hidden = true;
        },
        DisplayMessage: function (Data, Element, Error_Msg) {
            if (Data === null || Data.length === 0) {
                Element.innerHTML = Error_Msg;
                Element.hidden = false;
            }
            sum = 0;
            for (var i = 0; i < Data.length; i++) {
                sum = sum + Data[i];
            }
            if (sum === 0) {
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
        headers: {
            'Content-Type': 'application/json; charset=utf-8',
            Accept: 'application/json'
        }
    }
    state.SetBaseDate();
    state.Hid_NotFoundText();
    jQuery.datetimepicker.setLocale('en');
    $('#date_timepicker_start').datetimepicker({
        format: 'Y/m/d',
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
    $('#date_timepicker_end').datetimepicker({
        format: 'Y/m/d',
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
    state.StatsSerachBtn.addEventListener("click", function (e) {
        e.preventDefault()
        axios({
            method: 'post', url: '/api/Stats/PageViewCountStats/' + state.webSiteId,
            data: {
                CurrentStartDate: state.CurrentStartDate,
                CurrentEndDate: state.CurrentEndDate
            },
            headers: state.headers
        }).then(function (response) {
            var data = response.data;
            var GLabels = [];
            var GData = [];
            data.forEach(function (stat) {
                GLabels.push(moment(stat.createdAt).local().format('YYYY-MM-DD HH:mm:ss'));
                GData.push(stat.count);
            });
            
            state.PageViewStatsChart.data = {
                labels: GLabels,
                datasets: [
                    {
                        borderColor: 'rgb(255, 99, 132)',
                        label: "Counts",
                        data: GData
                    }
                ],
                fill: false
            };
            state.PageViewStatsChart.update();
        })

    });
    var scaleOptions = {
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
    }
    var scaleOptionsCountViews = {
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
                stepSize: 10,
                fontSize: 12,
                fontFamily: "'Roboto', sans-serif",
                fontColor: 'black',
                fontStyle: '500',
                autoSkip: false,
                maxRotation: 90,
                minRotation: 90
            }
        }]
    }
    var titleOptions = {
        fontSize: 14,
        fontColor: 'black',
        display: true,
    }
    if (state.GetDeviceType() === 'mobile') {
        ISChart.height = 80;
        BSChart.height = 40;
        SysSChart.height = 40;
        ScreenSChart.height = 40;
        LocationSChart.height = 40;
    }


    axios({ method: 'post', url: '/api/Stats/PageViewCountStats/' + state.webSiteId, data: state.baseDateTime, headers: state.headers })
        .then(function (response) {
            var data = response.data;
            var GLabels = [];
            var GData = [];
            data.forEach(function (stat) {
                GLabels.push(moment(stat.createdAt).local().format('YYYY-MM-DD HH:mm:ss'));
                GData.push(stat.count);
            });
            state.PageViewStatsChart = new Chart(state.PageViewCountChartNode, {
                type: 'line',
                data: {
                    labels: GLabels,
                    datasets: [
                        {
                            borderColor: 'rgb(255, 99, 132)',
                            label: "Counts",
                            data: GData
                        }
                    ],
                    fill: false
                },
                options: {
                    legend: { display: false },
                    title: {
                        fontSize: 18,
                        fontColor: titleOptions.fontColor,
                        display: titleOptions.display,
                        text: 'Page View Counts'
                    },
                    scales: scaleOptionsCountViews
                }
            });
        })
        .catch(function (error) {
            console.log(error);
        })

    axios({ method: 'post', url: '/api/Stats/InteractionStats/' + state.webSiteId, data: state.baseDateTime, headers: state.headers })
        .then(function (response) {
            var data = response.data;
            var GLabels = [];
            var GData = [];
            state.DisplayMessage(data, state.InteractionStats_NotFoundText, state.Error_Message)
            data.forEach(function (stat) {
                GLabels.push(stat.path);
                GData.push(stat.total);
            });
            new Chart(state.URLRoutesVisitedChartNode, {
                type: 'horizontalBar',
                data: {
                    labels: GLabels,
                    datasets: [
                        {
                            label: "Counts",
                            backgroundColor: chroma.scale(state.newGradArr).mode('lch').colors(GData.length),
                            data: GData
                        }
                    ]
                },
                options: {
                    legend: { display: false },
                    title: {
                        fontSize: 18,
                        fontColor: titleOptions.fontColor,
                        display: titleOptions.display,
                        text: 'Page Interactions'
                    },
                    scales: scaleOptions
                }
            });
        })
        .catch(function (error) {
            console.log(error);
        })

    axios({ method: 'post', url: '/api/Stats/BrowserStats/' + state.webSiteId, data: state.baseDateTime, headers: state.headers })
        .then(function (response) {
            var data = response.data;
            state.DisplayMessage(data, state.BrowserStats_NotFoundText, state.Error_Message)
            var Blabels = []
            var Bdata = []
            data.forEach(function (stat) {
                Blabels.push(stat.browser + " " + stat.version);
                Bdata.push(stat.count);
            });
            new Chart(state.BrowserTypeStatsChartNode, {
                type: 'horizontalBar',
                data: {
                    labels: Blabels,
                    datasets: [
                        {
                            label: "Counts",
                            backgroundColor: chroma.scale(state.newGradArr).mode('lch').colors(Bdata.length),
                            data: Bdata
                        }
                    ]
                },
                options: {
                    legend: { display: false },
                    title: {
                        fontSize: titleOptions.fontSize,
                        fontColor: titleOptions.fontColor,
                        display: titleOptions.display,
                        text: 'Browser Stats'
                    },
                    scales: scaleOptions
                }
            });

        })
        .catch(function (error) {
            console.log(error);
        })

    axios({ method: 'post', url: '/api/Stats/SystemStats/' + state.webSiteId, data: state.baseDateTime, headers: state.headers })
        .then(function (response) {
            var data = response.data;
            state.DisplayMessage(data, state.SystemStats_NotFoundText, state.Error_Message)
            var Slabels = [];
            var Sdata = [];
            data.forEach(function (stat) {
                Slabels.push(stat.platform + " " + stat.version);
                Sdata.push(stat.count);
            });
            new Chart(state.SystemStatsChartNode, {
                type: 'horizontalBar',
                data: {
                    labels: Slabels,
                    datasets: [
                        {
                            label: "Counts",
                            backgroundColor: chroma.scale(state.newGradArr).mode('lch').colors(Sdata.length),
                            data: Sdata
                        }
                    ]
                },
                options: {
                    legend: { display: false },
                    title: {
                        fontSize: titleOptions.fontSize,
                        fontColor: titleOptions.fontColor,
                        display: titleOptions.display,
                        text: 'System Stats'
                    },
                    scales: scaleOptions
                }
            });

        })
        .catch(function (error) {
            console.log(error);
        })

    axios({ method: 'post', url: '/api/Stats/ScreenSizeStats/' + state.webSiteId, data: state.baseDateTime, headers: state.headers })
        .then(function (response) {
            var data = response.data;
            var Blabels = [
                'Phones',
                'Large Phones',
                'Tables',
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
            var Bdata = [];
            for (var key in data[0]) {
                for (var label in Blabels) {
                    if (hashName[key] === Blabels[label]) {
                        Bdata.push(data[0][key])
                    }
                }
            }
            console.log(Bdata)
            state.DisplayMessage(Bdata, state.ScreenStats_NotFoundText, state.Error_Message)

            new Chart(state.ScreenSizeStatsChartNode, {
                type: 'horizontalBar',
                data: {
                    labels: Blabels,
                    datasets: [
                        {
                            label: "Counts",
                            backgroundColor: chroma.scale(state.newGradArr).mode('lch').colors(Bdata.length),
                            data: Bdata
                        }
                    ]
                },
                options: {
                    legend: { display: false },
                    title: {
                        fontSize: titleOptions.fontSize,
                        fontColor: titleOptions.fontColor,
                        display: titleOptions.display,
                        text: 'Screen Size Stats'
                    },
                    scales: scaleOptions
                }
            });
        })
        .catch(function (error) {
            console.log(error);
        })

    axios({ method: 'post', url: '/api/Stats/LocationStats/' + state.webSiteId, data: state.baseDateTime, headers: state.headers })
        .then(function (response) {
            var data = response.data;
            state.DisplayMessage(data, state.LocationStats_NotFoundText, state.Error_Message)
            var Blabels = []
            var Bdata = []
            data.forEach(function (stat) {
                Blabels.push(stat.location);
                Bdata.push(stat.count);
            });
            new Chart(state.LocationOfVisitorsStatsChartNode, {
                type: 'horizontalBar',
                data: {
                    labels: Blabels,
                    datasets: [
                        {
                            label: "Counts",
                            backgroundColor: chroma.scale(state.newGradArr).mode('lch').colors(Bdata.length),
                            data: Bdata
                        }
                    ]
                },
                options: {
                    legend: { display: false },
                    title: {
                        fontSize: titleOptions.fontSize,
                        fontColor: titleOptions.fontColor,
                        display: titleOptions.display,
                        text: 'Location Stats'
                    },
                    scales: scaleOptions
                }
            });
        })
        .catch(function (error) {
            console.log(error);
        })


})
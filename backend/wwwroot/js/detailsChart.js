$(function () {
    let webSiteId = document.querySelector('script[data-website-id]').getAttribute("data-website-id");;
    function getDeviceType() {
        const ua = navigator.userAgent;
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
    };
    const ISChart = document.getElementById("bar-chart-InteractionStats");
    const BSChart = document.getElementById("bar-chart-BrowserStats");
    const SysSChart = document.getElementById("bar-chart-SystemStats");
    const ScreenSChart = document.getElementById("bar-chart-ScreenStats");
    const LocationSChart = document.getElementById("bar-chart-LocationStats");
    const startGradColor = '#CC2936';
    const endGradColor = '#08415C';
    const newGradArr = [startGradColor, endGradColor];
    const scaleOptions = {
        yAxes: [{ ticks: { fontSize: 14, fontFamily: "'Roboto', sans-serif", fontColor: 'black', fontStyle: '500' } }],
        xAxes: [{ ticks: {
                beginAtZero: true,
                stepSize: 10,
                fontSize: 14, fontFamily: "'Roboto', sans-serif", fontColor: 'black', fontStyle: '500'} }]
    }
    const titleOptions = {
        fontSize: 14,
        fontColor: 'black',
        display: true,
    }
    if (getDeviceType() === 'mobile') {
        ISChart.height = 80;
        BSChart.height = 40;
        SysSChart.height = 40;
        ScreenSChart.height = 40;
        LocationSChart.height = 40;
    }

    axios.get('/api/Stats/InteractionStats/' + webSiteId)
        .then(function (response) {
            let data = response.data;
            let Ilabels = [];
            let Idata = [];
            data.forEach(stat => {
                Ilabels.push(stat.path);
                Idata.push(stat.total);
            });
            new Chart(ISChart, {
                type: 'horizontalBar',
                data: {
                    labels: Ilabels,
                    datasets: [
                        {
                            label: "Counts",
                            backgroundColor: chroma.scale(newGradArr).mode('lch').colors(Idata.length),
                            data: Idata
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

    axios.get('/api/Stats/BrowserStats/' + webSiteId)
        .then(function (response) {
            let data = response.data;
            let Blabels = []
            let Bdata = []
            data.forEach(stat => {
                Blabels.push(stat.browser + " " + stat.version);
                Bdata.push(stat.count);
            });
            let newBSChart = new Chart(BSChart, {
                type: 'horizontalBar',
                data: {
                    labels: Blabels,
                    datasets: [
                        {
                            label: "Counts",
                            backgroundColor: chroma.scale(newGradArr).mode('lch').colors(Bdata.length),
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

    


    axios.get('/api/Stats/SystemStats/' + webSiteId)
        .then(function (response) {
            let data = response.data;
            let Slabels = [];
            let Sdata = [];
            data.forEach(stat => {
                Slabels.push(stat.platform + " " + stat.version);
                Sdata.push(stat.count);
            });
            new Chart(SysSChart, {
                type: 'horizontalBar',
                data: {
                    labels: Slabels,
                    datasets: [
                        {
                            label: "Counts",
                            backgroundColor: chroma.scale(newGradArr).mode('lch').colors(Sdata.length),
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

    axios.get('/api/Stats/ScreenSizeStats/' + webSiteId)
        .then(function (response) {
            let data = response.data;

            if (data.length === 0) {
                BSChart.hidden = true;
                document.getElementById("ScreenStats-text").innerHTML =
                    `<div style="padding-left: 200px">
                            <h3>Nothing Here Yet</h3>
                            <h5>Screen Stats</h5>
                        </div>`
            } else {
                let Blabels = [
                    'Phones',
                    'Large Phones',
                    'Tables',
                    'Desktops',
                    'Monitors 4K Plus'
                ]
                let Bdata = []
                for (const key of Object.keys(data[0])) {
                    Bdata.push(data[0][key]);
                }
                new Chart(ScreenSChart, {
                    type: 'horizontalBar',
                    data: {
                        labels: Blabels,
                        datasets: [
                            {
                                label: "Counts",
                                backgroundColor: chroma.scale(newGradArr).mode('lch').colors(Bdata.length),
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
            }

        })
        .catch(function (error) {
            console.log(error);
        })

    axios.get('/api/Stats/LocationStats/' + webSiteId)
        .then(function (response) {
            let data = response.data;
            let Blabels = []
            let Bdata = []
            data.forEach(stat => {
                Blabels.push(stat.location);
                Bdata.push(stat.count);
            });
            new Chart(LocationSChart, {
                type: 'horizontalBar',
                data: {
                    labels: Blabels,
                    datasets: [
                        {
                            label: "Counts",
                            backgroundColor: chroma.scale(newGradArr).mode('lch').colors(Bdata.length),
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
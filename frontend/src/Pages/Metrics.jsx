import React, { Component } from "react";
import { view } from '@risingstack/react-easy-state';
import Paper from '@material-ui/core/Paper';
import Grid from '@material-ui/core/Grid';
import {
    VictoryBar, VictoryChart, VictoryAxis,
    VictoryTheme
} from 'victory';
import MaterialTable from "material-table";

const data = [
    { quarter: 1, earnings: 13000 },
    { quarter: 2, earnings: 16500 },
    { quarter: 3, earnings: 14250 },
    { quarter: 4, earnings: 19000 }
];
function createData(name, count) {
    return { name, count };
}

const rows = [
    createData('open-source/victory/docs/victory-histogram', 159,),
    createData('open-source/victory/docs', 237),
    createData('open-source/victory', 262),
    createData('Cupcake', 305),
    createData('Gingerbread', 356),
];
class Metrics extends Component {
    render() {
        return (
            <div>
                <h1>Metrics</h1>
                <Grid container spacing={3}>
                    <Grid item xs={12}>
                        <Grid container spacing={3}>
                            <Grid item xs={1}></Grid>
                            <Grid item xs={10}>
                                <MaterialTable
                                    title="One Detail Panel Preview"
                                    columns={[
                                        { title: 'URL', field: 'name' },
                                        { title: 'Count', field: 'count' },
                                    ]}
                                    data={rows}
                                // detailPanel={rowData => {
                                //     return (
                                //         <iframe
                                //             width="100%"
                                //             height="315"
                                //             src="https://www.youtube.com/embed/C0DPdy98e4c"
                                //             frameborder="0"
                                //             allow="accelerometer; autoplay; encrypted-media; gyroscope; picture-in-picture"
                                //             allowfullscreen
                                //         />
                                //     )
                                // }}
                                /></Grid>
                            <Grid item xs={1}></Grid>
                        </Grid>


                    </Grid>
                    <Grid item xs={6}>
                        <VictoryChart responsive padding={{ left: 120, top: 30, bottom: 30, right: 20 }} height={200} width={500} theme={VictoryTheme.material} domainPadding={20}>
                            <VictoryAxis
                                // tickValues specifies both the number of ticks and where
                                // they are placed on the axis
                                tickValues={[1, 2, 3, 4]}
                                tickFormat={["Quarter 1", "Quarter 2", "Quarter 3", "Quarter 4"]}
                            />
                            <VictoryAxis
                                dependentAxis
                                // tickFormat specifies how ticks should be displayed
                                tickFormat={(x) => (`$${x / 1000}k`)}
                            />
                            <VictoryBar horizontal
                                data={data}
                                x="quarter"
                                y="earnings"
                            />
                        </VictoryChart>

                    </Grid>
                    <Grid item xs={6}>

                        <VictoryChart padding={{ left: 120, top: 30, bottom: 30, right: 20 }} responsive height={200} width={500} theme={VictoryTheme.material} domainPadding={20}>
                            <VictoryAxis
                                // tickValues specifies both the number of ticks and where
                                // they are placed on the axis
                                tickValues={[1, 2, 3, 4]}
                                tickFormat={["Quarter 1", "Quarter 2", "Quarter 3", "Quarter 4"]}
                            />
                            <VictoryAxis
                                dependentAxis
                                // tickFormat specifies how ticks should be displayed
                                tickFormat={(x) => (`$${x / 1000}k`)}
                            />
                            <VictoryBar horizontal
                                data={data}
                                x="quarter"
                                y="earnings"
                            />
                        </VictoryChart>
                    </Grid>
                    <Grid item xs={6}>
                        <VictoryChart padding={{ left: 120, top: 30, bottom: 30, right: 20 }} responsive height={200} width={500} theme={VictoryTheme.material} domainPadding={20}>
                            <VictoryAxis
                                // tickValues specifies both the number of ticks and where
                                // they are placed on the axis
                                tickValues={[1, 2, 3, 4]}
                                tickFormat={["Quarter 1", "Quarter 2", "Quarter 3", "Quarter 4"]}
                            />
                            <VictoryAxis
                                dependentAxis
                                // tickFormat specifies how ticks should be displayed
                                tickFormat={(x) => (`$${x / 1000}k`)}
                            />
                            <VictoryBar horizontal
                                data={data}
                                x="quarter"
                                y="earnings"
                            />
                        </VictoryChart>
                    </Grid>
                    <Grid item xs={6}>
                        <VictoryChart padding={{ left: 120, top: 30, bottom: 30, right: 20 }} responsive height={200} width={500} theme={VictoryTheme.material} domainPadding={20}>
                            <VictoryAxis
                                // tickValues specifies both the number of ticks and where
                                // they are placed on the axis
                                tickValues={[1, 2, 3, 4]}
                                tickFormat={["Quarter 1", "Quarter 2", "Quarter 3", "Quarter 4"]}
                            />
                            <VictoryAxis
                                dependentAxis
                                // tickFormat specifies how ticks should be displayed
                                tickFormat={(x) => (`$${x / 1000}k`)}
                            />
                            <VictoryBar horizontal
                                data={data}
                                x="quarter"
                                y="earnings"
                            />
                        </VictoryChart>
                    </Grid>
                </Grid>
            </div >
        );
    }
}

export default view(Metrics);

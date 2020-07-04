import React, { Component } from "react";
import { view } from '@risingstack/react-easy-state';
import Paper from '@material-ui/core/Paper';
import Grid from '@material-ui/core/Grid';
import {
    VictoryBar, VictoryChart, VictoryAxis,
    VictoryTheme, VictoryLabel
} from 'victory';
import MaterialTable from "material-table";
import { DateTimePicker } from "@material-ui/pickers";
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
    constructor(props) {
        super(props);
        this.state = {
            StartDate: new Date(),
            EndDate: new Date(),
        };
    }
    onChangeStartDate = (e) => {

        this.setState({ StartDate: e });
    };
    onChangeEndDate = (e) => {

        this.setState({ EndDate: e });
    };
    render() {
        return (
            <div>
                <Grid container spacing={3}>

                    <Grid item xs={2}></Grid>
                    <Grid item xs>
                        <h1>Metrics for Sharp Counter</h1>
                    </Grid>
                    <Grid item xs>
                        <h2>Start Time</h2>
                        <DateTimePicker
                            variant="inline"
                            value={this.state.StartDate}
                            onChange={this.onChangeStartDate}
                            onError={console.log}
                        />

                    </Grid>
                    <Grid item xs>
                        <h2>End Time</h2>

                        <DateTimePicker
                            variant="inline"
                            value={this.state.EndDate}
                            onChange={this.onChangeEndDate}
                            onError={console.log}
                        />

                    </Grid>
                </Grid>
                <Grid container spacing={3}>

                    <Grid item xs={12}>
                        <Grid container spacing={3}>
                            <Grid item xs={1}></Grid>
                            <Grid item xs={10}>
                                <MaterialTable
                                    title="Page Views"
                                    columns={[
                                        { title: 'URL', field: 'name' },
                                        { title: 'Count', field: 'count' },
                                    ]}
                                    data={rows}
                                    detailPanel={rowData => {
                                        return (
                                            <h2>sdf</h2>
                                        )
                                    }}
                                /></Grid>
                            <Grid item xs={1}></Grid>
                        </Grid>
                    </Grid>
                    <Grid item xs={6}>
                        <VictoryChart title="Browser Stats" responsive
                            padding={{ left: 120, top: 30, bottom: 30, right: 20 }}
                            height={170} width={480} theme={VictoryTheme.material} domainPadding={10}>
                            <VictoryLabel text="Browser Stats" x={250} y={20} textAnchor="middle" />
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

                        <VictoryChart padding={{ left: 120, top: 30, bottom: 30, right: 20 }}
                            responsive height={170} width={480}
                            theme={VictoryTheme.material} domainPadding={20}>
                            <VictoryLabel text="System Stats" x={250} y={20} textAnchor="middle" />
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
                        <VictoryChart screen="Screen Stats" padding={{ left: 120, top: 30, bottom: 30, right: 20 }}
                            responsive height={170} width={480}
                            theme={VictoryTheme.material} domainPadding={20}>
                            <VictoryLabel text="Screen Stats" x={250} y={30} textAnchor="middle" />

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
                        <VictoryChart title="Location Stats" padding={{ left: 120, top: 30, bottom: 30, right: 20 }}
                            responsive height={170} width={480}
                            theme={VictoryTheme.material} domainPadding={20}>
                            <VictoryLabel text="Location Stats" x={250} y={30} textAnchor="middle" />

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

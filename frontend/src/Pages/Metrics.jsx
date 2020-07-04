import React, { Component } from "react";
import {
    XYPlot,
    XAxis,
    YAxis,
    HorizontalBarSeries,
    VerticalGridLines,
    HorizontalGridLines,
} from 'react-vis';
class Metrics extends Component {
    render() {
        return (
            <div>
                <XYPlot width={1000} height={300} stackBy="x"
                    xDomain={[
                        0,
                        20
                    ]}
                    yDomain={[
                        0,
                        8
                    ]}
                >
                    <VerticalGridLines />
                    <HorizontalGridLines />
                    <XAxis />
                    <YAxis />
                    <HorizontalBarSeries
                        data={[
                            {
                                x: 10,
                                y: 0
                            },
                            {
                                x: 10.243957399656558,
                                y: 1
                            },
                            {
                                x: 9.827327392945978,
                                y: 2
                            },
                            {
                                x: 10.344640987364263,
                                y: 3
                            },
                            {
                                x: 10.534826267984268,
                                y: 4
                            },
                            {
                                x: 10.458216906994641,
                                y: 5
                            },
                            {
                                x: 9.83044773586021,
                                y: 6
                            },
                            {
                                x: 11.311085114735427,
                                y: 7
                            },
                            {
                                x: 12.720380871251928,
                                y: 8
                            }
                        ]}
                        style={{}}
                    />
                </XYPlot>
            </div>
        );
    }
}

export default Metrics;

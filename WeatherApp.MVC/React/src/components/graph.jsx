import React, { Component } from 'react';
import Chart from 'chart.js/auto';

class Graph extends Component {
    constructor(props) {
        super(props);
        this.chartRef = React.createRef();
        this.state = {
            graphData: [],
        };
    }

    componentDidMount() {
        this.loadData();
        this.interval = setInterval(() => this.loadData(), 60000); // Refresh every minute
    }

    componentWillUnmount() {
        clearInterval(this.interval);
    }

    loadData = async () => {
        try {
            const response = await fetch('/GraphData');
            const rawData = await response.json();
            const graphData = this.processData(rawData);

            console.log("GraphData");
            console.log(graphData);

            this.setState({ graphData }, () => this.updateGraph());
        } catch (error) {
            console.error('Error fetching data:', error);
        }
    };

    processData(data) {        
        const result = data.reduce((acc, curr) => {

            console.log("curr");
            console.log(curr);

            const key = `${curr.country}/${curr.city}`;
            if (!acc[key]) {
                acc[key] = {
                    ...curr,
                    minTemp: curr.temperatureC,
                    maxTemp: curr.temperatureC,
                    lastUpdated: curr.lastUpdated
                };
            } else {
                acc[key].minTemp = Math.min(acc[key].minTemp, curr.temperatureC);
                acc[key].maxTemp = Math.max(acc[key].maxTemp, curr.temperatureC);
                if (new Date(curr.lastUpdated) > new Date(acc[key].lastUpdated)) {
                    acc[key].lastUpdated = curr.lastUpdated;
                }
            }

            console.log("acc");
            console.log(acc);

            return acc;
        }, {});

        return Object.values(result);
    }

    updateGraph() {
        const { graphData } = this.state;
        
        const lastUpdatedTimes = graphData.map(d => d.lastUpdated);

        const chartData = {
            labels: graphData.map(d => `${d.country} - ${d.city}`),
            datasets: [{
                label: 'Min Temperature (C)',
                data: graphData.map(d => d.minTemp),
                borderColor: 'rgb(75, 192, 192)',
                backgroundColor: 'rgba(75, 192, 192, 0.2)',
            }, {
                label: 'Max Temperature (C)',
                data: graphData.map(d => d.maxTemp),
                borderColor: 'rgb(255, 99, 132)',
                backgroundColor: 'rgba(255, 99, 132, 0.2)',
            }]
        };

        const footer = (tooltipItems) => {
            const index = tooltipItems[0].dataIndex;
            return `Last Updated: ${lastUpdatedTimes[index]}`;
        };

        if (this.chart) {
            this.chart.data = chartData;
            this.chart.update();
        } else {
            this.chart = new Chart(this.chartRef.current.getContext('2d'), {
                type: 'bar',
                data: chartData,
                options: {
                    scales: {
                        y: {
                            beginAtZero: true
                        }
                    },
                    plugins: {
                        tooltip: {
                            callbacks: {
                                footer: footer,
                            }
                        }
                    }
                }
            });
        }
    }

    render() {
        return <div className="chart-container" style={{ position: 'relative', height: '50vh' }}>
            <canvas ref={this.chartRef}></canvas>
        </div>;
    }
}

export default Graph;
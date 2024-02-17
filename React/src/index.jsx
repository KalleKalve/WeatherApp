import React from 'react';
import ReactDOM from 'react-dom';
import Graph from './components/graph';

const App = () => {
    return (
        <div>
            <h1>Min and Max temperature</h1>
            <Graph />
        </div>
    );
};

ReactDOM.render(<App />, document.getElementById('root'));
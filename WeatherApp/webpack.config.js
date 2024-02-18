const path = require('path');

module.exports = {
    entry: './React/src/index.jsx', // Entry point of your React app
    output: {
        path: path.resolve(__dirname, 'wwwroot/dist'), // Output directory
        filename: 'bundle.js' // Output file
    },
    module: {
        rules: [
            {
                test: /\.(js|jsx)$/, // Files to apply this rule to
                exclude: /node_modules/, // Ignore node_modules
                use: {
                    loader: 'babel-loader', // Use babel-loader to transpile files
                    options: {
                        presets: ['@babel/preset-env', '@babel/preset-react'] // Presets to use
                    }
                }
            }
        ]
    },
    resolve: {
        extensions: ['.js', '.jsx'] // Automatically resolve these extensions
    }
};
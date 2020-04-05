const path = require("path");

module.exports = {
    entry: './src/index.js',
    output: {
        filename: 'main.js'
    },
    watch: true,
    devtool: 'source-map',
    module: {
        rules: [
            {
                test: /\.jsx?$/,
                exclude: /node_modules/,
                loader: "babel-loader"
            }
        ]
    }
};
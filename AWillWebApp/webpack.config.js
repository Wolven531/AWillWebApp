//const ExtractTextPlugin = require('extract-text-webpack-plugin')
const path = require('path')
const webpack = require('webpack')
const DashboardPlugin = require('webpack-dashboard/plugin')

const inputEntryFile = 'app.js'
const outputFile = 'bundle.js'
const mode = 'development'

module.exports = {
	context: __dirname,
	devtool: 'source-map',
	entry: path.resolve(__dirname, 'wwwroot', 'Source', inputEntryFile),
	mode,
	optimization: {
		minimize: true
	},
	output: {
		filename: outputFile,
		path: path.resolve(__dirname, 'wwwroot', 'Dist')
	},
	plugins: [
		new webpack.ProgressPlugin({ profile: false }),
		//new webpack.optimize.CommonsChunkPlugin({
		//	name: 'vendor',
		//	filename: 'vendor-[hash].min.js',
		//}),
		//new ExtractTextPlugin({
		//	filename: 'build.min.css',
		//	allChunks: true,
		//}),
		new webpack.IgnorePlugin(/^\.\/locale$/, /moment$/),
		new webpack.DefinePlugin({
			'process.env.NODE_ENV': '"production"',
		}),
		new DashboardPlugin()
		//new webpack.HotModuleReplacementPlugin()
	],
	target: 'web'
}

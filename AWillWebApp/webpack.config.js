//const ExtractTextPlugin = require('extract-text-webpack-plugin')
const path = require('path')
const webpack = require('webpack')
const DashboardPlugin = require('webpack-dashboard/plugin')
//const jQuery = require('jquery')

const inputEntryFile = 'app.js'
const outputFile = 'bundle.js'
const mode = 'development'

//window.$ = window.jQuery = jQuery

module.exports = {
	context: __dirname,
	devtool: 'source-map',
	externals: [
		'window'
	],
	entry: path.resolve(__dirname, 'wwwroot', 'Source', inputEntryFile),
	mode,
	module: {
		rules: [
			// NOTE: does not actually include jQuery; possibly outdated technique
			//{
			//	test: require.resolve('jquery'),
			//	use: [{
			//		loader: 'expose-loader',
			//		options: '$'
			//	}]
			//}
			{
				test: /\.jsx?$/,
				use: [
					{
						loader: 'babel-loader',
						options: {
							presets: ['@babel/preset-env']
						}
					}
				]
			}
		]
	
	},
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
		// NOTE: does not actually include jQuery; possibly outdated technique
		//new webpack.ProvidePlugin({
		//	$: 'jquery',
		//	jQuery: 'jquery',
		//	'window.jQuery': 'jquery'
		//}),
		new DashboardPlugin(),
		new webpack.HotModuleReplacementPlugin()
	],
	target: 'web'
}

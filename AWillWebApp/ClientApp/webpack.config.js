// const ExtractTextPlugin = require('extract-text-webpack-plugin')
const path = require('path')
// const webpack = require('webpack')
//const DashboardPlugin = require('webpack-dashboard/plugin')
//const { CheckerPlugin } = require('awesome-typescript-loader')
//const CheckerPlugin = require('awesome-typescript-loader').CheckerPlugin
////const jQuery = require('jquery')

//const extractCSS = new ExtractTextPlugin('allstyles.css')
//const inputEntryFile = 'app.tsx'
//const outputFile = 'bundle.js'
// const mode = 'development'

// const bundleOutputDir = './ClientApp/dist/'

//window.$ = window.jQuery = jQuery

module.exports = (env) => {
	const isDevBuild = !(env && env.prod)
	return [{
		stats: 'verbose',//'normal'|'verbose'
		entry: {
			//main: path.relative(
			//	__dirname,
			//	path.join('ClientApp', 'src', 'app.tsx')
			//)
			//main: './ClientApp/src/app.tsx'
			main: path.resolve(__dirname, 'src', 'app.tsx')
		},
		resolve: { extensions: ['.js', '.jsx', '.ts', '.tsx'] },
		output: {
			path: path.resolve(__dirname, 'dist'),
			filename: '[name].js',
			publicPath: 'dist/'
		},
		// mode,
		module: {
			rules: [
				{
					test: /\.(jsx?)$/,
					include: /src/,
					use: { loader: 'babel-loader' }
					// exclude: [
					// 	path.resolve(__dirname, 'ClientApp', 'node_modules')
					// ]
				},
				{
					test: /\.tsx?$/,
					include: /src/,
					use: 'ts-loader'
					// exclude: [
					// 	path.resolve(__dirname, 'ClientApp', 'node_modules')
					// ]
				},
				{
					test: /\.css$/,
					use: ['style-loader', 'css-loader']
					// use: isDevBuild ?
					// 	['style-loader', 'css-loader'] :
					// 	ExtractTextPlugin.extract({ use: 'css-loader?minimize' })
				},
				{ test: /\.(png|jpg|jpeg|gif|svg)$/, use: 'url-loader?limit=25000' }
			]
		}
		// plugins: [
		// 	//new CheckerPlugin(),
		// 	//new webpack.DllReferencePlugin({
		// 	//	context: __dirname,
		// 	//	manifest: require('./ClientApp/dist/vendor-manifest.json')
		// 	//})
		// ].concat(isDevBuild ? [
		// 	// Plugins that apply in development builds only
		// 	//new webpack.SourceMapDevToolPlugin({
		// 	//	filename: '[file].map', // Remove this line if you prefer inline source maps
		// 	//	moduleFilenameTemplate: path.relative(bundleOutputDir, '[resourcePath]') // Point sourcemap entries to the original file locations on disk
		// 	//})
		// ] : [
		// 	// Plugins that apply in production builds only
		// 	//new webpack.optimize.UglifyJsPlugin(),
		// 	//new ExtractTextPlugin('allstyles.css')
		// ])

		//context: __dirname,
		////devtool: 'source-map',
		//externals: {
		//	jQuery: 'jQuery',
		//	window: 'window'
		//},
		//entry: {
		//	main: [path.resolve(__dirname, 'wwwroot', 'src', inputEntryFile)]
		//},
		//mode,
		//module: {
		//	rules: [
		//		// NOTE: does not actually include jQuery; possibly outdated technique
		//		//{
		//		//	test: require.resolve('jquery'),
		//		//	use: [{
		//		//		loader: 'expose-loader',
		//		//		options: '$'
		//		//	}]
		//		//}
		//		{
		//			test: /\.css$/,
		//			//use: [
		//			//	{ loader: 'style-loader' },
		//			//	{ loader: 'css-loader' }
		//			//]
		//			use: extractCSS.extract(['css-loader?minimize'])
		//		},
		//		{
		//			test: /\.tsx?$/,
		//			loader: 'awesome-typescript-loader'
		//		},
		//		{
		//			test: /\.jsx?$/,
		//			use: [
		//				{
		//					loader: 'babel-loader',
		//					options: {
		//						presets: ['@babel/preset-react', '@babel/preset-env']
		//					}
		//				}
		//			]
		//		}
		//	]

		//},
		//optimization: {
		//	minimize: true
		//},
		//output: {
		//	filename: outputFile,
		//	path: path.resolve(__dirname, 'wwwroot', 'Dist')
		//},
		//plugins: [
		//	extractCSS,
		//	new webpack.ProgressPlugin({ profile: false }),
		//	//new webpack.optimize.CommonsChunkPlugin({
		//	//	name: 'vendor',
		//	//	filename: 'vendor-[hash].min.js',
		//	//}),
		//	//new ExtractTextPlugin({
		//	//	filename: 'build.min.css',
		//	//	allChunks: true,
		//	//}),
		//	new CheckerPlugin(),
		//	new webpack.IgnorePlugin(/^\.\/locale$/, /moment$/),
		//	new webpack.DefinePlugin({
		//		'process.env.NODE_ENV': '"production"',
		//	}),
		//	// NOTE: does not actually include jQuery; possibly outdated technique
		//	//new webpack.ProvidePlugin({
		//	//	$: 'jquery',
		//	//	jQuery: 'jquery',
		//	//	'window.jQuery': 'jquery',
		//	//	Popper: ['popper.js', 'default']
		//	//}),
		//	//new DashboardPlugin(),
		//	new webpack.HotModuleReplacementPlugin()
		//],
		//target: 'web'
	}]
}

const chromeLauncher = require('chrome-launcher')
const fs = require('fs')
const lighthouse = require('lighthouse')
const path = require('path')

const targetUrl = 'https://localhost:5001/'
const opts = {
	chromeFlags: ['--show-paint-rects']
}
const launchChromeAndRunLighthouse = (url, opts, config = null) => {
	return chromeLauncher.launch({ chromeFlags: opts.chromeFlags }).then(chrome => {
		opts.port = chrome.port
		return lighthouse(url, opts, config).then(results => {
			// use results.lhr for the JS-consumeable output
			// https://github.com/GoogleChrome/lighthouse/blob/master/typings/lhr.d.ts
			// use results.report for the HTML/JSON/CSV output as a string
			// use results.artifacts for the trace/screenshots/other specific case you need (rarer)
			return chrome.kill().then(() => results.lhr)
		})
	})
}

launchChromeAndRunLighthouse(targetUrl, opts).then(results => {
	if (!results) {
		console.warn(`Finished run of ${targetUrl}, no results!`)
		return
	}

	const resultsPath = path.join(__dirname, 'lighthouse_results.json')
	const fileOptions = { encoding: 'utf8', flag: 'w' }

	console.log(`Finished run of ${targetUrl}, writing results to disk...`)
	fs.writeFileSync(resultsPath, JSON.stringify(results, null, 4), fileOptions)
	console.log(`Lighthouse results written to ${resultsPath}`)
})

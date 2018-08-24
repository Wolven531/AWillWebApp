/*
 * Author: Anthony Williams
 * Version: Cassiopeia
 * Date: 08/24/2018
 * Target Site: summonerswarskyarena.info
 * Target URL: https://summonerswarskyarena.info/monster-list/
 * Purpose: Grab all tr elements on page and parse them to create basic data model objects
 */

const getBase64Image = img => {
	const canvas = document.createElement('canvas')
	const { height, width } = img
	canvas.width = width
	canvas.height = height

	const ctx = canvas.getContext('2d')
	ctx.drawImage(img, 0, 0, width, height)

	const dataURL = canvas.toDataURL('image/png')

	// NOTE: remove data type for lighter storage footprint
	return dataURL.replace(/^data:image\/(png|jpg|gif);base64,/, '')
}

const DOMElementsToImageInfo = imageDOMElements => {
	let awakenedImage = ''
	let awakenedImageSource = ''
	let image = ''
	let imageSource = ''

	if (imageDOMElements && imageDOMElements.length > 1) {
		const awakenedImageDOMElements = imageDOMElements[1].getElementsByTagName('img')
		const normalImageDOMElements = imageDOMElements[0].getElementsByTagName('img')

		if (awakenedImageDOMElements && awakenedImageDOMElements.length > 0) {
			awakenedImageSource = awakenedImageDOMElements[0].getAttribute('src')
			awakenedImage = getBase64Image(awakenedImageDOMElements[0])
		}

		if (normalImageDOMElements && normalImageDOMElements.length > 0) {
			imageSource = normalImageDOMElements[0].getAttribute('src')
			image = getBase64Image(normalImageDOMElements[0])
		}
	}

	return {
		awakenedImage,
		awakenedImageSource,
		image,
		imageSource
	}
}

const DOMElementsToRuneInfo = runesDOMElements => {
	let runeList = ''
	let runeValues = ''

	if (runesDOMElements && runesDOMElements.length > 0) {
		const runesDOMElement = runesDOMElements[0]
		const runeListDOMElements = runesDOMElement.getElementsByClassName('rune-list')
		const runeValuesDOMElements = runesDOMElement.getElementsByClassName('rune-values')

		if (runeListDOMElements && runeListDOMElements.length > 0) {
			runeList = runeListDOMElements[0].textContent
		}

		if (runeValuesDOMElements && runeValuesDOMElements.length > 0) {
			runeValues = runeValuesDOMElements[0].textContent
		}
	}

	return {
		runeList,
		runeValues
	}
}

const DOMRowToMonster = rowDOM => {
	const nameElements = rowDOM.getElementsByClassName('name')
	const rating = parseInt(rowDOM.getElementsByClassName('rating')[0].textContent, 10)
	const statPriority = rowDOM.getElementsByClassName('stat-priority')[0].textContent
	const awakenedName = nameElements[1].textContent

	const currentAnchor = nameElements[0].getElementsByTagName('a')[0]
	const element = currentAnchor.getElementsByClassName('element')[0].textContent
	const name = currentAnchor.getElementsByTagName('h3')[0].textContent

	const earlyRuneInfo = DOMElementsToRuneInfo(rowDOM.getElementsByClassName('early-runes'))
	const lateRuneInfo = DOMElementsToRuneInfo(rowDOM.getElementsByClassName('late-runes'))

	const imageInfo = DOMElementsToImageInfo(rowDOM.getElementsByClassName('icon'))

	return {
		awakenedName,
		earlyRuneList: earlyRuneInfo.runeList,
		earlyRuneValues: earlyRuneInfo.runeValues,
		element,
		lateRuneList: lateRuneInfo.runeList,
		lateRuneValues: lateRuneInfo.runeValues,
		name,
		rating,
		statPriority,
		...imageInfo
	}
}

const parseMonsters = () => {
	const allMonsterRows = $('tr.searchable')
	const generatedMonsters = []

	for (let index = 0; index < allMonsterRows.length; index++) {
		generatedMonsters.push(DOMRowToMonster(allMonsterRows[index]))
	}

	return generatedMonsters
}

const launchScroller = () => {
	const bottomThreshold = 750
	const parseDelay = 1000
	const scrollAmount = 250
	const scrollInterval = 75
	const launchParser = () => {
		console.log('about to parse monsters...')
		monsters = parseMonsters()
	}
	const runScrollCheck = () => {
		const distanceToBottom = document.body.offsetHeight - window.pageYOffset
		if (distanceToBottom <= bottomThreshold) {
			window.clearInterval(scrollTimer)
			console.log(`delay for ${parseDelay} millis for monster images load...`)
			window.setTimeout(launchParser, parseDelay)
			return
		}
		window.scrollTo({
			behavior: 'smooth',
			top: window.pageYOffset + scrollAmount
		})
	}
	const scrollTimer = window.setInterval(runScrollCheck, scrollInterval)
}

let monsters = []
launchScroller()

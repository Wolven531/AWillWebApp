/*
 * Author: Anthony Williams
 * Version: Baretta
 * Date: 07/30/2018
 * Target Site: summonerswarskyarena.info
 * Target URL: https://summonerswarskyarena.info/monster-list/
 * Purpose: Grab all tr elements on page and parse them to create basic data model objects
 */

const DOMElementsToRuneInfo = (runesDOMElements) => {
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

const DOMRowToMonster = (rowDOM) => {
	const nameElements = rowDOM.getElementsByClassName('name')
	const rating = parseInt(rowDOM.getElementsByClassName('rating')[0].textContent, 10)
	const statPriority = rowDOM.getElementsByClassName('stat-priority')[0].textContent
	const awakenedName = nameElements[1].textContent

	const currentAnchor = nameElements[0].getElementsByTagName('a')[0]
	const element = currentAnchor.getElementsByClassName('element')[0].textContent
	const name = currentAnchor.getElementsByTagName('h3')[0].textContent

	const earlyRuneInfo = DOMElementsToRuneInfo(rowDOM.getElementsByClassName('early-runes'))
	const lateRuneInfo = DOMElementsToRuneInfo(rowDOM.getElementsByClassName('late-runes'))

	return {
		awakenedName,
		earlyRuneList: earlyRuneInfo.runeList,
		earlyRuneValues: earlyRuneInfo.runeValues,
		element,
		lateRuneList: lateRuneInfo.runeList,
		lateRuneValues: lateRuneInfo.runeValues,
		name,
		rating,
		statPriority
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

const monsters = parseMonsters()

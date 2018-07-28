/*
 * Author: Anthony Williams
 * Version: Alexandra
 * Date: 07/28/2018
 * Target Site: summonerswarskyarena.info
 * Target URL: https://summonerswarskyarena.info/monster-list/
 * Purpose: Grab all tr elements on page and parse them to create basic data model objects
 */

const DOMRowToMonster = (rowDOM) => {
	const nameElements = rowDOM.getElementsByClassName('name')
	const awakenedName = nameElements[1].textContent

	const currentAnchor = nameElements[0].getElementsByTagName('a')[0]
	const element = currentAnchor.getElementsByClassName('element')[0].textContent
	const name = currentAnchor.getElementsByTagName('h3')[0].textContent

	return {
		awakenedName,
		element,
		name
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

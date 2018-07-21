export default class ES6Lib {
	_text: string

	constructor() {
		this._text = 'Data from ES6 class'
	}

	getData = (): string => this._text
}

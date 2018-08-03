// TODO: find a way to avoid including
// jquery in app entry as follows
import * as jQuery from 'jquery'
const $ = (window as any).$ = (window as any).jQuery = jQuery

import * as React from 'react'
import * as ReactDOM from 'react-dom'

import { getText } from './lib'
import ES6Lib from './es6lib'
import Counter from './counter'
import MonsterSearcher from './MonsterSearcher'

//import 'bootstrap/dist/css/bootstrap.min.css'
import '../css/site.css'

//// NOTE: vanilla JS
//document.getElementById('fillthis').appendChild(document.createTextNode(getText()))

//// NOTE: jQuery
//$('#fillthiswithjquery').html('Filled by Jquery!')

//// NOTE: ES6
//const myES6Object = new ES6Lib()
//$('#fillthiswithes6lib').html(myES6Object.getData())

class App extends React.Component {
	public componentDidMount() {
		document.title = "Home"
	}
	
	public render() {
		return (
			// <React.Fragment>
			// 	<Counter />
			// </React.Fragment>
			<MonsterSearcher />
		)
	}
}

ReactDOM.render(<App />, document.getElementById('app'))

//// NOTE: ReactJS
//ReactDOM.render(
//	<Counter />,
//	document.getElementById('basicreactcomponent')
//)

//// NOTE: ReactJS with API
//ReactDOM.render(
//	<FetchData />,
//	document.getElementById('reactcomponentwithapidata')
//)

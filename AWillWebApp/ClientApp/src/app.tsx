// TODO: find a way to avoid including
// jquery in app entry as follows
import * as jQuery from 'jquery'
const $ = ((window as any).$ = (window as any).jQuery = jQuery)

import * as React from 'react'
import * as ReactDOM from 'react-dom'

import { BrowserHistoryBuildOptions, createBrowserHistory } from 'history'
import { Provider } from 'react-redux'
import { Redirect, Route, Router } from 'react-router-dom'
// import { ConnectedRouter } from 'react-router-redux'

import configureStore from './store/configureStore'

// import Counter from './counter'
// import ES6Lib from './es6lib'
// import { getText } from './lib'
// import MonsterSearcher from './MonsterSearcher'
import { Login } from './Login'

// import 'bootstrap/dist/css/bootstrap.min.css'
import '../css/site.css'

//// NOTE: vanilla JS
// document.getElementById('fillthis').appendChild(document.createTextNode(getText()))

//// NOTE: jQuery
// $('#fillthiswithjquery').html('Filled by Jquery!')

//// NOTE: ES6
// const myES6Object = new ES6Lib()
// $('#fillthiswithes6lib').html(myES6Object.getData())

// Create browser history to use in the Redux store
const baseNode = document.getElementsByTagName('base')[0]
const baseUrl = baseNode ? baseNode.getAttribute('href') : '/'
const history = createBrowserHistory({
	basename: baseUrl
} as BrowserHistoryBuildOptions)

// Get the application-wide store instance, prepopulating with state from the server where available.
// const initialState = (window as any).initialReduxState
// const store = configureStore(history, initialState)
const store = configureStore(history)

const HomePage = () => <div>Homepage</div>

const PrivateRoute = ({ component: Component, ...rest }: any) => (
	<Route
		{...rest}
		render={props =>
			localStorage.getItem('user') ? (
				<Component {...props} />
			) : (
				<Redirect
					to={{ pathname: '/login', state: { from: props.location } }}
				/>
			)
		}
	/>
)

class App extends React.Component {
	public componentDidMount() {
		document.title = 'Home'
	}

	public render() {
		return (
			// <React.Fragment>
			// 	<Counter />
			// </React.Fragment>
			// <MonsterSearcher />
			// TODO: find out why Provider store is not enough for compiler
			<Provider store={store}>
				{/*
				<ConnectedRouter history={history}>
					<Login store={store} />
				</ConnectedRouter>
				*/}
				<Router history={history}>
					<React.Fragment>
						<PrivateRoute exact path="/" component={HomePage} />
						<Route path="/login" component={Login} />
					</React.Fragment>
				</Router>
			</Provider>
		)
	}
}

ReactDOM.render(<App />, document.getElementById('app'))

//// NOTE: ReactJS
// ReactDOM.render(
// 	<Counter />,
// 	document.getElementById('basicreactcomponent')
// )

//// NOTE: ReactJS with API
// ReactDOM.render(
// 	<MonsterSearcher />,
// 	document.getElementById('reactcomponentwithapidata')
// )

// // TODO: find a way to avoid including
// // jquery in app entry as follows
// import * as jQuery from 'jquery'
// const $ = ((window as any).$ = (window as any).jQuery = jQuery)

import { BrowserHistoryBuildOptions, createBrowserHistory } from 'history'
import * as React from 'react'
import * as ReactDOM from 'react-dom'
import { Provider } from 'react-redux'
import { Redirect, Route, Router } from 'react-router-dom'

import configureStore from './store/configureStore'

// import MonsterSearcher from './MonsterSearcher'
import { AllMonstersPage } from './containers/pages/AllMonstersPage'
import { HomePage } from './containers/pages/HomePage'
import { MyMonstersPage } from './containers/pages/MyMonstersPage'

import { Footer } from './components/Footer/Footer'
import { Header } from './components/Header/Header'
import { NavBar } from './components/NavBar/NavBar'
import { Login } from './containers/Login/Login'

import '../css/site.css'

// Create browser history to use in the Redux store
const baseNode = document.getElementsByTagName('base')[0]
const baseUrl = baseNode ? baseNode.getAttribute('href') : '/'
const history = createBrowserHistory({
	basename: baseUrl
} as BrowserHistoryBuildOptions)

// NOTE: Get the application-wide store instance, prepopulating with state from the server where available.
// const initialState = (window as any).initialReduxState
// const store = configureStore(history, initialState)
const store = configureStore(history)

const PrivateRoute = ({ component: Component, ...rest }: any) => (
	<Route
		{...rest}
		render={props => {
			const user = localStorage.getItem('user')
			return user && user !== 'undefined' ? (
				<Component {...props} />
			) : (
				<Redirect to={{ pathname: '/login', state: { from: props.location } }} />
			)
		}}
	/>
)

class App extends React.Component {
	public componentDidMount() {
		document.title = 'Home'
	}

	public render() {
		return (
			<div id="page-container">
				<Header />
				<NavBar />
				<div id="content-wrap">
					<Provider store={store}>
						<Router history={history}>
							<React.Fragment>
								<PrivateRoute exact path="/" component={HomePage} />
								<PrivateRoute exact path="/my-monsters" component={MyMonstersPage} />
								<Route path="/login" component={Login} />
								<Route path="/monsters" component={AllMonstersPage} />
							</React.Fragment>
						</Router>
					</Provider>
				</div>
				<Footer />
			</div>
		)
	}
}

ReactDOM.render(<App />, document.getElementById('app'))

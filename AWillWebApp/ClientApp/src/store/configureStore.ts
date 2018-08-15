import { routerMiddleware, routerReducer } from 'react-router-redux'
import { applyMiddleware, combineReducers, compose, createStore } from 'redux'
import thunk from 'redux-thunk'
import { reducer as authentication } from './Authentication'
// import * as Counter from './Counter'
// import * as WeatherForecasts from './WeatherForecasts'

export default function configureStore(history: any) {
// export default function configureStore(history: any, initialState: any) {
	const reducers = {
		authentication
		// counter: Counter.reducer
		// weatherForecasts: WeatherForecasts.reducer
	}

	const middleware = [
		thunk,
		routerMiddleware(history)
	]

	// In development, use the browser's Redux dev tools extension if installed
	const enhancers = []
	const isDevelopment = process.env.NODE_ENV === 'development'
	if (isDevelopment && typeof window !== 'undefined' && (window as any).devToolsExtension) {
		enhancers.push((window as any).devToolsExtension())
	}

	// NOTE: need to cast as any to make the TSC happy
	const rootReducer = combineReducers({
		...reducers,
		routing: routerReducer
	} as any)

	return createStore(
		rootReducer,
		compose(applyMiddleware(...middleware), ...enhancers)
	)
	// return createStore(
	// 	rootReducer,
	// 	initialState,
	// 	compose(applyMiddleware(...middleware), ...enhancers)
	// )
}

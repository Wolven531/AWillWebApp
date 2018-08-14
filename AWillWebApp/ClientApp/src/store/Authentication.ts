// const incrementCountType = 'INCREMENT_COUNT'
// const decrementCountType = 'DECREMENT_COUNT'
const initialState = { loggedIn: false }

export const actionCreators = {
	// increment: () => ({ type: incrementCountType }),
	// decrement: () => ({ type: decrementCountType })
}

export const reducer = (state: any, action: any) => {
	state = state || initialState

	// if (action.type === incrementCountType) {
	// 	return { ...state, count: state.count + 1 }
	// }

	// if (action.type === decrementCountType) {
	// 	return { ...state, count: state.count - 1 }
	// }

	return state
}

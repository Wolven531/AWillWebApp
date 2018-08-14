const incrementCountType = 'INCREMENT_COUNT'
const decrementCountType = 'DECREMENT_COUNT'
const initialState = { count: 0 }

export const actionCreators = {
	decrement: () => ({ type: decrementCountType }),
	increment: () => ({ type: incrementCountType })
}

export const reducer = (state: any, action: any) => {
	state = state || initialState

	if (action.type === incrementCountType) {
		return { ...state, count: state.count + 1 }
	}

	if (action.type === decrementCountType) {
		return { ...state, count: state.count - 1 }
	}

	return state
}
